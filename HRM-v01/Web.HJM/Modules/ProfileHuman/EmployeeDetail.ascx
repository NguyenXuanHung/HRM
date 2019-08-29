<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.EmployeeDetail" Codebehind="EmployeeDetail.ascx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<%@ Register Src="../ChooseEmployee/ucChooseEmployee.ascx" TagName="ucChooseEmployee" TagPrefix="uc1" %>
<script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
<script src="../../Resource/js/Extcommon.js" type="text/javascript"></script>
<script src="../../Resource/js/JScript.js" type="text/javascript"></script>
<script src="../../Resource/js/RenderJS.js" type="text/javascript"></script>
<script src="../../Resource/js/global.js" type="text/javascript"></script>
<script src="EmployeeDetail.js" type="text/javascript"></script>

<style type="text/css">
    .Download
    {
        background-image: url(../../Resource/images/download.png) !important;
    }
    #employeeDetail_GridPanel_BangCap .x-grid3-cell-inner
    {
        white-space: normal !important;
    }
</style>
<script type="text/javascript">
    var searchBoxKT = function (f, e) {
        employeeDetail_hdfLyDoKTTemp.setValue(employeeDetail_cbLyDoKhenThuong.getRawValue());
        if (employeeDetail_hdfIsDanhMuc.getValue() == '1') {
            employeeDetail_hdfIsDanhMuc.setValue('2');
        }
        if (employeeDetail_cbLyDoKhenThuong.getRawValue() == '') {
            employeeDetail_hdfIsDanhMuc.reset();
        }
    }
    var searchBoxKL = function () {
        employeeDetail_hdfLyDoKLTemp.setValue(employeeDetail_cbLyDoKyLuat.getRawValue());
        if (employeeDetail_hdfIsDanhMucKL.getValue() == '1') {
            employeeDetail_hdfIsDanhMucKL.setValue('2');
        }
        if (employeeDetail_cbLyDoKyLuat.getRawValue() == '') {
            employeeDetail_hdfIsDanhMucKL.reset();
        }
    }
    var prepare = function (grid, command, record, row, col, value) {
        if ((record.data.TepTinDinhKem == '' || record.data.TepTinDinhKem == null) && command.command == "Download") {
            command.hidden = true;
            command.hideMode = "visibility";
        }
    }
</script>
<ext:Hidden runat="server" ID="hdfRecordId" />
<ext:Hidden runat="server" ID="hdfCurrentRecordId" />
<ext:Hidden runat="server" ID="hdfMaDonViTinh" />
<ext:Hidden runat="server" ID="hdfTypeWindow" />
<ext:Hidden runat="server" ID="hdfTenQuocGia" />
<ext:Hidden runat="server" ID="hdfBusinessType" />
<ext:Store ID="cbx_quocgia_Store" runat="server" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
<ext:Store ID="StoreCapKhenThuongKyLuat" AutoLoad="false" runat="server">
     <Proxy>
        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
    </Proxy>
    <BaseParams>
    <ext:Parameter Name="objname" Value="cat_LevelRewardDiscipline" Mode="Value" />
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
<ext:Store ID="store_ChucVu" runat="server" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
<ext:Store runat="server" AutoLoad="false" ID="storeJobTitle">
     <Proxy>
        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
    </Proxy>
        <BaseParams>
        <ext:Parameter Name="objname" Value="cat_JobTitle" Mode="Value" />
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
 <ext:Store ID="StoreGraduationType" runat="server" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
    </Proxy>
        <BaseParams>
        <ext:Parameter Name="objname" Value="cat_GraduationTimeSheetHandlerType" Mode="Value" />
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
<ext:Store ID="storeBusiness" AutoLoad="false" AutoSave="true" ShowWarningOnFailure="false"
    SkipIdForNewRecords="false" RefreshAfterSaving="None" runat="server" OnRefreshData="StoreBusiness_OnRefreshData">
    <Reader>
    <ext:JsonReader IDProperty="Id">
        <Fields>
            <ext:RecordField Name="Id" />
            <ext:RecordField Name="RecordId" />
            <ext:RecordField Name="EmployeeCode" />
            <ext:RecordField Name="FullName" />
            <ext:RecordField Name="CurrentPosition" />
            <ext:RecordField Name="NewPosition" />
            <ext:RecordField Name="OldDepartment" />
            <ext:RecordField Name="DecisionNumber" />
            <ext:RecordField Name="DecisionDate" />
            <ext:RecordField Name="EffectiveDate" />
            <ext:RecordField Name="ExpireDate" />
            <ext:RecordField Name="SourceDepartment" />
            <ext:RecordField Name="DestinationDepartment" />
            <ext:RecordField Name="CurrentDepartment" />
            <ext:RecordField Name="DecisionMaker" />
            <ext:RecordField Name="DecisionPosition" />
            <ext:RecordField Name="FileScan" />
            <ext:RecordField Name="Description" />
            <ext:RecordField Name="ShortDecision" />
            <ext:RecordField Name="EmulationTitle" />
            <ext:RecordField Name="Money" DefaultValue="0" />
        </Fields>
    </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="wdDiNuocNgoai" AutoHeight="true" Width="500" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Quản lý đi nước ngoài">
    <Items>
        <ext:Hidden runat="server" ID="hdfGoBoardNationId" />
        <ext:ComboBox runat="server" ID="cbx_DNN_MaNuoc" DisplayField="Name" MinChars="1"
            ValueField="Id" FieldLabel="Quốc gia<span style='color:red'>*</span>" AnchorHorizontal="100%"
            Editable="true" ItemSelector="div.list-item" StoreID="cbx_quocgia_Store" CtCls="requiredData">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template30" runat="server">
                <Html>
                    <tpl for=".">
				        <div class="list-item"> 
					        {Name}
				        </div>
			        </tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfGoBoardNationId}.setValue(#{cbx_DNN_MaNuoc}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfGoBoardNationId}.reset();" />
            </Listeners>
        </ext:ComboBox>
        <ext:Container runat="server" ID="Container32" Layout="ColumnLayout" Height="27">
            <Items>
                <ext:Container runat="server" ID="Container33" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:DateField ID="dfNgayBatDau" runat="server" FieldLabel="Ngày bắt đầu" AnchorHorizontal="98%"
                            MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Vtype="daterange" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                            RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfNgayKetThuc}" Mode="Value">
                                </ext:ConfigItem>
                            </CustomConfig>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();" />
                                <TriggerClick Handler="if (index == 0) { this.reset();#{dfNgayKetThuc}.setMinValue(); this.triggers[0].hide(); }" />
                            </Listeners>
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container34" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:DateField ID="dfNgayKetThuc" runat="server" FieldLabel="Ngày kết thúc" AnchorHorizontal="100%"
                            MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Vtype="daterange" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                            RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfNgayBatDau}" Mode="Value">
                                </ext:ConfigItem>
                            </CustomConfig>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();" />
                                <TriggerClick Handler="if (index == 0) { this.reset();#{dfNgayBatDau}.setMaxValue(); this.triggers[0].hide(); }" />
                            </Listeners>
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:TextArea runat="server" ID="txtGoAboardReason" AnchorHorizontal="100%" FieldLabel="Lý do" />
        <ext:TextArea runat="server" ID="txtGoAboardNote" AnchorHorizontal="100%" FieldLabel="Ghi chú" />
    </Items>
    <Listeners>
        <Hide Handler="resetWdDiNuocNgoai();" />
    </Listeners>
    <Buttons>
        <ext:Button runat="server" ID="btn_updateDNN" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputDiNuocNgoai();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateDiNuocNgoai_Click">
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update" />
                    </ExtraParams>
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btn_InsertDNN" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputDiNuocNgoai();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateDiNuocNgoai_Click" After="resetWdDiNuocNgoai();">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btn_UpdateAndCloseDNN" Text="Cập nhật & Đóng lại"
            Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputDiNuocNgoai();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateDiNuocNgoai_Click">
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="btn_CloseDNN">
            <Listeners>
                <Click Handler="#{wdDiNuocNgoai}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window ID="wdQuaTrinhDaoTao" AutoHeight="true" Width="590" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Đào tạo\ Bồi dưỡng">
    <Items>
        <ext:Container ID="Container8" runat="server" Layout="Column" Height="55">
            <Items>
                <ext:Container ID="Container25" runat="server" LabelWidth="110" LabelAlign="left"
                    Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:TextField ID="txtDTTenKhoaDaoTao" runat="server" FieldLabel="Tên khóa đào tạo<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="95%" TabIndex="1" MaxLength="500" />
                        <ext:DateField ID="dfDTTuNgay" runat="server" Vtype="daterange" FieldLabel="Ngày bắt đầu"
                            AnchorHorizontal="95%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfDTDenNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container26" runat="server" LabelAlign="left" LabelWidth="110"
                    Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfDTQuocGia" />
                        <ext:ComboBox runat="server" ID="cbxDTQuocGia" FieldLabel="Quốc gia đào tạo" DisplayField="Name"
                            MinChars="1" ValueField="Id" AnchorHorizontal="100%" ListWidth="200" TabIndex="2"
                            Editable="true" ItemSelector="div.list-item" PageSize="15" LoadingText="Quốc gia đào tạo"
                            EmptyText="Gõ để tìm kiếm" StoreID="cbx_quocgia_Store">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template28" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();#{hdfDTQuocGia}.setValue(#{cbxDTQuocGia}.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfDTQuocGia}.reset();" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:DateField ID="dfDTDenNgay" runat="server" Vtype="daterange" FieldLabel="Ngày kết thúc"
                            AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfTuNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Container ID="Container27" runat="server" Layout="Column" Height="120">
            <Items>
                <ext:Container ID="Container31" runat="server" LabelWidth="110" LabelAlign="left"
                    Layout="Form" ColumnWidth="1">
                    <Items>
                        <ext:TextField ID="txtLyDoDaoTao" runat="server" FieldLabel="Lý do đào tạo" AnchorHorizontal="100%"
                            TabIndex="5" MaxLength="500" />
                        <ext:TextField ID="txtNoiDaoTao" runat="server" FieldLabel="Nơi đào tạo" AnchorHorizontal="100%"
                            TabIndex="6" MaxLength="200" />
                        <ext:TextArea ID="txtDTGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                            TabIndex="7" MaxLength="500" />
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
    </Items>
    <Buttons>
        <ext:Button ID="btnDTInsert" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputDaoTao();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateDaoTao_Click" After="ResetWdQuaTrinhDaoTao();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnDTEdit" Hidden="true" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputDaoTao();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateDaoTao_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnDTUpdateAndClose" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputDaoTao();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateDaoTao_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnDTClose" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdQuaTrinhDaoTao}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnDTInsert}.show();#{btnDTEdit}.hide();#{btnDTUpdateAndClose}.show();ResetWdQuaTrinhDaoTao();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdBaoHiem" AutoHeight="true" Width="540" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Bảo hiểm" LabelWidth="70">
    <Items>
        <ext:Hidden runat="server" ID="hdfInsurancePositionId" />
        <ext:ComboBox runat="server" Editable="false" ID="cbBHChucVu" DisplayField="Name"
            AnchorHorizontal="100%" FieldLabel="Chức vụ<span style='color:red;'>*</span>"
            LabelWidth="70" CtCls="requiredData" ValueField="Id" TabIndex="2" ItemSelector="div.list-item">
            <Store>
                <ext:Store runat="server" AutoLoad="false" ID="StoreChucVu">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
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
            </Store>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template3" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfInsurancePositionId}.setValue(#{cbBHChucVu}.getValue());" />
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfInsurancePositionId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Container ID="Container18" runat="server" Layout="Column" Height="106">
            <Items>
                <ext:Container ID="Container19" runat="server" LabelWidth="70" LabelAlign="left"
                    Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:DateField ID="dfBHTuNgay" runat="server" Vtype="daterange" FieldLabel="Từ ngày"
                            AnchorHorizontal="95%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfBHDenNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                        <ext:NumberField ID="txtBHPhuCap" runat="server" FieldLabel="Phụ cấp" AnchorHorizontal="95%"
                            TabIndex="5" MaxLength="12" />
                        <ext:NumberField ID="txtBHMucLuong" runat="server" FieldLabel="Mức lương" AnchorHorizontal="95%"
                            TabIndex="7" MaxLength="14" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container4" runat="server" LabelAlign="left" LabelWidth="70" Layout="Form"
                    ColumnWidth=".5">
                    <Items>
                        <ext:DateField ID="dfBHDenNgay" runat="server" Vtype="daterange" FieldLabel="Đến ngày"
                            AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfBHTuNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                        <ext:TextField runat="server" ID="txtBHHSLuong" FieldLabel="Hệ số lương" TabIndex="6"
                                                AnchorHorizontal="100%" Editable="false" MaskRe="/[0-9.,]/" MaxLength="9" />
                        <ext:NumberField ID="txtBHTyle" runat="server" FieldLabel="Tỷ lệ %" EmptyText="0-100"
                            MinValue="0" MaxValue="100" AnchorHorizontal="100%" TabIndex="8" MaxLength="4" />
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Container ID="Container21" runat="server" Layout="Column" Height="80">
            <Items>
                <ext:Container ID="Container22" runat="server" LabelWidth="70" LabelAlign="left"
                    Layout="Form" ColumnWidth="1">
                    <Items>
                        <ext:TextArea ID="txtBHGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                            TabIndex="9" MaxLength="500" />
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdateCongViec" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputBaoHiem();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateCongViec_Click" After="ResetWdBaoHiem();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnEditBaoHiem" Hidden="true" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputBaoHiem();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateCongViec_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnCNVaDongBaoHiem" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputBaoHiem();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateCongViec_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button9" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdBaoHiem}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnUpdateCongViec}.show();#{btnEditBaoHiem}.hide();#{btnCNVaDongBaoHiem}.show();ResetWdBaoHiem();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdDaiBieu" AutoHeight="true" Width="490" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Đại biểu">
    <Items>
        <ext:Container ID="Container5" runat="server" Layout="Column" Height="55">
            <Items>
                <ext:Container ID="Container6" runat="server" LabelWidth="100" LabelAlign="left"
                    Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:TextField ID="txtDBLoaiHinh" FieldLabel="Loại hình<span style='color:red;'>*</span>"
                            AnchorHorizontal="95%" runat="server" TabIndex="1" MaxLength="500" CtCls="requiredData" />
                        <ext:DateField ID="dfDBTuNgay" Vtype="daterange" FieldLabel="Từ ngày" runat="server"
                            AnchorHorizontal="95%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfDBDenNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container7" runat="server" LabelAlign="left" LabelWidth="100"
                    Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:TextField ID="txtDBNhiemKy" FieldLabel="Nhiệm kỳ<span style='color:red;'>*</span>"
                            AnchorHorizontal="100%" runat="server" TabIndex="2" MaxLength="500" CtCls="requiredData" />
                        <ext:DateField ID="dfDBDenNgay" Vtype="daterange" FieldLabel="Đến ngày" runat="server"
                            AnchorHorizontal="100%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfDBTuNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:TextArea runat="server" LabelWidth="70" ID="txtDBGhiChu" FieldLabel="Ghi chú"
            AnchorHorizontal="100%" TabIndex="5" MaxLength="500">
        </ext:TextArea>
    </Items>
    <Buttons>
        <ext:Button ID="btnCapNhatDaiBieu" Text="Cập nhật" Icon="Disk" runat="server">
            <Listeners>
                <Click Handler="return CheckInputDaiBieu();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatDaiBieu_Click" After="ResetWdDaiBieu();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnEditDaiBieu" Icon="Disk" Hidden="true" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputDaiBieu();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatDaiBieu_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button7" Text="Cập nhật & Đóng lại" Icon="Disk" runat="server">
            <Listeners>
                <Click Handler="return CheckInputDaiBieu();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatDaiBieu_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button12" Text="Đóng lại" Icon="Decline" runat="server">
            <Listeners>
                <Click Handler="#{wdDaiBieu}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnCapNhatDaiBieu}.show();#{btnEditDaiBieu}.hide();#{Button7}.show();ResetWdDaiBieu();" />
    </Listeners>
</ext:Window>
<uc1:ucChooseEmployee ID="ucChooseEmployee1" ChiChonMotCanBo="true" DisplayWorkingStatus="DangLamViec"
    runat="server" />
