using BobsCorn.Application.Clock;
using BobsCorn.Application.RateLimiting;
using MediatR;
using BobsCorn.Domain.Corn;

namespace BobsCorn.Application.Corn.BuyCorn
{
    public sealed class BuyCornCommandHandler : IRequestHandler<BuyCornCommand, BuyCornResult>
    {
        private readonly IRateLimiter _rateLimiter;
        private readonly ICornPurchaseStore _cornPurchaseStore;
        private readonly IClock _clock;

        public BuyCornCommandHandler(
            IRateLimiter rateLimiter,
            ICornPurchaseStore cornPurchaseStore,
            IClock clock)
        {
            _rateLimiter = rateLimiter;
            _cornPurchaseStore = cornPurchaseStore;
            _clock = clock;
        }

        public async Task<BuyCornResult> Handle(BuyCornCommand command, CancellationToken cancellationToken)
        {
            var rateLimit = await _rateLimiter.CheckRateLimitAsync(command.ClientId, cancellationToken);

            if (!rateLimit.IsAllowed)
            {
                return new BuyCornResult(
                    Success: false,
                    TotalCornBought: 0,
                    Message: "Too many requests. You can only buy 1 corn per minute.",
                    RetryAfterSeconds: (int)Math.Ceiling(rateLimit.RetryAfter.TotalSeconds));
            }

            var cornPurchase = CornPurchase.Create(command.ClientId, _clock.UtcNow);

            var total = await _cornPurchaseStore.AddCornPurchaseAsync(
                cornPurchase.ClientId,
                cornPurchase.PurchasedAtUtc,
                cancellationToken);

            return new BuyCornResult(
                Success: true,
                TotalCornBought: total,
                Message: "Corn purchased successfully");
        }
    }
}
