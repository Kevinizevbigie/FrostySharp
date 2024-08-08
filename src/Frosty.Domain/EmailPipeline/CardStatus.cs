
namespace Frosty.Domain.EmailPipeline;

public enum CardStatus {
    ReadyToSend = 1,
    InitialEmailSent = 2,
    MultipleContactsSent = 3,
    Unsubscribed = 4,
    Rejected = 5
}
