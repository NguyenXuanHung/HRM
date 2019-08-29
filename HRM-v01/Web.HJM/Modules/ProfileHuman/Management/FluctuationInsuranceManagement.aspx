<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.FluctuationInsuranceManagement" Codebehind="FluctuationInsuranceManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
    <script src="../../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <script>
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

        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }

        var checkInputBienDong = function () {
            if (!rgType.getValue()) {
                alert('Bạn chưa chọn loại biến động bảo hiểm!');
                return false;
            }
            if (!txtReason.getValue()) {
                alert('Bạn chưa nhập lý do!');
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
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <!-- store chức vụ -->
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="grp_FluctuationInsurance" TrackMouseOver="true" Header="false" runat="server"
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
                                    <ext:Store runat="server" ID="storeFluctuation">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />

                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="FluctuationInsurance" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
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
                                                    <ext:RecordField Name="ReasonDecrease" />
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
                                        <ext:Column Header="Lý do giảm" Width="140" Align="Left" DataIndex="ReasonDecrease" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1">
                                    </ext:LockingGridView>
                                </View>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();btnReport.enable();" />
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
            <ext:Window runat="server" Title="Tạo quyết định tăng giảm tham gia BHXH, BHYT, BHTN" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdCreateFluctuation"
                Modal="true" Constrain="true" Height="350">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandName" />
                    <ext:FieldSet runat="server" ID="FieldSet9" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Ctn11" Layout="ColumnLayout" Height="160" AnchorHorizontal="100%">
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
                                            <ext:TextField runat="server" ID="txtSalaryInsurance" FieldLabel="Mức đóng BH" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtInsuranceSubmit" FieldLabel="BH phải nộp" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:DateField runat="server" ID="txtContractDate" FieldLabel="Ngày tháng HĐLĐ" AnchorHorizontal="98%"
                                                           Disabled="true" DisabledClass="disabled-input-style" Format="dd/MM/yyyy">
                                            </ext:DateField>
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
                                            <ext:TextField runat="server" ID="txtContractNumber" FieldLabel="Số HĐLĐ" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:DateField runat="server" ID="txtEffectiveDate" FieldLabel="Từ năm, tháng" AnchorHorizontal="100%" Format="dd/MM/yyyy"
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
                                FieldLabel="Kiểu biến động tham gia BH"
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

