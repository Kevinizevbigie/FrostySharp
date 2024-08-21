using Frosty.Domain.Framework;
namespace Frosty.Domain.Records.Services;

public interface IPingWebsiteService {
    public Task<bool> Ping(string website);
}
