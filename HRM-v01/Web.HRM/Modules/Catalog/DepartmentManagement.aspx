<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Catalog.DepartmentManagement" Codebehind="DepartmentManagement.aspx.cs" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                tgDonVi.getRootNode().reload();
            }
        }
        function nodeLoad(node) {
            Ext.net.DirectMethods.NodeLoad(node.id, {
                success: function (result) {
                    var data = eval("(" + result + ")");
                    node.loadNodes(data);
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });
        }
        var RemoveItemOnGrid = function (grid) {
            Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
                if (btn == "yes") {
                    if (hdfRecordId.getValue() == "") {
                        alert('Bạn chưa chọn bản ghi cần xóa');
                        return false;
                    }
                    try {
                        grid.getRowEditor().stopEditing();
                    }
                    catch (e) {

                    }
                    var id = hdfRecordId.getValue().toString();
                    Ext.net.DirectMethods.DeleteNode(id);

                    try {
                        btnEdit.disable();
                        btnDelete.disable();
                    }
                    catch (e) { }
                    return true;
                }
            });
        }
        var CheckInputDonVi = function () {
            if (txtName.getValue().trim() == '') {
                alert('Bạn chưa nhập tên đơn vị');
                txtName.focus();
                return false;
            }         

            //if (cbxParent.getValue() == '') {
            //    alert('Bạn chưa chọn đơn vị cấp cha');
            //    cbxParent.focus();
            //    return false;
            //}
            return true;
        }

        var BeforeAddNew = function () {
            if (hdfRecordId.getValue() != '') {
                cbxParent.setValue(hdfRecordId.getValue());
            }
            btnSave.show();
            btnUpdate.hide();
            btnSaveAndClose.show();
        }
    </script>
