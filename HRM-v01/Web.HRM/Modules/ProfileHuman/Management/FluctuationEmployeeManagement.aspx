<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.Management.FluctuationEmployeeManagement" CodeBehind="FluctuationEmployeeManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script>
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
        var checkInputBienDong = function () {
            if (!rgType.getValue()) {
                alert('Bạn chưa chọn loại biến động nhân sự!');
                return false;
            }
            if (!txtReason.getValue()) {
                alert('Bạn chưa nhập lý do!');
                return false;
            }
            if (!txtDate.getValue()) {
                alert('Bạn chưa chọn ngày biến động nhân sự!');
                return false;
            }
            return true;
        }
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
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <!-- store chức vụ -->
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="grp_FluctuationEmployee" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdCreateFluctuation.show();" />
                                                </Listeners>

                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowFluctuation">
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
                                            <%-- <ext:Button runat="server" ID="btnReport" Text="Báo cáo" Icon="Printer" Disabled="true">
                                                <DirectEvents>
                                                </DirectEvents>
                                            </ext:Button>--%>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarFill />
                                            <ext:DateField ID="dfNgayBatDau" runat="server" Width="140" EmptyText="Ngày quyết định từ ngày"
                                                AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfNgayKetThuc}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfNgayKetThuc.setMinValue(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();}" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:DateField ID="dfNgayKetThuc" runat="server" EmptyText="Ngày quyết định đến ngày"
                                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange" Width="140"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfNgayBatDau}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfNgayBatDau.setMaxValue(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();}" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="240" EmptyText="Nhập mã, họ tên CCVC lý do hoặc ghi chú">
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
                                    <ext:Store runat="server" ID="storeFluctuation">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />

                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="FluctuationEmployee" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="TuNgay" Value="dfNgayBatDau.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="DenNgay" Value="dfNgayKetThuc.getRawValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Key" Root="plants" TotalProperty="total">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="BirthDate" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="IDNumber" />
                                                    <ext:RecordField Name="ReasonIncrease" />
                                                    <ext:RecordField Name="IncreaseDate" />
                                                    <ext:RecordField Name="ReasonDecrease" />
                                                    <ext:RecordField Name="DecreaseDate" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" />
                                        <ext:Column Header="Mã CCVC" Width="100" Align="Left" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ tên" Width="220" Align="Left" DataIndex="FullName" />
                                        <ext:DateColumn Header="Ngày sinh" Width="120" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Phòng ban" Width="170" DataIndex="DepartmentName" />
                                        <ext:Column Header="Số CMND" Width="120" DataIndex="IDNumber" />
                                        <ext:Column Header="Lý do tăng" Width="145" Align="Left" DataIndex="ReasonIncrease" />
                                        <ext:DateColumn Header="Ngày tăng" Width="145" Align="Left" DataIndex="IncreaseDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Lý do giảm" Width="140" Align="Left" DataIndex="ReasonDecrease" />
                                        <ext:DateColumn Header="Ngày giảm" Width="145" Align="Left" DataIndex="DecreaseDate" Format="dd/MM/yyyy" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1">
                                    </ext:LockingGridView>
                                </View>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id'));" />
                                            <RowDeselect Handler="hdfRecordId.reset();hdfKeyRecord.reset(); " />
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
            <ext:Window runat="server" Title="Tạo quyết định tăng giảm nhân sự" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdCreateFluctuation"
                Modal="true" Constrain="true" Height="350">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandName" />
                    <ext:FieldSet runat="server" ID="FieldSet9" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Ctn11" Layout="ColumnLayout" Height="130" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" ID="Ctn12" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfChonCanBo" />
                                            <ext:ComboBox ID="cbxChonCanBo" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                                FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                                                ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                                LoadingText="Đang tải dữ liệu..." AnchorHorizontal="98%" runat="server">
                                                <Store>
                                                    <ext:Store ID="cbxChonCanBo_Store" runat="server" AutoLoad="false">
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
                                                    <Select Handler="hdfChonCanBo.setValue(cbxChonCanBo.getValue());" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Select OnEvent="cbxChonCanBo_Selected" />
                                                </DirectEvents>
                                            </ext:ComboBox>
                                            <ext:TextField runat="server" ID="txtBirthDate" FieldLabel="Ngày sinh" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtPosition" FieldLabel="Chức vụ" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtJobTitle" FieldLabel="Nghề nghiệp" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtIDNumber" FieldLabel="Số CMND" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Ctn13" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtEmployeeCode" FieldLabel="Mã CBCCVC" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtDepartment" FieldLabel="Phòng-Ban" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtConstruction" FieldLabel="Công trình" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtTeam" FieldLabel="Tổ đội" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:DateField runat="server" ID="txtIDIssueDate" FieldLabel="Ngày cấp CMND" AnchorHorizontal="100%" Format="d/M/yyyy"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="Ctn15" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                        <Items>
                            <ext:RadioGroup
                                ID="rgType"
                                runat="server"
                                GroupName="RadioGroup3"
                                FieldLabel="Kiểu biến động nhân sự"
                                LabelWidth="200"
                                ColumnsNumber="6"
                                Cls="x-check-group-alt">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="Tăng" ID="rbIncrease" />
                                    <ext:Radio runat="server" BoxLabel="Giảm" ID="rbDecrease" />
                                </Items>
                            </ext:RadioGroup>
                        </Items>
                    </ext:Container>
                    <ext:TextField runat="server" ID="txtReason" FieldLabel="Lý do" AnchorHorizontal="98%" LabelWidth="120">
                    </ext:TextField>
                    <ext:DateField ID="txtDate" runat="server" FieldLabel="Ngày"
                        AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange"
                        Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                        <Listeners>
                            <Select Handler="this.triggers[0].show();" />
                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                        </Listeners>
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                    </ext:DateField>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnCapNhat" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputBienDong();" />
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
                    <ext:Button runat="server" ID="btnCapNhatDongLai" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputBienDong();" />
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
                    <ext:Button runat="server" ID="btnDongLai" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdCreateFluctuation.hide();ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetForm();" />
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>