<ext:Window ID="wdHopDong" AutoHeight="true" Width="550" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Hợp đồng" Resizable="false">
    <Items>
        <ext:CompositeField runat="server" AnchorHorizontal="99%">
            <Items>
                <ext:TextField runat="server" FieldLabel="Số hợp đồng<span style='color:red;'>*</span>"
                    Width="386" ID="txtHopDongSoHopDong" MaxLength="30" CtCls="requiredData" TabIndex="1" />
                <ext:Button runat="server" ID="btnSinhSoHopDong" Icon="Reload">
                    <ToolTips>
                        <ext:ToolTip runat="server" Title="Hướng dẫn" Html="Sinh số hợp đồng mới (Chỉ áp dụng cho trường hợp chưa có số hợp đồng)" />
                    </ToolTips>
                    <Listeners>
                        <Click Handler="if (#{txtHopDongSoHopDong}.getValue().trim() != '' && #{txtHopDongSoHopDong}.getValue() != null) { this.blur(); alert('Số hợp đồng đã được sinh');} else {#{DirectMethods}.GenerateSoQD();}" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:CompositeField>
        <ext:Hidden runat="server" ID="hdfContractTypeId" />
        <ext:ComboBox runat="server" ID="cbHopDongLoaiHopDong" DisplayField="Name"
            ItemSelector="div.list-item" FieldLabel="Loại hợp đồng<span style='color:red;'>*</span>"
            Editable="false" ValueField="Id" AnchorHorizontal="99%" CtCls="requiredData"
            TabIndex="2">
            <Store>
                <ext:Store runat="server" ID="cbHopDongLoaiHopDongStore" AutoLoad="false" >
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="cat_ContractTimeSheetHandlerType" Mode="Value" />
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
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template2" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfContractTimeSheetHandlerTypeId}.setValue(#{cbHopDongLoaiHopDong}.getValue()); #{DirectMethods}.SetNgayHetHD();" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfContractTimeSheetHandlerTypeId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfContractStatusId" />
        <ext:ComboBox runat="server" ID="cbHopDongTinhTrangHopDong" DisplayField="Name"
            ItemSelector="div.list-item" FieldLabel="Tình trạng HĐ" Editable="false" ValueField="Id"
            AnchorHorizontal="99%" TabIndex="3">
            <Store>
                <ext:Store runat="server" ID="cbHopDongTinhTrangHopDongStore" AutoLoad="false">
                   <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="cat_ContractStatus" Mode="Value" />
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
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template4" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfContractStatusId}.setValue(#{cbHopDongTinhTrangHopDong}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfJobTitleOldId" />
        <ext:ComboBox runat="server" ID="cbHopDongCongViec" DisplayField="Name" FieldLabel="Chức danh"
            StoreID="storeJobTitle" Editable="false" ValueField="Id" AnchorHorizontal="99%"
            TabIndex="4" ItemSelector="div.list-item">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template5" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfJobTitleOldId}.setValue(#{cbHopDongCongViec}.getValue());" />
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfJobTitleOldId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfOldPositionId" />
        <ext:ComboBox runat="server" ID="cbx_HopDongChucVu" FieldLabel="Chức vụ" DisplayField="Name"
            ValueField="Id" AnchorHorizontal="99%" TabIndex="9" Editable="false" ItemSelector="div.list-item">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template27" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Store>
                <ext:Store runat="server" ID="cbx_HopDongChucVu_Store" AutoLoad="false">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
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
            </Store>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfOldPositionId}.setValue(#{cbx_HopDongChucVu}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();#{hdfOldPositionId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Container ID="Container43" runat="server" Layout="Column" Height="52">
            <Items>
                <ext:Container ID="Container44" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:DateField runat="server" Vtype="daterange" FieldLabel="Ngày ký kết<span style='color:red;'>*</span>"
                            ID="dfHopDongNgayHopDong" AnchorHorizontal="99%" Editable="true" MaskRe="/[0-9\/]/"
                            Format="dd/MM/yyyy" CtCls="requiredData" TabIndex="5">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfHopDongNgayKiKet}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                        <ext:TextField ID="txt_NguoiKyHD" runat="server" FieldLabel="Người ký HĐ" CtCls="requiredData"
                            AllowBlank="false" AnchorHorizontal="98%" MaxLength="20" LabelWidth="165" Width="300">
                        </ext:TextField>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container45" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                    <Items>
                        <ext:DateField runat="server" FieldLabel="Ngày hiệu lực<span style='color:red;'>*</span>"
                            ID="dfNgayCoHieuLuc" AnchorHorizontal="99%" Editable="true" MaskRe="/[0-9\/]/"
                            Format="dd/MM/yyyy" CtCls="requiredData" TabIndex="6">
                            <Listeners>
                                <Select Handler="#{DirectMethods}.SetNgayHetHD();" />
                            </Listeners>
                        </ext:DateField>
                        <ext:DateField runat="server" Vtype="daterange" FieldLabel="Ngày hết hạn HĐ" ID="dfHopDongNgayKiKet"
                            AnchorHorizontal="98%" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" TabIndex="7">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfHopDongNgayHopDong}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Hidden runat="server" ID="hdfHopDongTepTinDK" />
        <ext:CompositeField runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
            <Items>
                <ext:FileUploadField ID="fufHopDongTepTin" runat="server" EmptyText="Chọn tệp tin"
                    ButtonText="" Icon="Attach" Width="358">
                </ext:FileUploadField>
                <ext:Button runat="server" ID="btnHopDongAttachDownload" Icon="ArrowDown" ToolTip="Tải về">
                    <DirectEvents>
                        <Click OnEvent="btnHopDongAttachDownload_Click" IsUpload="true" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnHopDongAttachDelete" Icon="Delete" ToolTip="Xóa">
                    <DirectEvents>
                        <Click OnEvent="btnHopDongAttachDelete_Click" After="#{fufHopDongTepTin}.reset();">
                            <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                ConfirmRequest="true" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:CompositeField>
        <ext:ComboBox runat="server" ID="cbx_HopDongTrangThai" FieldLabel="Trạng thái HĐ"
            Hidden="false" AnchorHorizontal="99%" Editable="false" TabIndex="10">
            <Items>
                <ext:ListItem Text="Chưa duyệt" Value="ChuaDuyet" />
                <ext:ListItem Text="Đã duyệt" Value="DaDuyet" />
            </Items>
            <SelectedItem Value="DaDuyet" />
        </ext:ComboBox>
        <ext:TextArea runat="server" ID="txtHopDongGhiChu" FieldLabel="Ghi chú" AnchorHorizontal="99%"
            TabIndex="11" />
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdateHopDong" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputHopDong(#{fufHopDongTepTin}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateHopDong_Click" After="ResetWdHopDong();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnEditHopDong" Icon="Disk" Hidden="true" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputHopDong(#{fufHopDongTepTin}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateHopDong_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button20" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputHopDong(#{fufHopDongTepTin}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateHopDong_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button21" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdHopDong}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="if (#{hdfButtonClick}.getValue() == 'Insert') #{DirectMethods}.GenerateSoQD();" />
        <Hide Handler="#{btnUpdateHopDong}.show();#{btnEditHopDong}.hide();#{Button20}.show();ResetWdHopDong();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdKhaNang" AutoHeight="true" Width="400" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Năng lực, sở trường" Resizable="false">
    <Items>
        <ext:Hidden runat="server" ID="hdfAbilityId" />
        <ext:ComboBox runat="server" ID="cbKhaNang" DisplayField="Name" FieldLabel="Khả năng<span style='color:red;'>*</span>"
            ValueField="Id" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
            CtCls="requiredData">
            <Store>
                <ext:Store ID="cbKhaNangStore" runat="server" AutoLoad="false">
                     <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="cat_Ability" Mode="Value" />
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
            <Template ID="Template6" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfAbilityId}.setValue(#{cbKhaNang}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfAbilityId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfAbilityGraduationTypeId" />
        <ext:ComboBox runat="server" ID="cbKhaNangXepLoai" DisplayField="Name" FieldLabel="Mức đạt"
            ValueField="Id" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item" StoreID="StoreGraduationType">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template7" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfAbilityGraduationTimeSheetHandlerTypeId}.setValue(#{cbKhaNangXepLoai}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfAbilityGraduationTimeSheetHandlerTypeId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:TextArea ID="txtKhaNangGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%" />
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdateKhaNang" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKhaNang();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateKhaNang_Click" After="ResetWdKhaNang();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnEditKhaNang" runat="server" Text="Cập nhật" Hidden="true" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKhaNang();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateKhaNang_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button22" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKhaNang();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateKhaNang_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button23" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdKhaNang}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnUpdateKhaNang}.show();#{btnEditKhaNang}.hide();#{Button22}.show();ResetWdKhaNang();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdKhenThuong" AutoHeight="true" Width="550" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Khen thưởng" Resizable="false">
    <Items>
        <ext:Container ID="Container46" runat="server" Layout="ColumnLayout" Height="53">
            <Items>
                <ext:Container ID="Container47" runat="server" LabelAlign="left" Layout="FormLayout"
                    ColumnWidth="0.5">
                    <Items>
                        <ext:TextField runat="server" FieldLabel="Số quyết định" ID="txtKhenThuongSoQuyetDinh"
                            AnchorHorizontal="98%" MaxLength="20" />
                        <ext:Hidden runat="server" ID="hdfKhenThuongNguoiQD" />
                        <ext:TextField runat="server" ID="txtRewardDecisionMaker" FieldLabel="Người quyết định"
                            AnchorHorizontal="98%" Editable="false" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container48" runat="server" LabelAlign="left" Layout="FormLayout"
                    LabelWidth="110" ColumnWidth="0.5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfRewardFormId" />
                        <ext:ComboBox runat="server" ID="cbHinhThucKhenThuong" DisplayField="Name"
                            FieldLabel="Hình thức<span style='color:red;'>*</span>" ValueField="Id"
                            AnchorHorizontal="98%" Editable="false" ItemSelector="div.list-item"
                            CtCls="requiredData">
                            <Store>
                                <ext:Store ID="cbHinhThucKhenThuongStore" runat="server" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_Reward" Mode="Value" />
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
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template9" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();#{hdfRewardFormId}.setValue(#{cbHinhThucKhenThuong}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfRewardFormId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:DateField runat="server" FieldLabel="Ngày quyết định" ID="dfKhenThuongNgayQuyetDinh"
                            AnchorHorizontal="98%" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" />
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Hidden runat="server" ID="hdfIsDanhMuc" Text="0" />
        <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
        <ext:Hidden runat="server" ID="hdfReasonRewardId" />
        <ext:Hidden runat="server" ID="hdfLyDoKTTemp" />
        <ext:ComboBox runat="server" ID="cbLyDoKhenThuong" DisplayField="Name" FieldLabel="Lý do thưởng<span style='color:red;'>*</span>"
            ValueField="Id" AnchorHorizontal="99%" Editable="true" ItemSelector="div.list-item"
            MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..." CtCls="requiredData"
            EnableKeyEvents="true">
            <Store>
                <ext:Store ID="storeReasonReward" runat="server" AutoLoad="false">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="cat_ReasonReward" Mode="Value" />
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
            </Store>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template8" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfReasonRewardId}.setValue(#{cbLyDoKhenThuong}.getValue());
                                        #{hdfIsDanhMuc}.setValue('1');
                                        #{hdfLyDoKTTemp}.setValue(#{cbLyDoKhenThuong}.getRawValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfIsDanhMuc}.reset(); }" />
                <KeyUp Fn="searchBoxKT" />
                <Blur Handler="#{cbLyDoKhenThuong}.setRawValue(#{hdfLyDoKTTemp}.getValue());
                                    if (#{hdfIsDanhMuc}.getValue() != '1') {#{cbLyDoKhenThuong}.setValue(#{hdfLyDoKTTemp}.getValue());}" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfLevelRewardId" />
        <ext:ComboBox runat="server" ID="cbxCapKhenThuong" DisplayField="Name" FieldLabel="Cấp khen thưởng"
            ValueField="Id" AnchorHorizontal="99%" Editable="false" ItemSelector="div.list-item"
            LoadingText="Đang tải dữ liệu..." CtCls="requiredData" StoreID="StoreCapKhenThuongKyLuat">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template13" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfLevelRewardId}.setValue(#{cbxCapKhenThuong}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfLevelRewardId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfKhenThuongTepTinDinhKem" />
        <ext:CompositeField ID="CompositeField5" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
            <Items>
                <ext:FileUploadField ID="fufKhenThuongTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                    ButtonText="" Icon="Attach" Width="358">
                </ext:FileUploadField>
                <ext:Button runat="server" ID="btnKhenThuongDownload" Icon="ArrowDown" ToolTip="Tải về">
                    <DirectEvents>
                        <Click OnEvent="btnKhenThuongDownload_Click" IsUpload="true" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnKhenThuongDelete" Icon="Delete" ToolTip="Xóa">
                    <DirectEvents>
                        <Click OnEvent="btnKhenThuongDelete_Click" After="#{fufKhenThuongTepTinDinhKem}.reset();">
                            <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                ConfirmRequest="true" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:CompositeField>
        <ext:TextArea ID="txtKhenThuongGhiCHu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="99%"
            MaxLength="1000" />
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdateKhenThuong" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKhenThuong(#{fufKhenThuongTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateKhenThuong_Click" After="ResetWdKhenThuong();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnEditKhenThuong" runat="server" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKhenThuong(#{fufKhenThuongTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateKhenThuong_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button24" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKhenThuong(#{fufKhenThuongTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateKhenThuong_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button25" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdKhenThuong}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="if (#{hdfButtonClick}.getValue() == 'Insert') #{DirectMethods}.GenerateKhenThuongSoQD();" />
        <Hide Handler="#{btnUpdateKhenThuong}.show();#{btnEditKhenThuong}.hide();#{Button24}.show();ResetWdKhenThuong();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdKyLuat" AutoHeight="true" Width="550" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Kỷ luật" Resizable="false">
    <Items>
        <ext:Container ID="Container51" runat="server" Layout="Column" Height="55">
            <Items>
                <ext:Container ID="Container52" runat="server" LabelAlign="left" Layout="Form" ColumnWidth="0.5">
                    <Items>
                        <ext:TextField runat="server" FieldLabel="Số quyết định" ID="txtKyLuatSoQD" AnchorHorizontal="98%"
                            TabIndex="1" MaxLength="50" />
                        <ext:Hidden runat="server" ID="hdfKyLuatNguoiQD" />
                        <ext:TextField runat="server" ID="tgfKyLuatNguoiQD" FieldLabel="Người quyết định"
                            AnchorHorizontal="99%" Editable="false" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container53" runat="server" LabelAlign="left" Layout="Form" ColumnWidth="0.5"
                    LabelWidth="110">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfDisciplineFormId" />
                        <ext:ComboBox runat="server" ID="cbHinhThucKyLuat" DisplayField="Name" FieldLabel="Hình thức<span style='color:red;'>*</span>"
                            CtCls="requiredData" ValueField="Id" AnchorHorizontal="100%" TabIndex="4"
                            Editable="false" ItemSelector="div.list-item">
                            <Store>
                                <ext:Store ID="cbHinhThucKyLuatStore" runat="server" AutoLoad="false" >
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_Discipline" Mode="Value" />
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
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template11" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();#{hdfDisciplineFormId}.setValue(#{cbHinhThucKyLuat}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:DateField runat="server" FieldLabel="Ngày quyết định" ID="dfKyLuatNgayQuyetDinh"
                            AnchorHorizontal="100%" TabIndex="2" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" />
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Hidden runat="server" ID="hdfIsDanhMucKL" Text="0" />
        <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
        <ext:Hidden runat="server" ID="hdfReasonDisciplineId" />
        <ext:Hidden runat="server" ID="hdfLyDoKLTemp" />
        <ext:ComboBox runat="server" ID="cbLyDoKyLuat" DisplayField="Name" FieldLabel="Lý do<span style='color:red;'>*</span>"
            CtCls="requiredData" ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
            ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
            EnableKeyEvents="true">
            <Store>
                <ext:Store ID="cbLyDoKyLuatStore" runat="server" AutoLoad="false">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="cat_ReasonDiscipline" Mode="Value" />
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
            </Store>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template10" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show();  #{hdfReasonDisciplineId}.setValue(#{cbLyDoKyLuat}.getValue());
				                        #{hdfIsDanhMucKL}.setValue('1');
				                        #{hdfLyDoKLTemp}.setValue(#{cbLyDoKyLuat}.getRawValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfIsDanhMucKL}.reset();#{hdfReasonDisciplineId}.reset();  }" />
                <KeyUp Fn="searchBoxKL" />
                <Blur Handler="#{cbLyDoKyLuat}.setRawValue(#{hdfLyDoKLTemp}.getValue());
			                            if (#{hdfIsDanhMucKL}.getValue() != '1') {#{cbLyDoKyLuat}.setValue(#{hdfLyDoKLTemp}.getValue());}" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfLevelDisciplineId" />
        <ext:ComboBox runat="server" ID="cbxCapKyLuat" DisplayField="Name" FieldLabel="Cấp kỷ luật"
            ValueField="Id" AnchorHorizontal="99%" Editable="false" ItemSelector="div.list-item"
            LoadingText="Đang tải dữ liệu..." CtCls="requiredData" StoreID="StoreCapKhenThuongKyLuat">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template21" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfLevelDisciplineId}.setValue(#{cbxCapKyLuat}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfLevelDisciplineId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:Hidden runat="server" ID="hdfKyLuatTepTinDinhKem" />
        <ext:CompositeField ID="CompositeField6" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
            <Items>
                <ext:FileUploadField ID="fufKyLuatTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                    ButtonText="" Icon="Attach" Width="365">
                </ext:FileUploadField>
                <ext:Button runat="server" ID="btnKyLuatDownload" Icon="ArrowDown" ToolTip="Tải về">
                    <DirectEvents>
                        <Click OnEvent="btnKyLuatDownload_Click" IsUpload="true" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnKyLuatDelete" Icon="Delete" ToolTip="Xóa">
                    <DirectEvents>
                        <Click OnEvent="btnKyLuatDelete_Click" After="#{fufKyLuatTepTinDinhKem}.reset();">
                            <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                ConfirmRequest="true" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:CompositeField>
        <ext:TextArea ID="txtKyLuatGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
            TabIndex="6" />
    </Items>
    <Buttons>
        <ext:Button ID="btnCapNhatKyLuat" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKyLuat(#{fufKyLuatTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatKyLuat_Click" After="ResetWdKyLuat();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnEditKyLuat" runat="server" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKyLuat(#{fufKyLuatTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatKyLuat_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button26" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKyLuat(#{fufKyLuatTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatKyLuat_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button27" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdKyLuat}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="if (#{hdfButtonClick}.getValue() == 'Insert') #{DirectMethods}.GenerateKyLuatSoQD();" />
        <Hide Handler="#{btnCapNhatKyLuat}.show();#{btnEditKyLuat}.hide();#{Button26}.show();ResetWdKyLuat();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdQuanHeGiaDinh" AutoHeight="true" Width="550" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Quan hệ gia đình" Resizable="false">
    <Items>
        <ext:Container ID="Container56" runat="server" Layout="Column" Height="77">
            <Items>
                <ext:Container ID="Container57" runat="server" LabelAlign="left" Layout="Form" ColumnWidth="0.5">
                    <Items>
                        <ext:TextField runat="server" FieldLabel="Họ tên<span style='color:red;'>*</span>"
                            CtCls="requiredData" ID="txtQHGDHoTen" AnchorHorizontal="95%" TabIndex="1" MaxLength="50">
                            <Listeners>
                                <Blur Handler="ChuanHoaTen(#{txtQHGDHoTen});" />
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Phần mềm sẽ tự động chuẩn hóa họ và tên của bạn. Ví dụ: bạn nhập nguyễn văn huy, kết quả trả về Nguyễn Văn Huy." />
                            </ToolTips>
                        </ext:TextField>
                        <ext:NumberField runat="server" ID="txtQHGDNamSinh" FieldLabel="Năm sinh" AnchorHorizontal="95%"
                            TabIndex="2" MaxLength="4" MinLength="4" />
                        <ext:TextField runat="server" FieldLabel="Nghề nghiệp" ID="txtQHGDNgheNghiep" AnchorHorizontal="95%"
                            TabIndex="3" MaxLength="50" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container58" runat="server" LabelAlign="left" Layout="Form" ColumnWidth="0.5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfRelationshipSex" />
                        <ext:ComboBox runat="server" Editable="false" FieldLabel="Giới tính<span style='color:red;'>*</span>"
                            CtCls="requiredData" ID="cbQHGDGioiTinh" AnchorHorizontal="100%" SelectedIndex="0"
                            TabIndex="4">
                            <Items>
                                <ext:ListItem Value="1" Text="Nam" />
                                <ext:ListItem Value="0" Text="Nữ" />
                            </Items>
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfRelationshipSex}.setValue(#{cbQHGDGioiTinh}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:Hidden runat="server" ID="hdfRelationshipId" />
                        <ext:ComboBox runat="server" ID="cbQuanHeGiaDinh" Editable="false" DisplayField="Name"
                            FieldLabel="Quan hệ<span style='color:red;'>*</span>" CtCls="requiredData" ValueField="Id"
                            AnchorHorizontal="100%" TabIndex="5" ItemSelector="div.list-item">
                            <Store>
                                <ext:Store ID="cbQuanHeGiaDinhStore" runat="server" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_Relationship" Mode="Value" />
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
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template12" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfRelationshipId}.setValue(#{cbQuanHeGiaDinh}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:TextField runat="server" FieldLabel="Nơi làm việc" ID="txtQHGDNoiLamViec" AnchorHorizontal="100%"
                            TabIndex="6" MaxLength="50" />
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Container runat="server" Layout="ColumnLayout" Height="30" AnchorHorizontal="100%">
            <Items>
                <ext:Container Layout="FormLayout" runat="server" ID="ctainer" Height="30" ColumnWidth="0.5">
                    <Items>
                        <ext:Checkbox runat="server" ID="chkQHGDLaNguoiPhuThuoc" BoxLabel="Là người phụ thuộc"
                            Checked="false" TabIndex="7">
                            <Listeners>
                                <Check Handler="if (#{chkQHGDLaNguoiPhuThuoc}.checked == true) {
                                                    #{dfQHGDBatDauGiamTru}.enable();
                                                    #{dfQHGDKetThucGiamTru}.enable();
                                                }
                                                else
                                                {
                                                    #{dfQHGDBatDauGiamTru}.disable();
                                                    #{dfQHGDKetThucGiamTru}.disable();
                                                }
                                            " />
                            </Listeners>
                        </ext:Checkbox>
                    </Items>
                </ext:Container>
                <ext:NumberField runat="server" ID="txtSoCMT" FieldLabel="Số CMT" ColumnWidth="0.5"
                    TabIndex="8" />
            </Items>
        </ext:Container>
        <ext:Container runat="server" Layout="ColumnLayout" Height="35">
            <Items>
                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:DateField runat="server" ID="dfQHGDBatDauGiamTru" Disabled="true" Vtype="daterange"
                            FieldLabel="Bắt đầu giảm trừ" AnchorHorizontal="95%" Editable="true" MaskRe="/[0-9\/]/"
                            Format="dd/MM/yyyy" TabIndex="9">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfQHGDKetThucGiamTru}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:DateField runat="server" ID="dfQHGDKetThucGiamTru" Disabled="true" Vtype="daterange"
                            FieldLabel="Kết thúc giảm trừ" AnchorHorizontal="100%" Editable="true" MaskRe="/[0-9\/]/"
                            Format="dd/MM/yyyy" TabIndex="10">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfQHGDBatDauGiamTru}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:TextArea runat="server" FieldLabel="Ghi chú" ID="txtQHGDGhiChu" AnchorHorizontal="100%"
            TabIndex="11" MaxLength="500" />
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdateQuanHeGiaDinh" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputQHGD();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateQuanHeGiaDinh_Click" After="ResetWdQuanHeGiaDinh();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnUpdate" runat="server" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputQHGD();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateQuanHeGiaDinh_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button28" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputQHGD();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateQuanHeGiaDinh_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button29" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdQuanHeGiaDinh}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnUpdateQuanHeGiaDinh}.show();#{btnUpdate}.hide();#{Button28}.show();ResetWdQuanHeGiaDinh();" />
    </Listeners>
