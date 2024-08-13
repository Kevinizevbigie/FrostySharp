using Frosty.Domain.Framework;
namespace Frosty.Domain.Records.Services;

public interface IPingWebsiteService<T> {
    public Task<Result<T>> Ping(string website);
}
