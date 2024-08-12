
namespace Frosty.Domain.Records;

public sealed class Email {

    public string Value { get; private set; }

    public Email(string submittedEmail) {
        Value = submittedEmail;
    }

}
