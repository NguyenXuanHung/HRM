<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.UserControl.Modules_Base_ComboBox_ComboBox" Codebehind="ComboBox.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Form/Form.ascx" TagName="Form" TagPrefix="uc3" %> 
<ext:Hidden runat="server" ID="hdfInsertStatus" />
<asp:PlaceHolder runat="server" ID="plcInput"></asp:PlaceHolder>
<ext:Hidden runat="server" ID="hdfValue" /> <%--value khi combobox được gán 1 giá trị--%>
<ext:Container ID="cContainerComboBox" runat="server" Layout="Form" ColumnWidth="1">
    <Items>
        <ext:ComboBox runat="server" ID="cbBox" Editable="false">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" Qtip="Hủy" />
                <ext:FieldTrigger Icon="SimplePlus" Qtip="Thêm mới" />
            </Triggers>
            <Store>
                <ext:Store ID="Store1" runat="server" AutoLoad="false">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="Handler.ashx" />
                    </Proxy>
                </ext:Store>
            </Store>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfValue}.setValue('');" />
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
               <%-- <Hide Handler="alert('hihi');this.reset();" />--%>
            </Listeners>
        </ext:ComboBox>
    </Items>
</ext:Container> 

