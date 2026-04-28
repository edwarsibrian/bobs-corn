using BobsCorn.Infrastructure.RateLimiting;
using BobsCorn.Infrastructure.Tests.Fakes;
using FluentAssertions;

namespace BobsCorn.Infrastructure.Tests.RateLimiting
{
    public sealed class InMemoryCornRateLimiterTests
    {
        [Fact]
        public async Task CheckAsync_ShouldAllowFirstPurchase_ForClient()
        {
            // Arrange
            var clock = new FakeClock(new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero));
            var rateLimiter = new InMemoryCornRateLimiter(clock);

            // Act
            var result = await rateLimiter.CheckRateLimitAsync("client-1", CancellationToken.None);

            // Assert
            result.IsAllowed.Should().BeTrue();
            result.RetryAfter.Should().Be(TimeSpan.Zero);
        }

        [Fact]
        public async Task CheckAsync_ShouldRejectSecondPurchase_WhenWithinOneMinute()
        {
            // Arrange
            var clock = new FakeClock(new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero));
            var rateLimiter = new InMemoryCornRateLimiter(clock);

            await rateLimiter.CheckRateLimitAsync("client-1", CancellationToken.None);

            clock.Advance(TimeSpan.FromSeconds(30));

            // Act
            var result = await rateLimiter.CheckRateLimitAsync("client-1", CancellationToken.None);

            // Assert
            result.IsAllowed.Should().BeFalse();
            result.RetryAfter.Should().Be(TimeSpan.FromSeconds(30));
        }

        [Fact]
        public async Task CheckAsync_ShouldAllowPurchase_WhenOneMinuteHasPassed()
        {
            // Arrange
            var clock = new FakeClock(new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero));
            var rateLimiter = new InMemoryCornRateLimiter(clock);

            await rateLimiter.CheckRateLimitAsync("client-1", CancellationToken.None);

            clock.Advance(TimeSpan.FromMinutes(1));

            // Act
            var result = await rateLimiter.CheckRateLimitAsync("client-1", CancellationToken.None);

            // Assert
            result.IsAllowed.Should().BeTrue();
            result.RetryAfter.Should().Be(TimeSpan.Zero);
        }

        [Fact]
        public async Task CheckAsync_ShouldTrackClientsIndependently()
        {
            // Arrange
            var clock = new FakeClock(new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero));
            var rateLimiter = new InMemoryCornRateLimiter(clock);

            await rateLimiter.CheckRateLimitAsync("client-1", CancellationToken.None);

            // Act
            var result = await rateLimiter.CheckRateLimitAsync("client-2", CancellationToken.None);

            // Assert
            result.IsAllowed.Should().BeTrue();
            result.RetryAfter.Should().Be(TimeSpan.Zero);
        }
    }
}
