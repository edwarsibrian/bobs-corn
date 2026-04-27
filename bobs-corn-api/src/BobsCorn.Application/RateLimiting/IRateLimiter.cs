namespace BobsCorn.Application.RateLimiting
{
    public interface IRateLimiter
    {
        Task<RateLimitResult> CheckRateLimitAsync(string clientId, CancellationToken cancellationToken);
    }
}
