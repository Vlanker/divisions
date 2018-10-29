PRINT N'Creating Divisions...';  
GO  
CREATE SCHEMA [Divisions]  
    AUTHORIZATION [dbo];  
GO  
PRINT N'Creating Divisions.Departaments...';  
GO  
CREATE TABLE [Divisions].[Departaments] (  
    [DepartamentID]   INT           IDENTITY (1, 1) NOT NULL,  
    [DepartamentName] NVARCHAR (40)					NOT NULL,  
    [ParentID]		  INT							NOT NULL,  
      
);  
GO  
PRINT N'Creating Divisions.Workers...';  
GO  
CREATE TABLE [Divisions].[Workers] (
    [WorkerID]      INT            IDENTITY (1, 1) NOT NULL,
    [DepartamentID] INT            NOT NULL,
    [PersNum]       NVARCHAR (50)  NOT NULL,
    [FullName]      NVARCHAR (250) NOT NULL,
    [Birthday]      DATE           NOT NULL,
    [HiringDay]     DATE           NOT NULL,
    [Salary]        MONEY          NOT NULL,
    [ProfArea]      NVARCHAR (150) NOT NULL,
    [Gender]        NVARCHAR (4)   NOT NULL,  
);  
GO  
PRINT N'Creating Divisions.Def_Departaments_ParentID...';  
GO  
ALTER TABLE [Divisions].[Departaments]  
    ADD CONSTRAINT [Def_Departaments_ParentID] DEFAULT 0 FOR [ParentID];  
GO  
PRINT N'Creating Divisions.Def_Workers_HiringDay...';  
GO  
ALTER TABLE [Divisions].[Workers]  
    ADD CONSTRAINT [Def_Workers_HiringDay] DEFAULT Getdate() FOR [HiringDay];  
GO  
PRINT N'Creating Divisions.Def_Workers_Salary...';  
GO  
ALTER TABLE [Divisions].[Workers] 
    ADD CONSTRAINT [Def_Workers_Salary] DEFAULT 0 FOR [Salary];  
GO  
PRINT N'Creating Divisions.Def_Workers_ProfArea...';  
GO  
ALTER TABLE [Divisions].[Workers]  
    ADD CONSTRAINT [Def_Workers_ProfArea] DEFAULT '-' FOR [ProfArea];  
GO  
PRINT N'Creating Divisions.PK_Departaments_DepartametID...';  
GO  
ALTER TABLE [Divisions].[Departaments]  
    ADD CONSTRAINT [PK_Departaments_DepartametID] PRIMARY KEY CLUSTERED ([DepartametID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);  
GO  
PRINT N'Creating Divisions.PK_Workers_WorkerID...';  
GO  
ALTER TABLE [Divisions].[Workers]  
    ADD CONSTRAINT [PK_Workers_WorkerID] PRIMARY KEY CLUSTERED ([WorkerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);  

GO  
PRINT N'Creating Sales.FK_Workers_Departaments_DepartamentID...';  
GO  
ALTER TABLE [Divisions].[Workers]  
    ADD CONSTRAINT [FK_Workers_Departaments_DepartamentID] FOREIGN KEY ([DepartamentID]) REFERENCES [Divisions].[Departaments]  ([DepartamentID]) ON DELETE NO ACTION ON UPDATE NO ACTION;  
GO