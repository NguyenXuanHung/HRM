﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetRuleWrongTimeManagement" CodeBehind="TimeSheetRuleWrongTimeManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">
        var validateForm = function () {
            if (!txtFromMinute.getValue()) {
                alert('Bạn chưa nhập thời gian từ!');
                return false;
            }
            if (!txtToMinute.getValue()) {
                alert('Bạn chưa nhập thời gian đến!');
                return false;
            }
            if (hdfSymbolId.getValue() == '' || hdfSymbolId.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu hiển thị trên bảng công!');
                return false;
            }
            if (!cbxType.getValue()) {
                alert('Bạn chưa chọn loại cấu hình!');
                return false;
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server"></ext:ResourceManager>
            
            <!-- hidden -->
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfType" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfGroupSymbolId" />
            
            <!-- user control -->
            <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            
            <!-- store -->
            <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/TimeSheet/HandlerTimeSheetRuleWrongTime.ashx" />
                </Proxy>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="SymbolId" />
                            <ext:RecordField Name="SymbolName" />
                            <ext:RecordField Name="SymbolColor" />
                            <ext:RecordField Name="FromMinute" />
                            <ext:RecordField Name="ToMinute" />
                            <ext:RecordField Name="WorkConvert" />
                            <ext:RecordField Name="TimeConvert" />
                            <ext:RecordField Name="Type" />
                            <ext:RecordField Name="TypeName" />
                            <ext:RecordField Name="Order" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeSymbol" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
                    <ext:Parameter Name="GroupSymbolTypeId" Value="hdfGroupSymbolId.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            
            <ext:Store ID="storeType" runat="server" AutoLoad="False" OnRefreshData="storeType_OnRefreshData">
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
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gpTimeSheetRuleEarlyOrLate" StoreID="storeTimeSheetRule" TrackMouseOver="true" Header="false" runat="server"
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
                                                <Listeners>
                                                    <Click Handler="console.log(rowSelectionModel.getSelected().get('Id'));return true;"></Click>
                                                </Listeners>
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
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>                                
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FromMinute" Header="Thời gian từ" Width="170" DataIndex="FromMinute" />
                                        <ext:Column ColumnID="ToMinute" Header="Thời gian đến" Width="150" DataIndex="ToMinute" />
                                        <ext:Column ColumnID="WorkConvert" Header="Công quy đổi" Width="150" Align="Center" DataIndex="WorkConvert">
                                        </ext:Column>
                                        <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="150" Align="Center" DataIndex="TimeConvert" />
                                        <ext:Column ColumnID="SymbolName" Header="Ký hiệu hiển thị trên bảng công" Width="220" Align="Center" DataIndex="SymbolName">
                                            <Renderer Fn="RenderSymbol" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TypeName" Width="100" Header="Loại cấu hình" Align="Center" DataIndex="TypeName">
                                        </ext:Column>
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="150" Align="Center" DataIndex="Order" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="20" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
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
                                                <SelectedItem Value="20" />
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
            
            
            <!-- wdsetting -->
            <ext:Window runat="server" Title="Tạo mới cấu hình đi muộn, về sớm" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdSetting"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:NumberField runat="server" FieldLabel="Thời gian từ<span style='color:red;'>*</span>" ID="txtFromMinute"
                        AnchorHorizontal="98%" TabIndex="1" />
                    <ext:NumberField runat="server" FieldLabel="Thời gian đến<span style='color:red;'>*</span>" ID="txtToMinute"
                                     AnchorHorizontal="98%" TabIndex="2" />
                    <ext:Hidden runat="server" ID="hdfSymbolId" />
                    <ext:ComboBox runat="server" ID="cbxSymbol" StoreID="storeSymbol" FieldLabel="Ký hiệu hiển thị<span style='color:red;'>*</span>" DisplayField="Code" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" PageSize="10"
                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                        
                        <Template ID="Template2" runat="server">
                            <Html>
                                <tpl for=".">
                                    <div class="list-item"> 
                                        {Code}
                                    </div>
                                </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Expand Handler="if(#{cbxSymbol}.store.getCount()==0){#{storeSymbol}.reload();}"></Expand>
                            <Select Handler="hdfSymbolId.setValue(cbxSymbol.getValue());"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cbxType" StoreID="storeType" FieldLabel="Loại cấu hình" DisplayField="Name" ValueField="Id" Width="653">
                        <Listeners>
                            <Expand Handler="if(#{cbxType}.store.getCount()==0){#{storeType}.reload();}" />
                            <Select Handler="hdfType.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>

                    <ext:TextField runat="server" ID="txtWorkConvert" FieldLabel="Công quy đổi" TabIndex="3"
                        AnchorHorizontal="98%" MaskRe="/[0-9.]/" />
                    <ext:NumberField runat="server" FieldLabel="Thời gian quy đổi" ID="txtTimeConvert"
                        AnchorHorizontal="98%" TabIndex="4" />
                    <ext:NumberField runat="server" FieldLabel="Thứ tự" ID="txtOrder"
                        AnchorHorizontal="98%" TabIndex="6" />
                    <ext:Checkbox runat="server" ID="chkIsMinus" FieldLabel="Trừ công" Checked="True" />
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
