<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.PluralityManagement" Codebehind="PluralityManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="../../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
    <script src="../../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <script src="../JScript.js" type="text/javascript"></script>
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

        var checkInputPlurality = function () {
            if (hdfChonCanBo.getValue() == '' || hdfChonCanBo.getValue().trim == '') {
                alert('Bạn chưa nhập tên cán bộ!');
                return false;
            }
            if (txtDecisionNumber.getValue() == '' || txtDecisionNumber.getValue().trim == '') {
                alert('Bạn chưa nhập số quyết định!');
                return false;
            }
            if (dfDecisionDate.getValue() == '' || dfDecisionDate.getValue().trim == '' || dfDecisionDate.getValue() == null) {
                alert('Bạn chưa nhập ngày quyết định!');
                return false;
            }
            if (dfExpireDate.getValue() == '' || dfExpireDate.getValue().trim == '' || dfExpireDate.getValue() == null) {
                alert('Bạn chưa nhập thời hạn kiêm nhiệm!');
                return false;
            }
            if (dfEffectiveDate.getValue() == '' || dfEffectiveDate.getValue().trim == '' || dfEffectiveDate.getValue() == null) {
                alert('Bạn chưa nhập ngày hiệu lực!');
                return false;
            }
            return true;
        }
        var checkUpdatePlurality = function () {
            if (txtUpdateDecisionNumber.getValue() == '' || txtUpdateDecisionNumber.getValue().trim == '') {
                alert('Bạn chưa nhập số quyết định!');
                return false;
            }
            if (dfUpdateDecisionDate.getValue() == '' || dfUpdateDecisionDate.getValue().trim == '' || dfUpdateDecisionDate.getValue() == null) {
                alert('Bạn chưa nhập ngày quyết định!');
                return false;
            }
            if (dfUpdateExpireDate.getValue() == '' || dfUpdateExpireDate.getValue().trim == '' || dfUpdateExpireDate.getValue() == null) {
                alert('Bạn chưa nhập thời hạn kiêm nhiệm!');
                return false;
            }
            if (dfUpdateEffectiveDate.getValue() == '' || dfUpdateEffectiveDate.getValue().trim == '' || dfUpdateEffectiveDate.getValue() == null) {
                alert('Bạn chưa nhập ngày hiệu lực!');
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
        var searchBoxDestDepartment = function (f, e) {
            hdfDestDepartmentTemp.setValue(cbxDestDepartment.getRawValue());
            if (hdfIsDestDepartment.getValue() == '1') {
                hdfIsDestDepartment.setValue('2');
            }
            if (cbxDestDepartment.getRawValue() == '') {
                hdfIsDestDepartment.reset();
            }
        }
        var searchBoxDestDepartmentUpdate = function (f, e) {
            hdfUpdateDestDepartmentTemp.setValue(cbxUpdateDestDepartment.getRawValue());
            if (hdfIsUpdateDestDepartment.getValue() == '1') {
                hdfIsUpdateDestDepartment.setValue('2');
            }
            if (cbxUpdateDestDepartment.getRawValue() == '') {
                hdfIsUpdateDestDepartment.reset();
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
            <ext:Hidden runat="server" ID="hdfTotalRecord" />
            <ext:Hidden runat="server" ID="hdfPositionId" />
            <ext:Hidden runat="server" ID="hdfOldPositionId" />
            <ext:Hidden runat="server" ID="hdfKeyRecord" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
            <ext:Hidden runat="server" ID="hdfBusinessType" />
            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <!-- store chức vụ -->
             <ext:Store ID="storePosition" runat="server" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Category" />
                    <ext:Parameter Name="table" Value="cat_Position" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader Root="plants" TotalProperty="total" IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
             <ext:Store runat="server" ID="storeDepartment" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
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
                            <ext:GridPanel ID="gridPlurality" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdPlurality.show();ResetFormBusiness();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">

                                                <DirectEvents>
                                                    <Click OnEvent="EditMoveToClick">
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
                                    <ext:Store runat="server" ID="storePlurality">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={25}" />

                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="BusinessHistory" />
                                            <ext:Parameter Name="businessTimeSheetHandlerType" Value="hdfBusinessTimeSheetHandlerType.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="plants" TotalProperty="total">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="CurrentPosition" />
                                                    <ext:RecordField Name="OldDepartment" />
                                                    <ext:RecordField Name="DecisionNumber" />
                                                    <ext:RecordField Name="DecisionDate" />
                                                    <ext:RecordField Name="EffectiveDate" />
                                                    <ext:RecordField Name="ExpireDate" />
                                                    <ext:RecordField Name="SourceDepartment" />
                                                    <ext:RecordField Name="CurrentDepartment" />
                                                    <ext:RecordField Name="DestinationDepartment" />
                                                    <ext:RecordField Name="DecisionMaker" />
                                                    <ext:RecordField Name="DecisionPosition" />
                                                    <ext:RecordField Name="ShortDecision" />
                                                    <ext:RecordField Name="NewPosition" />
                                                    <ext:RecordField Name="FileScan" />
                                                    <ext:RecordField Name="Description" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column Header="Số quyết định" Width="150" DataIndex="DecisionNumber" />
                                        <ext:DateColumn Header="Ngày quyết định" Width="145" Align="Left" DataIndex="DecisionDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Trích yếu quyết định" Width="150" Align="Left" Locked="true" DataIndex="ShortDecision" />
                                        <ext:Column Header="Họ và tên" Width="220" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column Header="Số hiệu CBCC" Width="150" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Chức vụ hiện tại" Width="140" Align="Left" DataIndex="CurrentPosition" />
                                        <ext:Column Header="Đơn vị đang công tác" Width="160" DataIndex="CurrentDepartment" />
                                        <ext:Column Header="Cơ quan ra quyết định" Width="170" DataIndex="SourceDepartment" />
                                        <ext:Column Header="Đơn vị kiêm nhiệm" Width="170" DataIndex="DestinationDepartment" />
                                        <ext:Column Header="Chức vụ kiêm nhiệm" Width="140" Align="Left" DataIndex="NewPosition" />
                                        <ext:DateColumn Header="Thời hạn kiêm nhiệm" Width="145" Align="Left" DataIndex="ExpireDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn Header="Ngày hiệu lực" Width="145" Align="Left" DataIndex="EffectiveDate" Format="dd/MM/yyyy" />
                                        <ext:Column Header="Người ký" Width="140" DataIndex="DecisionMaker"/>
                                        <ext:Column Header="Chức vụ người ký" Width="140" Align="Left" DataIndex="DecisionPosition" />
                                        <ext:Column Header="File Scan" Width="180" DataIndex="FileScan"/>
                                        <ext:Column Header="Ghi chú" Width="180" DataIndex="Description"/>
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
            <ext:Window runat="server" Title="Tạo quyết định kiêm nhiệm" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdPlurality"
                Modal="true" Constrain="true" Height="500">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandName" />
                    <ext:FieldSet runat="server" ID="FieldSet9" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Ctn11" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" ID="Ctn12" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfChonCanBo" />
                                            <ext:ComboBox ID="cbxChonCanBo" CtCls="requiredData" ValueField="Id" DisplayField="FullName"
                                                FieldLabel="Tên cán bộ<span style='color:red'>*</span>" PageSize="10" HideTrigger="true"
                                                ItemSelector="div.search-item" MinChars="1" EmptyText="Nhập tên để tìm kiếm"
                                                LoadingText="Đang tải dữ liệu..." AnchorHorizontal="98%" runat="server">
                                                 <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
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
                                                    <Select Handler="hdfChonCanBo.setValue(cbxChonCanBo.getValue());txtDecisionNumber.enable();dfDecisionDate.enable();dfEffectiveDate.enable();dfExpireDate.enable();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfChonCanBo.reset(); }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Ctn13" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtCurrentPosition" FieldLabel="Chức vụ hiện tại" AnchorHorizontal="100%"/>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="Ctn15" Layout="ColumnLayout" Height="470" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Ctn17" Layout="FormLayout" ColumnWidth="0.66" Hidden="false" Height="470">
                                <Items>
                                    <ext:FieldSet runat="server" ID="FieldSet5" Title="Thông tin quyết định" Layout="FormLayout"
                                        AnchorHorizontal="100%" LabelWidth="110" Height="450">
                                        <Items>
                                            <ext:Container runat="server" ID="Container18" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="26">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container19" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="150">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtDecisionNumber" CtCls="requiredData" Disabled="true" AnchorHorizontal="100%"
                                                                FieldLabel="Số quyết định<span style='color:red'>*</span>" MaxLength="20">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container20" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfDecisionDate" CtCls="requiredData" FieldLabel="Ngày quyết định<span style='color:red'>*</span>" Disabled="true"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                                            </ext:DateField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" ID="Container1" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="300">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container2" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="150">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtShortDecision" FieldLabel="Trích yếu quyết định"
                                                                AnchorHorizontal="100%"/>
                                                            <ext:DateField runat="server" ID="dfExpireDate" CtCls="requiredData" FieldLabel="Thời hạn kiêm nhiệm<span style='color:red'>*</span>" Disabled="true"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="50%">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                            <ext:DateField runat="server" ID="dfEffectiveDate" CtCls="requiredData" FieldLabel="Ngày hiệu lực<span style='color:red'>*</span>" Disabled="true"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="50%">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                             <ext:TextField runat="server" ID="txtCurrentDepartment" FieldLabel="Đơn vị đang công tác" 
                                                                AnchorHorizontal="100%"/>
                                                            <ext:TextField runat="server" ID="txtSourceDepartment" FieldLabel="Cơ quan ra quyết định" AnchorHorizontal="100%"/>
                                                            <ext:Hidden runat="server" ID="hdfIsDestDepartment" Text="0" />
                                                            <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                                            <ext:Hidden runat="server" ID="hdfDestDepartment" />
                                                            <ext:Hidden runat="server" ID="hdfDestDepartmentTemp" />
                                                            <ext:ComboBox runat="server" ID="cbxDestDepartment" DisplayField="Name" FieldLabel="Đơn vị kiêm nhiệm"
                                                                ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                                                                ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                                EnableKeyEvents="true" StoreID="storeDepartment">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
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
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();  hdfDestDepartment.setValue(cbxDestDepartment.getValue());
				                                                                            hdfIsDestDepartment.setValue('1');
				                                                                            hdfDestDepartmentTemp.setValue(cbxDestDepartment.getRawValue());" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsDestDepartment.reset();hdfDestDepartmentTemp.reset();hdfDestDepartment.reset();  }" />
                                                                    <KeyUp Fn="searchBoxDestDepartment" />
                                                                    <Blur Handler="cbxDestDepartment.setRawValue(hdfDestDepartmentTemp.getValue());
			                                                                                if (hdfIsDestDepartment.getValue() != '1') {cbxDestDepartment.setValue(hdfDestDepartmentTemp.getValue());}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtNewPosition" FieldLabel="Chức vụ kiêm nhiệm"
                                                                AnchorHorizontal="100%"/>
                                                            <ext:TextField runat="server" ID="txtDecisionMaker" FieldLabel="Người ký"
                                                                AnchorHorizontal="100%"/>
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
                                                            <ext:Hidden runat="server" ID="hdfTepTinDinhKem" />
                                                            <ext:CompositeField ID="composifieldAttach" runat="server" AnchorHorizontal="100%"
                                                                FieldLabel="File Scan">
                                                                <Items>
                                                                    <ext:FileUploadField ID="fufTepTinDinhKem" runat="server" EmptyText="Chọn tệp tin"
                                                                        ButtonText="" Icon="Attach" Width="367" AnchorHorizontal="100%">
                                                                    </ext:FileUploadField>
                                                                    <ext:Button runat="server" ID="btnQDLDownload" Icon="ArrowDown" ToolTip="Tải về">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnMoveToDownload_Click" IsUpload="true" />
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                    <ext:Button runat="server" ID="btnQDLDelete" Icon="Delete" ToolTip="Xóa">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnMoveToDelete_Click" After="#{fufTepTinDinhKem}.reset();">
                                                                                <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                                                                    ConfirmRequest="true" />
                                                                            </Click>
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:CompositeField>
                                                            <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                                                Height="40" MaxLength="500" />
                                                        </Items>
                                                    </ext:Container>
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
                    <ext:Button runat="server" ID="btnCapNhat" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputPlurality();" />
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
                            <Click Handler="return checkInputPlurality();" />
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
                            <Click Handler="wdPlurality.hide();ResetFormBusiness(); txtDescription.reset();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetFormBusiness();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" Title="Cập nhật kiêm nhiệm" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdUpdatePlurality"
                Modal="true" Constrain="true" Height="500">
                <Items>
                    <ext:Hidden runat="server" ID="hdfCommandUpdate" />
                    <ext:FieldSet runat="server" ID="FieldSet1" Layout="FormLayout" AnchorHorizontal="100%"
                        Title="Thông tin cán bộ">
                        <Items>
                            <ext:Container runat="server" ID="Container3" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" ID="Container4" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtUpdateFullname" FieldLabel="Tên cán bộ<span style='color:red'>*</span>" AnchorHorizontal="98%"
                                                Disabled="true" DisabledClass="disabled-input-style">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" ID="Container5" Layout="FormLayout" ColumnWidth="0.5">
                                        <Items>
                                            <ext:TextField runat="server" ID="txtUpdateCurrentPosition" FieldLabel="Chức vụ hiện tại" AnchorHorizontal="100%"/>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server" ID="Container6" Layout="ColumnLayout" Height="470" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Container7" Layout="FormLayout" ColumnWidth="0.66" Hidden="false" Height="470">
                                <Items>
                                    <ext:FieldSet runat="server" ID="FieldSet2" Title="Thông tin quyết định" Layout="FormLayout"
                                        AnchorHorizontal="100%" LabelWidth="110" Height="450">
                                        <Items>
                                            <ext:Container runat="server" ID="Container8" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="26">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container9" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="150">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtUpdateDecisionNumber" CtCls="requiredData" Disabled="true" AnchorHorizontal="100%"
                                                                FieldLabel="Số quyết định<span style='color:red'>*</span>" MaxLength="20">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container10" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfUpdateDecisionDate" CtCls="requiredData" FieldLabel="Ngày quyết định<span style='color:red'>*</span>" Disabled="true"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="100%">
                                                            </ext:DateField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" ID="Container11" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="280">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container12" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="150">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtUpdateShortDecision" FieldLabel="Trích yếu quyết định"
                                                                AnchorHorizontal="100%"/>
                                                             <ext:DateField runat="server" ID="dfUpdateExpireDate" CtCls="requiredData" FieldLabel="Thời hạn kiêm nhiệm<span style='color:red'>*</span>" Disabled="true"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="50%">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                            <ext:DateField runat="server" ID="dfUpdateEffectiveDate" CtCls="requiredData" FieldLabel="Ngày hiệu lực<span style='color:red'>*</span>" Disabled="true"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="dd/MM/yyyy" AnchorHorizontal="50%">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); }" />
                                                                </Listeners>
                                                            </ext:DateField>
                                                             <ext:TextField runat="server" ID="txtUpdateCurrentDepartment" FieldLabel="Đơn vị đang công tác" 
                                                                AnchorHorizontal="100%"/>
                                                            <ext:TextField runat="server" ID="txtUpdateSourceDepartment" FieldLabel="Cơ quan ra quyết định" AnchorHorizontal="100%"/>
                                                            <ext:Hidden runat="server" ID="hdfIsUpdateDestDepartment" Text="0" />
                                                            <%-- 0: user enter text, 1: user select item, 2: user select and edit --%>
                                                            <ext:Hidden runat="server" ID="hdfUpdateDestDepartment" />
                                                            <ext:Hidden runat="server" ID="hdfUpdateDestDepartmentTemp" />
                                                            <ext:ComboBox runat="server" ID="cbxUpdateDestDepartment" DisplayField="Name" FieldLabel="Đơn vị kiêm nhiệm"
                                                                ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true"
                                                                ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" LoadingText="Đang tải dữ liệu..."
                                                                EnableKeyEvents="true" StoreID="storeDepartment">
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
                                                                    <Select Handler="this.triggers[0].show();  hdfUpdateDestDepartment.setValue(cbxUpdateDestDepartment.getValue());
				                                                                            hdfIsUpdateDestDepartment.setValue('1');
				                                                                            hdfUpdateDestDepartmentTemp.setValue(cbxUpdateDestDepartment.getRawValue());" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfIsUpdateDestDepartment.reset();hdfUpdateDestDepartmentTemp.reset();hdfUpdateDestDepartment.reset();  }" />
                                                                    <KeyUp Fn="searchBoxDestDepartmentUpdate" />
                                                                    <Blur Handler="cbxUpdateDestDepartment.setRawValue(hdfUpdateDestDepartmentTemp.getValue());
			                                                                                if (hdfIsUpdateDestDepartment.getValue() != '1') {cbxUpdateDestDepartment.setValue(hdfUpdateDestDepartmentTemp.getValue());}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtUpdateNewPosition" FieldLabel="Chức vụ kiêm nhiệm"
                                                                AnchorHorizontal="100%"/>
                                                            <ext:TextField runat="server" ID="txtUpdateDecisionMaker" FieldLabel="Người ký"
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
                                                                <Template ID="Template1" runat="server">
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
                                                            <ext:CompositeField ID="CompositeField1" runat="server" AnchorHorizontal="100%"
                                                                FieldLabel="File Scan">
                                                                <Items>
                                                                    <ext:FileUploadField ID="uploadFileScan" runat="server" EmptyText="Chọn tệp tin"
                                                                        ButtonText="" Icon="Attach" Width="367" AnchorHorizontal="100%">
                                                                    </ext:FileUploadField>
                                                                    <ext:Button runat="server" ID="Button1" Icon="ArrowDown" ToolTip="Tải về">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnMoveToDownload_Click" IsUpload="true" />
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                    <ext:Button runat="server" ID="Button2" Icon="Delete" ToolTip="Xóa">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="btnMoveToDelete_Click" After="#{uploadFileScan}.reset();">
                                                                                <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa tệp tin đính kèm?"
                                                                                    ConfirmRequest="true" />
                                                                            </Click>
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:CompositeField>
                                                            <ext:TextArea runat="server" ID="txtUpdateDescription" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                                                Height="40" MaxLength="500" />
                                                        </Items>
                                                    </ext:Container>
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
                         <Listeners>
                            <Click Handler="return checkUpdatePlurality();" />
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
                    <ext:Button runat="server" ID="Button6" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdUpdatePlurality.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

