<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_OneManyForm_OneManyForm" Codebehind="OneManyForm.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Form/Form.ascx" TagName="Form" TagPrefix="uc1" %>
<script type="text/javascript">
    var GetStatus = function (value, p, record) {
        if (value == "1")
            return "<span style='color:blue'>Hiện</span>";
        else
            return "<span style='color:red'>Ẩn</span>";
    }
</script>
<ext:Hidden ID="hdfCurrentRecordID" runat="server" />
<ext:Hidden ID="hdfCurrentTableName" runat="server" />
<ext:Hidden ID="hdfPrimaryColumnName" EnableViewState="true" runat="server" /> 
<ext:Hidden runat="server" ID="hdfCommand" >   
</ext:Hidden> 
<ext:Button runat="server" ID="btnUpdate_OneMany" Text="Cập nhật" Icon="Disk">
    <DirectEvents>
        <Click OnEvent="btnUpdate_OneMany_Click">
            <ExtraParams>
                <ext:Parameter Name="SqlCommand" Value="Update" />
            </ExtraParams>
            <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
        </Click>
    </DirectEvents>
    <Listeners>
        <BeforeShow Handler="#{btnUpdate_OneMany}.hide();" />
    </Listeners>
</ext:Button>
<ext:Button runat="server" ID="btnUpdateAndClose" Text="Cập nhật & đóng lại" Icon="Disk">
    <DirectEvents>
        <Click OnEvent="btnUpdate_OneMany_Click">
            <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
            <ExtraParams>
                <ext:Parameter Name="SqlCommand" Value="UpdateAndClose" />
            </ExtraParams>
        </Click>
    </DirectEvents> 
</ext:Button>  
<ext:Store ID="StoreTableDetailInfo" AutoSave="true" RefreshAfterSaving="None" OnBeforeStoreChanged="HandleColumnChanges"
    SkipIdForNewRecords="false" ShowWarningOnFailure="true" runat="server">
    <Proxy>
        <ext:HttpProxy Method="GET" Url="Handler.ashx" />
    </Proxy>
    <Reader>
        <ext:JsonReader IDProperty="ID" Root="Data" TotalProperty="TotalRecords">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="OneManyFormName" />
                <ext:RecordField Name="TableName" />
                <ext:RecordField Name="Order" />
                <ext:RecordField Name="Title" />
                <ext:RecordField Name="ForeignKey" />
                <ext:RecordField Name="Visible" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window runat="server" Title="Chọn bảng detail" Icon="Table" Hidden="true" Padding="6"
    Resizable="false" Border="false" ID="wdConfigTable" AutoHeight="true" Modal="true"
    Width="580">
    <Items>
        <ext:Container runat="server" ID="ctConfig" Height="60" Layout="ColumnLayout">
            <Items>
                <ext:Container runat="server" ID="ctTitle" Height="30" Layout="FormLayout">
                    <Items>
                        <ext:TextField runat="server" ID="txtTitle" FieldLabel="Tiêu đề" />
                        <ext:Checkbox runat="server" ID="chkVisible" Checked="true" AnchorHorizontal="100%"
                            FieldLabel="Hiển thị" />
                    </Items>
                </ext:Container>
                <ext:Container runat="server" ID="Container1" Height="30" Layout="FormLayout">
                    <Items>
                        <ext:ComboBox runat="server" ID="cbTable" SelectedIndex="0" Editable="false" AnchorHorizontal="100%"
                            FieldLabel="Chọn bảng">
                            <%--<DirectEvents>
                                <Select OnEvent="cbTable_Select">
                                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                </Select>
                            </DirectEvents>--%>
                        </ext:ComboBox>
                        <ext:ComboBox runat="server" ID="cbForeignKey" FieldLabel="Khóa ngoại" DisplayField="ColumnName"
                            ValueField="ColumnName">
                            <Store>
                                <ext:Store runat="server" ID="storeForeignKey" OnRefreshData="storeForeignKey_Refresh">
                                    <Proxy>
                                        <ext:PageProxy />
                                    </Proxy>
                                    <Reader>
                                        <ext:ArrayReader>
                                            <Fields>
                                                <ext:RecordField Name="ColumnName" />
                                            </Fields>
                                        </ext:ArrayReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Container>
        <ext:Button runat="server" ID="btnAddConfig" Text="Thêm mới" Icon="Disk">
            <DirectEvents>
                <Click OnEvent="btnAddConfig_Click">
                    <EventMask ShowMask="true" Msg="Đang lưu dữ liệu..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:GridPanel Border="true" runat="server" StoreID="StoreTableDetailInfo" AutoExpandColumn="ColumnHeader"
            Height="200" ID="gdPanel">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:RowNumbererColumn />
                    <ext:Column ColumnID="ColumnName" Header="Tên bảng" Width="70" DataIndex="TableName"
                        Sortable="true">
                        <Editor>
                            <ext:ComboBox runat="server" ID="cbColumnField">
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Tiêu đề" ColumnID="ColumnHeader" DataIndex="Title">
                        <Editor>
                            <ext:TextField ID="TextField2" runat="server" EmptyText="Nhập tiêu đề" AllowBlank="true" />
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Thứ tự" DataIndex="Order" Width="60">
                        <Editor>
                            <ext:SpinnerField ID="Checkbox2" runat="server" />
                        </Editor>
                    </ext:Column>
                    <ext:Column Header="Ẩn hiện" Width="60" DataIndex="Visible">
                        <Editor>
                            <ext:Checkbox ID="Checkbox1" runat="server" />
                        </Editor>
                        <Renderer Fn="GetStatus" />
                    </ext:Column>
                </Columns>
            </ColumnModel>
            <Plugins>
                <ext:RowEditor ID="RowEditor1" ErrorSummary="false" runat="server" CancelText="Hủy"
                    SaveText="Cập nhật">
                </ext:RowEditor>
            </Plugins>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" ID="rse" />
            </SelectionModel>
            <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
        </ext:GridPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" Icon="Decline" ID="btnCloseConfigForm" Text="Đóng lại">
            <Listeners>
                <Click Handler="#{wdConfigTable}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<uc1:Form ID="OneManyForm" WindowHidden="true" FormType="Window" AllowChangeTable="true"
    CommandButton="Display" runat="server" />
