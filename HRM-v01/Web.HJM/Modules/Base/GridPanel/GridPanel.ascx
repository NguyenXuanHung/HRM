<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_GridPanel" Codebehind="GridPanel.ascx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Form/Form.ascx" TagName="Form" TagPrefix="uc2" %>
<%@ Register Src="Config/GridConfig.ascx" TagName="GridConfig" TagPrefix="uc1" %>
<%@ Register Src="../OneManyForm/OneManyForm.ascx" TagName="OneManyForm" TagPrefix="uc3" %>

<style type="text/css">
    .highlight
    {
        background: yellow;
    }
</style>

<script src="/Resource/js/jquery-3.2.1.min.js" type="text/javascript"></script>
<script src="/Resource/js/jquery.ui.core.min.js" type="text/javascript"></script>
<script src="/Resource/js/jquery.ui.widget.min.js" type="text/javascript"></script>
<script src="/Resource/js/jquery.ui.mouse.min.js" type="text/javascript"></script>
<script src="/Resource/js/jquery.ui.sortable.min.js" type="text/javascript"></script>
<script src="/Resource/js/RenderJS.js" type="text/javascript"></script>

<asp:Literal Text="" ID="ltrAfterEditJs" runat="server" />

<script type="text/javascript">
    var RemoveItemOnBaseGrid = function (grid, Store, directMethod) {
        try {
            grid.getRowEditor().stopEditing();
        } catch (e) {

        }
        var s = grid.getSelectionModel().getSelections();
        for (var i = 0, r; r = s[i]; i++) {
            Store.remove(r);
            //     directMethod.DirectDelete();
            Store.commitChanges();
        }
    }
    var GetStatus = function (value, p, record) {
        if (value == "1")
            return "<span style='color:blue'>Hiện</span>";
        else
            return "<span style='color:red'>Ẩn</span>";
    }
    var CheckInputNewPrimaryKey = function (textField) {
        if (textField.getValue().trim() == '') {
            alert('Bạn chưa nhập khóa chính mới !');
            textField.focus();
            return false;
        }
        return true;
    }
    var GetRenderValue = function (value, p, record) {
        if (value == "")
            return "Mặc định";
        if (value == "GetDateFormat")
            return "<span style='color:blue'>Ngày tháng</span>";
        if (value == "GetDateFormatIncludeTime")
            return "<span style='color:blue'>Ngày tháng có giờ</span>";
        if (value == "GetGender")
            return "<span style='color:blue'>Giới tính</span>";
        if (value == "RenderVND")
            return "<span style='color:blue'>Tiền tệ VNĐ</span>";
        if (value == "RenderUSD")
            return "<span style='color:blue'>Tiền tệ USD</span>";
        if (value == "GetBooleanIcon")
            return "<span style='color:blue'>Đúng/Sai</span>";
        return "<span style='color:blue'>" + value + "</span>";
    }
    var RenderVND = function (value, p, record) {
        if (value == null)
            return "";
        var l = (value + "").length;
        var s = value + "";
        var rs = "";
        var count = 0;
        for (var i = l - 1; i >= 0; i--) {
            count++;
            if (count == 3) {
                rs = "." + s.charAt(i) + rs;
                count = 0;
            }
            else {
                rs = s.charAt(i) + rs;
            }
        }
        if (rs.charAt(0) == '.') {
            return rs.substring(1, rs.length) + " VNĐ";
        }
        return rs + " VNĐ";
    }
    var enterKeyPressHandler = function (f, e) {
        if (e.getKey() == e.ENTER) {
            StaticPagingToolbar.pageIndex = 0; StaticPagingToolbar.doLoad(); 
        }
        if (txtSearchKey.getValue() != '') {
            this.triggers[0].show();
        } else {
            this.triggers[0].hide();
        }
    }
    var DeleteKey = function (grid, Store, deleteButton, editButton, directMethod, recordId) {
        if (recordId != "") {
            // RemoveItemOnGridPanel(grid, Store, deleteButton, editButton, directMethod);
        }
    }
    var ChangeBetweenTableAndView = function (FieldStore, AutoExpandColumn) {
        AutoExpandColumn.clearValue();
        FieldStore.reload();
        try {
            AutoExpandColumn.setValue(AutoExpandColumn.store.getAt(0).get('Name'));
        }
        catch (err) {
            //Handle errors here
        }
    }
    var store;
    var directMethod;
    var setVariable = function (_store, _directmethod) {
        store = _store;
        directMethod = _directmethod;
    }
    var SaveColumnName = function (gridPanel, hdfSelectedColumnName) {
        var count = gridPanel.getSelectionModel().getCount();
        var lst = gridPanel.getSelectionModel().getSelections();
        hdfSelectedColumnName.setValue("");
        for (var i = 0; i < count; i++) {
            var _value = hdfSelectedColumnName.getValue();
            _value += lst[i].data.ColumnName + ",";
            hdfSelectedColumnName.setValue(_value);
        }
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
    var ChangeColumnOrder = function (extGridPanel) {
        var row = "";
        Ext.each(extGridPanel.getColumnModel().columns, function (column, index) {
            row += column.dataIndex + ",";
        });
        return row;
    }
    var ChangeColumnWidth = function (extGridPanel) {
        var row = "";
        Ext.each(extGridPanel.getColumnModel().columns, function (column, index) {
            // alert("id: " + column.id + " dataIndex: " + column.dataIndex + " index: " + index);
            row += column.dataIndex + "=" + column.width + ",";
        });
        return row;
    } 
    var RenderHightLight = function (value, p, record) {
        var keyword = document.getElementById("txtSearchKey").value;
        if (keyword == "" || keyword == "Nhập từ khóa tìm kiếm...")
            return value;
        var rs = "<p>" + value + "</p>";
        var keys = keyword.split(" ");
        for (i = 0; i < keys.length; i++) {
            if ($.trim(keys[i]) != "") {
                var o = { words: keys[i] };
                rs = highlight(o, rs);
            }
        }
        return rs;
    }
    var highlight = function(options, content) {
        var o = {
            words: '',
            caseSensitive: false,
            wordsOnly: true,
            template: '$1<span class="highlight">$2</span>$3'
        }, pattern;
        $.extend(true, o, options || {});
        if (o.words.length == 0) { return; }
        pattern = new RegExp('(>[^<.]*)(' + o.words + ')([^<.]*)', o.caseSensitive ? "" : "ig");
        return content.replace(pattern, o.template);
    }

    var CheckSelectRow = function (hdfRecordId) {
        if (hdfRecordId.getValue() == "") {
            window.Ext.Msg.alert('Thông báo', 'Bạn phải chọn ít nhất một dòng để nhân đôi dữ liệu');
            return false;
        } else {
            return true;
        }
    } 
</script>



<!-- resource manager -->
<ext:ResourceManager ID="RM" runat="server" />
<!-- hidden field -->
<ext:Hidden runat="server" ID="sHdfgridName" /> <!-- grid name -->
<ext:Hidden runat="server" ID="sHdftable" /> <!-- table name -->
<ext:Hidden runat="server" ID="sHdfLimit" />

<ext:Hidden runat="server" ID="sHdfPrimaryKeyName" />
<ext:Hidden runat="server" ID="sHdfwhere" />
<ext:Hidden runat="server" ID="sHdfOrderBy" />
<ext:Hidden runat="server" ID="hdfColumnList" EnableViewState="true" />

<ext:Hidden ID="hdfQueryPhu" runat="server" EnableViewState="true" />
<ext:Hidden ID="hdfRecordId" IDMode="Static" runat="server" />

<!-- place holder window, config -->
<asp:PlaceHolder runat="server" ID="plcWindowOneManyForm" />
<asp:PlaceHolder runat="server" ID="plcWindowAddForm" />
<asp:PlaceHolder runat="server" ID="plcWindowEditForm" />
<asp:PlaceHolder runat="server" ID="plcConfig" />

<!-- data source -->
<ext:Store ID="Store1" IDMode="Static" ShowWarningOnFailure="true" runat="server" AutoLoad="False">
    <Proxy>
        <ext:HttpProxy Json="true" Method="GET" Url="Handler.ashx" />
    </Proxy>
    <AutoLoadParams>
        <ext:Parameter Name="start" Value="={0}" />
    </AutoLoadParams>
    <BaseParams>
        <ext:Parameter Name="gridName" Value="#{sHdfgridName}.getValue()" Mode="Raw" />
        <ext:Parameter Name="table" Value="#{sHdftable}.getValue()" Mode="Raw" />
        <ext:Parameter Name="primarykey" Value="#{sHdfPrimaryKeyName}.getValue()" Mode="Raw" />
        <ext:Parameter Name="where" Value="#{sHdfwhere}.getValue()" Mode="Raw" />
        <ext:Parameter Name="keyword" Value="#{txtSearchKey}.getValue()" Mode="Raw" />
        <ext:Parameter Name="OutsideQuery" Value="#{hdfQueryPhu}.getValue()" Mode="Raw" />
        <ext:Parameter Name="OrderBy" Value="#{sHdfOrderBy}.getValue()" Mode="Raw" />
        <ext:Parameter Name="Column" Value="#{hdfColumnList}.getValue()" Mode="Raw" />
    </BaseParams>
</ext:Store>

<!-- context menu -->
<ext:Menu ID="RowContextMenu" runat="server">
    <Items>
        <ext:MenuItem ID="mnuAddNew" runat="server" Icon="Add" Text="Thêm mới">
            <DirectEvents>
                <Click OnEvent="btnAdd_Click">
                    <EventMask Msg="Đang tải dữ liệu..." ShowMask="true" />
                </Click>
            </DirectEvents>
        </ext:MenuItem>
        <ext:MenuItem ID="MenuItem4" runat="server" Icon="Delete" Text="<%$ Resources:Language, delete%>">
            <DirectEvents>
                <Click OnEvent="btnDelete_Click">
                    <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa !" ConfirmRequest="true" />
                    <EventMask ShowMask="true" Msg="Đang xóa dữ liệu" />
                </Click>
            </DirectEvents>
        </ext:MenuItem>
        <ext:MenuItem ID="mnuEditUser" runat="server" Icon="Pencil" Text="Sửa">
            <Listeners>
                <Click Handler="if(#{extGridPanel}.getSelectionModel().getCount()>1){alert('Bạn chỉ được chọn 1 dòng để sửa');return false;}" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnEdit_Click">
                    <EventMask Msg="Đang tải dữ liệu..." ShowMask="true" />
                </Click>
            </DirectEvents>
        </ext:MenuItem>
        <ext:MenuSeparator runat="server" ID="mnuDuplicate" />
        <ext:MenuItem ID="mnuDuplicateData" runat="server" Icon="DiskMultiple" Text="Nhân đôi dữ liệu">
            <Listeners>
                <Click Handler="return CheckSelectRow(#{hdfRecordId});" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCopyData_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                    <Confirmation Title="Cảnh báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn nhân đôi dữ liệu !" />
                </Click>
            </DirectEvents>
        </ext:MenuItem>
    </Items>
</ext:Menu>
<!-- main view -->
<ext:Viewport ID="vpMain" runat="server" Layout="Center">
    <Items>
        <ext:BorderLayout ID="borderLayout" runat="server">
            <Center>
                <ext:GridPanel ID="extGridPanel" Border="false" runat="server" TrackMouseOver="true" StoreID="Store1" StripeRows="true">
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server">
                            <Listeners>
                                <RowSelect Handler="try
                                                    {
                                                        #{hdfRecordId}.setValue(#{rowSelectionModel}.getSelected().id);
                                                        #{btnDelete}.enable();
                                                        #{btnEdit}.enable();
                                                        setVariable(#{Store1},#{DirectMethods});
                                                     }
                                                    catch(e){}" />
                            </Listeners>
                            <DirectEvents>
                                <RowDeselect OnEvent="DeselectedRow1" />
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <BottomBar>
                        <ext:PagingToolbar ID="StaticPagingToolbar" IDMode="Static" EmptyMsg="Hiện không có dữ liệu"
                            NextText="Trang sau" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                            DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                            <Items>
                                <ext:Label runat="server" Text="Số bản ghi trên 1 trang:" />
                                <ext:ToolbarSpacer runat="server" Width="10" />
                                <ext:ComboBox ID="ComboBoxPaging" Editable="false" runat="server" Width="80">
                                    <Items>
                                        <ext:ListItem Text="5" />
                                        <ext:ListItem Text="10" />
                                        <ext:ListItem Text="15" />
                                        <ext:ListItem Text="20" />
                                        <ext:ListItem Text="25" />
                                        <ext:ListItem Text="30" />
                                    </Items>
                                    <Listeners>
                                        <Select Handler="#{StaticPagingToolbar}.pageSize = parseInt(this.getValue()); #{StaticPagingToolbar}.doLoad();" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                            <Listeners>
                                <Change Handler="#{rowSelectionModel}.clearSelections();" />
                            </Listeners>
                        </ext:PagingToolbar>
                    </BottomBar>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:Button ID="btnAddRecord" runat="server" Text="Thêm mới" ToolTip="Phím tắt : F2" Icon="Add">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click">
                                            <EventMask Msg="Đang tải dữ liệu..." ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btnEdit" runat="server" Disabled="true" Text="Sửa" Icon="Pencil">
                                    <Listeners>
                                        <Click Handler="if(#{extGridPanel}.getSelectionModel().getCount() > 1) { alert('Bạn chỉ được chọn 1 dòng để sửa');return false; }" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnEdit_Click">
                                            <EventMask Msg="Đang tải dữ liệu..." ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btnDelete" Disabled="true" runat="server" Text="Xóa" Icon="Delete">
                                    <DirectEvents>
                                        <Click OnEvent="btnDelete_Click">
                                            <Confirmation Title="<%$ Resources:Language, warning%>" Message="<%$ Resources:Language, confirm_delete%>" ConfirmRequest="true" />
                                            <EventMask ShowMask="true" Msg="<%$ Resources:Language, deleting%>" />
                                        </Click>
                                    </DirectEvents>
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Chọn một hoặc nhiều dòng để xóa !" />
                                    </ToolTips>
                                </ext:Button>
                                <ext:Button ID="btnButtonTienIch" runat="server" ToolTip="Tiện ích" Text="Tiện ích" Icon="Build">
                                    <Menu>
                                        <ext:Menu ID="menuTienIch" runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="Nhân đôi dữ liệu" ID="menuCopyData" Icon="DiskMultiple">
                                                    <Listeners>
                                                        <Click Handler="return CheckSelectRow(#{hdfRecordId});" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="btnCopyData_Click">
                                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                            <Confirmation Title="Cảnh báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn nhân đôi dữ liệu !" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                                <ext:ToolbarSeparator runat="server" />
                                <ext:Button ID="btnConfig" runat="server" Text="Cấu hình" Hidden="true" Icon="Cog">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem ID="mnuShowConfigWindow" runat="server" Icon="Table" Text="Thông tin bảng">
                                                    <DirectEvents>
                                                        <Click OnEvent="mnuShowConfigWindow_Click">
                                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                            <ExtraParams>
                                                                <ext:Parameter Name="Config" Value="GridPanel" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server" ID="mnuColumnInformation" Text="Thông tin cột" Icon="ControlBlankBlue">
                                                    <DirectEvents>
                                                        <Click OnEvent="mnuColumnInformation_Click">
                                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                                <ext:ToolbarFill runat="server" />
                                <ext:TriggerField runat="server" Width="200" AllowBlank="false" IDMode="Static" BlankText="Nhập từ khóa tìm kiếm..."
                                    EmptyText="Nhập từ khóa tìm kiếm..." EnableKeyEvents="true" ID="txtSearchKey">
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressHandler" />
                                    </Listeners>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Blur Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="#{txtSearchKey}.reset();
                                                               #{StaticPagingToolbar}.pageIndex=0;
                                                               #{StaticPagingToolbar}.doLoad();
                                                               this.triggers[0].hide();" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:Button ID="btnSearch" Visible="true" runat="server" Text="Tìm kiếm" Icon="Zoom">
                                    <Listeners>
                                        <Click Handler="#{StaticPagingToolbar}.pageIndex=0;#{StaticPagingToolbar}.doLoad();#{Store1}.reload();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Listeners>
                        <AfterEdit Fn="afterEdit" />
                        <RowContextMenu Handler="e.preventDefault();
                                                    #{RowContextMenu}.dataRecord = this.store.getAt(rowIndex);
                                                    #{RowContextMenu}.showAt(e.getXY());
                                                    #{extGridPanel}.getSelectionModel().selectRow(rowIndex);" />
                    </Listeners>
                    <KeyMap>
                        <ext:KeyBinding>
                            <Keys>
                                <ext:Key Code="DELETE" />
                            </Keys>
                        </ext:KeyBinding>
                        <ext:KeyBinding>
                            <Keys>
                                <ext:Key Code="F3" />
                            </Keys>
                            <Listeners>
                                <Event Handler="#{DirectMethods}.DirectEdit();" />
                            </Listeners>
                        </ext:KeyBinding>
                    </KeyMap>
                </ext:GridPanel>
            </Center>
            <South>
                <ext:Panel runat="server" Collapsible="true" ID="plSouth" Visible="false" Border="false">
                    <Content>
                        <asp:PlaceHolder runat="server" ID="plcSouth"></asp:PlaceHolder>
                    </Content>
                </ext:Panel>
            </South>
            <North>
                <ext:Panel runat="server" Collapsible="true" ID="plNorth" Visible="false" Border="false">
                    <Content>
                        <asp:PlaceHolder runat="server" ID="plcNorth" />
                    </Content>
                </ext:Panel>
            </North>
            <East>
                <ext:Panel runat="server" ID="plEast" Collapsible="true" Visible="false" Border="false">
                    <Content>
                        <asp:PlaceHolder runat="server" ID="plcEast" />
                    </Content>
                </ext:Panel>
            </East>
            <West Split="true">
                <ext:Panel runat="server" ID="plWest" Collapsible="true" Visible="false" Border="false">
                    <Content>
                        <asp:PlaceHolder runat="server" ID="plcWest" />
                    </Content>
                </ext:Panel>
            </West>
        </ext:BorderLayout>
    </Items>
</ext:Viewport>

<ext:KeyMap runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
    <ext:KeyBinding>
        <Keys>
            <ext:Key Code="F2" />
        </Keys>
        <Listeners>
            <Event Handler="#{DirectMethods}.DirectAdd();" />
        </Listeners>
    </ext:KeyBinding>
</ext:KeyMap>

