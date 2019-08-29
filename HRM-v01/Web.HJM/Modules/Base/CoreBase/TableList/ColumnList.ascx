<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_CoreBase_TableList_ColumnList" Codebehind="ColumnList.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%--Lấy danh sách tất cả các cột trong 1 bảng--%>
<ext:Hidden runat="server" ID="hdfColumn" />
<ext:Store runat="server" ID="ColumnStore" AutoLoad="false" OnRefreshData="CitiesRefresh">
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
    <Listeners>
        <Load Handler="if(#{hdfColumn}.getValue()!='')
                       {
                           #{cbColumn}.setValue(#{hdfColumn}.getValue());
                       }
                       else
                       { 
                           #{cbColumn}.setValue(#{cbColumn}.store.getAt(0).get('Name'));
                       }" />
    </Listeners>
</ext:Store>

<ext:ComboBox runat="server" ID="cbColumn" DisplayField="Name" StoreID="ColumnStore" ValueField="Name" Editable="false">
    <Listeners>
        <Focus Handler="#{ColumnStore}.reload();" /> 
        <BeforeShow Handler="#{ColumnStore}.reload();" />
    </Listeners>
</ext:ComboBox>
