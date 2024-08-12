
using Frosty.Domain.Framework;

namespace Frosty.Domain.Record;

public static class RecordErrors {

    public static Error DuplicateWebsite = new(
        "Record.Duplicate",
        "This website record already exists in the system"
    );

    public static Error RejectedRecord = new(
        "Record.Rejected",
        "This record has beed rejected. Cannot process further."
    );
}
