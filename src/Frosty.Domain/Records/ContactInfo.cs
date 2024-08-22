
using Frosty.Domain.Shared;
namespace Frosty.Domain.Records;

public sealed record ContactInfo {
    public Name<Firstname> Firstname { get; set; }
    public Name<Lastname> Lastname { get; set; }
    public Email? Email = null;

    public ContactInfo(Name<Firstname> fn,
                       Name<Lastname> ln) {

        Firstname = fn;
        Lastname = ln;
    }
}
