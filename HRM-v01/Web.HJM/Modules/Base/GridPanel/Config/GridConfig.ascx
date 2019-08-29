<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_GridPanel_Config_GridConfig" Codebehind="GridConfig.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../../CoreBase/TableList/ColumnList.ascx" TagName="ColumnList"
    TagPrefix="uc3" %>
<%@ Register Src="../../CoreBase/TableList/TableList.ascx" TagName="TableList" TagPrefix="uc2" %>
<ext:Hidden ID="txtSelectedColumnName" runat="server" />
<ext:Store runat="server" ID="FieldStore" AutoLoad="false" OnRefreshData="FieldRefresh">
    <DirectEventConfig>
        <EventMask ShowMask="false" />
    </DirectEventConfig>
    <Reader>
        <ext:JsonReader IDProperty="Name">
            <Fields>
                <ext:RecordField Name="Name" Type="String" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Store ID="StoreColumnInfo" AutoSave="true" RefreshAfterSaving="None" OnBeforeStoreChanged="HandleColumnChanges"
    SkipIdForNewRecords="false" ShowWarningOnFailure="true" runat="server">
    <Proxy>
        <ext:HttpProxy Method="GET" Url="ColumnInfoHandler.ashx" />
    </Proxy>
    <Reader>
        <ext:JsonReader IDProperty="ID" Root="Data" TotalProperty="TotalRecords">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="ColumnName" />
                <ext:RecordField Name="ColumnHeader" />
                <ext:RecordField Name="Width" />
                <ext:RecordField Name="Order" />
                <ext:RecordField Name="Visible" />
                <ext:RecordField Name="AllowSearch" />
                <ext:RecordField Name="RenderJS" />
                <ext:RecordField Name="AllowComboBoxOnGrid" />
                <ext:RecordField Name="ComboBoxTable" />
                <ext:RecordField Name="ValueFieldComboBox" />
                <ext:RecordField Name="DisplayFieldComboBox" />
                <ext:RecordField Name="MasterColumnComboID" />
                <ext:RecordField Name="WhereFilterComboBox" />
                <ext:RecordField Name="DataTimeSheetHandlerType" />
                <ext:RecordField Name="AllowEditOnGrid" />
                <ext:RecordField Name="Locked" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="wdUpdatePanelInfo" Hidden="true" runat="server" Title="Cấu hình bảng biểu"
    Modal="true" AutoHeight="true" Border="false" Padding="0" Icon="Cog" Width="560"
    Maximizable="false" Resizable="false" Height="700">
    <Items>
        <ext:Panel ID="Panel3" runat="server" Frame="true" ButtonAlign="Center">
            <Items>
                <ext:TextField runat="server" FieldLabel="Tiêu đề bảng" ID="txtTitle" Width="522" />
                <ext:TextField runat="server" FieldLabel="Điều kiện lọc" EmptyText="Ví dụ: ID % 2 = 0 and GioiTinh = 'Nam'"
                    ID="txtWhereClause" Width="522" />
                <ext:TextField runat="server" FieldLabel="Điều kiện sắp xếp" EmptyText="Ví dụ: Order by CreatedDate desc"
                    ID="txtOrderBy" Width="522" />
                <ext:GridPanel runat="server" ID="gdFilter" Height="70" Visible="false" Width="522">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="gdFilterToolBar">
                            <Items>
                                <ext:Button runat="server" Text="Thêm mới" Icon="Add">
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="gdFilterToolBarSelectionModel" />
                    </SelectionModel>
                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn />
                            <ext:Column DataIndex="WhereClause" Header="WhereClause">
                                <Editor>
                                    <ext:TextField runat="server" ID="txtWhereClauseEditor" />
                                </Editor>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <Plugins>
                        <ext:RowEditor ID="RowEditor2" ErrorSummary="false" runat="server" CancelText="Hủy"
                            SaveText="Cập nhật">
                        </ext:RowEditor>
                    </Plugins>
                    <Store>
                        <ext:Store runat="server" ID="FilterStore" AutoLoad="true" OnRefreshData="FilterStore_Refresh">
                            <Proxy>
                                <ext:PageProxy />
                            </Proxy>
                            <Reader>
                                <ext:JsonReader IDProperty="ID">
                                    <Fields>
                                        <ext:RecordField Name="ID" />
                                        <ext:RecordField Name="WhereClause" />
                                        <ext:RecordField Name="IdGridPanel" />
                                        <ext:RecordField Name="UserID" />
                                        <ext:RecordField Name="RoleID" />
                                        <ext:RecordField Name="IsInUsed" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                </ext:GridPanel>
                <ext:ComboBox runat="server" ID="cbDataSource" Editable="false" Width="522" FieldLabel="Nguồn dữ liệu">
                    <Items>
                        <ext:ListItem Text="Sử dụng bảng" Value="Table" />
                        <ext:ListItem Text="Sử dụng view" Value="View" />
                    </Items>
                    <Listeners>
                        <%--<Select Handler="if(#{cbDataSource}.getValue()=='Table'){#{cbView}.disable();#{cbTable}.enable();}
                                        else{#{cbView}.enable();#{cbTable}.disable();}ChangeBetweenTableAndView(#{FieldStore},#{cbAutoExpandColumn});" />--%>
                        <Select Handler="if(#{cbDataSource}.getValue()=='Table'){#{cbView}.disable();#{cbTable}.enable();}
                                        else{#{cbView}.enable();}ChangeBetweenTableAndView(#{FieldStore},#{cbAutoExpandColumn});" />
                    </Listeners>
                </ext:ComboBox>
                <ext:Container runat="server" Layout="Column" Height="30" ID="ctl548">
                    <Items>
                        <ext:Container runat="server" Layout="Form" ColumnWidth=".5" ID="ctl549">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbTable" Editable="false" FieldLabel="Chọn bảng"
                                    AnchorHorizontal="95%">
                                    <Listeners>
                                        <Select Handler="ChangeBetweenTableAndView(#{FieldStore},#{cbAutoExpandColumn});" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container1" runat="server" Layout="Form" ColumnWidth=".5">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbView" Editable="false" FieldLabel="Chọn view"
                                    AnchorHorizontal="95%">
                                    <Listeners>
                                        <Select Handler="ChangeBetweenTableAndView(#{FieldStore},#{cbAutoExpandColumn});" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:ComboBox Editable="false" ID="cbAutoExpandColumn" Width="522" FieldLabel="Cột co giãn"
                    runat="server" StoreID="FieldStore" DisplayField="Name" ValueField="Name">
                </ext:ComboBox>
                <ext:Container ID="Container7" runat="server" Layout="Column" Height="60">
                    <Items>
                        <ext:Container runat="server" Layout="FormLayout" ColumnWidth=".5" ID="ctl559">
                            <Items>
                                <ext:NumberField runat="server" FieldLabel="Chiều dài" ID="txtwidth" AnchorHorizontal="95%" />
                                <ext:ComboBox runat="server" Editable="false" AllowBlank="false" ID="cbPageSize"
                                    FieldLabel="Số dòng/trang" AnchorHorizontal="95%">
                                    <Items>
                                        <ext:ListItem Value="5" Text="5" />
                                        <ext:ListItem Value="10" Text="10" />
                                        <ext:ListItem Value="15" Text="15" />
                                        <ext:ListItem Value="20" Text="20" />
                                        <ext:ListItem Value="25" Text="25" />
                                        <ext:ListItem Value="30" Text="30" />
                                    </Items>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container6" runat="server" Layout="FormLayout" ColumnWidth=".5">
                            <Items>
                                <ext:NumberField runat="server" FieldLabel="Chiều cao" ID="txtheight" AnchorHorizontal="95%" />
                                <ext:TextField runat="server" FieldLabel="Icon" ID="txtIcon" AnchorHorizontal="95%" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:ComboBox runat="server" Visible="true" ID="cbInformationPanel" EmptyText="Chọn vị trí"
                    Width="522" Editable="false" FieldLabel="Bảng thông tin">
                    <Items>
                        <ext:ListItem Text="Tắt" Value="" />
                        <ext:ListItem Text="Trên cùng" Value="Top" />
                        <ext:ListItem Text="Phía dưới" Value="Bottom" />
                        <ext:ListItem Text="Bên trái" Value="Left" />
                        <ext:ListItem Text="Bên phải" Value="Right" />
                    </Items>
                </ext:ComboBox>
                <ext:Container ID="Container2" runat="server" Layout="Column" Height="30">
                    <Items>
                        <ext:Container ID="Container3" runat="server" Layout="Form" ColumnWidth=".25">
                            <Items>
                                <ext:Checkbox runat="server" ID="chkRowNumber" FieldLabel="Hiển thị số dòng" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container4" runat="server" Layout="Form" ColumnWidth=".25">
                            <Items>
                                <ext:Checkbox runat="server" ID="chkHeader" FieldLabel="Hiển thị header" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container5" runat="server" Layout="Form" ColumnWidth=".25">
                            <Items>
                                <ext:Checkbox runat="server" ID="chkFilter" FieldLabel="Có chức năng lọc" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container8" runat="server" Layout="Form" ColumnWidth=".25">
                            <Items>
                                <ext:Checkbox runat="server" ID="chkCheckBox" FieldLabel="Hiện CheckBox" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Height="33">
                    <Items>
                        <ext:Container ID="Container10" runat="server" Layout="Form" ColumnWidth=".25">
                            <Items>
                                <ext:Checkbox runat="server" ID="chkAllowEditOnGrid" FieldLabel="Sửa trên Grid" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container11" runat="server" Layout="Form" ColumnWidth=".25">
                            <Items>
                                <ext:Checkbox runat="server" ID="chkOneManyForm" ColumnWidth=".25" FieldLabel="Form nhập liệu 1-n" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Panel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnSaveConfig" Icon="Disk" Text="Lưu cấu hình">
            <DirectEvents>
                <Click OnEvent="btnUpdateConfigGridPanel_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button4" runat="server" Icon="Decline" Text="Hủy">
            <Listeners>
                <Click Handler="#{wdUpdatePanelInfo}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <%--<Show Handler="#{FieldStore}.reload();if(#{cbDataSource}.getValue()=='Table'){#{cbView}.disable();}else{#{cbTable}.disable();}" />--%>
        <Show Handler="#{FieldStore}.reload();if(#{cbDataSource}.getValue()=='Table'){#{cbView}.disable();}" />
    </Listeners>
</ext:Window>
<ext:Window ID="wdSetComboBoxOnGrid" Hidden="true" runat="server" Padding="6" Modal="true"
    Title="Cấu hình ComboBox" AutoHeight="true" Border="false" Icon="Cog" Width="400"
    Maximizable="false" Resizable="false">
    <Content>
        <uc2:TableList ID="cbTableComboBox" runat="server" />
        <uc3:ColumnList ID="cbDisplayColumn" FieldLabel="Cột hiển thị" runat="server" />
        <uc3:ColumnList ID="cbValueColumn" FieldLabel="Cột giá trị" runat="server" />
        <ext:TextField runat="server" ID="txtWhereFilter" Width="350" FieldLabel="Điều kiện lọc" />
        <ext:Hidden runat="server" ID="hdfColumnID" />
        <ext:ComboBox runat="server" ID="cbMasterColumnID" Width="350" FieldLabel="Cột phụ thuộc"
            DisplayField="ColumnHeader" ValueField="ID" StoreID="StoreColumnInfo">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Listeners>
                <Select Handler="this.triggers[0].show();" />
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
            </Listeners>
            <Listeners>
                <Expand Handler="#{cbMasterColumnID}.setValue(#{hdfColumnID}.getValue());" />
            </Listeners>
        </ext:ComboBox>
    </Content>
    <Buttons>
        <ext:Button runat="server" ID="btnUpdateComboBox" Text="Lưu" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="btnUpdateComboBox_Click">
                    <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="Button2" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdSetComboBoxOnGrid}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window ID="wdColumnInfomation" Hidden="true" runat="server" Modal="true" Title="Cấu hình thông tin cột"
    AutoHeight="true" Border="false" Padding="0" Icon="Cog" Width="700" Maximizable="true"
    Resizable="false" Height="400">
    <Items>
        <ext:GridPanel Border="true" runat="server" StoreID="StoreColumnInfo" AutoExpandColumn="ColumnHeader"
            Height="310" ID="gdPanel">
            <TopBar>
                <ext:Toolbar runat="server" ID="ctl1153">
                    <Items>
                        <ext:Button runat="server" Icon="Add" ID="btnAddColumn" Text="Tạo mới cột">
                            <Menu>
                                <ext:Menu runat="server" ID="mnuColumnData">
                                    <Items>
                                        <ext:GridPanel ID="GridPanel2" runat="server" StripeRows="true" Header="false" Title="Danh sách cột"
                                            Width="200" Height="300">
                                            <ColumnModel ID="ColumnModel2" runat="server">
                                                <Columns>
                                                    <ext:Column ColumnID="TableColumnName" Header="Tên cột" Width="160" DataIndex="ColumnName"
                                                        Resizable="false" />
                                                </Columns>
                                            </ColumnModel>
                                            <Store>
                                                <ext:Store runat="server" ID="GridPanel2_Store">
                                                    <Reader>
                                                        <ext:ArrayReader>
                                                            <Fields>
                                                                <ext:RecordField Name="ColumnName" />
                                                            </Fields>
                                                        </ext:ArrayReader>
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <SelectionModel>
                                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server">
                                                    <Listeners>
                                                        <RowSelect Handler="SaveColumnName(#{GridPanel2},#{txtSelectedColumnName});" />
                                                        <RowDeselect Handler="SaveColumnName(#{GridPanel2},#{txtSelectedColumnName});" />
                                                    </Listeners>
                                                </ext:CheckboxSelectionModel>
                                            </SelectionModel>
                                            <Buttons>
                                                <ext:Button ID="btnUpdateColumnTable" Icon="Disk" runat="server" Text="Đồng ý">
                                                    <DirectEvents>
                                                        <Click OnEvent="btnUpdateColumnTable_Click">
                                                            <EventMask Msg="Đang lưu dữ liệu..." ShowMask="true" />
                                                        </Click>
                                                    </DirectEvents>
                                                    <Listeners>
                                                        <Click Handler="#{mnuColumnData}.hide();" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button runat="server" Text="Đóng lại" Icon="Decline" ID="ctl1155">
                                                    <Listeners>
                                                        <Click Handler="#{mnuColumnData}.hide();" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                            <LoadMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Menu>
                            </Menu>
                        </ext:Button>
                        <ext:Button runat="server" Icon="Delete" ID="btnDeleteColumnInfo" Text="Xóa">
                            <Listeners>
                                <Click Handler="RemoveItemOnGrid(#{gdPanel},#{StoreColumnInfo});#{btnDeleteColumnInfo}.disable();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button runat="server" Icon="Cog" ID="btnConfigComboBox" Text="Cấu hình Combobox">
                            <DirectEvents>
                                <Click OnEvent="btnConfigComboBox_Click">
                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button runat="server" ID="btnReloadData" Text="Nạp lại dữ liệu"></ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:RowNumbererColumn />
                    <ext:Column ColumnID="ColumnName" Header="Tên trường" Width="70" DataIndex="ColumnName"
                        Sortable="true">
                        <Editor>
                            <ext:ComboBox runat="server" ID="cbColumnField">
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Tên cột" ColumnID="ColumnHeader" DataIndex="ColumnHeader">
                        <Editor>
                            <ext:TextField ID="TextField2" runat="server" EmptyText="Nhập tên cột" AllowBlank="true" />
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="ComboBox" DataIndex="AllowComboBoxOnGrid" Width="60">
                        <Editor>
                            <ext:Checkbox ID="Checkbox2" runat="server" />
                        </Editor>
                        <Renderer Fn="GetBooleanIcon" />
                    </ext:Column>
                    <ext:Column Header="Trạng thái" Width="60" DataIndex="Visible">
                        <Editor>
                            <ext:Checkbox runat="server" ID="ctl1167" />
                        </Editor>
                        <Renderer Fn="GetStatus" />
                    </ext:Column>
                    <ext:Column Header="Cho phép tìm kiếm" DataIndex="AllowSearch">
                        <Editor>
                            <ext:Checkbox ID="Checkbox1" runat="server" />
                        </Editor>
                        <Renderer Fn="GetBooleanIcon" />
                    </ext:Column>
                    <ext:Column Header="Định dạng" DataIndex="RenderJS">
                        <Editor>
                            <ext:ComboBox runat="server" ID="cbRenderJs">
                                <Items>
                                    <ext:ListItem Text="Mặc định" Value="" />
                                    <ext:ListItem Text="Ngày tháng" Value="GetDateFormat" />
                                    <ext:ListItem Text="Ngày tháng có giờ" Value="GetDateFormatIncludeTime" />
                                    <ext:ListItem Text="Giới tính(bit)" Value="GetGender" />
                                    <ext:ListItem Text="Giới tính(Char)" Value="GetGenderChar" />
                                    <ext:ListItem Text="Tiền tệ(VNĐ)" Value="RenderVND" />
                                    <ext:ListItem Text="Tiền tệ(USD)" Value="RenderUSD" />
                                    <ext:ListItem Text="Đúng/Sai" Value="GetBooleanIcon" />
                                </Items>
                            </ext:ComboBox>
                        </Editor>
                        <Renderer Fn="GetRenderValue" />
                    </ext:Column>
                    <ext:Column Header="Cho phép sửa trên Grid" DataIndex="AllowEditOnGrid">
                        <Editor>
                            <ext:Checkbox runat="server" ID="chkEditOnGrid">
                            </ext:Checkbox>
                        </Editor>
                        <Renderer Fn="GetBooleanIcon" />
                    </ext:Column>
                    <ext:Column Header="Locked" DataIndex="Locked">
                        <Editor>
                            <ext:Checkbox runat="server" ID="Checkbox3">
                            </ext:Checkbox>
                        </Editor>
                        <Renderer Fn="GetBooleanIcon" />
                    </ext:Column>
                </Columns>
            </ColumnModel>
            <Plugins>
                <ext:RowEditor ID="RowEditor1" ErrorSummary="false" runat="server" CancelText="Hủy"
                    SaveText="Cập nhật">
                </ext:RowEditor>
            </Plugins>
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel2" SingleSelect="true" runat="server">
                    <Listeners>
                        <RowSelect Handler="#{btnDeleteColumnInfo}.enable();" />
                    </Listeners>
                </ext:RowSelectionModel>
            </SelectionModel>
            <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
        </ext:GridPanel>
    </Items>
    <Buttons>
        <ext:Button ID="Button1" Text="Nạp lại trang" runat="server" Icon="Reload">
            <Listeners>
                <Click Handler="#{wdColumnInfomation}.hide();location.reload();" />
            </Listeners>
        </ext:Button>
        <ext:Button Text="Đóng lại" runat="server" Icon="Decline" ID="ctl1174">
            <Listeners>
                <Click Handler="#{wdColumnInfomation}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="#{btnDeleteColumnInfo}.disable();" />
    </Listeners>
</ext:Window>
