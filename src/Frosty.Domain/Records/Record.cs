using Frosty.Domain.Framework;
using Frosty.Domain.Records.Events;
using Frosty.Domain.Records.Services;
using Frosty.Domain.Shared;

namespace Frosty.Domain.Records;

public sealed class Record : Entity {

    private Record(
        Guid id,
        ContactInfo primaryContact,
        Website website,
        LeadStatus leadStatus,
        DateTime createDate,
        List<ContactInfo>? secondaryContacts = null
    ) : base(id) {
        PrimaryContact = primaryContact;
        Website = website;
        LeadStatus = leadStatus;
        CreateDate = createDate;
        SecondaryContacts = secondaryContacts;
    }

    public ContactInfo PrimaryContact { get; private set; }
    public List<ContactInfo>? SecondaryContacts { get; private set; }

    // The website is a unique field. A person can own multiple companies.
    // Meaning, the system can reference the same person in different companies.
    public Website Website { get; private set; }
    public DateTime? WebsiteVerifyDate { get; private set; }

    public EmailVerifyId? EmailVerifyId { get; private set; }
    public DateTime? EmailVerifyDate { get; private set; }
    public List<EmailVerificationResponse>? EmailVerifyList = null;

    public LeadStatus LeadStatus { get; private set; }
    public List<Comment>? Comments { get; private set; }

    // NOTE: if the verification or website service failed, Reject.
    public DateTime? RejectDate { get; private set; }
    public DateTime CreateDate { get; private set; }

    public async static Task<Result<Record>> Create(
        Name<Firstname> firstname,
        Name<Lastname> lastname,
        Result<Website> websiteResult,
        DateTime createDate,
        IRecordCheckDuplicateService service,
        LeadStatus leadStatus = LeadStatus.New,
        List<ContactInfo>? secondaryContacts = null
    ) {

        if (websiteResult.Error == RecordErrors.WebsiteRejected) {
            return Result.Failure<Record>(RecordErrors.WebsiteRejected);
        }

        var website = websiteResult._value;

        var recordCheck = await service.Check(website);

        if (recordCheck == false) {
            return Result.Failure<Record>(RecordErrors.DuplicateWebsite);
        }


        var record = new Record(
            Guid.NewGuid(),
            new ContactInfo(firstname, lastname),
            website,
            leadStatus,
            DateTime.UtcNow
        );

        record.ChangeLeadStatus(LeadStatus.WebsiteValid);

        record.AddDomainEvent(new RecordCreatedDomainEvent(record.Id));

        return Result.Success<Record>(record);
    }

    public Result ChangeLeadStatus(LeadStatus ls) {

        // rejected records cannot be changed.
        if (LeadStatus == LeadStatus.Rejected) {
            return Result.Failure(RecordErrors.RejectedRecord);
        }

        LeadStatus = ls;
        return Result.Success();
    }

    public void AddEmail(Result<Email> emailResult) {
        this.PrimaryContact.Email = emailResult._value;
    }

    public bool AddComment(string name, string description) {
        var comment = Comment.Create(name, description);

        if (comment.Error == RecordErrors.BlankValue) {
            return false;
        }

        Comments?.Add(comment._value);

        return true;
    }

    public async Task<Result> VerifyEmailGuesses(
        IEmailVerificationService service
    ) {

        // Can only verify new records
        if (LeadStatus != LeadStatus.WebsiteValid) {
            return Result.Failure(RecordErrors.UnableToVerify);
        }

        var email = PrimaryContact.Email;

        // if email guess list is blank
        if (email?.EmailGuessList?.Count < 1) {
            return Result.Failure(RecordErrors.VerifyListEmpty);
        }

        var verifyResponse = await service.Send(
            Id,
            email?.EmailGuessList);

        var results = verifyResponse._value;

        // if any emails pass, return true
        var scanForPassed = results.Any(item =>
            item.EmailStatus == EmailGuessStatus.Passed
        );

        // if no Passes, then reject record
        if (scanForPassed == false) {
            this.AddDomainEvent(new RejectRecordDomainEvent(Id));
            ChangeLeadStatus(LeadStatus.Rejected);
            return Result.Success();
        }

        // else, add data to EmailVerifyList and update lead status
        ChangeLeadStatus(LeadStatus.EmailVerified);
        EmailVerifyList = verifyResponse._value;
        return Result.Success();
    }


    // records that are not ready to be added are simply ignored
    public void AddRecordToSendingQueue() {

        // if there are no email guesses.
        if (EmailVerifyList == null) {
            return;
        }

        if (RejectDate is not null || LeadStatus == LeadStatus.Rejected) {
            return;
        }

        if (PrimaryContact.Firstname is null ||
            PrimaryContact.Lastname is null ||
            // If email is present, then email is verified
            PrimaryContact.Email is null
        ) {

            return;
        }

        // check if website verification is true
        if (LeadStatus == LeadStatus.New) {
            return;
        }

        // if all these are true, change lead status to ReadyToSend
        // This should be an event - The handler will be in the app layer
        // AddToPipeline function in EmailPipelineCard Entity should run
        // in an event handler

        this.AddDomainEvent(new AddToEmailPipelineDomainEvent(Id));

    }

}
