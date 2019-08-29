<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HJM.Modules.UserControl.Modules_Base_Form_ConfigLayout" Codebehind="ConfigLayout.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script src="../../../../Resource/js/GridPanelEvent.js" type="text/javascript"></script>
<script type="text/javascript">
    var GetValidateType = function (value, p, record) {
        if (value == "")
            return "Không bắt lỗi";
        else
            return "<span tyle='color:green;'>" + value + "</span>";
    }
    var storeField;
    var SearchFieldPressHandler = function (f, e) {
        if (e.getKey() == e.ENTER) {
            storeField.reload();
        }
    }
    var setStore = function (s) {
        storeField = s;
    }

    Ext.apply(Ext.form.VTypes, {
        numberrange: function (val, field) {
            if (!val) {
                return;
            }

            if (field.startNumberField && (!field.numberRangeMax || (val != field.numberRangeMax))) {
                var start = Ext.getCmp(field.startNumberField);

                if (start) {
                    start.setMaxValue(val);
                    field.numberRangeMax = val;
                    start.validate();
                }
            } else if (field.endNumberField && (!field.numberRangeMin || (val != field.numberRangeMin))) {
                var end = Ext.getCmp(field.endNumberField);

                if (end) {
                    end.setMinValue(val);
                    field.numberRangeMin = val;
                    end.validate();
                }
            }

            return true;
        }
    });
</script>
<style type="text/css">
    #sortable
    {
        list-style-type: none;
        margin: 20px auto;
        padding: 0;
        width: 60%;
        text-align: left;
    }
    #sortable li
    {
        margin: 6px 3px 3px;
        padding: 3px;
        border: solid 1px #ccc;
        font-family: Arial;
        font-size: 11px;
        cursor: move;
        background-color: #F9F9F9;
        height: 15px;
        line-height: 15px;
    }
    #sortable li b
    {
        background: none repeat scroll 0 0 red;
        color: Red;
        cursor: pointer;
        display: block;
        float: right;
        line-height: 15px;
        width: 10px;
    }
</style>
<script type="text/javascript">
    var grid;
    var store;
    var DirectMethod;
    var SetGridStore = function (_grid, _store,_DirectMethod) {
        grid = _grid;
        store = _store;
        DirectMethod = _DirectMethod;
    } 
    var AddColumnField = function()
    { 
        if($("#sortable").html()==null)
        {
            alert("Bạn phải chọn nhóm trước");
            return;
        }
        var columnID = grid.getSelectionModel().getSelected().id;
        $("#sortable li:last").after("<li id='"+columnID+"'>"+grid.getSelectionModel().getSelected().data.LabelName+"<b onclick=RemoveField('"+columnID+"')>.</b></li>");
        //delete row on grid
        var s = grid.getSelectionModel().getSelections();
        for (var i = 0, r; r = s[i]; i++) {
            store.remove(r); 
        }
        store.commitChanges();  
        //end delete row
    }

    var RemoveField = function(fieldid)
    {  
        grid.insertRecord(0, {
            ID: fieldid,
            LabelName: $("#"+fieldid+"").text().replace(".",""), 
        });
        store.commitChanges();  
        $("#"+fieldid+"").remove(); 
        DirectMethod.SaveRemovedColumnID(fieldid);
    }
    
    var moveNode = function (tree, node, oldParent, newParent, index) {  
            DirectMethod.UpdateGroup(index,node.text,node.id,newParent.id); 
        }
     var refreshTree = function (tree) {            
            DirectMethod.RefreshMenu({
                success : function (result) {
                    var nodes = eval(result);
                    if(nodes.length > 0){
                        tree.initChildren(nodes);
                    }
                    else{
                        tree.getRootNode().removeChildren();
                    }
                }
            });
        }
