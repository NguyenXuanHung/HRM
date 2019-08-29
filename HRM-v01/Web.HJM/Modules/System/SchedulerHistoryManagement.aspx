<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.System.SchedulerHistoryManagement" Codebehind="SchedulerHistoryManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Modules/System/ucSchedulerLogManagement.ascx" TagPrefix="uc1" TagName="ucSchedulerLogManagement" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="/Resource/js/global.js" type="text/javascript"></script>
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
            window.PagingToolbar1.pageIndex = 0;
            window.PagingToolbar1.doLoad();
            window.RowSelectionModel1.clearSelections();
        }
        var checkInputSchedulerHistory = function () {
            if (!cbxScheduler.getValue()) {
                alert('Bạn chưa chọn tiến trình!');
                return false;
            }
            if (txtStartTime.getValue() == '' || txtStartTime.getValue().trim == '' || txtStartTime.getValue() == null) {
                alert('Bạn chưa chọn thời gian bắt đầu!');
                return false;
            }
            if (txtEndTime.getValue() == '' || txtEndTime.getValue().trim == '' || txtEndTime.getValue() == null) {
                alert('Bạn chưa chọn thời gian kết thúc!');
                return false;
            }
            if (txtStartTime.getValue() > txtEndTime.getValue()) {
                alert("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                txtEndTime.focus();
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfKeyRecordLog" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <uc1:ucSchedulerLogManagement runat="server" ID="ucSchedulerLogManagement" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Store ID="storeScheduler" AutoSave="false" ShowWarningOnFailure="false"
                SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="storeScheduler_OnRefreshData"
                runat="server">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridScheduleHistory" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <%--<ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTimeSheetRule.show();wdTimeSheetRule.setTitle('Thêm lịch sử tiến trình');btnUpdate.hide();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>--%>
                                            <ext:Button runat="server" ID="btnEditHistory" Text="Sửa" Icon="Pencil" Disabled="true">
                                               <DirectEvents>
                                                   <Click OnEvent="EditSchedulerHistory_Click"></Click>
                                               </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDeleteHistory" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa lịch sử tiến trình này" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDetail" Text="Xem chi tiết" Icon="Pencil" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="ucSchedulerLogManagement_wdSchedulerLog.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="700" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="260" EmptyText="Nhập tên hiển thị hoặc mô tả">
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
                                    <ext:Store ID="StoreSchedulerHistory" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="SchedulerHistory" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="SchedulerName" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="HasError" />
                                                    <ext:RecordField Name="ErrorMessage" />
                                                    <ext:RecordField Name="ErrorDescription" />
                                                    <ext:RecordField Name="StartTime" />
                                                    <ext:RecordField Name="EndTime" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="False" />
                                        <ext:Column ColumnID="SchedulerName" Header="Tên tiến trình" Width="170" DataIndex="SchedulerName" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="170" DataIndex="Description" />
                                        <ext:Column ColumnID="HasError" Header="Lỗi" Width="50" DataIndex="HasError" Align="Center">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="ErrorMessage" Header="Message Error" Width="150" DataIndex="ErrorMessage" />
                                        <ext:Column ColumnID="ErrorDescription" Header="Mô tả lỗi" Width="200" DataIndex="ErrorDescription" />
                                        <ext:Column ColumnID="StartTime" Header="Thời gian bắt đầu" Width="170" DataIndex="StartTime" />
                                        <ext:Column ColumnID="EndTime" Header="Thời gian kết thúc" Width="170" DataIndex="EndTime" />

                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEditHistory.enable();btnDeleteHistory.enable();btnDetail.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id'));
                                                ucSchedulerLogManagement_hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id'));
                                                ucSchedulerLogManagement_gridSchedulerLog.reload();
                                                ucSchedulerLogManagement_btnEdit.disable();
                                                ucSchedulerLogManagement_btnDelete.disable();" />
                                            <RowDeselect Handler="hdfKeyRecord.reset(); ucSchedulerLogManagement_hdfKeyRecord.reset();" />
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
            <ext:Window runat="server" Title="Thêm lịch sử tiến trình mới" Resizable="true" Layout="FormLayout"
                Padding="6" Width="600" Hidden="true" Icon="UserTick" ID="wdTimeSheetRule"
                Modal="true" Constrain="true" Height="350">
                <Items>
                    <ext:ComboBox runat="server" ID="cbxScheduler" DisplayField="Name"
                        FieldLabel="Tiến trình<span style='color:red;'>*</span>" ValueField="Id"
                        AnchorHorizontal="99%" TabIndex="3" Editable="false" ItemSelector="div.list-item" StoreID="storeScheduler"
                        CtCls="requiredData">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template9" runat="server">
                            <Html>
                                <tpl for=".">
                                <div class="list-item"> 
                                    {Name}
                                </div>
                            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Expand Handler="if(#{cbxScheduler}.store.getCount()==0){#{storeScheduler}.reload();}" />
                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" FieldLabel="Mô tả" ID="txtDescription"
                        AnchorHorizontal="99%" TabIndex="1" />
                    <ext:Checkbox runat="server" ID="chkHasError" FieldLabel="Lỗi" AnchorHorizontal="100%" />
                    <ext:TextArea ID="txtErrorMessage" runat="server" FieldLabel="Message Error" AnchorHorizontal="99%"
                        TabIndex="3" MaxLength="500" />
                    <ext:TextArea ID="txtErrorDescription" runat="server" FieldLabel="Mô tả lỗi" AnchorHorizontal="99%"
                        TabIndex="3" MaxLength="2000" />
                    <ext:Container ID="Container5" runat="server" Layout="Column" Height="40">
                        <Items>
                            <ext:Container ID="Container6" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100"
                                ColumnWidth="0.5">
                                <Items>
                                    <ext:DateField runat="server" FieldLabel="Thời gian bắt đầu" ID="txtStartTime"
                                        AnchorHorizontal="99%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                        RegexText="Định dạng thời gian bắt đầu không đúng" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container7" runat="server" LabelAlign="left" Layout="FormLayout"
                                LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:DateField runat="server" FieldLabel="Thời gian kết thúc" ID="txtEndTime"
                                        AnchorHorizontal="99%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                        RegexText="Định dạng thời gian kết thúc không đúng" />

                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>

                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputSchedulerHistory();" />
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
                            <Click Handler="return checkInputSchedulerHistory();" />
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