</head>
<body>
    <form id="frmDepartmentManagement" runat="server">
        <div id="">
            <ext:ResourceManager runat="server" ID="RM" />
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfNodeOrder" />
            <ext:Hidden runat="server" ID="hdfIsChange" Text="False" />

            <ext:Menu ID="mnuDonVi" runat="server">
                <Items>
                    <ext:MenuItem ID="mnuAdd" runat="server" Icon="Add" Text="Thêm mới">
                        <Listeners>
                            <Click Handler="BeforeAddNew(); #{wdAddDepartment}.show();" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem ID="mnuEdit" runat="server" Icon="Pencil" Text="Sửa">
                        <DirectEvents>
                            <Click OnEvent="InitWindowDepartment">
                                <EventMask ShowMask="true" />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    <ext:MenuItem ID="MnuDelete" runat="server" Icon="Delete" Text="Xóa">
                        <DirectEvents>
                            <Click OnEvent="Delete" After="#{tgDonVi}.getRootNode().reload();">
                                <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa không ?" ConfirmRequest="true" />
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                </Items>
            </ext:Menu>
            <ext:Window runat="server" ID="wdAddDepartment" Icon="Add" Padding="6" Title="Thêm mới đơn vị"
                Layout="FormLayout" Width="600" AutoHeight="true" Hidden="true" Resizable="false"
                Constrain="true" Modal="true" LabelWidth="105">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandName" />
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" AnchorHorizontal="99%" MaxLength="256" FieldLabel="Tên đơn vị<span style='color:red;'>*</span>" />
                    <ext:TextField runat="server" ID="txtShortName" AnchorHorizontal="99%" MaxLength="64" FieldLabel="Tên viết tắt" />
                    <ext:TextField runat="server" ID="txtAddress" AnchorHorizontal="99%" FieldLabel="Địa chỉ" />
                    <ext:TextField runat="server" ID="txtTelephone" AnchorHorizontal="99%" FieldLabel="Điện thoại" />
                    <ext:TextField runat="server" ID="txtFax" AnchorHorizontal="99%" FieldLabel="Fax" />
                    <ext:TextField runat="server" ID="txtTaxCode" AnchorHorizontal="99%" FieldLabel="Mã số thuế" />
                    <ext:TextField runat="server" ID="txtOrder" AnchorHorizontal="99%" FieldLabel="Thứ tự" />
                    <ext:ComboBox runat="server" ID="cbxParent" FieldLabel="Đơn vị cấp cha<span style='color:red;'>*</span>"
                        DisplayField="Name" ValueField="Id" AnchorHorizontal="99%" ItemSelector="div.list-item" Editable="false" AllowBlank="false"
                        EmptyText="Phải chọn đơn vị cấp cha khi tạo đơn vị con">
                        <Template ID="Template3" runat="server">
                            <Html>
                                <tpl for=".">
                                <div class="list-item"> 
                                    {Name}
                                </div>
                            </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="cbxParent_Store" AutoLoad="False" OnRefreshData="cbxParent_Store_OnRefreshData">
                                <Reader>
                                    <ext:JsonReader IDProperty="Id">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Expand Handler="cbxParent_Store.reload();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" AutoHeight="true">
                        <Items>
                            <ext:Container runat="server" ColumnWidth="0.5" Layout="FormLayout" Height="27" LabelWidth="105">
                                <Items>
                                    <ext:Checkbox runat="server" ID="chkIsPrimary" BoxLabel="Đơn vị quản lý" AnchorHorizontal="100%" />
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ColumnWidth="0.5" Layout="FormLayout" Height="27">
                                <Items>
                                    <ext:Checkbox runat="server" ID="chkIsLocked" BoxLabel="Đơn vị bị khóa" AnchorHorizontal="100%" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Icon="Disk" Text="Cập nhật">
                        <Listeners>
                            <Click Handler="return CheckInputDonVi();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" />
                                <ExtraParams>
                                    <ext:Parameter Name="Closed" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnSaveAndClose" Icon="Disk" Text="Cập nhật và đóng lại">
                        <Listeners>
                            <Click Handler="return CheckInputDonVi();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Icon="Decline" Text="Đóng lại">
                        <Listeners>
                            <Click Handler="#{wdAddDepartment}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetWindowTitle();#{tgDonVi}.getRootNode().reload();" />
                </Listeners>
            </ext:Window>
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:TreeGrid ID="tgDonVi" runat="server" StripeRows="true" TrackMouseOver="true"
                                AnchorHorizontal="100%" NoLeafIcon="false" AutoExpandColumn="Name" AutoExpandMin="150"
                                EnableDD="false" Border="false" Animate="true" ContainerScroll="true" UseArrows="true"
                                EnableSort="false">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnAdd" runat="server" Text="Thêm mới" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="#{wdAddDepartment}.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="btnEdit" runat="server" Disabled="true" Text="Sửa" Icon="Pencil">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowDepartment">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnDelete" runat="server" Text="Xóa" Disabled="true" Icon="Delete">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete" After="#{tgDonVi}.getRootNode().reload();">
                                                        <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa không ?" ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server" />
                                            <ext:Button runat="server" Text="Tải lại dữ liệu" Icon="Reload">
                                                <Listeners>
                                                    <Click Handler="#{tgDonVi}.getRootNode().reload();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarFill runat="server" ID="tbfill" />
                                            <ext:TextField runat="server" Width="200" EnableKeyEvents="true" Text="" ID="txtSearch"
                                                Hidden="true">
                                                <Listeners>
                                                    <KeyPress Fn="enterKeyPressHandler" />
                                                </Listeners>
                                            </ext:TextField>
                                            <ext:Button Text="Tìm kiếm" Icon="Zoom" runat="server" ID="btnSearch" Hidden="true">
                                                <Listeners>
                                                    <Click Handler="#{tgDonVi}.getRootNode().reload();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Columns>
                                    <ext:TreeGridColumn Header="Tên đơn vị" Width="150" DataIndex="Name" />
                                    <ext:TreeGridColumn Header="Tên viết tắt" Width="150" DataIndex="ShortName" />
                                    <ext:TreeGridColumn Header="Thứ tự" Width="120" DataIndex="Order" />
                                    <ext:TreeGridColumn Header="Là đơn vị quản lý" Width="110" DataIndex="IsPrimary" />
                                    <ext:TreeGridColumn Header="Không nhập hồ sơ" Width="110" DataIndex="IsLocked" />
                                </Columns>
                                <Root>
                                    <ext:AsyncTreeNode NodeID="0" Text="Root" />
                                </Root>
                                <SelectionModel>
                                    <ext:DefaultSelectionModel runat="server" ID="DefaultSelectionModel1">
                                        <Listeners>
                                            <SelectionChange Handler="#{hdfRecordId}.setValue(node.id);#{btnEdit}.enable();#{btnDelete}.enable();" />
                                        </Listeners>
                                    </ext:DefaultSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                    <BeforeLoad Fn="nodeLoad" />
                                    <ContextMenu Handler="e.preventDefault();#{mnuDonVi}.node = node; node.select();#{mnuDonVi}.showAt(e.getXY());" />
                                </Listeners>
                                <DirectEvents>
                                    <DblClick OnEvent="InitWindowDepartment">
                                        <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                    </DblClick>
                                </DirectEvents>
                            </ext:TreeGrid>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
