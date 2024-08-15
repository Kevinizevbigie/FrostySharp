
namespace Frosty.Domain.Records;

public sealed record EmailVerifyId {

    public string Value { get; private set; }

    private EmailVerifyId(string code) {
        Value = code;
    }

    public static EmailVerifyId GenerateRandomCode() {

        // LOGIC FOR RANDOM STRING HERE - 8 Chars

        var randomString = "";

        return new EmailVerifyId(randomString);
    }

}
