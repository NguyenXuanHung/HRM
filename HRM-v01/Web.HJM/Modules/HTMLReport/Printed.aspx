<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.HTMLReport.Printed" Codebehind="Printed.aspx.cs" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #non-printable
        {
            font-size: 13px;
            font-weight: bold;
            text-align: center;
            color: red;
        }

        @media print
        {
            #non-printable
            {
                display: none;
            }

            #printable
            {
                display: block;
            }
        }
    </style> 
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
