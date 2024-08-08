
namespace Frosty.Domain.Framework;

public abstract class Entity {

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

    public void ClearAllDomainEvents() {
        domainEvents.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent) {
        domainEvents.Add(domainEvent);
    }



    // this will be needed later to allow EF core to run
    // migrations. It remains blank.
    protected Entity() {
    }
}
