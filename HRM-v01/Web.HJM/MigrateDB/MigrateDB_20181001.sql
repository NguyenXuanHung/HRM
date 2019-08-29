DECLARE @LastMigrate NVARCHAR(14)
SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'

IF @LastMigrate IS NULL
BEGIN
	INSERT INTO [SystemConfig] VALUES ('LAST_MIGRATE', '20180101', NULL, getdate(), 'system', NULL, NULL)
END

SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'

IF @LastMigrate < '20181001'
BEGIN
	-- add column into table training history
	IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name IN ( N'FieldTrainingId', N'OrganizeDepartmentId', N'DocumentNumber')
      AND Object_ID = Object_ID(N'hr_TrainingHistory'))
	BEGIN
		ALTER TABLE hr_TrainingHistory
		ADD 
		[FieldTrainingId] int,
		[OrganizeDepartmentId] int,
		[DocumentNumber] NVARCHAR(64)
	END

	IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name IN ( N'TrainingStatusId', N'Type')
      AND Object_ID = Object_ID(N'hr_TrainingHistory'))
	BEGIN
		ALTER TABLE hr_TrainingHistory
		ADD 
		[TrainingStatusId] int,
		[Type] NVARCHAR(64)
	END

	-- add column into table business history
	ALTER TABLE hr_BusinessHistory
	ADD 
	[PlanJobTitleId] int,
	[PlanPhaseId] int,
	[TrainingStatusId] int

	-- add column into table record
	ALTER TABLE hr_Record
	ADD 
	[FamilyIncome] int,
	[OtherIncome] NVARCHAR(64),
	[AllocatedHouse] NVARCHAR(64),
	[AllocatedtHouseArea] float,
	[House] NVARCHAR(64),
	[HouseArea] float,
	[AllocatedLandArea] float,
	[BusinessLandArea] NVARCHAR(64),
	[LandArea] float
		
	UPDATE [SystemConfig] Set [Value]='20181001' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

