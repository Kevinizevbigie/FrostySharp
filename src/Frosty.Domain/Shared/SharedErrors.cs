
using Frosty.Domain.Framework;

namespace Frosty.Domain.Shared;


public static class SharedErrors {

    public static Error NameCannotBeBlank = new(
            "Name.Blank",
            "The name supplied cannot be blank");

    public static Error UnlikeyNotName = new(
            "Name.NotReal",
            "The name supplied does not look real");
}
