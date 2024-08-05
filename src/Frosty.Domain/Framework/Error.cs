
namespace Frosty.Domain.Framework;

public sealed class Error {

    public string ErrorName;
    public string Description;

    public Error(string errorName, string description) {
        ErrorName = errorName;
        Description = description;
    }

    // create an empty error object
    public static Error None = new Error(
        string.Empty,
        string.Empty);
}
