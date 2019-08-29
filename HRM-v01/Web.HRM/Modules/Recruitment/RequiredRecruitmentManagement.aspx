<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequiredRecruitmentManagement.aspx.cs" Inherits="Web.HRM.Modules.Recruitment.RequiredRecruitmentManagement" %>

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
            gpRecruitment.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var validateForm = function () {
            if (txtName.getValue() === '' || txtName.getValue().trim === '') {
                alert('Bạn chưa nhập tên tuyển dụng!');
                return false;
            }
            if (txtCode.getValue() === '' || txtCode.getValue().trim === '') {
                alert('Bạn chưa nhập mã tuyển dụng!');
                return false;
            }

            if (hdfWorkingFormType.getValue() === '' || hdfWorkingFormType.getValue().trim === '') {
                alert('Bạn chưa chọn hình thức làm việc!');
                return false;
            }

            if (txtQuantity.getValue() === '' || txtQuantity.getValue().trim === '') {
                alert('Bạn chưa nhập số lượng!');
                return false;
            }

            if (!dfExpiredDate.getValue()) {
                alert('Bạn chưa nhập hạn nộp hồ sơ!');
                return false;
            }

            if (txtSignerApproved.getValue() === '' || txtSignerApproved.getValue().trim === '') {
                alert('Bạn chưa nhập người duyệt!');
                return false;
            }
            return true;
        }

        
        var handlerRowSelect = function (p) {
            var selections = chkSelectionModel.getSelections();
            if (selections.length > 1) {
                // clear hidden field id
                hdfId.reset();
               
                // disable single edit / delete button
                btnEdit.disable();
                btnDelete.disable();
                
                var ids = selections[0].data.Id;
                for (var i = 1; i < selections.length; i++) {
                    ids = ids + "," + selections[i].data.Id;
                }
                hdfIds.setValue(ids);
            } else {
                
                hdfId.setValue(selections[0].id);
                hdfIds.setValue(selections[0].id);
                
            }

            this.setEnableMenu(selections[0].data.Status);
           
            btnEdit.enable();
            btnDelete.enable();
            if (gpRecruitment.getSelectionModel().getCount() > 1) {
                btnEdit.disable();
                btnDelete.disable();
                mnApproved.enable();
                mnComplete.enable();
                mnRequest.enable();
            }
        }
        
        var handlerRowDeselect = function () {
            if (gpRecruitment.getSelectionModel().getCount() === 0) {
                btnEdit.disable();
                btnDelete.disable();
                mnApproved.disable();
                mnComplete.disable();
                mnRequest.disable();
            }
            if (gpRecruitment.getSelectionModel().getCount() === 1) {
                btnEdit.enable();
                btnDelete.enable();
                var selections = chkSelectionModel.getSelections();
                this.setEnableMenu(selections[0].data.Status);
            }
        }

        var setEnableMenu = function (status) {
            switch (status) {
            case 'Pending':
                mnApproved.enable();
                mnUnApproved.enable();
                mnComplete.disable();
                mnRequest.disable();
                mnCancel.disable();
                break;
            case 'Approved':
                mnApproved.disable();
                mnUnApproved.disable();
                mnRequest.disable();
                mnComplete.enable();
                mnCancel.enable();
                break;
            case 'UnApproved':
                mnRequest.enable();
                mnApproved.disable();
                mnUnApproved.disable();
                mnComplete.disable();
                mnCancel.disable();
                break;
            case 'Complete':
                mnApproved.disable();
                mnUnApproved.disable();
                mnComplete.disable();
                mnRequest.disable();
                mnCancel.disable();
                break;
            case 'Cancel':
                mnApproved.disable();
                mnUnApproved.disable();
                mnComplete.disable();
                mnRequest.disable();
                mnCancel.disable();
                break;
            default:
            }
        }

        var RenderStatus = function(p, value, record) {
            var date = new Date();
            var expiredDate = new Date(record.data.ExpiredDate);

            var font = "font-size:10px !important;";
            var color = '';
            switch (record.data.Status) {
            case 'Pending':
                color = 'blue';
                if (expiredDate < date) {
                    return "<span class='badge' style='background:" + color + ";" + font + "'>" + p + "</span>" + " " + "<span class='badge' style='background:red; font-size:10px !important;' title='Quá hạn'> ! </span>";
                }
                break;
            case 'Approved':
                color = 'darkgreen';
                if (expiredDate < date) {
                    return "<span class='badge' style='background:" + color + ";" + font + "'>" + p + "</span>" + " " + "<span class='badge' style='background:red; font-size:10px !important;' title='Quá hạn'> ! </span>";
                }
                break;
            case 'UnApproved':
                color = 'grey';
                break;
            case 'Complete':
                color = 'darkorange';
                    break;
            case 'Cancel':
                color = 'grey';
                break;
           
            }
            return "<span class='badge' style='background:" + color + ";" + font + "'>" + p + "</span>";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfIds" />
            <ext:Hidden runat="server" ID="hdfOrder" />
           
            <!-- store -->
            <ext:Store runat="server" ID="storeRecruitment" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Recruitment/HandlerRequiredRecruitment.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                    <ext:Parameter Name="status" Value="hdfStatusFilter.getValue()" Mode="Raw" />
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
            <ext:Store ID="storePosition" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Position" Mode="Value" />
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
            <ext:Store ID="storeJobTitlePosition" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_JobTitle" Mode="Value" />
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

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpRecruitment" StoreID="storeRecruitment" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True">
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
                                            <ext:Button runat="server" ID="btnConfig" Text="Tiện ích" Icon="Cog">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" ID="mnApproved" Text="Duyệt" Icon="UserTick" Disabled="True">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWdConfig">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Event" Value="Approved" />
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" ID="mnUnApproved" Text="Không duyệt" Icon="UserCross">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWdConfig">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Event" Value="UnApproved" />
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" ID="mnComplete" Text="Hoàn thành" Icon="UserTick" Disabled="True">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWdConfig">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Event" Value="Complete" />
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" ID="mnCancel" Text="Hủy" Icon="UserDelete" Disabled="True">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWdConfig">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Event" Value="Cancel" />
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem> 
                                                            <ext:MenuItem runat="server" ID="mnRequest" Text="Hoàn duyệt" Icon="ArrowLeft" Disabled="True">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWdConfig">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Event" Value="Request" />
                                                                        </ExtraParams>
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
                                            <ext:Hidden runat="server" ID="hdfStatusFilter" />
                                            <ext:ComboBox runat="server" ID="cboStatusFilter" FieldLabel="Trạng thái" AnchorHorizontal="100%"
                                                          DisplayField="Name" ValueField="Id" LabelWidth="55" Width="200">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeStatusFilter" OnRefreshData="storeStatusFilter_OnRefreshData" AutoLoad="False">
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
                                                    <Expand Handler="if(#{cboStatusFilter}.store.getCount() == 0){storeStatusFilter.reload();}"></Expand>
                                                    <Select Handler="hdfStatusFilter.setValue(cboStatusFilter.getValue());reloadGrid();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
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
                                        <ext:Column ColumnID="Name" Header="Tên tuyển dụng" Width="250" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Code" Header="Mã tuyển dụng" Width="200" Align="Left" DataIndex="Code" />
                                        <ext:Column ColumnID="JobTitlePositionName" Header="Vị trí tuyển dụng" Width="250" Align="Left" DataIndex="JobTitlePositionName" />
                                        <ext:DateColumn ColumnID="ExpiredDate" Header="Hạn nộp hồ sơ" Width="90" DataIndex="ExpiredDate" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="Quantity" Header="Số lượng tuyển" Width="150" Align="Left" DataIndex="Quantity" />
                                        <ext:Column ColumnID="CandidateCount" Header="Ứng tuyển" Width="150" Align="Left" DataIndex="CandidateCount" />
                                        <ext:Column ColumnID="PassedCount" Header="Trúng tuyển" Width="150" Align="Left" DataIndex="PassedCount" />
                                        <ext:Column ColumnID="StatusName" Header="Trạng thái" Width="100" Align="Left" DataIndex="StatusName">
                                            <Renderer Fn="RenderStatus"></Renderer>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkSelectionModel">
                                        <Listeners>
                                            <RowSelect Handler="handlerRowSelect();"></RowSelect>
                                            <RowDeselect Handler="handlerRowDeselect();"></RowDeselect>
                                        </Listeners>
                                    </ext:CheckboxSelectionModel>
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
                                            <Change Handler="chkSelectionModel.clearSelections();btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

            <ext:Window runat="server" ID="wdSetting" Title="Thêm mới yêu cầu tuyển dụng" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:FieldSet runat="server" Title="Thông tin chung" Padding="10">
                        <Items>
                            <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="130">
                                <Items>
                                    <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên tuyển dụng"
                                                AnchorHorizontal="95%" />
                                            <ext:Hidden runat="server" ID="hdfJobTitlePosition" />
                                            <ext:ComboBox runat="server" ID="cboJobTitlePosition" DisplayField="Name" FieldLabel="Vị trí"
                                                ValueField="Id" AnchorHorizontal="95%" Editable="true"
                                                ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                StoreID="storeJobTitlePosition">
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
                                                    <Expand Handler="storeJobTitlePosition.reload();" />
                                                    <Select Handler="this.triggers[0].show(); hdfJobTitlePosition.setValue(cboJobTitlePosition.getValue());" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfJobTitlePosition.reset();}" />
                                                </Listeners>
                                            </ext:ComboBox>

                                            <ext:TextField runat="server" ID="txtWorkPlace" FieldLabel="Nơi làm việc" AnchorHorizontal="95%" />
                                            <ext:TextField runat="server" ID="txtSalaryFrom" FieldLabel="Mức lương từ" AnchorHorizontal="95%" MaxLength="20" MaskRe="/[0-9.]/" />
                                            <ext:TextField runat="server" ID="txtQuantity" FieldLabel="Số lượng" CtCls="requiredData" AnchorHorizontal="95%" MaskRe="/[0-9]/" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container3" runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtCode" CtCls="requiredData" FieldLabel="Mã tuyển dụng"
                                                AnchorHorizontal="100%" />
                                            <ext:Hidden runat="server" ID="hdfWorkingFormType" />
                                            <ext:ComboBox runat="server" ID="cboWorkingFormType" FieldLabel="Hình thức làm việc" CtCls="requiredData" AnchorHorizontal="100%"
                                                DisplayField="Name" ValueField="Id">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeWorkingFormType" OnRefreshData="storeWorkingFormType_OnRefreshData" AutoLoad="False">
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
                                                    <Expand Handler="if(#{cboWorkingFormType}.store.getCount() == 0){storeWorkingFormType.reload();}"></Expand>
                                                    <Select Handler="hdfWorkingFormType.setValue(cboWorkingFormType.getValue());"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:Hidden runat="server" ID="hdfPosition" />
                                            <ext:ComboBox runat="server" ID="cboPosition" DisplayField="Name" FieldLabel="Chức vụ"
                                                ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                StoreID="storePosition">
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
                                                    <Expand Handler="storePosition.reload();" />
                                                    <Select Handler="this.triggers[0].show(); hdfPosition.setValue(cboPosition.getValue());" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfPosition.reset();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:TextField runat="server" ID="txtSalaryTo" FieldLabel="Mức lương đến" AnchorHorizontal="100%" MaxLength="20" MaskRe="/[0-9.]/" />
                                            <ext:DateField runat="server" ID="dfExpiredDate" FieldLabel="Hạn nộp hồ sơ" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%" CtCls="requiredData" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet runat="server" Title="Yêu cầu ứng viên" Padding="10">
                        <Items>
                            <ext:Container runat="server" Layout="ColumnLayout" Height="155">
                                <Items>
                                    <ext:Container runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:NumberField runat="server" ID="txtAgeFrom" FieldLabel="Tuổi từ" AnchorHorizontal="95%" />
                                            <ext:NumberField runat="server" ID="txtHeight" FieldLabel="Chiều cao (Cm)" AnchorHorizontal="95%" />
                                            <ext:Hidden runat="server" ID="hdfSex"/>
                                            <ext:ComboBox ID="cboSex" runat="server" FieldLabel="Giới tính" DisplayField="Name" ValueField="Id"
                                                AnchorHorizontal="95%" Width="68">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeSex" OnRefreshData="storeSexType_OnRefreshData" AutoLoad="False">
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
                                                    <Expand Handler="if(cboSex.store.getCount() == 0){storeSex.reload();}"></Expand>
                                                    <Select Handler="hdfSex.setValue(cboSex.getValue());"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:TextField runat="server" ID="txtSignerApproved" FieldLabel="Người duyệt" CtCls="requiredData" AnchorHorizontal="95%" />
                                            <ext:TextArea runat="server" ID="txtRequirement" FieldLabel="Yêu cầu"
                                                AnchorHorizontal="95%" Height="23" LabelWidth="150" Width="400" EmptyText="Nhập yêu cầu tuyển dụng" />
                                            <ext:Hidden runat="server" ID="hdfExperience" />
                                            <ext:ComboBox runat="server" ID="cboExperience" FieldLabel="Kinh nghiệm" AnchorHorizontal="95%"
                                                          DisplayField="Name" ValueField="Id">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeExperience" OnRefreshData="storeExperience_OnRefreshData" AutoLoad="False">
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
                                                    <Expand Handler="if(#{cboExperience}.store.getCount() == 0){storeExperience.reload();}"></Expand>
                                                    <Select Handler="hdfExperience.setValue(cboExperience.getValue());"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" LabelWidth="120" LabelAlign="left"
                                        Layout="Form" ColumnWidth="0.5">
                                        <Items>
                                            <ext:NumberField runat="server" ID="txtAgeTo" FieldLabel="Tuổi đến" AnchorHorizontal="100%" />
                                            <ext:NumberField runat="server" ID="txtWeight" FieldLabel="Cân nặng (Kg)" AnchorHorizontal="100%" />
                                            <ext:Hidden runat="server" ID="hdfEducationId" />
                                            <ext:ComboBox runat="server" ID="cboEducation" FieldLabel="Trình độ"
                                                DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                LabelWidth="252" Width="422" ItemSelector="div.list-item" PageSize="10">
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
                                                <Store>
                                                    <ext:Store runat="server" ID="storeEducation" AutoLoad="false">
                                                        <Proxy>
                                                            <ext:HttpProxy Method="POST" Url="/Services/Catalog/HandlerCatalogBase.ashx" />
                                                        </Proxy>
                                                        <BaseParams>
                                                            <ext:Parameter Name="objname" Value="cat_Education" Mode="Value" />
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
                                                </Store>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();hdfEducationId.setValue(cboEducation.getValue());" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfEducationId.reset(); };" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả công việc"
                                                AnchorHorizontal="100%" Height="23" LabelWidth="150" Width="400" />
                                            <ext:TextArea runat="server" ID="txtReason" FieldLabel="Lý do" EmptyText="Nhập lý do tuyển dụng"
                                                AnchorHorizontal="100%" Height="23" LabelWidth="150" Width="400" />
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
        </div>
    </form>
</body>
</html>
