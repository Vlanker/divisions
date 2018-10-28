CREATE PROCEDURE [Offices].[uspInsertRoot]  
@Title NVARCHAR (50)
AS  
BEGIN  
DECLARE @DepID INT  
BEGIN TRANSACTION  
INSERT INTO [Offices].[Departaments] ([Title])
     VALUES (@Title)  
SELECT @DepID = SCOPE_IDENTITY();  
INSERT INTO [Offices].[Structure] ([AncestorID], [DescendarID], [Level])
		VALUES(@DepID, @DepID, 0)
COMMIT TRANSACTION   
END