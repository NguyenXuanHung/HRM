<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.WorkProcessManagement" Codebehind="WorkProcessManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
    <script src="../../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <script>
        var keyPresstxtSearch = function (f, e) {
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

        var checkInputCanBo = function () {
            if (cbxChonCanBo.getValue() == '' || cbxChonCanBo.getValue().trim == '') {
                alert('Bạn chưa nhập tên cán bộ!');
                return false;
            }
            if (!txtSoQDMoi.getValue()) {
                alert('Bạn chưa nhập số quyết định!');
                return false;
            }
            if (!dfNgayQDMoi.getValue()) {
                alert('Bạn chưa nhập ngày quyết định!');
                return false;
            }
            if (!cbxChucVu.getValue()) {
                alert('Bạn chưa chọn chức vụ!');
                return false;
            }
            if (!dfNgayHieuLucMoi.getValue()) {
                alert('Bạn chưa chọn ngày quyết định!');
                return false;
            }
            if (!dfExpireDate.getValue()) {
                alert('Bạn chưa chọn thời hạn bổ nhiệm!');
                return false;
            }
            return true;
        }
        var searchBoxPosition = function (f, e) {
            hdfMakerTempPosition.setValue(cbxMakerPosition.getRawValue());
            if (hdfIsMakerPosition.getValue() == '1') {
                hdfIsMakerPosition.setValue('2');
            }
            if (cbxMakerPosition.getRawValue() == '') {
                hdfIsMakerPosition.reset();
            }
        }
        var searchBoxPositionUpdate = function (f, e) {
            hdfUpdateMakerTempPosition.setValue(cbxUpdateMakerPosition.getRawValue());
            if (hdfIsUpdateMakerPosition.getValue() == '1') {
                hdfIsUpdateMakerPosition.setValue('2');
            }
            if (cbxUpdateMakerPosition.getRawValue() == '') {
                hdfIsUpdateMakerPosition.reset();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfRecordId" />
            <ext:Hidden runat="server" ID="hdfMaDonVi" />
            <ext:Hidden runat="server" ID="hdfMenuID" />
            <ext:Hidden runat="server" ID="hdfUserID" />
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfPositionId" />
            <ext:Hidden runat="server" ID="hdfOldPositionId" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <!-- store chức vụ -->
             <ext:Store ID="storePosition" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="objname" Value="cat_Position" Mode="Value" />
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
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="grp_QuanLyNangNgach" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdTaoQuyetBoNhiemChucVu.show();" />
                                                </Listeners>
                                                
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowWorkProcess">
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
                                            <ext:Button runat="server" ID="btnReport" Text="In quyết định" Icon="Printer" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="ShowReport_Click">
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
                                    <ext:Store runat="server" ID="storeWorkProcess">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />

                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="WorkProcess" />
                                            <ext:Parameter Name="MaDonVi" Value="hdfMaDonVi.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="userID" Value="hdfUserID.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="menuID" Value="hdfMenuID.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Key" Root="plants" TotalProperty="total">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="Key" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="ManagementName" />
                                                    <ext:RecordField Name="DecisionNumber" />
                                                    <ext:RecordField Name="DecisionDate" />
                                                    <ext:RecordField Name="EffectiveDate" />
                                                    <ext:RecordField Name="PositionName" />
                                                    <ext:RecordField Name="NewDecisionDate" />
                                                    <ext:RecordField Name="DecisionMaker" />
                                                    <ext:RecordField Name="IsApproved" />
                                                    <ext:RecordField Name="NewPositionId" />
                                                    <ext:RecordField Name="NewPositionName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column Header="Mã CCVC" Width="100" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ tên" Width="220" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column Header="Phòng ban" Width="120" DataIndex="DepartmentName" />
                                        <ext:Column Header="Đơn vị quản lý" Width="170" DataIndex="ManagementName" />
                                        <ext:Column Header="Số quyết định" Width="230" DataIndex="DecisionNumber" />
                                        <ext:DateColumn Header="Ngày bổ nhiệm cũ" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn Header="Ngày có hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Chức vụ đang đảm nhận" Width="140" Align="Left" DataIndex="PositionName" />
                                        <ext:DateColumn Header="Ngày bổ nhiệm mới" Width="145" Align="Left" DataIndex="NewDecisionDate" Css="color:blue;font-weight:bold;" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Chức vụ mới" Width="140" Align="Left" Css="color:red;font-weight:bold;" DataIndex="NewPositionName" />
                                        <ext:Column Header="Người quyết định" Width="180" DataIndex="DecisionMaker">
                                        </ext:Column>
                                        <ext:CheckColumn Header="Duyệt" Width="80" Align="Left" DataIndex="Duyet">
                                        </ext:CheckColumn>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:LockingGridView runat="server" ID="LockingGridView1">
                                    </ext:LockingGridView>
                                </View>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnEdit.enable();btnDelete.enable();btnReport.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfRecordId.setValue(RowSelectionModel1.getSelected().get('RecordId'));hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Key')); " />
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
            <ext:Window runat="server" Title="Tạo quyết định bổ nhiệm chức vụ" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdTaoQuyetBoNhiemChucVu"
                Modal="true" Constrain="true" Height="500">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandName" />
                    <ext:FieldSet runat="server" ID="FieldSet9" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Ctn11" Layout="ColumnLayout" Height="50" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" ID="Ctn12" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfChonCanBo" />
                                            <ext:ComboBox ID="cbxChonCanBo" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                                FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                                                ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                                LoadingText="Đang tải dữ liệu..." AnchorHorizontal="98%" runat="server">
                                                <Store>
                                                    <ext:Store ID="cbxChonCanBo_Store" runat="server" AutoLoad="false">
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
                                                    <Select Handler="hdfChonCanBo.setValue(cbxChonCanBo.getValue());txtSoQDMoi.enable();dfNgayQDMoi.enable();cbxChucVu.enable();dfNgayHieuLucMoi.enable();txtNguoiQD.enable();dfExpireDate.enable();" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Select OnEvent="cbxChonCanBo_Selected" />
                                                </DirectEvents>
                                            </ext:ComboBox>
                                            <ext:TextField runat="server" ID="txtDepartment" FieldLabel="Bộ phận" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Ctn13" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtChucVu" FieldLabel="Chức vụ" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtCongViec" FieldLabel="Chức danh" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="Ctn15" Layout="ColumnLayout" Height="420" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Ctn16" Layout="FormLayout" ColumnWidth="0.34">
                                <Items>
                                    <ext:FieldSet runat="server" ID="Fs10" Layout="FormLayout" Title="Thông tin quyết định Chức vụ gần nhất"
                                        AnchorHorizontal="98%" Height="320">
                                        <Items>
                                            <ext:Container runat="server" ID="Ctn21" Layout="FormLayout" Height="150">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtSoQDCu" FieldLabel="Số quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:DateField runat="server" ID="dfNgayQDCu" FieldLabel="Ngày quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-control-style">
                                                    </ext:DateField>
                                                    <ext:TextField runat="server" ID="txtNguoiQDCu" FieldLabel="Người quyết định" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                    <ext:DateField runat="server" ID="dfNgayCoHieuLucCu" FieldLabel="Ngày hiệu lực" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-control-style">
                                                    </ext:DateField>
                                                    <ext:TextField runat="server" ID="txtBoPhanCu" FieldLabel="Bộ phận cũ" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                   <ext:TextField runat="server" ID="txtChucVuCu" FieldLabel="Chức vụ cũ" AnchorHorizontal="100%"
                                                        Disabled="true" DisabledClass="disabled-input-style">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Ctn17" Layout="FormLayout" ColumnWidth="0.66" Hidden="false" Height="440">
                                <Items>
                                    <ext:FieldSet runat="server" ID="FieldSet5" Title="Thông tin quyết định mới" Layout="FormLayout"
                                        AnchorHorizontal="100%" LabelWidth="110" Height="420">
                                        <Items>
                                            <ext:Container runat="server" ID="Container19" Layout="FormLayout" ColumnWidth="0.5"
                                                LabelWidth="110">
                                                <Items>
                                                    <ext:TextField runat="server" ID="txtSoQDMoi" CtCls="requiredData" Disabled="true" AnchorHorizontal="100%"
                                                        FieldLabel="Số quyết định<span style='color:red'>*</span>" MaxLength="20">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" ID="Container20" Layout="FormLayout" ColumnWidth="0.5"
                                                LabelWidth="107">
                                                <Items>
                                                    <ext:DateField runat="server" ID="dfNgayQDMoi" CtCls="requiredData" FieldLabel="Ngày quyết định<span style='color:red'>*</span>" Disabled="true"
                                                        Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                                    </ext:DateField>
                                                </Items>
                                            </ext:Container>
                                                <ext:Container runat="server" ID="Container2" Layout="FormLayout" ColumnWidth="0.5"
                                                LabelWidth="110">
                                                <Items>
                                                    <ext:ComboBox runat="server" ID="cbxChucVu" FieldLabel="Chức vụ <span style='color:red'>*</span>" Disabled="true"
                                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" CtCls="requiredData"
                                                        LabelWidth="130" Width="370" Editable="true" ItemSelector="div.list-item" EmptyText="Gõ để tìm kiếm"
                                                        ListWidth="250" LoadingText="Đang tải dữ liệu" StoreID="storePosition">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                        </Triggers>
                                                        <Template ID="Template53" runat="server">
                                                            <Html>
                                                                <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                            </Html>
                                                        </Template>
                                                        <ToolTips>
                                                            <ext:ToolTip runat="server" ID="ToolTip1" Title="Hướng dẫn" Html="Nhập chức vụ để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                                                        </ToolTips>
                                                        <Listeners>
                                                            <Expand Handler="storePosition.reload();"/>
                                                            <Select Handler="this.triggers[0].show(); hdfPositionId.setValue(cbxChucVu.getValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfPositionId.reset(); }" />
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Select OnEvent="cbxChucVu_Selected" />
                                                        </DirectEvents>
                                                    </ext:ComboBox>
                                                    <ext:DateField runat="server" ID="dfNgayHieuLucMoi" CtCls="requiredData" FieldLabel="Ngày hiệu lực<span style='color:red'>*</span>" Disabled="true"
                                                        Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();" />
                                                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:DateField>
                                                    <ext:DateField runat="server" ID="dfExpireDate" CtCls="requiredData" FieldLabel="Thời hạn bổ nhiệm<span style='color:red'>*</span>" Disabled="true"
                                                        Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();" />
                                                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:DateField>
                                                        <ext:TextField runat="server" ID="txtSourceDepartment" FieldLabel="Cơ quan bổ nhiệm"
                                                        AnchorHorizontal="100%"/>
                                                    <ext:Hidden runat="server" ID="hdfNguoiQuyetDinh" />
                                                    <ext:TextField runat="server" ID="txtNguoiQD" FieldLabel="Người QĐ" Disabled="true"
                                                        AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                    <ext:Hidden runat="server" ID="hdfIsMakerPosition" Text="0" />
                                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                                    <ext:Hidden runat="server" ID="hdfMakerPosition" />
                                                    <ext:Hidden runat="server" ID="hdfMakerTempPosition" />
                                                    <ext:ComboBox runat="server" ID="cbxMakerPosition" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                        EnableKeyEvents="true" StoreID="storePosition">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Template ID="Template10" runat="server">
                                                            <Html>
                                                                <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                            </Html>
                                                        </Template>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();  hdfMakerPosition.setValue(cbxMakerPosition.getValue());
				                                                                    hdfIsMakerPosition.setValue('1');
				                                                                    hdfMakerTempPosition.setValue(cbxMakerPosition.getRawValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsMakerPosition.reset();hdfMakerTempPosition.reset();hdfMakerPosition.reset();  }" />
                                                            <KeyUp Fn="searchBoxPosition" />
                                                            <Blur Handler="cbxMakerPosition.setRawValue(hdfMakerTempPosition.getValue());
			                                                                        if (hdfIsMakerPosition.getValue() != '1') {cbxMakerPosition.setValue(hdfMakerTempPosition.getValue());}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Hidden runat="server" ID="hdfTepTinDinhKem" />
                                            <ext:CompositeField ID="composifieldAttach" runat="server" AnchorHorizontal="100%"
                                                FieldLabel="Tệp tin đính kèm">
                                                <Items>
                                                    <ext:FileUploadField ID="fufTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                                                        ButtonText="" Icon="Attach" Width="367" AnchorHorizontal="100%">
                                                    </ext:FileUploadField>
                                                    <ext:Button runat="server" ID="btnQDLDownload" Icon="ArrowDown" ToolTip="Tải về">
                                                        <DirectEvents>
                                                            <Click OnEvent="btnQDLDownload_Click" IsUpload="true" />
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button runat="server" ID="btnQDLDelete" Icon="Delete" ToolTip="Xóa">
                                                        <DirectEvents>
                                                            <Click OnEvent="btnQDLDelete_Click" After="#{fufTepTinDinhKem}.reset();">
                                                                <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                                                    ConfirmRequest="true" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:CompositeField>
                                            <ext:TextField runat="server" ID="txtPositionAllowanceNew" FieldLabel="Phụ cấp chức vụ" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9.,]/">
                                            </ext:TextField>
                                            <ext:TextArea runat="server" ID="txtGhiChuMoi" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                                Height="40" MaxLength="500" />
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnCapNhat" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputCanBo();" />
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
                    <ext:Button runat="server" ID="btnCapNhatDongLai" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputCanBo();" />
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
                            <Click Handler="wdTaoQuyetBoNhiemChucVu.hide();ResetForm();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetForm();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" Title="Cập nhật bổ nhiệm chức vụ" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdCapNhatBoNhiemChucVu"
                Modal="true" Constrain="true" Height="480">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandUpdate" />
                    <ext:FieldSet runat="server" ID="FieldSet1" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Container3" Layout="ColumnLayout" Height="50" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" ID="Container4" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="Hidden1" />
                                            <ext:TextField runat="server" ID="txtFullname" FieldLabel="Tên cán bộ<span style='color:red'>*</span>" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtNewDepartment" FieldLabel="Bộ phận" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Container5" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtNewPosition" FieldLabel="Chức vụ" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                            <ext:TextField runat="server" ID="txtNewJobTitle" FieldLabel="Chức danh" AnchorHorizontal="100%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="Container6" Layout="ColumnLayout" Height="470" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Container7" Layout="FormLayout" ColumnWidth="1">
                                <Items>
                                    <ext:FieldSet runat="server" ID="FieldSet2" Layout="FormLayout" Title="Thông tin quyết định Chức vụ gần nhất"
                                        AnchorHorizontal="100%" Height="410">
                                        <Items>
                                            <ext:Container runat="server" ID="Container8" Layout="FormLayout" Height="200">
                                                <Items>
                                                    <ext:Hidden runat="server" ID="hdfChucVuMoi" />
                                                    <ext:ComboBox runat="server" ID="cbxChucVuMoi" FieldLabel="Chức vụ mới <span style='color:red'>*</span>"
                                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%" CtCls="requiredData"
                                                        LabelWidth="130" Editable="true" ItemSelector="div.list-item" EmptyText="Gõ để tìm kiếm"
                                                        ListWidth="250" LoadingText="Đang tải dữ liệu" StoreID="storePosition">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                        </Triggers>
                                                        <Template ID="Template2" runat="server">
                                                            <Html>
                                                                <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                            </Html>
                                                        </Template>
                                                        <ToolTips>
                                                            <ext:ToolTip runat="server" ID="ToolTip2" Title="Hướng dẫn" Html="Nhập chức vụ để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                                                        </ToolTips>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show(); hdfChucVuMoi.setValue(cbxChucVuMoi.getValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfChucVuMoi.reset(); }" />
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Select OnEvent="cbxChucVuMoi_Selected" />
                                                        </DirectEvents>
                                                    </ext:ComboBox>
                                                    <ext:TextField runat="server" ID="txtCapNhatSoQD" FieldLabel="Số quyết định" AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                    <ext:DateField runat="server" ID="dfCapNhatNgayQD" FieldLabel="Ngày quyết định" AnchorHorizontal="100%"
                                                        Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy">
                                                         <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();" />
                                                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:DateField>
                                                    <ext:TextField runat="server" ID="txtCapNhatNguoiQD" FieldLabel="Người quyết định" AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                    <ext:DateField runat="server" ID="dfCapNhatNgayHieuLuc" FieldLabel="Ngày hiệu lực" AnchorHorizontal="100%"
                                                        Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy">
                                                         <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();" />
                                                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:DateField>
                                                     <ext:DateField runat="server" ID="dfUpdateExpireDate" CtCls="requiredData" FieldLabel="Thời hạn bổ nhiệm<span style='color:red'>*</span>"
                                                        Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();" />
                                                            <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:DateField>
                                                    <ext:TextField runat="server" ID="txtUpdateSourceDepartment" FieldLabel="Cơ quan  bổ nhiệm"
                                                        AnchorHorizontal="100%"/>
                                                    <ext:Hidden runat="server" ID="hdfIsUpdateMakerPosition" Text="0" />
                                                    <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                                    <ext:Hidden runat="server" ID="hdfUpdateMakerTempPosition" />
                                                    <ext:Hidden runat="server" ID="hdfUpdateMakerPosition" />
                                                    <ext:ComboBox runat="server" ID="cbxUpdateMakerPosition" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                        EnableKeyEvents="true" StoreID="storePosition">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Template ID="Template3" runat="server">
                                                            <Html>
                                                                <tpl for=".">
						                                            <div class="list-item"> 
							                                            {Name}
						                                            </div>
					                                            </tpl>
                                                            </Html>
                                                        </Template>
                                                        <Listeners>
                                                            <Select Handler="this.triggers[0].show();  hdfUpdateMakerPosition.setValue(cbxUpdateMakerPosition.getValue());
				                                                                    hdfIsUpdateMakerPosition.setValue('1');
				                                                                    hdfUpdateMakerTempPosition.setValue(cbxUpdateMakerPosition.getRawValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsUpdateMakerPosition.reset();hdfMakerTempPosition.reset();hdfUpdateMakerPosition.reset();  }" />
                                                            <KeyUp Fn="searchBoxPositionUpdate" />
                                                            <Blur Handler="cbxUpdateMakerPosition.setRawValue(hdfUpdateMakerTempPosition.getValue());
			                                                                        if (hdfIsUpdateMakerPosition.getValue() != '1') {cbxUpdateMakerPosition.setValue(hdfUpdateMakerTempPosition.getValue());}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:ComboBox runat="server" ID="cbxOldDEpartment" FieldLabel="Bộ phận cũ"
                                                                  DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                                                                  AnchorHorizontal="100%" TabIndex="33" Editable="false">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Template ID="Template37" runat="server">
                                                            <Html>
                                                            <tpl for=".">
                                                                <div class="list-item"> 
                                                                    {Name}
                                                                </div>
                                                            </tpl>
                                                            </Html>
                                                        </Template>
                                                        <Store>
                                                            <ext:Store runat="server" ID="stDepartmentList" AutoLoad="false" OnRefreshData="stDepartmentList_OnRefreshData">
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
                                                            <Expand Handler="if(#{stDepartmentList}.getCount()==0) #{stDepartmentList}.reload();" />
                                                            <Select Handler="#{PagingToolbar2}.pageIndex=0;#{PagingToolbar2}.doLoad();#{Store3}.reload();"></Select>
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                     <ext:ComboBox runat="server" ID="cbxOldPosition" FieldLabel="Chức vụ cũ"
                                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="100%"
                                                        LabelWidth="130" Width="370" Editable="true" ItemSelector="div.list-item" EmptyText="Gõ để tìm kiếm"
                                                        ListWidth="250" LoadingText="Đang tải dữ liệu" StoreID="storePosition">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="SimpleAdd" HideTrigger="false" />
                                                    </Triggers>
                                                    <Template ID="Template1" runat="server">
                                                        <Html>
                                                            <tpl for=".">
						                                        <div class="list-item"> 
							                                        {Name}
						                                        </div>
					                                        </tpl>
                                                        </Html>
                                                    </Template>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" ID="ToolTip3" Title="Hướng dẫn" Html="Nhập chức vụ để tìm kiếm. Nhập <span style='color:red;'>*</span> nếu muốn tìm tất cả." />
                                                    </ToolTips>
                                                    <Listeners>
                                                        <Select Handler="this.triggers[0].show(); hdfOldPositionId.setValue(cbxOldPosition.getValue());" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfOldPositionId.reset(); }" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:TextField runat="server" ID="txtUpdatePositionAllowance" FieldLabel="Phụ cấp chức vụ" AnchorHorizontal="100%" MaxLength="15" MaskRe="/[0-9.,]/">
                                                </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateAndSave" Text="Cập nhật và đóng lại" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button6" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdCapNhatBoNhiemChucVu.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wdShowReport"
                Title="Quyết định bổ nhiệm chức vụ" Maximized="true" Icon="Printer">
                <Items>
                    <ext:Hidden runat="server" ID="hdfTypeReport" />
                    <ext:TabPanel ID="pnReportPanel" Region="Center" AnchorVertical="100%" Border="false"
                        runat="server">
                    </ext:TabPanel>
                    <ext:HtmlEditor Region="Center" runat="server" ID="htmlEditor">
                    </ext:HtmlEditor>
                </Items>
                <Listeners>
                    <BeforeShow Handler="#{pnReportPanel}.remove(0);addHomePage(#{pnReportPanel},'Homepage','DecisionAppointmentEmployee.html?prkey='+#{hdfKeyRecord}.getValue(), '<asp:Literal runat=\'server\' Text=\'<%$ Resources:Desktop, report_staff_profile%>\' />')" />
                    <Hide Handler="hdfQueryReport.reset();" />
                </Listeners>
                <Buttons>
                    <ext:Button ID="Button5" runat="server" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wdShowReport}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="Button1" Text="In ấn" Icon="Printer">
                                <DirectEvents>
                                    <Click OnEvent="InReport_Click" />
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:Window>
        </div>
    </form>
</body>
</html>

