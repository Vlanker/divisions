CREATE PROCEDURE [Offices].[uspDeleteSubtree]
	@DepartamentID INT
AS
BEGIN
	DELETE FROM [Offices].[Structure] 
	WHERE [Structure].[DescendarID] IN (SELECT [S].[DescendarID] FROM [Offices].[Structure] AS [S] WHERE [S].[AncestorID] = @DepartamentID)
END