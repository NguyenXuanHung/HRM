<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeArgumentManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.EmployeeArgumentManagement" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <style type="text/css">
        div#gpEmployeeArgument .x-toolbar-left {
            width: 50% !important;
        }
    </style>
    <script type="text/javascript">
        var OnClickArgument = function(argumentCode, recordId) {
            Ext.net.DirectMethods.InitWindowArgument(argumentCode, recordId);
        }
        var RenderArgument = function (value, p, record) {
            return `<div style="width:100%;height:100%;" onclick="OnClickArgument('${p.id}', '${record.json.RecordId}')">${value}</div>`;
        }
        var RenderPercent = function (value, p, record) {
            var width = value / 2;
            var style = `width:${width}%;
                            height:100%;
                            font-weight:bold;
                            background:red;
                            color:white;
                            padding:2px;`;

            if (value <= 0) {
                style += "padding:0; color:black;";
            } else if (value < 20) {
                style += "color:black;";
            } else if (value < 70) {
                style += "background:red;";
            } else if (value < 90) {
                style += "background:gold;";
            } else {
                style += "background:limegreen;";
            }
            return `<div style="${style}" onclick="OnClickArgument('${p.id}', '${record.json.RecordId}')">${value} %</div>`;
        }
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
            gpEmployeeArgument.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }

        var handlerRowSelect = function (p) {
            var selections = chkSelectionModel.getSelections();
            if (selections.length > 1) {
                // clear hidden field id
                hdfId.reset();
                hdfRecordId.reset();
                // disable single edit / delete button
                btnEdit.disable();
                btnDelete.disable();

                var recordIds = selections[0].data.RecordId;
                for (var i = 1; i < selections.length; i++) {
                    recordIds = recordIds + "," + selections[i].data.RecordId;
                }
                hdfRecordIds.setValue(recordIds);
            } else {
                hdfId.setValue(selections[0].id);
                hdfRecordIds.setValue(selections[0].data.RecordId);
                hdfRecordId.setValue(selections[0].data.RecordId);
            }

            btnEdit.enable();
            btnDelete.enable();
            if (gpEmployeeArgument.getSelectionModel().getCount() > 1) {
                btnEdit.disable();
                btnDelete.disable();
                btnMultipleDelete.enable();
            }
        }

        var handlerRowDeselect = function () {
            if (gpEmployeeArgument.getSelectionModel().getCount() === 0) {
                btnEdit.disable();
                btnDelete.disable();
                btnMultipleDelete.disable();
            }
            if (gpEmployeeArgument.getSelectionModel().getCount() === 1) {
                btnEdit.enable();
                btnDelete.enable();
                btnMultipleDelete.disable();
            }
        }

        var validateForm = function () {
            if (hdfChooseEmployee.getValue() == '' || hdfChooseEmployee.getValue().trim == '') {
                alert('Bạn chưa chọn nhân viên!');
                return false;
            }

            if (hdfGroupInput.getValue() == '' || hdfGroupInput.getValue().trim == '') {
                alert('Bạn chưa chọn loại KPI!');
                return false;
            }

            if (hdfArgumentId.getValue() == '' || hdfArgumentId.getValue().trim == '') {
                alert('Bạn chưa chọn tham số!');
                return false;
            }
            if (hdfMonth.getValue() == '' || hdfMonth.getValue().trim == '') {
                alert('Bạn chưa chọn tháng!');
                return false;
            }
            if (hdfYear.getValue() == '' || hdfYear.getValue().trim == '') {
                alert('Bạn chưa chọn năm!');
                return false;
            }

            return true;
        }

        var validateFormDelete = function () {
            if (hdfMonthDelete.getValue() == '' || hdfMonthDelete.getValue().trim == '') {
                alert('Bạn chưa chọn tháng!');
                return false;
            }
            if (hdfYearDelete.getValue() == '' || hdfYearDelete.getValue().trim == '') {
                alert('Bạn chưa chọn năm!');
                return false;
            }

            return true;
        }

        var validateFormDownloadTemplate = function () {
            if (hdfGroup.getValue() === '' || hdfGroup.getValue().trim === '') {
                alert('Vui lòng chọn loại KPI!');
                return false;
            }
            return true;
        }

        var ResetWdImportExcelFile = function () {
            $('input[type=text]').val('');
            $('#textarea').val('');
            $('input[type=select]').val('');
            $('input[type=radio]').val('');
            $('input[type=checkbox]').val('');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfRecordIds" />
            <ext:Hidden runat="server" ID="hdfDepartmentIds" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonthFilter" />
            <ext:Hidden runat="server" ID="hdfYearFilter" />
            <ext:Hidden runat="server" ID="hdfArgumentId" />
            <ext:Hidden runat="server" ID="hdfChooseEmployee" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <ext:Hidden runat="server" ID="hdfStatus" />
            <ext:Hidden runat="server" ID="hdfGroupFilter" />

            <!-- store -->
            <ext:Store runat="server" ID="storeEmployeeArgument" AutoSave="True" GroupField="FullName">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="EmployeeArgument" Mode="Value" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="month" Value="hdfMonthFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="year" Value="hdfYearFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="departmentIds" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                    <ext:Parameter Name="departments" Value="hdfDepartmentIds.getValue()" Mode="Raw" />
                    <ext:Parameter Name="group" Value="hdfGroupFilter.getValue()" Mode="Raw"/>
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="RecordId" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="FullName" />
                            <ext:RecordField Name="DepartmentName" />
                            <ext:RecordField Name="ArgumentDetailModels" />
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
            <ext:Store runat="server" ID="storeGroup">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GroupKpi" Mode="Value" />
                    <ext:Parameter Name="Status" Value="hdfStatus.getValue()" Mode="Raw" />
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

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpEmployeeArgument" StoreID="storeEmployeeArgument" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnBack" runat="server" Text="Quay lại" Icon="ArrowLeft" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="btnBack_Click">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Command" Value="Update" />
                                                            <ext:Parameter Name="Id" Value="chkSelectionModel.getSelected().get('Id')" Mode="Raw" />
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
                                            <ext:Button runat="server" ID="btnMultipleDelete" Text="Xóa nhiều" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitDeleteMultiple">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button ID="btnEditByExcel" runat="server" Text="Nhập từ excel" Icon="PageExcel">
                                                <Listeners>
                                                    <Click Handler="#{wdImportExcelFile}.show();" />
                                                </Listeners>
                                            </ext:Button>
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
                                                    <ext:Store runat="server" ID="storeGroupFilter">
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
                                                    <Select Handler="hdfGroupFilter.setValue(cboGroupFilter.getValue());reloadGrid();"></Select>
                                                </Listeners>
                                                 <DirectEvents>
                                                     <Select OnEvent="ReloadGridColumn"></Select>
                                                 </DirectEvents>
                                            </ext:ComboBox>
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
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center"/>
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="100" Align="Left" DataIndex="EmployeeCode"/>
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="150" Align="Left" DataIndex="FullName"/>
                                        <ext:Column ColumnID="DepartmentName" Header="Đơn vị" Width="150" Align="Left" DataIndex="DepartmentName" />
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

            <ext:Window runat="server" ID="wdSetting" Title="Thêm mới tham số KPI cho nhân viên" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                        Layout="Form" Height="600">
                        <Items>
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
                             <ext:Hidden runat="server" ID="hdfGroupInput" />
                            <ext:ComboBox runat="server" ID="cboGroupInput" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                FieldLabel="Chọn loại KPI" ItemSelector="div.list-item" PageSize="20" AnchorHorizontal="100%" StoreID="storeGroup">
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
                                    <Expand Handler="if(#{cboGroupInput}.store.getCount() == 0){storeGroup.reload();}" />
                                    <Select Handler="this.triggers[0].show();hdfGroupInput.setValue(cboGroupInput.getValue());"></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupInput.reset();}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="cboArgument" FieldLabel="Chọn tham số" CtCls="requiredData" AnchorHorizontal="100%"
                                DisplayField="Name" ValueField="Id" ItemSelector="div.list-item" PageSize="20">
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
                                    <ext:Store runat="server" ID="storeArgument" AutoLoad="false">
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="Argument" Mode="Value" />
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
                                    <Expand Handler="if(#{cboArgument}.store.getCount() == 0){storeArgument.reload();}" />
                                    <Select Handler="this.triggers[0].show();hdfArgumentId.setValue(cboArgument.getValue());"></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfArgumentId.reset();txtValueType.reset();}" />
                                </Listeners>
                                <DirectEvents>
                                    <Select OnEvent="OnSelectArgument"></Select>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:TextField runat="server" ID="txtValueType" ReadOnly="True" FieldLabel="Loại dữ liệu" AnchorHorizontal="100%" />
                            <ext:TextField runat="server" ID="txtValue" CtCls="requiredData" FieldLabel="Giá trị"
                                AnchorHorizontal="100%" />
                            <ext:CompositeField runat="server" ID="cf1" FieldLabel="Chọn tháng">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cboMonth" Width="80" Editable="false" FieldLabel="Chọn tháng" CtCls="requiredData">
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
                                            <Select Handler="hdfMonth.setValue(cboMonth.getValue());" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="55" CtCls="requiredData">
                                        <Listeners>
                                            <Spin Handler="hdfYear.setValue(spnYear.getValue());" />
                                        </Listeners>
                                    </ext:SpinnerField>
                                </Items>
                            </ext:CompositeField>
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
            <ext:Window runat="server" Title="Nhập dữ liệu từ file excel" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdImportExcelFile"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" LabelWidth="150">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfDepartmentId" />
                            <ext:ComboBox runat="server" ID="cboDepartment" FieldLabel="Phòng - Ban" EmptyText="Chọn đơn vị"
                                LabelWidth="260" Width="550" AllowBlank="false" DisplayField="Name" ValueField="Id"
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
                                    <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cboDepartment.getValue());" />
                                    <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="hdfGroup" />
                            <ext:ComboBox runat="server" ID="cboGroup" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                FieldLabel="Chọn loại KPI" ItemSelector="div.list-item" PageSize="20" AnchorHorizontal="100%" StoreID="storeGroup">
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
                                    <Expand Handler="if(#{cboGroup}.store.getCount() == 0){storeGroup.reload();}" />
                                    <Select Handler="this.triggers[0].show();hdfGroup.setValue(cboGroup.getValue());"></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroup.reset();}" />
                                </Listeners>
                            </ext:ComboBox>
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
                        <Listeners>
                            <Click Handler="return validateFormDownloadTemplate();" />
                        </Listeners>
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
                <Listeners>
                    <Hide Handler="ResetWdImportExcelFile();" />
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>
