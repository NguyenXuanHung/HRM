namespace Web.Core.Framework.Adapter
{
    /// <summary>
    /// Summary description for SQLManagementRewardDisciplineAdapter
    /// </summary>
    public class SQLManagementRewardDisciplineAdapter
    {        
        /// <summary>
        /// Danh sách cán bộ được khen thưởng
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_DanhSachCanBoDuocKhenThuong(string maDonVi, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += "	SELECT " +
                   "	'' as 'STT', " +
                   "	h.DepartmentId as 'DepartmentId', " +
                   "	dd.Name as 'GROUP', " +
                   "	h.EmployeeCode as 'EmployeeCode', " +
                   "	h.FullName as 'FullName', " +
                   "	dd.Name as 'DepartmentName', " +
                   "	dc.Name as 'PositionName', " +
                   "	hk.DecisionNumber as 'DecisionNumber', " +
                   "	hk.EffectiveDate as 'EffectiveDate', " +
                   "	kt.Name as 'FormRewardName', " +
                   "	hk.MoneyAmount as 'MoneyAmount', " +
                   "	hk.SourceDepartment as 'SourceDepartment', " +
                   "	hk.DecisionMaker as 'DecisionMaker', " +
                   "	hk.MakerPosition as 'MakerPosition' " +
                   "	INTO #tmpB " +
                   "	FROM hr_Reward hk	 " +
                   "	LEFT JOIN hr_Record h " +
                   "		ON hk.RecordId = H.Id " +
                   "	LEFT JOIN cat_Position dc " +
                   "		ON dc.Id = h.PositionId " +
                   "	LEFT JOIN cat_Reward kt " +
                   "		ON kt.Id = hk.FormRewardId " +
                   "	LEFT JOIN cat_Department dd " +
                   "		ON dd.Id = h.DepartmentId " +
                   "	LEFT JOIN cat_LevelRewardDiscipline ckt " +
                   "		ON ckt.ID = hk.LevelRewardId " +
                   "	WHERE h.DepartmentId IN ({0}) ".FormatWith(maDonVi);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "	SELECT * FROM #tmpB ";

            return sql;
        }

        /// <summary>
        /// Danh sách cán bộ bị kỷ luật
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_DanhSachCanBoBiKyLuat(string maDonVi, string condition)
        {
            var sql = string.Empty;

            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";

            sql += "SELECT " +
                   " hs.DepartmentId AS 'DepartmentId', " +
                   " '' AS 'STT', " +
                   " dv.Name AS 'GROUP',  " +
                   " hs.EmployeeCode AS 'EmployeeCode', " +
                   " hs.FullName AS 'FullName', " +
                   " dv.Name AS 'DepartmentName', " +
                   " cv.Name AS 'PositionName', " +
                   " kl.DecisionNumber AS 'DecisionNumber', " +
                   " kl.DecisionDate AS 'DecisionDate', " +
                   " htkl.Name AS 'FormDisciplineName', " +
                   " kl.Reason AS 'Reason', " +
                   " kl.ExpireDate AS 'ExpireDate', " +
                   " kl.SourceDepartment AS 'SourceDepartment', " +
                   " kl.DecisionMaker AS 'DecisionMaker', " +
                   " kl.MakerPosition AS 'MakerPosition' " +
                   "	INTO #tmpB " +
                   " FROM hr_Record hs " +
                   " INNER JOIN hr_Discipline kl " +
                   "     ON hs.Id = kl.RecordId " +
                   " LEFT JOIN cat_Department dv " +
                   "     ON hs.DepartmentId = dv.Id " +
                   " LEFT JOIN cat_Position cv " +
                   "   ON hs.PositionId = cv.Id " +
                   " LEFT JOIN cat_Discipline htkl " +
                   "   ON htkl.Id = kl.FormDisciplineId " +
                   " WHERE hs.DepartmentId IN ({0}) ".FormatWith(maDonVi);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "SELECT * FROM #tmpB";

            return sql;
        }

        public static string GetDanhSachCanBoDuocTangDanhHieuThiDua(string maDonVi)
        {
            var sql = string.Empty;
            sql +=  "	SELECT " +
                    "	'' AS 'STT', " +
                    "	H.DepartmentId AS 'DepartmentId', " +
                    "	DV.Name AS 'GROUP', " +
                    "	H.EmployeeCode AS 'EmployeeCode', " +
                    "	H.FullName AS 'FullName', " +
                    "	DV.Name AS 'DepartmentName',	 " +
                    "	CV.Name AS 'PositionName',	 " +
                    "	KT.DecisionNumber AS 'DecisionNumber',	 " +
                    "	KT.EffectiveDate AS 'EffectiveDate',	 " +
                    "	HKT.Name AS 'FormRewardName', " +
                    "	KT.MoneyAmount AS 'MoneyAmount', " +
                    "	CKT.Name AS 'RewardAgency', " +
                    "	KT.DecisionMaker AS 'DecisionMaker',	 " +
                    "	'' AS 'ChucVuNguoiKy' " +
                    "	FROM hr_Reward KT	 " +
                    "	LEFT JOIN hr_Record H " +
                    "		ON KT.RecordId = H.Id	 " +
                    "	LEFT JOIN cat_Department DV 	 " +
                    "		ON DV.Id = H.DepartmentId	 " +
                    "	LEFT JOIN cat_Position CV	 " +
                    "		ON CV.Id = H.PositionId " +
                    "	LEFT JOIN cat_Reward HKT	 " +
                    "		ON HKT.Id = KT.FormRewardId " +
                    "	LEFT JOIN cat_LevelRewardDiscipline CKT " +
                    "		ON CKT.ID = KT.LevelRewardId " +
                    "	WHERE H.DepartmentId IN ({0}) ".FormatWith(maDonVi) +
                    "		AND KT.FormRewardId != 0 ";

            return sql;
        }
    }
}