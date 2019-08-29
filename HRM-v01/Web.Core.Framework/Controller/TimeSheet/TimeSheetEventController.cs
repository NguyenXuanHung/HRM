using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model.TimeSheet;
using Web.Core.Framework.Utils;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class TimeSheetEventController
    {
        #region Variables

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["StandardConfig"].ConnectionString;
        private static readonly string _selectColumns = " tse.[Id]," +
                                                        " ISNULL(tse.[RecordId], 0) AS RecordId," +
                                                        " ISNULL(tse.[WorkShiftId], 0) AS WorkShiftId," +
                                                        " ISNULL(tse.[GroupSymbolId], 0) AS GroupSymbolId," +
                                                        " ISNULL(tse.[SymbolId], 0) AS SymbolId," +
                                                        " ISNULL(tse.[WorkConvert], 0) AS WorkConvert," +
                                                        " ISNULL(tse.[TimeConvert], 0) AS TimeConvert," +
                                                        " ISNULL(tse.[Type], 0) AS Type," +
                                                        " ISNULL(tse.[Description], '') AS Description," +
                                                        " ISNULL(tse.[Status], 0) AS Status," +
                                                        " ISNULL(tse.[IsDeleted], 0) AS IsDeleted," +
                                                        " tse.[CreatedDate] AS CreatedDate," +
                                                        " ISNULL(tse.[CreatedBy], '') AS CreatedBy," +
                                                        " tse.[EditedDate] AS EditedDate," +
                                                        " ISNULL(tse.[EditedBy], '') AS EditedBy," +
                                                          " ISNULL(rc.[EmployeeCode], '') AS EmployeeCode," +
                                                          " ISNULL(rc.[FullName], '') AS FullName," +
                                                          " ISNULL(dp.[Name], '') AS DepartmentName," +
                                                          " ISNULL(tsgws.[Name], '') AS GroupWorkShiftName," +
                                                          " ISNULL(tsws.[Name], '') AS WorkShiftName," +
                                                          " tsws.[StartDate] AS StartDate," +
                                                          " tsws.[EndDate] AS EndDate," +
                                                          " ISNULL(tss.[Name], '') AS SymbolName," +
                                                          " ISNULL(tss.[Code], '') AS SymbolCode," +
                                                          " ISNULL(tss.[Color], '') AS SymbolColor," +
                                                          " ISNULL(tsgs.[Name], '') AS GroupSymbolName," +
                                                          " ISNULL(tsgs.[Group], '') AS GroupSymbolGroup";
        private static readonly string _tables = " hr_TimeSheetEvent tse" +
                                                         " LEFT JOIN hr_Record rc ON tse.[RecordId] = rc.[Id]" +
                                                         " LEFT JOIN hr_TimeSheetSymbol tss ON tss.[Id] = tse.[SymbolId]" +
                                                         " LEFT JOIN hr_TimeSheetGroupSymbol tsgs ON tsgs.[Id] = tse.[GroupSymbolId]" +
                                                         " LEFT JOIN hr_TimeSheetWorkShift tsws ON tsws.[Id] = tse.[WorkShiftId]" +
                                                         " LEFT JOIN hr_TimeSheetGroupWorkShift tsgws ON tsgws.[Id] = tsws.[GroupWorkShiftId]" +
                                                         " LEFT JOIN cat_Department dp ON dp.[Id] = rc.[DepartmentId]";

        #endregion

        /// <summary>
        /// Get time sheet event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetEventModel GetById(int id)
        {
            var timeSheetEvent = hr_TimeSheetEventServices.GetById(id);
            return new TimeSheetEventModel(timeSheetEvent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="eventIds"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="workShiftId"></param>
        /// <param name="symbolId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="eventStatus"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetEventModel> GetAll(string keyword, string recordIds, List<int> eventIds, int? groupWorkShiftId, int? workShiftId, int? symbolId, bool? isDeleted, int? type, DateTime? startDate, DateTime? endDate, EventStatus? eventStatus,
            string order, int? start, int? limit)
        {
            var result = Search(keyword, recordIds, eventIds, groupWorkShiftId, workShiftId, symbolId, isDeleted, type, startDate,
                endDate, eventStatus, order, start, limit);
            return result.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="eventIds"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="workShiftId"></param>
        /// <param name="symbolId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="eventStatus"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetEventModel> GetPaging(string keyword, string recordIds, List<int> eventIds, int? groupWorkShiftId, int? workShiftId, int? symbolId, bool? isDeleted, int? type, DateTime? startDate, DateTime? endDate, EventStatus? eventStatus,
            string order, int start, int limit)
        {
            return Search(keyword, recordIds, eventIds, groupWorkShiftId, workShiftId, symbolId, isDeleted, type, startDate,
                endDate, eventStatus, order, start, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetEventSummaryModel> GetReportDetail(string keyword, string recordIds, DateTime? startDate, DateTime? endDate, int? start, int? limit)
        {
            // init object
            var startPage = 0;
            int limitPage;
            var resultModels = new List<TimeSheetEventSummaryModel>();
            var listRecordIds = new List<string>();

            // string to list record id
            if (!string.IsNullOrEmpty(recordIds))
            {
                listRecordIds = recordIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            // caculate start limit page
            if (start != null) startPage = (int)start;
            if (limit != null && startPage + limit < listRecordIds.Count)
                limitPage = (int)limit;
            else
                limitPage = listRecordIds.Count - startPage;

            // paging by record id
            recordIds = string.Join(",", listRecordIds.GetRange(startPage, limitPage));

            //get all events
            var timeSheetEvents = GetAll(keyword, recordIds, null, null, null, null, false, null, startDate, endDate, null,
                null, null, null);

            foreach (var id in listRecordIds.GetRange(startPage, limitPage))
            {
                var timeSheetEvent = new TimeSheetEventModel();
                // get event by record id
                var listEventByRecord = timeSheetEvents.Where(ts => ts.RecordId == Convert.ToInt32(id)).ToList();
                if (listEventByRecord.Count > 0)
                {
                    timeSheetEvent = listEventByRecord.First();
                }
                else
                {
                    if (!string.IsNullOrEmpty(keyword)) continue;
                    // get record that has no event
                    var record = RecordController.GetById(Convert.ToInt32(id));
                    timeSheetEvent.RecordId = record.Id;
                    timeSheetEvent.FullName = record.FullName;
                    timeSheetEvent.EmployeeCode = record.EmployeeCode;
                    timeSheetEvent.DepartmentName = record.DepartmentName;
                }

                var timeSheetEventSummary = new TimeSheetEventSummaryModel(listEventByRecord, startDate, endDate)
                {
                    RecordId = timeSheetEvent.RecordId,
                    EmployeeCode = timeSheetEvent.EmployeeCode,
                    FullName = timeSheetEvent.FullName,
                    DepartmentName = timeSheetEvent.DepartmentName
                };
                // add summary to list summary
                resultModels.Add(timeSheetEventSummary);
            }

            return new PageResult<TimeSheetEventSummaryModel>(listRecordIds.Count, resultModels);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeReports"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<TimeSheetEventReportModel> GetTimeSheetReport(List<TimeSheetEmployeeReportModel> employeeReports, DateTime startDate, DateTime endDate)
        {
            var listTimeSheetModel = new List<TimeSheetEventReportModel>();
            var recordIds = string.Join(",", employeeReports.Select(rc => rc.RecordId).ToList());
            //get all events
            var timeSheetEvents = GetAll(null, recordIds, null, null, null, null, false, null, startDate, endDate, null, null, null, null);
            foreach (var item in employeeReports)
            {
                double? totalActualDay = 0.0;
                double? totalWorkPaidLeave = 0.0;
                double? totalOverTime = 0.0;
                double? totalOverTimeDay = 0.0;
                double? totalOverTimeNight = 0.0;
                double? totalOverTimeHoliday = 0.0;
                double? totalOverTimeWeekend = 0.0;
                double? totalGoWorkC = 0.0;
                double? totalHolidayL = 0.0;
                double? totalLateM = 0.0;
                double? totalUnLeaveK = 0.0;
                double? totalUnpaidLeaveP = 0.0;

                // get event by record id
                var listEventByRecord = timeSheetEvents.Where(ts => ts.RecordId == Convert.ToInt32(item.RecordId)).ToList();

                var eventModel = new TimeSheetEventReportModel()
                {
                    RecordId = Convert.ToInt32(item.RecordId),
                    FullName = item.FullName,
                    EmployeeCode = item.EmployeeCode
                };

                for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
                {
                    var symbol = string.Empty;
                    var symbolDisplay = string.Empty;
                    double? totalItem = 0.0;
                    double? timeConvert = 0;

                    // init field
                    var fieldInfo = eventModel.GetType().GetProperty("Day{0}".FormatWith(day.Day)); //get type time sheet
                    var itemEventDayModel = new TimeSheetItemModel(day.Day);
                    if (listEventByRecord.Count > 0)
                    {
                        var timeSheetEvent = listEventByRecord.Where(ev => ev.StartDate.Date == day).ToList();
                        foreach (var itemEvent in timeSheetEvent)
                        {
                            totalItem += itemEvent.WorkConvert;
                            symbol += "," + itemEvent.SymbolCode;
                            symbolDisplay += ", " + itemEvent.SymbolName;
                            switch (itemEvent.GroupSymbolGroup)
                            {
                                //Tong so cong nghi phep
                                case Constant.TimesheetLeave:
                                    totalWorkPaidLeave += itemEvent.WorkConvert;
                                    break;
                                //Tong so cong khong phep
                                case Constant.TimesheetUnLeave:
                                    totalUnLeaveK += itemEvent.WorkConvert;
                                    break;
                                //Tong so cong cong tac
                                case Constant.TimesheetGoWork:
                                    totalGoWorkC += itemEvent.WorkConvert;
                                    break;
                                //Tong so cong muon
                                case Constant.TimesheetLate:
                                    totalLateM++;
                                    break;
                                //Tong so cong nghi le
                                case Constant.TimesheetHoliday:
                                    totalHolidayL += itemEvent.WorkConvert;
                                    break;
                                //Tong so cong khong luong
                                case Constant.TimesheetNotPaySalary:
                                    totalUnpaidLeaveP += itemEvent.WorkConvert;
                                    break;
                                //Tong thoi gian them gio
                                case Constant.TimesheetOverTime:
                                    totalOverTime += itemEvent.TimeConvert;
                                    timeConvert += itemEvent.TimeConvert;
                                    break;
                                //Tong thoi gian tang ca ngay
                                case Constant.TimesheetOverTimeDay:
                                    totalOverTimeDay += itemEvent.TimeConvert;
                                    timeConvert += itemEvent.TimeConvert;
                                    break;
                                //Tong thoi gian tang ca dem
                                case Constant.TimesheetOverTimeNight:
                                    totalOverTimeNight += itemEvent.TimeConvert;
                                    timeConvert += itemEvent.TimeConvert;
                                    break;
                                //Tong thoi gian tang ca ngay le
                                case Constant.TimesheetOverTimeHoliday:
                                    totalOverTimeHoliday += itemEvent.TimeConvert;
                                    timeConvert += itemEvent.TimeConvert;
                                    break;
                                //Tong thoi gian tang ca ngay nghi
                                case Constant.TimesheetOverTimeWeekend:
                                    totalOverTimeWeekend += itemEvent.TimeConvert;
                                    timeConvert += itemEvent.TimeConvert;
                                    break;
                            }
                        }
                    }
                    itemEventDayModel.WorkConvert = Math.Round(totalItem.Value, 2);
                    itemEventDayModel.TimeConvert = Math.Round(timeConvert.Value, 2);
                    totalActualDay += totalItem;
                    itemEventDayModel.Symbol = symbol.TrimStart(',').TrimEnd(',');
                    itemEventDayModel.SymbolDisplay = symbolDisplay.TrimStart(',').TrimEnd(',').Replace(",", "");
                    itemEventDayModel.DetailDisplay = symbol.TrimStart(',').TrimEnd(',').Replace(",", " | ");
                    if (fieldInfo != null) fieldInfo.SetValue(eventModel, itemEventDayModel);
                }

                //Tinh tong cong
                eventModel.TotalActual = Math.Round(totalActualDay.Value, 2);
                eventModel.TotalPaidLeaveT = Math.Round(totalWorkPaidLeave.Value, 2);
                eventModel.TotalLateM = totalLateM;
                eventModel.TotalGoWorkC = totalGoWorkC;
                eventModel.TotalHolidayL = totalHolidayL;
                eventModel.TotalUnleaveK = Math.Abs(totalUnLeaveK.Value);
                eventModel.TotalUnpaidLeaveP = Math.Abs(totalUnpaidLeaveP.Value);

                //Tinh tong thoi gian quy doi ra gio
                eventModel.TotalOverTime = ConvertUtils.ConvertToHour(totalOverTime.Value);
                eventModel.TotalOverTimeDay = ConvertUtils.ConvertToHour(totalOverTimeDay.Value);
                eventModel.TotalOverTimeNight = ConvertUtils.ConvertToHour(totalOverTimeNight.Value);
                eventModel.TotalOverTimeHoliday = ConvertUtils.ConvertToHour(totalOverTimeHoliday.Value);
                eventModel.TotalOverTimeWeekend = ConvertUtils.ConvertToHour(totalOverTimeWeekend.Value);

                //add to list
                listTimeSheetModel.Add(eventModel);
            }

            return listTimeSheetModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="eventIds"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="workShiftId"></param>
        /// <param name="symbolId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="eventStatus"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static PageResult<TimeSheetEventModel> Search(string keyword, string recordIds, List<int> eventIds, int? groupWorkShiftId, int? workShiftId, int? symbolId, bool? isDeleted, int? type, DateTime? startDate, DateTime? endDate, EventStatus? eventStatus,
            string order, int? start, int? limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition +=
                    " AND tse.[RecordId] IN (SELECT Id  FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.EscapeQuote());

            if (!string.IsNullOrEmpty(recordIds))
                condition += " AND tse.[RecordId] IN ({0})".FormatWith(recordIds);

            if (eventIds != null)
            {
                var eventIdsString = "";
                foreach (var eventId in eventIds)
                {
                    eventIdsString += eventId + ",";
                }
                condition += " AND tse.[Id] IN ({0}) ".FormatWith(eventIdsString);
            }

            if (startDate != null)
                condition += " AND tse.[WorkShiftId] IN ( SELECT ws.Id FROM hr_TimeSheetWorkShift ws WHERE ws.StartDate IS NOT NULL AND ws.StartDate >= '{0}' AND tse.WorkShiftId = ws.Id )"
                    .FormatWith(startDate.Value.ToString("yyyy-MM-dd"));

            if (endDate != null)
                condition += " AND tse.[WorkShiftId] IN ( SELECT ws.Id FROM hr_TimeSheetWorkShift ws WHERE ws.StartDate IS NOT NULL AND ws.StartDate <= '{0}' AND tse.WorkShiftId = ws.Id)"
                    .FormatWith(endDate.Value.AddDays(1).ToString("yyyy-MM-dd"));

            if (groupWorkShiftId != null)
                condition +=
                    " AND (SELECT tsg.[Id] FROM hr_TimeSheetWorkShift ts LEFT JOIN hr_TimeSheetGroupWorkShift tsg ON ts.GroupWorkShiftId = tsg.Id WHERE tse.WorkShiftId = ts.Id) = {0}"
                        .FormatWith(groupWorkShiftId);

            if (symbolId != null)
                condition += " AND tse.[SymbolId] = {0}".FormatWith(symbolId);

            if (workShiftId != null)
                condition += " AND tse.[WorkShiftId] = {0}".FormatWith(workShiftId);

            if (isDeleted != null)
                condition += " AND tse.[IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");

            if (type != null)
                condition += " AND tse.[Type] = {0}".FormatWith(type);

            if (eventStatus != null)
                condition += " AND tse.[Status] = {0} ".FormatWith((int)eventStatus);

            // count total
            var countQuery = @"SELECT COUNT(*) AS TOTAL FROM [hr_TimeSheetEvent] tse WHERE {0}".FormatWith(condition);
            var objCount = SQLHelper.ExecuteScalar(countQuery);
            var total = (int?)objCount ?? 0;

            // init return value
            var models = new List<TimeSheetEventModel>();

            // init reader
            SqlDataReader dr = null;

            // init connection string
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init start, limit
                if (start == null || start < 0) start = 0;
                if (limit == null || limit < 0) limit = total;

                // init order
                if (string.IsNullOrEmpty(order)) order = "rc.[FullName]";

                // int get paging sql
                var sql = "SELECT * FROM (SELECT {0}, ROW_NUMBER() OVER (ORDER BY {1}) as row_number FROM {2} WHERE {3}) t0 WHERE t0.row_number>{4} AND  t0.row_number<={5}"
                        .FormatWith(_selectColumns, order.EscapeQuote(), _tables, condition, start, start + limit);

                // init command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };

                // open connection
                conn.Open();

                // execute reader
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    // fill object
                    var model = FillObject(dr);

                    // add to return list
                    models.Add(model);
                }

                // return
                return new PageResult<TimeSheetEventModel>(total, models);
            }
            catch
            {
                // return blank result
                return new PageResult<TimeSheetEventModel>(0, new List<TimeSheetEventModel>());
            }
            finally
            {
                // close reader
                dr.Close();

                // close connection
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetEventModel Create(TimeSheetEventModel model)
        {
            var entity = new hr_TimeSheetEvent();

            // fill
            model.FillEntity(ref entity);

            // create entity
            return new TimeSheetEventModel(hr_TimeSheetEventServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetEventModel Update(TimeSheetEventModel model)
        {
            var entity = new hr_TimeSheetEvent();
            model.FillEntity(ref entity);
            entity.EditedDate = DateTime.Now;
            return new TimeSheetEventModel(hr_TimeSheetEventServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static TimeSheetEventModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            model.IsDeleted = true;
            return Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workShiftIds"></param>
        /// <param name="recordIds"></param>
        /// <param name="workShiftId"></param>
        /// <param name="recordId"></param>
        /// <param name="type"></param>
        public static void DeleteByCondition(List<int> workShiftIds, List<int> recordIds, int? workShiftId, int? recordId, TimeSheetAdjustmentType? type)
        {
            var condition = Constant.ConditionDefault;
            if (workShiftIds != null)
            {
                var workShiftIdsString = "";
                foreach (var eventId in workShiftIds)
                {
                    workShiftIdsString += eventId + ",";
                }
                condition += " AND [WorkShiftId] IN ({0}) ".FormatWith(workShiftIdsString.TrimEnd(','));
            }

            if (recordIds != null)
            {
                var recordIdsString = "";
                foreach (var eventId in recordIds)
                {
                    recordIdsString += eventId + ",";
                }
                condition += " AND [RecordId] IN ({0}) ".FormatWith(recordIdsString.TrimEnd(','));
            }

            if (workShiftId != null)
            {
                condition += " AND [WorkShiftId] = {0}".FormatWith(workShiftId);
            }

            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            if (type != null)
            {
                condition += " AND [Type] = {0}".FormatWith((int)type);
            }

            hr_TimeSheetEventServices.Delete(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static TimeSheetEventModel FillObject(IDataRecord dr)
        {
            try
            {
                var model = new TimeSheetEventModel
                {
                    Id = (int)dr["Id"],
                    RecordId = (int)dr["RecordId"],
                    WorkShiftId = (int)dr["WorkShiftId"],
                    GroupSymbolId = (int)dr["GroupSymbolId"],
                    SymbolId = (int)dr["SymbolId"],
                    WorkConvert = (double)dr["WorkConvert"],
                    TimeConvert = (double)dr["TimeConvert"],
                    Type = (TimeSheetAdjustmentType)(int)dr["Type"],
                    Description = (string)dr["Description"],
                    Status = (EventStatus)(int)dr["Status"],
                    IsDeleted = (bool)dr["IsDeleted"],
                    // custom props
                    EmployeeCode = (string)dr["EmployeeCode"],
                    FullName = (string)dr["FullName"],
                    DepartmentName = (string)dr["DepartmentName"],
                    GroupWorkShiftName = (string)dr["GroupWorkShiftName"],
                    WorkShiftName = (string)dr["WorkShiftName"],
                    SymbolName = (string)dr["SymbolName"],
                    SymbolCode = (string)dr["SymbolCode"],
                    SymbolColor = (string)dr["SymbolColor"],
                    GroupSymbolName = (string)dr["GroupSymbolName"],
                    GroupSymbolGroup = (string)dr["GroupSymbolGroup"]
                };

                if (!(dr["CreatedDate"] is DBNull)) model.CreatedDate = (DateTime)dr["CreatedDate"];
                if (!(dr["EditedDate"] is DBNull)) model.EditedDate = (DateTime)dr["EditedDate"];
                if (!(dr["StartDate"] is DBNull)) model.StartDate = (DateTime)dr["StartDate"];
                if (!(dr["EndDate"] is DBNull)) model.EndDate = (DateTime)dr["EndDate"];

                return model;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error on fill object: " + ex.Message);
                return new TimeSheetEventModel();
            }
        }
    }
}
