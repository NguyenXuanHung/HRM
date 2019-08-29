<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.System.ModulesSystemRoleDefault" Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Resource/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="Resource/role.css" type="text/css" />
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/Resource/js/RenderJS.js"></script>
    <script type="text/javascript" src="Resource/gettheme.js"></script>
    <script type="text/javascript" src="Resource/jqxcore.js"></script>
    <script type="text/javascript" src="Resource/jqxbuttons.js"></script>
    <script type="text/javascript" src="Resource/jqxscrollbar.js"></script>
    <script type="text/javascript" src="Resource/jqxpanel.js"></script>
    <script type="text/javascript" src="Resource/jqxtree.js"></script>
    <script type="text/javascript" src="Resource/jqxcheckbox.js"></script>
    <script type="text/javascript" src="Resource/role.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- resource manager -->
            <ext:ResourceManager ID="RM" runat="server" />
            <!-- hidden field -->
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfMaDonVi" />
            <ext:Hidden runat="server" ID="hdfMenusForRole" />
            <ext:Hidden runat="server" ID="hdfFunctionOfMenu" />
            <ext:Hidden runat="server" ID="hdfDVC" />
            <!-- main viewport -->
            <ext:Viewport runat="server">
                <Items>
                    <ext:ColumnLayout runat="server" FitHeight="true" Split="true" ID="br">
                        <Columns>
                            <ext:LayoutColumn ColumnWidth="0.4">
                                <ext:GridPanel ID="gridPanelViewport" FireSelectOnLoad="true" Border="false" AutoExpandColumn="RoleName" runat="server">
                                    <TopBar>
                                        <ext:Toolbar runat="server" ID="tb">
                                            <Items>
                                                <ext:Button runat="server" Text="Thêm quyền" Icon="KeyAdd" ID="btnAddKey">
                                                    <Listeners>
                                                        <Click Handler="wdAddRole.show();" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button runat="server" Text="Sửa" Icon="Pencil" Disabled="true" ID="btnEditRole">
                                                    <Listeners>
                                                        <Click Handler="wdAddRole.setTitle('Sửa quyền');btnDuplicateRole.hide();btnUpdateRole.hide();btnUpdateRoleAndClose.hide();btnEdit.show();wdAddRole.show();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="InitEditingRole">
                                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Button runat="server" Text="Xóa" Icon="Delete" Disabled="true" ID="btnDeleteRole">
                                                    <Listeners>
                                                        <Click Handler="deleteRole(gridPanelViewport, roleStore);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:ToolbarSeparator runat="server" ID="sp" />
                                                <ext:Button runat="server" Text="Nhân đôi dữ liệu" Icon="DiskMultiple">
                                                    <Listeners>
                                                        <Click Handler="wdAddRole.setTitle('Nhân đôi quyền');btnDuplicateRole.show();btnUpdateRole.hide();btnUpdateRoleAndClose.hide();wdAddRole.show();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="InitEditingRole">
                                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="roleStore" runat="server">
                                            <Proxy>
                                                <ext:HttpProxy Method="GET" Url="~/Services/HandlerSystemRole.ashx" />
                                            </Proxy>
                                            <AutoLoadParams>
                                                <ext:Parameter Name="start" Value="={0}" />
                                                <ext:Parameter Name="limit" Value="={5}" />
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
                                    </Store>
                                    <ColumnModel ID="columnModel" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn Header="STT" Width="30" Locked="true" />
                                            <ext:TemplateColumn Header="Tên quyền" ColumnID="RoleName" Width="220">
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
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel" SingleSelect="true" runat="server">
                                            <Listeners>
                                                <RowSelect Handler="#{hdfRecordId}.setValue(#{rowSelectionModel}.getSelected().id);btnDeleteRole.enable();btnEditRole.enable();
                                                    btnLuuThietLap.enable();Ext.net.DirectMethods.LoadMenusForRole(#{rowSelectionModel}.getSelected().id);" />
                                            </Listeners>
                                        </ext:RowSelectionModel>
                                    </SelectionModel>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="pagingToolbar" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                            PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                            DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                        </ext:PagingToolbar>
                                    </BottomBar>
                                    <Listeners>
                                        <RowContextMenu Handler="e.preventDefault(); #{RowContextMenu}.dataRecord = this.store.getAt(rowIndex);#{RowContextMenu}.showAt(e.getXY());#{gridPanelViewport}.getSelectionModel().selectRow(rowIndex);" />
                                    </Listeners>
                                    <LoadMask ShowMask="true" />
                                    <DirectEvents>
                                        <DblClick OnEvent="InitEditingRole">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </DblClick>
                                    </DirectEvents>
                                </ext:GridPanel>
                            </ext:LayoutColumn>
                            <ext:LayoutColumn ColumnWidth="0.5">
                                <ext:TabPanel ID="tabPanel" Border="false" runat="server">
                                    <Items>
                                        <ext:Panel ID="pnlMenu" Title="Danh sách chức năng" Border="false" runat="server">
                                            <Items>
                                                <ext:ColumnLayout ID="ColumnLayout1" runat="server" Split="true" FitHeight="true">
                                                    <Columns>
                                                        <ext:LayoutColumn ColumnWidth="1">
                                                            <ext:Panel ID="pnlMenuForRole" runat="server" Border="false">
                                                                <TopBar>
                                                                    <ext:Toolbar runat="server" ID="tbds">
                                                                        <Items>
                                                                            <ext:Button runat="server" Disabled="true" ID="btnLuuThietLap" Text="Lưu thiết lập" Icon="Disk">
                                                                                <Listeners>
                                                                                    <Click Handler="saveMenusForRole();" />
                                                                                </Listeners>
                                                                            </ext:Button>
                                                                        </Items>
                                                                    </ext:Toolbar>
                                                                </TopBar>
                                                                <Content>
                                                                    <div id='jqxWidget'>
                                                                        <div id='jqxMenu' style='float: left; margin-left: 20px;'>
                                                                            <ul>
                                                                                <asp:Literal runat="server" ID="ltrMenuTree" />
                                                                            </ul>
                                                                        </div>
                                                                    </div>
                                                                </Content>
                                                            </ext:Panel>
                                                        </ext:LayoutColumn>
                                                        <%--<ext:LayoutColumn ColumnWidth="0.5">
                                                            <ext:Panel ID="pnlMenuPermission" Border="false" runat="server">
                                                                <TopBar>
                                                                    <ext:Toolbar runat="server" ID="tbdscn">
                                                                        <Items>
                                                                            <ext:Button ID="Button3" runat="server" Text="Chọn tất cả" Icon="BulletTick">
                                                                                <Listeners>
                                                                                    <Click Handler="CheckAllFunction()" />
                                                                                </Listeners>
                                                                            </ext:Button>
                                                                            <ext:Button ID="Button4" runat="server" Text="Hủy chọn tất cả" Icon="BulletCross">
                                                                                <Listeners>
                                                                                    <Click Handler="UnCheckAllFunction()" />
                                                                                </Listeners>
                                                                            </ext:Button>
                                                                        </Items>
                                                                    </ext:Toolbar>
                                                                </TopBar>
                                                                <Content>
                                                                    <asp:Literal runat="server" ID="ltrMenuPermission"></asp:Literal>
                                                                </Content>
                                                            </ext:Panel>
                                                        </ext:LayoutColumn>--%>
                                                    </Columns>
                                                </ext:ColumnLayout>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:TabPanel>
                            </ext:LayoutColumn>
                        </Columns>
                    </ext:ColumnLayout>
                </Items>
            </ext:Viewport>
            <!-- window add/edit role -->
            <ext:Window ID="wdAddRole" runat="server" Border="false" Padding="0" Icon="KeyAdd" Constrain="true" Width="460" Modal="true"
                Maximizable="false" Resizable="false" AutoHeight="true" Title="Thêm quyền" Hidden="true">
                <Items>
                    <ext:Panel ID="pnlAddRole" runat="server" Frame="true" Padding="6" Border="false" Layout="FormLayout">
                        <Items>
                            <ext:Hidden runat="server" ID="txtRoleCommand" />
                            <ext:TextField runat="server" ID="txtRoleName" CtCls="requiredData" FieldLabel="Tên quyền<span style='color:red'>*</span>" AnchorHorizontal="100%" />
                            <ext:TextArea ID="txtDescription" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%" Height="110" EmptyText="Thông tin mô tả quyền..." />
                        </Items>
                    </ext:Panel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Hidden="true" ID="btnDuplicateRole" Text="Nhân đôi dữ liệu" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="DuplicateRole">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" AnchorHorizontal="center" Text="Cập nhật" Icon="Disk" ID="btnUpdateRole">
                        <Listeners>
                            <Click Handler="if(#{txtRoleName}.getValue().trim()==''){alert('Bạn chưa nhập tên quyền');#{txtRoleCommand}.focus();return false;}" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdateRole">
                                <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Insert" />
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" AnchorHorizontal="center" Text="Cập nhật & Đóng lại" Icon="Disk" ID="btnUpdateRoleAndClose">
                        <Listeners>
                            <Click Handler="if(#{txtRoleName}.getValue().trim()==''){alert('Bạn chưa nhập tên quyền');#{txtRoleCommand}.focus();return false;}" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdateRole">
                                <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Insert" />
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Hidden="true" AnchorHorizontal="center" Text="Cập nhật" Icon="Disk" ID="btnEdit">
                        <Listeners>
                            <Click Handler="if(#{txtRoleName}.getValue().trim()==''){alert('Bạn chưa nhập tên quyền');#{txtRoleCommand}.focus();return false;}" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdateRole">
                                <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="btnClose">
                        <Listeners>
                            <Click Handler="#{wdAddRole}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <BeforeShow Handler="if(txtRoleCommand.getValue()=='Update'){btnUpdateRole.hide();btnUpdateRoleAndClose.hide();btnEdit.show();}" />
                    <Hide Handler="resetForm();" />
                </Listeners>
            </ext:Window>
            <!-- context menu -->
            <ext:Menu ID="RowContextMenu" runat="server">
                <Items>
                    <ext:MenuItem ID="mnuAddRole" runat="server" Icon="Add" Text="Thêm quyền">
                        <Listeners>
                            <Click Handler="wdAddRole.show();" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem ID="mnuUpdateRole" runat="server" Icon="Pencil" Text="Sửa">
                        <DirectEvents>
                            <Click OnEvent="InitEditingRole">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    <ext:MenuItem ID="MnuDeleteRole" runat="server" Icon="Delete" Text="Xóa">
                        <Listeners>
                            <Click Handler="deleteRole(gridPanelViewport, roleStore);" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                    <ext:MenuItem ID="mnuDuplicateData" runat="server" Icon="DiskMultiple" Text="Nhân đôi dữ liệu">
                        <Listeners>
                            <Click Handler="wdAddRole.setTitle('Nhân đôi quyền');btnDuplicateRole.show();btnUpdateRole.hide();btnUpdateRoleAndClose.hide();wdAddRole.show();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InitEditingRole">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                </Items>
            </ext:Menu>

        </div>
    </form>
</body>
</html>