-- Create new catalog
IF @LastMigrate < '20181002'
BEGIN
	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_PlanJobTitle' AND type = 'U')
	BEGIN
		-- create table catalog plan job title (chức danh quy hoạch)
		CREATE TABLE [dbo].[cat_PlanJobTitle](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Code] [nvarchar](64) NOT NULL,
			[Name] [nvarchar](256) NOT NULL,
			[Description] [nvarchar](1024) NULL,
			[Group] [nvarchar](64) NOT NULL,
			[Order] [int] NOT NULL,
			[Status] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,		
			[CreatedBy] [nvarchar](64) NOT NULL,
			[CreatedDate] [datetime] NULL,
			[EditedBy] [nvarchar](64) NOT NULL,
			[EditedDate] [datetime] NULL		
		 CONSTRAINT [PK_cat_PlanJobTitle] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]	
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_Name]  DEFAULT ('') FOR [Name]	
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_Description]  DEFAULT ('') FOR [Description]	
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_Order]  DEFAULT ((0)) FOR [Order]		
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_Status]  DEFAULT ((1)) FOR [Status]
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]		
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]	
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]	
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_EditedBy]  DEFAULT (N'system') FOR [EditedBy]	
		ALTER TABLE [dbo].[cat_PlanJobTitle] ADD CONSTRAINT [DF_cat_PlanJobTitle_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]		
	END

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_PlanPhase' AND type = 'U')
	BEGIN
		-- create table catalog plan phase (giai đoạn quy hoạch)
		CREATE TABLE [dbo].[cat_PlanPhase](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Code] [nvarchar](64) NOT NULL,
			[Name] [nvarchar](256) NOT NULL,
			[Description] [nvarchar](1024) NULL,
			[Group] [nvarchar](64) NOT NULL,
			[Order] [int] NOT NULL,
			[Status] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,		
			[CreatedBy] [nvarchar](64) NOT NULL,
			[CreatedDate] [datetime] NULL,
			[EditedBy] [nvarchar](64) NOT NULL,
			[EditedDate] [datetime] NULL	
		 CONSTRAINT [PK_cat_PlanPhase] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_Name]  DEFAULT ('') FOR [Name]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_Description]  DEFAULT ('') FOR [Description]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_Order]  DEFAULT ((0)) FOR [Order]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_Status]  DEFAULT ((1)) FOR [Status]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]	
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_EditedBy]  DEFAULT (N'system') FOR [EditedBy]
		ALTER TABLE [dbo].[cat_PlanPhase] ADD CONSTRAINT [DF_cat_PlanPhase_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]	
	END

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_FieldOfTraining' AND type = 'U')
	BEGIN
		-- create table catalog field of training (lĩnh vực đào tạo)
		CREATE TABLE [dbo].[cat_FieldOfTraining](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Code] [nvarchar](64) NOT NULL,
			[Name] [nvarchar](256) NOT NULL,
			[Description] [nvarchar](1024) NULL,
			[Group] [nvarchar](64) NOT NULL,
			[Order] [int] NOT NULL,
			[Status] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,		
			[CreatedBy] [nvarchar](64) NOT NULL,
			[CreatedDate] [datetime] NULL,
			[EditedBy] [nvarchar](64) NOT NULL,
			[EditedDate] [datetime] NULL	
		 CONSTRAINT [PK_cat_FieldOfTraining] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_Name]  DEFAULT ('') FOR [Name]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_Description]  DEFAULT ('') FOR [Description]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_Order]  DEFAULT ((0)) FOR [Order]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_Status]  DEFAULT ((1)) FOR [Status]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]	
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_EditedBy]  DEFAULT (N'system') FOR [EditedBy]
		ALTER TABLE [dbo].[cat_FieldOfTraining] ADD CONSTRAINT [DF_cat_FieldOfTraining_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
	END

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_TrainingOrganization' AND type = 'U')
	BEGIN
		-- create table catalog training org (đơn vị tổ chức đào tạo)
		CREATE TABLE [dbo].[cat_TrainingOrganization](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Code] [nvarchar](64) NOT NULL,
			[Name] [nvarchar](256) NOT NULL,
			[Description] [nvarchar](1024) NULL,
			[Group] [nvarchar](64) NOT NULL,
			[Order] [int] NOT NULL,
			[Status] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,		
			[CreatedBy] [nvarchar](64) NOT NULL,
			[CreatedDate] [datetime] NULL,
			[EditedBy] [nvarchar](64) NOT NULL,
			[EditedDate] [datetime] NULL
		 CONSTRAINT [PK_cat_TrainingOrganization] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_Name]  DEFAULT ('') FOR [Name]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_Description]  DEFAULT ('') FOR [Description]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_Order]  DEFAULT ((0)) FOR [Order]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_Status]  DEFAULT ((1)) FOR [Status]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]	
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_EditedBy]  DEFAULT (N'system') FOR [EditedBy]
		ALTER TABLE [dbo].[cat_TrainingOrganization] ADD CONSTRAINT [DF_cat_TrainingOrganization_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
	END

	UPDATE [SystemConfig] Set [Value]='20181002' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

-- Add column status into existed catalog
IF @LastMigrate < '20181003'
BEGIN
	--rename table
	EXEC sp_rename 'cat_HealthInsurance', 'cat_HealthInsurancePlace';  
	EXEC sp_rename 'cat_PassportIssuePlase', 'cat_PassportIssuePlace';  
	EXEC sp_rename 'cat_SocialInsurance', 'cat_SocialInsurancePlace'; 
	-- add status column into catalog
	ALTER TABLE [dbo].[cat_Ability] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Ability_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_ArmyLevel] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ArmyLevel_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_ArmyPosition] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ArmyPosition_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Bank] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Bank_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_BasicEducation] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_BasicEducation_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_BasicSalary] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_BasicSalary_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Certificate] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Certificate_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_ContractStatus] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ContractStatus_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_ContractType] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ContractType_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_CPVPosition] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_CPVPosition_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Department] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Department_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Department] ADD [Code] nvarchar(64) NOT NULL CONSTRAINT [DF_cat_Department_Code] DEFAULT (('')) WITH VALUES
	ALTER TABLE [dbo].[cat_Department] DROP COLUMN MA_DONVI
	ALTER TABLE [dbo].[cat_Discipline] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Discipline_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Education] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Education_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_EmployeeType] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_EmployeeType_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_FamilyClass] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_FamilyClass_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_FamilyPolicy] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_FamilyPolicy_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Folk] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Folk_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Folk] ADD [Code] nvarchar(64) NOT NULL CONSTRAINT [DF_cat_Folk_Code] DEFAULT (('')) WITH VALUES
	ALTER TABLE [dbo].[cat_GraduationType] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_GraduationType_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_GroupQuantum] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_GroupQuantum_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_GroupTimeSheetSymbol] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_GroupTimeSheetSymbol_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_HealthInsurancePlace] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_HealthInsurancePlace_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_HealthStatus] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_HealthStatus_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Holiday] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Holiday_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_IDIssuePlace] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_IDIssuePlace_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Industry] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Industry_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_ITLevel] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ITLevel_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_JobTitle] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_JobTitle_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_LanguageLevel] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_LanguageLevel_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_LevelRewardDiscipline] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_LevelRewardDiscipline_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Location] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Location_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Location] ADD [Code] nvarchar(64) NOT NULL CONSTRAINT [DF_cat_Location_Code] DEFAULT (('')) WITH VALUES
	ALTER TABLE [dbo].[cat_ManagementLevel] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ManagementLevel_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_MaritalStatus] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_MaritalStatus_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_MedicalPlace] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_MedicalPlace_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Nation] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Nation_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_PassportIssuePlace] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_PassportIssuePlace_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_PayrollConfig] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_PayrollConfig_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_PayrollConfig] ADD [Code] nvarchar(64) NOT NULL CONSTRAINT [DF_cat_PayrollConfig_Code] DEFAULT (('')) WITH VALUES
	ALTER TABLE [dbo].[cat_PersonalClass] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_PersonalClass_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_PolicePosition] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_PolicePosition_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_PoliticLevel] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_PoliticLevel_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Position] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Position_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Quantum] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Quantum_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Quantum] DROP COLUMN MA_NHOMNGACH
	ALTER TABLE [dbo].[cat_ReasonDiscipline] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ReasonDiscipline_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_ReasonReward] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_ReasonReward_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_RecruitmentType] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_RecruitmentType_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Relationship] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Relationship_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Religion] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Religion_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_Reward] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_Reward_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_SalaryLevelQuantum] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_SalaryLevelQuantum_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_SalaryLevelQuantum] DROP COLUMN MA_NGACH
	ALTER TABLE [dbo].[cat_SalaryLevelQuantum] DROP COLUMN MA_NHOM_NGACH
	ALTER TABLE [dbo].[cat_SchedulerType] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_SchedulerType_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_SchedulerType] ADD [Code] nvarchar(64) NOT NULL CONSTRAINT [DF_cat_SchedulerType_Code] DEFAULT (('')) WITH VALUES
	ALTER TABLE [dbo].[cat_SocialInsurancePlace] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_SocialInsurancePlace_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_TimeSheetSymbol] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_TimeSheetSymbol_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_TrainingSystem] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_TrainingSystem_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_University] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_University_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_VYUPosition] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_VYUPosition_Status] DEFAULT ((1)) WITH VALUES
	ALTER TABLE [dbo].[cat_WorkStatus] ADD [Status] int NOT NULL CONSTRAINT [DF_cat_WorkStatus_Status] DEFAULT ((1)) WITH VALUES

	UPDATE [SystemConfig] Set [Value]='20181003' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