</script>
<ext:Hidden runat="server" ID="hdfColumnID" />
<ext:Hidden runat="server" ID="hdfCommand" Text="insert" EnableViewState="true" />
<ext:Hidden runat="server" ID="hdfRemoveColumnID" />
<ext:Hidden runat="server" ID="hdfGroupID" />
<ext:Hidden runat="server" ID="hdfRecordId" />
<ext:Store ID="Store1" runat="server" OnRefreshData="ColumnStoreRefresh">
    <Proxy>
        <ext:PageProxy />
    </Proxy>
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="LabelName" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Store runat="server" ID="storeTable" AutoLoad="false" OnRefreshData="storeTableRefresh">
    <Proxy>
        <ext:PageProxy />
    </Proxy>
    <Reader>
        <ext:ArrayReader>
            <Fields>
                <ext:RecordField Name="name" />
            </Fields>
        </ext:ArrayReader>
    </Reader>
</ext:Store>
<ext:Store runat="server" ID="ColumnStore" AutoLoad="false" OnRefreshData="ColumnStore_RefreshData">
    <DirectEventConfig>
        <EventMask ShowMask="false" />
    </DirectEventConfig>
    <Reader>
        <ext:JsonReader IDProperty="Name">
            <Fields>
                <ext:RecordField Name="Name" Type="String" Mapping="Name" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Store runat="server" ID="GroupStore" AutoLoad="true" OnRefreshData="GroupStoreRefresh">
    <Proxy>
        <ext:PageProxy />
    </Proxy>
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="Title" Type="String" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window runat="server" ID="wdSetupLayout" Title="Cài đặt bố cục" AutoHeight="true"
    Modal="true" Resizable="false" Hidden="false" Padding="0" Width="740" Border="false"
    Icon="ApplicationForm">
    <Items>
        <ext:Panel ID="Panel1" runat="server" Border="false" Width="730" Height="400">
            <Items>
                <ext:BorderLayout ID="BorderLayout1" runat="server">
                    <West MarginsSummary="0 5 0 0">
                        <ext:GridPanel runat="server" StoreID="Store1" StripeRows="true" Title="Double click vào tên cột"
                            AutoScroll="false" EnableDragDrop="true" DDGroup="grid2tree" Width="200" ID="GridPanel1"
                            EnableViewState="true" AutoExpandColumn="LabelName">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="GridPanel1_toolbar">
                                    <Items>
                                        <ext:Button runat="server" ID="btnReloadStore1" Text="Tải lại"></ext:Button>
                                    </Items> 
                                </ext:Toolbar>
                            </TopBar>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ColumnID="ID" Header="ID" Sortable="true" Width="45" DataIndex="ID" />
                                </Columns>
                                <Columns>
                                    <ext:Column ColumnID="LabelName" Header="Tên cột" Width="180" Sortable="true" DataIndex="LabelName" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                    <%--<Listeners>
                                        <RowSelect Handler="#{hdfColumnID}.setValue(#{RowSelectionModel1}.getSelected().id);" />
                                    </Listeners>--%>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <Listeners>
                                <DblClick Handler="AddColumnField();" />
                            </Listeners>
                            <LoadMask ShowMask="true" />
                        </ext:GridPanel>
                    </West>
                    <Center Split="true" MarginsSummary="0 0 0 0">
                        <ext:Panel ID="Panel12" runat="server" Header="true" Title="Danh mục nhóm">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:DropDownField ID="ddfGroupTree" runat="server" Width="274" Editable="false"
                                            TriggerIcon="SimpleArrowDown">
                                            <Component>
                                                <ext:TreePanel EnableViewState="true" ID="TreePanel1" runat="server" Height="370" Shadow="None" UseArrows="true"
                                                    AutoScroll="true" Animate="true" EnableDD="true" ContainerScroll="true">
                                                    <Listeners>
                                                        <MoveNode Fn="moveNode" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="GroupStore_Change">
                                                            <EventMask ShowMask="true" Msg="Đợi trong giây lát..." />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:TreePanel>
                                            </Component> 
                                        </ext:DropDownField>
                                        <ext:Button runat="server" Icon="Add" ID="btnCreateNewFormGroup" ToolTip="Tạo mới nhóm"> 
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Label runat="server" ID="lblOutput" />
                            </Items>
                        </ext:Panel>
                    </Center>
                    <East MarginsSummary="0 0 0 5">
                        <ext:Panel runat="server" ID="pnlFormGroupDetail" Header="true" Title="Chi tiết nhóm"
                            Padding="6" EnableViewState="true" Width="200">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <ext:Button runat="server" ID="btnSaveFormGroupInformation" Icon="Disk" Text="Cập nhật">
                                            <DirectEvents>
                                                <Click OnEvent="btnSaveFormGroupInformation_Click">
                                                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnDeleteGroup" Icon="Delete" Text="Xóa nhóm">
                                            <%--<Listeners>
                                                <Click Handler="#{DirectMethods}.btnDeleteGroup_Click();#{Store1}.reload();#{btnDeleteGroup}.disable();$('#sortable').remove();" />
                                            </Listeners>--%>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:TextField runat="server" ID="txtGroupTitle" Width="174" LabelAlign="Top" FieldLabel="Tiêu đề"
                                    EnableViewState="true" />
                                <ext:NumberField runat="server" ID="txtWidth" Width="174" MinValue="0.0" MaxValue="1.0"
                                    LabelAlign="Top" DecimalSeparator="." AllowDecimals="true" EnableViewState="true" Text="100" FieldLabel="Width" />
                                <ext:NumberField runat="server" ID="txtHeight" Width="174" LabelAlign="Top" Text="0"
                                    EnableViewState="true" FieldLabel="Height" />
                                <ext:ComboBox runat="server" StoreID="GroupStore" Editable="false" EnableViewState="true"
                                    ValueField="ID" FieldLabel="Nhóm cấp trên" LabelAlign="Top" DisplayField="Title"
                                    ID="cbGroup2">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" Qtip="Hủy" />
                                    </Triggers>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show();" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox runat="server" ID="cbLabelAlign" SelectedIndex="0" LabelAlign="Top"
                                    EnableViewState="true" Editable="false" FieldLabel="Vị trí Label">
                                    <Items>
                                        <ext:ListItem Text="Bên Trái" Value="Left" />
                                        <ext:ListItem Text="Bên Phải" Value="Right" />
                                        <ext:ListItem Text="Phía trên" Value="Top" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:ComboBox runat="server" ID="cbFormLayout" SelectedIndex="0" LabelAlign="Top"
                                    EnableViewState="true" Editable="false" FieldLabel="Bố cục">
                                    <Items>
                                        <ext:ListItem Text="RowLayout" Value="RowLayout" />
                                        <ext:ListItem Text="ColumnLayout" Value="ColumnLayout" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:Checkbox runat="server" ID="chkDisplayText" FieldLabel="Hiện tiêu đề" EnableViewState="true" />
                                <ext:Checkbox runat="server" ID="chkIsTab" FieldLabel="Chọn làm Tab" EnableViewState="true" />
                            </Items>
                        </ext:Panel>
                    </East>
                </ext:BorderLayout>
            </Items>
        </ext:Panel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnSaveLayout" Text="Lưu cấu hình" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="btnSaveLayout_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                    <ExtraParams>
                        <ext:Parameter Name="order" Value="$('#sortable').sortable('toArray').toString()"
                            Mode="Raw">
                        </ext:Parameter>
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="btnCloseSetupLayout" runat="server" Text="Đóng lại" Icon="Decline">
            <%--<Listeners>
                <Click Handler="#{wdSetupLayout}.hide();" />
            </Listeners>--%>
        </ext:Button>
    </Buttons>
    <%--<Listeners>
        <BeforeShow Handler="SetGridStore(#{GridPanel1},#{Store1},#{DirectMethods});
                            $('#sortable').sortable();
                            $('#sortable').disableSelection();
                            #{btnDeleteGroup}.disable();
                            #{btnSaveFormGroupInformation}.disable();" />
    </Listeners>--%>
