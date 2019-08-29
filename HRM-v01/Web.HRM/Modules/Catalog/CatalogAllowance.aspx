<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogAllowance.aspx.cs" Inherits="Web.HRM.Modules.Catalog.CatalogAllowance" %>
<%@ Register Src="~/Modules/UC/Resource.ascx" TagPrefix="CCVC" TagName="Resource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:Resource runat="server" id="Resource" />
    <script type="text/javascript">
        // handler enter key
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                search();
            } else {
                if (txtKeyword.getValue() !== '') {
                    txtKeyword.triggers[0].show();
                }
            }
        };

        // search
        var search = function () {
            if (txtKeyword.getValue() == '') {
                txtKeyword.triggers[0].hide();
            }
            else {
                txtKeyword.triggers[0].show();
            }
            reloadGrid();
        };

        // reload form
        var reloadGrid = function () {
            rowSelectionModel.clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        };

        // validate form
        var validateForm = function () {
            if (txtName.getValue() == '' || txtName.getValue().trim == '') {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />

            <!-- hidden -->
            <ext:Hidden runat="server" ID="hdfOrder" />
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfValueType" />
            <ext:Hidden runat="server" ID="hdfType" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfGroup" />
            
            <!-- store -->
            <ext:Store ID="storeCatalog" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogAllowance.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={50}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="Value" />
                            <ext:RecordField Name="Formula" />
                            <ext:RecordField Name="ValueType" />
                            <ext:RecordField Name="ValueTypeName" />
                            <ext:RecordField Name="Type" />
                            <ext:RecordField Name="TypeName" />
                            <ext:RecordField Name="Taxable" />
                            <ext:RecordField Name="Group" />
                            <ext:RecordField Name="GroupName" />
                            <ext:RecordField Name="Order" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>     
        
            <ext:Store runat="server" ID="storeValueType" OnRefreshData="storeValueType_OnRefreshData" AutoLoad="False">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            
            <ext:Store runat="server" ID="storeType" OnRefreshData="storeType_OnRefreshData" AutoLoad="False">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeGroup" OnRefreshData="storeGroup_OnRefreshData" AutoLoad="False">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeStatus" OnRefreshData="storeStatus_OnRefreshData" AutoLoad="False">
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
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gpCatalog" StoreID="storeCatalog" TrackMouseOver="true" Header="false" runat="server" AutoExpandColumn="Description"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
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
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
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
                                        <ext:RowNumbererColumn Header="TT" Width="30" Align="Right" />
                                        <ext:Column ColumnID="Code" Header="Mã" Width="200" Align="Left" DataIndex="Code" />
                                        <ext:Column ColumnID="Name" Header="Tên" Width="200" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="200" DataIndex="Description" Align="Left" />
                                        <ext:NumberColumn ColumnID="Value" Header="Giá trị" Width="100" DataIndex="Value" Align="Center" />
                                        <ext:Column ColumnID="Formula" Header="Công thức" Width="150" DataIndex="Formula" Align="Center" />
                                        <ext:Column ColumnID="ValueTypeName" Header="Kiểu giá trị" Width="100" DataIndex="ValueTypeName" Align="Center" />
                                        <ext:Column ColumnID="GroupName" Header="Nhóm" Width="150" DataIndex="GroupName" Align="Center" />
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="80" DataIndex="Order" Align="Center" />
                                        <ext:Column ColumnID="StatusName" Header="Trạng thái" Width="80" DataIndex="StatusName" Align="Center" />
                                    </Columns>
                                </ColumnModel>
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="50" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
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
                                                <SelectedItem Value="50" />
                                                <Listeners>
                                                    <Select Handler="#{pagingToolbar}.pageSize=parseInt(this.getValue());#{pagingToolbar}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
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
        <ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="500" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
                <Items>
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên <span style='color:red;'>*</span>" AnchorHorizontal="100%"></ext:TextField>
                    <ext:TextField runat="server" ID="txtDescription" FieldLabel="Mô tả"  AnchorHorizontal="100%"></ext:TextField>
                    <ext:TextField runat="server" ID="txtValue" FieldLabel="Giá trị" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtFormula" FieldLabel="Công thức" AnchorHorizontal="100%" />
                    <ext:ComboBox runat="server" ID="cboValueType" StoreID="storeValueType" FieldLabel="Loại giá trị" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboValueType}.store.getCount()==0){#{storeValueType}.reload();}"></Expand>
                            <Select Handler="hdfValueType.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboType" StoreID="storeType" FieldLabel="Loại phụ cấp" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboType}.store.getCount()==0){#{storeType}.reload();}"></Expand>
                            <Select Handler="hdfType.setValue(this.getValue()); txtName.setValue(this.getText());"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboGroup" StoreID="storeGroup" FieldLabel="Nhóm" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboGroup}.store.getCount()==0){#{storeGroup}.reload();}"></Expand>
                            <Select Handler="hdfGroup.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" AnchorHorizontal="100%" />
                    <ext:ComboBox runat="server" ID="cboStatus" StoreID="storeStatus" FieldLabel="Trạng thái" DisplayField="Name" ValueField="Id"  AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboStatus}.store.getCount()==0){#{storeStatus}.reload();}"></Expand>
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
