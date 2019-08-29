<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.TimeSheetRuleEarlyOrLateManagement" Codebehind="TimeSheetRuleEarlyOrLateManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
    <script src="../../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <script type="text/javascript">
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
        var checkInputTimeSheetRule = function () {
            if (!txtFromMinute.getValue()) {
                alert('Bạn chưa nhập thời gian từ!');
                return false;
            }
            if (!txtToMinute.getValue()) {
                alert('Bạn chưa nhập thời gian đến!');
                return false;
            }
            if (!txtWorkConvert.getValue() && !txtMoneyConvert.getValue()) {
                alert('Công quy đổi và tiền quy đổi không được đồng thời để trống!');
                return false;
            }
            if (!cbxType.getValue()) {
                alert('Bạn chưa chọn loại cấu hình!');
                return false;
            }
            if (txtSymbol.getValue() == '' || txtSymbol.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu hiển thị trên bảng công!');
                return false;
            }
            if (txtSymbolDisplay.getValue() == '' || txtSymbolDisplay.getValue().trim == '') {
                alert('Bạn chưa nhập diễn giải ký hiệu hiển thị trên bảng công!');
                return false;
            }
            return true;
        }
        var RenderTime = function (value, p, record) {
            try {
                if (value == null) return "";
                var timeStr = value.substring(0, 2) + ":" + value.substring(2, 4);
                if (timeStr != "") {
                    return timeStr;
                }
            } catch (e) {

            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTimeSheetRuleEarlyOrLate" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTimeSheetRule.show();wdTimeSheetRule.setTitle('Tạo mới cấu hình đi muộn, về sớm');btnUpdate.hide();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditTimeSheetRule_Click">
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
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetRuleLateOrEarly" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="FromTime" />
                                                    <ext:RecordField Name="ToTime" />
                                                    <ext:RecordField Name="WorkConvert" />
                                                    <ext:RecordField Name="MoneyConvert" />
                                                    <ext:RecordField Name="Type" />
                                                    <ext:RecordField Name="Order"/>
                                                    <ext:RecordField Name="TimeConvert"/>
                                                    <ext:RecordField Name="Symbol"/>
                                                    <ext:RecordField Name="SymbolDisplay"/>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FromTime" Header="Thời gian từ" Width="170" DataIndex="FromTime"/>
                                        <ext:Column ColumnID="ToTime" Header="Thời gian đến" Width="150" DataIndex="ToTime"/>
                                        <ext:Column ColumnID="WorkConvert" Header="Công quy đổi" Width="150" Align="Center" DataIndex="WorkConvert"  >
                                        </ext:Column>
                                        <ext:Column ColumnID="MoneyConvert" Header="Tiền quy đổi" Width="150" DataIndex="MoneyConvert" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="150" Align="Center" DataIndex="TimeConvert" />  <ext:Column ColumnID="Symbol" Header="Ký hiệu hiển thị trên bảng công" Width="220" Align="Center" DataIndex="Symbol" />  
                                        <ext:Column ColumnID="SymbolDisplay" Header="Diễn giải" Width="150" Align="Center" DataIndex="SymbolDisplay" /> 
                                        <ext:Column ColumnID="Type" Width="100" Header="Loại cấu hình" Align="Center" DataIndex="Type">
                                            <Renderer Fn="GetTimeSheetEarlyOrLate" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="150" Align="Center" DataIndex="Order" />
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
                                            <RowSelect Handler="hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfKeyRecord.reset(); " />
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
            <ext:Window runat="server" Title="Tạo mới cấu hình đi muộn, về sớm" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdTimeSheetRule"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container47" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="120"
                                ColumnWidth="0.5">
                                <Items>
                                    <ext:NumberField runat="server" FieldLabel="Từ<span style='color:red;'>*</span>" ID="txtFromMinute"
                                        AnchorHorizontal="98%" TabIndex="1" />
                                    <ext:RadioGroup runat="server" ID="groupRadioSelectWork" FieldLabel="Loại công" ColumnsNumber="2">
                                        <Items>
                                            <ext:Radio runat="server" ID="chkAddWork" BoxLabel="Cộng" AnchorHorizontal="100%" Checked="True"/>
                                            <ext:Radio runat="server" ID="chkMinusWork" BoxLabel="Trừ" AnchorHorizontal="100%"/>
                                        </Items>
                                    </ext:RadioGroup>
                                    <ext:TextField runat="server" ID="txtWorkConvert" FieldLabel="Công quy đổi" TabIndex="3"
                                        AnchorHorizontal="98%" MaskRe="/[0-9.]/"/>
                                    <ext:ComboBox runat="server" ID="cbxType" FieldLabel="Loại cấu hình" AnchorHorizontal="98%"
                                        CtCls="requiredData">
                                        <Items>
                                            <ext:ListItem Text="Đi muộn" Value="0" />
                                            <ext:ListItem Text="Về sớm" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtSymbol" CtCls="requiredData" FieldLabel="Ký hiệu hiển thị trên bảng công<span style='color:red;'>*</span>" AnchorHorizontal="98%" MaxLength="10"/>
                                    <ext:TextField runat="server" ID="txtSymbolDisplay" CtCls="requiredData" FieldLabel="Diễn giải<span style='color:red;'>*</span>" AnchorHorizontal="98%"/>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container48" runat="server" LabelAlign="left" Layout="FormLayout"
                                LabelWidth="100" ColumnWidth="0.5">
                                <Items>

                                    <ext:NumberField runat="server" FieldLabel="Đến<span style='color:red;'>*</span>" ID="txtToMinute"
                                        AnchorHorizontal="98%" TabIndex="2" />
                                    <ext:NumberField runat="server" FieldLabel="Tiền quy đổi" ID="txtMoneyConvert"
                                        AnchorHorizontal="98%" TabIndex="4" />
                                    <ext:NumberField runat="server" FieldLabel="Thời gian quy đổi" ID="txtTimeConvert"
                                                     AnchorHorizontal="98%" TabIndex="4" />
                                    <ext:NumberField runat="server" FieldLabel="Thứ tự" ID="txtOrder"
                                                     AnchorHorizontal="98%" TabIndex="6" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetRule();" />
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
                            <Click Handler="return checkInputTimeSheetRule();" />
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
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetRule.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
