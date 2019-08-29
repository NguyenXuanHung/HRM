<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogQuantum.aspx.cs" Inherits="Web.HJM.Modules.Catalog.CatalogQuantum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var keyPressSearch = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                PagingToolbar1.doLoad();
                RowSelectionModel1.clearSelections();
                if (this.getValue() == '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() != '') {
                this.triggers[0].show();
            }
        }

        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }

        var checkInput = function () {
            if (txtName.getValue() == '' || txtName.getValue().trim == '') {
                alert('Bạn chưa nhập tên ngạch!');
                return false;
            }

            if (txtCode.getValue() == '' || txtCode.getValue().trim == '') {
                alert('Bạn chưa nhập mã ngạch!');
                return false;
            }

            if (hdfGroupQuantumId.getValue() == '' || hdfGroupQuantumId.getValue().trim == '') {
                alert('Bạn chưa chọn loại nhóm ngạch!');
                return false;
            }


            //if (txtGradeMax.getValue() == null) {
            //    alert('Bạn chưa nhập số bậc tối đa!');
            //    return false;
            //}

            //if (txtMonth.getValue() == null) {
            //    alert('Bạn chưa nhập số tháng nâng lương!');
            //    return false;
            //}


            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>

            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfGroupQuantumId" />

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridQuantum" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%" AutoExpandColumn="Description">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdQuantum.show();wdQuantum.setTitle('Thêm mới thông tin nhóm ngạch');btnUpdate.hide();btnUpdateNew.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditQuantum_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa thông tin này" />
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
                                                    <KeyPress Fn="keyPressSearch" />
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
                                    <ext:Store ID="store_GroupQuantum" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={30}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="Quantum" Mode="Value" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="Code" />
                                                    <ext:RecordField Name="MonthStep" />
                                                    <ext:RecordField Name="Percent" />
                                                    <ext:RecordField Name="GroupQuantumName" />
                                                    <ext:RecordField Name="SalaryGrade" />
                                                </Fields>
                                            </ext:JsonReader>

                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên ngạch" Width="250" Align="Left" Locked="true" DataIndex="Name" />
                                        <ext:Column ColumnID="Code" Header="Mã ngạch" Width="150" Align="Left" Locked="true" DataIndex="Code" />
                                        <ext:Column ColumnID="GroupQuantumName" Header="Nhóm ngạch" Width="150" Align="Left" Locked="true" DataIndex="GroupQuantumName" />
                                        <ext:Column ColumnID="Percent" Header="Phần trăm" Width="150" Align="Left" Locked="true" DataIndex="Percent" />
                                        <ext:Column ColumnID="SalaryGrade" Header="Số bậc" Width="150" Align="Left" Locked="true" DataIndex="SalaryGrade" />
                                        <ext:Column ColumnID="MonthStep" Header="Số tháng nâng lương" Width="150" Align="Left" Locked="true" DataIndex="MonthStep" />
                                        <ext:Column ColumnID="Description" Header="Ghi chú" Width="200" DataIndex="Description" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();" />
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
            <ext:Window runat="server" Title="Thêm mới thông tin ngạch" Resizable="true" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="UserTick" ID="wdQuantum"
                Modal="true" Constrain="true" Height="450" LabelWidth="120">
                <Items>
                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên ngạch<span style='color:red;'>*</span>"
                        AnchorHorizontal="98%" />
                    <ext:TextField runat="server" ID="txtCode" CtCls="requiredData" FieldLabel="Mã ngạch<span style='color:red;'>*</span>"
                        AnchorHorizontal="98%" />
                    <ext:ComboBox runat="server" ID="cbxGroupQuantum" FieldLabel="Nhóm ngạch<span style='color:red;'>*</span>"
                        CtCls="requiredData" DisplayField="Name" ValueField="Id" AnchorHorizontal="98%"
                        Editable="true" ItemSelector="div.list-item" PageSize="20">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Template ID="Template6" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="list-item"> 
							            {Name}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Store>
                            <ext:Store runat="server" ID="storeGroupQuantum" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="objname" Value="cat_GroupQuantum" />
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
                        </Store>
                        <Listeners>
                            <Select Handler="this.triggers[0].show(); hdfGroupQuantumId.setValue(cbxGroupQuantum.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                        </Listeners>
                    </ext:ComboBox>


                    <ext:NumberField runat="server" ID="txtGrade" FieldLabel="Số bậc" AnchorHorizontal="98%" CtCls="requiredData" />
                    <ext:NumberField runat="server" ID="txtMonth" FieldLabel="Số tháng nâng lương" AnchorHorizontal="98%" CtCls="requiredData" />
                    <ext:NumberField runat="server" ID="txtPercent" FieldLabel="Phần trăm" AnchorHorizontal="98%" CtCls="requiredData" />
                    <ext:TextArea runat="server" FieldLabel="Ghi chú" ID="txtNote" AnchorHorizontal="98%" />
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>

                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdQuantum.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
