
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards;

public static class CardErrors {

    public static Error CannotAddToSendList = new(
        "Record.CannotAddToSendList",
        "This record does not meet the criteria for email sending"
    );

    public static Error RejectedRecord = new(
        "Record.RejectedRecord",
        "This record is rejected and therefore cannot be emailed"
    );

    public static Error InitialEmailAlreadySent = new(
        "Record.InitialEmailAlreadySent",
        "This record has already sent an initial email"
    );

}