</ext:Window>

<ext:Window ID="wdQuaTrinhDieuChuyen" AutoHeight="true" Width="550" runat="server"
    Padding="6" EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true"
    Constrain="true" Icon="Add" Title="Quá trình công tác tại đơn vị" Resizable="false">
    <Items>
        <ext:TextField runat="server" ID="txtQTDCSoQuyetDinh" FieldLabel="Số quyết định"
            AnchorHorizontal="99%" TabIndex="1" />
        <ext:Container ID="Container28" runat="server" Layout="ColumnLayout" Height="55">
            <Items>
                <ext:Container ID="Container29" runat="server" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfQTDCNguoiQuyetDinh" />
                        <ext:TextField runat="server" ID="tgfQTDCNguoiQuyetDinh" FieldLabel="Người quyết định"
                            AnchorHorizontal="98%" Editable="false" TabIndex="2" />
                        <ext:DateField runat="server" ID="dfQTDCNgayQuyetDinh" AnchorHorizontal="98%" FieldLabel="Từ ngày<span style='color:red;'>*</span>"
                            CtCls="requiredData" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" TabIndex="3">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();" />
                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                            </Listeners>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container30" runat="server" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:Container runat="server" ID="ctn1" Layout="FormLayout" Height="27" />
                        <ext:DateField runat="server" ID="dfQTDCNgayCoHieuLuc" AnchorHorizontal="98%" FieldLabel="Đến ngày<span style='color:red;'>*</span>"
                            CtCls="requiredData" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" TabIndex="4">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();" />
                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                            </Listeners>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Hidden runat="server" ID="hdfNewDepartmentId" />
        <ext:ComboBox runat="server" ID="cbxQTDCBoPhanMoi" FieldLabel="Phòng ban<span style='color:red;'>*</span>"
            DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
            AnchorHorizontal="99%" TabIndex="6" Editable="false" CtCls="requiredData">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Store>
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
            </Store>
            <Template ID="Template15" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfNewDepartmentId}.setValue(#{cbxQTDCBoPhanMoi}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfNewDepartmentId}.reset();}" />
            </Listeners>
        </ext:ComboBox>
        <ext:Container runat="server" ID="ctndc1" Layout="ColumnLayout" AnchorHorizontal="100%"
            Height="27">
            <Items>
                <ext:Container runat="server" ID="ctndc2" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfNewPositionId" />
                        <ext:ComboBox runat="server" ID="cbxQTDCChucVuMoi" FieldLabel="Chức vụ" DisplayField="Name"
                            StoreID="store_ChucVu" ValueField="Id" AnchorHorizontal="98%" TabIndex="7" Editable="true"
                            ItemSelector="div.list-item" MinChars="1" LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template29" runat="server">
                                <Html>
                                    <tpl for=".">
					                    <div class="list-item"> 
						                    {Name}
					                    </div>
				                    </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfNewPositionId}.setValue(#{cbxQTDCChucVuMoi}.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfNewPositionId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="ctndc3" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfJobTitleNewId" />
                        <ext:ComboBox runat="server" ID="cbxCongViecMoi" FieldLabel="Chức danh" DisplayField="Name"
                            StoreID="storeJobTitle" ValueField="Id" AnchorHorizontal="98%" TabIndex="8" Editable="true"
                            ItemSelector="div.list-item" MinChars="1" LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template17" runat="server">
                                <Html>
                                    <tpl for=".">
					                    <div class="list-item"> 
						                    {Name}
					                    </div>
				                    </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfJobTitleNewId}.setValue(#{cbxCongViecMoi}.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfJobTitleNewId}.reset();}" />
                            </Listeners>
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Hidden runat="server" ID="hdfQTDCTepTinDinhKem" />
        <ext:CompositeField ID="CompositeField8" runat="server" AnchorHorizontal="99%" FieldLabel="Tệp tin đính kèm">
            <Items>
                <ext:FileUploadField ID="fufQTDCTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                    ButtonText="" Icon="Attach" Width="358" TabIndex="9">
                </ext:FileUploadField>
                <ext:Button runat="server" ID="btnQTDCDownload" Icon="ArrowDown" ToolTip="Tải về">
                    <DirectEvents>
                        <Click OnEvent="btnQTDCDownload_Click" IsUpload="true" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnQTDCDelete" Icon="Delete" ToolTip="Xóa">
                    <DirectEvents>
                        <Click OnEvent="btnQTDCDelete_Click" After="#{fufQTDCTepTinDinhKem}.reset();">
                            <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                ConfirmRequest="true" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:CompositeField>
        <ext:TextArea runat="server" FieldLabel="Ghi chú" ID="txtDieuChuyenGhiChu" AnchorHorizontal="99%"
            TabIndex="10">
        </ext:TextArea>
    </Items>
    <Buttons>
        <ext:Button ID="btnCapNhatQuaTrinhDieuChuyen" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputQuaTrinhDieuChuyen(#{fufQTDCTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatQuaTrinhDieuChuyen_Click" After="ResetWdQuaTrinhDieuChuyen();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnUpdateQuaTrinhDieuChuyen" Hidden="true" runat="server" Text="Cập nhật"
            Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputQuaTrinhDieuChuyen(#{fufQTDCTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatQuaTrinhDieuChuyen_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button32" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputQuaTrinhDieuChuyen(#{fufQTDCTepTinDinhKem}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatQuaTrinhDieuChuyen_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button33" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdQuaTrinhDieuChuyen}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="if (#{hdfButtonClick}.getValue() == 'Insert') #{DirectMethods}.GenerateQTDCSoQD();" />
        <Hide Handler="#{btnCapNhatQuaTrinhDieuChuyen}.show();#{btnUpdateQuaTrinhDieuChuyen}.hide();#{Button32}.show();ResetWdQuaTrinhDieuChuyen();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdAddTaiSan" AutoHeight="true" Width="500" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Tài sản cấp phát" Resizable="false">
    <Content>
        <ext:TextField runat="server" ID="txtAssetName" AnchorHorizontal="100%" FieldLabel="Tên tài sản<span style='color:red;'>*</span>"
            CtCls="requiredData">
        </ext:TextField>
        <ext:Container runat="server" AnchorHorizontal="100%" Height="27" Layout="ColumnLayout">
            <Items>
                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5">
                    <Items>
                        <ext:TextField runat="server" CtCls="requiredData" ID="txtTheTaiSan" FieldLabel="Thẻ tài sản"
                            AnchorHorizontal="98%" />
                    </Items>
                </ext:Container>
                <ext:DateField ID="tsDateField" runat="server" FieldLabel="Ngày nhận" ColumnWidth="0.5"
                    Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" />
            </Items>
        </ext:Container>
        <ext:CompositeField runat="server" FieldLabel="Số lượng<span style='color:red;'>*</span>"
            AnchorHorizontal="100%">
            <Items>
                <ext:TextField runat="server" ID="txtTaiSanSoLuong" Width="80" MaskRe="/[0-9]/" CtCls="requiredData" />
                <ext:DisplayField runat="server" Text="Đơn vị tính" />
                <ext:TextField runat="server" ID="txtUnitCode" Editable="true" Width="220" FieldLabel="Đơn vị tính">
                </ext:TextField>
            </Items>
        </ext:CompositeField>
        <ext:TextField ID="tsTxtinhTrang" runat="server" FieldLabel="Tình trạng<span style='color:red;'>*</span>"
            CtCls="requiredData" AnchorHorizontal="100%" MaxLength="50" />
        <ext:TextArea ID="tsGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
            MaxLength="50" />
    </Content>
    <Buttons>
        <ext:Button ID="Button2" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputTaiSan();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateTaiSan_Click" After="ResetWdTaiSan();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnEditTaiSan" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputTaiSan();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateTaiSan_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnUpdateTaiSan" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputTaiSan();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateTaiSan_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button6" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdAddTaiSan}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{Button2}.show();#{btnEditTaiSan}.hide();#{btnUpdateTaiSan}.show();ResetWdTaiSan();" />
    </Listeners>
</ext:Window>
<ext:Window runat="server" Hidden="true" Resizable="false" Padding="6" Layout="FormLayout"
    Modal="true" Width="500" ID="wdAttachFile" Title="Tệp tin đính kèm" Icon="Attach"
    AutoHeight="true" Constrain="true" LabelWidth="120">
    <Items>
        <ext:TextField ID="txtFileName" runat="server" AnchorHorizontal="100%" FieldLabel="Tên tệp tin<span style='color:red;'>*</span>"
            CtCls="requiredData" MaxLength="200" />
        <ext:Hidden runat="server" ID="hdfDinhKem" />
        <ext:Hidden runat="server" ID="hdfFileSizeKB" />
        <ext:FileUploadField ID="file_cv" runat="server" FieldLabel="Tệp tin đính kèm<span style='color:red;'>*</span>"
            CtCls="requiredData" AnchorHorizontal="100%" Icon="Attach">
            <Listeners>
                <FileSelected Handler="GetFileNameUpload();" />
            </Listeners>
        </ext:FileUploadField>
        <ext:TextArea runat="server" FieldLabel="Ghi chú" ID="txtGhiChu" AnchorHorizontal="100%">
        </ext:TextArea>
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdateAtachFile" runat="server" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputAttachFile();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateAtachFile_Click" After="ResetWdAttachFile();">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button10" runat="server" Icon="Disk" Text="Cập nhật & Đóng lại">
            <Listeners>
                <Click Handler="return CheckInputAttachFile();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateAtachFile_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnEditAttachFile" Hidden="true" runat="server" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputAttachFile();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateAtachFile_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button13" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdAttachFile}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="#{file_cv}.enable();" />
        <Hide Handler="#{btnEditAttachFile}.hide();#{Button10}.show();#{btnUpdateAtachFile}.show();ResetWdAttachFile();" />
    </Listeners>
</ext:Window>
<ext:Window runat="server" ID="wdAddBangCap" Width="600" EnableViewState="false"
    Title="Quá trình đào tạo, bồi dưỡng chuyên môn nghiệp vụ" Resizable="false" Constrain="true"
    Modal="true" Icon="Add" AutoHeight="true" Hidden="true" Padding="6">
    <Items>
        <ext:Container runat="server" ID="Container13" Layout="ColumnLayout" Height="133">
            <Items>
                <ext:Container runat="server" ID="Container14" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfMaTruongDaoTao" />
                        <ext:ComboBox AnchorHorizontal="98%" ValueField="Id" HideTrigger="false" DisplayField="Name"
                            runat="server" FieldLabel="Trường đào tạo<span style='color:red;'>*</span>" CtCls="requiredData"
                            PageSize="15" ItemSelector="div.search-item" MinChars="1" EmptyText="Gõ để tìm kiếm"
                            ID="cbx_NoiDaoTaoBangCap" LoadingText="Đang tải dữ liệu...">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                             <Store>
                                <ext:Store ID="storeUniversity" runat="server" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_University" Mode="Value" />
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
                            </Store>
                            <Template ID="Template19" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="search-item">
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <ToolTips>
                                <ext:ToolTip runat="server" ID="ttTruongDT" Title="Hướng dẫn" Html="Nhập tên trường đào tạo để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                            </ToolTips>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfMaTruongDaoTao}.setValue(#{cbx_NoiDaoTaoBangCap}.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfMaTruongDaoTao}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:Hidden ID="hdfTrainingSystemId" runat="server" />
                        <ext:ComboBox runat="server" DisplayField="Name" ValueField="Id"
                            ID="cbx_hinhthucdaotaobang" FieldLabel="Hình thức<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="98%" ItemSelector="div.list-item">
                             <Store>
                                <ext:Store runat="server" ID="storeTrainingSystem" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_TrainingSystem" Mode="Value" />
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
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template20" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfTrainingSystemId}.setValue(#{cbx_hinhthucdaotaobang}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfTrainingSystemId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:TextField ID="txt_khoa" AllowBlank="true" runat="server" FieldLabel="Khoa" AnchorHorizontal="98%"
                            MaxLength="200">
                        </ext:TextField>
                        <ext:Checkbox ID="Chk_DaTotNghiep" runat="server" AnchorHorizontal="98%" BoxLabel="Đã tốt nghiệp"
                            Checked="false">
                            <Listeners>
                                <Check Handler="
                                    if (#{Chk_DaTotNghiep}.checked == true) {
                                        #{cbx_xeploaiBangCap}.enable(); 
                                    }
                                    else {
                                        #{cbx_xeploaiBangCap}.disable(); 
                                        #{cbx_xeploaiBangCap}.reset(); 
                                    }
                                " />
                            </Listeners>
                        </ext:Checkbox>
                        <ext:TextField runat="server" ID="txtTuNam" FieldLabel="Từ năm" AnchorHorizontal="98%"
                            MaskRe="[0-9]" MaxLength="4" MinLength="4">
                        </ext:TextField>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container15" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfMaChuyenNganh" />
                        <ext:ComboBox AnchorHorizontal="98%" ValueField="Id" DisplayField="Name" runat="server"
                            FieldLabel="Chuyên ngành<span style='color:red;'>*</span>" CtCls="requiredData"
                            PageSize="15" HideTrigger="false" ItemSelector="div.search-item" MinChars="1"
                            EmptyText="Gõ để tìm kiếm" ID="cbx_ChuyenNganhBangCap" LoadingText="Đang tải dữ liệu...">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                             <Store>
                                <ext:Store ID="storeIndustry" runat="server" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_Industry" Mode="Value" />
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
                            </Store>
                            <Template ID="Template16" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="search-item">
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <ToolTips>
                                <ext:ToolTip runat="server" ID="ToolTip3" Title="Hướng dẫn" Html="Nhập tên chuyên ngành để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                            </ToolTips>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfMaChuyenNganh}.setValue(#{cbx_ChuyenNganhBangCap}.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfMaChuyenNganh}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:Hidden runat="server" ID="hdfEducationId" />
                        <ext:ComboBox runat="server" DisplayField="Name" ValueField="Id" MinChars="1" PageSize="15"
                            EmptyText="Gõ để tìm kiếm" ID="cbx_trinhdobangcap" FieldLabel="Trình độ<span style='color:red;'>*</span>"
                            LoadingText="Đang tải dữ liệu..." CtCls="requiredData" AnchorHorizontal="98%"
                            ItemSelector="div.list-item">
                            
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template22" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Store>
                                <ext:Store runat="server" ID="storeEducation2" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                <Select Handler="this.triggers[0].show(); #{hdfEducationId}.setValue(#{cbx_trinhdobangcap}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfEducationId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:Hidden runat="server" ID="hdfQuocGia" />
                        <ext:ComboBox runat="server" ID="cbx_quocgia" FieldLabel="Quốc gia đào tạo" DisplayField="Name"
                            MinChars="1" ValueField="Id" AnchorHorizontal="98%" ListWidth="200" Editable="true"
                            ItemSelector="div.list-item" PageSize="15" LoadingText="Quốc gia đào tạo" EmptyText="Gõ để tìm kiếm"
                            StoreID="cbx_quocgia_Store">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template1" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();#{hdfQuocGia}.setValue(#{cbx_quocgia}.getValue());" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfQuocGia}.reset();" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:Hidden runat="server" ID="hdfGraduationTypeId" />
                        <ext:ComboBox runat="server" ID="cbx_xeploaiBangCap" ValueField="Id" DisplayField="Name"
                            FieldLabel="Xếp loại" AnchorHorizontal="98%" ItemSelector="div.list-item" MinChars="1"
                            EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu" Disabled="true" StoreID="StoreGraduationType">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template23" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfGraduationTimeSheetHandlerTypeId}.setValue(#{cbx_xeploaiBangCap}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfGraduationTimeSheetHandlerTypeId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:TextField runat="server" ID="txtDenNam" FieldLabel="Đến năm" AnchorHorizontal="98%"
                            MaskRe="[0-9]" MaxLength="4" MinLength="4">
                        </ext:TextField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnUpdateBangCap" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputBangCap();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateBangCap_Click" After="ResetWdBangCap();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btn_EditBangCap" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputBangCap();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateBangCap_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Edit">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnUpdateandCloseBangCap" Icon="Disk" Text="Cập nhật & đóng lại">
            <Listeners>
                <Click Handler="return CheckInputBangCap();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateBangCap_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btn_ThoatBangCap" Icon="Decline" Text="Đóng lại">
            <Listeners>
                <Click Handler="#{wdAddBangCap}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnUpdateBangCap}.show();#{btn_EditBangCap}.hide();#{btnUpdateandCloseBangCap}.show();ResetWdBangCap();" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdNgoaiNgu" AutoHeight="true" Width="500" runat="server" Padding="6"
    EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
    Icon="Add" Title="Ngoại ngữ" Resizable="false">
    <Items>
        <ext:Hidden runat="server" ID="hdfLanguageLevelId" />
        <ext:ComboBox runat="server" ID="cbxNgoaiNgu" Editable="false" AnchorHorizontal="100%"
            DisplayField="Name" FieldLabel="Loại ngoại ngữ<span style='color:red;'>*</span>"
            CtCls="requiredData" ValueField="Id" ItemSelector="div.list-item">
            <Store>
                <ext:Store ID="cbxNgoaiNguStore" runat="server" AutoLoad="false">
                     <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                    </Proxy>
                        <BaseParams>
                        <ext:Parameter Name="objname" Value="cat_LanguageLevel" Mode="Value" />
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
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Template ID="Template26" runat="server">
                <Html>
                    <tpl for=".">
						<div class="list-item"> 
							{Name}
						</div>
					</tpl>
                </Html>
            </Template>
            <Listeners>
                <Select Handler="this.triggers[0].show(); #{hdfLanguageLevelId}.setValue(#{cbxNgoaiNgu}.getValue());" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfLanguageLevelId}.reset(); }" />
            </Listeners>
        </ext:ComboBox>
        <ext:TextField ID="txtXepLoaiNgoaiNgu" runat="server" FieldLabel="Xếp loại" AnchorHorizontal="100%"
            MaxLength="500" />
        <ext:TextArea ID="txtGhiChuNgoaiNgu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%"
            MaxLength="2000" />
    </Items>
    <Buttons>
        <ext:Button ID="btnNgoaiNguInsert" runat="server" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputNgoaiNgu();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnNgoaiNgu_Click" After="ResetWdNgoaiNgu();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnNgoaiNguEdit" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputNgoaiNgu();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnNgoaiNgu_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Update">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnNgoaiNguClose" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputNgoaiNgu();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnNgoaiNgu_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button16" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdNgoaiNgu}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnNgoaiNguInsert}.show();#{btnNgoaiNguEdit}.hide();#{btnNgoaiNguClose}.show();ResetWdNgoaiNgu();" />
    </Listeners>
