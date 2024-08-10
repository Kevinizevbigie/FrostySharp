
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards.Events;

public sealed record InitialEmailSentDomainEvent(Guid RecordId) : IDomainEvent;
