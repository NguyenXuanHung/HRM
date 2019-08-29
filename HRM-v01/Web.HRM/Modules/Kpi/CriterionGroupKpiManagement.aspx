<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CriterionGroupKpiManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.CriterionGroupKpiManagement" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">       
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                // reload grid
                reloadGrid();
                // show keyword trigger
                if (this.getValue() === '')
                    this.triggers[0].hide();
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        };
        var reloadGrid = function () {
            gpGroupKpi.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var showTip = function () {
            var rowIndex = gpGroupKpi.view.findRowIndex(this.triggerElement),
                cellIndex = gpGroupKpi.view.findCellIndex(this.triggerElement),
                record = storeCriterionGroup.getAt(rowIndex);
            var fieldName = gpGroupKpi.getColumnModel().getDataIndex(cellIndex);
            if (!isNaN(parseInt(rowIndex)))
                data = record.get(fieldName);
            else
                data = '';
            this.body.dom.innerHTML = data;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfGroup" />

            <!-- store -->
            <ext:Store runat="server" ID="storeCriterionGroup" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="CriterionGroup" Mode="Value" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="group" Value="hdfGroup.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="CriterionId" />
                            <ext:RecordField Name="GroupId" />
                            <ext:RecordField Name="CriterionName" />
                            <ext:RecordField Name="GroupName" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="ValueTypeName" />
                            <ext:RecordField Name="ValueType" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpGroupKpi" StoreID="storeCriterionGroup" TrackMouseOver="true" Header="True" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True" Title="Thông tin nhóm KPI" AutoExpandColumn="Description">
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
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear(); reloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                        <ext:Column ColumnID="CriterionName" Header="Tên tiêu chí" Width="350" Align="Left" DataIndex="CriterionName" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="250" Align="Left" DataIndex="Description" />
                                        <ext:Column ColumnID="ValueTypeName" Header="Kiểu dữ liệu" Width="80" Align="Center" DataIndex="ValueTypeName">
                                            <Renderer Fn="RenderValueTypeName"></Renderer>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));" />
                                            <RowDeselect Handler="hdfId.reset();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="30" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:Label runat="server" Text="Số bản ghi trên trang:" />
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="30" />
                                                <Listeners>
                                                    <Select Handler="#{pagingToolbar}.pageSize=parseInt(this.getValue());#{pagingToolbar}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="rowSelectionModel.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

            <%--tooltips grid--%>
            <ext:ToolTip
                ID="RowTip"
                runat="server"
                Target="={gpGroupKpi.getView().mainBody}"
                Delegate=".x-grid3-cell"
                TrackMouse="true" 
                AutoWidth="True">
                <Listeners>
                    <Show Fn="showTip" />
                </Listeners>
            </ext:ToolTip>
        </div>
    </form>
</body>
</html>
