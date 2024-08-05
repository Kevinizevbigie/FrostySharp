
namespace Frosty.Domain.Framework;

abstract class Entity {

    private readonly List<IDomainEvent> domainEvents = new();
    public Guid Id { get; init; }

    protected Entity(Guid id) {
        Id = id;
    }

    // return all domain events for an entity
    // NOTE: these will be looped through and published
    // in the application or infrastructure layer. Decide later.
    // Most likly to be done with mediator IPublisher
    public IReadOnlyList<IDomainEvent> GetDomainEvents() {
        return domainEvents.ToList();
    }

    // this will be needed later to allow EF core to run
    // migrations. It remains blank.
    protected Entity() {
    }
}
