<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateManagement.aspx.cs" Inherits="Web.HRM.Modules.Recruitment.CandidateManagement" %>
<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<%@ Register TagPrefix="uc" TagName="InputEmployee" Src="~/Modules/UC/InputEmployee.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript" src="/Resource/js/EmployeeSetting.js"></script>
    <script type="text/javascript">
        var handlerKeyPressEnter = function(f, e) {
            if (e.getKey() === e.ENTER) {
                reloadGrid();
                if (this.getValue() === '')
                    this.triggers[0].hide();
            }
            if (this.getValue() !== '')
                this.triggers[0].show();
        }
        var reloadGrid = function() {
            gpCandidate.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        }
        var RenderDate2 = function (value, p, record) {
            var style = 'align-items: center; ' +
                'display: flex;';
            return "<div style='" + style + "'>" + (value ? iconImg('calendar') + value : '') + "</div>";
        }
        var RenderCandidateType = function(value, p, record) {
            var style = 'font-size: 10px!important;';
            switch (record.data.Status) {
            case 'Interview':
                style += "background:yellowgreen;";
                break;
            case 'TransferRequirement':
                style += "background:green;";
                break;
            case 'Fail':
                style += "background:gray;";
                break;
            case 'Passed':
                style += "background:mediumseagreen;";
                break;
            case 'PassedNotWork':
                style += "background:tomato;";
                break;
            }
            return `<span class='badge' style='${style}'>${value}</span>`;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM"></ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId"></ext:Hidden>
            <ext:Hidden runat="server" ID="hdfRecordId"></ext:Hidden>

            <ext:Store runat="server" ID="storeCandidate">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Recruitment/HandlerCandidate.ashx"></ext:HttpProxy>
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}"/>
                    <ext:Parameter Name="limit" Value="={30}"/>
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="keyword" Value="txtKeyword.getValue()" Mode="Raw"/>
                    <ext:Parameter Name="fromDate" Value="dfStartDate.getRawValue()" Mode="Raw"/>
                    <ext:Parameter Name="toDate" Value="dfEndDate.getRawValue()" Mode="Raw"/>
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
            
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpCandidate" StoreID="storeCandidate" TrackMouseOver="true" Header="False" StripeRows="True" Border="False" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True" AutoExpandColumn="BirthPlace">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Icon="Add" Text="Thêm">
                                                <Listeners>
                                                    <Click Handler="inputEmployee_hdfRecordId.setValue('');inputEmployee_hdfCandidateId.setValue('');ResetForm();"></Click>
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow"></Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Icon="Pencil" Text="Sửa" Disabled="True">
                                                <Listeners>
                                                    <Click Handler="if(CheckSelectedRecord(#{gpCandidate}, #{storeCandidate})) {inputEmployee_hdfEven.setValue('Edit');inputEmployee_hdfRecordId.setValue(#{hdfRecordId}.getValue());inputEmployee_hdfCandidateId.setValue(#{hdfId}.getValue())}"></Click>
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="hdfId.getValue()" Mode="Raw"/>
                                                            <ext:Parameter Name="RecordId" Value="hdfRecordId.getValue()" Mode="Raw"/>
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Icon="Delete" Text="Xóa" Disabled="True">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                                      ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Id" Value="hdfId.getValue()" Mode="Raw"/>
                                                            <ext:Parameter Name="RecordId" Value="hdfRecordId.getValue()" Mode="Raw"/>
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server"/>
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarFill runat="server"/>
                                            <ext:DateField ID="dfStartDate" runat="server" Vtype="daterange" EmptyText="Từ ngày"
                                                Width="140" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();#{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); dfEndDate.setMinValue();this.triggers[0].hide(); #{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();}" />
                                                </Listeners>
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfEndDate}" Mode="Value" />
                                                </CustomConfig>
                                                <ToolTips>
                                                    <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Lọc theo ngày bắt đầu">
                                                    </ext:ToolTip>
                                                </ToolTips>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer2" Width="10" runat="server" />
                                            <ext:DateField ID="dfEndDate" runat="server" Vtype="daterange" EmptyText="Đến ngày"
                                                Width="140" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                                <ToolTips>
                                                    <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Lọc theo ngày kết thúc">
                                                    </ext:ToolTip>
                                                </ToolTips>
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfStartDate}" Mode="Value" />
                                                </CustomConfig>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();#{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset();dfStartDate.setMaxValue(); this.triggers[0].hide(); #{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();}" />
                                                </Listeners>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" Width="10" runat="server" />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="True" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True"></ext:FieldTrigger>
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter"></KeyPress>
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();reloadGrid();"></TriggerClick>
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Icon="Zoom" Text="Tìm kiếm">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center"/>
                                        <ext:Column Header="Mã ứng viên" Width="100" Align="Left" DataIndex="Code"></ext:Column>
                                        <ext:Column Header="Họ tên" Width="250" Align="Left" DataIndex="FullName"></ext:Column>
                                        <ext:Column Header="Giới tính" Width="70" Align="Left" DataIndex="SexName"></ext:Column>
                                        <ext:Column Header="Ngày sinh" Width="100" Align="Left" DataIndex="BirthDate">
                                            <Renderer Fn="RenderDate2"></Renderer>
                                        </ext:Column>
                                        <ext:Column Header="Yêu cầu tuyển dụng" Width="250" Align="Left" DataIndex="RequiredRecruitmentName"></ext:Column>
                                        <ext:Column Header="Trạng thái" Width="200" Align="Center" DataIndex="StatusName">
                                            <Renderer Fn="RenderCandidateType"></Renderer>
                                        </ext:Column>
                                        <ext:Column Header="Ngày nộp" Width="100" Align="Left" DataIndex="ApplyVnDate">
                                            <Renderer Fn="RenderDate2"></Renderer>
                                        </ext:Column>
                                        <ext:Column Header="Nguyên quán" Width="250" Align="Left" DataIndex="BirthPlace"></ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));hdfRecordId.setValue(rowSelectionModel.getSelected().get('RecordId'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();hdfId.reset();hdfRecordId.reset();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="30" DisabledClass="True" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10"/>
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
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            
            <ext:Panel runat="server" ID="Panel1" Height="220" Hidden="True">
                <Content>
                    <uc:InputEmployee ID="inputEmployee" runat="server" OnUserControlClose="inputEmployee_OnUserControlClose" />
                </Content>
            </ext:Panel>
        </div>
    </form>
</body>
</html>
