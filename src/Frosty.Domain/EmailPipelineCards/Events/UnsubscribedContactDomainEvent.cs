
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards.Events;

public sealed record UnsubscribedContactDomainEvent(Guid RecordId) : IDomainEvent;

