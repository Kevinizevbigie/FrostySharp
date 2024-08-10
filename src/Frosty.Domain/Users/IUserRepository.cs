
namespace Frosty.Domain.Users;

public interface IUserRepository {

    Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void AddNew(User user);
}
