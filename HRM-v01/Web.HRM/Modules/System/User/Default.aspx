<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Setting.ModulesSystemUserDefault" Codebehind="Default.aspx.cs" %>

<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagName="ChooseEmployee" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />    
    <script type="text/javascript" src="/Resource/js/User.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- resource manager & hidden field -->
        <ext:ResourceManager ID="RM" runat="server" />
        <ext:Hidden runat="server" ID="hdfAction" />
        <ext:Hidden runat="server" ID="hdfSeletedRoleId" />
        <ext:Hidden runat="server" ID="hdfSeletedUserId" />
        <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfListUserID" />
        <input type="hidden" id="hdfCheckLoaded" />

        <!-- viewport main -->
        <ext:Viewport runat="server" ID="vp">
            <Items>
                <ext:BorderLayout runat="server" ID="brLayout">
                    <Center>
                        <ext:GridPanel ID="gridUsersForRole" Border="false" runat="server" StripeRows="true" AnchorHorizontal="100%">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="tbgridpanel">
                                    <Items>
                                        <ext:Button runat="server" ID="btnAssignRole" Disabled="true" Text="Gán quyền" Icon="User">
                                            <DirectEvents>
                                                <Click OnEvent="ShowWindowRole">
                                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát" />
                                                </Click>
                                            </DirectEvents>
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Hướng dẫn" Html="Chọn một hoặc nhiều người dùng để gán quyền. Giữ Ctrl để chọn từng người dùng và giữ shift để chọn một khoảng người dùng." />
                                            </ToolTips>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnAssignDepartment" Disabled="true" Text="Gán đơn vị" Icon="BuildingEdit">
                                            <DirectEvents>
                                                <Click OnEvent="ShowWindowDepartment">
                                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát" />
                                                </Click>
                                            </DirectEvents>
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Hướng dẫn" Html="Chọn một hoặc nhiều người dùng để gán quyền. Giữ Ctrl để chọn từng người dùng và giữ shift để chọn một khoảng người dùng." />
                                            </ToolTips>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnAddNewUser" EnableViewState="false" Text="Thêm mới" Icon="Add">
                                            <Listeners>
                                                <Click Handler="wdAddUser.show();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnWizardAddNewUser" EnableViewState="false" Text="Thêm nhanh" Icon="Add" Disable="true">
                                            <Listeners>
                                                <Click Handler="wdAddUser.show();" />
                                               
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="btnEditUser" runat="server" Disabled="true" Text="Sửa" Icon="Pencil">
                                            <DirectEvents>
                                                <Click OnEvent="InitEditedUser">
                                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnDeleteUser" Text="Xóa" Disabled="true" Icon="Delete">
                                            <Listeners>
                                                <Click Handler="RemoveItemOnGrid(gridUsersForRole,storeUsersForRole)" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="mnuTienIch" Text="Quản lý" Icon="ApplicationCascade">
                                            <Menu>
                                                <ext:Menu ID="Menu1" runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Disabled="true" Icon="Reload" ID="mnuResetPassword" Text="Reset mật khẩu">
                                                            <Listeners>
                                                                <Click Handler="if(#{hdfSeletedUserId}.getValue()=='') {
                                                                                Ext.Msg.alert('Cảnh báo','Bạn chưa chọn tài khoản nào'); return false; }" />
                                                            </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="mnuResetPassword_Click" Before="GetListUserID(#{gridUsersForRole}, #{storeUsersForRole})">
                                                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát" />
                                                                    <Confirmation ConfirmRequest="true" Title="Cảnh báo" Message="Đây là chức năng để thay đổi mật khẩu tự động. Được áp dụng đối với những trường hợp người dùng quên mật khẩu. Mật khẩu mới sẽ được gửi tự động vào Email của người dùng !" />
                                                                </Click>
                                                            </DirectEvents>
                                                            <ToolTips>
                                                                <ext:ToolTip runat="server" Title="Đổi mật khẩu tự động" Html="Chọn một hoặc nhiều người dùng để đổi mật khẩu. Giữ Ctrl để chọn từng người dùng và giữ shift để chọn một khoảng người dùng." />

                                                            </ToolTips>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem ID="mnuShowChangePasswordWindow" Disabled="true" Icon="Key" runat="server" Text="Đổi mật khẩu">
                                                            <Listeners>
                                                                <Click Handler="if(#{hdfSeletedUserId}.getValue()==''){
                                                                                    Ext.Msg.alert('Cảnh báo','Bạn chưa chọn tài khoản nào'); }
                                                                                else {
                                                                                    wdChangePassword.show(); }" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem ID="mnuLockUser" runat="server" Disabled="true" Icon="Lock" Text="Khóa tài khoản">
                                                            <Listeners>
                                                                <Click Handler="if(#{hdfSeletedUserId}.getValue()==''){
                                                                                Ext.Msg.alert('Cảnh báo','Bạn chưa chọn tài khoản nào'); return false; }" />
                                                            </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="mnuLockUser_Click">
                                                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                                </Click>
                                                            </DirectEvents>
                                                            <ToolTips>
                                                                <ext:ToolTip runat="server" Title="Hướng dẫn" Html="Bạn có thể chọn một hoặc nhiều tài khoản để tiến hành khóa tài khoản." />
                                                            </ToolTips>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem ID="mnuUnlockUser" Disabled="true" runat="server" Icon="Lock" Text="Mở tài khoản">
                                                            <Listeners>
                                                                <Click Handler="if(#{hdfSeletedUserId}.getValue()==''){
                                                                                Ext.Msg.alert('Cảnh báo','Bạn chưa chọn tài khoản nào'); return false; }" />
                                                            </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="mnuUnlockUser_Click">
                                                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:MenuItem>
                                                       
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:Button>
                                        <ext:ToolbarFill runat="server" />
                                        <ext:TextField runat="server" ID="txtSearchKey" EnableKeyEvents="true" Width="220" EmptyText="Nhập tên tài khoản hoặc tên hiển thị...">
                                            <Listeners>
                                                <KeyPress Fn="enterKeyPressHandler" />
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:Button runat="server" Text="Tìm kiếm" Icon="Zoom" ID="btnSearch">
                                            <Listeners>
                                                <Click Handler="storeUsersForRole.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="storeUsersForRole" runat="server">
                                    <Proxy>
                                        <ext:HttpProxy Method="GET" Url="~/Services/HandlerSystemUser.ashx" />
                                    </Proxy>
                                    <AutoLoadParams>
                                        <ext:Parameter Name="start" Value="={0}" />
                                        <ext:Parameter Name="limit" Value="={25}" />
                                    </AutoLoadParams>
                                    <BaseParams>
                                        <ext:Parameter Name="action" Value="#{hdfAction}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="keyword" Value="#{txtSearchKey}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="roleid" Value="#{hdfSeletedRoleId}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                    </BaseParams>
                                    <Reader>
                                        <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                                            <Fields>
                                                <ext:RecordField Name="Id" />
                                                <ext:RecordField Name="UserName" />
                                                <ext:RecordField Name="Email" />
                                                <ext:RecordField Name="FirstName" />
                                                <ext:RecordField Name="LastName" />
                                                <ext:RecordField Name="DisplayName" />
                                                <ext:RecordField Name="BirthDate" />
                                                <ext:RecordField Name="Sex" />
                                                <ext:RecordField Name="Address" />
                                                <ext:RecordField Name="PhoneNumber" />
                                                <ext:RecordField Name="CreatedDate" />
                                                <ext:RecordField Name="IsLocked" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column Header="Tài khoản" Locked="true" Width="160" DataIndex="UserName" />
                                    <ext:Column Header="Tên hiển thị" Locked="true" Width="160" DataIndex="DisplayName" />
                                    <ext:Column Header="Giới tính" Width="75" DataIndex="Sex">
                                        <Renderer Fn="RenderSex" />
                                    </ext:Column>
                                    <ext:Column Header="Email" Width="175" DataIndex="Email" />
                                    <ext:Column Header="Điện thoại" Width="100" DataIndex="PhoneNumber" />
                                    <ext:Column Header="Địa chỉ" Width="275" DataIndex="Address" />
                                    <ext:DateColumn Header="Ngày sinh" Width="75" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                    <ext:DateColumn Header="Ngày tạo" Width="75" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                    <ext:Column Header="Bị tạm khóa" Width="75" Align="Center" DataIndex="IsLocked">
                                        <Renderer Fn="RenderTrueFalseIcon" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:LockingGridView runat="server" ID="lkv" />
                            </View>
                            <Listeners>
                                <RowContextMenu Handler="e.preventDefault(); #{RowContextMenu}.dataRecord = this.store.getAt(rowIndex);#{RowContextMenu}.showAt(e.getXY());#{gridUsersForRole}.getSelectionModel().selectRow(rowIndex);" />
                            </Listeners>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                    <Listeners>
                                        <RowSelect Handler="#{hdfSeletedUserId}.setValue(#{RowSelectionModel1}.getSelected().id);
                                                        btnEditUser.enable();btnDeleteUser.enable();btnAssignRole.enable();btnAssignDepartment.enable();
                                                        mnuResetPassword.enable();mnuShowChangePasswordWindow.enable();mnuLockUser.enable();
                                                        mnuUnlockUser.enable();
                                                        " />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <LoadMask ShowMask="true" />
                            <DirectEvents>
                                <DblClick OnEvent="InitEditedUser">
                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                </DblClick>
                            </DirectEvents>
                            <BottomBar>
                                <ext:PagingToolbar ID="pagingToolbar" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau" PrevText="Trang trước"
                                    LastText="Trang cuối cùng" FirstText="Trang đầu tiên" DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên 1 trang:" />
                                        <ext:ToolbarSpacer runat="server" Width="10" />
                                        <ext:ComboBox ID="ComboBoxPaging" SelectedIndex="4" runat="server" Width="80">
                                            <Items>
                                                <ext:ListItem Text="5" />
                                                <ext:ListItem Text="10" />
                                                <ext:ListItem Text="15" />
                                                <ext:ListItem Text="20" />
                                                <ext:ListItem Text="25" />
                                                <ext:ListItem Text="30" />
                                            </Items>
                                            <Listeners>
                                                <Select Handler="#{pagingToolbar}.pageSize = parseInt(this.getValue()); #{pagingToolbar}.doLoad();"></Select>
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Center>
                    <West Split="true" Collapsible="true">
                        <ext:Panel runat="server" Width="220" CtCls="west-panel2" Title="Lọc theo quyền" Collapsible="true" Collapsed="false" Border="false" Layout="BorderLayout">
                            <Items>
                                <ext:TreePanel ID="treeRoleFilter" runat="server" Region="Center" Header="false" Border="false" RootVisible="false" AutoScroll="true">
                                    <TopBar>
                                        <ext:Toolbar runat="server" ID="tbTreeRoleFilter">
                                            <Items>
                                                <ext:Button ID="btnExpandTreeRoleFilter" runat="server" Text="Mở rộng">
                                                    <Listeners>
                                                        <Click Handler="#{treeAvailableRolesFilter}.expandAll();" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnCollapseTreeRoleFilter" runat="server" Text="Thu nhỏ">
                                                    <Listeners>
                                                        <Click Handler="#{treeAvailableRolesFilter}.collapseAll();" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:TreePanel>
                            </Items>
                        </ext:Panel>
                    </West>
                </ext:BorderLayout>
            </Items>
        </ext:Viewport>

        <!-- window add user -->
        <ext:Window Modal="true" Icon="UserAdd" Hidden="true" Padding="6" Layout="FormLayout" Width="450" LabelWidth="120" AutoHeight="true" Resizable="false"
            Title="Thêm người dùng" runat="server" Constrain="true" ID="wdAddUser">
            <Items>
                <ext:Hidden runat="server" ID="hdfUserCommand" />
                <ext:FieldSet runat="server" AnchorHorizontal="100%" Title="Thông tin tài khoản"
                    AutoHeight="true" ID="fsThongTInTaiKhoan">
                    <Items>
                        <ext:TextField runat="server" AllowBlank="false" EmptyText="Ví dụ: Nguyễn Văn" ID="txtFirstName" FieldLabel="Họ đệm<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
                        <ext:TextField runat="server" AllowBlank="false" ID="txtLastName" EmptyText="Ví dụ: Tiến" FieldLabel="Tên<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50">
                            <Listeners>
                                <Blur Handler="txtDisplayName.setValue(txtFirstName.getValue()+' '+txtLastName.getValue());" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField runat="server" AllowBlank="false" ID="txtDisplayName" FieldLabel="Tên hiển thị<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
                        <ext:TextField runat="server" ID="txtUserName" AllowBlank="false" FieldLabel="Tài khoản<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
                        <ext:TextField runat="server" InputType="Password" ID="txtPassword" AllowBlank="false" FieldLabel="Mật khẩu<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="500" />
                        <ext:TextField runat="server" InputType="Password" ID="txtConfirmPassword" AllowBlank="false" FieldLabel="Nhập lại mật khẩu<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="500" />
                    </Items>
                </ext:FieldSet>
                <ext:FieldSet runat="server" AnchorHorizontal="100%" Title="Thông tin cá nhân" ID="fsThongTinCaNhan">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfSelectedDepartmentID" />
                        <ext:ComboBox runat="server" ID="cboDepartment" FieldLabel="Bộ phận<span style='color:red;'>*</span>" CtCls="requiredData" DisplayField="Name" ValueField="Id"
                            LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item" AnchorHorizontal="100%" TabIndex="33" Editable="false" AllowBlank="false">
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
                                <ext:Store runat="server" ID="cboDepartmentStore" AutoLoad="false">
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
                                <Expand Handler="if(cboDepartment.store.getCount()==0) cboDepartmentStore.reload();" />
                                <Select Handler="this.triggers[0].show();hdfSelectedDepartmentID.setValue(cboDepartment.getValue());"></Select>
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSelectedDepartmentID.reset();}" />
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
                                <ext:Container runat="server" Height="27" Layout="FormLayout" ColumnWidth="0.5">
                                    <Items>
                                        <ext:TextField runat="server" Regex="/[0-9\.]/" ID="txtPhone" FieldLabel="Điện thoại" AnchorHorizontal="98%" MaxLength="50" />
                                    </Items>
                                </ext:Container>
                                <ext:DateField runat="server" ColumnWidth="0.5" ID="dfBirthday" FieldLabel="Ngày sinh" AllowBlank="false" AnchorHorizontal="100%" Editable="false" />
                            </Items>
                        </ext:Container>
                        <ext:RadioGroup ID="rdgSex" runat="server" FieldLabel="Giới tính">
                            <Items>
                                <ext:Radio BoxLabel="Nam" runat="server" Checked="true" ID="rdNam" />
                                <ext:Radio BoxLabel="Nữ" runat="server" ID="rdNu" />
                            </Items>
                        </ext:RadioGroup>
                    </Items>
                </ext:FieldSet>
                <ext:Checkbox runat="server" ID="chkSendMail" BoxLabel="Gửi mail thông báo tới người dùng" Hidden="true" />
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Cập nhật" Icon="Disk" ID="btnAddUser">
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
                <ext:Button runat="server" Text="Cập nhật & Đóng lại" Icon="Disk" ID="btnAddUserAndClose">
                    <Listeners>
                        <Click Handler="return validateUserData();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="InsertOrUpdateUser">
                            <ExtraParams>
                                <ext:Parameter Name="Close" Value="True" />
                                <ext:Parameter Name="Command" Value="Insert" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Cập nhật" Hidden="true" Icon="Disk" ID="btnUpdateUser">
                    <Listeners>
                        <Click Handler="return validateUserData();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="InsertOrUpdateUser">
                            <ExtraParams>
                                <ext:Parameter Name="Close" Value="True" />
                                <ext:Parameter Name="Command" Value="Update" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="btnClose2">
                    <Listeners>
                        <Click Handler="wdAddUser.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <BeforeShow Handler="if(hdfUserCommand.getValue()=='Update'){
                                        btnAddUser.hide();btnAddUserAndClose.hide();btnUpdateUser.show();
                                        txtUserName.disable();txtPassword.disable();txtConfirmPassword.disable(); }" />
                <Hide Handler="resetForm();" />
            </Listeners>
        </ext:Window>

        <!-- window change password -->
        <ext:Window ID="wdChangePassword" Padding="6" Border="false" runat="server" Width="400"
            AutoHeight="true" Resizable="false" Modal="true" Layout="FormLayout" Icon="Lock"
            Title="Đổi mật khẩu" Hidden="true" LabelWidth="130">
            <Items>
                <ext:TextField ID="txtNewPassword" AllowBlank="false" runat="server" FieldLabel="Mật khẩu mới"
                    InputType="Password" AnchorHorizontal="95%">
                </ext:TextField>
                <ext:TextField ID="TextField1" AllowBlank="false" runat="server" Vtype="password"
                    FieldLabel="Nhập lại mật khẩu" InputType="Password" MsgTarget="Side" AnchorHorizontal="95%">
                    <CustomConfig>
                        <ext:ConfigItem Name="initialPassField" Value="#{txtNewPassword}" Mode="Value" />
                    </CustomConfig>
                </ext:TextField>
            </Items>
            <Listeners>
                <Hide Handler="txtNewPassword.reset();TextField1.reset();" />
            </Listeners>
            <Buttons>
                <ext:Button runat="server" Icon="Disk" Text="Cập nhật" ID="btnUpdatePassword">
                    <Listeners>
                        <Click Handler="return checkChangePassword();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="btnUpdatePassword_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Icon="Decline" Text="Hủy" ID="btnClose">
                    <Listeners>
                        <Click Handler="wdChangePassword.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <!-- window assign role -->
        <ext:Window runat="server" Hidden="true" AutoHeight="true" Layout="FormLayout" Width="300"
            Padding="0" Resizable="false" Icon="Key" Modal="true" ID="wdAssignRole" Title="Gán quyền cho người dùng">
            <Items>
                <ext:Hidden runat="server" ID="hdfAvailableRoleIds" />
                <ext:Hidden runat="server" ID="hdfCurrentRoleIdsForUser" />
                <ext:Hidden runat="server" ID="hdfSelectedRoleIds" />
                <ext:TreePanel ID="treeAvailableRoles" runat="server" Width="300" Height="250" Border="false" RootVisible="false" AutoScroll="true">
                    <Root></Root>
                </ext:TreePanel>
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Đồng ý" ID="btnAddRole" Icon="Accept">
                    <Listeners>
                        <Click Handler="#{hdfSelectedRoleIds}.setValue(getCheckedNode(#{treeAvailableRoles},#{hdfAvailableRoleIds}.getValue()));" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="AssignRolesForUsers">
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát..." />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdAssignRole}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <BeforeShow Handler="setCheckedNode(#{treeAvailableRoles},#{hdfCurrentRoleIdsForUser}.getValue(),#{hdfAvailableRoleIds}.getValue());" />
            </Listeners>
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

        <!-- context menu -->
        <ext:Menu ID="RowContextMenu" runat="server">
            <Items>
                <ext:MenuItem ID="MenuItem2" runat="server" Icon="Key" Text="Gán quyền">
                    <DirectEvents>
                        <Click OnEvent="ShowWindowRole">
                            <EventMask ShowMask="true" Msg="Chờ trong giây lát" />
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuItem ID="mnuAddNew" runat="server" Icon="Add" Text="Thêm mới">
                    <Listeners>
                        <Click Handler="wdAddUser.show();" />
                    </Listeners>
                </ext:MenuItem>
               
                <ext:MenuItem ID="MenuItem4" runat="server" Icon="Delete" Text="Xóa">
                    <Listeners>
                        <Click Handler="RemoveItemOnGrid(gridUsersForRole,storeUsersForRole)" />
                    </Listeners>
                </ext:MenuItem>
                <ext:MenuItem ID="mnuEditUser" runat="server" Icon="Pencil" Text="Sửa">
                    <DirectEvents>
                        <Click OnEvent="InitEditedUser">
                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuSeparator runat="server" />
                <ext:MenuItem runat="server" ID="mnuQuanLy" Text="Quản lý" Icon="ApplicationCascade">
                    <Menu>
                        <ext:Menu ID="Menu2" runat="server">
                            <Items>
                                <ext:MenuItem runat="server" Icon="Reload" ID="MenuItem3" Text="Reset mật khẩu">
                                    <DirectEvents>
                                        <Click OnEvent="mnuResetPassword_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát" />
                                            <Confirmation ConfirmRequest="true" Title="Cảnh báo" Message="Đây là chức năng để thay đổi mật khẩu tự động. Được áp dụng đối với những trường hợp người dùng quên mật khẩu. Mật khẩu mới sẽ được gửi tự động vào Email của người dùng !" />
                                        </Click>
                                    </DirectEvents>
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Title="Đổi mật khẩu tự động" Html="Chọn một hoặc nhiều người dùng để đổi mật khẩu! Giữ Ctrl để chọn từng người dùng và giữ shift để chọn một khoảng người dùng">
                                        </ext:ToolTip>
                                    </ToolTips>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItem5" Icon="Key" runat="server" Text="Đổi mật khẩu">
                                    <Listeners>
                                        <Click Handler="if(#{hdfSeletedUserId}.getValue()==''){
                                                                                Ext.Msg.alert('Cảnh báo','Bạn chưa chọn tài khoản nào');
                                                                            }else
                                                                            {
                                                                                wdChangePassword.show();
                                                                            }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItem6" runat="server" Icon="Lock" Text="Khóa tài khoản">
                                    <DirectEvents>
                                        <Click OnEvent="mnuLockUser_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip3" runat="server" Title="Hướng dẫn" Html="Bạn có thể chọn một hoặc nhiều tài khoản để tiến hành khóa tài khoản">
                                        </ext:ToolTip>
                                    </ToolTips>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItem7" runat="server" Icon="Lock" Text="Mở tài khoản">
                                    <DirectEvents>
                                        <Click OnEvent="mnuUnlockUser_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                                <%--<ext:MenuItem ID="MenuItem8" runat="server" Icon="UserAdd" Text="Tạo tài khoản cho nhân viên" Hidden="true">
                                <Listeners>
                                    <Click Handler="ucChooseEmployee1_wdChooseUser.show();" />
                                </Listeners>
                            </ext:MenuItem>--%>
                            </Items>
                        </ext:Menu>
                    </Menu>
                </ext:MenuItem>
            </Items>
        </ext:Menu>

        <!-- choose employee -->
        <uc2:ChooseEmployee ID="ucChooseEmployee1" runat="server" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
    </form>
</body>
</html>
