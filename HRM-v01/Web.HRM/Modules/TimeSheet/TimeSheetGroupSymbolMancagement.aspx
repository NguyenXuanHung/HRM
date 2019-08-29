<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetGroupSymbolManagement" Codebehind="TimeSheetGroupSymbolMancagement.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>

<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">
        var validationGroupSymbol = function () {
            if (txtGroupName.getValue() == '' || txtGroupName.getValue().trim == '') {
                alert('Bạn chưa nhập tên nhóm ký hiệu chấm công!');
                return false;
            }
          
            return true;
        }

        var GetBooleanIcon = function(value) {
            var imageCheck = "<img  src='/Resource/Images/check.png'>";
            var imageUnCheck = "<img src='/Resource/Images/uncheck.gif'>";
            if (value === "Active") {
                return imageCheck;
            } else if (value === "Locked") {
                return imageUnCheck;
            }
            return "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfKeyRecord" />

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridGroupSymbol" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditGroupSymbol_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button> 
                                           
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); RowSelectionModel1.clearSelections();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeGroupSymbol" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={20}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="GroupTimeSheetSymbol" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                    <ext:RecordField Name="Status" />
                                                    <ext:RecordField Name="Order" />
                                                    <ext:RecordField Name="Group" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên nhóm" Width="200" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:Column ColumnID="Group" Header="Nhóm" Width="200" Align="Left" Locked="true" DataIndex="Group" />
                                        <ext:Column ColumnID="Status" Width="100" Header="Đang sử dụng" Align="Center"
                                            DataIndex="Status">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="150" DataIndex="CreatedBy" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Ngày tạo" Width="150" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfKeyRecord.reset(); " />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="25">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="25" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="RowSelectionModel1.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server" Title="Cập nhật nhóm ký hiệu chấm công" Resizable="true" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="Cog" ID="wdGroupSymbol"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:TextField runat="server" ID="txtGroupName" CtCls="requiredData" FieldLabel="Tên nhóm ký hiệu<span style='color:red;'>*</span>" AnchorHorizontal="100%" />
                    <ext:Checkbox runat="server" FieldLabel="Trạng thái" BoxLabel="Đang được sử dụng" ID="chk_Status">

                    </ext:Checkbox>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validationGroupSymbol();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="UpdateGroup_Click">
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                   
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdGroupSymbol.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

