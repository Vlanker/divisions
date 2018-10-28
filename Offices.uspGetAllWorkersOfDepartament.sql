CREATE PROCEDURE [Offices].[uspGetAllWorkersOfDepartament]
	@AncestorID INT 
AS
BEGIN
	SELECT [D].[Title], [W].*
	FROM (([Offices].[Departaments] AS [D]
		INNER JOIN [Offices].[Structure] AS [S]
			ON ([D].[DepartamentID] = [S].[AncestorID]) AND ([D].[DepartamentID] = [S].[DescendarID]))
		INNER JOIN [Offices].[Workers] AS [W]
			ON [S].StructureID = [W].[StructureID])
	INNER JOIN (SELECT [Dd].[DepartamentID]
				FROM [Offices].[Departaments] AS [Dd]
				INNER JOIN [Offices].[Structure] AS [Ss]
					ON [Dd].[DepartamentID] = [Ss].[DescendarID]
				WHERE [Ss].[AncestorID] = @AncestorID) AS [TMP]
	ON [D].[DepartamentID] =[TMP].[DepartamentID]
	WHERE [D].[DepartamentID] = [TMP].[DepartamentID]
END