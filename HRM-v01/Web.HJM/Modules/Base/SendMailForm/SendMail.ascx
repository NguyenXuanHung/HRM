<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_SendMailForm_SendMail" Codebehind="SendMail.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">
    var CheckInputEmailInformation = function (emailGo, password, emailTo, subject, content) {
        if (emailGo.getValue().trim() == '') {
            Ext.Msg.alert('Thông báo', 'Bạn chưa nhập địa chỉ mail đi');
            return false;
        }
        if (password.getValue().trim() == '') {
            Ext.Msg.alert('Thông báo', 'Bạn chưa nhập mật khẩu');
            return false;
        }
        if (emailTo.getValue().trim() == '') {
            Ext.Msg.alert('Thông báo', 'Bạn chưa nhập địa chỉ mail đến');
            return false;
        }
        if (subject.getValue().trim() == '') {
            Ext.Msg.alert('Thông báo', 'Bạn chưa nhập tiêu đề hòm thư');
            return false;
        }
        if (content.getValue().trim() == '') {
            Ext.Msg.alert('Thông báo', 'Bạn chưa nhập nội dung thư');
            return false;
        }
        return true;
    }
    var FocusAfterAlert = function (emailGo, password, emailTo, subject, content) {
        if (emailGo.getValue().trim() == '') {
            emailGo.focus();
            alert("da vao day rui");
        }
    }
</script>
<ext:Window Icon="EmailGo" runat="server" Modal="true" LabelWidth="110" Padding="6"
    Title="Gửi Email" Constrain="true" Layout="FormLayout" ID="wdSendEmail" Width="600"
    AutoHeight="true" Hidden="true">
    <Items>
        <ext:TextField runat="server" ID="txtMailGo" AnchorHorizontal="100%" AllowBlank="false"
            BlankText="Bắt buộc phải nhập địa chỉ mail đi" FieldLabel="Gửi từ" Regex="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
            EmptyText="Địa chỉ hòm thư của bạn ! Chỉ có thể là địa chỉ yahoo hoặc gmail"
            RegexText="Địa chỉ Email không chính xác" />
        <ext:TextField runat="server" ID="txtPassword" InputType="Password" AnchorHorizontal="100%"
            FieldLabel="Mật khẩu" />
        <ext:TextField runat="server" ID="txtMailTo" AnchorHorizontal="100%" FieldLabel="Gửi đến"
            AllowBlank="false" BlankText="Bắt buộc phải nhập địa chỉ mail đến" Regex="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
            RegexText="Địa chỉ Email không chính xác" EmptyText="Địa chỉ cần gửi đến" />
        <ext:TextField runat="server" AnchorHorizontal="100%" FieldLabel="Tiêu đề" ID="txtTieuDe" />
        <ext:HtmlEditor runat="server" AnchorHorizontal="100%" ID="htmlMail" FieldLabel="Nội dung hòm thư">
        </ext:HtmlEditor>
    </Items>
    <Listeners>
        <Hide Handler="#{txtMailGo}.reset();#{txtPassword}.reset();#{txtMailTo}.reset();#{txtTieuDe}.reset();#{htmlMail}.reset();" />
    </Listeners>
    <Buttons>
        <ext:Button ID="btnSendMail" runat="server" Text="Gửi" Icon="EmailGo">
            <%--<Listeners>
                <Click Handler="return CheckInputEmailInformation(#{txtMailGo},#{txtPassword},#{emailTo},#{subject},#{content});" />
            </Listeners>--%>
            <DirectEvents>
                <Click OnEvent="btnSendMail_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Text="Đóng lại" ID="ClosewdSendEmail" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdSendEmail}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