-- Create new catalog
IF @LastMigrate < '20181004'
BEGIN
	ALTER TABLE [dbo].[hr_Record]
	ADD 
	[GovernmentPositionId] int DEFAULT ((0)) WITH VALUES,
	[PluralityPositionId] int DEFAULT ((0)) WITH VALUES

	UPDATE [SystemConfig] Set [Value]='20181004' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181005'
BEGIN
	ALTER TABLE [dbo].[hr_SalaryBoardList]
	ADD 
	[Data] NVARCHAR(MAX)

	-- add column hr_Salary
	ALTER TABLE [dbo].[hr_Salary]
	ADD 
	SalaryRaiseNextDate DATE,
	ProlongRaiseSalaryDisciplineDate DATE,
	Reason nvarchar(MAX),
	SalaryRaiseTypeId int,
	StatusId int

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_Construction' AND type = 'U')
	BEGIN
		-- create cat_contraction
		CREATE TABLE [dbo].[cat_Construction](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Name] [nvarchar](256) NOT NULL,
			[Description] [nvarchar](1024) NULL,
			[Order] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,
			[Group] [nvarchar](64) NOT NULL,
			[CreatedBy] [nvarchar](64) NOT NULL,
			[CreatedDate] [datetime] NOT NULL,
			[EditedBy] [nvarchar](64) NOT NULL,
			[EditedDate] [datetime] NOT NULL,
			[Code] [nvarchar](64) NOT NULL,
			[Status] [int] NOT NULL,
		 CONSTRAINT [PK_cat_Construction] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_Name]  DEFAULT ('') FOR [Name]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_Description]  DEFAULT ('') FOR [Description]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_Order]  DEFAULT ((0)) FOR [Order]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_CreatedBy]  DEFAULT (N'admin') FOR [CreatedBy]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_EditedBy]  DEFAULT (N'admin') FOR [EditedBy]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_Construction] ADD  CONSTRAINT [DF_cat_Construction_Status]  DEFAULT ((1)) FOR [Status]
	END

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_Team' AND type = 'U')
	BEGIN
		-- create cat_Team
		CREATE TABLE [dbo].[cat_Team](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[Description] [nvarchar](1024) NULL,
		[Order] [int] NOT NULL,
		[IsDeleted] [bit] NOT NULL,
		[Group] [nvarchar](64) NOT NULL,
		[CreatedBy] [nvarchar](64) NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[EditedBy] [nvarchar](64) NOT NULL,
		[EditedDate] [datetime] NOT NULL,
		[Code] [nvarchar](64) NOT NULL,
		[Status] [int] NOT NULL,
		 CONSTRAINT [PK_cat_Team] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_Name]  DEFAULT ('') FOR [Name]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_Description]  DEFAULT ('') FOR [Description]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_Order]  DEFAULT ((0)) FOR [Order]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_CreatedBy]  DEFAULT (N'admin') FOR [CreatedBy]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_EditedBy]  DEFAULT (N'admin') FOR [EditedBy]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_Team] ADD  CONSTRAINT [DF_cat_Team_Status]  DEFAULT ((1)) FOR [Status]
	END

	UPDATE [SystemConfig] Set [Value]='20181005' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181008'
