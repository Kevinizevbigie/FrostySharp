
namespace Frosty.Domain.Record;

public sealed record ContactInfo(
    string Firstname,
    string Lastname,
    string? Email = null);
