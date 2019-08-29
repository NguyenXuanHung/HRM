<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.UserControl.Modules_Base_DateFieldCustom_DateFieldCustom" Codebehind="DateFieldCustom.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Container ID="cContainerComboBox" runat="server" Layout="Form" ColumnWidth="1">
    <Items>
        <ext:DateField runat="server" ID="dfCustomDateField" Editable="false">
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Listeners>
                <Select Handler="this.triggers[0].show();" />
                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
            </Listeners>
        </ext:DateField>
    </Items>
</ext:Container>
