CREATE PROCEDURE [Offices].[uspGetTreePath]
	@Level INT,
	@AncestorID INT
AS
BEGIN
	SELECT [D].[Title], [D].[DepartamentID], [S].[Level]
	FROM [Departaments] AS D
	INNER JOIN [Structure] AS S
			ON [D].[DepartamentID] = [S].[DescendarID]
	WHERE (([S].[Level] =@Level) AND ([S].[AncestorID] = @AncestorID))
END