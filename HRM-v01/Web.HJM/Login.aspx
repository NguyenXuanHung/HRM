<%@ Page Language="C#" AutoEventWireup="true" Theme="LoginHRM2013" Inherits="Web.HJM.Login" Codebehind="~/Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập</title>
    <script type="text/javascript">
        var handlerLogin = function() {
            // validate form here
            // direct method
            window.Ext.net.DirectMethods.DirectLogin();
        }

        var handlerEnterKeyPress = function (f, e) {
            if (e.getKey() === e.ENTER) {
                handlerLogin();
            }
        }
    </script>
    <style type="text/css">
        div#FormPanel1 .x-panel-body
        {
            background: #DFE8F6 !important;
        }

        div#ext-gen47, div#ext-gen51, div#ext-gen55, div#ext-gen59
        {
            position: relative !important;
        }
    </style>
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
            <ext:Window Draggable="false" Title="<%$ Resources:Language, forgot_password%>" Width="350"
                runat="server" Icon="Key" Padding="10" ID="wdResetPassword" Hidden="true" AutoHeight="true"
                Modal="false" Layout="FormLayout">
                <Items>
                    <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5" ButtonAlign="Right" Layout="FormLayout" Border="false">
                        <Items>
                            <ext:TextField ID="txtResetUsername" AnchorHorizontal="100%" FieldLabel="<%$ Resources:Language, username%>"
                                AllowBlank="false" runat="server" EmptyText="<%$ Resources:Language, enter_username%>" />
                            <ext:TextField ID="txtResetEmail" Regex="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                AnchorHorizontal="100%" FieldLabel="<%$ Resources:Language, email%>" runat="server"
                                AllowBlank="false" EmptyText="<%$ Resources:Language, enter_email%>" />
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnResetPassword" Icon="Accept" runat="server" Text="<%$ Resources:Language, txt_get_password%>">
                        <Listeners>
                            <Click Handler="if (!#{FormPanel1}.getForm().isValid()) {
	                            Ext.Msg.show({icon: Ext.MessageBox.ERROR, title: 'Thông báo', msg: 'Email phải đúng định dạng và tên tài khoản tồn tại trong hệ thống', buttons:Ext.Msg.OK});
                                return false;
                            }" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnResetPassword_Click">
                                <EventMask ShowMask="true" Msg="<%$ Resources:Language, handling%>" />
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
            <ext:Window ID="wdCapKey" Border="false" runat="server" Width="400" AutoHeight="true"
                Resizable="false" Modal="true" Icon="Lock" Title="<%$ Resources:Language, wd_key_donvi%>"
                Hidden="true" Padding="6" LabelWidth="130" Layout="Form">
                <Items>
                    <ext:TextField ID="txtNewKey" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:Language, key_donvi%>"
                        InputType="Password" AnchorHorizontal="100%">
                    </ext:TextField>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Icon="Disk" Text="<%$ Resources:Language, update%>" ID="btnUpdateKey">
                        <Listeners>
                            <Click Handler="return checkUpdateKey();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnUpdateKey_Click" />
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Icon="Decline" Text="<%$ Resources:Language, cancel%>"
                        ID="btnClose">
                        <Listeners>
                            <Click Handler="wdCapKey.hide();" />
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
                                    <ext:TextField runat="server" LabelAlign="Top" ID="txtUserName"  EnableKeyEvents="true" Width="255" FieldLabel="<%$ Resources:Language, username%>">
                                        <Listeners>
                                            <KeyPress Fn="handlerEnterKeyPress" />
                                        </Listeners>
                                    </ext:TextField>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ext:TextField runat="server" LabelAlign="Top" InputType="Password"
                                        EnableKeyEvents="true" Width="255" ID="txtPassword" FieldLabel="<%$ Resources:Language, txt_password%>">
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
                                    <ext:Hidden ID="hdfKey" runat="server" />
                                    <ext:ComboBox runat="server" LabelAlign="Top" AllowBlank="false" FieldLabel="Chọn đơn vị" DisplayField="Name"
                                        ID="CbDepartment" ValueField="Id" ItemSelector="div.list-item" Width="255" Editable="false">
                                        <Store>
                                            <ext:Store AutoLoad="false" runat="server" OnRefreshData="stDepartment_OnRefreshData" ID="stDepartment">
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id">
                                                        <Fields>
                                                            <ext:RecordField Name="Name" />
                                                            <ext:RecordField Name="Id" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        
                                        <Template ID="Template3" runat="server">
                                            <Html>
                                                <tpl for=".">
						                           <div class="list-item"> 
                                                       <div style="line-height:18px;">
                                                           {Name}<br />
                                                       </div> 
						                           </div>
					                            </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Expand Handler="if(stDepartment.getCount()==0) stDepartment.reload();" />
                                            <Select Handler="hdfKey.setValue(this.getValue());if (Ext.net.DirectMethods.checkkey()) {wdCapKey.show()};" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ext:LinkButton runat="server" ID="aQuenmk" Cls="alink" Text="<%$ Resources:Language, forgot_password%>">
                                        <Listeners>
                                            <Click Handler="#{wdResetPassword}.show();" />
                                        </Listeners>
                                    </ext:LinkButton>
                                    <ext:Button runat="server" Icon="User" Cls="btnLogin" ID="btnLogin" Text="<%$ Resources:Language, login%>" Height="25" Width="100">
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
                <h1 class="copyright">&copy; Được phát triển bởi Công ty Cổ Phần Công Nghệ DTH và Giải Pháp Số</h1>
                <h1 class="copyright">website: <a href="http://dthsoft.com.vn">www.dthsoft.com.vn</a></h1>
                <span style="font-size: 11px; font-style: italic; font-family: Tahoma;">Phần mềm chạy ổn định trên các trình duyệt : Chrome, Chrome cộng, Firefox, Opera, Safari</span>
            </div>
        </div>
    </form>
</body>
</html>
