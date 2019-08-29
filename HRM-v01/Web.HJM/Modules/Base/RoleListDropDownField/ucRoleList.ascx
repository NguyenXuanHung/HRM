<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.UserControl.Modules_RoleListDropDownField_ucRoleList" Codebehind="ucRoleList.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">
    var getRoleName = function (tree) {
        var msg = [],
                selNodes = tree.getChecked();
        msg.push("[");

        Ext.each(selNodes, function (node) {
          //  if (node.getUI().checkbox.checked == true) {
                if (msg.length > 1) {
                    msg.push(",");
                }

                msg.push(node.text);
           // }
        });

        msg.push("]");

        return msg.join("");
    };
    var getRoleID = function (tree) {
        var msg = [],
                selNodes = tree.getChecked();

        Ext.each(selNodes, function (node) {
          //  if (node.getUI().checkbox.checked == true) {
               // if (msg.length > 1) {
                    msg.push(",");
              //  }

                msg.push(node.id);
          //  }
        });


        return msg.join("");
    };
    var RemoveChecked = function (tree) {
        var msg = [],
                selNodes = tree.getChecked();
        Ext.each(selNodes, function (node) {
            node.getUI().checkbox.checked = false;
        });
    }
</script>
<ext:Window runat="server" Hidden="true" Constrain="true" Padding="7" Icon="Key"
    Title="Chọn quyền" Modal="true" Resizable="false" Width="450" ID="wdRoleList"
    AutoHeight="true">
    <Items>
        <ext:TextField runat="server" ID="txtRoleId" />
        <ext:DropDownField ID="Field3" FieldLabel="Chọn quyền" runat="server" Editable="false"
            Width="400" TriggerIcon="SimpleArrowDown">
            <Component>
                <ext:TreePanel ID="TreePanel1" runat="server" Title="Danh mục quyền" Icon="Key" Height="300"
                    Shadow="None" UseArrows="true" AutoScroll="true" Animate="true" EnableDD="true"
                    ContainerScroll="true" RootVisible="false">
                    <Root>
                    </Root>
                    <Listeners>
                        <CheckChange Handler="this.dropDownField.setValue(getRoleName(this), false);#{txtRoleId}.setValue(getRoleID(this));" />
                      <%--<BeforeShow Handler="RemoveChecked(this);" />--%>
                    </Listeners>
                </ext:TreePanel>
            </Component>
            <Listeners>
                <Expand Handler="this.component.getRootNode().expand(true);" Single="true" Delay="10" />
            </Listeners>
        </ext:DropDownField>
    </Items>
    <Buttons>
        <ext:Button runat="server" Text="Đồng ý" ID="btnAccept" Icon="Accept">
            <DirectEvents>
                <Click OnEvent="btnAccept_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Text="Đóng lại" ID="Button2" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdRoleList}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
   <%-- <Listeners>
        <BeforeShow Handler="#{txtRoleId}.setValue('');#{Field3}.setValue('',false);" />
    </Listeners>--%>
</ext:Window>
