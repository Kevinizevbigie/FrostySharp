
namespace Frosty.Domain.Records;

public sealed class Email {

    public string Value { get; private set; }
    public List<EmailGuess>? EmailGuessList { get; private set; }

    public Email(string submittedEmail) {
        
        Value = submittedEmail;
    }

    // triggered within creation event
    public List<EmailGuess> CreateListGuesses(
        ContactInfo primaryContact,
        Website website
    ) {

        List<EmailGuess> guessList = new();

        var fn = primaryContact.Firstname.Value;
        var ln = primaryContact.Lastname.Value;

        var fnFirstChar = fn[0];
        var lnFirstChar = ln[0];

        guessList.Add(new EmailGuess(fn + ln + "@" + website));
        guessList.Add(new EmailGuess(fn + "@" + website));
        guessList.Add(new EmailGuess(fn + lnFirstChar + "@" + website));
        guessList.Add(new EmailGuess(fn + "." + ln + "@" + website));
        guessList.Add(new EmailGuess(fn + "_" + ln + "@" + website));

        guessList.Add(new EmailGuess(fnFirstChar + ln + "@" + website));
        guessList.Add(new EmailGuess(fnFirstChar + lnFirstChar + "@" + website));

        guessList.Add(new EmailGuess(ln + fn + "@" + website));
        guessList.Add(new EmailGuess(ln + "@" + website));
        guessList.Add(new EmailGuess(ln + fnFirstChar + "@" + website));
        guessList.Add(new EmailGuess(ln + "." + fn + "@" + website));
        guessList.Add(new EmailGuess(ln + "_" + fn + "@" + website));

        // NOTE: For now, secondary contacts are ignored
        return guessList;
    }

}
