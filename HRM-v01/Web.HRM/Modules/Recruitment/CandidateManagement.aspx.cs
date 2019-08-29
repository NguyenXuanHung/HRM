using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Utils;

namespace Web.HRM.Modules.Recruitment
{
    public partial class CandidateManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                dfStartDate.SetValue(ConvertUtils.GetStartDayOfMonth());
                dfEndDate.SetValue(ConvertUtils.GetLastDayOfMonth());
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            var id = e.ExtraParams["Id"];
            var recordId = e.ExtraParams["RecordId"];
            if (int.TryParse(id, out var resultId) && resultId > 0 && int.TryParse(recordId, out var resultRecordId) && resultRecordId > 0)
            {
                inputEmployee.btnEdit_Click(RecordType.Candidate);
            }
            else
            {
                inputEmployee.InitWindowInput(RecordType.Candidate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            var id = e.ExtraParams["Id"];
            var recordId = e.ExtraParams["RecordId"];

            if (int.TryParse(id, out var resultId) && resultId > 0)
            {
                CandidateController.Delete(resultId);
                if(int.TryParse(recordId, out var resultRecordId) && resultRecordId > 0)
                    RecordController.Delete(resultRecordId);
            }
            gpCandidate.Reload();
        }

        protected void inputEmployee_OnUserControlClose()
        {
            gpCandidate.Reload();
        }
    }
}