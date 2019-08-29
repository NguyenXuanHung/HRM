<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetCodeManagement" CodeBehind="TimeSheetCodeManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">
        var ResetWdImportExcelFile = function () {
            $('input[type=text]').val('');
            $('#textarea').val('');
            $('input[type=select]').val('');
            $('input[type=radio]').val('');
            $('input[type=checkbox]').val('');
        }

        var checkInputTimeSheetCode = function () {
            if (hdfTimeMachineId.getValue() === '' || hdfTimeMachineId.getValue().trim === '') {
                alert('Bạn chưa chọn số series máy chấm công!');
                return false;
            }
            if (txtTimeSheetCode.getValue() === '' || txtTimeSheetCode.getValue().trim === '') {
                alert('Bạn chưa nhập mã chấm công!');
                return false;
            }
            if (hdfEmployeeSelectedId.getValue() === '' || hdfEmployeeSelectedId.getValue().trim === '') {
                alert('Bạn chưa chọn cán bộ nào!');
                return false;
            }

            if (dfStartTime.getValue() === '' || dfStartTime.getValue() == null) {
                alert('Bạn chưa nhập thời gian bắt đầu!');
                return false;
            }
            if (ValidateDateField(dfStartTime) === false) {
                alert('Định dạng thời gian bắt đầu không đúng');
                return false;
            }

            return true;
        }

        var showResult = function (count, employeeCode, fullname) {
            Ext.Msg.confirm('Xác nhận', 'Bạn có muốn cập nhật nhân viên: ' + fullname + ', Mã nhân viên: ' + employeeCode, function (btn) {
                if (btn == "yes") {
                    Ext.net.DirectMethods.UpdateDuplicate(hdfTimeSheetCode.getValue())
                    Ext.net.DirectMethods.ContinueProcess(count);
                } else {
                    Ext.net.DirectMethods.ContinueProcess(count);
                }
            });
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
            <ext:Hidden runat="server" ID="hdfTimeSheetCode" />
            <ext:Hidden runat="server" ID="hdfDataTable" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />

            <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTimeSheetCode" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="AddTimeSheetCode_Click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditTimeSheetCode_Click">
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
                                            <ext:Button ID="btnAddImportExcel" runat="server" Text="Nhập từ excel" Icon="PageExcel">
                                                <Listeners>
                                                    <Click Handler="#{wdImportExcelFile}.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
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
                                    <ext:Store ID="storeTimeSheetCode" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetCode" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="StartTime" />
                                                    <ext:RecordField Name="EndTime" />
                                                    <ext:RecordField Name="IsActive" />
                                                    <ext:RecordField Name="MachineId" />
                                                    <ext:RecordField Name="MachineName" />
                                                    <ext:RecordField Name="SerialNumber" />
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
                                        <ext:Column ColumnID="AccountNumber" Header="Mã chấm công" Width="150" DataIndex="Code" />
                                        <ext:DateColumn ColumnID="StartTime" Header="Thời gian bắt đầu" Width="170" DataIndex="StartTime" Format="dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="EndTime" Header="Thời gian kết thúc" Width="150" DataIndex="EndTime" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="MachineName" Header="Tên máy" Width="150" DataIndex="MachineName" />
                                        <ext:Column ColumnID="SerialNumber" Header="Seri máy" Width="150" DataIndex="SerialNumber" />
                                        <ext:Column ColumnID="IsActive" Width="100" Header="Đang kích hoạt" Align="Center"
                                            DataIndex="IsActive">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
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
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />

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
            <ext:Window runat="server" Title="Đăng ký mới mã chấm công" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdTimeSheetCode"
                Modal="true" Constrain="true" AutoHeight="True" LabelWidth="120">
                <Items>
                    <ext:Hidden runat="server" ID="hdfTimeMachineId" />
                    <ext:ComboBox runat="server" ID="cbxTimeMachine" FieldLabel="Số series máy"
                        DisplayField="SerialNumber" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                        LabelWidth="252" Width="422" ItemSelector="div.search-item" CtCls="requiredData" PageSize="10">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="search-item"> 
							            {Name} - {SerialNumber}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="storeTimeMachine" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerTimeSheetMachine.ashx" />
                                </Proxy>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                            <ext:RecordField Name="SerialNumber" />
                                            <ext:RecordField Name="IPAddress" />
                                            <ext:RecordField Name="Location" />
                                            <ext:RecordField Name="CreatedBy" />
                                            <ext:RecordField Name="UpdatedBy" />
                                            <ext:RecordField Name="CreatedAt" />
                                            <ext:RecordField Name="UpdatedAt" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfTimeMachineId.setValue(cbxTimeMachine.getValue());"></Select>
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfTimeMachineId.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtTimeSheetCode" CtCls="requiredData" FieldLabel="Mã chấm công"
                        AnchorHorizontal="100%" />
                    <ext:Hidden runat="server" ID="hdfEmployeeSelectedId" />
                    <ext:ComboBox ID="cbxSelectedEmployee" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                        FieldLabel="Tên cán bộ" PageSize="10" HideTrigger="true"
                        ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                        LoadingText="Đang tải dữ liệu..." AnchorHorizontal="100%" runat="server">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Store>
                            <ext:Store ID="cbxSelectedEmployee_Store" runat="server" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={10}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="SearchUser" />
                                    <ext:Parameter Name="name" />
                                    <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader Root="plants" TotalProperty="total">
                                        <Fields>
                                            <ext:RecordField Name="FullName" />
                                            <ext:RecordField Name="EmployeeCode" />
                                            <ext:RecordField Name="BirthDate" />
                                            <ext:RecordField Name="DepartmentName" />
                                            <ext:RecordField Name="Id" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Template runat="server">
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
                            <Select Handler="hdfEmployeeSelectedId.setValue(cbxSelectedEmployee.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfEmployeeSelectedId.reset(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:DateField ID="dfStartTime" runat="server" Vtype="daterange" FieldLabel="Thời gian bắt đầu" CtCls="requiredData"
                        AnchorHorizontal="100%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                        <CustomConfig>
                            <ext:ConfigItem Name="endTimeField" Value="#{dfEndTime}" Mode="Value" />
                        </CustomConfig>
                    </ext:DateField>
                    <ext:DateField ID="dfEndTime" runat="server" Vtype="daterange" FieldLabel="Thời gian kết thúc"
                        AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                        <CustomConfig>
                            <ext:ConfigItem Name="startTimeField" Value="#{dfStartTime}" Mode="Value" />
                        </CustomConfig>
                    </ext:DateField>
                    <ext:Checkbox runat="server" FieldLabel="Trạng thái hoạt động" BoxLabel="Đang kích hoạt" ID="chk_IsActive" />
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetCode();" />
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
                            <Click Handler="return checkInputTimeSheetCode();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetCode.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Nhập dữ liệu từ file excel" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdImportExcelFile"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" LabelWidth="150">
                        <Items>
                            <ext:Label runat="server" ID="labelDownload" FieldLabel="Tải tệp tin mẫu">
                            </ext:Label>
                            <ext:Button runat="server" ID="btnDownloadTemplate" Icon="ArrowDown" ToolTip="Tải về" Text="Tải về" Width="100">
                                <DirectEvents>
                                    <Click OnEvent="DownloadTemplate_Click" IsUpload="true" />
                                </DirectEvents>
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Nếu bạn chưa có tệp tin Excel mẫu để nhập liệu. Hãy ấn nút này để tải tệp tin mẫu về máy">
                                    </ext:ToolTip>
                                </ToolTips>
                            </ext:Button>
                            <ext:FileUploadField ID="fileExcel" runat="server" FieldLabel="Tệp tin đính kèm<span style='color:red;'>*</span>"
                                CtCls="requiredData" AnchorHorizontal="98%" Icon="Attach">
                            </ext:FileUploadField>
                            <ext:TextField runat="server" ID="txtSheetName" FieldLabel="Tên sheet Excel" AnchorHorizontal="98%" />
                            <ext:TextField runat="server" ID="txtFromRow" FieldLabel="Từ hàng" AnchorHorizontal="98%" MaskRe="/[0-9.]/" Hidden="True" />
                            <ext:TextField runat="server" ID="txtToRow" FieldLabel="Đến hàng" AnchorHorizontal="98%" MaskRe="/[0-9.]/" Hidden="True" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateImportExcel" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="ImportFile">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>

                    <ext:Button runat="server" ID="btnCloseImport" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdImportExcelFile.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetWdImportExcelFile();" />
                </Listeners>
            </ext:Window>

        </div>
    </form>
</body>
</html>

