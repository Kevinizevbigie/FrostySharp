
namespace Frosty.Domain.UnitTests.Record;

public class RecordTests {

    [Fact]
    public void Create_Should_ReturnFailure_When_DuplicateRecordServiceFails() {

        // Arrange


        // Act


        // Assert

    }

    [Fact]
    public void Create_Should_ReturnRecord_On_Success() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_On_Success() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void VerifyEmails_Should_ChangeLeadStatusToVerifed() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void VerifyEmails_Should_AddVerifyEmailResultsToEmailVerifyList() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void AddToSendQueue_Should_RaiseDomainEvent_On_Success() {

        // Arrange



        // Act


        // Assert

    }


    // ======================================== //
    // Value Object Functionality Unit Tests
    // ======================================== //

    // Website Create new (Factory)
    // Email Create new (Factory)
    // Email Verify Email Address
    // Email Create list guesses are accurate
    // Email Create list guesses are are added to email prop

    // Email verify id random id created

    // New comment created

    // ======================================== //
    // Tests to add later
    // ======================================== //

    // Record rejected via domain event
}
