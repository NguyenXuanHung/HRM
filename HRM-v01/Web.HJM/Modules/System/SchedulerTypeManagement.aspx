<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.System.SchedulerTypeManagement" Codebehind="SchedulerTypeManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="/Resource/js/global.js" type="text/javascript"></script>
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
        var checkInputSchedulerType = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên tiến trình!');
                return false;
            }
            if (!txtDisplayName.getValue()) {
                alert('Bạn chưa nhập tên hiển thị tiến trình!');
                return false;
            }
            if (!cbxSchedulerTypeStatus.getValue()) {
                alert('Bạn chưa chọn trạng thái tiến trình!');
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
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
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
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridScheduleType" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTimeSheetRule.show();wdTimeSheetRule.setTitle('Thêm mới loại tiến trình');btnUpdate.hide();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditSchedulerType_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa loại tiến trình này" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="850" />
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
                                    <ext:Store ID="StoreSchedulerType" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="SchedulerType" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="Status" Value="cbxSchedulerStatusFilter.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="DisplayName" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="StatusName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="False" />
                                        <ext:Column ColumnID="Name" Header="Tên loại tiến trình" Width="170" DataIndex="Name"/>
                                        <ext:Column ColumnID="DisplayName" Header="Tên hiển thị" Width="200" DataIndex="DisplayName"/>
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="150" DataIndex="Description" />
                                        <ext:Column ColumnID="Status" Header="Trạng thái" Width="200" DataIndex="StatusName" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1">
                                        <HeaderRows>
                                            <ext:HeaderRow>
                                                <Columns>
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="False">
                                                        <Component>
                                                            <ext:ComboBox runat="server" ID="cbxSchedulerStatusFilter" DisplayField="Name"
                                                                          ValueField="Id" AnchorHorizontal="99%" ListWidth="190" Editable="false" ItemSelector="div.list-item" StoreID="storeSchedulerStatus"
                                                                          LoadingText="Đang tải dữ liệu...">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Template ID="Template6" runat="server">
                                                                    <Html>
                                                                    <tpl for=".">
                                                                        <div class="list-item"> 
                                                                            {Name}
                                                                        </div>
                                                                    </tpl>
                                                                    </Html>
                                                                </Template>
                                                                <Listeners>
                                                                    <Expand Handler="#{cbxSchedulerStatusFilter}.store.reload();" />
                                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); 
                                                                        if (cbxSchedulerStatusFilter.getValue() == '-1') {$('#cbxSchedulerStatusFilter').removeClass('combo-selected');}
                                                                        else {$('#cbxSchedulerStatusFilter').addClass('combo-selected');}"></Select>
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();
                                                                                            #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();
                                                                                            $('#cbxSchedulerStatusFilter').removeClass('combo-selected');}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                </Columns>
                                            </ext:HeaderRow>
                                        </HeaderRows>
                                    </ext:LockingGridView>
                                </View>

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
            <ext:Window runat="server" Title="Thêm loại tiến trình" Resizable="true" Layout="FormLayout"
                Padding="6" Width="600" Hidden="true" Icon="UserTick" ID="wdTimeSheetRule"
                Modal="true" Constrain="true" Height="300">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="40">
                        <Items>
                            <ext:Container ID="Container47" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="100"
                                ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" FieldLabel="Tên tiến trình<span style='color:red;'>*</span>" ID="txtName"
                                        AnchorHorizontal="98%" TabIndex="1" />
                                    
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container48" runat="server" LabelAlign="left" Layout="FormLayout"
                                LabelWidth="100" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" FieldLabel="Tên hiển thị<span style='color:red;'>*</span>" ID="txtDisplayName"
                                        AnchorHorizontal="98%" TabIndex="2" />
                                    
                                </Items>
                            </ext:Container>
                            
                        </Items>
                    </ext:Container>
                    <ext:TextArea ID="txtDescription" runat="server" FieldLabel="Mô tả" AnchorHorizontal="99%"
                                  TabIndex="3" MaxLength="1000"/>
                    <ext:ComboBox runat="server" ID="cbxSchedulerTypeStatus" DisplayField="Name"
                                  FieldLabel="Trạng thái<span style='color:red;'>*</span>" ValueField="Id"
                                  AnchorHorizontal="99%" TabIndex="3" Editable="false" ItemSelector="div.list-item"
                                  CtCls="requiredData" StoreID="storeSchedulerStatus">
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
                        <Listeners>
                            <Expand Handler="if(#{cbxSchedulerTypeStatus}.store.getCount()==0){#{storeSchedulerStatus}.reload();}" />
                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                        </Listeners>
                    </ext:ComboBox>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputSchedulerType();" />
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
                            <Click Handler="return checkInputSchedulerType();" />
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
