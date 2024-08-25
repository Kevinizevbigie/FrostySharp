
using Frosty.Domain.Framework;
using Frosty.Domain.Records;
using Frosty.Domain.Shared;
using Frosty.Domain.UnitTests.Records;

namespace Frosty.Domain.UnitTests.EmailPipelineCards;

internal class CardData {

    public async static Task<Frosty.Domain.Records.Record> MakeRecord() {

        var Fn = new Name<Firstname>("Kev");
        var Ln = new Name<Lastname>("Ize");

        // var contact = new ContactInfo(Fn, Ln);

        var record = await Frosty.Domain.Records.Record.Create(
            Fn,
            Ln,
            RecordSensitiveData.WebsitePass,
            DateTime.Now,
            RecordServices.DupCheckSucceed
        );

        return record._value;

    }


}
