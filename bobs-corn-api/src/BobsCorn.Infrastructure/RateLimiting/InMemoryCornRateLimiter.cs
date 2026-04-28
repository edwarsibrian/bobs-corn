using BobsCorn.Application.Clock;
using BobsCorn.Application.RateLimiting;
using System.Collections.Concurrent;

namespace BobsCorn.Infrastructure.RateLimiting
{
    public sealed class InMemoryCornRateLimiter : IRateLimiter
    {
        private readonly IClock _clock;
        private readonly ConcurrentDictionary<string, DateTimeOffset> _lastPurchaseByClient = new();

        private static readonly TimeSpan RateLimitWindow = TimeSpan.FromSeconds(1);

        public InMemoryCornRateLimiter(IClock clock)
        {
            _clock = clock;
        }

        public Task<RateLimitResult> CheckRateLimitAsync(string clientId, CancellationToken cancellationToken)
        {
            var now = _clock.UtcNow;

            var allowed = _lastPurchaseByClient.AddOrUpdate(clientId, now, (key, lastPurchase) =>
            {
                var elapsed = now - lastPurchase;
                return elapsed >= RateLimitWindow ? now : lastPurchase;
            });

            var isAllowed = allowed == now;

            if (isAllowed)
            {
                return Task.FromResult(new RateLimitResult(true, TimeSpan.Zero));
            }

            var retryAfter = RateLimitWindow - (now - allowed);

            return Task.FromResult(new RateLimitResult(false, retryAfter));
        }
    }
}
