<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportColumnManagement.aspx.cs" Inherits="Web.HRM.Modules.Report.ReportColumnManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <UC:Resource runat="server" ID="resource" />
    <link type="text/css" href="/Resource/css/IconStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                reloadGrid();
            }
        };
        var handlerRowSelect = function () {
            var selections = chkSelectionModel.getSelections();
            if (selections.length > 1) {
                // clear hidden field id
                hdfId.reset();
                // disable single edit / delete button
                btnEdit.disable();
                btnDelete.disable();
                btnDuplicateData.disable();
                // set hidden field ids
                var ids = selections[0].id;
                for (var i = 1; i < selections.length; i++) {
                    ids = ids + "," + selections[i].id;
                }
                hdfIds.setValue(ids);
            } else {
                hdfId.setValue(selections[0].id);
                hdfIds.setValue(selections[0].id);
            }

            btnEdit.enable();
            btnDelete.enable();
            btnDuplicateData.enable();

            if (gpReportColumn.getSelectionModel().getCount() > 1) {
                btnEdit.disable();
                btnDelete.disable();
                btnMultipleEdit.enable();
                btnDuplicateData.disable();
            }
        }
        var handlerRowDeselect = function () {
            if (gpReportColumn.getSelectionModel().getCount() === 0) {
                btnEdit.disable();
                btnDelete.disable();
                btnMultipleEdit.disable();
                btnDuplicateData.disable();
            }
            if (gpReportColumn.getSelectionModel().getCount() === 1) {
                btnEdit.enable();
                btnDelete.enable();
                btnDuplicateData.enable();
                btnMultipleEdit.disable();
            }
        }
        var reloadGrid = function () {
            if (txtKeyword.getValue() == '') {
                txtKeyword.triggers[0].hide();
            }
            else {
                txtKeyword.triggers[0].show();
            }
            gpReportColumn.getSelectionModel().clearSelections();
            gpReportColumn.reload();
        }
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        };

        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('lock');
            }
            return '';
        }
        var CountRowLabel = function (p, record) {
            var rowCount = 0;
            var labelCount = 0;
            if (record.data.GroupName === 'Cột')
                rowCount++;
            if (record.data.GroupName === 'Nhãn')
                labelCount++;
            return `(${rowCount} Cột và ${labelCount} Nhãn)`;
        }
        var showTip = function () {
            var rowIndex = gpReportColumn.view.findRowIndex(this.triggerElement),
                cellIndex = gpReportColumn.view.findCellIndex(this.triggerElement),
                record = storeReportColumn.getAt(rowIndex);
            var fieldName = gpReportColumn.getColumnModel().getDataIndex(cellIndex);
            if (!isNaN(parseInt(rowIndex)))
                data = record.get(fieldName);
            else
                data = '';
            this.body.dom.innerHTML = data;
        };
        var iconImg = function(name) {
            return "<img src='/Resource/icon/" + name + ".png'>";
        }
        var RenderTextAlign = function (value, p, record) {
            var icon = '';
            switch (record.data.TextAlign) {
                case 'MiddleCenter':
                    icon += iconImg('shape_align_middle') + iconImg('text_align_center');
                    break;
                case 'TopCenter':
                    icon += iconImg('shape_align_top') + iconImg('text_align_center');
                    break;
                case 'BottomCenter':
                    icon += iconImg('shape_align_bottom') + iconImg('text_align_center');
                    break;
                case 'MiddleLeft':
                    icon += iconImg('shape_align_middle') + iconImg('text_align_left');
                    break;
                case 'TopLeft':
                    icon += iconImg('shape_align_top') + iconImg('text_align_left');
                    break;
                case 'BottomLeft':
                    icon += iconImg('shape_align_bottom') + iconImg('text_align_left');
                    break;
                case 'MiddleRight':
                    icon += iconImg('shape_align_middle') + iconImg('text_align_right');
                    break;
                case 'TopRight':
                    icon += iconImg('shape_align_top') + iconImg('text_align_right');
                    break;
                case 'BottomRight':
                    icon += iconImg('shape_align_bottom') + iconImg('text_align_right');
                    break;
                case 'TopJustify':
                    icon += iconImg('shape_align_top') + iconImg('text_align_justify');
                    break;
                case 'MiddleJustify':
                    icon += iconImg('shape_align_middle') + iconImg('text_align_justify');
                    break;
                case 'BottomJustify':
                    icon += iconImg('shape_align_bottom') + iconImg('text_align_justify');
                    break;
            }
            return icon;
        }
        var RenderGroup = function(value, p, record) {
            return record.data.IsGroup ? iconImg('table_sort') : iconImg('table_column');
        }
        var RenderDataType = function (value, p, record) {
            var style = 'font-style:italic;' +
                        'color:green;' +
                        'font-weight:bold;' +
                        'letter-spacing:-1px;';
            return record.data.DataType === 'String' ? iconImg('text_ab') : "<span style='"+ style +"'>123</span>";
        }
        var RenderSummaryFunction = function(value, p, record) {
            var icon = '';
            switch (record.data.SummaryFunction) {
                case 'Sum':
                    icon += iconImg('sum');
                    break;
                case 'Count':
                    icon += iconImg('count');
                    break;
                case 'CustomCount':
                    icon += iconImg('cog');
                    break;
            }
            return icon;
        }
        var RenderFormat = function (value, p, record) {
            var style = 'font-weight:bold;' +
                        'color:limegreen;';
            return "<span style='" + style + "'>" + value + "</span>";
        }
        var RenderConfigName = function (value, p, record) {
            var i = -1;
            i = value.lastIndexOf('+---');
            var levelColor = '';
            switch (record.data.Level) {
                case 0:
                    levelColor = 'darkblue';
                    break;
                case 1:
                    levelColor = 'darkred';
                    break;
                case 2:
                    levelColor = 'darkcyan';
                    break;
                case 3:
                    levelColor = 'darkgreen';
                    break;
            }
            return "<span style='font-weight:bold;color:" + levelColor + "'>" + value + "</span>";
        }
        var RenderSummaryRunning = function(value, p, record) {
            if (record.data.SummaryRunning === 'None')
                value = '';
            return value;
        }
        var RenderTotalWidth = function (value, p, record) {
            var reportWidth = hdfReportWidth.getValue();
            var style = 'color:blue';
            if (value > reportWidth)
                style = 'color:red';
            return "<span style='" + style + "'>" + value + '</span>' + '/' + "<span style='color:blue'>" + reportWidth +"</span>";
        }
        var RenderWidthRemain = function(value, p, record) {
            return record.data.Width - value;
        }
        var RenderParentName = function(value, p, record) {
            var levelColor = '';
            switch (record.data.Level - 1) {
            case 0:
                levelColor = 'darkblue';
                break;
            case 1:
                levelColor = 'darkred';
                break;
            case 2:
                levelColor = 'darkcyan';
                break;
            case 3:
                levelColor = 'darkgreen';
                break;
            }
            return "<span style='font-weight:bold;color:" + levelColor + "'>" + value + "</span>";
        }
        var RenderOrder = function(value, p, record) {
            var levelColor = '';
            switch (record.data.Level) {
            case 0:
                levelColor = 'darkblue';
                break;
            case 1:
                levelColor = 'darkred';
                break;
            case 2:
                levelColor = 'darkcyan';
                break;
            case 3:
                levelColor = 'darkgreen';
                break;
            }
            return "<span style='font-weight:bold;color:" + levelColor + "'>" + value + "</span>";
        }
        var TotalWidthFunction = function (v, record) {
            var value = v + (record.data.ParentId === 0 ? record.data.Width : 0);
            return value;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- resource manager -->
            <ext:ResourceManager ID="RM" runat="server" />
            <!-- hidden field -->
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfIds" />
            <ext:Hidden runat="server" ID="hdfReportId" />
            <ext:Hidden runat="server" ID="hdfFieldName" />
            <ext:Hidden runat="server" ID="hdfTextAlign" />
            <ext:Hidden runat="server" ID="hdfTextAlign2" />
            <ext:Hidden runat="server" ID="hdfParentId" />
            <ext:Hidden runat="server" ID="hdfIsGroup" />
            <ext:Hidden runat="server" ID="hdfIsGroup2" />
            <ext:Hidden runat="server" ID="hdfDataType" />
            <ext:Hidden runat="server" ID="hdfDataType2" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfType" />
            <ext:Hidden runat="server" ID="hdfSummaryRunning" />
            <ext:Hidden runat="server" ID="hdfSummaryRunning2" />
            <ext:Hidden runat="server" ID="hdfSummaryFunction" />
            <ext:Hidden runat="server" ID="hdfSummaryFunction2" />
            <ext:Hidden runat="server" ID="hdfOrder" />
            <ext:Hidden runat="server" ID="hdfHeaderWidth" />
            <ext:Hidden runat="server" ID="hdfFooterWidth" />
            <ext:Hidden runat="server" ID="hdfReportWidth" />
            <!-- store -->
            <ext:Store ID="storeReportColumn" AutoLoad="True" runat="server" GroupField="OrderedTypeName">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Report/HandlerReportColumn.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="reportId" Value="hdfReportId.getValue()" Mode="Raw" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="FieldName" />
                            <ext:RecordField Name="ConfigName" />
                            <ext:RecordField Name="ParentId" />
                            <ext:RecordField Name="ParentName" />
                            <ext:RecordField Name="DataType" />
                            <ext:RecordField Name="DataTypeName" />
                            <ext:RecordField Name="TextAlign" />
                            <ext:RecordField Name="TextAlignName" />
                            <ext:RecordField Name="FontSize" />
                            <ext:RecordField Name="Format" />
                            <ext:RecordField Name="Width" />
                            <ext:RecordField Name="Height" />
                            <ext:RecordField Name="Order" />
                            <ext:RecordField Name="IsGroup" />
                            <ext:RecordField Name="GroupName" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                            <ext:RecordField Name="Type" />
                            <ext:RecordField Name="TypeName" />
                            <ext:RecordField Name="SummaryRunning" />
                            <ext:RecordField Name="SummaryRunningName" />
                            <ext:RecordField Name="SummaryFunction" />
                            <ext:RecordField Name="SummaryFunctionName" />
                            <ext:RecordField Name="SummaryValue" />
                            <ext:RecordField Name="WidthRemain" />
                            <ext:RecordField Name="OrderedTypeName" />
                            <ext:RecordField Name="Level" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportParentColumn" AutoLoad="False" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Report/HandlerReportColumn.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="reportId" Value="hdfReportId.getValue()" Mode="Raw" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="WidthRemain" />
                            <ext:RecordField Name="TypeName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeFieldName" runat="server" AutoLoad="False" OnRefreshData="storeFieldName_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportTextAlign" runat="server" AutoLoad="False" OnRefreshData="storeReportTextAlign_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportColumnStatus" runat="server" AutoLoad="False" OnRefreshData="storeReportColumnStatus_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportColumnType" runat="server" AutoLoad="False" OnRefreshData="storeReportColumnType_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportColumnDataType" runat="server" AutoLoad="False" OnRefreshData="storeReportColumnDataType_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportSummaryRunning" runat="server" AutoLoad="False" OnRefreshData="storeReportSummaryRunning_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportSummaryFunction" runat="server" AutoLoad="False" OnRefreshData="storeReportSummaryFunction_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport runat="server" ID="viewport" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpReportColumn" StoreID="storeReportColumn" TrackMouseOver="true" Header="false" StripeRows="true" AutoScroll="True"
                                Border="false" Layout="Fit" AutoExpandColumn="ConfigName" Collapsible="true" AnimCollapse="true" >
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnBack" Text="Quay lại" Icon="ArrowLeft">
                                                <DirectEvents>
                                                    <Click OnEvent="RedirectBack">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server" />
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="chkSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="chkSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button ID="btnMultipleEdit" runat="server" Text="Sửa nhiều" Icon="Pencil" Disabled="True">
                                                <DirectEvents>
                                                    <Click OnEvent="InitMultipleEditWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server"/>
                                            <ext:Button runat="server" Text="Nhân đôi cột" Icon="DiskMultiple" ID="btnDuplicateData" Disabled="True">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowDuplicateData">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="chkSelectionModel.getSelected().get('Id')" Mode="Raw"/>
                                                        </ExtraParams>
                                                        <EventMask ShowMask="True" Msg="Đang xử lý..."></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" Text="Mẫu báo cáo" Icon="PageGo" ID="btnPreview">
                                                <Listeners>
                                                    <Click Handler="#{WindowPreview}.show(this);#{WindowPreview}.reload();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập tên cột hoặc trường dữ liệu">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();reloadGrid();" />
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
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false" GroupTextTpl='{text} ({[values.rs.filter(v => v.data.IsGroup).length]} Nhãn, {[values.rs.filter(v => !v.data.IsGroup).length]} Cột)'
                                        ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="TT" Width="30" Align="Right" />
                                        <ext:Column ColumnID="ConfigName" Width="150" Header="Tên cột" DataIndex="ConfigName">
                                            <Renderer Fn="RenderConfigName"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="FieldName" Header="Trường dữ liệu" Width="150" Align="Left" DataIndex="FieldName" />
                                        <ext:Column ColumnID="ParentName" Header="Cột cha" Width="150" Align="Left" DataIndex="ParentName">
                                            <Renderer Fn="RenderParentName"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="TextAlign" Header="Căn lề" Width="60" Align="Center" DataIndex="TextAlignName">
                                            <Renderer Fn="RenderTextAlign"></Renderer>
                                        </ext:Column>
                                        <ext:GroupingSummaryColumn Width="80" ColumnID="Width" Header="Độ Rộng" Sortable="true" Align="Right"
                                            DataIndex="Width" CustomSummaryType="totalWidth">
                                            <SummaryRenderer Fn="RenderTotalWidth"></SummaryRenderer>
                                        </ext:GroupingSummaryColumn>
                                        <ext:Column ColumnID="WidthRemain" Header="Độ rộng cột con" Width="100" Align="Center" DataIndex="WidthRemain">
                                            <Renderer Fn="RenderWidthRemain"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="FontSize" Header="Cỡ chữ" Width="60" Align="Right" DataIndex="FontSize" />
                                        <ext:Column ColumnID="Height" Header="Cao" Width="50" Align="Right" DataIndex="Height" />
                                        <ext:Column ColumnID="DataTypeName" Header="Kiểu dữ liệu" Width="80" Align="Center" DataIndex="DataTypeName">
                                            <Renderer Fn="RenderDataType"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="Format" Header="Định dạng" Width="80" Align="Left" DataIndex="Format">
                                            <Renderer Fn="RenderFormat"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="60" Align="Right" DataIndex="Order">
                                            <Renderer Fn="RenderOrder"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="GroupName" Header="Thuộc loại" Width="70" Align="Center" DataIndex="GroupName">
                                            <Renderer Fn="RenderGroup"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="StatusName" Header="Trạng thái" Width="80" Align="Center" DataIndex="StatusName">
                                            <Renderer Fn="RenderRowStatus"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="SummaryRunningName" Header="Phạm vi tính" Width="150" Align="Left" DataIndex="SummaryRunningName">
                                            <Renderer Fn="RenderSummaryRunning"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="SummaryFunctionName" Header="Hàm" Width="60" Align="Center" DataIndex="SummaryFunctionName">
                                            <Renderer Fn="RenderSummaryFunction"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="SummaryValue" Header="Tham số" Width="70" Align="Center" DataIndex="SummaryValue" />
                                        <ext:Column ColumnID="OrderedTypeName" Header="Thứ tự vị trí" Width="70" Align="Center" DataIndex="OrderedTypeName" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkSelectionModel">
                                        <Listeners>
                                            <RowSelect Handler="handlerRowSelect();"></RowSelect>
                                            <RowDeselect Handler="handlerRowDeselect();"></RowDeselect>
                                        </Listeners>
                                    </ext:CheckboxSelectionModel>
                                </SelectionModel>
                                <Plugins>
                                    <ext:GroupingSummary runat="server">
                                        <Calculations>
                                            <ext:JFunction Name="totalWidth" Fn="TotalWidthFunction"/>
                                        </Calculations>
                                    </ext:GroupingSummary>
                                </Plugins>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

            <%--tooltips grid--%>
            <ext:ToolTip
                ID="RowTip"
                runat="server"
                Target="={gpReportColumn.getView().mainBody}"
                Delegate=".x-grid3-cell"
                TrackMouse="true">
                <Listeners>
                    <Show Fn="showTip" />
                </Listeners>
            </ext:ToolTip>

            <!-- window -->
            <ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="700" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
                <Items>
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên <span style='color:red;'> * </span>" AnchorHorizontal="100%" Visible="True" />
                    <ext:ComboBox runat="server" ID="cboFieldName" StoreID="storeFieldName" FieldLabel="Trường dữ liệu" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboFieldName}.store.getCount()==0){#{storeFieldName}.reload();}" />
                            <Select Handler="#{hdfFieldName}.setValue(this.getValue());"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboTextAlign" StoreID="storeReportTextAlign" FieldLabel="Canh lề" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboTextAlign}.store.getCount()==0){#{storeReportTextAlign}.reload();}" />
                            <Select Handler="hdfTextAlign.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtFormat" FieldLabel="Định dạng" AnchorHorizontal="100%" />
                    <ext:NumberField runat="server" ID="txtFontSize" FieldLabel="Cỡ chữ" Text="6" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:ComboBox runat="server" ID="cboParent" StoreID="storeReportParentColumn" FieldLabel="Cột cha" DisplayField="Name" 
                                  ValueField="Id" Width="560" ItemSelector="div.list-item">
                        <Listeners>
                            <Expand Handler="if(#{cboParent}.store.getCount()==0){#{storeReportParentColumn}.reload();}" />
                            <Select Handler="hdfParentId.setValue(this.getValue())"></Select>
                        </Listeners>
                        <Template runat="server">
                            <Html>
                            <tpl for=".">
                                <div class="list-item">
                                    <h3>{Name}</h3>
                                    <span style="color:blue;">{TypeName}</span> - Độ rộng còn lại: <span style="color:blue;">{WidthRemain}</span>
                                </div>
                            </tpl>
                            </Html>
                        </Template>    
                    </ext:ComboBox>
                    <ext:NumberField runat="server" ID="txtWidth" FieldLabel="Độ rộng" Text="100" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:NumberField runat="server" ID="txtHeight" FieldLabel="Chiều cao" Text="25" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" Text="0" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:ComboBox runat="server" ID="cboGroup" FieldLabel="Thuộc loại" Width="560">
                        <Items>
                            <ext:ListItem Text="Nhãn" Value="True" />
                            <ext:ListItem Text="Cột" Value="False" />
                        </Items>
                        <Listeners>
                            <Select Handler="hdfIsGroup.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboDataType" StoreID="storeReportColumnDataType" FieldLabel="Kiểu dữ liệu" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboDataType}.store.getCount()==0){#{storeReportColumnDataType}.reload();}" />
                            <Select Handler="hdfDataType.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboStatus" StoreID="storeReportColumnStatus" FieldLabel="Trạng thái" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboStatus}.store.getCount()==0){#{storeReportColumnStatus}.reload();}" />
                            <Select Handler="hdfStatus.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboType" StoreID="storeReportColumnType" FieldLabel="Vị trí" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboType}.store.getCount()==0){#{storeReportColumnType}.reload();}" />
                            <Select Handler="hdfType.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboSummaryRunning" StoreID="storeReportSummaryRunning" FieldLabel="Phạm vi tính" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboSummaryRunning}.store.getCount()==0){#{storeReportSummaryRunning}.reload();}" />
                            <Select Handler="hdfSummaryRunning.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboSummaryFunction" StoreID="storeReportSummaryFunction" FieldLabel="Hàm" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboSummaryFunction}.store.getCount()==0){#{storeReportSummaryFunction}.reload();}" />
                            <Select Handler="hdfSummaryFunction.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtValue" FieldLabel="Tham số" AnchorHorizontal="100%" />
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validateForm();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang tải..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdSetting.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>

            <!-- window multiple setting-->
            <ext:Window runat="server" ID="wdMultipleSetting" Resizable="true" Layout="FormLayout" Icon="Pencil" Title="Sửa nhiều cột" Padding="10" Width="800" Height="600" Hidden="true" Modal="true" Constrain="true" CloseAction="Hide">
                <Items>
                    <ext:ComboBox runat="server" ID="cboTextAlign2" StoreID="storeReportTextAlign" FieldLabel="Canh lề" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboTextAlign2}.store.getCount()==0){#{storeReportTextAlign}.reload();}" />
                            <Select Handler="hdfTextAlign2.setValue(this.getValue())"></Select>
                        </Listeners>

                        <Template Visible="False" ID="ctl154" EnableViewState="False"></Template>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtFormat2" FieldLabel="Định dạng" AnchorHorizontal="100%" />
                    <ext:NumberField runat="server" ID="txtFontSize2" FieldLabel="Cỡ chữ" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:NumberField runat="server" ID="txtWidth2" FieldLabel="Độ rộng" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:NumberField runat="server" ID="txtHeight2" FieldLabel="Chiều cao" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:ComboBox runat="server" ID="cboDataType2" StoreID="storeReportColumnDataType" FieldLabel="Kiểu dữ liệu" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboDataType2}.store.getCount()==0){#{storeReportColumnDataType}.reload();}" />
                            <Select Handler="hdfDataType2.setValue(this.getValue())"></Select>
                        </Listeners>

                        <Template Visible="False" ID="ctl160" EnableViewState="False"></Template>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboGroup2" FieldLabel="Thuộc loại" AnchorHorizontal="100%">
                        <Items>
                            <ext:ListItem Text="Nhãn" Value="True" />
                            <ext:ListItem Text="Cột" Value="False" />
                        </Items>
                        <Listeners>
                            <Select Handler="hdfIsGroup2.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboSummaryRunning2" StoreID="storeReportSummaryRunning" FieldLabel="Phạm vi tính" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboSummaryRunning2}.store.getCount()==0){#{storeReportSummaryRunning}.reload();}" />
                            <Select Handler="hdfSummaryRunning2.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboSummaryFunction2" StoreID="storeReportSummaryFunction" FieldLabel="Hàm" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboSummaryFunction2}.store.getCount()==0){#{storeReportSummaryFunction}.reload();}" />
                            <Select Handler="hdfSummaryFunction2.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave2" Text="Lưu" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="MultipleUpdate">
                                <EventMask ShowMask="true" Msg="Đang tải..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancel2" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdMultipleSetting.hide();gpReportColumn.getSelectionModel().clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <BeforeClose Handler="gpReportColumn.getSelectionModel().clearSelections();"></BeforeClose>
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>