</ext:Window>
<ext:Window runat="server" ID="wdKinhNghiemLamViec" Width="600" EnableViewState="false"
    Title="Công tác trước khi vào đơn vị" Resizable="false" Constrain="true" Modal="true"
    Icon="Add" AutoHeight="true" Hidden="true" Padding="6" Layout="FormLayout">
    <Items>
        <ext:Container runat="server" ID="Container9" AnchorHorizontal="100%" Layout="ColumnLayout"
            Height="52">
            <Items>
                <ext:Container runat="server" ID="cot1" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:TextField runat="server" ID="txt_noilamviec" FieldLabel="Nơi làm việc<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="95%" AllowBlank="false" MaxLength="500">
                        </ext:TextField>
                        <ext:DateField runat="server" Vtype="daterange" EnableKeyEvents="true" ID="dfKNLVTuNgay"
                            FieldLabel="Từ ngày<span style='color:red;'>*</span>" CtCls="requiredData" Editable="true"
                            MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" AnchorHorizontal="95%">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{dfKNLVDenNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container12" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:TextField runat="server" ID="txt_vitriconviec" FieldLabel="Vị trí công việc<span style='color:red;'>*</span>"
                            CtCls="requiredData" AllowBlank="false" AnchorHorizontal="100%" MaxLength="500">
                        </ext:TextField>
                        <ext:DateField runat="server" ID="dfKNLVDenNgay" Vtype="daterange" FieldLabel="Đến ngày"
                            Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{dfKNLVTuNgay}" Mode="Value" />
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:TextField runat="server" ID="txtThanhTichTrongCongViec" FieldLabel="Thành tích"
            EmptyText="Thành tích đạt được trong công việc" AllowBlank="false" AnchorHorizontal="100%"
            MaxLength="500">
        </ext:TextField>
        <ext:Container AnchorHorizontal="100%" runat="server" Height="35" Layout="ColumnLayout">
            <Items>
                <ext:Container ColumnWidth="0.5" runat="server" Layout="FormLayout" Height="30">
                    <Items>
                        <ext:NumberField ID="nbfMucLuong" AnchorHorizontal="95%" runat="server" FieldLabel="Mức lương" />
                    </Items>
                </ext:Container>
                <ext:TextField runat="server" ID="txtLyDoThoiViec" FieldLabel="Lý do chuyển công tác"
                    ColumnWidth="0.5" />
            </Items>
        </ext:Container>
        <ext:TextArea runat="server" ID="txtGhiChuKinhNghiemLamViec" FieldLabel="Ghi chú"
            AnchorHorizontal="100%" MaxLength="500" Height="50">
        </ext:TextArea>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="Update" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputKinhNghiemLamViec();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="Update_Click" After="ResetWdKinhNghiemLamViec();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnEditKinhNghiem" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputKinhNghiemLamViec();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="Update_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Edit">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="UpdateandClose" Icon="Disk" Text="Cập nhật & đóng lại">
            <Listeners>
                <Click Handler="return CheckInputKinhNghiemLamViec();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="Update_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="Close" Icon="Decline" Text="Đóng lại">
            <Listeners>
                <Click Handler="#{wdKinhNghiemLamViec}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{Update}.show();#{btnEditKinhNghiem}.hide();#{UpdateandClose}.show();ResetWdKinhNghiemLamViec();" />
    </Listeners>
</ext:Window>
<ext:Window runat="server" ID="wdAddChungChi" Width="600" EnableViewState="false"
    Title="Chứng chỉ" Resizable="true" Constrain="true" Modal="true" Icon="Add" AutoHeight="true"
    Hidden="true" Padding="6" Layout="FormLayout">
    <Items>
        <ext:Container runat="server" ID="Container10" Layout="ColumnLayout" Height="27">
            <Items>
                <ext:Container runat="server" ID="Container11" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfCertificateId" />
                        <ext:ComboBox runat="server" ValueField="Id" DisplayField="Name"
                            ID="cbx_certificate" FieldLabel="Chứng chỉ<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="95%" TabIndex="1" Editable="false" ItemSelector="div.list-item">
                            <Store>
                                <ext:Store runat="server" ID="cbx_tenchungchiStore" AutoLoad="false">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                    </Proxy>
                                    <BaseParams>
                                        <ext:Parameter Name="objname" Value="cat_Certificate" Mode="Value" />
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
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template24" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show();#{hdfCertificateId}.setValue(#{cbx_certificate}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfCertificateId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container16" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:Hidden runat="server" ID="hdfCertificateGraduationTypeId" />
                        <ext:ComboBox runat="server" ID="cbx_XepLoaiChungChi" DisplayField="Name"
                            ValueField="Id" FieldLabel="Xếp loại<span style='color:red;'>*</span>"
                            CtCls="requiredData" AnchorHorizontal="100%" TabIndex="2" Editable="false" ItemSelector="div.list-item" StoreID="StoreGraduationType">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            </Triggers>
                            <Template ID="Template25" runat="server">
                                <Html>
                                    <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                </Html>
                            </Template>
                            <Listeners>
                                <Select Handler="this.triggers[0].show(); #{hdfCertificateGraduationTimeSheetHandlerTypeId}.setValue(#{cbx_XepLoaiChungChi}.getValue());" />
                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfCertificateGraduationTimeSheetHandlerTypeId}.reset(); }" />
                            </Listeners>
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:TextField runat="server" ID="txt_EducationPlace" FieldLabel="Nơi đào tạo" AnchorHorizontal="100%"
            TabIndex="3" MaxLength="500">
        </ext:TextField>
        <ext:Container runat="server" ID="Container17" Layout="ColumnLayout" Height="27">
            <Items>
                <ext:Container runat="server" ID="Container23" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:DateField runat="server" ID="df_NgayCap" Vtype="daterange" FieldLabel="Ngày cấp"
                            Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" AnchorHorizontal="95%" TabIndex="4">
                            <CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="#{df_NgayHetHan}" Mode="Value">
                                </ext:ConfigItem>
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container24" Layout="FormLayout" ColumnWidth=".5">
                    <Items>
                        <ext:DateField runat="server" ID="df_NgayHetHan" Vtype="daterange" FieldLabel="Ngày hết hạn"
                            Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%"
                            TabIndex="5">
                            <CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="#{df_NgayCap}" Mode="Value">
                                </ext:ConfigItem>
                            </CustomConfig>
                        </ext:DateField>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:TextArea runat="server" ID="txtGhiChuChungChi" FieldLabel="Ghi chú" AnchorHorizontal="100%"
            TabIndex="6" MaxLength="5000">
        </ext:TextArea>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnUpdateChungChi" Icon="Disk" Text="Cập nhật">
            <Listeners>
                <Click Handler="return CheckInputChungChi();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateChungChi_Click" After="ResetWdChungChi();">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="False" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnEditChungChi" Hidden="true" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckInputChungChi();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateChungChi_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Edit">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnUpdateandCloseChungChi" Icon="Disk" Text="Cập nhật & đóng lại">
            <Listeners>
                <Click Handler="return CheckInputChungChi();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnUpdateChungChi_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                    <ExtraParams>
                        <ext:Parameter Name="Close" Value="True">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="Button3" Icon="Decline" Text="Đóng lại">
            <Listeners>
                <Click Handler="#{wdAddChungChi}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="#{btnUpdateChungChi}.show();#{btnEditChungChi}.hide();#{btnUpdateandCloseChungChi}.show();ResetWdChungChi();" />
    </Listeners>
