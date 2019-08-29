<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Salary.SalaryBoardInfo" Codebehind="SalaryBoardInfo.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Resource/js/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="/Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <style type="text/css">
        #gridSalaryInfo .x-grid3-hd-inner {
            white-space: normal !important;
        }

        .x-grid3-scroller {
            height: 0
        }

        #sheetPanel {
            height: 93vh;
            width: 99.7%
        }
    </style>

    <script type="text/javascript">

        function checkInputSalaryBoard() {
            if (hdfConfigId.getValue() == '' || hdfConfigId.getValue() == null) {
                alert('Bạn chưa chọn cấu hình cho bảng lương!');
                return false;
            }
            if (hdfDepartmentId.getValue() == '' || hdfDepartmentId.getValue() == null) {
                alert("Bạn chưa chọn bộ phận!");
                return false;
            }
            if (txtTitleSalaryBoard.getValue() == '' || txtTitleSalaryBoard.getValue() == null) {
                alert("Bạn chưa nhập tên bảng tính lương!");
                return false;
            }
            if (txtCode.getValue() == '' || txtCode.getValue() == null) {
                alert("Bạn chưa nhập mã bảng tính lương!");
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfSalaryBoardId" />
            <ext:Hidden runat="server" ID="hdfType" />
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

            <ext:GridPanel ID="gridSalaryInfo" TrackMouseOver="true" runat="server"
                StripeRows="true" Border="false" AnchorHorizontal="100%" Title="Vui lòng chọn bảng lương" Icon="Date">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tb">
                        <Items>
                            <ext:Button ID="Button3" runat="server" Text="Quản lý bảng lương" Icon="Table" Hidden="true">
                                <Listeners>
                                    <Click Handler="wdSalaryBoardManage.show();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnBack" runat="server" Text="Quay lại" Icon="ArrowLeft">
                                <DirectEvents>
                                    <Click OnEvent="btnBack_Click">
                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Store>
                    <ext:Store runat="server" ID="storeTimeSheet">
                        <Proxy>
                            <ext:HttpProxy Method="POST" Url="~/Services/HandlerSalaryBoardDyamic.ashx" />
                        </Proxy>
                        <AutoLoadParams>
                            <ext:Parameter Name="start" Value="={0}" />
                            <ext:Parameter Name="limit" Value="={15}" />
                        </AutoLoadParams>
                        <BaseParams>
                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                            <ext:Parameter Name="year" Value="hdfYear.getValue()" Mode="Raw" />
                            <ext:Parameter Name="month" Value="hdfMonth.getValue()" Mode="Raw" />
                            <ext:Parameter Name="salaryBoardId" Value="hdfSalaryBoardId.getValue()" Mode="Raw" />
                        </BaseParams>
                        <Reader>
                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                <Fields>
                                </Fields>
                            </ext:JsonReader>
                        </Reader>
                    </ext:Store>
                </Store>
            </ext:GridPanel>

            <iframe id="sheetPanel" src="SalaryBoard\index.html?id=<%= SalaryBoardListId %>"></iframe>

            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdSalaryBoardManage" Constrain="true"
                Title="Quản lý bảng tính lương" Icon="Table" Layout="FormLayout" Width="800" AutoHeight="true">
                <Items>
                    <ext:Container runat="server" ID="ctn23" Layout="ColumnLayout" Height="360">
                        <Items>
                            <ext:Hidden ID="hdfSalaryBoardListID" runat="server" />
                            <ext:GridPanel ID="grpSalaryBoardList" runat="server" StripeRows="true" Border="false"
                                AnchorHorizontal="100%" Header="false" Height="350" Title="Danh sách bảng tính lương"
                                AutoExpandColumn="Title">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="tbgr">
                                        <Items>
                                            <ext:Button ID="btnAddSalaryBoardList" Icon="Add" runat="server" Text="Thêm">
                                                <Listeners>
                                                    <Click Handler="Ext.net.DirectMethods.ResetFormSalaryList() ; Ext.net.DirectMethods.btnAddSalaryBoardList_Click();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" Disabled="true" ID="btnEditSalaryBoardList" Text="Sửa"
                                                Icon="Pencil">
                                                <Listeners>
                                                    <Click Handler="Ext.net.DirectMethods.btnEditSalaryBoardList_Click();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" Text="Xóa" Icon="Delete"
                                                Disabled="true" ID="btnDeleteSalaryBoardList">
                                                <Listeners>
                                                    <Click Handler="if (CheckSelectedRows(grpSalaryBoardList) == false) {return false;}" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnDeleteSalaryBoardList_Click">
                                                        <Confirmation Title="<%$ Resources:CommonMessage, Warning%>" Message="Bạn có chắc chắn muốn xóa không ?"
                                                            ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeSalaryBoardList" AutoSave="true" runat="server" GroupField="Year">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="SalaryBoardList" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Title" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Month" />
                                                    <ext:RecordField Name="Year" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" />
                                </View>
                                <ColumnModel ID="ColumnModel3" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" />
                                        <ext:Column ColumnID="Title" Header="Tên bảng lương" Width="160" DataIndex="Title">
                                        </ext:Column>
                                        <ext:Column ColumnID="Title" Header="Mã bảng lương" Width="160" DataIndex="Code">
                                        </ext:Column>
                                        <ext:Column ColumnID="Month" Align="Center" Header="Tháng"
                                            Width="120" DataIndex="Month" />
                                        <ext:Column ColumnID="Year" Align="Center" Header="Năm"
                                            Width="120" DataIndex="Year" Hidden="True" />
                                        <ext:Column ColumnID="CreatedBy" Align="Center" Header="Người tạo" Width="120" DataIndex="CreatedBy" />
                                        <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo"
                                            Width="80" DataIndex="CreatedDate" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModelSalaryBoardList" runat="server">
                                        <Listeners>
                                            <RowSelect Handler="btnEditSalaryBoardList.enable();hdfSalaryBoardListID.setValue(RowSelectionModelSalaryBoardList.getSelected().get('Id')); btnDeleteSalaryBoardList.enable(); " />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="true" Msg="Đang tải" />
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="10">
                                        <Items>
                                            <ext:Label ID="Label2" runat="server" Text="<%$ Resources:HOSO, number_line_per_page%>" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer7" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="10" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar2}.pageSize = parseInt(this.getValue()); #{PagingToolbar2}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModelSalaryBoardList.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="10" />
                    <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Height="50">
                        <Items>
                            <ext:Container ID="Container8" runat="server" LabelAlign="left" Layout="Form"
                                LabelWidth="120">
                                <Items>
                                    <ext:Checkbox runat="server" BoxLabel="Cập nhật lại dữ liệu chấm công" ID="chk_IsUpdateTimeSheet" />
                                    <ext:Checkbox runat="server" BoxLabel="Cập nhật lại dữ liệu lương" ID="chk_IsUpdateSalary" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAcceptSalaryBoardList" runat="server" Icon="Accept" Text="Đồng ý">
                        <Listeners>
                            <Click Handler="if (CheckSelectedRows(grpSalaryBoardList) == false) {return false;}" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="ChooseSalaryBoardList_Click">
                                <EventMask ShowMask="true" Msg="Đang tạo bảng lương..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseSalaryBoardList" Text="Đóng lại"
                        Icon="Decline">
                        <Listeners>
                            <Click Handler="wdSalaryBoardManage.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdCreateSalaryBoardList" Constrain="true"
                Title="Tạo bảng lương" Icon="Add" Layout="FormLayout" Width="800" AutoHeight="true" Padding="6" >
                <Items>
                    <ext:Container ID="Container2" runat="server" Layout="Form" Height="400"
                        LabelWidth="200">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfConfigId" />
                            <ext:ComboBox runat="server" ID="cbxConfigList" FieldLabel="Chọn bảng cấu hình"
                                CtCls="requiredData" DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%"
                                ItemSelector="div.list-item" Width="368">
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
                                    <ext:Store runat="server" ID="store_cbxConfigList" AutoLoad="false">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="/Services/HandlerCatalog.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="objname" Value="cat_PayrollConfig" Mode="Value" />
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
                                    <Expand Handler="store_cbxConfigList.reload();" />
                                    <Select Handler="this.triggers[0].show();hdfConfigId.setValue(cbxConfigList.getValue());" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfConfigId.reset();}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="hdfDepartmentId" />
                            <ext:ComboBox runat="server" ID="cbxDepartment" FieldLabel="Chọn bộ phận"
                                LabelWidth="70" Width="300" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
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
                                    <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cbxDepartment.getValue());
                                txtTitleSalaryBoard.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                    <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Container ID="Containerm" Height="27" runat="server" Layout="ColumnLayout">
                                <Items>
                                    <ext:CompositeField runat="server" ID="cf1" FieldLabel="Chọn tháng">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cbxMonth" Width="80" Editable="false" FieldLabel="Chọn tháng">
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
                                                    <Select Handler="hdfMonth.setValue(cbxMonth.getValue());
                                                txtTitleSalaryBoard.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="55">
                                                <Listeners>
                                                    <Spin Handler="hdfYear.setValue(spnYear.getValue());
                                                txtTitleSalaryBoard.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                                </Listeners>
                                            </ext:SpinnerField>
                                        </Items>
                                    </ext:CompositeField>
                                </Items>
                            </ext:Container>
                            <ext:TextArea ID="txtTitleSalaryBoard" BlankText="Bạn bắt buộc phải nhập tên bảng lương" CtCls="requiredData"
                                AllowBlank="false" AnchorHorizontal="98%" FieldLabel="Tên bảng tính lương"
                                runat="server" />
                            <ext:TextField runat="server" ID="txtCode" FieldLabel="Mã bảng lương" CtCls="requiredData" AnchorHorizontal="98%" />
                            <ext:TextArea ID="txtDesciptionSalaryBoard" AnchorHorizontal="98%" FieldLabel="Mô tả" runat="server" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="btnCreateSalaryBoard" runat="server" Icon="Accept" Text="Tạo bảng">
                        <Listeners>
                            <Click Handler="return checkInputSalaryBoard();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="CreateSalaryBoardList_Click">
                                <EventMask ShowMask="true" Msg="Đang tạo bảng tính lương..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdateEditSalaryBoard" Text="Cập nhật" Icon="Disk" Hidden="True">
                        <Listeners>
                            <Click Handler="return checkInputSalaryBoard();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="UpdateSalaryBoardList_Click">
                                <EventMask ShowMask="true" />
                                <ExtraParams>
                                    <ext:Parameter Name="Edit" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnCloseSalaryBoard" runat="server" Icon="Decline" Text="Đóng lại">
                        <Listeners>
                            <Click Handler="wdCreateSalaryBoardList.hide();Ext.net.DirectMethods.ResetFormSalaryList();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

