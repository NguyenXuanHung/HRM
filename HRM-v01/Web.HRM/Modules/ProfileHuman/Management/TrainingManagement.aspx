<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.ProfileHuman.Management.TrainingManagement" CodeBehind="TrainingManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">
        var CheckInputTraining = function () {
            if (txtDaoTao.getValue().trim() == '') {
                alert('Bạn chưa nhập tên khóa đào tạo');
                txtDaoTao.focus();
                return false;
            }
            if (ValidateDateField(dfTuNgay) == false) {
                alert('Định dạng từ ngày không đúng');
                return false;
            }
            if (ValidateDateField(dfDenNgay) == false) {
                alert('Định dạng từ ngày không đúng');
                return false;
            }
            return true;
        }
        var CheckInputTrainingHangLoat = function () {
            if (txtDTTenKhoaDaoTao.getValue().trim() == '') {
                alert('Bạn chưa nhập tên khóa đào tạo');
                txtDTTenKhoaDaoTao.focus();
                return false;
            }
            if (ValidateDateField(dfDTTuNgay) == false) {
                alert('Định dạng từ ngày không đúng');
                return false;
            }
            if (ValidateDateField(dfDTDenNgay) == false) {
                alert('Định dạng từ ngày không đúng');
                return false;
            }

            if (gpListEmployee.getSelectionModel().getCount() === 0 && gpListEmployee.disabled === false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }

            return true;
        }
        var searchBoxKT2 = function (f, e) {
            hdfLyDoTempl.setValue(cbbLyDoHangLoat.getRawValue());
            if (hdfIsDanhMuc.getValue() == '1') {
                hdfIsDanhMuc.setValue('2');
            }
            if (cbbLyDoHangLoat.getRawValue() == '') {
                hdfIsDanhMuc.reset();
            }
        }


        var searchBoxPosition = function (f, e) {
            hdfMakerTempPosition.setValue(cbxMakerPosition.getRawValue());
            if (hdfIsMakerPosition.getValue() === '1') {
                hdfIsMakerPosition.setValue('2');
            }
            if (cbxMakerPosition.getRawValue() == '') {
                hdfIsMakerPosition.reset();
            }
        }
        var searchBoxPositionUpdate = function (f, e) {
            hdfUpdateMakerTempPosition.setValue(cbxUpdateMakerPosition.getRawValue());
            if (hdfIsUpdateMakerPosition.getValue() === '1') {
                hdfIsUpdateMakerPosition.setValue('2');
            }
            if (cbxUpdateMakerPosition.getRawValue() === '') {
                hdfIsUpdateMakerPosition.reset();
            }
        }
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfMaDonVi" />
            <ext:Hidden runat="server" ID="hdfMenuID" />
            <ext:Hidden runat="server" ID="hdfUserID" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <!-- store chức vụ -->
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
            <ext:Store ID="storeNation" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Nation" Mode="Value" />
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

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gpTrainingHistory" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTraining.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowEdit">
                                                        <EventMask ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="return CheckSelectedRow(gpTrainingHistory);" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnDelete_Click">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:DateField ID="dfNgayBatDauFind" runat="server" Vtype="daterange" EmptyText="Từ ngày"
                                                Width="140" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" LabelWidth="101">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); dfNgayKetThucFind.setMinValue();this.triggers[0].hide(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();}" />
                                                </Listeners>
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfNgayKetThucFind}" Mode="Value" />
                                                </CustomConfig>
                                                <ToolTips>
                                                    <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Lọc theo ngày bắt đầu">
                                                    </ext:ToolTip>
                                                </ToolTips>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer2" Width="5" runat="server" />
                                            <ext:DateField ID="dfNgayKetThucFind" runat="server" Vtype="daterange" EmptyText="Đến ngày"
                                                Width="140" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                                <ToolTips>
                                                    <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Lọc theo ngày kết thúc">
                                                    </ext:ToolTip>
                                                </ToolTips>
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfNgayBatDauFind}" Mode="Value" />
                                                </CustomConfig>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset();dfNgayBatDauFind.setMaxValue(); this.triggers[0].hide(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();}" />
                                                </Listeners>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer3" Width="5" runat="server" />
                                            <ext:Hidden runat="server" ID="hdfNationIdFilter" />
                                            <ext:ComboBox ID="cboNationFilter" Editable="true" MinChars="1"
                                                runat="server" DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..."
                                                ItemSelector="div.list-item" AnchorHorizontal="98%" LabelWidth="120" Width="140"
                                                EmptyText="Quốc gia đào tạo" StoreID="storeNation">
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
                                                    <Select Handler="this.triggers[0].show();hdfNationIdFilter.setValue(cboNationFilter.getValue());#{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfNationIdFilter.reset(); #{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer4" Width="5" runat="server" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220"
                                                EmptyText="Nhập mã, họ tên, tên khóa ĐT">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store runat="server" ID="storeTrainingHistory" GroupField="FullName">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/Training/HandlerTrainingHistory.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="fromDate" Value="dfNgayBatDauFind.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="toDate" Value="dfNgayKetThucFind.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="nationId" Value="hdfNationIdFilter.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="TrainingName" />
                                                    <ext:RecordField Name="StartDate" />
                                                    <ext:RecordField Name="EndDate" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="NationName" />
                                                    <ext:RecordField Name="TrainingPlace" />
                                                    <ext:RecordField Name="Reason" />
                                                    <ext:RecordField Name="Note" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                        <Listeners>
                                            <Load Handler="#{RowSelectionModel1}.clearSelections();" />
                                        </Listeners>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView2" runat="server" ForceFit="true" MarkDirty="false"
                                        EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                        <ext:Column Header="Mã nhân viên" Width="95" Align="Left" DataIndex="EmployeeCode" />
                                        <ext:GroupingSummaryColumn DataIndex="FullName" Header="Cán bộ" Width="200" Sortable="true" Hideable="false" SummaryType="Count">
                                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' khóa học)' : '(1 khóa học)');" />
                                        </ext:GroupingSummaryColumn>
                                        <ext:Column Header="Tên khóa đào tạo" Width="300" Align="Left" DataIndex="TrainingName" />
                                        <ext:DateColumn Header="Ngày bắt đầu" Width="120" Align="Center" DataIndex="StartDate"
                                            Format="dd/MM/yyyy" />
                                        <ext:DateColumn Header="Ngày kết thúc" Width="120" Align="Center" DataIndex="EndDate"
                                            Format="dd/MM/yyyy" />
                                        <ext:Column Header="Bộ phận" Width="200" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column Header="Lý do đào tạo" Width="200" Align="Left" DataIndex="Reason" />
                                        <ext:Column Header="Nơi đào tạo" Width="200" Align="Left" DataIndex="TrainingPlace" />
                                        <ext:Column Header="Quốc gia đào tạo" Width="150" Align="Left" DataIndex="NationName" />
                                        <ext:Column Header="Ghi chú" Align="Left" DataIndex="Note" Width="350" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().id);btnDelete.enable();btnEdit.enable(); " />
                                            <RowDeselect Handler="hdfRecordId.reset();btnEdit.disable();btnDelete.disable(); " />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="25">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="25" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window ID="wdDaoTao" AutoHeight="true" Width="550" runat="server" Padding="6"
                EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
                LabelWidth="120" Icon="Pencil" Title="Đào tạo/ Bồi dưỡng" Resizable="false">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="55">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="110" LabelAlign="left"
                                Layout="Form" ColumnWidth=".5">
                                <Items>
                                    <ext:TextField ID="txtDaoTao" runat="server" FieldLabel="Tên khóa đào tạo<span style='color:red;'>*</span>"
                                        CtCls="requiredData" AnchorHorizontal="95%" TabIndex="1" MaxLength="500" />
                                    <ext:DateField ID="dfTuNgay" runat="server" Vtype="daterange" FieldLabel="Ngày bắt đầu"
                                        AnchorHorizontal="95%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                        <CustomConfig>
                                            <ext:ConfigItem Name="endDateField" Value="#{dfDenNgay}" Mode="Value" />
                                        </CustomConfig>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container3" runat="server" LabelAlign="left" LabelWidth="110"
                                Layout="Form" ColumnWidth=".5">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfNationId" />
                                    <ext:ComboBox runat="server" ID="cboNation" FieldLabel="Quốc gia đào tạo"
                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" ListWidth="200"
                                        TabIndex="2" Editable="true" ItemSelector="div.list-item" PageSize="15" LoadingText="Quốc gia đào tạo"
                                        EmptyText="Gõ để tìm kiếm" StoreID="storeNation">
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
                                            <Select Handler="this.triggers[0].show();#{hdfNationId}.setValue(#{cboNation}.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfNationId}.reset();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:DateField ID="dfDenNgay" runat="server" Vtype="daterange" FieldLabel="Ngày kết thúc"
                                        AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                        <CustomConfig>
                                            <ext:ConfigItem Name="startDateField" Value="#{dfTuNgay}" Mode="Value" />
                                        </CustomConfig>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container4" runat="server" Layout="Column" Height="220">
                        <Items>
                            <ext:Container ID="Container5" runat="server" LabelWidth="110" LabelAlign="left"
                                Layout="Form" ColumnWidth="1">
                                <Items>
                                    <ext:TextField ID="txtLyDoDaoTao" runat="server" FieldLabel="Lý do đào tạo" AnchorHorizontal="100%"
                                        TabIndex="5" MaxLength="500" />
                                    <ext:TextField ID="txtNoiDaoTao" runat="server" FieldLabel="Nơi đào tạo" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:TextField ID="txtUpdateSponsorDepartment" runat="server" FieldLabel="Đơn vị tài trợ" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:TextField ID="txtUpdateSourceDepartment" runat="server" FieldLabel="Cơ quan quyết định" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:TextField ID="txtUpdateDecisionMaker" runat="server" FieldLabel="Người ký" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:Hidden runat="server" ID="hdfIsUpdateMakerPosition" Text="0" />
                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                    <ext:Hidden runat="server" ID="hdfUpdateMakerPosition" />
                                    <ext:Hidden runat="server" ID="hdfUpdateMakerTempPosition" />
                                    <ext:ComboBox runat="server" ID="cbxUpdateMakerPosition" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                        EnableKeyEvents="true" StoreID="storePosition">
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
                                            <Select Handler="this.triggers[0].show();  hdfUpdateMakerPosition.setValue(cbxUpdateMakerPosition.getValue());
				                                                    hdfIsUpdateMakerPosition.setValue('1');
				                                                    hdfUpdateMakerTempPosition.setValue(cbxUpdateMakerPosition.getRawValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsUpdateMakerPosition.reset();hdfUpdateMakerTempPosition.reset();hdfUpdateMakerPosition.reset();  }" />
                                            <KeyUp Fn="searchBoxPositionUpdate" />
                                            <Blur Handler="cbxUpdateMakerPosition.setRawValue(hdfUpdateMakerTempPosition.getValue());
			                                                        if (hdfIsUpdateMakerPosition.getValue() != '1') {cbxUpdateMakerPosition.setValue(hdfUpdateMakerTempPosition.getValue());}" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextArea ID="txt_GhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                        TabIndex="7" MaxLength="500" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return CheckInputTraining()" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button4" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdDaoTao.hide(); Ext.net.DirectMethods.ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetForm();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="wdTraining" Constrain="true" Modal="true"
                Title="Thêm cán bộ đào tạo bồi dưỡng hoàng loạt" Icon="UserAdd" Layout="FormLayout"
                AutoHeight="true" Width="650" Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container ID="Container8" runat="server" Layout="Column" Height="55">
                        <Items>
                            <ext:Container ID="Container25" runat="server" LabelWidth="110" LabelAlign="left"
                                Layout="Form" ColumnWidth=".5">
                                <Items>
                                    <ext:TextField ID="txtDTTenKhoaDaoTao" runat="server" FieldLabel="Tên khóa đào tạo<span style='color:red;'>*</span>"
                                        CtCls="requiredData" AnchorHorizontal="95%" TabIndex="1" MaxLength="500" />
                                    <ext:DateField ID="dfDTTuNgay" runat="server" Vtype="daterange" FieldLabel="Ngày bắt đầu"
                                        AnchorHorizontal="95%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                        <CustomConfig>
                                            <ext:ConfigItem Name="endDateField" Value="#{dfDTDenNgay}" Mode="Value" />
                                        </CustomConfig>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container26" runat="server" LabelAlign="left" LabelWidth="110"
                                Layout="Form" ColumnWidth=".5">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfNationIdMany" />
                                    <ext:ComboBox runat="server" ID="cboNationMany" FieldLabel="Quốc gia đào tạo" DisplayField="Name"
                                        MinChars="1" ValueField="Id" AnchorHorizontal="100%" ListWidth="200" TabIndex="2"
                                        Editable="true" ItemSelector="div.list-item" PageSize="15" LoadingText="Quốc gia đào tạo"
                                        EmptyText="Gõ để tìm kiếm" StoreID="storeNation">
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
                                            <Select Handler="this.triggers[0].show();#{hdfNationIdMany}.setValue(#{cboNationMany}.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfNationIdMany}.reset();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:DateField ID="dfDTDenNgay" runat="server" Vtype="daterange" FieldLabel="Ngày kết thúc"
                                        AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                        <CustomConfig>
                                            <ext:ConfigItem Name="startDateField" Value="#{dfTuNgay}" Mode="Value" />
                                        </CustomConfig>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container27" runat="server" Layout="Column" Height="200">
                        <Items>
                            <ext:Container ID="Container31" runat="server" LabelWidth="110" LabelAlign="left"
                                Layout="Form" ColumnWidth="1">
                                <Items>
                                    <ext:TextField ID="txtDTLyDoDaoTao" runat="server" FieldLabel="Lý do đào tạo" AnchorHorizontal="100%"
                                        TabIndex="5" MaxLength="500" />
                                    <ext:TextField ID="txtDTNoiDaoTao" runat="server" FieldLabel="Nơi đào tạo" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:TextField ID="txtSponsorDepartment" runat="server" FieldLabel="Đơn vị tài trợ" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:TextField ID="txtSourceDepartment" runat="server" FieldLabel="Cơ quan quyết định" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:TextField ID="txtDecisionMaker" runat="server" FieldLabel="Người ký" AnchorHorizontal="100%"
                                        TabIndex="6" MaxLength="200" />
                                    <ext:Hidden runat="server" ID="hdfIsMakerPosition" Text="0" />
                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                    <ext:Hidden runat="server" ID="hdfMakerPosition" />
                                    <ext:Hidden runat="server" ID="hdfMakerTempPosition" />
                                    <ext:ComboBox runat="server" ID="cbxMakerPosition" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                        EnableKeyEvents="true" StoreID="storePosition">
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
                                            <Select Handler="this.triggers[0].show();  hdfMakerPosition.setValue(cbxMakerPosition.getValue());
				                                                    hdfIsMakerPosition.setValue('1');
				                                                    hdfMakerTempPosition.setValue(cbxMakerPosition.getRawValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsMakerPosition.reset();hdfMakerTempPosition.reset();hdfMakerPosition.reset();  }" />
                                            <KeyUp Fn="searchBoxPosition" />
                                            <Blur Handler="cbxMakerPosition.setRawValue(hdfMakerTempPosition.getValue());
			                                                        if (hdfIsMakerPosition.getValue() != '1') {cbxMakerPosition.setValue(hdfMakerTempPosition.getValue());}" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextArea ID="txtDTGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                        TabIndex="7" MaxLength="500" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="230">
                        <Items>
                            <ext:GridPanel runat="server" ID="gpListEmployee" TrackMouseOver="true" Title="Danh sách cán bộ"
                                StripeRows="true" Border="true" Region="Center" Icon="User" AutoExpandColumn="DepartmentName"
                                AutoExpandMin="150">
                                <Store>
                                    <ext:Store ID="storeEmployee" AutoLoad="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="UcChooseEmployee" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="PositionName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="40" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Width="100" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="FullName" Header="Họ tên" Width="200" DataIndex="FullName" />
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" DataIndex="DepartmentName">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                                </SelectionModel>
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
                    <ext:Button runat="server" ID="btnCapNhatHL" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return CheckInputTrainingHangLoat();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Insert" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLaiHL" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTraining.hide();Ext.net.DirectMethods.ResetForm();chkEmployeeRowSelection.clearSelections();" />
                        </Listeners>

                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetForm();chkEmployeeRowSelection.clearSelections();" />
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>
