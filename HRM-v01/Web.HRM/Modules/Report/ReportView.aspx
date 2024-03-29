﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="Web.HRM.Modules.Report.ReportView" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.XtraReports.Web" Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        // load selected department
        var loadSelectedDepartments = function () {
            var selectedDepartments = hdfSelectedDepartmentIds.getValue().split(',');
            for (var i = 0; i < selectedDepartments.length; i++) {
                var node = pnlTreeDepartments.getNodeById(selectedDepartments[i]);
                if (node) node.checked = true;
            }
        };
        // save selected department
        var saveSelectedDepartments = function () {
            var selectNodes = pnlTreeDepartments.getChecked();
            var selectedDepartmentIds = "";
            var selectedDepartmentNames = "";
            for (var i = 0; i < selectNodes.length; i++) {
                selectedDepartmentIds += selectNodes[i].id + ',';
                selectedDepartmentNames += selectNodes[i].text + ', ';
            }
            if (selectedDepartmentIds.length > 0) {
                selectedDepartmentIds = selectedDepartmentIds.substr(0, selectedDepartmentIds.length - 1);
                selectedDepartmentNames = selectedDepartmentNames.substr(0, selectedDepartmentNames.length - 2);
            }
            hdfSelectedDepartmentIds.setValue(selectedDepartmentIds);
            hdfSelectedDepartmentNames.setValue(selectedDepartmentNames);
        };
        // add filter
        var addFilter = function () {
            if (gpFilter.getSelectionModel().getCount() === 0) {
                window.Ext.Msg.alert("Thông báo", "Bạn chưa chọn điều kiện nào cả!");
                return;
            }
            var selections = gpFilter.getSelectionModel().getSelections();
            for (var i = 0, row; row = selections[i]; i++) {
                if (!checkExits(hdfConditionSelecting.getValue(), row.data.Name)) {
                    gpSelectedFilter.insertRecord(0, {
                        FilterItemName: hdfConditionSelecting.getValue(),
                        ConditionName: row.data.Name,
                        ConditionClause: row.data.Clause
                    });
                }
            }
            storeFilterSelected.commitChanges();
            hdfConditionSelected.setValue(generateSelectedFilterJSON());
        };
        // remove filter
        var removeFilter = function () {
            if (gpSelectedFilter.getSelectionModel().getCount() === 0) {
                window.Ext.Msg.alert("Thông báo", "Bạn chưa chọn điều kiện nào cả!");
                return;
            }
            var selections = gpSelectedFilter.getSelectionModel().getSelections();
            for (var i = 0, row; row = selections[i]; i++) {
                storeFilterSelected.remove(row);
            }
            hdfConditionSelected.setValue(generateSelectedFilterJSON());
        };
        // check exists
        var checkExits = function (filterItemName, conditionName) {
            for (var i = 0; i < storeFilterSelected.data.items.length; i++) {
                var row = storeFilterSelected.data.items[i];
                if (row.data.FilterItemName === filterItemName && row.data.ConditionName === conditionName) {
                    return true;
                }
            }
            return false;
        };
        // generate filter json
        var generateSelectedFilterJSON = function () {
            var jsonData = '[';
            for (var i = 0; i < storeFilterSelected.data.items.length; i++) {
                var row = storeFilterSelected.data.items[i];
                jsonData += '{"FilterItemName":"' + row.data.FilterItemName + '","ConditionName":"' + row.data.ConditionName + '","ConditionClause":"' + row.data.ConditionClause + '"},';
            }
            if (jsonData.endsWith(","))
                jsonData = jsonData.substr(0, jsonData.length - 1);
            jsonData += ']';
            return jsonData;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="RM" />
        <!-- hidden field -->
        <ext:Hidden runat="server" ID="hdfSelectedDepartmentIds" />
        <ext:Hidden runat="server" ID="hdfSelectedDepartmentNames" />
        <ext:Hidden runat="server" ID="hdfFilterItems" />
        <ext:Hidden runat="server" ID="hdfConditionSelecting" />
        <ext:Hidden runat="server" ID="hdfConditionSelected" />
        <ext:Hidden runat="server" ID="hdfSelectedEmployees"/>
        
        <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />

        <!-- store -->
        <ext:Store runat="server" ID="storeFilterName">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="Name" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Store runat="server" ID="storeFilterValue" OnRefreshData="storeFilterValue_OnRefreshData" AutoLoad="false">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="Name" />
                        <ext:RecordField Name="Clause" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Store runat="server" ID="storeFilterSelected" AutoLoad="false">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="FilterItemName" />
                        <ext:RecordField Name="ConditionName" />
                        <ext:RecordField Name="ConditionClause" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <!-- report -->
        <table id="tblReportViewer" cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="padding-left: 6px; padding-right: 6px; width: 100%">
                    <dx:ReportToolbar runat="server" ID="rpToolbarTop" ReportViewerID="reportViewer" ShowDefaultButtons="False" EnableViewState="False" Width="100%">
                        <Items>
                            <dx:ReportToolbarButton ToolTip="Cấu hình bộ lọc" ImageUrl="/Resource/icon/cog_edit.png" Name="Filter" />
                            <dx:ReportToolbarSeparator />
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
                        <ClientSideEvents ItemClick="function(s, e) { console.log(e.item); if (e.item.name === 'Filter') wdReportFilter.show();}" />
                    </dx:ReportToolbar>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
            <tr>
                <td style="height: 250px; width: 100%;">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td class="PageBorder_tlc" style="width: 10px; height: 10px;">
                                <div style="width: 10px; height: 10px; font-size: 1px"></div>
                            </td>
                            <td class="PageBorder_t"></td>
                            <td class="PageBorder_trc" style="width: 10px; height: 10px;">
                                <div style="width: 10px; height: 10px; font-size: 1px"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="PageBorder_l"></td>
                            <td style="background-color: white;">
                                <dx:ReportViewer runat="server" ID="reportViewer" Style="width: 100%; height: 100%" LoadingPanelImage-AlternateText="Tạo báo cáo...."
                                    ClientInstanceName="reportViewer" EnableViewState="False">
                                </dx:ReportViewer>
                            </td>
                            <td class="PageBorder_r"></td>
                        </tr>
                        <tr>
                            <td class="PageBorder_blc" style="width: 10px; height: 10px;"></td>
                            <td class="PageBorder_b"></td>
                            <td class="PageBorder_brc" style="width: 10px; height: 10px;"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
            <tr>
                <td style="padding-left: 6px; padding-right: 6px;">
                    <dx:ReportToolbar runat="server" ID="rpToolbarBottom" ReportViewerID="reportViewer" ShowDefaultButtons="False" EnableViewState="False" Width="100%">
                        <Items>
                            <dx:ReportToolbarButton ToolTip="Cấu hình bộ lọc" ImageUrl="/Resource/icon/cog_edit.png" Name="Filter" />
                            <dx:ReportToolbarSeparator />
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
                        <ClientSideEvents ItemClick="function(s, e) { if (e.item.name === 'Filter') wdReportFilter.show(); }" />
                    </dx:ReportToolbar>
                </td>
            </tr>
        </table>
        <!-- window report filter -->
        <ext:Window runat="server" ID="wdReportFilter" Hidden="true" Modal="true" AutoHeight="true" ConstrainHeader="true" Width="650" Icon="Printer" Padding="10" Constrain="true"
            Layout="FormLayout" Title="Điều kiện báo cáo" Resizable="false">
            <Items>
                <ext:TriggerField runat="server" AnchorHorizontal="100%" FieldLabel="Tiêu đề báo cáo" ID="txtReportTitle" Height="30">
                    <Triggers>
                        <ext:FieldTrigger Icon="Clear" Qtip="Xóa trắng" HideTrigger="false" />
                    </Triggers>
                    <Listeners>
                        <TriggerClick Handler="txtReportTitle.reset();" />
                    </Listeners>
                </ext:TriggerField>
                <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" Height="30">
                    <Items>
                        <ext:Container runat="server" ColumnWidth="0.5" Layout="FormLayout" AutoHeight="true" ID="ctnDepartment">
                            <Items>
                                <ext:TriggerField runat="server" ID="txtSelectedDepartments" CtCls="requiredData" FieldLabel="Đơn vị" AnchorHorizontal="98%" Visible="False">
                                    <Listeners>
                                        <TriggerClick Handler="wdDepartment.show();" />
                                        <Focus Handler="wdDepartment.show();" />
                                    </Listeners>
                                </ext:TriggerField>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" AutoHeight="true">
                            <Items>
                                <ext:DateField runat="server" AnchorHorizontal="100%" FieldLabel="Ngày báo cáo" CtCls="requiredData" ID="dfReportDate" Visible="False" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" Layout="FormLayout" AutoHeight="true" ID="ctnChooseEmployee">
                    <Items>
                        <ext:TriggerField runat="server" ID="txtSelectedEmployee" CtCls="requiredData" FieldLabel="Chọn cán bộ" AnchorHorizontal="100%" Visible="False">
                            <Listeners>
                                <TriggerClick Handler="ucChooseEmployee_wdChooseUser.show();" />
                                <Focus Handler="ucChooseEmployee_wdChooseUser.show();" />
                            </Listeners>
                        </ext:TriggerField>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" AutoHeight="true">
                    <Items>
                        <ext:Container runat="server" ID="ctnStartDate" Layout="FormLayout" ColumnWidth="0.5" Height="30">
                            <Items>
                                <ext:DateField ID="dfStartDate" runat="server" FieldLabel="Từ ngày" AnchorHorizontal="98%" Format="d/M/yyyy" Visible="False"/>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" ID="ctnEndDate" Layout="FormLayout" ColumnWidth="0.5" Height="30">
                            <Items>
                                <ext:DateField ID="dfEndDate" runat="server" FieldLabel="Đến ngày" AnchorHorizontal="100%" Format="d/M/yyyy" Visible="False"/>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container1" ColumnWidth="0.66" Layout="FormLayout" AutoHeight="true">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfTimeSheetReport"></ext:Hidden>
                        <ext:ComboBox runat="server" ID="cbxTimeSheetReport" FieldLabel="Chọn bảng chấm công"
                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                            LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData" Visible="False">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Store>
                                <ext:Store ID="storeSelectTimeSheetReport" AutoSave="true" runat="server">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="handlers" Value="TimeSheetReportList" />
                                    </BaseParams>
                                    <Reader>
                                        <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                            <Fields>
                                                <ext:RecordField Name="Id" />
                                                <ext:RecordField Name="Name" />
                                                <ext:RecordField Name="StartDate" />
                                                <ext:RecordField Name="EndDate" />
                                                <ext:RecordField Name="CreatedDate" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();hdfTimeSheetReport.setValue(cbxTimeSheetReport.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfTimeSheetReport.reset(); };" />
                            </Listeners>
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
                <ext:TabPanel runat="server" AnchorHorizontal="100%" Border="true" Height="250">
                    <Items>
                        <ext:Panel runat="server" ID="pnlFilerCondition" Title="Điều kiện lọc" Border="false" Layout="FormLayout" AnchorHorizontal="100%">
                            <Items>
                                <ext:Container runat="server" Height="220" AnchorHorizontal="100%" Layout="ColumnLayout">
                                    <Items>
                                        <ext:Container runat="server" Layout="BorderLayout" ColumnWidth="0.49" Height="220">
                                            <Items>
                                                <ext:GridPanel runat="server" ID="gpFilter" StoreID="storeFilterValue" Border="false" AnchorHorizontal="100%"
                                                    StripeRows="true" AutoExpandColumn="Name" Title="" Region="Center" TrackMouseOver="false">
                                                    <TopBar>
                                                        <ext:Toolbar ID="toolbarSelectCondition" runat="server">
                                                            <Items>
                                                                <ext:ComboBox runat="server" ID="cboFilterName" StoreID="storeFilterName" ValueField="Name"
                                                                    DisplayField="Name" LabelWidth="70" EmptyText="Chọn điều kiện" Editable="false">
                                                                    <Listeners>
                                                                        <Select Handler="hdfConditionSelecting.setValue(cboFilterName.getValue());storeFilterValue.reload();"></Select>
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:ToolbarFill runat="server" />
                                                                <ext:Button runat="server" ID="btnAddFilter" Text="Chọn" Icon="ArrowRight">
                                                                    <Listeners>
                                                                        <Click Handler="addFilter();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:Column ColumnID="Name" Header="Giá trị" DataIndex="Name" />
                                                            <ext:Column ColumnID="Clause" DataIndex="Clause" Hidden="True" Width="0" />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:CheckboxSelectionModel ID="chkSelectionModelFilter" runat="server" />
                                                    </SelectionModel>
                                                    <SaveMask ShowMask="True" Msg="Đang tải..."></SaveMask>
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ColumnWidth="0.02"></ext:Container>
                                        <ext:Container runat="server" Layout="BorderLayout" ColumnWidth="0.49" Height="220">
                                            <Items>
                                                <ext:GridPanel runat="server" ID="gpSelectedFilter" StoreID="storeFilterSelected" Border="false" AnchorHorizontal="100%" StripeRows="true" AutoExpandColumn="FilterItemName"
                                                    Title="Điều kiện đã chọn" Header="false" Icon="Tick" Region="Center" TrackMouseOver="false">
                                                    <TopBar>
                                                        <ext:Toolbar runat="server">
                                                            <Items>
                                                                <ext:DisplayField runat="server" ID="dflTitle" Text="<h1 style='color:#15428B'> Điều kiện đã chọn</h1>" />
                                                                <ext:ToolbarFill runat="server" />
                                                                <ext:Button ID="btnDeleteFilter" runat="server" Text="Xóa" Icon="Delete">
                                                                    <Listeners>
                                                                        <Click Handler="removeFilter();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:Column ColumnID="FilterItemName" Header="Điều kiện lọc" DataIndex="FilterItemName"></ext:Column>
                                                            <ext:Column ColumnID="ConditionName" Header="Giá trị" DataIndex="ConditionName"></ext:Column>
                                                            <ext:Column ColumnID="ConditionClause" DataIndex="ConditionClause" Hidden="True" Width="0"></ext:Column>
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:CheckboxSelectionModel ID="chkSelectionModelSelectedFilter" runat="server" />
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Title="Ghi chú" Border="false" Layout="FitLayout" Hidden="true" AnchorHorizontal="100%">
                            <Items>
                                <ext:HtmlEditor runat="server" ID="htmlNote">
                                </ext:HtmlEditor>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:TabPanel>
                <ext:FieldSet runat="server" ID="fsLaber" Title="Nhãn">
                    <Items>
                        <ext:Container runat="server" ID="columnlayout" Layout="FormLayout" AnchorHorizontal="100%">
                            <Items>
                                <ext:Container runat="server" Layout="ColumnLayout" LabelAlign="Top" Height="25">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.32">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtCreatedByTitle" FieldLabel="Người lập" LabelWidth="60" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ColumnWidth="0.02"></ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.32">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtReviewedByTitle" FieldLabel="Người duyệt" LabelWidth="60" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ColumnWidth="0.02"></ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.32">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtSignedByTitle" FieldLabel="Người ký" LabelWidth="60" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="ColumnLayout" LabelAlign="Top" Height="25">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.32">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtCreatedByName" FieldLabel="Tên người lập" LabelWidth="60" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ColumnWidth="0.02"></ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.32">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtReviewedByName" FieldLabel="Tên người duyệt" LabelWidth="60" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ColumnWidth="0.02"></ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.32">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtSignedByName" FieldLabel="Tên người ký" LabelWidth="60" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:FieldSet>
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Đồng ý" Icon="Accept" ID="btnShowReport">
                    <Listeners>
                        <Click Handler="wdReportFilter.hide();reportViewer.Refresh();"></Click>
                    </Listeners>
                </ext:Button>
                <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="btnClose">
                    <Listeners>
                        <Click Handler="#{wdReportFilter}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <!-- window select department -->
        <ext:Window runat="server" ID="wdDepartment" Hidden="true" AutoHeight="true" Layout="FormLayout" Width="300" Padding="0" Resizable="false" Icon="Key" Modal="true" Title="Chọn đơn vị">
            <Items>
                <ext:TreePanel ID="pnlTreeDepartments" runat="server" Width="300" Height="250" Border="false" RootVisible="false" AutoScroll="true">
                    <Listeners>
                        <Render Handler="loadSelectedDepartments();"></Render>
                        <CheckChange Handler="saveSelectedDepartments();" />
                    </Listeners>
                </ext:TreePanel>
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="Lưu" ID="btnSaveSelectdDepartments" Icon="Disk">
                    <Listeners>
                        <Click Handler="saveSelectedDepartments();wdDepartment.hide();" />
                    </Listeners>
                </ext:Button>
                <ext:Button runat="server" Text="Hủy" Icon="Decline">
                    <Listeners>
                        <Click Handler="wdDepartment.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Hide Handler="txtSelectedDepartments.setValue(hdfSelectedDepartmentNames.getValue())"></Hide>
            </Listeners>
        </ext:Window>
    </form>
</body>
</html>
