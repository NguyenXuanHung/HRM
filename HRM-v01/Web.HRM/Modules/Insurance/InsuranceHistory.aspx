<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceHistory.aspx.cs" Inherits="Web.HRM.Modules.Insurance.InsuranceHistory" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <ext:ResourcePlaceHolder runat="server" Mode="ScriptFiles" />
    <style type="text/css">
        
        /*.x-grid3-td-colIndex
        {
            width: 442px !important;
        }*/
    </style>
    <script type="text/javascript">
        var RenderMonth = function (value, p, record) {
            value = (p.id === "EnterpriseSocial" ? value.EnterpriseSocial : value.LaborerSocial);
          
            return "<span style='float:right;'>" + RenderMoneyVND(value) + "</span>";
        }

        var RenderMoneyVND = function (value) {
            if (value == null || value.length === 0)
                return "0";
            value = Math.round(value);
            var l = (value + "").length;
            var s = value + "";
            var rs = "";
            var count = 0;
            for (var i = l - 1; i > 0; i--) {
                count++;
                if (count == 3) {
                    rs = "." + s.charAt(i) + rs;
                    count = 0;
                }
                else {
                    rs = s.charAt(i) + rs;
                }
            }
            rs = s.charAt(0) + rs;
            if (rs.replace(".", "").trim() * 1 === 0) {
                return "0";
            }

            return  rs;
        }

        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                // reload grid
                reloadGrid();
                // show keyword trigger
                if (this.getValue() === '')
                    this.triggers[0].hide();
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        };
        var reloadGrid = function () {
            gpInsurance.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfYearFilter" />

            <!-- store -->
            <ext:Store runat="server" ID="storeInsurance" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Insurance/HandlerInsurance.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="year" Value="hdfYearFilter.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="RecordId" />
                            <ext:RecordField Name="EmployeeCode" />
                            <ext:RecordField Name="FullName" />
                            <ext:RecordField Name="InsuranceDetailModels" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpInsurance" StoreID="storeInsurance" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" AutoScroll="True">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            
                                            <ext:ToolbarFill />
                                            <ext:SpinnerField runat="server" ID="spnYearFilter" Width="80">
                                                <Listeners>
                                                    <Spin Handler="hdfYearFilter.setValue(spnYearFilter.getValue());reloadGrid();" />
                                                </Listeners>
                                            </ext:SpinnerField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear(); reloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView runat="server" ForceFit="False" />
                                </View>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="" />
                                            <RowDeselect Handler="" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="30" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:Label runat="server" Text="Số bản ghi trên trang:" />
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="30" />
                                                <Listeners>
                                                    <Select Handler="#{pagingToolbar}.pageSize=parseInt(this.getValue());#{pagingToolbar}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="rowSelectionModel.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

        </div>
    </form>
</body>
</html>

