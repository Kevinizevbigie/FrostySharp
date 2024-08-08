
namespace Frosty.Domain.Records;

public sealed record ContactInfo(
    string Firstname,
    string Lastname,
    string? Email = null);