BEGIN
	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'cat_PoliceLevel' AND type = 'U')
	BEGIN
		--create cat_PoliceLevel
		CREATE TABLE [dbo].[cat_PoliceLevel](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[Description] [nvarchar](1024) NULL,
		[Order] [int] NOT NULL,
		[IsDeleted] [bit] NOT NULL,
		[Group] [nvarchar](64) NOT NULL,
		[CreatedBy] [nvarchar](64) NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[EditedBy] [nvarchar](64) NOT NULL,
		[EditedDate] [datetime] NOT NULL,
		[Code] [nvarchar](64) NOT NULL,
		[Status] [int] NOT NULL,
		 CONSTRAINT [PK_cat_PoliceLevel] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_Name]  DEFAULT ('') FOR [Name]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_Description]  DEFAULT ('') FOR [Description]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_Order]  DEFAULT ((0)) FOR [Order]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_Group]  DEFAULT ('') FOR [Group]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_CreatedBy]  DEFAULT (N'admin') FOR [CreatedBy]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_EditedBy]  DEFAULT (N'admin') FOR [EditedBy]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_Code]  DEFAULT ('') FOR [Code]
		ALTER TABLE [dbo].[cat_PoliceLevel] ADD  CONSTRAINT [DF_cat_PoliceLevel_Status]  DEFAULT ((1)) FOR [Status]
	END

	UPDATE [SystemConfig] Set [Value]='20181008' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181009'
