
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards.Events;

public sealed record UnsubscribedContactEvent(Guid RecordId) : IDomainEvent;

