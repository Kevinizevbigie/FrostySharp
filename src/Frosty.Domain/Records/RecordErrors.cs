
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

    public static Error BadEmail = new(
        "Record.BadEmail",
        "This email address does not look proper"
    );

    public static Error BlankValue = new(
        "Record.BlankValue",
        "The value is blank"
    );

    public static Error UnableToVarify = new(
        "Record.UnableToVarify",
        "Cannot verify a record that is not WebsiteValid Status. Email or email guess list cannot be null");

    public static Error VerifyListEmpty = new(
        "Record.VerifyListEmpty",
        "The verify list is empty. Cannot verify without a list."
    );
}
