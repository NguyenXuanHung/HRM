<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Salary.SalaryBoardInfo" Codebehind="SalaryBoardInfo.aspx.cs" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />    
    <script type="text/javascript">
       
    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfSalaryBoardId" />
            <ext:Hidden runat="server" ID="hdfType" />

            <ext:GridPanel ID="gridSalaryInfo" TrackMouseOver="true" runat="server"
                StripeRows="true" Border="false" AnchorHorizontal="100%" Title="Vui lòng chọn bảng lương" Icon="Date">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tb">
                        <Items>
                            <ext:Button ID="Button3" runat="server" Text="Quản lý bảng lương" Icon="Table" Hidden="true">
                                <Listeners>
                                    <Click Handler="wdSalaryBoardManage.show();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnBack" runat="server" Text="Quay lại" Icon="ArrowLeft">
                                <DirectEvents>
                                    <Click OnEvent="btnBack_Click">
                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Store>
                    <ext:Store runat="server" ID="storeTimeSheet">
                        <Proxy>
                            <ext:HttpProxy Method="POST" Url="~/Services/HandlerSalaryConfig.ashx" />
                        </Proxy>
                        <AutoLoadParams>
                            <ext:Parameter Name="start" Value="={0}" />
                            <ext:Parameter Name="limit" Value="={15}" />
                        </AutoLoadParams>
                        <BaseParams>
                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                            <ext:Parameter Name="year" Value="hdfYear.getValue()" Mode="Raw" />
                            <ext:Parameter Name="month" Value="hdfMonth.getValue()" Mode="Raw" />
                            <ext:Parameter Name="salaryBoardId" Value="hdfSalaryBoardId.getValue()" Mode="Raw" />
                        </BaseParams>
                        <Reader>
                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                <Fields>
                                </Fields>
                            </ext:JsonReader>
                        </Reader>
                    </ext:Store>
                </Store>
            </ext:GridPanel>

            <iframe id="sheetPanel" src="SalaryBoard\index.html?id=<%= SalaryBoardListId %>"></iframe>

        </div>
    </form>
</body>
</html>

