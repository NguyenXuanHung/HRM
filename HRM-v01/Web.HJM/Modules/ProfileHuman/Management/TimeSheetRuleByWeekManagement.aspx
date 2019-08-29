<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.TimeSheetRuleByWeekManagement" Codebehind="TimeSheetRuleByWeekManagement.aspx.cs" %>

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
                alert('Bạn chưa nhập tên luật!');
                return false;
            }
            if (!startInputTime.getValue()) {
                alert('Bạn chưa nhập thời gian bắt đầu chấm công vào!');
                return false;
            }
            if (!endInputTime.getValue()) {
                alert('Bạn chưa nhập thời gian kết thúc chấm công vào!');
                return false;
            }
            if (!startOutputTime.getValue()) {
                alert('Bạn chưa nhập thời gian bắt đầu chấm công ra!');
                return false;
            }
            if (!endOutputTime.getValue()) {
                alert('Bạn chưa nhập thời gian kết thúc chấm công ra!');
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
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridTimeSheetRule" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTimeSheetRule.show();btnUpdate.hide();btnUpdateClose.show();
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

                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập tên luật">
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
                                    <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="TimeSheetRule" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="DayOfWeek" />
                                                    <%--<ext:RecordField Name="SpecifiedDate" />--%>
                                                    <ext:RecordField Name="StartInputTime" />
                                                    <ext:RecordField Name="EndInputTime" />
                                                    <ext:RecordField Name="StartOutputTime" />
                                                    <ext:RecordField Name="EndOutputTime" />
                                                    <ext:RecordField Name="WorkConvert" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên luật" Width="150" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:Column ColumnID="Code" Header="Ký hiệu" Width="150" Align="Left" Locked="true" DataIndex="Code" />
                                        <ext:Column ColumnID="DayOfWeek" Header="Thứ" Width="150" Align="Left" Locked="true" DataIndex="DayOfWeek" />
                                        <%--<ext:DateColumn ColumnID="SpecifiedDate" Header="Ngày cụ thể" Width="170" DataIndex="SpecifiedDate" Format="dd/MM/yyyy" />--%>
                                        <ext:Column ColumnID="StartInputTime" Header="Thời gian bắt đầu vào" Width="170" DataIndex="StartInputTime">
                                            <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="EndInputTime" Header="Thời gian kết thúc vào" Width="150" DataIndex="EndInputTime">
                                                <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="StartOutputTime" Header="Thời gian bắt đầu ra" Width="170" DataIndex="StartOutputTime">
                                             <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                        <ext:Column ColumnID="EndOutputTime" Header="Thời gian kết thúc ra" Width="150" DataIndex="EndOutputTime">
                                             <Renderer Fn="RenderTime" />
                                        </ext:Column>
                                         <ext:Column ColumnID="WorkConvert" Header="Số công quy đổi" Width="150" Align="Left" Locked="true" DataIndex="WorkConvert" />
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
            <ext:Window runat="server" Title="Tạo mới luật chấm công theo tuần" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdTimeSheetRule"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên luật<span style='color:red;'>*</span>"
                                        AnchorHorizontal="98%" />
                                    <ext:TextField runat="server" ID="txtCode" CtCls="requiredData" FieldLabel="Ký hiệu<span style='color:red;'>*</span>"
                                        AnchorHorizontal="98%" />
                                    <ext:Container ID="containerDay" runat="server">
                                        <Items>
                                            <ext:CheckboxGroup ID="checkGroupDay" runat="server" ColumnsNumber="1" FieldLabel="Chọn thứ">
                                                <Items>
                                                    <ext:Checkbox runat="server" ID="chkMonday" BoxLabel="Thứ 2"/>
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
                                    <%--<ext:DateField ID="dfSpecifiedDate" runat="server" Vtype="daterange" FieldLabel="Chọn ngày (nếu có)"
                                        AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" />--%>
                                    <ext:Container ID="container3" runat="server" FieldLabel="Bắt đầu chấm công" AnchorHorizontal="100%" Layout="ColumnLayout" Height="25px">
                                        <Items>
                                            <ext:Container ID="container4" runat="server" ColumnWidth="0.5" Layout="FormLayout">
                                                <Items>
                                                    <ext:TimeField ID="startInputTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Từ<span style='color:red;'>*</span>" AnchorHorizontal="98%"
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
                                            <ext:Container ID="container5" runat="server" ColumnWidth="0.5" Layout="FormLayout">
                                                <Items>
                                                    <ext:TimeField ID="endInputTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
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
                                    <ext:Container ID="container6" runat="server" FieldLabel="Kết thúc chấm công" AnchorHorizontal="100%" Layout="ColumnLayout" Height="25px">
                                        <Items>
                                            <ext:Container ID="container7" runat="server" ColumnWidth="0.5" Layout="FormLayout">
                                                <Items>
                                                    <ext:TimeField ID="startOutputTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                                        Format="H:mm" FieldLabel="Từ<span style='color:red;'>*</span>" AnchorHorizontal="98%"
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
                                            <ext:Container ID="container8" runat="server" ColumnWidth="0.5" Layout="FormLayout">
                                                <Items>
                                                    <ext:TimeField ID="endOutputTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
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
                                    <ext:TextField runat="server" ID="txtNumberOfDay" FieldLabel="Số công quy đổi"
                                        AnchorHorizontal="100%" MaskRe="/[0-9.,]/" MaxLength="10" />
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
