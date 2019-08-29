using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using DataController;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Service.HumanRecord;
using Web.Core.Object.Report;

namespace Web.HJM.Modules.Report
{

    public partial class Default : BasePage
    {
        public GridPanel gp;

        public List<DanhMucCanboInfo> listThongTin;
        private const string ConstTypeTimeSheet = "Automatic";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                BindReportList();
                BindDepartmentTree();
                hdfMaDonvi.Text = Session["MaDonVi"].ToString();
                hdfTypeTimeSheet.Text = ConstTypeTimeSheet;
                // departments
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfSelectedDepartmentIdList.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                spfyear.Text = DateTime.Now.Year.ToString();
                dfReportDate.SelectedDate = DateTime.Now;
                stShow.AddField(new RecordField("ID", RecordFieldType.Int));
                stShow.AddField(new RecordField("Name"));
                stShow.ClearMeta();
                stShow.DataSource = ThongTinCanBo().OrderBy(x => x.Name);
                stShow.DataBind();
                stDieuKienLoc2.DataSource = DieuKienLocCanBo().OrderBy(x => x.Name);
                stDieuKienLoc2.DataBind();
                GridSelect();

            }
        }

        #region Private Methods

        /// <summary>
        /// init root menu of report list (left panel)
        /// </summary>
        private void BindReportList()
        {
            // init root node (tbl ReportGroup)
            var root = new Ext.Net.TreeNode()
            {
                Text = "Report",
                Expanded = true,
                NodeID = @"0"
            };
            // add to tree view
            treeReportList.Root.Add(root);
            // bind child node
            BindGroupNode(root);
        }

        /// <summary>
        /// bind group node of report list
        /// </summary>
        /// <param name="parentNode"></param>
        private void BindGroupNode(Ext.Net.TreeNode parentNode)
        {
            var reportGroupList = new ReportGroupController().GetReportGroupByParentID(parentNode.NodeID, true);
            foreach (var childNode in reportGroupList.Select(item => new Ext.Net.TreeNode()
            {
                // init group node
                Text = item.ReportGroupName,
                NodeID = item.Id.ToString(),
                Icon = Icon.Folder,
                Qtip = item.ReportGroupName,
                Expanded = true
            }))
            {
                // add tree view
                parentNode.Nodes.Add(childNode);
                // bind child group
                BindGroupNode(childNode);
                // bind child node
                BindChildNode(childNode);
            }
        }

        /// <summary>
        /// bind child node of report list
        /// </summary>
        /// <param name="parentNode"></param>
        private void BindChildNode(Ext.Net.TreeNode parentNode)
        {

            var lstReport = new ReportGroupController().GetReportByGroupID(parentNode.NodeID);
            var script = string.Empty;
            var number = 0;
            foreach (var reportInfo in lstReport)
            {
                // init child node
                var childNode = new Ext.Net.TreeNode()
                {
                    Text = (++number) + ", " + reportInfo.Name,
                    NodeID = reportInfo.ID.ToString()
                };
                // check report key name
                if (reportInfo.ReportKeyName == null)
                {
                    // handler double click function
                    childNode.Listeners.DblClick.Handler =
                        "document.getElementById('divReportPreview').style.display = 'none';" +
                        "hdfTitle.setValue('" + reportInfo.Name + "');" +
                        "txtReportTitle.setValue('" + reportInfo.Name.ToUpper() +
                        " NĂM ' + new Date().getFullYear());" +
                        "wdReportFilter.show();" +
                        reportInfo.Javascript;
                    // handler click function
                    childNode.Listeners.Click.Handler = "getReportPreview('" + reportInfo.ImageReportPreview + "');" +
                                                        "hdfReportID.setValue('" + childNode.NodeID + "');";
                    // handler context menu
                    childNode.Listeners.ContextMenu.Handler = "hdfReportID.setValue('" + reportInfo.ID + "');";
                    // add tree view
                    parentNode.Nodes.Add(childNode);
                    // set javascript
                    if (!string.IsNullOrEmpty(reportInfo.Javascript))
                    {
                        script += "if(hdfReportID.getValue()=='" + reportInfo.ID + "') {" +
                                  "alert(hdfReportID.getValue());" +
                                  reportInfo.Javascript + "}";
                    }
                }
                else
                {
                    // all current report key is null => ignore
                    wdBaoCaoTongHop.Title = reportInfo.Name;
                    childNode.Listeners.DblClick.Handler =
                        "document.getElementById('divReportPreview').style.display = 'none';" +
                        "hdfTitle.setValue('" + reportInfo.Name + "');" +
                        "txtTieuDe.setValue('" + reportInfo.Name.ToUpper() + "');" +
                        "txtReportTitle.setValue('" + reportInfo.Name.ToUpper() + " NĂM ' new Date().getFullYear());" +
                        "wdReportFilter.show();" +
                        "#{storeFilterTable}.reload();" +
                        "wdReportFilter.hide();" +
                        "wdBaoCaoTongHop.show();" +
                        reportInfo.Javascript;
                    childNode.Listeners.Click.Handler = "getReportPreview('" + reportInfo.ImageReportPreview + "');" +
                                                        "hdfReportID.setValue('" + childNode.NodeID + "');";
                    childNode.Listeners.ContextMenu.Handler = "hdfReportID.setValue('" + reportInfo.ID + "');";
                    parentNode.Nodes.Add(childNode);

                    if (!string.IsNullOrEmpty(reportInfo.Javascript))
                    {
                        script += "if(hdfReportID.getValue()=='" + reportInfo.ID + "') {" +
                                  "alert(hdfReportID.getValue());" + reportInfo.Javascript + "}";
                    }
                }
            }

            ltrShowSettingDialog.Text = @"<script type='text/javascript'>" +
                                        @"var ShowSettingDialog = function() {" +
                                        @"document.getElementById('divReportPreview').style.display = 'none';" +
                                        script + @"}" +
                                        @"</script>";
        }

        /// <summary>
        /// get departments list to display on window
        /// </summary>
        private void BindDepartmentTree()
        {
            var rootDepartment = CurrentUser.RootDepartment;
            var departmentsTree = "<li id='{0}' item-expanded='true'>{1}{2}</li>".FormatWith(rootDepartment.Id,
                rootDepartment.Name, PopulateChildDepartment(rootDepartment.Id));
            ltrDepartment.Text = departmentsTree;
        }

        /// <summary>
        /// populate child departments
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private string PopulateChildDepartment(int parentId)
        {
            var lstDepartment = CurrentUser.Departments.Where(d => d.ParentId == parentId).ToList();
            if (lstDepartment.Count == 0)
                return string.Empty;
            var strDepartments = lstDepartment.Aggregate(string.Empty,
                (current, d) => current + "<li id='{0}' item-expanded='true'>{1}{2}</li>".FormatWith(d.Id, d.Name,
                                    PopulateChildDepartment(d.Id)));
            return "<ul>{0}</ul>".FormatWith(strDepartments);
        }

        #endregion

        #region Filter Condition

        protected void storeFilterValue_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                storeFilterValue.DataSource = string.IsNullOrEmpty(cbChooseFilterTable.SelectedItem.Value)
                    ? new DataTable()
                    : DataHandler.GetInstance()
                        .ExecuteDataTable("report_GetTableFilter", "@ID", cbChooseFilterTable.SelectedItem.Value);
                storeFilterValue.DataBind();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        #endregion

        /// <summary>
        /// Lấy danh sách các mã đơn vị được chọn
        /// </summary>
        /// <returns></returns>
        public string GetSelectedDepartment()
        {
            return hdfSelectedDepartmentIdList.Text;
        }

        [DirectMethod(Namespace = "CompanyX")]
        public void AfterEdit(int id, string field, string oldValue, string newValue, object customer)
        {
            grpSelectedReportFilter.Store.Primary.CommitChanges();
        }

        #region window filter

        struct WhereClause
        {
            public string WhereField { get; set; }
            public string WhereValue { get; set; }
        };

        protected void btnShowReport_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var whereList = new List<WhereClause>();
                var sql = "(";
                if (!string.IsNullOrEmpty(e.ExtraParams["SQL"]))
                {
                    var filter = e.ExtraParams["SQL"].Split(';');
                    whereList.AddRange(from item in filter
                        where !string.IsNullOrEmpty(item)
                        select item.Split('=')
                        into pair
                        select new WhereClause
                        {
                            WhereField = pair[0],
                            WhereValue = pair[1]
                        });
                    var lastWhere = whereList.OrderBy(t => t.WhereField).FirstOrDefault().WhereField;
                    foreach (var item in whereList.OrderBy(t => t.WhereField))
                    {
                        if (item.WhereField == lastWhere)
                        {

                            sql += " " + item.WhereField + " = N'" + item.WhereValue + "' or";
                        }
                        else
                        {
                            if (sql.EndsWith("or"))
                            {
                                sql = sql.Remove(sql.LastIndexOf("or"));
                                sql += ") and (";
                            }
                            else if (sql.EndsWith("and"))
                            {
                                sql = sql.Remove(sql.LastIndexOf("and"));
                                sql += " ) ";
                            }

                            sql += " " + item.WhereField + " = N'" + item.WhereValue + "' or";
                        }

                        lastWhere = item.WhereField;
                    }

                    sql = sql.Remove(sql.LastIndexOf(" ")) + ")";
                }
                else
                    sql = "1 = 1";

                var reportFilter = new ReportFilter()
                {
                    ReportTitle = txtReportTitle.Text.Trim(),
                    RecordId = int.Parse("0" + hdfChonCanBo.Text),
                    Note = htmlNote.Value.ToString(),
                    WhereClause = sql,
                    Reporter = CurrentUser.User.DisplayName,
                    Gender = cbGender.SelectedItem.Value,
                    LoaiCanBo = cbCanBo.SelectedItem.Value,
                    SelectedDepartment = hdfSelectedDepartmentIdList.Text,
                    SessionDepartment = CurrentUser.DepartmentsTree[0].Id.ToString(),
                    StartMonth = ReportController.GetInstance().GetStartMonth(cbMonth.SelectedItem.Value),
                    EndMonth = ReportController.GetInstance().GetEndMonth(cbMonth.SelectedItem.Value),
                    MinSeniority = int.Parse("0" + txtSeniorityMin.Text),
                    MaxSeniority = int.Parse("0" + txtSeniorityMax.Text),
                    MinAge = int.Parse("0" + txtAgeMin.Text),
                    MaxAge = int.Parse("0" + txtAgeMax.Text),
                    Title1 = txttieudechuky1.Text,
                    Title2 = txttieudechuky2.Text,
                    Title3 = txttieudechuky3.Text,
                    Name1 = txtnguoiky1.Text,
                    Name2 = txtnguoiky2.Text,
                    Name3 = txtnguoiky3.Text,
                    ReportedDate = dfReportDate.SelectedDate,
                    BirthPlaceWard = hdfBirthPlaceWardId.Text,
                    BirthPlaceDistrict = hdfBirthPlaceDistrictId.Text,
                    BirthPlaceProvince = hdfBirthPlaceProvinceId.Text,
                    HopDong = cb_HopDong.SelectedItem.Value,
                    LoginDepartment = string.Join(",", CurrentUser.Departments.Select(d => d.Id)),
                    TimeSheetReportId = int.Parse("0" + hdfTimeSheetReport.Text),
                };
                var tbl = hr_RecordServices.GetById(reportFilter.RecordId);
                if (tbl != null)
                {
                    reportFilter.EmployeeCode = tbl.EmployeeCode;
                }

                if (!Util.GetInstance().IsDateNull(dfStartDate.SelectedDate))
                {
                    reportFilter.StartDate = dfStartDate.SelectedDate;
                }

                if (!Util.GetInstance().IsDateNull(dfEndDate.SelectedDate))
                {
                    reportFilter.EndDate = dfEndDate.SelectedDate;
                }

                reportFilter.Year = string.IsNullOrEmpty(spfyear.Text) ? DateTime.Now.Year : int.Parse(spfyear.Text);
                if (string.IsNullOrEmpty(reportFilter.Gender))
                {
                    reportFilter.Gender = "";
                }

                var tinhTrang = cbWorkingStatus.SelectedItems.ValuesToJsonArray();
                var split = tinhTrang.Split('[', ']', ' ', '"');
                tinhTrang = split.Where(item => item.Trim() != "").Aggregate("", (current, item) => current + item);
                reportFilter.WorkingStatus = tinhTrang;

                Session["rp"] = reportFilter;
                var s = Util.GetInstance().RemoveSpaceAndVietnamese(hdfTitle.Text);
                RM.RegisterClientScriptBlock("addTabReport",
                    string.Format(
                        "addTabReport(pnTabReport,hdfReportID.getValue(),'BaoCao_Main.aspx?type={0}',hdfTitle.getValue());",
                        s.Replace(",", "")));
                RM.RegisterClientScriptBlock("RemoveAndAddNewTab", "RemoveAndAddNewTab('" + hdfReportID.Text + "')");
            }

            catch (Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        protected void storeFilterTable_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                storeFilterTable.DataSource =
                    new ReportTableFilterController().GetTableFilter(int.Parse("0" + hdfReportID.Text));
                storeFilterTable.DataBind();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }


        #endregion

        protected void btnChon_Click(object sender, DirectEventArgs e)
        {
            // X.Js.Call("cbxChonCanBo.hide();cbGender.hide();cbWorkingStatus.hide();cbChonPhongBan.hide();CompositeField1.hide();cbMonth.hide();spfyear.hide();CompositeField7.hide();dfStartDate.hide();dfEndDate.hide();cbx_NoiSinhXa.hide();cbx_NoiSinhHuyen.hide();cbx_NoiSinhTinh.hide()");
            string Id = hdfReportID.Text;
            string js = "";
            int i = 1;
            RowSelectionModel smLoc = this.gpDieuKienLoc2.SelectionModel.Primary as RowSelectionModel;
            foreach (SelectedRow row in smLoc.SelectedRows)
            {
                js = js + DieuKienLocCanBo().FirstOrDefault(x => x.ID == int.Parse(row.RecordID)).Description + ";";
            }

            X.Js.Call(js + "#{storeFilterTable}.reload(); a()");
            RowSelectionModel sm = this.gpShow.SelectionModel.Primary as RowSelectionModel;
            listThongTin = new List<DanhMucCanboInfo>();
            foreach (SelectedRow row in sm.SelectedRows)
            {
                listThongTin.Add(new DanhMucCanboInfo(int.Parse(row.RecordID),
                    ThongTinCanBo().FirstOrDefault(x => x.ID == int.Parse(row.RecordID)).Name,
                    ThongTinCanBo().FirstOrDefault(x => x.ID == int.Parse(row.RecordID)).Description, 0, false));
            }

            Session["ListThongTin"] = listThongTin;
            RowSelectionModel sm1 = this.gpDieuKienLoc.SelectionModel.Primary as RowSelectionModel;
            ReportList_ReportTableFilter model1 = new ReportList_ReportTableFilter();
            model1.ReportID = int.Parse(Id);
            new ReportGroupController().UpdateReportTableFilter(model1, 0);
            foreach (SelectedRow row in sm1.SelectedRows)
            {
                ReportList_ReportTableFilter model = new ReportList_ReportTableFilter();
                model.ReportID = int.Parse(Id);
                model.ReportFilterID = int.Parse(row.RecordID);
                model.WhereField = new ReportGroupController().GetReportTableFilterById(int.Parse(row.RecordID))
                    .SQLTableName;
                model.Order = i;
                model.ReturnInWhereClause = false;
                new ReportGroupController().UpdateReportTableFilter(model, i);
                i++;
            }

        }

        #region Báo cáo tổng hợp

        public Window CreateWinDow(string titleName)
        {
            var win = new Window
            {
                ID = "wdBaoCaoTongHop",
                Title = titleName,
                Width = Unit.Pixel(1000),
                Height = Unit.Pixel(700),
                Icon = Icon.Printer,
                Modal = true,
                Collapsible = true,
                Maximizable = true,
                Hidden = true,
                AutoHeight = true,
                ConstrainHeader = true,
                Padding = 6,
                Constrain = true,
                Resizable = false
            };
            // Create items
            FormPanel editpanel = new FormPanel()
            {
                ID = "Panel_Edit",
                AutoWidth = true,
                Padding = 10,
            };

            TriggerField ftName = new TriggerField();
            ftName.AnchorHorizontal = "100%";
            ftName.AutoWidth = true;
            ftName.FieldLabel = "Tiêu đề báo cáo";
            ftName.ID = "txtReportName";
            ftName.Text = titleName.ToString();
            FieldTrigger ftFieldTrigger =
                new FieldTrigger {Icon = TriggerIcon.Clear, Qtip = "Xóa trắng", HideTrigger = false};
            ftName.Triggers.Add(ftFieldTrigger);
            ftName.Listeners.TriggerClick.Handler = "txtReportName.reset();";
            editpanel.Items.Add(ftName);

            // end create items
            // create panel
            Ext.Net.Container ctn = new Ext.Net.Container()
            {
                ID = "ctn",
                Layout = "ColumnLayout",
                Height = Unit.Pixel(300),
                AnchorHorizontal = "100%",
            };

            Ext.Net.Container ctn1 = new Ext.Net.Container()
            {
                Layout = "FormLayout",
                AnchorHorizontal = "100%",
                LabelAlign = LabelAlign.Top,
                AutoHeight = true,
                ColumnWidth = 0.5,
            };

            FieldSet flSet1 = new FieldSet()
            {
                AutoHeight = true,
                Title = "Các trường hiển thị"
            };

            this.gpShow.ColumnModel.Columns.Add(new Column {DataIndex = "ID", Header = "Mã"});
            this.gpShow.ColumnModel.Columns.Add(new Column
            {
                DataIndex = "Name",
                Header = "Tên trường",
                Width = Unit.Pixel(300)
            });

            flSet1.Items.Add(this.gpShow);


            ctn1.Items.Add(flSet1);

            Ext.Net.Container ctn2 = new Ext.Net.Container()
            {
                AutoHeight = true,
                LabelAlign = LabelAlign.Top,
                Layout = "FormLayout",
                AnchorHorizontal = "100%",
                ColumnWidth = 0.5,
            };

            FieldSet flSet2 = new FieldSet()
            {
                AutoHeight = true,
                Title = "Các điều kiện lọc"
            };

            this.gpDieuKienLoc.ColumnModel.Columns.Add(new Column {DataIndex = "ID", Header = "Mã"});
            this.gpDieuKienLoc.ColumnModel.Columns.Add(new Column
            {
                DataIndex = "DescriptionTableName",
                Header = "Tiêu đề",
                Width = Unit.Pixel(300)
            });
            //  flSet2.Items.Add(this.gpDieuKienLoc);

            flSet2.Items.Add(gp);

            ctn2.Items.Add(flSet2);

            ctn.Items.Add(ctn1);
            ctn.Items.Add(ctn2);
            editpanel.Items.Add(ctn);
            // end panel
            win.Buttons.Add(btnChon);
            win.Buttons.Add(Button6);
            win.Items.Add(editpanel);
            win.Render(this.Form);
            return win;
        }

        public TreePanel TreeSelect(FieldSet ds)
        {
            TreePanel tp = new TreePanel()
            {
                //   ID = "tpSelect",
                Title = "Danh mục hiển thị",
                Icon = Icon.Accept,
                Height = Unit.Pixel(270),
                //  Width = Unit.Pixel(250),
                UseArrows = true,
                AutoScroll = true,
                Animate = true,
                EnableDD = true,
                ContainerScroll = true,
                RootVisible = true,
            };
            Ext.Net.TreeNode chidNode = new Ext.Net.TreeNode()
            {
                Text = "1234",
                NodeID = "1",
                Leaf = true,
                Checked = ThreeStateBool.False,
                // Qtip = item.Tooltip,
            };
            Ext.Net.TreeNode tn = new Ext.Net.TreeNode()
            {
                Text = "Report",
                Expanded = true,
                NodeID = "0"
            };
            Ext.Net.TreeNode tn1 = new Ext.Net.TreeNode()
            {
                Text = "To do",
                NodeID = "12",
                Checked = chidNode.Checked,
            };
            tn1.Nodes.Add(chidNode);

            Ext.Net.TreeNode tn2 = new Ext.Net.TreeNode()
            {
                Text = "Energy foods",
                NodeID = "13",
                Checked = chidNode.Checked,
            };
            tn2.Nodes.Add(chidNode);
            tn.Nodes.Add(tn1);
            tn.Nodes.Add(tn2);
            tp.Root.Add(tn);
            return tp;
        }

        public void GridSelect()
        {
            gp = new GridPanel()
            {
                StoreID = "stDieuKienLoc",
                // ID="gpLoc",
                StripeRows = true,
                Title = "Điều kiện chọn",
                Collapsible = true,
                AutoWidth = true,
                AutoScroll = true,
                Height = Unit.Pixel(270),
            };
            CheckboxSelectionModel csm = new CheckboxSelectionModel();
            gp.ColumnModel.Columns.AddRange(new Column[]
            {
                new Column
                {
                    Header = "Mã",
                    DataIndex = "ID",
                    Resizable = false,
                    MenuDisabled = true,
                    Fixed = true,
                },
                new Column
                {
                    Header = "Tiêu đề",
                    Width = Unit.Pixel(300),
                    DataIndex = "DescriptionTableName"
                },
            });
            gp.SelectionModel.Add(csm);

        }

        public Store GetSore()
        {
            Store st = new Store()
            {
                ID = "stShow",
            };

            JsonReader jd = new JsonReader()
            {
                IDProperty = "ID"
            };
            RecordField rf1 = new RecordField()
            {
                Name = "ID"
            };
            RecordField rf2 = new RecordField()
            {
                Name = "DescriptionTableName"
            };

            jd.Fields.Add(rf1);
            jd.Fields.Add(rf2);
            st.Reader.Add(jd);

            return st;
        }


        #endregion

        public List<DanhMucCanboInfo> ThongTinCanBo()
        {
            var list = new List<DanhMucCanboInfo>
            {
                new DanhMucCanboInfo(1, "Mã cán bộ", "MA_CB", 0, false),
                new DanhMucCanboInfo(2, "Tên cán bộ", "TEN_CB", 0, false),
                new DanhMucCanboInfo(3, "Họ tên", "HO_TEN", 0, false),
                new DanhMucCanboInfo(4, "Số hiệu", "SOHIEU_CCVC", 0, false),
                new DanhMucCanboInfo(5, "Ngày sinh", "NGAY_SINH", 0, false),
                new DanhMucCanboInfo(6, "Giới tính", "GIOI_TINH", 0, false),
                new DanhMucCanboInfo(7, "Hôn nhân", "HON_NHAN", 0, false),
                new DanhMucCanboInfo(8, "Bí danh", "BI_DANH", 0, false),
                new DanhMucCanboInfo(9, "Nơi sinh", "NOI_SINH", 0, false),
                new DanhMucCanboInfo(10, "Quê quán", "QUE_QUAN", 0, false),
                new DanhMucCanboInfo(11, "Địa chỉ liên hệ", "DIA_CHI_LH", 0, false),
                new DanhMucCanboInfo(12, "Hộ khẩu", "HO_KHAU", 0, false),
                new DanhMucCanboInfo(13, "Đi động", "DI_DONG", 0, false),
                new DanhMucCanboInfo(14, "Điện thoại nhà", "DT_NHA", 0, false),
                new DanhMucCanboInfo(15, "Điện cơ quan", "DT_CQUAN", 0, false),
                new DanhMucCanboInfo(16, "Nhóm máu", "NHOM_MAU", 0, false),
                new DanhMucCanboInfo(17, "Chiều cao", "CHIEU_CAO", 0, false),
                new DanhMucCanboInfo(18, "Cân nặng", "CAN_NANG", 0, false),
                new DanhMucCanboInfo(19, "Email", "EMAIL", 0, false),
                new DanhMucCanboInfo(20, "Ngày bổ nhiệm", "NGAY_BN_NGACH", 0, false),
                new DanhMucCanboInfo(21, "Công việc được giao", "CONGVIEC_DUOCGIAO", 0, false),
                new DanhMucCanboInfo(22, "Số CMTND", "SO_CMND", 0, false),
                new DanhMucCanboInfo(23, "Ngày cấp CMTND", "NGAYCAP_CMND", 0, false),
                new DanhMucCanboInfo(24, "Ngày tham gia cách mạng", "NGAY_TGCM", 0, false),
                new DanhMucCanboInfo(25, "Ngày vào quân đội", "NGAYVAO_QDOI", 0, false),
                new DanhMucCanboInfo(26, "Ngày ra quân đội", "NGAYRA_QDOI", 0, false),
                new DanhMucCanboInfo(27, "Ngày vào đoàn", "NGAYVAO_DOAN", 0, false),
                new DanhMucCanboInfo(28, "Nơi kết nạp đoàn", "NOI_KETNAP_DOAN", 0, false),
                new DanhMucCanboInfo(29, "Ngày vào đảng", "NGAYVAO_DANG", 0, false),
                new DanhMucCanboInfo(30, "Nơi kết nạp đảng", "NOI_KETNAP_DANG", 0, false),
                new DanhMucCanboInfo(31, "Số thẻ đảng", "SOTHE_DANG", 0, false),
                new DanhMucCanboInfo(32, "Nghề trước khi tuyển", "NGHE_TRUOC_TUYEN", 0, false),
                new DanhMucCanboInfo(33, "Cơ quan tuyển dụng", "COQUAN_TUYENDUNG", 0, false),
                new DanhMucCanboInfo(34, "Lịch sử bản thân", "LICHSU_THANNHAN", 0, false),
                new DanhMucCanboInfo(35, "Ngày biên chế", "NgayBienChe", 0, false),
                new DanhMucCanboInfo(36, "Bậc ngạch", "BAC_NGACH", 0, false),
                new DanhMucCanboInfo(37, "Số thẻ bảo hiểm", "SOTHE_BHXH", 0, false),
                new DanhMucCanboInfo(38, "Ngày cấp bảo hiểm", "NGAYCAP_BHXH", 0, false),


                new DanhMucCanboInfo(39, "Tôn giáo", "TON_GIAO", 0, false),
                new DanhMucCanboInfo(40, "Trình độ văn hóa", "VAN_HOA", 0, false),
                new DanhMucCanboInfo(41, "Sức khỏe", "SUC_KHOE", 0, false),
                new DanhMucCanboInfo(42, "Ngoại ngữ", "NGOAI_NGU", 0, false),
                new DanhMucCanboInfo(43, "Tin học", "TIN_HOC", 0, false),
                new DanhMucCanboInfo(44, "Trình độ", "TRINH_DO", 0, false),
                new DanhMucCanboInfo(45, "Công việc", "CONG_VIEC", 0, false),
                new DanhMucCanboInfo(46, "Loại hợp đồng", "HOP_DONG", 0, false),
                new DanhMucCanboInfo(47, "Chức vụ", "CHUC_VU", 0, false),
                new DanhMucCanboInfo(48, "Ngạch", "NGACH", 0, false),
                new DanhMucCanboInfo(49, "Thành phần gia đình", "THANH_PHAN_GIA_DINH", 0, false),
                new DanhMucCanboInfo(50, "Thành phần bản thân", "THANH_PHAN_BAN_THAN", 0, false),
                new DanhMucCanboInfo(51, "Trình độ chính trị", "TRINH_DO_CHINH_TRI", 0, false),
                new DanhMucCanboInfo(52, "Trình độ quản lý", "TRINH_DO_QUAN_LY", 0, false),
                new DanhMucCanboInfo(53, "Nơi cấp CMND", "NOI_CAP_CMND", 0, false),
                new DanhMucCanboInfo(54, "Dân tộc", "DAN_TOC", 0, false), //c
                new DanhMucCanboInfo(55, "Chức vụ quân đội", "CHUC_VU_QUAN_DOI", 0, false),
                new DanhMucCanboInfo(56, "Cấp bậc quân đội", "CAP_BAC_QUAN_DOI", 0, false),
                new DanhMucCanboInfo(57, "Chức vụ đoàn", "CHUC_VU_DOAN", 0, false),
                new DanhMucCanboInfo(58, "Chức vụ đảng", "CHUC_VU_DANG", 0, false),
                new DanhMucCanboInfo(59, "Đơn vị", "DON_VI", 0, false),
                //   new DanhMucCanboInfo(60,"Đơn vị quản lý","DON_VI_QUAN_LY",0,false),
            };
            return list;
        }

        public List<DanhMucCanboInfo> DieuKienLocCanBo()
        {
            var list = new List<DanhMucCanboInfo>
            {
                new DanhMucCanboInfo(1, "Tên cán bộ", "cbxChonCanBo.show()", 0, false),
                new DanhMucCanboInfo(2, "Giới tính", "cbGender.show()", 0, false),
                new DanhMucCanboInfo(3, "Trạng thái làm việc", "cbWorkingStatus.show()", 0, false),
                new DanhMucCanboInfo(4, "Phòng ban", "cbChonPhongBan.show()", 0, false),
                new DanhMucCanboInfo(5, "Độ tuổi", "CompositeField1.show()", 0, false),
                new DanhMucCanboInfo(6, "Khoảng ngày sinh", "dfStartDate.show();dfEndDate.show()", 0, false),
                new DanhMucCanboInfo(7, "Thâm niên", "CompositeField7.show()", 0, false),
                //  new DanhMucCanboInfo(8,"Quê quán","cbx_NoiSinhXa.show();cbx_NoiSinhHuyen.show();cbx_NoiSinhTinh.show()",0,false),
            };
            return list;
        }
    }
}