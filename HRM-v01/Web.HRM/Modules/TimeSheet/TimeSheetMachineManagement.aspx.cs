using System;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetMachineManagement : BasePage
    {        
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            }
        }

        /// <summary>
        /// init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if(int.TryParse(param, out var id))
                {
                    // init window props
                    if(id > 0)
                    {
                        // edit
                        wdSetting.Title = @"Sửa";
                        wdSetting.Icon = Icon.Pencil;
                    }
                    else
                    {
                        // insert
                        wdSetting.Title = @"Thêm mới";
                        wdSetting.Icon = Icon.Add;
                    }

                    // init id
                    hdfId.Text = id.ToString();

                    // init object
                    var model = new TimeSheetMachineModel(null);

                    // check id
                    if (id > 0)
                    {
                        var result = TimeSheetMachineController.GetById(id);
                        if (result != null)
                            model = result;
                    }

                    // set props
                    txtName.Text = model.Name;
                    txtIpAddress.Text = model.IPAddress;
                    txtLocation.Text = model.Location;
                    txtSerialNumber.Text = model.SerialNumber;
                    hdfDepartmentId.Text = model.DepartmentId.ToString();
                    cbxDepartment.Text = model.DepartmentName;

                    // show window
                    wdSetting.Show();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// insert or update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new TimeSheetMachineModel(null);

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = TimeSheetMachineController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // set new props for entity
                model.Name = txtName.Text;
                model.SerialNumber = txtSerialNumber.Text;
                model.IPAddress = txtIpAddress.Text;
                model.Location = txtLocation.Text;
                if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
                {
                    model.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);

                }
                // check entity id
                if(model.Id > 0)
                {                    
                    var timeSheetMachineModal = TimeSheetMachineController.GetBySerialNumber(model.SerialNumber);
                    // check existed
                    if (timeSheetMachineModal == null || timeSheetMachineModal.Id == model.Id)
                    {
                        model.UpdatedAt = DateTime.Now;
                        model.UpdatedBy = CurrentUser.User.UserName;
                        // update
                        TimeSheetMachineController.Update(model);
                    }
                    else
                    {
                        Dialog.Alert("Số serial máy đã tồn tại, vui lòng nhập serial khác");
                    }
                }
                else
                {
                    var timeSheetMachineModal = TimeSheetMachineController.GetBySerialNumber(model.SerialNumber);
                    // check existed
                    if (timeSheetMachineModal == null)
                    {
                        model.CreatedBy = CurrentUser.User.UserName;
                        model.CreatedAt = DateTime.Now;
                        model.UpdatedAt = DateTime.Now;
                        model.UpdatedBy = "";
                        // insert
                        TimeSheetMachineController.Create(model);
                    }
                    else
                    {
                        Dialog.Alert("Số serial máy đã tồn tại, vui lòng nhập serial khác");
                    }
                       
                }
                // hide window
                wdSetting.Hide();

                // reload data
                gpTimeSheetMachine.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];

                // parse id
                if(!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }

                // delete
                TimeSheetMachineController.Delete(id);

                // reload data
                gpTimeSheetMachine.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeDepartment_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {            
            storeDepartment.DataSource = CurrentUser.DepartmentsTree;            
            storeDepartment.DataBind();
        }

    }
}