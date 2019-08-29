IF NOT EXISTS(SELECT * FROM  sys.columns WHERE NAME IN (N'Code') AND object_id = object_id(N'rec_Candidate'))
BEGIN
	ALTER TABLE rec_Candidate
	ADD
		[Code] nvarchar(64) NOT NULL
END

IF EXISTS(SELECT * FROM  sys.columns WHERE NAME IN (N'ApplyDate') AND object_id = object_id(N'rec_Candidate'))
BEGIN
	ALTER TABLE rec_Candidate
	ALTER COLUMN ApplyDate date NULL
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE NAME IN (N'FormulaRange') AND object_id = object_id(N'kpi_Criterion'))
BEGIN
	ALTER TABLE kpi_Criterion
	ADD [FormulaRange] nvarchar(1024)
END

--------------------------------
-- add column
IF NOT EXISTS(SELECT * FROM  sys.columns WHERE NAME IN (N'WorkFullDay') AND object_id = object_id(N'hr_SalaryBoardInfo'))
BEGIN
	ALTER TABLE hr_SalaryBoardInfo
	ADD  [WorkFullDay] float NULL
END
