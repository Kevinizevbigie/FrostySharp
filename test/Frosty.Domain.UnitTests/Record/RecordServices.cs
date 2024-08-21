using Frosty.Domain.Records;
using Frosty.Domain.Records.Services;

namespace Frosty.Domain.UnitTests.Records;

internal class WebsitePingTrue : IPingWebsiteService {
    public async Task<bool> Ping(string website) {
        return true;
    }
}

internal class WebsitePingFalse : IPingWebsiteService {
    public async Task<bool> Ping(string website) {
        return false;
    }
}

internal class DuplicateCheckServiceFailed : IRecordCheckDuplicateService {
    public async Task<bool> Check(Website website) {
        return false;
    }
}

internal class DuplicateCheckServiceSuccess : IRecordCheckDuplicateService {
    public async Task<bool> Check(Website website) {
        return true;
    }
}


internal static class RecordServices {
    internal static WebsitePingTrue PingTrue = new();
    internal static WebsitePingFalse PingFalse = new();
    internal static DuplicateCheckServiceFailed DupCheckFail = new();
    internal static DuplicateCheckServiceSuccess DupCheckSucceed = new();
}
