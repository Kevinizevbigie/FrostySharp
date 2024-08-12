
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards;

public static class EmailPipelineCardErrors {

    public static Error DuplicateCard = new(
        "EmailPipelineCard.Duplicate",
        "This pipeline card already exists in the system"
    );

    public static Error UnsubscribedRecord = new(
        "EmailPipelineCard.Unsubscribed",
        "This record cannot be contacted because they have unsubscribed"
    );
}
