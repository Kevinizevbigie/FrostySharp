
namespace Frosty.Domain.Records;

// An email log records when and how often an email has been sent
public sealed record EmailLog {

    public string Name;
    public DateTime LastDateEmailUtc;

    public EmailLog(DateTime emailDate) {
        LastDateEmailUtc = emailDate;
        Name = "Email Sent at " + LastDateEmailUtc;
    }

}
