
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

}







// NOTE: Potential Services
// Email Verification Service - regex testing
// Website Verification Service - ping website for status code
// Increase email counter
