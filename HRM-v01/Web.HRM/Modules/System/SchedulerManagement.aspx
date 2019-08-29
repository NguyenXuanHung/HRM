<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Setting.SchedulerManagement" CodeBehind="SchedulerManagement.aspx.cs" %>
<%@ Register src="/Modules/UC/ResourceCommon.ascx" tagName="ResourceCommon" tagPrefix="CCVC"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">
        // validate form in setting window
        var validateForm = function() {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên tiến trình!');
                return false;
            }
            if (!cbxSchedulerType.getValue()) {
                alert('Bạn chưa chọn loại tiến trình!');
                return false;
            }
            if (!cbxSchedulerRepeatType.getValue()) {
                alert('Bạn chưa chọn kiểu lặp tiến trình!');
                return false;
            }
            if (!cbxSchedulerScope.getValue()) {
                alert('Bạn chưa chọn phạm vi tiến trình!');
                return false;
            }
            if (!cbxSchedulerStatus.getValue()) {
                alert('Bạn chưa chọn trạng thái tiến trình!');
                return false;
            }
            if (!txtNextRuntime.getValue()) {
                alert('Bạn chưa nhập thời gian chạy!');
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

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <!-- Store -->
            <ext:Store runat="server" ID="storeScheduler" AutoSave="true">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerScheduler.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="schedulerType" Value="cbxSchedulerTypeFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="repeatType" Value="cbxSchedulerRepeatTypeFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="status" Value="cbxSchedulerStatusFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="scope" Value="cbxSchedulerScopeFilter.getValue()" Mode="Raw" />
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
            <ext:Store runat="server" ID="storeSchedulerType" AutoLoad="False" OnRefreshData="storeSchedulerType_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
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
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
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
                                                            <ext:Parameter Name="Id" Value="RowSelectionModel1.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server"/>
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="ReloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="False" />
                                        <ext:Column ColumnID="Name" Header="Tên" Width="200" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="100" DataIndex="Description"/>
                                        <ext:Column ColumnID="Arguments" Header="Tham số" Width="250" DataIndex="Arguments" />
                                        <ext:DateColumn ColumnID="LastRunTime" Header="Lần chạy cuối" Width="150" DataIndex="LastRunTime" Format="HH:mm:ss dd/MM/yyyy"/>
                                        <ext:DateColumn ColumnID="NextRunTime" Header="Lần chạy tiếp" Width="150" DataIndex="NextRunTime" Format="HH:mm:ss dd/MM/yyyy"/>
                                        <ext:Column ColumnID="Status" Header="Trạng thái" Width="150" DataIndex="StatusName" >
                                            <Renderer Fn="RenderStatus"/>
                                        </ext:Column>
                                        <ext:Column ColumnID="SchedulerType" Header="Loại" Width="150" DataIndex="SchedulerTypeName" />
                                        <ext:Column ColumnID="RepeatType" Header="Kiểu lặp" Width="100" DataIndex="RepeatTypeName" />
                                        <ext:Column ColumnID="Scope" Header="Phạm vi" Width="100" DataIndex="ScopeName">
                                            <Renderer Fn="RenderScope"/>
                                        </ext:Column>
                                        <ext:Column ColumnID="Enable" Header="Được chạy" Width="80" DataIndex="Enabled" Align="Center">
                                            <Renderer Fn="renderBooleanIcon" />
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server">
                                        <HeaderRows>
                                            <ext:HeaderRow>
                                                <Columns>
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                    <ext:HeaderColumn AutoWidthElement="False">
                                                        <Component>
                                                            <ext:ComboBox runat="server"
                                                                ID="cbxSchedulerStatusFilter"
                                                                StoreID="storeSchedulerStatus"
                                                                DisplayField="Name"
                                                                ValueField="Id"
                                                                AnchorHorizontal="99%"
                                                                Width="150"
                                                                ListWidth="190"
                                                                LoadingText="Đang tải...">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Expand Handler="#{cbxSchedulerStatusFilter}.store.reload();" />
                                                                    <Select Handler="
                                                                        this.triggers[0].show();
                                                                        ReloadGrid();"></Select>
                                                                    <TriggerClick Handler="
                                                                        if (index == 0) { 
                                                                            this.clearValue(); 
                                                                            this.triggers[0].hide();
                                                                            ReloadGrid();
                                                                        }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                    <ext:HeaderColumn AutoWidthElement="False">
                                                        <Component>
                                                            <ext:ComboBox runat="server"
                                                                ID="cbxSchedulerTypeFilter"
                                                                StoreID="storeSchedulerType"
                                                                DisplayField="Name"
                                                                ValueField="Id"
                                                                AnchorHorizontal="99%"
                                                                Width="150"
                                                                ListWidth="200"
                                                                LoadingText="Đang tải...">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Expand Handler="cbxSchedulerTypeFilter.store.reload();" />
                                                                    <Select Handler="
                                                                        this.triggers[0].show();
                                                                        ReloadGrid();"></Select>
                                                                    <TriggerClick Handler="
                                                                        if (index == 0) { 
                                                                            this.clearValue(); 
                                                                            this.triggers[0].hide();
                                                                            ReloadGrid();
                                                                        }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                    <ext:HeaderColumn AutoWidthElement="False">
                                                        <Component>
                                                            <ext:ComboBox runat="server"
                                                                ID="cbxSchedulerRepeatTypeFilter"
                                                                StoreID="storeSchedulerRepeatType"
                                                                DisplayField="Name"
                                                                ValueField="Id"
                                                                AnchorHorizontal="99%"
                                                                Width="100"
                                                                ListWidth="150"
                                                                LoadingText="Đang tải...">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Expand Handler="cbxSchedulerRepeatTypeFilter.store.reload();" />
                                                                    <Select Handler="
                                                                        this.triggers[0].show();
                                                                        ReloadGrid();"></Select>
                                                                    <TriggerClick Handler="
                                                                        if (index == 0) { 
                                                                            this.clearValue(); 
                                                                            this.triggers[0].hide();
                                                                            ReloadGrid();
                                                                        }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                    <ext:HeaderColumn AutoWidthElement="False">
                                                        <Component>
                                                            <ext:ComboBox runat="server"
                                                                ID="cbxSchedulerScopeFilter"
                                                                StoreID="storeSchedulerScope"
                                                                DisplayField="Name"
                                                                ValueField="Id"
                                                                AnchorHorizontal="99%"
                                                                Width="100"
                                                                ListWidth="150"
                                                                LoadingText="Đang tải...">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Expand Handler="#{cbxSchedulerScopeFilter}.store.reload();" />
                                                                    <Select Handler="
                                                                        this.triggers[0].show();
                                                                        ReloadGrid();"></Select>
                                                                    <TriggerClick Handler="
                                                                        if (index == 0) { 
                                                                            this.clearValue(); 
                                                                            this.triggers[0].hide();
                                                                            ReloadGrid();
                                                                        }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                    <ext:HeaderColumn AutoWidthElement="False" />
                                                </Columns>
                                            </ext:HeaderRow>
                                        </HeaderRows>
                                    </ext:LockingGridView>
                                </View>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();" />
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
                                            <Change Handler="#{RowSelectionModel1}.clearSelections();btnEdit.disable();btnDelete.disable();" />
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
                    <ext:Hidden runat="server" ID="hdfId" />
                    <ext:Hidden runat="server" ID="hdfSchedulerType" />
                    <ext:Hidden runat="server" ID="hdfSchedulerRepeatType" />
                    <ext:Hidden runat="server" ID="hdfSchedulerScope" />
                    <ext:Hidden runat="server" ID="hdfSchedulerStatus" />
                    <ext:TextField runat="server" ID="txtName" FieldLabel="Tên <span style='color:red;'>*</span>" AnchorHorizontal="100%" CtCls="requiredData" TabIndex="1" />
                    <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="100%" MaxLength="1000" TabIndex="2" />
                    <ext:Container runat="server" Layout="Column" Height="30">
                        <Items>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server"
                                        ID="cbxSchedulerType"
                                        StoreID="storeSchedulerType"
                                        DisplayField="Name"
                                        ValueField="Id"
                                        FieldLabel="Loại <span style='color:red;'>*</span>"
                                        AnchorHorizontal="97%"
                                        CtCls="requiredData"
                                        TabIndex="3">
                                        <Listeners>
                                            <Expand Handler="if(#{cbxSchedulerType}.store.getCount()==0) {#{storeSchedulerType}.reload();}" />
                                            <Select Handler="#{hdfSchedulerType}.setValue(this.getValue());"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server"
                                        ID="cbxSchedulerRepeatType"
                                        StoreID="storeSchedulerRepeatType"
                                        DisplayField="Name"
                                        ValueField="Id"
                                        FieldLabel="Kiểu lặp <span style='color:red;'>*</span>"
                                        AnchorHorizontal="100%"
                                        CtCls="requiredData"
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
                                        FieldLabel="Phạm vi <span style='color:red;'>*</span>"
                                        AnchorHorizontal="97%"
                                        CtCls="requiredData"
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
                                    <ext:TextField runat="server" ID="txtNextRuntime" FieldLabel="Thời gian chạy" AnchorHorizontal="100%" Editable="true" CtCls="requiredData" EmptyText="YYYY/MM/DD HH:MM (24 giờ)" TabIndex="10" />
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
        
        </div>
    </form>
</body>
</html>
