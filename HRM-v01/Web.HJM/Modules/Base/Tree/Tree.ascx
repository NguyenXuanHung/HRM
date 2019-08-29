<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.UserControl.Modules_Base_Tree_Tree" Codebehind="Tree.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:TreePanel ID="TreePanel1" runat="server" Width="200" Header="false" Border="false"
    RootVisible="false" AutoScroll="true">
    <Root>
        <ext:TreeNode Text="Composers" Expanded="true">
            <Nodes>
                <ext:TreeNode Text="Công việc hôm nay" Expanded="true" Icon="Time">
                    <Nodes>
                        <ext:TreeNode Text="Sáng" Expanded="true">
                            <Nodes>
                                <ext:TreeNode Text="Lập báo cáo" Icon="TagBlue" />
                                <ext:TreeNode Text="Lịch hẹn phỏng vấn" Icon="TagBlue" />
                                <ext:TreeNode Text="Thuyết trình" Icon="TagBlue" />
                            </Nodes>
                        </ext:TreeNode>
                        <ext:TreeNode Text="Chiều" Expanded="true">
                            <Nodes>
                                <ext:TreeNode Text="Lập báo cáo" Icon="TagBlue" />
                                <ext:TreeNode Text="Lịch hẹn phỏng vấn" Icon="TagBlue" />
                                <ext:TreeNode Text="Thuyết trình" Icon="TagBlue" />
                            </Nodes>
                        </ext:TreeNode>
                    </Nodes>
                </ext:TreeNode>
                <ext:TreeNode Text="Công việc ngày mai" Expanded="false" Icon="Time">
                    <Nodes>
                        <ext:TreeNode Text="Sáng" Expanded="true">
                            <Nodes>
                                <ext:TreeNode Text="Lập báo cáo" Icon="TagBlue" />
                                <ext:TreeNode Text="Lịch hẹn phỏng vấn" Icon="TagBlue" />
                                <ext:TreeNode Text="Thuyết trình" Icon="TagBlue" />
                            </Nodes>
                        </ext:TreeNode>
                        <ext:TreeNode Text="Chiều">
                            <Nodes>
                                <ext:TreeNode Text="Lập báo cáo" Icon="TagBlue" />
                                <ext:TreeNode Text="Lịch hẹn phỏng vấn" Icon="TagBlue" />
                                <ext:TreeNode Text="Thuyết trình" Icon="TagBlue" />
                            </Nodes>
                        </ext:TreeNode>
                    </Nodes>
                </ext:TreeNode>
                <ext:TreeNode Text="Công việc tuần trước" Expanded="false" Icon="Time">
                    <Nodes>
                        <ext:TreeNode Text="Sáng" Expanded="true">
                            <Nodes>
                                <ext:TreeNode Text="Lập báo cáo" Icon="TagBlue" />
                                <ext:TreeNode Text="Lịch hẹn phỏng vấn" Icon="TagBlue" />
                                <ext:TreeNode Text="Thuyết trình" Icon="TagBlue" />
                            </Nodes>
                        </ext:TreeNode>
                        <ext:TreeNode Text="Chiều">
                            <Nodes>
                                <ext:TreeNode Text="Lập báo cáo" Icon="TagBlue" />
                                <ext:TreeNode Text="Lịch hẹn phỏng vấn" Icon="TagBlue" />
                                <ext:TreeNode Text="Thuyết trình" Icon="TagBlue" />
                            </Nodes>
                        </ext:TreeNode>
                    </Nodes>
                </ext:TreeNode>
            </Nodes>
        </ext:TreeNode>
    </Root>
</ext:TreePanel>
