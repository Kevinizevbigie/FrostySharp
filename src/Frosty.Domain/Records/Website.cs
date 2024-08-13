
namespace Frosty.Domain.Records;

public sealed record Website {

    public string Value;

    private Website(string website) {
        Value = website;
    }

    public static Website Create(string website) {

        // VALIDATION


        return new Website(website);

    }
}

