<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetDetailManagement" CodeBehind="TimeSheetDetailManagement.aspx.cs" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <ext:ResourcePlaceHolder runat="server" Mode="ScriptFiles" />
    <script type="text/javascript">
        Ext.net.DirectEvent.timeout = 200000;
        Ext.Ajax.timeout = 600000;
        var RenderDay = function (value, p, record) {
            return `<div style="width:100%;height:100%;" onclick="Ext.net.DirectMethods.InitWindowTimeSheet('${value.Day.toString()}', '${record.json.RecordId}')">${value.SymbolDisplay}</div>`;
        }

        var RenderTotal = function (value) {
            if (value != null && (value > 0 || value < 0)) {
                return "<span style='color:red'>" + value.toString() + "</span>";
            } else {
                return value;
            }
        }

        var RenderSymbol2 = function (value, p, record) {
            var color = record.data.Color || record.data.SymbolColor
            if (value)
                return "<span class='badge' style='background:" + color + "'>" + value + "</span>";
            else
                return "<span class='badge' style='background:#FF0000'>!</span>";
        }

        var CheckInput = function () {
            if (grpWorkShift.getSelectionModel().getCount() == 0) {
                alert("Bạn phải chọn ít nhất một ca");
                return false;
            }
            if (gridTimeSheetSymbol.getSelectionModel().getCount() == 0) {
                alert("Bạn phải chọn ít nhất một ký hiệu");
                return false;
            }
            wdTimeSheetAdd.hide();
            return true;
        }

        var onKeyUp = function (field) {
            if (this.startDateField) {
                field = Ext.getCmp(this.startDateField);
                field.setMaxValue();
                this.dateRangeMax = null;
            } else if (this.endDateField) {
                field = Ext.getCmp(this.endDateField);
                field.setMinValue();
                this.dateRangeMin = null;
            }
            field.validate();
        }

        var keyPressFilter = function (f, e) {
            if (e.getKey() == e.ENTER) {
                //Ext.net.DirectMethods.OnSearchClick()
                PagingToolbar1.doLoad();
                PagingToolbar1.pageIndex = 0;
                if (this.getValue() == '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() != '') {
                this.triggers[0].show();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentId" />
            <ext:Hidden runat="server" ID="hdfNumDayOfMonth" />
            <ext:Hidden runat="server" ID="hdfStartDate" />
            <ext:Hidden runat="server" ID="hdfEndDate" />
            <ext:Hidden runat="server" ID="hdfTimeSheetEventId" />
            <ext:Hidden runat="server" ID="hdfIsLocked" />
            <ext:Hidden runat="server" ID="hdfTimeSheetReportId" />
            <ext:Hidden runat="server" ID="hdfTypeTimeSheet" />
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfRecordIds" />
            <ext:Hidden runat="server" ID="hdfUpdateTimeSheetEventId" />
            <ext:Hidden runat="server" ID="hdfStartDateEmployee" />
            <ext:Hidden runat="server" ID="hdfEndDateEmployee" />
            <ext:Hidden runat="server" ID="hdfGroupWorkShift" />

            <uc1:ChooseEmployee ID="ucChooseEmployee1" runat="server" ChiChonMotCanBo="false"
                DisplayWorkingStatus="TatCa" />

            <!-- Store lấy đơn vị theo người dùng đăng nhập -->
            <ext:Store runat="server" ID="storeDepartment" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Window runat="server" Title="Cập nhật thông tin chấm công" Resizable="true" Layout="FormLayout"
                Padding="6" Width="650" Hidden="true" Icon="UserTick" ID="wdUpdateTimeSheet"
                Modal="true" Constrain="true" Height="570">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="Form" Height="130">
                        <Items>
                            <ext:Container ID="Container6" runat="server" LabelAlign="left" Layout="Form">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cbxDay" FieldLabel="Chọn ca" ValueField="Day" DisplayField="Day"
                                        AnchorHorizontal="100%" ItemSelector="div.list-item" Width="300">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                                        </Triggers>
                                        <Template ID="tpl23" runat="server">
                                            <Html>
                                                <tpl for=".">
                                                    <div class="list-item"> 
                                                        {Day}
                                                    </div>
                                                </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show(); hdfStartDateEmployee.setValue(cbxDay.getValue()); hdfEndDateEmployee.setValue(cbxDay.getValue());gridUpdateTimeSheet.reload()" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfStartDateEmployee.reset(); hdfEndDateEmployee.reset(); gridUpdateTimeSheet.reload()}" />
                                        </Listeners>
                                        <Store>
                                            <ext:Store ID="storeCbxDay" runat="server">
                                                <Reader>
                                                    <ext:JsonReader>
                                                        <Fields>
                                                            <ext:RecordField Name="Day" Type="String" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:TextArea runat="server" ID="txtTimeLogs" ReadOnly="True" FieldLabel="Thời gian logs" AnchorHorizontal="100%"></ext:TextArea>
                        </Items>
                    </ext:Container>
                    <ext:GridPanel ID="gridUpdateTimeSheet" runat="server" Height="350" AnchorHorizontal="100%"
                        Title="" Border="false" ClicksToEdit="1">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="tbUpdateTimeSheet">
                                <Items>
                                    <ext:Button runat="server" ID="btnAddNewTimeSheet" Icon="UserAdd" Text="Thêm mới"
                                        TabIndex="12">
                                        <Listeners>
                                            <Click Handler="wdTimeSheetAdd.show();storeWorkShift.reload();chkWorkShiftRowSelection.clearSelections();chkSelectionModelSymbol.clearSelections();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="btnDeleteUpdateTimeSheet" Icon="Delete" Text="Xóa" Disabled="true"
                                        TabIndex="13">
                                        <DirectEvents>
                                            <Click OnEvent="btnDeleteTime_Click">
                                                <Confirmation Title="Thông báo" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" ConfirmRequest="true" />
                                                <EventMask ShowMask="true" Msg="Đang xóa dữ liệu. Vui lòng chờ..." />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store ID="storeTimeSheetEventEmployee" AutoSave="true" runat="server" GroupField="GroupSymbolName">
                                <Proxy>
                                    <ext:HttpProxy Method="GET" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={15}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="TimeSheetEvent" />
                                    <ext:Parameter Name="timeSheetEventId" Value="hdfTimeSheetEventId.getValue()" Mode="Raw" />
                                    <ext:Parameter Name="fromDate" Value="hdfStartDateEmployee.getValue()" Mode="Raw" />
                                    <ext:Parameter Name="toDate" Value="hdfEndDateEmployee.getValue()" Mode="Raw" />
                                    <ext:Parameter Name="recordIds" Value="hdfRecordId.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="StatusId" />
                                            <ext:RecordField Name="SymbolColor" />
                                            <ext:RecordField Name="SymbolCode" />
                                            <ext:RecordField Name="SymbolName" />
                                            <ext:RecordField Name="GroupSymbolName" />
                                            <ext:RecordField Name="CreatedDate" />
                                            <ext:RecordField Name="WorkConvert" />
                                            <ext:RecordField Name="TimeConvert" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <View>
                            <ext:GroupingView ID="GroupingView2" runat="server" ForceFit="false" MarkDirty="false"
                                ShowGroupName="false" EnableNoGroups="true" />
                        </View>
                        <ColumnModel ID="ColumnModel4" runat="server">
                            <Columns>
                                <ext:Column Header="Ký hiệu" DataIndex="SymbolCode" Width="50">
                                    <Renderer Fn="RenderSymbol2" />
                                </ext:Column>
                                <ext:Column ColumnID="SymbolName" Header="Tên ký hiệu"
                                    DataIndex="SymbolName" Width="200" />
                                <ext:Column ColumnID="WorkConvert" Header="Số công quy đổi" Width="100" DataIndex="WorkConvert" />
                                <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" Hidden="True">
                                    <Renderer Fn="RenderGroupSymbol" />
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <LoadMask ShowMask="true" Msg="Đang tải" />
                        <Listeners>
                            <RowClick Handler="btnDeleteUpdateTimeSheet.enable();" />
                            <RowDblClick Handler="" />
                        </Listeners>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelUpdateTimeSheet">
                                <Listeners>
                                    <RowSelect Handler="hdfUpdateTimeSheetEventId.setValue(RowSelectionModelUpdateTimeSheet.getSelected().get('Id')); " />
                                    <RowDeselect Handler="hdfUpdateTimeSheetEventId.reset(); " />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="25">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" Text="Số bản ghi trên một trang:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="15" />
                                            <ext:ListItem Text="25" />
                                            <ext:ListItem Text="30" />
                                            <ext:ListItem Text="40" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                        </Items>
                                        <SelectedItem Value="15" />
                                        <Listeners>
                                            <Select Handler="#{PagingToolbar2}.pageSize = parseInt(this.getValue()); #{PagingToolbar2}.doLoad();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Listeners>
                                    <Change Handler="" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="Button2" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdUpdateTimeSheet.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>

            <ext:Window runat="server" Title="Tạo mới thông tin chấm công" Resizable="true" Layout="FormLayout"
                Padding="6" Width="690" Hidden="true" Icon="UserTick" ID="wdTimeSheetAdd"
                Modal="true" Constrain="true" Height="550">
                <Items>
                    <ext:Container ID="Container18" runat="server" Layout="FormLayout">
                        <Items>
                            <ext:ComboBox runat="server" ID="cbxGroupWorkShift" FieldLabel="Chọn nhóm phân ca"
                                DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                LabelWidth="270" Width="422" ItemSelector="div.list-item">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template ID="Template1" runat="server">
                                    <Html>
                                        <tpl for=".">
                                                <div class="list-item"> 
                                                    {Name}
                                                </div>
                                            </tpl>
                                    </Html>
                                </Template>
                                <Store>
                                    <ext:Store ID="storeGroupWorkShift" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="GroupWorkShift" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();hdfGroupWorkShift.setValue(cbxGroupWorkShift.getValue());#{storeWorkShift}.reload();" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupWorkShift.reset(); #{storeWorkShift}.reload()};" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:GridPanel runat="server" ID="grpWorkShift" AnchorHorizontal="100%" Height="200" AutoExpandColumn="Name"
                                Title="Bảng phân ca" Border="True" ClicksToEdit="1">
                                <Store>
                                    <ext:Store runat="server" ID="storeWorkShift" AutoSave="True" AutoLoad="True">
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="WorkShift" />
                                            <ext:Parameter Name="fromDate" Value="hdfStartDateEmployee.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="toDate" Value="hdfEndDateEmployee.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="groupWorkShift" Value="hdfGroupWorkShift.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="StartDate" />
                                                    <ext:RecordField Name="EndDate" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView runat="server" ForceFit="false" MarkDirty="false" ShowGroupName="false" EnableNoGroups="true" />
                                </View>
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:Column Header="Tên ca" DataIndex="Name" Width="50" />
                                        <ext:DateColumn Header="Ngày bắt đầu" DataIndex="StartDate" Width="100" Format="dd/MM/yyyy" />
                                        <ext:DateColumn Header="Ngày kết thúc" DataIndex="EndDate" Width="100" Format="dd/MM/yyyy" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel ID="chkWorkShiftRowSelection" runat="server" SingleSelect="false">
                                    </ext:CheckboxSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar3" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                        PageSize="20" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                        DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:GridPanel ID="gridTimeSheetSymbol" runat="server" Height="235" AnchorHorizontal="100%"
                                Title="Ký hiệu chấm công" ClicksToEdit="1" AutoExpandColumn="Name">
                                <Store>
                                    <ext:Store ID="storeStatusWork" AutoSave="true" runat="server" GroupField="GroupSymbolName">
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={10}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
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
                                                    <ext:RecordField Name="Color" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" />
                                </View>
                                <ColumnModel ID="ColumnModel2" runat="server">
                                    <Columns>
                                        <ext:Column Header="Ký hiệu" DataIndex="Code" Width="50">
                                            <Renderer Fn="RenderSymbol2" />
                                        </ext:Column>
                                        <ext:Column ColumnID="txtStatusName" Header="Tên tình trạng"
                                            DataIndex="Name" Width="120" />
                                        <ext:Column ColumnID="WorkConvert" Header="Số công quy đổi" Width="170" DataIndex="WorkConvert" />
                                        <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" Hidden="True">
                                            <Renderer Fn="RenderGroupSymbol" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel ID="chkSelectionModelSymbol" runat="server" SingleSelect="false">
                                    </ext:CheckboxSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="true" Msg="Đang tải" />
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10">
                                        <Items>
                                            <ext:Label ID="Label4" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer5" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox3" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                </Items>
                                                <SelectedItem Value="10" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar4}.pageSize = parseInt(this.getValue()); #{PagingToolbar4}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnAccept" Text="Đồng ý" Icon="Accept">
                        <Listeners>
                            <Click Handler="return CheckInput();"></Click>
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnAccept_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="jsonTimeSheetEvent" Value="Ext.encode(#{gridTimeSheetSymbol}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetAdd.hide();chkSelectionModelSymbol.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
              
            </ext:Window>

            <ext:Viewport ID="vp" runat="server" Layout="Center">
                <Items>
                    <ext:BorderLayout Border="false" Header="false" runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTimeSheet" TrackMouseOver="true" Header="true" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%" Title="Thông tin về bảng chấm công" Icon="Date">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnBack" runat="server" Text="Quay lại" Icon="ArrowLeft">
                                                <DirectEvents>
                                                    <Click OnEvent="btnBack_Click">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer4" runat="server" Width="10" />

                                            <ext:Button runat="server" ID="btnLockTimeSheet" Text="Khóa bảng công" Icon="Lock"
                                                Hidden="False">
                                                <DirectEvents>
                                                    <Click OnEvent="btnLockTimeSheetReportClick">
                                                        <EventMask ShowMask="true" Msg="Đang khóa bảng công. Vui lòng đợi..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnOpenTimeSheet" runat="server" Text="Mở bảng công" Icon="LockOpen"
                                                Hidden="true">
                                                <DirectEvents>
                                                    <Click OnEvent="btnUnlockTimeSheetReportClick">
                                                        <EventMask ShowMask="true" Msg="Đang mở khóa bảng công. Vui lòng đợi..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>

                                            <ext:ToolbarFill runat="server" ID="tbf" />
                                            <ext:DateField ID="dfFromDateSearch" runat="server" Width="140" EmptyText="Từ ngày"
                                                AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Vtype="daterange" AutoDataBind="true"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfToDateSearch}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show();Ext.net.DirectMethods.OnFilterDay();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfToDateSearch.setMinValue();Ext.net.DirectMethods.OnFilterDay();}" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:DateField ID="dfToDateSearch" runat="server" EmptyText="Đến ngày"
                                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Vtype="daterange" Width="140"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfFromDateSearch}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show();Ext.net.DirectMethods.OnFilterDay();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfFromDateSearch.setMaxValue();Ext.net.DirectMethods.OnFilterDay();}"/>
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập mã, họ tên CCVC">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPressFilter" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();PagingToolbar1.doLoad();PagingToolbar1.pageIndex = 0;" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="PagingToolbar1.doLoad();PagingToolbar1.pageIndex = 0;" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store runat="server" ID="storeTimeSheet">
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/HandlerTimeSheetAutomatic.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="searchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentId.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="recordIds" Value="hdfRecordIds.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="startDate" Value="hdfStartDate.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="endDate" Value="hdfEndDate.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="timeSheetReportId" Value="hdfTimeSheetReportId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="TimeSheetCode" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="TotalActual" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalWorkDay" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalHolidayL" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalPaidLeaveT" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalUnpaidLeaveR" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalUnleaveK" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalLateM" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalGoWorkC" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalFullDayX" DefaultValue="0" />
                                                    <ext:RecordField Name="TimeSheetEventDayModels" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:LockingGridView runat="server" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn ColumnID="STT" Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="200" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Số hiệu CBCC" Width="85" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="DepartmentName" Header="Tên đơn vị" Width="150" Align="Left" Locked="true" DataIndex="DepartmentName" />
                                        <ext:Column ColumnID="TotalActual" Header="Tổng ngày công thực tế" Width="120" Align="Center" DataIndex="TotalWorkDay" Tooltip="Tổng ngày công thực tế">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalActual" Header="Tổng số ngày công" Width="120" Align="Center" DataIndex="TotalActual" Tooltip="Tổng số ngày công">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalFullDayX" Header="Tổng ngày công đầy đủ" Width="120" Align="Center" DataIndex="TotalFullDayX" Tooltip="Tổng số ngày công đầy đủ">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalHolidayL" Header="Tổng nghỉ lễ (L)" Width="120" Align="Center" DataIndex="TotalHolidayL" Tooltip="Tổng nghỉ lễ (L)">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalPaidLeaveT" Header="Tổng số ngày nghỉ phép hưởng lương (P)" Width="120" Align="Center" DataIndex="TotalPaidLeaveT" Tooltip="Tổng số ngày nghỉ phép hưởng lương (P)">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalUnpaidLeaveP" Header="Tổng số ngày nghỉ việc riêng (R)" Width="120" Align="Center" DataIndex="TotalUnpaidLeaveR" Tooltip="Tổng số ngày nghỉ việc riêng (R)">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalGoWorkC" Header="Tổng công tác (C)" Width="120" Align="Center" DataIndex="TotalGoWorkC" Tooltip="Tổng công tác (C)">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalUnleaveK" Header="Tổng công nghỉ không phép (K)" Width="120" Align="Center" DataIndex="TotalUnleaveK" Tooltip="Tổng công nghỉ không phép (K)">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalLateM" Header="Tổng số ngày đi trễ (M)" Width="120" Align="Center" DataIndex="TotalLateM" Tooltip="Tổng số ngày đi trễ (M)">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));" />
                                            <RowDeselect Handler="hdfRecordId.reset();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="15">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="15" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="15" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>



