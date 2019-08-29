<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollConfig.aspx.cs" Inherits="Web.HRM.Modules.Salary.PayrollConfig" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script src="../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        var RenderConfig = function () {
            return "<div style='width:100%;height:100%;cursor:pointer;' onclick='Ext.net.DirectMethods.SelectConfig();'>" +
                "<img  src='/Resource/icon/cog.png' >" +
                "</div>";
        };
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        };
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfConfigId" />
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel
                                runat="server"
                                ID="grdPayrollConfig"
                                TrackMouseOver="true"
                                StripeRows="true"
                                AutoExpandColumn="Description"
                                Border="false"
                                Layout="Fit">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Icon="Add" Text="Thêm">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Icon="Pencil" Text="Sửa" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Icon="Delete" Text="Xóa" Disabled="true">
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
                                            <ext:Button runat="server" ID="btnConfig" Text="Tiện ích" Icon="Cog" Disabled="True">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" Text="Nhân đôi cấu hình" Icon="DiskMultiple">
                                                                <DirectEvents>
                                                                    <Click OnEvent="DuplicateData">
                                                                        <Confirmation Message="Bạn có muốn nhân đôi cấu hình này?" Title="Thông báo" ConfirmRequest="True"></Confirmation>
                                                                        <EventMask ShowMask="True" Msg="Đang xử lý"></EventMask>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm...">
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
                                <Store>
                                    <ext:Store runat="server" ID="storePayrollConfig">
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="0" Mode="Raw" />
                                            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
                                        </AutoLoadParams>
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="objName" Value="sal_PayrollConfig" Mode="Value" />
                                            <ext:Parameter Name="keyword" Value="" Mode="Value" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Group" />
                                                    <ext:RecordField Name="Order" />
                                                    <ext:RecordField Name="IsDeleted" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                        <ext:Column ColumnID="Name" Header="Tên cấu hình" Width="300" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="500" Align="Left" DataIndex="Description" />
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="80" DataIndex="Order" Align="Center" />
                                        <ext:Column ColumnID="IsDeleted" Header="Trạng thái" Width="80" DataIndex="IsDeleted" Align="Center">
                                            <Renderer Fn="renderStatus"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="Config" Header="Cấu hình chi tiết" Align="Center">
                                            <Renderer Fn="RenderConfig" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfConfigId.setValue(RowSelectionModel1.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnConfig.enable();" />
                                            <RowDeselect Handler="hdfConfigId.reset();btnEdit.disable();btnDelete.disable();btnConfig.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server"
                                        ID="PagingToolbar1"
                                        PageSize="30"
                                        DisplayInfo="true"
                                        DisplayMsg="Từ {0} - {1} / {2}"
                                        EmptyMsg="Không có dữ liệu">
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
            <ext:Window runat="server"
                ID="wdSetting"
                Resizable="true"
                Layout="FormLayout"
                Padding="10"
                Width="500"
                Height="300"
                Hidden="true"
                Modal="true"
                Constrain="true">
                <Items>
                    <ext:Hidden runat="server" ID="hdfIsDeleted" />
                    <ext:Hidden runat="server" ID="hdfGroup" />
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên <span style='color:red;'> * </span>" AnchorHorizontal="100%" />
                    <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" Width="360" Height="100"></ext:TextArea>
                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" Width="360"></ext:NumberField>
                    <ext:ComboBox runat="server"
                        ID="cboIsDeleted"
                        FieldLabel="Trạng thái"
                        Width="360">
                        <Items>
                            <ext:ListItem Text="Kích hoạt" Value="False" />
                            <ext:ListItem Text="Khóa" Value="True" />
                        </Items>
                        <Listeners>
                            <Select Handler="hdfIsDeleted.setValue(this.getValue())"></Select>
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
