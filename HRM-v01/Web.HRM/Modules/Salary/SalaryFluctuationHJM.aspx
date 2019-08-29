<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryFluctuationHJM.aspx.cs" Inherits="Web.HRM.Modules.Salary.SalaryFluctuationHJM" %>

<%@ Register Src="~/Modules/UC/Resource.ascx" TagPrefix="CCVC" TagName="Resource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:Resource runat="server" ID="resource" />
    <script type="text/javascript">
        // handler enter key
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                search();
                reloadGrid();
                if (this.getValue() === '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        };

        // search
        var search = function () {
            if (txtKeyword.getValue() != '') {
                txtKeyword.triggers[0].show();
            }
            reloadGrid();
        };

        var prepare = function (grid, command, record, row, col, value) {
            if (record.data.AttachFileName == '' && command.command == "Download") {
                command.hidden = true;
                command.hideMode = "visibility";
            }
        }

        // reload grid
        var reloadGrid = function () {
            rowSelectionModel.clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        };

        // validate form
        var validateForm = function () {
            if (!txtName.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }

            if (!txtDecisionNumber.getValue()) {
                alert('Bạn chưa nhập số quyết định!');
                return false;
            }

            if (!dfDecisionDate.getValue()) {
                alert('Bạn chưa nhập ngày quyết định');
                return false;
            }

            if (!dfEffectiveDate.getValue()) {
                alert('Bạn chưa nhập ngày hiệu lực');
                return false;
            }

            if (hdfQuantumId.getValue() === '' || hdfQuantumId.getValue() === null) {
                alert('Bạn chưa chọn ngạch lương');
                return false;
            }

            if (!cboSalaryGrade.getValue()) {
                alert('Bạn chưa chọn bậc lương');
                return false;
            }
            return true;
        };

        var validateFormMany = function () {
            if (!txtNameMany.getValue()) {
                alert('Bạn chưa nhập tên!');
                return false;
            }
            if (!txtDecisionNumberMany.getValue()) {
                alert('Bạn chưa nhập số quyết định!');
                return false;
            }

            if (!dfDecisionDateMany.getValue()) {
                alert('Bạn chưa nhập ngày quyết định');
                return false;
            }

            if (!dfEffectiveDateMany.getValue()) {
                alert('Bạn chưa nhập ngày hiệu lực');
                return false;
            }
            if (hdfQuantumIdMany.getValue() === '' || hdfQuantumIdMany.getValue() === null) {
                alert('Bạn chưa chọn ngạch lương');
                return false;
            }

            if (!cboSalaryGradeMany.getValue()) {
                alert('Bạn chưa chọn bậc lương');
                return false;
            }
            if (EmployeeGrid.getSelectionModel().getCount() == 0 && EmployeeGrid.disable == false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }

            return true;
        };
    </script>
</head>
<body>
    <form id="frm" runat="server">
        <div id="main">
            <ext:ResourceManager ID="RM" runat="server" />

            <!-- hidden -->

            <ext:Hidden runat="server" ID="hdfOrder" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <ext:Hidden runat="server" ID="hdfDepartmentIds" />
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfEmployee" />
            <ext:Hidden runat="server" ID="hdfContractId" />
            <ext:Hidden runat="server" ID="hdfPosition" />
            <ext:Hidden runat="server" ID="hdfPositionMany" />
            <ext:Hidden runat="server" ID="hdfAttachFile" />
            <ext:Hidden runat="server" ID="hdfAttachFileMany" />  
            <ext:Hidden runat="server" ID="hdfBasicSalary" /> 
            
            <!-- Store lấy đơn vị theo người dùng đăng nhập -->
            <ext:Store runat="server" ID="storeDepartment" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- store -->
            <ext:Store ID="storeSalaryDecision" AutoSave="true" runat="server" GroupField="EmployeeName">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Salary/HandlerSalaryDecision.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                    <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="RecordId" />
                            <ext:RecordField Name="ContractId" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="DecisionNumber" />
                            <ext:RecordField Name="DecisionDate" />
                            <ext:RecordField Name="EffectiveDate" />
                            <ext:RecordField Name="SignerName" />
                            <ext:RecordField Name="SignerPosition" />
                            <ext:RecordField Name="AttachFileName" />
                            <ext:RecordField Name="Note" />
                            <ext:RecordField Name="GroupQuantumId" />
                            <ext:RecordField Name="QuantumId" />
                            <ext:RecordField Name="Grade" />
                            <ext:RecordField Name="Factor" />
                            <ext:RecordField Name="InsuranceSalary" />
                            <ext:RecordField Name="ContractSalary" />
                            <ext:RecordField Name="GrossSalary" />
                            <ext:RecordField Name="NetSalary" />
                            <ext:RecordField Name="PercentageSalary" />
                            <ext:RecordField Name="PercentageOverGrade" />
                            <ext:RecordField Name="Allowance" />
                            <ext:RecordField Name="NextRaiseDate" />
                            <ext:RecordField Name="Type" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="StatusName" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="EmployeeName" />
                            <ext:RecordField Name="EmployeeSex" />
                            <ext:RecordField Name="EmployeeBirthDate" />
                            <ext:RecordField Name="ContractTypeName" />
                            <ext:RecordField Name="GroupQuantumName" />
                            <ext:RecordField Name="QuantumName" />
                            <ext:RecordField Name="TypeName" />
                            <ext:RecordField Name="BasicSalary" />
                            <ext:RecordField Name="DepartmentName" />
                            <ext:RecordField Name="QuantumCode" />
                            <ext:RecordField Name="Salary" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeStatus" OnRefreshData="storeStatus_OnRefreshData" AutoLoad="False">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
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

            <ext:Store runat="server" ID="storeContractType" AutoLoad="false" OnRefreshData="storeContractType_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="ContractTypeId" DefaultValue="0" />
                            <ext:RecordField Name="ContractTypeName" />
                            <ext:RecordField Name="JobName" />
                            <ext:RecordField Name="ContractNumber" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store ID="storePosition" AutoSave="true" runat="server">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Position" Mode="Value" />
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
        
            <ext:Store ID="storeQuantum" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerQuantum.ashx" />
                </Proxy>
                <Reader>
                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="GroupQuantumId" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Code" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeSalaryGrade" AutoLoad="false" OnRefreshData="storeSalaryGrade_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Code">
                        <Fields>
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store> 
            <ext:Store runat="server" ID="storeSalaryGradeMany" AutoLoad="false" OnRefreshData="storeSalaryGradeMany_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Code">
                        <Fields>
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport runat="server" ID="viewport" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpSalaryDecision" StoreID="storeSalaryDecision" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" Layout="Fit">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Tạo quyết định" Icon="UserTick">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem Text="Tạo quyết định cho một cán bộ" ID="mnuDecisionOne" Icon="User">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWindow">
                                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem Text="Tạo quyết định hàng loạt" Icon="User" ID="mnuDecisionMany">
                                                                <DirectEvents>
                                                                    <Click OnEvent="InitWindowMany">
                                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Id" Value="0" Mode="Value" />
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>

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
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                        <ext:Column ColumnID="AttachFileName" Width="25" DataIndex="" Align="Center">
                                            <Commands>
                                                <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                                    <ToolTip Text="Tải tệp tin đính kèm" />
                                                </ext:ImageCommand>
                                            </Commands>
                                            <PrepareCommand Fn="prepare" />
                                        </ext:Column>
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã CNV" Width="100" Align="Left" DataIndex="EmployeeCode" />
                                        <ext:GroupingSummaryColumn ColumnID="EmployeeName" DataIndex="EmployeeName" Header="Họ tên" Width="200" Sortable="true" Hideable="false" SummaryType="Count">
                                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Quyết định)' : '(1 Quyết định)');" />
                                        </ext:GroupingSummaryColumn>
                                        <ext:Column ColumnID="EmployeeSex" Header="Giới tính" Width="80" Align="Left" DataIndex="EmployeeSex" />
                                        <ext:Column ColumnID="EmployeeBirthDate" Header="Ngày sinh" Width="90" Align="Left" DataIndex="EmployeeBirthDate" />
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column ColumnID="Factor" Header="Hệ số lương" Width="80" Align="Left" DataIndex="Factor" />
                                        <ext:Column ColumnID="Grade" Header="Bậc lương" Width="80" Align="Left" DataIndex="Grade" />
                                        <ext:Column ColumnID="QuantumCode" Header="Mã ngạch" Width="80" Align="Left" DataIndex="QuantumCode" />
                                        <ext:Column ColumnID="Salary" Header="Mức lương" Width="100" Align="Left" DataIndex="Salary">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="BasicSalary" Header="Lương cơ bản" Width="100" Align="Left" DataIndex="BasicSalary">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="DecisionNumber" Header="Số quyết định" Width="100" Align="Left" DataIndex="DecisionNumber" />
                                        <ext:DateColumn ColumnID="DecisionDate" Header="Ngày quyết định" Width="100" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="EffectiveDate" Header="Ngày hiệu lực" Width="100" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="SignerName" Header="Người quyết định" Width="200" Align="Left" DataIndex="SignerName" />
                                        <ext:Column ColumnID="Status" Header="Trạng thái" Width="80" DataIndex="StatusName" Align="Center" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                    <Command Handler="Ext.net.DirectMethods.DownloadAttach(record.data.AttachFileName, {isUpload: true});" />
                                </Listeners>
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

            <!-- window -->
            <ext:Window runat="server" ID="wdSetting" Resizable="true" Layout="FormLayout" Padding="10" Width="900" AutoHeight="True" Hidden="true" Modal="true" Constrain="true">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout">
                        <Items>
                            <ext:FieldSet runat="server" Layout="FormLayout" AnchorHorizontal="100%" Title="Thông tin CNV" BodyStyle="padding-top: 10px;">
                                <Items>
                                    <ext:Container runat="server" Layout="ColumnLayout" Height="50" AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:ComboBox ID="cboEmployee" StoreID="storeRecord" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                                        FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                                                        ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                                        LoadingText="Đang tải dữ liệu..." AnchorHorizontal="98%" runat="server">
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
                                                            <Select Handler="hdfEmployee.setValue(cboEmployee.getValue());"></Select>
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Select OnEvent="cboEmployee_Selected"></Select>
                                                        </DirectEvents>
                                                    </ext:ComboBox>
                                                    <ext:TextField runat="server" ID="txtDepartmentName" FieldLabel="Bộ phận" AnchorHorizontal="98%" Disabled="true" DisabledClass="disabled-input-style" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtPositionName" FieldLabel="Chức vụ" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtJobTitleName" FieldLabel="Chức danh" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Layout="ColumnLayout" Height="380" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.34">
                                <Items>
                                    <ext:FieldSet runat="server" Layout="FormLayout" Title="Quyết định lương gần nhất" AnchorHorizontal="98%" BodyStyle="padding-top: 10px;">
                                        <Items>
                                            <ext:Container runat="server" Layout="FormLayout" Height="340">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtCurrName" FieldLabel="Tên quyết định" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrDecisionNumber" FieldLabel="Số quyết định" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrDecisionDate" FieldLabel="Ngày quyết định" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-control-style" />
                                                    <ext:TextField runat="server" ID="txtCurrSignerName" FieldLabel="Người ký" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrSignerPosition" FieldLabel="Chức vụ" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrContractTypeName" FieldLabel="Loại hợp đồng" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrQuantumCode" FieldLabel="Ngạch" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrGrade" FieldLabel="Bậc lương" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrFactor" FieldLabel="Hệ số" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrSalaryLevel" FieldLabel="Mức lương" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrContractSalary" FieldLabel="Lương hợp đồng" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrInsuranceSalary" FieldLabel="Lương bảo hiểm" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                    <ext:TextField runat="server" ID="txtCurrPercentageLeader" FieldLabel="Phụ cấp chức vụ" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.66">
                                <Items>
                                    <ext:FieldSet runat="server" Title="Thông tin quyết định mới" Layout="FormLayout" AnchorHorizontal="100%" BodyStyle="padding-top: 10px;">
                                        <Items>
                                            <ext:Container runat="server" Layout="FormLayout">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" AnchorHorizontal="100%" FieldLabel="Tên quyết định<span style='color:red'>*</span>" MaxLength="20" LabelWidth="110" />
                                                    <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" Height="78">
                                                        <Items>
                                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.51" LabelWidth="100">
                                                                <Items>
                                                                    <ext:TextField runat="server" ID="txtDecisionNumber" CtCls="requiredData" AnchorHorizontal="98%" FieldLabel="Số quyết định<span style='color:red'>*</span>" />
                                                                    <ext:DateField runat="server" ID="dfDecisionDate" FieldLabel="Ngày quyết định" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="98%" CtCls="requiredData" />
                                                                    <ext:TextField runat="server" ID="txtSignerName" AnchorHorizontal="98%" FieldLabel="Người ký" />
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.49" LabelWidth="100">
                                                                <Items>
                                                                    <ext:ComboBox runat="server" ID="cboContractType" DisplayField="ContractTypeName"
                                                                        ItemSelector="div.list-item" FieldLabel="Loại hợp đồng" Editable="false" ValueField="Id"
                                                                        AnchorHorizontal="100%" TabIndex="2" ListWidth="200" StoreID="storeContractType">
                                                                        <Triggers>
                                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                        </Triggers>
                                                                        <Template runat="server">
                                                                            <Html>
                                                                                <tpl for=".">
						                                                            <div class="list-item"> 
							                                                            <h3>{ContractTypeName}</h3>
                                                                                        Số HĐ: {ContractNumber}<br />
                                                                                        Chức danh: {JobName}
						                                                            </div>
					                                                            </tpl>
                                                                            </Html>
                                                                        </Template>
                                                                        <Listeners>
                                                                            <Focus Handler="#{storeContractType}.reload();" />
                                                                            <Select Handler="hdfContractId.setValue(cboContractType.getValue()); this.triggers[0].show();" />
                                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfContractId.reset();}" />
                                                                        </Listeners>
                                                                    </ext:ComboBox>

                                                                    <ext:DateField runat="server" ID="dfEffectiveDate" FieldLabel="Ngày hiệu lực" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%" CtCls="requiredData" />
                                                                    <ext:ComboBox runat="server" ID="cboPosition" StoreID="storePosition" DisplayField="Name" ValueField="Id"
                                                                        ItemSelector="div.list-item" FieldLabel="Chức vụ người ký" Editable="true" AnchorHorizontal="100%" ListWidth="200">
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
                                                                            <Expand Handler="if(#{cboPosition}.store.getCount()==0){#{storePosition}.reload();}"></Expand>
                                                                            <Select Handler="hdfPosition.setValue(cboPosition.getValue()); this.triggers[0].show();"></Select>
                                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfPosition.reset();}" />
                                                                        </Listeners>
                                                                    </ext:ComboBox>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" Layout="FormLayout">
                                                        <Items>
                                                            <ext:CompositeField ID="cfAttachFile" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp đính kèm">
                                                                <Items>
                                                                    <ext:FileUploadField ID="fufAttachFile" runat="server" EmptyText="Chọn tệp tin" ButtonText="" Icon="Attach" Width="350" />
                                                                    <ext:Button runat="server" ID="btnDownloadAttachFile" Icon="ArrowDown" ToolTip="Tải về">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnDownloadAttachFile_Click" IsUpload="true" />
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                    <ext:Button runat="server" ID="btnDeleteAttachFile" Icon="Delete" ToolTip="Xóa">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnDeleteAttachFile_Click" After="#{fufAttachFile}.reset();">
                                                                                <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?" ConfirmRequest="true" />
                                                                            </Click>
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:CompositeField>
                                                            <ext:TextArea runat="server" ID="txtNote" FieldLabel="Ghi chú" AnchorHorizontal="100%" Height="58" MaxLength="500" />
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                    <ext:FieldSet runat="server" Title="Thông tin lương mới" Layout="FormLayout" AnchorHorizontal="100%" BodyStyle="padding-top: 10px;">
                                        <Items>
                                            <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" Height="100">
                                                <Items>
                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.51">
                                                        <Items>
                                                             <ext:Hidden ID="hdfQuantumId" runat="server" />
                                                            <ext:ComboBox runat="server" ID="cboQuantum" FieldLabel="Ngạch" DisplayField="Name" ValueField="Id" AnchorHorizontal="98%" MinChars="1" StoreID="storeQuantum"
                                                                Editable="true" ItemSelector="div.list-item" CtCls="requiredData">
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
                                                                    <Select Handler="this.triggers[0].show(); hdfQuantumId.setValue(cboQuantum.getValue()); storeSalaryGrade.reload(); if(cboSalaryGrade.getValue() != ''){txtFactor.reset();txtSalaryLevel.reset();Ext.net.DirectMethods.GetInformationSalary();}"></Select>
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfQuantumId.reset();txtFactor.reset();txtSalaryLevel.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtFactor" FieldLabel="Hệ số lương" AnchorHorizontal="98%" MaxLength="5" MaskRe="/[0-9,]/" ReadOnly="True"/>
                                                            <ext:TextField runat="server" ID="txtPercentageSalary" FieldLabel="% hưởng lương" AnchorHorizontal="98%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.49">
                                                        <Items>
                                                            <ext:Hidden ID="hdfSalaryGrade" runat="server" />
                                                            <ext:ComboBox runat="server" ID="cboSalaryGrade" FieldLabel="Bậc lương"
                                                                DisplayField="Name" ValueField="Code" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
                                                                CtCls="requiredData" StoreID="storeSalaryGrade">
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
                                                                    <Focus Handler="storeSalaryGrade.reload();" />
                                                                    <Expand Handler="if (cboQuantum.getValue() == '') { storeSalaryGrade.removeAll(); alert('Bạn phải chọn ngạch trước');} storeSalaryGrade.reload();" />
                                                                    <Select Handler="this.triggers[0].show(); hdfSalaryGrade.setValue(cboSalaryGrade.getValue()); Ext.net.DirectMethods.GetInformationSalary();" ></Select>
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryGrade.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>  
                                                            <ext:TextField runat="server" ID="txtSalaryLevel" FieldLabel="Mức lương" AnchorHorizontal="100%" ReadOnly="True" />
                                                            <ext:TextField runat="server" ID="txtPercentageLeader" FieldLabel="% PC lãnh đạo" AnchorHorizontal="100%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
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
            <ext:Window runat="server" ID="wdSettingMany" Resizable="true" Layout="FormLayout" Padding="5" Width="900" Hidden="true" Modal="true" Constrain="true" Height="600" AutoScroll="True" AutoHeight="True">
                <Items>
                    <ext:Container runat="server" Layout="ColumnLayout" Height="315" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.66">
                                <Items>
                                    <ext:FieldSet runat="server" Title="Thông tin quyết định" Layout="FormLayout" AnchorHorizontal="100%" BodyStyle="padding-top: 5px;">
                                        <Items>
                                            <ext:Container runat="server" Layout="FormLayout">
                                                <Items>
                                                    <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" Height="78">
                                                        <Items>
                                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.51" LabelWidth="100">
                                                                <Items>
                                                                    <ext:TextField runat="server" ID="txtNameMany" CtCls="requiredData" AnchorHorizontal="98%" FieldLabel="Tên quyết định<span style='color:red'>*</span>" MaxLength="20" LabelWidth="110" />
                                                                    <ext:DateField runat="server" ID="dfDecisionDateMany" FieldLabel="Ngày quyết định" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="98%" CtCls="requiredData" />
                                                                    <ext:TextField runat="server" ID="txtSignerNameMany" AnchorHorizontal="98%" FieldLabel="Người ký" />
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.49" LabelWidth="100">
                                                                <Items>
                                                                    <ext:TextField runat="server" ID="txtDecisionNumberMany" CtCls="requiredData" AnchorHorizontal="100%" FieldLabel="Số quyết định<span style='color:red'>*</span>" />
                                                                    <ext:DateField runat="server" ID="dfEffectiveDateMany" FieldLabel="Ngày hiệu lực" Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%" CtCls="requiredData" />
                                                                    <ext:ComboBox runat="server" ID="cboPositionMany" StoreID="storePosition" DisplayField="Name" ValueField="Id"
                                                                        ItemSelector="div.list-item" FieldLabel="Chức vụ người ký" Editable="false" AnchorHorizontal="100%" ListWidth="200">
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
                                                                            <Expand Handler="if(#{cboPositionMany}.store.getCount()==0){#{storePosition}.reload();}"></Expand>
                                                                            <Select Handler=" this.triggers[0].show();hdfPositionMany.setValue(cboPositionMany.getValue());"></Select>
                                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfPositionMany.reset();}" />
                                                                        </Listeners>
                                                                    </ext:ComboBox>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" Layout="FormLayout">
                                                        <Items>
                                                            <ext:CompositeField ID="cfAttachFileMany" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp đính kèm">
                                                                <Items>
                                                                    <ext:FileUploadField ID="fufAttachFileMany" runat="server" EmptyText="Chọn tệp tin" ButtonText="" Icon="Attach" Width="350" />
                                                                    <ext:Button runat="server" ID="btnDownloadAttachFileMany" Icon="ArrowDown" ToolTip="Tải về">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnDownloadAttachFile_ClickMany" IsUpload="true" />
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                    <ext:Button runat="server" ID="btnDeleteAttachFileMany" Icon="Delete" ToolTip="Xóa">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnDeleteAttachFile_ClickMany" After="#{fufAttachFileMany}.reset();">
                                                                                <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?" ConfirmRequest="true" />
                                                                            </Click>
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:CompositeField>
                                                            <ext:TextArea runat="server" ID="txtNoteMany" FieldLabel="Ghi chú" AnchorHorizontal="100%" Height="23" MaxLength="500" />
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                    <ext:FieldSet runat="server" Title="Thông tin lương" Layout="FormLayout" AnchorHorizontal="100%" BodyStyle="padding-top: 5px;">
                                        <Items>
                                            <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" Height="100">
                                                <Items>
                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.51">
                                                        <Items>
                                                             <ext:Hidden ID="hdfQuantumIdMany" runat="server" />
                                                            <ext:ComboBox runat="server" ID="cboQuantumMany" FieldLabel="Ngạch" DisplayField="Name" ValueField="Id" AnchorHorizontal="98%" MinChars="1" StoreID="storeQuantum"
                                                                Editable="true" ItemSelector="div.list-item" CtCls="requiredData">
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
                                                                    <Select Handler="this.triggers[0].show(); hdfQuantumIdMany.setValue(cboQuantumMany.getValue()); storeSalaryGrade.reload(); if(cboSalaryGrade.getValue() != ''){txtFactor.reset();txtSalaryLevel.reset();Ext.net.DirectMethods.GetInformationSalary();}"></Select>
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfQuantumIdMany.reset();txtFactor.reset();txtSalaryLevel.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtFactorMany" FieldLabel="Hệ số" AnchorHorizontal="100%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                            <ext:TextField runat="server" ID="txtPercentageSalaryMany" FieldLabel="% hưởng lương" AnchorHorizontal="98%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.49">
                                                        <Items>
                                                             <ext:Hidden ID="hdfSalaryGradeMany" runat="server" />
                                                            <ext:ComboBox runat="server" ID="cboSalaryGradeMany" FieldLabel="Bậc lương"
                                                                DisplayField="Name" ValueField="Code" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
                                                                CtCls="requiredData" StoreID="storeSalaryGradeMany">
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
                                                                    <Focus Handler="storeSalaryGradeMany.reload();" />
                                                                    <Expand Handler="if (cboQuantumMany.getValue() == '') { storeSalaryGradeMany.removeAll(); alert('Bạn phải chọn ngạch trước');} storeSalaryGradeMany.reload();" />
                                                                    <Select Handler="this.triggers[0].show(); hdfSalaryGradeMany.setValue(cboSalaryGradeMany.getValue()); Ext.net.DirectMethods.GetInformationSalaryMany();" ></Select>
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryGradeMany.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtSalaryLevelMany" FieldLabel="Mức lương" AnchorHorizontal="100%" ReadOnly="True" />
                                                            <ext:TextField runat="server" ID="txtPercentageLeaderMany" FieldLabel="% PC lãnh đạo" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9,]/" />
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Layout="FormLayout" AutoHeight="True">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfDepartmentId" />
                            <ext:ComboBox runat="server" ID="cboDepartment" FieldLabel="Chọn bộ phận" LabelWidth="70" Width="300" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                          ItemSelector="div.list-item" AnchorHorizontal="100%" Editable="false" StoreID="storeDepartment">
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
                                    <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cboDepartment.getValue());
                                             #{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();#{storeEmployees}.reload();" />
                                    <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();#{storeEmployees}.reload();" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:GridPanel runat="server" ID="EmployeeGrid" Icon="UserAdd" Header="true" Title="Chọn nhân viên"
                                AutoExpandColumn="FullName" AnchorHorizontal="100%" Height="250">
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                                </SelectionModel>
                                <ColumnModel runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Width="30" Header="STT" />
                                        <ext:Column Header="Mã CB" Width="60" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ Tên" Width="150" ColumnID="FullName" DataIndex="FullName" />
                                        <ext:DateColumn Header="Ngày sinh" Width="100" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Giới tính" DataIndex="SexName" Width="70" />
                                        <ext:Column Header="Bộ phận" DataIndex="DepartmentName" />
                                        <ext:Column Header="Chức vụ" DataIndex="PositionName" />
                                        <ext:Column Header="Chức danh" DataIndex="JobTitleName" />
                                    </Columns>
                                </ColumnModel>
                                <Store>
                                    <ext:Store ID="storeEmployees" ShowWarningOnFailure="false" runat="server" AutoLoad="True">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={20}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="UcChooseEmployee" />
                                            <ext:Parameter Name="Department" Value="#{cboDepartment}.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="BirthDate" />
                                                    <ext:RecordField Name="SexName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="PositionName" />
                                                    <ext:RecordField Name="JobTitleName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                        <Listeners>
                                            <Load Handler="" />
                                        </Listeners>
                                    </ext:Store>
                                </Store>
                                <SaveMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar2" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                        PageSize="30" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                        DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSaveMany" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validateFormMany();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertMany">
                                <EventMask ShowMask="true" Msg="Đang tải..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancelMany" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdSettingMany.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>

        </div>
    </form>
</body>
</html>