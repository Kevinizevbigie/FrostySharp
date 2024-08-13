
using System.Globalization;

using Frosty.Domain.Framework;

namespace Frosty.Domain.Shared;

public class Name<T> {

    public string Value;

    public Name(string name) {

        Validate(name);

        Value = MakeProper(name);
    }

    private Result Validate(string name) {

        // If blank, name cannot be blank exception
        if (string.IsNullOrEmpty(name)) {
            return Result.Failure(SharedErrors.NameCannotBeBlank);
        }

        // if less than 3 chars - unlikely to be a name exception
        if (name.Length < 3) {
            return Result.Failure(SharedErrors.UnlikeyNotName);
        }

        // Check that name is only one word

        return Result.Success();
    }


    private string MakeProper(string name) {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(name);
    }

    // Add a method to update a name

}
