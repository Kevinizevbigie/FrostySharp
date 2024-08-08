
namespace Frosty.Domain.EmailPipeline;

public enum CardStatus {
    ReadyToSend = 1,
    InitialEmailSent = 2,
    MultipleContactsSent = 3,
    Rejected = 4
}
