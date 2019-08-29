IF  NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'kpi_Group' AND type = 'U')
BEGIN
	-- create table kpi_Group 
	CREATE TABLE [dbo].[kpi_Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](64) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[EditedBy] [nvarchar](64) NOT NULL,
	[EditedDate] [datetime] NOT NULL,
		CONSTRAINT [PK_kpi_Group] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[kpi_Group] ADD  CONSTRAINT [DF_kpi_Group_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
	ALTER TABLE [dbo].[kpi_Group] ADD  CONSTRAINT [DF_kpi_Group_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
	ALTER TABLE [dbo].[kpi_Group] ADD  CONSTRAINT [DF_kpi_Group_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
END

IF  NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'kpi_Criterion' AND type = 'U')
BEGIN
	-- create table kpi_Criterion 
	CREATE TABLE [dbo].[kpi_Criterion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](64) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[Formula] [nvarchar](1024) NULL,
	[ValueType] [int] NOT NULL,
	[Order] [int] NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](64) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[EditedBy] [nvarchar](64) NOT NULL,
	[EditedDate] [datetime] NOT NULL,
	 CONSTRAINT [PK_kpi_Criterion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[kpi_Criterion] ADD  CONSTRAINT [DF_kpi_Criterion_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
	ALTER TABLE [dbo].[kpi_Criterion] ADD  CONSTRAINT [DF_kpi_Criterion_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
	ALTER TABLE [dbo].[kpi_Criterion] ADD  CONSTRAINT [DF_kpi_Criterion_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]

END

IF  NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'kpi_CriterionGroup' AND type = 'U')
BEGIN
	-- create table kpi_CriterionGroup 
	CREATE TABLE [dbo].[kpi_CriterionGroup](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[CriterionId] [int] NOT NULL,
		[GroupId] [int] NOT NULL,
	 CONSTRAINT [PK_kpi_CriterionGroup] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF  NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'kpi_Argument' AND type = 'U')
BEGIN
	-- create table kpi_Argument 
	CREATE TABLE [dbo].[kpi_Argument](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Code] [nvarchar](64) NOT NULL,
		[CalculateCode] [nvarchar](8) NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[Description] [nvarchar](1024) NULL,
		[ValueType] [int] NOT NULL,
		[Order] [int] NOT NULL,
		[Status] [int] NOT NULL,
		[IsDeleted] [bit] NOT NULL,
		[CreatedBy] [nvarchar](64) NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[EditedBy] [nvarchar](64) NOT NULL,
		[EditedDate] [datetime] NOT NULL,
	 CONSTRAINT [PK_kpi_Argument] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[kpi_Argument] ADD  CONSTRAINT [DF_kpi_Argument_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
	ALTER TABLE [dbo].[kpi_Argument] ADD  CONSTRAINT [DF_kpi_Argument_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
	ALTER TABLE [dbo].[kpi_Argument] ADD  CONSTRAINT [DF_kpi_Argument_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
END

IF  NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'kpi_EmployeeArgument' AND type = 'U')
BEGIN
	-- create table kpi_EmployeeArgument 
	CREATE TABLE [dbo].[kpi_EmployeeArgument](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[RecordId] [int] NOT NULL,
		[CriterionId] [int] NOT NULL,
		[ArgumentId] [int] NOT NULL,
		[Month] [int] NOT NULL,
		[Year] [int] NOT NULL,
		[Value] [nvarchar](128) NULL,
		[CreatedBy] [nvarchar](64) NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[EditedBy] [nvarchar](64) NOT NULL,
		[EditedDate] [datetime] NOT NULL,
	 CONSTRAINT [PK_kpi_EmployeeArgument] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[kpi_EmployeeArgument] ADD  CONSTRAINT [DF_kpi_EmployeeArgument_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
	ALTER TABLE [dbo].[kpi_EmployeeArgument] ADD  CONSTRAINT [DF_kpi_EmployeeArgument_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]
END

IF  NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'kpi_Evaluation' AND type = 'U')
BEGIN
	-- create table kpi_Evaluation 
	CREATE TABLE [dbo].[kpi_Evaluation](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[RecordId] [int] NOT NULL,
		[CriterionId] [int] NOT NULL,
		[Month] [int] NOT NULL,
		[Year] [int] NOT NULL,
		[Value] [nvarchar](128) NULL,
	 CONSTRAINT [PK_kpi_Evaluation] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END