using Frosty.Domain.Framework;
using Frosty.Domain.Records.Events;

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

    // The website is the unique field as a person can own
    // multiple companies
    public Website Website { get; private set; }
    public DateTime? WebsiteVerifyDate { get; private set; }

    public string? EmailVerifyId { get; private set; }
    public DateTime? EmailVerifyDate { get; private set; }
    public List<EmailGuess>? EmailGuessList { get; private set; }
    // TODO: seperate the DTO from the object type
    public List<EmailVerificationResponse>? EmailVerifyList { get; private set; }

    public LeadStatus LeadStatus { get; private set; }

    public List<Comment>? Comments { get; private set; }

    public DateTime CreateDate { get; private set; }

    // NOTE: if the verification or website service failed, Reject.
    public DateTime? RejectDate { get; private set; }
    public List<EmailLog>? EmailLogs { get; private set; }
    public int? EmailCounter { get; private set; }


    public static Record Create(
        string firstname,
        string lastname,
        string email,
        Website website,
        DateTime createDate,
        LeadStatus leadStatus = LeadStatus.New,
        List<ContactInfo>? secondaryContacts = null
    ) {

        var record = new Record(
            Guid.NewGuid(),
            new ContactInfo(firstname, lastname, email),
            website,
            leadStatus,
            DateTime.UtcNow
        );

        // TODO: Domain event will trigger website verification in
        // TODO: Domain event will Create email guess list
        // application/Infra layer
        record.AddDomainEvent(new RecordCreatedEvent(record.Id));

        return record;

    }

    // TODO: move below to infra/app layer
    // public void VerifyRecordWebsite() {

    // update verify data time to now

    // If result obj is not failed
    // set lead status to NameFound


    // if the result obj is failed, 
    // set reject timestamp
    // set lead status to Rejected
    // }



    // EmailVerification is a slow process. It's also limited by one thread.
    // NOTE: at the moment, the production system has this entire process
    // running as a microservice managed by RabbitMQ
    // TODO: move to app service
    // public void VerifyRecordEmail() {
    // If result obj is not failed
    // set lead status to EmailFound
    // set email verify date
    // set email verify id/file id

    // if the result obj is failed, 
    // set reject timestamp
    // }

    // Triggered by application service
    public void UpdateVerificationList(
        // list is returned by Send() in service interface
        List<EmailVerificationResponse> res
    ) {
        EmailVerifyList = res;

    }

    // triggered within creation event
    public List<EmailGuess> CreateListGuesses(
        ContactInfo primaryContact,
        Website website
    ) {

        List<EmailGuess> guessList = new();

        string fn = primaryContact.Firstname;
        string ln = primaryContact.Lastname;

        char FirstCharOfFirstname = fn[0];
        char FirstCharOflastname = ln[0];

        //TODO: add all variations
        guessList.Add(new EmailGuess(fn + ln + "@" + website));

        return guessList;
    }

    // records that are not ready to be added are simply ignored
    public void AddRecordToSendingQueue() {

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

        this.AddDomainEvent(new AddToEmailPipelineEvent(Id));

    }


}
