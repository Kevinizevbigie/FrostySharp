
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards;

public static class CardErrors {

    public static Error CannotAddToSendList = new(
        "Record.CannotAddToSendList",
        "This record does not meet the criteria for email sending"
    );

    // public static Error  = new(
    //     "Record.",
    //     ""
    // );

}
