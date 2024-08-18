
namespace Frosty.Domain.Records;

public enum LeadStatus {
    New = 1,
    WebsiteValid = 2,
    NameFound = 3,
    EmailFound = 4,
    EmailVerified = 5,
    ReadyToSend = 6,
    Emailed = 7,
    Rejected = 8,
}
