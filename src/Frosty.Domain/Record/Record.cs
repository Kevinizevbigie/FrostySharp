using Frosty.Domain.Framework;
using Frosty.Domain.Record.Events;

namespace Frosty.Domain.Record;

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

    public LeadStatus LeadStatus { get; private set; }

    public List<Comment>? Comments { get; private set; }

    // public string Niche { get; private set; }
    // public string NicheType { get; private set; }

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

        // Domain event will trigger website verification in
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
    public void VerifyRecordEmail(
    Result<EmailVerificationResponse> emailVerification
    ) {

        // If result obj is not failed
        // set lead status to EmailFound
        // set email verify date
        // set email verify id/file id

        // if the result obj is failed, 
        // set reject timestamp

    }

    // THOUGHT: instead of injecting a result object, why not inject a service object?
    // OR - I think websiteverification can be a domain EVENT that triggers when a new record is created.
    // And Verification should be a service implemented in infrastructure but has an interface here...


    // records that are not ready to be added are simply ignored
    public void AddToSendingQueue() {

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
        // TODO: doesn't exist yet
        EmailQueue.Add(Id);
    }


}