</ext:Window>
<ext:Window runat="server" Title="Cấu hình Form" Modal="true" Width="320" Hidden="true"
    AutoHeight="true" Icon="Cog" Padding="6" ID="wdConfigWindow">
    <Content>
        <ext:TextField ID="txtTitle" runat="server" AllowBlank="false" BlankText="Nhập tiêu đề form"
            Width="263" FieldLabel="Tiêu đề form" />
        <ext:ComboBox runat="server" ID="TableList1" StoreID="storeTable" ValueField="name"
            Width="263" DisplayField="name" Editable="false">
        </ext:ComboBox>
        <ext:SpinnerField runat="server" ID="SpinnerWidth" FieldLabel="Dài" />
        <ext:SpinnerField runat="server" ID="SpinnerHeight" FieldLabel="Cao" /> 
        <ext:TextField ID="txtIcon" runat="server" Width="263" FieldLabel="Icon" />
        <ext:Checkbox runat="server" ID="chkResizeable" FieldLabel="Resizeable" /> 
        <ext:ComboBox runat="server" ID="cbCommandButton" Width="263" Editable="false" FieldLabel="Command Button">
            <Items>
                <ext:ListItem Text="Display" Value="Display" />
                <ext:ListItem Text="Update" Value="Update" />
                <ext:ListItem Text="Insert" Value="Insert" />
            </Items>
        </ext:ComboBox>
    </Content>
    <Buttons>
        <ext:Button runat="server" Text="Cập nhật" Icon="Disk" ID="btnUpdateConfig">
            <DirectEvents>
                <Click OnEvent="btnUpdateConfig_Click">
                    <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Icon="Decline" Text="Đóng lại" ID="wdConfigWindow_btnCancel">
            <%--<Listeners>
                <Click Handler="#{wdConfigWindow}.hide();" />
            </Listeners>--%>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Store ID="StoreFieldInfo" AutoSave="true" RefreshAfterSaving="None" OnBeforeStoreChanged="HandleFieldChanges"
    SkipIdForNewRecords="false" ShowWarningOnFailure="true" runat="server">
    <Proxy>
        <ext:HttpProxy Method="GET" Url="Handler.ashx" />
    </Proxy>
    <Reader>
        <ext:JsonReader IDProperty="ID" Root="Data" TotalProperty="TotalRecords">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="FormID" />
                <ext:RecordField Name="TimeSheetHandlerType" />
                <ext:RecordField Name="LabelName" />
                <ext:RecordField Name="ValidateTimeSheetHandlerType" />
                <ext:RecordField Name="FieldNote" />
                <ext:RecordField Name="Position" />
                <ext:RecordField Name="Width" />
                <ext:RecordField Name="Height" />
                <ext:RecordField Name="ControlConfig" />
                <ext:RecordField Name="TableName" />
                <ext:RecordField Name="ColumnName" />
                <ext:RecordField Name="Visible" />
                <ext:RecordField Name="AutoGenerateValue" />
                <ext:RecordField Name="InputControl" />
                <ext:RecordField Name="GroupID" />
                <ext:RecordField Name="Disable" />
            </Fields>
        </ext:JsonReader>
    </Reader>
    <BaseParams>
        <ext:Parameter Name="SearchKey" Value="#{txtKeyword}.getValue()" Mode="Raw" />
    </BaseParams>
