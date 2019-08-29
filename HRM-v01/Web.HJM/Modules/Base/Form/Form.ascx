<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_Form_Form" Codebehind="Form.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../CoreBase/TableList/TableList.ascx" TagName="TableList" TagPrefix="uc1" %>
<%@ Register Src="../CoreBase/TableList/ColumnList.ascx" TagName="ColumnList" TagPrefix="uc2" %>
<%@ Register Src="Config/ConfigLayout.ascx" TagName="ConfigLayout" TagPrefix="uc3" %>
<%@ Register Src="../MiniGridPanel/MiniGrid.ascx" TagName="MiniGrid" TagPrefix="uc4" %>
<link href="../../../Resource/css/IconStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var GetVisibleStatus = function (value, p, record) {
        if (value == "1")
            return "<span style='color:blue'>Hiện</span>";
        else
            return "<span style='color:red'>Ẩn</span>";
    }
    var GetAutoGenerateValue = function (value, p, record) {
        if (value == "")
            return "Người dùng nhập";
        if (value == "IntIncrement")
            return "<span style='color:red'>Số nguyên tự động tăng<span>";
        if (value == "UseTrigger")
            return "<span style='color:red'>Trigger tạo khóa chính<span>";
        if (value == "CreatedDate")
            return "<span style='color:red'>Ngày tạo<span>";
        if (value == "CreatedBy")
            return "<span style='color:red'>Người tạo<span>";
        if (value == "EdittedDate")
            return "<span style='color:red'>Ngày sửa<span>";
        if (value == "EdittedBy")
            return "<span style='color:red'>Người sửa<span>";
        if (value == "MaDonVi")
            return "<span style='color:red'>Mã đơn vị<span>";
    }
    var GetInputControlValue = function (value, p, record) {
        if (value == "")
            return "Mặc định";
        return "<b style='color:blue;'>" + value + "</b>";
    }
    //duoc su dung cho tree
    var getTasks = function (tree) {
        var msg = [],
            selNodes = tree.getChecked();
        msg.push("[");

        Ext.each(selNodes, function (node) {
            if (msg.length > 1) {
                msg.push(",");
            }

            msg.push(node.text);
        });

        msg.push("]");

        return msg.join("");
    };
    var ConfirmToSave = function (directMethod, clickUpdateStatus) {
        if (clickUpdateStatus.getValue() != "")
            return;
        Ext.MessageBox.buttonText.yes = " Có ";
        Ext.MessageBox.buttonText.no = " Không ";
        Ext.Msg.confirm('Xác nhận', 'Bạn có muốn lưu lại thông tin không ?', function (btn) {
            if (btn == 'yes') {
                directMethod.btnUpdate_Click_DirectMethod();
            }
        });
    }
    //phục vụ cho bắt lỗi ngày tháng
    var onKeyUp = function (field) {
        var v = this.processValue(this.getRawValue()),
                field;

        if (this.startDateField) {
            field = Ext.getCmp(this.startDateField);
            field.setMaxValue();
            this.dateRangeMax = null;
        } else if (this.endDateField) {
            field = Ext.getCmp(this.endDateField);
            field.setMinValue();
            this.dateRangeMin = null;
        }
        field.validate();
    }; 
</script>
<asp:PlaceHolder runat="server" ID="plcConfigControl"></asp:PlaceHolder>
<ext:Hidden runat="server" ID="hdfClickUpdateStatus" />
<%--Lưu câu truy vấn để lấy dữ liệu phục vụ cho hàm SetValue--%>
<ext:Hidden runat="server" ID="hdfQuery" EnableViewState="true" />
<ext:FormPanel runat="server" ID="frmControl" Padding="6" Border="true" Cls="form" AutoScroll="true" Frame="false">
    <TopBar>
        <ext:Toolbar ID="ToolBarControl" runat="server">
            <Items>
                <ext:Button runat="server" Icon="Disk" ID="btnUpdate" Text="Cập nhật">
                    <DirectEvents>
                        <Click OnEvent="btnUpdate_Click">
                            <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Cấu hình" ID="btnConfig" Icon="Cog">
                    <Menu>
                        <ext:Menu ID="Menu3" runat="server">
                            <Items>
                                <ext:MenuItem runat="server" Icon="Wand" ID="wdWindow_btnConfigForm" Text="Thông tin form">
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuFormControl" runat="server" Icon="TextfieldAdd" Text="Các phần tử trên form">
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuSetLayout" runat="server" Icon="ApplicationForm" Text="Thiết lập bố cục">
                                </ext:MenuItem>
                            </Items>
                        </ext:Menu>
                    </Menu>
                </ext:Button>
            </Items>
        </ext:Toolbar>
    </TopBar>
    <Content>
        <asp:PlaceHolder runat="server" ID="plcUserControl"></asp:PlaceHolder>
    </Content>
</ext:FormPanel>
<ext:Window runat="server" Padding="0" ID="wdWindow" Hidden="true" Border="false"
    Constrain="true" Maximizable="true" AutoHeight="false" Modal="true" AutoScroll="true">
    <Buttons>
        <ext:Button runat="server" Icon="Disk" Visible="true" ID="wdBtnUpdate" Text="Cập nhật">
            <DirectEvents>
                <Click OnEvent="btnUpdate_Click">
                    <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnAddNewAndClose" Visible="true" Icon="Disk" Text="Cập nhật & đóng lại">
            <Listeners>
                <Click Handler="hdfClickUpdateStatus" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdate_Click">
                    <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                    <ExtraParams>
                        <ext:Parameter Name="CloseCommand" Value="Close" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnCloseForm" Icon="Decline" Text="Đóng">
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" Title="Chọn ảnh" Resizable="false" Icon="ImageAdd" Hidden="true"
    Padding="6" ID="wdUploadImageWindow" Width="400" Modal="true" AutoHeight="true">
    <Items>
        <ext:FormPanel runat="server" Border="false" ID="frmPanelUploadImage">
            <Items>
                <ext:Hidden runat="server" Text="" ID="hdfImageFolder" />
                <ext:Hidden runat="server" ID="hdfColumnName" />
                <ext:FileUploadField runat="server" ID="fufUploadControl" AllowBlank="false" RegexText="Định dạng tệp tin không hợp lệ"
                    Regex="(http(s?):)|([/|.|\w|\s])*\.(?:jpg|gif|png|bmp|jpeg)" AnchorHorizontal="100%" FieldLabel="Chọn ảnh">
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
                <Click OnEvent="btnAccept_Click">
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
