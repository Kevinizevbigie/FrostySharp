
using Frosty.Domain.Framework;

namespace Frosty.Domain.Shared;


public static class SharedErrors {

    public static Error NameCannotBeBlank = new(
            "Name.Blank",
            "The name supplied cannot be blank");

    public static Error UnlikeyNotName = new(
            "Name.NotReal",
            "The name supplied does not look real");

    public static Error MustBeOneWord = new(
            "Name.MoreOrLessThanOneWord",
            "A name can only be a single word. This has more or less than 1");
}
