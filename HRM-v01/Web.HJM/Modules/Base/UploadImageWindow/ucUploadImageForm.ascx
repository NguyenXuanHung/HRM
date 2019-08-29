<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.UserControl.Modules_Base_UploadImageWindow_ucUploadImageForm" Codebehind="ucUploadImageForm.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Window runat="server" Title="Chọn ảnh" Resizable="false" Icon="ImageAdd" Hidden="true"
    Padding="6" ID="wdUploadImageWindow" Width="400" Modal="true" AutoHeight="true">
    <Items>
        <ext:FormPanel runat="server" Border="false" ID="frmPanelUploadImage">
            <Items>
                <ext:Hidden runat="server" Text="" ID="hdfImageFolder" />
                <ext:Hidden runat="server" ID="hdfColumnName" />
                <ext:FileUploadField runat="server" ID="fufUploadControl" AllowBlank="false" RegexText="Định dạng tệp tin không hợp lệ"
                    Regex="(http(s?):)|([/|.|\w|\s])*\.(?:jpg|gif|png|bmp|jpeg|JPG|GIF|PNG|BMP\JPEG)" AnchorHorizontal="100%"
                    FieldLabel="Chọn ảnh">
                    <Listeners>
                        <FileSelected Handler="if (#{frmPanelUploadImage}.getForm().isValid())
                                                {
                                                    #{btnAccept}.enable();    
                                                }else
                                                {
                                                    Ext.Msg.alert('Thông báo','Định dạng file không hợp lệ, chương trình chỉ chấp nhận định dạng jpg, bmp, jpeg, gif, png !');
                                                    #{btnAccept}.disable();
                                                }" />
                    </Listeners>
                </ext:FileUploadField>
            </Items>
        </ext:FormPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnAccept" Text="Đồng ý" Icon="Accept">
            <DirectEvents>
                <Click OnEvent="btnAcceptUpload_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button1" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdUploadImageWindow}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="#{btnAccept}.disable();" />
    </Listeners>
</ext:Window>
