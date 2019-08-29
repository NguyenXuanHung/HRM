<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.UC.InputEmployee" CodeBehind="InputEmployee.ascx.cs" %>
<%@ Register Src="ResourceCommon.ascx" TagPrefix="UC" TagName="ResourceCommon" %>

<UC:ResourceCommon runat="server" ID="ResourceCommon" />
<script type="text/javascript" src="../../Resource/js/EmployeeSetting.js"></script>

<ext:Hidden runat="server" ID="hdfRecordId" />
<ext:Hidden runat="server" ID="hdfCandidateId" />
<ext:Hidden runat="server" ID="hdfImagePerson" />
<ext:Hidden runat="server" ID="hdfDataTable" />
<ext:Hidden runat="server" ID="hdfEnableCatalogGroup" />
<ext:Hidden runat="server" ID="hdfCurrentCatalogName" />
<ext:Hidden runat="server" ID="hdfCurrentCatalogGroupName" />
<ext:Hidden runat="server" ID="hdfWorkStatusId" />
<ext:Hidden runat="server" ID="hdfEven" />
<ext:Hidden runat="server" ID="hdfType" />
<ext:Hidden runat="server" ID="hdfRequiredRecruitmentStatus" />

<ext:Store ID="StoreGraduationType" runat="server" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
    </Proxy>
    <BaseParams>
        <ext:Parameter Name="objname" Value="cat_GraduationType" Mode="Value" />
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
<ext:Store runat="server" ID="storeCandidateStatus" OnRefreshData="storeCandidateStatus_OnRefreshData">
    <Reader>
        <ext:JsonReader IDProperty="Id">
            <Fields>
                <ext:RecordField Name="Id" Mapping="Key" />
                <ext:RecordField Name="Name" Mapping="Value" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Store runat="server" ID="storeRequiredRecruitment">
    <Proxy>
        <ext:HttpProxy Url="~/Services/Recruitment/HandlerRequiredRecruitment.ashx"></ext:HttpProxy>
    </Proxy>
    <BaseParams>
        <ext:Parameter Name="status" Value="#{hdfRequiredRecruitmentStatus}.getValue()" Mode="Raw" />
    </BaseParams>
    <Reader>
        <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
            <Fields>
                <ext:RecordField Name="Id"/>
                <ext:RecordField Name="Name"/>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window runat="server" Title="Thêm danh mục" Icon="Add" Layout="BorderLayout" Width="550" Modal="true" Hidden="true" Padding="6" Height="350" Constrain="true" ID="wdAddCategory" Resizable="true">
    <TopBar>
        <ext:Toolbar runat="server">
            <Items>
                <ext:Button runat="server" ID="btnAddCategory" Text="Thêm mới" Icon="Add">
                    <Listeners>
                        <Click Handler="#{btnAddCategory}.disable(); #{txtTenDM}.show(); #{btnSave}.show(); #{btnCancel}.show();" />
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
                        <Click Handler="#{DirectMethods}.ResetInputCategory();" />
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
                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="#{hdfTableDM}.getValue()" Mode="Raw" />
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
            <ColumnModel runat="server">
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
                <ext:PagingToolbar ID="PagingToolbarCategory" runat="server" PageSize="20">
                    <Listeners>
                        <Change Handler="#{RowSelectionModelCategory}.clearSelections();" />
                    </Listeners>
                </ext:PagingToolbar>
            </BottomBar>
        </ext:GridPanel>
    </Items>
    <Listeners>
        <Hide Handler="#{btnCancel}.hide(); #{btnSave}.hide();#{txtTenDM}.reset(); #{txtTenDM}.hide();#{btnAddCategory}.enable();" />
        <BeforeShow Handler="beforeShowWdCategoryHRM();" />
    </Listeners>
    <Buttons>
        <ext:Button runat="server" Text="Đồng ý chọn" Icon="Accept">
            <Listeners>
                <Click Handler="if(CheckSelectedRows(#{grpCategory})){selectedCategoryHRM();}" />
            </Listeners>
        </ext:Button>
        <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdAddCategory}.hide();" />
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
                        <Click Handler="#{btnAddCategoryGroup}.disable(); #{txtTenDMGroup}.show(); #{btnSaveGroupCategory}.show(); #{btnCancelGroupCategory}.show();if(#{hdfEnableCatalogGroup}.getValue()=='true') {#{cboCategoryGroup}.show(); #{storeGroup}.reload();}" />
                    </Listeners>
                </ext:Button>
                <ext:ToolbarSeparator runat="server" />
                <ext:TextField runat="server" ID="txtTenDMGroup" FieldLabel="Tên" Width="150" LabelWidth="40" Hidden="true" MaxLength="50" />
                <ext:ToolbarSpacer Width="10" />
                <ext:Hidden runat="server" ID="hdfGroup" />
                <ext:ComboBox runat="server" ID="cboCategoryGroup" FieldLabel="Loại" Hidden="True" Editable="True" CtCls="requiredData" DisplayField="Name" ValueField="Id"
                    AnchorHorizontal="100%" LabelWidth="40" Width="200">
                    <Triggers>
                        <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                    </Triggers>
                    <Store>
                        <ext:Store runat="server" ID="storeGroup" OnRefreshData="storeGroup_OnRefreshData" AutoLoad="False">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id" Mapping="Key" />
                                        <ext:RecordField Name="Name" Mapping="Value" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <Listeners>
                        <Expand Handler="if(#{cboCategoryGroup}.store.getCount() == 0){#{storeGroup}.reload();}" />
                        <Select Handler="this.triggers[0].show();#{hdfGroup}.setValue(#{cboCategoryGroup}.getValue());"></Select>
                        <TriggerClick Handler="if(index == 0){this.clearValue();this.triggers[0].hide();#{hdfGroup}.reset();};" />
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
                        <Click Handler="#{DirectMethods}.ResetInputCategoryGroup();" />
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
                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="objname" Value="#{hdfCurrentCatalogName}.getValue()" Mode="Raw" />
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
            <ColumnModel runat="server">
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
                        <Change Handler="#{RowSelectionModelCategoryGroup}.clearSelections();" />
                    </Listeners>
                </ext:PagingToolbar>
            </BottomBar>
        </ext:GridPanel>
    </Items>
    <Listeners>
        <Hide Handler="#{hdfEnableCatalogGroup}.setValue('');#{hdfCurrentCatalogName}.setValue('');" />
        <BeforeShow Handler="beforeShowWdCategoryGroupHRM();" />
    </Listeners>
    <Buttons>
        <ext:Button runat="server" Text="Đồng ý chọn" Icon="Accept">
            <Listeners>
                <Click Handler="if(CheckSelectedRows(#{grpCategoryGroup})){selectedCategoryGroupHRM();}" />
            </Listeners>
        </ext:Button>
        <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdAddCategoryGroup}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" Title="Chọn ảnh" Resizable="false"
    Icon="ImageAdd" Hidden="true" Padding="6" ID="wdUploadImageWindow" Width="400"
    Modal="true" AutoHeight="true">
    <Items>
        <ext:FormPanel runat="server" Border="false" ID="frmPanelUploadImage">
            <Items>
                <ext:Hidden runat="server" Text="File/ImagesEmployee" ID="hdfImageFolder" />
                <ext:Hidden runat="server" ID="hdfColumnName" />
                <ext:FileUploadField runat="server" ID="fufUploadControl" AllowBlank="false" RegexText="Định dạng file không hợp lệ"
                    Regex="(http(s?):)|([/|.|\w|\s])*\.(?:jpg|gif|png|bmp|jpeg|JPG|PNG|GIF|BMP|JPEG)"
                    AnchorHorizontal="100%" FieldLabel="Chọn ảnh">
                    <Listeners>
                        <FileSelected Handler="if (#{frmPanelUploadImage}.getForm().isValid()){#{btnAccept}.enable();}else{Ext.Msg.alert('<asp:Literal runat=\'server\' Text=\'Thông báo\' />','<asp:Literal runat=\'server\' Text=\'Định dạng file không hợp lệ\' />, <asp:Literal runat=\'server\' Text=\'Phần mềm chỉ chấp nhận các định dạng ảnh jpg, png, gif, bmp, jpeg!\' />');#{btnAccept}.disable();}" />
                    </Listeners>
                </ext:FileUploadField>
            </Items>
        </ext:FormPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnAccept" Text="Đồng ý" Icon="Accept">
            <DirectEvents>
                <Click OnEvent="btnAccept_Click">
                    <EventMask ShowMask="true" Msg="Đang tải..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button2" runat="server" Text="Đóng" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdUploadImageWindow}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="#{btnAccept}.disable();" />
        <Hide Handler="#{fufUploadControl}.reset();" />
    </Listeners>
