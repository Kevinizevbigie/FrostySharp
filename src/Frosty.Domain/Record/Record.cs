
using Frosty.Domain.Framework;
namespace Frosty.Domain.Record;

public sealed class Record : Entity {

    public Record(
        Guid id,
        LeadStatus ls
    ) : base(id) {

        LeadStatus = ls;

    }

    public ContactInfo ContactInfo { get; private set; }
    public string SecondaryContacts { get; private set; }

    // The website is the unique field as a person can own
    // multiple companies
    public Website Website { get; private set; }

    public string EmailVerifyId { get; private set; }
    public DateTime EmailVerifyDate { get; private set; }

    public LeadStatus LeadStatus { get; private set; }

    public List<Comment> Comments { get; private set; }

    // public string Niche { get; private set; }
    // public string NicheType { get; private set; }

    public DateTime CreateDate { get; private set; }

    // NOTE: if the verification or website service failed, Reject.
    public DateTime RejectDate { get; private set; }
    public string EmailLog { get; private set; }
    public string EmailCounter { get; private set; }

}

// NOTE: Potential Value Objects

// public sealed record ContactInfo();
// public sealed record VerificationDetails();
// public sealed record ContactInfo();

public sealed record Comment(
    string Name,
    string Description);


public sealed record Website(string Value);

public sealed record ContactInfo(
    string Firstname,
    string Lastname,
    string? Email = null);

public enum LeadStatus {
    New = 1,
    NameFound = 2,
    EmailFound = 3,
    EmailVerified = 4,
    Emailed = 5,
    Rejected = 6,
}

// secondary contacts - list of value model


// NOTE: Potential Services
// Email Verification Service - regex testing
// Website Verification Service - ping website for status code
