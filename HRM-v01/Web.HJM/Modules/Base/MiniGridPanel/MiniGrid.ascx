<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_MiniGridPanel_MiniGrid" Codebehind="MiniGrid.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="Config/GridConfig.ascx" TagName="GridConfig" TagPrefix="uc1" %>
<script src="../../Resource/js/GridPanelEvent.js" type="text/javascript"></script>
<script type="text/javascript">
    var GetStatus = function (value, p, record) {
        if (value == "1")
            return "<span style='color:blue'>Hiện</span>";
        else
            return "<span style='color:red'>Ẩn</span>";
    }
    var GetRenderValue = function (value, p, record) {
        if (value == "")
            return "Mặc định";
        if (value == "GetDateFormat")
            return "<span style='color:blue'>Ngày tháng</span>";
        if (value == "GetGender")
            return "<span style='color:blue'>Giới tính</span>";
        if (value == "RenderVND")
            return "<span style='color:blue'>Tiền tệ VNĐ</span>";
        if (value == "RenderUSD")
            return "<span style='color:blue'>Tiền tệ USD</span>";
        if (value == "GetBooleanIcon")
            return "<span style='color:blue'>Đúng/Sai</span>";
    }
    var enterKeyPressHandler = function (f, e) {
        if (e.getKey() == e.ENTER) {
            Store1.reload();
        }
    }
    var DeleteKey = function (DirectMethod, recordId) {
        if (recordId != "") {//btnDelete.disable 
            Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
                if (btn == "yes") {
                    DirectMethod.DirectDelete();
                }
            });

        }
    }
    var RemoveItemOnGridPanel = function (grid, Store) {
        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
            if (btn == "yes") {
                try {
                    grid.getRowEditor().stopEditing();
                } catch (e) {

                }
                var s = grid.getSelectionModel().getSelections();
                for (var i = 0, r; r = s[i]; i++) {
                    Store.remove(r);
                }
                Store.commitChanges();
                btnDelete.disable(); btnEdit.disable();
            }
        });
    }
    var GetColumnID = function (RowSelectID) {
        var count = RowSelectID.getCount();
        var lst = RowSelectID.getSelections();
        for (var i = 0; i < count; i++) {
            // document.getElementById("txtSelectedColumnName").value += lst[i].data.ColumnName + ",";
            alert(lst[i].id);
        }
    }

    //Thay đổi thứ tự của GridPanel rồi lưu vào CSDL
    var ChangeColumnOrder = function (GridPanel1) {
        var row = "";
        Ext.each(GridPanel1.getColumnModel().columns, function (column, index) {
            row += column.dataIndex + ",";
        });
        return row;
    }
    var ChangeColumnWidth = function (GridPanel1) {
        var row = "";
        Ext.each(GridPanel1.getColumnModel().columns, function (column, index) {
            // alert("id: " + column.id + " dataIndex: " + column.dataIndex + " index: " + index);
            row += column.dataIndex + "=" + column.width + ",";
        });
        return row;
    }
    var gridPanel;
    var directMethod;
    var store;
    var SetGridPanelAndDirectMethod = function (grid, direct, _store) {
        gridPanel = grid;
        directMethod = direct;
        store = _store;
    }
    var startEditing = function (e) {
        if (e.getKey() === e.ENTER) {
            var grid = gridPanel,
                    record = grid.getSelectionModel().getSelected(),
                    index = grid.store.indexOf(record);

            grid.startEditing(index, 1);
        }
    }; 
    var afterEditInMiniGrid = function (e) {
        gridPanel.submitData(false);
        // directMethod.AfterEdit(e.field, e.value);
        // store.commitChanges();
    };
     
    var DeleteItemOnGrid = function (grid, store) { 
        var s = grid.getSelectionModel().getSelections();
            for (var i = 0, r; r = s[i]; i++) {
               store.remove(r);
        }
        store.commitChanges();
    }

    var DuplicateData = function (gridPanel) { 
        var index = gridPanel.store.indexOf(gridPanel.getSelectionModel().getSelected());
        alert(gridPanel.getSelectionModel().getSelected().data.GHI_CHU);


        gridPanel.insertRecord(index, {
            GHI_CHU: "test1", 
        });

        gridPanel.getView().refresh();
        gridPanel.getSelectionModel().selectRow(index);
        gridPanel.getRowEditor().startEditing(index);
    }
