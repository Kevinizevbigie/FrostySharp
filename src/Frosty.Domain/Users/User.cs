
using Frosty.Domain.Framework;

namespace Frosty.Domain.Users;

public sealed class User : Entity {

    public Firstname? Firstname { get; private set; }
    public Lastname? Lastname { get; private set; }
    public Email? Email { get; private set; }

    private User(
        Guid id,
        Firstname fn,
        Lastname ln,
        Email email) : base(id) {
        Firstname = fn;
        Lastname = ln;
        Email = email;
    }

    public static User Create(
        Firstname fn,
        Lastname ln,
        Email email) {
    }
}
