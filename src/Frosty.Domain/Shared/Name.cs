
using System.Globalization;

using Frosty.Domain.Framework;

namespace Frosty.Domain.Shared;

public class Name<T> {

    public string Value;
    public T Obj;

    public Name(string name, T entity) {

        Validate(name);

        Value = MakeProper(name);
        Obj = entity;

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

        return Result.Success();
    }


    private string MakeProper(string name) {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(name);
    }

}
