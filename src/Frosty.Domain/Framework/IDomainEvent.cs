using MediatR;

namespace Frosty.Domain.Framework;

// a domain event uses MediatR INotification
public interface IDomainEvent : INotification {
}
