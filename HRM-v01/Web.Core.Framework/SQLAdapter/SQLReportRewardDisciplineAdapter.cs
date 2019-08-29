namespace Web.Core.Framework.SQLAdapter
{
    public class SQLReportRewardDisciplineAdapter
    {
        private const string ConstBusinessType = "DanhHieuThiDua";
        
        /// <summary>
        /// Báo cáo danh sách được khen thưởng
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeRewarded(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += "	SELECT " +
                   "	'' as 'STT', " +
                   "	rc.DepartmentId, " +
                   "	rc.EmployeeCode, " +
                   "	rc.FullName, " +
                   "	dp.Name as 'DepartmentName', " +
                   "	cp.Name as 'PositionName', " +
                   "	hr.DecisionNumber, " +
                   "	hr.EffectiveDate, " +
                   "	cr.Name as 'FormRewardName', " +
                   "	hr.MoneyAmount, " +
                   "	hr.SourceDepartment, " +
                   "	hr.DecisionMaker, " +
                   "	hr.MakerPosition " +
                   "	INTO #tmpB " +
                   "	FROM hr_Reward hr	 " +
                   "	LEFT JOIN hr_Record rc " +
                   "		ON hr.RecordId = rc.Id " +
                   "	LEFT JOIN cat_Position cp " +
                   "		ON rc.PositionId = cp.Id " +
                   "	LEFT JOIN cat_Reward cr " +
                   "		ON hr.FormRewardId = cr.Id " +
                   "	LEFT JOIN cat_Department dp " +
                   "		ON rc.DepartmentId = dp.Id " +
                   "	LEFT JOIN cat_LevelRewardDiscipline lrd " +
                   "		ON hr.LevelRewardId = lrd.Id " +
                   "	WHERE rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hr.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hr.DecisionDate <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "	SELECT * FROM #tmpB ";

            return sql;
        }

        ///// <summary>
        ///// Báo cáo danh sách được nhận danh hiệu thi đua
        ///// </summary>
        ///// <param name="department"></param>
        ///// <param name="fromDate"></param>
        ///// <param name="toDate"></param>
        ///// <param name="condition"></param>
        ///// <returns></returns>
        public static string GetStore_EmployeeReceivedAward(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT	" +
                   " rc.EmployeeCode,	" +
                   " rc.FullName,	" +
                   " rc.DepartmentId,	" +
                   " rc.ParticipationDate," +
                   " cp.Name AS PositionName,	" +
                   " dp.Name AS DepartmentName,	" +
                   " bh.EmulationTitle,	" +
                   " bh.CurrentDepartment," +
                   " bh.CurrentPosition," +
                   " bh.DecisionNumber," +
                   " bh.Money," +
                   " bh.SourceDepartment,"+
                   " bh.DecisionMaker,"+
                   " bh.DecisionPosition,"+
                   " bh.DecisionDate	" +
                   " FROM hr_BusinessHistory bh	" +
                   " LEFT JOIN hr_Record rc  ON bh.RecordId = rc.Id	" +
                   " LEFT JOIN cat_Department dp ON  rc.DepartmentId = dp.Id	" + // filter department
                   " LEFT JOIN cat_Position cp ON  rc.PositionId  =  cp.Id	" + // filter position
                   " WHERE bh.BusinessType = N'{0}'	".FormatWith(ConstBusinessType);

            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND bh.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND bh.DecisionDate <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }

            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách bị kỷ luật
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeDisciplined(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
                   " SELECT rc.EmployeeCode," +
                   " rc.FullName," +
                   " rc.DepartmentId,"+
                   " rc.ParticipationDate, " +
                   " cp.Name AS PositionName," +
                   " dp.Name AS DepartmentName," +
                   " hd.Reason," +
                   " hd.DecisionDate," +
                   " hd.DecisionNumber, " +
                   " hd.ExpireDate, " +
                   " hd.SourceDepartment, " +
                   " hd.DecisionMaker, " +
                   " hd.MakerPosition, " +
                   " cd.Name AS FormDisciplineName " +
                   " INTO #tmpA" +
                   " FROM hr_Discipline hd" +
                   " LEFT JOIN hr_Record rc" +
                   " ON hd.RecordId = rc.Id" +
                   " LEFT JOIN cat_Position cp" + // filter position
                   " ON rc.PositionId = cp.Id" +
                   " LEFT JOIN cat_Department dp" + // filter department
                   " ON rc.DepartmentId = dp.Id" +
                   " LEFT JOIN cat_Discipline cd" + // filter discipline
                   " ON hd.FormDisciplineId = cd.Id" +
                   " WHERE  1 = 1";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hd.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hd.DecisionDate <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " SELECT* FROM #tmpA";
            return sql;
        }
    }
}
