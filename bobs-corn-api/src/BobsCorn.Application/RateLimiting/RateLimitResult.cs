namespace BobsCorn.Application.RateLimiting
{
    public sealed record RateLimitResult(
        bool IsAllowed,
        TimeSpan RetryAfter);
}
