
using Frosty.Domain.Framework;
namespace Frosty.Domain.Record;

public sealed class Record : Entity {

    public Record(
        Guid id
    ) : base(id) {

    }

    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public string Website { get; private set; }

    public string VerifyFileId { get; private set; }
    public string EmailVerifyDate { get; private set; }
    public string LeadStatus { get; private set; }

    public string Comments { get; private set; }
    public string SecondaryContacts { get; private set; }

    public string Niche { get; private set; }
    public string NicheType { get; private set; }

    public string CreateDate { get; private set; }
    public string EmailLog { get; private set; }
}


// Contact Details - value object - FN/LN/Email
// secondary contacts - list of value model
// Lead status - enum
// 

