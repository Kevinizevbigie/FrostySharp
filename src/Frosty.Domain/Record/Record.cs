
using Frosty.Domain.Framework;

namespace Frosty.Domain.Record;

public sealed class Record : Entity {

    public Record(
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
    public Website WebsiteVerifyDate { get; private set; }

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

    //NOTE: Planning Functions inside The Record Entity
    // 1. Send email
    // Helper Service required to send emails


    public void VerifyRecordWebsite(
    Result<WebsiteTestResponse> websiteTest
    ) {

        // update verify data time to now

        // If result obj is not failed
        // set lead status to NameFound


        // if the result obj is failed, 
        // set reject timestamp
        // set lead status to Rejected

    }

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
