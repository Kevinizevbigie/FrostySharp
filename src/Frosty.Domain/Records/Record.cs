using Frosty.Domain.Framework;
using Frosty.Domain.Records.Events;
using Frosty.Domain.Records.Services;

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

    public string? EmailVerifyId { get; private set; }
    public DateTime? EmailVerifyDate { get; private set; }

    // TODO: seperate the DTO from the object type
    // a list of email guesses returned back from an external service
    public List<EmailVerificationResponse>? EmailVerifyList { get; private set; }

    // TODO: Add functions to change status here
    public LeadStatus LeadStatus { get; private set; }

    // TODO: Add functions for add/remove comment
    public List<Comment>? Comments { get; private set; }

    public DateTime CreateDate { get; private set; }

    // NOTE: if the verification or website service failed, Reject.
    public DateTime? RejectDate { get; private set; }


    public static Record Create(
        string firstname,
        string lastname,
        string email,
        Website website,
        DateTime createDate,
        LeadStatus leadStatus = LeadStatus.New,
        List<ContactInfo>? secondaryContacts = null
    // IRecordDuplicateCheckerService service
    ) {

        var record = new Record(
            Guid.NewGuid(),
            new ContactInfo(firstname, lastname, email),
            website,
            leadStatus,
            DateTime.UtcNow
        );

        // TODO: Create email guess list here from value object

        // application/Infra layer
        // Should trigger website ping service. Need interface in domain.
        // if fail, record gets rejected
        record.AddDomainEvent(new RecordCreatedDomainEvent(record.Id));

        return record;

    }

    // NOTE: STOP HERE - for now
    // Triggered by application service
    public void UpdateVerificationList(
        // list is returned by Send() in service interface
        List<EmailVerificationResponse> res
    ) {
        EmailVerifyList = res;

    }


    // records that are not ready to be added are simply ignored
    public void AddRecordToSendingQueue(
        IEmailVerificationService service) {

        //TODO: I need a better way of organising the verification service.
        // The concrete class will be in application layer.
        // But, how will the domain know about the result?

        // Create new record and Website check is coupled.
        // Email verification should only happened when we want to add to
        // EmailPipeline as a card.

        // Therefore...
        // Command - AddNewRecord
        // Command - AddToPipeline
        // Command - SendEmail
        // Query - GetPipelineReport
        // Query - GetRecord
        // ^^ These will be the CQRS application layer
        // Because of the nature of the external API for verification
        // Verification should be sent to rabbit service and Frosty should wait for the return value before deciding to add to pipeline
        var verificationStatus = service.Send(Id);

        if (RejectDate is not null || LeadStatus == LeadStatus.Rejected) {
            return;
        }

        if (EmailCounter > 0) {
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
        // This should be an event - Add to pipeline domain event
        // Then the addtopipeline function in EmailPipeline should run
        // in an event handler

        this.AddDomainEvent(new AddToEmailPipelineDomainEvent(Id));

    }

}
