namespace BobsCorn.Domain.Corn
{
    public sealed class CornPurchase
    {
        public Guid Id { get; private set; }
        public string ClientId { get; private set; }
        public DateTimeOffset PurchasedAtUtc { get; private set; }

        private CornPurchase(Guid id, string clientId, DateTimeOffset purchasedAtUtc)
        {
            Id = id;
            ClientId = clientId;
            PurchasedAtUtc = purchasedAtUtc;
        }

        public static CornPurchase Create(string clientId, DateTimeOffset purchasedAtUtc)
        {
            if(string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException("Client ID cannot be null or whitespace.", nameof(clientId));
            }

            return new CornPurchase(Guid.NewGuid(), clientId, purchasedAtUtc);
        }
    }
}
