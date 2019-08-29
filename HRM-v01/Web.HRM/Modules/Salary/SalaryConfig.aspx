<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Salary.SalaryConfig" CodeBehind="SalaryConfig.aspx.cs" %>

<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" ID="ResourceCommon" />    
    <script type="text/javascript">
        var checkInput = function () {
            if (!cbxDataType.getValue()) {
                alert('Bạn chưa chọn loại dữ liệu!');
                return false;
            }
            if (cbxDataType.getValue() == '1') {
                if (hdfChoseFieldFixed.getValue() == '' || hdfChoseFieldFixed.getValue() == null) {
                    alert('Bạn chưa nhập mã cột!');
                    return false;
                }
            } else if(cbxDataType.getValue() == '5') {
                if (hdfChoseFieldFormula.getValue() == '' || hdfChoseFieldFormula.getValue() == null) {
                    alert('Bạn chưa nhập mã cột!');
                    return false;
                }
            }
            else {
                if (txtColumnCode.getValue() == '' || txtColumnCode.getValue().trim == '') {
                    alert('Bạn chưa nhập mã cột!');
                    return false;
                }
            }

            if (txtDisplay.getValue() == '' || txtDisplay.getValue().trim == '') {
                alert('Bạn chưa nhập tên cột!');
                return false;
            }
            if (cboExcelColumn.getValue() == '' || cboExcelColumn.getValue().trim == '') {
                alert('Bạn chưa nhập cột Excel!');
                return false;
            }

            return true;
        }

        var checkInputPayrollConfig = function () {
            if (txtPayrollConfigDescription.getValue() == '' || txtPayrollConfigDescription.getValue().trim == '') {
                alert('Bạn chưa nhập chi tiết!');
                return false;
            }
            if (txtPayrollConfigName.getValue() == '' || txtPayrollConfigName.getValue().trim == '') {
                alert('Bạn chưa nhập tên danh mục!');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <div>
            <!-- Resource manager -->
            <ext:ResourceManager ID="RM" runat="server" />
            <!-- Hidden field -->
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfConfigId" />

            <!-- Viewport -->
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridSalaryConfig" TrackMouseOver="true" Title="Cấu hình chi tiết" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%" Icon="Cog">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnManageConfig" runat="server" Text="Quản lý danh mục cấu hình bảng lương" Icon="Table" Hidden="true">
                                                <Listeners>
                                                    <Click Handler="wdConfigList.show();" />
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
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdSalaryBoardConfig.show();wdSalaryBoardConfig.setTitle('Thêm cấu hình bảng lương');Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="if (CheckSelectedRows(gridSalaryConfig) == false) {return false;}; " />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnEdit_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="return CheckSelectedRow(gridSalaryConfig);" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="btnDelete_Click">
                                                        <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa?" ConfirmRequest="true" />
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            
                                            <ext:ToolbarSpacer Width="5" />

                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarSpacer Width="10" />

                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="250"
                                                EmptyText="Nhập tên cột, mã cột hoặc tên cột excel">
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
                                    <ext:Store runat="server" ID="storeSalaryConfig">
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="0" Mode="Raw" />
                                            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
                                        </AutoLoadParams>
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="Handlers" Value="SalaryConfig" Mode="Value" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="ConfigId" Value="hdfConfigId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="ColumnCode" />
                                                    <ext:RecordField Name="Display" />
                                                    <ext:RecordField Name="Formula" />
                                                    <ext:RecordField Name="ColumnExcel" />
                                                    <ext:RecordField Name="IsInUsed" />
                                                    <ext:RecordField Name="IsReadOnly" />
                                                    <ext:RecordField Name="IsDisable" />
                                                    <ext:RecordField Name="Order" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="DataType" />
                                                    <ext:RecordField Name="DataTypeName" />
                                                    <ext:RecordField Name="ConfigId" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                        <Listeners>
                                            <Load Handler="#{RowSelectionModel1}.clearSelections();" />
                                        </Listeners>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" Locked="true" />
                                        <ext:Column Header="Mã cột" Width="200" Align="Left" Locked="true" DataIndex="ColumnCode" />
                                        <ext:Column Header="Tên cột" Width="250" Align="Left" Locked="true" DataIndex="Display" />
                                        <ext:Column Header="Cột Excel" Width="100" Align="Left" DataIndex="ColumnExcel" />
                                        <ext:Column Header="Công thức" Width="350" Align="Left" DataIndex="Formula" />
                                        <ext:Column Header="Mô tả công thức" Width="350" Align="Left" DataIndex="Description" />
                                        <ext:Column Header="Loại dữ liệu" Width="150" Align="Left" DataIndex="DataTypeName" />
                                        <ext:Column Header="Đang sử dụng" Width="100" Align="Center" DataIndex="IsInUsed">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column Header="Chỉ đọc" Width="100" Align="Center" DataIndex="IsReadOnly">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column Header="Thứ tự hiển thị" Width="100" Align="Center" DataIndex="Order" Hidden="True" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <View>
                                    <ext:LockingGridView runat="server" />
                                </View>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(RowSelectionModel1.getSelected().id);btnEdit.enable();btnDelete.enable();" />
                                            <RowDeselect Handler="hdfId.reset();btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <DirectEvents>
                                    <RowDblClick>
                                        <EventMask ShowMask="true" />
                                    </RowDblClick>
                                </DirectEvents>

                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbar1" PageSize="50" DisplayInfo="True" DisplayMsg="Từ {0} đến {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:Label runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="50" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window runat="server" ID="wdSalaryBoardConfig" Constrain="true" Modal="true" Title="Tạo mới cấu hình bảng lương"
                Icon="UserAdd" Layout="FormLayout" Resizable="false" AutoHeight="true" Width="650"
                Hidden="true" Padding="6" LabelWidth="120">
                <Items>
                    <ext:Container ID="Container7" runat="server" Layout="Form" Height="400">
                        <Items>
                            <ext:ComboBox ID="cbxDataType" runat="server" CtCls="requiredData" FieldLabel="Loại dữ liệu"
                                AnchorHorizontal="98%" SelectedIndex="0" AllowBlank="True" Width="68">
                                <Items>
                                    <ext:ListItem Text="" Value="" />
                                    <ext:ListItem Text="Giá trị từ trường" Value="1" />
                                    <ext:ListItem Text="Giá trị cố định" Value="2" />
                                    <ext:ListItem Text="Giá trị động" Value="3" />
                                    <ext:ListItem Text="Giá trị công thức" Value="4" />
                                    <ext:ListItem Text="Giá trị từ trường - công thức" Value="5" />
                                </Items>
                                <Listeners>
                                    <Select Handler="if(cbxDataType.getValue() == '1' || cbxDataType.getValue() == '5'){txtColumnCode.hide();cbxChoseFieldFixed.show();} else{txtColumnCode.show();cbxChoseFieldFixed.hide();hdfChoseFieldFixed.reset();cbxChoseFieldFixed.clearValue();cbxChoseFieldFormula.hide();cbxChoseFieldFormula.clearValue();hdfChoseFieldFormula.reset();}"></Select>
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="hdfChoseFieldFixed" />
                            <ext:ComboBox runat="server" ID="cbxChoseFieldFixed" FieldLabel="Mã cột" CtCls="requiredData" DisplayField="Name" ValueField="Name" Hidden="True"
                                LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item" AnchorHorizontal="98%" PageSize="20" Editable="false" AllowBlank="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template runat="server">
                                    <Html>
                                        <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                    </Html>
                                </Template>
                                <Store>
                                    <ext:Store runat="server" ID="cbxChoseFieldFixedStore" AutoLoad="false">
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();hdfChoseFieldFixed.setValue(cbxChoseFieldFixed.getValue()); "></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfChoseFieldFixed.reset();}" />
                                </Listeners>
                            </ext:ComboBox>  
                            
                            <ext:Hidden runat="server" ID="hdfChoseFieldFormula" />
                            <ext:ComboBox runat="server" ID="cbxChoseFieldFormula" FieldLabel="Mã cột" CtCls="requiredData" DisplayField="Name" ValueField="Name" Hidden="True"
                                LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item" AnchorHorizontal="98%" PageSize="20" Editable="True" AllowBlank="false" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template runat="server">
                                    <Html>
                                        <tpl for=".">
						                <div class="list-item"> 
							                {Name}
						                </div>
					                </tpl>
                                    </Html>
                                </Template>
                                <Store>
                                    <ext:Store runat="server" ID="cbxChoseFieldFormulaStore" AutoLoad="false">
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();hdfChoseFieldFormula.setValue(cbxChoseFieldFormula.getValue()); "></Select>
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfChoseFieldFormula.reset();}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:TextField runat="server" ID="txtColumnCode" FieldLabel="Mã cột" AnchorHorizontal="98%" CtCls="requiredData"></ext:TextField>
                            <ext:TextField runat="server" ID="txtDisplay" FieldLabel="Tên cột" AnchorHorizontal="98%" CtCls="requiredData"></ext:TextField>
                            <ext:ComboBox runat="server" ID="cboExcelColumn" DisplayField="Value" ValueField="Value" AnchorHorizontal="98%" CtCls="requiredData" FieldLabel="Cột Excel">
                                <Store>
                                    <ext:Store runat="server" ID="storeExcelColumn" OnRefreshData="storeExcelColumn_OnRefreshData" AutoLoad="True">
                                        <Reader>
                                            <ext:JsonReader IDProperty="Key">
                                                <Fields>
                                                    <ext:RecordField Name="Key" Type="String" />
                                                    <ext:RecordField Name="Value" Type="String" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <Expand Handler="storeExcelColumn.reload();"></Expand>
                                </Listeners>
                            </ext:ComboBox>
                            <ext:TextField runat="server" ID="txtFormula" FieldLabel="Công thức" AnchorHorizontal="98%"></ext:TextField>
                            <ext:TextField runat="server" ID="txtDescription" FieldLabel="Mô tả công thức" AnchorHorizontal="98%"></ext:TextField>
                            <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự hiển thị" AnchorHorizontal="98%" CtCls="requiredData" Hidden="True" />
                            <ext:Checkbox runat="server" BoxLabel="Đang sử dụng" ID="chk_IsInUsed" />
                            <ext:Checkbox runat="server" BoxLabel="Chỉ đọc" ID="chk_IsReadOnly" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnUpdate_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Command" Value="Update" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btUpdateNew" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnUpdate_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu" />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseHL" Text="Đóng lại" Icon="Decline" Hidden="True">
                        <Listeners>
                            <Click Handler="wdSalaryBoardConfig.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdConfigList" Constrain="true"
                Title="Chọn danh mục cấu hình bảng lương" Icon="Table" Layout="FormLayout" Width="660" AutoHeight="true">
                <Items>
                    <ext:GridPanel ID="gridConfigList" runat="server" StripeRows="true" Border="false"
                        AnchorHorizontal="100%" Header="false" Height="350" Title="Danh sách bảng cấu hình"
                        AutoExpandColumn="Name">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="tbgr">
                                <Items>
                                    <ext:Button ID="btnAddPayrollConfig" Icon="Add" runat="server" Text="Thêm">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.btnAddPayrollConfig_Click();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" Disabled="true" ID="btnEditPayrollConfig" Text="Sửa"
                                        Icon="Pencil">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.btnEditPayrollConfig_Click();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" Text="Xóa" Icon="Delete"
                                        Disabled="true" ID="btnDeletePayrollConfig">
                                        <Listeners>
                                            <Click Handler="if (CheckSelectedRows(gridConfigList) == false) {return false;}" />
                                        </Listeners>
                                        <DirectEvents>
                                            <Click OnEvent="btnDeletePayrollConfig_Click">
                                                <Confirmation Title="Cảnh báo" Message="Bạn có chắc chắn muốn xóa không ?"
                                                    ConfirmRequest="true" />
                                                <EventMask ShowMask="true" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store runat="server" ID="store_ConfigList" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="objname" Value="sal_PayrollConfig" Mode="Value" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                            <ext:RecordField Name="CreatedDate" />
                                            <ext:RecordField Name="CreatedBy" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <ColumnModel runat="server">
                            <Columns>
                                <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                <ext:Column ColumnID="Name" Header="Tên bảng cấu hình" Width="160" DataIndex="Name">
                                </ext:Column>
                                <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo"
                                    Width="80" DataIndex="CreatedDate" />
                                <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="160" DataIndex="CreatedBy">
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelSelectConfig">
                                <Listeners>
                                    <RowSelect Handler="btnEditPayrollConfig.enable(); btnDeletePayrollConfig.enable(); hdfConfigId.setValue(RowSelectionModelSelectConfig.getSelected().get('Id'));" />
                                    <RowDeselect Handler="hdfConfigId.reset(); " />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar3" runat="server" PageSize="10">
                                <Items>
                                    <ext:Label ID="Label3" runat="server" Text="Số bản ghi trên một trang" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer8" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox5" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="30" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                        </Items>
                                        <SelectedItem Value="10" />
                                        <Listeners>
                                            <Select Handler="#{PagingToolbar3}.pageSize = parseInt(this.getValue()); #{PagingToolbar3}.doLoad();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Listeners>
                                    <Change Handler="RowSelectionModelSelectConfig.clearSelections();" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button Text="Đồng ý" Icon="Accept" ID="btnAcceptTimeSheetBoard" runat="server">
                        <Listeners>
                            <Click Handler="return CheckSelectedRows(gridConfigList);" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnAcceptConfig_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button7" Text="Đóng lại"
                        Icon="Decline">
                        <Listeners>
                            <Click Handler="wdConfigList.hide();RowSelectionModelSelectTimeSheetReport.clearSelections();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdCreatePayrollConfig" Constrain="true"
                Title="Tạo danh mục cấu hình bảng lương" Icon="Add" Layout="FormLayout" Width="500" AutoHeight="true" Padding="6">
                <Items>
                    <ext:Container ID="Container2" runat="server" Layout="Form" Height="150"
                        LabelWidth="200">
                        <Items>
                            <ext:TextField runat="server" ID="txtPayrollConfigName" AnchorHorizontal="99%" FieldLabel="Tên danh mục cấu hình" CtCls="requiredData" />
                            <ext:TextArea runat="server" ID="txtPayrollConfigDescription" AnchorHorizontal="99%" FieldLabel="Chi tiết" CtCls="requiredData" Height="100" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnCreatePayrollConfig" Text="Đồng ý" Icon="Accept">
                        <Listeners>
                            <Click Handler="return checkInputPayrollConfig();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="CreatePayrollConfig_Click">
                                <EventMask ShowMask="true" Msg="Đang tạo danh mục cấu hình..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdatePayrollConfig" Text="Cập nhật" Icon="Disk" Hidden="true">
                        <Listeners>
                            <Click Handler="return checkInputPayrollConfig();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="UpdatePayrollConfig_Click">
                                <EventMask ShowMask="true" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClosePayrollConfig" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdCreatePayrollConfig.hide(); Ext.net.DirectMethods.ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

