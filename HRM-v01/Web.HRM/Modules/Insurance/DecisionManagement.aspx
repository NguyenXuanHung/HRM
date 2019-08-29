<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DecisionManagement.aspx.cs" Inherits="Web.HRM.Modules.Insurance.DecisionManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">       
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                // reload grid
                reloadGrid();
                // show keyword trigger
                if (this.getValue() === '')
                    this.triggers[0].hide();
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        };
        var reloadGrid = function () {
            gpDecision.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var validateForm = function () {
            if (hdfChooseEmployee.getValue() === '' || hdfChooseEmployee.getValue().trim === '') {
                alert('Bạn chưa chọn nhân viên!');
                return false;
            }

            if (txtName.getValue() === '' || txtName.getValue().trim === '') {
                alert('Bạn chưa nhập tên quyết định!');
                return false;
            }

            if (hdfReason.getValue() === '' || hdfReason.getValue().trim === '') {
                alert('Bạn chưa nhập lý do!');
                return false;
            }

            if (!dfDecisionDate.getValue()) {
                alert('Bạn chưa nhập ngày quyết định!');
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
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfDepartmentIds" />
            <ext:Hidden runat="server" ID="hdfType" />

             <!-- store -->
            <ext:Store runat="server" ID="storeDecision" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/HumanRecord/HandlerDecision.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="type" Value="hdfType.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="ReasonName" />
                            <ext:RecordField Name="DecisionTypeName" />
                            <ext:RecordField Name="FullName" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="DepartmentName" />
                            <ext:RecordField Name="CreatedDate" />
                            <ext:RecordField Name="CreatedBy" />
                            <ext:RecordField Name="EditedDate" />
                            <ext:RecordField Name="EditedBy" />
                            <ext:RecordField Name="DecisionDate" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeRecord" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HumanRecord/HandlerRecord.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={10}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="departmentIds" Value="hdfDepartmentIds.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="FullName" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="BirthDate" />
                            <ext:RecordField Name="DepartmentName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReasonInsurance" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_ReasonInsurance" Mode="Value" />
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

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpDecision" StoreID="storeDecision" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Command" Value="Update" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear(); reloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="250" Align="Left" DataIndex="FullName" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã NV" Width="100" Align="Left" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="Name" Header="Tên quyết định" Width="200" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="ReasonName" Header="Lý do" Width="200" Align="Left" DataIndex="ReasonName" />
                                        <ext:DateColumn ColumnID="DecisionDate" Header="Ngày quyết định" Width="100" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="100" DataIndex="CreatedBy" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Ngày tạo" Width="100" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();hdfId.reset();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="30" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
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

            <ext:Window runat="server" ID="wdSetting" Title="Thêm mới loại KPI" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Form" Height="400">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfChooseEmployee"/>
                            <ext:ComboBox runat="server" ID="cboEmployee" StoreID="storeRecord" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                          FieldLabel="Tên cán bộ" PageSize="10" HideTrigger="true"
                                          ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                          LoadingText="Đang tải dữ liệu..." AnchorHorizontal="100%">
                                <Template runat="server">
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
                                    <Select Handler="hdfChooseEmployee.setValue(cboEmployee.getValue());"></Select>
                                </Listeners>
                            </ext:ComboBox>

                            <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên quyết định"
                                AnchorHorizontal="100%" />
                            <ext:Hidden runat="server" ID="hdfReason"></ext:Hidden>
                            <ext:ComboBox runat="server" ID="cboReason" StoreID="storeReasonInsurance" DisplayField="Name" ValueField="Id"
                                          ItemSelector="div.list-item" FieldLabel="Lý do" Editable="true" AnchorHorizontal="100%" ListWidth="200" CtCls="requiredData">
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
                                    <Expand Handler="if(#{cboReason}.store.getCount()==0){#{storeReasonInsurance}.reload();}"></Expand>
                                    <Select Handler="hdfReason.setValue(cboReason.getValue()); this.triggers[0].show();"></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfReason.reset();}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:DateField runat="server" ID="dfDecisionDate" FieldLabel="Ngày quyết định" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%" CtCls="requiredData" />
                           
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validateForm();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu.Vui lòng đợi..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Decline">
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
