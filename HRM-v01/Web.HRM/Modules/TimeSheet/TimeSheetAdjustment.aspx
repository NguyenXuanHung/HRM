<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetAdjustment" CodeBehind="TimeSheetAdjustment.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />

    <script type="text/javascript">
        var ResetwdAdjustment = function () {
            chkSymbolRowSelection.clearSelections();

            storeEmployee.removeAll();
        }

        var CheckInputHL = function () {
          
            if (gridListEmployee.getSelectionModel().getCount() == 0) {
                alert("Bạn phải chọn ít nhất một nhân viên");
                return false;
            }
            if (grpWorkShift.getSelectionModel().getCount() == 0) {
                alert("Bạn phải chọn ít nhất một ca");
                return false;
            }
            if (gridSymbol.getSelectionModel().getCount() == 0) {
                alert("Bạn phải chọn ít nhất một ký hiệu");
                return false;
            }
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

        var keyPresstxtSearch2 = function (f, e) {
            if (e.getKey() === e.ENTER) {
                PagingToolbar3.pageIndex = 0;
                PagingToolbar3.doLoad();
                chkEmployeeRowSelection.clearSelections();
                if (this.getValue() === '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
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
            <ext:Hidden runat="server" ID="hdfMenuID" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfTypeTimeSheet" />
            <ext:Hidden runat="server" ID="hdfTimeSheetEventIds" />
            <ext:Hidden runat="server" ID="hdfGroupSymbol" />
            <ext:Hidden runat="server" ID="hdfTimeSheetHandlerType" />
            <ext:Hidden runat="server" ID="hdfAdjustTimeSheetHandlerType" />

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

            <!-- store chức vụ -->
            <ext:Store ID="storePosition" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Position" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTimeAdjust" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="Ext.net.DirectMethods.ResetForm();wdAdjustment.show();storeEmployee.reload();" />
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
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarSpacer Width="10" />
                                            <ext:ToolbarFill />
                                            <ext:Button ID="Button1" runat="server" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="200"
                                                EmptyText="Nhập mã nhân viên hoặc họ tên ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Hidden runat="server" ID="hdfGroupWorkShiftSearch"></ext:Hidden>
                                            <ext:ComboBox runat="server" ID="cbxGroupWorkShiftSearch" DisplayField="Name" EmptyText="Chọn nhóm phân ca"
                                                MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                Width="150" ItemSelector="div.list-item">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Template ID="Template3" runat="server">
                                                    <Html>
                                                        <tpl for=".">
                                                        <div class="list-item"> 
                                                            {Name}
                                                        </div>
                                                    </tpl>
                                                    </Html>
                                                </Template>
                                                <Store>
                                                    <ext:Store ID="store1" AutoSave="true" runat="server">
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
                                                    <Select Handler="this.triggers[0].show();hdfGroupWorkShiftSearch.setValue(cbxGroupWorkShiftSearch.getValue());gridTimeAdjust.reload()" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupWorkShiftSearch.reset();};gridTimeAdjust.reload()" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:DateField ID="dfFromDateSearch" runat="server" Width="140" EmptyText="Từ ngày"
                                                AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Vtype="daterange"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfToDateSearch}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfToDateSearch.setMinValue(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();}" />
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
                                                    <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfFromDateSearch.setMaxValue(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();}" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store runat="server" ID="storeAdjustment" GroupField="FullName">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetEvent" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelected.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="adjustTimeSheetHandlerType" Value="hdfAdjustTimeSheetHandlerType.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="groupWorkShiftId" Value="hdfGroupWorkShiftSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="fromDate" Value="dfFromDateSearch.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="toDate" Value="dfToDateSearch.getRawValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="StartDate" />
                                                    <ext:RecordField Name="EndDate" />
                                                    <ext:RecordField Name="SymbolCode" />
                                                    <ext:RecordField Name="SymbolColor" />
                                                    <ext:RecordField Name="SymbolName" />
                                                    <ext:RecordField Name="WorkShiftName" />
                                                    <ext:RecordField Name="GroupWorkShiftName" />
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
                                <View>
                                    <ext:GroupingView ID="GroupingView2" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" Locked="true" />
                                        <ext:Column Header="Mã nhân viên" Width="85" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:GroupingSummaryColumn ColumnID="FullName" DataIndex="FullName" Header="Họ tên" Width="200" Sortable="true" Hideable="false" SummaryType="Count" />
                                        <ext:Column Header="Phòng ban" Width="250" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column Header="Nhóm ca" Width="100" Align="Left" DataIndex="GroupWorkShiftName" />
                                        <ext:Column Header="Tên ca" Width="100" DataIndex="WorkShiftName" />
                                        <ext:DateColumn DataIndex="StartDate" Header="Từ ngày" Format="dd/MM/yyyy" Align="Center" />
                                        <ext:DateColumn DataIndex="EndDate" Header="Đến ngày" Format="dd/MM/yyyy" Align="Center" />
                                        <ext:DateColumn Header="Ngày hiệu chỉnh" Width="150" Align="Center" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Ký hiệu" Width="50" Align="Center" DataIndex="SymbolCode">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column Header="Tên ký hiệu" DataIndex="SymbolName" Width="150" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1" SingleSelect="True">
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
            <ext:Window runat="server" ID="wdAdjustment" Constrain="true" Modal="true" Title="Hiệu chỉnh chấm công"
                Icon="UserAdd" Layout="FormLayout" Resizable="True" Height="600" Width="800"
                Hidden="true" Padding="6">
                <Items>
                    <ext:Container ID="Container7" runat="server" Layout="Column" Height="160">
                        <Items>
                            <ext:Container ID="Container8" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                LabelWidth="150">
                                <Items>
                                    <ext:TriggerField runat="server" EnableKeyEvents="true" ID="txtSearchEmployee" FieldLabel="Tìm kiếm" AnchorHorizontal="100%" EmptyText="Nhập mã nhân viên hoặc họ tên">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="keyPresstxtSearch2"></KeyPress>
                                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); #{PagingToolbar3}.pageIndex=0;#{PagingToolbar3}.doLoad(); }" />
                                        </Listeners>
                                    </ext:TriggerField>                                   
                                    <ext:DateField runat="server" ID="dfFromDate" AnchorHorizontal="100%" FieldLabel="Từ ngày" CtCls="requiredData" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                        <Listeners>
                                            <Select Handler="#{storeWorkShift}.reload();#{storeEmployee}.reload()"></Select>
                                        </Listeners>
                                    </ext:DateField>
                                    <ext:DateField runat="server" ID="dfToDate" AnchorHorizontal="100%" FieldLabel="Đến ngày" CtCls="requiredData" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                        <Listeners>
                                            <Select Handler="#{storeWorkShift}.reload();#{storeEmployee}.reload()"></Select>
                                        </Listeners>
                                    </ext:DateField>
                                    <ext:Hidden runat="server" ID="hdfGroupWorkShift" />
                                    <ext:ComboBox runat="server" ID="cbxGroupWorkShift" FieldLabel="Chọn nhóm phân ca"
                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item">
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
                                            <Select Handler="this.triggers[0].show();hdfGroupWorkShift.setValue(cbxGroupWorkShift.getValue());#{storeWorkShift}.reload();#{storeEmployee}.reload()" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupWorkShift.reset(); #{storeWorkShift}.reload()};#{storeEmployee}.reload()" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:Hidden runat="server" ID="hdfDepartment" />
                                    <ext:Hidden runat="server" ID="hdfRecordIds" />
                                    <ext:ComboBox runat="server" ID="cbxDepartments" FieldLabel="Chọn phòng ban"
                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" StoreID="storeDepartment">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template ID="Template2" runat="server">
                                            <Html>
                                                <tpl for=".">
                                                <div class="list-item"> 
                                                    {Name}
                                                </div>
                                            </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();hdfDepartment.setValue(cbxDepartments.getValue());#{storeEmployee}.reload()" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfDepartment.reset();#{storeEmployee}.reload() };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:Button ID="Button3" runat="server" Text="Tìm kiếm" Icon="Zoom">
                                        <Listeners>
                                            <Click Handler="#{PagingToolbar3}.pageIndex=0;#{PagingToolbar3}.doLoad();" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="175">
                        <Items>
                            <ext:Container ID="Container6" runat="server" LabelAlign="left" Layout="FormLayout" ColumnWidth=".5"
                                LabelWidth="120">
                                <Items>
                                    <ext:GridPanel ID="gridSymbol" runat="server" AnchorHorizontal="100%" Height="175" AutoExpandColumn="Name"
                                        Title="Danh sách ký hiệu chấm công" Border="true" ClicksToEdit="1" Icon="UserTick">
                                        <Store>
                                            <ext:Store ID="storeSymbol" AutoSave="true" runat="server" GroupField="GroupSymbolName" AutoLoad="True">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
                                                    <ext:Parameter Name="group" Value="hdfGroupSymbol.getValue()" Mode="Raw" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Name" />
                                                            <ext:RecordField Name="Description" />
                                                            <ext:RecordField Name="Code" />
                                                            <ext:RecordField Name="GroupSymbolName" />
                                                            <ext:RecordField Name="Group" />
                                                            <ext:RecordField Name="Color" />
                                                            <ext:RecordField Name="SymbolDisplay" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <View>
                                            <ext:GroupingView ID="GroupingView3" runat="server" ForceFit="false" MarkDirty="false"
                                                ShowGroupName="false" EnableNoGroups="true" />
                                        </View>
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                <ext:Column Header="Ký hiệu" DataIndex="Code" Width="50">
                                                    <Renderer Fn="RenderSymbol" />
                                                </ext:Column>
                                                <ext:Column ColumnID="Name" Header="Tên ký hiệu"
                                                    DataIndex="Name" Width="200" />
                                                <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" Hidden="True">
                                                    <Renderer Fn="RenderGroupSymbol" />
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>

                                        <SelectionModel>
                                            <ext:CheckboxSelectionModel ID="chkSymbolRowSelection" runat="server" SingleSelect="false">
                                            </ext:CheckboxSelectionModel>
                                        </SelectionModel>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar4" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                                PageSize="20" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                                DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                            </ext:PagingToolbar>
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container9" runat="server" LabelAlign="left" Layout="FormLayout" ColumnWidth=".5"
                                LabelWidth="120">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfWorkShifts"></ext:Hidden>
                                    <ext:GridPanel runat="server" ID="grpWorkShift" AnchorHorizontal="100%" Height="175" AutoExpandColumn="Name"
                                        Title="Bảng phân ca" Border="True" ClicksToEdit="1">
                                        <Store>
                                            <ext:Store runat="server" ID="storeWorkShift" AutoSave="True" AutoLoad="True">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="WorkShift" />
                                                    <ext:Parameter Name="fromDate" Value="dfFromDate.getRawValue()" Mode="Raw" />
                                                    <ext:Parameter Name="toDate" Value="dfToDate.getRawValue()" Mode="Raw" />
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
                                        <ColumnModel ID="ColumnModel2" runat="server">
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
                                            <ext:PagingToolbar ID="PagingToolbar2" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                                PageSize="20" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                                DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                            </ext:PagingToolbar>
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container5" runat="server" Layout="Column" Height="175">
                        <Items>
                            <ext:GridPanel runat="server" ID="gridListEmployee" TrackMouseOver="true" Title="Danh sách cán bộ"
                                Border="true" Icon="User" AnchorHorizontal="100%" AutoExpandColumn="DepartmentName" Height="200">
                                <Store>
                                    <ext:Store ID="storeEmployee" AutoLoad="False" runat="server" ShowWarningOnFailure="false"
                                        SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="false">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/TimeSheet/HandlerEmployeeWorkShift.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="SearchKey" Value="txtSearchEmployee.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="groupWorkShiftId" Value="hdfGroupWorkShift.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartment.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="PositionName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="80" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="FullName" Header="Họ tên" Width="200" DataIndex="FullName" />
                                        <ext:Column ColumnID="PositionName" Header="Chức vụ" Width="150" DataIndex="PositionName" />
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="250" DataIndex="DepartmentName">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar3" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                        PageSize="20" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                        DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateHL" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler=" return CheckInputHL();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnUpdateHL_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseHL" Text="Hủy" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAdjustment.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetwdAdjustment();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="wdUpdateAdjustment" Constrain="true" Modal="true" Title="Cập nhật hiệu chỉnh chấm công"
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
                                    <ext:TextArea runat="server" ID="txtReasonUpdate" FieldLabel="Lý do hiệu chỉnh" AnchorHorizontal="98%"
                                        Height="35" />
                                </Items>
                            </ext:Container>

                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="btnUpdateTimeSheet" runat="server" Hidden="false" Text="Lưu" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="btnUpdate_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button6" Text="Hủy" Icon="Decline">
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

