
using Frosty.Domain.Framework;

namespace Frosty.Domain.Records.Events;

public sealed record RecordCreatedDomainEvent(Guid RecordId) : IDomainEvent;
