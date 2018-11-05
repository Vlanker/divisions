CREATE PROCEDURE [Offices].[uspInsertBranch]  
	@Title NVARCHAR (50), @DesedantID INT, @Level INT
AS  
BEGIN  
DECLARE @DepID INT  
BEGIN TRANSACTION  
INSERT INTO [Offices].[Departaments] ([Title])
VALUES (@Title)  

SELECT @DepID = SCOPE_IDENTITY();  

INSERT INTO [Offices].[Structure] ([AncestorID], [DescendarID], [Level])
(SELECT [S].[AncestorID],  @DepID, [S].[Level] + 1 FROM [Structure] AS [S] WHERE [S].[DescendarID] = @DesedantID UNION ALL SELECT @DepID, @DepID, @Level)

COMMIT TRANSACTION   
END