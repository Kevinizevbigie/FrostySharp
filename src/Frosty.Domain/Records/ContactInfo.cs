
using Frosty.Domain.Shared;
namespace Frosty.Domain.Records;

public sealed record ContactInfo(
    Name<Firstname> Firstname,
    Name<Lastname> Lastname,
    Email? Email = null);
