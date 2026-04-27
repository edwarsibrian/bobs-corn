namespace BobsCorn.Application.Corn.BuyCorn
{
    public sealed record BuyCornResult(
        bool Success,
        int TotalCornBought,
        string Message,
        int? RetryAfterSeconds = null);
}
