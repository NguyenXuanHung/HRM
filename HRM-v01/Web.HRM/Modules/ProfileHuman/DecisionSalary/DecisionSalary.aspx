<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.DecisionSalary.DecisionSalary" Codebehind="DecisionSalary.aspx.cs" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="/Modules/UC/ChooseEmployee.ascx" TagName="ChooseEmployee"
    TagPrefix="uc1" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script>
        var getVuotKhung = function () {
            var vuotkhung = txtVuotKhungMoi.getValue();
            if (vuotkhung > 100 || vuotkhung < 0) {
                alert('Giá trị không hợp lệ');
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
        
        var searchBoxPositionHL = function (f, e) {
            hdfMakerTempPositionHL.setValue(cbxMakerPositionHL.getRawValue());
            if (hdfIsMakerPositionHL.getValue() == '1') {
                hdfIsMakerPositionHL.setValue('2');
            }
            if (cbxMakerPositionHL.getRawValue() == '') {
                hdfIsMakerPositionHL.reset();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM" />
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfButtonClick" />
            <ext:Hidden runat="server" ID="hdfMenuID" />
            <ext:Hidden runat="server" ID="hdfUserID" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <uc1:ChooseEmployee ID="ucChooseEmployee1" runat="server" ChiChonMotCanBo="false"
                DisplayWorkingStatus="DangLamViec" />
            <ext:Store runat="server" ID="cbHopDongLoaiHopDongStore" AutoLoad="false" OnRefreshData="cbHopDongLoaiHopDongStore_OnRefreshData">
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
            <ext:Store ID="cbxQuantumStore" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerQuantum.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Quantum" Mode="Value" />
                </BaseParams>
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
            <ext:Menu ID="RowContextMenu" runat="server">
                <Items>
                    <ext:MenuItem ID="mnuApDungChoTatCa" runat="server" Icon="UserTick" Text="Áp dụng cho tất cả cán bộ">
                    </ext:MenuItem>
                </Items>
            </ext:Menu>
            <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wdShowReport"
                Title="Báo cáo quyết định lương" Maximized="true" Icon="Printer">
                <Items>
                    <ext:TabPanel ID="pnReportPanel" Region="Center" AnchorVertical="100%" Border="false"
                        runat="server">
                    </ext:TabPanel>
                </Items>
                <Listeners>
                    <BeforeShow Handler="pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../../Report/BaoCao_Main.aspx?type=QuyetDinhLuong&id=' + hdfRecordId.getValue(), 'Báo cáo quyết định lương');" />
                </Listeners>
                <Buttons>
                    <ext:Button ID="Button5" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdShowReport}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Tạo quyết định lương" Resizable="true" Layout="FormLayout"
                Padding="6" Width="1140" Hidden="true" Icon="UserTick" ID="wdTaoQuyetDinhLuong"
                Modal="true" Constrain="true" AutoHeight="true">
                <Items>
                    <ext:FieldSet runat="server" ID="FieldSet9" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Ctn11" Layout="ColumnLayout" Height="50" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" ID="Ctn12" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfChonCanBo" />
                                           <ext:ComboBox ID="cbxChonCanBo" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                                FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                                                ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                                LoadingText="Đang tải dữ liệu..." AnchorHorizontal="98%" runat="server">
                                                <Store>
                                                <ext:Store ID="cbxChonCanBo_Store" runat="server" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                                    </Proxy>
                                                    <AutoLoadParams>
                                                        <ext:Parameter Name="start" Value="0" />
                                                        <ext:Parameter Name="limit" Value="10" />
                                                    </AutoLoadParams>
                                                    <BaseParams>
                                                        <ext:Parameter Name="handlers" Value="SearchUser" />
                                                        <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                                    </BaseParams>
                                                    <Reader>
                                                        <ext:JsonReader Root="plants" TotalProperty="total">
                                                            <Fields>
                                                                <ext:RecordField Name="FullName" />
                                                                <ext:RecordField Name="EmployeeCode" />
                                                                <ext:RecordField Name="BirthDate" />
                                                                <ext:RecordField Name="DepartmentName" />
                                                                <ext:RecordField Name="Id" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <Template ID="Template4" runat="server">
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
                                                    <Select Handler="hdfChonCanBo.setValue(cbxChonCanBo.getValue());" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Select OnEvent="cbxChonCanBo_Selected" />
                                                </DirectEvents>
                                            </ext:ComboBox>
                                             <ext:TextField runat="server" ID="txtDepartment" FieldLabel="Bộ phận" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Ctn13" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtChucVu" FieldLabel="Chức vụ" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtCongViec" FieldLabel="Chức danh" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="Ctn15" Layout="ColumnLayout" Height="485" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Ctn16" Layout="FormLayout" ColumnWidth="0.34">
                                <Items>
                                    <ext:FieldSet runat="server" ID="Fs10" Layout="FormLayout" Title="Thông tin quyết định lương gần nhất"
                                        AnchorHorizontal="98%" Height="465">
                                        <Items>
                                            <ext:Container runat="server" ID="Ctn21" Layout="FormLayout" Height="150">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtSoQDCu" FieldLabel="Số quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtTenQDCu" FieldLabel="Tên quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:DateField runat="server" ID="dfNgayQDCu" FieldLabel="Ngày quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-control-style">
                                                    </ext:DateField>
                                                    <ext:TextField runat="server" ID="txtNguoiQDCu" FieldLabel="Người quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:DateField runat="server" ID="dfNgayCoHieuLucCu" FieldLabel="Ngày hiệu lực" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-control-style">
                                                    </ext:DateField>
                                                    <ext:TextField runat="server" ID="cbHopDongLoaiHopDongCu" FieldLabel="Loại hợp đồng" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" ID="Ctn22" Layout="FormLayout" Height="240">
                                                <Items>
                                                    <ext:TextField runat="server" ID="cbxNgachCu" FieldLabel="Ngạch" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtBacLuongCu" FieldLabel="Bậc lương" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtHeSoLuongCu" FieldLabel="Hệ số lương" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtMucLuongCu" FieldLabel="Mức lương" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtLuongDongBHCu" FieldLabel="Lương đóng BH" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtPhuCapKhacCu" FieldLabel="Phụ cấp khác" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtVuotKhungCu" FieldLabel="Vượt khung" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtThamNienCu" FieldLabel="Thâm niên" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtPCNganhCu" FieldLabel="Phụ cấp ngành" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtPCTrachNhiemCu" FieldLabel="Phụ cấp trách nhiệm" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:TextField runat="server" ID="txtPCKhuVucCu" FieldLabel="Phụ cấp khu vực" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Ctn17" Layout="FormLayout" ColumnWidth="0.66">
                                <Items>
                                    <ext:FieldSet runat="server" ID="FieldSet5" Title="Thông tin quyết định mới" Layout="FormLayout"
                                        AnchorHorizontal="100%" LabelWidth="110">
                                        <Items>
                                            <ext:Container runat="server" ID="Container18" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="26">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container19" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="110">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtSoQDMoi" CtCls="requiredData" AnchorHorizontal="99%"
                                                                FieldLabel="Số quyết định<span style='color:red'>*</span>" MaxLength="20">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container20" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfNgayQDMoi" FieldLabel="Ngày quyết định"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%">
                                                            </ext:DateField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:TextField runat="server" ID="txtTenQDMoi" FieldLabel="Tên quyết định<span style='color:red'>*</span>"
                                                AnchorHorizontal="100%" MaxLength="200" CtCls="requiredData">
                                            </ext:TextField>
                                            <ext:Container runat="server" ID="Container1" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="27">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container2" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="110">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfNgayHieuLucMoi" CtCls="requiredData" FieldLabel="Ngày hiệu lực<span style='color:red'>*</span>"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="99%">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                            <ext:Hidden runat="server" ID="hdfNguoiQuyetDinh" />
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container3" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>

                                                            <ext:Hidden ID="hdfLoaiHopDong" runat="server"></ext:Hidden>
                                                            <ext:ComboBox runat="server" ID="cbHopDongLoaiHopDongMoi" DisplayField="ContractTypeName"
                                                                ItemSelector="div.list-item" FieldLabel="Loại hợp đồng" Editable="false" ValueField="Id"
                                                                AnchorHorizontal="100%" TabIndex="2" ListWidth="200" StoreID="cbHopDongLoaiHopDongStore">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Template ID="Template10" runat="server">
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
                                                                    <Focus Handler="#{cbHopDongLoaiHopDongStore}.reload();" />
                                                                    <Select Handler="hdfLoaiHopDong.setValue(cbHopDongLoaiHopDongMoi.getValue()); this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfLoaiHopDong.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" Layout="ColumnLayout" Height="27" ColumnWidth="1" >
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtNguoiQD" FieldLabel="Người QĐ"
                                                        AnchorHorizontal="99%" ColumnWidth="0.6">
                                                    </ext:TextField>
                                                    <ext:DisplayField runat="server" Width="5" />
                                                    <ext:Checkbox runat="server" ID="chkNangNgach" FieldLabel="Nâng ngạch mới">
                                                    </ext:Checkbox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" Layout="ColumnLayout" Height="27" ColumnWidth="1">
                                                <Items>
                                                    <ext:Hidden runat="server" ID="hdfIsMakerPosition" Text="0" />
                                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                                    <ext:Hidden runat="server" ID="hdfMakerPosition" />
                                                    <ext:Hidden runat="server" ID="hdfMakerTempPosition" />
                                                    <ext:ComboBox runat="server" ID="cbxMakerPosition" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true" ColumnWidth="1"
                                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                        EnableKeyEvents="true" StoreID="storePosition">
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
                                                            <Select Handler="this.triggers[0].show();  hdfSignerPosition.setValue(cbxSignerPosition.getValue());
				                                                                    hdfIsSignerPosition.setValue('1');
				                                                                    hdfMakerTempPosition.setValue(cbxSignerPosition.getRawValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsSignerPosition.reset();hdfMakerTempPosition.reset();hdfSignerPosition.reset();  }" />
                                                            <KeyUp Fn="searchBoxPosition" />
                                                            <Blur Handler="cbxSignerPosition.setRawValue(hdfMakerTempPosition.getValue());
			                                                                        if (hdfIsSignerPosition.getValue() != '1') {cbxSignerPosition.setValue(hdfMakerTempPosition.getValue());}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Hidden runat="server" ID="hdfTepTinDinhKem" />
                                            <ext:CompositeField ID="composifieldAttach" runat="server" AnchorHorizontal="100%"
                                                FieldLabel="Tệp tin đính kèm">
                                                <Items>
                                                    <ext:FileUploadField ID="fufTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                                                        ButtonText="" Icon="Attach" Width="353">
                                                    </ext:FileUploadField>
                                                    <ext:Button runat="server" ID="btnQDLDownload" Icon="ArrowDown" ToolTip="Tải về">
                                                        <DirectEvents>
                                                            <Click OnEvent="btnQDLDownload_Click" IsUpload="true" />
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button runat="server" ID="btnQDLDelete" Icon="Delete" ToolTip="Xóa">
                                                        <DirectEvents>
                                                            <Click OnEvent="btnQDLDelete_Click" After="#{fufTepTinDinhKem}.reset();">
                                                                <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                                                    ConfirmRequest="true" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:CompositeField>
                                            <ext:TextArea runat="server" ID="txtGhiChuMoi" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                                Height="40" MaxLength="500" />
                                        </Items>
                                    </ext:FieldSet>
                                    <ext:FieldSet runat="server" ID="Fs6" AnchorHorizontal="100%" Title="Thông tin lương mới"
                                        Layout="FormLayout">
                                        <Items>
                                            <ext:Container runat="server" ID="Ctn5" Layout="ColumnLayout" Height="225" AnchorHorizontal="100%">
                                                <Items>
                                                    <ext:Container runat="server" ID="Ctn6" Layout="FormLayout" ColumnWidth="0.5" LabelWidth="110">
                                                        <Items>
                                                            <ext:Hidden ID="hdfQuantumId" runat="server" />
                                                            <ext:ComboBox runat="server" ID="cbx_ngachMoi" FieldLabel="Ngạch<span style='color:red'>*</span>"
                                                                DisplayField="Name" ValueField="Id" AnchorHorizontal="99%" MinChars="1" StoreID="cbxQuantumStore"
                                                                TabIndex="83" Editable="true" ItemSelector="div.list-item" CtCls="requiredData">
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
                                                                    <Select Handler="this.triggers[0].show(); hdfQuantumId.setValue(cbx_ngachMoi.getValue()); cbxBacStore.reload(); if(cbxBacMoi.getValue() != ''){Ext.net.DirectMethods.GetThongTinLuong();}" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfQuantumId.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtHeSoLuongMoi" FieldLabel="Hệ số lương<span style='color:red'>*</span>"
                                                                AnchorHorizontal="99%" MaskRe="/[0-9.,]/" MaxLength="9" CtCls="requiredData">
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtLuongDongBHMoi" FieldLabel="Lương đóng BH" AnchorHorizontal="99%"
                                                                MaxLength="15" MaskRe="/[0-9.,]/">
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtBacLuongNBMoi" FieldLabel="Bậc lương NB" AnchorHorizontal="99%"
                                                                MaxLength="15" MaskRe="/[0-9.,]/">
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtPhuCapKhacMoi" FieldLabel="Phụ cấp khác" AnchorHorizontal="99%"
                                                                MaxLength="15" MaskRe="/[0-9.,]/">
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtPhuCapChucVuMoi" FieldLabel="Phụ cấp chức vụ"
                                                                AnchorHorizontal="99%" MaxLength="15" MaskRe="/[0-9.,]/">
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtPCNganh" FieldLabel="Phụ cấp ngành"
                                                                AnchorHorizontal="99%" EmptyText="" MaxLength="4" MaskRe="/[0-9.,]/">
                                                                <Listeners>
                                                                    <Blur Handler="getVuotKhung();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtPhuCapTrachNhiemMoi" FieldLabel="Phụ cấp trách nhiệm"
                                                                AnchorHorizontal="99%" EmptyText="" MaxLength="4" MaskRe="/[0-9.,]/">
                                                                <Listeners>
                                                                    <Blur Handler="getVuotKhung();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                             <ext:TextField runat="server" ID="txtPhuCapKhuVucMoi" FieldLabel="Phụ cấp khu vực"
                                                                AnchorHorizontal="99%" EmptyText="" MaxLength="4" MaskRe="/[0-9.,]/">
                                                                <Listeners>
                                                                    <Blur Handler="getVuotKhung();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Ctn7" Layout="FormLayout" ColumnWidth="0.5" LabelWidth="107">
                                                        <Items>
                                                            <ext:Hidden ID="hdfSalaryGrade" runat="server" />
                                                            <ext:ComboBox runat="server" ID="cbxBacMoi" FieldLabel="Bậc lương<span style='color:red'>*</span>"
                                                                DisplayField="TEN" ValueField="MA" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
                                                                CtCls="requiredData">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Template ID="Template8" runat="server">
                                                                    <Html>
                                                                        <tpl for=".">
						                                                <div class="list-item"> 
							                                                {TEN}
						                                                </div>
					                                                </tpl>
                                                                    </Html>
                                                                </Template>
                                                                <Store>
                                                                    <ext:Store runat="server" ID="cbxBacStore" AutoLoad="false" OnRefreshData="cbxBacStore_OnRefreshData">
                                                                        <Reader>
                                                                            <ext:JsonReader IDProperty="MA">
                                                                                <Fields>
                                                                                    <ext:RecordField Name="MA" />
                                                                                    <ext:RecordField Name="TEN" />
                                                                                </Fields>
                                                                            </ext:JsonReader>
                                                                        </Reader>
                                                                    </ext:Store>
                                                                </Store>
                                                                <Listeners>
                                                                    <Focus Handler="cbxBacStore.reload();" />
                                                                    <Expand Handler="if (cbx_ngachMoi.getValue() == '') { cbxBacStore.removeAll(); alert('Bạn phải chọn ngạch trước');} cbxBacStore.reload();" />
                                                                    <Select Handler="this.triggers[0].show(); hdfSalaryGrade.setValue(cbxBacMoi.getValue()); Ext.net.DirectMethods.GetThongTinLuong();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryGrade.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtMucLuongMoi" FieldLabel="Mức lương" AnchorHorizontal="100%"
                                                                MaskRe="/[0-9.,]/" MaxLength="15">
                                                            </ext:TextField>
                                                            <ext:DateField runat="server" ID="dfNgayHLMoi" FieldLabel="Ngày hưởng lương" AnchorHorizontal="100%"
                                                                MaskRe="/[0-9|/]/" Format="d/M/yyyy">
                                                            </ext:DateField>
                                                            <ext:TextField runat="server" ID="txtVuotKhungMoi" FieldLabel="Vượt khung"
                                                                AnchorHorizontal="99%" EmptyText="nhập số %" MaxLength="4" MaskRe="/[0-9.,]/">
                                                                <Listeners>
                                                                    <Blur Handler="getVuotKhung();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtThamnien" FieldLabel="Thâm niên"
                                                                AnchorHorizontal="99%" EmptyText="nhập số %" MaxLength="4" MaskRe="/[0-9.,]/">
                                                                <Listeners>
                                                                    <Blur Handler="getVuotKhung();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:DateField runat="server" ID="dfNgayHLNBMoi" FieldLabel="Ngày HL nội bộ" AnchorHorizontal="100%"
                                                                MaskRe="/[0-9|/]/" Format="d/M/yyyy">
                                                            </ext:DateField>
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
                    <ext:Button runat="server" ID="btnCapNhat" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return ValidateWdTaoQuyetDinhLuong();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhat_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCapNhatSua" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return ValidateWdTaoQuyetDinhLuong();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhat_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Edit" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCapNhatDongLai" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return ValidateWdTaoQuyetDinhLuong();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhat_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLai" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdTaoQuyetDinhLuong}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetWdTaoQuyetDinhLuong();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" Title="Tạo quyết định lương hàng loạt" Resizable="true"
                Layout="FormLayout" Padding="6" Width="1140" Hidden="true" Icon="UserTick" ID="wdTaoQuyetDinhLuongHangLoat"
                Modal="true" Constrain="true" AutoHeight="true">
                <Items>
                    <ext:Container runat="server" ID="Container26" Layout="ColumnLayout" Height="250"
                        AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Container27" Layout="FormLayout" ColumnWidth="0.50">
                                <Items>
                                    <ext:FormPanel runat="server" ID="fpTTQD" Frame="true" AnchorHorizontal="99%" Title="Thông tin quyết định"
                                        Icon="BookOpen" Height="250">
                                        <Items>
                                            <ext:Container runat="server" ID="Container8" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="26">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container9" Layout="FormLayout" ColumnWidth="0.5">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtSoQDHL" CtCls="requiredData" AnchorHorizontal="99%"
                                                                FieldLabel="Số quyết định<span style='color:red'>*</span>" MaxLength="20">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container10" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfNgayQDHL" CtCls="requiredData" FieldLabel="Ngày quyết định"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%">
                                                            </ext:DateField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:TextField runat="server" ID="txtTenQDHL" CtCls="requiredData" FieldLabel="Tên quyết định<span style='color:red'>*</span>"
                                                AnchorHorizontal="100%">
                                            </ext:TextField>
                                            <ext:Container runat="server" ID="Container14" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="27">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container23" Layout="FormLayout" ColumnWidth="0.5">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfNgayHieuLucHL" CtCls="requiredData" FieldLabel="Ngày hiệu lực<span style='color:red'>*</span>"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="99%">
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
                                                    <ext:Container runat="server" ID="Container24" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtNguoiQDHL" FieldLabel="Người QĐ"
                                                                AnchorHorizontal="99%">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                             <ext:Container runat="server" Layout="ColumnLayout" Height="27" ColumnWidth="1">
                                                <Items>
                                                    <ext:Hidden runat="server" ID="hdfIsMakerPositionHL" Text="0" />
                                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                                    <ext:Hidden runat="server" ID="hdfMakerPositionHL" />
                                                    <ext:Hidden runat="server" ID="hdfMakerTempPositionHL" />
                                                    <ext:ComboBox runat="server" ID="cbxMakerPositionHL" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true" ColumnWidth="1"
                                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                        EnableKeyEvents="true" StoreID="storePosition">
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
                                                            <Select Handler="this.triggers[0].show();  hdfSignerPositionHL.setValue(cbxSignerPositionHL.getValue());
				                                                                    hdfIsSignerPositionHL.setValue('1');
				                                                                    hdfMakerTempPositionHL.setValue(cbxSignerPositionHL.getRawValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsSignerPositionHL.reset();hdfMakerTempPositionHL.reset();hdfSignerPositionHL.reset();  }" />
                                                            <KeyUp Fn="searchBoxPositionHL" />
                                                            <Blur Handler="cbxSignerPositionHL.setRawValue(hdfMakerTempPositionHL.getValue());
			                                                                        if (hdfIsSignerPositionHL.getValue() != '1') {cbxSignerPositionHL.setValue(hdfMakerTempPositionHL.getValue());}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Hidden runat="server" ID="hdfTepTinDinhKemHL" />
                                            <ext:CompositeField ID="cpfAttachHL" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
                                                <Items>
                                                    <ext:FileUploadField ID="fufTepTinDinhKemHL" runat="server" EmptyText="Chọn tệp tin"
                                                        ButtonText="" Icon="Attach" Width="310">
                                                    </ext:FileUploadField>
                                                    <ext:Button runat="server" ID="btnHLDelete" Icon="Delete" ToolTip="Xóa">
                                                        <Listeners>
                                                            <Click Handler="#{fufTepTinDinhKemHL}.reset();" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:CompositeField>
                                            <ext:TextArea runat="server" ID="txtGhiChuHL" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                                Height="50" />
                                        </Items>
                                    </ext:FormPanel>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Container28" Layout="FormLayout" ColumnWidth="0.50">
                                <Items>
                                    <ext:FormPanel runat="server" ID="fpn_QDLuongHL" Frame="true" AnchorHorizontal="100%"
                                        Title="Thông tin lương" Icon="Money" Height="250">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfQuantumHLId" />
                                            <ext:Container runat="server" ID="Container29" Layout="ColumnLayout" Height="152"
                                                AnchorHorizontal="100%">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container30" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="110">
                                                        <Items>
                                                            <ext:ComboBox runat="server" ID="cbxNgachHL" FieldLabel="Ngạch<span style='color:red'>*</span>"
                                                                DisplayField="Name" StoreID="cbxQuantumStore" ValueField="Id" AnchorHorizontal="99%"
                                                                TabIndex="83" CtCls="requiredData" Editable="false" ItemSelector="div.list-item"
                                                                ListWidth="200" DataIndex="MaNgach">
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
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                    <Expand Handler="return checkChooseEmployeeFirst();if(cbxNgachHL.store.getCount()==0) cbxQuantumStore.reload();" />
                                                                    <Select Handler="this.triggers[0].show(); hdfQuantumHLId.setValue(cbxNgachHL.getValue()); cbxBacLuongHLStore.reload(); if(cbxBacLuongHL.getValue() != '') {Ext.net.DirectMethods.GetSalaryInfoHL();}" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfQuantumHLId.reset(); }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtHeSoLuongHL" FieldLabel="Hệ số lương<span style='color:red'>*</span>"
                                                                AnchorHorizontal="99%" MaskRe="/[0-9.]/" MaxLength="9" DataIndex="HeSoLuong"
                                                                CtCls="requiredData" AllowBlank="false">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtLuongDongBHHL" FieldLabel="Lương đóng BH" AnchorHorizontal="99%"
                                                                MaskRe="/[0-9.]/" MaxLength="15" DataIndex="LuongDongBHXH">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtBacLuongNBHL" FieldLabel="Bậc lương NB" AnchorHorizontal="99%"
                                                                DataIndex="BacLuongNB" MaskRe="/[0-9.]/">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtPhuCapKhacHL" FieldLabel="Phụ cấp khác" AnchorHorizontal="99%"
                                                                MaskRe="/[0-9.]/" MaxLength="15" DataIndex="PhuCapKhac">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="txtPhuCapChucVuHL" FieldLabel="Phụ cấp chức vụ"
                                                                AnchorHorizontal="99%" MaskRe="/[0-9.]/" MaxLength="15" DataIndex="PhuCapChucVu">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>

                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container31" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:Hidden runat="server" ID="hdfSalaryGradeHL" />
                                                            <ext:ComboBox runat="server" ID="cbxBacLuongHL" FieldLabel="Bậc lương<span style='color:red'>*</span>"
                                                                DisplayField="TEN" ValueField="MA" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
                                                                CtCls="requiredData">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Template ID="Template9" runat="server">
                                                                    <Html>
                                                                        <tpl for=".">
						                                                <div class="list-item"> 
							                                                {TEN}
						                                                </div>
					                                                </tpl>
                                                                    </Html>
                                                                </Template>
                                                                <Store>
                                                                    <ext:Store runat="server" ID="cbxBacLuongHLStore" AutoLoad="false" OnRefreshData="cbxBacLuongHLStore_OnRefreshData">
                                                                        <Reader>
                                                                            <ext:JsonReader IDProperty="MA">
                                                                                <Fields>
                                                                                    <ext:RecordField Name="MA" />
                                                                                    <ext:RecordField Name="TEN" />
                                                                                </Fields>
                                                                            </ext:JsonReader>
                                                                        </Reader>
                                                                    </ext:Store>
                                                                </Store>
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                    <Expand Handler="return checkChooseEmployeeFirst(); if (cbxNgachHL.getValue() == ''){ cbxBacLuongHLStore.removeAll(); alert('Bạn phải chọn ngạch trước');}" />
                                                                    <Select Handler="this.triggers[0].show();hdfSalaryGradeHL.setValue(cbxBacLuongHL.getValue()); Ext.net.DirectMethods.GetSalaryInfoHL();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryGradeHL.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtMucLuongHL" FieldLabel="Mức lương" AnchorHorizontal="100%"
                                                                MaskRe="/[0-9.]/" MaxLength="15" DataIndex="LuongCung" AllowBlank="true">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:DateField runat="server" ID="dfNgayHuongLuongHL" FieldLabel="Ngày hưởng lương"
                                                                AnchorHorizontal="100%" MaskRe="/[0-9|/]/" Format="d/M/yyyy" DataIndex="NgayHuongLuong">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                    <Select Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                            <ext:DateField runat="server" ID="dfNgayHuongLuongNBHL" FieldLabel="Ngày HL nội bộ"
                                                                AnchorHorizontal="100%" MaskRe="/[0-9|/]/" Format="d/M/yyyy" DataIndex="NgayHuongLuongNB">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                    <Select Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                            <ext:TextField runat="server" ID="txtVuotKhungHL" FieldLabel="Vượt khung"
                                                                AnchorHorizontal="99%" EmptyText="nhập số %" MaskRe="/[0-9.,]/" MaxLength="3" DataIndex="VuotKhung">
                                                                <Listeners>
                                                                    <Focus Handler="return checkChooseEmployeeFirst();" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                        <Buttons>
                                            <ext:Button runat="server" ID="btnCapNhatTTLuong" Text="Cập nhật" Icon="Disk">
                                                <Listeners>
                                                    <Click Handler="updateRecord(#{grp_DanhSachQuyetDinh});" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnCapNhatTatCaCanBo" Text="Cập nhật cho tất cả cán bộ"
                                                Icon="DiskMultiple">
                                                <Listeners>
                                                    <Click Handler="updateAllRecord(#{grp_DanhSachQuyetDinh});" />
                                                </Listeners>
                                            </ext:Button>
                                        </Buttons>
                                    </ext:FormPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" ID="Container25" Layout="BorderLayout" Height="250">
                        <Items>
                            <ext:GridPanel runat="server" ID="grp_DanhSachQuyetDinh" TrackMouseOver="true" Title="Danh sách cán bộ nhận quyết định"
                                StripeRows="true" Border="true" Region="Center" Icon="User">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tbDanhSachQD">
                                        <Items>
                                            <ext:Button runat="server" ID="btnChonDanhSachCanBo" Icon="UserAdd" Text="Chọn cán bộ">
                                                <Listeners>
                                                    <Click Handler="ucChooseEmployee1_wdChooseUser.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnXoaCanBo" Icon="Delete" Text="Xóa" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="#{grp_DanhSachQuyetDinh}.deleteSelected();#{fpn_QDLuongHL}.getForm().reset();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="grp_DanhSachQuyetDinh_Store" AutoSave="false" AutoLoad="false" runat="server"
                                        ShowWarningOnFailure="false" SkipIdForNewRecords="false"
                                        RefreshAfterSaving="None">
                                        <Reader>
                                            <ext:JsonReader IDProperty="RecordId">
                                                <Fields>
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="PositionName" />
                                                    <ext:RecordField Name="QuantumId" DefaultValue="0"/>
                                                    <ext:RecordField Name="QuantumName" />
                                                    <ext:RecordField Name="SalaryGrade" />
                                                    <ext:RecordField Name="SalaryFactor" DefaultValue="0"/>
                                                    <ext:RecordField Name="SalaryBasic" DefaultValue="0"/>
                                                    <ext:RecordField Name="SalaryInsurance" DefaultValue="0"/>
                                                    <ext:RecordField Name="SalaryPayDate" DateFormat="dd/MM/yyyy"/>
                                                    <ext:RecordField Name="OtherAllowance" DefaultValue="0" />
                                                    <ext:RecordField Name="OutFrame" DefaultValue="0"/>
                                                    <ext:RecordField Name="PositionAllowance" DefaultValue="0"/>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel ID="ColumnModel3" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Locked="true" Width="35" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Locked="true" Width="70" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="HO_TEN" Header="Họ tên" Locked="true" Width="150" DataIndex="FullName" />
                                        <ext:Column ColumnID="TEN_DONVI" Header="Bộ phận" Width="200" DataIndex="DepartmentName">
                                        </ext:Column>
                                        <ext:Column ColumnID="TEN_CHUCVU" Header="Chức vụ" Width="150" DataIndex="PositionName">
                                        </ext:Column>
                                        <ext:Column ColumnID="MaNgach" Header="Ngạch" Width="150" DataIndex="QuantumId">
                                            <Renderer Fn="ngachRenderer" />
                                        </ext:Column>
                                        <ext:Column ColumnID="BacLuong" Header="Bậc lương" Width="80" DataIndex="SalaryGrade">
                                            <Renderer Fn="RenderBacLuong" />
                                        </ext:Column>
                                        <ext:Column ColumnID="HeSoLuong" Header="Hệ số lương" Width="90" DataIndex="SalaryFactor" Align="Right">
                                        </ext:Column>
                                        <ext:Column ColumnID="LuongCung" Header="Mức lương" Width="80" DataIndex="SalaryBasic" Align="Right">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="LuongDongBHXH" Header="Lương đóng BHXH" Width="125" DataIndex="SalaryInsurance" Align="Right">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:DateColumn ColumnID="NgayHuongLuong" Header="Ngày hưởng lương" Width="120" DataIndex="SalaryPayDate"
                                            Format="dd/MM/yyyy">
                                        </ext:DateColumn>
                                        <ext:Column ColumnID="PhuCapChucVu" Header="Phụ cấp chức vụ" Width="120" DataIndex="PositionAllowance" Align="Right">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="PhuCapKhac" Header="Phụ cấp khác" Width="100" DataIndex="OtherAllowance" Align="Right">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="VuotKhung" Header="Vượt khung" Width="100" DataIndex="OutFrame" Align="Right">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1" LockText="Cố định cột này lại"
                                        UnlockText="Hủy cố định cột" />
                                </View>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelQDHL" SingleSelect="true">
                                        <Listeners>
                                            <RowSelect Handler="#{fpn_QDLuongHL}.getForm().loadRecord(record);#{fpn_QDLuongHL}.record = record;btnXoaCanBo.enable();" />
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
                            <Click Handler="return ValidateWdTaoQuyetDinhLuongHangLoat();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhatHL_Click">
                                <EventMask ShowMask="true" />
                                <ExtraParams>
                                    <ext:Parameter Name="json" Value="getJsonOfStore(grp_DanhSachQuyetDinh_Store)" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCapNhatDongLaiHL" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return ValidateWdTaoQuyetDinhLuongHangLoat();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnCapNhatHL_Click">
                                <EventMask ShowMask="true" />
                                <ExtraParams>
                                    <ext:Parameter Name="close" Value="True" />
                                    <ext:Parameter Name="json" Value="getJsonOfStore(grp_DanhSachQuyetDinh_Store)" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button6" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdTaoQuyetDinhLuongHangLoat}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetWdTaoQuyetDinhLuongHangLoat();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" Title="Cấu hình các cột trên lưới" Resizable="false" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="Cog" ID="wdConfigGridPanel" Modal="true"
                Constrain="true" AutoHeight="true">
                <Items>
                    <ext:FieldSet runat="server" ID="fs17" Title="Chọn các cột muốn hiển thị trên lưới"
                        AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="ctn105" AnchorHorizontal="100%" Layout="ColumnLayout"
                                Height="180">
                                <Items>
                                    <ext:Container runat="server" ID="ctn106" ColumnWidth="0.5" Layout="RowLayout">
                                        <Items>
                                            <ext:Checkbox runat="server" ID="chkLuongCung" BoxLabel="Mức lương" Height="25" Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkHeSoLuong" BoxLabel="Hệ số lương" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkPhuCapKhac" BoxLabel="Phụ cấp khác" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkVuotKhung" BoxLabel="Vượt khung" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkLuongDongBHXH" BoxLabel="Lương đóng BHXH" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkBacLuong" BoxLabel="Bậc lương" Height="25" Checked="true" />
                                            <ext:Checkbox runat="server" Hidden="true" ID="chkBacLuongNB" BoxLabel="Bậc lương NB" Height="25"
                                                Checked="true" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="ctn107" ColumnWidth="0.5" Layout="RowLayout">
                                        <Items>
                                            <ext:Checkbox runat="server" ID="chkNgayHL" BoxLabel="Ngày hưởng lương" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkNgayHLNB" Hidden="true" BoxLabel="Ngày hưởng lương nội bộ"
                                                Height="25" Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkSoQD" BoxLabel="Số quyết định" Height="25" Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkNgayQD" BoxLabel="Ngày quyết định" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkNgayHieuLuc" BoxLabel="Ngày hiệu lực" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkNgayHetHieuLuc" BoxLabel="Phụ cấp chức vụ" Height="25"
                                                Checked="true" />
                                            <ext:Checkbox runat="server" ID="chkNguoiQD" BoxLabel="Người quyết định" Height="25"
                                                Checked="true" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnCapNhatCauHinh" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="btnCapNhatCauHinh_Click">
                                <EventMask ShowMask="true" Msg="Đang tải dữ liệu. Vui lòng chờ..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLaiCauHinh" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdConfigGridPanel.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <BeforeShow Handler="Ext.net.DirectMethods.LoadConfigGridPanel();" />
                    <Hide Handler="ResetWdConfig();" />
                </Listeners>
            </ext:Window>
            <ext:Viewport runat="server" ID="vp" AutoScroll="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="br">
                        <Center>
                            <ext:GridPanel ID="GridPanel1" TrackMouseOver="true" Header="false" runat="server" AutoScroll="true"
                                Title="Bảng quyết định lương của cán bộ công nhân viên" StripeRows="true" Border="false"
                                AnchorHorizontal="100%">
                                <Store>
                                    <ext:Store ID="Store1" AutoLoad="true" runat="server" GroupField="FullName">
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/HandlerDecisionSalary.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="searchKey" Value="txtSearchKey.getValue()" Mode="Raw"/>
                                            <ext:Parameter Name="trangThai" Value="DaDuyet" Mode="Value"/>
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="DecisionNumber" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="SalaryFactor" />
                                                    <ext:RecordField Name="SalaryBasic" />
                                                    <ext:RecordField Name="SalaryInsurance" />
                                                    <ext:RecordField Name="SalaryGrade" />
                                                    <ext:RecordField Name="SalaryPayDate" />
                                                    <ext:RecordField Name="SalaryGradeLift" />
                                                    <ext:RecordField Name="DecisionDate" />
                                                    <ext:RecordField Name="EffectiveDate" />
                                                    <ext:RecordField Name="Note" />
                                                    <ext:RecordField Name="OtherAllowance" />
                                                    <ext:RecordField Name="OutFrame" />
                                                    <ext:RecordField Name="SignerName" />
                                                    <ext:RecordField Name="Sex" DefaultValue="false"/>
                                                    <ext:RecordField Name="BirthDate" />
                                                    <ext:RecordField Name="QuantumName" />
                                                    <ext:RecordField Name="PositionAllowance" />
                                                    <ext:RecordField Name="AttachFileName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tb">
                                        <Items>
                                            <ext:Button ID="btnTaoQuyetDinh" runat="server" Text="Tạo quyết định" Icon="UserTick">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem Text="Tạo quyết định cho một cán bộ" ID="mnuTaoQDChoMotCB" Icon="User">
                                                                <Listeners>
                                                                    <Click Handler="#{hdfButtonClick}.setValue('Insert');#{btnCapNhat}.show();#{btnCapNhatSua}.hide();#{btnCapNhatDongLai}.show();#{wdTaoQuyetDinhLuong}.show();" />
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem Text="Tạo quyết định hàng loạt" Icon="User" ID="mnuTaoQDHangLoat">
                                                                <Listeners>
                                                                    <Click Handler="#{hdfButtonClick}.setValue('Insert');#{wdTaoQuyetDinhLuongHangLoat}.show();" />
                                                                </Listeners>

                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:Button ID="btnEdit" runat="server" Disabled="true" Text="Sửa" Icon="Pencil">
                                                <Listeners>
                                                    <Click Handler="return CheckSelectedRows(GridPanel1);#{hdfButtonClick}.setValue('Edit');#{btnCapNhat}.hide();#{btnCapNhatSua}.show();#{btnCapNhatDongLai}.hide();" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnEdit_Click">
                                                        <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnDelete" runat="server" Disabled="true" Text="Xóa" Icon="Delete">
                                                <DirectEvents>
                                                    <Click OnEvent="btnDelete_Click">
                                                        <Confirmation Message="Bạn có chắc chắn muốn xóa không ?" Title="Cảnh báo" ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" Msg="Đang xóa dữ liệu" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDownloadAttachFile" Text="Tải tệp tin đính kèm"
                                                Hidden="true" Icon="ArrowDown">
                                                <Listeners>
                                                    <Click Handler="if (hdfRecordId.getValue() == '') {alert('Bạn chưa chọn quyết định lương nào'); return false;}" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnDownloadAttachFile_Click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server" ID="tbs" />
                            
                                            <ext:Button ID="btnCauHinh" runat="server" Text="Cấu hình" Icon="Cog">
                                                <Listeners>
                                                    <Click Handler="wdConfigGridPanel.show();" />
                                                </Listeners>
                                            </ext:Button>
                                  
                                            <ext:ToolbarFill runat="server" />
                                            <ext:TriggerField runat="server" Width="200" EnableKeyEvents="true" EmptyText="Nhập tên hoặc mã cán bộ"
                                                ID="txtSearchKey">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="enterKeyPressHandler" />
                                                    <KeyUp Handler="if (txtSearchKey.getValue() != '') this.triggers[0].show();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); PagingToolbar1.pageIndex = 0; PagingToolbar1.pageIndex = 0; #{Store1}.reload(); }" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button Text="Tìm kiếm" Icon="Zoom" runat="server" ID="btnSearch">
                                                <Listeners>
                                                    <Click Handler="PagingToolbar1.pageIndex = 0; PagingToolbar1.pageIndex = 0; #{Store1}.reload();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                        <ext:Column ColumnID="TepTinDinhKem" Width="25" DataIndex="" Align="Center">
                                            <Commands>
                                                <ext:ImageCommand CommandName="Download" IconCls="Download" Style="margin: 0 !important;">
                                                    <ToolTip Text="Tải tệp tin đính kèm" />
                                                </ext:ImageCommand>
                                            </Commands>
                                            <PrepareCommand Fn="prepare" />
                                        </ext:Column>
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã CBCC" Width="100" DataIndex="EmployeeCode" />
                                        <ext:GroupingSummaryColumn ColumnID="FullName" DataIndex="FullName" Header="Họ tên" Width="200" Sortable="true" Hideable="false" SummaryType="Count">
                                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Quyết định)' : '(1 Quyết định)');" />
                                        </ext:GroupingSummaryColumn>
                                        <ext:Column ColumnID="GioiTinh" Width="100" Align="Center" Header="Giới tính" DataIndex="Sex">
                                            <Renderer Fn="RenderSex" />
                                        </ext:Column>
                                        <ext:DateColumn ColumnID="NgaySinh" Format="dd/MM/yyyy" Width="100" Header="Ngày sinh"
                                            DataIndex="BirthDate">
                                        </ext:DateColumn>
                                        <ext:Column ColumnID="TenBoPhan" Width="180" Header="Bộ phận" DataIndex="DepartmentName">
                                        </ext:Column>
                                         <ext:Column ColumnID="QDL_LUONGCUNG" Width="150" Align="Right" Header="Mức lương"
                                            DataIndex="SalaryBasic">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_HESOLUONG" Width="150" Align="Right" Header="Hệ số lương"
                                            DataIndex="SalaryFactor">
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_NGAYHETHIEULUC" Width="150" Align="Left" Header="Phụ cấp chức vụ"
                                            DataIndex="PositionAllowance">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_PHANTRAMHL" Width="150" Align="Right" Header="Phụ cấp khác"
                                            DataIndex="OtherAllowance">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_LUONGDONGBHXH" Width="150" Align="Right" Header="Lương đóng BHXH"
                                            DataIndex="SalaryInsurance">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_NgachLuong" Width="120" Align="Left" Header="Ngạch lương"
                                            DataIndex="QuantumName">
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_BACLUONG" Width="120" Align="Right" Header="Bậc lương"
                                            DataIndex="SalaryGrade">
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_BACLUONGNB" Width="150" Align="Right" Header="Bậc lương NB"
                                            DataIndex="SalaryGradeLift">
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_SOQD" Width="120" Align="Left" Header="Số quyết định" DataIndex="DecisionNumber">
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_NGAYQD" Width="150" Align="Left" Header="Ngày quyết định"
                                            DataIndex="DecisionDate">
                                            <Renderer Fn="RenderDate" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_NGAYHIEULUC" Width="150" Align="Left" Header="Ngày hiệu lực"
                                            DataIndex="EffectiveDate">
                                            <Renderer Fn="RenderDate" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_NGAYHL" Width="150" Align="Left" Header="Ngày hưởng lương"
                                            DataIndex="SalaryPayDate">
                                            <Renderer Fn="RenderDate" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_VUOTKHUNG" Width="120" Align="Left" Header="Vượt khung"
                                            DataIndex="OutFrame">
                                            <Renderer Fn="renderPercentage" />
                                        </ext:Column>
                                        <ext:Column ColumnID="QDL_NGUOIQD" Width="150" Align="Left" Header="Người quyết định"
                                            DataIndex="SignerName">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                        <Listeners>
                                            <RowSelect Handler="try{btnEdit.enable();}catch(e){} try{btnDelete.enable();}catch(e){} try{btnAddPhuCap.enable();}catch(e){}
                                                            hdfRecordId.setValue(RowSelectionModel1.getSelected().id);" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="true" />
                                <Listeners>
                                    <ViewReady Handler="try{cbxLoaiLuongCu_Store.reload();}catch(e){} 
                                    try{cbxQuantumStore.reload();}catch(e){}" />
                                    <Command Handler="Ext.net.DirectMethods.DownloadAttach(record.data.AttachFileName);" />
                                </Listeners>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="15">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Page size:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                </Items>
                                                <SelectedItem Value="10" />
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
        </div>
    </form>
</body>
</html>
