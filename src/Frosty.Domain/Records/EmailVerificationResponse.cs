
namespace Frosty.Domain.Records;

public sealed record EmailVerificationResponse(
    string EmailVerifyId,
    EmailGuess EmailGuess,
    EmailGuessStatus EmailStatus
);

public enum EmailGuessStatus {
    Passed = 1,
    Unknown = 2,
    Failed = 3
}