BEGIN
	-- drop old report table
	IF OBJECT_ID('dbo.ReportList_ReportTableFilter', 'U') IS NOT NULL 
	  DROP TABLE dbo.ReportList_ReportTableFilter; 

	IF OBJECT_ID('dbo.ReportTableFilter', 'U') IS NOT NULL 
	  DROP TABLE dbo.ReportTableFilter; 

	IF OBJECT_ID('dbo.ReportList', 'U') IS NOT NULL 
	  DROP TABLE dbo.ReportList; 

	IF OBJECT_ID('dbo.ReportGroup', 'U') IS NOT NULL 
	  DROP TABLE dbo.ReportGroup; 

	IF OBJECT_ID('dbo.Category', 'U') IS NOT NULL 
	  DROP TABLE dbo.Category; 

	-- create new table for report list
	CREATE TABLE [dbo].[ReportList](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[Description] [nvarchar](1024) NULL,
		[ReportSource] [nvarchar](max) NOT NULL,
		[Template] [int] NOT NULL,
		[GroupHeader] [int] NOT NULL,
		[ParentDepartment] [nvarchar](256) NOT NULL,
		[Department] [nvarchar](256) NOT NULL,
		[Title] [nvarchar](256) NOT NULL,
		[Duration] [nvarchar](256) NOT NULL,
		[FilterCondition] [nvarchar](2048) NOT NULL,
		[CreatedByTitle] [nvarchar](256) NOT NULL,
		[CreatedByNote] [nvarchar](256) NOT NULL,
		[CreatedByName] [nvarchar](256) NOT NULL,
		[ReviewedByTitle] [nvarchar](256) NOT NULL,
		[ReviewedByNote] [nvarchar](256) NOT NULL,
		[ReviewedByName] [nvarchar](256) NOT NULL,
		[SignedByTitle] [nvarchar](256) NOT NULL,
		[SignedByNote] [nvarchar](256) NOT NULL,
		[SignedByName] [nvarchar](256) NOT NULL,
		[ReportDate] [datetime] NOT NULL,
		[FromDate] [datetime] NULL,
		[ToDate] [datetime] NULL,
		[CreatedBy] [nvarchar](64) NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[EditedBy] [nvarchar](64) NOT NULL,
		[EditedDate] [datetime] NOT NULL,
	 CONSTRAINT [PK_ReportList] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_Name]  DEFAULT ('') FOR [Name]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_Description]  DEFAULT ('') FOR [Description]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_ReportSource]  DEFAULT ('') FOR [ReportSource]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_Template]  DEFAULT ((1)) FOR [Template]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_GroupHeader]  DEFAULT ((0)) FOR [GroupHeader]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_ParentDepartment]  DEFAULT ('') FOR [ParentDepartment]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_Department]  DEFAULT ('') FOR [Department]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_Title]  DEFAULT ('') FOR [Title]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_Duration]  DEFAULT ('') FOR [Duration]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_FilterCondition]  DEFAULT ('') FOR [FilterCondition]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_CreatedByTitle]  DEFAULT ('') FOR [CreatedByTitle]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_CreatedByNote]  DEFAULT ('') FOR [CreatedByNote]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_CreatedByName]  DEFAULT ('') FOR [CreatedByName]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_ReviewedByTitle]  DEFAULT ('') FOR [ReviewedByTitle]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_ReviewedByNote]  DEFAULT ('') FOR [ReviewedByNote]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_ReviewedByName]  DEFAULT ('') FOR [ReviewedByName]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_SignedByTitle]  DEFAULT ('') FOR [SignedByTitle]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_SignedByNote]  DEFAULT ('') FOR [SignedByNote]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_SignedByName]  DEFAULT ('') FOR [SignedByName]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_ReportDate]  DEFAULT (getdate()) FOR [ReportDate]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_EditedBy]  DEFAULT (N'system') FOR [EditedBy]
	ALTER TABLE [dbo].[ReportList] ADD  CONSTRAINT [DF_ReportList_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]

	-- create new table for report column
	CREATE TABLE [dbo].[ReportColumn](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[ReportId] [int] NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[DisplayText] [nvarchar](256) NOT NULL,
		[FieldName] [nvarchar](64) NOT NULL,
		[HeaderTextAlign] [int] NOT NULL,
		[HeaderFontSize] [int] NOT NULL,
		[HeaderFormat] [nvarchar](64) NOT NULL,
		[DetailTextAlign] [int] NOT NULL,
		[DetailFontSize] [int] NOT NULL,
		[DetailFormat] [nvarchar](64) NOT NULL,
		[Width] [int] NOT NULL,
		[Height] [int] NOT NULL,
		[ParentId] [int] NOT NULL,
		[Order] [int] NOT NULL,
		[IsGroup] [bit] NOT NULL,
		[IsDeleted] [bit] NOT NULL,
		[Status] [int] NOT NULL,
		[Type] [int] NOT NULL,
		[SummaryRunning] [int] NOT NULL,
		[SummaryFunction] [int] NOT NULL,
		[SummaryValue] [nvarchar](64) NOT NULL,
		[CreatedBy] [nvarchar](64) NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[EditedBy] [nvarchar](64) NOT NULL,
		[EditedDate] [datetime] NOT NULL,
	 CONSTRAINT [PK_ReportColumn] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_Name]  DEFAULT ('') FOR [Name]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_DisplayText]  DEFAULT ('') FOR [DisplayText]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_FieldName]  DEFAULT ('') FOR [FieldName]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_HeaderTextAlign]  DEFAULT ((9)) FOR [HeaderTextAlign]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_HeaderFontSize]  DEFAULT ((6)) FOR [HeaderFontSize]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_HeaderFormat]  DEFAULT ('') FOR [HeaderFormat]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_DetailTextAlign]  DEFAULT ((9)) FOR [DetailTextAlign]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_DetailFontSize]  DEFAULT ((5)) FOR [DetailFontSize]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_DetailFormat]  DEFAULT ('') FOR [DetailFormat]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_Width]  DEFAULT ((100)) FOR [Width]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_Height]  DEFAULT ((24)) FOR [Height]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_ParentId]  DEFAULT ((0)) FOR [ParentId]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_Order]  DEFAULT ((0)) FOR [Order]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_IsGroup]  DEFAULT ((0)) FOR [IsGroup]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_Status]  DEFAULT ((1)) FOR [Status]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_Type]  DEFAULT ((1)) FOR [Type]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_SummaryRunning]  DEFAULT ((1)) FOR [SummaryRunning]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_SummaryFunction]  DEFAULT ((1)) FOR [SummaryFunction]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_SummaryValue]  DEFAULT ('') FOR [SummaryValue]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_EditedBy]  DEFAULT (N'system') FOR [EditedBy]
	ALTER TABLE [dbo].[ReportColumn] ADD  CONSTRAINT [DF_ReportColumn_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
	
	-- update
	update hr_Salary set SalaryRaiseTypeId = 0,
	StatusId = 0

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'hr_FluctuationEmployee' AND type = 'U')
	BEGIN
		-- create hr_FluctuationEmployee
		CREATE TABLE [dbo].[hr_FluctuationEmployee](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[RecordId] [int] NULL,
		[Reason] [nvarchar](max) NULL,
		[Date] [date] NULL,
		[Type] [bit] NOT NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedBy] [nchar](10) NULL,
		[EditedDate] [datetime] NULL,
		[EditedBy] [nchar](10) NULL,
		 CONSTRAINT [PK_hr_FluctuationEmployee] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_RecordId]  DEFAULT ((0)) FOR [RecordId]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_ReasonIncrease]  DEFAULT ('') FOR [Reason]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_IncreaseDate]  DEFAULT (getdate()) FOR [Date]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_Type]  DEFAULT ((0)) FOR [Type]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_CreatedBy]  DEFAULT (N'admin') FOR [CreatedBy]
		ALTER TABLE [dbo].[hr_FluctuationEmployee] ADD  CONSTRAINT [DF_hr_FluctuationEmployee_EditedBy]  DEFAULT (N'admin') FOR [EditedBy]
	END

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'hr_FluctuationInsurance' AND type = 'U')
	BEGIN
		-- create hr_FluctuationInsurance
		CREATE TABLE [dbo].[hr_FluctuationInsurance](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[RecordId] [int] NULL,
		[Type] [bit] NULL,
		[Reason] [nvarchar](max) NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedBy] [nchar](10) NULL,
		[EditedDate] [datetime] NULL,
		[EditedBy] [nchar](10) NULL,
		 CONSTRAINT [PK_hr_FluctuationInsurance] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
		ALTER TABLE [dbo].[hr_FluctuationInsurance] ADD  CONSTRAINT [DF_hr_FluctuationInsurance_Type]  DEFAULT ((0)) FOR [Type]
		ALTER TABLE [dbo].[hr_FluctuationInsurance] ADD  CONSTRAINT [DF_hr_FluctuationInsurance_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[hr_FluctuationInsurance] ADD  CONSTRAINT [DF_hr_FluctuationInsurance_CreatedBy]  DEFAULT (N'admin') FOR [CreatedBy]
		ALTER TABLE [dbo].[hr_FluctuationInsurance] ADD  CONSTRAINT [DF_hr_FluctuationInsurance_EditedBy]  DEFAULT (N'admin') FOR [EditedBy]
	END

	IF  NOT EXISTS (SELECT * FROM sys.tables
	WHERE name = N'hr_Team' AND type = 'U')
	BEGIN
		-- create hr_Team
		CREATE TABLE [dbo].[hr_Team](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[RecordId] [int] NOT NULL,
		[TeamId] [int] NULL,
		[ConstructionId] [int] NULL,
		[WorkingFormId] [int] NULL,
		[ProbationWorkingTime] [int] NULL,
		[StudyWorkingDay] [int] NULL,
		[WorkLocationId] [int] NULL,
		[CreatedDate] [datetime] NOT NULL,
		[CreatedBy] [nvarchar](64) NULL,
		[EditedDate] [datetime] NULL,
		[EditedBy] [nvarchar](64) NULL,
		[UnionJoinedDate] [date] NULL,
		[UnionJoinedPlace] [nvarchar](256) NULL,
		[UnionJoinedPosition] [nvarchar](256) NULL,
		[GraduationYear] [int] NULL,
		[GraduationTypeId] [int] NULL,
		[UniversityId] [int] NULL,
		[HealthInsuranceNumber] [nvarchar](50) NULL,
		[HealthJoinedDate] [datetime] NULL,
		[HealthExpiredDate] [datetime] NULL,
		 CONSTRAINT [PK_hr_Team] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_RecordId]  DEFAULT ((0)) FOR [RecordId]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_TeamId]  DEFAULT ((0)) FOR [TeamId]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_ConstructionId]  DEFAULT ((0)) FOR [ConstructionId]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_WorkingFormId]  DEFAULT ((0)) FOR [WorkingFormId]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_ProbationWorkingTime]  DEFAULT ((0)) FOR [ProbationWorkingTime]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_StudyWorkingDay]  DEFAULT ((0)) FOR [StudyWorkingDay]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_WorkLocationId]  DEFAULT ((0)) FOR [WorkLocationId]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_CreatedBy]  DEFAULT ('admin') FOR [CreatedBy]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
		ALTER TABLE [dbo].[hr_Team] ADD  CONSTRAINT [DF_hr_Team_EditedBy]  DEFAULT ('') FOR [EditedBy]
	END

