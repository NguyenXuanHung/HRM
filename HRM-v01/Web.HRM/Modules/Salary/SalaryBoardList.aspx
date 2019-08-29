<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryBoardList.aspx.cs" Inherits="Web.HRM.Modules.Salary.SalaryBoardList" %>

<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />
    <script src="../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        // validate form in setting window
        var validateForm = function () {
            if (hdfConfigId.getValue() == '' || hdfConfigId.getValue() == null) {
                alert('Bạn chưa chọn cấu hình cho bảng lương!');
                return false;
            }
            if (txtName.getValue() == '' || txtName.getValue() == null) {
                alert("Bạn chưa nhập tên bảng tính lương!");
                return false;
            }
            if (txtCode.getValue() == '' || txtCode.getValue() == null) {
                alert("Bạn chưa nhập mã bảng tính lương!");
                return false;
            }

            if (EmployeeGrid.getSelectionModel().getCount() === 0 && EmployeeGrid.disabled === false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }
            return true;
        };
        // render salary board info logo
        var renderBoard = function () {
            return "<div style='width:100%;heigh:100%;cursor:pointer;' onclick='Ext.net.DirectMethods.SelectSalaryBoard();'>" +
                "<img  src='/Resource/icon/table.png' >" +
                "</div>";
        };
        // render salary board dynamic column logo
        var renderDynamic = function () {
            return "<div style='width:100%;heigh:100%;cursor:pointer;' onclick='Ext.net.DirectMethods.SelectColumnDynamic();'>" +
                "<img  src='/Resource/icon/cog.png' >" +
                "</div>";
        };
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfSalaryBoardListId" />
            <ext:Hidden runat="server" ID="hdfType" />
            <ext:Hidden runat="server" ID="hdfChkIsUpdateSalary" />
            <ext:Hidden runat="server" ID="hdfChkIsUpdateTimeSheet" />
            <ext:Hidden runat="server" ID="hdfMenuId" />
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
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel
                                runat="server"
                                ID="grdSalaryBoardList"
                                TrackMouseOver="true"
                                StripeRows="true"
                                Border="false"
                                Layout="Fit">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Icon="Add" Text="Thêm">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Icon="Pencil" Text="Sửa" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Icon="Delete" Text="Xóa" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnCustom" Text="Tùy chỉnh" Icon="Cog">
                                                <Menu>
                                                    <ext:Menu ID="MenuCustom" runat="server">
                                                        <Items>
                                                            <ext:Checkbox runat="server" BoxLabel="Cập nhật lại dữ liệu chấm công" ID="chk_IsUpdateTimeSheet" Height="20">
                                                                <Listeners>
                                                                    <Check Handler="hdfChkIsUpdateTimeSheet.setValue(chk_IsUpdateTimeSheet.checked)" />
                                                                </Listeners>
                                                            </ext:Checkbox>
                                                            <ext:Checkbox runat="server" BoxLabel="Cập nhật lại dữ liệu lương" ID="chk_IsUpdateSalary" Height="20">
                                                                <Listeners>
                                                                    <Check Handler="hdfChkIsUpdateSalary.setValue(chk_IsUpdateSalary.checked)" />
                                                                </Listeners>
                                                            </ext:Checkbox>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm...">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();ReloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="ReloadGrid();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store runat="server" ID="storeSalaryBoard" AutoSave="true" GroupField="Year">
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="0" Mode="Raw" />
                                            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
                                        </AutoLoadParams>
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="SalaryBoardList" />
                                            <ext:Parameter Name="SearchKey" Value="txtKeyword.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Title" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Month" />
                                                    <ext:RecordField Name="Year" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" />
                                        <ext:Column ColumnID="Name" Header="Tên bảng lương" DataIndex="Title" Width="400">
                                        </ext:Column>
                                        <ext:Column ColumnID="Title" Header="Mã bảng lương" DataIndex="Code">
                                        </ext:Column>
                                        <ext:Column ColumnID="Month" Align="Center" Header="Tháng" DataIndex="Month" />
                                        <ext:Column ColumnID="Year" Align="Center" Header="Năm" DataIndex="Year" Hidden="True" />
                                        <ext:Column ColumnID="CreatedBy" Align="Center" Header="Người tạo" DataIndex="CreatedBy" />
                                        <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo" DataIndex="CreatedDate" />
                                        <ext:Column ColumnID="SalaryBoard" Align="Center" Width="150" Header="Bảng tính lương" DataIndex="">
                                            <Renderer Fn="renderBoard" />
                                        </ext:Column>
                                        <ext:Column ColumnID="DynamicColumn" Align="Center" Width="150" Header="Cấu hình cột động" DataIndex="">
                                            <Renderer Fn="renderDynamic" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>

                                <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                        <Listeners>
                                            <RowSelect Handler="hdfSalaryBoardListId.setValue(RowSelectionModel1.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="hdfSalaryBoardListId.reset();btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server"
                                        ID="PagingToolbar1"
                                        PageSize="30"
                                        DisplayInfo="true"
                                        DisplayMsg="Từ {0} - {1} / {2}"
                                        EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:Label runat="server" Text="Số bản ghi trên trang:" />
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="30" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize=parseInt(this.getValue());#{PagingToolbar1}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server"
                ID="wdSetting"
                Resizable="true"
                Layout="FormLayout"
                Padding="10"
                Width="800"
                Hidden="true"
                Height="600"
                Modal="true"
                Constrain="true">
                <Items>
                    <ext:Container ID="Container2" runat="server" Layout="Form" Height="600" MonitorResize="True"
                        LabelWidth="200">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfConfigId" />
                            <ext:ComboBox runat="server" ID="cbxConfigList" FieldLabel="Chọn bảng cấu hình"
                                CtCls="requiredData" DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%"
                                ItemSelector="div.list-item" Width="368">
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
                                <Store>
                                    <ext:Store runat="server" ID="store_cbxConfigList" AutoLoad="false">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="objname" Value="sal_PayrollConfig" Mode="Value" />
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
                                    <Expand Handler="store_cbxConfigList.reload();" />
                                    <Select Handler="this.triggers[0].show();hdfConfigId.setValue(cbxConfigList.getValue());" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfConfigId.reset();}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="hdfDepartmentId" />
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
                                    <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cbxDepartment.getValue());
                                txtName.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue()); #{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();#{storeEmployees}.reload();" />
                                    <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();#{storeEmployees}.reload();" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Container ID="Containerm" Height="27" runat="server" Layout="ColumnLayout">
                                <Items>
                                    <ext:CompositeField runat="server" ID="cf1" FieldLabel="Chọn tháng">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cbxMonth" Width="80" Editable="false" FieldLabel="Chọn tháng">
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
                                                    <Select Handler="hdfMonth.setValue(cbxMonth.getValue());
                                                txtName.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="55">
                                                <Listeners>
                                                    <Spin Handler="hdfYear.setValue(spnYear.getValue());
                                                txtName.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                                </Listeners>
                                            </ext:SpinnerField>
                                        </Items>
                                    </ext:CompositeField>
                                </Items>
                            </ext:Container>
                            <ext:TextField runat="server" ID="txtName" EmptyText="" FieldLabel="Tên bảng tính lương" CtCls="requiredData" AnchorHorizontal="98%" />
                            <ext:TextField runat="server" ID="txtCode" FieldLabel="Mã bảng lương" CtCls="requiredData" AnchorHorizontal="98%" />
                            <ext:TextField runat="server" ID="txtNote" FieldLabel="Mô tả" AnchorHorizontal="98%" />
                            <ext:GridPanel runat="server" ID="EmployeeGrid" Icon="UserAdd" Header="true" Title="Chọn nhân viên" MonitorResize="True"
                                AutoExpandColumn="FullName" AnchorHorizontal="100%" Height="360">
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                                </SelectionModel>
                                <ColumnModel runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Width="30" Header="STT" />
                                        <ext:Column Header="Mã CB" Width="60" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ Tên" Width="150" ColumnID="FullName" DataIndex="FullName" />
                                        <ext:DateColumn Header="Ngày sinh" Width="100" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Giới tính" DataIndex="SexName" Width="70" />
                                        <ext:Column Header="Bộ phận" DataIndex="DepartmentName" />
                                        <ext:Column Header="Chức vụ" DataIndex="PositionName" />
                                        <ext:Column Header="Chức danh" DataIndex="JobTitleName" />
                                    </Columns>
                                </ColumnModel>
                                <Store>
                                    <ext:Store ID="storeEmployees" ShowWarningOnFailure="false" runat="server" AutoLoad="True">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={20}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="UcChooseEmployee" />
                                            <ext:Parameter Name="Department" Value="#{cbxDepartment}.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="BirthDate" />
                                                    <ext:RecordField Name="SexName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="PositionName" />
                                                    <ext:RecordField Name="JobTitleName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                        <Listeners>
                                            <Load Handler="" />
                                        </Listeners>
                                    </ext:Store>
                                </Store>
                                <SaveMask ShowMask="true" Msg="Đang tải dữ liệu..." />
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
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validateForm();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang tải..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdSetting.hide();#{chkEmployeeRowSelection}.clearSelections(); Ext.net.DirectMethods.ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
