<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Setting.SchedulerTimeSheetManagement" CodeBehind="SchedulerTimeSheetManagement.aspx.cs" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />

    <script type="text/javascript">
        // validate form in setting window
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên tiến trình!');
                return false;
            }

            if (!cbxSchedulerStatus.getValue()) {
                alert('Bạn chưa chọn trạng thái tiến trình!');
                return false;
            }
            if (hdfTimeSheetGroupListId.getValue() === null || hdfTimeSheetGroupListId.getValue() === '') {
                alert('Bạn chưa chọn bảng phân ca!');
                return false;
            }
            return true;
        };
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }

        var RenderStatus = function (value, p, record) {
            var styleColor = '';
            switch (record.data.Status) {
                case "Ready":
                    styleColor = 'limegreen';
                    break;
                case "Running":
                    styleColor = 'blue';
                    break;
                case "Completed":
                    styleColor = 'fuchsia';
                    break;
                case "Deleted":
                    styleColor = 'red';
                    break;
            }
            return "<span style='font-weight:bold;color:" + styleColor + "'>" + value + "</span>";
        }

        var RenderScope = function (value, p, record) {
            var styleColor = '';
            if (record.data.Scope === "Internal") {
                styleColor = 'darkgreen';
            } else if (record.data.Scope === "External") {
                styleColor = 'coral';
            }
            return "<span style='font-weight:bold;color:" + styleColor + "'>" + value + "</span>";
        }
        var RenderSymbol = function (value, p, record) {
            var color = record.data.Color || record.data.SymbolColor
            if (value)
                return "<span class='badge' style='background:" + color + "'>" + value + "</span>";
            else
                return "<span class='badge' style='background:#FF0000'>!</span>";
        }

        var ValidateInput = function () {
            if (hdfPayrollId.getValue() === null || hdfPayrollId.getValue() === '') {
                alert("Bạn phải chọn bảng lương");
                return false;
            }

            if (hdfColumnCode.getValue() === null || hdfColumnCode.getValue() === '') {
                alert("Bạn chưa chọn tên cột");
                return false;
            }
            if (gridTimeSheetSymbol.getSelectionModel().getCount() == 0) {
                alert("Bạn phải chọn ít nhất một ký hiệu");
                return false;
            }

            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfSchedulerType" />
            <ext:Hidden runat="server" ID="hdfSchedulerRepeatType" />
            <ext:Hidden runat="server" ID="hdfSchedulerScope" />
            <ext:Hidden runat="server" ID="hdfSchedulerStatus" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfTimeSheetGroupListId" />
            <ext:Hidden runat="server" ID="hdfConfigId" /> 
            <ext:Hidden runat="server" ID="hdfDataType" />

            <!-- Store -->
            <ext:Store runat="server" ID="storeScheduler" AutoSave="true">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerScheduler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="schedulerType" Value="hdfSchedulerType.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="SchedulerTypeId" />
                            <ext:RecordField Name="SchedulerTypeName" />
                            <ext:RecordField Name="Arguments" />
                            <ext:RecordField Name="RepeatType" />
                            <ext:RecordField Name="RepeatTypeName" />
                            <ext:RecordField Name="Scope" />
                            <ext:RecordField Name="ScopeName" />
                            <ext:RecordField Name="IntervalTime" />
                            <ext:RecordField Name="ExpiredAfter" />
                            <ext:RecordField Name="ExpiredTime" />
                            <ext:RecordField Name="LastRunTime" />
                            <ext:RecordField Name="NextRunTime" />
                            <ext:RecordField Name="Enabled" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeSchedulerRepeatType" AutoLoad="False" OnRefreshData="storeSchedulerRepeatType_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeSchedulerScope" AutoLoad="False" OnRefreshData="storeSchedulerScope_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeSchedulerStatus" AutoLoad="False" OnRefreshData="storeSchedulerStatus_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
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
            <!-- Viewport -->
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel runat="server"
                                ID="gpScheduler"
                                StoreID="storeScheduler"
                                TrackMouseOver="true"
                                Header="false"
                                StripeRows="true"
                                AutoExpandColumn="Description"
                                Border="false"
                                Layout="Fit">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete">
                                                <Listeners>
                                                    <Click Handler="wdWindow.show();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server" />
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="ReloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer runat="server" Width="5" />
                                            <ext:Button runat="server" ID="btnCog" Text="Tiện ích" Icon="Cog">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" ID="mnCreateDynamicColumn" Text="Tiến trình tạo cột động" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="wdDynamicColumn.show();"></Click>
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>

                                                </Menu>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="False" />
                                        <ext:Column ColumnID="Name" Header="Tên" Width="200" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="100" DataIndex="Description" />
                                        <ext:Column ColumnID="Arguments" Header="Tham số" Width="250" DataIndex="Arguments" />
                                        <ext:DateColumn ColumnID="LastRunTime" Header="Lần chạy cuối" Width="150" DataIndex="LastRunTime" Format="HH:mm:ss dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="NextRunTime" Header="Lần chạy tiếp" Width="150" DataIndex="NextRunTime" Format="HH:mm:ss dd/MM/yyyy" />
                                        <ext:Column ColumnID="Status" Header="Trạng thái" Width="150" DataIndex="StatusName">
                                            <Renderer Fn="RenderStatus" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SchedulerType" Header="Loại" Width="150" DataIndex="SchedulerTypeName" />
                                        <ext:Column ColumnID="RepeatType" Header="Kiểu lặp" Width="100" DataIndex="RepeatTypeName" />
                                        <ext:Column ColumnID="Scope" Header="Phạm vi" Width="100" DataIndex="ScopeName">
                                            <Renderer Fn="RenderScope" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Enable" Header="Được chạy" Width="80" DataIndex="Enabled" Align="Center">
                                            <Renderer Fn="renderBooleanIcon" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="btnEdit.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();" />
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
                                                    <Select Handler="#{PagingToolbar1}.pageSize=parseInt(this.getValue());#{PagingToolbar1}.doLoad();#{RowSelectionModel1}.clearSelections();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="#{RowSelectionModel1}.clearSelections();btnEdit.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <!-- Window -->
            <ext:Window runat="server"
                ID="wdSetting"
                Resizable="true"
                Layout="FormLayout"
                Padding="10"
                Width="600"
                Height="400"
                Hidden="true"
                Modal="true"
                Constrain="true">
                <Items>
                    <ext:TextField runat="server" ID="txtName" FieldLabel="Tên" AnchorHorizontal="100%" CtCls="requiredData" TabIndex="1" />
                    <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="100%" MaxLength="1000" TabIndex="2" />
                    <ext:Container runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server"
                                        ID="cbxSchedulerRepeatType"
                                        StoreID="storeSchedulerRepeatType"
                                        DisplayField="Name"
                                        ValueField="Id"
                                        FieldLabel="Kiểu lặp"
                                        AnchorHorizontal="100%"
                                        TabIndex="4">
                                        <Listeners>
                                            <Expand Handler="if(#{cbxSchedulerRepeatType}.store.getCount()==0){#{storeSchedulerRepeatType}.reload();}" />
                                            <Select Handler="#{hdfSchedulerRepeatType}.setValue(this.getValue());"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server"
                                        ID="cbxSchedulerScope"
                                        StoreID="storeSchedulerScope"
                                        DisplayField="Name"
                                        ValueField="Id"
                                        FieldLabel="Phạm vi"
                                        AnchorHorizontal="97%"
                                        TabIndex="5">
                                        <Listeners>
                                            <Expand Handler="if(#{cbxSchedulerScope}.store.getCount()==0){#{storeSchedulerScope}.reload();}" />
                                            <Select Handler="#{hdfSchedulerScope}.setValue(this.getValue());"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server"
                                        ID="cbxSchedulerStatus"
                                        StoreID="storeSchedulerStatus"
                                        DisplayField="Name"
                                        ValueField="Id"
                                        FieldLabel="Trạng thái <span style='color:red;'>*</span>"
                                        AnchorHorizontal="100%"
                                        CtCls="requiredData"
                                        TabIndex="6">
                                        <Listeners>
                                            <Expand Handler="if(#{cbxSchedulerStatus}.store.getCount()==0){#{storeSchedulerStatus}.reload();}" />
                                            <Select Handler="#{hdfSchedulerStatus}.setValue(this.getValue());"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtIntervalTime" FieldLabel="Thời gian lặp" AnchorHorizontal="97%" Editable="true" MaskRe="/[0-9]/" TabIndex="7"></ext:TextField>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtExpiredAfter" FieldLabel="Kết thúc sau" AnchorHorizontal="100%" Editable="true" MaskRe="/[0-9]/" EmptyText="Đơn vị (phút)" TabIndex="8"></ext:TextField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:Checkbox runat="server" ID="chkEnable" FieldLabel="Cho phép chạy" AnchorHorizontal="97%" Checked="True" TabIndex="9" />
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtNextRuntime" FieldLabel="Thời gian chạy" AnchorHorizontal="100%" Editable="true" EmptyText="YYYY/MM/DD HH:MM (24 giờ)" TabIndex="10" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:ComboBox runat="server" ID="cboGroupWorkShift"
                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true" StoreID="storeGroupWorkShift"
                        Width="200" ItemSelector="div.list-item" PageSize="20" CtCls="requiredData" FieldLabel="Nhóm phân ca">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template runat="server">
                            <Html>
                                <tpl for=".">
                            <div class="list-item"> 
                                {Name}
                            </div>
                        </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();#{hdfTimeSheetGroupListId}.setValue(#{cboGroupWorkShift}.getValue());txtArguments.setValue('-m ' + cbxMonth.getValue() +   ' -y ' + spnYear.getValue() + ' -groupWorkShiftId ' + cboGroupWorkShift.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfTimeSheetGroupListId}.reset(); };" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Container runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cbxMonth" Width="100" Editable="false" FieldLabel="Chọn tháng">
                                        <Items>
                                            <ext:ListItem Text="Tháng 1" Value="1" />
                                            <ext:ListItem Text="Tháng 2" Value="2" />
                                            <ext:ListItem Text="Tháng 3" Value="3" />
                                            <ext:ListItem Text="Tháng 4" Value="4" />
                                            <ext:ListItem Text="Tháng 5" Value="5" />
                                            <ext:ListItem Text="Tháng 6" Value="6" />
                                            <ext:ListItem Text="Tháng 7" Value="7" />
                                            <ext:ListItem Text="Tháng 8" Value="8" />
                                            <ext:ListItem Text="Tháng 9" Value="9" />
                                            <ext:ListItem Text="Tháng 10" Value="10" />
                                            <ext:ListItem Text="Tháng 11" Value="11" />
                                            <ext:ListItem Text="Tháng 12" Value="12" />
                                        </Items>
                                        <Listeners>
                                            <Select Handler="#{hdfMonth}.setValue(#{cbxMonth}.getValue());txtArguments.setValue('-m ' + cbxMonth.getValue() +   ' -y ' + spnYear.getValue() + ' -groupWorkShiftId ' + cboGroupWorkShift.getValue());" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" ColumnWidth="0.5">
                                <Items>
                                    <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="100">
                                        <Listeners>
                                            <Spin Handler="#{hdfYear}.setValue(#{spnYear}.getValue());txtArguments.setValue('-m ' + cbxMonth.getValue() +   ' -y ' + spnYear.getValue() + ' -groupWorkShiftId ' + cboGroupWorkShift.getValue());" />
                                        </Listeners>
                                    </ext:SpinnerField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:TextField runat="server" FieldLabel="Tham số" ID="txtArguments" AnchorHorizontal="100%" TabIndex="11" />
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk" TabIndex="12">
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
            <ext:Window runat="server" Width="800" ID="wdWindow" Layout="FormLayout" Title="Xóa dữ liệu chấm công"
                Constrain="true" Icon="Cog" Modal="true" Hidden="true" Resizable="false" AutoHeight="true" Padding="6">
                <Items>
                    <ext:Container runat="server" Layout="Form">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfGroupWorkShiftId" />
                            <ext:Hidden runat="server" ID="hdfChoseMonth" />
                            <ext:Hidden runat="server" ID="hdfChoseYear"></ext:Hidden>
                            <ext:ComboBox runat="server" ID="cboGroupWork"
                                DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupWorkShift"
                                Width="200" ItemSelector="div.list-item" PageSize="20" CtCls="requiredData" FieldLabel="Nhóm phân ca">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template runat="server">
                                    <Html>
                                        <tpl for=".">
                                            <div class="list-item"> 
                                                {Name}
                                            </div>
                                        </tpl>
                                    </Html>
                                </Template>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();#{hdfGroupWorkShiftId}.setValue(#{cboGroupWork}.getValue());" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfGroupWorkShiftId}.reset(); };" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Container runat="server" Layout="Column" Height="30">
                                <Items>
                                    <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboChoseMonth" Width="100" Editable="false" FieldLabel="Chọn tháng">
                                                <Items>
                                                    <ext:ListItem Text="Tháng 1" Value="1" />
                                                    <ext:ListItem Text="Tháng 2" Value="2" />
                                                    <ext:ListItem Text="Tháng 3" Value="3" />
                                                    <ext:ListItem Text="Tháng 4" Value="4" />
                                                    <ext:ListItem Text="Tháng 5" Value="5" />
                                                    <ext:ListItem Text="Tháng 6" Value="6" />
                                                    <ext:ListItem Text="Tháng 7" Value="7" />
                                                    <ext:ListItem Text="Tháng 8" Value="8" />
                                                    <ext:ListItem Text="Tháng 9" Value="9" />
                                                    <ext:ListItem Text="Tháng 10" Value="10" />
                                                    <ext:ListItem Text="Tháng 11" Value="11" />
                                                    <ext:ListItem Text="Tháng 12" Value="12" />
                                                </Items>
                                                <Listeners>
                                                    <Select Handler="#{hdfChoseMonth}.setValue(#{cboChoseMonth}.getValue());" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                        <Items>
                                            <ext:SpinnerField runat="server" ID="sfChoseYear" FieldLabel="Chọn năm" Width="100">
                                                <Listeners>
                                                    <Spin Handler="#{hdfChoseYear}.setValue(#{sfChoseYear}.getValue());" />
                                                </Listeners>
                                            </ext:SpinnerField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="Xóa dữ liệu" Icon="Delete">
                        <DirectEvents>
                            <Click OnEvent="Delete_Click">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Width="800" ID="wdDynamicColumn" Layout="FormLayout" Title="Tạo mới cột động"
                Constrain="true" Icon="Add" Modal="true" Hidden="true" Resizable="false" AutoHeight="true" Padding="6">
                <Items>
                    <ext:Hidden runat="server" ID="hdfPayrollId" />
                    <ext:ComboBox runat="server" ID="cboPayroll" FieldLabel="Chọn bảng lương"
                        DisplayField="Title" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                        ItemSelector="div.list-item" Width="368" PageSize="20" CtCls="requiredData">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template runat="server">
                            <Html>
                                <tpl for=".">
                                    <div class="list-item"> 
                                        {Title}
                                    </div>
                                </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="storeSalaryBoard">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="SalaryBoardList" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Title" />
                                            <ext:RecordField Name="Code" />
                                            <ext:RecordField Name="Description" />
                                            <ext:RecordField Name="Month" />
                                            <ext:RecordField Name="Year" />
                                            <ext:RecordField Name="CreatedDate" />
                                            <ext:RecordField Name="CreatedBy" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Expand Handler="#{storeSalaryBoard}.reload();" />
                            <Select Handler="this.triggers[0].show();#{hdfPayrollId}.setValue(#{cboPayroll}.getValue());Ext.net.DirectMethods.GetConfig();cbxColumnCodeStore.reload();" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfPayrollId}.reset();}" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Hidden ID="hdfColumnCode" runat="server" />
                    <ext:ComboBox runat="server" ID="cbxColumnCode" FieldLabel="Tên cột" AnchorHorizontal="100%"
                        DisplayField="Display" ValueField="ColumnCode" ItemSelector="div.list-item" PageSize="20" CtCls="requiredData">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="list-item"> 
							            {Display}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="cbxColumnCodeStore">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="SalaryConfig" />
                                    <ext:Parameter Name="ConfigId" Value="hdfConfigId.getValue()" Mode="Raw" />
                                    <ext:Parameter Name="DataType" Value="hdfDataType.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="ColumnCode" />
                                            <ext:RecordField Name="Display" />
                                            <ext:RecordField Name="Formula" />
                                            <ext:RecordField Name="ColumnExcel" />
                                            <ext:RecordField Name="IsReadOnly" />
                                            <ext:RecordField Name="IsInUsed" />
                                            <ext:RecordField Name="IsDisable" />
                                            <ext:RecordField Name="Order" />
                                            <ext:RecordField Name="Description" />
                                            <ext:RecordField Name="DataType" />
                                            <ext:RecordField Name="ConfigId" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfColumnCode.setValue(cbxColumnCode.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfColumnCode.reset();}" />
                        </Listeners>
                    </ext:ComboBox>
                    
                    <ext:GridPanel ID="gridTimeSheetSymbol" runat="server" Height="235" AnchorHorizontal="100%"
                        Title="Ký hiệu chấm công" ClicksToEdit="1" AutoExpandColumn="Name">
                        <Store>
                            <ext:Store ID="storeStatusWork" AutoSave="true" runat="server" GroupField="GroupSymbolName">
                                <Proxy>
                                    <ext:HttpProxy Method="GET" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
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
                                            <ext:RecordField Name="Color" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <View>
                            <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                ShowGroupName="false" EnableNoGroups="true" />
                        </View>
                        <ColumnModel runat="server">
                            <Columns>
                                <ext:Column Header="Ký hiệu" DataIndex="Code" Width="50">
                                    <Renderer Fn="RenderSymbol" />
                                </ext:Column>
                                <ext:Column ColumnID="txtStatusName" Header="Tên tình trạng"
                                    DataIndex="Name" Width="120" />
                                <ext:Column ColumnID="WorkConvert" Header="Số công quy đổi" Width="170" DataIndex="WorkConvert" />
                                <ext:Column ColumnID="GroupSymbolName" Header="Nhóm ký hiệu" Width="150" DataIndex="GroupSymbolName" Hidden="True">
                                    <Renderer Fn="RenderGroupSymbol" />
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:CheckboxSelectionModel ID="chkSelectionModelSymbol" runat="server" SingleSelect="false">
                            </ext:CheckboxSelectionModel>
                        </SelectionModel>
                        <LoadMask ShowMask="true" Msg="Đang tải" />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10">
                                <Items>
                                    <ext:Label ID="Label4" runat="server" Text="Số bản ghi trên một trang:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer5" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox3" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="20" />
                                            <ext:ListItem Text="30" />
                                            <ext:ListItem Text="50" />
                                        </Items>
                                        <SelectedItem Value="10" />
                                        <Listeners>
                                            <Select Handler="#{PagingToolbar4}.pageSize = parseInt(this.getValue()); #{PagingToolbar4}.doLoad();"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnSaveDynamicColumn" runat="server" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return ValidateInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="SaveDynamicColumnClick">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
