<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLogManagement.aspx.cs" Inherits="Web.HRM.Modules.Setting.SystemLogManagement" %>

<%@ Register Src="~/Modules/UC/Resource.ascx" TagPrefix="UC" TagName="Resource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <UC:Resource runat="server" ID="Resource" />
    <script type="text/javascript">
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() == e.ENTER) {
                search();
            }
        };
        var search = function () {
            if (txtKeyword.getValue() != '') {
                txtKeyword.triggers[0].show();
            }
            reloadGrid();
        };
        var reloadGrid = function () {
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        };

        var RenderSystemLogType = function(value, p, record) {
            var styleColor = '';
            switch (record.data.Type) {
            case "ScheduleAction":
                styleColor = 'limegreen';
                break;
            case "UserAction":
                styleColor = 'blue';
                    break;
            case "UserError":
                styleColor = 'blue';
                    break;
            case "HandlerException":
                styleColor = 'blue';
                break;
            case "ScheduleError":
                styleColor = 'fuchsia';
                    break;
            case "UnHandlerException":
                styleColor = 'red';
                break;
            }
            return "<span style='font-weight:bold;color:" + styleColor + "'>" + value + "</span>";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <!-- hidden field -->
            <ext:Hidden ID="hdfIsEx" runat="server" />
            <!-- store -->
            <ext:Store ID="storeSystemLog" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Security/HandlerSystemLog.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="isEx" Value="hdfIsEx.getValue()" Mode="Raw" />
                    <ext:Parameter Name="fromDate" Value="dfFromDate.getValue()" Mode="Raw" />
                    <ext:Parameter Name="toDate" Value="dfToDate.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Username" />
                            <ext:RecordField Name="Thread" />
                            <ext:RecordField Name="Action" />
                            <ext:RecordField Name="ActionName" />
                            <ext:RecordField Name="Type" />
                            <ext:RecordField Name="TypeName" />
                            <ext:RecordField Name="ShortDescription" />
                            <ext:RecordField Name="LongDescription" />
                            <ext:RecordField Name="IsException" />
                            <ext:RecordField Name="CreatedDate" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <!-- viewport -->
            <ext:Viewport runat="server" ID="viewport" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpSystemLog" StoreID="storeSystemLog" TrackMouseOver="true" Header="false" StripeRows="true" AutoExpandColumn="LongDescription"
                                Border="false" Layout="Fit">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:ToolbarFill />
                                            <ext:Label ID="lbFromDate" runat="server" Text="Từ ngày: "></ext:Label>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:DateField runat="server" ID="dfFromDate" AnchorHorizontal="95%" Editable="true" EnableKeyEvents="true" 
                                                           MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                           RegexText="Định dạng ngày không đúng">
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="10" />
                                            <ext:Label ID="lbToDate" runat="server" Text="Đến ngày: "></ext:Label>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:DateField runat="server" ID="dfToDate" AnchorHorizontal="95%" Editable="true" EnableKeyEvents="true" 
                                                           MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                           RegexText="Định dạng ngày không đúng">
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="10" />
                                            <ext:TriggerField runat="server" ID="txtKeyword" AnchorHorizontal="95%" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();reloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Lọc" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="search();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="TT" Width="30" Align="Right" />
                                        <ext:Column ColumnID="Username" Header="Tên" Width="120" Align="Left" DataIndex="Username" />
                                        <ext:Column ColumnID="Thread" Header="Nguồn" Width="120" Align="Left" DataIndex="Thread" />
                                        <ext:Column ColumnID="ActionName" Header="Hành động" Width="120" Align="Center" DataIndex="ActionName" />
                                        <ext:Column ColumnID="TypeName" Header="Loại sự kiện" Width="150" Align="Center" DataIndex="TypeName">
                                            <Renderer Fn="RenderSystemLogType"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="ShortDescription" Header="Mô tả" Width="200" Align="Left" DataIndex="ShortDescription" />
                                        <ext:Column ColumnID="LongDescription" Header="Chi tiết" Align="Left" DataIndex="LongDescription" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Thời gian tạo" Align="Left" Width="150" DataIndex="CreatedDate" Format="dd/MM/yyyy HH:mm"></ext:DateColumn>
                                    </Columns>
                                </ColumnModel>
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
