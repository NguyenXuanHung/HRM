<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_CoreBase_DropDownList_DropDownList" Codebehind="TableList.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Hidden runat="server" ID="hdfSelectedTable" />
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
    <Listeners> 
        <Load Handler="if(#{hdfSelectedTable}.getValue()!='')
                        {
                            #{cbTable}.setValue(#{hdfSelectedTable}.getValue());
                        }
                        else
                        {
                            #{cbTable}.setValue(#{cbTable}.store.getAt(0).get('name'));
                        }" /> 
    </Listeners>
</ext:Store> 
<ext:ComboBox runat="server" ID="cbTable" StoreID="storeTable" ValueField="name"
    DisplayField="name" Editable="false"> 
</ext:ComboBox>
