
using Frosty.Domain.Framework;
namespace Frosty.Domain.Record;

public sealed class Record : Entity {

    public Record(
        Guid id
    ) : base(id) {

    }

    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }

    // The website is the unique field as a person can own
    // multiple companies
    public string Website { get; private set; }

    public string VerifyFileId { get; private set; }
    public string EmailVerifyDate { get; private set; }
    public LeadStatus LeadStatus { get; private set; }

    public string Comments { get; private set; }
    public string SecondaryContacts { get; private set; }

    public string Niche { get; private set; }
    public string NicheType { get; private set; }

    public string CreateDate { get; private set; }
    public string EmailLog { get; private set; }
}

// NOTE: Potential Value Objects

// public sealed record ContactInfo();
// public sealed record VerificationDetails();
// public sealed record ContactInfo();

public enum LeadStatus {
    New = 1,
    NameFound = 2,
    EmailFound = 3,
    EmailVerified = 4,
    Emailed = 5,
}

// Contact Details - value object - FN/LN/Email
// secondary contacts - list of value model
// Lead status - enum
// 


// NOTE: Potential Services

// Email Verification Service - regex testing
// Website Verification Service - ping website for status code
