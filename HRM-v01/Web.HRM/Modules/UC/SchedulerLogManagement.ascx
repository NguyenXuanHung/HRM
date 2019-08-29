<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.UC.SchedulerLogManagement" Codebehind="~/Modules/UC/SchedulerLogManagement.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">
    var store3;
    var SetStore = function (s3) {
        store3 = s3;
    }
    var enterKeyToSearch = function (f, e) {
        try {
            this.triggers[0].show();
            if (e.getKey() == e.ENTER) {
                store3.reload();
            }
            if (txtFullName.getValue() == '') {
                this.triggers[0].hide();
            }
        } catch (e) {

        }
    }
    var btnAddEmployee_CheckSelectRow = function (gridPanel, hdfHoTenCanBo) {
        if (gridPanel.getSelectionModel().getCount() == 0) {
            Ext.Msg.alert("Thông báo", "Bạn phải chọn ít nhất một cán bộ !");
            return false;
        }
        getEmployeeName(gridPanel, hdfHoTenCanBo);
        return true;
    }

    var getEmployeeName = function (gridPanel, hdfHoTenCanBo) {
        var s = gridPanel.getSelectionModel().getSelections();
       
        var rs = "";
        for (var i = 0, r; r = s[i]; i++) {
            rs += r.data.FullName + ", ";
        }
        hdfHoTenCanBo.setValue(rs);
    }
    var ReloadGrid = function () {
        PagingToolbar1.pageIndex = 0;
        PagingToolbar1.doLoad();
        RowSelectionModel1.clearSelections();
    }
    var checkInputSchedulerLog = function () {
        if (!cbxSchedulerHistoryFilter.getValue()) {
            alert('Bạn chưa chọn lịch sử tiến trình!');
            return false;
        }
        return true;
    }
</script>
<ext:Hidden runat="server" ID="hdfKeyRecordLog" />
<ext:Hidden runat="server" ID="hdfDepartments" />
<ext:Hidden runat="server" ID="hdfKeyRecord" />
<ext:Store ID="storeSchedulerHistoryLog" AutoSave="false" ShowWarningOnFailure="false"
           SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="storeSchedulerHistoryLog_OnRefreshData"
           runat="server">
    <Reader>
        <ext:JsonReader IDProperty="Id">
            <Fields>
                <ext:RecordField Name="Id" />
                <ext:RecordField Name="Description" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window Modal="true" Resizable="false" Hidden="true" runat="server" ID="wdSchedulerLog"
    Constrain="true" Title="Log tiến trình" Icon="UserAdd" Width="700"
    Padding="6" AutoHeight="true">
    <Items>
        <ext:Container ID="Container1" runat="server" Height="330" Layout="FormLayout">
            <Items>
                <ext:GridPanel runat="server" ID="gridSchedulerLog" Icon="UserAdd" Header="false" Title="Log tiến trình"
                    AnchorHorizontal="100%" Height="300">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tb">
                            <Items>
                                <%--<ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                    <DirectEvents>
                                        <Click OnEvent="AddOrUpdateSchedulerLog">
                                            <ExtraParams>
                                                <ext:Parameter Name="Button" Value="Add" />
                                            </ExtraParams>
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>--%>
                                <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                    <DirectEvents>
                                        <Click OnEvent="EditSchedulerLog_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="Button" Value="Update" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                    <DirectEvents>
                                        <Click OnEvent="DeleteLog">
                                            <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa log tiến trình này" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store ID="StoreSchedulerHistory" AutoSave="true" runat="server">
                            <Proxy>
                                <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                            </Proxy>
                            <AutoLoadParams>
                                <ext:Parameter Name="start" Value="={0}" />
                                <ext:Parameter Name="limit" Value="={15}" />
                            </AutoLoadParams>
                            <BaseParams>
                                <ext:Parameter Name="handlers" Value="SchedulerLog" />
                                <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                <ext:Parameter Name="SchedulerHistory" Value="#{hdfKeyRecord}.getValue()" Mode="Raw"/>
                            </BaseParams>
                           <Reader>
                                <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="SchedulerHistoryId" />
                                        <ext:RecordField Name="Description" />
                                        <ext:RecordField Name="CreatedDate" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>

                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="False" />
                            <ext:Column ColumnID="Description" Header="Mô tả" Width="500" DataIndex="Description" />
                            <ext:Column ColumnID="CreatedDate" Header="Ngày tạo" Width="120" DataIndex="CreatedDate" />
                        </Columns>
                    </ColumnModel>

                    <LoadMask ShowMask="true" Msg="Đang tải...." />
                    <Listeners>
                        <RowClick Handler="ucSchedulerLogManagement_btnEdit.enable();ucSchedulerLogManagement_btnDelete.enable();" />
                        <RowDblClick Handler="" />
                    </Listeners>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                            <Listeners>
                                <RowSelect Handler="ucSchedulerLogManagement_hdfKeyRecordLog.setValue(ucSchedulerLogManagement_RowSelectionModel1.getSelected().get('Id'));" />
                                <RowDeselect Handler="ucSchedulerLogManagement_hdfKeyRecordLog.reset(); " />
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
                                <Change Handler="ucSchedulerLogManagement_RowSelectionModel1.clearSelections();" />
                            </Listeners>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Container>
    </Items>
</ext:Window>

<ext:Window runat="server" Title="Thêm log tiến trình mới" Resizable="true" Layout="FormLayout"
            Padding="6" Width="600" Hidden="true" Icon="UserTick" ID="wdSchedulerLogManagement"
            Modal="true" Constrain="true" Height="250">
    <Items>
        <%--<ext:ComboBox runat="server" ID="cbxSchedulerHistoryFilter" DisplayField="Description"
                      FieldLabel="Lịch sử tiến trình<span style='color:red;'>*</span>" ValueField="Id"
                      AnchorHorizontal="99%" TabIndex="3" Editable="false" ItemSelector="div.list-item" StoreID="storeSchedulerHistoryLog"
                      CtCls="requiredData">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template1" runat="server">
                <Html>
                <tpl for=".">
                    <div class="list-item"> 
                        {Description}
                    </div>
                </tpl>
                </Html>
            </Template>
            <Listeners>
                <Expand Handler="if(#{cbxSchedulerHistoryFilter}.store.getCount()==0){#{storeSchedulerHistoryLog}.reload();}" />
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
            </Listeners>
        </ext:ComboBox>--%>
        <ext:TextField runat="server" ID="txtSchedulerHistory" FieldLabel="Lịch sử tiến trình<span style='color:red'>*</span>" AnchorHorizontal="99%"
                       Disabled="true" DisabledClass="disabled-input-style">
        </ext:TextField>
        <ext:TextArea ID="txtDesciptionLog" runat="server" FieldLabel="Mô tả" AnchorHorizontal="99%"
                      TabIndex="3" MaxLength="500" />
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnAddAndCloseLog" Hidden="true" Text="Cập nhật" Icon="Disk">
           
            <DirectEvents>
                <Click OnEvent="InsertOrUpdateSchedulerLog">
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Add" />
                    </ExtraParams>
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnUpdateLog" Text="Cập nhật và đóng lại" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="InsertOrUpdateSchedulerLog">
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update" />
                    </ExtraParams>
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnCloseLog" Text="Đóng lại" Icon="Decline">
            <DirectEvents>
                <Click OnEvent="CloseWindow">
                    <EventMask ShowMask="true" />
                </Click>
            </DirectEvents>
        </ext:Button>
    </Buttons>
</ext:Window>
