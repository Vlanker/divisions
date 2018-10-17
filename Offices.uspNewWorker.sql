CREATE PROCEDURE [Offices].[uspNewWorker]
@StructureID INT,
@PersNum NVARCHAR (50),
@FullName NVARCHAR (250),
@Birthday DATE,
@HiringDay DATE,
@Salary MONEY,
@ProfArea NVARCHAR (150),
@Gender BIT
AS  
BEGIN  
INSERT INTO [Offices].[Workers] (StructureID, PersNum, FullName, Birthday, HiringDay, Salary, ProfArea, Gender) 
VALUES (@StructureID, @PersNum, @FullName, @Birthday, @HiringDay, @Salary, @ProfArea, @Gender);  

END
