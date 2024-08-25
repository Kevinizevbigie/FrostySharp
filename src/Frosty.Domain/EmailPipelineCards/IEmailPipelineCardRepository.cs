
namespace Frosty.Domain.EmailPipelineCards;

public interface IEmailPipelineCardRepository {

    Task<EmailPipelineCard?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationtoken);

    void AddNew(EmailPipelineCard emailPipelineCard);
}
