<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeKeeping.TimeSheetManagement" Codebehind="TimeSheetManagement.aspx.cs" %>
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

        function StartEditTimeSheetReport() {
            try {
                var record = grpTimeSheetReportList.getSelectionModel().getSelected();
                var index = grpTimeSheetReportList.store.indexOf(record);
                grpTimeSheetReportList.getRowEditor().stopEditing();
                grpTimeSheetReportList.getSelectionModel().selectRow(index);
                grpTimeSheetReportList.getRowEditor().startEditing(index);
            } catch (e) {
            }
        }

        function keyEnterPressTimeSheetReport(f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar2.pageIndex = 0;
                PagingToolbar2.doLoad();
                RowSelectionModelTimeSheetReport.clearSelections();
                if (this.getValue() == '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() != '') {
                this.triggers[0].show();
            }
        }

        function checkInputTimeSheetReport() {
            if (hdfDepartmentId.getValue() == "" || hdfDepartmentId.getValue() == null) {
                alert('Bạn chưa chọn phòng ban!');
                return false;
            }
            if (hdfMonth.getValue() == "" || hdfMonth.getValue() == null) {
                alert('Bạn chưa chọn tháng!');
                return false;
            }
            if (hdfYear.getValue() == "" || hdfYear.getValue() == null) {
                alert('Bạn chưa chọn năm!');
                return false;
            }
            if (txtWorkInMonth.getValue() == "" || txtWorkInMonth.getValue() == null) {
                alert('Bạn chưa nhập tổng số ngày công trong tháng!');
                return false;
            }
            if (txtTitleTimeSheetReport.getValue() == "" || txtTitleTimeSheetReport.getValue() == null) {
                alert('Bạn chưa nhập tiêu đề bảng chấm công!');
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="timeSheetManagement" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentId" />
            <ext:Hidden runat="server" ID="hdfNumDayOfMonth" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfTimeSheetCode" />
            <ext:Hidden runat="server" ID="hdfTimeSheetId" />
            <ext:Hidden runat="server" ID="hdfDay" />
            <ext:Hidden runat="server" ID="hdfIsLocked" />
            <ext:Hidden runat="server" ID="hdfTimeSheetReportId"/>
            <ext:Hidden runat="server" ID="hdfTypeTimeSheet"/>
            <ext:Hidden runat="server" ID="hdfRecordId"/>
            <ext:Hidden runat="server" ID="hdfUpdateTimeSheetEventId"/>
            

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

            <script>
                function intTimeSheetInput(id, day) {
                    document.getElementById("hdfTimeSheetId").value = id;
                    document.getElementById("hdfDay").value = day;
                    Ext.net.DirectMethods.InitWindowTimeSheet();
                }
            </script>
            <ext:Window runat="server" Title="Cập nhật thông tin chấm công" Resizable="true" Layout="FormLayout"
                Padding="6" Width="650" Hidden="true" Icon="UserTick" ID="wdUpdateTimeSheet"
                Modal="true" Constrain="true" Height="550">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="Form" Height="100">
                        <Items>
                            <ext:Container ID="Container6" runat="server" LabelAlign="left" Layout="ColumnLayout" LabelWidth="50" Height="23">
                                <Items>
                                    <ext:Container ID="Container4" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                        LabelWidth="100" >
                                        <Items>
                                            <ext:TextField runat="server" ID="txtUpdateAdjustDate" FieldLabel="Ngày" Width="70px" ReadOnly="True" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container5" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                                   LabelWidth="50">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtUpdateDayOfWeek" FieldLabel="Thứ" Width="70px" ReadOnly="True" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                            <ext:TextArea runat="server" ID="txtTimeLogs" ReadOnly="True" FieldLabel="Thời gian logs" AnchorHorizontal="98%"></ext:TextArea>
                        </Items>
                    </ext:Container>
                    <ext:GridPanel ID="gridUpdateTimeSheet" runat="server" Height="350" AnchorHorizontal="100%"
                        Title="" Border="false" ClicksToEdit="1" AutoExpandColumn="Description">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="tbUpdateTimeSheet">
                                <Items>
                                    <ext:Button runat="server" ID="btnAddNewTimeSheet" Icon="UserAdd" Text="Thêm mới"
                                                TabIndex="12">
                                        <Listeners>
                                            <Click Handler="wdTimeSheetAdd.show();wdUpdateTimeSheet.hide();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="btnDeleteUpdateTimeSheet" Icon="Delete" Text="Xóa" Disabled="true"
                                                TabIndex="13">
                                        <DirectEvents>
                                            <Click OnEvent="btnDeleteTime_Click">
                                                <EventMask ShowMask="true" Msg="Đang xóa dữ liệu. Vui lòng chờ..." />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store ID="store1" AutoSave="true" runat="server" GroupField="GroupSymbolName">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={15}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="UpdateTimeSheetEvent" />
                                    <ext:Parameter Name="timeSheetId" Value="hdfTimeSheetId.getValue()" Mode="Raw"/>
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="TimeSheetId" />
                                            <ext:RecordField Name="StatusId" />
                                            <ext:RecordField Name="Description" />
                                            <ext:RecordField Name="Symbol" />
                                            <ext:RecordField Name="GroupSymbolName" />
                                            <ext:RecordField Name="CreatedDate" />
                                            <ext:RecordField Name="WorkConvert" />
                                            <ext:RecordField Name="MoneyConvert" />
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
                                <ext:Column Header="Ký hiệu" DataIndex="Symbol" Width="50">
                                    <Renderer Fn="RenderSymbol" />
                                </ext:Column>
                                <ext:Column ColumnID="Description" Header="Tên ký hiệu"
                                    DataIndex="Description" Width="200" />
                                <ext:Column ColumnID="WorkConvert" Header="Số công quy đổi" Width="100" DataIndex="WorkConvert" />
                                <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" Hidden="True">
                                    <Renderer Fn="RenderGroupSymbol" />
                                </ext:Column>
                                <ext:Column Header="Trạng thái" DataIndex="StatusId" Width="100">
                                    <Renderer Fn="RenderStatusEvent" />
                                </ext:Column>
                                <ext:Column ColumnID="TimeSheetId" Header="" Width="50" DataIndex="TimeSheetId" Hidden="True">
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
                Padding="6" Width="650" Hidden="true" Icon="UserTick" ID="wdTimeSheetAdd"
                Modal="true" Constrain="true" Height="550">
                <Items>
                    <ext:Container ID="Container18" runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container ID="Container8" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                LabelWidth="120" >
                                <Items>
                                    <ext:TextField runat="server" ID="txtAdjustDate" FieldLabel="Ngày hiệu chỉnh" Width="70px" ReadOnly="True" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container1" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                           LabelWidth="50">
                                <Items>
                                    <ext:TextField runat="server" ID="txtDayOfWeek" FieldLabel="Thứ" Width="70px" ReadOnly="True" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:GridPanel ID="gridStatusWork" runat="server" Height="450" AnchorHorizontal="100%"
                        Title="" Border="false" ClicksToEdit="1" AutoExpandColumn="Name">
                        <Store>
                            <ext:Store ID="storeStatusWork" AutoSave="true" runat="server" GroupField="GroupSymbolName">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={10}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="TimeSheetSymbolEvent" />
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
                                    <Renderer Fn="RenderSymbol" />
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
                            <ext:CheckboxSelectionModel ID="RowSelectionModel2" runat="server" SingleSelect="false">
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
                <Buttons>
                    <ext:Button runat="server" ID="btnAccept" Text="Đồng ý" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="btnAccept_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="jsonTimeSheetEvent" Value="Ext.encode(#{gridStatusWork}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetAdd.hide();RowSelectionModel2.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="#{gridStatusWork}.getSelectionModel().clearSelections();" />
                </Listeners>
            </ext:Window>
             <ext:Window Modal="true" Hidden="true" runat="server" ID="wdTimeSheetManage" Constrain="true"
                Title="Quản lý bảng chấm công" Icon="Table" Layout="FormLayout" Width="660" AutoHeight="true">
                <Items>
                    <ext:Hidden ID="hdfTimeSheetReportListID" runat="server" />
                    <ext:GridPanel ID="grpTimeSheetReportList" runat="server" StripeRows="true" Border="false"
                        AnchorHorizontal="100%" Header="false" Height="350" Title="Danh sách bảng chấm công"
                        AutoExpandColumn="Title">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="toolbarFn">
                                <Items>
                                    <ext:Button ID="btnAddTimSheetManage" Icon="Add" runat="server" Text="Thêm">
                                        <Listeners>
                                            <Click Handler="wdCreateTimeSheet.show();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" Disabled="true" ID="btnEditTimSheetManage" Text="Sửa"
                                        Icon="Pencil">
                                        <Listeners>
                                            <Click Handler="StartEditTimeSheetReport();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" Text="Xóa" Icon="Delete"
                                        Disabled="true" ID="btnDeleteTimeSheetManage">
                                         <DirectEvents>
                                            <Click OnEvent="btnDeleteTimeSheetManage_Click">
                                                <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa không ?"
                                                    ConfirmRequest="true" />
                                                <EventMask ShowMask="true" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:ToolbarFill runat="server" ID="ToolbarFill1" />
                                    <ext:TriggerField ID="txtSearchTimeSheetReport" EnableKeyEvents="true" runat="server" EmptyText="Nhập từ khóa để tìm kiếm">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="keyEnterPressTimeSheetReport" />
                                            <TriggerClick Handler="this.clear();this.triggers[0].hide(); #{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad(); RowSelectionModelTimeSheetReport.clearSelections();  " />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:Button runat="server" Text="Tìm kiếm" Icon="Zoom">
                                        <Listeners>
                                            <Click Handler="#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad(); RowSelectionModelTimeSheetReport.clearSelections(); " />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store ID="storeListTimeSheetManage" AutoSave="true" runat="server">
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
                                    <ext:Parameter Name="SearchKey" Value="txtSearchTimeSheetReport.getValue()" Mode="Raw" />
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
                        <ColumnModel ID="ColumnModel3" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn Header="STT" Width="30" />
                                <ext:Column ColumnID="Title" Header="Tên bảng chấm công" Width="160" DataIndex="Title">
                                     <Editor>
                                        <ext:TextField runat="server" MaxLength="500" ID="txtTitleOfTimeSheetBoard">
                                            <Listeners>
                                                <Blur Handler="Ext.net.DirectMethods.UpdateTimeSheetBoard();" />
                                            </Listeners>
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column ColumnID="ThoiGianChamCong" Align="Center" Header="Thời gian chấm công"
                                    Width="120" DataIndex="Month">
                                    <Renderer Handler="return 'Tháng '+record.data.Month+'/'+record.data.Year" />
                                </ext:Column>
                                <ext:Column ColumnID="WorkInMonth" Align="Center" Header="Tổng số ngày công trong tháng" Width="120" DataIndex="WorkInMonth"></ext:Column>
                                <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo"
                                    Width="80" DataIndex="CreatedDate" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                             <ext:RowSelectionModel ID="RowSelectionModelTimeSheetReport" runat="server">
                                <Listeners>
                                    <RowSelect Handler="btnEditTimSheetManage.enable();hdfTimeSheetReportListID.setValue(RowSelectionModelTimeSheetReport.getSelected().get('Id')); btnDeleteTimeSheetManage.enable();" />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <Plugins>
                            <ext:RowEditor ID="RowEditor1" runat="server" SaveText="Cập nhật"
                                CancelText="Hủy" />
                        </Plugins>
                        <LoadMask ShowMask="true" Msg="Đang tải" />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="10">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" Text="Số bản ghi trên một trang" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer7" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="30" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                        </Items>
                                        <SelectedItem Value="10" />
                                        <Listeners>
                                            <Select Handler="#{PagingToolbar2}.pageSize = parseInt(this.getValue()); #{PagingToolbar2}.doLoad();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Listeners>
                                    <Change Handler="RowSelectionModelTimeSheetReport.clearSelections();" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnCloseTimeSheetManage" Text="Đóng lại"
                        Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetManage.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdCreateTimeSheet" Constrain="true"
                Title="Tạo bảng chấm công" Icon="DateAdd" Layout="FormLayout" Width="660" AutoHeight="true">
                <Items>
                    <ext:ComboBox runat="server" ID="cbxDepartment" FieldLabel="Chọn bộ phận"
                        LabelWidth="70" Width="300" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                        ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="false" StoreID="storeDepartment">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template runat="server">
                            <Html>
                                <tpl for=".">
                                <div class="list-item">
                                    {Name} 
                                </div>
                            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cbxDepartment.getValue());" />
                            <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Container ID="Containerm" Height="27" runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:CompositeField runat="server" ID="cf1" FieldLabel="Chọn tháng">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cbxMonth" Width="80" Editable="false" FieldLabel="Chọn tháng" CtCls="requiredData">
                                        <Items>
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
                                        </Items>
                                        <Listeners>
                                            <Select Handler="hdfMonth.setValue(cbxMonth.getValue());" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="55" CtCls="requiredData">
                                        <Listeners>
                                            <Spin Handler="hdfYear.setValue(spnYear.getValue());" />
                                        </Listeners>
                                    </ext:SpinnerField>
                                </Items>
                            </ext:CompositeField>
                        </Items>
                    </ext:Container>
                    <ext:TextField runat="server" ID="txtWorkInMonth" FieldLabel="Tổng số ngày công trong tháng"
                        AnchorHorizontal="100%" MaskRe="/[0-9,-]/" MaxLength="10" CtCls="requiredData"/>
                    <ext:TextArea ID="txtTitleTimeSheetReport" BlankText="Bạn bắt buộc phải nhập bảng chấm công" CtCls="requiredData"
                        AllowBlank="false" AnchorHorizontal="100%" ColumnWidth="1.0" FieldLabel="Tiêu đề bảng chấm công"
                        runat="server" />
                </Items>
                <Buttons>
                    <ext:Button ID="btnCreateTimeSheetReport" runat="server" Icon="Accept" Text="Tạo bảng công">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetReport();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="CreateTimeSheetReport_Click" Timeout="200000">
                                <EventMask ShowMask="true" Msg="Đang tạo bảng chấm công..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnCloseTimeSheetReport" runat="server" Icon="Decline" Text="Đóng lại">
                        <Listeners>
                            <Click Handler="wdCreateTimeSheet.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdSelectTimeSheetReport" Constrain="true"
                Title="Chọn bảng chấm công" Icon="Table" Layout="FormLayout" Width="660" AutoHeight="true">
                <Items>
                    <ext:Hidden ID="hdfSelectTimeSheetReportId" runat="server" />
                    <ext:GridPanel ID="gridSelectTimeSheetReport" runat="server" StripeRows="true" Border="false"
                        AnchorHorizontal="100%" Header="false" Height="350" Title="Danh sách bảng chấm công"
                        AutoExpandColumn="Title">
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
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn Header="STT" Width="30" />
                                <ext:Column ColumnID="Title" Header="Tên bảng chấm công" Width="160" DataIndex="Title">
                                </ext:Column>
                                <ext:Column ColumnID="ThoiGianChamCong" Align="Center" Header="Thời gian chấm công"
                                    Width="120" DataIndex="Month">
                                    <Renderer Handler="return 'Tháng '+record.data.Month+'/'+record.data.Year" />
                                </ext:Column>
                                <ext:Column ColumnID="WorkInMonth" Align="Center" Header="Tổng số ngày công trong tháng"
                                            Width="120" DataIndex="WorkInMonth"/>
                                <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo"
                                    Width="80" DataIndex="CreatedDate" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelSelectTimeSheetReport">
                                <Listeners>
                                    <RowSelect Handler="hdfSelectTimeSheetReportId.setValue(RowSelectionModelSelectTimeSheetReport.getSelected().get('Id'));" />
                                    <RowDeselect Handler="hdfSelectTimeSheetReportId.reset(); " />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar3" runat="server" PageSize="10">
                                <Items>
                                    <ext:Label ID="Label3" runat="server" Text="Số bản ghi trên một trang" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer8" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox5" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="30" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                        </Items>
                                        <SelectedItem Value="10" />
                                        <Listeners>
                                            <Select Handler="#{PagingToolbar3}.pageSize = parseInt(this.getValue()); #{PagingToolbar3}.doLoad();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Listeners>
                                    <Change Handler="RowSelectionModelSelectTimeSheetReport.clearSelections();" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button Text="Đồng ý" Icon="Accept" ID="btnAcceptTimeSheetBoard" runat="server">
                        <Listeners>
                            <Click Handler="return CheckSelectedRows(gridSelectTimeSheetReport);" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnAcceptTimeSheetBoard_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button7" Text="Đóng lại"
                        Icon="Decline">
                        <Listeners>
                            <Click Handler="wdSelectTimeSheetReport.hide();RowSelectionModelSelectTimeSheetReport.clearSelections();" />
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
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button ID="Button3" runat="server" Text="Bảng chấm công" Icon="UserTick">
                                                <Menu>
                                                    <ext:Menu ID="menu1" runat="server">
                                                        <Items>
                                                            <ext:MenuItem ID="MenuItem1" runat="server" Text="Chọn bảng chấm công" Icon="VcardKey">
                                                                <Listeners>
                                                                    <Click Handler="wdSelectTimeSheetReport.show();storeSelectTimeSheetReport.reload();" />
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem ID="MenuItem2" runat="server" Text="Quản lý bảng chấm công" Icon="Table">
                                                                <Listeners>
                                                                    <Click Handler="wdTimeSheetManage.show();" />
                                                                </Listeners>
                                                            </ext:MenuItem>

                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer4" runat="server" Width="10" />

                                           <%-- <ext:ComboBox runat="server" ID="cbxDepartment" FieldLabel="Chọn đơn vị"
                                                LabelWidth="70" Width="300" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                                ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="false" StoreID="storeDepartment">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Template runat="server">
                                                    <Html>
                                                        <tpl for=".">
						                                    <div class="list-item">
						                                        {Name} 
						                                    </div>
					                                    </tpl>
                                                    </Html>
                                                </Template>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cbxDepartment.getValue()); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                                                </Listeners>
                                            </ext:ComboBox>--%>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="5" />
                                            <ext:Container runat="server" ID="ctn111" Layout="FormLayout" LabelWidth="65">
                                                <Items>
                                                   <%-- <ext:ComboBox runat="server" ID="cbxMonth" Width="80" Editable="false" FieldLabel="Chọn tháng">
                                                        <Items>
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
                                                        </Items>
                                                        <Listeners>
                                                            <Select Handler="hdfMonth.setValue(cbxMonth.getValue());Ext.net.DirectMethods.CreateTimeSheet();PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                        </Listeners>
                                                    </ext:ComboBox>--%>
                                                </Items>
                                            </ext:Container>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer6" runat="server" Width="5" />
                                            <ext:Container runat="server" ID="Container2" Layout="FormLayout" LabelWidth="60">
                                                <Items>
                                                    <%--<ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="55">
                                                        <Listeners>
                                                            <Spin Handler="hdfYear.setValue(spnYear.getValue());Ext.net.DirectMethods.CreateTimeSheet();PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                        </Listeners>
                                                    </ext:SpinnerField>--%>
                                                </Items>
                                            </ext:Container>

                                            <ext:ToolbarSpacer ID="ToolbarSpacer3" runat="server" Width="10" />
                                            <ext:Button runat="server" ID="btnLockTimeSheet" Text="Khóa bảng công" Icon="Lock"
                                                        Hidden="False">
                                                <DirectEvents>
                                                    <Click OnEvent="BtnLockTimeSheetClick">
                                                        <EventMask ShowMask="true" Msg="Đang khóa bảng công. Vui lòng đợi..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnOpenTimeSheet" runat="server" Text="Mở bảng công" Icon="LockOpen"
                                                        Hidden="true">
                                                <DirectEvents>
                                                    <Click OnEvent="BtnOpenTimeSheetClick">
                                                        <EventMask ShowMask="true" Msg="Đang mở khóa bảng công. Vui lòng đợi..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>

                                            <ext:ToolbarFill runat="server" ID="tbf" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập mã, họ tên CCVC">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
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
                                    <ext:Store runat="server" ID="storeTimeSheet">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/HandlerTimeSheetAutomatic.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="searchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentId.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="numDayOfMonth" Value="hdfNumDayOfMonth.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="year" Value="hdfYear.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="month" Value="hdfMonth.getValue()" Mode="Raw" />
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
                                                    <ext:RecordField Name="TotalActual" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalHolidayL" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalPaidLeaveT" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalUnpaidLeaveP" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalUnleaveK" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalLateM" DefaultValue="0" />
                                                    <ext:RecordField Name="TotalGoWorkC" DefaultValue="0" />
                                                    <ext:RecordField Name="Day1" />
                                                    <ext:RecordField Name="Day2" />
                                                    <ext:RecordField Name="Day3" />
                                                    <ext:RecordField Name="Day4" />
                                                    <ext:RecordField Name="Day5" />
                                                    <ext:RecordField Name="Day6" />
                                                    <ext:RecordField Name="Day7" />
                                                    <ext:RecordField Name="Day8" />
                                                    <ext:RecordField Name="Day9" />
                                                    <ext:RecordField Name="Day10" />
                                                    <ext:RecordField Name="Day11" />
                                                    <ext:RecordField Name="Day12" />
                                                    <ext:RecordField Name="Day13" />
                                                    <ext:RecordField Name="Day14" />
                                                    <ext:RecordField Name="Day15" />
                                                    <ext:RecordField Name="Day16" />
                                                    <ext:RecordField Name="Day17" />
                                                    <ext:RecordField Name="Day18" />
                                                    <ext:RecordField Name="Day19" />
                                                    <ext:RecordField Name="Day20" />
                                                    <ext:RecordField Name="Day21" />
                                                    <ext:RecordField Name="Day22" />
                                                    <ext:RecordField Name="Day23" />
                                                    <ext:RecordField Name="Day24" />
                                                    <ext:RecordField Name="Day25" />
                                                    <ext:RecordField Name="Day26" />
                                                    <ext:RecordField Name="Day27" />
                                                    <ext:RecordField Name="Day28" />
                                                    <ext:RecordField Name="Day29" />
                                                    <ext:RecordField Name="Day30" />
                                                    <ext:RecordField Name="Day31" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:LockingGridView runat="server"/>
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn ColumnID="STT" Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="220" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Số hiệu CBCC" Width="100" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="TotalActual" Header="Tổng số ngày công thực tế" Width="150" Align="Left" DataIndex="TotalActual">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalHolidayL" Header="Tổng nghỉ lễ (L)" Width="150" Align="Left"  DataIndex="TotalHolidayL">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalPaidLeaveT" Header="Tổng số ngày nghỉ phép hưởng lương (P)" Width="150" Align="Left" DataIndex="TotalPaidLeaveT">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalUnpaidLeaveP" Header="Tổng số ngày nghỉ phép không hưởng lương (R)" Width="150" Align="Left" DataIndex="TotalUnpaidLeaveP">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalGoWorkC" Header="Tổng công tác (C)" Align="Left" DataIndex="TotalGoWorkC">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalUnleaveK" Header="Tổng công nghỉ không phép (K)" Width="150" Align="Left" DataIndex="TotalUnleaveK">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalLateM" Header="Tổng số ngày đi trễ (M)" Width="150" Align="Left" DataIndex="TotalLateM">
                                            <Renderer Fn="RenderTotal" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day1" Header="Day 1" Width="100" Align="Left" DataIndex="Day1">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day2" Header="Day 2" Width="100" Align="Left" DataIndex="Day2">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day3" Header="Day 3" Width="100" Align="Left" DataIndex="Day3">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day4" Header="Day 4" Width="100" Align="Left" DataIndex="Day4">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day5" Header="Day 5" Width="100" Align="Left"  DataIndex="Day5">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day6" Header="Day 6" Width="100" Align="Left" DataIndex="Day6">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day7" Header="Day 7" Width="100" Align="Left" DataIndex="Day7">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day8" Header="Day 8" Width="100" Align="Left" DataIndex="Day8">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day9" Header="Day 9" Width="100" Align="Left" DataIndex="Day9">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day10" Header="Day 10" Width="100" Align="Left" DataIndex="Day10">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day11" Header="Day 11" Width="100" Align="Left" DataIndex="Day11">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day12" Header="Day 12" Width="100" Align="Left" DataIndex="Day12">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day13" Header="Day 13" Width="100" Align="Left" DataIndex="Day13">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day14" Header="Day 14" Width="100" Align="Left" DataIndex="Day14">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day15" Header="Day 15" Width="100" Align="Left" DataIndex="Day15">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day16" Header="Day 16" Width="100" Align="Left" DataIndex="Day16">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day17" Header="Day 17" Width="100" Align="Left" DataIndex="Day17">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day18" Header="Day 18" Width="100" Align="Left" DataIndex="Day18">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day19" Header="Day 19" Width="100" Align="Left" DataIndex="Day19">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day20" Header="Day 20" Width="100" Align="Left" DataIndex="Day20">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day21" Header="Day 21" Width="100" Align="Left" DataIndex="Day21">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day22" Header="Day 22" Width="100" Align="Left" DataIndex="Day22">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day23" Header="Day 23" Width="100" Align="Left" DataIndex="Day23">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day24" Header="Day 24" Width="100" Align="Left" DataIndex="Day24">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day25" Header="Day 25" Width="100" Align="Left" DataIndex="Day25">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day26" Header="Day 26" Width="100" Align="Left" DataIndex="Day26">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day27" Header="Day 27" Width="100" Align="Left" DataIndex="Day27">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day28" Header="Day 28" Width="100" Align="Left" DataIndex="Day28">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day29" Header="Day 29" Width="100" Align="Left" DataIndex="Day29">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day30" Header="Day 30" Width="100" Align="Left" DataIndex="Day30">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Day31" Header="Day 31" Width="100" Align="Left" DataIndex="Day31">
                                            <Renderer Fn="RenderDay" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowDblClick Handler="" />
                                    <CellDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfTimeSheetCode.setValue(RowSelectionModel1.getSelected().get('TimeSheetCode')); hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'))" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
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



