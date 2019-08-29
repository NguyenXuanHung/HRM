<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluationManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.EvaluationManagement" %>
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
            gpEvaluation.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        }
        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('cross');
            }
            return '';
        }
        var RenderCriterion = function (value, p, record) {
            console.log(record);
            return value;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfMonthFilter" />
            <ext:Hidden runat="server" ID="hdfYearFilter" />
            <ext:Hidden runat="server" ID="hdfGroupFilter" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfDepartment" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />

            <!-- store -->
            <ext:Store runat="server" ID="storeEvaluation" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Evaluation" Mode="Value" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="month" Value="hdfMonthFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="year" Value="hdfYearFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="group" Value="hdfGroupFilter.getValue()" Mode="Raw"/>
                    <ext:Parameter Name="departmentIds" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                    <ext:Parameter Name="departments" Value="hdfDepartment.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="RecordId" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="FullName" />
                            <ext:RecordField Name="CriterionDetailModels" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
             <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpEvaluation" StoreID="storeEvaluation" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnEvaluationGroup" Text="Đánh giá KPI" Icon="Calculator">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowEvaluation"></Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnAddKpi" runat="server" Text="Nhập tham số KPI" Icon="UserEdit" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="AddKpiClick">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa tất cả đánh giá" Icon="Delete" >
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <EventMask ShowMask="true" Msg="Đang xử lý..." />
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarFill />
                                            <ext:ComboBox runat="server" ID="cboGroupFilter" Width="150" DisplayField="Name" ValueField="Id" ItemSelector="div.list-item">
                                                <Template runat="server">
                                                    <Html>
                                                    <tpl for=".">
                                                        <div class="list-item"> 
                                                            {Name}
                                                        </div>
                                                    </tpl>
                                                    </Html>
                                                </Template>
                                                <Store>
                                                    <ext:Store runat="server" ID="storeGroup">
                                                        <Proxy>
                                                            <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                                                        </Proxy>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="GroupKpi" Mode="Value" />
                                                            <ext:Parameter Name="Status" Value="hdfStatus.getValue()" Mode="Raw"/>
                                                        </BaseParams>
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="Name" />
                                                                    <ext:RecordField Name="Description" />
                                                                    <ext:RecordField Name="Status"/>
                                                                    <ext:RecordField Name="CreatedDate" />
                                                                    <ext:RecordField Name="CreatedBy" /> 
                                                                    <ext:RecordField Name="EditedDate" />
                                                                    <ext:RecordField Name="EditedBy" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <Listeners>
                                                    <Select Handler="hdfGroupFilter.setValue(cboGroupFilter.getValue());"></Select>
                                                </Listeners>
                                                <DirectEvents>
                                                    <Select OnEvent="ReloadGridColumn"></Select>
                                                </DirectEvents>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ComboBox runat="server" ID="cboMonthFilter" Width="80" Editable="false">
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
                                                    <Select Handler="hdfMonthFilter.setValue(cboMonthFilter.getValue());reloadGrid();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:SpinnerField runat="server" ID="spnYearFilter" Width="80">
                                                <Listeners>
                                                    <Spin Handler="hdfYearFilter.setValue(spnYearFilter.getValue());reloadGrid();" />
                                                </Listeners>
                                            </ext:SpinnerField>
                                            <ext:ToolbarSpacer Width="5" />
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
                                    <Columns/>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="" />
                                            <RowDeselect Handler="" />
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
                                            <Change Handler="rowSelectionModel.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            
            <ext:Window runat="server" ID="wdEvaluation" Title="Đánh giá" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="Column" Height="400">
                        <Items>
                            <ext:Container ID="Container4" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:GridPanel runat="server" ID="gpCriterionEvaluation" AnchorHorizontal="100%" Height="400" AutoExpandColumn="Name"
                                        Title="Danh sách các tiêu chí" Border="True" ClicksToEdit="1">
                                        <Store>
                                            <ext:Store runat="server" ID="storeCriterionByGroup" AutoSave="True" AutoLoad="True">
                                                <Proxy>
                                                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="handlers" Value="Criterion" />
                                                    <ext:Parameter Name="group" Value="hdfGroupFilter.getValue()" Mode="Raw" />
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
                    <ext:Button runat="server" ID="Button2" Text="Đánh giá" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="InsertEvaluationByGroup">
                                <EventMask ShowMask="true" Msg="Đang xử lý dữ liệu.Vui lòng đợi..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button3" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdEvaluation.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
