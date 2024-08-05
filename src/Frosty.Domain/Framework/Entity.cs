
namespace Frosty.Domain.Framework;

abstract class Entity {

    private readonly List<IDomainEvent> domainEvents = new();

    public Guid Id { get; init; }

    protected Entity(Guid id) {
        Id = id;
    }
}
