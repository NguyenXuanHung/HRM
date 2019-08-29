<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.Management.AnnualLeaveConfigureManagement" Codebehind="AnnualLeaveConfigureManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">
        
        var checkInputAnnualLeave = function () {
            if (txtAnnualLeaveDay.getValue() === '' || txtAnnualLeaveDay.getValue().trim === '') {
                alert('Bạn chưa nhập số ngày phép mặc định.');
                return false;
            }
            if (dfExpiredDate.getValue() == '' || dfExpiredDate.getValue() == null) {
                alert('Bạn chưa nhập thời hạn sử dụng phép năm trước.');
                return false;
            }

            if (grp_ListEmployee.getSelectionModel().getCount() == 0 && grp_ListEmployee.disable == false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
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
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
       
            <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridAnnualLeave" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdAnnualLeave.show(); wdAnnualLeave.setTitle('Tạo mới cấu hình ngày phép');btnUpdate.hide();btnUpdateClose.show();
                                                        ctnEmployee.show();grp_ListEmployee.show();txtFullName.hide();
                                                        Ext.net.DirectMethods.ResetForm();storeEmployee.reload();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditAnnualLeave_Click">
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
                                            
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập mã, họ tên CCVC">
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
                                    <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/TimeSheet/HandlerAnnualLeaveConfigure.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId"/>
                                                    <ext:RecordField Name="UsedLeaveDay"/>
                                                    <ext:RecordField Name="RemainLeaveDay"/>
                                                    <ext:RecordField Name="AnnualLeaveDay" />
                                                    <ext:RecordField Name="DayAddedStep" />
                                                    <ext:RecordField Name="YearStep" />
                                                    <ext:RecordField Name="AllowUseFirstYear" />
                                                    <ext:RecordField Name="AllowUsePreviousYear" />
                                                    <ext:RecordField Name="ExpiredDate" />
                                                    <ext:RecordField Name="MaximumPerMonth" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="150" Align="Left" Locked="true" DataIndex="FullName" />  
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="120" Align="Left" Locked="true" DataIndex="EmployeeCode" />  
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="200" Align="Left" Locked="true" DataIndex="DepartmentName" />  
                                        <ext:Column ColumnID="AnnualLeaveDay" Header="Số ngày phép mặc định" Width="150" Align="Left" Locked="true" DataIndex="AnnualLeaveDay" />  
                                        <ext:Column ColumnID="UsedLeaveDay" Header="Số ngày phép đã sử dụng" Width="150" Align="Left" Locked="true" DataIndex="UsedLeaveDay" />
                                        <ext:Column ColumnID="RemainLeaveDay" Header="Số ngày phép còn lại" Width="150" Align="Left" Locked="true" DataIndex="RemainLeaveDay" />
                                        <ext:Column ColumnID="YearStep" Header="Sau số năm được tăng thêm ngày phép" Width="250" Align="Left" Locked="true" DataIndex="YearStep" />
                                        <ext:Column ColumnID="DayAddedStep" Header="Số ngày phép cộng thêm" Width="170" DataIndex="DayAddedStep" />
                                        <ext:Column ColumnID="AllowUseFirstYear" Header="Cho sử dụng phép năm đầu tiên" Width="200" DataIndex="AllowUseFirstYear">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="AllowUsePreviousYear" Header="Cho sử dụng phép năm trước" Width="200" DataIndex="AllowUsePreviousYear">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="MaximumPerMonth" Header="Số ngày phép tối đa trên tháng" Width="200" DataIndex="MaximumPerMonth" />

                                        <ext:DateColumn ColumnID="ExpiredDate" Header="Thời hạn sử dụng phép năm trước" Width="200" DataIndex="ExpiredDate" Format="dd/MM/yyyy" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id'));btnEdit.enable();btnDelete.enable(); " />
                                            <RowDeselect Handler="hdfRecordId.reset();hdfKeyRecord.reset(); btnEdit.disable();btnDelete.disable();" />
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
                                            <Change Handler="RowSelectionModel1.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server" Title="Tạo mới cấu hình ngày phép" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdAnnualLeave"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" Height="250" LabelWidth="230">
                        <Items>
                            <ext:TextField runat="server" ID="txtFullName" FieldLabel="Họ và tên" ReadOnly="True" AnchorHorizontal="100%" Hidden="True"/>
                            <ext:TextField runat="server" ID="txtAnnualLeaveDay" FieldLabel="Số ngày phép mặc định"
                                AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" CtCls="requiredData" />
                            <ext:TextField runat="server" ID="txtUsedLeaveDay" FieldLabel="Số ngày phép đã sử dụng"
                                           AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:TextField runat="server" ID="txtRemainLeaveDay" FieldLabel="Số ngày phép còn lại"
                                           AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:TextField runat="server" ID="txtYearStep" FieldLabel="Sau số năm được tăng thêm ngày phép"
                                AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:TextField runat="server" ID="txtDayAddedStep" FieldLabel="Số ngày phép tăng thêm"
                                AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:Checkbox runat="server" FieldLabel="Sử dụng phép năm đầu" BoxLabel="Được sử dụng" ID="chk_AllowUseFirstYear" />
                            <ext:Checkbox runat="server" FieldLabel="Sử dụng phép năm trước" BoxLabel="Được sử dụng" ID="chk_AllowUsePreviousYear" />
                            <ext:TextField runat="server" ID="txtMaximumPerMonth" FieldLabel="Số ngày phép tối đa trên tháng"
                                AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:DateField ID="dfExpiredDate" runat="server" Vtype="daterange" FieldLabel="Thời hạn sử dụng phép năm trước"
                                AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" CtCls="requiredData"/>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" ID="ctnEmployee" Layout="BorderLayout" Height="350">
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
                                    <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="100" DataIndex="EmployeeCode" />
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
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputAnnualLeave();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdateClose" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputAnnualLeave();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAnnualLeave.hide();#{chkEmployeeRowSelection}.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

