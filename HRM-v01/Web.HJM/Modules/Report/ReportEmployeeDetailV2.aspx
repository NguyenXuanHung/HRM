<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Report.ReportEmployeeDetailV2" CodeBehind="ReportEmployeeDetailV2.aspx.cs" %>
<%@ Register src="UserControl/ucReportViewer.ascx" tagname="ReportViewer" tagprefix="uc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:ReportViewer ID="rpEmployeeDetailV2" runat="server" />
        </div>
    </form>
</body>
</html>
