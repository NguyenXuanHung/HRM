<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.UC.ChooseEmployee" Codebehind="ChooseEmployee.ascx.cs" %>

<script type="text/javascript">
    var store3;
    var SetStore = function (s3) {
        store3 = s3;
    }
    var enterKeyToSearch = function (f, e) {
        try {
            this.triggers[0].show();
            if (e.getKey() === e.ENTER) {
                store3.reload();
            }
            if (txtFullName.getValue() === '') {
                this.triggers[0].hide();
            }
        } catch (e) {

        }
    }
    var btnAddEmployee_CheckSelectRow = function (gridPanel, hdfHoTenCanBo) {
        if (gridPanel.getSelectionModel().getCount() == 0) {
            Ext.Msg.alert("Thông báo", "Bạn phải chọn ít nhất một cán bộ !");
            return false;
        }
        getEmployeeName(gridPanel, hdfHoTenCanBo);
        return true;
    }

    var getEmployeeName = function (gridPanel, hdfHoTenCanBo) {
        var s = gridPanel.getSelectionModel().getSelections();

        var rs = "";
        for (var i = 0, r; r = s[i]; i++) {
            rs += r.data.FullName + ", ";
        }
        hdfHoTenCanBo.setValue(rs);
    }
</script>
<ext:Hidden runat="server" ID="hdfChiChonMotCanBo" />
<ext:Hidden runat="server" ID="hdfHoTenCanBo" />
<ext:Store ID="cbxJobTitle_Store" runat="server" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
    </Proxy>
    <BaseParams>
        <ext:Parameter Name="handlers" Value="Category" />
        <ext:Parameter Name="table" Value="cat_JobTitle" Mode="Value" />
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
<ext:Store ID="cbxPosition_Store" runat="server" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
    </Proxy>
    <BaseParams>
        <ext:Parameter Name="handlers" Value="Category" />
        <ext:Parameter Name="table" Value="cat_Position" Mode="Value" />
        <ext:Parameter Name="ma" Value="Id" Mode="Value" />
        <ext:Parameter Name="ten" Value="Name" Mode="Value" />
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
<ext:Window Modal="true" Resizable="false" Hidden="true" runat="server" ID="wdChooseUser"
    Constrain="true" Title="Chọn danh sách CBCCVC" Icon="UserAdd" Width="700"
    Padding="6" AutoHeight="true">
    <Items>
        <ext:Container ID="Container1" runat="server" Height="330" Layout="FormLayout">
            <Items>
                <ext:ComboBox runat="server" ID="cbDepartmentList" FieldLabel="Chọn phòng ban" CtCls="requiredData"
                    DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                    AnchorHorizontal="100%" TabIndex="33" Editable="false" AllowBlank="false">
                    <Triggers>
                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                    </Triggers>
                    <Template ID="Template37" runat="server">
                        <Html>
                            <tpl for=".">
						        <div class="list-item"> 
                                {Name}
						        </div>
					        </tpl>
                        </Html>
                    </Template>
                    <Store>
                        <ext:Store runat="server" ID="stDepartmentList" AutoLoad="false" OnRefreshData="stDepartmentList_OnRefreshData">
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
                    <Listeners>
                        <Expand Handler="if(#{stDepartmentList}.getCount()==0) #{stDepartmentList}.reload();" />
                        <Select Handler="#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();#{Store3}.reload();"></Select>
                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                    </Listeners>
                </ext:ComboBox>
                <ext:GridPanel runat="server" ID="EmployeeGrid" Icon="UserAdd" Header="false" Title="Danh sách nhân viên"
                    AutoExpandColumn="HoVaTen" AnchorHorizontal="100%" Height="300">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbEmployeeGrid">
                            <Items>
                                <ext:ComboBox runat="server" LabelWidth="25" Width="150" Editable="false"
                                    DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                                    ID="cbxWorkStatus" FieldLabel="Lọc">
                                    <Template runat="server">
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
                                    <Store>
                                        <ext:Store runat="server" ID="cbxTrangThaiHoSo_store" AutoLoad="false" OnRefreshData="cbxTrangThaiHoSo_OnrefreshData">
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
                                    <Listeners>
                                        <Expand Handler="if(#{cbxTrangThaiHoSo_store}.getCount()==0)#{cbxTrangThaiHoSo_store}.reload();" />
                                        <Select Handler="#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();this.triggers[0].show();"></Select>
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();}" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ToolbarSpacer runat="server" ID="ts1" Width="5" />
                                <ext:ComboBox runat="server" ID="filterChucVu" DisplayField="Name" ValueField="Id"
                                    MinChars="1" PageSize="20" EmptyText="tìm chức vụ" Width="130" Editable="true"
                                    ListWidth="200" ItemSelector="div.list-item" StoreID="cbxPosition_Store" LoadingText="Đang tải dữ liệu...">
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
                                        <Expand Handler="#{filterChucVu}.store.reload();" />
                                        <Select Handler="this.triggers[0].show();#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();"></Select>
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();}" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ToolbarSpacer runat="server" ID="ToolbarSeparator1" Width="5" />
                                <ext:ComboBox runat="server" ID="filterCongViec" DisplayField="Name" ValueField="Id"
                                    MinChars="1" PageSize="20" Width="130" Editable="true" ListWidth="200" ItemSelector="div.list-item"
                                    StoreID="cbxJobTitle_Store" LoadingText="Đang tải dữ liệu..." EmptyText="tìm chức danh">
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
                                        <Expand Handler="#{cbxJobTitle_Store}.reload();" />
                                        <Select Handler="this.triggers[0].show();#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();"></Select>
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();}" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                <ext:TriggerField runat="server" EnableKeyEvents="true" ID="txtFullName" EmptyText ="Nhập tên CBCCVC">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyToSearch" />
                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); #{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad(); }" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:Button ID="Button3" runat="server" Text="Tìm kiếm" Icon="Zoom">
                                    <Listeners>
                                        <Click Handler="#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel runat="server" ID="chkEmployeeRowSelection" />
                    </SelectionModel>
                    <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn Width="30" Header="STT" />
                            <ext:Column Header="Mã CB" Width="45" DataIndex="EmployeeCode">
                            </ext:Column>
                            <ext:Column Header="Họ Tên" Width="130" ColumnID="HoVaTen" DataIndex="FullName">
                            </ext:Column>
                            <ext:DateColumn Header="Ngày sinh" Width="80" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                            <ext:Column Header="Giới tính" Width="60" DataIndex="SexName"/>
                            <ext:Column Header="Bộ phận" DataIndex="DepartmentName"/>
                            <ext:Column Header="Chức vụ" Width="60" DataIndex="PositionName"/>
                            <ext:Column Header="Chức danh" Width="70" DataIndex="JobTitleName"/>
                            
                        </Columns>
                    </ColumnModel>
                    <Store>
                        <ext:Store ID="Store3" ShowWarningOnFailure="false" runat="server" AutoLoad="True">
                            <Proxy>
                                <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                            </Proxy>
                            <AutoLoadParams>
                                <ext:Parameter Name="start" Value="={0}" />
                                <ext:Parameter Name="limit" Value="={20}" />
                            </AutoLoadParams>
                            <BaseParams>
                                <ext:Parameter Name="handlers" Value="UcChooseEmployee" />
                                <ext:Parameter Name="Department" Value="#{cbDepartmentList}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="SearchEmployee" Value="#{txtFullName}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="Filter" Value="#{cbxWorkStatus}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="Position" Value="#{filterChucVu}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="JobTitle" Value="#{filterCongViec}.getValue()" Mode="Raw" />
                            </BaseParams>
                            <Reader>
                                <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                                    <Fields>
                                        <ext:RecordField Name="Id" />
                                        <ext:RecordField Name="EmployeeCode" />
                                        <ext:RecordField Name="FullName" />
                                        <ext:RecordField Name="BirthDate" />
                                        <ext:RecordField Name="SexName" />
                                        <ext:RecordField Name="DepartmentName" />
                                        <ext:RecordField Name="PositionName" />
                                        <ext:RecordField Name="JobTitleName" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                            <Listeners>
                                <Load Handler="#{chkEmployeeRowSelection}.clearSelections();" />
                            </Listeners>
                        </ext:Store>
                    </Store>
                    <SaveMask ShowMask="true" Msg="Đang tải dữ liệu..." />
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
        <ext:Button runat="server" Text="Đồng ý" ID="btnAddEmployee" Icon="Accept">
            <Listeners>
                <Click Handler="return btnAddEmployee_CheckSelectRow(#{EmployeeGrid},#{hdfHoTenCanBo});" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnAddEmployee_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button4" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdChooseUser}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="SetStore(#{Store3});" />
        <Hide Handler="#{EmployeeGrid}.getSelectionModel().clearSelections();" />
    </Listeners>
</ext:Window>
