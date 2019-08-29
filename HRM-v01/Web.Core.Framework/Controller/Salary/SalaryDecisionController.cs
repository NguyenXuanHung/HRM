using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Salary;
using Web.Core.Service.Salary;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for SalaryDecisionController
    /// </summary>
    public class SalaryDecisionController
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SalaryDecisionModel GetById(int id)
        {
            // get entity
            var entity = sal_SalaryDecisionServices.GetById(id);

            // return
            return entity != null ? new SalaryDecisionModel(entity) : null;
        }

        /// <summary>
        /// Get by decision number
        /// </summary>
        /// <param name="decisionNumber"></param>
        /// <returns></returns>
        public static SalaryDecisionModel GetByDecisionNumber(string decisionNumber)
        {
            // init condition
            var condition = @"[DecisionNumber]='{0}'".FormatWith(decisionNumber.EscapeQuote());

            // get entity
            var entity = sal_SalaryDecisionServices.GetByCondition(condition);

            // return
            return entity != null ? new SalaryDecisionModel(entity) : null;
        }

        /// <summary>
        /// Get current active decision
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static SalaryDecisionModel GetCurrent(int recordId)
        {
            // get entity
            var entity = sal_SalaryDecisionServices.GetCurrent(recordId);

            // return
            return entity != null ? new SalaryDecisionModel(entity) : null;
        }

        /// <summary>
        /// Get current active decision
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static SalaryDecisionModel GetPrevious(int recordId)
        {
            // init condition
            var condition = @"[EffectiveDate]<'{0}' AND [RecordId]='{1}' AND [Status]='{2}'".FormatWith(DateTime.Now.ToString("yyyy-MM-dd"), recordId, (int)SalaryDecisionStatus.Approved);

            // init order
            var order = "[DecisionDate] DESC";

            // get entities
            var entities = sal_SalaryDecisionServices.GetAll(condition, order, 2);

            // check result
            if (entities.Count == 2)
                return new SalaryDecisionModel(entities.Last());

            // return
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int CountNextRaise(DateTime fromDate, DateTime toDate)
        {
            // init condition
            var condition = "[NextRaiseDate] IS NOT NULL AND [NextRaiseDate]>='{0}' AND [NextRaiseDate]<'{1}' AND [Status]='{2}'".FormatWith(
                fromDate.ToString("yyyy-MM-dd"), toDate.AddDays(1).ToString("yyyy-MM-dd"), (int)SalaryDecisionStatus.Approved);

            // return 
            return sal_SalaryDecisionServices.Count(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static List<SalaryDecisionModel> GetNextRaise(DateTime fromDate, DateTime toDate)
        {
            // init condition
            var condition = "[NextRaiseDate] IS NOT NULL AND [NextRaiseDate]>='{0}' AND [NextRaiseDate]<'{1}' AND [Status]='{2}'".FormatWith(
                fromDate.ToString("yyyy-MM-dd"), toDate.AddDays(1).ToString("yyyy-MM-dd"), (int)SalaryDecisionStatus.Approved);

            // return 
            return sal_SalaryDecisionServices.GetAll(condition, "[NextRaiseDate]").Select(sd => new SalaryDecisionModel(sd)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int CountOverGrade(DateTime fromDate, DateTime toDate)
        {
            // init condition
            var condition = "[NextRaiseDate]>='{0}' AND [NextRaiseDate]<'{1}' AND [Grade]=(SELECT [GradeMax] FROM cat_GroupQuantum WHERE cat_GroupQuantum.Id=[GroupQuantumId]) AND [Status]='{2}'".FormatWith(
                fromDate.ToString("yyyy-MM-dd"), toDate.AddDays(1).ToString("yyyy-MM-dd"), (int)SalaryDecisionStatus.Approved);

            // return 
            return sal_SalaryDecisionServices.Count(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static List<SalaryDecisionModel> GetOverGrade(DateTime fromDate, DateTime toDate)
        {
            // init condition
            var condition = "[NextRaiseDate]>='{0}' AND [NextRaiseDate]<'{1}' AND [Grade]=(SELECT [GradeMax] FROM cat_GroupQuantum WHERE cat_GroupQuantum.Id=[GroupQuantumId]) AND [Status]='{2}'".FormatWith(
                fromDate.ToString("yyyy-MM-dd"), toDate.AddDays(1).ToString("yyyy-MM-dd"), (int)SalaryDecisionStatus.Approved);

            // return 
            return sal_SalaryDecisionServices.GetAll(condition, "[NextRaiseDate]").Select(sd => new SalaryDecisionModel(sd)).ToList();
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentSelectedId"></param>
        /// <param name="recordId"></param>
        /// <param name="contractId"></param>
        /// <param name="groupQuantumId"></param>
        /// <param name="quantumId"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<SalaryDecisionModel> GetAll(string keyword, string departmentSelectedId, int? recordId, int? contractId, int? groupQuantumId, int? quantumId, SalaryDecisionType? type,
            SalaryDecisionStatus? status, DateTime? fromDate, DateTime? toDate, bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if (!string.IsNullOrEmpty(keyword))
                condition += " AND ([Name] LIKE N'%{0}%' OR [DecisionNumber] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // departmentSelectedId
            if (!string.IsNullOrEmpty(departmentSelectedId))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record rc WHERE rc.Id = sal_SalaryDecision.RecordId  AND rc.DepartmentId IN ({0}))".FormatWith(departmentSelectedId);

            // record
            if (recordId != null)
                condition += " AND [RecordId]='{0}'".FormatWith(recordId.Value);

            // contract
            if (contractId != null)
                condition += " AND [ContractId]='{0}'".FormatWith(contractId.Value);

            // group quantum
            if (groupQuantumId != null)
                condition += " AND [GroupQuantumId]='{0}'".FormatWith(groupQuantumId.Value);

            // quantum
            if (quantumId != null)
                condition += " AND [QuantumId]='{0}'".FormatWith(quantumId.Value);

            // type
            if (type != null)
                condition += " AND [Type]='{0}'".FormatWith((int)type.Value);

            // status
            if (status != null)
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);

            // template
            if (isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // return
            return sal_SalaryDecisionServices.GetAll(condition, order, limit).Select(sd => new SalaryDecisionModel(sd)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentSelectedId"></param>
        /// <param name="recordId"></param>
        /// <param name="contractId"></param>
        /// <param name="groupQuantumId"></param>
        /// <param name="quantumId"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<SalaryDecisionModel> GetPaging(string keyword, string departmentSelectedId, int? recordId, int? contractId, int? groupQuantumId, int? quantumId, SalaryDecisionType? type,
            SalaryDecisionStatus? status, DateTime? fromDate, DateTime? toDate, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ([Name] LIKE N'%{0}%' OR [DecisionNumber] LIKE N'%{0}%' OR [DecisionNumber] LIKE N'%{0}%'".FormatWith(keyword.EscapeQuote()) +
                             " OR [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')) "
                                 .FormatWith(keyword.EscapeQuote());
            }

            // departmentSelectedId
            if (!string.IsNullOrEmpty(departmentSelectedId))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record rc WHERE rc.Id = sal_SalaryDecision.RecordId  AND rc.DepartmentId IN ({0}))".FormatWith(departmentSelectedId);

            // record
            if (recordId != null)
                condition += " AND [RecordId]='{0}'".FormatWith(recordId.Value);

            // contract
            if (contractId != null)
                condition += " AND [ContractId]='{0}'".FormatWith(contractId.Value);

            // group quantum
            if (groupQuantumId != null)
                condition += " AND [GroupQuantumId]='{0}'".FormatWith(groupQuantumId.Value);

            // quantum
            if (quantumId != null)
                condition += " AND [QuantumId]='{0}'".FormatWith(quantumId.Value);

            // type
            if (type != null)
                condition += " AND [Type]='{0}'".FormatWith((int)type.Value);

            // status
            if (status != null)
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);

            // template
            if (isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // get result
            var result = sal_SalaryDecisionServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<SalaryDecisionModel>(result.Total, result.Data.Select(sd => new SalaryDecisionModel(sd)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SalaryDecisionModel Create(SalaryDecisionModel model)
        {
            // init entity
            var entity = new sal_SalaryDecision();

            // set entity props
            model.FillEntity(ref entity);

            // create
            return new SalaryDecisionModel(sal_SalaryDecisionServices.Create(entity));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SalaryDecisionModel Update(SalaryDecisionModel model)
        {

            // init entity
            var entity = new sal_SalaryDecision();

            // set entity props
            model.FillEntity(ref entity);

            // update
            return new SalaryDecisionModel(sal_SalaryDecisionServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SalaryDecisionModel Delete(int id)
        {
            // get model
            var model = GetById(id);

            // check result
            if (model != null)
            {
                // set deleted status
                model.IsDeleted = true;

                // update
                return Update(model);
            }

            // invalid id
            return null;
        }
    }
}