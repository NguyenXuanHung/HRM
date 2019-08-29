namespace Web.Core.Framework.Adapter
{
    /// <summary>
    /// Summary description for SQLManagementTrainingAdapter
    /// </summary>
    public class SQLManagementTrainingAdapter
    {	    
        /// <summary>
        /// Danh sách cán bộ được đào tạo tại đơn vị
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_DanhSachCanBoDuocDaoTaoTaiDonVi(string maDonVi, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += " SELECT 	 " +
                    "'' AS 'STT', " +
                    "	h.DepartmentId AS 'DepartmentId', " +
                    "	dd.Name AS 'DepartmentName', " +
                    "	h.EmployeeCode AS 'EmployeeCode', " +
                    "	h.FullName AS 'FullName', " +
                    "	dc.Name AS 'PositionName', " +
                    "	dt.DecisionNumber AS 'DecisionNumber', " +
                    "	dt.DecisionDate AS 'DecisionDate', " +
                    "	dt.TrainingName AS 'TrainingName', " +
                    "	dt.StartDate AS 'StartDate', " +
                    "	dt.EndDate AS 'EndDate', " +
                    "	ht.Name AS 'TrainingSystemName', " +
                    "	dtd.Name AS 'UniversityName', " +
                    "	dq.Name AS 'NationName', " +
                    "	dt.SponsorDepartment AS 'SponsorDepartment', " +
                    "	dt.SourceDepartment AS 'SourceDepartment', " +
                    "	dt.DecisionMaker AS 'DecisionMaker', " +
                    "	dt.MakerPosition AS 'MakerPosition' " +
                    "	INTO #tmpB " +
                   "   FROM hr_TrainingHistory dt" +
                   "   LEFT JOIN hr_Record h " +
                   "       ON h.Id = dt.RecordId " +
                    "	LEFT JOIN cat_TrainingSystem ht " +
                    "		ON ht.Id = dt.TrainingSystemId " +
                    "	LEFT JOIN cat_Department dd " +
                    "		ON dd.Id = h.DepartmentId	 " +
                    "	LEFT JOIN cat_Position dc	 " +
                    "		ON dc.Id = h.PositionId " +
                    "	LEFT JOIN  hr_EducationHistory hbu	 " +
                    "		ON hbu.RecordId = h.Id " +
                    "	LEFT JOIN cat_University dtd " +
                    "		ON dtd.Id = hbu.UniversityId " +
                    "	LEFT JOIN cat_Nation dq	 " +
                    "		ON dq.Id = dt.NationId " +

                    "	WHERE 1=1 ";
                    if (!string.IsNullOrEmpty(maDonVi))
                        sql += " AND h.DepartmentId IN ({0}) ".FormatWith(maDonVi);
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
        /// <param name="departments"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_DanhSachCanBoCongTacNuocNgoai(string departments, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += " SELECT " +
                    " hs.DepartmentId AS 'DepartmentId', " +
                    " dv.Name AS 'DepartmentName', " +
                    " '' AS 'STT', " +
                    " hs.EmployeeCode AS 'EmployeeCode', " +
                    " hs.FullName AS 'FullName', " +
                    " cv.Name AS 'PositionName', " +
                    " dt.DecisionNumber AS 'DecisionNumber', " +
                    " dt.DecisionDate AS 'DecisionDate', " +
                    " dt.Reason AS 'Reason', " +
                    " dt.StartDate AS 'StartDate', " +
                    " dt.EndDate AS 'EndDate', " +
                    " qg.Name AS 'NationName', " +
                    " dt.SponsorDepartment AS 'SponsorDepartment', " +
                    " dt.SourceDepartment AS 'SourceDepartment', " +
                    " dt.DecisionMaker AS 'DecisionMaker', " +
                    " dt.MakerPosition AS 'MakerPosition' " +
                    " INTO #tmpB " +
                    "   FROM hr_GoAboard dt" +
                    "   LEFT JOIN hr_Record hs " +
                    "       ON hs.Id = dt.RecordId " +
                    "    LEFT JOIN cat_Department dv " +
                    "       ON hs.DepartmentId = dv.Id " +
                    "   LEFT JOIN cat_Position cv " +
                    "       ON hs.PositionId = cv.Id " +
                    "   LEFT JOIN cat_Nation qg " +
                    "       ON dt.NationId = qg.Id" +
                    "   WHERE dt.NationId != 0 AND dt.NationId != 172 ";
                    if (!string.IsNullOrEmpty(departments))
                        sql += "  AND hs.DepartmentId IN ({0})".FormatWith(departments);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "	SELECT * FROM #tmpB ";

            return sql;
        }
    }
}