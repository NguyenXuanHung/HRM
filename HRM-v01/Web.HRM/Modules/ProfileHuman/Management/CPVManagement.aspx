<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.Management.CPVManagement" Codebehind="CPVManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script>
        var getPrKeyList = function () {
            var jsonDataEncode = "";
            var records = window.grp_DanhSachCanBoStore.getRange();
            for (var i = 0; i < records.length; i++) {
                jsonDataEncode += records[i].data.Id + ",";
            }
            return jsonDataEncode;
        }

        var addRecord = function (id, employeeCode, fullName, departmentName) {
            var rowindex = getSelectedIndexRow();
            window.grp_DanhSachCanBo.insertRecord(rowindex, {
                Id: id,
                EmployeeCode: employeeCode,
                FullName: fullName,
                DepartmentName: departmentName
            });
            window.grp_DanhSachCanBo.getView().refresh();
            window.grp_DanhSachCanBo.getSelectionModel().selectRow(rowindex);
            window.grp_DanhSachCanBoStore.commitChanges();
        }
        var getSelectedIndexRow = function () {
            var record = window.grp_DanhSachCanBo.getSelectionModel().getSelected();
            var index = window.grp_DanhSachCanBo.store.indexOf(record);
            if (index === -1)
                return 0;
            return index;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfMaDonVi" />
            <ext:Hidden runat="server" ID="hdfMenuID" />
            <ext:Hidden runat="server" ID="hdfUserID" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />

            <ext:Store runat="server" ID="cbxCPVPosition_Store" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_CPVPosition" Mode="Value" />
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
                            <ext:GridPanel ID="grp_QuanLyDangVien" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdAddNewCPV.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowEditCpv">
                                                        <EventMask ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
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
                                    <ext:Store runat="server" ID="stDangVien">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="CPVPosition" />
                                            <ext:Parameter Name="ListDepartment" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="userID" Value="hdfUserID.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="menuID" Value="hdfMenuID.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfMaDonVi.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="ID" Root="plants" TotalProperty="total">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="ParentDepartmentName" />
                                                    <ext:RecordField Name="CPVJoinedDate" />
                                                    <ext:RecordField Name="CPVOfficialJoinedDate" />
                                                    <ext:RecordField Name="CPVJoinedPlace" />
                                                    <ext:RecordField Name="CPVCardNumber" />
                                                    <ext:RecordField Name="CPVPositionName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column Header="Mã CCVC" Width="100" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ tên" Width="220" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column Header="Phòng ban" Width="220" DataIndex="DepartmentName" />
                                        <ext:Column Header="Đơn vị quản lý" Width="230" DataIndex="ParentDepartmentName" />
                                        <ext:DateColumn Header="Ngày vào Đảng" Width="95" Align="Left" DataIndex="CPVJoinedDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn Header="Ngày chính thức vào Đảng" Width="95" Align="Left" DataIndex="CPVOfficialJoinedDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Nơi kết nạp Đảng" Width="140" Align="Left" DataIndex="CPVJoinedPlace" />
                                        <ext:Column Header="Số thẻ Đảng" Width="90" DataIndex="CPVCardNumber">
                                        </ext:Column>
                                        <ext:Column Header="Chức vụ Đảng" Width="150" Align="Left" DataIndex="CPVPositionName">
                                        </ext:Column>

                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1">
                                    </ext:LockingGridView>
                                </View>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfRecordId.reset(); " />
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
            <ext:Window ID="wdCapNhatDangVien" Width="550" Height="180" runat="server" Padding="14"
                EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true" LabelWidth="120"
                Icon="Pencil" Title="Cập nhật thông tin Đảng viên" Resizable="false">
                <Items>
                    <ext:Container ID="Container46" runat="server" Layout="FormLayout" Height="83">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfChucvuD" />
                            <ext:ComboBox runat="server" ID="cbxCPVPosition" DisplayField="Name"
                                FieldLabel="Chức vụ Đảng<span style='color:red;'>*</span>" ValueField="Id"
                                AnchorHorizontal="98%" TabIndex="2" Editable="false" ItemSelector="div.list-item" StoreID="cbxCPVPosition_Store"
                                CtCls="requiredData" LabelAlign="Left">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template ID="Template9" runat="server">
                                    <Html>
                                        <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                    </Html>
                                </Template>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show(); hdfChucvuD.setValue(this.getValue())"></Select>
                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Container ID="Container48" runat="server" LabelAlign="left" Layout="ColumnLayout" Height="53"
                                LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:Container ID="Container4" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100"
                                        ColumnWidth="0.5">
                                        <Items>
                                            <ext:DateField runat="server" FieldLabel="Ngày vào Đảng" ID="dfNgayVaoD"
                                                AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                RegexText="Định dạng ngày không đúng" />
                                            <ext:TextField runat="server" ID="txtSoTheD" FieldLabel="Số thẻ Đảng" TabIndex="5"
                                                AnchorHorizontal="98%" Editable="false" MaskRe="/[0-9.,]/" MaxLength="18" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container112" runat="server" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                        <Items>
                                            <ext:DateField runat="server" FieldLabel="Ngày chính thức" ID="dfNgayCTDang"
                                                AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                RegexText="Định dạng ngày không đúng" />

                                            <ext:TextField runat="server" ID="txtNoiKetNapD" FieldLabel="Nơi kết nạp Đảng" TabIndex="6"
                                                AnchorHorizontal="98%" Editable="false" />
                                        </Items>
                                    </ext:Container>

                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>

                </Items>
                <Buttons>

                    <ext:Button ID="btnUpdate" runat="server" Hidden="false" Text="Cập nhật" Icon="Disk">

                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="UpdateARecord" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>

                    <ext:Button ID="Button25" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdCapNhatDangVien}.hide();ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>

            </ext:Window>
            <ext:Window runat="server" ID="wdAddNewCPV" Constrain="true" Modal="true"
                Title="Thêm cán bộ Đảng viên" Icon="UserAdd" Layout="FormLayout"
                AutoHeight="true" Width="650" Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container runat="server" ID="ctn30" Layout="FormLayout" AutoHeight="true" AnchorHorizontal="100%" LabelWidth="120">
                        <Items>
                            <ext:FormPanel runat="server" ID="fp1" Frame="true" AnchorHorizontal="100%" Title="Thông tin Đảng viên" LabelWidth="120"
                                Icon="BookOpenMark" Layout="FormLayout">
                                <Items>
                                    <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="55">
                                        <Items>
                                            <ext:Container ID="Container2" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="120"
                                                ColumnWidth="0.5">
                                                <Items>
                                                    <ext:DateField runat="server" FieldLabel="Ngày vào đảng" ID="dfNgayVaoDangHangLoat"
                                                        AnchorHorizontal="98%" TabIndex="1" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                        RegexText="Định dạng ngày không đúng" />
                                                    <ext:TextField runat="server" ID="SoTheDangHangLoat" FieldLabel="Số thẻ đảng" TabIndex="3"
                                                        AnchorHorizontal="98%" Editable="false" MaskRe="/[0-9.,]/" MaxLength="20" />

                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container3" runat="server" LabelAlign="left" Layout="FormLayout"
                                                LabelWidth="120" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:DateField runat="server" FieldLabel="Ngày chính thức" ID="dfNgayCTDangHangLoat"
                                                        AnchorHorizontal="100%" TabIndex="2" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                        RegexText="Định dạng ngày không đúng" />
                                                    <ext:TextField runat="server" ID="txtNoiKetNapDangHangLoat" FieldLabel="Nơi kết nạp đảng" TabIndex="4"
                                                        AnchorHorizontal="100%" Editable="false" />
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Containe1" runat="server" LabelAlign="Left" Layout="FormLayout" LabelWidth="120" ColumnWidth="0.5">
                                        <Items>
                                            <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                            <ext:Hidden runat="server" ID="hdfChucVuDang" />
                                            <ext:ComboBox runat="server" ID="cbxCPVPositionAdd" DisplayField="Name"
                                                FieldLabel="Chức vụ đảng<span style='color:red;'>*</span>" ValueField="Id"
                                                AnchorHorizontal="100%" TabIndex="6" Editable="true" MinChars="1" LoadingText="Đang tải dữ liệu..."
                                                ItemSelector="div.list-item" StoreID="cbxCPVPosition_Store"
                                                CtCls="requiredData" Width="301" EnableKeyEvents="true">
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
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show(); hdfChucVuDang.setValue(this.getValue())"></Select>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="230">
                        <Items>
                            <ext:GridPanel runat="server" ID="grp_DanhSachCanBo" TrackMouseOver="true" Title="Danh sách cán bộ"
                                StripeRows="true" Border="true" Region="Center" Icon="User"
                                AutoExpandMin="150">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tbDanhSachQD">
                                        <Items>
                                            <ext:Button runat="server" ID="btnChonDanhSachCanBo" Icon="UserAdd" Text="Chọn cán bộ" TabIndex="9">
                                                <Listeners>
                                                    <Click Handler="ucChooseEmployee_wdChooseUser.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnXoaCanBo" Icon="Delete" Text="Xóa" Disabled="true" TabIndex="10">
                                                <Listeners>
                                                    <Click Handler="#{grp_DanhSachCanBo}.deleteSelected(); #{hdfTotalRecord}.setValue(#{hdfTotalRecord}.getValue()*1 - 1);if(hdfTotalRecord.getValue() ==0){btnXoaCanBo.disable();}" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="grp_DanhSachCanBoStore" AutoLoad="false" runat="server" ShowWarningOnFailure="false"
                                        SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="false">
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel ID="ColumnModel3" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="40" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Width="100" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="FullName" Header="Họ tên" Width="200" DataIndex="FullName" />
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="270" DataIndex="DepartmentName">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="chkEmployeeRowSelection">
                                        <Listeners>
                                            <RowSelect Handler="btnXoaCanBo.enable();" />
                                            <RowDeselect Handler="btnXoaCanBo.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateRecords" Text="Cập nhật" Icon="Disk">

                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="ListId" Value="getPrKeyList()" Mode="Raw" />
                                    <ext:Parameter Name="Command" Value="UpdateRecords" />

                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLaiHL" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAddNewCPV.hide();ResetForm();" />
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
