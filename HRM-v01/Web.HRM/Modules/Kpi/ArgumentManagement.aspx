<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArgumentManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.ArgumentManagement" %>

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
            gpCriterionArgument.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var validateForm = function () {
            if (txtName.getValue() === '' || txtName.getValue().trim === '') {
                alert('Bạn chưa nhập tên tham số!');
                return false;
            }

            if (!cboCalculateCode.getValue()) {
                alert('Bạn chưa chọn mã tính toán!');
                return false;
            }

            if (hdfValueType.getValue() === '' || hdfValueType.getValue().trim === '') {
                alert('Bạn chưa chọn kiểu dữ liệu!');
                return false;
            }

            return true;
        }
        var showTip = function () {
            var rowIndex = gpCriterionArgument.view.findRowIndex(this.triggerElement),
                cellIndex = gpCriterionArgument.view.findCellIndex(this.triggerElement),
                record = storeCriterionArgument.getAt(rowIndex);
            var fieldName = gpCriterionArgument.getColumnModel().getDataIndex(cellIndex);
            if (!isNaN(parseInt(rowIndex)))
                data = record.get(fieldName);
            else
                data = '';
            this.body.dom.innerHTML = data;
        };
        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('cross');
            }
            return '';
        }
        var RenderCalculateCode = function (value, p, record) {
            var style = 'font-size: 10px!important;';
            switch (record.data.ValueType) {
            case 'Number':
                style += "background:green;";
                break;
            case 'String':
                style += "background:cornflowerblue;";
                break;
            case 'Percent':
                style += "background:tomato;";
                break;
            case 'Formula':
                style += "background:gray;";
                break;
            }
            return `<span class='badge' style='${style}'>${value}</span>`;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfCriterionId" />
            <ext:Hidden runat="server" ID="hdfOrder" />

            <!-- store -->
            <ext:Store runat="server" ID="storeCriterionArgument" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Argument" Mode="Value" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="CalculateCode" />
                            <ext:RecordField Name="Name" />
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

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpCriterionArgument" StoreID="storeCriterionArgument" TrackMouseOver="true" Header="True" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True"
                                Title="Thông tin về tham số cho mục tiêu đánh giá">
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
                                            <ext:Button ID="btnConfig" runat="server" Text="Tiện ích " Icon="Cog" Disabled="True" Hidden="True">
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnExcel" Text="Nhập từ Excel" Icon="PageExcel">
                                                <DirectEvents>
                                                    <Click OnEvent="btnExcel_Click"></Click>
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
                                        <ext:Column ColumnID="Name" Header="Tên tham số" Width="200" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Code" Header="Mã tham số" Width="150" Align="Left" DataIndex="Code" />
                                        <ext:Column ColumnID="CalculateCode" Header="Mã tính toán" Width="90" Align="Center" DataIndex="CalculateCode">
                                            <Renderer Fn="RenderCalculateCode"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="150" Align="Left" DataIndex="Description" />
                                        <ext:Column ColumnID="ValueTypeName" Header="Kiểu dữ liệu" Width="90" Align="Center" DataIndex="ValueTypeName">
                                            <Renderer Fn="RenderValueTypeName"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="60" Align="Right" DataIndex="Order" />
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
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnConfig.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();hdfId.reset();btnConfig.disable();" />
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

            <%--tooltips grid--%>
            <ext:ToolTip
                ID="RowTip"
                runat="server"
                Target="={gpCriterionArgument.getView().mainBody}"
                Delegate=".x-grid3-cell"
                TrackMouse="true" AutoWidth="True">
                <Listeners>
                    <Show Fn="showTip" />
                </Listeners>
            </ext:ToolTip>

            <ext:Window runat="server" ID="wdSetting" Title="Thêm mới tham số cho mục tiêu đánh giá" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Form">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên tham số"
                                        AnchorHorizontal="98%" />
                                    <ext:TextField runat="server" ID="txtImportCode" CtCls="requiredData" FieldLabel="Mã tham số"
                                        AnchorHorizontal="98%" Hidden="True" />
                                    <ext:ComboBox runat="server" ID="cboCalculateCode" DisplayField="Code" ValueField="Code" AnchorHorizontal="98%" CtCls="requiredData" FieldLabel="Mã tính toán">
                                        <Store>
                                            <ext:Store runat="server" ID="storeCalculateCode" OnRefreshData="storeCalculateCode_OnRefreshData" AutoLoad="True">
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Code">
                                                        <Fields>
                                                            <ext:RecordField Name="Code" Type="String" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Expand Handler="storeCalculateCode.reload();"></Expand>
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="98%" />
                                    <ext:Hidden runat="server" ID="hdfValueType" />
                                    <ext:ComboBox runat="server" ID="cboValueType" FieldLabel="Loại dữ liệu" CtCls="requiredData" AnchorHorizontal="98%"
                                        DisplayField="Name" ValueField="Id">
                                        <Store>
                                            <ext:Store runat="server" ID="storeValueType" OnRefreshData="storeValueType_OnRefreshData" AutoLoad="False">
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" Mapping="Key" />
                                                            <ext:RecordField Name="Name" Mapping="Value" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Expand Handler="if(#{cboValueType}.store.getCount() == 0){storeValueType.reload();}"></Expand>
                                            <Select Handler="hdfValueType.setValue(cboValueType.getValue());"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" AnchorHorizontal="98%" />
                                    <ext:Checkbox runat="server" FieldLabel="Trạng thái" BoxLabel="Đang sử dụng" ID="chkIsActive" />
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

            <ext:Window ID="wdExcel" runat="server" Layout="FormLayout" Height="600" Width="800" Header="True"
                Hidden="True" Title="Nhập dữ liệu từ Excel" Icon="PageExcel" Padding="6">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" LabelWidth="150">
                        <Items>
                            <ext:Label runat="server" FieldLabel="Tải về tệp tin mẫu"></ext:Label>
                            <ext:Button runat="server" ID="btnDownloadTemplate" Text="Tải về" Icon="ArrowDown" Width="100">
                                <DirectEvents>
                                    <Click OnEvent="DownloadTemplate_Click"></Click>
                                </DirectEvents>
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Nếu bạn chưa có tệp tin Excel mẫu để nhập liệu. Hãy ấn nút này để tải tệp tin mẫu về máy">
                                    </ext:ToolTip>
                                </ToolTips>
                            </ext:Button>
                            <ext:FileUploadField ID="fileExcel" runat="server" FieldLabel="Tệp tin đính kèm<span style='color:red;'>*</span>"
                                CtCls="requiredData" AnchorHorizontal="98%" Icon="Attach">
                            </ext:FileUploadField>
                            <ext:TextField runat="server" ID="txtSheetName" FieldLabel="Tên sheet Excel" AnchorHorizontal="98%" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnImport" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="ImportFile">
                                <EventMask ShowMask="True" Msg="Đang lưu dữ liệu..."></EventMask>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseUpload" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdExcel.hide()"></Click>
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
