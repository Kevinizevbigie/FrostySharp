
using Frosty.Domain.Records;
namespace Frosty.Domain.EmailPipelineCards;

public interface IEmailPipelineCardRepository {

    Task<EmailPipelineCard?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationtoken);

    void AddNew(EmailPipelineCard emailPipelineCard);

    // check if there is already a card created for this record
    Task<bool> IsDuplicateAsync(
        Record record
    );
}
