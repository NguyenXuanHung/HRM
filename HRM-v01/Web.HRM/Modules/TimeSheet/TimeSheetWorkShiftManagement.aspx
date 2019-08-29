<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetWorkShiftManagement" CodeBehind="TimeSheetWorkShiftManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">
        
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfTimeSheetGroupListId" />
            <ext:Hidden runat="server" ID="hdfGroupWorkShiftName"/>

            <ext:Store runat="server" ID="storeGroupWorkShift" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GroupWorkShift" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTime" TrackMouseOver="true" Header="True" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%" Title="" Icon="Date">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnBack" runat="server" Text="Quay lại" Icon="ArrowLeft">
                                                <DirectEvents>
                                                    <Click OnEvent="btnBack_Click">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                          
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />

                                            <ext:ComboBox runat="server" ID="cboGroupWorkShift"
                                                DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupWorkShift"
                                                Width="200" ItemSelector="div.list-item" PageSize="20" EmptyText="Nhóm phân ca">
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
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();hdfTimeSheetGroupListId.setValue(cboGroupWorkShift.getValue());#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfTimeSheetGroupListId.reset(); };" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer3" Width="10" runat="server" />
                                            <ext:DateField ID="dfStartDate" runat="server" Vtype="daterange" EmptyText="Từ ngày"
                                                Width="140" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); dfEndDate.setMinValue();this.triggers[0].hide(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();}" />
                                                </Listeners>
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfEndDate}" Mode="Value" />
                                                </CustomConfig>
                                                <ToolTips>
                                                    <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Lọc theo ngày bắt đầu">
                                                    </ext:ToolTip>
                                                </ToolTips>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer2" Width="10" runat="server" />
                                            <ext:DateField ID="dfEndDate" runat="server" Vtype="daterange" EmptyText="Đến ngày"
                                                Width="140" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                                <ToolTips>
                                                    <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Lọc theo ngày kết thúc">
                                                    </ext:ToolTip>
                                                </ToolTips>
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfStartDate}" Mode="Value" />
                                                </CustomConfig>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset();dfStartDate.setMaxValue(); this.triggers[0].hide(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();}" />
                                                </Listeners>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="10" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="240" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Icon="Zoom" Text="Tìm kiếm">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={30}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="WorkShift" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="fromDate" Value="dfStartDate.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="toDate" Value="dfEndDate.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="groupWorkShift" Value="hdfTimeSheetGroupListId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="StartInTime" />
                                                    <ext:RecordField Name="EndInTime" />
                                                    <ext:RecordField Name="StartOutTime" />
                                                    <ext:RecordField Name="EndOutTime" />
                                                    <ext:RecordField Name="StartDate" />
                                                    <ext:RecordField Name="EndDate" />
                                                    <ext:RecordField Name="GroupWorkShiftId" />
                                                    <ext:RecordField Name="GroupWorkShiftName" />
                                                    <ext:RecordField Name="WorkConvert" />
                                                    <ext:RecordField Name="TimeConvert" />
                                                    <ext:RecordField Name="SymbolCode" />
                                                    <ext:RecordField Name="SymbolName" />
                                                    <ext:RecordField Name="SymbolColor" />
                                                    <ext:RecordField Name="HasOverTime" />
                                                    <ext:RecordField Name="FactorOverTime" />
                                                    <ext:RecordField Name="HasInOutTime" />
                                                    <ext:RecordField Name="SymbolId" />
                                                    <ext:RecordField Name="HasLimitTime" />
                                                    <ext:RecordField Name="GroupSymbolName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên ca" Width="200" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:DateColumn ColumnID="StartDate" Header="Bắt đầu làm việc" Width="100" DataIndex="StartDate" Format="HH:mm" />
                                        <ext:DateColumn ColumnID="StartInTime" Header="Bắt đầu vào" Width="80" DataIndex="StartInTime" Format="HH:mm" />
                                        <ext:DateColumn ColumnID="EndInTime" Header="Kết thúc vào" Width="80" DataIndex="EndInTime" Format="HH:mm" />
                                        <ext:DateColumn ColumnID="StartDate" Header="Ngày bắt đầu chấm công" Width="150" DataIndex="StartDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="EndDate" Header="Kết thúc làm việc" Width="100" DataIndex="EndDate" Format="HH:mm" />
                                        <ext:DateColumn ColumnID="StartOutTime" Header="Bắt đầu ra" Width="80" DataIndex="StartOutTime" Format="HH:mm" />
                                        <ext:DateColumn ColumnID="EndOutTime" Header="Kết thúc ra" Width="80" DataIndex="EndOutTime" Format="HH:mm" />
                                        <ext:DateColumn ColumnID="EndOutTime" Header="Ngày kết thúc chấm công" Width="150" DataIndex="EndOutTime" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="WorkConvert" Header="Công quy đổi" Width="100" Align="Left" Locked="true" DataIndex="WorkConvert" />
                                        <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="120" Align="Left" Locked="true" DataIndex="TimeConvert" />
                                        <ext:Column ColumnID="SymbolCode" Header="Ký hiệu" Width="80" Align="Center" Locked="true" DataIndex="SymbolCode">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SymbolName" Header="Tên ký hiệu" Width="100" Align="Left" DataIndex="SymbolName" />
                                        <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfKeyRecord.reset(); " />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="30">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="30" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
