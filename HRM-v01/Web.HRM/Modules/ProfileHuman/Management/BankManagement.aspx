<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.Management.BankManagement" Codebehind="BankManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ChooseEmployee.ascx" TagPrefix="uc1" TagName="ChooseEmployee" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript">
        var checkInputBank = function () {
            if (hdfEmployeeSelectedId.getValue() == '' || hdfEmployeeSelectedId.getValue().trim === '') {
                alert('Bạn chưa nhập tên cán bộ!');
                return false;
            }
            if (txtAccountNumber.getValue() === '' || txtAccountNumber.getValue().trim === '') {
                alert('Bạn chưa nhập số tài khoản!');
                return false;
            }
            if (txtAccountName.getValue() === '' || txtAccountName.getValue().trim === '') {
                alert('Bạn chưa nhập tên tài khoản!');
                return false;
            }
            if (hdfBankId.getValue() === '' || hdfBankId.getValue().trim == '') {
                alert('Bạn chưa nhập ngân hàng!');
                return false;
            }
            return true;
        }
        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <uc1:ChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridBank" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%" AutoExpandColumn="Note">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdBank.show();wdBank.setTitle('Tạo mới thông tin thẻ ngân hàng');cbxSelectedEmployee.enable();btnUpdate.hide();btnUpdateNew.show();btnUpdateClose.show();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditBank_Click">
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
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập mã, họ tên CCVC">
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
                                    <ext:Store ID="storeBankATM" AutoSave="true" runat="server" GroupField="FullName">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="Bank" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="AccountName" />
                                                    <ext:RecordField Name="AccountNumber" />
                                                    <ext:RecordField Name="Note" />
                                                    <ext:RecordField Name="IsInUsed" />
                                                    <ext:RecordField Name="BankName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <View>
                                    <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                        ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:GroupingSummaryColumn ColumnID="FullName" DataIndex="FullName" Header="Họ tên" Width="200" Sortable="true" Hideable="false" SummaryType="Count">
                                             <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Cán bộ)' : '(1 cán bộ)');" />
                                        </ext:GroupingSummaryColumn>
                                        <ext:Column Header="Số hiệu CBCC" Width="150" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                         <ext:Column ColumnID="AccountNumber" Header="Số tài khoản" Width="150" DataIndex="AccountNumber"/>
                                        <ext:Column ColumnID="AccountName" Header="Tên tài khoản" Width="170" DataIndex="AccountName"/>
                                        <ext:Column ColumnID="IsInUsed" Width="100" Header="Đang sử dụng" Align="Center"
                                            DataIndex="IsInUsed">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>    
                                        <ext:Column ColumnID="BankName" Header="Tên ngân hàng" Width="150" DataIndex="BankName" />
                                        <ext:Column ColumnID="Note" Header="Ghi chú" DataIndex="Note"/>
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
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfRecordId.reset();hdfKeyRecord.reset(); " />
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
            <ext:Window runat="server" Title="Tạo mới thông tin thẻ ngân hàng" Resizable="true" Layout="FormLayout"
                Padding="6" Width="500" Hidden="true" Icon="UserTick" ID="wdBank"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Hidden runat="server" ID="hdfEmployeeSelectedId" />
                    <ext:ComboBox ID="cbxSelectedEmployee" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                        FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                        ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                        LoadingText="Đang tải dữ liệu..." AnchorHorizontal="100%" runat="server">
                            <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Store>
                            <ext:Store ID="cbxSelectedEmployee_Store" runat="server" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={10}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="SearchUser" />
                                    <ext:Parameter Name="name" />
                                    <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader Root="plants" TotalProperty="total">
                                        <Fields>
                                            <ext:RecordField Name="FullName" />
                                            <ext:RecordField Name="EmployeeCode" />
                                            <ext:RecordField Name="BirthDate" />
                                            <ext:RecordField Name="DepartmentName" />
                                            <ext:RecordField Name="Id" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Template ID="Template4" runat="server">
                            <Html>
                                <tpl for=".">
						            <div class="search-item">
							            <h3>{FullName}</h3>
                                        {EmployeeCode} <br />
                                        <tpl if="BirthDate &gt; ''">{BirthDate:date("d/m/Y")}</tpl><br />
							            {DepartmentName}
						            </div>
					            </tpl>
                            </Html>
                        </Template>
                        <Listeners>
                            <Select Handler="hdfEmployeeSelectedId.setValue(cbxSelectedEmployee.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfEmployeeSelectedId.reset(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:NumberField runat="server" ID="txtAccountNumber" CtCls="requiredData" FieldLabel="Số tài khoản<span style='color:red;'>*</span>"
                        AnchorHorizontal="100%" />
                    <ext:TextField runat="server" ID="txtAccountName" CtCls="requiredData" FieldLabel="Tên tài khoản<span style='color:red;'>*</span>" 
                        AnchorHorizontal="100%"/>
                    <ext:Hidden runat="server" ID="hdfBankId" />
                    <ext:ComboBox runat="server" Editable="false" ID="cbxBank" DisplayField="Name"
                        CtCls="requiredData" ValueField="Id" FieldLabel="Ngân hàng<span style='color:red;'>*</span>"
                        EmptyText="Chọn ngân hàng" AnchorHorizontal="100%">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Store>
                            <ext:Store runat="server" ID="storeBank" AutoLoad="false">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/Catalog/HandlerCatalogBase.ashx" />
                                </Proxy>
                                <BaseParams>
                                    <ext:Parameter Name="objname" Value="cat_Bank" Mode="Value" />
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Select Handler="this.triggers[0].show(); #{hdfBankId}.setValue(#{cbxBank}.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }; #{hdfBankId}.reset();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextArea runat="server" FieldLabel="Ghi chú" AnchorHorizontal="100%" ID="txt_Note" />
                    <ext:Checkbox runat="server" FieldLabel="Trạng thái" BoxLabel="Đang được sử dụng" ID="chk_IsInUsed">
                    </ext:Checkbox>      
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputBank();" />
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
                            <Click Handler="return checkInputBank();" />
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
                    <ext:Button runat="server" ID="btnUpdateClose" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputBank();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnDongLai" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdBank.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

