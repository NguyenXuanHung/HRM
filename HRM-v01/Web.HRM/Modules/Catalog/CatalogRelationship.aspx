<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Catalog.CatalogRelationship" CodeBehind="CatalogRelationship.aspx.cs" %>

<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">

        // handler enter key
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() == e.ENTER) {
                search();
            }
        };

        // search
        var search = function () {
            if (txtKeyword.getValue() != '') {
                txtKeyword.triggers[0].show();
            }
            reloadGrid();
        };

        // validate form
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        };

        // reload grid
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div id="main">
<ext:ResourceManager ID="RM" runat="server" />
<!-- hidden -->
<ext:Hidden runat="server" ID="hdfId" />
<ext:Hidden runat="server" ID="hdfOrder" />
<ext:Hidden runat="server" ID="hdfStatus" />
<!-- store -->
<ext:Store ID="storeCatalog" AutoSave="true" runat="server">
    <Proxy>
        <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogRelationship.ashx" />
    </Proxy>
    <AutoLoadParams>
        <ext:Parameter Name="start" Value="={0}" />
        <ext:Parameter Name="limit" Value="={30}" />
    </AutoLoadParams>
    <BaseParams>
        <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
    </BaseParams>
    <Reader>
        <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
            <Fields>
                <ext:RecordField Name="Id" />
                <ext:RecordField Name="Name" />
                <ext:RecordField Name="Description" />
                <ext:RecordField Name="HasHusband" />
                <ext:RecordField Name="Order" />
                <ext:RecordField Name="Status" />
                <ext:RecordField Name="StatusName" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<!-- viewport -->
<ext:Viewport runat="server" ID="viewport" HideBorders="true">
    <Items>
        <ext:BorderLayout runat="server">
            <Center>
                <ext:GridPanel runat="server" ID="gpCatalog" StoreID="storeCatalog" TrackMouseOver="true" Header="false" StripeRows="true" AutoExpandColumn="Description"
                               Border="false" Layout="Fit">
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
                                                <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
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
                                                <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill />
                                <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="keyPresstxtSearch" />
                                        <TriggerClick Handler="this.triggers[0].hide();this.clear();ReloadGrid();" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                    <Listeners>
                                        <Click Handler="ReloadGrid();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn Header="TT" Width="30" Align="Right" />
                            <ext:Column ColumnID="Name" Header="Tên" Width="300" Align="Left" DataIndex="Name" />
                            <ext:Column ColumnID="Description" Header="Mô tả" DataIndex="Description" />
                            <ext:Column ColumnID="HasHusband" Header="Quan hệ bên chồng" Width="200" DataIndex="HasHusband" Align="Center">
                                <Renderer Fn="renderHasHusband"></Renderer>
                            </ext:Column>
                            <ext:Column ColumnID="Order" Header="Thứ tự" Width="80" DataIndex="Order" Align="Center" />
                            <ext:Column ColumnID="IsDeleted" Header="Trạng thái" Width="80" DataIndex="IsDeleted" Align="Center">
                                <Renderer Fn="renderStatus"></Renderer>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="RowSelectionModel1" SingleSelect="True">
                            <Listeners>
                                <RowSelect Handler="hdfId.setValue(RowSelectionModel1.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                <RowDeselect Handler="btnEdit.disable();btnDelete.disable();" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar runat="server" ID="PagingToolbar1" PageSize="30" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
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
                                        <Select Handler="#{PagingToolbar1}.pageSize=parseInt(this.getValue());#{PagingToolbar1}.doLoad();"></Select>
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                            <Listeners>
                                <Change Handler="RowSelectionModel1.clearSelections();btnEdit.disable();btnDelete.disable();" />
                            </Listeners>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Center>
        </ext:BorderLayout>
    </Items>
</ext:Viewport>
<!-- window -->
<ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="500" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
    <Items>
        <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên <span style='color:red;'> * </span>" AnchorHorizontal="100%" />
        <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" Width="360" Height="100"></ext:TextArea>
        <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" Width="360"></ext:NumberField>        
        <ext:Checkbox runat="server" ID="chkWifeOrHusband" FieldLabel="Quan hệ bên chồng" Checked="True" />
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
