
using Frosty.Domain.Framework;
namespace Frosty.Domain.Records.Services;

// this send function will send the required data to the
// Rabbit MQ service. Which, at the moment is in PHP. That
// will not change for now.

// This function will send the guess list and guid to the rabbit mq service
// and return the Email Ver. Response list for all guesses.

// This is important. Because there is a 30% chance that people enter the wrong email by mistake. And we want to reduce the chance of email bounces.

// When this list is returned in the app layer, we then want to save the results to the database.

public interface IEmailVerificationService {
    public Task<Result<List<EmailVerificationResponse>>> Send(
        Guid id,
        List<EmailGuess> list
    );
}
