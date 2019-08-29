<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManagement.aspx.cs" Inherits="Web.HRM.Modules.Setting.RoleManagement" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">
        // role selected
        var handlerRoleChanged = function (roleId) {
            // set new selected role
            hdfRoleId.setValue(roleId);
            // show button edit, delete
            btnEdit.enable();
            btnDelete.enable();
            btnSaveMenuRole.enable();
            toggleCheckbox(false);
            // reload menu data
            window.Ext.net.DirectMethods.LoadMenuPermissionForRole();
        };
        // handler permission change
        var handlerPermissionChanged = function (chk) {
            // get menuId
            var menuId = chk.id.substr(chk.id.indexOf('_') + 1);
            // get checkbox permission of menu
            var chkRead = document.getElementById('read_' + menuId);
            var chkEdit = document.getElementById('edit_' + menuId);
            var chkDel = document.getElementById('del_' + menuId);
            var chkFull = document.getElementById('full_' + menuId);
            // init checked value
            if (chk.checked === true) {
                switch (chk.id) {
                case 'full_' + menuId:
                    chkDel.checked = true;
                    chkEdit.checked = true;
                    chkRead.checked = true;
                    break;
                case 'del_' + menuId:
                    chkEdit.checked = true;
                    chkRead.checked = true;
                    break;
                case 'edit_' + menuId:
                    chkRead.checked = true;
                    break;
                }
            } else {
                switch (chk.id) {
                case 'read_' + menuId:
                    chkEdit.checked = false;
                    chkDel.checked = false;
                    chkFull.checked = false;
                    break;
                case 'edit_' + menuId:
                    chkDel.checked = false;
                    chkFull.checked = false;
                    break;
                case 'del_' + menuId:
                    chkFull.checked = false;
                    break;
                }
            }
            hdfMenuRole.setValue(generateJSON());
        };
        // load menu permission
        var loadMenuPermission = function () {
            // uncheck all checkbox
            var inputs = document.getElementsByTagName('input');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type === 'checkbox') {
                    inputs[i].checked = false;
                }
            }
            // get current menu role
            var menuRoles = JSON.parse(hdfMenuRole.getValue());
            // loop from zero to end
            for (var j = 0; j < menuRoles.length; j++) {
                // get menuid, permission from json object
                var menuId = menuRoles[j].MenuId;
                var permission = menuRoles[j].Permission;
                // get perrmission checkbox of the menu
                var chkRead = document.getElementById('read_' + menuId);
                var chkEdit = document.getElementById('edit_' + menuId);
                var chkDel = document.getElementById('del_' + menuId);
                var chkFull = document.getElementById('full_' + menuId);
                // set permission value
                if (chkRead) {
                    chkRead.checked = permission[0] === '1' ? true : false;
                }
                if (chkEdit) {
                    chkEdit.checked = permission[1] === '1' ? true : false;
                }
                if (chkDel) {
                    chkDel.checked = permission[2] === '1' ? true : false;
                }
                if (chkFull) {
                    chkFull.checked = permission[3] === '1' ? true : false;
                }
            }
        };
        // save menu permission
        var saveMenuPermission = function() {
            // save menu data
            window.Ext.net.DirectMethods.SaveMenuPermissionForRole();
        };
        // generate json data
        var generateJSON = function() {
            var jsonData = '[';
            var lstDiv = document.getElementsByTagName('div');
            for (var i = 0; i < lstDiv.length; i++) {
                if (lstDiv[i].className === 'menuItem') {
                    // get menu id
                    var menuId = lstDiv[i].id;
                    // get checkbox permission of menu
                    var chkRead = document.getElementById('read_' + menuId);
                    var chkEdit = document.getElementById('edit_' + menuId);
                    var chkDel = document.getElementById('del_' + menuId);
                    var chkFull = document.getElementById('full_' + menuId);
                    // init default permission
                    var permissionArr = ["0", "0", "0", "0"];
                    // set perrmission props
                    if (chkRead.checked) permissionArr[0] = '1';
                    if (chkEdit.checked) permissionArr[1] = '1';
                    if (chkDel.checked) permissionArr[2] = '1';
                    if (chkFull.checked) permissionArr[3] = '1';
                    var permission = permissionArr[0] + permissionArr[1] + permissionArr[2] + permissionArr[3];
                    // create object
                    if (permission !== '0000') {
                        jsonData += '{"Id":0,' + '"MenuId":' + menuId + ',"RoleId":' + hdfRoleId.getValue() + ',"Permission":"' + permission + '"},';
                    }
                }
            }
            if (jsonData.endsWith(","))
                jsonData = jsonData.substr(0, jsonData.length - 1);
            jsonData += ']';
            return jsonData;
        };
        // enable / disable all checkbox
        var toggleCheckbox = function(disabled) {
            // enable all checkbox
            var inputs = document.getElementsByTagName('input');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type === 'checkbox') {
                    inputs[i].disabled = disabled;
                }
            }
        };
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- resource manager -->
            <ext:ResourceManager ID="RM" runat="server" />
            <!-- hidden field -->
            <ext:Hidden runat="server" ID="hdfRoleId" />
            <ext:Hidden runat="server" ID="hdfMenuRole" />
            <!-- store -->
            <ext:Store ID="roleStore" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/HandlerRole.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <Reader>
                    <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="RoleName" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="IsDeleted" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <!-- main viewport -->
            <ext:Viewport runat="server">
                <Items>
                    <ext:BorderLayout runat="server">
                        <West Split="true" Collapsible="true">
                            <ext:GridPanel runat="server" ID="gpRole" StoreID="roleStore" FireSelectOnLoad="true" Border="false" AutoExpandColumn="RoleName" Width="350" Title="Danh sách vai trò">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
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
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Locked="true" />
                                        <ext:TemplateColumn Header="Tên quyền" ColumnID="RoleName">
                                            <Template runat="server">
                                                <Html>
                                                    <div style="line-height: 18px;">
                                                        <b>{RoleName}</b><br />
                                                        <i>{Description}</i>
                                                    </div>
                                                </Html>
                                            </Template>
                                        </ext:TemplateColumn>
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="handlerRoleChanged(RowSelectionModel1.getSelected().get('Id'));" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();btnSaveMenuRole.disable();toggleCheckbox(true);" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbar1" PageSize="30" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();btnEdit.disable();btnDelete.disable();btnSaveMenuRole.disable();toggleCheckbox(true);" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </West>
                        <Center>
                            <ext:TabPanel ID="tabPanel" Border="false" runat="server">
                                <Items>
                                    <ext:Panel ID="pnlMenu" Title="Chức năng" Border="false" runat="server" AutoScroll="True">
                                        <Items>
                                            <ext:Panel ID="pnlMenuForRole" runat="server" Border="false">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" ID="toolbarMenuRole">
                                                        <Items>
                                                            <ext:Button runat="server" Disabled="true" ID="btnSaveMenuRole" Text="Lưu cấu hình" Icon="Disk">
                                                                <Listeners>
                                                                    <Click Handler="saveMenuPermission();" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Content>
                                                    <div id="menuRole" style='margin-left: 10px; width: 100%;'>
                                                        <asp:Literal runat="server" ID="ltrMenuTree" />
                                                    </div>
                                                </Content>
                                                <LoadMask ShowMask="True" Msg="Đang tải..."></LoadMask>
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:TabPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <!-- window add/edit role -->
            <ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="500" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
                <Items>
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên quyền<span style='color:red'> *</span>" AnchorHorizontal="100%" />
                    <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="100%" Height="110" EmptyText="Thông tin mô tả vai trò..." />
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
                            <Click Handler="wdSetting.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

