
namespace Frosty.Domain.Shared;

public class Name {

    private string Value;

    protected Name(string name) {
        Value = name;
    }

    public static TValue Create<TValue>(string name) where TValue
        : Name, new() {

        // If blank, name cannot be blank exception
        if (string.IsNullOrEmpty(name)) {
            throw new NameCannotBeBlankException();
        }

        // if less than 3 chars - unlikely to be a name exception
        if (name.Length < 3) {
            throw new UnlikelyToBeANameException();
        }

        // no arguement passed when created new generic static
        // but it works.
        return new TValue();
    }

}
