CREATE PROCEDURE [Offices].[uspGetWorkers]
	@DepartamentID INT
AS
BEGIN
	SELECT [W].*
	FROM ([Departaments] AS D 
		INNER JOIN [Structure] AS S
				ON (([D].[DepartamentID] = [S].[AncestorID]) AND([D].[DepartamentID] = [S].[DescendarID])))
	INNER JOIN [Workers] AS W
			ON [S].[StructureID] = [W].[StructureID]
	WHERE [D].[DepartamentID] = @DepartamentID
END