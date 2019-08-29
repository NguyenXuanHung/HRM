<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicReportManagement.aspx.cs" Inherits="Web.HRM.Modules.Report.DynamicReportManagement" %>

<%@ Register Src="~/Modules/UC/Resource.ascx" TagPrefix="UC" TagName="Resource" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <UC:Resource runat="server" ID="resource" />
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />    
    <script type="text/javascript">
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() == e.ENTER) {
                reloadGrid();
            }
        };
        var reloadGrid = function () {
            gpReport.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        }
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            return true;
        };
        var RenderStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('lock');
            }
            return '';
        }
        var RenderOrientation = function(value, p, record) {
            return record.data.Orientation === "Portrait"
                ? iconImg('page_white_text')
                : iconImg('page_white_text_width');
        }
        var RenderPaperKind = function (value, p, record) {
            var style = 'font-weight:bold;';
            switch (record.data.PaperKind) {
                case 'A4':
                    style += 'color:darkred';
                    break;
                case 'A3':
                    style += 'color:darkblue';
                    break;
                case 'A2':
                    style += 'color:darkgreen';
                    break;
            }
            return "<div style='" + style + "'>" + value + "</div>";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfReportTemplate" />
            <ext:Hidden runat="server" ID="hdfReportPaperKind" />
            <ext:Hidden runat="server" ID="hdfReportOrientation" />
            <ext:Hidden runat="server" ID="hdfReportSource" />
            <ext:Hidden runat="server" ID="hdfGroupHeader1" />
            <ext:Hidden runat="server" ID="hdfGroupHeader2" />
            <ext:Hidden runat="server" ID="hdfGroupHeader3" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <!-- store -->
            <ext:Store ID="storeReportDynamic" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Report/HandlerReportDynamic.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="ReportSource" />
                            <ext:RecordField Name="Template" />
                            <ext:RecordField Name="PaperKind" />
                            <ext:RecordField Name="PaperKindName" />
                            <ext:RecordField Name="Orientation" />
                            <ext:RecordField Name="OrientationName" />
                            <ext:RecordField Name="Template" />
                            <ext:RecordField Name="TemplateName" />
                            <ext:RecordField Name="GroupHeader1" />
                            <ext:RecordField Name="GroupHeaderName1" />
                            <ext:RecordField Name="GroupHeader2" />
                            <ext:RecordField Name="GroupHeaderName2" />
                            <ext:RecordField Name="GroupHeader3" />
                            <ext:RecordField Name="GroupHeaderName3" />
                            <ext:RecordField Name="ParentDepartment" />
                            <ext:RecordField Name="Department" />
                            <ext:RecordField Name="Title" />
                            <ext:RecordField Name="Duration" />
                            <ext:RecordField Name="CreatedByTitle" />
                            <ext:RecordField Name="CreatedByNote" />
                            <ext:RecordField Name="CreatedByName" />
                            <ext:RecordField Name="ReviewedByTitle" />
                            <ext:RecordField Name="ReviewedByNote" />
                            <ext:RecordField Name="ReviewedByName" />
                            <ext:RecordField Name="SignedByTitle" />
                            <ext:RecordField Name="SignedByNote" />
                            <ext:RecordField Name="SignedByName" />
                            <ext:RecordField Name="ReportDate" />
                            <ext:RecordField Name="FromDate" />
                            <ext:RecordField Name="ToDate" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                            <ext:RecordField Name="IsDeleted" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportTemplate" runat="server" AutoLoad="False" OnRefreshData="storeReportTemplate_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportPaperKind" runat="server" AutoLoad="False" OnRefreshData="storeReportPaperKind_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportOrientation" runat="server" AutoLoad="False" OnRefreshData="storeReportOrientation_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportSource" runat="server" AutoLoad="False" OnRefreshData="storeReportSource_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeGroupHeader" runat="server" AutoLoad="False" OnRefreshData="storeGroupHeader_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeReportStatus" runat="server" AutoLoad="False" OnRefreshData="storeReportStatus_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport runat="server" ID="viewport" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpReport" StoreID="storeReportDynamic" TrackMouseOver="true" Header="false" StripeRows="true" AutoExpandColumn="Description"
                                Border="false" Layout="Fit" AutoScroll="True">
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
                                                            <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnConfig" Text="Cấu hình" Icon="CogEdit" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="RedirectConfig">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
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
                                                            <ext:Parameter Name="Id" Value="rowSelectionModel.getSelected().get('Id')" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnUtil" runat="server" Text="Tiện ích" Icon="Cog" Disabled="True">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" Text="Nhân đôi dữ liệu" Icon="DiskMultiple">
                                                                <DirectEvents>
                                                                    <Click OnEvent="DuplicateData">
                                                                        <EventMask ShowMask="True" Msg="Đang xử lý..."></EventMask>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();reloadGrid();" />
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
                                        <ext:RowNumbererColumn Header="TT" Width="40" Align="Center" />
                                        <ext:Column ColumnID="Name" Header="Tên" Width="300" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Title" Header="Tiêu đề báo cáo" Width="200" Align="Left" DataIndex="Title" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Align="Left" DataIndex="Description" />
                                        <ext:Column ColumnID="TemplateName" Header="Mẫu báo cáo" Width="200" Align="Left" DataIndex="TemplateName" />
                                        <ext:Column ColumnID="PaperKindName" Header="Loại giấy" Width="80" Align="Center" DataIndex="PaperKindName">
                                            <Renderer Fn="RenderPaperKind"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="OrientationName" Header="Chiều" Width="80" Align="Center" DataIndex="OrientationName">
                                            <Renderer Fn="RenderOrientation"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="StatusName" Header="Trạng thái" Width="80" Align="Center" DataIndex="StatusName">
                                            <Renderer Fn="RenderStatus"></Renderer>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnConfig.enable();btnDelete.enable();btnUtil.enable()" />
                                            <RowDeselect Handler="btnEdit.disable();btnConfig.disable();btnDelete.disable();btnUtil.disable()" />
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
                                            <Change Handler="rowSelectionModel.clearSelections();btnEdit.disable();btnConfig.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

            <!-- window -->
            <ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="700" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
                <Items>
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên <span style='color:red;'> * </span>" AnchorHorizontal="100%" />
                    <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" Width="560" Height="100"></ext:TextArea>
                    <ext:ComboBox runat="server" ID="cboReportTemplate" StoreID="storeReportTemplate" FieldLabel="Mẫu báo cáo" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboReportTemplate}.store.getCount()==0){#{storeReportTemplate}.reload();}" />
                            <Select Handler="#{hdfReportTemplate}.setValue(this.getValue());#{storeReportSource}.reload();"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboReportPaperKind" StoreID="storeReportPaperKind" FieldLabel="Loại giấy" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboReportPaperKind}.store.getCount()==0){#{storeReportPaperKind}.reload();}" />
                            <Select Handler="#{hdfReportPaperKind}.setValue(this.getValue());"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboReportOrientation" StoreID="storeReportOrientation" FieldLabel="Chiều" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboReportOrientation}.store.getCount()==0){#{storeReportOrientation}.reload();}" />
                            <Select Handler="#{hdfReportOrientation}.setValue(this.getValue());"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboReportSource" StoreID="storeReportSource" FieldLabel="Nguồn dữ liệu" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboReportSource}.store.getCount()==0){#{storeReportSource}.reload();}" />
                            <Select Handler="hdfReportSource.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboGroupHeader1" StoreID="storeGroupHeader" FieldLabel="Nhóm loại 1" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboGroupHeader1}.store.getCount()==0){#{storeGroupHeader}.reload();}" />
                            <Select Handler="hdfGroupHeader1.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboGroupHeader2" StoreID="storeGroupHeader" FieldLabel="Nhóm loại 2" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboGroupHeader2}.store.getCount()==0){#{storeGroupHeader}.reload();}" />
                            <Select Handler="hdfGroupHeader2.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cboGroupHeader3" StoreID="storeGroupHeader" FieldLabel="Nhóm loại 3" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboGroupHeader3}.store.getCount()==0){#{storeGroupHeader}.reload();}" />
                            <Select Handler="hdfGroupHeader3.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtParentDepartment" FieldLabel="Đơn vị quản lý" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtDepartment" FieldLabel="Đơn vị" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtTitle" FieldLabel="Tiêu đề báo cáo" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtDuration" FieldLabel="Trong khoảng" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtCreatedByTitle" FieldLabel="Người lập" AnchorHorizontal="100%" Text="NGƯỜI LẬP" />
                    <ext:TextField runat="server" ID="txtCreatedByNote" FieldLabel="Ghi chú" AnchorHorizontal="100%" Text="(Ký, ghi rõ họ và tên)" />
                    <ext:TextField runat="server" ID="txtCreatedByName" FieldLabel="Tên người lập" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtReviewedByTitle" FieldLabel="Người duyệt" AnchorHorizontal="100%" Text="NGƯỜI DUYỆT" />
                    <ext:TextField runat="server" ID="txtReviewedByNote" FieldLabel="Ghi chú" AnchorHorizontal="100%" Text="(Chức vụ, ký, ghi rõ họ và tên)" />
                    <ext:TextField runat="server" ID="txtReviewedByName" FieldLabel="Tên người duyệt" AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtSignedByTitle" FieldLabel="Người ký" AnchorHorizontal="100%" Text="THỦ TRƯỞNG CƠ QUAN, ĐƠN VỊ" />
                    <ext:TextField runat="server" ID="txtSignedByNote" FieldLabel="Ghi chú" AnchorHorizontal="100%" Text="(Chức vụ, ký, ghi rõ họ và tên)" />
                    <ext:TextField runat="server" ID="txtSignedByName" FieldLabel="Tên người ký" AnchorHorizontal="100%" />
                    <ext:ComboBox runat="server" ID="cboStatus" StoreID="storeReportStatus" FieldLabel="Trạng thái" DisplayField="Name" ValueField="Id" Width="560">
                        <Listeners>
                            <Expand Handler="if(#{cboStatus}.store.getCount()==0){#{storeReportStatus}.reload();}" />
                            <Select Handler="hdfStatus.setValue(this.getValue())"></Select>
                        </Listeners>
                    </ext:ComboBox>
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
        </div>
    </form>
</body>
</html>
