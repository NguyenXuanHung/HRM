<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.TimeSheetWorkShiftManagement" Codebehind="TimeSheetWorkShiftManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
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

        var checkInputTimeSheetRule = function () {
            if (txtName.getValue() == '' || txtName.getValue().trim == '') {
                alert('Bạn chưa nhập tên ca!');
                return false;
            }

            if (!txtWorkConvert.getValue()) {
                alert('Bạn chưa nhập công quy đổi!');
                return false;
            }
            if (!txtTimeConvert.getValue()) {
                alert('Bạn chưa nhập thời gian quy đổi!');
                return false;
            }
            if (hdfSymbolId.getValue() == '' || hdfSymbolId.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu hiển thị trên bảng công!');
                return false;
            }
            if (txtSymbolDisplay.getValue() == '' || txtSymbolDisplay.getValue().trim == '') {
                alert('Bạn chưa nhập diễn giải ký hiệu hiển thị trên bảng công!');
                return false;
            }

            if (!cbxGroupWorkShift.getValue()) {
                alert('Bạn chưa chọn nhóm phân ca !');
                return false;
            }

            if (!inTime.getValue()) {
                alert('Bạn chưa nhập giờ bắt đầu làm việc !');
                return false;
            }
            if (!outTime.getValue()) {
                alert('Bạn chưa nhập giờ kết thúc làm việc !');
                return false;
            }

            if (!startInTime.getValue()) {
                alert('Bạn chưa nhập thời gian bắt đầu vào hiểu ca !');
                return false;
            }
            if (!endInTime.getValue()) {
                alert('Bạn chưa nhập thời gian kết thúc vào hiểu ca!');
                return false;
            }
            if (!startOutTime.getValue()) {
                alert('Bạn chưa nhập thời gian bắt đầu ra hiểu ca!');
                return false;
            }
            if (!endOutTime.getValue()) {
                alert('Bạn chưa nhập thời gian kết thúc ra hiểu ca!');
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

        var RenderGroupSymbol = function (value) {
            return "<b style='color:blue;'>" + value + "</b>";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
        
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
            <ext:Store runat="server" ID="storeSymbol" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Symbol" />
                    <ext:Parameter Name="GroupSymbolType" Value="hdfGroupSymbol.getValue()" Mode="Raw" />
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

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTime" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTimeSheetRule.show();wdTimeSheetRule.setTitle('Tạo mới chi tiết bảng phân ca');btnUpdate.hide();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();timeSheetFromDate.enable(); timeSheetToDate.enable();" />
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

                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập tên ca">
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
                                    <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server" GroupField="GroupWorkShiftName">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="WorkShift" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="SpecifyDate" />
                                                    <ext:RecordField Name="StartInTime" />
                                                    <ext:RecordField Name="EndInTime" />
                                                    <ext:RecordField Name="StartOutTime" />
                                                    <ext:RecordField Name="EndOutTime" />
                                                    <ext:RecordField Name="InTime" />
                                                    <ext:RecordField Name="OutTime" />
                                                    <ext:RecordField Name="GroupWorkShiftId" />
                                                    <ext:RecordField Name="GroupWorkShiftName" />
                                                    <ext:RecordField Name="WorkConvert" />
                                                    <ext:RecordField Name="TimeConvert" />
                                                    <ext:RecordField Name="Symbol" />
                                                    <ext:RecordField Name="SymbolDisplay" />
                                                    <ext:RecordField Name="StyleColor" />
                                                    <ext:RecordField Name="IsOverTime" />
                                                    <ext:RecordField Name="FactorOverTime" />
                                                    <ext:RecordField Name="IsHasInOutTime"/>
                                                    <ext:RecordField Name="SymbolId"/>
                                                    <ext:RecordField Name="GroupSymbolType"/>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView2" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên ca" Width="200" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:Column ColumnID="Code" Header="Ký hiệu" Width="100" Align="Left" Locked="true" DataIndex="Code" Hidden="True" />
                                        <ext:DateColumn ColumnID="SpecifyDate" Header="Ngày" Width="100" DataIndex="SpecifyDate" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="InTime" Header="Giờ bắt đầu làm việc" Width="150" DataIndex="InTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="OutTime" Header="Giờ kết thúc làm việc" Width="150" DataIndex="OutTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="StartInTime" Header="Thời gian bắt đầu vào" Width="150" DataIndex="StartInTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="EndInTime" Header="Thời gian kết thúc vào" Width="150" DataIndex="EndInTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="StartOutTime" Header="Thời gian bắt đầu ra" Width="150" DataIndex="StartOutTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="EndOutTime" Header="Thời gian kết thúc ra" Width="150" DataIndex="EndOutTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="WorkConvert" Header="Công quy đổi" Width="100" Align="Left" Locked="true" DataIndex="WorkConvert" />
                                        <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="120" Align="Left" Locked="true" DataIndex="TimeConvert" />
                                        <ext:Column ColumnID="Symbol" Header="Ký hiệu hiển thị" Width="100" Align="Left" Locked="true" DataIndex="Symbol" />
                                        <ext:Column ColumnID="SymbolDisplay" Header="Diễn giải" Width="250" Align="Center" DataIndex="SymbolDisplay" />
                                        <ext:Column ColumnID="GroupWorkShiftName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupWorkShiftName" Hidden="True">
                                            <Renderer Fn="RenderGroupSymbol" />
                                        </ext:Column>
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
            <ext:Window runat="server" Title="Tạo mới chi tiết bảng phân ca" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdTimeSheetRule"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Height="130">
                                        <Items>
                                            <ext:Container ID="Container11" runat="server" LabelWidth="150" LabelAlign="left"
                                                Layout="Form" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên ca<span style='color:red;'>*</span>"
                                                        AnchorHorizontal="98%" />
                                                    <ext:Hidden runat="server" ID="hdfSymbolId" />
                                                    <ext:ComboBox runat="server" ID="cbxSymbol" FieldLabel="Ký hiệu hiển thị<span style='color:red;'>*</span>" DisplayField="Code" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeSymbol" PageSize="15" Disabled="True"
                                                                  LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
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
                                                            <Select Handler="this.triggers[0].show(); hdfSymbolId.setValue(cbxSymbol.getValue()); Ext.net.DirectMethods.SetValueSelectSymbol();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSymbolId.reset(); };" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:TextField runat="server" ID="txtWorkConvert" CtCls="requiredData" FieldLabel="Công quy đổi<span style='color:red;'>*</span>" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="5" />
                                                    <ext:TextField runat="server" ID="txtStyleColor" FieldLabel="Màu background" AnchorHorizontal="98%" />
                                                    <ext:DateField ID="timeSheetFromDate" runat="server" Vtype="daterange" FieldLabel="Từ ngày" CtCls="requiredData"
                                                        AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                                        <CustomConfig>
                                                            <ext:ConfigItem Name="endDateField" Value="#{timeSheetToDate}" Mode="Value" />
                                                        </CustomConfig>
                                                    </ext:DateField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container12" runat="server" LabelWidth="150" LabelAlign="left"
                                                Layout="Form" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:Hidden runat="server" ID="hdfGroupSymbol" />
                                                    <ext:ComboBox runat="server" ID="cbxGroupSymbol" FieldLabel="Nhóm ký hiệu"
                                                        DisplayField="Name" MinChars="1" ValueField="Group" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupSymbol"
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
                                                       
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();hdfGroupSymbol.setValue(cbxGroupSymbol.getValue()); cbxSymbol.enable(); #{storeSymbol}.reload(); cbxSymbol.clearValue();txtSymbolDisplay.reset();txtWorkConvert.reset();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupSymbol.reset(); };" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:TextField runat="server" ID="txtCode" CtCls="requiredData" FieldLabel="Ký hiệu"
                                                        AnchorHorizontal="98%" Hidden="True"/>
                                                    <ext:TextField runat="server" ID="txtSymbolDisplay" CtCls="requiredData" FieldLabel="Diễn giải<span style='color:red;'>*</span>" AnchorHorizontal="98%" />
                                                    <ext:TextField runat="server" ID="txtTimeConvert" CtCls="requiredData" FieldLabel="Thời gian quy đổi<span style='color:red;'>*</span>" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="10" />
                                                    <ext:Hidden runat="server" ID="hdfGroupWorkShift" />
                                                    <ext:ComboBox runat="server" ID="cbxGroupWorkShift" FieldLabel="Nhóm phân ca"
                                                        LabelWidth="152" Width="305" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                                        ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="true" CtCls="requiredData">
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
                                                            <ext:Store runat="server" ID="storeGroupWorkShift" AutoLoad="false">
                                                                <Proxy>
                                                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                                </Proxy>
                                                                <BaseParams>
                                                                    <ext:Parameter Name="handlers" Value="GroupWorkShift" />
                                                                </BaseParams>
                                                                <Reader>
                                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                                        <Fields>
                                                                            <ext:RecordField Name="Id" />
                                                                            <ext:RecordField Name="Name" />
                                                                        </Fields>
                                                                    </ext:JsonReader>
                                                                </Reader>
                                                            </ext:Store>
                                                        </Store>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();hdfGroupWorkShift.setValue(cbxGroupWorkShift.getValue());"></Select>
                                                            <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();hdfGroupWorkShift.reset(); }" />
                                                        </Listeners>
                                                    </ext:ComboBox>

                                                    <ext:DateField ID="timeSheetToDate" runat="server" Vtype="daterange" FieldLabel="Đến ngày" CtCls="requiredData"
                                                        AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                                        <CustomConfig>
                                                            <ext:ConfigItem Name="startDateField" Value="#{timeSheetFromDate}" Mode="Value" />
                                                        </CustomConfig>
                                                    </ext:DateField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>

                                    <ext:Container ID="containerDay" runat="server">
                                        <Items>
                                            <ext:CheckboxGroup ID="checkGroupDay" runat="server" ColumnsNumber="7" FieldLabel="Chọn thứ">
                                                <Items>
                                                    <ext:Checkbox runat="server" ID="chkMonday" BoxLabel="Thứ 2" />
                                                    <ext:Checkbox runat="server" ID="chkTuesday" BoxLabel="Thứ 3" />
                                                    <ext:Checkbox runat="server" ID="chkWednesday" BoxLabel="Thứ 4" />
                                                    <ext:Checkbox runat="server" ID="chkThursday" BoxLabel="Thứ 5" />
                                                    <ext:Checkbox runat="server" ID="chkFriday" BoxLabel="Thứ 6" />
                                                    <ext:Checkbox runat="server" ID="chkSaturday" BoxLabel="Thứ 7" />
                                                    <ext:Checkbox runat="server" ID="chkSunday" BoxLabel="Chủ nhật" />
                                                </Items>
                                            </ext:CheckboxGroup>
                                        </Items>
                                    </ext:Container>

                                    <ext:Checkbox runat="server" BoxLabel="Bỏ ngày lễ tết" ID="chkTetHoliday" />
                                    <ext:Checkbox runat="server" BoxLabel="Tính giờ vào ra" ID="chk_IsHasInOutTime" />
                                    <ext:Checkbox runat="server" BoxLabel="Cho phép thêm giờ" ID="chk_IsOverTime" />
                                    <ext:TextField runat="server" ID="txtFactorOverTime" FieldLabel="Hệ số thêm giờ" MaskRe="/[0-9.]/" AnchorHorizontal="49%" MaxLength="5" />
                                    <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Height="80">
                                        <Items>
                                            <ext:Container ID="Container13" runat="server" LabelWidth="150" LabelAlign="left"
                                                Layout="Form" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TimeField ID="inTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Giờ vào làm việc<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:TimeField>
                                                    <ext:TimeField ID="startInTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Bắt đầu chấm công (Từ)<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:TimeField>
                                                    <ext:TimeField ID="startOutTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Kết thúc chấm công (Từ)<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:TimeField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container14" runat="server" LabelWidth="150" LabelAlign="left"
                                                Layout="Form" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TimeField ID="outTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Giờ kết thúc làm việc<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:TimeField>
                                                    <ext:TimeField ID="endInTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Đến<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:TimeField>
                                                    <ext:TimeField ID="endOutTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Đến<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:TimeField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
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
