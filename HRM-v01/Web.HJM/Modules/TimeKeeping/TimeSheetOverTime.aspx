<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetOverTime.aspx.cs" Inherits="Web.HJM.Modules.TimeKeeping.TimeSheetOverTime" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1"
    TagName="ucChooseEmployee" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        div#gridTimeAdjust .x-grid3-cell-inner, .x-grid3-hd-inner {
            white-space: nowrap !important;
        }

        #pnReportPanel .x-tab-panel-header {
            display: none !important;
        }
    </style>
    <script src="../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../Resource/js/global.js" type="text/javascript"></script>
    <script type="text/javascript">

        var keyPresstxtSearch = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                PagingToolbar1.doLoad();
                RowSelectionModel1.clearSelections();
                if (this.getValue() == '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() != '') {
                this.triggers[0].show();
            }
        }
        var ResetwdAdjustment = function () {

            grp_ListEmployeeStore.removeAll();
        }
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
        var triggershowChooseEmplyee = function (f, e) {
            if (e.getKey() == e.ENTER) {
                ucChooseEmployee1_wdChooseUser.show();
            }
        }
        var CheckInputHL = function () {
            if (grp_ListEmployee.store.getCount() == 0) {
                alert('Bạn chưa chọn cán bộ nào!');
                return false;
            }
            if (startDate.getValue() == '') {
                alert('Bạn chưa chọn ngày cần hiệu chỉnh');
                startDate.focus();
                return false;
            }
            if (hdfTimeSheetReport.getValue() == "" || hdfTimeSheetReport.getValue() == null) {
                alert('Bạn chưa chọn bảng chấm công!');
                return false;
            }
            if (txtReason.getValue() == '' || txtReason.getValue() == null) {
                alert('Bạn chưa nhập lý do');
                return false;
            }

            return true;
        }

        var addRecord = function (RecordId, EmployeeCode, FullName, DepartmentName) {
            var rowindex = getSelectedIndexRow();
            grp_ListEmployee.insertRecord(rowindex, {
                RecordId: RecordId,
                EmployeeCode: EmployeeCode,
                FullName: FullName,
                DepartmentName: DepartmentName
            });
            grp_ListEmployee.getView().refresh();
            grp_ListEmployee.getSelectionModel().selectRow(rowindex);
        }
        var getSelectedIndexRow = function () {
            var record = grp_ListEmployee.getSelectionModel().getSelected();
            var index = grp_ListEmployee.store.indexOf(record);
            if (index == -1)
                return 0;
            return index;
        }
        var ResetWdHopDong = function () {
            txtReason.reset();

        }

        var getPrKeyRecordList = function () {
            var jsonDataEncode = "";
            var records = window.grp_ListEmployeeStore.getRange();
            for (var i = 0; i < records.length; i++) {
                jsonDataEncode += records[i].data.RecordId + ",";
            }
            return jsonDataEncode;
        }

        var RenderGroupSymbol = function (value) {
            return "<b style='color:blue;'>" + value + "</b>";
        }

        var RenderSymbol = function (value) {
            return "<b style='color:fuchsia;'>" + value + "</b>";
        }

        var CheckUpdateInput = function () {
            if (txtReasonUpdate.getValue() == '' || txtReasonUpdate.getValue() == null) {
                alert('Bạn chưa nhập lý do');
                return false;
            }

            return true;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelected" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfIsLocked" />
            <ext:Hidden runat="server" ID="hdfTypeTimeSheet" />
            <ext:Hidden runat="server" ID="hdfTimeSheetEventIds" />

            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false"
                DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTimeAdjust" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdAdjustment.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="if (CheckSelectedRows(gridTimeAdjust) == false) {return false;}; " />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnEdit_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="return CheckSelectedRow(gridTimeAdjust);" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnDelete_Click">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button runat="server" ID="btnLockAdjust" Text="Khóa hiệu chỉnh" Icon="Lock"
                                                Hidden="False">
                                                <DirectEvents>
                                                    <Click OnEvent="BtnLockAdjustClick">
                                                        <EventMask ShowMask="true" Msg="Đang khóa hiệu chỉnh. Vui lòng đợi..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnOpenAdjust" runat="server" Text="Mở hiệu chỉnh" Icon="LockOpen"
                                                Hidden="true">
                                                <DirectEvents>
                                                    <Click OnEvent="BtnOpenAdjustClick">
                                                        <EventMask ShowMask="true" Msg="Đang mở khóa hiệu chỉnh. Vui lòng đợi..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarSpacer Width="10" />

                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220"
                                                EmptyText="Nhập mã, họ tên CCVC">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store runat="server" ID="storeAdjustment">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetOverTime" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelected.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="Day" />
                                                    <ext:RecordField Name="Month" />
                                                    <ext:RecordField Name="Year" />
                                                    <ext:RecordField Name="Reason" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                        <Listeners>
                                            <Load Handler="#{RowSelectionModel1}.clearSelections();" />
                                        </Listeners>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" Locked="true" />
                                        <ext:Column Header="Mã CCVC" Width="85" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ tên" Width="150" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column Header="Phòng ban" Width="250" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column Header="Ngày" Width="250" Align="Center" DataIndex="Day" />
                                        <ext:Column Header="Tháng" Width="250" Align="Center" DataIndex="Month" />
                                        <ext:Column Header="Năm" Width="250" Align="Center" DataIndex="Year" />
                                        <ext:Column Header="Lý do hiệu chỉnh" Width="250" Align="Center" DataIndex="Reason" />
                                        <ext:Column Header="Người hiệu chỉnh" Width="250" Align="Center" DataIndex="CreatedBy" />
                                        <ext:Column Header="Thời gian hiệu chỉnh" Width="250" Align="Center" DataIndex="CreatedDate" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <View>
                                    <ext:LockingGridView runat="server" />
                                </View>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(RowSelectionModel1.getSelected().id);hdfRecordId.setValue(RowSelectionModel1.getSelected().data.RecordId);" />
                                            <RowDeselect Handler="hdfId.reset();hdfRecordId.reset();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <DirectEvents>
                                    <RowDblClick>
                                        <EventMask ShowMask="true" />
                                    </RowDblClick>
                                </DirectEvents>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="25">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="25" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server" ID="wdAdjustment" Constrain="true" Modal="true" Title="Hiệu chỉnh thêm giờ"
                Icon="UserAdd" Layout="FormLayout" Resizable="false" AutoHeight="true" Width="650"
                Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container ID="Container7" runat="server" Layout="Column" Height="100">
                        <Items>
                            <ext:Container ID="Container8" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                LabelWidth="120">
                                <Items>
                                    <ext:DateField ID="startDate" runat="server" Vtype="daterange" FieldLabel="Chọn ngày" CtCls="requiredData"
                                        AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                    </ext:DateField>
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
                                                <AutoLoadParams>
                                                    <ext:Parameter Name="start" Value="={0}" />
                                                    <ext:Parameter Name="limit" Value="={15}" />
                                                </AutoLoadParams>
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
                                    <ext:TextArea runat="server" ID="txtReason" FieldLabel="Lý do hiệu chỉnh <span style='color:red'>*</span>" AnchorHorizontal="99%"
                                        Height="35" CtCls="requiredDate" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="ContainerSpace" runat="server" Layout="ColumnLayout" Height="20"></ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="200">
                        <Items>
                            <ext:GridPanel ID="GridPanel1" runat="server" Height="200" AnchorHorizontal="100%"
                                Title="Hiệu chỉnh thêm giờ" Border="true" ClicksToEdit="1" AutoExpandColumn="Name" Icon="UserTick">
                                <Store>
                                    <ext:Store ID="store1" AutoSave="true" runat="server" GroupField="GroupSymbolName" AutoLoad="True">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={10}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetSymbolEventOverTime" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="GroupSymbolName" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                    <ext:RecordField Name="IsInUsed" />
                                                    <ext:RecordField Name="Order" />
                                                    <ext:RecordField Name="Group" />
                                                    <ext:RecordField Name="WorkConvert" />
                                                    <ext:RecordField Name="MoneyConvert" />
                                                    <ext:RecordField Name="TimeConvert" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:Column Header="Ký hiệu" DataIndex="Code" Width="50">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column ColumnID="txtStatusName" Header="Tên tình trạng"
                                            DataIndex="Name" Width="120" />
                                        <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="170" DataIndex="TimeConvert" />
                                        <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" Hidden="True">
                                            <Renderer Fn="RenderGroupSymbol" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel ID="RowSelectionModel2" runat="server" SingleSelect="false">
                                    </ext:CheckboxSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="true" Msg="Đang tải" />
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar3" runat="server" PageSize="10">
                                        <Items>
                                            <ext:Label ID="Label3" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer3" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox3" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                </Items>
                                                <SelectedItem Value="10" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar2}.pageSize = parseInt(this.getValue()); #{PagingToolbar2}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Height="20"></ext:Container>
                    <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="200">
                        <Items>
                            <ext:GridPanel runat="server" ID="grp_ListEmployee" TrackMouseOver="true" Title="Danh sách cán bộ"
                                StripeRows="true" Border="true" Region="Center" Icon="User" AutoExpandColumn="DepartmentName"
                                AutoExpandMin="150">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tbListEmployee">
                                        <Items>
                                            <ext:Button runat="server" ID="btnChooseEmployee" Icon="UserAdd" Text="Chọn cán bộ"
                                                TabIndex="12">
                                                <Listeners>
                                                    <Click Handler="ucChooseEmployee_wdChooseUser.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDeleteEmployee" Icon="Delete" Text="Xóa" Disabled="true"
                                                TabIndex="13">
                                                <Listeners>
                                                    <Click Handler="#{grp_ListEmployee}.deleteSelected(); #{hdfTotalRecord}.setValue(#{hdfTotalRecord}.getValue()*1 - 1);if(hdfTotalRecord.getValue() ==0){btnDeleteEmployee.disable();}" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="grp_ListEmployeeStore" AutoLoad="false" runat="server" ShowWarningOnFailure="false"
                                        SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="false">
                                        <Reader>
                                            <ext:JsonReader IDProperty="RecordId">
                                                <Fields>
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel ID="ColumnModel3" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="40" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Width="100" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="FullName" Header="Họ tên" Width="200" DataIndex="FullName" />
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" DataIndex="DepartmentName">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="lkv1" />
                                </View>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="chkEmployeeRowSelection">
                                        <Listeners>
                                            <RowSelect Handler="btnDeleteEmployee.enable();" />
                                            <RowDeselect Handler="btnDeleteEmployee.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateHL" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler=" return CheckInputHL();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnUpdateHL_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="ListRecordId" Value="getPrKeyRecordList()" Mode="Raw" />
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseHL" Text="Đóng lại" Icon="Decline" Hidden="True">
                        <Listeners>
                            <Click Handler="wdAdjustment.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetwdAdjustment();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="wdUpdateAdjustment" Constrain="true" Modal="true" Title="Cập nhật hiệu chỉnh thêm giờ"
                Icon="Pencil" Layout="FormLayout" Resizable="false" AutoHeight="true" Width="650"
                Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="Column" Height="100">
                        <Items>
                            <ext:Container ID="Container4" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                LabelWidth="120">
                                <Items>
                                    <ext:TextField runat="server" ID="txtFullName" FieldLabel="Họ và tên" ReadOnly="True" AnchorHorizontal="98%" />
                                    <ext:DateField ID="AdjustUpdateDate" runat="server" Vtype="daterange" FieldLabel="Chọn ngày" ReadOnly="True"
                                        AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                    </ext:DateField>
                                    <ext:TextArea runat="server" ID="txtReasonUpdate" FieldLabel="Lý do hiệu chỉnh <span style='color:red'>*</span>" AnchorHorizontal="98%"
                                        Height="35" CtCls="requiredDate" />
                                </Items>
                            </ext:Container>

                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="btnUpdateTimeSheet" runat="server" Hidden="false" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler=" return CheckUpdateInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnUpdate_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button6" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdUpdateAdjustment.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
