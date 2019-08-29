<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.UserControl.Modules_Base_ComboAjaxSearch_AjaxSearch" Codebehind="AjaxSearch.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Form/Form.ascx" TagName="Form" TagPrefix="uc3" %> 
<style type="text/css">
    .search-item
    {
        font: normal 11px tahoma, arial, helvetica, sans-serif;
        padding: 3px 10px 3px 10px;
        border: 1px solid #fff;
        border-bottom: 1px solid #eeeeee;
        white-space: normal;
        color: #555;
    }
    
    .search-item h3
    {
        display: block;
        font: inherit;
        font-weight: bold;
        color: #222;
    }
    
    .search-item h3 span
    {
        float: right;
        font-weight: normal;
        margin: 0 0 5px 5px;
        display: block;
        clear: none;
    }
    
    .ext-ie .x-form-text
    {
        position: static !important;
    }
</style>
<asp:PlaceHolder runat="server" ID="plcInput"></asp:PlaceHolder>
<ext:Hidden runat="server" ID="hdfValue" />
<ext:Container ID="cContainerAjaxSearch" runat="server" AnchorHorizontal="100%" Layout="Form" ColumnWidth="1">
    <Items>
        <ext:ComboBox ID="cbAjaxSearch" runat="server" EmptyText="Nhập * hoặc tên để tìm kiếm"
            TypeAhead="false" LoadingText="Đang tìm kiếm..." HideTrigger="true" ItemSelector="div.search-item" MinChars="1">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" Qtip="Hủy" />
                <ext:FieldTrigger Icon="SimplePlus" Qtip="Thêm mới" />
            </Triggers>
            <Listeners>
                <Select Handler="this.triggers[0].show();#{hdfValue}.setValue('');"></Select>
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
            </Listeners>
            <Store>
                <ext:Store ID="Store1" runat="server" AutoLoad="false">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="Handler.ashx" />
                    </Proxy>
                </ext:Store>
            </Store>
            <Template ID="Template1" runat="server" />
        </ext:ComboBox>
    </Items>
</ext:Container>
