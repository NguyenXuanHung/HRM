<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.System.AccessHistoryManagement" Codebehind="AccessHistoryManagement.aspx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Base/GridPanel/GridPanel.ascx" TagName="GridPanel" TagPrefix="uc1" %>
<%@ Register src="../Base/DateTime/DateTime.ascx" tagname="DateTime" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Resource/js/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="/Resource/js/GridPanelEvent.js" type="text/javascript"></script>
    <script type="text/javascript">
        RenderErrorRow = function (value, p, record) {
            if ((value + "") == "true") {
                return "<img src='../../Resource/images/uncheck.gif' alt='' /> <b style='color:red;'>Lỗi</b>";
            }
            return "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <ext:Window ID="wdChooseTime" runat="server" Hidden="true" Icon="Clock" Padding="6" Title="Chọn thời điểm" Modal="true" Width="400" AutoHeight="true" Disabled="true">
            <Content>
                <uc2:DateTime ID="DateTime1" FieldLabel="Từ ngày" Width="350" runat="server" />
                <uc2:DateTime ID="DateTime2" FieldLabel="Đến ngày" Width="350" runat="server" />
            </Content>
            <Buttons>
                <ext:Button runat="server" ID="btnOK" Text="Đồng ý" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="btnOK_Click">
                            <EventMask ShowMask="true" Msg="Đợi trong giây lát" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdChooseTime}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window> 
        <ext:Button runat="server" Text="Xóa hết lịch sử truy cập" ID="btnTruncateHistory" Icon="Delete">
            <DirectEvents>
                <Click OnEvent="btnTruncateHistory_Click">
                    <Confirmation Title="Cảnh báo" ConfirmRequest="true" Message="Tất cả lịch sử truy cập sẽ bị xóa và không thể lấy lại được ! Bạn có chắc chắn muốn thực hiện hành động này không ? " />
                    <EventMask ShowMask="true" Msg="Đang xóa dữ liệu, chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <uc1:GridPanel ID="grp_nhatkytrucap" RowExpanderTemplateUrl="../System/RowExpanderTemplate.htm"  DisableEditWindow="true" runat="server" />
    </div>
    </form>
</body>
</html>