</ext:Window>

<ext:TabPanel ID="TabPanelBottom" runat="server" EnableTabScroll="true" AnchorHorizontal="100%" Height="220" Border="false">
    <Plugins>
        <ext:TabCloseMenu CloseOtherTabsText="Đóng Tab khác" CloseAllTabsText="Đóng tất cả các Tab"
            CloseTabText="Đóng Tab">
        </ext:TabCloseMenu>
        <ext:TabScrollerMenu ID="TabScrollerMenu1" runat="server" />
    </Plugins>
    <Items>
        <ext:Panel ID="panelGeneralInformation" Cls="panelGeneralInformation" Title="<%$ Resources:HOSO, general_information%>"
            Height="200" Padding="6" runat="server" Layout="FormLayout">
            <Items>
                <ext:Container ID="Container1" Layout="ColumnLayout" runat="server" Height="200"
                    AnchorHorizontal="100%">
                    <Items>
                        <ext:Container ID="Container20" Layout="FormLayout" runat="server" Width="130">
                            <Items>
                                <ext:ImageButton ID="hsImage" ImageUrl="../../../File/ImagesEmployee/No_person.jpg" runat="server" Width="120" Height="150" TabIndex="0">
                                </ext:ImageButton>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" ID="Contain8" Layout="FormLayout" ColumnWidth="0.9">
                            <Items>
                                <ext:Container runat="server" ID="Contain7" Layout="ColumnLayout" AnchorHorizontal="100%" Height="200">
                                    <Items>
                                        <ext:Container ID="Contain1" Layout="FormLayout" runat="server" ColumnWidth="0.33" LabelWidth="120">
                                            <Items>
                                                <ext:TextField FieldLabel="Số hiệu CBCC" EnableKeyEvents="true" AnchorHorizontal="95%" runat="server" ID="txtEmployeeCode"/>
                                                <ext:TextField FieldLabel="Họ và tên khai sinh" AnchorHorizontal="95%" runat="server" ID="txtFullName"/>
                                                <ext:TextField FieldLabel="Tên gọi khác" AnchorHorizontal="95%" runat="server" ID="txtAlias"/>
                                                <ext:TextField FieldLabel="Sinh ngày" runat="server" AnchorHorizontal="95%" ID="txtBirthDate"/>
                                                <ext:TextField FieldLabel="Nơi sinh" runat="server" AnchorHorizontal="95%" ID="txtBirthPlace"/>
                                                <ext:TextField FieldLabel="Quê quán" runat="server" AnchorHorizontal="95%" ID="txtHometown"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container2" Layout="FormLayout" runat="server" ColumnWidth=".33" LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Thành phần bản thân" runat="server" AnchorHorizontal="95%" ID="txtPersonalClassName"/>
                                                <ext:TextField FieldLabel="Thành phần gia đình" runat="server" AnchorHorizontal="95%" ID="txtFamilyClassName"/>
                                                <ext:TextField AnchorHorizontal="95%" FieldLabel="Dân tộc" runat="server" ID="txtFolkName"/>
                                                <ext:TextField runat="server" FieldLabel="Tôn giáo" ID="txtReligionName" AnchorHorizontal="95%"/>
                                                <ext:TextField runat="server" FieldLabel="Hộ khẩu thường trú" ID="txtResidentPlace" AnchorHorizontal="95%"
                                                    EmptyText="Nơi đăng ký hộ khẩu thường trú"/>
                                                <ext:TextField runat="server" FieldLabel="Nơi ở hiện nay" ID="txtAddress" AnchorHorizontal="95%"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container3" Layout="FormLayout" runat="server" ColumnWidth=".33"
                                            LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Trình độ phổ thông" AnchorHorizontal="95%" runat="server" ID="txtBasicEducationName"/>
                                                <ext:TextField FieldLabel="Trình độ chuyên môn" runat="server" AnchorHorizontal="95%" ID="txtEducationName"/>
                                                <ext:TextField FieldLabel="Lý luận chính trị" EnableKeyEvents="true" runat="server" AnchorHorizontal="95%" ID="txtPoliticLevelName"/>
                                                <ext:TextField FieldLabel="Quản lý nhà nước" runat="server" AnchorHorizontal="95%" ID="txtManagementLevelName"/>
                                                <ext:TextField FieldLabel="Ngoại ngữ" AnchorHorizontal="95%" runat="server" ID="txtLanguageLevelName"/>
                                                <ext:TextField FieldLabel="Tin học" runat="server" AnchorHorizontal="95%" ID="txtITLevelName"/>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
            </Items>
            <Listeners>
                <Activate Handler="" />
            </Listeners>
        </ext:Panel>
        <ext:Panel ID="panelJobInformation" Cls="panelJobInformation" Title="Thông tin nghề nghiệp"
            Height="200" Padding="6" runat="server" Layout="FormLayout">
              <Items>
                <ext:Container ID="Container39" Layout="ColumnLayout" runat="server" Height="200"
                    AnchorHorizontal="100%">
                    <Items>
                        <ext:Container runat="server" ID="Container42" Layout="FormLayout" ColumnWidth="0.9">
                            <Items>
                                <ext:Container runat="server" ID="Container49" Layout="ColumnLayout" AnchorHorizontal="100%" Height="200">
                                    <Items>
                                        <ext:Container ID="Container50" Layout="FormLayout" runat="server" ColumnWidth=".33" LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Nghề khi tuyển" runat="server" AnchorHorizontal="95%" ID="txtPreviousJob"
                                                    EmptyText="Nghề nghiệp khi được tuyển dụng"/>
                                                <ext:TextField FieldLabel="Ngày tuyển dụng" runat="server" AnchorHorizontal="95%" ID="txtRecruimentDate"/>
                                                <ext:TextField AnchorHorizontal="95%" FieldLabel="Cơ quan tuyển dụng" runat="server" ID="txtRecruimentDepartment"/>
                                                <ext:TextField runat="server" FieldLabel="Chức vụ hiện tại" ID="txtPositionName" AnchorHorizontal="95%"/>
                                                <ext:TextField runat="server" FieldLabel="Ngày kết nạp Đảng" ID="txtCPVJoinedDate" AnchorHorizontal="95%"
                                                    EmptyText="Ngày vào Đảng cộng sản Việt Nam"/>
                                                <ext:TextField runat="server" FieldLabel="Ngày chính thức" ID="txtCPVOfficialJoinedDate" AnchorHorizontal="95%"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container54" Layout="FormLayout" runat="server" ColumnWidth=".33"
                                            LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Số thẻ Đảng viên" AnchorHorizontal="95%" runat="server" ID="txtCPVCardNumber"/>
                                                <ext:TextField FieldLabel="Chức vụ Đảng" runat="server" AnchorHorizontal="95%" ID="txtCPVPositionName"/>
                                                <ext:TextField FieldLabel="Ngày vào Đoàn" EnableKeyEvents="true" runat="server" AnchorHorizontal="95%"
                                                    ID="txtVYUJoinedDate"/>
                                                <ext:TextField FieldLabel="Chức vụ Đoàn" runat="server" AnchorHorizontal="95%" ID="txtVYUPositionName"/>
                                                <ext:TextField FieldLabel="Công việc chính" AnchorHorizontal="95%" runat="server" ID="txtAssignedWork"/>
                                                <ext:TextField FieldLabel="Ngạch công chức" runat="server" AnchorHorizontal="95%"
                                                    ID="txtQuantumName"/>
                                            </Items>
                                        </ext:Container>
                                         <ext:Container ID="Container55" Layout="FormLayout" runat="server" ColumnWidth=".33"
                                            LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Mã ngạch" AnchorHorizontal="95%" runat="server" ID="txtQuantumCode">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Bậc lương" runat="server" AnchorHorizontal="95%"
                                                    ID="txtSalaryGrade">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Hệ số" EnableKeyEvents="true" runat="server" AnchorHorizontal="95%"
                                                    ID="txtSalaryFactor">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Ngày hưởng" runat="server" AnchorHorizontal="95%" ID="txtEffectiveDate"/>
                                                <ext:TextField FieldLabel="Phụ cấp chức vụ" AnchorHorizontal="95%" runat="server" ID="txtPositionAllowance">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Phụ cấp khác" runat="server" AnchorHorizontal="95%"
                                                    ID="txtOtherAllowance"/>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
            </Items>
            <Listeners>
                <Activate Handler="" />
            </Listeners>
        </ext:Panel>
        <ext:Panel ID="panelPersonalInformation" Cls="panelPersonalInformation" Title="<%$ Resources:HOSO, personal_information%>"
            Height="200" Padding="6" runat="server" Layout="FormLayout">
              <Items>
                <ext:Container ID="Container35" Layout="ColumnLayout" runat="server" Height="200"
                    AnchorHorizontal="100%">
                    <Items>
                        <ext:Container runat="server" ID="Container37" Layout="FormLayout" ColumnWidth="0.9">
                            <Items>
                                <ext:Container runat="server" ID="Container38" Layout="ColumnLayout" AnchorHorizontal="100%" Height="200">
                                    <Items>
                                        <ext:Container ID="Container40" Layout="FormLayout" runat="server" ColumnWidth=".33" LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Số CMND" runat="server" AnchorHorizontal="95%" ID="txtIDNumber"/>
                                                <ext:TextField FieldLabel="Ngày cấp" runat="server" AnchorHorizontal="95%" ID="txtIDIssueDate"
                                                    EmptyText="Ngày cấp CMND"/>
                                                <ext:TextField AnchorHorizontal="95%" FieldLabel="Nơi cấp" runat="server" ID="txtIDIssuePlaceName"
                                                    EmptyText="Nơi cấp CMND"/>
                                                <ext:TextField runat="server" FieldLabel="Ngày nhập ngũ" ID="txtArmyJoinedDate" AnchorHorizontal="95%"/>
                                                <ext:TextField runat="server" FieldLabel="Ngày xuất ngũ" ID="txtArmyLeftDate" AnchorHorizontal="95%"/>
                                                <ext:TextField runat="server" FieldLabel="Quân hàm cao nhất" ID="txtArmyLevelName" AnchorHorizontal="95%"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container41" Layout="FormLayout" runat="server" ColumnWidth=".33"
                                            LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Danh hiệu cao nhất" AnchorHorizontal="95%" runat="server" ID="txtTitleAwarded"
                                                    EmptyText="Danh hiệu được phong tặng cao nhất"/>
                                                <ext:TextField FieldLabel="Sở trường công tác" runat="server" AnchorHorizontal="95%" ID="txtSkills"/>
                                                <ext:TextField FieldLabel="Tình trạng sức khoẻ" EnableKeyEvents="true" runat="server" AnchorHorizontal="95%"
                                                    ID="txtHealthStatusName"/>
                                                <ext:TextField FieldLabel="Chiều cao" runat="server" AnchorHorizontal="95%" ID="txtHeight"/>
                                                <ext:TextField FieldLabel="Cân nặng" AnchorHorizontal="95%" runat="server" ID="txtWeight"/>
                                                <ext:TextField FieldLabel="Nhóm máu" runat="server" AnchorHorizontal="95%"
                                                    ID="txtBloodGroup"/>
                                            </Items>
                                        </ext:Container>
                                         <ext:Container ID="Container36" Layout="FormLayout" runat="server" ColumnWidth=".33"
                                            LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Là thương binh hạng" AnchorHorizontal="95%" runat="server" ID="txtRankWounded">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Gia đình chính sách" runat="server" AnchorHorizontal="95%"
                                                    ID="txtFamilyPolicyName">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Số sổ BHXH" EnableKeyEvents="true" runat="server" AnchorHorizontal="95%"
                                                    ID="txtInsuranceNumber">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Ngày cấp" runat="server" AnchorHorizontal="95%" ID="txtInsuranceIssueDate"
                                                    EmptyText="Ngày cấp (sổ BHXH)"/>
                                                <ext:TextField FieldLabel="Trạng thái làm việc" AnchorHorizontal="95%" runat="server" ID="txtWorkStatusName">
                                                </ext:TextField>
                                                <ext:TextField FieldLabel="Trạng thái hôn nhân" runat="server" AnchorHorizontal="95%"
                                                    ID="txtMaritalStatusName"/>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
            </Items>
            <Listeners>
                <Activate Handler="" />
            </Listeners>
        </ext:Panel>
         <ext:Panel ID="panelOtherInformation" Cls="panelOtherInformation" Title="Thông tin khác"
            Height="200" Padding="6" runat="server" Layout="FormLayout">
               <Items>
                <ext:Container ID="Container59" Layout="ColumnLayout" runat="server" Height="200"
                    AnchorHorizontal="100%">
                    <Items>
                        <ext:Container runat="server" ID="Container60" Layout="FormLayout" ColumnWidth="0.9">
                            <Items>
                                <ext:Container runat="server" ID="Container61" Layout="ColumnLayout" AnchorHorizontal="100%" Height="200">
                                    <Items>
                                        <ext:Container ID="Container62" Layout="FormLayout" runat="server" ColumnWidth=".33" LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Mã số thuế cá nhân" runat="server" AnchorHorizontal="95%" ID="txtPersonalTaxCode"/>
                                                <ext:TextField FieldLabel="Điện thoại di động" runat="server" AnchorHorizontal="95%" ID="txtCellPhoneNumber"
                                                    />
                                                <ext:TextField AnchorHorizontal="95%" FieldLabel="Điện thoại nhà riêng" runat="server" ID="txtHomePhoneNumber"/>
                                                <ext:TextField runat="server" FieldLabel="Điện thoại cơ quan" ID="txtWorkPhoneNumber" AnchorHorizontal="95%"/>
                                                <ext:TextField runat="server" FieldLabel="Email nội bộ" ID="txtWorkEmail" AnchorHorizontal="95%"/>
                                                <ext:TextField runat="server" FieldLabel="Email cá nhân" ID="txtPersonalEmail" AnchorHorizontal="95%"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container63" Layout="FormLayout" runat="server" ColumnWidth=".33"
                                            LabelWidth="150">
                                            <Items>
                                                <ext:TextField FieldLabel="Số tài khoản chính" AnchorHorizontal="95%" runat="server" ID="txtAccountNumber"
                                                    EmptyText="Số tài khoản ngân hàng"/>
                                                <ext:TextField FieldLabel="Mở tại" runat="server" AnchorHorizontal="95%" ID="txtBankName"
                                                    EmptyText="Tên ngân hàng"/>
                                                <ext:TextField FieldLabel="Người liên hệ" EnableKeyEvents="true" runat="server" AnchorHorizontal="95%"
                                                    ID="txtContactPersonName" EmptyText="Người liên hệ khi cần"/>
                                                <ext:TextField FieldLabel="Mối quan hệ" runat="server" AnchorHorizontal="95%" ID="txtContactRelation"
                                                    EmptyText="Mối quan hệ với cán bộ"/>
                                                <ext:TextField FieldLabel="Số điện thoại" AnchorHorizontal="95%" runat="server" ID="txtContactPhoneNumber"
                                                    EmptyText="Số điện thoại(di động)"/>
                                                <ext:TextField FieldLabel="Địa chỉ" runat="server" AnchorHorizontal="95%"
                                                    ID="txtContactAddress"/>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
            </Items>
            <Listeners>
                <Activate Handler="" />
            </Listeners>
        </ext:Panel>
        <ext:Panel ID="panelQuaTrinhHocTap" Title="Quá trình đào tạo, bồi dưỡng chuyên môn nghiệp vụ"
            runat="server" Closable="true" Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{MenuItemHocTap}.setDisabled(false);" />
                <Activate Handler="if(#{hdfQTHTRecordId}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{Store_BangCap}.reload();
                                        #{hdfQTHTRecordId}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden runat="server" ID="hdfQTHTRecordId" />
                <ext:GridPanel ID="GridPanel_BangCap" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoExpandColumn="UniversityName" AutoScroll="true" AnchorHorizontal="100%" Region="Center">
                    <Store>
                        <ext:Store ID="Store_BangCap" AutoLoad="false" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" OnRefreshData="Store_BangCap_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="RecordId" />
                                        <ext:RecordField Name="FromDate" DateFormat="yyyy" />
                                        <ext:RecordField Name="ToDate" DateFormat="yyyy" />
                                        <ext:RecordField Name="Faculty" />
                                        <ext:RecordField Name="IndustryName" />
                                        <ext:RecordField Name="EducationName" />
                                        <ext:RecordField Name="TrainingSystemName" />
                                        <ext:RecordField Name="UniversityName" />
                                        <ext:RecordField Name="NationName" />
                                        <ext:RecordField Name="IsGraduated" />
                                        <ext:RecordField Name="GraduateTimeSheetHandlerTypeName" />
                                        <ext:RecordField Name="DecisionNumber" />
                                        <ext:RecordField Name="DecisionDate" />
                                        <ext:RecordField Name="IsApproved" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel20" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:DateColumn ColumnID="FromDate" Width="70" Header="Từ năm" DataIndex="FromDate" Format="yyyy"/>
                            <ext:DateColumn ColumnID="ToDate" Width="70" Header="Đến năm" DataIndex="ToDate" Format="yyyy" />
                            <ext:Column ColumnID="Faculty" Header="Khoa" DataIndex="Faculty" Width="150" />
                            <ext:Column ColumnID="IndustryName" Header="Chuyên ngành" DataIndex="IndustryName" Width="200" />
                            <ext:Column ColumnID="EducationName" Width="70" Header="Trình độ" DataIndex="EducationName" />
                            <ext:Column ColumnID="TrainingSystemName" Width="100" Header="Hình thức" DataIndex="TrainingSystemName" />
                            <ext:Column ColumnID="UniversityName" Header="Trường đào tạo" DataIndex="UniversityName" />
                            <ext:Column ColumnID="NationName" Width="120" Header="Quốc gia đào tạo" DataIndex="NationName" />
                            <ext:Column ColumnID="IsGraduated" Width="100" Header="Đã tốt nghiệp" DataIndex="IsGraduated">
                                <Renderer Fn="GetBooleanIcon" />
                            </ext:Column>
                            <ext:Column ColumnID="GraduateTimeSheetHandlerTypeName" Width="70" Header="Xếp loại" DataIndex="GraduateTimeSheetHandlerTypeName" />
                            <ext:Column ColumnID="DecisionNumber" Width="100" Header="Quyết định số" DataIndex="DecisionNumber" />
                            <ext:Column ColumnID="DecisionDate" Width="100" Header="Ngày" DataIndex="DecisionDate" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel_BangCap" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <DblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelBangCapChungChi" Title="Chứng chỉ" runat="server" Closable="true"
            Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{MenuItemBangCapChungChi}.setDisabled(false);" />
                <Activate Handler="if(#{hdfCheckChungChiLoaded}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{Store_BangCapChungChi}.reload();
                                        #{hdfCheckChungChiLoaded}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden runat="server" ID="hdfCheckChungChiLoaded" />
                <ext:GridPanel ID="GridPanel_ChungChi" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoExpandColumn="Note" AutoScroll="true" AnchorHorizontal="100%"
                    Region="Center">
                    <Store>
                        <ext:Store ID="Store_BangCapChungChi" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            AutoLoad="false" OnRefreshData="Store_BangCapChungChi_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="RecordId" />
                                        <ext:RecordField Name="CertificateName" />
                                        <ext:RecordField Name="GraduationTimeSheetHandlerTypeName" />
                                        <ext:RecordField Name="EducationPlace" />
                                        <ext:RecordField Name="IssueDate" />
                                        <ext:RecordField Name="ExpirationDate" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel18" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="CertificateName" Header="Tên chứng chỉ" DataIndex="CertificateName" />
                            <ext:Column ColumnID="GraduationTimeSheetHandlerTypeName" Header="Xếp loại" DataIndex="GraduationTimeSheetHandlerTypeName" Width="60" />
                            <ext:Column ColumnID="EducationPlace" Header="Nơi đào tạo" DataIndex="EducationPlace" Width="200" />
                            <ext:Column ColumnID="IssueDate" Width="80" Header="Ngày cấp" DataIndex="IssueDate" />
                            <ext:Column ColumnID="ExpirationDate" Width="80" Header="Ngày hết hạn" DataIndex="ExpirationDate" />
                            <ext:Column ColumnID="Note" Width="150" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel_ChungChi" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelQuaTrinhDaoTao" Title="Quá trình đào tạo" Layout="BorderLayout" Hidden="true" runat="server" Closable="true" CloseAction="Hide">
            <Listeners>
                <Close Handler="#{mnuQuaTrinhDaoTao}.setDisabled(false);" />
                <Activate Handler="if(#{hdfQTDTRecordId}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreQuaTrinhDaoTao}.reload();
                                        #{hdfQTDTRecordId}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfQTDTRecordId" runat="server" />
                <ext:GridPanel ID="GridPanelQTDT" runat="server" Region="Center" StripeRows="true" Border="false" TrackMouseOver="true" AutoExpandColumn="TrainingName">
                    <Store>
                        <ext:Store ID="StoreQuaTrinhDaoTao" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            AutoLoad="false" OnRefreshData="StoreQuaTrinhDaoTao_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="StartDate" />
                                        <ext:RecordField Name="EndDate" />
                                        <ext:RecordField Name="TrainingName" />
                                        <ext:RecordField Name="NationName" />
                                        <ext:RecordField Name="TrainingPlace" />
                                        <ext:RecordField Name="Reason" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="40" />
                            <ext:Column ColumnID="TrainingName" Width="250" Header="Tên khóa đào tạo" DataIndex="TrainingName" />
                            <ext:DateColumn ColumnID="StartDate" Header="Ngày bắt đầu" Format="dd/MM/yyyy"
                                DataIndex="StartDate" Width="90" />
                            <ext:DateColumn ColumnID="EndDate" Header="Ngày kết thúc" Format="dd/MM/yyyy"
                                Width="90" DataIndex="EndDate" />
                            <ext:Column ColumnID="Reason" Width="250" Header="Lý do đào tạo" DataIndex="Reason" />
                            <ext:Column ColumnID="TrainingPlace" Width="250" Header="Nơi đào tạo" DataIndex="TrainingPlace" />
                            <ext:Column ColumnID="NationName" Width="120" Header="Quốc gia đào tạo" DataIndex="NationName" />
                            <ext:Column ColumnID="Note" Width="200" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <View>
                        <ext:LockingGridView ID="lkvKhoaDaoTao" />
                    </View>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelQuaTrinhDaoTao" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelBaoHiem" Title="Bảo hiểm" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuBaoHiem}.setDisabled(false);" />
                <Activate Handler="if(#{hdfBHRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreBaoHiem}.reload();
                                        #{hdfBHRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfBHRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelBaoHiem" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AnchorHorizontal="100%" Region="Center" AutoExpandColumn="PositionName">
                    <Store>
                        <ext:Store ID="StoreBaoHiem" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreBaoHiems_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="FromDate" />
                                        <ext:RecordField Name="ToDate" />
                                        <ext:RecordField Name="PositionName" />
                                        <ext:RecordField Name="SalaryFactor" />
                                        <ext:RecordField Name="Allowance" />
                                        <ext:RecordField Name="SalaryLevel" />
                                        <ext:RecordField Name="Rate" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModelBaoHiem" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="PositionName" Header="Chức vụ" DataIndex="PositionName" Align="Left" />
                            <ext:Column ColumnID="SalaryFactor" Header="Hệ số lương" DataIndex="SalaryFactor" Align="Right" />
                            <ext:Column ColumnID="Allowance" Header="Phụ cấp" DataIndex="Allowance" Align="Right">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="SalaryLevel" Header="Mức lương" DataIndex="SalaryLevel" Align="Right">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="Rate" Header="Tỷ lệ" DataIndex="Rate" Align="Right">
                                <Renderer Fn="RenderPercent" />
                            </ext:Column>
                            <ext:DateColumn ColumnID="FromDate" Header="Từ ngày" Width="90" DataIndex="FromDate" Format="dd/MM/yyyy"
                                Align="Center"/>
                            <ext:DateColumn ColumnID="ToDate" Header="Đến ngày" Width="90" DataIndex="ToDate" Format="dd/MM/yyyy"
                                Align="Center"/>
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" Align="Left"
                                Width="300" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelBaoHiem" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelDaiBieu" Title="Đại biểu" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuDaiBieu}.setDisabled(false);" />
                <Activate Handler="if(#{hdfDBRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreDaiBieu}.reload();
                                        #{hdfDBRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfDBRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelDaiBieu" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AnchorHorizontal="100%" Region="Center" AutoExpandColumn="TimeSheetHandlerType">
                    <Store>
                        <ext:Store ID="StoreDaiBieu" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreDaiBieu_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="FromDate" />
                                        <ext:RecordField Name="ToDate" />
                                        <ext:RecordField Name="TimeSheetHandlerType" />
                                        <ext:RecordField Name="Term" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel3" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="TimeSheetHandlerType" Header="Loại hình" DataIndex="TimeSheetHandlerType" />
                            <ext:Column ColumnID="Term" Header="Nhiệm kỳ" DataIndex="Term" />
                            <ext:DateColumn ColumnID="FromDate" Header="Từ ngày" DataIndex="FromDate" Format="dd/MM/yyyy"/>
                            <ext:DateColumn ColumnID="ToDate" Header="Từ ngày" DataIndex="ToDate" Format="dd/MM/yyyy"/>
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelDaiBieu" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelDienBienLuong" Title="Diễn biến lương" Hidden="true" runat="server"
            Closable="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuDienBienLuong}.setDisabled(false);" />
                <Activate Handler="if(#{hdfDBLRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreDienBienLuong}.reload();
                                        #{hdfDBLRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfDBLRecordID" runat="server" />
                <ext:Hidden ID="hdfIDHoSoLuong" runat="server" />
                <ext:GridPanel ID="GridPanelDienBienLuong" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandMin="100">
                    <Store>
                        <ext:Store ID="StoreDienBienLuong" AutoSave="true" ShowWarningOnFailure="false" SkipIdForNewRecords="false"
                            RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreDienBienLuong_OnRefreshData"
                            OnBeforeStoreChanged="HandleChangesDelete" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="ID">
                                    <Fields>
                                        <ext:RecordField Name="ID" />
                                        <ext:RecordField Name="DecisionNumber" />
                                        <ext:RecordField Name="Name" />
                                        <ext:RecordField Name="SalaryFactor" />
                                        <ext:RecordField Name="SalaryBasic" />
                                        <ext:RecordField Name="SalaryInsurance" />
                                        <ext:RecordField Name="SalaryGrade" />
                                        <ext:RecordField Name="SalaryPayDate" />
                                        <ext:RecordField Name="DecisionDate" />
                                        <ext:RecordField Name="EffectiveDate" />
                                        <ext:RecordField Name="EffectiveEndDate" />
                                        <ext:RecordField Name="DecisionMaker" />
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="OutFrame" />
                                        <ext:RecordField Name="QuantumName" />
                                        <ext:RecordField Name="PositionAllowance" />
                                        <ext:RecordField Name="OtherAllowance" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel5" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" Locked="true" />
                            <ext:Column ColumnID="DecisionNumber" Width="100" Header="Số quyết định" DataIndex="DecisionNumber"
                                Locked="true" Align="Left" />
                            <ext:Column ColumnID="Name" Width="200" Header="Tên quyết định" DataIndex="Name"
                                Locked="true" Align="Left" />
                            <ext:Column ColumnID="SalaryFactor" Width="90" Header="Hệ số lương" DataIndex="SalaryFactor"
                                Align="Right" />
                            <ext:Column ColumnID="QuantumName" Width="150" Header="Tên ngạch" DataIndex="QuantumName"
                                Align="Left" />
                            <ext:Column ColumnID="SalaryBasic" Width="90" Header="Lương cứng" DataIndex="SalaryBasic"
                                Align="Right">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="PositionAllowance" Width="120" Header="Phụ cấp chức vụ" DataIndex="PositionAllowance"
                                Align="Right">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="OtherAllowance" Width="100" Header="Phụ cấp khác" DataIndex="OtherAllowance"
                                Align="Right">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="SalaryInsurance" Width="125" Header="Lương đóng BHXH" DataIndex="SalaryInsurance"
                                Align="Right">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="SalaryGrade" Width="80" Header="Bậc lương" DataIndex="SalaryGrade"
                                Align="Right" />
                            <ext:DateColumn Width="110" ColumnID="DecideDate" Header="Ngày quyết định"
                                DataIndex="DecisionDate" Align="center" Format="dd/MM/yyyy"/>
                            <ext:DateColumn Width="110" ColumnID="EffectiveDate" Header="Ngày có hiệu lực"
                                DataIndex="EffectiveDate" Align="center" Format="dd/MM/yyyy" />
                            <ext:DateColumn ColumnID="SalaryPayDate" Width="120" Header="Ngày hưởng lương" 
                                DataIndex="SalaryPayDate" Align="center" Format="dd/MM/yyyy"/>
                            <ext:Column ColumnID="DecisionMaker" Width="120" Header="Người quyết định" DataIndex="DecisionMaker" />
                            <ext:Column ColumnID="OutFrame" Width="80" Header="Vượt khung" DataIndex="OutFrame" />
                            <ext:Column ColumnID="Note" Width="300" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelDienBienLuong" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="#{hdfIDHoSoLuong}.setValue(#{RowSelectionModelDienBienLuong}.getSelected().id);" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <View>
                        <ext:LockingGridView runat="server" ID="lkv1" />
                    </View>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelHopDong" Title="Hợp đồng" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuHopDong}.setDisabled(false);" />
                <Activate Handler="if(#{hdfHDRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreHopDong}.reload();
                                        #{hdfHDRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfHDRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelHopDong" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="ContractTimeSheetHandlerTypeName">
                    <Store>
                        <ext:Store ID="StoreHopDong" AutoLoad="false" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            OnRefreshData="StoreHopDong_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="ContractNumber" />
                                        <ext:RecordField Name="ContractTimeSheetHandlerTypeName" />
                                        <ext:RecordField Name="JobName" />
                                        <ext:RecordField Name="ContractDate" />
                                        <ext:RecordField Name="ContractEndDate" />
                                        <ext:RecordField Name="EffectiveDate" />
                                        <ext:RecordField Name="ContractStatusName" />
                                        <ext:RecordField Name="AttachFileName" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel7" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="AttachFileName" Width="25" DataIndex="" Align="Center" Locked="true">
                                <Commands>
                                    <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                        <ToolTip Text="Tải tệp tin đính kèm" />
                                    </ext:ImageCommand>
                                </Commands>
                                <PrepareCommand Fn="prepare" />
                            </ext:Column>
                            <ext:Column ColumnID="ContractNumber" Header="Số hợp đồng" DataIndex="ContractNumber" />
                            <ext:Column ColumnID="ContractTimeSheetHandlerTypeName" Header="Loại hợp đồng" DataIndex="ContractTimeSheetHandlerTypeName" />
                            <ext:Column ColumnID="JobName" Header="Chức danh" DataIndex="JobName" Width="200" />
                            <ext:DateColumn ColumnID="ContractDate" Header="Ngày ký kết" DataIndex="ContractDate" Align="Center" Format="dd/MM/yyyy"/>
                            <ext:DateColumn ColumnID="EffectiveDate" Header="Ngày có hiệu lực"  Align="Center" Width="120" DataIndex="EffectiveDate" Format="dd/MM/yyyy"/>
                            <ext:DateColumn ColumnID="ContractEndDate" Header="Ngày kết thúc" Align="Center" DataIndex="ContractEndDate" Format="dd/MM/yyyy"/>
                            <ext:Column ColumnID="ContractStatusName" Header="Tình trạng" DataIndex="ContractStatusName"  Width="300" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelHopDong" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                        <Command Handler="#{DirectMethods}.DownloadAttach(record.data.AttachFileName);" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelMoveBusiness" Title="Điều động" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuMoveBusiness}.setDisabled(false);" />
                <Activate Handler="if(#{hdfMoveRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('DieuDongDi');
                                        #{storeBusiness}.reload();
                                        #{hdfMoveRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfMoveRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelMove" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ hiện tại" Width="140" Align="Left" DataIndex="CurrentPosition" />
                            <ext:Column Header="Đơn vị nơi đến" Width="170" DataIndex="DestinationDepartment" />
                            <ext:Column Header="Chức vụ điều động" Width="140" Align="Left" DataIndex="NewPosition" />
                            <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelTurnover" Title="Luân chuyển" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuTurnover}.setDisabled(false);" />
                <Activate Handler="if(#{hdfTurnoverRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('LuanChuyenDi');
                                        #{storeBusiness}.reload();
                                        #{hdfTurnoverRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfTurnoverRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelTurnover" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel6" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ hiện tại" Width="140" Align="Left" DataIndex="CurrentPosition" />
                            <ext:Column Header="Đơn vị nơi đến" Width="170" DataIndex="DestinationDepartment" />
                            <ext:Column Header="Chức vụ luân chuyển" Width="140" Align="Left" DataIndex="NewPosition" />
                            <ext:DateColumn Header="Thời hạn luân chuyển" Width="145" Align="Left" DataIndex="ExpireDate" Format="dd/MM/yyyy" />
                            <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelSecondment" Title="Biệt phái" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuSecondment}.setDisabled(false);" />
                <Activate Handler="if(#{hdfSecondmentRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('BietPhaiDi');
                                        #{storeBusiness}.reload();
                                        #{hdfSecondmentRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfSecondmentRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelSecondment" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel14" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ hiện tại" Width="140" Align="Left" DataIndex="CurrentPosition" />
                            <ext:Column Header="Đơn vị nơi đến" Width="170" DataIndex="DestinationDepartment" />
                            <ext:Column Header="Chức vụ biệt phái" Width="140" Align="Left" DataIndex="NewPosition" />
                            <ext:DateColumn Header="Thời hạn biệt phái" Width="145" Align="Left" DataIndex="ExpireDate" Format="dd/MM/yyyy" />
                            <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelPlurality" Title="Kiêm nhiệm" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuPlurality}.setDisabled(false);" />
                <Activate Handler="if(#{hdfPluralityRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('KiemNhiem');
                                        #{storeBusiness}.reload();
                                        #{hdfPluralityRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfPluralityRecordID" runat="server" />
                <ext:GridPanel ID="GridPanel1" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel15" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ hiện tại" Width="140" Align="Left" DataIndex="CurrentPosition" />
                            <ext:Column Header="Đơn vị kiêm nhiệm" Width="170" DataIndex="DestinationDepartment" />
                            <ext:Column Header="Chức vụ kiêm nhiệm" Width="140" Align="Left" DataIndex="NewPosition" />
                            <ext:DateColumn Header="Thời hạn kiêm nhiệm" Width="145" Align="Left" DataIndex="ExpireDate" Format="dd/MM/yyyy" />
                            <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel4" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelDismissal" Title="Miễn nhiệm, bãi nhiệm" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuDismissal}.setDisabled(false);" />
                <Activate Handler="if(#{hdfDismissalRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('MienNhiemBaiNhiem');
                                        #{storeBusiness}.reload();
                                        #{hdfDismissalRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfDismissalRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelDismissal" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel16" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ miễn nhiệm, bãi nhiệm" Width="140" Align="Left" DataIndex="NewPosition" />
                            <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel5" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelTransfer" Title="Thuyên chuyển, điều chuyển" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuTransfer}.setDisabled(false);" />
                <Activate Handler="if(#{hdfTransferRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('ThuyenChuyenDieuChuyen');
                                        #{storeBusiness}.reload();
                                        #{hdfTransferRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfTransferRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelTransfer" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel21" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Đơn vị nơi đến" Width="120" DataIndex="DestinationDepartment" />
                            <ext:Column Header="Chức vụ nơi đến" Width="140" Align="Left" DataIndex="NewPosition" />
                            <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelRetirement" Title="Nghỉ hưu" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuRetirement}.setDisabled(false);" />
                <Activate Handler="if(#{hdfRetirementRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('NghiHuu');
                                        #{storeBusiness}.reload();
                                        #{hdfRetirementRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfRetirementRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelRetirement" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel22" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ" Width="140" Align="Left" DataIndex="CurrentPosition" />
                            <ext:DateColumn Header="Thời gian nghỉ hưu" Width="145" Align="Left" DataIndex="ExpireDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel7" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelKhaNang" Title="Năng lực, sở trường" runat="server" Hidden="true"
            Closable="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuKhaNang}.setDisabled(false);" />
                <Activate Handler="if(#{hdfKhaNangRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreKhaNang}.reload();
                                        #{hdfKhaNangRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfKhaNangRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelKhaNang" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="AbilityName">
                    <Store>
                        <ext:Store ID="StoreKhaNang" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreKhaNang_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="AbilityName" />
                                        <ext:RecordField Name="GraduationTimeSheetHandlerTypeName" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel8" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="AbilityName" Header="Khả năng" DataIndex="AbilityName" />
                            <ext:Column ColumnID="GraduationTimeSheetHandlerTypeName" Header="Mức đạt" DataIndex="GraduationTimeSheetHandlerTypeName" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelKhaNang" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelEmulationTitle" Title="Danh hiệu thi đua" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{menuEmulationTitle}.setDisabled(false);" />
                <Activate Handler="if(#{hdfEmulationTitleRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{hdfBusinessTimeSheetHandlerType}.setValue('DanhHieuThiDua');
                                        #{storeBusiness}.reload();
                                        #{hdfEmulationTitleRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfEmulationTitleRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelEmulationTitle" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Description" StoreID="storeBusiness">
                    <ColumnModel ID="ColumnModel25" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                            <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                            <ext:DateColumn Header="Ngày ký" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                            <ext:Column Header="Đơn vị đang công tác" Width="120" DataIndex="CurrentDepartment" />
                            <ext:Column Header="Chức vụ" Width="140" Align="Left" DataIndex="CurrentPosition" />
                            <ext:Column Header="Danh hiệu công nhận" Width="140" Align="Left" DataIndex="EmulationTitle" />
                            <ext:Column Header="Số tiền thưởng" Width="140" Align="Left" DataIndex="Money">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column Header="Cơ quan phong tặng" Width="120" DataIndex="SourceDepartment" />
                            <ext:Column Header="Người ký" Width="180" DataIndex="DecisionMaker"/>
                            <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                            <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel8" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelKhenThuong" Title="Khen thưởng" runat="server" Hidden="true"
            Closable="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuKhenThuong}.setDisabled(false);" />
                <Activate Handler="if(#{hdfKhenThuongRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreKhenThuong}.reload();
                                        #{hdfKhenThuongRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfKhenThuongRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelKhenThuong" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Reason">
                    <Store>
                        <ext:Store ID="StoreKhenThuong" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreKhenThuong_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="DecisionNumber" />
                                        <ext:RecordField Name="DecisionDate" />
                                        <ext:RecordField Name="Reason" />
                                        <ext:RecordField Name="DecisionMaker" />
                                        <ext:RecordField Name="FormRewardName" />
                                        <ext:RecordField Name="MoneyAmount" DefaultValue="0"/>
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="AttachFileName" />
                                        <ext:RecordField Name="Point" />
                                        <ext:RecordField Name="LevelRewardName" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel9" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="AttachFileName" Width="25" DataIndex="" Align="Center" Locked="true">
                                <Commands>
                                    <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                        <ToolTip Text="Tải tệp tin đính kèm" />
                                    </ext:ImageCommand>
                                </Commands>
                                <PrepareCommand Fn="prepare" />
                            </ext:Column>
                            <ext:Column ColumnID="Reason" Width="200" Header="Lý do khen thưởng" DataIndex="Reason" />
                            <ext:Column ColumnID="FormRewardName" Width="150" Header="Hình thức khen thưởng"
                                DataIndex="FormRewardName" />
                            <ext:Column ColumnID="DecisionNumber" Header="Số QĐ" DataIndex="DecisionNumber" />
                            <ext:Column ColumnID="DecisionMaker" Header="Người quyết định" Width="120" DataIndex="DecisionMaker" />
                            <ext:DateColumn ColumnID="DecisionDate" Header="Ngày quyết định" DataIndex="DecisionDate" Format="dd/MM/yyyy"/>
                            <ext:Column ColumnID="LevelRewardName" Header="Cấp khen thưởng" DataIndex="LevelRewardName"
                                Width="120" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelKhenThuong" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                        <Command Handler="#{DirectMethods}.DownloadAttach(record.data.AttachFileName);" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelKiLuat" Title="Kỷ luật" runat="server" Hidden="true" Closable="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuKyLuat}.setDisabled(false);" />
                <Activate Handler="if(#{hdfKyLuatRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreKyLuat}.reload();
                                        #{hdfKyLuatRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfKyLuatRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelKyLuat" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Note" AutoExpandMin="150">
                    <Store>
                        <ext:Store ID="StoreKyLuat" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreKyLuat_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="DecisionNumber" />
                                        <ext:RecordField Name="DecisionMaker" />
                                        <ext:RecordField Name="DecisionDate" />
                                        <ext:RecordField Name="Reason" />
                                        <ext:RecordField Name="FormDisciplineName" />
                                        <ext:RecordField Name="MoneyAmount" />
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="AttachFileName" />
                                        <ext:RecordField Name="Point" />
                                        <ext:RecordField Name="LevelDisciplineName" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel10" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="AttachFileName" Width="25" DataIndex="" Align="Center" Locked="true">
                                <Commands>
                                    <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                        <ToolTip Text="Tải tệp tin đính kèm" />
                                    </ext:ImageCommand>
                                </Commands>
                                <PrepareCommand Fn="prepare" />
                            </ext:Column>
                            <ext:Column ColumnID="Reason" Width="200" Header="Lý do kỷ luật" DataIndex="Reason" />
                            <ext:Column ColumnID="FormDisciplineName" Width="150" Header="Hình thức kỷ luật" DataIndex="FormDisciplineName" />
                            <ext:Column ColumnID="DecisionNumber" Header="Số quyết định" DataIndex="DecisionNumber" />
                            <ext:Column ColumnID="DecisionMaker" Header="Người quyết định" Width="120" DataIndex="DecisionMaker" />
                            <ext:DateColumn ColumnID="DecisionDate" Header="Ngày quyết định" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                            <ext:Column ColumnID="LevelDisciplineName" Header="Cấp kỷ luật" DataIndex="LevelDisciplineName"
                                Width="120" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelKyLuat" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                        <Command Handler="#{DirectMethods}.DownloadAttach(record.data.AttachFileName);" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelQuanHeGiaDinh" Title="Quan hệ gia đình" runat="server" Hidden="true"
            Closable="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuQuanHeGiaDinh}.setDisabled(false);" />
                <Activate Handler="if(#{hdfQHGDRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreQHGD}.reload();
                                        #{hdfQHGDRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfQHGDRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelQHGD" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Note">
                    <Store>
                        <ext:Store ID="StoreQHGD" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreQHGD_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="FullName" />
                                        <ext:RecordField Name="BirthYear" />
                                        <ext:RecordField Name="RelationName" />
                                        <ext:RecordField Name="Occupation" />
                                        <ext:RecordField Name="WorkPlace" />
                                        <ext:RecordField Name="Sex" />
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="IDNumber" />
                                        <ext:RecordField Name="IsDependent" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel11" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="FullName" Width="200" Header="Họ tên" DataIndex="FullName" />
                            <ext:Column ColumnID="BirthYear" Width="70" Header="Năm sinh" DataIndex="BirthYear" />
                             <ext:Column ColumnID="Sex" Width="60" Header="Giới tính" DataIndex="Sex">
                                <Renderer Fn="RenderSex" />
                            </ext:Column>
                            <ext:Column ColumnID="RelationName" Header="Quan hệ" DataIndex="RelationName" />
                            <ext:Column ColumnID="Occupation" Header="Nghề nghiệp" DataIndex="Occupation" />
                            <ext:Column ColumnID="Workplace" Header="Nơi làm việc" DataIndex="WorkPlace" />
                            <ext:Column ColumnID="IDNumber" Header="Số CMT" Width="100" DataIndex="IDNumber" />
                            <ext:CheckColumn ColumnID="IsDependent" Header="Là người phụ thuộc" Width="110"
                                DataIndex="IsDependent" />
                            <ext:Column ColumnID="Note" Header="Quê quán, nghề nghiệp, chức danh, chức vụ, học tập, nơi ở(Trong ngoài nước);Thành viên tổ chức chính trị- xã hội"
                                DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelQHGD" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelQuaTrinhDieuChuyen" Title="Quá trình công tác tại đơn vị" runat="server"
            Hidden="true" Closable="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuQuaTrinhDieuChuyen}.setDisabled(false);" />
                <Activate Handler="#{btnAddRecordInDetailTable}.enable();
                                    if(#{hdfQTDCRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                    #{StoreQuaTrinhDieuChuyen}.reload();
                                    #{hdfQTDCRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }  
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfQTDCRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelQuaTrinhDieuChuyen" runat="server" StripeRows="true"
                    Border="false" TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%"
                    Region="Center" AutoExpandColumn="Note" AutoExpandMin="100">
                    <Store>
                        <ext:Store ID="StoreQuaTrinhDieuChuyen" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            AutoLoad="false" OnRefreshData="StoreQuaTrinhDieuChuyen_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="DecisionNumber" />
                                        <ext:RecordField Name="DecisionMaker" />
                                        <ext:RecordField Name="DecisionDate" />
                                        <ext:RecordField Name="EffectiveDate" />
                                        <ext:RecordField Name="NewDepartmentName" />
                                        <ext:RecordField Name="NewPositionName" />
                                        <ext:RecordField Name="NewJobName" />
                                        <ext:RecordField Name="AttachFileName" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel13" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" />
                            <ext:Column ColumnID="AttachFileName" Width="25" DataIndex="" Align="Center" Locked="true">
                                <Commands>
                                    <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                        <ToolTip Text="Tải tệp tin đính kèm" />
                                    </ext:ImageCommand>
                                </Commands>
                                <PrepareCommand Fn="prepare" />
                            </ext:Column>
                            <ext:Column ColumnID="DecisionNumber" Width="110" Header="Số quyết định" DataIndex="DecisionNumber" />
                            <ext:Column ColumnID="DecisionMaker" Width="150" Header="Người quyết định" DataIndex="DecisionMaker" />
                            <ext:DateColumn ColumnID="DecisionDate" Width="100" Header="Từ ngày" DataIndex="DecisionDate" Format="dd/MM/yyyy"/>
                            <ext:DateColumn ColumnID="EffectiveDate" Width="100" Header="Đến ngày" DataIndex="EffectiveDate" Format="dd/MM/yyyy"/>
                            <ext:Column ColumnID="NewDepartmentName" Width="150" Header="Phòng ban" DataIndex="NewDepartmentName" />
                            <ext:Column ColumnID="NewPositionName" Header="Chức vụ" Width="100" DataIndex="NewPositionName" />
                            <ext:Column ColumnID="NewJobName" Header="Chức danh" Width="120" DataIndex="NewJobName" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" Width="100" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelQuaTrinhDieuChuyen" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                        <Command Handler="#{DirectMethods}.DownloadAttach(record.data.AttachFileName);" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelTaiSan" Title="Tài sản cấp phát" runat="server" Closable="true"
            Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuTaiSan}.setDisabled(false);" />
                <Activate Handler="if(#{hdfTaiSanRecordID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                    #{StoreTaiSan}.reload();
                                    #{hdfTaiSanRecordID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfTaiSanRecordID" runat="server" />
                <ext:GridPanel ID="GridPanelTaiSan" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center"
                    AutoExpandColumn="Note">
                    <Store>
                        <ext:Store ID="StoreTaiSan" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreTaiSan_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="AssetCode" />
                                        <ext:RecordField Name="AssetName" />
                                        <ext:RecordField Name="Quantity" />
                                        <ext:RecordField Name="UnitCode" />
                                        <ext:RecordField Name="ReceiveDate" />
                                        <ext:RecordField Name="Status" />
                                        <ext:RecordField Name="AttachFileName" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel12" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" />
                            <ext:Column ColumnID="AssetCode" Width="80" Header="Thẻ tài sản" DataIndex="AssetCode" />
                            <ext:Column ColumnID="AssetName" Width="200" Header="Tên tài sản" DataIndex="AssetName"/>
                            <ext:Column ColumnID="Quantity" Width="80" Header="Số lượng" DataIndex="Quantity" />
                            <ext:Column ColumnID="UnitCode" Width="100" Header="Đơn vị tính" DataIndex="UnitCode" />
                            <ext:DateColumn ColumnID="ReceiveDate" Header="Ngày nhận" DataIndex="ReceiveDate" Format="dd/MM/yyyy"/>
                            <ext:Column ColumnID="Status" Header="Tình trạng" DataIndex="Status">
                            </ext:Column>
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note">
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelTaiSan" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelTepDinhKem" Title="Tệp tin đính kèm" runat="server" Closable="true"
            Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{MenuItemAttachFile}.setDisabled(false);" />
                <Activate Handler="" />
            </Listeners>
            <Items>
                <ext:GridPanel ID="grpTepTinDinhKem" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoExpandColumn="AttachFileName" AutoScroll="true" AnchorHorizontal="100%"
                    Region="Center">
                    <Store>
                        <ext:Store ID="grpTepTinDinhKemStore" AutoLoad="false" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            OnRefreshData="grpTepTinDinhKemStore_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="AttachFileName" />
                                        <ext:RecordField Name="URL" />
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="SizeKB" />
                                        <ext:RecordField Name="CreatedDate" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel17" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" />
                            <ext:Column ColumnID="Link" Width="25" DataIndex="" Align="Center" Locked="true">
                                <Commands>
                                    <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                        <ToolTip Text="Tải tệp tin đính kèm" />
                                    </ext:ImageCommand>
                                </Commands>
                                <PrepareCommand Fn="prepare" />
                            </ext:Column>
                            <ext:Column ColumnID="AttachFileName" Width="200" Header="Tên tệp tin" DataIndex="AttachFileName" />
                            <ext:Column ColumnID="SizeKB" Width="100" Header="Dung lượng(KB)" DataIndex="SizeKB" />
                            <ext:Column ColumnID="Note" Width="300" Header="Ghi chú" DataIndex="Note" />
                            <ext:DateColumn ColumnID="CreatedDate" Width="100" Header="Ngày tạo" DataIndex="CreatedDate"  Format="dd/MM/yyyy"/>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelTepTinDinhKem" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                        <Command Handler="#{DirectMethods}.DownloadAttach(record.data.Link);" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        
        <ext:Panel ID="panelNgoaiNgu" Title="Ngoại ngữ" runat="server" Closable="true" Hidden="true"
            CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuNgoaiNgu}.setDisabled(false);" />
                <Activate Handler="if(#{hdfNgoaiNguID}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                    #{StoreNgoaiNgu}.reload();
                                    #{hdfNgoaiNguID}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdfNgoaiNguID" runat="server" />
                <ext:GridPanel ID="grpNgoaiNgu" runat="server" StripeRows="true" Border="false" TrackMouseOver="true"
                    AutoScroll="true" AnchorHorizontal="100%" Region="Center" AutoExpandColumn="Note">
                    <Store>
                        <ext:Store ID="StoreNgoaiNgu" AutoSave="true" ShowWarningOnFailure="false" OnBeforeStoreChanged="HandleChangesDelete"
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreNgoaiNgu_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="LanguageName" />
                                        <ext:RecordField Name="Rank" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel4" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" />
                            <ext:Column ColumnID="LanguageName" Width="200" Header="Tên ngoại ngữ" DataIndex="LanguageName" />
                            <ext:Column ColumnID="Rank" Width="80" Header="Xếp hạng" DataIndex="Rank" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel_NgoaiNgu" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                        <ViewReady Handler="if(#{cbxNgoaiNgu}.store.getCount()==0){#{cbxNgoaiNgu}.store.reload();}" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelKinhNghiemLamViec" Title="Công tác trước khi vào đơn vị" runat="server"
            Closable="true" Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{MenuItemKNLV}.setDisabled(false);" />
                <Activate Handler="if(#{hdfCheckKNLV}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                        #{StoreKinhNghiemLamViec}.reload();
                                        #{hdfCheckKNLV}.setValue(#{hdfRecordId}.getValue());
                                    }
                                    " />
            </Listeners>
            <Items>
                <ext:Hidden runat="server" ID="hdfCheckKNLV" />
                <ext:GridPanel ID="GridPanelKinhNghiemLamViec" runat="server" StripeRows="true" Border="false"
                    TrackMouseOver="true" AutoExpandColumn="Note" AutoScroll="true" AnchorHorizontal="100%"
                    Region="Center">
                    <Store>
                        <ext:Store ID="StoreKinhNghiemLamViec" AutoLoad="false" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            OnRefreshData="StoreKinhNghiemLamViec_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="RecordId" />
                                        <ext:RecordField Name="WorkPlace" />
                                        <ext:RecordField Name="WorkPosition" />
                                        <ext:RecordField Name="ExperienceWork" />
                                        <ext:RecordField Name="FromDate" />
                                        <ext:RecordField Name="ToDate" />
                                        <ext:RecordField Name="ReasonLeave" />
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="SalaryLevel" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel19" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="WorkPlace" Header="Nơi làm việc" DataIndex="WorkPlace" Width="200" />
                            <ext:Column ColumnID="WorkPosition" Width="150" Header="Vị trí công việc" DataIndex="WorkPosition" />
                            <ext:DateColumn ColumnID="FromDate" Width="90" Header="Từ ngày"
                                DataIndex="FromDate" Format="dd/MM/yyyy" />
                            <ext:DateColumn ColumnID="ToDate" Width="90" Header="Đến ngày"
                                DataIndex="ToDate" Format="dd/MM/yyyy" />
                            <ext:Column ColumnID="ExperienceWork" Width="150" Header="Thành tích đạt được"
                                DataIndex="ExperienceWork" />
                            <ext:Column ColumnID="SalaryLevel" Width="90" Header="Mức lương" DataIndex="SalaryLevel">
                                <Renderer Fn="RenderVND" />
                            </ext:Column>
                            <ext:Column ColumnID="ReasonLeave" Header="Lý do chuyển công tác" DataIndex="ReasonLeave" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelKinhNghiemLamViec" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        
        <ext:Panel ID="panelTheNganHang" Title="Thẻ ngân hàng" runat="server" Closable="true"
            Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuATM}.setDisabled(false);" />
                <Activate Handler="if(#{hdf_PrKeyHoSo}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                    #{StoregrpATM}.reload();
                                    #{hdf_PrKeyHoSo}.setValue(#{hdfRecordId}.getValue());
                                    }" />
            </Listeners>
            <Items>
                <ext:Hidden ID="hdf_PrKeyHoSo" runat="server" />
                <ext:GridPanel ID="grpATM" runat="server" StripeRows="true" Border="false" AutoExpandColumn="Note"
                    TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%" Region="Center">
                    <Store>
                        <ext:Store ID="StoregrpATM" AutoSave="true" ShowWarningOnFailure="false" 
                            SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoregrpATM_OnRefreshData"
                            runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="RecordId" />
                                        <ext:RecordField Name="AccountName" />
                                        <ext:RecordField Name="AccountNumber" />
                                        <ext:RecordField Name="Note" />
                                        <ext:RecordField Name="IsInUsed" />
                                        <ext:RecordField Name="BankName" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel23" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" />
                            <ext:Column ColumnID="AccountNumber" Header="Số tài khoản" Width="150" DataIndex="AccountNumber">
                            </ext:Column>
                            <ext:Column ColumnID="AccountName" Header="Tên tài khoản" Width="170" DataIndex="AccountName">
                            </ext:Column>
                            <ext:Column ColumnID="IsInUsed" Width="100" Header="Đang sử dụng" Align="Center"
                                DataIndex="IsInUsed">
                                <Renderer Fn="GetBooleanIcon" />
                            </ext:Column>    
                            <ext:Column ColumnID="BankName" Header="Tên ngân hàng" Width="150" DataIndex="BankName" />
                            <ext:Column ColumnID="Note" Header="Ghi chú" Width="150" DataIndex="Note">
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelATM" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Panel ID="panelDiNuocNgoai" Title="Quản lý đi nước ngoài" runat="server" Closable="true"
            Hidden="true" CloseAction="Hide" Layout="BorderLayout">
            <Listeners>
                <Close Handler="#{mnuDiNuocNgoai}.setDisabled(false);" />
                <Activate Handler="#{btnAddRecordInDetailTable}.enable();
                                   if(#{hdf_PrKeyHoSo}.getValue()!=#{hdfRecordId}.getValue())
                                    {
                                    #{StoregrpDiNuocNgoai}.reload();
                                    }
                                   " />
            </Listeners>
            <Items>
                <ext:GridPanel ID="grpDiNuocNgoai" runat="server" StripeRows="true" Border="false"
                    AutoExpandColumn="Note" TrackMouseOver="true" AutoScroll="true" AnchorHorizontal="100%"
                    Region="Center">
                    <Store>
                        <ext:Store ID="StoregrpDiNuocNgoai" AutoSave="true" ShowWarningOnFailure="false"
                            OnBeforeStoreChanged="HandleChangesDelete" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                            AutoLoad="false" OnRefreshData="StoregrpDiNuocNgoai_OnRefreshData" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="NationId" />
                                        <ext:RecordField Name="NationName" />
                                        <ext:RecordField Name="StartDate" />
                                        <ext:RecordField Name="EndDate" />
                                        <ext:RecordField Name="Reason" />
                                        <ext:RecordField Name="Note" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel24" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Header="STT" Width="35" />
                            <ext:Column ColumnID="NationName" Header="Quốc gia" Width="200" DataIndex="NationName">
                            </ext:Column>
                            <ext:DateColumn ColumnID="StartDate" Header="Ngày bắt đầu" Align="Center" Format="dd/MM/yyyy"
                                Width="100" DataIndex="StartDate">
                            </ext:DateColumn>
                            <ext:DateColumn ColumnID="EndDate" Header="Ngày kết thúc" Align="Center" Format="dd/MM/yyyy"
                                Width="100" DataIndex="EndDate">
                            </ext:DateColumn>
                            <ext:Column ColumnID="Reason" Header="Lý do" Width="500" DataIndex="Reason">
                            </ext:Column>
                            <ext:Column ColumnID="Note" Header="Ghi chú" Width="300" DataIndex="Note">
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelDiNuocNgoai" runat="server" SingleSelect="true">
                            <Listeners>
                                <RowSelect Handler="#{btnDeleteRecord}.enable();#{btnEditRecord}.enable();#{hdfTenQuocGia}.setValue(#{RowSelectionModelDiNuocNgoai}.getSelected().data.TEN_NUOC);" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <RowDblClick Handler="" />
                    </Listeners>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
    </Items>
    <BottomBar>
        <ext:Toolbar ID="Toolbar1sdsds" Hidden="false" runat="server">
            <Items>
                <ext:Button ID="CloseTab1" runat="server" Text="Thông tin quản lý" Icon="Information">
                    <Menu>
                        <ext:Menu runat="server" ID="menuAddMoreTab">
                            <Items>
                                 <ext:MenuItem ID="mnuHopDong" runat="server" Text="Hợp đồng">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelHopDong});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{StoreHopDong}.reload();}
                                                        #{hdfHDRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="menuMoveBusiness" runat="server" Text="Điều động">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelMoveBusiness});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('DieuDongDi');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfMoveRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                 <ext:MenuItem ID="menuAppointment" runat="server" Text="Bổ nhiệm chức vụ">
                                    <Listeners>
                                        <Click Handler=" " />
                                    </Listeners>
                                </ext:MenuItem>
                                 <ext:MenuItem ID="menuTurnover" runat="server" Text="Luân chuyển">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelTurnover});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('LuanChuyenDi');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfTurnoverRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="menuSecondment" runat="server" Text="Biệt phái">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelSecondment});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('BietPhaiDi');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfSecondmentRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                 <ext:MenuItem ID="menuPlurality" runat="server" Text="Kiêm nhiệm">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelPlurality});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('KiemNhiem');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfPluralityRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                 <ext:MenuItem ID="menuDismissal" runat="server" Text="Miễn nhiệm, bãi nhiệm">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelDismissal});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('MienNhiemBaiNhiem');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfDismissalRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                  <ext:MenuItem ID="menuTransfer" runat="server" Text="Thuyên chuyển, điều chuyển">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelTransfer});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('ThuyenChuyenDieuChuyen');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfTransferRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                 <ext:MenuItem ID="menuRetirement" runat="server" Text="Nghỉ hưu">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelRetirement});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('NghiHuu');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfRetirementRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItemHocTap" runat="server" Text="Quá trình đào tạo, bồi dưỡng chuyên môn nghiệp vụ" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelQuaTrinhHocTap});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{Store_BangCap}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItemBangCapChungChi" runat="server" Text="Chứng chỉ" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelBangCapChungChi});this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{Store_BangCapChungChi}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuBaoHiem" runat="server" Text="Bảo hiểm" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelBaoHiem});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{StoreBaoHiem}.reload();}
                                                        #{hdfBHRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuQuanHeGiaDinh" runat="server" Text="Quan hệ gia đình" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelQuanHeGiaDinh});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {#{StoreQHGD}.reload();}
                                                        #{hdfQHGDRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuDienBienLuong" runat="server" Text="Diễn biến quá trình lương">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelDienBienLuong});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{StoreDienBienLuong}.reload();}
                                                        #{hdfDBLRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                 <ext:MenuItem ID="menuEmulationTitle" runat="server" Text="Danh hiệu thi đua">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelEmulationTitle});
                                                        this.setDisabled(true);
                                                        #{hdfBusinessTimeSheetHandlerType}.setValue('DanhHieuThiDua');
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{storeBusiness}.reload();}
                                                        #{hdfEmulationTitleRecordID}.setValue(#{hdfRecordId}.getValue());
                                                        " />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuKhenThuong" runat="server" Text="Hình thức khen thưởng">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelKhenThuong});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {#{StoreKhenThuong}.reload();}
                                                        #{hdfKhenThuongRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuKyLuat" runat="server" Text="Hình thức kỷ luật">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelKiLuat});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {#{StoreKyLuat}.reload();}
                                                        #{hdfKyLuatRecordID}.setValue(#{hdfRecordId}.getValue())" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuQuaTrinhDaoTao" runat="server" Text="Quá trình đào tạo tại đơn vị">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelQuaTrinhDaoTao});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{StoreQuaTrinhDaoTao}.reload();
                                                        }
                                                        #{hdfQTDTRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItemKNLV" runat="server" Text="Quá trình công tác trước khi vào đơn vị" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelKinhNghiemLamViec});this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{StoreKinhNghiemLamViec}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuQuaTrinhDieuChuyen" runat="server" Text="Quá trình công tác tại đơn vị" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelQuaTrinhDieuChuyen});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {#{StoreQuaTrinhDieuChuyen}.reload();}
                                                        #{hdfQTDCRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuDiNuocNgoai" runat="server" Text="Quá trình công tác nước ngoài">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelDiNuocNgoai});this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{StoregrpDiNuocNgoai}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuKhaNang" runat="server" Text="Năng lực, sở trường">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelKhaNang});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{StoreKhaNang}.reload();}
                                                        #{hdfKhaNangRecordID}.setValue(#{hdfRecordId}.getValue())" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItemNgoaiNgu" runat="server" Text="Ngoại ngữ" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelNgoaiNgu});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{StoreNgoaiNgu}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuDaiBieu" runat="server" Text="Đại biểu" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelDaiBieu});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                            {#{StoreDaiBieu}.reload();}
                                                        #{hdfDBRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuTaiSan" runat="server" Text="Công cụ được cấp phát">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelTaiSan});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{StoreTaiSan}.reload();
                                                        }
                                                        #{hdfTaiSanRecordID}.setValue(#{hdfRecordId}.getValue());" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItemAttachFile" runat="server" Text="Tệp tin đính kèm" Hidden="true">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelTepDinhKem});
                                                        this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{grpTepTinDinhKemStore}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuATM" runat="server" Text="Thẻ ngân hàng">
                                    <Listeners>
                                        <Click Handler="#{TabPanelBottom}.addTab(#{panelTheNganHang});this.setDisabled(true);
                                                        if(#{hdfRecordId}.getValue()!='')
                                                        {
                                                            #{StoregrpATM}.reload();
                                                        }" />
                                    </Listeners>
                                </ext:MenuItem>
                            </Items>
                        </ext:Menu>
                    </Menu>
                </ext:Button>
                <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" />
                <ext:Hidden runat="server" ID="hdfButtonClick" />
                <ext:Button Text="Thêm mới" Disabled="true" runat="server" ID="btnAddRecordInDetailTable" Hidden="true"
                    Icon="Add">
                    <Listeners>
                        <Click Handler="if(#{hdfRecordId}.getValue()!='')
                                        {
                                            #{hdfButtonClick}.setValue('Insert');
                                            AddRecordClick(#{TabPanelBottom});
                                        }else
                                        {
                                            Ext.Msg.alert('Thông báo','Bạn chưa chọn cán bộ công chức nào !');
                                        }
                                        " />
                    </Listeners>
                </ext:Button>
                <ext:Button Text="Xóa" Disabled="true" runat="server" ID="btnDeleteRecord" Icon="Delete" Hidden="true">
                    <Listeners>
                        <Click Handler="DeleteRecordOnGrid();" />
                    </Listeners>
                </ext:Button>
                <ext:Button Text="Sửa" Disabled="true" runat="server" ID="btnEditRecord" Icon="Pencil" Hidden="true" >
                    <Listeners>
                        <Click Handler="EditClick(#{DirectMethods}); #{hdfButtonClick}.setValue('Edit');" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:Toolbar>
    </BottomBar>
</ext:TabPanel>
