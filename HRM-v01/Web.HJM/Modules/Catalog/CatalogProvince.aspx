<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Catalog.CatalogProvince" Codebehind="CatalogProvince.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="../../Resource/js/Extcommon.js"></script>
    <script type="text/javascript">
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                Store1.reload();
            }
        }

        var CheckInput = function () {
            if (txtProvinceName.getValue().trim() == '') {
                alert("Bạn chưa nhập tên tỉnh thành phố");
                txtProvinceName.focus();
                return false;
            }
            if (cbxLocationGroupChild.getValue().trim() == '') {
                alert("Bạn chưa chọn loại địa điểm");
                return false;
            }
            if (cbxLocationGroup.getValue().trim() == '') {
                alert("Bạn chưa chọn loại địa điểm cha");
                return false;
            }
            if ($('#cbxLocationParent').prop("disabled") == false
                && (cbxLocationParent.getValue() == '' || cbxLocationParent.getValue() == null)) {
                alert("Bạn chưa chọn địa điểm cha");
                return false;
            }
            return true

        }

        var RemoveItemOnGrid = function (grid, Store) {
            Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
                if (btn == "yes") {
                    try {
                        grid.getRowEditor().stopEditing();
                    } catch (e) {

                    }
                    var s = grid.getSelectionModel().getSelections();
                    for (var i = 0, r; r = s[i]; i++) {
                        Store.remove(r);
                        Store.commitChanges();
                        Ext.net.DirectMethods.DeleteProvince(r.data.Id);
                    }
                }
            });
        }
        var DuplicateProvince = function () {
            Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn nhân đôi dữ liệu không!', function (btn) {
                if (btn == "yes") {
                    Ext.net.DirectMethods.DuplicateLocation();
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfCatalogGroupName" />
            <ext:Hidden runat="server" ID="hdfGroupItemType" />
            <!-- store location type -->
            <ext:Store runat="server" ID="storeCbxCatalogyGroup" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="hdfCatalogGroupName.getValue()" Mode="Raw" />
                    <ext:Parameter Name="itemTimeSheetHandlerType" Value="hdfGroupItemTimeSheetHandlerType.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Group" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Menu ID="RowContextMenu" runat="server">
                <Items>
                    <ext:MenuItem ID="mnuAddNew" runat="server" Icon="Add" Text="Thêm mới">
                        <Listeners>
                            <Click Handler="#{wdAddWindow}.show();" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem ID="MenuItem4" runat="server" Icon="Delete" Text="Xóa">
                        <Listeners>
                            <Click Handler="RemoveItemOnGrid(GridPanel1,Store1)" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem ID="mnuEdit" runat="server" Icon="Pencil" Text="Sửa">
                        <Listeners>
                            <Click Handler="#{btnCapNhat}.hide();#{btnSua}.show();
                               #{Button2}.hide();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnEdit_Click">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    <ext:MenuSeparator runat="server" ID="mnuDuplicate" />
                    <ext:MenuItem ID="mnuDuplicateData" runat="server" Icon="DiskMultiple" Text="Nhân đôi dữ liệu">
                        <Listeners>
                            <Click Handler="DuplicateProvince();" />
                        </Listeners>
                    </ext:MenuItem>
                </Items>
            </ext:Menu>
            <ext:Window runat="server" Resizable="false" Hidden="true" Layout="FormLayout"
                Width="500" Height="300" Modal="true" Padding="6" Constrain="true" ID="wdAddWindow" Title="Thêm mới thông tin tỉnh, thành phố"
                Icon="Add">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommand" />
                    <ext:Hidden runat="server" ID="hdfLocationGroupId" />
                    <ext:Hidden runat="server" ID="hdfLocationGroupChildId" />
                    <ext:TextField runat="server" ID="txtProvinceName" CtCls="requiredData" FieldLabel="Tên tỉnh, thành phố<span style='color:red;'>*</span>"
                        AnchorHorizontal="98%">
                    </ext:TextField>
                     <ext:ComboBox runat="server" ID="cbxLocationGroupChild" FieldLabel="Loại địa điểm"
                        DisplayField="Name" MinChars="1" ValueField="Group" AnchorHorizontal="98%" Editable="true" CtCls="requiredData"
                        LabelWidth="40" Width="200" ItemSelector="div.list-item" StoreID="storeCbxCatalogyGroup">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template2" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="list-item"> 
							            {Name}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfLocationGroupChildId.setValue(cbxLocationGroupChild.getValue()); cbxLocationParent.enable(); cbxLocationParent.clearValue();storeLocationParent.reload();
                                if(cbxLocationGroupChild.getValue() == 'Tinh' || cbxLocationGroupChild.getValue() == 'ThanhPhoTW'){cbxLocationParent.disable();hdfProvinceParentId.reset();cbxLocationParent.clearValue();}   
                             " />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfLocationGroupChildId.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cbxLocationGroup" FieldLabel="Loại địa điểm cha"
                        DisplayField="Name" MinChars="1" ValueField="Group" AnchorHorizontal="98%" Editable="true" CtCls="requiredData"
                        LabelWidth="40" Width="200" ItemSelector="div.list-item" StoreID="storeCbxCatalogyGroup">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template3" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="list-item"> 
							            {Name}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfLocationGroupId.setValue(cbxLocationGroup.getValue()); cbxLocationParent.enable(); cbxLocationParent.clearValue();storeLocationParent.reload();
                                if(hdfLocationGroupChildId.getValue() == 'Tinh' || hdfLocationGroupChildId.getValue() == 'ThanhPhoTW'){cbxLocationParent.disable();hdfProvinceParentId.reset();cbxLocationParent.clearValue();}   
                             " />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfLocationGroupId.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                     <ext:Hidden runat="server" ID="hdfProvinceParentId" />
                    <ext:ComboBox runat="server" ID="cbxLocationParent" FieldLabel="Địa điểm cha<span style='color:red;'>*</span>" Disabled="true"
                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" CtCls="requiredData"
                        EmptyText="Gõ tên để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                        LabelWidth="40" Width="200" ItemSelector="div.list-item">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template1" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="list-item"> 
							            {Name}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="storeLocationParent" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerLocation.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="group" Value="hdfLocationGroupId.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                            <ext:RecordField Name="ParentId" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfProvinceParentId.setValue(cbxLocationParent.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfProvinceParentId.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                            
                    
                    <ext:TextArea runat="server" FieldLabel="Ghi chú" ID="txtNote" AnchorHorizontal="98%"/>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnCapNhat" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return CheckInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhat_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False">
                                    </ext:Parameter>
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnSua" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="btnCapNhat_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Edit">
                                    </ext:Parameter>
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button2" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return CheckInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhat_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True">
                                    </ext:Parameter>
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button5" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdAddWindow}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="#{btnCapNhat}.show(); #{btnSua}.hide();
                               #{Button2}.show();  hdfCommand.setValue('');
                               #{txtProvinceName}.reset(); #{hdfProvinceParentId}.reset();#{hdfLocationGroupId}.reset();cbxLocationGroup.clearValue();cbxLocationParent.clearValue();
                               #{txtNote}.reset();Ext.net.DirectMethods.ResetWindowTitle();" />
                </Listeners>
            </ext:Window>
            <ext:Store ID="Store1" AutoLoad="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerLocation.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtSearch.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="GridPanel1" Border="false" runat="server" StoreID="Store1" StripeRows="true"
                                Icon="ApplicationViewColumns" TrackMouseOver="true" AutoExpandColumn="Description"
                                AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnAddNew" runat="server" Text="Thêm mới" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="#{wdAddWindow}.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="btnEdit" runat="server" Disabled="true" Text="Sửa" Icon="Pencil">
                                                <Listeners>
                                                    <Click Handler="return CheckSelectedRows(GridPanel1);#{btnCapNhat}.hide();#{btnSua}.show();#{Button2}.hide();" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnEdit_Click">
                                                        <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnDelete" runat="server" Text="Xóa" Disabled="true" Icon="Delete">
                                                <Listeners>
                                                    <Click Handler="RemoveItemOnGrid(GridPanel1,Store1)" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" />
                                            <ext:Button ID="Button1" runat="server" Text="Tiện ích" Icon="Build">
                                                <Menu>
                                                    <ext:Menu ID="Menu4" runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" Disabled="true" ID="mnuNhanDoiDuLieu" Text="Nhân đôi dữ liệu"
                                                                Icon="DiskMultiple">
                                                                <Listeners>
                                                                    <Click Handler="DuplicateProvince();" />
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarFill runat="server" ID="tbfill" />
                                            <ext:TextField runat="server" Width="200" EnableKeyEvents="true" EmptyText="Nhập từ khóa tìm kiếm"
                                                EmptyTex="Nhập từ khóa tìm kiếm" ID="txtSearch">
                                                <Listeners>
                                                    <KeyPress Fn="enterKeyPressHandler" />
                                                </Listeners>
                                            </ext:TextField>
                                            <ext:Button Text="Tìm kiếm" Icon="Zoom" runat="server" ID="btnSearch">
                                                <Listeners>
                                                    <Click Handler="#{Store1}.reload();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Locked="true" Width="35" Header="STT" />
                                        <ext:Column ColumnID="ProvinceName" Header="Tên tỉnh thành phố" Width="200" Sortable="true"
                                            DataIndex="Name">
                                        </ext:Column>
          
                                        <ext:Column ColumnID="Description" Header="Ghi chú" Width="100" Sortable="true"
                                            DataIndex="Description">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="checkboxSelection">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(checkboxSelection.getSelected().id);btnEdit.enable();btnDelete.enable();mnuNhanDoiDuLieu.enable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                        PageSize="30" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                        DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên 1 trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBoxPaging" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="70" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:PagingToolbar>
                                </BottomBar>
                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                <Listeners>
                                    <RowContextMenu Handler="e.preventDefault(); #{RowContextMenu}.dataRecord = this.store.getAt(rowIndex);#{RowContextMenu}.showAt(e.getXY());#{GridPanel1}.getSelectionModel().selectRow(rowIndex);" />
                                    <DblClick Handler="#{btnCapNhat}.hide();#{btnSua}.show();
                               #{Button2}.hide();" />
                                </Listeners>
                                <DirectEvents>
                                    <DblClick OnEvent="btnEdit_Click">
                                        <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                    </DblClick>
                                </DirectEvents>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
