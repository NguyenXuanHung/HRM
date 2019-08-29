<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.Management.PlanJobTitleManagement" Codebehind="PlanJobTitleManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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

        var checkInput = function () {
            if (hdfEmployeeSelectedId.getValue() == '' || hdfEmployeeSelectedId.getValue().trim == '') {
                alert('Bạn chưa nhập tên cán bộ!');
                return false;
            }
            
            return true;
        }
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
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
            <ext:Hidden runat="server" ID="hdfTypePlanJobTitle" />
            <ext:Hidden runat="server" ID="hdfTypePlanPhase" />
            <ext:Hidden runat="server" ID="hdfBusinessType" />
            <!-- store chức danh quy hoạch -->
            <ext:Store ID="store_PlanJobTitle" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_PlanJobTitle" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            
            <!-- store giai đoạn quy hoạch -->
            <ext:Store ID="store_PlanPhase" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_PlanPhase" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
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
                            <ext:GridPanel ID="gridPlanJob" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdPlanJob.show();wdPlanJob.setTitle('Tạo mới thông tin quy hoạch chức danh');cbxSelectedEmployee.enable();btnUpdate.hide();btnUpdateNew.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditPlanJob_Click">
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
                                    <ext:Store ID="store_PlanJob" AutoSave="true" runat="server" GroupField="FullName">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="PlanJobTitle" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="SexName" />
                                                    <ext:RecordField Name="FolkName" />
                                                    <ext:RecordField Name="PlanJobTitleName" />
                                                    <ext:RecordField Name="PlanPhaseName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:GroupingSummaryColumn ColumnID="FullName" DataIndex="FullName" Header="Họ tên" Width="200" Sortable="true" Hideable="false" SummaryType="Count">
                                             <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Cán bộ)' : '(1 cán bộ)');" />
                                        </ext:GroupingSummaryColumn>
                                        <ext:Column Header="Số hiệu CBCC" Width="150" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Giới tính" Width="100" Align="Left" Locked="true" DataIndex="SexName" />
                                        <ext:Column Header="Dân tộc" Width="100" Align="Left" Locked="true" DataIndex="FolkName" />
                                        <ext:Column Header="Chức danh quy hoạch" Width="300" Align="Left" Locked="true" DataIndex="PlanJobTitleName" />
                                        <ext:Column Header="Giai đoạn quy hoạch" Width="200" Align="Left" Locked="true" DataIndex="PlanPhaseName" />
                                        
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
            <ext:Window runat="server" Title="Tạo mới thông tin quy hoạch chức danh" Resizable="true" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="UserTick" ID="wdPlanJob"
                Modal="true" Constrain="true" Height="450" LabelWidth="150">
                <Items>
                    <ext:Hidden runat="server" ID="hdfEmployeeSelectedId" />
                    <ext:ComboBox ID="cbxSelectedEmployee" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                        FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                        ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                        LoadingText="Đang tải dữ liệu..." AnchorHorizontal="100%" runat="server">
                            <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Store>
                            <ext:Store ID="cbxSelectedEmployee_Store" runat="server" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={10}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="SearchUser" />
                                    <ext:Parameter Name="name" />
                                    <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader Root="plants" TotalProperty="total">
                                        <Fields>
                                            <ext:RecordField Name="FullName" />
                                            <ext:RecordField Name="EmployeeCode" />
                                            <ext:RecordField Name="BirthDate" />
                                            <ext:RecordField Name="DepartmentName" />
                                            <ext:RecordField Name="Id" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Template ID="Template4" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="search-item">
							            <h3>{FullName}</h3>
                                        {EmployeeCode} <br />
                                        <tpl if="BirthDate &gt; ''">{BirthDate:date("d/m/Y")}</tpl><br />
							            {DepartmentName}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Select Handler="hdfEmployeeSelectedId.setValue(cbxSelectedEmployee.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfEmployeeSelectedId.reset(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Hidden runat="server" ID="hdfPlanJobTitleId"/>
                    <ext:ComboBox runat="server" ID="cbx_PlanJobTitle" StoreID="store_PlanJobTitle" FieldLabel="Chức danh quy hoạch" 
                                  DisplayField="Name" ValueField="Id" Width="360" Editable="true" ItemSelector="div.list-item" AnchorHorizontal="100%">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template5" runat="server">
                            <Html>
                            <tpl for=".">
                                <div class="list-item"> 
                                    {Name}
                                </div>
                            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Expand Handler="store_PlanJobTitle.reload();" />
                            <Select Handler="this.triggers[0].show();#{hdfPlanJobTitleId}.setValue(#{cbx_PlanJobTitle}.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfPlanJobTitleId}.reset();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Hidden runat="server" ID="hdfPlanPhaseId"/>
                    <ext:ComboBox runat="server" ID="cbx_PlanPhase" StoreID="store_PlanPhase" FieldLabel="Giai đoạn quy hoạch" 
                                  DisplayField="Name" ValueField="Id" Width="360" Editable="true" ItemSelector="div.list-item" AnchorHorizontal="100%">
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
                            <Expand Handler="store_PlanPhase.reload();" />
                            <Select Handler="this.triggers[0].show();#{hdfPlanPhaseId}.setValue(#{cbx_PlanPhase}.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfPlanPhaseId}.reset();" />
                        </Listeners>
                    </ext:ComboBox>
                   
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                     <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInput();" />
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
                    <ext:Button runat="server" ID="btnClose" Text="Hủy" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdPlanJob.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

