<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.HTMLReport.Printed" Codebehind="Printed.aspx.cs" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="~/Resource/css/CustomStyle.css"/>
</head>
<body onload="window.print();"> 
    <form id="form1" runat="server">
        <div id="non-printable">
            Ấn tổ hợp phím Ctrl + P để in
        </div>
        <div id="printable" runat="server">
        </div>
    </form>
</body>
</html>
