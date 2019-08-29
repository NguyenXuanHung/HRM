<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Catalog.CatalogHoliday" Codebehind="CatalogHoliday.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="../../../Resource/js/RenderJS.js" type="text/javascript"></script>

    <script type="text/javascript">
        var keyPresstxtSearch = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                PagingToolbar1.doLoad();
                RowSelectionModel1.clearSelections();
                if (this.getValue() == '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() != '') {
                this.triggers[0].show();
            }
        }

        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }

        var RenderSymbol = function (value, p, record) {
            return "<b style='color:blue;'>" + value + "</b>";
        }

        var checkInputHoliday = function () {
            if (txtHolidayName.getValue() == '' || txtHolidayName.getValue().trim == '') {
                alert('Bạn chưa nhập tên ngày lễ!');
                return false;
            }
            if (hdfGroupHoliday.getValue() == '' || hdfGroupHoliday.getValue().trim == '') {
                alert('Bạn chưa chọn loại ngày lễ!');
                return false;
            }
            if (txtDay.getValue() == null) {
                alert('Bạn chưa nhập ngày!');
                return false;
            }
            if (txtMonth.getValue() == null) {
                alert('Bạn chưa nhập tháng!');
                return false;
            }
            if (txtYear.getValue() == null) {
                alert('Bạn chưa nhập năm!');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridHoliday" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdHoliday.show();wdHoliday.setTitle('Tạo mới ngày lễ tết');btnUpdate.hide();btnUpdateNew.show();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditHoliday_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa thông tin này" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeTimeSheetSymbol" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="Holiday" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="DayMonth" />
                                                    <ext:RecordField Name="Day" />
                                                    <ext:RecordField Name="Month" />
                                                    <ext:RecordField Name="GroupHolidayName" />
                                                    <ext:RecordField Name="Group" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên ngày lễ" Width="200" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:Column ColumnID="DayMonth" Header="Ngày tháng" Width="200" DataIndex="DayMonth" />
                                        <ext:Column ColumnID="GroupHolidayName" Header="Loại ngày lễ" Width="100" DataIndex="GroupHolidayName" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfRecordId.reset();hdfKeyRecord.reset(); " />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="25">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="25" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server" Title="Tạo mới ngày lễ tết" Resizable="true" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="UserTick" ID="wdHoliday"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:TextField runat="server" ID="txtHolidayName" CtCls="requiredData" FieldLabel="Tên ngày lễ<span style='color:red;'>*</span>"
                        AnchorHorizontal="100%" />
                    <ext:Hidden runat="server" ID="hdfGroupHoliday" />
                    <ext:ComboBox runat="server" ID="cbxGroupHoliday" FieldLabel="Loại ngày lễ"
                        DisplayField="Name" MinChars="1" ValueField="Group" AnchorHorizontal="100%" Editable="true"
                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template16" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="list-item"> 
							            {Name}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="storeGroupHoliday" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="objname" Value="cat_GroupEnum" Mode="Value" />
                                    <ext:Parameter Name="itemTimeSheetHandlerType" Value="GroupHolidayTimeSheetHandlerType" Mode="Value" />
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
                        </Store>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfGroupHoliday.setValue(cbxGroupHoliday.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupHoliday.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:NumberField runat="server" ID="txtDay" FieldLabel="Ngày" AnchorHorizontal="100%" CtCls="requiredData"/>
                    <ext:NumberField runat="server" ID="txtMonth" FieldLabel="Tháng" AnchorHorizontal="100%" CtCls="requiredData"/>
                    <ext:NumberField runat="server" ID="txtYear" FieldLabel="Năm" AnchorHorizontal="100%" CtCls="requiredData"/>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputHoliday();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputHoliday();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdateClose" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputHoliday();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLai" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdHoliday.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
