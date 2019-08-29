<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryRaiseManagement.aspx.cs" Inherits="Web.HJM.Modules.ProfileHuman.DecisionSalary.SalaryRaiseManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <script src="../../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        var keyPressSearch = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                PagingToolbar1.doLoad();
                chkSelectionModel.clearSelections();
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
            chkSelectionModel.clearSelections();
        }

        var CheckInputDecision = function () {
            if (txtDecisionNumber.getValue() == "" || txtDecisionNumber.getValue() == null) {
                alert('Bạn chưa nhập số quyết định');
                return false;
            }
            if (hdfQuantumHLId.getValue() == "" || hdfQuantumHLId.getValue() == null) {
                alert('Bạn chưa chọn ngạch lương');
                return false;
            }
            if (gridListEmployee_Store.getCount() == 0) {
                alert('Bạn phải chọn cán bộ nhận quyết định trước');
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfSalaryRaiseType" />
            <ext:Hidden runat="server" ID="hdfRecordIds" />
            <ext:Hidden runat="server" ID="hdfSalaryType" />

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
        <!-- store ngach -->
        <ext:Store ID="cbxQuantumStore" runat="server" AutoLoad="false">
            <Proxy>
                <ext:HttpProxy Method="POST" Url="~/Services/HandlerQuantum.ashx" />
            </Proxy>
            <BaseParams>
                <ext:Parameter Name="objname" Value="cat_Quantum" Mode="Value" />
            </BaseParams>
            <Reader>
                <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="Id">
                    <Fields>
                        <ext:RecordField Name="Id" />
                        <ext:RecordField Name="GroupQuantumId" />
                        <ext:RecordField Name="Name" />
                        <ext:RecordField Name="Code" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridSalaryRaise" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="btnPrint" runat="server" Text="In danh sách" Icon="Printer">
                                                <DirectEvents>
                                                    <Click OnEvent="ShowRiseSalaryReport">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Button ID="btnAddDecision" runat="server" Text="Tạo quyết định" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdDecision.show();gridListEmployee_Store.reload();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:ToolbarFill />
                                            <ext:Label ID="lbFromDate" runat="server" Text="Từ ngày: "></ext:Label>
                                            <ext:DateField runat="server" ID="dfFromDate"
                                                AnchorHorizontal="95%" TabIndex="1" Editable="true" EnableKeyEvents="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                RegexText="Định dạng ngày không đúng">
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:Label ID="lbToDate" runat="server" Text="Đến ngày: "></ext:Label>
                                            <ext:DateField runat="server" ID="dfToDate"
                                                AnchorHorizontal="95%" TabIndex="1" Editable="true" EnableKeyEvents="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                RegexText="Định dạng ngày không đúng">
                                            </ext:DateField>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPressSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); chkSelectionModel.clearSelections();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad(); chkSelectionModel.clearSelections();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="storeSalaryRaise" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="RiseSalary" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="salaryRaiseTimeSheetHandlerType" Value="hdfSalaryRaiseTimeSheetHandlerType.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="fromDate" Value="#{dfFromDate}.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="toDate" Value="#{dfToDate}.getValue()" Mode="Raw" />

                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="QuantumCode" />
                                                    <ext:RecordField Name="QuantumName" />
                                                    <ext:RecordField Name="QuantumGrade" />
                                                    <ext:RecordField Name="DecisionDate" />
                                                    <ext:RecordField Name="RaisingSalaryDate" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="ParentDepartmentName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>

                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column Header="Họ và tên" Width="200" Align="Left" DataIndex="FullName" />
                                        <ext:Column Header="Số hiệu CBCC" Width="150" Align="Left" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Bộ phận" Width="250" Align="Left" DataIndex="DepartmentName" />
                                        <ext:Column Header="Tên ngạch" Width="200" Align="Left" DataIndex="QuantumName" />
                                        <ext:Column Header="Mã ngạch" Width="150" Align="Left" DataIndex="QuantumCode" />
                                        <ext:Column Header="Bậc" Width="150" Align="Left" DataIndex="QuantumGrade" />
                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày bổ nhiệm ngạch" Width="150" DataIndex="DecisionDate">
                                        </ext:DateColumn>
                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày nâng lương" Width="100" DataIndex="RaisingSalaryDate">
                                        </ext:DateColumn>
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel runat="server" ID="chkSelectionModel" />
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
                                            <Change Handler="chkSelectionModel.clearSelections();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wpReport"
                Title="" Maximized="true" Icon="Printer">
                <Items>
                    <ext:TabPanel ID="TabPanel1" Region="Center" AnchorVertical="100%" Border="false"
                        runat="server">
                    </ext:TabPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="Button4" runat="server" Text="Đóng" Icon="Decline">
                        <Listeners>
                            <Click Handler="#{wpReport}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Tạo quyết định lương" Resizable="true"
                Layout="FormLayout" Padding="6" Width="1140" Hidden="true" Icon="UserTick" ID="wdDecision"
                Modal="true" Constrain="true" AutoHeight="true">
                <Items>
                    <ext:Container runat="server" ID="Container26" Layout="ColumnLayout" Height="250"
                        AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="Container27" Layout="FormLayout" ColumnWidth="0.50">
                                <Items>
                                    <ext:FormPanel runat="server" ID="fpTTQD" Frame="true" AnchorHorizontal="99%" Title="Thông tin quyết định"
                                        Icon="BookOpen" Height="250">
                                        <Items>
                                            <ext:Container runat="server" ID="Container8" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="26">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container9" Layout="FormLayout" ColumnWidth="0.5">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtDecisionNumber" CtCls="requiredData" AnchorHorizontal="99%"
                                                                FieldLabel="Số quyết định<span style='color:red'>*</span>" MaxLength="20">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container10" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfDecisionDate" CtCls="requiredData" FieldLabel="Ngày quyết định"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="100%">
                                                            </ext:DateField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:TextField runat="server" ID="txtDecisionName" CtCls="requiredData" FieldLabel="Tên quyết định<span style='color:red'>*</span>"
                                                AnchorHorizontal="100%">
                                            </ext:TextField>
                                            <ext:Container runat="server" ID="Container14" Layout="ColumnLayout" AnchorHorizontal="100%"
                                                Height="27">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container23" Layout="FormLayout" ColumnWidth="0.5">
                                                        <Items>
                                                            <ext:DateField runat="server" ID="dfEffectiveDate" FieldLabel="Ngày hiệu lực"
                                                                Editable="true" MaskRe="/[0-9|/]/" Format="d/M/yyyy" AnchorHorizontal="99%">
                                                            </ext:DateField>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container24" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:TextField runat="server" ID="txtDecisionMaker" FieldLabel="Người QĐ"
                                                                AnchorHorizontal="99%">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container runat="server" Layout="ColumnLayout" Height="27" ColumnWidth="1">
                                                <Items>
                                                    <ext:Hidden runat="server" ID="hdfMakerPositionHL" />
                                                    <ext:ComboBox runat="server" ID="cbxMakerPositionHL" DisplayField="Name" FieldLabel="Chức vụ người ký"
                                                        ValueField="Id" AnchorHorizontal="100%" TabIndex="3" Editable="true" ColumnWidth="1"
                                                        ItemSelector="div.list-item" MinChars="1" EmptyText="Gõ để tìm kiếm" PageSize="10"
                                                        StoreID="storePosition">
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
                                                            <Select Handler="this.triggers[0].show();  hdfSignerPositionHL.setValue(cbxSignerPositionHL.getValue());" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfSignerPositionHL.reset();  }" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:CompositeField ID="cpfAttachHL" runat="server" AnchorHorizontal="100%" FieldLabel="Tệp tin đính kèm">
                                                <Items>
                                                    <ext:FileUploadField ID="fufAttachFile" runat="server" EmptyText="Chọn tệp tin"
                                                        ButtonText="" Icon="Attach" Width="310">
                                                    </ext:FileUploadField>
                                                    <ext:Button runat="server" ID="btnDeleteFile" Icon="Delete" ToolTip="Xóa">
                                                        <Listeners>
                                                            <Click Handler="#{fufAttachFile}.reset();" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:CompositeField>
                                            <ext:TextArea runat="server" ID="txtNote" FieldLabel="Ghi chú" AnchorHorizontal="100%"
                                                Height="50" />
                                        </Items>
                                    </ext:FormPanel>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Container28" Layout="FormLayout" ColumnWidth="0.50">
                                <Items>
                                    <ext:FormPanel runat="server" ID="fpn_Decision" Frame="true" AnchorHorizontal="100%"
                                        Title="Thông tin lương" Icon="Money" Height="250">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfQuantumHLId" />
                                            <ext:Container runat="server" ID="Container29" Layout="ColumnLayout" Height="252"
                                                AnchorHorizontal="100%">
                                                <Items>
                                                    <ext:Container runat="server" ID="Container30" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="110">
                                                        <Items>
                                                            <ext:ComboBox runat="server" ID="cbxQuantum" FieldLabel="Ngạch<span style='color:red'>*</span>"
                                                                DisplayField="Name" StoreID="cbxQuantumStore" ValueField="Id" AnchorHorizontal="98%"
                                                                TabIndex="83" CtCls="requiredData" Editable="false" ItemSelector="div.list-item"
                                                                ListWidth="200" PageSize="10">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Template ID="Template7" runat="server">
                                                                    <Html>
                                                                        <tpl for=".">
						                                                <div class="list-item"> 
							                                                {Name}
						                                                </div>
					                                                </tpl>
                                                                    </Html>
                                                                </Template>
                                                                <Listeners>
                                                                    <Select Handler="this.triggers[0].show(); hdfQuantumHLId.setValue(cbxQuantum.getValue()); cbxSalaryGradeStore.reload(); if(cbxSalaryGrade.getValue() != '') {Ext.net.DirectMethods.GetSalaryInfo();}" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfQuantumHLId.reset(); }" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtSalaryFactor" FieldLabel="Hệ số lương<span style='color:red'>*</span>"
                                                                           AnchorHorizontal="98%" MaskRe="/[0-9.]/" MaxLength="9"
                                                                           CtCls="requiredData" AllowBlank="false"/>
                                                            <ext:TextField runat="server" ID="txtSalaryInsurance" FieldLabel="Lương đóng BH" AnchorHorizontal="98%"
                                                                           MaskRe="/[0-9.]/" MaxLength="15"/>
                                                            <ext:TextField runat="server" ID="txtSalaryLiftGrade" FieldLabel="Bậc lương NB" AnchorHorizontal="98%"
                                                                           MaskRe="/[0-9.]/"/>
                                                            <ext:DateField runat="server" ID="dfProRaiseSalaryDisDate" FieldLabel="Ngày NL kéo dài"
                                                                           AnchorHorizontal="98%" MaskRe="/[0-9|/]/" Format="d/M/yyyy"/>
                                                            <ext:TextField runat="server" ID="txtPositionAllowance" FieldLabel="Phụ cấp chức vụ"
                                                                           AnchorHorizontal="98%" MaskRe="/[0-9.]/" MaxLength="15"/>
                                                            <ext:TextField runat="server" ID="txtOtherAllowance" FieldLabel="Phụ cấp khác" AnchorHorizontal="98%"
                                                                           MaskRe="/[0-9.]/" MaxLength="15"/>
                                                            
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container runat="server" ID="Container31" Layout="FormLayout" ColumnWidth="0.5"
                                                        LabelWidth="107">
                                                        <Items>
                                                            <ext:Hidden runat="server" ID="hdfSalaryGradeHL" />
                                                            <ext:ComboBox runat="server" ID="cbxSalaryGrade" FieldLabel="Bậc lương<span style='color:red'>*</span>"
                                                                DisplayField="TEN" ValueField="MA" AnchorHorizontal="100%" Editable="false" ItemSelector="div.list-item"
                                                                CtCls="requiredData">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                </Triggers>
                                                                <Template ID="Template9" runat="server">
                                                                    <Html>
                                                                        <tpl for=".">
						                                                <div class="list-item"> 
							                                                {TEN}
						                                                </div>
					                                                </tpl>
                                                                    </Html>
                                                                </Template>
                                                                <Store>
                                                                    <ext:Store runat="server" ID="cbxSalaryGradeStore" AutoLoad="false" OnRefreshData="cbxSalaryGradeStore_OnRefreshData">
                                                                        <Reader>
                                                                            <ext:JsonReader IDProperty="MA">
                                                                                <Fields>
                                                                                    <ext:RecordField Name="MA" />
                                                                                    <ext:RecordField Name="TEN" />
                                                                                </Fields>
                                                                            </ext:JsonReader>
                                                                        </Reader>
                                                                    </ext:Store>
                                                                </Store>
                                                                <Listeners>
                                                                    <Expand Handler="if (cbxQuantum.getValue() == ''){ cbxSalaryGradeStore.removeAll(); alert('Bạn phải chọn ngạch trước');}" />
                                                                    <Select Handler="this.triggers[0].show();hdfSalaryGradeHL.setValue(cbxSalaryGrade.getValue()); Ext.net.DirectMethods.GetSalaryInfo();" />
                                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSalaryGradeHL.reset();}" />
                                                                </Listeners>
                                                            </ext:ComboBox>
                                                            <ext:TextField runat="server" ID="txtSalaryLevel" FieldLabel="Mức lương" AnchorHorizontal="100%"
                                                                MaskRe="/[0-9.]/" MaxLength="15"/>
                                                            <ext:DateField runat="server" ID="dfSalaryPayDate" FieldLabel="Ngày hưởng lương"
                                                                AnchorHorizontal="100%" MaskRe="/[0-9|/]/" Format="d/M/yyyy">
                                                            </ext:DateField>
                                                            <ext:DateField runat="server" ID="dfSalaryRaiseNextDate" FieldLabel="Ngày NL tiếp  theo"
                                                                AnchorHorizontal="100%" MaskRe="/[0-9|/]/" Format="d/M/yyyy">
                                                            </ext:DateField>
                                                            <ext:TextField runat="server" ID="txtReason" FieldLabel="Lý do kỷ luật" AnchorHorizontal="99%"
                                                                           MaskRe="/[0-9.]/" MaxLength="15"/>
                                                            <ext:TextField runat="server" ID="txtOutFrame" FieldLabel="Vượt khung"
                                                                AnchorHorizontal="99%" EmptyText="nhập số %" MaskRe="/[0-9.,]/" MaxLength="3">
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FormPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" ID="Container25" Layout="BorderLayout" Height="250">
                        <Items>
                            <ext:GridPanel runat="server" ID="gridListEmployee" TrackMouseOver="true" Title="Danh sách cán bộ nhận quyết định"
                                StripeRows="true" Border="true" Region="Center" Icon="User">
                                <Store>
                                    <ext:Store ID="gridListEmployee_Store" AutoSave="false" AutoLoad="true" runat="server"
                                        ShowWarningOnFailure="false" SkipIdForNewRecords="false" OnRefreshData="ListEmployee_OnRefreshData"
                                        RefreshAfterSaving="None">
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="DepartmentName" />
                                                    <ext:RecordField Name="PositionName" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel ID="ColumnModel3" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Locked="true" Width="35" />
                                        <ext:Column Header="Mã cán bộ" Width="150" DataIndex="EmployeeCode" />
                                        <ext:Column Header="Họ tên" Width="200" DataIndex="FullName" />
                                        <ext:Column Header="Bộ phận" Width="300" DataIndex="DepartmentName"/>
                                        <ext:Column Header="Chức vụ" Width="200" DataIndex="PositionName"/>
                                        
                                    </Columns>
                                </ColumnModel>
                            </ext:GridPanel>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return CheckInputDecision();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnSave_Click">
                                <EventMask ShowMask="true" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdDecision.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="Ext.net.DirectMethods.ResetForm();"></Hide>
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>
