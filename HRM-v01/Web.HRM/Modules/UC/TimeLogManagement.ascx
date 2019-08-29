<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.UC.TimeLogManagement" CodeBehind="~/Modules/UC/TimeLogManagement.ascx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">

</script>


<ext:Store runat="server" ID="storeGroupWorkShift" AutoLoad="false">
    <Proxy>
        <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
    </Proxy>
    <BaseParams>
        <ext:Parameter Name="handlers" Value="GroupWorkShift" />
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

<ext:Window runat="server" Width="800" ID="wdWindow" Layout="FormLayout" Title="Quản lý dữ liệu chấm công"
    Constrain="true" Icon="Cog" Modal="true" Hidden="true" Resizable="false" AutoHeight="true" Padding="6">
    <Items>
        <ext:Container ID="Container3" runat="server" Layout="Form" Height="600">
            <Items>
                <ext:Hidden runat="server" ID="hdfYear" />
                <ext:Hidden runat="server" ID="hdfMonth" />
                <ext:Hidden runat="server" ID="hdfTimeSheetGroupListId"></ext:Hidden>
                <ext:ComboBox runat="server" ID="cboGroupWorkShift"
                              DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupWorkShift"
                              Width="200" ItemSelector="div.list-item" PageSize="20" CtCls="requiredData" FieldLabel="Nhóm phân ca">
                    <Triggers>
                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                    </Triggers>
                    <Template ID="Template4" runat="server">
                        <Html>
                        <tpl for=".">
                            <div class="list-item"> 
                                {Name}
                            </div>
                        </tpl>
                        </Html>
                    </Template>
                    <Listeners>
                        <Select Handler="this.triggers[0].show();#{hdfTimeSheetGroupListId}.setValue(#{cboGroupWorkShift}.getValue());" />
                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); #{hdfTimeSheetGroupListId}.reset(); };" />
                    </Listeners>
                </ext:ComboBox>
               
                <ext:ComboBox runat="server" ID="cbxMonth" Width="100" Editable="false" FieldLabel="Chọn tháng">
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
                        <Select Handler="#{hdfMonth}.setValue(#{cbxMonth}.getValue()); console.log(#{cbxMonth}.getValue());" />
                    </Listeners>
                </ext:ComboBox>
                <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="100">
                    <Listeners>
                        <Spin Handler="#{hdfYear}.setValue(#{spnYear}.getValue());" />
                    </Listeners>
                </ext:SpinnerField>

            </Items>
        </ext:Container>
    </Items>
   
    <Buttons>
        <ext:Button ID="btnDelete" runat="server" Text="Xóa dữ liệu" Icon="Delete">
            <DirectEvents>
                <Click OnEvent="Delete_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
    </Buttons>
</ext:Window>
