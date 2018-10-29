CREATE PROCEDURE [Offices].[uspGetRoots]
AS
BEGIN
	SELECT [D].[Title], [D].[DepartamentID]
	FROM [Departaments] AS [D]
	INNER JOIN [Structure] AS [S]
			ON [D].[DepartamentID] = [S].[DescendarID]
	WHERE [S].[Level] = 0
END