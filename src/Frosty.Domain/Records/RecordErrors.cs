
using Frosty.Domain.Framework;

namespace Frosty.Domain.Records;

public static class RecordErrors {

    public static Error DuplicateWebsite = new(
        "Record.Duplicate",
        "This website record already exists in the system"
    );

    public static Error RejectedRecord = new(
        "Record.RejectedRecord",
        "This record has beed rejected. Cannot process further."
    );

    public static Error WebsiteRejected = new(
        "Record.RejectedWebsite",
        "This website does not exist or is not functioning"
    );
}
