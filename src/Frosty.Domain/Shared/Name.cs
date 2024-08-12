
using System.Globalization;

using Frosty.Domain.Framework;

namespace Frosty.Domain.Shared;

public class Name {

    private string Value;

    protected Name(string name) {
        Value = name;
    }

    public static Result<TValue> Create<TValue>(string name) where TValue
        : Name, new() {

        // If blank, name cannot be blank exception
        if (string.IsNullOrEmpty(name)) {
            return Result.Failure<TValue>(SharedErrors.NameCannotBeBlank);
        }

        // if less than 3 chars - unlikely to be a name exception
        if (name.Length < 3) {
            return Result.Failure<TValue>(SharedErrors.UnlikeyNotName);
        }

        // no arguement passed when created new generic static
        // but it works.
        var obj = new TValue();

        obj.MakeProper();

        return Result.Success<TValue>(obj);
    }


    private string MakeProper() {

        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        return textInfo.ToTitleCase(Value);
    }

}
