namespace Frosty.Domain.Records.Services;

public interface IRecordCheckDuplicateService {
    public Task<bool> Check(Website website);
}
