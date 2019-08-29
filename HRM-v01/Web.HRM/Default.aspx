<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Default" CodeBehind="~/Default.aspx.cs" %>

<%@ Register Src="~/Modules/UC/SystemConfiguration.ascx" TagPrefix="CCVC" TagName="SystemConfiguration" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Web.Core.Framework.Resource.Get("ApplicationName") %></title>

    <link rel="shortcut icon" href="/favicon.ico" type="image/ico" />
    <link href="Resource/css/IconStyle.css" rel="stylesheet" type="text/css" />
    
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />

    <script type="text/javascript">
        function SetTreeNodeID(NodeID) {
            document.getElementById("hdfTreeNodeID").value = NodeID;
        }
        function ResetTreeID() {
            document.getElementById("hdfTreeNodeID").value = "";
        }
        var getValues = function (tree) {
            var msg = [],
                selNodes = tree.getChecked();

            Ext.each(selNodes, function (node) {
                //msg.push(node.id);
            });

            return msg.join(",");
        };
        //Thiết lập checked cho tree
        var SetNodeChecked = function (tree, textfield) {
            //  tree.getNodeById("4").getUI().checkbox.checked = true;
            //  alert("hehe");
        }
        var getRoleTextAndValue = function (tree) {
            var msg = [],
                selNodes = tree.getChecked();
            document.getElementById("hdfRoleID").value = "";
            Ext.each(selNodes, function (node) {
                msg.push(node.text);
                document.getElementById("hdfRoleID").value += node.id + ",";
            });

            return msg.join(",");
        };
        var LastChoice = new Array();
        var getText = function (tree) {
            var CurrentChoice = new Array();
            var msg = [],
                selNodes = tree.getChecked();
            var i = 0;
            Ext.each(selNodes, function (node) {
                CurrentChoice[i] = node.id;
                i = i + 1;
            });

            var CurrentNodeID = GetNodeID(CurrentChoice);
            Ext.each(selNodes, function (node) {
                node.getUI().checkbox.checked = false;
                if (CurrentNodeID == node.id) {
                    node.getUI().checkbox.checked = true;
                    msg.push(node.text);
                    document.getElementById("hdfParentID").value = node.id;
                }
            });
            return msg.join("");
        };

        var GetNodeID = function (CurrentChoice) {
            var rs = CurrentChoice[0];
            for (var i = 0; i < CurrentChoice.length; i++) {
                if (IsInLastChoice(CurrentChoice[i]) == false) {
                    rs = CurrentChoice[i];
                    break;
                }
            }
            for (var i = 0; i < CurrentChoice.length; i++) {
                LastChoice[i] = CurrentChoice[i];
            }
            return rs;
        };

        var IsInLastChoice = function (nodeID) {
            for (var j = 0; j < LastChoice.length; j++) {
                if (nodeID == LastChoice[j]) {
                    return true;
                }
            }
            return false;
        };

        var validateChangePasswordForm = function () {
            if (txtOldPassword.getValue() == '') {
                var content = "<asp:Literal runat='server' Text='Bạn chưa nhập đầy đủ thông tin' />";
                alert(content);
                txtOldPassword.focus();
                return false;
            }
            if (txtNewPassword.getValue() == '') {
                var content = "<asp:Literal runat='server' Text='Bạn chưa nhập đầy đủ thông tin' />";
                alert(content);
                txtNewPassword.focus();
                return false;
            }
            if (txtConfirmNewPassword.getValue() == '') {
                var content = "<asp:Literal runat='server' Text='Bạn chưa nhập đầy đủ thông tin' />";
                alert(content);
                txtConfirmNewPassword.focus();
                return false;
            }
            if (txtNewPassword.getValue() != txtConfirmNewPassword.getValue()) {
                var cont = "<asp:Literal runat='server' Text='Mật khẩu mới không khớp nhau' />";
                Ext.Msg.alert(Title, cont);
                return false;
            }
            return true;
        };

        var KiemTraPhanQuyen = function () {
            if (txtControlText.getValue().trim() == '') {
                alert('Bạn chưa nhập Tên định danh');
                txtControlText.focus();
                return false;
            }
            if (txtDescription.getValue().trim() == '') {
                alert('Bạn chưa nhập Tên chức năng');
                txtDescription.focus();
                return false;
            }
            if (cbxChucNangCha.getValue() == '') {
                alert('Bạn chưa chọn chức năng cha');
                cbxChucNangCha.focus();
                return false;
            }
            return true;
        };

        var resetWdFunction = function () {
            txtControlText.reset();
            txtDescription.reset();
            //cbxChucNangCha.reset();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="rm" runat="server">
                <Listeners>
                    <DocumentReady Handler="addTab(pnlCenter, 'dashboard', 'Modules/Home/Default.aspx', 'Bàn làm việc');" />
                </Listeners>
            </ext:ResourceManager>

            <!--view port-->
            <ext:Viewport runat="server" Layout="Fit">
                <Items>
                    <ext:BorderLayout ID="BorderLayout1" runat="server">
                        <North Split="false" Collapsible="true">
                            <ext:Panel runat="server" Height="27" Border="false" Title="false" Header="false">
                                <Items>
                                    <ext:Toolbar ID="tbMenuTop" runat="server" EnableOverflow="true">
                                        <Items>
                                            <ext:ToolbarFill />
                                            <ext:Button ID="btnUser" runat="server" Icon="User">
                                                <Menu>
                                                    <ext:Menu runat="server" Id="mnuTop">
                                                        <Items>
                                                            <ext:MenuItem runat="server" Id="mnuItemChangePassword" Icon="Key" Text="Đổi mật khẩu">
                                                                <Listeners>
                                                                    <Click Handler="wdChangePassword.show();" />
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" Id="mnuItemLogout" Icon="DoorOut" Text="Thoát">
                                                                <DirectEvents>
                                                                    <Click OnEvent="Logout">
                                                                        <Confirmation ConfirmRequest="true" Title="Cảnh báo" Message="Bạn có chắc muốn thoát khỏi hệ thống?" />
                                                                        <EventMask ShowMask="true" Msg="Thoát..." />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server" ID="toolbarSeperatorSystem" Visible="False" ti />
                                            <ext:Button runat="server" ID="mnuSystem" Text="Hệ thống&nbsp;" Icon="CogEdit" Visible="False" >
                                                <Menu>
                                                    <ext:Menu runat="server" ID="mnuSystemItems" />
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarSpacer runat="server" Width="10"/>
                                        </Items>
                                    </ext:Toolbar>
                                </Items>
                            </ext:Panel>
                        </North>
                        <West>
                            <ext:Panel runat="server" ID="pnlWest" Width="250" Layout="AccordionLayout" MinWidth="250" MaxWidth="400" Title="CHỨC NĂNG QUẢN LÝ" Icon="Table" Collapsible="True">
                                <Items></Items>
                            </ext:Panel>
                        </West>
                        <Center MarginsSummary="5 5 0 5">
                            <ext:TabPanel ID="pnlCenter" EnableTabScroll="true" runat="server">
                                <Plugins>
                                    <ext:TabCloseMenu CloseTabText="Đóng tab" CloseOtherTabsText="Đóng tab khác" CloseAllTabsText="Đóng tất cả tab" />
                                    <ext:TabScrollerMenu runat="server" />
                                </Plugins>  
                            </ext:TabPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

            <!--window for change password-->
            <ext:Window ID="wdChangePassword" Border="false" runat="server" Width="400" AutoHeight="true" Resizable="false" Modal="true" Icon="Lock" Title="Đổi mật khẩu" Hidden="true" Padding="6" LabelWidth="130" Layout="Form">
                <Items>
                    <ext:TextField ID="txtOldPassword" AllowBlank="false" runat="server" FieldLabel="Mật khẩu cũ" InputType="Password" AnchorHorizontal="100%" />
                    <ext:TextField ID="txtNewPassword" AllowBlank="false" runat="server" FieldLabel="Mật khẩu mới" InputType="Password" AnchorHorizontal="100%" />
                    <ext:TextField ID="txtConfirmNewPassword" AllowBlank="false" runat="server" Vtype="password" FieldLabel="Nhập lại mật khẩu mới" InputType="Password" MsgTarget="Side" AnchorHorizontal="100%">
                        <CustomConfig>
                            <ext:ConfigItem Name="initialPassField" Value="#{txtNewPassword}" Mode="Value" />
                        </CustomConfig>
                    </ext:TextField>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Icon="Disk" Text="Cập nhật" ID="btnUpdatePassword">
                        <Listeners>
                            <Click Handler="return validateChangePasswordForm();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="UpdatePassword" />
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Icon="Decline" Text="Hủy" ID="btnClose">
                        <Listeners>
                            <Click Handler="wdChangePassword.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>

            <!-- user control -->
            <CCVC:SystemConfiguration runat="server" id="systemConfig" />
           
        </div>
    </form>
</body>
</html>
