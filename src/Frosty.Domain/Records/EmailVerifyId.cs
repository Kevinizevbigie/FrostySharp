
namespace Frosty.Domain.Records;

public sealed record EmailVerifyId {

    public string Value { get; private set; }

    private EmailVerifyId(string code) {
        Value = code;
    }

    public static EmailVerifyId GenerateRandomCode() {

        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var stringChars = new char[8];

        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++) {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new EmailVerifyId(new string(stringChars));
    }

}
