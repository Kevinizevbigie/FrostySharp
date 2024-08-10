
using Frosty.Domain.Framework;
using Frosty.Domain.Users.Events;

namespace Frosty.Domain.Users;

// anything related to authentication and authorization will be
// handled at a later
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

        User user = new User(Guid.NewGuid(), fn, ln, email);

        user.AddDomainEvent(new NewUserCreatedDomainEvent(user.Id));

        return user;

    }
}
