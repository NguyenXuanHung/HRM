<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.Contract" Codebehind="Contract.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1"
    TagName="ucChooseEmployee" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS.css" rel="stylesheet" type="text/css" />
    <script src="../../Home/js/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        div#grpContract .x-grid3-cell-inner, .x-grid3-hd-inner
        {
            white-space: nowrap !important;
        }
        
        #pnReportPanel .x-tab-panel-header
        {
            display: none !important;
        }
        .Download
        {
            background-image: url(../../../Resource/images/download.png) !important;
        }
    </style>
    <script src="../../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
    <script type="text/javascript">
        var prepare = function (grid, command, record, row, col, value) {
            if (record.data.TepTinDinhKem == '' && command.command == "Download") {
                command.hidden = true;
                command.hideMode = "visibility";
            }
        }
        var keyPresstxtSearch = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                PagingToolbar1.doLoad();
                RowSelectionModel1.clearSelections();
                if (this.getValue() == '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() != '') {
                this.triggers[0].show();
            }
        }
        var ResetWdHopDongHangLoat = function () {
            txtHopDongSoHopDongHL.reset(); cbHopDongLoaiHopDongHL.reset(); cbHopDongTinhTrangHopDongHL.reset();
            cbHopDongCongViecHL.reset(); dfHopDongNgayHopDongHL.reset(); dfHopDongNgayKiKetHL.reset();
            dfNgayCoHieuLucHL.reset(); cbx_HopDongChucVuHL.reset();
            fufHopDongTepTinHL.reset(); txtHopDongGhiChuHL.reset();
            txt_NguoiKyHDHL.reset(); hdfHopDongTepTinDKHL.reset();
            grp_DanhSachCanBoStore.removeAll();
        }
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
        var triggershowChooseEmplyee = function (f, e) {
            if (e.getKey() == e.ENTER) {
                ucChooseEmployee1_wdChooseUser.show();
            }
        }
        var CheckInputKTKLHangLoat = function (el) {
            if (txtHopDongSoHopDongHL.getValue().trim() == '') {
                alert('Bạn chưa nhập số hợp đồng');
                txtHopDongSoHopDongHL.focus();
                return false;
            }
            if (cbHopDongLoaiHopDongHL.getValue() == null) {
                alert('Bạn chưa chọn loại hợp đồng');
                cbHopDongLoaiHopDongHL.focus();
                return false;
            }
            if (dfHopDongNgayHopDongHL.getValue() == '') {
                alert('Bạn chưa chọn ngày ký hợp đồng');
                dfHopDongNgayHopDongHL.focus();
                return false;
            }
            if (dfNgayCoHieuLucHL.getValue() == '') {
                alert('Bạn chưa nhập ngày hợp đồng có hiệu lực');
                dfNgayCoHieuLucHL.focus();
                return false;
            }
            if (!txt_NguoiKyHDHL.getValue()) {
                alert('Bạn chưa nhập người ký hợp đồng');
                txt_NguoiKyHDHL.focus();
                return false;
            }
            var size = 0;
            for (var num1 = 0; num1 < el.files.length; num1++) {
                var file = el.files[num1];
                size += file.size;
            }
            if (size > 10485760) {
                alert('Phần mềm chỉ hỗ trợ các tệp tin đính kèm nhỏ hơn 10MB');
                return false;
            }
            if (grp_DanhSachCanBo.store.getCount() == 0) {
                alert('Bạn chưa chọn cán bộ nào!');
                return false;
            }
            return true;
        }

        var addRecord = function (RecordId, EmployeeCode, FullName, DepartmentName) {
            var rowindex = getSelectedIndexRow();
            grp_DanhSachCanBo.insertRecord(rowindex, {
                RecordId: RecordId,
                EmployeeCode: EmployeeCode,
                FullName: FullName,
                DepartmentName: DepartmentName
            });
            grp_DanhSachCanBo.getView().refresh();
            grp_DanhSachCanBo.getSelectionModel().selectRow(rowindex);
        }
        var getSelectedIndexRow = function () {
            var record = grp_DanhSachCanBo.getSelectionModel().getSelected();
            var index = grp_DanhSachCanBo.store.indexOf(record);
            if (index == -1)
                return 0;
            return index;
        }
        var ResetWdHopDong = function () {
            txtHopDongSoHopDong.reset();
            cbxContractType.reset();
            cbxJob.reset();
            dfHopDongNgayHopDong.reset();
            dfHopDongNgayKiKet.reset();
            dfNgayCoHieuLuc.reset();
            cbxPosition.reset();
            fufHopDongTepTin.reset();
            txtHopDongGhiChu.reset();
            txt_NguoiKyHD.reset();
            hdfHopDongTepTinDK.reset();
        }
        var onKeyUp = function (field) {
            var v = this.processValue(this.getRawValue()),
                field;

            if (this.startDateField) {
                field = Ext.getCmp(this.startDateField);
                field.setMaxValue();
                this.dateRangeMax = null;
            } else if (this.endDateField) {
                field = Ext.getCmp(this.endDateField);
                field.setMinValue();
                this.dateRangeMin = null;
            }
            field.validate();
        }
        var CheckInputHopDong = function (el) {
            if (txtHopDongSoHopDong.getValue().trim() == '') {
                alert('Bạn chưa nhập số hợp đồng');
                txtHopDongSoHopDong.focus();
                return false;
            }
            if (cbxContractType.getValue() == null) {
                alert('Bạn chưa chọn loại hợp đồng');
                cbxContractType.focus();
                return false;
            }
            if (dfHopDongNgayHopDong.getValue() == '') {
                alert('Bạn chưa chọn ngày ký hợp đồng');
                dfHopDongNgayHopDong.focus();
                return false;
            }
            if (dfNgayCoHieuLuc.getValue() == '') {
                alert('Bạn chưa nhập ngày hợp đồng có hiệu lực');
                dfNgayCoHieuLuc.focus();
                return false;
            }
            if (!txt_NguoiKyHD.getValue()) {
                alert('Bạn chưa nhập người ký hợp đồng');
                txt_NguoiKyHD.focus();
                return false;
            }
            var size = 0;
            for (var num1 = 0; num1 < el.files.length; num1++) {
                var file = el.files[num1];
                size += file.size;
            }
            if (size > 10485760) {
                alert('Phần mềm chỉ hỗ trợ các tệp tin đính kèm nhỏ hơn 10MB');
                return false;
            }
            return true;
        }

        var getPrKeyRecordList = function () {
            var jsonDataEncode = "";
            var records = window.grp_DanhSachCanBoStore.getRange();
            for (var i = 0; i < records.length; i++) {
                jsonDataEncode += records[i].data.RecordId + ",";
            }
            return jsonDataEncode;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="RM" runat="server">
        </ext:ResourceManager>
        <ext:Hidden runat="server" ID="hdfRecordId" />
        <ext:Hidden runat="server" ID="hdfPrKey" />
        <ext:Hidden runat="server" ID="hdfDepartmentSelected" />
        <ext:Hidden runat="server" ID="hdfMenuID" />
        <ext:Hidden runat="server" ID="hdfTotalRecord" />
        <ext:Hidden runat="server" ID="hdfUserID" />
        <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfQuery" />
         
        <ext:Store runat="server" AutoLoad="false" ID="storeJobTitle">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
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
         <ext:Store runat="server" AutoLoad="false" ID="storePosition">
            <Proxy>
                <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
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
         <ext:Store runat="server" AutoLoad="false" ID="storeContractType">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
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
        <ext:Store runat="server" AutoLoad="false" ID="storeContractStatus">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
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
        <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false"
            DisplayWorkingStatus="TatCa" />
        <ext:Window ID="wdHopDong" AutoHeight="true" Width="550" runat="server" Padding="6"
            EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
            Icon="Add" Title="Hợp đồng" Resizable="false">
            <Items>
                <ext:Container ID="Container4" runat="server" Layout="Column" Height="27">
                    <Items>
                        <ext:Container ID="Container5" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:CompositeField ID="CompositeField11" runat="server" AnchorHorizontal="99%">
                                    <Items>
                                        <ext:TextField runat="server" FieldLabel="Số hợp đồng<span style='color:red;'>*</span>"
                                            Width="120" ID="txtHopDongSoHopDong" MaxLength="30" CtCls="requiredData" TabIndex="1" />
                                        <ext:Button runat="server" ID="btnSinhSoHopDong" Icon="Reload">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Sinh số hợp đồng mới (Chỉ áp dụng cho trường hợp chưa có số hợp đồng)" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Handler="if (#{txtHopDongSoHopDong}.getValue().trim() != '' && #{txtHopDongSoHopDong}.getValue() != null) { this.blur(); alert('Số hợp đồng đã được sinh');} else {#{DirectMethods}.GenerateSoQD();}" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:CompositeField>
                            </Items>
                        </ext:Container>
                        <ext:Hidden runat="server" ID="hdfRecruitmentTypeId" />
                        <ext:Container ID="Container6" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbxRecruitmentType" DisplayField="Name" MinChars="1" ItemSelector="div.list-item" FieldLabel="Hình thức TD" Editable="true" ValueField="Id" AnchorHorizontal="98%" LoadingText="Đang tải dữ liệu..." EmptyText="Gõ để tìm kiếm" TabIndex="2">
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
                                    <Store>
                                         <ext:Store runat="server" AutoLoad="false" ID="storeRecruitmentType">
                                                <Proxy>
                                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                                                </Proxy>
                                                <BaseParams>
                                                    <ext:Parameter Name="objname" Value="cat_RecruitmentTimeSheetHandlerType" Mode="Value" />
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
                                        <Select Handler="this.triggers[0].show(); hdfRecruitmentTimeSheetHandlerTypeId.setValue(cbxRecruitmentTimeSheetHandlerType.getValue());" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Hidden runat="server" ID="hdfContractStatusId" />
                <ext:ComboBox runat="server" ID="cbxContractStatus" DisplayField="Name"
                              MinChars="1" ItemSelector="div.list-item" FieldLabel="Tình trạng HĐ" Editable="true"
                              ValueField="Id" AnchorHorizontal="99%" StoreID="storeContractStatus"
                              LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
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
                        <Select Handler="this.triggers[0].show(); hdfContractStatusId.setValue(cbxContractStatus.getValue());" />
                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfContractStatusId.reset(); }" />
                    </Listeners>
                </ext:ComboBox>
                <ext:Hidden runat="server" ID="hdfContractTypeId" />
                <ext:ComboBox runat="server" ID="cbxContractType" DisplayField="Name" MinChars="1"
                    ItemSelector="div.list-item" FieldLabel="Loại hợp đồng<span style='color:red;'>*</span>"
                    Editable="true" ValueField="Id" AnchorHorizontal="99%" CtCls="requiredData" StoreID="storeContractType"
                    LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm" TabIndex="3">
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
                        <Select Handler="this.triggers[0].show();hdfContractTimeSheetHandlerTypeId.setValue(cbxContractTimeSheetHandlerType.getValue()) ;#{DirectMethods}.SetNgayHetHD();" />
                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                    </Listeners>
                </ext:ComboBox>
                <ext:Hidden runat="server" ID="hdfCbxJobId" />
                <ext:ComboBox runat="server" ID="cbxJob" DisplayField="Name" FieldLabel="Chức danh"
                    StoreID="storeJobTitle" Editable="true" ValueField="Id" AnchorHorizontal="99%"
                    TabIndex="4" MinChars="1" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu..."
                    EmptyText="gõ để tìm kiếm">
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
                        <Select Handler="this.triggers[0].show(); hdfCbxJobId.setValue(cbxJob.getValue());" />
                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                    </Listeners>
                </ext:ComboBox>
                <ext:Hidden runat="server" ID="hdfCbxPositionId" />
                <ext:ComboBox runat="server" ID="cbxPosition" FieldLabel="Chức vụ" DisplayField="Name"
                    MinChars="1" StoreID="storePosition" ValueField="Id" AnchorHorizontal="99%"
                    Editable="true" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu..."
                    EmptyText="gõ để tìm kiếm" TabIndex="5">
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
                    <Listeners>
                        <Select Handler="this.triggers[0].show();hdfCbxPositionId.setValue(cbxPosition.getValue());" />
                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                    </Listeners>
                </ext:ComboBox>
                <ext:Container ID="Container43" runat="server" Layout="Column" Height="52">
                    <Items>
                        <ext:Container ID="Container44" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:DateField runat="server" Vtype="daterange" FieldLabel="Ngày ký kết<span style='color:red;'>*</span>"
                                    ID="dfHopDongNgayHopDong" AnchorHorizontal="99%" Editable="true" MaskRe="/[0-9\/]/"
                                    Format="d/M/yyyy" CtCls="requiredData" TabIndex="6">
                                    <CustomConfig>
                                        <ext:ConfigItem Name="endDateField" Value="#{dfHopDongNgayKiKet}" Mode="Value" />
                                    </CustomConfig>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();}" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:TextField ID="txt_NguoiKyHD" runat="server" FieldLabel="Người ký HĐ<span style='color:red;'>*</span>"
                                    AllowBlank="false" AnchorHorizontal="98%" MaxLength="50" LabelWidth="165" Width="300"
                                    TabIndex="7" CtCls="requiredData">
                                </ext:TextField>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container45" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:DateField runat="server" FieldLabel="Ngày hiệu lực<span style='color:red;'>*</span>"
                                    ID="dfNgayCoHieuLuc" AnchorHorizontal="98%" Editable="true" MaskRe="/[0-9\/]/"
                                    Format="d/M/yyyy" CtCls="requiredData" TabIndex="8">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();#{DirectMethods}.SetNgayHetHD();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();}" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:DateField runat="server" Vtype="daterange" FieldLabel="Ngày hết hạn" ID="dfHopDongNgayKiKet"
                                    AnchorHorizontal="98%" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" TabIndex="9">
                                    <CustomConfig>
                                        <ext:ConfigItem Name="startDateField" Value="#{dfHopDongNgayHopDong}" Mode="Value" />
                                    </CustomConfig>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();}" />
                                    </Listeners>
                                </ext:DateField>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Hidden runat="server" ID="hdfHopDongTepTinDK" />
                <ext:CompositeField ID="CompositeField2" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
                    <Items>
                        <ext:FileUploadField ID="fufHopDongTepTin" runat="server" EmptyText="Chọn tệp tin"
                            ButtonText="" Icon="Attach" Width="358" TabIndex="11">
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
                <ext:TextArea runat="server" ID="txtHopDongGhiChu" FieldLabel="Ghi chú" AnchorHorizontal="99%"
                    TabIndex="12" />
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
                <Hide Handler="#{btnUpdateHopDong}.show();#{btnEditHopDong}.hide();#{Button20}.show();ResetWdHopDong();" />
            </Listeners>
        </ext:Window>
        <ext:Viewport ID="vp" runat="server" HideBorders="true">
            <Items>
                <ext:BorderLayout runat="server" ID="brlayout">
                    <Center>
                        <ext:GridPanel ID="grpContract" TrackMouseOver="true" Header="false" runat="server"
                            StripeRows="true" Border="false" AnchorHorizontal="100%">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="toolbarFn">
                                    <Items>
                                        <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                            <Listeners>
                                                <Click Handler="btnUpdateHopDong.show();btnEditHopDong.hide();Button20.show();wdHopDongHangLoat.show();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                            <Listeners>
                                                <Click Handler="if (CheckSelectedRows(grpContract) == false) {return false;} btnUpdateHopDong.hide();btnEditHopDong.show();Button20.hide();" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="btnEdit_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                            <Listeners>
                                                <Click Handler="return CheckSelectedRow(grpContract);" />
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
                                        <ext:Button runat="server" Text="Báo cáo" ID="btnPrint" Icon="Printer" Hidden="true">
                                        </ext:Button>
                                        <ext:ToolbarSeparator />
                                    
                                        <ext:DisplayField ID="DisplayField1" runat="server" Text="Từ ngày: " Hidden="true" />
                                        <ext:ToolbarSpacer Width="5" />
                                        <ext:DateField ID="dfNgayBatDau" runat="server" MaskRe="/[0-9\/]/" Format="d/M/yyyy"
                                            Vtype="daterange" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                            RegexText="Định dạng ngày hạn nộp hồ sơ không đúng" Width="120" Hidden="true">
                                            <CustomConfig>
                                                <ext:ConfigItem Name="endDateField" Value="#{dfNgayKetThuc}" Mode="Value">
                                                </ext:ConfigItem>
                                            </CustomConfig>
                                            <Listeners>
                                                <KeyUp Fn="onKeyUp" />
                                                <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                <TriggerClick Handler="if (index == 0) { this.reset();dfNgayKetThuc.setMinValue(); this.triggers[0].hide(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad(); }" />
                                            </Listeners>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                        </ext:DateField>
                                        <ext:ToolbarSpacer Width="10" />
                                        <ext:DisplayField ID="DisplayField2" runat="server" Text="đến ngày: " Hidden="true" />
                                        <ext:ToolbarSpacer Width="5" />
                                        <ext:DateField ID="dfNgayKetThuc" runat="server" AnchorHorizontal="100%" MaskRe="/[0-9\/]/"
                                            Hidden="true" Format="d/M/yyyy" Vtype="daterange" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                            RegexText="Định dạng ngày hạn nộp hồ sơ không đúng" Width="120">
                                            <CustomConfig>
                                                <ext:ConfigItem Name="startDateField" Value="#{dfNgayBatDau}" Mode="Value">
                                                </ext:ConfigItem>
                                            </CustomConfig>
                                            <Listeners>
                                                <KeyUp Fn="onKeyUp" />
                                                <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                <TriggerClick Handler="if (index == 0) { this.reset();dfNgayBatDau.setMaxValue(); this.triggers[0].hide(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad(); }" />
                                            </Listeners>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                        </ext:DateField>
                                        <ext:ToolbarFill />
                                        <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220"
                                            EmptyText="Nhập mã, họ tên CCVC">
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
                                <ext:Store runat="server" ID="StoreHopDong">
                                    <Proxy>
                                        <ext:HttpProxy Method="POST" Url="~/Services/HandlerContract.ashx" />
                                    </Proxy>
                                    <AutoLoadParams>
                                        <ext:Parameter Name="start" Value="={0}" />
                                        <ext:Parameter Name="limit" Value="={25}" />
                                    </AutoLoadParams>
                                    <BaseParams>
                                        <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelected.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="Query" Value="hdfQuery.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                    </BaseParams>
                                    <Reader>
                                        <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                            <Fields>
                                                <ext:RecordField Name="Id" />
                                                <ext:RecordField Name="RecordId" />
                                                <ext:RecordField Name="EmployeeCode" />
                                                <ext:RecordField Name="FullName" />
                                                <ext:RecordField Name="ContractNumber" />
                                                <ext:RecordField Name="ContractTimeSheetHandlerTypeName" />
                                                <ext:RecordField Name="JobTitleName" />
                                                <ext:RecordField Name="PositionName" />
                                                <ext:RecordField Name="ContractStatusName" />
                                                <ext:RecordField Name="ContractDate" />
                                                <ext:RecordField Name="EffectiveDate" />
                                                <ext:RecordField Name="ContractEndDate" />
                                                <ext:RecordField Name="PersonRepresent" />
                                                <ext:RecordField Name="Note" />
                                                <ext:RecordField Name="DepartmentName" />
                                                <ext:RecordField Name="DepartmentManagementName" />
                                                <ext:RecordField Name="AttachFileName" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <Listeners>
                                        <Load Handler="#{RowSelectionModel1}.clearSelections();" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:RowNumbererColumn Header="STT" Width="35" Locked="true" />
                                    <ext:Column ColumnID="TepTinDinhKem" Width="25" DataIndex="" Align="Center" Locked="true">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                                <ToolTip Text="Tải tệp tin đính kèm" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <PrepareCommand Fn="prepare" />
                                    </ext:Column>
                                    <ext:Column Header="Mã CCVC" Width="85" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                    <ext:Column Header="Họ tên" Width="150" Align="Left" Locked="true" DataIndex="FullName" />
                                    <ext:Column Header="Phòng ban" Width="250" Align="Left" DataIndex="DepartmentName" />
                                    <ext:Column Header="Đơn vị quản lý" Width="250" Align="Left" DataIndex="DepartmentManagementName" />
                                    <ext:Column Header="Số hợp đồng" Width="120" Align="Left" DataIndex="ContractNumber" />
                                    <ext:Column Header="Loại hợp đồng" Width="200" Align="Left" DataIndex="ContractTimeSheetHandlerTypeName" />
                                    <ext:Column Header="Chức danh" Width="150" Align="Left" DataIndex="JobTitleName" />
                                    <ext:Column Header="Chức vụ" Width="150" Align="Left" DataIndex="PositionName" />
                                    <ext:Column Header="Tình trạng hợp đồng" Align="Left" DataIndex="ContractStatusName" />
                                    <ext:DateColumn Header="Ngày ký kết" Width="110" Align="Center" DataIndex="ContractDate"
                                        Format="dd/MM/yyyy" />
                                    <ext:DateColumn Header="Ngày hiệu lực" Width="110" Align="Center" DataIndex="EffectiveDate"
                                        Format="dd/MM/yyyy" />
                                    <ext:DateColumn Header="Ngày hết hạn" Width="120" Align="Center" DataIndex="ContractEndDate"
                                        Format="dd/MM/yyyy" />
                                    <ext:Column Header="Người ký HĐ" Width="150" Align="Left" DataIndex="PersonRepresent" />
                                    <ext:Column Header="Ghi chú" Width="300" Align="Left" DataIndex="Note" />
                                    <ext:Column ColumnID="Hidden" Width="0" DataIndex="" Align="Center" Locked="false">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Hidden">
                                            </ext:ImageCommand>
                                        </Commands>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:LockingGridView runat="server" ID="lkv">
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterCbxContractType" DisplayField="Name" MinChars="1"
                                                            ItemSelector="div.list-item" Editable="true" ValueField="Id" Width="200" EmptyText="gõ để tìm kiếm" StoreID="storeContractType">
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
                                                                <Select Handler="this.triggers[0].show();#{DirectMethods}.SetValueQuery(); 
                                                                                if (filterCbxContractTimeSheetHandlerType.getValue() == '-1') {$('#filterCbxContractTimeSheetHandlerType').removeClass('combo-selected');}
                                                                                else {$('#filterCbxContractTimeSheetHandlerType').addClass('combo-selected');}" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();
                                                                                        #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();$('#filterCbxContractTimeSheetHandlerType').removeClass('combo-selected'); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterJob" DisplayField="Name" ValueField="Id"
                                                            Width="150" MinChars="1" PageSize="20" Editable="true" ListWidth="200" ItemSelector="div.list-item"
                                                            LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm" StoreID="storeJobTitle">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template18" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();#{DirectMethods}.SetValueQuery();
                                                                 if (filterJob.getValue() == '-1') {$('#filterJob').removeClass('combo-selected');}
                                                                                else {$('#filterJob').addClass('combo-selected');}" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();
                                                                $('#filterJob').removeClass('combo-selected');} " />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterPosition" DisplayField="Name" ValueField="Id"
                                                            Width="150" MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" StoreID="storePosition"
                                                            LoadingText="Đang tải dữ liệu...">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template39" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();#{DirectMethods}.SetValueQuery();
                                                                  if (filterPosition.getValue() == '-1') {$('#filterPosition').removeClass('combo-selected');}
                                                                                else {$('#filterPosition').addClass('combo-selected');}" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{PagingToolbar1}.pageIndex =0; #{PagingToolbar1}.doLoad();
                                                                $('#filterPosition').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterContractStatus" DisplayField="Name" MinChars="1"
                                                            ItemSelector="div.list-item" Editable="true" ValueField="Id" Width="200" EmptyText="gõ để tìm kiếm" StoreID="storeContractStatus">
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
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); 
                                                                  if (filterContractStatus.getValue() == '-1') {$('#filterContractStatus').removeClass('combo-selected');}
                                                                                else {$('#filterContractStatus').addClass('combo-selected');}" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); 
                                                                $('#filterContractStatus').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:LockingGridView>
                            </View>
                            <LoadMask ShowMask="true" Msg="Đang tải...." />
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                    <Listeners>
                                        <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().id);hdfPrKey.setValue(RowSelectionModel1.getSelected().data.RecordId); " />
                                        <RowDeselect Handler="hdfRecordId.reset();" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <Listeners>
                                <Command Handler="Ext.net.DirectMethods.DownloadAttach(record.data.AttachFileName, {isUpload: true});" />
                            </Listeners>
                            <Listeners>
                                <RowDblClick Handler="" />
                            </Listeners>
                            <DirectEvents>
                                <RowDblClick>
                                    <EventMask ShowMask="true" />
                                </RowDblClick>
                            </DirectEvents>
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
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Center>
                </ext:BorderLayout>
            </Items>
        </ext:Viewport>
        <ext:Window runat="server" ID="wdHopDongHangLoat" Constrain="true" Modal="true" Title="Thêm hợp đồng hàng loạt"
            Icon="UserAdd" Layout="FormLayout" Resizable="false" AutoHeight="true" Width="650"
            Hidden="true" Padding="6" LabelWidth="120">
            <Items>
                <ext:Container ID="Container7" runat="server" Layout="Column" Height="27">
                    <Items>
                        <ext:Container ID="Container8" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                            LabelWidth="120">
                            <Items>
                                <ext:CompositeField ID="CompositeField1" runat="server" AnchorHorizontal="99%">
                                    <Items>
                                        <ext:TextField runat="server" FieldLabel="Số hợp đồng<span style='color:red;'>*</span>"
                                            Width="150" ID="txtHopDongSoHopDongHL" MaxLength="30" CtCls="requiredData" />
                                        <ext:Button runat="server" ID="Button1" Icon="Reload">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Sinh số hợp đồng mới (Chỉ áp dụng cho trường hợp chưa có số hợp đồng)" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Handler="if (#{txtHopDongSoHopDongHL}.getValue().trim() != '' && #{txtHopDongSoHopDongHL}.getValue() != null) { this.blur(); alert('Số hợp đồng đã được sinh');} else {#{DirectMethods}.GenerateSoQDHL();}" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:CompositeField>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container9" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbHopDongTinhTrangHopDongHL" DisplayField="Name"
                                    MinChars="1" ItemSelector="div.list-item" FieldLabel="Tình trạng HĐ" Editable="true"
                                    ValueField="Id" AnchorHorizontal="98%" StoreID="storeContractStatus"
                                    LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
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
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Hidden runat="server" ID="hdfLoaiHopDong" />
                <ext:ComboBox runat="server" ID="cbHopDongLoaiHopDongHL" DisplayField="Name" ItemSelector="div.list-item"
                    FieldLabel="Loại hợp đồng<span style='color:red;'>*</span>" Editable="true" MinChars="1"
                    StoreID="storeContractType" ValueField="Id" AnchorHorizontal="99%" CtCls="requiredData"
                    LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
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
                        <Select Handler="this.triggers[0].show();#{hdfLoaiHopDong}.setValue(#{cbHopDongLoaiHopDongHL}.getValue()); #{DirectMethods}.SetNgayHetHDHL();" />
                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                    </Listeners>
                </ext:ComboBox>
                <ext:Container ID="Container1" runat="server" Layout="Column" Height="80">
                    <Items>
                        <ext:Container ID="Container2" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5"
                            LabelWidth="120">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbHopDongCongViecHL" DisplayField="Name" FieldLabel="Chức danh"
                                    StoreID="storeJobTitle" Editable="true" MinChars="1" ValueField="Id" AnchorHorizontal="99%"
                                    ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Template ID="Template6" runat="server">
                                        <Html>
                                            <tpl for=".">
						                        <div class="list-item"> 
							                        {Name}
						                        </div>
					                        </tpl>
                                        </Html>
                                    </Template>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();" />
                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:DateField runat="server" Vtype="daterange" FieldLabel="Ngày ký kết<span style='color:red;'>*</span>"
                                    ID="dfHopDongNgayHopDongHL" AnchorHorizontal="99%" Editable="true" MaskRe="/[0-9\/]/"
                                    Format="d/M/yyyy" CtCls="requiredData">
                                    <CustomConfig>
                                        <ext:ConfigItem Name="endDateField" Value="#{dfHopDongNgayKiKetHL}" Mode="Value" />
                                    </CustomConfig>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();}" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:TextField ID="txt_NguoiKyHDHL" runat="server" FieldLabel="Người ký HĐ<span style='color:red;'>*</span>"
                                    AllowBlank="false" AnchorHorizontal="99%" MaxLength="20" LabelWidth="165" Width="300"
                                    CtCls="requiredData">
                                </ext:TextField>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container3" runat="server" LabelAlign="left" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbx_HopDongChucVuHL" FieldLabel="Chức vụ" DisplayField="Name"
                                    StoreID="storePosition" ValueField="Id" AnchorHorizontal="98%" Editable="true"
                                    MinChars="1" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu..." EmptyText="gõ để tìm kiếm">
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
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:DateField runat="server" FieldLabel="Ngày hiệu lực<span style='color:red;'>*</span>"
                                    ID="dfNgayCoHieuLucHL" AnchorHorizontal="98%" Editable="true" MaskRe="/[0-9\/]/"
                                    Format="d/M/yyyy" CtCls="requiredData">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetNgayHetHDHL();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();}" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:DateField runat="server" Vtype="daterange" FieldLabel="Ngày hết hạn" ID="dfHopDongNgayKiKetHL"
                                    AnchorHorizontal="98%" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy">
                                    <CustomConfig>
                                        <ext:ConfigItem Name="startDateField" Value="#{dfHopDongNgayHopDongHL}" Mode="Value" />
                                    </CustomConfig>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide();}" />
                                    </Listeners>
                                </ext:DateField>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Hidden runat="server" ID="hdfHopDongTepTinDKHL" />
                <ext:FileUploadField ID="fufHopDongTepTinHL" runat="server" EmptyText="Chọn tệp tin"
                    FieldLabel="Tệp tin đính kèm" ButtonText="" Icon="Attach" AnchorHorizontal="99%">
                </ext:FileUploadField>
                <ext:TextArea runat="server" ID="txtHopDongGhiChuHL" FieldLabel="Ghi chú" AnchorHorizontal="99%"
                    Height="35" />
                <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="230">
                    <Items>
                        <ext:GridPanel runat="server" ID="grp_DanhSachCanBo" TrackMouseOver="true" Title="Danh sách cán bộ"
                            StripeRows="true" Border="true" Region="Center" Icon="User" AutoExpandColumn="DepartmentName"
                            AutoExpandMin="150">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="tbDanhSachQD">
                                    <Items>
                                        <ext:Button runat="server" ID="btnChonDanhSachCanBo" Icon="UserAdd" Text="Chọn cán bộ"
                                            TabIndex="12">
                                            <Listeners>
                                                <Click Handler="ucChooseEmployee_wdChooseUser.show();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnXoaCanBo" Icon="Delete" Text="Xóa" Disabled="true"
                                            TabIndex="13">
                                            <Listeners>
                                                <Click Handler="#{grp_DanhSachCanBo}.deleteSelected(); #{hdfTotalRecord}.setValue(#{hdfTotalRecord}.getValue()*1 - 1);if(hdfTotalRecord.getValue() ==0){btnXoaCanBo.disable();}" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="grp_DanhSachCanBoStore" AutoLoad="false" runat="server" ShowWarningOnFailure="false"
                                    SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="RecordId">
                                            <Fields>
                                                <ext:RecordField Name="RecordId" />
                                                <ext:RecordField Name="EmployeeCode" />
                                                <ext:RecordField Name="FullName" />
                                                <ext:RecordField Name="DepartmentName" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel3" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn Header="STT" Width="40" />
                                    <ext:Column ColumnID="MA_CB" Header="Mã cán bộ" Width="100" DataIndex="EmployeeCode" />
                                    <ext:Column ColumnID="HO_TEN" Header="Họ tên" Width="200" DataIndex="FullName" />
                                    <ext:Column ColumnID="TEN_DONVI" Header="Bộ phận" Width="100" DataIndex="DepartmentName">
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:LockingGridView runat="server" ID="lkv1" />
                            </View>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" ID="chkEmployeeRowSelection">
                                    <Listeners>
                                        <RowSelect Handler="btnXoaCanBo.enable();" />
                                        <RowDeselect Handler="btnXoaCanBo.disable();" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Container>
            </Items>
            <Buttons>
                <ext:Button runat="server" ID="btnCapNhatHL" Text="Cập nhật" Icon="Disk">
                    <Listeners>
                        <Click Handler=" return CheckInputKTKLHangLoat(#{fufHopDongTepTinHL}.fileInput.dom);" />
                    </Listeners>
                     <DirectEvents>
                        <Click OnEvent="btnUpdateHL_Click">
                            <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                            <ExtraParams>
                                <ext:Parameter Name="ListRecordId" Value="getPrKeyRecordList()" Mode="Raw" />
                                <ext:Parameter Name="Close" Value="False" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnDongLaiHL" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="wdHopDongHangLoat.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Hide Handler="ResetWdHopDongHangLoat();" />
            </Listeners>
        </ext:Window>
    </div>
    </form>
</body>
</html>
