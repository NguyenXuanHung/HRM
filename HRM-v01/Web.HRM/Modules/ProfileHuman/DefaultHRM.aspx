<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.DefaultHRM" CodeBehind="DefaultHRM.aspx.cs" %>

<%@ Register Src="~/Modules/UC/EmployeeDetail.ascx" TagName="EmployeeDetail" TagPrefix="UC" %>
<%@ Register Src="~/Modules/UC/InputEmployee.ascx" TagName="InputEmployee" TagPrefix="UC" %>
<%@ Register Src="~/Modules/UC/UpdateImageEmployeeSeries.ascx" TagName="CapNhatAnhHangLoat" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="UC" TagName="ResourceCommon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript" src="/Resource/js/Extcommon.js"></script>
    <script type="text/javascript" src="/Resource/js/EmployeeSetting.js"></script>
    <script type="text/javascript">
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                grp_HoSoNhanSu.getSelectionModel().clearSelections();
                hdfRecordId.setValue('');
                PagingToolbar1.doLoad();
            }
        }

        var confirmDelete = function () {
            Ext.Msg.confirm('Thông báo', 'Bạn có chắc chắn muốn xóa dữ liệu này', function (btn) {
                if (btn == "yes") {
                    Ext.net.DirectMethods.DeleteRecord();
                } else {
                    return false;
                } 
            });
        }
    </script>
