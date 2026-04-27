namespace BobsCorn.Application.Corn.BuyCorn
{
    public interface ICornPurchaseStore
    {
        Task<int> AddCornPurchaseAsync(string clientId, DateTimeOffset purchasedAtUtc, CancellationToken cancellationToken);
    }
}