</ext:Window>
<ext:Window Width="1000" ID="wdInput" AutoHeight="true" Maximizable="true"
    runat="server" Hidden="true" Icon="Add" Modal="true" Resizable="true" Constrain="true" Draggable="false"
    Title="Nhập thông tin hồ sơ" Layout="FormLayout" EnableViewState="false">
    <Items>
        <ext:Hidden runat="server" ID="hdfCommandButton" Text="Insert" />
        <ext:TabPanel Border="false" runat="server" Cls="bkGround" Padding="6" ID="tab_Employee"
            Height="550" DeferredRender="false">
            <Items>
                <ext:Panel ID="panelEmployee" Title="Hồ sơ nhân sự" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="0">
                    <Items>
                        <ext:Container runat="server" AnchorHorizontal="100%" Layout="FormLayout">
                            <Items>
                                <ext:FieldSet ID="fs_Employee" runat="server" Title="Thông tin cá nhân"
                                    Layout="ColumnLayout" Height="180">
                                    <Items>
                                        <ext:Container runat="server" Height="180" ID="Container23" ColumnWidth=".14">
                                            <Items>
                                                <ext:ImageButton ID="img_anhdaidien"
                                                    OverImageUrl="File/ImagesEmployee/No_person.jpg" ImageUrl="File/ImagesEmployee/No_person.jpg" runat="server" Width="120" Height="150" TabIndex="0">
                                                    <Listeners>
                                                        <Click Handler="#{wdUploadImageWindow}.show();" />
                                                    </Listeners>
                                                </ext:ImageButton>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Height="180" ID="ctan1" ColumnWidth=".29" Layout="FormLayout">
                                            <Items>
                                                <ext:TextField ID="txtEmployeeCode" runat="server" FieldLabel="Số hiệu CBNV" CtCls="requiredData" AllowBlank="false" AnchorHorizontal="98%" MaxLength="20"
                                                    MaxLengthText="Bạn chỉ được nhập tối đa 20 ký tự" LabelWidth="260" Width="550">
                                                </ext:TextField>
                                                <ext:TextField ID="txtCandidateCode" runat="server" FieldLabel="Số hiệu UV" CtCls="requiredData" AllowBlank="false" AnchorHorizontal="98%" MaxLength="20"
                                                               MaxLengthText="Bạn chỉ được nhập tối đa 20 ký tự" LabelWidth="260" Width="550" Hidden="True">
                                                </ext:TextField>
                                                <ext:TextField ID="txtFullName" runat="server" CtCls="requiredData" FieldLabel="Họ và tên<span style='color:red;'>*</span>"
                                                    AllowBlank="false" AnchorHorizontal="98%" MaxLength="50" MaxLengthText="Tối đa 50 ký tử"
                                                    LabelWidth="152" Width="440">
                                                    <Listeners>
                                                        <Blur Handler="ChuanHoaTen(#{txtFullName});" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Phần mềm sẽ tự động chuẩn hóa họ và tên của bạn. Ví dụ: bạn nhập nguyễn văn huy, kết quả trả về Nguyễn Văn Huy." />
                                                    </ToolTips>
                                                </ext:TextField>
                                                <ext:TextField ID="txtAlias" FieldLabel="Tên gọi khác" runat="server" AllowBlank="true"
                                                    TabIndex="4" AnchorHorizontal="98%" MaxLength="50" MaxLengthText="Tối đa 50 ký tự">
                                                </ext:TextField>
                                                <ext:DateField runat="server" ID="dfBirthDate" CtCls="requiredData" FieldLabel="Ngày sinh<span style='color:red;'>*</span>"
                                                    AnchorHorizontal="98%" TabIndex="7" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                    RegexText="Định dạng ngày sinh không đúng">
                                                    <Listeners>
                                                        <Blur Handler="checkNgaySinh(#{dfBirthDate},18);" />
                                                    </Listeners>
                                                </ext:DateField>
                                                <ext:ComboBox ID="cbxSex" FieldLabel="Giới tính" Editable="false" runat="server"
                                                    AnchorHorizontal="98%" TabIndex="8" SelectedIndex="0" AllowBlank="false">
                                                    <Items>
                                                        <ext:ListItem Text="Nam" Value="M" />
                                                        <ext:ListItem Text="Nữ" Value="F" />
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfFolkId" />
                                                <ext:ComboBox runat="server" ID="cbxFolk" FieldLabel="Dân tộc"
                                                    DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%"
                                                    ItemSelector="div.list-item" Width="368" LabelWidth="150">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <ext:Store runat="server" ID="store1" AutoLoad="false">
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
                                                        <Expand Handler="#{store_CbxFolk}.reload();" />
                                                        <Select Handler="this.triggers[0].show();#{hdfFolkId}.setValue(#{cbxFolk}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfFolkId}.reset();};
                                                                                    if (index == 1) { showWdAddCategoryHRM('DanToc'); this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Height="180" ID="Container3" ColumnWidth=".29" Layout="FormLayout">
                                            <Items>
                                                <ext:Hidden runat="server" ID="hdfHometownProvinceId" Text="0" />
                                                <ext:ComboBox runat="server" ID="cbxHometownProvince" FieldLabel="Quê quán (Tỉnh)" DisplayField="Name"
                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                    LabelWidth="150" Width="438" LabelAlign="Right" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                    PageSize="10">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Template runat="server">
                                                        <Html>
                                                            <tpl for=".">
						                                        <div class="list-item"> 
							                                        <p><b>{Name}</b></p>
						                                        </div>
					                                        </tpl>
                                                        </Html>
                                                    </Template>
                                                    <Store>
                                                        <ext:Store ID="storeHometownProvince" runat="server" AutoLoad="false">
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
                                                        <Expand Handler="#{storeHometownProvince}.reload();"></Expand>
                                                        <Select Handler="this.triggers[0].show();#{hdfHometownProvinceId}.setValue(#{cbxHometownProvince}.getValue());#{cbxHometownWard}.disable();#{cbxHometownDistrict}.clearValue();#{cbxHometownDistrict}.enable();#{cbxHometownWard}.clearValue();" />
                                                        <BeforeQuery Handler="this.triggers[0][this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfHometownDistrictId" />
                                                <ext:ComboBox runat="server" ID="cbxHometownDistrict" FieldLabel="Quận/Huyện" DisplayField="Name"
                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%" LabelAlign="Right"
                                                    LabelWidth="150" Width="438" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                    PageSize="10" Disabled="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Template runat="server">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfHometownDistrictId}.setValue(#{cbxHometownDistrict}.getValue());#{cbxHometownWard}.clearValue(); #{cbxHometownWard}.enable();#{cbxHometownWard}.clearValue(); " />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };#{hdfHometownDistrictId}.reset();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfHometownWardId" />
                                                <ext:ComboBox runat="server" ID="cbxHometownWard" FieldLabel="Phường/Xã" LabelAlign="Right"
                                                    DisplayField="Name" MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%" Disabled="true"
                                                    Editable="true" ItemSelector="div.list-item" LabelWidth="150" ListWidth="260" Width="438">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Template runat="server">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfHometownWardId}.setValue(#{cbxHometownWard}.getValue()); " />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfHometownWardId}.reset(); };" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfBirthPlaceProvinceId" />
                                                <ext:ComboBox runat="server" ID="cbxBirthPlaceProvince" FieldLabel="Nơi sinh (Tỉnh)" DisplayField="Name"
                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                    LabelWidth="150" Width="438" LabelAlign="Right" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                    PageSize="10">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Template runat="server">
                                                        <Html>
                                                            <tpl for=".">
						                                        <div class="list-item"> 
							                                        <p><b>{Name}</b></p>
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
                                                        <Select Handler="this.triggers[0].show();#{hdfBirthPlaceProvinceId}.setValue(#{cbxBirthPlaceProvince}.getValue()); #{cbxBirthPlaceDistrict}.clearValue(); #{cbxBirthPlaceDistrict}.enable();#{cbxBirthPlaceWard}.clearValue();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };#{hdfBirthPlaceProvinceId}.reset();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfBirthPlaceDistrictId" />
                                                <ext:ComboBox runat="server" ID="cbxBirthPlaceDistrict" LabelAlign="Right" FieldLabel="Quận/Huyện" DisplayField="Name"
                                                    MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                    LabelWidth="150" Width="438" ListWidth="260" Editable="true" ItemSelector="div.list-item"
                                                    PageSize="10" Disabled="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Template runat="server">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfBirthPlaceDistrictId}.setValue(#{cbxBirthPlaceDistrict}.getValue()); #{cbxBirthPlaceWard}.clearValue(); #{cbxBirthPlaceWard}.enable();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); };#{hdfBirthPlaceDistrictId}.reset();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfBirthPlaceWardId" />
                                                <ext:ComboBox runat="server" ID="cbxBirthPlaceWard" FieldLabel="Phường/Xã" LabelAlign="Right"
                                                    DisplayField="Name" MinChars="1" EmptyText="Gõ để tìm kiếm" ValueField="Id" AnchorHorizontal="98%"
                                                    Editable="true" ItemSelector="div.list-item" LabelWidth="150" Width="438" ListWidth="283"
                                                    LoadingText="Đang tải dữ liệu" PageSize="10" Disabled="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Template runat="server">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfBirthPlaceWardId}.setValue(#{cbxBirthPlaceWard}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfBirthPlaceWardId}.reset(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Height="180" ID="Container4" ColumnWidth=".28" Layout="FormLayout">
                                            <Items>
                                                <ext:Hidden runat="server" ID="hdfReligionId" />
                                                <ext:ComboBox runat="server" ID="cbxReligion" MinChars="1" FieldLabel="Tôn giáo"
                                                    DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" ItemSelector="div.list-item"
                                                    Width="373" LabelWidth="150">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <ext:Store runat="server" ID="storeCboReligion" AutoLoad="false">
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
                                                        <Expand Handler="#{storeCboReligion}.reload();" />
                                                        <Select Handler="this.triggers[0].show();#{hdfReligionId}.setValue(#{cbxReligion}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfReligionId}.reset();};
                                                                                   if (index == 1) { showWdAddCategoryHRM('TonGiao'); this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfMaritalStatusId" />
                                                <ext:ComboBox runat="server" ID="cboMaritalStatus" FieldLabel="Tình trạng HN" EmptyText="Tình trạng hôn nhân"
                                                    LabelWidth="152" Width="305" AllowBlank="false" DisplayField="Name" ValueField="Id"
                                                    ItemSelector="div.list-item" AnchorHorizontal="100%" Editable="true">
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
                                                        <ext:Store runat="server" ID="storeMaritalStatus" AutoLoad="false">
                                                            <Proxy>
                                                                <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                        <Expand Handler="#{storeMaritalStatus}.reload();" />
                                                        <Select Handler="this.triggers[0].show();#{hdfMaritalStatusId}.setValue(#{cboMaritalStatus}.getValue());" />
                                                        <BeforeQuery Handler="this.triggers[0][this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:TextField ID="txtIDNumber" CtCls="requiredDataWG" runat="server" MaskRe="/[0-9\.]/"
                                                    FieldLabel="Số CMND" AllowBlank="true" AnchorHorizontal="100%" MaxLength="12"
                                                    MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự">
                                                </ext:TextField>
                                                <ext:DateField Editable="true" ID="IDIssueDate" FieldLabel="Ngày cấp CMND"
                                                    runat="server" AnchorHorizontal="100%" TabIndex="57" MaskRe="/[0-9|/]/" Format="d/M/yyyy"
                                                    Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.triggers[0].show();" />
                                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                    </Listeners>
                                                </ext:DateField>
                                                <ext:Hidden runat="server" ID="hdfIDIssuePlaceId" />
                                                <ext:ComboBox runat="server" ID="cbxIDIssuePlace" FieldLabel="Nơi cấp CMND"
                                                    DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" Editable="true" LabelWidth="50"
                                                    Width="239" ListWidth="200" MinChars="1" ItemSelector="div.list-item" PageSize="20">
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
                                                        <ext:Store ID="cbx_IDIssuePlace_Store" runat="server" AutoLoad="false">
                                                            <Proxy>
                                                                <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                        <Select Handler="this.triggers[0].show();#{hdfIDIssuePlaceId}.setValue(#{cbxIDIssuePlace}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfIDIssuePlaceId}.reset(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:TextField runat="server" ID="txtWorkStatus" FieldLabel="Trạng thái" AnchorHorizontal="100%" ReadOnly="True" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" ID="ctnCandidate" AnchorHorizontal="100%" Layout="ColumnLayout" Hidden="True">
                            <Items>
                                <ext:FieldSet runat="server" ID="fsCandidate" Title="Thông tin tuyển dụng" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                    <Items>
                                        <ext:Container runat="server" Height="70" Layout="ColumnLayout">
                                            <Items>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" LabelWidth="150">
                                                    <Items>
                                                        <ext:Hidden runat="server" ID="hdfRequiredRecruitmentId"></ext:Hidden>
                                                        <ext:ComboBox ID="cboRequiredRecruitment" runat="server" FieldLabel="Yêu cầu tuyển dụng" AnchorHorizontal="98%" CtCls="requiredData" DisplayField="Name" ValueField="Id" StoreID="storeRequiredRecruitment">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Expand Handler="if(#{cboRequiredRecruitment}.store.getCount()==0){#{storeRequiredRecruitment}.reload();}"></Expand>
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfRequiredRecruitmentId}.reset(); }" />
                                                                <Select Handler="this.triggers[0].show();#{hdfRequiredRecruitmentId}.setValue(this.getValue());"></Select>
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                        <ext:NumberField ID="txtDesiredSalary" runat="server" FieldLabel="Mức lương mong muốn" AnchorHorizontal="98%" CtCls="requiredData">
                                                        </ext:NumberField>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" LabelWidth="120">
                                                    <Items>
                                                        <ext:DateField Editable="true" ID="dfApplyDate" Vtype="daterange" FieldLabel="Ngày nộp hồ sơ" CtCls="requiredData"
                                                                       runat="server" AnchorHorizontal="100%" TabIndex="34" MaskRe="/[0-9\/]/" Format="d/M/yyyy"
                                                                       Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset();this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                        <ext:Hidden runat="server" ID="hdfCandidateStatus" />
                                                        <ext:ComboBox ID="cboCandidateStatus" runat="server" FieldLabel="Trạng thái ứng viên" AnchorHorizontal="100%" DisplayField="Name" ValueField="Id" StoreID="storeCandidateStatus" CtCls="requiredData">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Expand Handler="if(#{cboCandidateStatus}.store.getCount()==0){#{storeCandidateStatus}.reload();}"></Expand>
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfCandidateStatus}.reset(); }" />
                                                                <Select Handler="this.triggers[0].show();#{hdfCandidateStatus}.setValue(this.getValue())"></Select>
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
                        <ext:Container ID="ctnContact" runat="server" Height="153" AnchorHorizontal="100%" Layout="ColumnLayout">
                            <Items>
                                <ext:FieldSet ID="fsContact" runat="server" Title="Thông tin liên hệ" Layout="FormLayout"
                                    ColumnWidth=".5" Padding="5">
                                    <Items>
                                        <ext:Container runat="server" Height="150" Layout="ColumnLayout">
                                            <Items>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5">
                                                    <Items>
                                                        <ext:TextField ID="txt_ResidentPlace" runat="server" EmptyText="Hộ khẩu thường trú" FieldLabel="Hộ khẩu TT"
                                                            Width="260" AllowBlank="true" TabIndex="19" AnchorHorizontal="98%">
                                                        </ext:TextField>
                                                        <ext:TextField ID="txtCellPhoneNumber" runat="server" MaskRe="/[0-9\.]/" FieldLabel="Di động"
                                                            AllowBlank="true" TabIndex="20" AnchorHorizontal="98%" MaxLength="50" MaxLengthText="Bạn chỉ được nhập tối đa 15 ký tự">
                                                        </ext:TextField>
                                                        <ext:TextField ID="txtWorkPhoneNumber" runat="server" MaskRe="/[0-9\.]/" FieldLabel="Điện thoại CQ"
                                                            TabIndex="21" AnchorHorizontal="98%" MaxLength="22" MaxLengthText="Bạn chỉ được nhập tối đa 15 ký tự">
                                                        </ext:TextField>
                                                        <ext:TextField ID="txtHomePhoneNumber" FieldLabel="Điện thoại nhà" MaskRe="/[0-9\.]/" runat="server"
                                                            AllowBlank="true" TabIndex="23" AnchorHorizontal="98%" MaxLength="15" MaxLengthText="Bạn chỉ được nhập tối đa 15 ký tự">
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" LabelWidth="120">
                                                    <Items>
                                                        <ext:TextField ID="txt_Address" runat="server" FieldLabel="Chỗ ở hiện nay" TabIndex="20"
                                                            AnchorHorizontal="100%">
                                                        </ext:TextField>
                                                        <ext:TextField ID="txtWorkEmail" runat="server" FieldLabel="Email nội bộ"
                                                            AllowBlank="true" TabIndex="24" AnchorHorizontal="100%" Regex="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                                                            RegexText="Định dạng email không đúng" MaxLength="50" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự">
                                                        </ext:TextField>
                                                        <ext:TextField ID="txtPersonalEmail" runat="server" FieldLabel="Email riêng" AllowBlank="true"
                                                            TabIndex="25" AnchorHorizontal="100%" Regex="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                                                            RegexText="Định dạng email không đúng" MaxLength="50" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự">
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                        </ext:Container>

                    </Items>
                </ext:Panel>
                <ext:Panel ID="panelJob" Title="Công việc" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="1">
                    <Items>
                        <ext:FieldSet ID="fsJob" runat="server" Title="Thông tin công việc" Height="150"
                            Layout="ColumnLayout" Padding="5">
                            <Items>
                                <ext:Container runat="server" Height="140" ID="Container7" Layout="FormLayout" ColumnWidth=".5">
                                    <Items>
                                        <ext:Hidden runat="server" ID="hdfDepartmentId" />
                                        <ext:ComboBox runat="server" ID="cboDepartment" FieldLabel="Phòng - Ban" EmptyText="Chọn đơn vị"
                                            LabelWidth="260" Width="550" AllowBlank="false" DisplayField="Name" ValueField="Id"
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
                                                <Select Handler="this.triggers[0].show();#{hdfDepartmentId}.setValue(#{cboDepartment}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};#{hdfDepartmentId}.reset();" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:Hidden runat="server" ID="hdfConstructionId" />
                                        <ext:ComboBox runat="server" ID="cbxConstruction" FieldLabel="Công trình"
                                            DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..."
                                            ItemSelector="div.list-item" AnchorHorizontal="98%" TabIndex="33" Editable="false"
                                            AllowBlank="false">
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
                                                <ext:Store runat="server" ID="storeConstruction" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                    </Proxy>
                                                    <BaseParams>
                                                        <ext:Parameter Name="objname" Value="cat_Construction" Mode="Value" />
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
                                                <Select Handler="this.triggers[0].show();#{hdfConstructionId}.setValue(#{cbxConstruction}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfConstructionId}.reset(); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:Hidden runat="server" ID="hdfTeamId" />
                                        <ext:ComboBox runat="server" ID="cbxTeam" FieldLabel="Tổ đội"
                                            DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..."
                                            ItemSelector="div.list-item" AnchorHorizontal="98%" TabIndex="33" Editable="false"
                                            AllowBlank="true">
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
                                                <ext:Store runat="server" ID="storeTeam" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                    </Proxy>
                                                    <BaseParams>
                                                        <ext:Parameter Name="objname" Value="cat_Team" Mode="Value" />
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
                                                <Select Handler="this.triggers[0].show();#{hdfTeamId}.setValue(#{cbxTeam}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfTeamId}.reset(); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:DateField Editable="true" ID="RecruimentDate" Vtype="daterange" FieldLabel="Ngày thử việc"
                                            runat="server" AnchorHorizontal="98%" TabIndex="34" MaskRe="/[0-9\/]/" Format="d/M/yyyy"
                                            Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.triggers[0].show();" />
                                                <TriggerClick Handler="if (index == 0) { this.reset();this.triggers[0].hide(); }" />
                                            </Listeners>
                                        </ext:DateField>
                                        <ext:TextField runat="server" ID="txtStudyWorkingDay" FieldLabel="Số ngày học việc"
                                            AnchorHorizontal="98%" MaskRe="/[0-9]/" MaxLength="10" />
                                        <ext:TextField ID="txtRecruitmentDepartment" runat="server" FieldLabel="Công ty ký HĐ"
                                            AnchorHorizontal="98%" Width="372" LabelWidth="130" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" LabelWidth="120">
                                    <Items>
                                        <ext:Hidden runat="server" ID="hdfPositionId" />
                                        <ext:ComboBox runat="server" ID="cbxPosition" FieldLabel="Chức vụ"
                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                            Width="364" Editable="true" ItemSelector="div.list-item"
                                            ListWidth="150" LoadingText="Đang tải dữ liệu">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                <ext:Store ID="store8" runat="server" AutoLoad="false">
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
                                                <Select Handler="this.triggers[0].show(); #{hdfPositionId}.setValue(#{cbxPosition}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfPositionId}.reset(); };
                                                    if (index == 1) { showWdAddCategoryHRM('ChucVu'); this.collapse(); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:Hidden runat="server" ID="hdfJobTitleId" />
                                        <ext:ComboBox runat="server" ID="cbxJobTitle" FieldLabel="Chức danh CM"
                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                            LabelWidth="130" Width="372" Editable="true" ItemSelector="div.list-item"
                                            ListWidth="250" LoadingText="Đang tải dữ liệu">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                <ext:Store ID="Store9" runat="server" AutoLoad="false">
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
                                                <Select Handler="this.triggers[0].show(); #{hdfJobTitleId}.setValue(#{cbxJobTitle}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfJobTitleId}.reset(); };
                                                        if (index == 1) { showWdAddCategoryHRM('ChucDanh'); this.collapse(); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:Hidden runat="server" ID="hdfWorkingFormId" />
                                        <ext:ComboBox runat="server" ID="cbxWorkingForm" FieldLabel="Hình thức làm việc"
                                            DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..."
                                            ItemSelector="div.list-item" AnchorHorizontal="100%" TabIndex="33" Editable="false"
                                            AllowBlank="false">
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
                                                <ext:Store ID="storeWorkingForm" runat="server" AutoLoad="False" OnRefreshData="storeWorkingForm_OnRefreshData">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="Id">
                                                            <Fields>
                                                                <ext:RecordField Name="Id" Mapping="Key" />
                                                                <ext:RecordField Name="Name" Mapping="Value" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <Listeners>
                                                <Expand Handler="if(#{cbxWorkingForm}.store.getCount()==0){#{storeWorkingForm}.reload();}" />
                                                <Select Handler="this.triggers[0].show();#{hdfWorkingFormId}.setValue(#{cbxWorkingForm}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfWorkingFormId}.reset(); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:DateField Editable="true" ID="ParticipationDate" FieldLabel="Ngày chính thức"
                                            runat="server" AnchorHorizontal="100%" TabIndex="35" MaskRe="/[0-9|/]/" Format="d/M/yyyy"
                                            Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.triggers[0].show();" />
                                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                            </Listeners>
                                        </ext:DateField>
                                        <ext:TextField runat="server" ID="txtProbationWorkingTime" FieldLabel="Thời gian thử việc"
                                            AnchorHorizontal="100%" MaskRe="/[0-9]/" MaxLength="10" />
                                        <ext:Hidden runat="server" ID="hdfWorkLocationId" />
                                        <ext:ComboBox runat="server" ID="cbxWorkLocation" FieldLabel="Địa điểm làm việc"
                                            DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                            LabelWidth="130" Width="372" Editable="true" ItemSelector="div.list-item"
                                            ListWidth="250" LoadingText="Đang tải dữ liệu">
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
                                                <ext:Store ID="storeWorkLocation" runat="server" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                    </Proxy>
                                                    <BaseParams>
                                                        <ext:Parameter Name="objname" Value="cat_WorkLocation" Mode="Value" />
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
                                                <Select Handler="this.triggers[0].show(); #{hdfWorkLocationId}.setValue(#{cbxWorkLocation}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfWorkLocationId}.reset(); };" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="panelEducation" Title="Trình độ" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="2">
                    <Items>
                        <ext:FieldSet ID="fs1" runat="server" Title="Trình độ" Layout="FormLayout" ColumnWidth=".5">
                            <Items>
                                <ext:Container runat="server" ID="ctn100" Layout="ColumnLayout" AnchorHorizontal="100%"
                                    Height="100">
                                    <Items>
                                        <ext:Container runat="server" ID="ctn101" ColumnWidth=".5" Layout="FormLayout">
                                            <Items>
                                                <ext:Hidden runat="server" ID="hdfUniversityId" />
                                                <ext:ComboBox runat="server" ID="cbxUniversity" FieldLabel="Trường đào tạo"
                                                    DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true"
                                                    LabelWidth="134" Width="313" ItemSelector="div.list-item" PageSize="10">
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
                                                        <ext:Store runat="server" ID="storeUniversity" AutoLoad="false">
                                                            <Proxy>
                                                                <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                                            </Proxy>
                                                            <BaseParams>
                                                                <ext:Parameter Name="objname" Value="cat_University" Mode="Value" />
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
                                                        <Select Handler="this.triggers[0].show();#{hdfUniversityId}.setValue(#{cbxUniversity}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfUniversityId}.reset(); };
                                                                                   " />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfGraduationTypeId" />
                                                <ext:ComboBox runat="server" ID="cbxGraduationType" DisplayField="Name" FieldLabel="Xếp loại"
                                                    ValueField="Id" AnchorHorizontal="98%" Editable="false" ItemSelector="div.list-item" StoreID="StoreGraduationType">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfGraduationTypeId}.setValue(#{cbxGraduationType}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfGraduationTypeId}.reset(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfITLevelId" />
                                                <ext:ComboBox runat="server" ID="cbxITLevel" FieldLabel="Tin học" DisplayField="Name"
                                                    LabelWidth="135" Width="314" ValueField="Id" AnchorHorizontal="98%" Editable="true" MinChars="1"
                                                    ItemSelector="div.list-item">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <ext:Store runat="server" ID="cbx_tinhoc_Store" AutoLoad="false">
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
                                                        <Expand Handler="#{cbx_tinhoc_Store}.reload();" />
                                                        <Select Handler="this.triggers[0].show();#{hdfITLevelId}.setValue(#{cbxITLevel}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); #{hdfITLevelId}.setValue('');#{cbxITLevel}.setValue('');this.triggers[0].hide();} 
                                                                                    if (index == 1) { #{hdfCurrentCatalogName}.setValue('cat_ITLevel'); #{hdfCurrentCatalogGroupName}.setValue('itlevel'); #{hdfEnableCatalogGroup}.setValue('true'); #{wdAddCategoryGroup}.show(); this.collapse();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfLanguageLevelId" />
                                                <ext:ComboBox runat="server" ID="cbxLanguageLevel" FieldLabel="Ngoại ngữ" MinChars="1"
                                                    DisplayField="Name" LabelWidth="250" Width="420" ValueField="Id" AnchorHorizontal="98%"
                                                    Editable="true" ItemSelector="div.list-item">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <ext:Store runat="server" ID="cbx_ngoaingu_Store" AutoLoad="false">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfLanguageLevelId}.setValue(#{cbxLanguageLevel}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfLanguageLevelId}.setValue(''); #{cbxLanguageLevel}.setValue('');
                                                                if (index == 1) { #{hdfCurrentCatalogName}.setValue('cat_LanguageLevel'); #{hdfCurrentCatalogGroupName}.setValue('languagelevel'); #{hdfEnableCatalogGroup}.setValue('true'); #{wdAddCategoryGroup}.show(); this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ID="ctn102" ColumnWidth=".5" Layout="FormLayout">
                                            <Items>
                                                <ext:Hidden runat="server" ID="hdfInputIndustryId" />
                                                <ext:ComboBox runat="server" ID="cbxInputIndustry" FieldLabel="Nghề nghiệp"
                                                    DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                    LabelWidth="134" Width="313" ItemSelector="div.list-item" PageSize="10">
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
                                                        <ext:Store runat="server" ID="storeInputIndustry" AutoLoad="false">
                                                            <Proxy>
                                                                <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                        <Select Handler="this.triggers[0].show();#{hdfInputIndustryId}.setValue(#{cbxInputIndustry}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{cbxInputIndustry}.reset(); };
                                                                                   " />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:NumberField ID="txtGraduationYear" runat="server" FieldLabel="Năm tốt nghiệp"
                                                    AllowBlank="true" TabIndex="31" AnchorHorizontal="100%" MaxLength="4" MinLength="4">
                                                </ext:NumberField>
                                                <ext:Hidden runat="server" ID="hdfEducationId" />
                                                <ext:ComboBox runat="server" ID="cbxEducation" FieldLabel="Trình độ"
                                                    DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" Editable="true"
                                                    LabelWidth="252" Width="422" ItemSelector="div.list-item">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <Expand Handler="#{store_CbxEducation}.reload();" />
                                                        <Select Handler="this.triggers[0].show();#{hdfEducationId}.setValue(#{cbxEducation}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfEducationId}.reset(); };
                                                                                   if (index == 1) { #{hdfCurrentCatalogName}.setValue('cat_Education'); #{hdfCurrentCatalogGroupName}.setValue('education'); #{hdfEnableCatalogGroup}.setValue('true'); #{wdAddCategoryGroup}.show(); this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:Hidden runat="server" ID="hdfBasicEducationId" />
                                                <ext:ComboBox runat="server" ID="cbxBasicEducation" FieldLabel="TĐ văn hóa"
                                                    DisplayField="Name" ValueField="Id" ListWidth="200" AnchorHorizontal="100%" ItemSelector="div.list-item" MinChars="1"
                                                    LabelWidth="500" Width="746">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <ext:Store ID="store7" runat="server" AutoLoad="false">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfBasicEducationId}.setValue(#{cbxBasicEducation}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfBasicEducationId}.reset() };
                                                                       if (index == 1) { #{hdfCurrentCatalogName}.setValue('cat_BasicEducation'); #{hdfCurrentCatalogGroupName}.setValue('basiceducation'); #{hdfEnableCatalogGroup}.setValue('true'); #{wdAddCategoryGroup}.show(); this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="panelEmployeeInsurance" Title="Bảo hiểm, Công đoàn" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="3">
                    <Items>
                        <ext:Container runat="server" AnchorHorizontal="100%" Layout="FormLayout">
                            <Items>
                                <ext:Container runat="server" ColumnWidth="0.65" Layout="FormLayout" AutoHeight="true">
                                    <Items>
                                        <ext:FieldSet runat="server" Layout="ColumnLayout" Title="Thông tin bảo hiểm" AutoHeight="true"
                                            AnchorHorizontal="100%">
                                            <Items>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" Height="85" LabelWidth="125">
                                                    <Items>
                                                        <ext:TextField ID="txtInsuranceNumber" runat="server" FieldLabel="Số sổ BHXH"
                                                            AllowBlank="true" TabIndex="49" AnchorHorizontal="98%" MaxLength="50" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự">
                                                        </ext:TextField>
                                                        <ext:DateField runat="server" ID="InsuranceIssueDate" FieldLabel="Năm tham gia BHXH"
                                                            AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng" Vtype="daterange">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{date_ketthucbh}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                        <ext:DateField runat="server" ID="dfHealthJoinedDate" FieldLabel="Ngày đóng BHYT"
                                                            AnchorHorizontal="98%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng" Vtype="daterange">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{date_ketthucbh}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" LabelWidth="125">
                                                    <Items>
                                                        <ext:TextField ID="txtHealthInsuranceNumber" runat="server" FieldLabel="Số thẻ BHYT"
                                                            AllowBlank="true" TabIndex="49" AnchorHorizontal="100%" MaxLength="50" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự">
                                                        </ext:TextField>
                                                        <ext:DateField runat="server" ID="dfHealthExpiredDate" FieldLabel="Ngày hết hạn BHYT"
                                                            AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng" Vtype="daterange">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="this.triggers[0].show();" />
                                                                <TriggerClick Handler="if (index == 0) { this.reset(); #{date_ketthucbh}.setMinValue(); this.triggers[0].hide(); }" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet runat="server" Layout="ColumnLayout" Title="Thông tin công đoàn" AutoHeight="true"
                                            AnchorHorizontal="100%">
                                            <Items>
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" Height="40" LabelWidth="125">
                                                    <Items>
                                                        <ext:DateField Editable="true" ID="dfUnionJoinedDate" FieldLabel="Ngày vào công đoàn" runat="server"
                                                            AnchorHorizontal="98%" TabIndex="63" MaskRe="/[0-9|/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                            RegexText="Định dạng ngày không đúng">
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
                                                <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" LabelWidth="125">
                                                    <Items>
                                                        <ext:Hidden runat="server" ID="hdfUnionPosition" />
                                                        <ext:ComboBox runat="server" ID="cboUnionPosition" DisplayField="Name" ValueField="Id"
                                                            MinChars="1" PageSize="20" Width="180" Editable="true" FieldLabel="Chức vụ công đoàn"
                                                            ListWidth="200" ItemSelector="div.list-item" AnchorHorizontal="100%">
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
                                                                <ext:Store ID="storeUnionPosition" runat="server" AutoLoad="false">
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
                                                                <Expand Handler="#{storeUnionPosition}.reload();" />
                                                                <Select Handler="this.triggers[0].show(); #{hdfUnionPosition}.setValue(#{cboUnionPosition}.getValue());" />
                                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfUnionPosition}.reset();}" />
                                                            </Listeners>
                                                        </ext:ComboBox>
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
                <ext:Panel ID="panelPolitic" Title="Chính trị" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="4">
                    <Items>
                        <ext:Container runat="server" AnchorHorizontal="100%" Layout="FormLayout">
                            <Items>
                                <ext:FieldSet runat="server" Layout="ColumnLayout" Title="Thông tin đoàn thể" AutoHeight="true"
                                    AnchorHorizontal="100%">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" Height="50" LabelWidth="125">
                                            <Items>
                                                <ext:DateField Editable="true" ID="VYUJoinedDate" FieldLabel="Ngày vào Đoàn" runat="server"
                                                    AnchorHorizontal="98%" TabIndex="63" MaskRe="/[0-9|/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                    RegexText="Định dạng ngày không đúng">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.triggers[0].show();" />
                                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                    </Listeners>
                                                </ext:DateField>
                                                <ext:Hidden runat="server" ID="hdfVYUPositionId" />
                                                <ext:ComboBox runat="server" ID="cbxVYUPosition" FieldLabel="Chức vụ Đoàn"
                                                    DisplayField="Name" ValueField="Id" MinChars="1" AnchorHorizontal="98%" Editable="true" ListWidth="200"
                                                    ItemSelector="div.list-item" LabelWidth="130" Width="307">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" />
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
                                                        <ext:Store runat="server" ID="cbx_VYUPosition_Store" AutoLoad="false">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfVYUPositionId}.setValue(#{cbxVYUPosition}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfVYUPositionId}.reset();}
                                                                    if (index == 1) { showWdAddCategoryHRM('ChucVuDoan');this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth="0.5" LabelWidth="125">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtVYUJoinedPlace" FieldLabel="Nơi kết nạp đoàn" AnchorHorizontal="100%" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="fieldSetPoliticlevelInfo" runat="server" Layout="ColumnLayout" AutoHeight="true"
                                    Title="Thông tin chính trị" AnchorHorizontal="100%">
                                    <Items>
                                        <ext:Container runat="server" Layout="FormLayout" AnchorHorizontal="100%" Height="110" ColumnWidth="0.5"
                                            LabelWidth="125">
                                            <Items>
                                                <ext:Hidden runat="server" ID="hdfPoliticLevelId" />
                                                <ext:ComboBox runat="server" ID="cbxPoliticLevel" FieldLabel="Trình độ chính trị"
                                                    DisplayField="Name" ValueField="Id" AnchorHorizontal="98%" TabIndex="68" Editable="false"
                                                    ItemSelector="div.list-item">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                        <Expand Handler="#{store_CbxPoliticLevel}.reload();" />
                                                        <Select Handler="this.triggers[0].show();#{hdfPoliticLevelId}.setValue(#{cbxPoliticLevel}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{cbxPoliticLevel}.setValue('');#{hdfPoliticLevelId}.setValue('');
                                                                                   if (index == 1) { #{hdfCurrentCatalogName}.setValue('cat_PoliticLevel');#{hdfCurrentCatalogGroupName}.setValue('politiclevel'); #{hdfEnableCatalogGroup}.setValue('true'); #{wdAddCategoryGroup}.show(); this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:DateField Editable="true" ID="CPVJoinedDate" FieldLabel="Ngày vào Đảng"
                                                    runat="server" AnchorHorizontal="98%" Vtype="daterange" MaskRe="/[0-9|/]/" Format="d/M/yyyy"
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

                                                <ext:Hidden runat="server" ID="hdfCPVPositionId" />
                                                <ext:ComboBox runat="server" ID="cbxCPVPosition" FieldLabel="Chức vụ đảng" DisplayField="Name"
                                                    ValueField="Id" AnchorHorizontal="98%" TabIndex="74" Editable="false" ItemSelector="div.list-item">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" />
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
                                                        <ext:Store runat="server" ID="storeCPVPosition" AutoLoad="false">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfCPVPositionId}.setValue(#{cbxCPVPosition}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfCPVPositionId}.reset(); }
                                                                if (index == 1) { showWdAddCategoryHRM('ChucVuDang');this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                                <ext:DateField Editable="true" ID="ArmyJoinedDate" FieldLabel="Ngày nhập ngũ" runat="server"
                                                    AnchorHorizontal="98%" TabIndex="77" Vtype="daterange" MaskRe="/[0-9|/]/"
                                                    Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                    RegexText="Định dạng ngày không đúng">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.triggers[0].show();" />
                                                        <TriggerClick Handler="if (index == 0) { this.reset(); #{ArmyLeftDate}.setMinValue(); this.triggers[0].hide(); }" />
                                                    </Listeners>
                                                </ext:DateField>
                                                <ext:Hidden runat="server" ID="hdfArmyLevelId" />
                                                <ext:ComboBox runat="server" ID="cbxArmyLevel" FieldLabel="Quân hàm cao nhất" DisplayField="Name"
                                                    EmptyText="Ví dụ Đại tá, thiếu úy..." ValueField="Id" AnchorHorizontal="98%"
                                                    Editable="false" ItemSelector="div.list-item">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" />
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
                                                        <ext:Store runat="server" ID="storeArmyLevel" AutoLoad="false">
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
                                                        <Select Handler="this.triggers[0].show();#{hdfArmyLevelId}.setValue(#{cbxArmyLevel}.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfArmyLevelId}.reset(); }
                                                                if (index == 1) { showWdAddCategoryHRM('CapBacQuanDoi');this.collapse(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="FormLayout" AnchorHorizontal="100%" Height="130" ColumnWidth="0.5"
                                            LabelWidth="125">
                                            <Items>
                                                <ext:TextField runat="server" ID="txtCPVCardNumber" AnchorHorizontal="100%" LabelWidth="70" Width="200" FieldLabel="Thẻ đảng" />
                                                <ext:DateField Editable="true" ID="CPVOfficialJoinedDate" FieldLabel="Ngày chính thức"
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
                                                <ext:TextField ID="txtCPVJoinedPlace" runat="server" FieldLabel="Nơi kết nạp Đảng"
                                                    AllowBlank="true" AnchorHorizontal="100%" MaxLength="50"
                                                    MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự" />
                                                <ext:DateField Editable="true" ID="ArmyLeftDate" FieldLabel="Ngày xuất ngũ" runat="server"
                                                    AnchorHorizontal="100%" TabIndex="78" MaskRe="/[0-9|/]/" Format="d/M/yyyy"
                                                    Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.triggers[0].show();" />
                                                        <TriggerClick Handler="if (index == 0) { this.reset(); #{ArmyJoinedDate}.setMaxValue(); this.triggers[0].hide(); }" />
                                                    </Listeners>
                                                </ext:DateField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="panelFamilyRelationship" Title="Quan hệ gia đình" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="5">
                    <Items>
                        <ext:GridPanel ID="GridPanelFamilyRelationship" runat="server" Height="400" Width="980">
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
                                <ext:Store runat="server" AutoLoad="False" ID="storeFamilyRelationship" OnBeforeStoreChanged="HandleChangesFamilyRelationship"
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
                            <ColumnModel runat="server">
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
                                        <Renderer Fn="FamilyRelationRendererHRM" />
                                        <Editor>
                                            <ext:ComboBox runat="server" ID="cbxFamilyRelationship" Editable="true" DisplayField="Name" MinChars="1" PageSize="15"
                                                ValueField="Id" AnchorHorizontal="100%" ItemSelector="div.list-item" Width="200">
                                                <Store>
                                                    <ext:Store ID="store_CbxFamilyRelationship" runat="server" AutoLoad="false">
                                                        <Proxy>
                                                            <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                    <Expand Handler="#{store_CbxFamilyRelationship}.reload();" />
                                                    <Select Handler="this.triggers[0].show();updateFamilyRelationHRM('RelationshipId',this.getValue());" />
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
                                        <RowSelect Handler="#{btnDelete}.enable();" />
                                        <RowDeselect Handler="#{btnDelete}.disable();" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="panelEmployeeOther" Title="Thông tin khác" runat="server"
                    AutoHeight="true" AnchorHorizontal="100%" Border="false" TabIndex="6">
                    <Items>
                        <ext:Container runat="server" AnchorHorizontal="100%" Layout="FormLayout">
                            <Items>
                                <ext:FieldSet ID="FieldSet5" runat="server" Title="Thông tin sức khỏe" Layout="FormLayout"
                                    AnchorHorizontal="100%" Height="150" LabelWidth="125">
                                    <Items>
                                        <ext:ComboBox ID="cbxBloodGroup" LabelWidth="70" runat="server" FieldLabel="Nhóm máu" AllowBlank="true"
                                            Width="132" Editable="true" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ListItem Text="A" Value="A" />
                                                <ext:ListItem Text="B" Value="B" />
                                                <ext:ListItem Text="AB" Value="AB" />
                                                <ext:ListItem Text="O" Value="O" />
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:CompositeField ID="CompositeField4" runat="server" AnchorHorizontal="100%" FieldLabel="Cao/Cân nặng">
                                            <Items>
                                                <ext:NumberField runat="server" EmptyText="Cao" ID="txtHeight" Width="100" TabIndex="98" />
                                                <ext:DisplayField ID="DisplayField3" runat="server" Text="cm  /" />
                                                <ext:NumberField runat="server" ID="txtWeight" EmptyText="Nặng" Width="100" TabIndex="99" />
                                                <ext:DisplayField ID="DisplayField4" runat="server" Text="Kg" />
                                            </Items>
                                        </ext:CompositeField>
                                        <ext:TextField runat="server" ID="txtRankWounded" FieldLabel="Là thương binh hạng"
                                            AnchorHorizontal="100%" MaxLength="100" LabelWidth="170" Width="336">
                                        </ext:TextField>
                                        <ext:Hidden runat="server" ID="hdfFamilyPolicyId" />
                                        <ext:ComboBox runat="server" ID="cbxFamilyPolicy" FieldLabel="Là con GĐ chính sách"
                                            DisplayField="Name" ValueField="Id" AnchorHorizontal="100%" MinChars="1" ListWidth="200"
                                            LabelWidth="170" Width="399" ItemSelector="div.list-item">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                <ext:Store runat="server" ID="cbx_FamilyPolicy_Store" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                <Select Handler="this.triggers[0].show();#{hdfFamilyPolicyId}.setValue(#{cbxFamilyPolicy}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfFamilyPolicyId}.reset(); }
                                                                 if (index == 1) { showWdAddCategoryHRM('ChinhSach'); this.collapse();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:Hidden runat="server" ID="hdfHealthStatusId" />
                                        <ext:ComboBox runat="server" ID="cbxHealthStatus" FieldLabel="Tình trạng sức khỏe" MinChars="1"
                                            DisplayField="Name" ValueField="Id" EmptyText="Tình trạng sức khỏe" AnchorHorizontal="100%"
                                            ItemSelector="div.list-item" LabelWidth="170" Width="306">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
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
                                                <ext:Store runat="server" ID="cbx_HealthStatus_Store" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
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
                                                <Select Handler="this.triggers[0].show();#{hdfHealthStatusId}.setValue(#{cbxHealthStatus}.getValue());" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{hdfHealthStatusId}.reset(); }
                                                                                     if (index == 1) { showWdAddCategoryHRM('TinhTrangSucKhoe');this.collapse(); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Height="170" AnchorHorizontal="100%" Layout="FormLayout">
                            <Items>
                                <ext:FieldSet runat="server" ID="FieldSet6" Title="Liên hệ trong trường hợp khẩn cấp"
                                    Layout="FormLayout" AnchorHorizontal="100%" LabelWidth="125" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtContactPersonName" runat="server" FieldLabel="Người liên hệ"
                                            AnchorHorizontal="100%" AllowBlank="true" TabIndex="105" MaxLength="50" EmptyText="Họ và tên người liên hệ">
                                        </ext:TextField>
                                        <ext:TextField ID="txtContactPhoneNumber" MaskRe="/[0-9\.]/" runat="server"
                                            FieldLabel="SDT người LH" EmptyText="Số điện thoại người liên hệ" AnchorHorizontal="100%"
                                            TabIndex="106" MaxLength="50">
                                        </ext:TextField>
                                        <ext:TextField ID="txtContactRelation" runat="server" FieldLabel="Quan hệ với CB"
                                            EmptyText="Ví dụ: Bố, Mẹ, ..." AnchorHorizontal="100%" TabIndex="107" MaxLength="100">
                                        </ext:TextField>
                                        <ext:TextArea ID="txtContactAddress" runat="server" FieldLabel="Địa chỉ"
                                            AnchorHorizontal="100%" Height="40" MaxLength="1000" TabIndex="109">
                                        </ext:TextArea>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container1" runat="server" AnchorHorizontal="100%" Layout="FormLayout" Height="150">
                            <Items>
                                <ext:FieldSet runat="server" ID="FieldSet1" Title="Thông tin khác"
                                    Layout="FormLayout" AnchorHorizontal="100%" LabelWidth="125" Padding="5">
                                    <Items>
                                        <ext:TextField runat="server" MaxLength="50" LabelWidth="125" Width="253" ID="txtPersonalTaxCode" FieldLabel="Mã số thuế cá nhân" AnchorHorizontal="100%" />

                                    </Items>
                                </ext:FieldSet>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:TabPanel>
    </Items>
    <Buttons>
        <ext:Button ID="btnUpdate" runat="server" Text="Lưu" Icon="Disk">
            <Listeners>
                <Click Handler="return ValidateInputHRM(#{hdfType}.getValue());" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnSaveEmployee_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                    <ExtraParams>
                        <ext:Parameter Name="Reset" Value="True" />
                        <ext:Parameter Name="jsonFamilyRelationship" Value="getJsonOfStore(#{storeFamilyRelationship})" Mode="Raw" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Icon="Disk" Text="Lưu" ID="btnUpdateEdit" Hidden="true">
            <Listeners>
                <Click Handler="return ValidateInputHRM(#{hdfType}.getValue());" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnSaveEmployee_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                    <ExtraParams>
                        <ext:Parameter Name="Command" Value="Edit" />
                        <ext:Parameter Name="jsonFamilyRelationship" Value="getJsonOfStore(#{storeFamilyRelationship})" Mode="Raw" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnClose" runat="server" Text="Hủy" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdInput}.hide();" />
            </Listeners>

        </ext:Button>
    </Buttons>
</ext:Window>
