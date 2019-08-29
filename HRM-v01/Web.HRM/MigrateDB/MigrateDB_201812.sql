IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name IN ( N'TeamId', N'ConstructionId', N'WorkingFormId', N'ProbationWorkingTime', N'StudyWorkingDay',
					N'WorkLocationId', N'UnionJoinedDate', N'UnionJoinedPlace',
					N'UnionJoinedPositionId', N'GraduationYear', N'GraduationTypeId',
					N'UniversityId', N'HealthInsuranceNumber', N'HealthJoinedDate', N'HealthExpiredDate')
      AND Object_ID = Object_ID(N'hr_Record'))
	BEGIN
		ALTER TABLE hr_Record
		ADD 
		[TeamId] int,
		[ConstructionId] int,
		[WorkingFormId] int,
		[ProbationWorkingTime] int,
		[StudyWorkingDay] int,
		[WorkLocationId] int,
		[UnionJoinedDate] date NULL,
		[UnionJoinedPlace] nvarchar(256) NULL,
		[UnionJoinedPositionId] int NULL,
		[GraduationYear] int NULL,
		[GraduationTypeId] int NULL,
		[UniversityId] int NULL,
		[HealthInsuranceNumber] nvarchar(64) NULL,
		[HealthJoinedDate] date NULL,
		[HealthExpiredDate] date NULL
	END 

	-- update value
	update hr_Record set
	[TeamId] = 0,
	[ConstructionId] = 0,
	[WorkingFormId] = 1,
	[ProbationWorkingTime] = 0,
	[StudyWorkingDay] = 0,
	[WorkLocationId] = 0,
	[UnionJoinedPositionId] = 0,
	[GraduationYear] = 0,
	[GraduationTypeId] = 0,
	[UniversityId] = 0,
	[HealthInsuranceNumber] = ''

IF NOT EXISTS(
	SELECT * FROM sys.columns 
	WHERE NAME IN (N'PaperKind',N'Orientation')
		AND Object_ID = Object_ID(N'ReportDynamic'))
BEGIN
	ALTER TABLE ReportDynamic
	ADD
	[PaperKind] int default 0 NOT NULL,
	[Orientation] int default 0 NOT NULL
END

--Update value
UPDATE ReportDynamic SET
[PaperKind] = 1,
[Orientation] = 1

-- recruitment
--create [rec_RequiredRecruitment]
CREATE TABLE [dbo].[rec_RequiredRecruitment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](64) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[JobTitlePositionId] [int] NOT NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NOT NULL,
	[ExpiredDate] [date] NOT NULL,
	[Quantity] [int] NOT NULL,
	[WorkPlace] [nvarchar](256) NULL,
	[WorkFormId] [int] NULL,
	[SalaryLevelFrom] [decimal](18, 4) NULL,
	[SalaryLevelTo] [decimal](18, 4) NULL,
	[AgeFrom] [int] NULL,
	[AgeTo] [int] NULL,
	[Sex] [int] NULL,
	[EducationId] [int] NULL,
	[ExperienceId] [int] NULL,
	[Height] [decimal](18, 4) NULL,
	[Weight] [decimal](18, 4) NULL,
	[Description] [nvarchar](max) NULL,
	[Requirement] [nvarchar](max) NULL,
	[Reason] [nvarchar](256) NULL,
	[SignerApprove] [nvarchar](64) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CreatedBy] [nvarchar](64) NOT NULL,
	[EditedDate] [date] NOT NULL,
	[EditedBy] [nvarchar](64) NOT NULL,
	CONSTRAINT [PK_rec_RequiredRecruitment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[rec_RequiredRecruitment] ADD  CONSTRAINT [DF_rec_RequiredRecruitment_Status]  DEFAULT ((0)) FOR [Status]
ALTER TABLE [dbo].[rec_RequiredRecruitment] ADD  CONSTRAINT [DF_rec_RequiredRecruitment_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
ALTER TABLE [dbo].[rec_RequiredRecruitment] ADD  CONSTRAINT [DF_rec_RequiredRecruitment_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]

-- create [rec_Candidate]
CREATE TABLE [dbo].[rec_Candidate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordId] [int] NOT NULL,
	[RequiredRecruitmentId] [int] NOT NULL,
	[DesiredSalary] [decimal](18, 4) NULL,
	[ApplyDate] [date] NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CreatedBy] [nvarchar](64) NOT NULL,
	[EditedDate] [date] NOT NULL,
	[EditedBy] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_rec_Candidate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[rec_Candidate] ADD  CONSTRAINT [DF_rec_Candidate_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
ALTER TABLE [dbo].[rec_Candidate] ADD  CONSTRAINT [DF_rec_Candidate_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]

-- create [rec_Interview]
CREATE TABLE [dbo].[rec_Interview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequiredRecruitmentId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[InterviewDate] [date] NOT NULL,
	[FromTime] [datetime] NOT NULL,
	[ToTime] [datetime] NOT NULL,
	[Interviewer] [nvarchar](64) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CreatedBy] [nvarchar](64) NOT NULL,
	[EditedDate] [date] NOT NULL,
	[EditedBy] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_rec_Interview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[rec_Interview] ADD  CONSTRAINT [DF_rec_Interview_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
ALTER TABLE [dbo].[rec_Interview] ADD  CONSTRAINT [DF_rec_Interview_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]

-- create [rec_CandidateInterview]
CREATE TABLE [dbo].[rec_CandidateInterview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InterviewId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[TimeInterview] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CreatedBy] [nvarchar](64) NOT NULL,
	[EditedDate] [date] NOT NULL,
	[EditedBy] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_rec_CandidateInterview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[rec_CandidateInterview] ADD  CONSTRAINT [DF_rec_CandidateInterview_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]


-- ho so
IF NOT EXISTS(
	SELECT * FROM sys.columns 
	WHERE NAME IN (N'Type')
		AND Object_ID = Object_ID(N'hr_Record'))
BEGIN
	ALTER TABLE hr_Record
	ADD
	[Type] int default 0 NOT NULL
END

UPDATE hr_Record set [Type] = 0

-- don vi tuyen dung
CREATE TABLE [dbo].[cat_RecruitmentDepartment](
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
	[EditedDate] [datetime] NULL,
 CONSTRAINT [PK_cat_RecruitmentDepartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_Code]  DEFAULT ('') FOR [Code]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_Name]  DEFAULT ('') FOR [Name]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_Description]  DEFAULT ('') FOR [Description]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_Group]  DEFAULT ('') FOR [Group]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_Order]  DEFAULT ((0)) FOR [Order]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_Status]  DEFAULT ((1)) FOR [Status]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_CreatedBy]  DEFAULT (N'system') FOR [CreatedBy]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_EditedBy]  DEFAULT (N'system') FOR [EditedBy]
ALTER TABLE [dbo].[cat_RecruitmentDepartment] ADD  CONSTRAINT [DF_cat_RecruitmentDepartment_EditedDate]  DEFAULT (getdate()) FOR [EditedDate]


IF NOT EXISTS(
	SELECT * FROM sys.columns 
	WHERE NAME IN (N'StartDate',N'EndDate')
		AND Object_ID = Object_ID(N'hr_TimeSheetGroupWorkShift'))
BEGIN
	ALTER TABLE hr_TimeSheetGroupWorkShift
	ADD
	[StartDate] datetime   NULL,
	[EndDate] datetime   NULL
END

