using System.Text.RegularExpressions;
using Frosty.Domain.Framework;
namespace Frosty.Domain.Records;

// An email is not created until much later in the process.
// Therefore, we will already have fn, ln and website.

public sealed class Email {

    public string Value { get; private set; }
    public List<EmailGuess>? EmailGuessList { get; private set; }

    public Email(
        string submittedEmail,
        ContactInfo info,
        Website website
    ) {

        Value = submittedEmail;
        EmailGuessList = CreateListGuesses(info, website);
    }


    // Factory
    public static Result<Email> Create(
        string submittedEmail,
        ContactInfo info,
        Website website
    ) {

        Email email = new Email(submittedEmail, info, website);

        var emailCheck = email.VerifyEmailAddress(submittedEmail);

        if (emailCheck == false) {
            return Result.Failure<Email>(RecordErrors.BadEmail);
        }

        return Result.Success<Email>(email);
    }

    private bool VerifyEmailAddress(string email) {

        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);

        if (match.Success) {
            return true;
        } else {
            return false;
        }
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
