<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Report.Default" Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../System/Role/Resource/jqx.base.css" />
    <link type="text/css" rel="stylesheet" href="Resource/Style.css" />
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/Resource/js/RenderJS.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/gettheme.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/jqxcore.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/jqxbuttons.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/jqxpanel.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/jqxtree.js"></script>
    <script type="text/javascript" src="../System/Role/Resource/jqxcheckbox.js"></script>
    <script type="text/javascript" src="/Resource/js/Extcommon.js"></script>
    <script type="text/javascript" src="/Modules/Report/Resource/Js.js"></script>
    <asp:Literal runat="server" ID="ltrShowSettingDialog"></asp:Literal>
    <style type="text/css">
        .x-html-editor-wrap {
            border: none !important;
        }

        #grpSelectedReportFilter {
            border-left: 1px solid #99bbe8;
        }

        #grpReportFilter {
            border-right: 1px solid #99bbe8;
        }

        #ctn #ext-gen328 {
            height: auto;
        }
    </style>
    <script type="text/javascript">

        function a() {
            return 1;
        }

        function resetReportFilterForm() {
            if (hdfReportID.getValue() == hdfFilterID.getValue())
                return;

            cbChooseFilterTable.reset();
            hdfCurrentReportID.reset();
            storeFilterValue.clearData();
            storeFilterTable.clearData();
            storeFilterValue.reload();
            storeSelectedReportFilter.clearData();
            storeSelectedReportFilter.reload();
            txtSeniorityMin.enable(); txtSeniorityMin.show(); txtSeniorityMin.reset();
            txtSeniorityMax.enable(); txtSeniorityMax.show(); txtSeniorityMax.reset();
            cbWorkingStatus.enable(); cbWorkingStatus.show(); cbWorkingStatus.reset();
            txtAgeMin.enable(); txtAgeMin.show(); txtAgeMin.reset();
            txtAgeMax.enable(); txtAgeMax.show(); txtAgeMax.reset();
            dfStartDate.enable(); dfStartDate.show(); dfStartDate.reset();
            dfEndDate.enable(); dfEndDate.show(); dfEndDate.reset();
            cbMonth.enable(); cbMonth.show(); cbMonth.reset();
            cbGender.enable(); cbGender.show(); cbGender.reset();
            cbCanBo.enable(); cbCanBo.show(); cbCanBo.reset();
            cb_HopDong.hide(); cb_HopDong.reset();
            cbChonPhongBan.enable(); cbChonPhongBan.show(); cbChonPhongBan.reset();
            hdfSelectedDepartmentIdList.reset(); spfyear.enable(); spfyear.show();
            hdfChonCanBo.reset(); cbxChonCanBo.reset(); cbxChonCanBo.enable(); cbxChonCanBo.show(); htmlNote.reset();
            txttieudechuky1.reset();
            txttieudechuky2.reset();
            txttieudechuky3.reset();
            txtnguoiky1.reset();
            txtnguoiky2.reset();
            txtnguoiky3.reset(); hdfFilterID.setValue(hdfReportID.getValue());
        }
        //lấy giá trị trong store dựa vào ID property
        function getWhereField(id) {
            for (var i = 0; i < storeFilterTable.getCount(); i++) {
                if (storeFilterTable.getAt(i).data.ID == id) {
                    return storeFilterTable.getAt(i).data.WhereField;
                }
            }
            return "";
        }

        function addSelectedValues() {
            if (grpReportFilter.getSelectionModel().getCount() == 0) {
                Ext.Msg.alert("Thông báo", "Bạn chưa chọn điều kiện nào cả!");
                return false;
            }
            else {
                var s = grpReportFilter.getSelectionModel().getSelections();
                for (var i = 0, r; r = s[i]; i++) {
                    if (!checkExits(r.data.ID, cbChooseFilterTable.getText())) {
                        grpSelectedReportFilter.insertRecord(0, {
                            ID: r.data.ID,
                            DisplayText: cbChooseFilterTable.getText(),
                            Value: r.data.Value,
                            WhereField: getWhereField(cbChooseFilterTable.getValue())
                        });
                    }
                }
                storeSelectedReportFilter.commitChanges();
            }
        }

        function addSelectTruong() {
            if (gpShow.getSelectionModel().getCount() == 0) {
                Ext.Msg.alert("Thông báo", "Bạn chưa chọn các trường hiển thị!");
                return false;
            }
            else {
                wdBaoCaoTongHop.hide();
                wdReportFilter.show();
                wdReportFilter.hide();
                wdReportFilter.show();
            }
        }

        var checkExits = function (ID, DisplayText) {
            var count = storeSelectedReportFilter.getCount();
            for (var j = 0; j < count; j++) {
                try {
                    grpSelectedReportFilter.getSelectionModel().selectRow(j);
                    var s = grpSelectedReportFilter.getSelectionModel().getSelections();
                    for (var i = 0, r; r = s[i]; i++) {
                        if (r.data.DisplayText == DisplayText && r.data.ID == ID) {
                            return true;
                        }
                    }
                } catch (e) {
                    alert(e);
                }
            }
            return false;
        }

        //gen code sql
        var getSelectedFilter = function () {
            var rs = "";
            var count = storeSelectedReportFilter.getCount();
            for (var j = 0; j < count; j++) {
                try {
                    //grpSelectedReportFilter.getSelectionModel().selectRow(j);
                    var s = grpSelectedReportFilter.getSelectionModel().getSelections();

                    for (var i = 0, r; r = s[i]; i++) {
                        rs += r.data.WhereField + '=' + r.data.ID + ';';
                    }
                } catch (e) {
                    alert(e);
                }
            }

            return rs;
            //alert(rs);
        }

        var removeFilter = function () {
            if (grpSelectedReportFilter.getSelectionModel().getCount() == 0) {
                Ext.Msg.alert("Thông báo", "Bạn chưa chọn điều kiện nào cả!");
                return false;
            }
            var s = grpSelectedReportFilter.getSelectionModel().getSelections();
            for (var i = 0, r; r = s[i]; i++) {
                storeSelectedReportFilter.remove(r);
            }
        }

        var afterEdit = function (e) {
            /*
            Properties of 'e' include:
            e.grid - This grid
            e.record - The record being edited
            e.field - The field name being edited
            e.value - The value being set
            e.originalValue - The original value for the field, before the edit.
            e.row - The grid row index
            e.column - The grid column index
            */

            // Call DirectMethod
            e.record.data.ID =
                CompanyX.AfterEdit(e.record.data.ID, e.field, e.originalValue, e.value, e.record.data);
        };
        $(document).ready(function () {
            $('div#wdReportFilter.x-window .x-column').hasClass('x-hide-display')
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM" />
            <!-- hiddenn field -->
            <ext:Hidden runat="server" ID="hdfTitle" />
            <ext:Hidden runat="server" ID="hdfReportID" />
            <ext:Hidden runat="server" ID="hdfCurrentReportID" />
            <ext:Hidden runat="server" ID="hdfSelectedDepartmentIdList" />
            <ext:Hidden runat="server" ID="hdfFilterID" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfTypeTimeSheet"/>
            <!-- preview report -->
            <div id="divReportPreview">
                <h1>Mẫu báo cáo</h1>
                <a href="#" id="aClose" onclick="document.getElementById('divReportPreview').style.display='none';"></a>
                <img id="imgReportPreview" src="" alt="Report preview" width="600" />
            </div>
            <!-- context menu for treeview -->
            <ext:Menu ID="RowContextMenu" runat="server">
                <Items>
                    <ext:MenuItem runat="server" Text="Xem báo cáo" Icon="Printer">
                        <Listeners>
                            <Click Handler="ShowSettingDialog();" />
                        </Listeners>
                    </ext:MenuItem>
                </Items>
            </ext:Menu>
            <!-- fillerter condition -->
            <ext:Store runat="server" ID="stDieuKienLoc" AutoLoad="true">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/FilterConditionHandler.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={100}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="len" Value="5" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="ID" Root="plants" TotalProperty="total">
                        <Fields>
                            <ext:RecordField Name="ID" />
                            <ext:RecordField Name="DescriptionTableName" />
                            <ext:RecordField Name="SQLTableName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="stDieuKienLoc2" runat="server">
                <Reader>
                    <ext:JsonReader IDProperty="ID">
                        <Fields>
                            <ext:RecordField Name="ID" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="stShow" runat="server">
                <Reader>
                    <ext:JsonReader IDProperty="ID">
                        <Fields>
                            <ext:RecordField Name="ID" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <!-- main view -->
            <ext:Viewport runat="server" ID="vReport">
                <Items>
                    <ext:BorderLayout runat="server" ID="brLayout">
                        <Center Split="true">
                            <ext:TabPanel ID="pnTabReport" Border="false" EnableTabScroll="true" runat="server">
                                <Plugins>
                                    <ext:TabCloseMenu CloseOtherTabsText="Đóng Tab khác" CloseAllTabsText="Đóng tất cả các Tab"
                                        CloseTabText="Đóng Tab">
                                    </ext:TabCloseMenu>
                                    <ext:TabScrollerMenu ID="TabScrollerMenu1" runat="server">
                                    </ext:TabScrollerMenu>
                                </Plugins>
                            </ext:TabPanel>
                        </Center>
                        <West Collapsible="true" Split="true">
                            <ext:TreePanel runat="server"
                                Border="false"
                                ID="treeReportList"
                                CtCls="west-report"
                                Width="300"
                                Icon="Report"
                                ContextMenuID="RowContextMenu"
                                Title="Mẫu báo cáo"
                                RootVisible="false"
                                AutoScroll="true">
                                <TopBar>
                                    <ext:Toolbar ID="Toolbar1" runat="server">
                                        <Items>
                                            <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="Tìm kiếm" />
                                            <ext:ToolbarSpacer />
                                            <ext:TriggerField ID="TriggerField1" runat="server" EnableKeyEvents="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyUp Fn="filterTree" Buffer="250" />
                                                    <TriggerClick Handler="clearFilter();" />
                                                </Listeners>
                                                <ToolTips>
                                                    <ext:ToolTip runat="server" Title="Hướng dẫn" Html="Nhập một từ khóa trong tên của báo cáo" ID="tl" />
                                                </ToolTips>
                                            </ext:TriggerField>
                                            <ext:ToolbarSeparator runat="server" ID="ts" />
                                            <ext:Button ID="Button1" runat="server" Text="Mở rộng">
                                                <Listeners>
                                                    <Click Handler="#{TreePanel1}.expandAll();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server" ID="ToolbarSeparator1" />
                                            <ext:Button ID="Button2" runat="server" Text="Thu nhỏ">
                                                <Listeners>
                                                    <Click Handler="#{TreePanel1}.collapseAll();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Root>
                                </Root>
                            </ext:TreePanel>
                        </West>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <!-- window report fillter / show when double click on report list -->
            <ext:Window runat="server"
                ID="wdReportFilter"
                Hidden="true"
                Modal="true"
                AutoHeight="true"
                ConstrainHeader="true"
                Width="650"
                Icon="Printer"
                Padding="6"
                Constrain="true"
                Layout="FormLayout"
                Title="Điều kiện báo cáo"
                Resizable="false">
                <Items>
                    <ext:TriggerField runat="server" AnchorHorizontal="98%" FieldLabel="Tiêu đề báo cáo" ID="txtReportTitle">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="Xóa trắng" HideTrigger="false" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="txtReportTitle.reset();" />
                        </Listeners>
                    </ext:TriggerField>
                    <ext:Container runat="server" ID="ctn5" Layout="ColumnLayout" AnchorHorizontal="100%" Height="157">
                        <Items>
                            <ext:Container runat="server" ID="ctnChooseEmployee" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfChonCanBo" />
                                    <ext:ComboBox ID="cbxChonCanBo" ValueField="Id" DisplayField="FullName" FieldLabel="Tên cán bộ"
                                        PageSize="10" ListWidth="180" ItemSelector="div.search-item" MinChars="1" EmptyText="nhập tên để tìm kiếm"
                                        LoadingText="Đang tải dữ liệu..." AnchorHorizontal="99%" runat="server">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Store>
                                            <ext:Store ID="cbxChonCanBo_Store" runat="server" AutoLoad="false">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerSearchUser.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="FullName" />
                                                            <ext:RecordField Name="EmployeeCode" />
                                                            <ext:RecordField Name="BirthDate" />
                                                            <ext:RecordField Name="DepartmentName" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Template ID="Template4" runat="server">
                                            <Html>
                                                <tpl for=".">
						                        <div class="search-item">
							                        <h3>{FullName}</h3>
                                                    {EmployeeCode} <br />
                                                    <tpl if="BirthDate &gt; ''">{BirthDate:date("d/m/Y")}</tpl><br />
							                        {DepartmentName}
						                        </div>
					                        </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show(); hdfChonCanBo.setValue(cbxChonCanBo.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfChonCanBo.reset(); }" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnGender" ColumnWidth="0.33" AutoHeight="true" Layout="FormLayout">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cbGender" Editable="false" FieldLabel="Giới tính" AnchorHorizontal="99%">
                                        <Items>
                                            <ext:ListItem Text="Tất cả" Value="" />
                                            <ext:ListItem Text="Nam" Value="M" />
                                            <ext:ListItem Text="Nữ" Value="F" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEmployeeType" ColumnWidth="0.33" AutoHeight="true" Layout="FormLayout">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cbCanBo" Editable="false" FieldLabel="Cán bộ" AnchorHorizontal="99%">
                                        <Items>
                                            <ext:ListItem Text="Tất cả" Value="" />
                                            <ext:ListItem Text="Công chức" Value="CC" />
                                            <ext:ListItem Text="Viên chức" Value="VC" />
                                            <ext:ListItem Text="Cán bộ" Value="CB" />
                                            <ext:ListItem Text="Lao động hợp đồng" Value="LDHD" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="ctnWorkingStatus" runat="server" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:MultiCombo ID="cbWorkingStatus" runat="server" AnchorHorizontal="99%" FieldLabel="Trạng thái" AutoHeight="true"
                                        DisplayField="Name" ValueField="Id">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Store>
                                            <ext:Store runat="server" ID="cbWorkingStatus_store" AutoLoad="false">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="objname" Value="cat_WorkStatus" Mode="Value" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Name" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();" />
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:MultiCombo>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Container1" ColumnWidth="0.66" Layout="FormLayout" AutoHeight="true">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfTimeSheetReport"></ext:Hidden>
                                    <ext:ComboBox runat="server" ID="cbxTimeSheetReport" FieldLabel="Chọn bảng chấm công"
                                        DisplayField="Title" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template ID="Template16" runat="server">
                                            <Html>
                                                <tpl for=".">
						                        <div class="list-item"> 
							                        {Title}
						                        </div>
					                        </tpl>
                                            </Html>
                                        </Template>
                                        <Store>
                                            <ext:Store ID="storeSelectTimeSheetReport" AutoSave="true" runat="server">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="TimeSheetReportList" />
                                                    <ext:Parameter Name="typeTimeSheet" Value="hdfTypeTimeSheet.getValue()" Mode="Raw" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="CreatedDate" />
                                                            <ext:RecordField Name="Title" />
                                                            <ext:RecordField Name="Month" />
                                                            <ext:RecordField Name="Year" />
                                                            <ext:RecordField Name="WorkInMonth" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();hdfTimeSheetReport.setValue(cbxTimeSheetReport.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfTimeSheetReport.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnDepartment" ColumnWidth="0.66" Layout="FormLayout" AutoHeight="true">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfMaDonvi" />
                                    <ext:TriggerField runat="server" ID="cbChonPhongBan" CtCls="requiredData" FieldLabel="Chọn đơn vị" AnchorHorizontal="99%">
                                        <Listeners>
                                            <TriggerClick Handler="wdChooseDepartment.show();" />
                                            <Focus Handler="wdChooseDepartment.show();" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnAge" ColumnWidth="0.33" Layout="FormLayout" AutoHeight="true">
                                <Items>
                                    <ext:CompositeField ID="CompositeField1" runat="server" AnchorHorizontal="99%" FieldLabel="Độ tuổi">
                                        <Items>
                                            <ext:TextField runat="server" EmptyText="số" ID="txtAgeMin" Width="41" MaxLength="3"
                                                MaskRe="[0-9]" />
                                            <ext:DisplayField ID="DisplayField2" runat="server" Text="-" />
                                            <ext:TextField runat="server" EmptyText="số" ID="txtAgeMax" Width="41" MaxLength="3"
                                                MaskRe="[0-9]" />
                                        </Items>
                                        <ToolTips>
                                            <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="nhập số tuổi,VD:18">
                                            </ext:ToolTip>
                                        </ToolTips>
                                    </ext:CompositeField>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnMonth" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:ComboBox ID="cbMonth" runat="server" Editable="false" AnchorHorizontal="99%" FieldLabel="Tháng" AutoHeight="true">
                                        <Items>
                                            <ext:ListItem Text="Cả năm" Value="0" />
                                            <ext:ListItem Text="Tháng 1" Value="1" />
                                            <ext:ListItem Text="Tháng 2" Value="2" />
                                            <ext:ListItem Text="Tháng 3" Value="3" />
                                            <ext:ListItem Text="Tháng 4" Value="4" />
                                            <ext:ListItem Text="Tháng 5" Value="5" />
                                            <ext:ListItem Text="Tháng 6" Value="6" />
                                            <ext:ListItem Text="Tháng 7" Value="7" />
                                            <ext:ListItem Text="Tháng 8" Value="8" />
                                            <ext:ListItem Text="Tháng 9" Value="9" />
                                            <ext:ListItem Text="Tháng 10" Value="10" />
                                            <ext:ListItem Text="Tháng 11" Value="11" />
                                            <ext:ListItem Text="Tháng 12" Value="12" />
                                            <ext:ListItem Text="Quý 1" Value="13" />
                                            <ext:ListItem Text="Quý 2" Value="14" />
                                            <ext:ListItem Text="Quý 3" Value="15" />
                                            <ext:ListItem Text="Quý 4" Value="16" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnStartDate" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:DateField ID="dfStartDate" runat="server" FieldLabel="Từ ngày" AnchorHorizontal="99%" Format="d/M/yyyy" />
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndDate" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:DateField ID="dfEndDate" runat="server" FieldLabel="Đến ngày" AnchorHorizontal="99%" Format="d/M/yyyy" />
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnYear" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:SpinnerField runat="server" FieldLabel="Năm" ID="spfyear" AnchorHorizontal="99%" />
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnSeniority" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:CompositeField ID="CompositeField7" runat="server" AnchorHorizontal="99%" FieldLabel="Thâm niên" ColumnWidth="0.33">
                                        <Items>
                                            <ext:TextField runat="server" EmptyText="Tháng" ID="txtSeniorityMin" Width="41" MaxLength="4"
                                                MaskRe="[0-9]" />
                                            <ext:DisplayField ID="DisplayField11" runat="server" Text="-" />
                                            <ext:TextField runat="server" EmptyText="Tháng" ID="txtSeniorityMax" Width="41" MaxLength="4"
                                                MaskRe="[0-9]" />
                                        </Items>
                                        <ToolTips>
                                            <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Thâm niên được tính theo tháng">
                                            </ext:ToolTip>
                                        </ToolTips>
                                    </ext:CompositeField>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnReportDate" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:DateField runat="server" AnchorHorizontal="99%" FieldLabel="Ngày báo cáo" CtCls="requiredData" ID="dfReportDate" />
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnBirthPlaceProvinceId" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfBirthPlaceProvinceId" />
                                    <ext:ComboBox runat="server" ID="cbxBirthPlaceProvince" FieldLabel="Tỉnh/Thành phố" DisplayField="Name"
                                        MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="99%"
                                        Width="260" LabelAlign="Right" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                        PageSize="10">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template ID="Template1" runat="server">
                                            <Html>
                                                <tpl for=".">
						                    <div class="list-item"> 
							                    <p><b>{Name}<//b><//p>
						                    </div>
					                    </tpl>
                                            </Html>
                                        </Template>
                                        <Store>
                                            <ext:Store ID="storeBirthPlaceProvince" runat="server" AutoLoad="false">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerLocation.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="group" Value="Tinh,ThanhPhoTW" Mode="Value" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Name" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();hdfBirthPlaceProvinceId.setValue(cbxBirthPlaceProvince.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };hdfBirthPlaceProvinceId.reset();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnBirthPlaceDistrictId" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfBirthPlaceDistrictId" />
                                    <ext:ComboBox runat="server" ID="cbxBirthPlaceDistrict" LabelAlign="Right" FieldLabel="Quận/Huyện" DisplayField="Name"
                                        MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="99%"
                                        Width="150" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                        PageSize="10">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template ID="Template2" runat="server">
                                            <Html>
                                                <tpl for=".">
						                                <div class="list-item"> 
							                                <p><b>{Name}</b></p> 
						                                </div>
					                                </tpl>
                                            </Html>
                                        </Template>
                                        <Store>
                                            <ext:Store runat="server" ID="storeBirthPlaceDistrict" AutoLoad="false">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerLocation.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="parentid" Value="#{hdfBirthPlaceProvinceId}.getValue()" Mode="Raw" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Name" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();hdfBirthPlaceDistrictId.setValue(cbxBirthPlaceDistrict.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };hdfBirthPlaceDistrictId.reset();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnBirthPlaceWardId" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfBirthPlaceWardId" />
                                    <ext:ComboBox runat="server" ID="cbxBirthPlaceWard" FieldLabel="Phường/Xã" LabelAlign="Right"
                                        DisplayField="Name" MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="99%"
                                        Editable="true" ItemSelector="div.list-item" Width="250" ListWidth="260" AutoHeight="true"
                                        LoadingText="Đang tải dữ liệu" PageSize="10">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template ID="Template3" runat="server">
                                            <Html>
                                                <tpl for=".">
						                                <div class="list-item"> 
							                                <p><b>{Name}</b></p> 
						                                </div>
					                                </tpl>
                                            </Html>
                                        </Template>
                                        <Store>
                                            <ext:Store runat="server" ID="storeBirthPlaceWard" AutoLoad="false">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerLocation.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="parentid" Value="#{hdfBirthPlaceDistrictId}.getValue()" Mode="Raw" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Name" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();hdfBirthPlaceWardId.setValue(cbxBirthPlaceWard.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfBirthPlaceWardId.reset(); }" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnContract" Layout="FormLayout" ColumnWidth="0.33" AutoHeight="true">
                                <Items>
                                    <ext:ComboBox runat="server" LabelWidth="70" ID="cb_HopDong" Editable="false"
                                        AnchorHorizontal="99%" FieldLabel="Hiệu lực">
                                        <Items>
                                            <ext:ListItem Text="Tất cả" Value="" />
                                            <ext:ListItem Text="Còn hiệu lực" Value="C" />
                                            <ext:ListItem Text="Hết hiệu lực" Value="H" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:TabPanel runat="server" ID="tpnlFilerCondition" AnchorHorizontal="100%" Border="true" Height="250">
                        <Items>
                            <ext:Panel runat="server" ID="pnlFilerCondition" Title="Các điều kiện lọc" Border="false" Layout="FormLayout" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container ID="ctnFilerCondition" runat="server" Height="220" AnchorHorizontal="100%" Layout="ColumnLayout">
                                        <Items>
                                            <ext:Container ID="ctnFilerConditionInner" runat="server" Height="220" Layout="BorderLayout" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:GridPanel Border="false" AnchorHorizontal="98%" ID="grpReportFilter" runat="server" StripeRows="true" AutoExpandColumn="Value" Title="" Region="Center" TrackMouseOver="false">
                                                        <Store>
                                                            <ext:Store runat="server" ID="storeFilterValue" OnRefreshData="storeFilterValue_OnRefreshData" AutoLoad="false">
                                                                <Reader>
                                                                    <ext:JsonReader IDProperty="ID">
                                                                        <Fields>
                                                                            <ext:RecordField Name="ID" />
                                                                            <ext:RecordField Name="Value" />
                                                                        </Fields>
                                                                    </ext:JsonReader>
                                                                </Reader>
                                                            </ext:Store>
                                                        </Store>
                                                        <TopBar>
                                                            <ext:Toolbar ID="toolbarSelectCondition" runat="server">
                                                                <Items>
                                                                    <ext:ComboBox runat="server" ValueField="ID" DisplayField="DescriptionTableName" ID="cbChooseFilterTable" LabelWidth="70" EmptyText="Chọn điều kiện" Editable="false">
                                                                        <Store>
                                                                            <ext:Store runat="server" OnRefreshData="storeFilterTable_OnRefreshData" AutoLoad="false" ID="storeFilterTable">
                                                                                <Reader>
                                                                                    <ext:JsonReader>
                                                                                        <Fields>
                                                                                            <ext:RecordField Name="ID" />
                                                                                            <ext:RecordField Name="DescriptionTableName" />
                                                                                            <ext:RecordField Name="WhereField" />
                                                                                        </Fields>
                                                                                    </ext:JsonReader>
                                                                                </Reader>
                                                                            </ext:Store>
                                                                        </Store>
                                                                        <Listeners>
                                                                            <Expand Handler="if(hdfCurrentReportID.getValue()!=hdfReportID.getValue()){
                                                                               hdfCurrentReportID.setValue(hdfReportID.getValue());storeFilterTable.reload();}" />
                                                                            <Select Handler="storeFilterValue.reload();" />
                                                                        </Listeners>
                                                                    </ext:ComboBox>
                                                                    <ext:Button runat="server" ID="btnAccept" Text="Đồng ý chọn" Icon="Accept">
                                                                        <Listeners>
                                                                            <Click Handler="addSelectedValues();" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                        <ColumnModel>
                                                            <Columns>
                                                                <%--<ext:Column Header="Mã" Width="70" DataIndex="ID">
                                                                </ext:Column>--%>
                                                                <ext:Column Header="Giá trị" ColumnID="Value" DataIndex="Value">
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server">
                                                            </ext:CheckboxSelectionModel>
                                                        </SelectionModel>
                                                    </ext:GridPanel>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container4" runat="server" Layout="FormLayout" Width="6"></ext:Container>
                                            <ext:Container ID="Container5" runat="server" Layout="BorderLayout" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:GridPanel Border="false" AnchorHorizontal="98%" ID="grpSelectedReportFilter"
                                                        runat="server" StripeRows="true" AutoExpandColumn="Value" Title="Điều kiện đã chọn"
                                                        Header="false" Icon="Tick" Region="Center" TrackMouseOver="false">
                                                        <TopBar>
                                                            <ext:Toolbar ID="Toolbar3" runat="server">
                                                                <Items>
                                                                    <ext:ToolbarSpacer runat="server" Width="5" />
                                                                    <ext:Label runat="server" Icon="Tick" />
                                                                    <ext:ToolbarSpacer runat="server" Width="3" />
                                                                    <ext:DisplayField runat="server" ID="dflTitle" Text="<h1 style='color:#15428B'>Điều kiện đã chọn</h1>" />
                                                                    <ext:ToolbarFill runat="server" ID="tbf" />
                                                                    <ext:Button ID="btnDeleteReportFilter" runat="server" Text="Xóa" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="removeFilter();" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                        <Store>
                                                            <ext:Store ID="storeSelectedReportFilter" runat="server" AutoLoad="false">
                                                                <Reader>
                                                                    <ext:JsonReader>
                                                                        <Fields>
                                                                            <ext:RecordField Name="ID" />
                                                                            <ext:RecordField Name="DisplayText" />
                                                                            <ext:RecordField Name="WhereField" />
                                                                            <ext:RecordField Name="Value" />
                                                                        </Fields>
                                                                    </ext:JsonReader>
                                                                </Reader>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel>
                                                            <Columns>
                                                                <ext:Column Header="Điều kiện" Width="120" DataIndex="DisplayText">
                                                                </ext:Column>
                                                                <ext:Column Header="Giá trị" ColumnID="Value" DataIndex="Value">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtValue" runat="server">
                                                                            <Listeners>
                                                                                <Focus Handler="txtValue.blur();" />
                                                                            </Listeners>
                                                                        </ext:TextField>
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:CheckboxSelectionModel ID="CheckboxSelectionModel2" runat="server">
                                                                <Listeners>
                                                                    <RowSelect Handler="btnDeleteReportFilter.enable();" />
                                                                </Listeners>
                                                            </ext:CheckboxSelectionModel>
                                                        </SelectionModel>
                                                        <Listeners>
                                                            <AfterEdit Fn="afterEdit" />
                                                            <BeforeShow Handler="resetReportFilterForm();wdReportFilter.setTitle(hdfTitle.getValue());if(hdfCurrentReportID.getValue()!=hdfReportID.getValue()){
                                                                            hdfCurrentReportID.setValue(hdfReportID.getValue());}" />
                                                            <%-- storeSelectedReportFilter.reload();--%>
                                                        </Listeners>
                                                    </ext:GridPanel>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" ID="Panel1" Title="Ghi chú" Border="false" Layout="FitLayout"
                                Hidden="true" AnchorHorizontal="100%">
                                <Items>
                                    <ext:HtmlEditor runat="server" ID="htmlNote">
                                    </ext:HtmlEditor>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:TabPanel>
                    <ext:FieldSet runat="server" ID="fsnhomchuky" Title="Cấu hình chữ ký ">
                        <Items>
                            <ext:Container runat="server" ID="columnlayout" Layout="FormLayout" AnchorHorizontal="99%">
                                <Items>
                                    <ext:Container runat="server" ID="column1" Layout="ColumnLayout" LabelAlign="Top" Height="25">
                                        <Items>
                                            <ext:Container ID="Container6" runat="server" Layout="FormLayout" ColumnWidth="0.43">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txttieudechuky1" FieldLabel="Tiêu đề 1" LabelWidth="65"
                                                        AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth="0.13">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txttieudechuky2" AnchorHorizontal="50%" FieldLabel="Tiêu đề 2"
                                                        LabelWidth="65" Hidden="true">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container16" runat="server" Layout="FormLayout" ColumnWidth="0.43">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txttieudechuky3" AnchorHorizontal="100%" FieldLabel="Tiêu đề 3"
                                                        LabelWidth="65">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Container8" Layout="ColumnLayout" LabelAlign="Top"
                                        Height="25">
                                        <Items>
                                            <ext:Container ID="Container17" runat="server" Layout="FormLayout" ColumnWidth="0.43">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtnguoiky1" FieldLabel="Người ký 1" LabelWidth="65"
                                                        AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container18" runat="server" Layout="FormLayout" ColumnWidth="0.13">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtnguoiky2" AnchorHorizontal="100%" FieldLabel="Người ký 2"
                                                        LabelWidth="65" Hidden="true">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container19" runat="server" Layout="FormLayout" ColumnWidth="0.43">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtnguoiky3" AnchorHorizontal="100%" FieldLabel="Người ký 3"
                                                        LabelWidth="65">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Listeners>
                    <BeforeShow Handler="resetReportFilterForm();wdReportFilter.setTitle(hdfTitle.getValue());if(hdfCurrentReportID.getValue()!=hdfReportID.getValue()){
                                                                            hdfCurrentReportID.setValue(hdfReportID.getValue());storeFilterTable.reload();}" />
                    <Hide Handler="$('#jqxTree').jqxTree('uncheckAll');" />
                </Listeners>
                <Buttons>
                    <ext:Button runat="server" Text="Đồng ý" Icon="Accept" ID="btnShowReport">
                        <DirectEvents>
                            <Click OnEvent="btnShowReport_Click" After="#{wdReportFilter}.hide();">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát.." />
                                <ExtraParams>
                                    <ext:Parameter Name="SQL" Value="getSelectedFilter()" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="btnClose">
                        <Listeners>
                            <Click Handler="#{wdReportFilter}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <!-- window -->
            <ext:Window runat="server" ID="wdChooseDepartment" Hidden="true" Icon="House" Constrain="true" Modal="true" Title="Chọn bộ phận 111" Height="450" Width="400">
                <Content>
                    <div id='jqxWidget'>
                        <div style='float: left;'>
                            <div id='jqxTree' style='float: left;'>
                                <ul>
                                    <asp:Literal runat="server" ID="ltrDepartment" />
                                </ul>
                            </div>
                        </div>
                    </div>
                </Content>
                <Buttons>
                    <ext:Button ID="Button3" runat="server" Text="Đồng ý" Icon="Accept">
                        <Listeners>
                            <Click Handler="getSelectedNode();wdChooseDepartment.hide();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="UnCheckAllFunction();wdChooseDepartment.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" ID="wdBaoCaoTongHop" Width="1000" Height="700" Icon="Printer" Modal="true" Collapsible="true" Maximizable="true" Hidden="true"
                AutoHeight="true" ConstrainHeader="true" Padding="6" Constrain="true" Resizable="false">
                <Items>
                    <ext:FormPanel runat="server" ID="Panel_Edit" AutoWidth="true" Padding="10">
                        <Items>
                            <ext:TriggerField runat="server" AnchorHorizontal="100%" FieldLabel="Tiêu đề báo cáo"
                                ID="txtTieuDe">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="Xóa trắng" HideTrigger="false" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="txtTieuDe.reset();" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:Label runat="server" ID="lbscript"></ext:Label>

                        </Items>
                    </ext:FormPanel>
                    <ext:Container runat="server" ID="ctn" Layout="ColumnLayout" Height="300" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Container13" Layout="FormLayout" AutoHeight="true" ColumnWidth="0.33" LabelAlign="Top">
                                <Items>
                                    <ext:FieldSet ID="fs1" runat="server" AutoHeight="true" Title="Các trường hiển thị">
                                        <Items>
                                            <ext:GridPanel
                                                ID="gpShow"
                                                StoreID="stShow"
                                                runat="server"
                                                Title="Trường hiển thị"
                                                Width="298"
                                                StripeRows="true"
                                                Collapsible="true"
                                                Height="270">

                                                <SelectionModel>
                                                    <ext:CheckboxSelectionModel runat="server" />
                                                </SelectionModel>

                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:Column Header="Mã" DataIndex="ID" />
                                                        <ext:Column Header="Tên trường" Width="150" DataIndex="Name" />
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Container14" Layout="FormLayout" AutoHeight="true" ColumnWidth="0.63" LabelAlign="Top">
                                <Items>
                                    <ext:FieldSet ID="fs2" runat="server" AutoHeight="true" Title="Các điều kiện lọc">
                                        <Items>
                                            <ext:Container runat="server" ID="Container21" Layout="ColumnLayout" Height="270" AnchorHorizontal="100%">
                                                <Items>

                                                    <ext:Container runat="server" ID="Container15" Layout="FormLayout" AutoHeight="true" ColumnWidth="0.5" LabelAlign="Top">
                                                        <Items>
                                                            <ext:GridPanel
                                                                ID="gpDieuKienLoc"
                                                                StoreID="stDieuKienLoc"
                                                                runat="server"
                                                                Title="Điều kiện lọc 1"
                                                                Width="298"
                                                                StripeRows="true"
                                                                Collapsible="true"
                                                                Height="270">
                                                                <SelectionModel>
                                                                    <ext:CheckboxSelectionModel runat="server" />
                                                                </SelectionModel>
                                                                <ColumnModel runat="server">
                                                                    <Columns>
                                                                        <ext:Column Header="Mã" DataIndex="ID" />
                                                                        <ext:Column Header="Tên trường" Width="150" DataIndex="DescriptionTableName" />
                                                                        <ext:Column Header="Tên trường" Width="300" Hidden="true" DataIndex="SQLTableName" />
                                                                    </Columns>
                                                                </ColumnModel>
                                                            </ext:GridPanel>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container20" Layout="FormLayout" AutoHeight="true" ColumnWidth="0.5" LabelAlign="Top">
                                                        <Items>
                                                            <ext:GridPanel
                                                                ID="gpDieuKienLoc2"
                                                                StoreID="stDieuKienLoc2"
                                                                runat="server"
                                                                Title="Điều kiện lọc 2"
                                                                Width="298"
                                                                StripeRows="true"
                                                                Collapsible="true"
                                                                Height="270">
                                                                <SelectionModel>
                                                                    <ext:CheckboxSelectionModel runat="server" />
                                                                </SelectionModel>
                                                                <ColumnModel runat="server">
                                                                    <Columns>
                                                                        <ext:Column Header="Mã" DataIndex="ID" />
                                                                        <ext:Column Header="Tên trường" Width="150" DataIndex="Name" />
                                                                        <ext:Column Header="Tên trường" Width="300" Hidden="true" DataIndex="Description" />
                                                                    </Columns>
                                                                </ColumnModel>
                                                            </ext:GridPanel>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>

                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Text="Đồng ý" Icon="Accept" ID="btnChon">
                        <Listeners>
                            <Click Handler="addSelectTruong();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnChon_Click">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát.." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="Button6">
                        <Listeners>
                            <Click Handler="#{wdBaoCaoTongHop}.hide();resetReportFilterForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
