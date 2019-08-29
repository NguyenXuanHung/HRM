<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryFluctuationHRM.aspx.cs" Inherits="Web.HRM.Modules.Salary.SalaryFluctuationHRM" %>

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

            if (EmployeeGrid.getSelectionModel().getCount() == 0 && EmployeeGrid.disable == false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }

            return true;
        };

        var afterEdit = function (e) {
            /*
            Properties of 'e' include:
                e.grid - This grid
                e.record - The record being edited
                e.field - The field name being edited
                e.value - The value being set
                e.originalValue - The original value for the field, before the edit.
                e.row - The grid row index
                e.column - The grid column index
            */
            if (!e.record.data.Factor)
                e.record.data.Factor = 0;
            if (!e.record.data.Percent)
                e.record.data.Percent = 0;
            if (!e.record.data.Value)
                e.record.data.Value = 0;
            Ext.net.DirectMethods.SaveEditData(e.record.data);
            console.log(hdfEditData.getValue());
        }

        var iconImg = function (name) {
            return "<img src='/Resource/icon/" + name + ".png'>";
        }

        var RenderTaxable = function (value, p, record) {
            if (value === 1) {
                return iconImg('tick');
            }
            return iconImg('cross');
        }

        var RenderStatus = function (value, p, record) {
            switch (record.data.Status) {
                case 'Pending':
                    return iconImg('time');
                case 'Approved':
                    return iconImg('tick');
                case 'Paused':
                    return iconImg('pause_green');
                case 'Locked':
                    return iconImg('lock');
            }
        }

        var RenderDate = function (value, p, record) {
            var style = 'align-items: center; ' +
                        'display: flex;';
            return "<div style='" + style + "'>" + (value ? iconImg('calendar') + value : '') + "</div>";
        }

        var RenderSex = function(value, p, record) {
            return value === "Nam" ? iconImg('male') : iconImg('female');
        }
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
            <ext:Hidden runat="server" ID="hdfEditData" />

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
                            <ext:RecordField Name="DecisionVnDate" />
                            <ext:RecordField Name="EffectiveDate" />
                            <ext:RecordField Name="EffectiveVnDate" />
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
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeAllowanceCatalog" OnRefreshData="storeAllowanceCatalog_OnRefreshData" AutoLoad="False">
                <Reader>
                    <ext:JsonReader IDProperty="AllowanceCode">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="SalaryDecisionId" />
                            <ext:RecordField Name="AllowanceCode" />
                            <ext:RecordField Name="AllowanceName" />
                            <ext:RecordField Name="Factor" DefaultValue="0" />
                            <ext:RecordField Name="Percent" DefaultValue="0" />
                            <ext:RecordField Name="Value" DefaultValue="0" />
                            <ext:RecordField Name="Taxable" />
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

            <ext:Store runat="server" ID="storeInsuranceType" OnRefreshData="storeInsuranceType_OnRefreshData" AutoLoad="False">
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
                                                                <Listeners>
                                                                    <Click Handler="storeAllowanceCatalog.rejectChanges();storeAllowanceCatalog.reload();"></Click>
                                                                </Listeners>
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
                                                <Listeners>
                                                    <Click Handler="storeAllowanceCatalog.rejectChanges();storeAllowanceCatalog.reload();"></Click>
                                                </Listeners>
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
                                        <ext:Column ColumnID="EmployeeSex" Header="Giới tính" Width="80" Align="Center" DataIndex="EmployeeSex">
                                            <Renderer Fn="RenderSex"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="EmployeeBirthDate" Header="Ngày sinh" Width="90" Align="Left" DataIndex="EmployeeBirthDate">
                                            <Renderer Fn="RenderDate"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column ColumnID="Factor" Header="Hệ số lương" Width="80" Align="Left" DataIndex="Factor" />
                                        <ext:Column ColumnID="BasicSalary" Header="Lương cơ bản" Width="100" Align="Left" DataIndex="BasicSalary">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="ContractSalary" Header="Lương hợp đồng" Width="100" Align="Left" DataIndex="ContractSalary">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="GrossSalary" Header="Lương Gross" Width="100" Align="Left" DataIndex="GrossSalary">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="NetSalary" Header="Lương Net" Width="100" Align="Left" DataIndex="NetSalary">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="DecisionNumber" Header="Số quyết định" Width="100" Align="Left" DataIndex="DecisionNumber" />
                                        <ext:Column ColumnID="DecisionDate" Header="Ngày quyết định" Width="100" Align="Left" DataIndex="DecisionVnDate">
                                            <Renderer Fn="RenderDate"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="EffectiveDate" Header="Ngày hiệu lực" Width="100" Align="Left" DataIndex="EffectiveVnDate">
                                            <Renderer Fn="RenderDate"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="SignerName" Header="Người quyết định" Width="200" Align="Left" DataIndex="SignerName" />
                                        <ext:Column ColumnID="Status" Header="Trạng thái" Width="80" DataIndex="StatusName" Align="Center">
                                            <Renderer Fn="RenderStatus"></Renderer>
                                        </ext:Column>
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
                    <ext:TabPanel Border="false" runat="server" Cls="bkGround" Padding="6" ID="tab_Employee"
                        Height="550" DeferredRender="false">
                        <Items>
                            <ext:Panel ID="panelEmployee" Title="Quyết định lương" runat="server"
                                AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="0">
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
                                    <ext:Container runat="server" Layout="ColumnLayout" Height="420" AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.34">
                                                <Items>
                                                    <ext:FieldSet runat="server" Layout="FormLayout" Title="Quyết định lương gần nhất" AnchorHorizontal="98%" BodyStyle="padding-top: 10px;">
                                                        <Items>
                                                            <ext:Container runat="server" Layout="FormLayout" Height="365">
                                                                <Items>
                                                                    <ext:TextField runat="server" ID="txtCurrName" FieldLabel="Tên quyết định" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrDecisionNumber" FieldLabel="Số quyết định" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrDecisionDate" FieldLabel="Ngày quyết định" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-control-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrSignerName" FieldLabel="Người ký" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrSignerPosition" FieldLabel="Chức vụ" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrContractTypeName" FieldLabel="Loại hợp đồng" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrBasicSalary" FieldLabel="Lương cơ bản" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrFactor" FieldLabel="Hệ số" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrGrossSalary" FieldLabel="Lương GROSS" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrNetSalary" FieldLabel="Lương NET" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrContractSalary" FieldLabel="Lương hợp đồng" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrInsuranceSalary" FieldLabel="Lương bảo hiểm" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                    <ext:TextField runat="server" ID="txtCurrPercentageLeader" FieldLabel="% PC chức vụ" AnchorHorizontal="100%" Disabled="true" DisabledClass="disabled-input-style" />
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
                                                                            <ext:TextArea runat="server" ID="txtNote" FieldLabel="Ghi chú" AnchorHorizontal="100%" Height="32" MaxLength="500" />
                                                                        </Items>
                                                                    </ext:Container>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:FieldSet>
                                                    <ext:FieldSet runat="server" Title="Thông tin lương mới" Layout="FormLayout" AnchorHorizontal="100%" BodyStyle="padding-top: 10px;">
                                                        <Items>
                                                            <ext:Container runat="server" Layout="ColumnLayout" AnchorHorizontal="100%" Height="153">
                                                                <Items>
                                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.51">
                                                                        <Items>
                                                                            <ext:TextField runat="server" ID="txtBasicSalary" FieldLabel="Lương cơ bản" AnchorHorizontal="98%" Disabled="true" DisabledClass="disabled-input-style" />
                                                                            <ext:TextField runat="server" ID="txtGrossSalary" FieldLabel="Lương GROSS" AnchorHorizontal="98%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                                            <ext:TextField runat="server" ID="txtContractSalary" FieldLabel="Lương hợp đồng" AnchorHorizontal="98%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                                            <ext:TextField runat="server" ID="txtPercentageSalary" FieldLabel="% hưởng lương" AnchorHorizontal="98%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                                            <ext:TextField runat="server" ID="txtPercentageOverGrade" FieldLabel="% vượt khung" AnchorHorizontal="98%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                                        </Items>
                                                                    </ext:Container>
                                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.49">
                                                                        <Items>
                                                                            <ext:TextField runat="server" ID="txtFactor" FieldLabel="Hệ số" AnchorHorizontal="100%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                                            <ext:TextField runat="server" ID="txtNetSalary" FieldLabel="Lương NET" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                                            <ext:TextField runat="server" ID="txtInsuranceSalary" FieldLabel="Lương bảo hiểm" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                                            <ext:TextField runat="server" ID="txtPercentageLeader" FieldLabel="% PC chức vụ" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9,]/" />
                                                                            <ext:Hidden runat="server" ID="hdfInsuranceType"></ext:Hidden>
                                                                            <ext:ComboBox runat="server" ID="cboInsuranceType" StoreID="storeInsuranceType" FieldLabel="Loại bảo hiểm" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%">
                                                                                <Listeners>
                                                                                    <Expand Handler="if(#{cboInsuranceType}.store.getCount()==0){#{storeInsuranceType}.reload();}"></Expand>
                                                                                    <Select Handler="hdfInsuranceType.setValue(this.getValue()); cboReason.show();"></Select>
                                                                                </Listeners>
                                                                            </ext:ComboBox>
                                                                            <ext:Hidden runat="server" ID="hdfReason"></ext:Hidden>
                                                                            <ext:ComboBox runat="server" ID="cboReason" StoreID="storeReasonInsurance" DisplayField="Name" ValueField="Id"
                                                                                ItemSelector="div.list-item" FieldLabel="Lý do" Editable="true" AnchorHorizontal="100%" ListWidth="200" Hidden="True">
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
                            </ext:Panel>
                            <ext:Panel ID="panelAllowance" Title="Phụ cấp" runat="server"
                                AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="1">
                                <Items>
                                    <ext:GridPanel runat="server" ID="gpSalaryAllowance" AutoExpandColumn="AllowanceName" TrackMouseOver="True" Header="False" StripeRows="True" StoreID="storeAllowanceCatalog" Height="500" Width="850">
                                        <TopBar>
                                            <ext:Toolbar runat="server">
                                                <Items>
                                                    <ext:Button runat="server" ID="btnExcel" Icon="PageExcel" Text="Nhập từ Excel">
                                                        <Listeners>
                                                            <Click Handler="wdExcel.show();Ext.net.DirectMethods.ResetExcelForm();"></Click>
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Listeners>
                                            <AfterEdit Fn="afterEdit"></AfterEdit>
                                        </Listeners>
                                        <ColumnModel>
                                            <Columns>
                                                <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                                <ext:Column ColumnID="AllowanceName" Width="200" DataIndex="AllowanceName" Header="Tên phụ cấp"></ext:Column>
                                                <ext:Column ColumnID="Factor" Header="Hệ số" DataIndex="Factor" Align="Right">
                                                    <Editor>
                                                        <ext:NumberField runat="server" BlankText="0"></ext:NumberField>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ColumnID="Percent" Header="Phần trăm" DataIndex="Percent" Align="Right">
                                                    <Editor>
                                                        <ext:NumberField runat="server" BlankText="0"></ext:NumberField>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ColumnID="Value" Header="Giá trị" DataIndex="Value" Align="Right">
                                                    <Editor>
                                                        <ext:NumberField runat="server" BlankText="0"></ext:NumberField>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ColumnID="Taxable" Header="Chịu thuế" DataIndex="Taxable" Align="Center">
                                                    <Renderer Fn="RenderTaxable"></Renderer>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:TabPanel>
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
                                                            <ext:TextField runat="server" ID="txtBasicSalaryMany" FieldLabel="Lương cơ bản" AnchorHorizontal="98%" Disabled="true" DisabledClass="disabled-input-style" />
                                                            <ext:TextField runat="server" ID="txtGrossSalaryMany" FieldLabel="Lương GROSS" AnchorHorizontal="98%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                            <ext:TextField runat="server" ID="txtContractSalaryMany" FieldLabel="Lương hợp đồng" AnchorHorizontal="98%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                            <ext:TextField runat="server" ID="txtPercentageSalaryMany" FieldLabel="% hưởng lương" AnchorHorizontal="98%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.49">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtFactorMany" FieldLabel="Hệ số" AnchorHorizontal="100%" MaxLength="5" MaskRe="/[0-9,]/" />
                                                            <ext:TextField runat="server" ID="txtNetSalaryMany" FieldLabel="Lương NET" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                            <ext:TextField runat="server" ID="txtInsuranceSalaryMany" FieldLabel="Lương bảo hiểm" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9.]/" />
                                                            <ext:TextField runat="server" ID="txtPercentageLeaderMany" FieldLabel="Phụ cấp chức vụ" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9,]/" />
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
            <ext:Window runat="server" Title="Nhập dữ liệu từ file excel" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdExcel"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" LabelWidth="150">
                        <Items>
                            <ext:Label runat="server" ID="labelDownload" FieldLabel="Tải tệp tin mẫu">
                            </ext:Label>
                            <ext:Button runat="server" ID="btnDownloadTemplate" Icon="ArrowDown" ToolTip="Tải về" Text="Tải về" Width="100">
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
                            <Click Handler="wdExcel.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

