using BobsCorn.Application.Corn.BuyCorn;
using Dapper;

namespace BobsCorn.Infrastructure.Persistence.Dapper
{
    public sealed class CornPurchaseStore : ICornPurchaseStore
    {
        private readonly IDbConnectionFactory _dbConnection;

        public CornPurchaseStore(IDbConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddCornPurchaseAsync(string clientId, DateTimeOffset purchasedAtUtc, CancellationToken cancellationToken)
        {
            using var connection = _dbConnection.CreateConnection();

            const string insertSql= """
                INSERT INTO 
                    CornPurchases (Id, ClientId, PurchasedAtUtc)
                VALUES (@Id, @ClientId, @PurchasedAtUtc);
            """;

            const string countSql = """
                SELECT 
                    COUNT(1) 
                FROM CornPurchases
                WHERE 
                    ClientId = @ClientId;
            """;

            await connection.ExecuteAsync(insertSql, new
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                PurchasedAtUtc = purchasedAtUtc
            });

            return await connection.ExecuteScalarAsync<int>(countSql, new
            {
                ClientId = clientId
            });
        }
    }
}
