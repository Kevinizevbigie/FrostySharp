
namespace Frosty.Domain.Record.Services;

// this send function will send the required data to the
// Rabbit MQ service. Which, at the moment is in PHP. That
// will not change for now.
interface IEmailVerificationService {
    public EmailVerificationResponse Send();
}
