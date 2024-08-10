
namespace Frosty.Domain.Records;

public interface IRecordRepository {

    Task<Record?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationtoken);

    void AddNew(Record record);
}
