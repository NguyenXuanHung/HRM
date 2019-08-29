<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManagement.aspx.cs" Inherits="Web.HRM.Modules.Setting.MenuManagement" %>

<%@ Register Src="~/Modules/UC/Resource.ascx" TagPrefix="UC" TagName="Resource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <UC:Resource runat="server" ID="resource" />
    <script type="text/javascript">
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() == e.ENTER) {
                reloadGrid();
            }
        };
        var reloadGrid = function () {
            gpMenu.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        }
        var validateForm = function () {
            if (!txtMenuName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        };

        var iconImg = function (name) {
            return "<img src='/Resource/icon/" + name + ".png'>";
        }
        
        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('cross');
            }
            return '';
        }

        var RenderMenuGroup = function (value, p, record) {
            var styleColor = '';
            switch (record.data.Group) {
            case "Dashboard":
                styleColor = 'limegreen';
                break;
            case "MenuLeft":
                styleColor = 'blue';
                break;
            case "MenuTop":
                styleColor = 'fuchsia';
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
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfFilterGroup" />
            <ext:Hidden runat="server" ID="hdfParentId" />
            <ext:Hidden runat="server" ID="hdfGroup" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <!-- store -->
            <ext:Store ID="storeMenu" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Security/HandlerMenu.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={1000}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="group" Value="hdfFilterGroup.getValue()" Mode="Raw" />
                    <ext:Parameter Name="returnType" Value="tree" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="MenuName" />
                            <ext:RecordField Name="DisplayName" />
                            <ext:RecordField Name="TabName" />
                            <ext:RecordField Name="LinkUrl" />
                            <ext:RecordField Name="Icon" />
                            <ext:RecordField Name="ParentId" />
                            <ext:RecordField Name="ParentName" />
                            <ext:RecordField Name="Order" />
                            <ext:RecordField Name="Group" />
                            <ext:RecordField Name="GroupName" />
                            <ext:RecordField Name="IsPanel" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeParentMenu" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Security/HandlerMenu.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={1000}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="group" Value="hdfFilterGroup.getValue()" Mode="Raw" />
                    <ext:Parameter Name="returnType" Value="tree" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="DisplayName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportDynamic" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Report/HandlerReportDynamic.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={10}" />
                </AutoLoadParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeMenuGroup" runat="server" AutoLoad="False" OnRefreshData="storeMenuGroup_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeMenuStatus" runat="server" AutoLoad="False" OnRefreshData="storeMenuStatus_OnRefreshData">
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
                            <ext:GridPanel runat="server" ID="gpMenu" StoreID="storeMenu" TrackMouseOver="true" Header="false" StripeRows="true" AutoExpandColumn="DisplayName"
                                Border="false" Layout="Fit" AutoScroll="True">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
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
                                                            <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
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
                                                            <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarFill />
                                            <ext:ComboBox runat="server" ID="cboFilterGroup" StoreID="storeMenuGroup" DisplayField="Name" ValueField="Id" Width="150">
                                                <Listeners>
                                                    <Expand Handler="if(#{cboFilterGroup}.store.getCount()==0){#{storeMenuGroup}.reload();}" />
                                                    <Select Handler="hdfFilterGroup.setValue(this.getValue());reloadGrid();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer runat="server" Width="10"/>
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="150" EmptyText="Tìm theo tên, tab">
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
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="TT" Width="40" Align="Center" />
                                        <ext:Column ColumnID="DisplayName" Header="Tên" Align="Left" DataIndex="DisplayName" />
                                        <ext:Column ColumnID="ParentName" Header="Thuộc mục" Width="300" Align="Left" DataIndex="ParentName" />
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="100" Align="Center" DataIndex="Order" />
                                        <ext:Column ColumnID="GroupName" Header="Nhóm" Width="100" Align="Center" DataIndex="GroupName">
                                            <Renderer Fn="RenderMenuGroup"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="StatusName" Header="Trạng thái" Width="100" Align="Center" DataIndex="StatusName">
                                            <Renderer Fn="RenderRowStatus"></Renderer>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="1000" DisplayInfo="true" DisplayMsg="Từ {0} - {1}" EmptyMsg="Không có dữ liệu">
                                        <Listeners>
                                            <Change Handler="rowSelectionModel.clearSelections();btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <!-- window -->
            <ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="700" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
                <Items>
                    <ext:TextField runat="server" ID="txtMenuName" CtCls="requiredData" FieldLabel="Tên <span style='color:red;'> * </span>" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtTabName" FieldLabel="Tab" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtIcon" FieldLabel="Icon" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtLinkUrl" FieldLabel="Liên kết" AnchorHorizontal="100%" />
                    <ext:ComboBox runat="server" ID="cboReport" StoreID="storeReportDynamic" FieldLabel="Chọn báo cáo" DisplayField="Name" ValueField="Id" Width="560" PageSize="10">
                        <Listeners>
                            <Expand Handler="if(#{cboReport}.store.getCount()==0){#{storeReportDynamic}.reload();}" />
                            <Select Handler="txtLinkUrl.setValue('Modules/Report/DynamicReportView.aspx?reportId=' + this.getValue());cboFilter.clearValue();"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboFilter" FieldLabel="Điều kiện lọc" Width="560">
                        <Items>
                            <ext:ListItem Text="Bảng lương" Value="&filter=Payroll"/>
                        </Items>
                        <Listeners>
                            <Select Handler="txtLinkUrl.setValue(txtLinkUrl.getValue() + this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboParent" StoreID="storeParentMenu" FieldLabel="Thuộc mục" DisplayField="DisplayName" ValueField="Id" Width="560" PageSize="1000">
                        <Listeners>
                            <Expand Handler="if(#{cboParent}.store.getCount()==0){#{storeParentMenu}.reload();}" />
                            <Select Handler="hdfParentId.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" Text="0" AnchorHorizontal="100%"></ext:NumberField>
                    <ext:ComboBox runat="server" ID="cboGroup" StoreID="storeMenuGroup" FieldLabel="Nhóm" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboGroup}.store.getCount()==0){#{storeMenuGroup}.reload();}" />
                            <Select Handler="hdfGroup.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboStatus" StoreID="storeMenuStatus" FieldLabel="Trạng thái" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboStatus}.store.getCount()==0){#{storeMenuStatus}.reload();}" />
                            <Select Handler="hdfStatus.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
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
        </div>
    </form>
</body>
</html>
