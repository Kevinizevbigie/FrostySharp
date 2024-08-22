
namespace Frosty.Domain.Records;

public enum LeadStatus {
    New = 1,
    WebsiteValid = 2,
    EmailFound = 3,
    EmailVerified = 4,
    ReadyToSend = 5,
    Emailed = 6,
    Rejected = 7,
}
