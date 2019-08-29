<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupKpiManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.GroupKpiManagement" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
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
            gpGroupKpi.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var validateForm = function () {
            if (txtName.getValue() == '' || txtName.getValue().trim == '') {
                alert('Bạn chưa nhập tên nhóm!');
                return false;
            }
          
            if (gpCriterion.getSelectionModel().getCount() === 0 && gpCriterion.disabled === false) {
                alert("Bạn phải chọn ít nhất một tiêu chí");
                return false;
            }

            return true;
        }

        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('cross');
            }
            return '';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfStatus" />

            <!-- store -->
            <ext:Store runat="server" ID="storeGroupKpi" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GroupKpi" Mode="Value" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="CreatedDate" />
                            <ext:RecordField Name="CreatedBy" />
                            <ext:RecordField Name="EditedDate" />
                            <ext:RecordField Name="EditedBy" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpGroupKpi" StoreID="storeGroupKpi" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True" AutoExpandColumn="Name">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="chkCriterionRowSelection.clearSelections();"></Click>
                                                </Listeners>
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
                                            <ext:Button runat="server" ID="btnCriterion" Text="Mục tiêu" Icon="PageWhiteText" Disabled="True">
                                                <Listeners>
                                                    <Click Handler="storeCriterionByGroup.reload();wdCriterion.show();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
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
                                        <ext:Column ColumnID="Name" Header="Tên loại KPI" Width="250" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="150" Align="Left" DataIndex="Description" />
                                        <ext:Column ColumnID="Status" Width="100" Header="Trạng thái" Align="Center" DataIndex="Status">
                                            <Renderer Fn="RenderRowStatus" />
                                        </ext:Column>
                                        <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="150" DataIndex="CreatedBy" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Ngày tạo" Width="150" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="EditedDate" Header="Ngày cập nhật" Width="150" DataIndex="EditedDate" Format="dd/MM/yyyy" Hidden="True" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnCriterion.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();hdfId.reset();btnCriterion.disable();" />
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
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên loại KPI"
                                        AnchorHorizontal="100%" />
                                    <ext:TextField runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="100%" Margins="bottom: 10" />
                                    <ext:Checkbox runat="server" FieldLabel="Trạng thái" BoxLabel="Đang sử dụng" ID="chkIsActive" />

                                    <ext:GridPanel runat="server" ID="gpCriterion" AnchorHorizontal="100%" Height="300" AutoExpandColumn="Name"
                                        Title="Danh sách các tiêu chí" Border="True" ClicksToEdit="1">
                                        <Store>
                                            <ext:Store runat="server" ID="storeCriterion" AutoSave="True" AutoLoad="True">
                                                <Proxy>
                                                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="Criterion" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Code" />
                                                            <ext:RecordField Name="Name" />
                                                            <ext:RecordField Name="Formula" />
                                                            <ext:RecordField Name="Description" />
                                                            <ext:RecordField Name="Status" />
                                                            <ext:RecordField Name="ValueType" />
                                                            <ext:RecordField Name="ValueTypeName" />
                                                            <ext:RecordField Name="Order" />
                                                            <ext:RecordField Name="CreatedDate" />
                                                            <ext:RecordField Name="CreatedBy" />
                                                            <ext:RecordField Name="EditedDate" />
                                                            <ext:RecordField Name="EditedBy" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                                <ext:Column ColumnID="Code" Header="Mã tiêu chí" Width="100" Align="Left" DataIndex="Code" Hidden="True" />
                                                <ext:Column ColumnID="Name" Header="Tên tiêu chí" Width="250" Align="Left" DataIndex="Name" />
                                                <ext:Column ColumnID="Formula" Header="Công thức" Width="200" Align="Left" DataIndex="Formula" />
                                                <ext:Column ColumnID="Description" Header="Mô tả" Width="100" Align="Left" DataIndex="Description" />
                                                <ext:Column ColumnID="ValueTypeName" Header="Kiểu dữ liệu" Width="80" Align="Left" DataIndex="ValueTypeName" />
                                                <ext:Column ColumnID="Status" Width="100" Header="Trạng thái" Align="Center" DataIndex="Status">
                                                    <Renderer Fn="RenderRowStatus" />
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:CheckboxSelectionModel ID="chkCriterionRowSelection" runat="server" SingleSelect="false">
                                            </ext:CheckboxSelectionModel>
                                        </SelectionModel>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar2" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                                PageSize="20" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                                DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                            </ext:PagingToolbar>
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
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
                    <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdSetting.hide();chkCriterionRowSelection.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        
            <ext:Window runat="server" ID="wdCriterion" Title="Mục tiêu đánh giá" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container4" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:GridPanel runat="server" ID="gpCriterionEvaluation" AnchorHorizontal="100%" Height="400" AutoExpandColumn="Name"
                                        Title="Danh sách các mục tiêu" Border="True" ClicksToEdit="1">
                                        <Store>
                                            <ext:Store runat="server" ID="storeCriterionByGroup" AutoSave="True" AutoLoad="True">
                                                <Proxy>
                                                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="Criterion" />
                                                    <ext:Parameter Name="group" Value="hdfId.getValue()" Mode="Raw" />
                                                </BaseParams>
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" />
                                                            <ext:RecordField Name="Code" />
                                                            <ext:RecordField Name="Name" />
                                                            <ext:RecordField Name="Formula" />
                                                            <ext:RecordField Name="Description" />
                                                            <ext:RecordField Name="Status" />
                                                            <ext:RecordField Name="ValueType" />
                                                            <ext:RecordField Name="ValueTypeName" />
                                                            <ext:RecordField Name="Order" />
                                                            <ext:RecordField Name="CreatedDate" />
                                                            <ext:RecordField Name="CreatedBy" />
                                                            <ext:RecordField Name="EditedDate" />
                                                            <ext:RecordField Name="EditedBy" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                                <ext:Column ColumnID="Code" Header="Mã tiêu chí" Width="100" Align="Left" DataIndex="Code" Hidden="True" />
                                                <ext:Column ColumnID="Name" Header="Tên tiêu chí" Width="250" Align="Left" DataIndex="Name" />
                                                <ext:Column ColumnID="Description" Header="Mô tả" Width="100" Align="Left" DataIndex="Description" />
                                                <ext:Column ColumnID="Formula" Header="Công thức" Width="200" Align="Left" DataIndex="Formula" />
                                                <ext:Column ColumnID="ValueTypeName" Header="Kiểu dữ liệu" Width="80" Align="Left" DataIndex="ValueTypeName" />
                                                <ext:Column ColumnID="Status" Width="100" Header="Trạng thái" Align="Center" DataIndex="Status">
                                                    <Renderer Fn="RenderRowStatus" />
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar1" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                                PageSize="20" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                                DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                            </ext:PagingToolbar>
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="Button3" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdCriterion.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
