<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetListManagement.aspx.cs" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetListManagement" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<%@ Register Src="/Modules/UC/ChooseEmployee.ascx" TagName="ChooseEmployee" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">

        var checkInputTimeSheetReport = function () {
            if (txtName.getValue() == "" || txtName.getValue() == null) {
                alert('Bạn chưa nhập tiêu đề bảng chấm công!');
                return false;
            }
            if (EmployeeGrid.getSelectionModel().getCount() == 0 && EmployeeGrid.disable == false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }
        }
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
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
        var enterKeyToSearch = function (f, e) {
            try {
                this.triggers[0].show();
                if (e.getKey() === e.ENTER) {
                    store3.reload();
                }
                if (txtFullName.getValue() === '') {
                    this.triggers[0].hide();
                }
            } catch (e) {

            }
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
            <ext:Hidden runat="server" ID="hdfStartDate" />
            <ext:Hidden runat="server" ID="hdfEndDate" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfTimeSheetReportId" />
            <ext:Hidden runat="server" ID="hdfDepartmentId" />
            <ext:Hidden runat="server" ID="hdfMenuId" />
            <ext:Hidden runat="server" ID="hdfTypeTimeSheet" />
            <ext:Hidden runat="server" ID="hdfTimeSheetHandlerType" />

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

            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel
                                runat="server"
                                ID="gridTimeSheetList"
                                TrackMouseOver="true"
                                StripeRows="true"
                                Border="false"
                                Layout="Fit">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Icon="Add" Text="Thêm">
                                                <Listeners>
                                                    <Click Handler="#{chkEmployeeRowSelection}.clearSelections();Ext.net.DirectMethods.ReloadForm();stDepartmentList.reload();"></Click>
                                                </Listeners>
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
                                                <Listeners>
                                                    <Click Handler="#{chkEmployeeRowSelection}.clearSelections();"></Click>
                                                </Listeners>
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
                                            <ext:Button ID="btnConfig" runat="server" Text="Chi tiết" Icon="Table" Disabled="True">
                                                <Listeners>
                                                    <Click Handler=" return CheckSelectedRows(gridTimeSheetList);btnConfig.show();"></Click>
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="SelectTimeSheetReport_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
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
                                    <ext:Store runat="server" ID="storeTimeSheetList" AutoSave="true">
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="0" Mode="Raw" />
                                            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
                                        </AutoLoadParams>
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetReportList" />
                                            <ext:Parameter Name="SearchKey" Value="txtKeyword.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="StartDate" />
                                                    <ext:RecordField Name="EndDate" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" />
                                        <ext:Column ColumnID="Name" Header="Tên bảng chấm công" Width="300" DataIndex="Name" />
                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Từ ngày" Width="120" DataIndex="StartDate" Align="Center" />
                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Đến ngày" Width="120" DataIndex="EndDate" Align="Center" />
                                        <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo" Width="80" DataIndex="CreatedDate" />
                                    </Columns>
                                </ColumnModel>

                                <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                        <Listeners>
                                            <RowSelect Handler="hdfTimeSheetReportId.setValue(RowSelectionModel1.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnConfig.enable();" />
                                            <RowDeselect Handler="hdfTimeSheetReportId.reset();btnEdit.disable();btnDelete.disable();" />
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

            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdTimeSheet" Constrain="true"
                Title="Tạo bảng chấm công" Icon="DateAdd" Layout="FormLayout" Width="800" Height="600" Padding="5">
                <Items>
                    <ext:Container runat="server" ID="containerTime" Layout="Form" Height="600" LabelWidth="200">
                        <Items>
                            <ext:TextField ID="txtName" BlankText="Bạn bắt buộc phải nhập bảng chấm công" CtCls="requiredData"
                                AllowBlank="false" AnchorHorizontal="100%" FieldLabel="Tiêu đề bảng chấm công"
                                runat="server" />
                            <ext:DateField runat="server" ID="dfFromDate" AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Vtype="daterange" EmptyText="Từ ngày" FieldLabel="Từ ngày"
                                Format="dd/MM/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                <CustomConfig>
                                    <ext:ConfigItem Name="endDateField" Value="#{dfToDate}" Mode="Value"></ext:ConfigItem>
                                </CustomConfig>
                                <Listeners>
                                    <KeyUp Fn="onKeyUp" />
                                    <Select Handler="this.triggers[0].show();" />
                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfToDate.setMinValue();}" />
                                </Listeners>
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                            </ext:DateField>
                            <ext:DateField ID="dfToDate" runat="server" EmptyText="Đến ngày" FieldLabel="Đến ngày"
                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Vtype="daterange" Width="140"
                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                <CustomConfig>
                                    <ext:ConfigItem Name="startDateField" Value="#{dfFromDate}" Mode="Value"></ext:ConfigItem>
                                </CustomConfig>
                                <Listeners>
                                    <KeyUp Fn="onKeyUp" />
                                    <Select Handler="this.triggers[0].show();" />
                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfFromDate.setMaxValue();}" />
                                </Listeners>
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="cbDepartmentList" FieldLabel="Chọn phòng ban" CtCls="requiredData"
                                DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                                AnchorHorizontal="100%" TabIndex="33" Editable="false" AllowBlank="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template ID="Template37" runat="server">
                                    <Html>
                                        <tpl for=".">
						                            <div class="list-item"> 
                                                    {Name}
						                            </div>
					                            </tpl>
                                    </Html>
                                </Template>
                                <Store>
                                    <ext:Store runat="server" ID="stDepartmentList" AutoLoad="false" OnRefreshData="stDepartmentList_OnRefreshData">
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <Expand Handler="if(#{stDepartmentList}.getCount()==0) #{stDepartmentList}.reload();" />
                                    <Select Handler="this.triggers[0].show();#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();#{Store3}.reload();"></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{Store3}.reload()}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Container ID="Container1" runat="server" AutoHeight="True" Layout="FormLayout">
                                <Items>
                                    <ext:GridPanel runat="server" ID="EmployeeGrid" Icon="UserAdd" Header="true" Title="Chọn nhân viên"
                                        AutoExpandColumn="HoVaTen" AnchorHorizontal="100%" Height="420">
                                        <SelectionModel>
                                            <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                                        </SelectionModel>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn Width="20" Header="STT" />
                                                <ext:Column Header="Mã CB" Width="60" DataIndex="EmployeeCode">
                                                </ext:Column>
                                                <ext:Column Header="Họ Tên" Width="100" ColumnID="HoVaTen" DataIndex="FullName">
                                                </ext:Column>
                                                <ext:DateColumn Header="Ngày sinh" Width="110" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                                <ext:Column Header="Giới tính" DataIndex="SexName">
                                                </ext:Column>
                                                <ext:Column Header="Bộ phận" DataIndex="DepartmentName">
                                                </ext:Column>
                                                <ext:Column Header="Chức vụ" DataIndex="PositionName">
                                                </ext:Column>
                                                <ext:Column Header="Chức danh" DataIndex="JobTitleName">
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <Store>
                                            <ext:Store ID="Store3" ShowWarningOnFailure="false" runat="server" AutoLoad="True">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                </Proxy>
                                                <AutoLoadParams>
                                                    <ext:Parameter Name="start" Value="={0}" />
                                                    <ext:Parameter Name="limit" Value="={20}" />
                                                </AutoLoadParams>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="UcChooseEmployee" />
                                                    <ext:Parameter Name="Department" Value="#{cbDepartmentList}.getValue()" Mode="Raw" />
                                                    <ext:Parameter Name="SearchEmployee" Value="#{txtFullName}.getValue()" Mode="Raw" />
                                                    <ext:Parameter Name="Filter" Value="#{cbxWorkStatus}.getValue()" Mode="Raw" />
                                                    <ext:Parameter Name="Position" Value="#{filterChucVu}.getValue()" Mode="Raw" />
                                                    <ext:Parameter Name="JobTitle" Value="#{filterCongViec}.getValue()" Mode="Raw" />
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
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetReport();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnUpdate" runat="server" Icon="Accept" Text="Cập nhật">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetReport();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate" Timeout="200000">
                                <EventMask ShowMask="true" Msg="Đang tạo bảng chấm công..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnCloseTimeSheetReport" runat="server" Icon="Decline" Text="Đóng lại">
                        <Listeners>
                            <Click Handler="wdTimeSheet.hide();#{chkEmployeeRowSelection}.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
