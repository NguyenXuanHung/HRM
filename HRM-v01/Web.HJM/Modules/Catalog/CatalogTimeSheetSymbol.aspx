<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Catalog.CatalogTimeSheetSymbol" Codebehind="CatalogTimeSheetSymbol.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="/Resource/js/RenderJS.js" type="text/javascript"></script>

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

        var checkInputTimeSheetSymbol = function () {
            if (txtSymbolCode.getValue() == '' || txtSymbolCode.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu chấm công!');
                return false;
            }
            if (txtSymbolName.getValue() == '' || txtSymbolName.getValue().trim == '') {
                alert('Bạn chưa nhập diễn giải!');
                return false;
            }
            if (hdfGroupSymbol.getValue() == '' || hdfGroupSymbol.getValue().trim == '') {
                alert('Bạn chưa chọn nhóm ký hiệu chấm công!');
                return false;
            }
            return true;
        }

        var RenderFormat = function () {
            var value = txtMoneyOfDay.getValue();
            if (value == null || value.length === 0)
                return "";
            value = Math.round(value);
            var l = (value + "").length;
            var s = value + "";
            var rs = "";
            var count = 0;
            for (var i = l - 1; i > 0; i--) {
                count++;
                if (count === 3) {
                    rs = "," + s.charAt(i) + rs;
                    count = 0;
                }
                else {
                    rs = s.charAt(i) + rs;
                }
            }
            rs = s.charAt(0) + rs;
            if (rs.replace(",", "").trim() * 1 === 0) {
                return "";
            }
            txtMoneyOfDay.setValue(rs);
            console.log(rs);
            if (rs === 'NaN' || rs === '') {
                rs = "";
            }
            return "<span >" + txtMoneyOfDay.setValue(rs) + "</span>";
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
                            <ext:GridPanel ID="gridTimeSheetSymbol" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTimeSheetSymbol.show();wdTimeSheetSymbol.setTitle('Tạo mới ký hiệu chấm công');btnUpdate.hide();btnUpdateNew.show();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditTimeSheetSymbol_Click">
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
                                    <ext:Store ID="storeTimeSheetSymbol" AutoSave="true" runat="server" GroupField="GroupSymbolName">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="GroupSymbolName" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                    <ext:RecordField Name="IsInUsed" />
                                                    <ext:RecordField Name="Order" />
                                                    <ext:RecordField Name="Group" />
                                                    <ext:RecordField Name="WorkConvert" />
                                                    <ext:RecordField Name="MoneyConvert" />
                                                    <ext:RecordField Name="SymbolDisplay"/>
                                                    <ext:RecordField Name="TimeConvert"/>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Code" Header="Ký hiệu" Width="150" Align="Left" Locked="true" DataIndex="Code">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SymbolDisplay" Header="Ký hiệu hiển thị báo cáo" Width="150" Align="Left" Locked="true" DataIndex="SymbolDisplay">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Name" Header="Diễn giải" Width="200" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Ghi chú" Width="200" DataIndex="Description" />
                                        <ext:Column ColumnID="Order" Header="Số thứ tự" Width="80" DataIndex="Order" />
                                        <ext:Column ColumnID="WorkConvert" Header="Số công quy đổi" Width="170" DataIndex="WorkConvert" />
                                        <ext:Column ColumnID="MoneyConvert" Header="Số tiền quy đổi" Width="170" DataIndex="MoneyConvert" >
                                            <Renderer Fn="RenderAllowMinusVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="170" DataIndex="TimeConvert" />
                                        <ext:Column ColumnID="IsInUsed" Width="100" Header="Đang sử dụng" Align="Center"
                                            DataIndex="IsInUsed">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="150" DataIndex="CreatedBy" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Ngày tạo" Width="150" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
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
            <ext:Window runat="server" Title="Tạo mới ký hiệu chấm công" Resizable="true" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="UserTick" ID="wdTimeSheetSymbol"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:TextField runat="server" ID="txtSymbolCode" CtCls="requiredData" FieldLabel="Ký hiệu<span style='color:red;'>*</span>"
                        AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtSymbolDisplay" CtCls="requiredData" FieldLabel="Ký hiệu hiển thị<span style='color:red;'>*</span>"
                        AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtSymbolName" CtCls="requiredData" FieldLabel="Diễn giải<span style='color:red;'>*</span>"
                        AnchorHorizontal="100%" />
                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Số thứ tự" AnchorHorizontal="100%" />
                    <ext:Hidden runat="server" ID="hdfGroupSymbol" />
                    <ext:ComboBox runat="server" ID="cbxGroupSymbol" FieldLabel="Nhóm ký hiệu"
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
                            <ext:Store runat="server" ID="storeGroupSymbol" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={20}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="GroupSymbolType" />
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
                            <Select Handler="this.triggers[0].show();hdfGroupSymbol.setValue(cbxGroupSymbol.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupSymbol.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:RadioGroup runat="server" ID="groupRadioSelectWork" FieldLabel="Loại công" ColumnsNumber="2">
                        <Items>
                            <ext:Radio runat="server" ID="chkAddWork" BoxLabel="Cộng" AnchorHorizontal="100%" Checked="True"/>
                            <ext:Radio runat="server" ID="chkMinusWork" BoxLabel="Trừ" AnchorHorizontal="100%"/>
                        </Items>
                    </ext:RadioGroup>
                    <ext:TextField runat="server" ID="txtNumberOfDay" FieldLabel="Số công quy đổi"
                        AnchorHorizontal="100%" MaskRe="/[0-9,]/" MaxLength="5" />
                    <ext:TextField runat="server" ID="txtMoneyOfDay" FieldLabel="Số tiền quy đổi"
                        AnchorHorizontal="100%" MaskRe="/[0-9]/" MaxLength="20" >
                        <Listeners>
                            <Blur Fn="RenderFormat" ></Blur>
                        </Listeners>
                    </ext:TextField>
                    <ext:TextField runat="server" ID="txtTimeConvert" FieldLabel="Thời gian quy đổi"
                                   AnchorHorizontal="100%" MaskRe="/[0-9,]/" MaxLength="5" />
                    <ext:TextArea runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%" ID="txtNote" />
                    <ext:Checkbox runat="server" FieldLabel="Trạng thái" BoxLabel="Đang được sử dụng" ID="chk_IsInUsed">
                    </ext:Checkbox>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetSymbol();" />
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
                            <Click Handler="return checkInputTimeSheetSymbol();" />
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
                            <Click Handler="return checkInputTimeSheetSymbol();" />
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
                            <Click Handler="wdTimeSheetSymbol.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>



