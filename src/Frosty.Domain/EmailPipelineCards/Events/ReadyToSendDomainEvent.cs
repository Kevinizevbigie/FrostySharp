using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards.Events;

public sealed record ReadyToSendDomainEvent(Guid RecordId) : IDomainEvent;