</ext:Store>
<ext:Window runat="server" ID="wdSetComboBox" Hidden="true" Icon="Cog" Border="true"
    Modal="true" Title="Cấu hình combobox" Padding="6" Width="400" AutoHeight="true">
    <Content>
        <ext:ComboBox runat="server" ID="cbTableComboBox" Width="350" StoreID="storeTable"
            FieldLabel="Tên bảng" ValueField="name" DisplayField="name" Editable="false">
        </ext:ComboBox>
        <ext:ComboBox runat="server" ID="cbDisplayColumn" Width="350" DisplayField="Name"
            FieldLabel="Cột hiển thị" StoreID="ColumnStore" ValueField="Name" Editable="false">
        </ext:ComboBox>
        <ext:ComboBox runat="server" ID="cbValueColumn" Width="350" DisplayField="Name" FieldLabel="Cột giá trị"
            StoreID="ColumnStore" ValueField="Name" Editable="false">
        </ext:ComboBox>
        <ext:Checkbox runat="server" ID="chkAllowInsert" FieldLabel="Có nút thêm mới" />
    </Content>
    <Buttons>
        <ext:Button runat="server" ID="btnwdSetComboBox" Text="Lưu" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="btnwdSetComboBox_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button2" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdSetComboBox}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" ID="wdSetAjaxSearch" Hidden="true" Icon="Cog" Border="true"
    Modal="true" Title="Cấu hình Ajax Search" Padding="6" Width="400" AutoHeight="true">
    <Content>
        <ext:Hidden runat="server" ID="hdfAjaxSearchConfig" />
        <ext:ComboBox runat="server" ID="cbTableSearch" StoreID="storeTable" Width="350"
            FieldLabel="Tên bảng" ValueField="name" DisplayField="name" Editable="false">
        </ext:ComboBox>
        <ext:ComboBox runat="server" ID="cbDisplayField" DisplayField="Name" Width="350"
            FieldLabel="Trường hiển thị" StoreID="ColumnStore" ValueField="Name" Editable="false">
        </ext:ComboBox>
        <ext:ComboBox runat="server" ID="cbValueField" DisplayField="Name" Width="350" FieldLabel="Trường giá trị"
            StoreID="ColumnStore" ValueField="Name" Editable="false">
        </ext:ComboBox>
        <ext:ComboBox runat="server" ID="cbSearchField" DisplayField="Name" Width="350" FieldLabel="Trường tìm kiếm"
            StoreID="ColumnStore" ValueField="Name" Editable="false">
        </ext:ComboBox>
        <ext:NumberField runat="server" ID="txtPageSize" Text="5" Note="Số dòng trên 1 trang"
            NoteAlign="Top" Width="350" FieldLabel="Số dòng" />
        <ext:Checkbox runat="server" ID="chkAllowInsertAjaxSearch" FieldLabel="Cho phép thêm mới" />
    </Content>
    <Buttons>
        <ext:Button runat="server" ID="btnSetAjaxSearch" Text="Lưu" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="btnSetAjaxSearch_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button3" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdSetAjaxSearch}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" ID="wdBooleanControl" Title="Cấu hình RadioButton" Icon="Cog"
    Modal="true" Hidden="true" Border="false" Padding="6" Width="400" AutoHeight="true">
    <Items>
        <ext:TextField runat="server" ID="txtTrueLabel" FieldLabel="True label" Width="350"
            AllowBlank="false" />
        <ext:TextField runat="server" ID="txtFalseLabel" FieldLabel="False label" Width="350"
            AllowBlank="false" />
    </Items>
    <Buttons>
        <ext:Button ID="Button4" runat="server" Text="Lưu" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="wdBooleanControl_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button5" runat="server" Text="Hủy" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdBooleanControl}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" ID="WdUpdateHtmlConfig" Title="Cấu hình HTML Control"
    Icon="Cog" Hidden="true" Modal="true" Border="false" Padding="6" Width="400"
    AutoHeight="true">
    <Items>
        <ext:NumberField runat="server" ID="txtHtmlHeight" Width="350" FieldLabel="Nhập chiều cao" />
    </Items>
    <Buttons>
        <ext:Button runat="server" Text="Cập nhật" Icon="Disk" ID="btnUpdateHtmlConfig">
            <DirectEvents>
                <Click OnEvent="btnUpdateHtmlConfig_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button1" runat="server" Text="Hủy" Icon="Decline">
            <Listeners>
                <Click Handler="#{WdUpdateHtmlConfig}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" ID="wdDateRangeConfig" Hidden="true" Modal="true" Padding="6"
    AutoHeight="true" Width="400" Title="Chọn ">
    <Items>
        <ext:RadioGroup runat="server" ID="rgwdDateRangeConfig">
            <Items>
                <ext:Radio runat="server" ID="rdStartDate" FieldLabel="Chọn làm ngày bắt đầu">
                    <Listeners>
                        <Check Handler="if(#{rdStartDate}.checked){#{cbEndDate}.show();#{cbStartDate}.hide();}" />
                    </Listeners>
                </ext:Radio>
                <ext:Radio runat="server" ID="rdEndDate" FieldLabel="Chọn làm ngày kết thúc">
                    <Listeners>
                        <Check Handler="if(#{rdEndDate}.checked){#{cbEndDate}.hide();#{cbStartDate}.show();}" />
                    </Listeners>
                </ext:Radio>
            </Items>
        </ext:RadioGroup>
        <ext:Hidden runat="server" ID="hdfDataRangeConfig" />
        <ext:ComboBox runat="server" ID="cbStartDate" Hidden="true" SelectedIndex="0" Editable="false"
            Width="350" FieldLabel="Ngày bắt đầu">
        </ext:ComboBox>
        <ext:ComboBox runat="server" ID="cbEndDate" Hidden="true" SelectedIndex="0" Editable="false"
            Width="350" FieldLabel="Ngày kết thúc">
        </ext:ComboBox>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="Button7" Icon="Accept" Text="Đồng ý">
            <Listeners>
                <Click Handler=" if(#{rdStartDate}.checked)
                                 {
                                      #{cbValidateTimeSheetHandlerType}.setValue('DateRange:endDateField-'+ #{cbEndDate}.getValue()); 
                                 }
                                 else
                                 {
                                    #{cbValidateTimeSheetHandlerType}.setValue('DateRange:startDateField-'+ #{cbStartDate}.getValue());
                                 }
                                 #{wdDateRangeConfig}.hide();
                                " />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" ID="wdSetStringLength" Hidden="true" Modal="true" Padding="6"
    AutoHeight="true" Width="300" Title="Cấu hình khoảng số nguyên, độ dài chuỗi">
    <Items>
        <ext:FormPanel runat="server" ID="frmwdSetStringLength" Padding="6" Border="true"
            Frame="false">
            <Items>
                <ext:NumberField ID="txtStringMin" runat="server" Number="5" Vtype="numberrange"
                    FieldLabel="Độ dài tối thiểu" EndNumberField="#{txtStringMax}" MinValue="0" />
                <ext:NumberField ID="txtStringMax" runat="server" Vtype="numberrange" FieldLabel="Độ dài tối đa"
                    StartNumberField="#{txtStringMin}" />
            </Items>
        </ext:FormPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnGetStringLength" Icon="Accept" Text="Đồng ý">
            <Listeners>
                <Click Handler="if(#{frmwdSetStringLength}.getForm().isValid()==false || #{txtStringMin}.getValue()=='' || #{txtStringMax}.getValue()=='')
                                {
                                    Ext.Msg.alert('Thông báo', 'Dữ liệu bạn nhập vào không hợp lệ !');
                                }
                                else
                                { 
                                    #{cbValidateTimeSheetHandlerType}.setValue(#{cbValidateTimeSheetHandlerType}.getValue()+':'+#{txtStringMin}.getValue()+'-'+#{txtStringMax}.getValue());  
                                    #{wdSetStringLength}.hide();
                                }
                                " />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window ID="wdImageInputControl" runat="server" AutoHeight="true" Width="400" Hidden="true" Modal="true" Title="Cấu hình công cụ ảnh" Padding="6">
    <Items>
        <ext:Checkbox runat="server" FieldLabel="Cho phép thay đổi ảnh" ID="chkAllowChangeImage">
           <%-- <Listeners>
                <Change Handler="if(#{chkAllowChangeImage}.checked){
                                     #{txtImageFolderUrl}.enable();
                                 }else
                                 {
                                     #{txtImageFolderUrl}.disable();
                                 }" />
            </Listeners>--%>
        </ext:Checkbox>
        <ext:TextField runat="server" ID="txtImageFolderUrl" FieldLabel="Đường dẫn lưu ảnh" />
    </Items>
    <Buttons>
        <ext:Button runat="server" Text="Đồng ý" Icon="Accept">
            <DirectEvents>
                <Click OnEvent="mnuImage_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdImage}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Window runat="server" Title="Cấu hình các thành phần trên form" Icon="Cog" Hidden="true"
    Maximizable="true" AutoScroll="true" Resizable="true" Border="false" ID="wdConfigElement"
    Modal="true" Height="450" Width="950">
    <TopBar>
        <ext:Toolbar ID="Toolbar3" runat="server">
            <Items> 
                <ext:Button runat="server" ID="btnAdduserControl" Icon="Add" Text="Chọn công cụ nhập liệu">
                    <Menu>
                        <ext:Menu ID="Menu2" runat="server">
                            <Items>
                                <ext:MenuItem runat="server" ID="btnSetDefaultControl" Text="Chuyển về công cụ mặc định">
                                    <DirectEvents>
                                        <Click OnEvent="btnSetDefaultControl_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuComboBox" runat="server" Text="Danh sách ComboBox">
                                    <DirectEvents>
                                        <Click OnEvent="mnuComboBox_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuTreePanel" runat="server" Text="Danh sách dạng cây">
                                    <DirectEvents>
                                        <Click OnEvent="mnuTreePanel_Click" />
                                    </DirectEvents>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuBooleanControl" runat="server" Text="Dữ liệu kiểu boolean">
                                    <DirectEvents>
                                        <Click OnEvent="mnuBooleanControl_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuAjaxSearch" runat="server" Text="Tìm kiếm dạng Ajax">
                                    <DirectEvents>
                                        <Click OnEvent="mnuAjaxSearch_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuHTMLInput" runat="server" Text="Nhập văn bản dạng HTML">
                                    <DirectEvents>
                                        <Click OnEvent="mnuHTMLInput_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuImage" runat="server" Text="Hiển thị dưới dạng ảnh">
                                   <%-- <DirectEvents>
                                        <Click OnEvent="mnuImage_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>--%>
                                    <Listeners>
                                        <Click Handler="#{wdImageInputControl}.show();" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="mnuTextArea" runat="server" Text="TextArea">
                                    <DirectEvents>
                                        <Click OnEvent="mnuTextArea_Click">
                                            <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                        </Click>
                                    </DirectEvents>
                                </ext:MenuItem>
                            </Items>
                        </ext:Menu>
                    </Menu>
                </ext:Button>
                <ext:ToolbarFill runat="server" Width="300" />
                <ext:TextField runat="server" ID="txtKeyword" EnableKeyEvents="true" EmptyText="Nhập tên Label hoặc tên cột">
                    <Listeners>
                        <KeyPress Fn="SearchFieldPressHandler" />
                    </Listeners>
                </ext:TextField>
                <ext:Button runat="server" Icon="Zoom" Text="Tìm kiếm" ID="btnSearchField">
                    <Listeners>
                        <Click Handler="#{StoreFieldInfo}.reload();" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:Toolbar>
    </TopBar>
    <Items>
        <ext:GridPanel Border="true" runat="server" StoreID="StoreFieldInfo" Height="450"
            AnchorHorizontal="100%" ID="gdPanel" AutoExpandColumn="LabelColumn">
            <ColumnModel ID="ColumnModel2" runat="server">
                <Columns>
                    <ext:RowNumbererColumn />
                    <ext:Column Header="Tên Label" ColumnID="LabelColumn" DataIndex="LabelName" Sortable="true">
                        <Editor>
                            <ext:TextField ID="txtLabelName" runat="server" EmptyText="Nhập tên label" AllowBlank="true" />
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Trạng thái" DataIndex="Visible" Width="60">
                        <Editor>
                            <ext:Checkbox ID="Checkbox1" runat="server" />
                        </Editor>
                        <Renderer Fn="GetVisibleStatus" />
                    </ext:Column>
                    <ext:Column Header="Width" DataIndex="Width" Width="45">
                        <Editor>
                            <ext:SpinnerField ID="SpinnerField3" AllowDecimals="true" IncrementValue="0.1" runat="server">
                            </ext:SpinnerField>
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Height" DataIndex="Height" Width="45">
                        <Editor>
                            <ext:SpinnerField ID="SpinnerField2" AllowDecimals="true" IncrementValue="0.1" runat="server">
                            </ext:SpinnerField>
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Vị trí" DataIndex="Position" Width="45">
                        <Editor>
                            <ext:SpinnerField ID="SpinnerField1" runat="server">
                            </ext:SpinnerField>
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Field Note" DataIndex="FieldNote" Width="200">
                        <Editor>
                            <ext:TextField runat="server" ID="txtFieldNote"/>
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Kiểu bắt lỗi" DataIndex="ValidateTimeSheetHandlerType" Width="145">
                        <Editor>
                            <ext:ComboBox ID="cbValidateType" runat="server">
                                <Items>
                                    <ext:ListItem Text="Không bắt lỗi" Value="" />
                                    <ext:ListItem Text="Email" Value="Email" />
                                    <ext:ListItem Text="Bắt buộc phải nhập" Value="NotAllowBlank" />
                                    <ext:ListItem Text="Độ dài chuỗi" Value="StringLength" />
                                    <ext:ListItem Text="Chỉ được phép nhập số" Value="OnlyAllowNumber" />
                                    <ext:ListItem Text="Khoảng số nguyên" Value="IntegerRange" />
                                    <ext:ListItem Text="Khoảng ngày tháng" Value="DateRange" />
                                    <ext:ListItem Text="Ngày tháng < ngày hiện tại" Value="DateTimeSmallerThanToday" />
                                    <ext:ListItem Text="Ngày tháng > ngày hiện tại" Value="DateTimeBiggerThanToday" />
                                </Items>
                                <Listeners>
                                    <Select Handler="if(#{cbValidateTimeSheetHandlerType}.getValue()=='StringLength' || #{cbValidateTimeSheetHandlerType}.getValue()=='IntegerRange')
                                                     {
                                                        #{wdSetStringLength}.show();
                                                     } 
                                                     if(#{cbValidateTimeSheetHandlerType}.getValue()=='DateRange')
                                                     {
                                                        #{wdDateRangeConfig}.show();
                                                     }
                                                    " />
                                </Listeners>
                            </ext:ComboBox>
                        </Editor>
                        <Renderer Fn="GetValidateTimeSheetHandlerType" />
                    </ext:Column>
                     
                    <ext:Column Header="Giá trị" Width="150" DataIndex="AutoGenerateValue">
                        <Editor>
                            <ext:ComboBox ID="ComboBox1" runat="server">
                                <Items>
                                    <ext:ListItem Value="" Text="Người dùng nhập" />
                                    <ext:ListItem Value="IntIncrement" Text="Số nguyên tự động tăng" />
                                    <ext:ListItem Value="UseTrigger" Text="Trigger tạo khóa chính" />
                                    <ext:ListItem Value="CreatedDate" Text="Ngày tạo" />
                                    <ext:ListItem Value="CreatedBy" Text="Người tạo" />
                                    <ext:ListItem Value="EdittedDate" Text="Ngày sửa" />
                                    <ext:ListItem Value="EdittedBy" Text="Người sửa" />
                                    <ext:ListItem Value="MaDonVi" Text="Mã đơn vị" />
                                </Items>
                            </ext:ComboBox>
                        </Editor>
                        <Renderer Fn="GetAutoGenerateValue" />
                    </ext:Column>
                    <ext:Column Header="Công cụ" Width="70" Tooltip="Công cụ nhập liệu" DataIndex="InputControl">
                        <Editor>
                            <ext:ComboBox ID="ComboBox2" runat="server">
                                <Items>
                                    <ext:ListItem Text="Mặc định" Value="" />
                                    <ext:ListItem Text="Ảnh" Value="Image" />
                                </Items>
                            </ext:ComboBox>
                        </Editor>
                        <Renderer Fn="GetInputControlValue" />
                    </ext:Column>
                    <ext:Column Header="Disable" DataIndex="Disable" Width="60">
                        <Editor>
                            <ext:Checkbox ID="chkDisable" runat="server" />
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
                <ext:RowSelectionModel runat="server" ID="CheckboxSelectionModel1">
                </ext:RowSelectionModel>
            </SelectionModel> 
        </ext:GridPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" Icon="Decline" Text="Đóng lại" ID="BtnClosewdConfigElement">
            <%--<Listeners>
                <Click Handler="#{wdConfigElement}.hide();" />
            </Listeners>--%>
        </ext:Button>
    </Buttons>
    <%--<Listeners>
        <BeforeShow Handler="#{btnAdduserControl}.disable();" />
    </Listeners>--%>
</ext:Window>
