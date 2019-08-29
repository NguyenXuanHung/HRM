<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.ucReportViewer" Codebehind="ucReportViewer.ascx.cs" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
        </td>
        <td style="padding-left: 6px;  padding-right: 6px; width:100%">
            <dx:ReportToolbar ID="ReportToolbar1" runat="server"
                ShowDefaultButtons="False" EnableViewState="False" Width="100%">
                <Items>
                    <dx:ReportToolbarButton ItemKind="Search" ToolTip="Tìm kiếm" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="PrintReport" ToolTip="In báo cáo" />
                    <dx:ReportToolbarButton ItemKind="PrintPage" ToolTip="In trang hiện tại" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="Trang đầu tiên" />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="Trang trước" />
                    <dx:ReportToolbarLabel ItemKind="PageLabel" Text="Trang" />
                    <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                    </dx:ReportToolbarComboBox>
                    <dx:ReportToolbarLabel ItemKind="OfLabel" Text="của" />
                    <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                    <dx:ReportToolbarButton ItemKind="NextPage" ToolTip="Trang tiếp" />
                    <dx:ReportToolbarButton ItemKind="LastPage" ToolTip="Trang cuối" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Tải báo cáo về máy tính" />
                    <dx:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Xuất bảo cáo ra một window mới" />
                    <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                        <Elements>
                            <dx:ListElement Value="pdf" />
                            <dx:ListElement Value="xls" />
                            <dx:ListElement Value="xlsx" />
                            <dx:ListElement Value="rtf" />
                            <dx:ListElement Value="mht" />
                            <dx:ListElement Value="html" />
                            <dx:ListElement Value="txt" />
                            <dx:ListElement Value="csv" />
                            <dx:ListElement Value="png" />
                        </Elements>
                    </dx:ReportToolbarComboBox>
                </Items>
            </dx:ReportToolbar>
        </td>
    </tr>
    <tr>
        <td style="height: 8px" colspan="2">
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <%--<td style="height: 241px; width: 417px;">--%>
        <td style="height: 250px; width: 100%;">
            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
                    <td class="PageBorder_tlc" style="width: 10px; height: 10px;">
                        <div style="width: 10px; height: 10px; font-size: 1px">
                        </div>
                    </td>
                    <td class="PageBorder_t">
                    </td>
                    <td class="PageBorder_trc" style="width: 10px; height: 10px;">
                        <div style="width: 10px; height: 10px; font-size: 1px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="PageBorder_l">
                    </td>
                    <td style="background-color: white;">
                        <dx:ReportViewer ID="ReportViewer1" Style="width: 100%; height: 100%" runat="server"
                            LoadingPanelImage-AlternateText="Tạo report...." ClientInstanceName="ReportViewer1"
                            EnableViewState="False">
                        </dx:ReportViewer>
                    </td>
                    <td class="PageBorder_r">
                    </td>
                </tr>
                <tr>
                    <td class="PageBorder_blc" style="width: 10px; height: 10px;">
                    </td>
                    <td class="PageBorder_b">
                    </td>
                    <td class="PageBorder_brc" style="width: 10px; height: 10px;">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 8px" colspan="2">
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td valign="top" style="padding-left: 6px; padding-right: 6px;">
            <dx:ReportToolbar ID="ReportToolbar2" runat="server"
                ShowDefaultButtons="False" EnableViewState="False"  Width="100%">
                <Items>
                    <dx:ReportToolbarButton ItemKind="Search" ToolTip="Tìm kiếm" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="PrintReport" ToolTip="In báo cáo" />
                    <dx:ReportToolbarButton ItemKind="PrintPage" ToolTip="In trang hiện tại" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="Trang đầu tiên" />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="Trang trước" />
                    <dx:ReportToolbarLabel ItemKind="PageLabel" Text="Trang" />
                    <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                    </dx:ReportToolbarComboBox>
                    <dx:ReportToolbarLabel ItemKind="OfLabel" Text="của" />
                    <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                    <dx:ReportToolbarButton ItemKind="NextPage" ToolTip="Trang tiếp" />
                    <dx:ReportToolbarButton ItemKind="LastPage" ToolTip="Trang cuối" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Tải báo cáo về máy tính" />
                    <dx:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Xuất bảo cáo ra một window mới" />
                    <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                        <Elements>
                            <dx:ListElement Value="pdf" />
                            <dx:ListElement Value="xls" />
                            <dx:ListElement Value="xlsx" />
                            <dx:ListElement Value="rtf" />
                            <dx:ListElement Value="mht" />
                            <dx:ListElement Value="html" />
                            <dx:ListElement Value="txt" />
                            <dx:ListElement Value="csv" />
                            <dx:ListElement Value="png" />
                        </Elements>
                    </dx:ReportToolbarComboBox>
                </Items>
            </dx:ReportToolbar>
        </td>
    </tr>
</table>