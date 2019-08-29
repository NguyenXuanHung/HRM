<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HRM.Modules.UserControl.Modules_Base_DropDownFieldTree_DropDownFieldTree" Codebehind="DropDownFieldTree.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">
    var getTasks = function (tree) {
        var msg = [],
                selNodes = tree.getChecked();
        msg.push("[");

        Ext.each(selNodes, function (node) {
            if (msg.length > 1) {
                msg.push(",");
            }

            msg.push(node.text);
        });

        msg.push("]");

        return msg.join("");
    }; 
</script>
<ext:TextField ID="TextField1" runat="server" Text="Node" />
<ext:DropDownField ID="Field3" runat="server" Editable="false" Width="300" TriggerIcon="SimpleArrowDown">
    <Component>
        <%--<ext:TreePanel ID="TreePanel1" runat="server" Title="My Task List" Icon="Accept"
            Height="300" Shadow="None" UseArrows="true" AutoScroll="true" Animate="true"
            EnableDD="true" ContainerScroll="true" RootVisible="false">
            <Buttons>
                <ext:Button ID="Button1" runat="server" Icon="Decline" Text="Đóng lại">
                    <Listeners>
                        <Click Handler="#{Field3}.collapse();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <CheckChange Handler="this.dropDownField.setValue(getTasks(this), false);" />
            </Listeners>
        </ext:TreePanel>--%>
           
    </Component>
    <Listeners>
        <Expand Handler="this.component.getRootNode().expand(true);" Single="true" Delay="10" />
    </Listeners>
    <%--<DirectEvents>
        <Expand OnEvent="TreePanel1_LoadData">
            <EventMask ShowMask="true" Msg="Đang tải dữ liệu..." />
        </Expand>
    </DirectEvents>--%>
</ext:DropDownField>

<ext:Window runat="server" Width="500" Height="300">
    <Content>
        
    </Content>
</ext:Window>


<ext:TreePanel 
            ID="TreePanel1" 
            runat="server" 
            Title="Tree" 
            AutoHeight="true" 
            Border="false">
            <Loader>
                <ext:PageTreeLoader OnNodeLoad="NodeLoad"> 
                </ext:PageTreeLoader>
            </Loader>
            <Root>
                <ext:AsyncTreeNode NodeID="0" Text="Root" />
            </Root>
        </ext:TreePanel>