</head>
<body>
    <form id="frmProfileHumanDefault" runat="server">
        <!-- resource manager & hidden field -->
        <ext:ResourceManager runat="server" ID="RM" />
        <ext:Hidden runat="server" ID="hdfRecordId" />
        <ext:Hidden runat="server" ID="hdfAnhDaiDien" />
        <ext:Hidden runat="server" ID="hdfMaDonVi" />
        <ext:Hidden runat="server" ID="hdfSelectedDepartment" />
        <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfQuery" />
        <ext:Hidden runat="server" ID="hdfIsChuaCoAnh" Text="false" />
        <ext:Hidden runat="server" ID="hdfQueryReport" />
        <ext:Hidden runat="server" ID="hdfMenuID" />
        <ext:Hidden runat="server" ID="hdfEven" />
        <ext:Hidden runat="server" ID="hdfGenerateEmployeeCode"/>
     
        <uc2:CapNhatAnhHangLoat ID="CapNhatAnhHangLoat1" runat="server" />

        <ext:Store ID="store_HoSoNhanSu" AutoSave="True" runat="server">
            <Proxy>
                <ext:HttpProxy Method="GET" Url="~/Services/HandlerRecord.ashx" />
            </Proxy>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="={0}" />
                <ext:Parameter Name="limit" Value="={12}" />
            </AutoLoadParams>
            <BaseParams>
                <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                <ext:Parameter Name="Query" Value="hdfQuery.getValue()" Mode="Raw" />
                <ext:Parameter Name="IsChuaCoAnh" Value="hdfIsChuaCoAnh.getValue()" Mode="Raw" />
            </BaseParams>
            <Reader>
                <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                    <Fields>
                        <ext:RecordField Name="Id" />
                        <ext:RecordField Name="EmployeeCode" />
                        <ext:RecordField Name="FullName" />
                        <ext:RecordField Name="Alias" />
                        <ext:RecordField Name="BirthDate" />
                        <ext:RecordField Name="Age" />
                        <ext:RecordField Name="Sex" />
                        <ext:RecordField Name="MaritalStatusName" />
                        <ext:RecordField Name="DepartmentName" />
                        <ext:RecordField Name="BirthPlace" />
                        <ext:RecordField Name="Hometown" />
                        <ext:RecordField Name="FolkName" />
                        <ext:RecordField Name="ReligionName" />
                        <ext:RecordField Name="PersonalClassName" />
                        <ext:RecordField Name="FamilyClassName" />
                        <ext:RecordField Name="ResidentPlace" />
                        <ext:RecordField Name="Address" />
                        <ext:RecordField Name="PreviousJob" />
                        <ext:RecordField Name="RecruimentDate" />
                        <ext:RecordField Name="RecruimentDepartment" />
                        <ext:RecordField Name="PositionName" />
                        <ext:RecordField Name="JobTitleName" />
                        <ext:RecordField Name="AssignedWork" />
                        <ext:RecordField Name="ImageUrl" />
                        <ext:RecordField Name="QuantumName" />
                        <ext:RecordField Name="QuantumCode" />
                        <ext:RecordField Name="SalaryGrade" />
                        <ext:RecordField Name="SalaryFactor" />
                        <ext:RecordField Name="OtherAllowance" />
                        <ext:RecordField Name="PositionAllowance" />
                        <ext:RecordField Name="EffectiveDate" />
                        <ext:RecordField Name="BasicEducationName" />
                        <ext:RecordField Name="EducationName" />
                        <ext:RecordField Name="PoliticLevelName" />
                        <ext:RecordField Name="ManagementLevelName" />
                        <ext:RecordField Name="LanguageLevelName" />
                        <ext:RecordField Name="ITLevelName" />
                        <ext:RecordField Name="CPVJoinedDate" />
                        <ext:RecordField Name="CPVOfficialJoinedDate" />
                        <ext:RecordField Name="CPVJoinedPlace" />
                        <ext:RecordField Name="CPVCardNumber" />
                        <ext:RecordField Name="CPVPositionName" />
                        <ext:RecordField Name="VYUPositionName" />
                        <ext:RecordField Name="VYUJoinedDate" />
                        <ext:RecordField Name="VYUJoinedPlace" />
                        <ext:RecordField Name="ArmyJoinedDate" />
                        <ext:RecordField Name="ArmyLeftDate" />
                        <ext:RecordField Name="ArmyLevelName" />
                        <ext:RecordField Name="TitleAwarded" />
                        <ext:RecordField Name="Skills" />
                        <ext:RecordField Name="HealthStatusName" />
                        <ext:RecordField Name="BloodGroup" />
                        <ext:RecordField Name="Height" />
                        <ext:RecordField Name="Weight" />
                        <ext:RecordField Name="RankWounded" />
                        <ext:RecordField Name="FamilyPolicyName" />
                        <ext:RecordField Name="IDNumber" />
                        <ext:RecordField Name="IDIssueDate" />
                        <ext:RecordField Name="IDIssuePlaceName" />
                        <ext:RecordField Name="InsuranceNumber" />
                        <ext:RecordField Name="InsuranceIssueDate" />
                        <ext:RecordField Name="PersonalTaxCode" />
                        <ext:RecordField Name="CellPhoneNumber" />
                        <ext:RecordField Name="HomePhoneNumber" />
                        <ext:RecordField Name="WorkPhoneNumber" />
                        <ext:RecordField Name="WorkEmail" />
                        <ext:RecordField Name="PersonalEmail" />
                        <ext:RecordField Name="ContactPersonName" />
                        <ext:RecordField Name="ContactRelation" />
                        <ext:RecordField Name="ContactPhoneNumber" />
                        <ext:RecordField Name="ContactAddress" />
                        <ext:RecordField Name="WorkStatusName" />
                        <ext:RecordField Name="AccountNumber" />
                        <ext:RecordField Name="BankName" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <Load Handler="grp_HoSoNhanSu.getSelectionModel().clearSelections();" />
            </Listeners>
        </ext:Store>
        <ext:Store ID="cbx_phongban_Store" runat="server" AutoLoad="false" OnRefreshData="filterDepartmentStore_OnRefreshData">
            <Reader>
                <ext:JsonReader IDProperty="Id">
                    <Fields>
                        <ext:RecordField Name="Id" />
                        <ext:RecordField Name="Name" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
       
        <ext:Menu ID="RowContextMenu" runat="server">
            <Items>
                <ext:MenuItem ID="mnuAdd" runat="server" Icon="Add" Text="Thêm mới">
                    <Listeners>
                        <Click Handler="ResetForm();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="InitWindowEdit">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="event" Value="Insert" Mode="Value" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuItem ID="mnuEdit" runat="server" Icon="Pencil" Text="Sửa">
                    <Listeners>
                        <Click Handler="#{hdfEven}.setValue('Edit');if(CheckSelectedRecord(#{grp_HoSoNhanSu}, #{store_HoSoNhanSu})){}else{return false;}" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="InitWindowEdit">
                            <EventMask ShowMask="true" />
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuItem ID="mnuDelete" runat="server" Icon="Delete" Text="Xóa">
                    <Listeners>
                        <Click Handler="RemoveItemOnGrid(grp_HoSoNhanSu, store_HoSoNhanSu)" />
                    </Listeners>
                </ext:MenuItem>
            </Items>
        </ext:Menu>
      
        <ext:Panel runat="server" ID="Panel1" Height="220" Hidden="True">
            <Content>
                <uc:InputEmployee ID="inputEmployee" runat="server" OnUserControlClose="inputEmployee_OnUserControlClose" />
            </Content>
        </ext:Panel>
        
        <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wdShowReport"
            Title="Báo cáo hồ sơ cán bộ" Maximized="true" Icon="Printer">
            <Items>
                <ext:Hidden runat="server" ID="hdfTypeReport" />
                <ext:TabPanel ID="pnReportPanel" Region="Center" AnchorVertical="100%" Border="false"
                    runat="server">
                </ext:TabPanel>
            </Items>
            <Listeners>
                <BeforeShow Handler="ShowReportAction();" />
                <Hide Handler="hdfQueryReport.reset();" />
            </Listeners>
            <Buttons>
                <ext:Button ID="Button5" runat="server" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdShowReport}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window runat="server" Title="Nhập dữ liệu từ file excel" Resizable="true" Layout="FormLayout"
            Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdImportExcelFile"
            Modal="true" Constrain="true" Height="200">
            <Items>
                <ext:Container runat="server" Layout="FormLayout" LabelWidth="150" ID="ctl1121">
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
                            CtCls="requiredData" AnchorHorizontal="100%" Icon="Attach">
                            <DirectEvents>
                                <FileSelected OnEvent="fileAttachment_FileSelected" />
                            </DirectEvents>
                        </ext:FileUploadField>
                        <ext:TextField runat="server" ID="txtSheetName" FieldLabel="Tên sheet Excel" AnchorHorizontal="100%" />
                    </Items>
                </ext:Container>
                <ext:GridPanel ID="grpImportExcel" runat="server" Hidden="True" AnchorHorizontal="100%" Height="270"
                    Title="Nhân viên từ file excel">
                    <Store>
                        <ext:Store ID="storeImportExcel" runat="server">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="EmployeeCode" Type="String" />
                                        <ext:RecordField Name="FullName" Type="String" />
                                        <ext:RecordField Name="Sex" Type="String" />
                                        <ext:RecordField Name="BirthDate" Type="String" />
                                        <ext:RecordField Name="DepartmentId" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel4" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="35" Header="STT" />
                            <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" DataIndex="EmployeeCode" Width="100" />
                            <ext:Column ColumnID="FullName" Header="Họ tên" DataIndex="FullName" Width="200" />
                            <ext:Column ColumnID="SexName" Header="Giới tính" DataIndex="Sex" Width="100" />
                            <ext:Column ColumnID="BirthDate" Header="Ngày sinh" DataIndex="BirthDate" Width="100" />
                            <ext:Column ColumnID="DepartmentName" Header="Danh tính phòng ban" DataIndex="DepartmentId" Width="200" />
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
            <Buttons>
                <ext:Button runat="server" ID="btnUpdateImportExcel" Text="Cập nhật" Icon="Disk">
                    <DirectEvents>
                        <Click OnEvent="ImportFile">
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
        </ext:Window>

        <!-- viewport -->
        <ext:Viewport runat="server" ID="vp" HideBorders="True">
            <Items>
                <ext:BorderLayout runat="server" ID="brlayout">
                    <Center>
                        <ext:GridPanel runat="server" ID="grp_HoSoNhanSu" StoreID="store_HoSoNhanSu" Border="false" StripeRows="True" TrackMouseOver="true" AnchorHorizontal="100%"
                            Draggable="false" EnableDragDrop="false" EnableColumnMove="false">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="toolbarFn">
                                    <Items>
                                        <ext:Button ID="btnAddNew" runat="server" Text="Thêm mới" Icon="Add">
                                            <Listeners>
                                                <Click Handler="#{hdfEven}.setValue('Insert');ResetForm();" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="InitWindowEdit">
                                                    <EventMask ShowMask="true" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="event" Value="Insert" Mode="Value" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnEdit" runat="server" Disabled="true" Text="Sửa" Icon="Pencil">
                                            <Listeners>
                                                <Click Handler="#{hdfEven}.setValue('Edit');if(CheckSelectedRecord(#{grp_HoSoNhanSu}, #{store_HoSoNhanSu})){inputEmployee_hdfEven.setValue('Edit');inputEmployee_hdfRecordId.setValue(rowSelection.getSelected().id);
                                                    }else{return false;}" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="InitWindowEdit">
                                                    <EventMask ShowMask="true" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="event" Value="Edit" Mode="Value" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnDelete" runat="server" Text="Xóa" Disabled="true" Icon="Delete">
                                            <Listeners>
                                                <Click Handler="if (hdfRecordId.getValue() == '') {alert ('Bạn chưa chọn bản ghi nào'); return false;}else{ confirmDelete();}" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnBaoCao" Icon="Printer" Text="In Sơ yếu lý lịch ">
                                            <Menu>
                                                <ext:Menu ID="Menu1" runat="server">
                                                    <Items>
                                                        <%--Báo cáo Hồ sơ nhân sự--%>
                                                        <ext:MenuItem runat="server" ID="mnuInfoRecord" Text="1. Sơ yếu lý lịch (2C - 32)" Icon="PrinterGo">
                                                            <Listeners>
                                                                <Click Handler="if (CheckSelectedRecord(#{grp_HoSoNhanSu}, #{store_HoSoNhanSu}) == false) {return;} else {#{hdfTypeReport}.setValue('RecordDetail'); #{wdShowReport}.show();}" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" ID="mnuRecordNew" Text="2. Sơ yếu lý lịch (2C - 1998)" Icon="PrinterGo">
                                                            <Listeners>
                                                                <Click Handler="if (CheckSelectedRecord(#{grp_HoSoNhanSu}, #{store_HoSoNhanSu}) == false) {return;} else {#{hdfTypeReport}.setValue('RecordDetailV2'); #{wdShowReport}.show();}" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:Button>
                                        <ext:Button ID="btnTienIchHoSo" runat="server" Text="Tiện ích" Icon="Build">
                                            <Menu>
                                                <ext:Menu ID="Menu4" runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" ID="mnuXuatExcel" Text="Xuất thông tin ra Excel" Icon="PageExcel">
                                                            <DirectEvents>
                                                                <Click OnEvent="menuExportExcel_Event">
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" ID="mnuImportExcel" Text="Nhập thông tin từ Excel" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Handler="Ext.net.DirectMethods.ResetForm();wdImportExcelFile.show();" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" ID="mnuCapNhatAnhHangLoat" Text="Cập nhật ảnh hàng loạt" Icon="Images">
                                                            <Listeners>
                                                                <Click Handler="CapNhatAnhHangLoat1_wdCapNhatAnhHangLoat.show(); tb.hide();employeeDetail_Toolbar1sdsds.hide();" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:Button>
                                        <ext:ToolbarFill runat="server" ID="tbfill" />
                                        <ext:TriggerField runat="server" Width="250" EnableKeyEvents="true" EmptyText="Nhập tên, mã cán bộ hoặc số CMND" ID="txtSearch">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <KeyPress Fn="enterKeyPressHandler" />
                                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad(); }" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:Button Text="Tìm kiếm" Icon="Zoom" runat="server" ID="btnSearch">
                                            <Listeners>
                                                <Click Handler="PagingToolbar1.pageIndex = 0; PagingToolbar1.doLoad();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <View>
                                <ext:LockingGridView runat="server" ID="lkv" LockText="Cố định cột này lại" UnlockText="Hủy cố định cột">
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <%-- Ngày sinh --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox ID="filterBirthDate" Width="80" runat="server" Editable="false" DisplayField="TEN_NAMSINH" ValueField="NAM_SINH">
                                                            <Store>
                                                                <ext:Store runat="server" ID="filterBirthDateStore" AutoLoad="False" OnRefreshData="filterBirthDateStore_OnRefreshData">
                                                                    <Reader>
                                                                        <ext:JsonReader IDProperty="NAM_SINH">
                                                                            <Fields>
                                                                                <ext:RecordField Name="NAM_SINH" />
                                                                                <ext:RecordField Name="TEN_NAMSINH" />
                                                                            </Fields>
                                                                        </ext:JsonReader>
                                                                    </Reader>
                                                                </ext:Store>
                                                            </Store>
                                                            <Listeners>
                                                                <Expand Handler="filterBirthDateStore.reload();" />
                                                                <Select Handler="#{DirectMethods}.SetValueQuery();
                                                                if (filterBirthDate.getValue() == '-1') {$('#filterBirthDate').removeClass('combo-selected');}
                                                                else {$('#filterBirthDate').addClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Tuổi --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Giới tính --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox ID="filterSex" runat="server" Width="65" SelectedIndex="0">
                                                            <Items>
                                                                <ext:ListItem Text="Tất cả" Value="" />
                                                                <ext:ListItem Text="Nam" Value="M" />
                                                                <ext:ListItem Text="Nữ" Value="F" />
                                                            </Items>
                                                            <Listeners>
                                                                <Select Handler="#{DirectMethods}.SetValueQuery();
                                                                    if (filterSex.getValue() == '') {$('#filterSex').removeClass('combo-selected');}
                                                                    else {$('#filterSex').addClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%--Tình trạng hôn nhân--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterMaritalStatus" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="100" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="storeMaritalStatus" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_MaritalStatus" Mode="Value" />
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
                                                                <Expand Handler="filterMaritalStatus.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterMaritalStatus').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterMaritalStatus').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Đơn vị công tác --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterPhongBan" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" EmptyText="gõ để tìm kiếm" Width="230" Editable="true"
                                                            ListWidth="230" ItemSelector="div.list-item" StoreID="cbx_phongban_Store" LoadingText="Đang tải dữ liệu...">
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
                                                                <Expand Handler="filterPhongBan.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterPhongBan').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterPhongBan').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Nơi sinh --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Quê quán --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Dân tộc --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterFolk" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="90" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxFolk" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_Folk" Mode="Value" />
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
                                                                <Expand Handler="filterFolk.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterFolk').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterFolk').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Tôn giáo --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterReligion" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="90" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxReligion" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_Religion" Mode="Value" />
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
                                                                <Expand Handler="filterReligion.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterReligion').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterReligion').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Thành phần bản thân --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterPersonalClass" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxPersonalClass" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_PersonalClass" Mode="Value" />
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
                                                                <Expand Handler="filterPersonalClass.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterPersonalClass').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterPersonalClass').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Thành phần gia đình --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterFamilyClass" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxFamilyClass" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_FamilyClass" Mode="Value" />
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
                                                                <Expand Handler="filterFamilyClass.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterFamilyClass').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterFamilyClass').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Nơi đăng ký Hộ khẩu thường trú --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Nơi ở hiện nay --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Nghề nghiệp khi được tuyển dụng --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Ngày tuyển dụng--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox ID="filterRecruimentDate" Width="120" runat="server" Editable="false" DisplayField="TEN"
                                                            ValueField="MA">
                                                            <Store>
                                                                <ext:Store runat="server" ID="filterRecruitmentDateStore" AutoLoad="False" OnRefreshData="filterRecruitmentDateStore_Store_OnRefreshData">
                                                                    <Reader>
                                                                        <ext:JsonReader IDProperty="TEN">
                                                                            <Fields>
                                                                                <ext:RecordField Name="TEN" />
                                                                                <ext:RecordField Name="MA" />
                                                                            </Fields>
                                                                        </ext:JsonReader>
                                                                    </Reader>
                                                                </ext:Store>
                                                            </Store>
                                                            <Listeners>
                                                                <Expand Handler="filterRecruitmentDateStore.reload();" />
                                                                <Select Handler="#{DirectMethods}.SetValueQuery();
                                                                if (filterRecruimentDate.getValue() == '-1') {$('#filterRecruimentDate').removeClass('combo-selected');}
                                                                else {$('#filterRecruimentDate').addClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Cơ quan tuyển dụng--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Chức vụ hiện tại --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterPosition" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="180" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store ID="store_CbxPosition" runat="server" AutoLoad="false">
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
                                                            </Store>
                                                            <Listeners>
                                                                <Expand Handler="store_CbxPosition.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterPosition').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterPosition').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Chức danh hiện tại --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterJobTitle" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="180" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store ID="StoreJobTitle" runat="server" AutoLoad="false">
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
                                                            </Store>
                                                            <Listeners>
                                                                <Expand Handler="StoreJobTitle.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterJobTitle').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterJobTitle').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Công việc chính được giao --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Trình độ giáo dục phổ thông --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterBasicEducation" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="100" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                                <ext:Store ID="store_CbxBasicEducation" runat="server" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_BasicEducation" Mode="Value" />
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
                                                            <Listeners>
                                                                <Expand Handler="filterBasicEducation.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterBasicEducation').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterBasicEducation').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Trình độ chuyên môn cao nhất --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterEducation" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="180" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxEducation" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                                <Expand Handler="filterEducation.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterEducation').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterEducation').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Lý luận chính trị--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterPoliticLevel" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxPoliticLevel" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_PoliticLevel" Mode="Value" />
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
                                                                <Expand Handler="filterPoliticLevel.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterPoliticLevel').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterPoliticLevel').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Quản lý Nhà nước--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterManagementLevel" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbx_trinhdoquanly_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_ManagementLevel" Mode="Value" />
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
                                                                <Expand Handler="filterManagementLevel.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterManagementLevel').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterManagementLevel').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Ngoại ngữ--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterLanguageLevel" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="90" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template14" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store runat="server" ID="StoreLanguageLevel" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                            <Listeners>
                                                                <Expand Handler="filterLanguageLevel.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterLanguageLevel').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterLanguageLevel').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Tin học--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterITLevel" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="90" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template15" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbx_ITLevel_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_ITLevel" Mode="Value" />
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
                                                                <Expand Handler="filterITLevel.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterITLevel').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterITLevel').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Ngày vào Đảng--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Ngày chính thức--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Nơi kết nạp--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Số thẻ Đảng--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Chức vụ Đảng--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterCPVPosition" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template16" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbx_CPVPosition_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_CPVPosition" Mode="Value" />
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
                                                                <Expand Handler="filterCPVPosition.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterCPVPosition').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterCPVPosition').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Ngày vào Đoàn--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Nơi kết nạp Đoàn --%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Chức vụ Đoàn--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterVYUPosition" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="StoreVYUPosition" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_VYUPosition" Mode="Value" />
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
                                                                <Expand Handler="filterVYUPosition.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterVYUPosition').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterVYUPosition').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%--Ngày nhập ngũ--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Ngày xuất ngũ--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Cấp bậc quân đội --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterArmyLevel" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template19" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbx_ArmyLevel_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_ArmyLevel" Mode="Value" />
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
                                                                <Expand Handler="filterArmyLevel.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterArmyLevel').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterArmyLevel').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%-- Danh hiệu được phong tặng cao nhất--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Sở trường công tác--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Tình trạng sức khoẻ--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Nhóm máu--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Chiều cao--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Cân nặng--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Là thương binh hạng--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Là con gia đình chính sách--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Số CMND--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Ngày cấp--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Nơi cấp--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Số sổ BHXH--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Ngày cấp sổ--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Mã số thuế cá nhân--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Di động--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:TextField Width="100" ID="filterDiDong" runat="server" EnableKeyEvents="true"
                                                            MaskRe="[0-9|.,]">
                                                            <Listeners>
                                                                <KeyPress Fn="enterKeyPressHandler1" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%--Điện thoại nhà--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Điện thoại cơ quan--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Email nội bộ--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:TextField Width="175" ID="filterEmail" runat="server" EnableKeyEvents="true">
                                                            <Listeners>
                                                                <KeyPress Fn="enterKeyPressHandler1" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%--Email riêng--%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:TextField Width="175" ID="filterEmailRieng" runat="server" EnableKeyEvents="true">
                                                            <Listeners>
                                                                <KeyPress Fn="enterKeyPressHandler1" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <%--Người liên hệ (khi cần)--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Mối quan hệ--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Số điện thoại--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%--Địa chỉ người liên hệ--%>
                                                <ext:HeaderColumn AutoWidthElement="false" />
                                                <%-- Trạng thái làm việc --%>
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:ComboBox runat="server" ID="filterWorkStatus" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" EmptyText="gõ để tìm kiếm" Width="150" Editable="true"
                                                            ListWidth="200" ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu...">
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="StoreWorkStatus" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_WorkStatus" Mode="Value" />
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
                                                                <Expand Handler="filterWorkStatus.store.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{DirectMethods}.SetValueQuery(); $('#filterWorkStatus').addClass('combo-selected');" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{DirectMethods}.SetValueQuery(); 
                                                                $('#filterWorkStatus').removeClass('combo-selected');}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:LockingGridView>
                            </View>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn Locked="True" Width="35" Header="STT" />
                                    <ext:Column ColumnID="EmployeeCode" Width="70" Locked="True" Header="Số hiệu CBCC" DataIndex="EmployeeCode">
                                        <Renderer Fn="RenderHightLight" />
                                    </ext:Column>
                                    <ext:Column ColumnID="FullName" Width="150" Locked="True" Header="Họ và tên khai sinh" DataIndex="FullName">
                                        <Renderer Fn="RenderHightLight" />
                                    </ext:Column>
                                    <ext:DateColumn ColumnID="BirthDate" Width="80" Header="Ngày sinh" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                    <ext:Column ColumnID="Age" DataIndex="Age" Width="50" Align="Right" Header="Tuổi" />
                                    <ext:Column ColumnID="Sex" Width="65" Header="Giới tính" DataIndex="Sex">
                                        <Renderer Fn="RenderSex" />
                                    </ext:Column>
                                    <ext:Column ColumnID="MaritalStatusName" Width="100" Header="Hôn nhân" DataIndex="MaritalStatusName" Fixed="true" />
                                    <ext:Column ColumnID="DepartmentName" Width="230" Header="Đơn vị công tác" DataIndex="DepartmentName" Fixed="true" />
                                    <ext:Column ColumnID="BirthPlace" Width="250" Header="Nơi sinh" DataIndex="BirthPlace" Fixed="true" />
                                    <ext:Column ColumnID="Hometown" Width="250" Header="Quê quán" DataIndex="Hometown" Fixed="true" />
                                    <ext:Column ColumnID="FolkName" Width="90" Header="Dân tộc" DataIndex="FolkName" Fixed="true" />
                                    <ext:Column ColumnID="ReligionName" Width="90" Header="Tôn giáo" DataIndex="ReligionName" Fixed="true" />
                                    <ext:Column ColumnID="PersonalClassName" Width="90" Header="Thành phần bản thân" DataIndex="PersonalClassName" />
                                    <ext:Column ColumnID="FamilyClassName" Width="90" Header="Thành phần gia đình" DataIndex="FamilyClassName" />
                                    <ext:Column ColumnID="ResidentPlace" Width="90" Header="Hộ khẩu thường trú" DataIndex="ResidentPlace" />
                                    <ext:Column ColumnID="Address" Width="90" Header="Nơi ở hiện nay" DataIndex="Address" />
                                    <ext:Column ColumnID="PreviousJob" Width="150" Header="Nghề nghiệp trước tuyển dụng" DataIndex="PreviousJob" />
                                    <ext:DateColumn ColumnID="RecruimentDate" Width="120" Header="Ngày tuyển dụng" DataIndex="RecruimentDate" Format="dd/MM/yyyy" />
                                    <ext:Column ColumnID="RecruimentDepartment" Width="230" Header="Cơ quan tuyển dụng" DataIndex="RecruimentDepartment" />
                                    <ext:Column ColumnID="PositionName" Width="180" Header="Chức vụ hiện tại" DataIndex="PositionName" />
                                    <ext:Column ColumnID="JobTitleName" Width="180" Header="Chức danh hiện tại" DataIndex="JobTitleName" />
                                    <ext:Column ColumnID="AssignedWork" Width="200" Header="Công việc chính được giao" DataIndex="AssignedWork" />
                                    <ext:Column ColumnID="BasicEducationName" Width="100" Header="Trình độ văn hóa" DataIndex="BasicEducationName" />
                                    <ext:Column ColumnID="EducationName" Width="180" Header="Trình độ chuyên môn cao nhất" DataIndex="EducationName" />
                                    <ext:Column ColumnID="PoliticLevelName" Width="150" Header="Lý luận chính trị" DataIndex="PoliticLevelName" />
                                    <ext:Column ColumnID="ManagementLevelName" Width="150" Header="Quản lý Nhà nước" DataIndex="ManagementLevelName" />
                                    <ext:Column ColumnID="LanguageLevelName" Width="90" Header="Ngoại ngữ" DataIndex="LanguageLevelName" />
                                    <ext:Column ColumnID="ITLevelName" Width="90" Header="Tin học" DataIndex="ITLevelName" />
                                    <ext:Column ColumnID="CPVJoinedDate" Width="100" Header="Ngày vào Đảng" DataIndex="CPVJoinedDate">
                                        <Renderer Fn="RenderDate" />
                                    </ext:Column>
                                    <ext:Column ColumnID="CPVOfficialJoinedDate" Width="100" Header="Ngày chính thức" DataIndex="CPVOfficialJoinedDate">
                                        <Renderer Fn="RenderDate" />
                                    </ext:Column>
                                    <ext:Column ColumnID="CPVJoinedPlace" Width="200" Header="Nơi kết nạp Đảng" DataIndex="CPVJoinedPlace" />
                                    <ext:Column ColumnID="CPVCardNumber" Width="100" Header="Số thẻ Đảng" DataIndex="CPVCardNumber" />
                                    <ext:Column ColumnID="CPVPositionName" Width="150" Header="Chức vụ Đảng" DataIndex="CPVPositionName" />
                                    <ext:Column ColumnID="VYUJoinedDate" Width="100" Header="Ngày vào Đoàn" DataIndex="VYUJoinedDate" />
                                    <ext:Column ColumnID="VYUJoinedPlace" Width="200" Header="Nơi kết nạp Đoàn" DataIndex="VYUJoinedPlace" />
                                    <ext:Column ColumnID="VYUPositionName" Width="150" Header="Chức vụ Đoàn" DataIndex="VYUPositionName" />
                                    <ext:Column ColumnID="ArmyJoinedDate" Width="100" Header="Ngày nhập ngũ" DataIndex="ArmyJoinedDate" />
                                    <ext:Column ColumnID="ArmyLeftDate" Width="100" Header="Ngày xuất ngũ" DataIndex="ArmyLeftDate" />
                                    <ext:Column ColumnID="ArmyLevelName" Width="150" Header="Cấp bậc quân đội" DataIndex="ArmyLevelName" />
                                    <ext:Column ColumnID="TitleAwarded" Width="200" Header="Danh hiệu được phong tặng" DataIndex="TitleAwarded" />
                                    <ext:Column ColumnID="Skills" Width="150" Header="Sở trường công tác" DataIndex="Skills" />
                                    <ext:Column ColumnID="HealthStatusName" Width="150" Header="Tình trạng sức khoẻ" DataIndex="HealthStatusName" />
                                    <ext:Column ColumnID="BloodGroup" Width="90" Header="Nhóm máu" DataIndex="BloodGroup" />
                                    <ext:Column ColumnID="Height" Width="90" Header="Chiều cao" DataIndex="Height" />
                                    <ext:Column ColumnID="Weight" Width="90" Header="Cân nặng" DataIndex="Weight" />
                                    <ext:Column ColumnID="RankWounded" Width="120" Header="Là thương binh hạng" DataIndex="RankWounded" />
                                    <ext:Column ColumnID="FamilyPolicyName" Width="150" Header="Gia đình chính sách" DataIndex="FamilyPolicyName" />
                                    <ext:Column ColumnID="IDNumber" Width="90" Header="Số CMND" DataIndex="IDNumber" />
                                    <ext:Column ColumnID="IDIssueDate" Width="100" Header="Ngày cấp" DataIndex="IDIssueDate">
                                        <Renderer Fn="RenderDate" />
                                    </ext:Column>
                                    <ext:Column ColumnID="IDIssuePlaceName" Width="150" Header="Nơi cấp" DataIndex="IDIssuePlaceName" />
                                    <ext:Column ColumnID="InsuranceNumber" Width="100" Header="Số sổ BHXH" DataIndex="InsuranceNumber" />
                                    <ext:Column ColumnID="InsuranceIssueDate" Width="100" Header="Ngày cấp sổ" DataIndex="InsuranceIssueDate" />
                                    <ext:Column ColumnID="PersonalTaxCode" Width="120" Header="Mã số thuế cá nhân" DataIndex="PersonalTaxCode" />
                                    <ext:Column ColumnID="CellPhoneNumber" Width="100" Header="Di động" DataIndex="CellPhoneNumber" />
                                    <ext:Column ColumnID="HomePhoneNumber" Width="100" Header="Điện thoại nhà" DataIndex="HomePhoneNumber" />
                                    <ext:Column ColumnID="WorkPhoneNumber" Width="100" Header="Điện thoại cơ quan" DataIndex="WorkPhoneNumber" />
                                    <ext:Column ColumnID="WorkEmail" Width="175" Header="Email nội bộ" DataIndex="WorkEmail" />
                                    <ext:Column ColumnID="PersonalEmail" Width="175" Header="Email riêng" DataIndex="PersonalEmail" />
                                    <ext:Column ColumnID="ContactPersonName" Width="150" Header="Người liên hệ (khi cần)" DataIndex="ContactPersonName" />
                                    <ext:Column ColumnID="ContactRelation" Width="100" Header="Mối quan hệ" DataIndex="ContactRelation" />
                                    <ext:Column ColumnID="ContactPhoneNumber" Width="100" Header="Số điện thoại" DataIndex="ContactPhoneNumber" />
                                    <ext:Column ColumnID="ContactAddress" Width="120" Header="Địa chỉ người liên hệ" DataIndex="ContactAddress" />
                                    <ext:Column ColumnID="WorkStatusName" Width="175" Header="Trạng thái" DataIndex="WorkStatusName" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" ID="rowSelection">
                                    <Listeners>
                                        <RowSelect Handler="hdfRecordId.setValue(rowSelection.getSelected().id);
                                                        employeeDetail_hdfRecordId.setValue(rowSelection.getSelected().id);
                                                        inputEmployee_hdfRecordId.setValue(rowSelection.getSelected().id);
                                                        ReloadStoreOfTabIndex();btnEdit.enable();btnDelete.enable();" />
                                        <RowDeselect Handler="btnEdit.disable();btnDelete.disable();"/>
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" EmptyMsg="Không có dữ liệu"
                                    PageSize="12" DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="Số dòng trên 1 trang" />
                                        <ext:ToolbarSpacer runat="server" Width="10" />
                                        <ext:ComboBox ID="ComboBoxPaging" runat="server" Width="80">
                                            <Items>
                                                <ext:ListItem Text="12" />
                                                <ext:ListItem Text="20" />
                                                <ext:ListItem Text="25" />
                                                <ext:ListItem Text="30" />
                                                <ext:ListItem Text="40" />
                                                <ext:ListItem Text="100" />
                                            </Items>
                                            <SelectedItem Value="12" />
                                            <Listeners>
                                                <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();"></Select>
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                    <Listeners>
                                        <Change Handler="rowSelection.clearSelections();try{btnEdit.disable();}catch(e){}try{btnDelete.disable();}catch(e){}" />
                                    </Listeners>
                                </ext:PagingToolbar>
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                            <Listeners>
                                <RowContextMenu Handler="e.preventDefault(); #{RowContextMenu}.dataRecord = this.store.getAt(rowIndex);#{RowContextMenu}.showAt(e.getXY());#{grp_HoSoNhanSu}.getSelectionModel().selectRow(rowIndex);" />

                            </Listeners>
                        </ext:GridPanel>
                    </Center>
                    <South>
                        <ext:Panel runat="server" ID="fd" Height="220">
                            <Content>
                                <uc:EmployeeDetail ID="employeeDetail" runat="server" />
                            </Content>
                        </ext:Panel>
                    </South>
                </ext:BorderLayout>
            </Items>
        </ext:Viewport>

    </form>
</body>
</html>
