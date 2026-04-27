using BobsCorn.Application.Corn.BuyCorn;
using FluentAssertions;

namespace BobsCorn.Application.Tests.Corn.BuyCorn
{
    public sealed class BuyCornCommandValidatorTests
    {
        private readonly BuyCornCommandValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnError_WhenClientIdIsRequired()
        {
            // Arrange
            var command = new BuyCornCommand(string.Empty);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error =>
                error.PropertyName == nameof(BuyCornCommand.ClientId) &&
                error.ErrorMessage == "ClientId is required.");
        }
    }
}
