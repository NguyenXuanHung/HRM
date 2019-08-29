<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.Management.RewardDisciplineManagement" CodeBehind="RewardDisciplineManagement.aspx.cs" %>

<!DOCTYPE html>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript" src="/Resource/js/Extcommon.js"></script>
    <script type="text/javascript">
        var CheckInputKhenThuong = function (el) {
            if (cbHinhThucKhenThuong.getValue().trim() == '') {
                if (hdfTypePage.getValue() == "KhenThuong") {
                    alert('Bạn chưa chọn hình thức khen thưởng');
                } else {
                    alert('Bạn chưa chọn hình thức kỷ luật');
                }
                
                return false;
            }
            if (cbLyDoKhenThuong.getValue().trim() == '' && cbLyDoKhenThuong.getRawValue().trim() == '') {
                if (hdfTypePage.getValue() == "KhenThuong") {
                    alert('Bạn chưa chọn lý do khen thưởng');
                } else {
                    alert('Bạn chưa chọn lý do kỷ luật');
                }
               
                return false;
            }
            if (hdfCapKhenThuongKyLuat.getValue() == '') {
                if (hdfTypePage.getValue() == "KhenThuong") {
                    alert('Bạn chưa chọn cấp khen thưởng');
                } else {
                    alert('Bạn chưa chọn cấp kỷ luật');
                }
               
                return false;
            }
            if (ValidateDateField(dfKhenThuongNgayQuyetDinh) == false) {
                alert('Định dạng ngày quyết định không đúng');
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
      
        var CheckInputMany = function (el) {
            if (hdfHinhThucHangLoat.getValue() === '' || hdfHinhThucHangLoat.getValue() === null) {
                if (hdfTypePage.getValue() == "KhenThuong") {
                    alert('Bạn chưa chọn hình thức khen thưởng');
                } else {
                    alert('Bạn chưa chọn hình thức kỷ luật');
                }
                return false;
            }
            
            if (hdfLyDoTempl.getValue() === '' || hdfLyDoTempl.getValue() === null) {
                if (hdfTypePage.getValue() == "KhenThuong") {
                    alert('Bạn chưa chọn lý do khen thưởng');
                } else {
                    alert('Bạn chưa chọn lý do kỷ luật');
                }
                return false;
            }
            
            if (hdfLevelManyId.getValue() === '' || hdfLevelManyId.getValue() === null) {
                if (hdfTypePage.getValue() == "KhenThuong") {
                    alert('Bạn chưa chọn cấp khen thưởng');
                } else {
                    alert('Bạn chưa chọn cấp kỷ luật');
                }
             
                return false;
            }
            if (ValidateDateField(dfNgayQuyetDinhHangLoat) == false) {
                alert('Định dạng ngày quyết định không đúng');
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

            if (gpListEmployee.getSelectionModel().getCount() === 0 && gpListEmployee.disabled === false) {
                alert("Bạn phải chọn ít nhất một cán bộ !");
                return false;
            }

            return true;
        }
        var searchBoxKT = function (f, e) {
            hdfLyDoKTTemp.setValue(cbLyDoKhenThuong.getRawValue());
            if (hdfIsDanhMuc.getValue() == '1') {
                hdfIsDanhMuc.setValue('2');
            }
            if (cbLyDoKhenThuong.getRawValue() == '') {
                hdfIsDanhMuc.reset();
            }
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

        var onKeyUp = function (field) {
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
            <ext:Hidden runat="server" ID="hdfTypePage" />
            <ext:Hidden runat="server" ID="hdfTableLyDo" />
            <ext:Hidden runat="server" ID="hdfValueLyDo" />
            <ext:Hidden runat="server" ID="hdfDisplayLyDo" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfAddMore" Text="0" />
            <ext:Hidden runat="server" ID="hdfTitleReport" />
            <ext:Hidden runat="server" ID="hdfTypeReport" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />

            <ext:Store ID="cbHinhThucKhenThuongStore" runat="server" AutoLoad="false" OnRefreshData="cbHinhThucKhenThuongStore_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeLevelMany" AutoSave="false" ShowWarningOnFailure="false"
                SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="storeLevelMany_OnRefreshData"
                runat="server">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="cbLyDoKhenThuongStore" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Category" />
                    <ext:Parameter Name="Table" Value="hdfTableLyDo.getValue()" Mode="Raw" />
                    <ext:Parameter Name="Id" Value="hdfValueLyDo.getValue()" Mode="Raw" />
                    <ext:Parameter Name="Name" Value="hdfDisplayLyDo.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="plants" TotalProperty="total">
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
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Category" />
                    <ext:Parameter Name="table" Value="cat_Position" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="plants" TotalProperty="total" IDProperty="Id">
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
                            <ext:GridPanel ID="grp_KhenThuongKyLuat" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdKhenThuongKyLuatHangLoat.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowEditRewardOrDiscipline">
                                                        <EventMask ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button runat="server" Text="Báo cáo" ID="btnPrint" Icon="Printer">
                                                <Listeners>
                                                    <Click Handler="wdShowReport.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:DateField ID="dfNgayBatDau" runat="server" Width="140" EmptyText="Ngày quyết định từ ngày"
                                                AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="#{dfNgayKetThuc}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfNgayKetThuc.setMinValue(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();}" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:DateField ID="dfNgayKetThuc" runat="server" EmptyText="Ngày quyết định đến ngày"
                                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Vtype="daterange" Width="140"
                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày hạn nộp hồ sơ không đúng">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="#{dfNgayBatDau}" Mode="Value"></ext:ConfigItem>
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                    <Select Handler="this.triggers[0].show(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); dfNgayBatDau.setMaxValue(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();}" />
                                                </Listeners>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="240" EmptyText="Nhập mã, họ tên CCVC lý do hoặc ghi chú">
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
                                    <ext:Store runat="server" ID="stKhenThuongKyLuat">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="Reward" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="TypePage" Value="hdfTypePage.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="TuNgay" Value="dfNgayBatDau.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="DenNgay" Value="dfNgayKetThuc.getRawValue()" Mode="Raw" />
                                            <ext:Parameter Name="Cap" Value="cbbCapKTKLFilter.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="HinhThuc" Value="cbbHinhThucFilter.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="plants" TotalProperty="total">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="LevelName" />
                                                    <ext:RecordField Name="DecisionNumber" />
                                                    <ext:RecordField Name="DecisionDate" />
                                                    <ext:RecordField Name="EffectiveDate" />
                                                    <ext:RecordField Name="Reason" />
                                                    <ext:RecordField Name="FormName" />
                                                    <ext:RecordField Name="Note" />
                                                    <ext:RecordField Name="DecisionMaker" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="ParentDepartmentName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" Locked="true" />
                                        <ext:Column Header="Mã CCVC" Width="85" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ tên" Width="150" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column Header="Phòng ban" Width="250" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column Header="Đơn vị quản lý" Width="250" Align="Left" DataIndex="ParentDepartmentName" />
                                        <ext:Column Header="Cấp khen thưởng kỷ luật" Width="150" Align="Left" DataIndex="LevelName" />
                                        <ext:Column Header="Số quyết định" Width="100" Align="Left" DataIndex="DecisionNumber" />
                                        <ext:DateColumn Header="Ngày quyết định" Width="110" Align="Center" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Hình thức" Width="200" Align="Left" DataIndex="FormName" />
                                        <ext:DateColumn Header="Ngày hiệu lực" Width="110" Align="Center" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Người quyết định" Width="120" Align="Left" DataIndex="DecisionMaker" />
                                        <ext:Column Header="Lý do" Width="220" Align="Left" DataIndex="Reason" />
                                        <ext:Column Header="Ghi chú" Width="200" Align="Left" DataIndex="Note" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1">
                                        <HeaderRows>
                                            <ext:HeaderRow>
                                                <Columns>
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="true">
                                                        <Component>
                                                            <ext:ComboBox runat="server" ID="cbbCapKTKLFilter" DisplayField="Name"
                                                                ValueField="Id" AnchorHorizontal="100%" ListWidth="200" Editable="false" ItemSelector="div.list-item" StoreID="storeLevelMany"
                                                                LoadingText="Đang tải dữ liệu...">
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
                                                                    <Expand Handler="#{cbbCapKTKLFilter}.store.reload();" />
                                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); 
                                                                        if (cbbCapKTKLFilter.getValue() == '-1') {$('#cbbCapKTKLFilter').removeClass('combo-selected');}
                                                                        else {$('#cbbCapKTKLFilter').addClass('combo-selected');}"></Select>
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();
                                                                                            #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();
                                                                                            $('#cbbCapKTKLFilter').removeClass('combo-selected');}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="false" />
                                                    <ext:HeaderColumn AutoWidthElement="true">
                                                        <Component>
                                                            <ext:ComboBox runat="server" ID="cbbHinhThucFilter" DisplayField="Name" ValueField="Id"
                                                                AnchorHorizontal="100%" ListWidth="200" Width="200" Editable="false" ItemSelector="div.list-item" StoreID="cbHinhThucKhenThuongStore">
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
                                                                    <Expand Handler="#{cbHinhThucKhenThuongStore}.reload();" />
                                                                    <Select Handler="this.triggers[0].show();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); 
                                                                        if (cbbHinhThucFilter.getValue() == '-1') {$('#cbbHinhThucFilter').removeClass('combo-selected');}
                                                                        else {$('#cbbHinhThucFilter').addClass('combo-selected');}" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();
                                                                                            #{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();
                                                                                            $('#cbbHinhThucFilter').removeClass('combo-selected');}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </Component>
                                                    </ext:HeaderColumn>
                                                </Columns>
                                            </ext:HeaderRow>
                                        </HeaderRows>
                                    </ext:LockingGridView>
                                </View>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().id);btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="hdfRecordId.reset();btnEdit.disable();btnDelete.disable();" />
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
            <ext:Window ID="wdKhenThuong" AutoHeight="true" Width="550" runat="server" Padding="6"
                EnableViewState="false" Layout="FormLayout" Modal="true" Hidden="true" Constrain="true" LabelWidth="120"
                Icon="Pencil" Title="Khen thưởng" Resizable="false">
                <Items>
                    <ext:Container ID="Container46" runat="server" Layout="ColumnLayout" Height="53">
                        <Items>
                            <ext:Container ID="Container47" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="120"
                                ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" FieldLabel="Số quyết định" ID="txtKhenThuongSoQuyetDinh"
                                        AnchorHorizontal="98%" TabIndex="1" MaxLength="20" />
                                    <ext:Hidden runat="server" ID="hdfKhenThuongNguoiQD" />

                                    <ext:TextField runat="server" ID="tgf_KhenThuong_NguoiRaQD" FieldLabel="Người quyết định" TabIndex="3"
                                        AnchorHorizontal="98%" Editable="false" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container48" runat="server" LabelAlign="left" Layout="FormLayout"
                                LabelWidth="100" ColumnWidth="0.5">
                                <Items>

                                    <ext:DateField runat="server" FieldLabel="Ngày quyết định" ID="dfKhenThuongNgayQuyetDinh"
                                        AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                        RegexText="Định dạng ngày sinh không đúng" />
                                    <ext:DateField runat="server" FieldLabel="Ngày hiệu lực" ID="dfKhenThuongNgayHieuLuc"
                                        AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                        RegexText="Định dạng ngày sinh không đúng" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:DateField runat="server" FieldLabel="Thời hạn kỷ luật" ID="dfUpdateExpireDate" Hidden="true"
                        AnchorHorizontal="50%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                        RegexText="Định dạng Thời hạn kỷ luật không đúng" />
                    <ext:Hidden runat="server" ID="hdfIsDanhMuc" Text="0" />
                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                    <ext:Hidden runat="server" ID="hdfMaLyDoKhenThuong" />
                    <ext:Hidden runat="server" ID="hdfLyDoKTTemp" />
                    <ext:Hidden runat="server" ID="hdfHinhThucKhenThuongKyLuat" />
                    <ext:ComboBox runat="server" ID="cbHinhThucKhenThuong" DisplayField="Name"
                        FieldLabel="Hình thức<span style='color:red;'>*</span>" ValueField="Id"
                        AnchorHorizontal="99%" TabIndex="2" Editable="false" ItemSelector="div.list-item" StoreID="cbHinhThucKhenThuongStore"
                        CtCls="requiredData">
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
                            <Expand Handler="if(#{cbHinhThucKhenThuong}.store.getCount()==0){#{cbHinhThucKhenThuongStore}.reload();}" />
                            <Select Handler="this.triggers[0].show(); hdfHinhThucKhenThuongKyLuat.setValue(this.getValue())" />
                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cbLyDoKhenThuong" DisplayField="Name" FieldLabel="Lý do<span style='color:red;'>*</span>"
                        ValueField="Id" AnchorHorizontal="99%" TabIndex="5" Editable="true" ItemSelector="div.list-item"
                        MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..." CtCls="requiredData" StoreID="cbLyDoKhenThuongStore"
                        EnableKeyEvents="true">
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
                            <Expand Handler="if(#{cbLyDoKhenThuong}.store.getCount()==0){#{cbLyDoKhenThuongStore}.reload();}" />
                            <Select Handler="this.triggers[0].show(); #{hdfMaLyDoKhenThuong}.setValue(#{cbLyDoKhenThuong}.getValue());
                                        #{hdfIsDanhMuc}.setValue('1');
                                        #{hdfLyDoKTTemp}.setValue(#{cbLyDoKhenThuong}.getRawValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfIsDanhMuc}.reset(); }" />
                            <KeyUp Fn="searchBoxKT" />
                            <Blur Handler="#{cbLyDoKhenThuong}.setRawValue(#{hdfLyDoKTTemp}.getValue());
                                    if (#{hdfIsDanhMuc}.getValue() != '1') {#{cbLyDoKhenThuong}.setValue(#{hdfLyDoKTTemp}.getValue());}" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Hidden runat="server" ID="hdfCapKhenThuongKyLuat" />
                    <ext:ComboBox runat="server" ID="cbxCapKhenThuong" DisplayField="Name" FieldLabel="Cấp khen thưởng - kỷ luật" TabIndex="6"
                        ValueField="Id" AnchorHorizontal="99%" Editable="false" ItemSelector="div.list-item" StoreID="storeLevelMany"
                        LoadingText="Đang tải dữ liệu..." CtCls="requiredData">
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
                            <Expand Handler="#{cbxCapKhenThuong}.store.reload();" />
                            <Select Handler="this.triggers[0].show(); hdfCapKhenThuongKyLuat.setValue(this.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfCapKhenThuongKyLuat.reset(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtUpdateSourceDepartment" FieldLabel="Cơ quan khen thưởng - kỷ luật" AnchorHorizontal="100%" />
                    <ext:NumberField runat="server" ID="txtUpdateMoneyAmount" FieldLabel="Số tiền thưởng" MaskRe="/[0-9.,]/" AnchorHorizontal="50%" Hidden="true" />
                    <ext:Hidden runat="server" ID="hdfIsUpdateMakerPosition" Text="0" />
                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                    <ext:Hidden runat="server" ID="hdfUpdateMakerTempPosition" />
                    <ext:Hidden runat="server" ID="hdfUpdateMakerPosition" />
                    <ext:ComboBox runat="server" ID="cbxUpdateMakerPosition" DisplayField="Name" FieldLabel="Chức vụ người ký"
                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                        EnableKeyEvents="true" StoreID="storePosition">
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
                            <Select Handler="this.triggers[0].show();  hdfUpdateMakerPosition.setValue(cbxUpdateMakerPosition.getValue());
				                                    hdfIsUpdateMakerPosition.setValue('1');
				                                    hdfUpdateMakerTempPosition.setValue(cbxUpdateMakerPosition.getRawValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsUpdateMakerPosition.reset();hdfMakerTempPosition.reset();hdfUpdateMakerPosition.reset();  }" />
                            <KeyUp Fn="searchBoxPositionUpdate" />
                            <Blur Handler="cbxUpdateMakerPosition.setRawValue(hdfUpdateMakerTempPosition.getValue());
			                                        if (hdfIsUpdateMakerPosition.getValue() != '1') {cbxUpdateMakerPosition.setValue(hdfUpdateMakerTempPosition.getValue());}" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Hidden runat="server" ID="hdfKhenThuongTepTinDinhKem" />
                    <ext:CompositeField ID="CompositeField5" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
                        <Items>
                            <ext:FileUploadField ID="fufKhenThuongTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin" TabIndex="7"
                                ButtonText="" Icon="Attach" Width="338">
                            </ext:FileUploadField>
                            <ext:Button runat="server" ID="btnKhenThuongDownload" Icon="ArrowDown" ToolTip="Tải về">
                                <DirectEvents>
                                    <Click OnEvent="btnKhenThuongDownload_Click" IsUpload="true" />
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnKhenThuongDelete" Icon="Delete" ToolTip="Xóa">
                                <DirectEvents>
                                    <Click OnEvent="btnKhenThuongDelete_Click">
                                        <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                            ConfirmRequest="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:CompositeField>
                    <ext:TextArea ID="txtKhenThuongGhiChu" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="99%"
                        TabIndex="8" MaxLength="1000" />
                </Items>
                <Buttons>
                    <ext:Button ID="btnUpdateKhenThuong" runat="server" Text="Cập nhật" Icon="Disk" Hidden="true">
                        <Listeners>
                            <Click Handler="return CheckInputKhenThuong(#{fufKhenThuongTepTinDinhKem}.fileInput.dom);" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnEditKhenThuong" runat="server" Hidden="false" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button24" runat="server" Text="Cập nhật & Đóng lại" Icon="Disk" Hidden="true">
                        <Listeners>
                            <Click Handler="return CheckInputKhenThuong(#{fufKhenThuongTepTinDinhKem}.fileInput.dom);" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="Button25" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdKhenThuong}.hide(); Ext.net.DirectMethods.ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetWindowTitle();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="wdKhenThuongKyLuatHangLoat" Constrain="true" Modal="true"
                Title="Thêm cán bộ khen thưởng - kỷ luật hàng loạt" Icon="UserAdd" Layout="FormLayout"
                AutoHeight="true" Width="700" Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container runat="server" ID="ctn30" Layout="FormLayout" AutoHeight="true" AnchorHorizontal="100%" LabelWidth="120">
                        <Items>
                            <ext:FormPanel runat="server" ID="fp1" Frame="true" AnchorHorizontal="100%" Title="Thông tin" LabelWidth="160"
                                Icon="BookOpenMark" Layout="FormLayout">
                                <Items>
                                    <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="53">
                                        <Items>
                                            <ext:Container ID="Container2" runat="server" LabelAlign="left" Layout="FormLayout" LabelWidth="160"
                                                ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TextField runat="server" FieldLabel="Số quyết định" ID="txtSoQuyetDinhHangLoat"
                                                        AnchorHorizontal="98%" TabIndex="1" MaxLength="20" />
                                                    <ext:Hidden runat="server" ID="hdfNguoiQuyetDinhHangLoat" />

                                                    <ext:TextField runat="server" ID="tgfNguoiQuyetDinhHangLoat" FieldLabel="Người quyết định" TabIndex="3"
                                                        AnchorHorizontal="98%" Editable="false" />

                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container3" runat="server" LabelAlign="left" Layout="FormLayout"
                                                LabelWidth="160" ColumnWidth="0.5">
                                                <Items>

                                                    <ext:DateField runat="server" FieldLabel="Ngày quyết định" ID="dfNgayQuyetDinhHangLoat"
                                                        AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                        RegexText="Định dạng ngày sinh không đúng" />
                                                    <ext:DateField runat="server" FieldLabel="Ngày hiệu lực" ID="dfNgayHieuLuc"
                                                        AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                        RegexText="Định dạng ngày hiệu lực không đúng" />
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:DateField runat="server" FieldLabel="Thời hạn kỷ luật" ID="dfExpireDate" Hidden="true"
                                        AnchorHorizontal="50%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                        RegexText="Định dạng Thời hạn kỷ luật không đúng" />
                                    <ext:Hidden runat="server" ID="hdfIsDanhMuc2" Text="0" />
                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                    <ext:Hidden runat="server" ID="hdfLyDoTempl" />
                                    <ext:Hidden runat="server" ID="hdfLyDoHangLoat" />
                                    <ext:Hidden runat="server" ID="hdfHinhThucHangLoat" />
                                    <ext:ComboBox runat="server" ID="cbbHinhThucHangLoat" DisplayField="Name"
                                        FieldLabel="Hình thức<span style='color:red;'>*</span>" ValueField="Id"
                                        AnchorHorizontal="99%" TabIndex="2" Editable="false" ItemSelector="div.list-item" StoreID="cbHinhThucKhenThuongStore"
                                        CtCls="requiredData">
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
                                            <Expand Handler="#{cbHinhThucKhenThuongStore}.reload();" />
                                            <Select Handler="this.triggers[0].show(); hdfHinhThucHangLoat.setValue(this.getValue())" />
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfHinhThucHangLoat.reset();}" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:ComboBox runat="server" ID="cbbLyDoHangLoat" DisplayField="Name" FieldLabel="Lý do<span style='color:red;'>*</span>"
                                        ValueField="Id" AnchorHorizontal="99%" TabIndex="5" Editable="true" ItemSelector="div.list-item"
                                        MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..." CtCls="requiredData" StoreID="cbLyDoKhenThuongStore"
                                        EnableKeyEvents="true">
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
                                            <Expand Handler="#{cbLyDoKhenThuongStore}.reload();" />
                                            <Select Handler="this.triggers[0].show(); #{hdfLyDoHangLoat}.setValue(#{cbbLyDoHangLoat}.getValue());
                                                        #{hdfIsDanhMuc2}.setValue('1');
                                                         #{hdfLyDoTempl}.setValue(#{cbbLyDoHangLoat}.getRawValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfIsDanhMuc2}.reset(); }" />
                                            <KeyUp Fn="searchBoxKT2" />
                                            <Blur Handler="#{cbbLyDoHangLoat}.setRawValue(#{hdfLyDoTempl}.getValue());
                                                    if (#{hdfIsDanhMuc2}.getValue() != '1') {#{cbbLyDoHangLoat}.setValue(#{hdfLyDoTempl}.getValue());}" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:Hidden runat="server" ID="hdfLevelManyId" />
                                    <ext:ComboBox runat="server" ID="cboLevelMany" DisplayField="Name" FieldLabel="Cấp khen thưởng - kỷ luật"
                                        ValueField="Id" AnchorHorizontal="99%" Editable="false" ItemSelector="div.list-item" StoreID="storeLevelMany" TabIndex="6"
                                        LoadingText="Đang tải dữ liệu..." CtCls="requiredData">
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
                                            <Expand Handler="storeLevelMany.reload();" />
                                            <Select Handler="this.triggers[0].show(); hdfLevelManyId.setValue(cboLevelMany.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfLevelManyId.reset();}" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtSourceDepartment" FieldLabel="Cơ quan khen thưởng - kỷ luật" AnchorHorizontal="100%" />
                                    <ext:NumberField runat="server" ID="txtMoneyAmount" FieldLabel="Số tiền thưởng" MaskRe="/[0-9.,]/" AnchorHorizontal="50%" Hidden="true" />
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
                                            <Select Handler="this.triggers[0].show();  hdfMakerPosition.setValue(cbxMakerPosition.getValue());
				                                                    hdfIsMakerPosition.setValue('1');
				                                                    hdfMakerTempPosition.setValue(cbxMakerPosition.getRawValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsMakerPosition.reset();hdfMakerTempPosition.reset();hdfMakerPosition.reset();  }" />
                                            <KeyUp Fn="searchBoxPosition" />
                                            <Blur Handler="cbxMakerPosition.setRawValue(hdfMakerTempPosition.getValue());
			                                                        if (hdfIsMakerPosition.getValue() != '1') {cbxMakerPosition.setValue(hdfMakerTempPosition.getValue());}" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:Hidden runat="server" ID="hdfTepDinhKemHangLoat" />
                                    <ext:CompositeField ID="CompositeField1" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
                                        <Items>
                                            <ext:FileUploadField ID="fufTepTinHangLoat" runat="server" EmptyText="Chọn tệp tin" TabIndex="7"
                                                ButtonText="" Icon="Attach" Width="348">
                                            </ext:FileUploadField>

                                            <ext:Button runat="server" ID="btnDeleteFile" Icon="Delete" ToolTip="Xóa" Disabled="false">
                                                <Listeners>
                                                    <Click Handler="hdfTepDinhKemHangLoat.reset(); fufTepTinHangLoat.reset();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:CompositeField>
                                    <ext:TextArea ID="txtGhuChuHangLoat" runat="server" FieldLabel="Ghi chú" AnchorHorizontal="99%"
                                        TabIndex="8" MaxLength="1000" />
                                </Items>
                            </ext:FormPanel>
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
                            <Click Handler="return CheckInputMany(#{fufTepTinHangLoat}.fileInput.dom);" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Create" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>


                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLaiHL" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdKhenThuongKyLuatHangLoat.hide(); Ext.net.DirectMethods.ResetForm();chkEmployeeRowSelection.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>

                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetWindowTitle();chkEmployeeRowSelection.clearSelections();" />
                </Listeners>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wdShowReport"
                Title="Báo cáo" Maximized="true" Icon="Printer">
                <Items>
                    <ext:TabPanel ID="pnReportPanel" Region="Center" AnchorVertical="100%" Border="false"
                        runat="server">
                    </ext:TabPanel>
                </Items>
                <Listeners>
                    <BeforeShow Handler="pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../../Report/ReportView.aspx?rp=' + hdfTypeReport.getValue() + '&filter=' + dfNgayBatDau.getRawValue() + ',' + dfNgayKetThuc.getRawValue(), hdfTitleReport.getValue());" />
                </Listeners>
                <Buttons>
                    <ext:Button ID="Button5" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdShowReport}.hide(); " />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
