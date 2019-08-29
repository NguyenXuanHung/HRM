<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.GoAboard" Codebehind="GoAboard.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .x-grid3-cell-inner 
        {
            white-space:normal !important;
        }
    </style>
    <script src="../../../Resource/js/Extcommon.js"></script>
    <script>
        var keyPressTxtSearch = function (f, e) {
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
        var addRecord = function (id, employeeCode, fullName, departmentName) {
            var rowindex = getSelectedIndexRow();
            window.grp_DanhSachCanBo.insertRecord(rowindex, {
                Id: id,
                EmployeeCode: employeeCode,
                FullName: fullName,
                DepartmentName: departmentName
            });
            window.grp_DanhSachCanBo.getView().refresh();
            window.grp_DanhSachCanBo.getSelectionModel().selectRow(rowindex);
        }
        var getSelectedIndexRow = function () {
            var record = window.grp_DanhSachCanBo.getSelectionModel().getSelected();
            var index = window.grp_DanhSachCanBo.store.indexOf(record);
            if (index === -1)
                return 0;
            return index;
        }
        var RenderDate = function (value) {
            try {
                if (value == null) return "";
                // kiểm tra có phải kiểu ngày tháng VN ko 
                if (value.indexOf("SA") !== -1 || value.indexOf("CH") != -1) {
                    var d = value.split(' ')[0];
                    var year = d.split('/')[2];
                    if (year === "0001" || year === "1900") {
                        return "";
                    }
                    return d;
                }
                //nếu ko phải thì render bình thường
                value = value.replace(" ", "T");
                var temp = value.split("T");
                var date = temp[0].split("-");
                var dateStr = date[2] + "/" + date[1] + "/" + date[0];
                if (date[0] === "1900" || date[0] === "0001") {
                    return "";
                }
                if (true) {
                    return dateStr;
                }
            } catch (e) {

            }
        }
        var checkInputWdDiNuocNgoai = function () {
            if (window.hdfNationId.getValue() == '') {
                alert("Bạn chưa chọn quốc gia");
                window.cbx_quocgia.focus();
                return false;
            }
            if (txtDecisionNumber.getValue() == '' || txtDecisionNumber.getValue().trim == '') {
                alert('Bạn chưa nhập số quyết định!');
                return false;
            }
            if (dfDecisionDate.getValue() == '' || dfDecisionDate.getValue().trim == '' || dfDecisionDate.getValue() == null) {
                alert('Bạn chưa nhập ngày quyết định!');
                return false;
            }
            if (window.dfNgayBatDau.getValue() !== '' && window.dfNgayKetThuc.getValue() !== '' && window.dfNgayBatDau.getValue() > window.dfNgayKetThuc.getValue()) {
                alert("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                window.dfNgayKetThuc.focus();
                return false;
            }
            return true;
        }
        var ReloadGrid = function () {
            window.PagingToolbar1.pageIndex = 0;
            window.PagingToolbar1.doLoad();
            window.RowSelectionModel1.clearSelections();
            window.btnEdit.disable(); window.btnDelete.disable();
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
        var getPrKeyList = function () {
            var jsonDataEncode = "";
            var records = window.grp_DanhSachCanBoStore.getRange();
            for (var i = 0; i < records.length; i++) {
                jsonDataEncode += records[i].data.Id + ",";
            }
            return jsonDataEncode;
        }
        var searchBoxPosition = function (f, e) {
            hdfMakerTempPosition.setValue(cbxMakerPosition.getRawValue());
            if (hdfIsMakerPosition.getValue() == '1') {
                hdfIsMakerPosition.setValue('2');
            }
            if (cbxMakerPosition.getRawValue() == '') {
                hdfIsMakerPosition.reset();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM" />
            <ext:Hidden runat="server" ID="hdfMaDonVi" />
            <ext:Hidden runat="server" ID="hdfMenuID" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfPrKeyHoSo" />
            <ext:Hidden runat="server" ID="hdfUserID" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
        
            <ext:Store runat="server" ID="stHoSoDiNuocNgoai">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={25}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GoAboard"/>

                    <ext:Parameter Name="MaDonVi" Value="hdfMaDonVi.getValue()" Mode="Raw" />
                    <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                    <ext:Parameter Name="QuocGia" Value=" cbx_QuocGiaFilter.getValue()" Mode="Raw" />
                    <ext:Parameter Name="UserID" Value="hdfUserID.getValue()" Mode="Raw" />
                    <ext:Parameter Name="MenuID" Value="hdfMenuID.getValue()" Mode="Raw" />
                    <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                    <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="plants" TotalProperty="total">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="FullName" />
                            <ext:RecordField Name="DepartmentName" />
                            <ext:RecordField Name="ParentDepartmentName" />
                            <ext:RecordField Name="StartDate" />
                            <ext:RecordField Name="EndDate" />
                            <ext:RecordField Name="NationId" />
                            <ext:RecordField Name="Reason" />
                            <ext:RecordField Name="Note" />
                            <ext:RecordField Name="NationName" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="cbx_quocgia_Store" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
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
             <!-- store chức vụ -->
            <ext:Store ID="storePosition" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
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
            <ext:Window ID="wdDiNuocNgoai" AutoHeight="true" Width="550" runat="server" Padding="6"
                EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true"
                Icon="Add" Title="Cán bộ đi nước ngoài" Resizable="false">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandName" />
                    <ext:FieldSet runat="server" Title="Thông tin hồ sơ đi nước ngoài">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfNationId" />
                            <ext:ComboBox runat="server" ID="cbx_quocgia" DisplayField="Name" MinChars="1" ValueField="Id" FieldLabel="Quốc gia<span style='color:red'>*</span>"
                                AnchorHorizontal="100%" Editable="true" ItemSelector="div.list-item" StoreID="cbx_quocgia_Store" CtCls="requiredData">
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
                                    <Select Handler="this.triggers[0].show(); hdfNationId.setValue(cbx_quocgia.getValue());" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; hdfNationId.reset();" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Container runat="server" ID="Container1" Layout="ColumnLayout" Height="27">
                                <Items>
                                    <ext:Container runat="server" ID="Container2" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:DateField ID="dfNgayBatDau" runat="server" FieldLabel="Ngày bắt đầu"
                                                AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfNgayKetThuc}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Container3" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:DateField ID="dfNgayKetThuc" runat="server" FieldLabel="Ngày kết thúc"
                                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfNgayBatDau}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                             <ext:Container runat="server" ID="Container4" Layout="ColumnLayout" Height="27">
                                <Items>
                                    <ext:Container runat="server" ID="Container5" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField ID="txtDecisionNumber" runat="server" CtCls="requiredData" FieldLabel="Số quyết định<span style='color:red'>*</span>" AnchorHorizontal="98%"
                                                MaxLength="200" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Container6" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:DateField runat="server" ID="dfDecisionDate" CtCls="requiredData" FieldLabel="Ngày quyết định<span style='color:red'>*</span>"
                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
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
                            <ext:TextArea runat="server" ID="txtLyDo" AnchorHorizontal="100%" FieldLabel="Lý do" />
                            <ext:TextArea runat="server" ID="txtGhiChu" AnchorHorizontal="100%" FieldLabel="Ghi chú" />
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="200">
                        <Items>
                            <ext:GridPanel runat="server" ID="grp_DanhSachCanBo" TrackMouseOver="true" Title="Danh sách cán bộ"
                                StripeRows="true" Border="true" Region="Center" Icon="User" AutoExpandColumn="DepartmentName"
                                AutoExpandMin="150">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tbDanhSachQD">
                                        <Items>
                                            <ext:Button runat="server" ID="btnChonDanhSachCanBo" Icon="UserAdd" Text="Chọn cán bộ" TabIndex="9">
                                                <Listeners>
                                                    <Click Handler="ucChooseEmployee_wdChooseUser.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnXoaCanBo" Icon="Delete" Text="Xóa" Disabled="true" TabIndex="10">
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
                                            <ext:JsonReader IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
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
                                        <ext:RowNumbererColumn Header="STT" Locked="true" Width="40" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Locked="true" Width="100" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="FullName" Header="Họ tên" Locked="true" Width="200" DataIndex="FullName" />
                                        <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" DataIndex="DepartmentName">
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
                    <ext:Button runat="server" ID="btnInserted" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputWdDiNuocNgoai();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="ListId" Value="getPrKeyList()" Mode="Raw" />
                                    
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    
                    <ext:Button runat="server" ID="Button2" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdDiNuocNgoai.hide(); Ext.net.DirectMethods.ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetForm();" />
                </Listeners>
            </ext:Window>
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server" ID="br">
                        <Center>
                            <ext:GridPanel ID="grp_HoSoDiNuocNgoai" runat="server" Border="false"
                                StripeRows="true" AutoExpandColumn="Note" AutoExpandMin="200" StoreID="stHoSoDiNuocNgoai">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Icon="Add" Text="Thêm">
                                                <Listeners>
                                                    <Click Handler="wdDiNuocNgoai.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Icon="Pencil" Text="Sửa" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowEdit">
                                                        <EventMask ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Icon="Delete" Text="Xóa" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="return CheckSelectedRow(grp_HoSoDiNuocNgoai);" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator />
                                            
                                            <ext:ComboBox runat="server" ID="cbx_QuocGiaFilter" DisplayField="Name" MinChars="1" ValueField="Id" FieldLabel="Lọc theo quốc gia"
                                                Editable="true" ItemSelector="div.list-item" EmptyText="Gõ để tìm kiếm..." StoreID="cbx_quocgia_Store" Width="300" LabelWidth="100">
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
                                                    <Select Handler="this.triggers[0].show(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections(); " />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections(); }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ToolbarFill runat="server" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="240" EmptyText="Nhập mã, tên CCVC, lý do hoặc ghi chú">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPressTxtSearch" />
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
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                        <ext:Column ColumnID="Company" Header="Số hiệu CBCC" Width="85" DataIndex="EmployeeCode" Align="Left" />
                                        <ext:Column Header="Họ tên" Width="170" DataIndex="FullName" Align="Left">
                                        </ext:Column>
                                        <ext:Column Header="Phòng ban" Width="200" DataIndex="DepartmentName" Align="Left" />
                                        <ext:Column Header="Đơn vị quản lý" Width="200" Align="Left" DataIndex="ParentDepartmentName" />
                                        <ext:Column Header="Quốc gia" Width="110" DataIndex="NationName" Align="Left">
                                        </ext:Column>
                                        <ext:DateColumn Header="Ngày bắt đầu" Width="90" DataIndex="StartDate" Format="dd/MM/yyyy" Align="Center">
                                        </ext:DateColumn>
                                        <ext:DateColumn Header="Ngày kết thúc" Width="90" DataIndex="EndDate" Format="dd/MM/yyyy" Align="Center">
                                        </ext:DateColumn>
                                        <ext:Column Header="Lý do" Width="200" DataIndex="Reason" Align="Left">
                                        </ext:Column>
                                        <ext:Column Header="Ghi chú" Width="100" ColumnID="Note" DataIndex="Note" Align="Left">
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfRecordId.reset(); " />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="true" />
                                <Listeners>
                                    <RowDblClick Handler="" />
                                </Listeners>
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
        </div>
    </form>
</body>
</html>
