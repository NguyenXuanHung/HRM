<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Web.HJM.Modules.Home.Chart.QualificationsChart" Codebehind="PieChart.aspx.cs" %>

<%@ Register Assembly="Highcharts" Namespace="Highcharts.UI" TagPrefix="cc1" %>
     
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/highcharts.js"></script>
    <script type="text/javascript" src="js/modules/exporting.js"></script>
    <link href="../css/Chart.css" rel="stylesheet" type="text/css" />
    <%--<style type="text/css">
        tspan
        {
            font-size: 11px !important;
            font-family: Tahoma;
        }
        b
        {
            font-size: 12px !important;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server"> 
        <cc1:PieChart ID="hcVendas"  Width="100%" Height="300"  runat="server" />
    </form>
</body>
</html>