</script>
<style type="text/css">
    .x-toolbar
    {
        width: 100% !important;
    }
</style>
<ext:Hidden ID="hdfRecordId" runat="server" />
<ext:Hidden ID="hdfIsInserting" runat="server" />
<ext:Hidden ID="hdfSqlInsertCommand" runat="server" />
<ext:Hidden ID="hdfForeignKeyName" runat="server" />
<ext:Hidden ID="hdfForeignKeyValue" runat="server" />
<%--Chức các bản ghi được thêm vào--%>
<ext:Hidden ID="hdfJsonObjects" runat="server" />
<asp:PlaceHolder runat="server" ID="plcConfig"></asp:PlaceHolder>
<ext:Menu ID="RowContextMenu" runat="server">
    <Items>
        <ext:MenuItem ID="mnuAddNew" runat="server" Icon="Add" Text="Thêm mới">
        </ext:MenuItem>
        <ext:MenuItem ID="MenuItem4" runat="server" Icon="Delete" Text="Xóa">
            <DirectEvents>
                <Click OnEvent="btnDelete_Click">
                    <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa !" ConfirmRequest="true" />
                    <EventMask ShowMask="true" Msg="Đang xóa dữ liệu" />
                </Click>
            </DirectEvents>
        </ext:MenuItem>
    </Items>
</ext:Menu>
<ext:Container runat="server" ID="clayout" AnchorHorizontal="100%" Layout="FormLayout">
    <Items>
        <ext:GridPanel ID="GridPanel1" Border="false" runat="server" StripeRows="true" Width="980"
            Height="150" AutoScroll="true" ClicksToEdit="2">
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                </ext:RowSelectionModel>
            </SelectionModel>
            <Store>
                <ext:Store ID="Store1" runat="server" OnSubmitData="Store1_OnSubmitData" OnRefreshData="Store1_refreshData" AutoLoad="false">
                </ext:Store>
            </Store>
            <Listeners>
                <KeyDown Fn="startEditing" />
                <AfterEdit Fn="afterEditInMiniGrid" />
            </Listeners>
            <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
            <TopBar>
                <ext:Toolbar runat="server" ID="tbar">
                    <Items>
                        <ext:Button ID="btnAddRecord" runat="server" Text="Thêm mới" Icon="Add">
                        </ext:Button>
                        <ext:Button ID="btnDeleteRecord" runat="server" Text="Xóa" Icon="Delete">
                        </ext:Button>
                        <ext:Button ID="btnCopyDataInMiniGrid" runat="server" Text="Nhân đôi dữ liệu" Icon="DiskMultiple">
                            <Listeners>
                                <Click Handler="DuplicateData(#{GridPanel1});" />
                            </Listeners>
                        </ext:Button>  
                        <ext:Button ID="btnConfig" runat="server" Text="Cấu hình" Icon="Cog">
                            <Menu>
                                <ext:Menu ID="Menu1" runat="server">
                                    <Items>
                                        <ext:MenuItem ID="mnuTableInformationConfig" runat="server" Icon="Table" Text="Thông tin bảng">
                                            <DirectEvents>
                                                <Click OnEvent="mnuTableInformationConfig_Click">
                                                    <EventMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Config" Value="GridPanel" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:MenuItem>
                                        <ext:MenuItem ID="mnuColumnConfig" runat="server" Text="Thông tin cột" Icon="ControlBlankBlue">
                                            <%--<DirectEvents>
                                                <Click OnEvent="mnuColumnConfig_Click">
                                                    <EventMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Config" Value="GridPanel" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>--%>
                                        </ext:MenuItem>
                                    </Items>
                                </ext:Menu>
                            </Menu>
                        </ext:Button>
                        <ext:ToolbarFill runat="server" ID="tbfull" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:GridPanel>
    </Items>
</ext:Container>
