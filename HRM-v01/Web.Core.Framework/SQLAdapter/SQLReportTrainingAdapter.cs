namespace Web.Core.Framework.SQLAdapter
{
    public class SQLReportTrainingAdapter
    {
        private const string NationName = "%Việt Nam%";
        /// <summary>
        /// Báo cáo danh sách cán bộ được đào tạo tại đơn vị
        /// </summary>
        /// <param name = "department" ></ param >
        /// < param name="fromDate"></param>
        /// <param name = "toDate" ></ param >
        /// < param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeTraining(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += " SELECT 	 " +
                   "'' AS 'STT', " +
                   "	rc.DepartmentId, " +
                   "    ci.Name AS IndustryName," +
                   "	dp.Name AS 'DepartmentName', " +
                   "	rc.EmployeeCode, " +
                   "	rc.FullName, " +
                   "	cp.Name AS 'PositionName', " +
                   "	th.DecisionNumber, " +
                   "	th.DecisionDate, " +
                   "	th.TrainingName, " +
                   "	th.StartDate, " +
                   "	th.EndDate, " +
                   "	ts.Name AS 'TrainingSystemName', " +
                   "	cu.Name AS 'UniversityName', " +
                   "	cn.Name AS 'NationName', " +
                   "	th.SponsorDepartment, " +
                   "	th.SourceDepartment, " +
                   "	th.DecisionMaker, " +
                   "	th.MakerPosition " +
                   "	INTO #tmpB " +
                   "   FROM hr_TrainingHistory th" +
                   "   LEFT JOIN hr_Record rc " +
                   "       ON th.RecordId = rc.Id" +
                   "	LEFT JOIN cat_TrainingSystem ts " +
                   "		ON th.TrainingSystemId = ts.Id " +
                   "	LEFT JOIN cat_Department dp " + // filter department
                   "		ON rc.DepartmentId = dp.Id" +
                   "	LEFT JOIN cat_Position cp	 " + // filter position
                   "		ON rc.PositionId = cp.Id" +
                   "	LEFT JOIN  hr_EducationHistory eh	 " + // filter education history
                   "		ON eh.RecordId = rc.Id " +
                   "	LEFT JOIN cat_University cu " +
                   "		ON eh.UniversityId = cu.Id" +
                   "	LEFT JOIN cat_Nation cn	 " +
                   "		ON th.NationId = cn.Id " +
                   "    LEFT JOIN cat_Industry ci" +
                   "        ON rc.IndustryId = ci.Id" + // filter industry
                   "	WHERE 1=1 ";
            if (!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND th.StartDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND th.StartDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "	SELECT * FROM #tmpB ";

            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách cán bộ công tác nước ngoài
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeOnsite(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += " SELECT " +
                   " rc.DepartmentId, " +
                   " dp.Name AS 'DepartmentName', " +
                   " '' AS 'STT', " +
                   " rc.EmployeeCode, " +
                   " rc.FullName, " +
                   " cp.Name AS 'PositionName', " +
                   " ga.DecisionNumber, " +
                   " ga.DecisionDate, " +
                   " ga.Reason, " +
                   " ga.StartDate , " +
                   " ga.EndDate, " +
                   " cn.Name AS 'NationName', " +
                   " ga.SponsorDepartment, " +
                   " ga.SourceDepartment, " +
                   " ga.DecisionMaker, " +
                   " ga.MakerPosition " +
                   " INTO #tmpB " +
                   "   FROM hr_GoAboard ga" +
                   "   LEFT JOIN hr_Record rc " +
                   "       ON ga.RecordId = rc.Id" + 
                   "    LEFT JOIN cat_Department dp " + // filter department
                   "       ON rc.DepartmentId = dp.Id " +
                   "   LEFT JOIN cat_Position cp " + // filter position
                   "       ON rc.PositionId = cp.Id " + 
                   "   LEFT JOIN cat_Nation cn " + // filter nation
                   "       ON ga.NationId = cn.Id" +
                   "   WHERE ga.NationId != 0 AND ga.NationId not in (SELECT TOP 1 Id FROM cat_Nation WHERE Name Like N'{0}')".FormatWith(NationName);
            if (!string.IsNullOrEmpty(department))
                sql += "  AND rc.DepartmentId IN ({0})".FormatWith(department);
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND ga.StartDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND ga.StartDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "	SELECT * FROM #tmpB ";

            return sql;
        }

    }    
}
