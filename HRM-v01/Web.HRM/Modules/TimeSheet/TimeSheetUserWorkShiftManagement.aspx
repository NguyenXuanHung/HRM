<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetUserWorkShiftManagement" Codebehind="TimeSheetUserWorkShiftManagement.aspx.cs" %>
<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />        
    <script type="text/javascript">
       
        var checkInputUserRule = function () {
            if (!cbxGroupWorkShift.getValue()) {
                alert('Bạn chưa chọn nhóm phân ca');
                return false;
            }

            if (grp_ListEmployee.getSelectionModel().getCount() == 0 && grp_ListEmployee.disable == false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }

            return true;
        }

        function ValidateInputTimeSheetAllEmployee() {
            if (!cbxGroupWorkShiftAll.getValue()) {
                alert('Bạn chưa chọn nhóm phân ca');
                return false;
            }
           
            return true;
        }

        var CheckUpdateInput = function() {
            if (!cbxUpdateGroupWorkShift.getValue()) {
                alert('Bạn chưa chọn nhóm phân ca');
                return false;
            }
            return true;
        }

        var reloadGrid = function () {
            gridUserRule.getSelectionModel().clearSelections();
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelected" />

            <ext:Store runat="server" ID="storeGroupWorkShift" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GroupWorkShift" />
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

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridUserRule" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdUserRule.show();storeEmployee.reload();#{chkEmployeeRowSelection}.clearSelections();
                                                        cbxGroupWorkShift.reset();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="if (CheckSelectedRows(gridUserRule) == false) {return false;}; " />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa thông tin này" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button ID="btnExtension" runat="server" Text="Tiện ích" Icon="Build">
                                                <Menu>
                                                    <ext:Menu ID="menuExtension" runat="server">
                                                        <Items>
                                                            <ext:MenuItem ID="itemExtension" runat="server" Text="Áp dụng cho tất cả nhân viên">
                                                                <Listeners>
                                                                    <Click Handler="wdTimeSheetAllEmployee.show();" />
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem ID="itemDeleteTimeSheet" runat="server" Text="Xóa thiết lập cho tất cả nhân viên">
                                                                <DirectEvents>
                                                                    <Click OnEvent="DeleteUserRule">
                                                                        <EventMask ShowMask="true" Msg="Đang xử lý dữ liệu. Vui lòng chờ..." />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:Hidden runat="server" ID="hdfGroupWorkShiftFilterId" />
                                            <ext:ComboBox runat="server" ID="cbxGroupWorkShiftFilter" EmptyText="Chọn bảng phân ca"
                                                          LabelWidth="150" Width="300" AllowBlank="True" DisplayField="Name" ValueField="Id"
                                                          ItemSelector="div.list-item" AnchorHorizontal="100%" Editable="true" PageSize="20"
                                                          StoreID="storeGroupWorkShift">
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
                                                    <Select Handler="this.triggers[0].show();hdfGroupWorkShiftFilterId.setValue(cbxGroupWorkShiftFilter.getValue());reloadGrid();"></Select>
                                                    <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();hdfGroupWorkShiftFilterId.reset(); reloadGrid();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập mã, họ tên CCVC">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();reloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeUserRule" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/TimeSheet/HandlerEmployeeWorkShift.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={20}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelected.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="groupWorkShiftId" Value="hdfGroupWorkShiftFilterId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="GroupWorkShiftName" />
                                                    <ext:RecordField Name="DepartmentId" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FullName" Header="Họ tên" Width="150" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="150" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="DepartmentName" Header="Tên đơn vị" Width="350" Align="Left" Locked="true" DataIndex="DepartmentName" />
                                        <ext:Column ColumnID="GroupWorkShiftName" Header="Tên bảng phân ca" Width="200" DataIndex="GroupWorkShiftName" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id'));" />
                                            <RowDeselect Handler="hdfKeyRecord.reset(); " />
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
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="15" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server" Title="Thêm mới phân ca nhân viên" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdUserRule"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfGroupWorkShift" />
                                    <ext:ComboBox runat="server" ID="cbxGroupWorkShift" FieldLabel="Nhóm phân ca"
                                        LabelWidth="152" Width="305" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                        ItemSelector="div.list-item" AnchorHorizontal="100%" Editable="true" CtCls="requiredData" PageSize="20"
                                        StoreID="storeGroupWorkShift">
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
                                            <Select Handler="this.triggers[0].show();hdfGroupWorkShift.setValue(cbxGroupWorkShift.getValue());"></Select>
                                            <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();hdfGroupWorkShift.reset(); }" />
                                        </Listeners>
                                    </ext:ComboBox>

                                    <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="350">
                                        <Items>
                                            <ext:GridPanel runat="server" ID="grp_ListEmployee" TrackMouseOver="true" Title="Danh sách cán bộ"
                                                StripeRows="true" Border="true" Region="Center" Icon="User" AutoExpandColumn="DepartmentName"
                                                AutoExpandMin="150">
                                                <Store>
                                                    <ext:Store ID="storeEmployee" AutoLoad="true" runat="server" >
                                                        <Proxy>
                                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="UcChooseEmployee" />
                                                        </BaseParams>
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
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
                                                        <ext:RowNumbererColumn Header="STT" Width="40" />
                                                        <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Width="100" DataIndex="EmployeeCode" />
                                                        <ext:Column ColumnID="FullName" Header="Họ tên" Width="200" DataIndex="FullName" />
                                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" DataIndex="DepartmentName">
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                                                </SelectionModel>
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar2" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                                                       PageSize="30" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                                                       DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateClose" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputUserRule();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdUserRule.hide();#{chkEmployeeRowSelection}.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="storeEmployee.removeAll();storeEmployee.reload();"></Hide>
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="wdUpdateUserRule" Constrain="true" Modal="true" Title="Cập nhật áp dụng cho nhân viên"
                Icon="Pencil" Layout="FormLayout" Resizable="false" AutoHeight="true" Width="650"
                Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="Column" Height="100">
                        <Items>
                            <ext:Container ID="Container4" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                                LabelWidth="120">
                                <Items>
                                    <ext:TextField runat="server" ID="txtFullName" FieldLabel="Họ và tên" ReadOnly="True" AnchorHorizontal="98%" />
                                    <ext:Hidden runat="server" ID="hdfUpdateGroupWorkShift" />
                                    <ext:ComboBox runat="server" ID="cbxUpdateGroupWorkShift" FieldLabel="Nhóm phân ca"
                                                  LabelWidth="152" Width="305" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                                  ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="true" CtCls="requiredData"
                                                  StoreID="storeGroupWorkShift">
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
                                            <Select Handler="this.triggers[0].show();hdfUpdateGroupWorkShift.setValue(cbxUpdateGroupWorkShift.getValue());"></Select>
                                            <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();hdfUpdateGroupWorkShift.reset(); }" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>

                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="btnUpdateUser" runat="server" Hidden="false" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler=" return CheckUpdateInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button6" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdUpdateUserRule.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Áp dụng cho tất cả nhân viên" Resizable="true" Layout="FormLayout"
                Padding="6" Width="650" Hidden="true" Icon="UserTick" ID="wdTimeSheetAllEmployee"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container runat="server" AnchorHorizontal="100%" Layout="FormLayout">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfGroupWorkShiftAll" />
                            <ext:ComboBox runat="server" ID="cbxGroupWorkShiftAll" FieldLabel="Nhóm phân ca"
                                LabelWidth="152" Width="305" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                ItemSelector="div.list-item" AnchorHorizontal="100%" Editable="true" CtCls="requiredData" StoreID="storeGroupWorkShift">
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
                                    <Select Handler="this.triggers[0].show();hdfGroupWorkShiftAll.setValue(cbxGroupWorkShiftAll.getValue());"></Select>
                                    <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();hdfGroupWorkShiftAll.reset(); }" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="Button1" Text="Đồng ý" Icon="Accept">
                        <Listeners>
                            <Click Handler="return ValidateInputTimeSheetAllEmployee();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnAccept_ClickAllEmployee">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button2" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetAllEmployee.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

