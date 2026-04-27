using BobsCorn.Application.Clock;
using BobsCorn.Application.Corn.BuyCorn;
using BobsCorn.Application.RateLimiting;
using FluentAssertions;
using NSubstitute;

namespace BobsCorn.Application.Tests.Corn.BuyCorn
{
    public sealed class BuyCornCommandHandlerTests
    {
        private readonly BuyCornCommandHandler _handler;
        private readonly IRateLimiter _rateLimiter;
        private readonly ICornPurchaseStore _cornPurchaseStore;
        private readonly IClock _clock;

        public BuyCornCommandHandlerTests()
        {
            _rateLimiter = Substitute.For<IRateLimiter>();
            _cornPurchaseStore = Substitute.For<ICornPurchaseStore>();
            _clock = Substitute.For<IClock>();

            _handler = new BuyCornCommandHandler(_rateLimiter, _cornPurchaseStore, _clock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenClientIsWithinRateLimit()
        {
            // Arrange
            var clientId = "client-123";
            var now = new DateTimeOffset(2026, 4, 27, 12, 0, 0, TimeSpan.Zero);

            _clock.UtcNow.Returns(now);

            _rateLimiter
                .CheckRateLimitAsync(clientId, Arg.Any<CancellationToken>())
                .Returns(new RateLimitResult(true, TimeSpan.Zero));

            _cornPurchaseStore
                .AddCornPurchaseAsync(clientId, now, Arg.Any<CancellationToken>())
                .Returns(1);

            var command = new BuyCornCommand(clientId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.TotalCornBought.Should().Be(1);
            result.RetryAfterSeconds.Should().BeNull();
            result.Message.Should().Be("Corn purchased successfully");

            await _rateLimiter
                .Received(1)
                .CheckRateLimitAsync(clientId, Arg.Any<CancellationToken>());

            await _cornPurchaseStore
                .Received(1)
                .AddCornPurchaseAsync(clientId, now, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ShouldReturnTooManyRequests_WhenClientExceedsRateLimit()
        {
            // Arrange
            var clientId = "client-123";
            var retryAfter = TimeSpan.FromSeconds(45);

            _rateLimiter
                .CheckRateLimitAsync(clientId, Arg.Any<CancellationToken>())
                .Returns(new RateLimitResult(false, retryAfter));

            var command = new BuyCornCommand(clientId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.TotalCornBought.Should().Be(0);
            result.RetryAfterSeconds.Should().Be(45);
            result.Message.Should().Be("Too many requests. You can only buy 1 corn per minute.");

            await _rateLimiter
                .Received(1)
                .CheckRateLimitAsync(clientId, Arg.Any<CancellationToken>());

            await _cornPurchaseStore
                .DidNotReceive()
                .AddCornPurchaseAsync(
                    Arg.Any<string>(),
                    Arg.Any<DateTimeOffset>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
