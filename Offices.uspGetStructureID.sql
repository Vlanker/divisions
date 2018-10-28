CREATE PROCEDURE [Offices].[uspGetStructureID]  
@DepartamentID INT,  
@StructureID INT OUTPUT  
AS  
BEGIN  
SET @StructureID = (SELECT [S].[StructureID] FROM [Offices].[Structure] AS [S] WHERE (([S].AncestorID = @DepartamentID) AND ([S].[DescendarID] = @DepartamentID)));  
RETURN @@ERROR  
END