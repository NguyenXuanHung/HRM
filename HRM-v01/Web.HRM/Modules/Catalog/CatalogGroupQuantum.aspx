<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogGroupQuantum.aspx.cs" Inherits="Web.HRM.Modules.Catalog.CatalogGroupQuantum" %>
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
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfGroup" />
            
            <!-- store -->
            <ext:Store ID="storeCatalog" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogGroupQuantum.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="GradeMax" />
                            <ext:RecordField Name="MonthStep" />
                            <ext:RecordField Name="PercentageOverGrade" />
                            <ext:RecordField Name="PercentageOverGradeStep" />
                            <ext:RecordField Name="Group" />
                            <ext:RecordField Name="GroupName" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                            <ext:RecordField Name="Order" />
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
                                        <ext:Column ColumnID="Name" Header="Tên" Width="300" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="200" DataIndex="Description" />
                                        <ext:Column ColumnID="GradeMax" Header="Số bậc tối đa" Width="100" DataIndex="GradeMax" Align="Center" />
                                        <ext:Column ColumnID="MonthStep" Header="Số tháng nâng lương" Width="130" DataIndex="MonthStep" Align="Center" />
                                        <ext:NumberColumn ColumnID="PercentageOverGrade" Header="% Vượt khung" Width="130" DataIndex="PercentageOverGrade" Align="Center" Format="0.00 %" />
                                        <ext:NumberColumn ColumnID="PercentageOverGradeStep" Header="% Tăng vượt khung" Width="130" DataIndex="PercentageOverGradeStep" Align="Center" Format="0.00 %" />
                                        <ext:Column ColumnID="GroupName" Header="Nhóm" Width="250" DataIndex="GroupName" Align="Left" />
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
                    <ext:NumberField runat="server" ID="txtGradeMax" FieldLabel="Số bậc tối đa" AnchorHorizontal="100%" CtCls="requiredData"/>
                    <ext:NumberField runat="server" ID="txtMonthStep" FieldLabel="Tháng nâng lương" AnchorHorizontal="100%" CtCls="requiredData"/>
                    <ext:TextField runat="server" ID="txtPercentageOverGrade" FieldLabel="% Vượt khung" AnchorHorizontal="100%" MaskRe="[0-9,]" />
                    <ext:TextField runat="server" ID="txtPercentageOverGradeStep" FieldLabel="% Tăng vượt khung" AnchorHorizontal="100%" MaskRe="[0-9,]" />
                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" AnchorHorizontal="100%" />
                    <ext:ComboBox runat="server" ID="cboGroup" StoreID="storeGroup" FieldLabel="Nhóm" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                        <Listeners>
                            <Expand Handler="if(#{cboGroup}.store.getCount()==0){#{storeGroup}.reload();}"></Expand>
                            <Select Handler="hdfGroup.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
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
