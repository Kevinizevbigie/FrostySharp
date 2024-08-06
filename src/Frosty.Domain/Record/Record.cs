
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
    public string EmailCounter { get; private set; }


}

// NOTE: Potential Value Objects

// public sealed record ContactInfo();
// public sealed record VerificationDetails();
// public sealed record ContactInfo();

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
}



// public struct LeadStatus2 {

//     public static string New = "new record";
//     public static string NameFound = "new record";
//     public static string EmailFound = "new record";
//     public static string EmailVerified = "new record";
//     public static string Emailed = "new record";
// }


// Contact Details - value object - FN/LN/Email
// secondary contacts - list of value model
// Lead status - enum
// 


// NOTE: Potential Services

// Email Verification Service - regex testing
// Website Verification Service - ping website for status code
