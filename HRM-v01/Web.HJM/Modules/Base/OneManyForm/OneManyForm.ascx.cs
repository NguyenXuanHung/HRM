using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUI.BaseControl;
using Ext.Net;
using WebUI.Controller;
using DataController;
using WebUI.Entity;
using SoftCore;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_OneManyForm_OneManyForm : BaseForm, IBaseControl, IBaseWindow
    {
        public event EventHandler AfterClickUpdateButton;

        protected void Page_Load(object sender, EventArgs e)
        {
            OneManyForm.GridPanelName = this.GridPanelName;
            OneManyForm.AccessHistory = this.AccessHistory;
            GenerateDetailTable();
            AddButton();
            if (CurrentUser.User.IsSuperUser)
            {
                LoadDataForConfig();
            }
        }

        #region Generate control

        /// <summary>
        /// sinh các detail table nằm trong tab
        /// </summary>
        public void GenerateDetailTable()
        {
            Ext.Net.TabPanel tab = new TabPanel();
            tab.Border = false;
            tab.ID = this.ID + "TabPanel";
            tab.AnchorHorizontal = "100%";
            tab.Height = 170;
            tab.EnableTabScroll = true;
            tab.Plugins.Add(new TabScrollerMenu()
            {
                PageSize = 30,
                Width = new Unit(500),
            });
            List<OneManyFormInfo> rs = OneManyFormController.GetInstance()
                .GetAll(this.GridPanelName + "OneManyForm", 1);
            int count = 0;
            foreach (OneManyFormInfo item in rs)
            {
                Control ct = this.Page.LoadControl("../Base/MiniGridPanel/MiniGrid.ascx");
                ct.ID = this.GridPanelName + "_OneManyForm_" + item.TableName; //Tên của Grid được lưu trong CSDL
                GridTable gridTable = ct as GridTable;
                gridTable.Height = 150;
                gridTable.Width = OneManyForm.Width;
                //gridTable
                gridTable.OutSideQuery = item.ForeignKey;
                count++;
                Ext.Net.Panel panel = new Ext.Net.Panel(count + "." + item.Title);
                panel.ID = "pnl" + item.TableName;
                panel.AnchorHorizontal = "100%";
                panel.Border = false;

                Ext.Net.Container c = new Container();
                c.AnchorHorizontal = "100%";
                c.Height = 160;
                c.Layout = "Form";
                c.ContentControls.Add(ct);
                //  c.Items.Add(gridTable.GetGridPanel());
                panel.Items.Add(c);

                tab.Items.Add(panel);
            }

            OneManyForm.AddDetailTable(tab, this.GridPanelName + "OneManyForm");
        }

        private void AddButton()
        {
            OneManyForm.AddButton(btnUpdateAndClose);
            OneManyForm.AddButton(btnUpdate_OneMany);
            Ext.Net.MenuItem item = new Ext.Net.MenuItem("Chọn bảng detail");
            item.Icon = Icon.Table;
            item.Listeners.Click.Handler = wdConfigTable.ClientID + ".show();";
            OneManyForm.AddToConfigMenu(item);
        }

        #endregion

        /// <summary>
        /// Sự kiện của 2 nút cập nhật, cập nhật & đóng lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_OneMany_Click(object sender, DirectEventArgs e)
        {
            List<MiniGridData> miniGridDataList = OneManyForm.GetMiniGridData();
            if (hdfCommand.Text == "insert")
            {
                OneManyForm.CallInsertFunction();
                if (e.ExtraParams["SqlCommand"] == "UpdateAndClose")
                {
                    hdfCommand.Text = "";
                    OneManyForm.Hide();
                }
                else
                {
                    Dialog.ShowNotification("Thêm mới thành công");
                }

                //  Dialog.ShowNotification(MasterTable.SCOPE_IDENTITY.ToString());  
                InsertDetailTableValue(miniGridDataList, OneManyForm.SCOPE_IDENTITY.ToString());
            }
            else if (hdfCommand.Text == "update")
            {
                OneManyForm.CallUpdateFunction(hdfPrimaryColumnName.Text, hdfCurrentRecordID.Text);
                OneManyForm.Hide();
                hdfCommand.Text = "";
                InsertDetailTableValue(miniGridDataList, hdfCurrentRecordID.Text);
            }
        }

        private void InsertDetailTableValue(List<MiniGridData> miniGridDataList, string ForeignKeyValue)
        {
            List<OneManyFormInfo> formList =
                OneManyFormController.GetInstance().GetAll(this.GridPanelName + "OneManyForm", 1);
            //trong chuối json đã có sẵn tên cột, chỉ cần replace lại là ok
            foreach (var item in miniGridDataList)
            {
                if (!string.IsNullOrEmpty(item.JsonData))
                {
                    string tableName = item.MiniGridName.Replace(this.GridPanelName + "_OneManyForm_", "");
                    OneManyFormInfo frmTmp =
                        (from t in formList where t.TableName == tableName select t).FirstOrDefault();
                    string sqlUpdate = "update {1} set {0} where " + frmTmp.PrimaryKey + " = N'";
                    string sqlInsert = "insert into {2}({0}) values ({1})";
                    string checkSql = "";
                    //cần phải viết lại hàm tách
                    string[] tmp = item.JsonData.Replace("[", "").Replace("]", "").Replace("},{", "#").Replace("{", "")
                        .Replace("}", "").Replace("\"", "").Split('#');
                    //Chạy để insert và update từng bản ghi một
                    foreach (var jsonRecord in tmp)
                    {
                        string[] colArr = jsonRecord.Split(',');
                        string columnInsert = "";
                        string valueInsert = "";
                        string valueUpdate = "";
                        foreach (var col in colArr)
                        {
                            string[] i = col.Split(':');
                            //fix lại định dạng ngày tháng, [nên chuyển thành regex để bắt lỗi]
                            if (i[1].Contains("-") && i[1].Length == 13)
                            {
                                i[1] = i[1].Replace("T00", "");
                            }

                            if (i[0] != frmTmp.PrimaryKey)
                            {
                                valueUpdate += "[" + i[0] + "] = N'" + i[1] + "',";
                                //Khóa chính mặc định là tự động tăng, nếu muốn ko tự động tăng thì phải tạo thêm 1 cấu hình 
                                columnInsert += "[" + i[0] + "],";
                                valueInsert += "N'" + i[1] + "',";
                            }
                            else
                            {
                                if (frmTmp.PrKeyIsAutoIncrement == false)
                                {
                                    columnInsert += "[" + i[0] + "],";
                                    valueInsert += "N'" + i[1] + "',";
                                }

                                sqlUpdate += i[1] + "'";
                                checkSql = "select top 1 * from " + tableName + " where " + frmTmp.PrimaryKey +
                                           " = N'" + i[1] + "'";
                            }
                        }

                        if (DataHandler.GetInstance().ExecuteScalar(checkSql) != null)
                        {
                            valueUpdate +=
                                "[" + frmTmp.ForeignKey + "] = N'" + ForeignKeyValue +
                                "'"; //Thêm cột khóa ngoại vào câu lệnh insert
                            sqlUpdate = string.Format(sqlUpdate, valueUpdate, tableName);
                            DataHandler.GetInstance().ExecuteNonQuery(sqlUpdate);
                        }
                        else
                        {
                            columnInsert += "[" + frmTmp.ForeignKey + "]";
                            valueInsert += "N'" + ForeignKeyValue + "'";
                            sqlInsert = string.Format(sqlInsert, columnInsert, valueInsert, tableName);
                            DataHandler.GetInstance().ExecuteNonQuery(sqlInsert);
                        }

                        //    Dialog.ShowNotification(sqlUpdate + "   " + sqlInsert);
                    }
                }
            }
        }


        #region cấu hình

        /// <summary>
        /// Lấy dữ liệu để phục vụ việc cấu hình
        /// </summary>
        private void LoadDataForConfig()
        {
            try
            {
                if (OneManyForm.formInfo == null)
                    OneManyForm.formInfo = FormController.GetInstance().GetForm(this.GridPanelName + "OneManyForm");
                List<OneManyFormInfo> rs = OneManyFormController.GetInstance()
                    .GetAll(this.GridPanelName + "OneManyForm", 1);
                List<string> table =
                    (from t in SoftCore.Util.GetInstance().GetRelationTable(OneManyForm.formInfo.TableName)
                        where rs.Where(p => p.TableName == t).Count() == 0
                        select t).ToList();
                foreach (string item in table)
                {
                    Ext.Net.ListItem list = new Ext.Net.ListItem(item, item);
                    cbTable.Items.Add(list);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("OneManyForm / LoadDataForConfig() = " + ex.Message);
            }
        }

        protected void btnAddConfig_Click(object sender, DirectEventArgs e)
        {
            try
            {
                OneManyFormInfo info = new OneManyFormInfo();
                info.Title = txtTitle.Text;
                info.TableName = cbTable.SelectedItem.Value;
                info.Order = 0;
                info.Visible = chkVisible.Checked;
                info.ForeignKey = cbForeignKey.SelectedItem.Value;
                info.OneManyFormName = this.GridPanelName + "OneManyForm";
                OneManyFormController.GetInstance().Add(info);
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private static object lockObj = new object();

        /// <summary>
        /// autosave cho store chứa thông tin cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleColumnChanges(object sender, BeforeStoreChangedEventArgs e)
        {
            ChangeRecords<OneManyFormInfo> columns = e.DataHandler.ObjectData<OneManyFormInfo>();
            foreach (OneManyFormInfo item in columns.Updated)
            {
                lock (lockObj)
                {
                    OneManyFormController.GetInstance().Update(item);
                }

                if (StoreTableDetailInfo.UseIdConfirmation)
                {
                    e.ConfirmationList.ConfirmRecord(item.ID.ToString());
                }
            }

            e.Cancel = true;
        }

        protected void storeForeignKey_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(cbTable.SelectedItem.Value))
                return;
            try
            {
                List<string> foreignKey = SoftCore.Util.GetInstance().GetForeignKeyOfTable(cbTable.SelectedItem.Value);
                object[] Data = new object[foreignKey.Count()];
                int c = 0;
                foreach (var item in foreignKey)
                {
                    Data[c] = new object[] {item};
                    c++;
                }

                storeForeignKey.DataSource = Data;
                storeForeignKey.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("OneManyForm/storeForeignKey_Refresh = " + ex.Message);
            }
        }

        #endregion

        public void Show()
        {
            OneManyForm.CommandButton = this.CommandButton;
            if (this.CommandButton == Command.Insert)
            {
                hdfCommand.Text = "insert";
                btnUpdateAndClose.Text = "Cập nhật & đóng lại";
            }
            else
            {
                btnUpdateAndClose.Text = "Cập nhật";
                hdfCommand.Text = "update";
            }

            OneManyForm.Show();
        }

        public void Show(object id)
        {
            if (id != null)
            {
                SetValue(id);
                hdfCurrentRecordID.Text = id.ToString();
                OneManyForm.CommandButton = this.CommandButton;
                if (this.CommandButton == Command.Insert)
                {
                    hdfCommand.Text = "insert";
                    btnUpdateAndClose.Text = "Cập nhật & đóng lại";
                }
                else
                {
                    btnUpdate_OneMany.Hide();
                    btnUpdateAndClose.Text = "Cập nhật";
                    hdfCommand.Text = "update";
                }

                OneManyForm.Show();
            }
        }

        public string GetID()
        {
            throw new NotImplementedException();
        }

        public object GetValue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Giá trị của khóa chính</param>
        public void SetValue(object value)
        {
            try
            {
                if (value == null)
                    return;
                OneManyForm.formInfo = FormController.GetInstance().GetForm(this.GridPanelName + "OneManyForm");
                if (string.IsNullOrEmpty(hdfPrimaryColumnName.Text))
                {
                    hdfPrimaryColumnName.Text = Util.GetInstance().GetPrimaryKeyOfTable(OneManyForm.formInfo.TableName);
                }

                OneManyForm.PrimaryColumnName = hdfPrimaryColumnName.Text;
                OneManyForm.CommandButton = Command.Update; //Xem xét sửa lại
                OneManyForm.SetValue(value);
                hdfCurrentRecordID.Text = value.ToString();
                hdfCurrentTableName.Text = OneManyForm.formInfo.TableName;
                OneManyForm.ReloadTableDetail(value.ToString());
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Function GetFirstRecord: = " + ex.Message);
            }
        }
    }
}