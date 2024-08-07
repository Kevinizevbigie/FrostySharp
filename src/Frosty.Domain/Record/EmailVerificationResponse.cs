
namespace Frosty.Domain.Record;

public sealed record EmailVerificationResponse(
    DateTime VerificationDate,
    string EmailVerifyId,
    string Email,
    string EmailStatus
);
