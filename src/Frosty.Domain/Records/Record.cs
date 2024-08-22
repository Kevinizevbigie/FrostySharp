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
        // BUG: Email shouldn't be required here. Removed.
        Website website,
        DateTime createDate,
        IRecordCheckDuplicateService service,
        LeadStatus leadStatus = LeadStatus.New,
        List<ContactInfo>? secondaryContacts = null
    ) {

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

        record.AddDomainEvent(new RecordCreatedDomainEvent(record.Id));

        return Result.Success<Record>(record);
    }

    public Result ChangeLeadStatus(LeadStatus ls) {

        // rejected records cannot be changed.
        if (LeadStatus == LeadStatus.Rejected) {
            Result.Failure(RecordErrors.RejectedRecord);
        }
        LeadStatus = ls;
        return Result.Success();
    }

    public async Task VerifyEmailGuesses(
        IEmailVerificationService service
    ) {

        var verifyResponse = await service.Send(Id);
        var results = verifyResponse._value;

        // if any emails pass, return true
        var scanForPassed = results.Any(item =>
            item.EmailStatus == EmailGuessStatus.Passed
        );

        // if no Passes, then reject record
        if (scanForPassed == false) {
            this.AddDomainEvent(new RejectRecordDomainEvent(Id));
            ChangeLeadStatus(LeadStatus.Rejected);
            return;
        }

        // else, add data to EmailVerifyList and update lead status
        ChangeLeadStatus(LeadStatus.EmailVerified);
        EmailVerifyList = verifyResponse._value;
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
