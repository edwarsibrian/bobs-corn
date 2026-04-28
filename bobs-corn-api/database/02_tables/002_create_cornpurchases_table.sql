USE BobsCornDb;
GO

IF OBJECT_ID('dbo.CornPurchases', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.CornPurchases 
	(
		Id				UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
		ClientId		NVARCHAR(100) NOT NULL,
		PurchasedAtUtc	DATETIMEOFFSET NOT NULL
	);

	CREATE INDEX IX_CornPurchases_ClientId 
		ON dbo.CornPurchases(ClientId);
END
GO