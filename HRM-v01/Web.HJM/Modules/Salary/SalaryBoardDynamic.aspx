<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Salary.SalaryBoardDynamic" Codebehind="SalaryBoardDynamic.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Resource/js/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="/Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <style type="text/css">
        #gridSalaryInfo .x-grid3-hd-inner 
        {
            white-space:normal !important;
        }
    </style>

    <script type="text/javascript">
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

        function checkInputSalaryBoard() {
            if (hdfDepartmentId.getValue() == '' || hdfDepartmentId.getValue() == null) {
                alert("Bạn chưa chọn bộ phận!");
                return false;
            }
            if (txtTitleSalaryBoard.getValue() == '' || txtTitleSalaryBoard.getValue() == null) {
                alert("Bạn chưa nhập tên bảng tính lương!");
                return false;
            }
            return true;
        }
        var ResetAjustmentWindow = function () {
            cbxSelectColumn.reset();
            hdfSelectColumnId.reset();
            txtValueAdjustment.reset();
            chkApplySelectedEmployee.reset();
            chkApplyForAll.reset();
        }

        var ValidateAjustment = function () {
            if (hdfSelectColumnId.getValue() == null || hdfSelectColumnId.getValue() == '') {
                alert('Bạn chưa chọn cột áp dụng');
                cbxSelectColumn.focus();
                return false;
            }
            if (!txtValueAdjustment.getValue()) {
                alert('Bạn chưa nhập giá trị điều chỉnh');
                txtValueAdjustment.focus();
                return false;
            }
            if (chkApplySelectedEmployee.getValue() == false && chkApplyForAll.getValue() == false) {
                alert('Bạn chưa chọn hình thức áp dụng');
                return false;
            }
            if (chkApplySelectedEmployee.getValue() == true && chkApplyForAll.getValue() == true) {
                alert('Bạn chỉ đươc chọn một hình thức áp dụng');
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfSalaryBoardId" />
            <ext:Hidden runat="server" ID="hdfType"/>

            <!-- Store lấy đơn vị theo người dùng đăng nhập -->
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

            <ext:Viewport ID="vp" runat="server" Layout="Center">
                <Items>
                    <ext:BorderLayout Border="false" Header="false" runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridSalaryInfo" TrackMouseOver="true" runat="server" 
                                StripeRows="true" Border="false" AnchorHorizontal="100%" Title="Vui lòng chọn bảng lương" Icon="Date">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button ID="Button3" runat="server" Text="Quản lý bảng lương" Icon="Table">
                                                <Listeners>
                                                    <Click Handler="wdSalaryBoardManage.show();" />
                                                </Listeners>
                                            </ext:Button>

                                            <ext:ToolbarSpacer ID="ToolbarSpacer4" runat="server" Width="10" />
                                            <ext:Button runat="server" ID="btnAdjustment" Text="Điều chỉnh" Icon="Pencil" Disabled="true">
                                                <Listeners>
                                                    <Click Handler="wdAdjustment.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer5" runat="server" Width="10" />
                                            <ext:Button ID="btnExtension" runat="server" Text="Tính lương" Icon="Build" Disabled="True">
                                                <Menu>
                                                    <ext:Menu ID="menuExtension" runat="server">
                                                        <Items>
                                                            <ext:MenuItem ID="itemExtension" runat="server" Text="Bước 1: Lấy dữ liệu chấm công">
                                                                <Menu>
                                                                    <ext:Menu runat="server">
                                                                        <Items>
                                                                            <ext:MenuItem runat="server" Text="Lấy cho tất cả nhân viên">
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="GetMenuAllEmployee">
                                                                                        <EventMask ShowMask="true" />
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:MenuItem>
                                                                            <ext:MenuItem runat="server" Text="Lấy cho những nhân viên được chọn">
                                                                                <Listeners>
                                                                                    <Click Handler="return CheckSelectedRow(gridSalaryInfo);" />
                                                                                </Listeners>
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="GetDataForSelectedEmployee_Click">
                                                                                        <EventMask ShowMask="true" />
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:MenuItem>
                                                                        </Items>
                                                                    </ext:Menu>
                                                                </Menu>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem ID="itemCalculateEmpSelected" runat="server" Text="Bước 2: Tính toán lương">
                                                                <Menu>
                                                                    <ext:Menu runat="server">
                                                                        <Items>
                                                                            <ext:MenuItem runat="server" Text="Tính cho tất cả nhân viên">
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="CaculateAllEmployee">
                                                                                        <EventMask ShowMask="true" />
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:MenuItem>
                                                                            <ext:MenuItem runat="server" Text="Tính cho những nhân viên được chọn">
                                                                                <Listeners>
                                                                                    <Click Handler="return CheckSelectedRow(gridSalaryInfo);" />
                                                                                </Listeners>
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="CaculateSelectedEmployee_Click">
                                                                                        <EventMask ShowMask="true" />
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:MenuItem>
                                                                        </Items>
                                                                    </ext:Menu>
                                                                </Menu>
                                                            </ext:MenuItem>

                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:ToolbarSpacer ID="ToolbarSpacer3" runat="server" Width="10" />

                                            <ext:ToolbarFill runat="server" ID="tbf" />
                                            <ext:TriggerField runat="server" ID="txtSearch" EnableKeyEvents="true" Width="220" EmptyText="Nhập mã, họ tên CCVC">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="keyPresstxtSearch" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
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
                                    <ext:Store runat="server" ID="storeTimeSheet">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/HandlerSalaryBoardDyamic.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="searchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="year" Value="hdfYear.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="month" Value="hdfMonth.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="salaryBoardId" Value="hdfSalaryBoardId.getValue()" Mode="Raw"/>
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="PositionName" />
                                                    <ext:RecordField Name="PercentagePaySalary"/>
                                                    <ext:RecordField Name="SalaryNet"/>
                                                    <ext:RecordField Name="SalaryContract"/>
                                                    <ext:RecordField Name="SalaryBasic"/> 
                                                    <ext:RecordField Name="ResponsibilityAllowance"/>
                                                    <ext:RecordField Name="OtherAllowance"/>
                                                    <ext:RecordField Name="SalaryLevel"/>
                                                    <ext:RecordField Name="SalaryProbationLevel"/>
                                                    <ext:RecordField Name="WorkStandard"/>
                                                    <ext:RecordField Name="WorkPaySalary"/>
                                                    <ext:RecordField Name="WorkProbation"/>
                                                    <ext:RecordField Name="WorkDay"/> 
                                                    <ext:RecordField Name="TaxArrears"/>
                                                    <ext:RecordField Name="TaxOffset"/>
                                                    <ext:RecordField Name="SalaryWorkDay"/>
                                                    <ext:RecordField Name="SalaryWorkProbation"/>
                                                    <ext:RecordField Name="TotalSalaryWorkDay"/>
                                                    <ext:RecordField Name="TotalSalaryWorkProbation"/>
                                                    <ext:RecordField Name="WorkOverTime"/>
                                                    <ext:RecordField Name="MoneyOverTime"/>
                                                    <ext:RecordField Name="TotalIncome"/>
                                                    <ext:RecordField Name="TotalInsurance"/>
                                                    <ext:RecordField Name="TotalDepartmentInsurance"/>
                                                    <ext:RecordField Name="TotalAfterSocialInsurance"/>
                                                    <ext:RecordField Name="DependenceNumber"/>
                                                    <ext:RecordField Name="ReduceFamily"/>
                                                    <ext:RecordField Name="ReducePersonal"/>
                                                    <ext:RecordField Name="TotalIncomeTax"/>
                                                    <ext:RecordField Name="PersonalIncomeTax"/>
                                                    <ext:RecordField Name="ProbationIncomeTax"/>
                                                    <ext:RecordField Name="CooperationIncomeTax"/>
                                                    <ext:RecordField Name="TotalPersonalIncomeTax"/>
                                                    <ext:RecordField Name="OtherRevenue"/>
                                                    <ext:RecordField Name="CTPDay"/>
                                                    <ext:RecordField Name="Expense"/>
                                                    <ext:RecordField Name="SalaryAddition"/>
                                                    <ext:RecordField Name="SalaryAdvance"/>
                                                    <ext:RecordField Name="NonTaxOffset"/>
                                                    <ext:RecordField Name="NonTaxArrears"/>
                                                    <ext:RecordField Name="ActualTransfer"/>
                                                    <ext:RecordField Name="Note"/>
                                                    <ext:RecordField Name="ATMNumber"/>
                                                    <ext:RecordField Name="PersonalSocialInsurance"/>
                                                    <ext:RecordField Name="PersonalHealthInsurance"/>
                                                    <ext:RecordField Name="PersonalVoluntaryInsurance"/> 
                                                    <ext:RecordField Name="DepartmentSocialInsurance"/>
                                                    <ext:RecordField Name="DepartmentHealthInsurance"/>
                                                    <ext:RecordField Name="DepartmentVoluntaryInsurance"/>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn ColumnID="STT" Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="200" Align="Left" Locked="true" DataIndex="FullName" />
                                        <ext:Column ColumnID="EmployeeCode" Header="Số hiệu CBCC" Width="100" Align="Left" Locked="true" DataIndex="EmployeeCode" />
                                        <ext:Column ColumnID="PositionName" Header="Chức vụ" Width="100" Align="Left" Locked="true" DataIndex="PositionName" />
                                        <ext:Column ColumnID="PercentagePaySalary" Header="%Hưởng lương" Width="100" Align="Left" Locked="true" DataIndex="PercentagePaySalary" />
                                        <ext:Column ColumnID="SalaryNet" Header="Lương Net" Width="100" Align="Left" Locked="true" DataIndex="SalaryNet" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryContract" Header="Lương theo HĐ" Width="100" Align="Left" Locked="true" DataIndex="SalaryContract" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryBasic" Header="Lương cơ bản" Width="100" Align="Left" Locked="true" DataIndex="SalaryBasic" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="ResponsibilityAllowance" Header="Phụ cấp trách nhiệm" Width="100" Align="Left" Locked="true" DataIndex="ResponsibilityAllowance" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="OtherAllowance" Header="Các khoản phụ cấp khác" Width="100" Align="Left" Locked="true" DataIndex="OtherAllowance">
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryLevel" Header="Mức lương" Width="100" Align="Left" Locked="true" DataIndex="SalaryLevel" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryProbationLevel" Header="Mức lương thử việc" Width="100" Align="Left" Locked="true" DataIndex="SalaryProbationLevel" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="WorkStandard" Header="Công chuẩn" Width="100" Align="Left" Locked="true" DataIndex="WorkStandard" />
                                        <ext:Column ColumnID="WorkPaySalary" Header="Công hưởng lương" Width="100" Align="Left" Locked="true" DataIndex="WorkPaySalary" />  
                                        <ext:Column ColumnID="WorkProbation" Header="Công thử việc" Width="100" Align="Left" Locked="true" DataIndex="WorkProbation" />
                                        <ext:Column ColumnID="WorkDay" Header="Ngày công(thử việc+chính thức)" Width="100" Align="Left" Locked="true" DataIndex="WorkDay"/>
                                        <ext:Column ColumnID="TaxArrears" Header="Truy thu chịu thuế" Width="100" Align="Left" Locked="true" DataIndex="TaxArrears" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TaxOffset" Header="Truy lĩnh chịu thuế" Width="100" Align="Left" Locked="true" DataIndex="TaxOffset" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryWorkDay" Header="Lương theo ngày công" Width="100" Align="Left" Locked="true" DataIndex="SalaryWorkDay" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryWorkProbation" Header="Lương theo ngày công thử việc" Width="100" Align="Left" Locked="true" DataIndex="SalaryWorkProbation" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalSalaryWorkDay" Header="Tổng lương theo ngày công" Width="100" Align="Left" Locked="true" DataIndex="TotalSalaryWorkDay" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="WorkOverTime" Header="Ngày công thêm giờ" Width="100" Align="Left" Locked="true" DataIndex="WorkOverTime" />
                                        <ext:Column ColumnID="MoneyOverTime" Header="Thành tiền thêm giờ" Width="100" Align="Left" Locked="true" DataIndex="MoneyOverTime" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalIncome" Header="Tổng thu nhập" Width="100" Align="Left" Locked="true" DataIndex="TotalIncome" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalInsurance" Header="Tổng 10.5% BH" Width="100" Align="Left" Locked="true" DataIndex="TotalInsurance" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalDepartmentInsurance" Header="Tổng 21.5% BH đơn vị đóng" Width="100" Align="Left" Locked="true" DataIndex="TotalDepartmentInsurance" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalAfterSocialInsurance" Header="Thu nhập sau BHXH" Width="100" Align="Left" Locked="true" DataIndex="TotalAfterSocialInsurance" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="DependenceNumber" Header="Số người phụ thuộc" Width="100" Align="Left" Locked="true" DataIndex="DependenceNumber" />
                                        <ext:Column ColumnID="ReduceFamily" Header="Tổng giảm trừ gia cảnh" Width="100" Align="Left" Locked="true" DataIndex="ReduceFamily" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="ReducePersonal" Header="Giảm trừ bản thân" Width="100" Align="Left" Locked="true" DataIndex="ReducePersonal" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalIncomeTax" Header="Tổng thu nhập chịu thuế" Width="100" Align="Left" Locked="true" DataIndex="TotalIncomeTax" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="PersonalIncomeTax" Header="Thuế TNCN (Thuế suất)" Width="100" Align="Left" Locked="true" DataIndex="PersonalIncomeTax" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="ProbationIncomeTax" Header="Thuế TNCN (10% thử việc)" Width="100" Align="Left" Locked="true" DataIndex="ProbationIncomeTax" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="CooperationIncomeTax" Header="Thuế TNCN (10% cộng tác)" Width="100" Align="Left" Locked="true" DataIndex="CooperationIncomeTax" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="TotalPersonalIncomeTax" Header="Thuế TNCN (Tổng thuế)" Width="100" Align="Left" Locked="true" DataIndex="TotalPersonalIncomeTax" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="OtherRevenue" Header="Thu khác" Width="100" Align="Left" Locked="true" DataIndex="OtherRevenue" />
                                        <ext:Column ColumnID="CTPDay" Header="Số ngày CTP" Width="100" Align="Left" Locked="true" DataIndex="CTPDay" />
                                        <ext:Column ColumnID="Expense" Header="Công tác phí" Width="100" Align="Left" Locked="true" DataIndex="Expense" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryAddition" Header="Bổ sung lương" Width="100" Align="Left" Locked="true" DataIndex="SalaryAddition" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="SalaryAdvance" Header="Tạm ứng lương" Width="100" Align="Left" Locked="true" DataIndex="SalaryAdvance" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="NonTaxOffset" Header="Truy lĩnh không chịu thuế" Width="100" Align="Left" Locked="true" DataIndex="NonTaxOffset" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="NonTaxArrears" Header="Truy thu không chịu thuế" Width="100" Align="Left" Locked="true" DataIndex="NonTaxArrears" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="ActualTransfer" Header="Thực lãnh chuyển khoản" Width="100" Align="Left" Locked="true" DataIndex="ActualTransfer" >
                                            <Renderer Fn="RenderVND" />
                                        </ext:Column>
                                        <ext:Column ColumnID="Note" Header="Ghi chú" Width="100" Align="Left" Locked="true" DataIndex="Note" />
                                        <ext:Column ColumnID="ATMNumber" Header="Số tài khoản ATM" Width="150" Align="Left" Locked="true" DataIndex="ATMNumber" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="" />
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
                                            <Change Handler="" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdSalaryBoardManage" Constrain="true"
                Title="Quản lý bảng tính lương" Icon="Table" Layout="FormLayout" Width="660" AutoHeight="true">
                <Items>
                    <ext:Hidden ID="hdfSalaryBoardListID" runat="server" />
                    <ext:GridPanel ID="grpSalaryBoardList" runat="server" StripeRows="true" Border="false"
                        AnchorHorizontal="100%" Header="false" Height="350" Title="Danh sách bảng tính lương"
                        AutoExpandColumn="Title">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="tbgr">
                                <Items>
                                    <ext:Button ID="btnAddSalaryBoardList" Icon="Add" runat="server" Text="Thêm">
                                        <Listeners>
                                            <Click Handler="wdCreateSalaryBoardList.show();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" Disabled="true" ID="btnEditSalaryBoardList" Text="Sửa"
                                        Icon="Pencil">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.btnEditSalaryBoardList_Click();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" Text="Xóa" Icon="Delete"
                                        Disabled="true" ID="btnDeleteSalaryBoardList">
                                        <Listeners>
                                            <Click Handler="if (CheckSelectedRows(grpSalaryBoardList) == false) {return false;}" />
                                        </Listeners>
                                        <DirectEvents>
                                            <Click OnEvent="btnDeleteSalaryBoardList_Click">
                                                <Confirmation Title="<%$ Resources:CommonMessage, Warning%>" Message="Bạn có chắc chắn muốn xóa không ?"
                                                    ConfirmRequest="true" />
                                                <EventMask ShowMask="true" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store ID="storeSalaryBoardList" AutoSave="true" runat="server" GroupField="Year">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={15}" />
                                </AutoLoadParams>
                                <BaseParams>
                                    <ext:Parameter Name="handlers" Value="SalaryBoardList" />
                                    <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw"/>
                                </BaseParams>
                                <Reader>
                                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Title" />
                                            <ext:RecordField Name="Description" />
                                            <ext:RecordField Name="Month" />
                                            <ext:RecordField Name="Year" />
                                            <ext:RecordField Name="CreatedDate" />
                                            <ext:RecordField Name="CreatedBy" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <View>
                            <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="false" MarkDirty="false"
                                              ShowGroupName="false" EnableNoGroups="true" />
                        </View>
                        <ColumnModel ID="ColumnModel3" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn Header="STT" Width="30" />
                                <ext:Column ColumnID="Title" Header="Tên bảng tính lương" Width="160" DataIndex="Title">
                                </ext:Column>
                                <ext:Column ColumnID="Month" Align="Center" Header="Tháng"
                                            Width="120" DataIndex="Month"/>
                                <ext:Column ColumnID="Year" Align="Center" Header="Năm"
                                            Width="120" DataIndex="Year" Hidden="True" />
                                <ext:Column ColumnID="CreatedBy" Align="Center" Header="Người tạo" Width="120" DataIndex="CreatedBy"/>
                                <ext:DateColumn Format="dd/MM/yyyy" ColumnID="CreatedDate" Header="Ngày tạo"
                                    Width="80" DataIndex="CreatedDate" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModelSalaryBoardList" runat="server">
                                <Listeners>
                                    <RowSelect Handler="btnEditSalaryBoardList.enable();hdfSalaryBoardListID.setValue(RowSelectionModelSalaryBoardList.getSelected().get('Id')); btnDeleteSalaryBoardList.enable(); btnExtension.enable(); btnAdjustment.enable();" />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <LoadMask ShowMask="true" Msg="Đang tải" />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="10">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" Text="<%$ Resources:HOSO, number_line_per_page%>" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer7" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="30" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                        </Items>
                                        <SelectedItem Value="10" />
                                        <Listeners>
                                            <Select Handler="#{PagingToolbar2}.pageSize = parseInt(this.getValue()); #{PagingToolbar2}.doLoad();" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Listeners>
                                    <Change Handler="RowSelectionModelSalaryBoardList.clearSelections();" />
                                </Listeners>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAcceptSalaryBoardList" runat="server" Icon="Accept" Text="Đồng ý">
                        <Listeners>
                            <Click Handler="if (CheckSelectedRows(grpSalaryBoardList) == false) {return false;}" />
                        </Listeners>
                         <DirectEvents>
                            <Click OnEvent="ChooseSalaryBoardList_Click">
                                <EventMask ShowMask="true" Msg="Đang tạo bảng lương..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseSalaryBoardList" Text="Đóng lại"
                        Icon="Decline">
                        <Listeners>
                            <Click Handler="wdSalaryBoardManage.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdCreateSalaryBoardList" Constrain="true"
                Title="Tạo bảng lương" Icon="Add" Layout="FormLayout" Width="660" AutoHeight="true">
                <Items>
                    <ext:Hidden runat="server" ID="hdfDepartmentId" />
                    <ext:ComboBox runat="server" ID="cbxDepartment" FieldLabel="Chọn bộ phận"
                        LabelWidth="70" Width="300" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                        ItemSelector="div.list-item" AnchorHorizontal="98%" Editable="false" StoreID="storeDepartment">
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
                        <Listeners>
                            <Select Handler="this.triggers[0].show();hdfDepartmentId.setValue(cbxDepartment.getValue());
                                txtTitleSalaryBoard.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                            <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Container ID="Containerm" Height="27" runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:CompositeField runat="server" ID="cf1" FieldLabel="Chọn tháng">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cbxMonth" Width="80" Editable="false" FieldLabel="Chọn tháng">
                                        <Items>
                                            <ext:ListItem Text="Tháng 1" Value="1" />
                                            <ext:ListItem Text="Tháng 2" Value="2" />
                                            <ext:ListItem Text="Tháng 3" Value="3" />
                                            <ext:ListItem Text="Tháng 4" Value="4" />
                                            <ext:ListItem Text="Tháng 5" Value="5" />
                                            <ext:ListItem Text="Tháng 6" Value="6" />
                                            <ext:ListItem Text="Tháng 7" Value="7" />
                                            <ext:ListItem Text="Tháng 8" Value="8" />
                                            <ext:ListItem Text="Tháng 9" Value="9" />
                                            <ext:ListItem Text="Tháng 10" Value="10" />
                                            <ext:ListItem Text="Tháng 11" Value="11" />
                                            <ext:ListItem Text="Tháng 12" Value="12" />
                                        </Items>
                                        <Listeners>
                                            <Select Handler="hdfMonth.setValue(cbxMonth.getValue());
                                                txtTitleSalaryBoard.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:SpinnerField runat="server" ID="spnYear" FieldLabel="Chọn năm" Width="55">
                                        <Listeners>
                                            <Spin Handler="hdfYear.setValue(spnYear.getValue());
                                                txtTitleSalaryBoard.setValue('Bảng tính lương tháng '+cbxMonth.getValue()+' năm '+spnYear.getValue());" />
                                        </Listeners>
                                    </ext:SpinnerField>
                                </Items>
                            </ext:CompositeField>
                        </Items>
                    </ext:Container>
                    <ext:TextArea ID="txtTitleSalaryBoard" BlankText="Bạn bắt buộc phải nhập tên bảng lương" CtCls="requiredData"
                        AllowBlank="false" AnchorHorizontal="100%" ColumnWidth="1.0" FieldLabel="Tên bảng tính lương"
                        runat="server" />
                    <ext:TextArea ID="txtDesciptionSalaryBoard" AnchorHorizontal="100%" ColumnWidth="1.0" FieldLabel="Mô tả" runat="server" />
                   
                </Items>
                <Buttons>
                    <ext:Button ID="btnCreateSalaryBoard" runat="server" Icon="Disk" Text="Cập nhật">
                        <Listeners>
                            <Click Handler="return checkInputSalaryBoard();" />
                        </Listeners>
                           <DirectEvents>
                                <Click OnEvent="CreateSalaryBoardList_Click">
                                    <EventMask ShowMask="true" Msg="Đang tạo bảng tính lương..." />
                                </Click>
                            </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdateEditSalaryBoard" Text="Cập nhật" Icon="Disk" Hidden="True">
                        <Listeners>
                            <Click Handler="return checkInputSalaryBoard();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="UpdateSalaryBoardList_Click">
                                <EventMask ShowMask="true" />
                                <ExtraParams>
                                    <ext:Parameter Name="Edit" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnCloseSalaryBoard" runat="server" Icon="Decline" Text="Đóng lại">
                        <Listeners>
                            <Click Handler="wdCreateSalaryBoardList.hide();Ext.net.DirectMethods.ResetFormSalaryList();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window Modal="true" Hidden="true" runat="server" ID="wdAdjustment" Constrain="true"
                Title="Điều chỉnh" Icon="Pencil" Layout="FormLayout" Width="450" AutoHeight="true"
                Padding="6">
                <Items>
                    <ext:Hidden runat="server" ID="hdfSelectColumnId"/>
                    <ext:ComboBox runat="server" ID="cbxSelectColumn" Editable="false" FieldLabel="Chọn cột<span style='color:red'>*</span>" CtCls="requiredData"
                        DisplayField="Display" ValueField="Id" AnchorHorizontal="100%"
                        TabIndex="93">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                        </Triggers>
                        <Store>
                            <ext:Store runat="server" ID="cbxSelectColumnStore" AutoLoad="false" OnRefreshData="cbxSelectColumnStore_OnRefreshData">
                                <Reader>
                                    <ext:JsonReader IDProperty="Id">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="ColumnCode" />
                                            <ext:RecordField Name="Display" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <Listeners>
                            <Expand Handler="if(cbxSelectColumn.store.getCount()==0) cbxSelectColumnStore.reload();" />
                            <Select Handler="this.triggers[0].show();hdfSelectColumnId.setValue(cbxSelectColumn.getValue());" />
                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfSelectColumnId.reset(); }" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtValueAdjustment" FieldLabel="Giá trị<span style='color:red'>*</span>" AnchorHorizontal="100%" CtCls="requiredData"
                        />
                    <ext:CompositeField runat="server" ID="cpf" FieldLabel="Áp dụng<span style='color:red'>*</span>" AnchorHorizontal="100%">
                        <Items>
                            <ext:Container runat="server" ID="ctn" Layout="FormLayout" AnchorHorizontal="100%"
                                LabelWidth="1">
                                <Items>
                                    <ext:Checkbox runat="server" ID="chkApplySelectedEmployee" BoxLabel="Cho những nhân viên được chọn"/>
                                    <ext:Checkbox runat="server" ID="chkApplyForAll" BoxLabel="Cho tất cả nhân viên trong bảng lương"/>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:CompositeField>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnAcceptAdjustment" Text="Đồng ý" Icon="Accept">
                        <Listeners>
                            <Click Handler="return ValidateAjustment();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnAcceptAdjustment_Click">
                                <EventMask ShowMask="true" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button1" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAdjustment.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetAjustmentWindow();" />
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>

