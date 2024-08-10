
using Frosty.Domain.Framework;

namespace Frosty.Domain.Users.Events;

public sealed record NewUserCreatedDomainEvent(Guid UserId) : IDomainEvent;
