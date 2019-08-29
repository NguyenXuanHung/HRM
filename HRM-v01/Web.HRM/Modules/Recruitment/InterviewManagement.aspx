<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterviewManagement.aspx.cs" Inherits="Web.HRM.Modules.Recruitment.InterviewManagement" %>

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
            gpInterview.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var validateForm = function () {
            if (txtName.getValue() === '' || txtName.getValue().trim === '') {
                alert('Bạn chưa nhập tên lịch phỏng vấn!');
                return false;
            }
            if (hdfRecruitment.getValue() === '' || hdfRecruitment.getValue().trim === '') {
                alert('Bạn chưa chọn yêu cầu tuyển dụng!');
                return false;
            }

            if (!dfInterviewDate.getValue()) {
                alert('Bạn chưa nhập hạn nộp hồ sơ!');
                return false;
            }
            if (!tfFromTime.getValue()) {
                alert('Bạn chưa nhập từ giờ!');
                return false;
            }
            if (!tfToTime.getValue()) {
                alert('Bạn chưa nhập đến giờ!');
                return false;
            }
            
            if (txtInterviewer.getValue() === '' || txtInterviewer.getValue().trim === '') {
                alert('Bạn chưa nhập người phỏng vấn!');
                return false;
            }
         
            if (fsCandidate.disabled === false) {
                if (hdfCandidate.getValue() === '' || hdfCandidate.getValue().trim === '') {
                    alert('Bạn chưa chọn ứng viên!');
                    return false;
                }
                if (!tfInterview.getValue()) {
                    alert('Bạn chưa nhập giờ phỏng vấn!');
                    return false;
                }
            }

           
            return true;
        }

        var RenderDateIcon = function (value, p, record) {
            var style = 'align-items: center; ' +
                'display: flex;';
            return "<div style='" + style + "'>" + (value ? iconImg('calendar') + value : '') + "</div>";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfOrder" />
            <ext:Hidden runat="server" ID="hdfDepartmentIds" /> 
            <ext:Hidden runat="server" ID="hdfCandidateType" />
            <ext:Hidden runat="server" ID="hdfRequiredRecruitmentStatus" />

            <!-- store -->
            <ext:Store runat="server" ID="storeInterview" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Recruitment/HandlerInterview.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />

                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="RequiredRecruitmentName" />
                            <ext:RecordField Name="TimeDisplay" />
                            <ext:RecordField Name="InterviewDate" />
                            <ext:RecordField Name="InterviewVnDate" />
                            <ext:RecordField Name="Interviewer" />
                            <ext:RecordField Name="CandidateCount" />
                            <ext:RecordField Name="CreatedDate" />
                            <ext:RecordField Name="CreatedBy" />
                            <ext:RecordField Name="EditedDate" />
                            <ext:RecordField Name="EditedBy" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeRecruitment" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Recruitment/HandlerRequiredRecruitment.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="status" Value="#{hdfRequiredRecruitmentStatus}.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="JobTitlePositionName" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                            <ext:RecordField Name="Quantity" />
                            <ext:RecordField Name="ExpiredDate" />
                            <ext:RecordField Name="WorkPlace" />
                            <ext:RecordField Name="Requirement" />
                            <ext:RecordField Name="CreatedDate" />
                            <ext:RecordField Name="CreatedBy" />
                            <ext:RecordField Name="EditedDate" />
                            <ext:RecordField Name="EditedBy" />
                            <ext:RecordField Name="PassedCount" />
                            <ext:RecordField Name="CandidateCount" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeCandidate" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Recruitment/HandlerCandidate.ashx"></ext:HttpProxy>
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="candidateType" Value="hdfCandidateType.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id"/>
                            <ext:RecordField Name="RecordId"/>
                            <ext:RecordField Name="Code"/>
                            <ext:RecordField Name="RequiredRecruitmentId"/>
                            <ext:RecordField Name="RequiredRecruitmentName"/>
                            <ext:RecordField Name="DesiredSalary"/>
                            <ext:RecordField Name="ApplyDate"/>
                            <ext:RecordField Name="ApplyVnDate"/>
                            <ext:RecordField Name="Status"/>
                            <ext:RecordField Name="StatusName"/>
                            <ext:RecordField Name="IsDeleted"/>
                            <ext:RecordField Name="FullName"/>
                            <ext:RecordField Name="EmployeeCode"/>
                            <ext:RecordField Name="SexName"/>
                            <ext:RecordField Name="DepartmentName"/>
                            <ext:RecordField Name="BirthDate"/>
                            <ext:RecordField Name="BirthPlace"/>
                            <ext:RecordField Name="CreatedBy"/>
                            <ext:RecordField Name="CreatedDate"/>
                            <ext:RecordField Name="EditedBy"/>
                            <ext:RecordField Name="EditedDate"/>
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpInterview" StoreID="storeInterview" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True">
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
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button runat="server" ID="btnCog" Text="Tiện ích" Icon="Cog" Disabled="true">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" ID="mnImportExcel" Text="Nhập thông tin từ Excel" Icon="PageExcel">
                                                                <Listeners>
                                                                    <Click Handler="#{wdImportExcelFile}.show();" />
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" ID="mnCandidate" Text="Danh sách ứng viên" Icon="Table" >
                                                                <DirectEvents>
                                                                    <Click OnEvent="CandidateDetail">
                                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" ID="mnCandidateDelete" Text="Xóa danh sách ứng viên" Icon="UserDelete" >
                                                                <DirectEvents>
                                                                    <Click OnEvent="CandidateDelete">
                                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                  
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>

                                            <ext:ToolbarFill />
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
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                        <ext:Column ColumnID="Name" Header="Tên lịch" Width="300" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="TimeDisplay" Header="Thời gian" Width="120" Align="Left" DataIndex="TimeDisplay" />
                                        <ext:Column ColumnID="InterviewVnDate" Header="Ngày phỏng vấn" Width="120" DataIndex="InterviewVnDate">
                                            <Renderer Fn="RenderDateIcon"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="RequiredRecruitmentName" Header="Yêu cầu tuyển dụng" Width="250" Align="Left" DataIndex="RequiredRecruitmentName" />
                                        <ext:Column ColumnID="CandidateCount" Header="Số lượng ứng viên" Width="150" Align="Left" DataIndex="CandidateCount" />
                                        <ext:Column ColumnID="Interviewer" Header="Người phỏng vấn" Width="200" Align="Left" DataIndex="Interviewer" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnCog.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();hdfId.reset();btnCog.disable();" />
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
            <ext:Window runat="server" ID="wdSetting" Title="Thêm mới lịch phỏng vấn" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:FieldSet runat="server" Title="Thông tin chung" Padding="10">
                        <Items>
                            <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="75">
                                <Items>
                                    <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên lịch phỏng vấn"
                                                AnchorHorizontal="95%" />
                                            <ext:DateField runat="server" ID="dfInterviewDate" FieldLabel="Ngày phỏng vấn" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="95%" CtCls="requiredData" />
                                            <ext:TimeField ID="tfFromTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="95%" EmptyText="Giờ" CtCls="requiredData"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Từ giờ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>

                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container3" runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfRecruitment" />
                                            <ext:ComboBox runat="server" ID="cboRecruitment" DisplayField="Name" FieldLabel="Yêu cầu tuyển dụng"
                                                ValueField="Id" AnchorHorizontal="100%" Editable="true" CtCls="requiredData"
                                                ItemSelector="div.list-item" MinChars="1" PageSize="10"
                                                StoreID="storeRecruitment">
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
                                                    <Expand Handler="storeRecruitment.reload();" />
                                                    <Select Handler="this.triggers[0].show(); hdfRecruitment.setValue(cboRecruitment.getValue());" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfRecruitment.reset();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:TextField runat="server" ID="txtInterviewer" FieldLabel="Người phỏng vấn" AnchorHorizontal="100%" CtCls="requiredData" />
                                            <ext:TimeField ID="tfToTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="100%" EmptyText="Giờ" CtCls="requiredData"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Đến giờ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>

                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                     <ext:FieldSet runat="server" Title="Ứng viên tham gia" Padding="10" Disabled="False" ID="fsCandidate">
                        <Items>
                            <ext:Container runat="server" Layout="Column" Height="50">
                                <Items>
                                    <ext:Container ID="Container4" runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfCandidate" />
                                            <ext:ComboBox ID="cboEmployee" StoreID="storeCandidate" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                                PageSize="10" HideTrigger="true" FieldLabel="Chọn ứng viên"
                                                ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                                LoadingText="Đang tải dữ liệu..." AnchorHorizontal="95%" runat="server">
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
                                                    <Select Handler="hdfCandidate.setValue(cboEmployee.getValue());"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container5" runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TimeField ID="tfInterview" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="100%" EmptyText="Giờ" CtCls="requiredData"
                                                MaskRe="/[0-9:]/" FieldLabel="Giờ phỏng vấn">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
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
             <ext:Window runat="server" Title="Nhập dữ liệu từ file excel" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdImportExcelFile"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" LabelWidth="150">
                        <Items>
                            <ext:Button runat="server" ID="btnDownloadTemplate" Icon="ArrowDown" ToolTip="Tải về" Text="Tải về" Width="100" FieldLabel="Tải tệp tin mẫu">
                                <DirectEvents>
                                    <Click OnEvent="DownloadTemplate_Click" IsUpload="true" />
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
                            <ext:TextField runat="server" ID="txtFromRow" FieldLabel="Từ hàng" AnchorHorizontal="98%" MaskRe="/[0-9.]/" Hidden="true" />
                            <ext:TextField runat="server" ID="txtToRow" FieldLabel="Đến hàng" AnchorHorizontal="98%" MaskRe="/[0-9.]/" Hidden="true" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateImportExcel" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="btnUpdateImportExcel_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>

                    <ext:Button runat="server" ID="btnCloseImport" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdImportExcelFile.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
