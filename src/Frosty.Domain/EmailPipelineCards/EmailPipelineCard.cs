using Frosty.Domain.EmailPipelineCards.Events;
using Frosty.Domain.Framework;
using Frosty.Domain.Records;

namespace Frosty.Domain.EmailPipelineCards;

// Sometimes, in the business, this is called a Email Queue. And sometimes it is called an Email Pipeline. It's simply the records that need to be sent an initial email.

// The reason I picked Pipeline instead of queue is because I want to seperate business thinking from the Queue data Structure.

public class EmailPipelineCard : Entity {

    private EmailPipelineCard(
        Guid id,
        Record record
    ) : base(id) {

        if (record.PrimaryContact.Email == null ||
            record.PrimaryContact.Firstname == null) {
            // return exception
        }


        RecordId = record.Id;
        // RecordFirstname = record.PrimaryContact.Firstname;
        // RecordEmail = record.PrimaryContact.Email;
    }

    public Guid RecordId { get; private set; }
    public string? RecordFirstname { get; private set; }
    public string? RecordEmail { get; private set; }

    public DateTime AddedToPipelineUtc { get; private set; }
    public DateTime RemovedToPipelineUtc { get; private set; }

    public SendingAccount PrimarySendingAccount { get; private set; }

    public CardStatus CardStatus { get; private set; }

    public List<EmailLog>? EmailLogs { get; private set; }
    public int? EmailCounter { get; private set; }




    public void SendEmail() {
        // Function - Send Email
        // if emailverified, continue
        // if record rejected, do not continue
        // if email counter is 0, continue

    }



    public void FirstEmailSentStatusChange() {
        CardStatus = CardStatus.InitialEmailSent;

        AddDomainEvent(new InitialEmailSentDomainEvent(Id));

    }

    // public void MakeAccountReadyToSend() {
    // }

    public void UnsubscribeRecord() {
        CardStatus = CardStatus.Unsubscribed;

        AddDomainEvent(new UnsubscribedContactDomainEvent(Id));
    }

}