UPDATE [SystemConfig] Set [Value]='20181009' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181010'
BEGIN
	-- add column into table hr_TimeSheetRuleEarlyOrLate
	IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name IN ( N'SymbolId')
      AND Object_ID = Object_ID(N'hr_TimeSheetRuleEarlyOrLate'))
	BEGIN
		ALTER TABLE hr_TimeSheetRuleEarlyOrLate
		ADD [SymbolId] int
	END

UPDATE [SystemConfig] Set [Value]='20181010' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181011'
BEGIN
	ALTER TABLE [dbo].[hr_Record]
	ADD 
	[RevolutionJoinedDate] DATE,
	[LongestJob] NVARCHAR(128)

	UPDATE [SystemConfig] Set [Value]='201810011' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181012'
BEGIN
	-- update menu hrm
	update Menu
	set LinkUrl = N'Modules/TimeKeeping/TimeSheetAdjustment.aspx?AdjustType=AdjustOverTime'
	where MenuName like N'%Thêm giờ%'

	--rename table
	EXEC sp_rename 'UserRuleTimeSheet', 'hr_TimeSheetUserRule';
	EXEC sp_rename 'MachineTimeSheet', 'hr_TimeSheetMachine';

	UPDATE [SystemConfig] Set [Value]='201810012' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181015'
BEGIN
	-- add column hr_TimeSheetWorkShift for hrm
	ALTER TABLE hr_TimeSheetWorkShift 
	ADD StartDate datetime,
	EndDate datetime

	UPDATE [SystemConfig] Set [Value]='201810015' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END

IF @LastMigrate < '20181013'
BEGIN
	-- add column into table hr_SalaryBoardList
	IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name IN ( N'IsDeleted',N'Status')
      AND Object_ID = Object_ID(N'hr_SalaryBoardList'))
	BEGIN
		ALTER TABLE hr_SalaryBoardList
		ADD 
		[Status] int CONSTRAINT [DF_hr_SalaryBoardList_Status] DEFAULT (1) NOT NULL,
		[IsDeleted] bit CONSTRAINT [DF_hr_SalaryBoardList_IsDeleted] DEFAULT (0) NOT NULL
	END
		
	UPDATE [SystemConfig] Set [Value]='20181013' WHERE [Name]='LAST_MIGRATE'
	SELECT @LastMigrate = [Value] FROM [SystemConfig] WHERE [Name]='LAST_MIGRATE'
END