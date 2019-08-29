<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.LoginHJM" Codebehind="~/LoginHJM.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ĐĂNG NHẬP</title>
    <link href="/Resource/css/hjm/StyleSheet.css" rel="stylesheet" />
    <link href="/Resource/css/CustomStyle.css" rel="stylesheet" />

    <script type="text/javascript">
        var handlerLogin = function () {
            // validate form here
            // direct method
            window.Ext.net.DirectMethods.ProcessLogin();
        }

        var handlerEnterKeyPress = function (f, e) {
            if (e.getKey() === e.ENTER) {
                handlerLogin();
            }
        }
    </script>

    <!--[if lt IE 8]>
        <style type='text/css'>
            #table
            {
                margin:0px auto !important;
            }
            *{
                font-size:19px !important;
            }
        </style>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM" />
            <div id="LoginHeader">
                <div id="InsideLoginHeader"></div>
            </div>
            <ext:Window runat="server" ID="wdResetPassword" Draggable="false" Title="Quên mật khẩu" Width="350" AutoHeight="true" 
                        Padding="10" Icon="Key" Hidden="true" Modal="false" Layout="FormLayout">
                <Items>
                    <ext:FormPanel runat="server" ID="fpnlInputUser" BodyPadding="5" ButtonAlign="Right" Layout="FormLayout" Border="false">
                        <Items>
                            <ext:TextField ID="txtResetUsername" AnchorHorizontal="100%" FieldLabel="Tài khoản"
                                AllowBlank="false" runat="server" EmptyText="Nhập tài khoản" />
                            <ext:TextField ID="txtResetEmail" Regex="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                AnchorHorizontal="100%" FieldLabel="Email" runat="server"
                                AllowBlank="false" EmptyText="Nhập email" />
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnResetPassword" Icon="Accept" runat="server" Text="Lấy mật khẩu">
                        <Listeners>
                            <Click Handler="if (!#{fpnlInputUser}.getForm().isValid()) {
	                            Ext.Msg.show({icon: Ext.MessageBox.ERROR, title: 'Thông báo', msg: 'Email phải đúng định dạng và tên tài khoản tồn tại trong hệ thống', buttons:Ext.Msg.OK});
                                return false;
                            }" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="ResetPassword">
                                <EventMask ShowMask="true" Msg="Đang tải..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Icon="Decline" Text="Đóng">
                        <Listeners>
                            <Click Handler="#{wdResetPassword}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <div id="WrapperLogin">
                <div id="QuocHuy"></div>
                <div id="LoginBox">
                    <div id="logo">
                    </div>
                    <div id="BoxLogin">
                        <p id="ptitle">
                            Đăng nhập hệ thống
                        </p>
                        <table border="0" id="table" cellpadding="6" cellspacing="6">
                            <tr>
                                <td>
                                    <ext:TextField runat="server" LabelAlign="Top" ID="txtUserName"  EnableKeyEvents="true" Width="255" FieldLabel="Tài khoản">
                                        <Listeners>
                                            <KeyPress Fn="handlerEnterKeyPress" />
                                        </Listeners>
                                    </ext:TextField>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ext:TextField runat="server" LabelAlign="Top" InputType="Password"
                                        EnableKeyEvents="true" Width="255" ID="txtPassword" FieldLabel="Mật khẩu">
                                        <ToolTips>
                                            <ext:ToolTip Closable="true" Draggable="true" AutoHide="false" ID="tl" runat="server"
                                                Title="Nhập mật khẩu" Header="true" Frame="true" Html="Vì mật khẩu phân biệt chữ hoa chữ thường nên nếu bạn đang bật phím Caps Lock thì hãy tắt đi">
                                            </ext:ToolTip>
                                        </ToolTips>
                                        <Listeners>
                                            <Blur Handler="tl.hide();" />
                                            <Focus Handler="tl.show();" />
                                        </Listeners>
                                        <Listeners>
                                            <KeyPress Fn="handlerEnterKeyPress" />
                                        </Listeners>
                                    </ext:TextField>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ext:LinkButton runat="server" ID="aQuenmk" Cls="alink" Text="Quên mật khẩu">
                                        <Listeners>
                                            <Click Handler="#{wdResetPassword}.show();" />
                                        </Listeners>
                                    </ext:LinkButton>
                                    <ext:Button runat="server" Icon="User" Cls="btnLogin" ID="btnLogin" Text="Đăng nhập" Height="25" Width="100">
                                        <Listeners>
                                            <Click Handler="handlerLogin();"></Click>
                                        </Listeners>
                                    </ext:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div id="bottom">
                <h1 class="copyright">&copy; Bản quyền thuộc về <a href="http://dthsoft.com.vn">DTH Soft</a> và <a href="https://vgroup.vn">VGROUP</a> &nbsp;&nbsp;|&nbsp;&nbsp; Phân phối bởi: <a href="http://dthsoft.com.vn">DTH Soft</a></h1>
            </div>
        </div>
    </form>
</body>
</html>
