<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Report.PrinterRecord" Codebehind="PrinterRecord.aspx.cs" %>
<%@ Register src="UserControl/ucReportViewer.ascx" tagname="ReportViewer" tagprefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:ReportViewer ID="reportEmployeeSelected" runat="server" />
        </div>
    </form>
</body>
</html>
