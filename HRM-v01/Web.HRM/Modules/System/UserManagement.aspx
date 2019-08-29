<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="Web.HRM.Modules.Setting.UserManagement" %>

<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagName="ChooseEmployee" TagPrefix="UC" %>
<%@ Register src="~/Modules/UC/ResourceCommon.ascx" tagName="ResourceCommon" tagPrefix="UC"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <UC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript" src="/Resource/js/User.js"></script>
    <script type="text/javascript">
        // role selected
        var handlerRoleChanged = function(roleId) {
            // set new selected role
            if (roleId && roleId > 0)
                hdfRoleId.setValue(roleId);
            else
                hdfRoleId.setValue('');
            // reload data
            ReloadGrid();
        };
        // validate form change password
        var validateFormChangePassword = function() {
            if (!txtNewPassword.getValue()) {
                Ext.Msg.alert('Cảnh báo', 'Bạn chưa nhập mật khẩu!');
                return false;
            }
            if (!txtConfirmNewPassword.getValue()) {
                Ext.Msg.alert('Cảnh báo', 'Bạn chưa nhập xác nhận mật khẩu!');
                return false;
            }
            if (txtNewPassword.getValue() !== txtConfirmNewPassword.getValue()) {
                Ext.Msg.alert('Cảnh báo', 'Mật khẩu và mật khẩu xác nhận không khớp!');
                return false;
            }
            return true;
        };
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- resource manager & hidden field -->
        <ext:ResourceManager ID="RM" runat="server" />
        <ext:Hidden runat="server" ID="hdfAction" />
        <ext:Hidden runat="server" ID="hdfSeletedRoleId" />
        <ext:Hidden runat="server" ID="hdfSeletedUserId" />

        <ext:Hidden runat="server" ID="hdfListUserID" />
        <input type="hidden" id="hdfCheckLoaded" />

        <!-- hidden field -->
        <ext:Hidden runat="server" ID="hdfUserId" />
        <ext:Hidden runat="server" ID="hdfRoleId" />
        <ext:Hidden runat="server" ID="hdfDepartments" />

        <!-- store -->
        <ext:Store ID="storeUser" runat="server">
            <Proxy>
                <ext:HttpProxy Method="GET" Url="~/Services/HandlerUser.ashx" />
            </Proxy>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="={0}" />
                <ext:Parameter Name="limit" Value="={30}" />
            </AutoLoadParams>
            <BaseParams>
                <ext:Parameter Name="keyword" Value="#{txtKeyword}.getValue()" Mode="Raw" />
                <ext:Parameter Name="role" Value="#{hdfRoleId}.getValue()" Mode="Raw" />
                <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
            </BaseParams>
            <Reader>
                <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                    <Fields>
                        <ext:RecordField Name="Id" />
                        <ext:RecordField Name="UserName" />
                        <ext:RecordField Name="Email" />
                        <ext:RecordField Name="DisplayName" />
                        <ext:RecordField Name="BirthDate" />
                        <ext:RecordField Name="Sex" />
                        <ext:RecordField Name="Address" />
                        <ext:RecordField Name="PhoneNumber" />
                        <ext:RecordField Name="IsLocked" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Store runat="server" ID="storeDepartment" AutoLoad="false" OnRefreshData="storeDepartment_OnRefreshData">
            <Reader>
                <ext:JsonReader IDProperty="Id">
                    <Fields>
                        <ext:RecordField Name="Id" />
                        <ext:RecordField Name="Name" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <!-- viewport main -->
        <ext:Viewport runat="server">
            <Items>
                <ext:BorderLayout runat="server" ID="brLayout">
                    <West Split="true" Collapsible="true">
                        <ext:Panel runat="server" Width="200" Title="Lọc theo vai trò" Collapsible="true" Collapsed="false" Border="false" Layout="BorderLayout">
                            <Items>
                                <ext:TreePanel ID="treeRoleFilter" runat="server" Region="Center" Header="false" Border="false" RootVisible="false" AutoScroll="true" />
                            </Items>
                        </ext:Panel>
                    </West>
                    <Center>
                        <ext:GridPanel runat="server" ID="gpUser" StoreID="storeUser" Border="false" StripeRows="true" AnchorHorizontal="100%" AutoExpandColumn="Address">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="toolbarFn">
                                    <Items>
                                        <ext:Button runat="server" ID="btnAdd" EnableViewState="false" Text="Thêm" Icon="Add">
                                            <DirectEvents>
                                                <Click OnEvent="InitWindowUserSetting">
                                                    <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnEdit" runat="server" Text="Sửa" Icon="Pencil" Disabled="true">
                                            <DirectEvents>
                                                <Click OnEvent="InitWindowUserSetting">
                                                    <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                            <DirectEvents>
                                                <Click OnEvent="Delete">
                                                    <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarSpacer runat="server" Width="5"/>
                                        <ext:ToolbarSeparator runat="server" ID="toolbarSeparatorSetting"/>
                                        <ext:ToolbarSpacer runat="server" Width="5"/>
                                        <ext:Button runat="server" ID="btnSetting" Text="Quản lý&nbsp;&nbsp;" Icon="CogEdit" Disabled="True">
                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" ID="mnuItemAssignRole" Icon="GroupEdit" Text="Thiết lập quyền">
                                                            <DirectEvents>
                                                                <Click OnEvent="InitWindowUserRole">
                                                                    <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" ID="mnuItemAssignDepartment" Icon="VcardEdit" Text="Thiết lập đơn vị">
                                                            <Listeners>
                                                                <Click Handler="wdAssignDepartment.show();" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuSeparator runat="server"/>
                                                        <ext:MenuItem runat="server" ID="mnuItemResetPassword" Icon="Key" Text="Đặt lại mật khẩu">
                                                            <Listeners>
                                                                <Click Handler="wdChangePassword.show();" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" ID="mnuItemLockUser" Icon="Lock" Text="Khóa tài khoản">
                                                            <DirectEvents>
                                                                <Click OnEvent="ChangeStatus">
                                                                    <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                    <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn thực hiện thao tác này?" />
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                                        <ext:Parameter Name="Status" Value="True" Mode="Value" />
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" ID="mnuItemUnlockUser" Icon="LockOpen" Text="Mở khóa tài khoản">
                                                            <DirectEvents>
                                                                <Click OnEvent="ChangeStatus">
                                                                    <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                    <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn thực hiện thao tác này?" />
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                                        <ext:Parameter Name="Status" Value="False" Mode="Value" />
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:Button>
                                        <ext:ToolbarFill />
                                        <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
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
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn Header="TT" Width="30" Align="Right" />
                                    <ext:Column ColumnID="UserName" Header="Tài khoản" Width="100" DataIndex="UserName" />
                                    <ext:Column ColumnID="DisplayName" Header="Tên hiển thị" Width="120" DataIndex="DisplayName" />
                                    <ext:DateColumn ColumnID="BirthDate" Header="Ngày sinh" Width="80" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                    <ext:Column ColumnID="Sex" Header="Giới tính" Width="80" DataIndex="Sex">
                                        <Renderer Fn="renderSex" />
                                    </ext:Column>
                                    <ext:Column ColumnID="Email" Header="Email" Width="150" DataIndex="Email" />
                                    <ext:Column ColumnID="Address" Header="Địa chỉ" Width="150" DataIndex="Address" />
                                    <ext:Column ColumnID="PhoneNumber" Header="Điện thoại" Width="80" DataIndex="PhoneNumber" />
                                    <ext:Column ColumnID="IsLocked" Header="Trạng thái" Width="80" Align="Center" DataIndex="IsLocked">
                                        <Renderer Fn="renderStatus" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                    <Listeners>
                                        <RowSelect Handler="hdfUserId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnSetting.enable();" />
                                        <RowDeselect Handler="btnEdit.disable();btnDelete.disable();btnSetting.disable();" />
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
                                            <Change Handler="rowSelectionModel.clearSelections();btnEdit.disable();btnDelete.disable();btnSetting.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                        </ext:GridPanel>
                    </Center>
                </ext:BorderLayout>
            </Items>
        </ext:Viewport>
        <!-- window add user -->
        <ext:Window runat="server" ID="wdSetting" Modal="true" Hidden="true" Padding="10" Layout="FormLayout" Width="450" LabelWidth="120" AutoHeight="true" 
            Resizable="false" Constrain="true">
            <Items>
                <ext:Hidden runat="server" ID="hdfDepartmentForEditingUser" />
                <ext:FieldSet runat="server" AnchorHorizontal="100%" Title="Thông tin tài khoản" AutoHeight="true">
                    <Items>
                        <ext:TextField runat="server" AllowBlank="false" EmptyText="Ví dụ: Nguyễn Văn" ID="txtFirstName" FieldLabel="Họ đệm <span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
                        <ext:TextField runat="server" AllowBlank="false" ID="txtLastName" EmptyText="Ví dụ: Tiến" FieldLabel="Tên <span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50">
                            <Listeners>
                                <Blur Handler="txtDisplayName.setValue(txtFirstName.getValue() + ' ' + txtLastName.getValue());" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField runat="server" AllowBlank="false" ID="txtDisplayName" FieldLabel="Tên hiển thị <span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
                        <ext:TextField runat="server" ID="txtUserName" AllowBlank="false" FieldLabel="Tài khoản <span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
                        <ext:TextField runat="server" InputType="Password" ID="txtPassword" AllowBlank="false" FieldLabel="Mật khẩu <span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="500" />
                        <ext:TextField runat="server" InputType="Password" ID="txtConfirmPassword" AllowBlank="false" FieldLabel="Nhập lại mật khẩu <span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="500" />
                    </Items>
                </ext:FieldSet>
                <ext:FieldSet runat="server" AnchorHorizontal="100%" Title="Thông tin cá nhân">
                    <Items>
                        <ext:ComboBox runat="server" ID="cboDepartment" StoreID="storeDepartment" FieldLabel="Bộ phận <span style='color:red;'>*</span>" CtCls="requiredData"
                            DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" Editable="false" AllowBlank="false">
                            <Listeners>
                                <Expand Handler="if(storeDepartment.getCount()==0) storeDepartment.reload();" />
                                <Select Handler="hdfDepartmentForEditingUser.setValue(cboDepartment.getValue());"></Select>
                            </Listeners>
                        </ext:ComboBox>
                        <ext:FormPanel ID="frmEmail" runat="server" ButtonAlign="Right" Layout="FormLayout" Border="false">
                            <Items>
                                <ext:TextField runat="server" ID="txtEmail" AllowBlank="true" FieldLabel="Email" AnchorHorizontal="100%"
                                    Regex="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">
                                    <Listeners>
                                        <Blur Handler="if (!#{frmEmail}.getForm().isValid()) {
	                                                    Ext.Msg.show({icon: Ext.MessageBox.ERROR, title: 'Thông báo', msg: 'Email không hợp lệ', buttons:Ext.Msg.OK}); return false; }" />
                                    </Listeners>
                                </ext:TextField>
                            </Items>
                        </ext:FormPanel>
                        <ext:TextField runat="server" ID="txtAddress" FieldLabel="Địa chỉ" AnchorHorizontal="100%" MaxLength="500" />
                        <ext:Container runat="server" Height="27" AnchorHorizontal="100%" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField runat="server" ID="txtPhoneNumber" ColumnWidth="0.5" Regex="/[0-9\.]/" FieldLabel="Điện thoại" AnchorHorizontal="98%" MaxLength="50" />
                                <ext:DateField runat="server" ColumnWidth="0.5" ID="dfBirthday" FieldLabel="Ngày sinh" AllowBlank="false" AnchorHorizontal="100%" Editable="false" />
                            </Items>
                        </ext:Container>
                        <ext:RadioGroup ID="rdgSex" runat="server" FieldLabel="Giới tính">
                            <Items>
                                <ext:Radio BoxLabel="Nam" runat="server" Checked="true" ID="rdMale" />
                                <ext:Radio BoxLabel="Nữ" runat="server" ID="rdFemale" />
                            </Items>
                        </ext:RadioGroup>
                    </Items>
                </ext:FieldSet>
                <ext:Checkbox runat="server" ID="chkSendMail" BoxLabel="Gửi mail thông báo tới người dùng" Hidden="true" />
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Lưu" Icon="Disk" ID="btnSaveSetting">
                    <Listeners>
                        <Click Handler="return validateUserData();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="InsertOrUpdateUser">
                            <ExtraParams>
                                <ext:Parameter Name="Close" Value="False" />
                                <ext:Parameter Name="Command" Value="Insert" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="btnCancelSetting">
                    <Listeners>
                        <Click Handler="wdSetting.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <!-- window change password -->
        <ext:Window runat="server" ID="wdChangePassword" Title="Đổi mật khẩu" Width="400" Padding="10" Border="false" AutoHeight="true" Resizable="false" Modal="true" 
                    Layout="FormLayout" Icon="Key" Hidden="true" LabelWidth="130">
            <Items>
                <ext:TextField runat="server" ID="txtNewPassword" AllowBlank="false" FieldLabel="Mật khẩu mới" InputType="Password" AnchorHorizontal="100%" />
                <ext:TextField runat="server" ID="txtConfirmNewPassword" AllowBlank="false" FieldLabel="Nhập lại mật khẩu" InputType="Password" AnchorHorizontal="100%" />
            </Items>
            <Listeners>
                <Hide Handler="txtNewPassword.reset();txtConfirmNewPassword.reset();" />
            </Listeners>
            <Buttons>
                <ext:Button runat="server" Icon="Disk" Text="Lưu" ID="btnSaveChangePassword">
                    <Listeners>
                        <Click Handler="return validateFormChangePassword();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="ChangePassword" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Icon="Decline" Text="Hủy" ID="btnCancelChangePassword">
                    <Listeners>
                        <Click Handler="wdChangePassword.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <!-- window assign role -->
        <ext:Window runat="server" Hidden="true" AutoHeight="true" Layout="FormLayout" Width="300"
            Padding="0" Resizable="false" Icon="Key" Modal="true" ID="wdUserRole" Title="Gán quyền cho người dùng">
            <Items>
                <ext:Hidden runat="server" ID="hdfAvailableRoleIds" />
                <ext:Hidden runat="server" ID="hdfCurrentRoleIdsForUser" />
                <ext:Hidden runat="server" ID="hdfSelectedRoleIds" />
                <ext:TreePanel ID="treeAvailableRoles" runat="server" Width="300" Height="250" Border="false" RootVisible="false" AutoScroll="true" />
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Đồng ý" ID="btnAddRole" Icon="Accept">
                    <%--<Listeners>
                        <Click Handler="#{hdfSelectedRoleIds}.setValue(getCheckedNode(#{treeAvailableRoles},#{hdfAvailableRoleIds}.getValue()));" />
                    </Listeners>--%>
                    <DirectEvents>
                        <Click OnEvent="AssignRolesForUsers">
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát..." />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="wdUserRole.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <%--<Listeners>
                <BeforeShow Handler="setCheckedNode(#{treeAvailableRoles},#{hdfCurrentRoleIdsForUser}.getValue(),#{hdfAvailableRoleIds}.getValue());" />
            </Listeners>--%>
        </ext:Window>
        <!-- window assign department -->
        <ext:Window runat="server" Hidden="true" AutoHeight="true" Layout="FormLayout" Width="300"
            Padding="0" Resizable="false" Icon="Key" Modal="true" ID="wdAssignDepartment" Title="Gán đơn vị cho người dùng">
            <Items>
                <ext:Hidden runat="server" ID="hdfAvailableDepartmentIds" />
                <ext:Hidden runat="server" ID="hdfCurrentDepartmentIdsForUser" />
                <ext:Hidden runat="server" ID="hdfSelectedDepartmentIds" />
                <ext:TreePanel ID="treeAvailableDepartments" runat="server" Width="300" Height="250" Border="false" RootVisible="false" AutoScroll="true">
                    <Root></Root>
                </ext:TreePanel>
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Đồng ý" ID="btnAddDepartment" Icon="Accept">
                    <Listeners>
                        <Click Handler="#{hdfSelectedDepartmentIds}.setValue(getCheckedNode(#{treeAvailableDepartments},#{hdfAvailableDepartmentIds}.getValue()));" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="AssignDepartmentsForUsers">
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát..." />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdAssignDepartment}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <BeforeShow Handler="setCheckedNode(#{treeAvailableDepartments},#{hdfCurrentDepartmentIdsForUser}.getValue(),#{hdfAvailableDepartmentIds}.getValue());" />
            </Listeners>
        </ext:Window>
        <!-- choose employee -->
        <UC:ChooseEmployee ID="chooseEmployee" runat="server" OnlyChooseOnePerson="false" DisplayWorkingStatus="TatCa" />
    </form>
</body>
</html>
