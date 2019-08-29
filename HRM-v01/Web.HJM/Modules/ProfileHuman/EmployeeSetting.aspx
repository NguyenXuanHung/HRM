<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.EmployeeSetting" Codebehind="EmployeeSetting.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="ucSample.ascx" TagName="ucSample" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Modules/ProfileHuman/Resource/EmployeeSetting.css" rel="stylesheet" />
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/Resource/js/global.js"></script>
    <script type="text/javascript" src="Resource/EmployeeSetting.js"></script>
    <script type="text/javascript" src="JScript.js"></script>
    <script src="/Resource/js/RenderJS.js" type="text/javascript"></script>
    <script type="text/javascript">

        var addDatetoDF2 = function () {
            var today = new Date();
            var month = today.getMonth() + 1;
            var now1 = today.getDate() + '/' + month + '/' + today.getFullYear();
            DateField2.setValue(now1);
        }
        var RenderFormat = function () {
            var value = txtFamilyIncome.getValue();
            if (value == null || value.length === 0)
                return "";
            value = Math.round(value);
            var l = (value + "").length;
            var s = value + "";
            var rs = "";
            var count = 0;
            for (var i = l - 1; i > 0; i--) {
                count++;
                if (count === 3) {
                    rs = "," + s.charAt(i) + rs;
                    count = 0;
                }
                else {
                    rs = s.charAt(i) + rs;
                }
            }
            rs = s.charAt(0) + rs;
            if (rs.replace(",", "").trim() * 1 === 0) {
                return "";
            }
            txtFamilyIncome.setValue(rs);
            console.log(rs);
            if (rs === 'NaN' || rs === '') {
                rs = "";
            }
            return "<span >" + txtFamilyIncome.setValue(rs) + "</span>";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <ext:ResourceManager runat="server" ID="RM">
                <Listeners>
                    <DocumentReady Handler="hideLeftCommand();loadGridPanel();" />
                </Listeners>
            </ext:ResourceManager>

            <ext:Hidden runat="server" ID="hdfAnhDaiDien" />
            <ext:Hidden runat="server" ID="hdfPrKeyHoSo" />
            <ext:Hidden runat="server" ID="hdfMaCB" />
            <ext:Hidden runat="server" ID="hdfSampleID" />
            <ext:Hidden runat="server" ID="hdfTime1" />
            <ext:Hidden runat="server" ID="hdfKeyContract" />
            <ext:Hidden runat="server" ID="hdfKeyTrainingHistory" />
            <uc:ucSample ID="ucSample1" runat="server" />
            <ext:Store runat="server" ID="cbxNgach_Store" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="/Services/HandlerQuantum.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Quantum" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Code" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Hidden runat="server" ID="hdfEnableCatalogGroup" />
            <ext:Hidden runat="server" ID="hdfCurrentCatalogName" />
            <ext:Hidden runat="server" ID="hdfCurrentCatalogGroupName" />
            <ext:Window runat="server" Title="Thêm danh mục" Icon="Add" Layout="BorderLayout" Width="550" Modal="true" Hidden="true" Padding="6" Height="350" Constrain="true" ID="wdAddCategory" Resizable="true">
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="btnAddCategory" Text="Thêm mới" Icon="Add">
                                <Listeners>
                                    <Click Handler="btnAddCategory.disable(); txtTenDM.show(); btnSave.show(); btnCancel.show();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator runat="server" />
                            <ext:TextField runat="server" ID="txtTenDM" FieldLabel="Tên" Width="150" LabelWidth="40" Hidden="true" MaxLength="50" />
                            <ext:ToolbarSpacer Width="5" />
                            <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk" Hidden="true">
                                <DirectEvents>
                                    <Click OnEvent="btnSave_Click"></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Cancel" Hidden="true">
                                <Listeners>
                                    <Click Handler="resetInputCategory();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Hidden runat="server" ID="hdfTableDM" />
                    <ext:Hidden runat="server" ID="hdfColMa" />
                    <ext:Hidden runat="server" ID="hdfColTen" />
                    <ext:Hidden runat="server" ID="hdfCurrentCategory" />
                    <ext:GridPanel ID="grpCategory" runat="server" Border="false" TrackMouseOver="true" StripeRows="true" AutoExpandColumn="Name"
                        Region="Center">
                        <Store>
                            <ext:Store ID="stCategory" runat="server" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="objname" Value="hdfTableDM.getValue()" Mode="Raw" />
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
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:Column Header="Tên danh mục" ColumnID="Name" Width="175" DataIndex="Name">
                                    <Editor>
                                        <ext:TextField runat="server" />
                                    </Editor>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModelCategory" runat="server" />
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20">
                                <Listeners>
                                    <Change Handler="RowSelectionModelCategory.clearSelections();" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Listeners>
                    <Hide Handler="btnCancel.hide(); btnSave.hide();txtTenDM.reset(); txtTenDM.hide();btnAddCategory.enable();" />
                    <BeforeShow Handler="beforeShowWdCategory();" />
                </Listeners>
                <Buttons>
                    <ext:Button runat="server" Text="Đồng ý chọn" Icon="Accept">
                        <Listeners>
                            <Click Handler="if(CheckSelectedRows(grpCategory)){selectedCategory();}" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAddCategory.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>

            <ext:Window runat="server" Title="Thêm danh mục" Icon="Add" Layout="BorderLayout" Width="600" Modal="true" Hidden="true" Padding="6" Height="350" Constrain="true" ID="wdAddCategoryGroup" Resizable="true">
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="btnAddCategoryGroup" Text="Thêm mới" Icon="Add">
                                <Listeners>
                                    <Click Handler="btnAddCategoryGroup.disable(); txtTenDMGroup.show(); btnSaveGroupCategory.show(); btnCancelGroupCategory.show();if(hdfEnableCatalogGroup.getValue()=='true') {cbxWdCategoryGroup.show(); storeCbxCatalogyGroup.reload();}" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator runat="server" />
                            <ext:TextField runat="server" ID="txtTenDMGroup" FieldLabel="Tên" Width="150" LabelWidth="40" Hidden="true" MaxLength="50" />
                            <ext:ToolbarSpacer Width="10" />
                            <ext:Hidden runat="server" ID="hdfCategoryGroupId" />
                            <ext:ComboBox runat="server" ID="cbxWdCategoryGroup" FieldLabel="Loại" Hidden="true"
                                DisplayField="Group" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true" CtCls="requiredData"
                                LabelWidth="40" Width="200" ItemSelector="div.list-item">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template ID="Template3" runat="server">
                                    <Html>
                                        <tpl for=".">
						                    <div class="list-item"> 
							                    {Group}
						                    </div>
					                    </tpl>
                                    </Html>
                                </Template>
                                <Store>
                                    <ext:Store runat="server" ID="storeCbxCatalogyGroup" AutoLoad="false">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="objname" Value="hdfCurrentCatalogGroupName.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="itemTimeSheetHandlerType" Value="hdfCurrentCatalogName.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Group" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();hdfCategoryGroupId.setValue(cbxWdCategoryGroup.getValue());" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfCategoryGroupId.reset(); };" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ToolbarSpacer Width="5" />
                            <ext:Button runat="server" ID="btnSaveGroupCategory" Text="Lưu" Icon="Disk" Hidden="true">
                                <DirectEvents>
                                    <Click OnEvent="btnSave_ClickGroup"></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnCancelGroupCategory" Text="Hủy" Icon="Cancel" Hidden="true">
                                <Listeners>
                                    <Click Handler="resetInputCategoryGroup();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:GridPanel ID="grpCategoryGroup" runat="server" Border="false" TrackMouseOver="true" StripeRows="true" AutoExpandColumn="Name" Region="Center">
                        <Store>
                            <ext:Store ID="StoreCategoryGroup" runat="server" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="objname" Value="hdfCurrentCatalogName.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                            <ext:RecordField Name="Group" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel2" runat="server">
                            <Columns>
                                <ext:Column Header="Tên danh mục" ColumnID="Name" Width="175" DataIndex="Name">
                                    <Editor>
                                        <ext:TextField runat="server" />
                                    </Editor>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModelCategoryGroup" runat="server" />
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbarCategoryGroup" runat="server" PageSize="20">
                                <Listeners>
                                    <Change Handler="RowSelectionModelCategoryGroup.clearSelections();" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Listeners>
                    <Hide Handler="resetInputCategoryGroup();hdfEnableCatalogGroup.setValue('');hdfCurrentCatalogName.setValue('');" />
                    <BeforeShow Handler="beforeShowWdCategoryGroup();" />
                </Listeners>
                <Buttons>
                    <ext:Button runat="server" Text="Đồng ý chọn" Icon="Accept">
                        <Listeners>
                            <Click Handler="if(CheckSelectedRows(grpCategoryGroup)){selectedCategoryGroup();}" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAddCategoryGroup.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Resizable="false" Hidden="true" runat="server" ID="wdCreateSample"
                Constrain="true" Title="Tạo mẫu" Icon="Database" Width="400"
                Padding="6" AutoHeight="true">
                <Items>
                    <ext:Container runat="server" AnchorHorizontal="100%" ID="ctv1" Layout="FormLayout">
                        <Items>
                            <ext:TextField runat="server" ID="txtSampleName" FieldLabel="Tên mẫu<span style='color:red'>*</span>"
                                AnchorHorizontal="100%" CtCls="requiredData">
                            </ext:TextField>
                            <ext:TextArea runat="server" ID="txtSampleNote" FieldLabel="Ghi chú" AnchorHorizontal="100%">
                            </ext:TextArea>
                            <ext:Checkbox runat="server" ID="ckClearValue" BoxLabel="Xóa trắng các giá trị trên biểu mẫu" Checked="true" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Text="Đồng ý" ID="btnAddSample" Icon="Accept" Hidden="false">
                        <Listeners>
                            <Click Handler="return CheckInputCreateSample();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="Button21" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdCreateSample}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <BeforeShow Handler="txtSampleName.reset(); txtSampleNote.reset();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" Title="<%$ Resources:HOSO, Select_Image%>" Resizable="false"
                Icon="ImageAdd" Hidden="true" Padding="6" ID="wdUploadImageWindow" Width="400"
                Modal="true" AutoHeight="true">
                <Items>
                    <ext:FormPanel runat="server" Border="false" ID="frmPanelUploadImage">
                        <Items>
                            <ext:Hidden runat="server" Text="../../File/ImagesEmployee" ID="hdfImageFolder" />
                            <ext:Hidden runat="server" ID="hdfColumnName" />
                            <ext:FileUploadField runat="server" ID="fufUploadControl" AllowBlank="false" RegexText="<%$ Resources:HOSO, Formats_file_is_not_valid%>"
                                Regex="(http(s?):)|([/|.|\w|\s])*\.(?:jpg|gif|png|bmp|jpeg|JPG|PNG|GIF|BMP|JPEG)"
                                AnchorHorizontal="100%" FieldLabel="<%$ Resources:HOSO, Select_Image%>">
                                <Listeners>
                                    <FileSelected Handler="if (#{frmPanelUploadImage}.getForm().isValid()){#{btnAccept}.enable();}else{Ext.Msg.alert('<asp:Literal runat=\'server\' Text=\'<%$ Resources:HOSO, notify%>\' />','<asp:Literal runat=\'server\' Text=\'<%$ Resources:HOSO, Formats_file_is_not_valid%>\' />, <asp:Literal runat=\'server\' Text=\'<%$ Resources:HOSO, software_only_accept_image%>\' />');#{btnAccept}.disable();}" />
                                </Listeners>
                            </ext:FileUploadField>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnAccept" Text="<%$ Resources:HOSO, Accept%>" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="btnAccept_Click">
                                <EventMask ShowMask="true" Msg="<%$ Resources:HOSO, Please_wait%>" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button2" runat="server" Text="<%$ Resources:HOSO, close%>" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdUploadImageWindow}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <BeforeShow Handler="#{btnAccept}.disable();" />
                    <Hide Handler="fufUploadControl.reset();" />
                </Listeners>
            </ext:Window>

            <ext:Store runat="server" ID="cbx_bophan_Store" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="IsLocked" />
                            <ext:RecordField Name="MA_DONVI" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store ID="StoreLevelRewardDiscipline" AutoSave="false" ShowWarningOnFailure="false"
                SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoLoad="false"
                runat="server">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_LevelRewardDiscipline" Mode="Value" />
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
            <ext:Store ID="cbxLoaiHinhDaoTaoStore" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_TrainingSystem" Mode="Value" />
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
            <!-- Store lấy loại cán bộ -->
            <ext:Store runat="server" ID="storeEmployeeType" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <script type="text/javascript">
                function changeDepartment(id) {
                    Ext.net.DirectMethods.ChangeDepartment(id);
                }
            </script>

            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server" ID="br">
                        <Center>
                            <ext:Panel runat="server" ID="pn" AutoScroll="true" Padding="6" Border="false">
                                <Content>
                                    <div class="col col-10">
                                        <div id="help">
                                            Bạn hãy nhập các giá trị bên dưới (Bỏ qua những thông tin không cần thiết) rồi ấn nút <b>lưu mẫu</b>
                                        </div>
                                        <div id="A4">
                                            <div class="header">
                                                <a name="DauTrang"></a>
                                                <ext:Hidden runat="server" ID="hdfManagementDepartmentId" />
                                                <ext:TextField runat="server" ID="txtManagementDepartment" FieldLabel="Cơ quan, đơn vị có thẩm quyền quản lý CBCC"
                                                    AllowBlank="false" AnchorHorizontal="98%" LabelWidth="260" Width="550" Disabled="True">
                                                </ext:TextField>
                                                <ext:TextField ID="txtEmployeeCode" runat="server" FieldLabel="<b>Số hiệu cán bộ, công chức</b>"
                                                    CtCls="requiredData" AllowBlank="false" AnchorHorizontal="98%" MaxLength="20"
                                                    MaxLengthText="Bạn chỉ được nhập tối đa 20 ký tự" LabelWidth="260" Width="550">
                                                </ext:TextField>
                                                <ext:TextField ID="txtTimeKeepingCode" runat="server" FieldLabel="<b>Mã chấm công</b>"
                                                    AnchorHorizontal="100%" Width="550" LabelWidth="260" />
                                                <ext:Hidden ID="hdfEmployeeTypeId" runat="server"></ext:Hidden>
                                                <ext:ComboBox runat="server" ID="cboEmployeeType" FieldLabel="<b>Loại cán bộ</b>" EmptyText="Chọn loại cán bộ"
                                                    LabelWidth="260" Width="550" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                                    ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="false" StoreID="storeEmployeeType">
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
                                                        <Select Handler="this.triggers[0].show();hdfEmployeeTimeSheetHandlerTypeId.setValue(cboEmployeeTimeSheetHandlerType.getValue());" />
                                                        <BeforeQuery Handler="this.triggers[0][this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfDepartmentId" />
                                                <ext:ComboBox runat="server" ID="cboDepartment" FieldLabel="<b>Cơ quan, đơn vị sử dụng CBCC</b>" EmptyText="Chọn đơn vị"
                                                    LabelWidth="260" Width="550" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                                    ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="false" StoreID="storeDepartment">
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
                                                        <Select Handler="this.triggers[0].show();warningUseDepartment(cboDepartment, hdfDepartmentId);changeDepartment(cboDepartment.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <div id="title">
                                                    <a name="SoYeuLyLich">SƠ YẾU LÝ LỊCH CÁN BỘ, CÔNG CHỨC</a>
                                                </div>
                                            </div>
                                            <div id="ThongTinCaNhan">
                                                <div id="image">
                                                    <ext:ImageButton ID="img_anhdaidien" OverImageUrl="../../../File/ImagesEmployee/No_person.jpg"
                                                        ImageUrl="../../../File/ImagesEmployee/No_person.jpg" runat="server" Width="120" Height="150">
                                                        <Listeners>
                                                            <Click Handler="#{wdUploadImageWindow}.show();" />
                                                        </Listeners>
                                                    </ext:ImageButton>
                                                </div>
                                                <div id="info">
                                                    <ext:TextField ID="txtFullName" runat="server" CtCls="requiredData" FieldLabel="<b>&nbsp1) Họ và tên khai sinh</b><span style='color:red;'>*</span>"
                                                        AllowBlank="false" AnchorHorizontal="98%" MaxLength="50" MaxLengthText="<%$ Resources:HOSO, maximum_characters%>"
                                                        LabelWidth="152" Width="440">
                                                        <Listeners>
                                                            <Blur Handler="ChuanHoaTen(#{txtFullName});" />
                                                        </Listeners>
                                                        <ToolTips>
                                                            <ext:ToolTip ID="ToolTip2" runat="server" Title="<%$ Resources:HOSO, guide%>" Html="Phần mềm sẽ tự động chuẩn hóa họ và tên của bạn. Ví dụ: bạn nhập nguyễn văn huy, kết quả trả về Nguyễn Văn Huy." />
                                                        </ToolTips>
                                                    </ext:TextField>
                                                    <ext:TextField ID="txtAlias" FieldLabel="<b>&nbsp2) Tên gọi khác</b>" runat="server"
                                                        Width="440" AllowBlank="true" LabelWidth="152" AnchorHorizontal="98%" MaxLength="50"
                                                        MaxLengthText="<%$ Resources:HOSO, maximum_characters%>">
                                                    </ext:TextField>

                                                    <ext:CompositeField runat="server" ID="cps" LabelWidth="152" FieldLabel="<b>&nbsp3) Ngày sinh</b> <span style='color:red;'>*</span>">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfBirthDate" CtCls="requiredData"
                                                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                                RegexText="Định dạng ngày sinh không đúng" Width="147">
                                                                <Listeners>
                                                                    <Blur Handler="checkNgaySinh(dfBirthDate,18);" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                            <ext:DisplayField runat="server" Text="<b>Giới tính</b><span style='color:red;'>*</span>" />
                                                            <ext:ComboBox ID="cbxSex" runat="server"
                                                                AnchorHorizontal="100%" SelectedIndex="0" AllowBlank="false" Width="68">
                                                                <Items>
                                                                    <ext:ListItem Text="Nam" Value="M" />
                                                                    <ext:ListItem Text="Nữ" Value="F" />
                                                                </Items>
                                                            </ext:ComboBox>
                                                        </Items>
                                                    </ext:CompositeField>
                                                    <ext:Hidden runat="server" ID="hdfMaritalStatusId" />
                                                    <ext:ComboBox runat="server" ID="cboMaritalStatus" FieldLabel="<b>Tình trạng hôn nhân</b>" EmptyText="Tình trạng hôn nhân"
                                                        LabelWidth="152" Width="305" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                                        ItemSelector="div.list-item" AnchorHorizontal="100%" Editable="true">
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
                                                            <ext:Store runat="server" ID="storeMaritalStatus" AutoLoad="false">
                                                                <Proxy>
                                                                    <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                            <Expand Handler="storeMaritalStatus.reload();" />
                                                            <Select Handler="this.triggers[0].show();hdfMaritalStatusId.setValue(cboMaritalStatus.getValue());" />
                                                            <BeforeQuery Handler="this.triggers[0][this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <ext:Hidden runat="server" ID="hdfBirthPlaceProvinceId" />
                                                                <ext:ComboBox runat="server" ID="cbxBirthPlaceProvince" FieldLabel="<b>4) Nơi sinh:</b>&nbsp;&nbsp;&nbsp;Tỉnh/Thành phố" DisplayField="Name"
                                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                                    LabelWidth="150" Width="438" LabelAlign="Right" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                                    PageSize="10">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template22" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                            <div class="list-item"> 
							                                            <p><b>{Name}<//b><//p>
						                                            </div>
					                                            </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store ID="storeBirthPlaceProvince" runat="server" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerLocation.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="group" Value="Tinh,ThanhPhoTW" Mode="Value" />
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
                                                                        <Select Handler="this.triggers[0].show();hdfBirthPlaceProvinceId.setValue(cbxBirthPlaceProvince.getValue()); cbxBirthPlaceDistrict.clearValue(); cbxBirthPlaceDistrict.enable();cbxBirthPlaceWard.clearValue();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };hdfBirthPlaceProvinceId.reset();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ext:Hidden runat="server" ID="hdfBirthPlaceDistrictId" />
                                                                <ext:ComboBox runat="server" ID="cbxBirthPlaceDistrict" LabelAlign="Right" FieldLabel="Quận/Huyện" DisplayField="Name"
                                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                                    LabelWidth="150" Width="438" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                                    PageSize="10" Disabled="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template5" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                        <div class="list-item"> 
							                                                        <p><b>{Name}</b></p> 
						                                                        </div>
					                                                        </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="storeBirthPlaceDistrict" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerLocation.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="parentid" Value="#{hdfBirthPlaceProvinceId}.getValue()" Mode="Raw" />
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
                                                                        <Select Handler="this.triggers[0].show();hdfBirthPlaceDistrictId.setValue(cbxBirthPlaceDistrict.getValue()); cbxBirthPlaceWard.clearValue(); cbxBirthPlaceWard.enable();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };hdfBirthPlaceDistrictId.reset();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ext:Hidden runat="server" ID="hdfBirthPlaceWardId" />
                                                                <ext:ComboBox runat="server" ID="cbxBirthPlaceWard" FieldLabel="Phường/Xã" LabelAlign="Right"
                                                                    DisplayField="Name" MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                                    Editable="true" ItemSelector="div.list-item" LabelWidth="150" Width="438" ListWidth="283"
                                                                    LoadingText="Đang tải dữ liệu" PageSize="10" Disabled="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template9" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                        <div class="list-item"> 
							                                                        <p><b>{Name}</b></p> 
						                                                        </div>
					                                                        </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="storeBirthPlaceWard" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerLocation.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="parentid" Value="#{hdfBirthPlaceDistrictId}.getValue()" Mode="Raw" />
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
                                                                        <Select Handler="this.triggers[0].show();hdfBirthPlaceWardId.setValue(cbxBirthPlaceWard.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfBirthPlaceWardId.reset(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <ext:Hidden runat="server" ID="hdfHometownProvinceId" Text="0" />
                                                                <ext:ComboBox runat="server" ID="cbxHometownProvince" FieldLabel="<b>5) Quê quán:</b>&nbsp;Tỉnh/Thành phố" DisplayField="Name"
                                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                                    LabelWidth="150" Width="438" LabelAlign="Right" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                                    PageSize="10">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template31" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                <p><b>{Name}<//b><//p>
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store ID="storeHometownProvice" runat="server" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerLocation.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="group" Value="Tinh,ThanhPhoTW" Mode="Value" />
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
                                                                        <Select Handler="this.triggers[0].show();hdfHometownProvinceId.setValue(cbxHometownProvince.getValue());cbxHometownWard.disable();cbxHometownDistrict.clearValue();cbxHometownDistrict.enable();cbxHometownWard.clearValue();" />
                                                                        <BeforeQuery Handler="this.triggers[0][this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();}" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ext:Hidden runat="server" ID="hdfHometownDistrictId" />
                                                                <ext:ComboBox runat="server" ID="cbxHometownDistrict" FieldLabel="Quận/Huyện" DisplayField="Name"
                                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%" LabelAlign="Right"
                                                                    LabelWidth="150" Width="438" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                                    PageSize="10" Disabled="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template23" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                <p><b>{Name}</b></p> 
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="storeHometownDistrict" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerLocation.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="parentid" Value="#{hdfHometownProvinceId}.getValue()" Mode="Raw" />
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
                                                                        <Select Handler="this.triggers[0].show();hdfHometownDistrictId.setValue(cbxHometownDistrict.getValue());cbxHometownWard.clearValue(); cbxHometownWard.enable();cbxHometownWard.clearValue(); " />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };hdfHometownDistrictId.reset();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ext:Hidden runat="server" ID="hdfHometownWardId" />
                                                                <ext:ComboBox runat="server" ID="cbxHometownWard" FieldLabel="Phường/Xã" LabelAlign="Right"
                                                                    DisplayField="Name" MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%" Disabled="true"
                                                                    Editable="true" ItemSelector="div.list-item" LabelWidth="150" ListWidth="260" Width="438">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template6" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                    <div class="list-item"> 
							                                                    <p><b>{Name}</b></p>                                    
						                                                    </div>
					                                                    </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="storeHometownWard" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerLocation.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="parentid" Value="#{hdfHometownDistrictId}.getValue()" Mode="Raw" />
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
                                                                        <Select Handler="this.triggers[0].show();hdfHometownWardId.setValue(cbxHometownWard.getValue()); " />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfHometownWardId.reset(); };" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <ext:Hidden runat="server" ID="hdfTepTinDinhKem" />
                                                    <ext:CompositeField ID="composifieldAttach" FieldLabel="<b>&nbsp6) Hồ sơ gốc</b>" runat="server"
                                                        Width="440" AllowBlank="true" LabelWidth="152" AnchorHorizontal="98%">
                                                        <Items>
                                                            <ext:FileUploadField ID="fufTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                                                                ButtonText="" Icon="Attach" Width="229" AnchorHorizontal="100%">
                                                            </ext:FileUploadField>
                                                            <ext:Button runat="server" ID="btnQDLDownload" Icon="ArrowDown" ToolTip="Tải về">
                                                                <DirectEvents>
                                                                    <Click OnEvent="btnQDLDownload_Click" IsUpload="true" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="btnFileLDelete" Icon="Delete" ToolTip="Xóa">
                                                                <DirectEvents>
                                                                    <Click OnEvent="btnFileLDelete_Click">
                                                                        <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                                                            ConfirmRequest="true" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:CompositeField>
                                                </div>
                                                <div class="clr"></div>
                                            </div>
                                            <a name="ThongTinCoBan"></a>
                                            <table class="table">
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfFolkId" />
                                                        <ext:ComboBox runat="server" ID="cbxFolk" FieldLabel="<b>6) Dân tộc</b><span style='color:red;'>*</span>"
                                                            CtCls="requiredData" DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                                            ItemSelector="div.list-item" Width="368" LabelWidth="150">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="store_CbxFolk" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="store_CbxFolk.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfFolkId.setValue(cbxFolk.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfFolkId.reset();};
                                                                                    if (index == 1) { showWdAddCategory('DanToc'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfReligionId" />
                                                        <ext:ComboBox runat="server" ID="cbxReligion" MinChars="1" FieldLabel="<b>7) Tôn giáo</b>"
                                                            DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" ItemSelector="div.list-item"
                                                            Width="373" LabelWidth="150">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="store_CbxReligion" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="store_CbxReligion.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfReligionId.setValue(cbxReligion.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfReligionId.reset();};
                                                                                   if (index == 1) { showWdAddCategory('TonGiao'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfPersonalClassId" />
                                                        <ext:ComboBox runat="server" ID="cbxPersonalClass" FieldLabel="<b>Thành phần bản thân</b>"
                                                            MinChars="1" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" LabelWidth="150"
                                                            Width="368" ItemSelector="div.list-item" Editable="true">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                            </Triggers>
                                                            <Template ID="Template32" runat="server">
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
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="store_CbxPersonalClass.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfPersonalClassId.setValue(cbxPersonalClass.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfPersonalClassId.reset();};
                                                                                    if (index == 1) { showWdAddCategory('ThanhPhanBanThan'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfFamilyClassId" />
                                                        <ext:ComboBox runat="server" ID="cbxFamilyClass" FieldLabel="<b>Thành phần gia đình</b>"
                                                            LabelWidth="150" Width="373" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%"
                                                            ItemSelector="div.list-item" MinChars="1">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                            </Triggers>
                                                            <Template ID="Template35" runat="server">
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
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="store_CbxFamilyClass.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfFamilyClassId.setValue(cbxFamilyClass.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfFamilyClassId.reset();};
                                                                                 if (index == 1) { showWdAddCategory('ThanhPhanGiaDinh'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="clr">
                                            </div>
                                            <ext:TextField ID="txt_ResidentPlace" runat="server" FieldLabel="<b>&nbsp8) Nơi đăng ký hộ khẩu thường trú</b>"
                                                AllowBlank="true" AnchorHorizontal="100%" Width="746" LabelWidth="230">
                                            </ext:TextField>
                                            <ext:Label runat="server" ID="Lable16" LabelWidth="500" FieldLabel="(Số nhà, đường phố, thành phố, xóm, thôn, xã, huyện, tỉnh)">
                                            </ext:Label>
                                            <div class="clr">
                                            </div>
                                            <ext:TextField ID="txt_Address" runat="server" FieldLabel="<b>&nbsp9) Nơi ở hiện nay</b>"
                                                AnchorHorizontal="100%" Width="746" LabelWidth="120">
                                            </ext:TextField>
                                            <ext:Label runat="server" ID="Label15" LabelWidth="500" FieldLabel="(Số nhà, đường phố, thành phố, xóm, thôn, xã, huyện, tỉnh)">
                                            </ext:Label>
                                            <div class="clr">
                                            </div>
                                            <ext:TextField ID="txtPreviousJob" runat="server" FieldLabel="<b>10) Nghề nghiệp khi được tuyển dụng</b>"
                                                AnchorHorizontal="100%" Width="746" LabelWidth="275">
                                            </ext:TextField>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="RecruimentDate" Vtype="daterange" FieldLabel="<b>11) Ngày tuyển dụng</b>"
                                                            runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Width="280"
                                                            LabelWidth="140" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{date_ngaynhanct}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="endDateField" Value="#{date_ngaynhanct}" Mode="Value" />
                                                            </CustomConfig>
                                                        </ext:DateField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:TextField ID="txtRecruitmentDepartment" runat="server" FieldLabel="<b>Cơ quan tuyển dụng</b>"
                                                            AnchorHorizontal="100%" Width="455" LabelWidth="200">
                                                        </ext:TextField>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="FunctionaryDate" Vtype="daterange" FieldLabel="<b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Ngày biên chế</b>"
                                                                       runat="server" AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Width="282"
                                                                       LabelWidth="142" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                                       RegexText="Định dạng ngày không đúng">
                                                        </ext:DateField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="RevolutionJoinDate" Vtype="daterange" FieldLabel="<b>Ngày tham gia cách mạng</b>"
                                                                       runat="server" AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Width="455"
                                                                       LabelWidth="200" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                                       RegexText="Định dạng ngày không đúng">
                                                        </ext:DateField>
                                                    </td>
                                                </tr>
                                            </table>
                                           
                                            <div class="clr">
                                            </div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfPositionId" />
                                                        <ext:ComboBox runat="server" ID="cbxPosition" FieldLabel="<b>12) Chức vụ hiện tại</b>"
                                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                                            LabelWidth="140" Width="364" Editable="true" ItemSelector="div.list-item"
                                                            ListWidth="150" LoadingText="Đang tải dữ liệu">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                            <Store>
                                                                <ext:Store ID="store_CbxPosition" runat="server" AutoLoad="false">
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
                                                            </Store>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show(); hdfPositionId.setValue(cbxPosition.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfPositionId.reset(); };
                                                                                    if (index == 1) { showWdAddCategory('ChucVu'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfJobTitleId" />
                                                        <ext:ComboBox runat="server" ID="cbxJobTitle" FieldLabel="<b> Chức danh hiện tại</b>"
                                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                                            LabelWidth="130" Width="372" Editable="true" ItemSelector="div.list-item"
                                                            ListWidth="250" LoadingText="Đang tải dữ liệu">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                            </Triggers>
                                                            <Template ID="Template53" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store ID="Store1" runat="server" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Select Handler="this.triggers[0].show(); hdfJobTitleId.setValue(cbxJobTitle.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfJobTitleId.reset(); };
                                                                                if (index == 1) { showWdAddCategory('ChucDanh'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfGovernmentPositionId" />
                                                        <ext:ComboBox runat="server" ID="cbxGovernmentPosition" FieldLabel="<b>Chức vụ chính quyền</b>"
                                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                                            LabelWidth="140" Width="364" Editable="true" ItemSelector="div.list-item"
                                                            ListWidth="150" LoadingText="Đang tải dữ liệu">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                            <Store>
                                                                <ext:Store ID="store2" runat="server" AutoLoad="false">
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
                                                            </Store>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show(); hdfGovernmentPositionId.setValue(cbxGovernmentPosition.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGovernmentPositionId.reset(); };
                                                                                    if (index == 1) { showWdAddCategory('ChucVu'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfPluralityPositionId" />
                                                        <ext:ComboBox runat="server" ID="cbxPluralityPosition" FieldLabel="<b> Chức vụ kiêm nhiệm</b>"
                                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                                            LabelWidth="130" Width="372" Editable="true" ItemSelector="div.list-item"
                                                            ListWidth="250" LoadingText="Đang tải dữ liệu">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                            </Triggers>
                                                            <Template ID="Template36" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store ID="Store3" runat="server" AutoLoad="false">
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
                                                            </Store>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show(); hdfPluralityPositionId.setValue(cbxPluralityPosition.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfPluralityPositionId.reset(); };
                                                                                if (index == 1) { showWdAddCategory('ChucVu'); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            (Về chính quyền hoặc Đảng, đoàn thể, kể cả chức vụ kiêm nhiệm)
                                        <div class="clr">
                                        </div>
                                            <ext:TextField runat="server" ID="txtAssignedWork" FieldLabel="<b>13) Công việc chính được giao</b>"
                                                LabelWidth="233" Width="746">
                                            </ext:TextField>
                                            <ext:Hidden runat="server" ID="tdVanHoa" />
                                            <table class="clr">
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtQuantumName" FieldLabel="<b>14) Ngạch công chức (viên chức)</b>" Width="420" LabelWidth="230" ReadOnly="true" CtCls="requiredData" />
                                                    </td>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtQuantumCode" FieldLabel="<b>, Mã ngạch</b>" Width="322" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtSalaryGrade" FieldLabel="<b>Bậc lương</b>" Width="235" ReadOnly="true" CtCls="requiredData" />
                                                    </td>
                                                    <td>
                                                        <ext:NumberField runat="server" ID="txtSalaryFactor" FieldLabel="<b>, Hệ số</b>" Width="183" ReadOnly="true" />
                                                    </td>
                                                    <td>
                                                        <ext:DateField runat="server" ID="QuantumEffectiveDate" Format="dd/MM/yyyy" CtCls="requiredData"
                                                            FieldLabel="<b>, Ngày hưởng</b>" Width="322" LabelWidth="100" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:NumberField runat="server" ID="txtPositionAllowance" FieldLabel="<b>Phụ cấp chức vụ</b>" LabelWidth="150" Width="420" ReadOnly="true" />
                                                    </td>
                                                    <td>
                                                        <ext:NumberField runat="server" ID="txtOtherAllowance" FieldLabel="<b>, Phụ cấp khác</b>" Width="322" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <ext:Hidden runat="server" ID="hdfBasicEducationId" />
                                            <ext:ComboBox runat="server" ID="cbxBasicEducation" FieldLabel="<b>15.1- Trình độ giáo dục phổ thông (đã tốt nghiệp lớp mấy/thuộc loại nào)</b>"
                                                DisplayField="Name" ValueField="Id" ListWidth="200" AnchorHorizontal="100%" ItemSelector="div.list-item" MinChars="1"
                                                LabelWidth="500" Width="746">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                    <ext:Store ID="store_CbxBasicEducation" runat="server" AutoLoad="false">
                                                        <Proxy>
                                                            <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                    <Select Handler="this.triggers[0].show();hdfBasicEducationId.setValue(cbxBasicEducation.getValue());" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfBasicEducationId.reset() };
                                                                       if (index == 1) { hdfCurrentCatalogName.setValue('cat_BasicEducation'); hdfCurrentCatalogGroupName.setValue('cat_GroupEnum'); hdfEnableCatalogGroup.setValue('true'); wdAddCategoryGroup.show(); this.collapse(); }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <table class="clr">
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfEducationId" />
                                                        <ext:ComboBox runat="server" ID="cbxEducation" FieldLabel="<b>15.2- Trình độ chuyên môn cao nhất</b>"
                                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                            LabelWidth="252" Width="422" ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="store_CbxEducation" AutoLoad="false">
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
                                                                <Expand Handler="store_CbxEducation.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfEducationId.setValue(cbxEducation.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfEducationId.reset(); };
                                                                                   if (index == 1) { hdfCurrentCatalogName.setValue('cat_Education'); hdfCurrentCatalogGroupName.setValue('cat_GroupEnum'); hdfEnableCatalogGroup.setValue('true'); wdAddCategoryGroup.show(); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfInputIndustryId" />
                                                        <ext:ComboBox runat="server" ID="cbxInputIndustry" FieldLabel="<b>Chuyên ngành</b>"
                                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true"
                                                            LabelWidth="134" Width="313" ItemSelector="div.list-item">
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
                                                                <ext:Store runat="server" ID="storeInputIndustry" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_Industry" Mode="Value" />
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
                                                                <Select Handler="this.triggers[0].show();hdfInputIndustryId.setValue(cbxInputIndustry.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); cbxInputIndustry.reset(); };
                                                                                   " />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="clr">
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfPoliticLevelId" />
                                                        <ext:ComboBox runat="server" ID="cbxPoliticLevel" FieldLabel="<b>15.3- Lý luận chính trị</b>"
                                                            DisplayField="Name" ValueField="Id" MinChars="1" AnchorHorizontal="100%" ListWidth="200"
                                                            ItemSelector="div.list-item" LabelWidth="250" Width="420">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_CbxPoliticLevel" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="store_CbxPoliticLevel.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfPoliticLevelId.setValue(cbxPoliticLevel.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; cbxPoliticLevel.setValue('');hdfPoliticLevelId.setValue('');
                                                                                   if (index == 1) { hdfCurrentCatalogName.setValue('cat_PoliticLevel'); hdfCurrentCatalogGroupName.setValue('cat_GroupEnum'); hdfEnableCatalogGroup.setValue('true'); wdAddCategoryGroup.show(); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfManagementLevelId" />
                                                        <ext:ComboBox runat="server" ID="cbxManagementLevel" FieldLabel="<b>15.4- Q.lý nhà nước</b>" MinChars="1"
                                                            DisplayField="Name" Width="314" LabelWidth="135" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                            ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                            </Triggers>
                                                            <Template ID="Template34" runat="server">
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
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="cbx_trinhdoquanly_Store.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfManagementLevelId.setValue(cbxManagementLevel.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; cbxManagementLevel.setValue(''); hdfManagementLevelId.setValue('');
                                                                            if (index == 1) { hdfCurrentCatalogName.setValue('cat_ManagementLevel'); hdfCurrentCatalogGroupName.setValue('cat_GroupEnum'); hdfEnableCatalogGroup.setValue('true'); wdAddCategoryGroup.show(); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfLanguageLevelId" />
                                                        <ext:ComboBox runat="server" ID="cbxLanguageLevel" FieldLabel="<b>15.5- Ngoại ngữ</b>" MinChars="1"
                                                            DisplayField="Name" LabelWidth="250" Width="420" ValueField="Id" AnchorHorizontal="98%"
                                                            Editable="true" ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="cbx_ngoaingu_Store" AutoLoad="false">
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
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();hdfLanguageLevelId.setValue(cbxLanguageLevel.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; hdfLanguageLevelId.setValue(''); cbxLanguageLevel.setValue('');
                                                                if (index == 1) { hdfCurrentCatalogName.setValue('cat_LanguageLevel'); hdfCurrentCatalogGroupName.setValue('cat_GroupEnum'); hdfEnableCatalogGroup.setValue('true'); wdAddCategoryGroup.show(); this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfITLevelId" />
                                                        <ext:ComboBox runat="server" ID="cbxITLevel" FieldLabel="<b>15.6- Tin học</b>" DisplayField="Name"
                                                            LabelWidth="135" Width="314" ValueField="Id" AnchorHorizontal="98%" Editable="true" MinChars="1"
                                                            ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="cbx_tinhoc_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="cbx_tinhoc_Store.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfITLevelId.setValue(cbxITLevel.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); #{hdfITLevelId}.setValue('');#{cbxITLevel}.setValue('');this.triggers[0].hide();} 
                                                                if (index == 1) { hdfCurrentCatalogName.setValue('cat_ITLevel'); hdfCurrentCatalogGroupName.setValue('cat_GroupEnum'); hdfEnableCatalogGroup.setValue('true'); wdAddCategoryGroup.show(); this.collapse();}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="clr">
                                                <tr>
                                                    <td colspan="4">
                                                        <ext:DateField Editable="true" ID="CPVJoinedDate" FieldLabel="<b> 16) Ngày vào Đảng Cộng sản Việt Nam</b>"
                                                            runat="server" AnchorHorizontal="100%" Vtype="daterange" MaskRe="/[0-9|/]/" Format="d/M/yyyy"
                                                            Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng"
                                                            LabelWidth="240" Width="378">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{CPVOfficialJoinedDate}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="endDateField" Value="#{CPVOfficialJoinedDate}" Mode="Value" />
                                                            </CustomConfig>
                                                        </ext:DateField>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="CPVOfficialJoinedDate" FieldLabel="<b>Ngày chính thức</b>"
                                                            runat="server" AnchorHorizontal="100%" Vtype="daterange" LabelWidth="110" Width="255"
                                                            MaskRe="/[0-9|/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{CPVJoinedDate}.setMaxValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="startDateField" Value="#{CPVJoinedDate}" Mode="Value" />
                                                            </CustomConfig>
                                                        </ext:DateField>
                                                    </td>
                                                    <td colspan="2">
                                                        <ext:TextField runat="server" ID="txtCPVCardNumber" AnchorHorizontal="100%" LabelWidth="70" Width="200" FieldLabel="<b>Thẻ đảng</b>" />
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfCPVPositionId" />
                                                        <ext:ComboBox runat="server" ID="cbxCPVPosition" FieldLabel="<b>Chức vụ Đảng</b>"
                                                            LabelWidth="110" Width="279" DisplayField="Name" ValueField="Id" AnchorHorizontal="100%"
                                                            Editable="true" MinChars="1" ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="cbx_chuvudang_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Select Handler="this.triggers[0].show();hdfCPVPositionId.setValue(cbxCPVPosition.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfCPVPositionId.reset(); }
                                                                if (index == 1) { showWdAddCategory('ChucVuDang');this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <ext:TextField ID="txtCPVJoinedPlace" runat="server" FieldLabel="<b>Nơi kết nạp Đảng</b>"
                                                            AllowBlank="true" AnchorHorizontal="100%" MaxLength="50" LabelWidth="249" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự"
                                                            Width="745">
                                                        </ext:TextField>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="NgayThamGiaToChucChinhTri" class="clr">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <ext:DateField Editable="true" ID="VYUJoinedDate" FieldLabel="<b>17) Ngày tham gia tổ chức chính trị - xã hội</b>"
                                                                runat="server" LabelWidth="300" Width="429" MaskRe="/[0-9|/]/" Format="d/M/yyyy"
                                                                Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                        </td>
                                                        <td style="width: 5px;"></td>
                                                        <td>
                                                            <ext:Hidden runat="server" ID="hdfVYUPositionId" />
                                                            <ext:ComboBox runat="server" ID="cbxVYUPosition" FieldLabel="<b>Chức vụ Đoàn </b>"
                                                                DisplayField="Name" ValueField="Id" MinChars="1" AnchorHorizontal="100%" Editable="true" ListWidth="200"
                                                                ItemSelector="div.list-item" LabelWidth="130" Width="307">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    <ext:FieldTrigger Icon="SimpleAdd" />
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
                                                                    <ext:Store runat="server" ID="cbx_chucvudoan_Store" AutoLoad="false">
                                                                        <Proxy>
                                                                            <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                    <Expand Handler="cbx_chucvudoan_Store.reload();" />
                                                                    <Select Handler="this.triggers[0].show();hdfVYUPositionId.setValue(cbxVYUPosition.getValue());" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfVYUPositionId.reset();}
                                                                    if (index == 1) { showWdAddCategory('ChucVuDoan');this.collapse(); }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="ArmyJoinedDate" FieldLabel="<b>18) Ngày nhập ngũ</b>"
                                                            runat="server" AnchorHorizontal="100%" Vtype="daterange" LabelWidth="140" Width="271"
                                                            MaskRe="/[0-9|/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{ArmyJoinedDate}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="endDateField" Value="#{ArmyJoinedDate}" Mode="Value" />
                                                            </CustomConfig>
                                                        </ext:DateField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="ArmyLeftDate" FieldLabel="<b>Ngày xuất ngũ</b>"
                                                            runat="server" AnchorHorizontal="100%" Vtype="daterange" LabelWidth="100" Width="200"
                                                            MaskRe="/[0-9|/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{ArmyLeftDate}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="endDateField" Value="#{ArmyLeftDate}" Mode="Value" />
                                                            </CustomConfig>
                                                        </ext:DateField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfArmyLevelId" />
                                                        <ext:ComboBox runat="server" ID="cbxArmyLevel" FieldLabel="<b>Quân hàm cao nhất</b>"
                                                            LabelWidth="130" Width="256" DisplayField="Name" EmptyText="Ví dụ Đại tá, thiếu úy..." MinChars="1"
                                                            ValueField="Id" AnchorHorizontal="100%" ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbx_bacquandoi_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
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
                                                                <Expand Handler="cbx_bacquandoi_Store.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfArmyLevelId.setValue(cbxArmyLevel.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfArmyLevelId.reset(); }
                                                                if (index == 1) { showWdAddCategory('CapBacQuanDoi');this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="clr">
                                            </div>
                                            <ext:TextField runat="server" ID="txtTitleAwarded" FieldLabel="<b>19) Danh hiệu được phong tặng cao nhất</b>"
                                                LabelWidth="270" Width="746">
                                            </ext:TextField>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:TextArea runat="server" ID="txtSkills" FieldLabel="<b>20) Sở trường công tác</b>"
                                                                      AnchorHorizontal="100%" Height="40" LabelWidth="150" Width="400">
                                                        </ext:TextArea>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtLongestJob" FieldLabel="<b>Công việc đã làm lâu nhất</b>"
                                                                       LabelWidth="170">
                                                        </ext:TextField>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfHealthStatusId" />
                                                        <ext:ComboBox runat="server" ID="cbxHealthStatus" FieldLabel="<b>21) Tình trạng sức khỏe</b>" MinChars="1"
                                                            DisplayField="Name" ValueField="Id" EmptyText="Tình trạng sức khỏe" AnchorHorizontal="100%"
                                                            ItemSelector="div.list-item" LabelWidth="170" Width="306">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                                <ext:Store runat="server" ID="cbx_ttsuckhoe_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_HealthStatus" Mode="Value" />
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
                                                                <Expand Handler="cbx_ttsuckhoe_Store.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfHealthStatusId.setValue(cbxHealthStatus.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfHealthStatusId.reset(); }
                                                                                     if (index == 1) { showWdAddCategory('TinhTrangSucKhoe');this.collapse(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td>
                                                        <ext:NumberField runat="server" EmptyText="Cao" ID="txtHeight" LabelWidth="100" FieldLabel="<b>Chiều cao(cm)</b>" Width="150" />
                                                    </td>
                                                    <td>
                                                        <ext:NumberField runat="server" ID="txtWeight" EmptyText="Nặng" LabelWidth="100" FieldLabel="<b>Cân nặng(Kg)</b>" Width="150" />
                                                    </td>
                                                    <td>
                                                        <ext:ComboBox ID="cbxBloodGroup" LabelWidth="70" runat="server" FieldLabel="<b>Nhóm máu</b>" AllowBlank="true"
                                                            Width="132" Editable="true" AnchorHorizontal="100%">
                                                            <Items>
                                                                <ext:ListItem Text="A" Value="A" />
                                                                <ext:ListItem Text="B" Value="B" />
                                                                <ext:ListItem Text="AB" Value="AB" />
                                                                <ext:ListItem Text="O" Value="O" />
                                                            </Items>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtRankWounded" FieldLabel="<b>22) Là thương binh hạng</b>"
                                                            AnchorHorizontal="100%" MaxLength="100" LabelWidth="170" Width="336">
                                                        </ext:TextField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfFamilyPolicyId" />
                                                        <ext:ComboBox runat="server" ID="cbxFamilyPolicy" FieldLabel="<b>Là con gia đình chính sách</b>"
                                                            DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" MinChars="1" ListWidth="200"
                                                            LabelWidth="170" Width="399" ItemSelector="div.list-item">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbx_huongcs_Store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_FamilyPolicy" Mode="Value" />
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
                                                                <Expand Handler="cbx_huongcs_Store.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfFamilyPolicyId.setValue(cbxFamilyPolicy.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfFamilyPolicyId.reset(); }
                                                                 if (index == 1) { showWdAddCategory('ChinhSach'); this.collapse();}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:TextField ID="txtIDNumber" CtCls="requiredDataWG" runat="server" MaskRe="/[0-9]/"
                                                            FieldLabel="<b>23) Số chứng minh nhân dân</b> <span style='color:red;'>*</span>" AllowBlank="true" AnchorHorizontal="98%"
                                                            MaxLength="12" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự" LabelWidth="200"
                                                            Width="307">
                                                        </ext:TextField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="IDIssueDate" FieldLabel="<b>Ngày cấp</b>"
                                                            runat="server" AnchorHorizontal="98%" MaskRe="/[0-9|/]/" Format="d/M/yyyy" LabelWidth="60"
                                                            Width="180" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfIDIssuePlaceId" />
                                                        <ext:ComboBox runat="server" ID="cbxIDIssuePlace" FieldLabel="<b>Nơi cấp</b>"
                                                            DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" Editable="true" LabelWidth="50"
                                                            Width="239" ListWidth="200" MinChars="1" ItemSelector="div.list-item"
                                                            LoadingText="Đang tải dữ liệu..." EmptyText="Gõ để tìm kiếm">
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
                                                            <Store>
                                                                <ext:Store ID="cbx_noicapcmnd_Store" runat="server" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_IDIssuePlace" Mode="Value" />
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
                                                            <ToolTips>
                                                                <ext:ToolTip runat="server" ID="ToolTip4" Title="Hướng dẫn" Html="Nhập tên nơi cấp CMND để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                                                            </ToolTips>
                                                            <Listeners>
                                                                <Expand Handler="cbx_noicapcmnd_Store.reload();" />
                                                                <Select Handler="this.triggers[0].show();hdfIDIssuePlaceId.setValue(cbxIDIssuePlace.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfIDIssuePlaceId.reset(); }" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="SoSoBHXH" class="clr">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <ext:TextField ID="txtInsuranceNumber" runat="server" FieldLabel="<b>24) Số sổ BHXH</b>"
                                                                LabelWidth="110" Width="229" AllowBlank="true" AnchorHorizontal="100%" MaxLength="50"
                                                                MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự">
                                                            </ext:TextField>
                                                        </td>
                                                        <td style="width: 5px;"></td>
                                                        <td>
                                                            <ext:DateField runat="server" ID="InsuranceIssueDate" FieldLabel="<b>Ngày cấp sổ BHXH</b>"
                                                                AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                                RegexText="Định dạng ngày không đúng" Vtype="daterange" LabelWidth="120" Width="250">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); #{date_ketthucbh}.setMinValue(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                                <CustomConfig>
                                                                    <ext:ConfigItem Name="endDateField" Value="#{date_ketthucbh}" Mode="Value" />
                                                                </CustomConfig>
                                                            </ext:DateField>
                                                        </td>
                                                        <td>
                                                            <ext:TextField runat="server" MaxLength="50" LabelWidth="125" Width="253" ID="txtPersonalTaxCode" FieldLabel="<b>Mã số thuế cá nhân</b>" AnchorHorizontal="100%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <table class="table">
                                                <tr>
                                                    <td>
                                                        <ext:Hidden runat="server" ID="hdfWorkStatusId" />
                                                        <ext:ComboBox runat="server" ID="cbxWorkStatus" Width="386" DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item" CtCls="requiredDataWG"
                                                            FieldLabel="<b>Trạng thái làm việc(Đang làm việc,nghỉ hưu, từ trần...)</b>" LabelWidth="275">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Template ID="Template44" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                                                <div class="list-item"> 
							                                                 {Name}
						                                                </div>
					                                            </tpl>
                                                                </Html>
                                                            </Template>
                                                            <Store>
                                                                <ext:Store runat="server" ID="cbxTrangThaiHoSo_store" AutoLoad="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                    </Proxy>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="objname" Value="cat_WorkStatus" Mode="Value" />
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
                                                                <Select Handler="this.triggers[0].show();hdfWorkStatusId.setValue(cbxWorkStatus.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfWorkStatusId.reset();}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </td>
                                                    <td style="width: 5px;"></td>
                                                    <td>
                                                        <ext:DateField Editable="true" ID="WorkStatusDate" FieldLabel="<b>Ngày nghỉ hưu, thôi việc, từ trần...</b>"
                                                            runat="server" AnchorHorizontal="100%" MaskRe="/[0-9|/]/" Format="d/M/yyyy" LabelWidth="230"
                                                            Width="352" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                            <ext:TextArea runat="server" MaxLength="200" ID="txtWorkStatusReason" EmptyText="Lý do thôi việc, lý do về hưu sớm (muộn)..."
                                                AnchorHorizontal="100%" Width="746" LabelWidth="180" FieldLabel="<b>Ghi chú trạng thái làm việc</b>" />
                                            <table class="table">
                                                <tr>
                                                    <td>
                                                        <ext:TextField ID="txtCellPhoneNumber" runat="server" MaxLength="50" Width="227" FieldLabel="<b>Di động</b>" />
                                                    </td>
                                                    <td>
                                                        <ext:TextField ID="txtHomePhoneNumber" runat="server" MaxLength="20" FieldLabel="<b>ĐT Nhà</b>" />
                                                    </td>
                                                    <td>
                                                        <ext:TextField ID="txtWorkPhoneNumber" runat="server" MaxLength="50" Width="261" FieldLabel="<b>ĐT cơ quan</b>" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="table">
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" MaxLength="200" ID="txtWorkEmail" Width="371" FieldLabel="<b>Email nội bộ</b>" />
                                                    </td>
                                                    <td>
                                                        <ext:TextField runat="server" MaxLength="200" ID="txtPersonalEmail" Width="372" FieldLabel="<b>Email riêng</b>" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <p class="label clr"><a name="DaoTaoBoiDuong">25) Đào tạo, bồi dưỡng về chuyên môn, nghiệp vụ, lý luận chính trị, ngoại ngữ, tin học</a></p>
                                            <ext:GridPanel ID="GridEducation" runat="server" Width="745" Height="270">
                                                <Store>
                                                    <ext:Store ID="StoreEducation" runat="server" AutoSave="false" OnRefreshData="StoreEducation_OnRefreshData" OnBeforeStoreChanged="StoreEducation_BeforeStoreChanged"
                                                        ShowWarningOnFailure="false" SkipIdForNewRecords="false" AutoLoad="false" RefreshAfterSaving="None">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="UniversityName" />
                                                                    <ext:RecordField Name="UniversityId" DefaultValue="0" />
                                                                    <ext:RecordField Name="NationName" />
                                                                    <ext:RecordField Name="NationId" DefaultValue="0" />
                                                                    <ext:RecordField Name="Faculty" />
                                                                    <ext:RecordField Name="IndustryName" />
                                                                    <ext:RecordField Name="IndustryId" DefaultValue="0" />
                                                                    <ext:RecordField Name="EducationName" />
                                                                    <ext:RecordField Name="EducationId" DefaultValue="0" />
                                                                    <ext:RecordField Name="TrainingSystemName" />
                                                                    <ext:RecordField Name="TrainingSystemId" DefaultValue="0" />
                                                                    <ext:RecordField Name="GraduateTimeSheetHandlerTypeName" />
                                                                    <ext:RecordField Name="GraduationTimeSheetHandlerTypeId" DefaultValue="0" />
                                                                    <ext:RecordField Name="IsGraduated" DefaultValue="false" />
                                                                    <ext:RecordField Name="FromDate" DateFormat="MM/yyyy" />
                                                                    <ext:RecordField Name="ToDate" DateFormat="MM/yyyy" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn />
                                                        <ext:Column Header="Quốc gia đào tạo" DataIndex="NationName" Width="120" Sortable="false"
                                                            Align="Left">
                                                            <Renderer Fn="nationRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxNation" DisplayField="Name" MinChars="1" ValueField="Id"
                                                                    AnchorHorizontal="100%" ListWidth="200" Editable="true" ItemSelector="div.list-item" PageSize="15" StoreID="storeNation">
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
                                                                        <Expand Handler="storeNation.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateGridDatao('NationId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Trường đào tạo" DataIndex="UniversityName" Width="200" Sortable="false"
                                                            Align="Left">
                                                            <Renderer Fn="truongRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox AnchorHorizontal="100%" ValueField="Id" HideTrigger="false" DisplayField="Name"
                                                                    runat="server" PageSize="15" ItemSelector="div.search-item" MinChars="1" ID="CbxUniversity"
                                                                    LoadingText="Đang tải dữ liệu...">
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
                                                                    <Listeners>
                                                                        <Expand Handler="storeUniversity.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateGridDatao('UniversityId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();  }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Chuyên ngành" DataIndex="IndustryName"
                                                            Width="250" Sortable="false" Align="Left">
                                                            <Renderer Fn="ChuyenNganhRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox AnchorHorizontal="100%" ValueField="Id" DisplayField="Name" runat="server"
                                                                    PageSize="15" HideTrigger="false" ItemSelector="div.search-item" MinChars="1"
                                                                    ID="cbxIndustry" LoadingText="Đang tải dữ liệu...">
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
                                                                    <Template ID="Template11" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                    <div class="search-item">
							                                                    {Name}
						                                                    </div>
					                                                    </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Expand Handler="storeIndustry.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateGridDatao('IndustryId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>

                                                        <ext:DateColumn Format="MM/yyyy" Header="Từ tháng, năm" DataIndex="FromDate" Width="100" Sortable="false" Align="Center">
                                                            <Editor>
                                                                <ext:DateField ID="dfTuNgay" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="MM/yyyy"
                                                                    Regex="/^(1[0-2]|0?[0-9])\/[0-9]{4}$/">
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="MM/yyyy" Header="Đến tháng, năm" DataIndex="ToDate" Width="100" Sortable="false" Align="Center">
                                                            <Editor>
                                                                <ext:DateField ID="dfDenNgay" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="MM/yyyy"
                                                                    Regex="/^(1[0-2]|0?[0-9])\/[0-9]{4}$/">
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Hình thức" DataIndex="TrainingSystemName" Width="200"
                                                            Sortable="false" Align="Left">
                                                            <Renderer Fn="HinhThucDaoTaoRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxTrainingSystem" DisplayField="Name" MinChars="1" ValueField="Id"
                                                                    AnchorHorizontal="100%" Editable="true" LabelWidth="250" Width="420" ItemSelector="div.list-item">
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
                                                                    <Listeners>
                                                                        <Expand Handler="storeTrainingSystem.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateGridDatao('TrainingSystemId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Trình độ" Width="200" DataIndex="EducationName">
                                                            <Renderer Fn="TrinhDoRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox AnchorHorizontal="100%" ValueField="Id" HideTrigger="false" DisplayField="Name"
                                                                    runat="server" PageSize="15" ItemSelector="div.search-item" MinChars="1" ID="cbxEducation2"
                                                                    LoadingText="Đang tải dữ liệu...">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template2" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                    <div class="search-item">
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
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" ID="ToolTip5" Title="Hướng dẫn" Html="Nhập tên trình độ để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                                                                    </ToolTips>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateGridDatao('EducationId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();  }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel2" runat="server">
                                                        <Listeners>
                                                            <RowSelect Handler="#{btnDelete1}.enable();" />
                                                            <RowDeselect Handler="if (!#{GridEducation}.hasSelection()) {#{btnDelete1}.disable();}" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <TopBar>
                                                    <ext:Toolbar runat="server" ID="tbdaotao">
                                                        <Items>
                                                            <ext:Button ID="Button1" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridEducation}.addRecord(); #{GridEducation}.getView().focusRow(rowIndex); #{GridEducation}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="btnDelete1" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridEducation}.deleteSelected();#{GridEducation}.save(); if (!#{GridEducation}.hasSelection()) {#{btnDelete1}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:DisplayField runat="server" Text="<i>  (Nhấp chuột hai lần để tiến hành sửa)</i>" />
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                            </ext:GridPanel>
                                            <p class="label"><a name="QuaTrinhCongTac">26) Tóm tắt quá trình công tác(Trước khi vào đơn vị)</a></p>
                                            <ext:GridPanel ID="GridWorkHistory" runat="server" Width="745" Height="260">
                                                <Store>
                                                    <ext:Store ID="storeWorkHistory" runat="server" ShowWarningOnFailure="false" SkipIdForNewRecords="false"
                                                        RefreshAfterSaving="None" AutoLoad="false" OnBeforeStoreChanged="storeWorkHistory_BeforeStoreChanged" OnRefreshData="storeWorkHistory_OnRefreshData">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="FromDate" />
                                                                    <ext:RecordField Name="ToDate" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                    <ext:RecordField Name="SalaryLevel" DefaultValue="0" />
                                                                    <ext:RecordField Name="AddressCompany" />
                                                                    <ext:RecordField Name="ExperienceWork" />
                                                                    <ext:RecordField Name="ReasonLeave" />
                                                                    <ext:RecordField Name="WorkPosition" />
                                                                    <ext:RecordField Name="WorkPlace" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button ID="Button9" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridWorkHistory}.addRecord(); #{GridWorkHistory}.getView().focusRow(rowIndex); #{GridWorkHistory}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button10" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridWorkHistory}.deleteSelected(); #{GridWorkHistory}.save(); if (!#{GridWorkHistory}.hasSelection()) {#{Button10}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:DisplayField ID="DisplayField1" runat="server" Text="<i>  (Nhấp chuột hai lần để tiến hành sửa)</i>" />
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                                        <Listeners>
                                                            <RowSelect Handler="Button10.enable();" />
                                                            <RowDeselect Handler="Button10.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn />
                                                        <ext:DateColumn Header="Từ tháng/năm" DataIndex="FromDate" Width="90" Sortable="true" Format="MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfFromDate_WorkHistory" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Header="Đến tháng/năm" DataIndex="ToDate" Width="100" Sortable="true" Format="MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfToDate_WorkHistory" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Chức danh, chức vụ, đơn vị công tác" DataIndex="Note" Width="445" Sortable="true"
                                                            Align="Left">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="txtNote_WorkHistory" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="LichSuBanThan">27) Đặc điểm lịch sử bản thân</a></p>
                                            <p class="label">- Khai rõ: bị bắt,bị tù (từ ngày tháng năm nào đến ngày tháng năm nào, ở đâu), đã khai báo cho ai, những vấn đề gì? Bản thân có làm việc trong chế độ cũ (cơ quan, đơn vị nào, địa điểm, chức danh, chức vụ, thời gian làm việc ...)</p>
                                            <ext:TextArea runat="server" ID="txtBiography" Height="70" Width="745" />
                                            <p class="label">- Tham gia hoặc có quan hệ với các tổ chức chính trị, kinh tế, xã hội nào ở nước ngoài (làm gì, tổ chức nào, đặt trụ sở ở đâu ...?)</p>
                                            <ext:TextArea runat="server" ID="txtForeignOrganizationJoined" Height="70" Width="745" />
                                            <p class="label">- Có thân nhân (Cha, Mẹ, Vợ, Chồng, con, anh chị em ruột) ở nước ngoài (làm gì, địa chỉ ......)?</p>
                                            <ext:TextArea runat="server" ID="txtRelativesAboard" Height="70" Width="745" />
                                            <p class="label"><a name="QHGD" class="anchor">28) Quan hệ gia đình</a></p>
                                            <ext:GridPanel ID="GridPanelFamilyRelationship" runat="server" Height="260" Width="745">
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridPanelFamilyRelationship}.addRecord(); #{GridPanelFamilyRelationship}.getView().focusRow(rowIndex); #{GridPanelFamilyRelationship}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridPanelFamilyRelationship}.deleteSelected();#{GridPanelFamilyRelationship}.save(); if (!#{GridPanelFamilyRelationship}.hasSelection()) {#{btnDelete}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store runat="server" AutoLoad="false" ID="storeFamilyRelationship" OnBeforeStoreChanged="HandleChangesQuanHeGiaDinh"
                                                        OnRefreshData="StoreFamilyRelationship_OnRefreshData"
                                                        ShowWarningOnFailure="false" SkipIdForNewRecords="false" RefreshAfterSaving="None">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="FullName" />
                                                                    <ext:RecordField Name="Age" />
                                                                    <ext:RecordField Name="RelationshipId" DefaultValue="0" />
                                                                    <ext:RecordField Name="RelationName" />
                                                                    <ext:RecordField Name="Occupation" />
                                                                    <ext:RecordField Name="WorkPlace" />
                                                                    <ext:RecordField Name="Sex" DefaultValue="false" />
                                                                    <ext:RecordField Name="SexName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="IDNumber" />
                                                                    <ext:RecordField Name="IsDependent" DefaultValue="false" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                    <ext:RecordField Name="TaxCode" />
                                                                    <ext:RecordField Name="ReduceStartDate" />
                                                                    <ext:RecordField Name="ReduceEndDate" />
                                                                    <ext:RecordField Name="BirthYear" DefaultValue="0" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel11" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Width="35" Header="STT" />
                                                        <ext:Column ColumnID="FullName" Width="200" Header="Họ tên" DataIndex="FullName" Sortable="false">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="txtFullName_FamilyRelationship" AnchorHorizontal="100%" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="BirthYear" Width="70" Header="Năm sinh" DataIndex="BirthYear" Sortable="false">
                                                            <Editor>
                                                                <ext:NumberField runat="server" ID="TextFieldBirthYear" AnchorHorizontal="100%" AllowNegative="false" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="SexName" Width="50" Header="Giới tính" DataIndex="SexName" Sortable="false">
                                                            <Renderer Fn="GetGender" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxGenDer" SelectedIndex="0">
                                                                    <Items>
                                                                        <ext:ListItem Text="Nam" Value="M" />
                                                                        <ext:ListItem Text="Nữ" Value="F" />
                                                                    </Items>

                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="RelationName" Header="Quan hệ" DataIndex="RelationName" Sortable="true" Width="200">
                                                            <Renderer Fn="QuanHeGiaDinhRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxFamilyRelationship" Editable="true" DisplayField="Name" MinChars="1" PageSize="15"
                                                                    ValueField="Id" AnchorHorizontal="100%" ItemSelector="div.list-item" Width="200">
                                                                    <Store>
                                                                        <ext:Store ID="store_CbxFamilyRelationship" runat="server" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="objname" Value="cat_Relationship" Mode="Value" />
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
                                                                    <Template ID="Template33" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Expand Handler="store_CbxFamilyRelationship.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateQuanHeGiaDinh('RelationshipId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="Occupation" Header="Nghề nghiệp" DataIndex="Occupation" Sortable="false">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="TextFieldOccupation" AnchorHorizontal="100%" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="WorkPlace" Header="Nơi làm việc" DataIndex="WorkPlace" Sortable="false">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="TextFieldWorkPlace" AnchorHorizontal="100%" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="IDNumber" Header="Số CMT" Width="100" DataIndex="IDNumber" Sortable="true">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="TextFieldIDNumber" AnchorHorizontal="100%" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="IsDependent" Header="Là người phụ thuộc" Width="110" Align="Center"
                                                            DataIndex="IsDependent">
                                                            <Renderer Fn="GetRendererTrueFalse" />
                                                            <Editor>
                                                                <ext:Checkbox runat="server" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="Note" Width="200" Header="Quê quán, nghề nghiệp, chức danh, chức vụ, học tập, nơi ở(Trong ngoài nước);Thành viên tổ chức chính trị- xã hội"
                                                            DataIndex="Note" Sortable="false">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="txtNote_FamilyRelationship" AnchorHorizontal="100%" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModelQHGD" runat="server" SingleSelect="true">
                                                        <Listeners>
                                                            <RowSelect Handler="btnDelete.enable();" />
                                                            <RowDeselect Handler="btnDelete.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="DienBienLuong">29) Diễn biến lương <span style='color: red'>*</span></a></p>
                                            <ext:Hidden ID="hdfSalaryQuantumGrid" runat="server" />
                                            <ext:Hidden ID="hdfSalaryGroupQuantumGrid" runat="server" />
                                            <ext:Hidden ID="hdfSalaryGradeGrid" runat="server" />
                                            <ext:GridPanel ID="GridPanelSalary" runat="server" Width="745" Height="260"
                                                AutoExpandColumn="Note" AutoExpandMin="100">
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button ID="Button13" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridPanelSalary}.addRecord(); #{GridPanelSalary}.getView().focusRow(rowIndex); #{GridPanelSalary}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button14" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridPanelSalary}.deleteSelected(); #{GridPanelSalary}.save();if (!#{GridPanelSalary}.hasSelection()) {#{Button14}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel4">
                                                        <Listeners>
                                                            <RowSelect Handler="Button14.enable();" />
                                                            <RowDeselect Handler="Button14.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <Store>
                                                    <ext:Store ID="StoreSalary" ShowWarningOnFailure="false" SkipIdForNewRecords="false" OnBeforeStoreChanged="HandleChangesSalary"
                                                        RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="StoreSalary_OnRefreshData" runat="server">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="Name" />
                                                                    <ext:RecordField Name="DecisionMaker" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="EffectiveDate" />
                                                                    <ext:RecordField Name="EffectiveEndDate" />
                                                                    <ext:RecordField Name="SalaryPayDate" />
                                                                    <ext:RecordField Name="QuantumName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="SalaryGrade" />
                                                                    <ext:RecordField Name="SalaryFactor" DefaultValue="0" />
                                                                    <ext:RecordField Name="SalaryBasic" DefaultValue="0" />
                                                                    <ext:RecordField Name="SalaryInsurance" DefaultValue="0" />
                                                                    <ext:RecordField Name="OtherAllowance" DefaultValue="0" />
                                                                    <ext:RecordField Name="PositionAllowance" DefaultValue="0" />
                                                                    <ext:RecordField Name="OutFrame" DefaultValue="0" />
                                                                    <ext:RecordField Name="QuantumId" DefaultValue="0" />
                                                                    <ext:RecordField Name="GroupQuantumId" DefaultValue="0" />
                                                                    <ext:RecordField Name="ContractId" DefaultValue="0" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="Status" />
                                                                    <ext:RecordField Name="QuantumCode" />
                                                                    <ext:RecordField Name="ResponsibilityAllowance" DefaultValue="0" />
                                                                    <ext:RecordField Name="AreaAllowance" DefaultValue="0" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel5" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column ColumnID="DecisionNumber" Width="95" Header="Số quyết định" DataIndex="DecisionNumber">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="130">
                                                                </ext:TextField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="QuantumName" Width="105" Header="Tên ngạch" DataIndex="QuantumName"
                                                            Align="Right">
                                                            <Renderer Fn="ngachRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxQuantum" CtCls="requiredData" TabIndex="132"
                                                                    DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                                                                    AnchorHorizontal="100%" Editable="true" ListWidth="250" AllowBlank="false" PageSize="15"
                                                                    MinChars="1">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template54" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                    <div class="list-item"> 
							                                                    {Name}
						                                                    </div>
					                                                    </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store ID="storeQuantum" runat="server" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerQuantum.ashx" />
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
                                                                    </Store>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show(); hdfSalaryQuantumGrid.setValue(this.getValue()); cbxSalaryGrade.clearValue(); cbxSalaryGrade.enable(); cbxSalaryGradeStore.reload();hdfSalaryGradeGrid.reset(); updateSalaryProcess('QuantumId', this.getValue()); Ext.net.DirectMethods.getQuantumCode();
                                                                        if(cbxSalaryGrade.getValue() != ''){Ext.net.DirectMethods.GetSalaryInfoGrid();}" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryQuantumGrid.reset();hdfSalaryGradeGrid.reset();txtColQuantumCode.reset(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="QuantumCode" Width="100" Header="Mã ngạch" DataIndex="QuantumCode"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="txtColQuantumCode" AnchorHorizontal="98%">
                                                                </ext:TextField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="SalaryGrade" Width="95" Header="Bậc lương" DataIndex="SalaryGrade"
                                                            Align="Right">
                                                            <Renderer Fn="bacRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxSalaryGrade" CtCls="requiredData" Width="235" Disabled="true"
                                                                    DisplayField="TEN" ValueField="MA" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
                                                                    ReadOnly="false">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template13" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                    <div class="list-item"> 
							                                                    {TEN}
						                                                    </div>
					                                                    </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="cbxSalaryGradeStore" AutoLoad="false" OnRefreshData="cbxSalaryGradeStore_OnRefreshData">
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
                                                                        <Select Handler="this.triggers[0].show(); hdfSalaryGradeGrid.setValue(this.getValue()); updateSalaryProcess('SalaryGrade', this.getValue());
                                                                        Ext.net.DirectMethods.GetSalaryInfoGrid();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryGradeGrid.reset();}" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="SalaryFactor" Width="100" Header="Hệ số lương" DataIndex="SalaryFactor"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField ID="nfDblHeSoLuong" runat="server" AllowNegative="false" TabIndex="131">
                                                                </ext:NumberField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="PositionAllowance" Width="120" Header="Phụ cấp chức vụ" DataIndex="PositionAllowance"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" AllowNegative="false" TabIndex="132">
                                                                </ext:NumberField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="OtherAllowance" Width="100" Header="Phụ cấp khác" DataIndex="OtherAllowance"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" AllowNegative="false" TabIndex="134">
                                                                </ext:NumberField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="ResponsibilityAllowance" Width="100" Header="Phụ cấp trách nhiệm" DataIndex="ResponsibilityAllowance"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" AllowNegative="false" TabIndex="134">
                                                                </ext:NumberField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="AreaAllowance" Width="100" Header="Phụ cấp khu vực" DataIndex="AreaAllowance"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" AllowNegative="false" TabIndex="134">
                                                                </ext:NumberField>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Width="125" ColumnID="DecisionDate" Header="Ngày quyết định"
                                                            DataIndex="DecisionDate">
                                                            <Editor>
                                                                <ext:DateField ID="DateField1" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy"
                                                                    Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" TabIndex="135">
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="Đề nghị nhập đầy đủ ngày quyết định" />
                                                                    </ToolTips>
                                                                    <Listeners>
                                                                        <Blur Handler="addDatetoDF2();" />
                                                                    </Listeners>
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Width="115" ColumnID="EffectiveDate" Header="Ngày có hiệu lực <span style='color:red'>*</span>"
                                                            DataIndex="EffectiveDate">
                                                            <Editor>
                                                                <ext:DateField ID="DateField2" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy" CtCls="requiredData"
                                                                    Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" TabIndex="136">
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="Đề nghị nhập đầy đủ ngày có hiệu lực" />
                                                                    </ToolTips>
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn ColumnID="SalaryPayDate" Width="120" Header="Ngày hưởng lương" Format="dd/MM/yyyy"
                                                            DataIndex="SalaryPayDate">
                                                            <Editor>
                                                                <ext:DateField ID="DateField3" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy"
                                                                    Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" TabIndex="137">
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column ColumnID="SalaryInsurance" Width="150" Header="Mức lương đóng BHXH" DataIndex="SalaryInsurance"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" AllowNegative="false" TabIndex="133" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="DecisionMaker" Width="110" Header="Người quyết định" DataIndex="DecisionMaker">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="138" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column ColumnID="Note" Width="100" Header="Ghi chú" DataIndex="Note">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="139" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                            </ext:GridPanel>
                                            <p class="label"><a name="QuaTrinhDaoTao">30) Quá trình đào tạo</a></p>
                                            <i>Theo dõi các khóa đào tạo trong thời gian ngắn vài tháng</i>
                                            <ext:GridPanel ID="gridTrainingHistory" runat="server" Width="745" Height="260">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeTrainingHistory" AutoSave="false" OnBeforeStoreChanged="HandleChangesTrainingHistory"
                                                        ShowWarningOnFailure="false" SkipIdForNewRecords="false" AutoLoad="false" OnRefreshData="storeTrainingHistory_RefreshData" RefreshAfterSaving="None">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" DefaultValue="0" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="TrainingName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="StartDate" />
                                                                    <ext:RecordField Name="EndDate" />
                                                                    <ext:RecordField Name="NationId" DefaultValue="0" />
                                                                    <ext:RecordField Name="NationName" />
                                                                    <ext:RecordField Name="Reason" />
                                                                    <ext:RecordField Name="TrainingPlace" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="TrainingSystemId" DefaultValue="0" />
                                                                    <ext:RecordField Name="TrainingSystemName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button ID="Button5" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{gridTrainingHistory}.addRecord();#{gridTrainingHistory}.getView().focusRow(rowIndex); #{gridTrainingHistory}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button6" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{gridTrainingHistory}.deleteSelected(); #{gridTrainingHistory}.save();if (!#{gridTrainingHistory}.hasSelection()) {#{Button6}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelQuaTrinhDaoTao">
                                                        <Listeners>
                                                            <RowSelect Handler="Button6.enable(); " />
                                                            <RowDeselect Handler="if (!#{gridTrainingHistory}.hasSelection()) {#{Button6}.disable();}" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="45" />
                                                        <ext:Column Header="Số QĐ" DataIndex="DecisionNumber" Width="100" Sortable="true"
                                                            Align="Left">
                                                            <Editor>
                                                                <ext:TextField ID="TextField2" runat="server" TabIndex="140" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:DateColumn Header="Ngày QĐ" DataIndex="DecisionDate" Width="100" Sortable="true" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="DateField5" TabIndex="140" CtCls="requiredData" Format="dd/MM/yyyy" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Tên khóa <span style='color:red'>*</span>" DataIndex="TrainingName" Width="250" Sortable="true"
                                                            Align="Left">
                                                            <Editor>
                                                                <ext:TextField ID="txtTenKhoaDaoTao" runat="server" TabIndex="140" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Quốc gia" DataIndex="NationName" Width="100" Sortable="true"
                                                            Align="Left">
                                                            <Renderer Fn="nationRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxTEN" DisplayField="Name" MinChars="1" ValueField="Id" TabIndex="141"
                                                                    AnchorHorizontal="100%" ListWidth="200" Editable="true" ItemSelector="div.list-item" StoreID="storeNation"
                                                                    PageSize="15">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template52" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                    <div class="list-item"> 
							                                                    {Name}
						                                                    </div>
					                                                    </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateQTDaoTao('NationId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:DateColumn Header="Ngày bắt đầu" DataIndex="StartDate" Width="100" Sortable="true" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="EditdfNgayBatDau" TabIndex="142" CtCls="requiredData" Format="dd/MM/yyyy" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Header="Ngày kết thúc" DataIndex="EndDate" Width="100" Sortable="true" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="EditdfNgayKetThuc" TabIndex="143" CtCls="requiredData" Format="dd/MM/yyyy" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Loại hình đào tạo" DataIndex="TrainingSystemName" Width="100" Sortable="true"
                                                            Align="Left">
                                                            <Renderer Fn="loaiHinhDaoTaoRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxLoaiHinhDaoTao" DisplayField="Name" MinChars="1" ValueField="Id" TabIndex="141"
                                                                    AnchorHorizontal="100%" ListWidth="200" Editable="true" ItemSelector="div.list-item" StoreID="cbxLoaiHinhDaoTaoStore"
                                                                    PageSize="15">
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
                                                                        <Expand Handler="if(cbxLoaiHinhDaoTao.store.getCount()==0) cbxLoaiHinhDaoTaoStore.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateQTDaoTao('TrainingSystemId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Lý do đào tạo" Width="150" DataIndex="Reason">
                                                            <Editor>
                                                                <ext:TextField ID="EditLyDoDaoTao" runat="server" TabIndex="144" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Nơi đào tạo" Width="150" DataIndex="TrainingPlace">
                                                            <Editor>
                                                                <ext:TextField ID="EditNoiDaoTao" runat="server" TabIndex="145" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Ghi chú" DataIndex="Note" Width="295" Sortable="true"
                                                            Align="Left">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="EditGhiCHu" TabIndex="146" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="">31) Nguồn thu nhập của gia đình (hàng năm)</a></p>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" MaskRe="/[0-9]/" ID="txtFamilyIncome" FieldLabel="<b>31.1 - Lương</b>" LabelWidth="200">
                                                            <Listeners>
                                                                <Blur Fn="RenderFormat"></Blur>
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtOtherIncome" FieldLabel="<b>Các nguồn khác</b>" LabelWidth="150"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtAllocatedHouse" FieldLabel="<b>31.2 - Nhà được cấp, được thuê, loại nhà</b>" LabelWidth="200"/>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:TextField runat="server" MaskRe="/[0-9.,]/" ID="txtAllocatedHouseArea" FieldLabel="<b>Tổng diện tích sử dụng</b>"  EmptyText="Đơn vị m2" LabelWidth="150"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtHouse" FieldLabel="<b>31.3 - Nhà tự mua, tự xây, loại nhà</b>" LabelWidth="200"/>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:TextField runat="server" MaskRe="/[0-9.,]/" ID="txtHouseArea" FieldLabel="<b>Tổng diện tích sử dụng</b>"  EmptyText="Đơn vị m2" LabelWidth="150"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" MaskRe="/[0-9.,]/" ID="txtAllocatedLandArea" FieldLabel="<b>31.4 - Diện tích đất được cấp</b>" EmptyText="Đơn vị m2" LabelWidth="200"/>
                                                    </td>
                                                    <td style="width: 7px;"></td>
                                                    <td>
                                                        <ext:TextField runat="server" MaskRe="/[0-9.,]/" ID="txtLandArea" FieldLabel="<b>Diện tích đất tự mua</b>" EmptyText="Đơn vị m2" LabelWidth="150"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ext:TextField runat="server" ID="txtBusinessLand" FieldLabel="<b>31.5 - Đất sản xuất, kinh doanh</b>" LabelWidth="200"/>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <p class="label"><a name="NangLucSoTruong">32) Năng lực, sở trường</a></p>
                                            <ext:GridPanel ID="gridAbility" runat="server" Width="745" Height="260" AutoExpandColumn="Note">
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button ID="Button7" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{gridAbility}.addRecord(); #{gridAbility}.getView().focusRow(rowIndex); #{gridAbility}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button8" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{gridAbility}.deleteSelected(); #{gridAbility}.save();if (!#{gridAbility}.hasSelection()) {#{Button8}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="storeAbility" runat="server" ShowWarningOnFailure="false" SkipIdForNewRecords="false" OnBeforeStoreChanged="HandleChangesAbility"
                                                        RefreshAfterSaving="None" AutoLoad="false" OnRefreshData="storeAbility_OnRefreshData">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="AbilityId" DefaultValue="0" />
                                                                    <ext:RecordField Name="AbilityName" />
                                                                    <ext:RecordField Name="GraduationTimeSheetHandlerTypeId" DefaultValue="0" />
                                                                    <ext:RecordField Name="GraduationTimeSheetHandlerTypeName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelKhaNang">
                                                        <Listeners>
                                                            <RowSelect Handler="Button8.enable();" />
                                                            <RowDeselect Handler="Button8.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn />
                                                        <ext:Column Header="Khả năng <span style='color:red'>*</span>" DataIndex="AbilityName" Width="250" Sortable="true"
                                                            Align="Left">
                                                            <Renderer Fn="KhaNangRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbKhaNang" DisplayField="Name" TabIndex="150" PageSize="15"
                                                                    ValueField="Id" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item">
                                                                    <Store>
                                                                        <ext:Store ID="store_CbxAbility" runat="server" AutoLoad="false">
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
                                                                    <Template ID="Template38" runat="server">
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
                                                                        <Select Handler="this.triggers[0].show();updateGridNangLucST('AbilityId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Mức đạt" DataIndex="GraduationTimeSheetHandlerTypeName" Width="200" Sortable="true"
                                                            Align="Left">
                                                            <Renderer Fn="MucDatRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbKhaNangXepLoai" DisplayField="Name" TabIndex="151" PageSize="15"
                                                                    ValueField="Id" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item">
                                                                    <Store>
                                                                        <ext:Store ID="cbKhaNangXepLoaiStore" runat="server" AutoLoad="false">
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
                                                                    </Store>
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template40" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateGridNangLucST('GraduationTimeSheetHandlerTypeId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="<b>Ghi chú</b>" DataIndex="Note" Width="325" Sortable="true"
                                                            Align="Left">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="txtNote_Ability" TabIndex="152" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="ThamGiaBaoHiem">33) Quá trình tham gia bảo hiểm</a></p>
                                            <ext:GridPanel ID="GridInsurance" runat="server" Width="745" Height="260">
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button ID="Button15" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridInsurance}.addRecord(); #{GridInsurance}.getView().focusRow(rowIndex); #{GridInsurance}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button16" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridInsurance}.deleteSelected(); #{GridInsurance}.save();if (!#{GridInsurance}.hasSelection()) {#{Button16}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel5">
                                                        <Listeners>
                                                            <RowSelect Handler="Button16.enable();" />
                                                            <RowDeselect Handler="Button16.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <Store>
                                                    <ext:Store ID="storeInsurance" runat="server" SkipIdForNewRecords="false" ShowWarningOnFailure="false" AutoSave="false" AutoLoad="false"
                                                        OnBeforeStoreChanged="HandlerChangesInsurance" OnRefreshData="storeInsurance_OnRefreshData" RefreshAfterSaving="None">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="SalaryFactor" DefaultValue="0" />
                                                                    <ext:RecordField Name="Allowance" DefaultValue="0" />
                                                                    <ext:RecordField Name="SalaryLevel" DefaultValue="0" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="Rate" />
                                                                    <ext:RecordField Name="FromDate" />
                                                                    <ext:RecordField Name="ToDate" />
                                                                    <ext:RecordField Name="PositionName" />
                                                                    <ext:RecordField Name="PositionId" DefaultValue="0" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <View>
                                                    <ext:GridView ID="GridView7" runat="server" MarkDirty="false" />
                                                </View>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn />
                                                        <ext:DateColumn Header="Từ ngày" DataIndex="FromDate" Width="90" Sortable="true" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfFromDate_Insurance" TabIndex="160" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Header="Đến ngày" DataIndex="ToDate" Width="90" Sortable="true" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfToDate_Insurance" TabIndex="161" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Chức vụ" DataIndex="PositionName" Width="200" Sortable="true"
                                                            Align="Left">
                                                            <Renderer Fn="PositionRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbx_Position" DisplayField="Name" TabIndex="162"
                                                                    PageSize="15" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true"
                                                                    ItemSelector="div.list-item" LoadingText="Đang tải dữ liệu">
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
                                                                    <Store>
                                                                        <ext:Store ID="StorePosition" runat="server" AutoLoad="false">
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
                                                                    </Store>
                                                                    <Listeners>
                                                                        <Expand Handler="StorePosition.reload();" />
                                                                        <Select Handler="this.triggers[0].show(); updateInsurance('PositionId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Hs Lương" DataIndex="SalaryFactor" Width="90" Sortable="true"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" MaskRe="/[0-9.,-]/" ID="txtSalaryFactorInsurance" TabIndex="163" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Phụ cấp" DataIndex="Allowance" Width="90" Sortable="true"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" ID="txtAllowanceInsurance" MaskRe="/[0-9.,-]/" TabIndex="164" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Mức lương" DataIndex="SalaryLevel" Width="90" Sortable="true"
                                                            Align="Right">
                                                            <Editor>
                                                                <ext:NumberField runat="server" ID="txtSalaryLevelInsurance" MaskRe="/[0-9.,]/" MaxLength="11" MinValue="0" TabIndex="165" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Ghi Chú" DataIndex="Note" Width="375" Sortable="true"
                                                            Align="Left">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="txtNoteInsurance" TabIndex="166" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                            <p class="label">34) Khen thưởng, Kỷ luật</p>

                                            <p class="label"><a name="KhenThuong">a. Khen thưởng</a></p>

                                            <ext:GridPanel ID="GridReward" TrackMouseOver="true" Header="false" runat="server"
                                                Width="745" Height="260" AutoExpandColumn="Note">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" ID="tb">
                                                        <Items>
                                                            <ext:Button ID="Button17" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridReward}.addRecord();#{GridReward}.getView().focusRow(rowIndex); #{GridReward}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button18" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridReward}.deleteSelected(); #{GridReward}.save();if (!#{GridReward}.hasSelection()) {#{Button18}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store runat="server" ID="storeReward" ShowWarningOnFailure="false"
                                                        OnBeforeStoreChanged="storeReward_BeforeStoreChanged" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                                                        AutoLoad="false" OnRefreshData="storeReward_RefreshData">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="Name" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="EffectiveDate" />
                                                                    <ext:RecordField Name="Reason" />
                                                                    <ext:RecordField Name="FormRewardName" />
                                                                    <ext:RecordField Name="MoneyAmount" DefaultValue="0" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="DecisionMaker" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                    <ext:RecordField Name="Point" DefaultValue="0" />
                                                                    <ext:RecordField Name="LevelRewardName" />
                                                                    <ext:RecordField Name="LevelRewardId" DefaultValue="0" />
                                                                    <ext:RecordField Name="FormRewardId" DefaultValue="0" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="45" Align="Right" Locked="true" />
                                                        <ext:Column Header="Cấp khen thưởng" Width="200" Align="Left" DataIndex="LevelRewardName">
                                                            <Renderer Fn="LevelRewardDisciplineRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxCapKhenThuong" DisplayField="Name" TabIndex="170" PageSize="15"
                                                                    ValueField="Id" AnchorHorizontal="99%" Editable="true" ItemSelector="div.list-item" StoreID="StoreLevelRewardDiscipline"
                                                                    LoadingText="Đang tải dữ liệu...">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template47" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateKhenThuong('LevelRewardId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();}" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Số quyết định" Width="85" Align="Left" DataIndex="DecisionNumber">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="171" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:DateColumn Header="Ngày quyết định <span style='color:red'>*</span>" Width="110" Align="Center" DataIndex="DecisionDate" Format="dd/MM/yyyy">
                                                            <Editor>
                                                                <ext:DateField runat="server" TabIndex="172" CtCls="requiredData" ID="dfNgayQDKT" Format="dd/MM/yyyy">
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="Nhập vào ngày quyết định khen thưởng" />
                                                                    </ToolTips>
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Hình thức" Width="200" Align="Left" DataIndex="FormRewardName">
                                                            <Renderer Fn="HinhThucKhenThuongRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbHinhThucKhenThuong" DisplayField="Name" ValueField="Id" TabIndex="173" MinChars="1"
                                                                    AnchorHorizontal="100%" Editable="true" ItemSelector="div.list-item">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template48" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store ID="store_CbxFormReward" runat="server" AutoLoad="false">
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
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateKhenThuong('FormRewardId',this.getValue());" />
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Ghi chú" Width="200" Align="Left" DataIndex="Note">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="176" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <View>
                                                    <ext:GridView ID="GridView8" runat="server" MarkDirty="false" />
                                                </View>
                                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel6">
                                                        <Listeners>
                                                            <RowSelect Handler="Button18.enable();" />
                                                            <RowDeselect Handler="Button18.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="KyLuat">b. Kỷ luật</a></p>
                                            <ext:GridPanel ID="GridDiscipline" TrackMouseOver="true" Header="false" runat="server"
                                                Width="745" Height="260" AutoExpandColumn="Note">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" ID="Toolbar2">
                                                        <Items>
                                                            <ext:Button ID="Button19" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridDiscipline}.addRecord();#{GridDiscipline}.getView().focusRow(rowIndex); #{GridDiscipline}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button20" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridDiscipline}.deleteSelected();#{GridDiscipline}.save(); if (!#{GridDiscipline}.hasSelection()) {#{Button20}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store runat="server" ID="storeDiscipline" AutoSave="false" AutoLoad="false" SkipIdForNewRecords="false" ShowWarningOnFailure="false"
                                                        OnRefreshData="storeDiscipline_RefreshData" OnBeforeStoreChanged="storeDiscipline_BeforeStoreChanged">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="Reason" />
                                                                    <ext:RecordField Name="FormDisciplineName" />
                                                                    <ext:RecordField Name="MoneyAmount" DefaultValue="0" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="DecisionMaker" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                    <ext:RecordField Name="Point" DefaultValue="0" />
                                                                    <ext:RecordField Name="LevelDisciplineName" />
                                                                    <ext:RecordField Name="LevelDisciplineId" DefaultValue="0" />
                                                                    <ext:RecordField Name="FormDisciplineId" DefaultValue="0" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="45" Align="Right" Locked="true" />
                                                        <ext:Column Header="Cấp kỷ luật" Width="200" Align="Left" DataIndex="LevelDisciplineName">
                                                            <Renderer Fn="LevelRewardDisciplineRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="ComboBox3" DisplayField="Name" CtCls="requiredData" PageSize="15"
                                                                    ValueField="Id" AnchorHorizontal="99%" Editable="true" MinChars="1" ItemSelector="div.list-item" StoreID="StoreLevelRewardDiscipline"
                                                                    LoadingText="Đang tải dữ liệu..." TabIndex="180">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template49" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateKyLuat('LevelDisciplineId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();}" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Số quyết định" Width="85" Align="Left" DataIndex="DecisionNumber">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="181" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:DateColumn Header="Ngày quyết định <span style='color:red'>*</span>" Width="110" Align="Center" DataIndex="DecisionDate" Format="dd/MM/yyyy">
                                                            <Renderer Fn="Ext.util.Format.dateRenderer('dd/MM/yyyy')" />
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfNgayQDKL" TabIndex="182" CtCls="requiredData">
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="Nhập vào ngày quyết định kỷ luật" />
                                                                    </ToolTips>
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Hình thức" Width="200" Align="Left" DataIndex="FormDisciplineName">
                                                            <Renderer Fn="HinhThucKyLuatRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxFormDiscipline" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                                                    AnchorHorizontal="100%" Editable="true" MinChars="1" ItemSelector="div.list-item" TabIndex="183">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template50" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store ID="store_CbxFormDiscipline" runat="server" AutoLoad="false">
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
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateKyLuat('FormDisciplineId',this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Ghi chú" Width="200" Align="Left" DataIndex="Note">
                                                            <Editor>
                                                                <ext:TextField runat="server" TabIndex="186" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <View>
                                                    <ext:GridView ID="GridView9" runat="server" MarkDirty="false" />
                                                </View>
                                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel7">
                                                        <Listeners>
                                                            <RowSelect Handler="Button20.enable();" />
                                                            <RowDeselect Handler="Button20.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="CtacTaiDonVi">35) Quá trình công tác tại đơn vị</a></p>
                                            <ext:GridPanel ID="GridWorkProcess" runat="server" Width="745" Height="270" ClicksToEdit="2" AutoExpandColumn="Note"
                                                AutoExpandMin="100" StripeRows="true" TrackMouseOver="true">
                                                <Store>
                                                    <ext:Store ID="StoreWorkProcess" AutoSave="false" ShowWarningOnFailure="false"
                                                        OnBeforeStoreChanged="HandleChangesWorkProcess" SkipIdForNewRecords="false" RefreshAfterSaving="None"
                                                        AutoLoad="false" OnRefreshData="StoreWorkProcess_OnRefreshData" runat="server">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="DecisionMaker" />
                                                                    <ext:RecordField Name="EffectiveDate" />
                                                                    <ext:RecordField Name="EffectiveEndDate" />
                                                                    <ext:RecordField Name="NewDepartmentId" DefaultValue="0" />
                                                                    <ext:RecordField Name="NewDepartmentName" />
                                                                    <ext:RecordField Name="OldDepartmentId" DefaultValue="0" />
                                                                    <ext:RecordField Name="OldDepartmentName" />
                                                                    <ext:RecordField Name="OldJobId" DefaultValue="0" />
                                                                    <ext:RecordField Name="OldJobName" />
                                                                    <ext:RecordField Name="NewJobId" DefaultValue="0" />
                                                                    <ext:RecordField Name="NewJobName" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="IsApproved" DefaultValue="false" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="NewPositionId" DefaultValue="0" />
                                                                    <ext:RecordField Name="NewPositionName" />
                                                                    <ext:RecordField Name="OldPositionId" DefaultValue="0" />
                                                                    <ext:RecordField Name="OldPositionName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel13" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="45" />
                                                        <ext:DateColumn Format="MM/yyyy" ColumnID="EffectiveDate" Width="100" Header="Từ tháng/năm"
                                                            DataIndex="EffectiveDate">
                                                            <Editor>
                                                                <ext:DateField ID="dfNgayQuyetDinh" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="MM/yyyy"
                                                                    Regex="/^(1[0-2]|0?[0-9])\/[0-9]{4}$/" TabIndex="192">
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="MM/yyyy" ColumnID="EffectiveEndDate" Width="100" Header="Đến tháng/năm"
                                                            DataIndex="EffectiveEndDate">
                                                            <Editor>
                                                                <ext:DateField ID="DateField4" runat="server" AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="MM/yyyy"
                                                                    Regex="/^(1[0-2]|0?[0-9])\/[0-9]{4}$/" TabIndex="193">
                                                                </ext:DateField>
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column ColumnID="Note" Header="Công tác tại bộ phận, chức vụ, chức danh" Width="200" DataIndex="Note">
                                                            <Editor>
                                                                <ext:TextField runat="server" ID="TextField7" AnchorHorizontal="100%" TabIndex="198" />
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModelQuaTrinhDieuChuyen" runat="server" SingleSelect="true">
                                                        <Listeners>
                                                            <RowSelect Handler="Button4.enable();" />
                                                            <RowDeselect Handler="Button4.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <View>
                                                    <ext:GridView ID="GridView10" runat="server" MarkDirty="false" />
                                                </View>
                                                <TopBar>
                                                    <ext:Toolbar runat="server" ID="tbQuaTrinhDieuChuyen">
                                                        <Items>
                                                            <ext:Button ID="Button3" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridWorkProcess}.addRecord(); #{GridWorkProcess}.getView().focusRow(rowIndex); #{GridWorkProcess}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="Button4" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridWorkProcess}.deleteSelected(); #{GridWorkProcess}.save(); if (!#{GridWorkProcess}.hasSelection()) {#{Button4}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                            </ext:GridPanel>
                                            <p class="label"><a name="HDLD">36) Hợp đồng lao động <span style='color: red'>*</span></a></p>
                                            <ext:GridPanel ID="GridContract" runat="server" Width="745" Height="260">
                                                <TopBar>
                                                    <ext:Toolbar runat="server">
                                                        <Items>
                                                            <ext:Button ID="Button11" runat="server" Text="Thêm" Icon="Add">
                                                                <Listeners>
                                                                    <Click Handler="var rowIndex = #{GridContract}.addRecord(); #{GridContract}.getView().focusRow(rowIndex); #{GridContract}.startEditing(rowIndex, 1);" />
                                                                </Listeners>
                                                            </ext:Button>
                                                            <ext:Button ID="btnDelHopDong" runat="server" Text="Xóa" Icon="Delete" Disabled="true">
                                                                <Listeners>
                                                                    <Click Handler="#{GridContract}.deleteSelected(); #{GridContract}.save(); if (!#{GridContract}.hasSelection()) {#{btnDelHopDong}.disable();}" />
                                                                </Listeners>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel3">
                                                        <Listeners>
                                                            <RowSelect Handler="btnDelHopDong.enable(); hdfKeyContract.setValue(RowSelectionModel3.getSelected().get('Id'));" />
                                                            <RowDeselect Handler="if (!#{GridContract}.hasSelection()) {#{btnDelHopDong}.disable();};hdfKeyContract.reset(); " />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <Store>
                                                    <ext:Store ID="storeContract" runat="server" AutoSave="false" OnRefreshData="storeContract_OnRefreshData" RefreshAfterSaving="None"
                                                        AutoLoad="false" ShowWarningOnFailure="false" WarningOnDirty="False" OnBeforeStoreChanged="HandleChangesContract_BeforeStoreChanged">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" DefaultValue="0" />
                                                                    <ext:RecordField Name="ContractNumber" />
                                                                    <ext:RecordField Name="ContractTimeSheetHandlerTypeId" DefaultValue="0" />
                                                                    <ext:RecordField Name="ContractTimeSheetHandlerTypeName" />
                                                                    <ext:RecordField Name="ContractStatusId" DefaultValue="0" />
                                                                    <ext:RecordField Name="ContractStatusName" />
                                                                    <ext:RecordField Name="SalaryId" DefaultValue="0" />
                                                                    <ext:RecordField Name="JobId" DefaultValue="0" />
                                                                    <ext:RecordField Name="JobName" />
                                                                    <ext:RecordField Name="ContractCondition" />
                                                                    <ext:RecordField Name="ContractDate" />
                                                                    <ext:RecordField Name="ContractEndDate" />
                                                                    <ext:RecordField Name="EffectiveDate" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="PersonRepresent" />
                                                                    <ext:RecordField Name="PersonPositionId" DefaultValue="0" />
                                                                    <ext:RecordField Name="PersonPositionName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <View>
                                                    <ext:GridView ID="GridView1" runat="server" MarkDirty="false" />
                                                </View>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn />
                                                        <ext:Column Header="Số hợp đồng" DataIndex="ContractNumber" Width="145" Sortable="false">
                                                            <Editor>
                                                                <ext:TextField runat="server" />
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:Column Header="Loại hợp đồng <span style='color:red'>*</span>" DataIndex="ContractTimeSheetHandlerTypeName" Width="150" Sortable="false"
                                                            Align="Left">
                                                            <Renderer Fn="LoaiHopDongRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxContract" DisplayField="Name"
                                                                    ItemSelector="div.list-item" Editable="false" ValueField="Id" AnchorHorizontal="99%">
                                                                    <Store>
                                                                        <ext:Store ID="store_CbxContract" runat="server" AutoLoad="false">
                                                                            <Proxy>
                                                                                <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                                                            </Proxy>
                                                                            <BaseParams>
                                                                                <ext:Parameter Name="objname" Value="cat_ContractTimeSheetHandlerType" Mode="Value" />
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
                                                                    <Template ID="Template43" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Expand Handler="store_CbxContract.reload();" />
                                                                        <Select Handler="this.triggers[0].show();updateContract('ContractTimeSheetHandlerTypeId', this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                        <ext:DateColumn Header="Ngày ký <span style='color:red'>*</span>" DataIndex="ContractDate" Width="90" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfNgayKy_HopDongLaoDong" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Header="Ngày kết thúc" DataIndex="ContractEndDate" Width="90" Format="dd/MM/yyyy"
                                                            Align="Center">
                                                            <Editor>
                                                                <ext:DateField runat="server" ID="dfNgayKetThuc_HopDongLaoDong" />
                                                            </Editor>
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Tình trạng <span style='color:red'>*</span>" DataIndex="ContractStatusName" Width="130" Sortable="true"
                                                            Align="Left">
                                                            <Renderer Fn="TinhTrangHopDongRenderer" />
                                                            <Editor>
                                                                <ext:ComboBox runat="server" ID="cbxContractStatus" DisplayField="Name"
                                                                    ItemSelector="div.list-item" Editable="false" ValueField="Id"
                                                                    AnchorHorizontal="99%">
                                                                    <Store>
                                                                        <ext:Store runat="server" AutoLoad="false" ID="store_CbxContractStatus">
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
                                                                    <Template ID="Template45" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Listeners>
                                                                        <Select Handler="this.triggers[0].show();updateContract('ContractStatusId', this.getValue());" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                            <p class="label"><a name="DanhGia">37) Nhận xét, đánh giá của đơn vị quản lý sử dụng cán bộ, công chức</a></p>
                                            <ext:TextArea runat="server" ID="txtReview" Height="70" Width="745" />
                                            <div>
                                                <br />
                                                <p class="label">38.1) Người liên hệ khi cần</p>
                                                <ext:TextArea runat="server" ID="txtContactPersonName" Height="70" Width="745" />
                                                <p class="label">38.2) Mối quan hệ với cán bộ</p>
                                                <ext:TextArea runat="server" ID="txtContactRelation" Height="70" Width="745" />
                                                <br />
                                                <ext:TextField ID="txtContactPhoneNumber" runat="server" MaxLength="50" Width="500" LabelWidth="200" FieldLabel="<b>38.3) Số điện thoại(di động)</b>" />
                                                <p class="label">38.4) Địa chỉ</p>
                                                <ext:TextArea runat="server" ID="txtContactAddress" Height="70" Width="745" />
                                            </div>
                                            <div style="height: 90px;"></div>
                                        </div>
                                        <div class="clr"></div>
                                    </div>
                                    <div class="col col-2">
                                        <div id="RightColumn">
                                            <div id="RightCommand">
                                                <span>Điều hướng nhanh</span>
                                                <ul>
                                                    <li><a href="#DauTrang">Đầu trang</a></li>
                                                    <li><a href="#SoYeuLyLich">Thông tin cá nhân</a></li>
                                                    <li><a href="#ThongTinCoBan">Thông tin cơ bản</a></li>
                                                    <li><a href="#DaoTaoBoiDuong">Đào tạo, bồi dưỡng về chuyên môn, nghiệp vụ</a></li>
                                                    <li><a href="#QuaTrinhCongTac">Tóm tắt quá trình công tác</a></li>
                                                    <li><a href="#LichSuBanThan">Đặc điểm lịch sử bản thân</a></li>
                                                    <li><a href="#QHGD">Quan hệ gia đình</a></li>
                                                    <li><a href="#DienBienLuong">Diễn biến lương</a></li>
                                                    <li><a href="#QuaTrinhDaoTao">Quá trình đào tạo</a></li>
                                                    <li><a href="#NangLucSoTruong">Năng lực sở trường</a></li>
                                                    <li><a href="#ThamGiaBaoHiem">Quá trình tham gia bảo hiểm</a></li>
                                                    <li><a href="#KhenThuong">Khen thưởng</a></li>
                                                    <li><a href="#KyLuat">Kỷ luật</a></li>
                                                    <li><a href="#CtacTaiDonVi">Quá trình công tác tại đơn vị</a></li>
                                                    <li><a href="#HDLD">Hợp đồng lao động</a></li>
                                                    <li><a href="#DanhGia">Nhận xét, đánh giá</a></li>
                                                </ul>
                                            </div>
                                            <div id="LeftCommand" class="w80">
                                                <a href="#" id="aLinkTaoMau" class="btn btn-primary btn-customer uppercase" title="Thực hiện tạo ra một mẫu lưu các trường dữ liệu tương đồng giữa các cán bộ để dùng cho nhiều cán bộ" onclick="wdCreateSample.show();">Tạo mẫu</a>
                                                <a href="#" id="aLinkSuDungMau" class="btn btn-primary btn-customer uppercase" title="Chọn và sử dụng các mẫu đã được tạo từ trước." onclick="ucSample1_wdSampleList.show();">Sử dụng mẫu</a>
                                            </div>
                                            <div id="BottomCommand" class="w80">
                                                <div id="ButtonSave" class="btn-customer">
                                                    <ext:ImageButton ID="iBtnSave" runat="server" ImageUrl="../../Resource/images/LuuVaGiuNguyen.png">
                                                        <Listeners>
                                                            <Click Handler="return ValidateInput();" />
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Click OnEvent="iBtnSave_Click">
                                                                <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="Reset" Value="False">
                                                                    </ext:Parameter>
                                                                    <ext:Parameter Name="jsonEducation" Value="getJsonOfStore(StoreEducation)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonSalary" Value="getJsonOfStore(StoreSalary)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonWorkHistory" Value="getJsonOfStore(storeWorkHistory)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonReward" Value="getJsonOfStore(storeReward)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonDiscipline" Value="getJsonOfStore(storeDiscipline)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonContract" Value="getJsonOfStore(storeContract)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonTrainingHistory" Value="getJsonOfStore(storeTrainingHistory)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonFamilyRelationship" Value="getJsonOfStore(storeFamilyRelationship)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonAbility" Value="getJsonOfStore(storeAbility)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonWorkProcess" Value="getJsonOfStore(StoreWorkProcess)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonInsurance" Value="getJsonOfStore(storeInsurance)" Mode="Raw" />
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:ImageButton>
                                                </div>
                                                <div id="ButtonSaveAndClear" class="btn-customer">
                                                    <ext:ImageButton ID="iBtnSaveAndClear" runat="server" ImageUrl="../../Resource/images/LuuVaXoaTrang.png">
                                                        <Listeners>
                                                            <Click Handler="return ValidateInput();" />
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Click OnEvent="iBtnSave_Click">
                                                                <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="Reset" Value="True">
                                                                    </ext:Parameter>
                                                                    <ext:Parameter Name="jsonEducation" Value="getJsonOfStore(StoreEducation)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonSalary" Value="getJsonOfStore(StoreSalary)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonWorkHistory" Value="getJsonOfStore(storeWorkHistory)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonReward" Value="getJsonOfStore(storeReward)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonDiscipline" Value="getJsonOfStore(storeDiscipline)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonContract" Value="getJsonOfStore(storeContract)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonTrainingHistory" Value="getJsonOfStore(storeTrainingHistory)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonFamilyRelationship" Value="getJsonOfStore(storeFamilyRelationship)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonAbility" Value="getJsonOfStore(storeAbility)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonWorkProcess" Value="getJsonOfStore(StoreWorkProcess)" Mode="Raw" />
                                                                    <ext:Parameter Name="jsonInsurance" Value="getJsonOfStore(storeInsurance)" Mode="Raw" />
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:ImageButton>
                                                </div>
                                                <div id="ButtonClear" class="btn-customer">
                                                    <ext:ImageButton ID="iBtnClear" runat="server" ImageUrl="../../Resource/images/XoaTrang.png">
                                                        <Listeners>
                                                            <Click Handler="ResetForm();" />
                                                        </Listeners>
                                                    </ext:ImageButton>
                                                    <ext:ImageButton ID="iBtnSaveSample" ToolTip="Bạn hãy nhập các thông tin theo mẫu trên rồi ấn lưu để hoàn thành việc tạo mẫu"
                                                        runat="server" Hidden="true" ImageUrl="../../Resource/images/LuuMau.png">
                                                        <DirectEvents>
                                                            <Click OnEvent="BtnSaveSample_Click">
                                                                <EventMask ShowMask="true" Msg="Bạn chờ trong giây lát, đang tiến hành lưu mẫu !" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:ImageButton>
                                                    <ext:ImageButton ID="iBtnCancelSample" ToolTip="Hủy việc tạo mẫu, sẽ không có bất cứ thông tin nào được thêm" Hidden="true" runat="server" ImageUrl="../../Resource/images/HuyMau.png">
                                                        <Listeners>
                                                            <Click Handler="iBtnCancelSample_Click()" />
                                                        </Listeners>
                                                    </ext:ImageButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clr"></div>
                                    </div>
                                </Content>
                            </ext:Panel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
