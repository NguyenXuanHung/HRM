<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Report.BaoCao_Main" Codebehind="BaoCao_Main.aspx.cs" %>

<%@ Register Src="/Modules/Report/UserControl/ucReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<%@ Register TagPrefix="ext" Namespace="Ext.Net" Assembly="Ext.Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        div#Panel1 {
            border-top: 1px solid #99bbe8 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM" />
            <div style="height: 10px;"></div>
            <uc1:ReportViewer ID="ReportViewer1" runat="server" />
        </div>
    </form>
</body>
</html>
