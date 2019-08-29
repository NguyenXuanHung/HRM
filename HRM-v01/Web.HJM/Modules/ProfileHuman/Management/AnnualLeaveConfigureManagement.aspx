<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.ProfileHuman.Management.AnnualLeaveConfigureManagement" Codebehind="AnnualLeaveConfigureManagement.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/ChooseEmployee/ucChooseEmployee.ascx" TagPrefix="uc1" TagName="ucChooseEmployee" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Resource/js/jquery-3.2.1.min.js"></script>
    <script src="../../../Resource/js/global.js" type="text/javascript"></script>
    <script src="../../../Resource/js/RenderJS.js" type="text/javascript"></script>
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

        var ReloadGrid = function () {
            PagingToolbar1.pageIndex = 0;
            PagingToolbar1.doLoad();
            RowSelectionModel1.clearSelections();
        }

        var triggershowChooseEmplyee = function (f, e) {
            if (e.getKey() == e.ENTER) {
                ucChooseEmployee1_wdChooseUser.show();
            }
        }
        var addRecord = function (RecordId, EmployeeCode, FullName, DepartmentName) {
            var rowindex = getSelectedIndexRow();
            grp_ListEmployee.insertRecord(rowindex, {
                RecordId: RecordId,
                EmployeeCode: EmployeeCode,
                FullName: FullName,
                DepartmentName: DepartmentName
            });
            grp_ListEmployee.getView().refresh();
            grp_ListEmployee.getSelectionModel().selectRow(rowindex);
        }
        var getSelectedIndexRow = function () {
            var record = grp_ListEmployee.getSelectionModel().getSelected();
            var index = grp_ListEmployee.store.indexOf(record);
            if (index == -1)
                return 0;
            return index;
        }

        var getPrKeyRecordList = function () {
            var jsonDataEncode = "";
            var records = window.grp_ListEmployeeStore.getRange();
            for (var i = 0; i < records.length; i++) {
                jsonDataEncode += records[i].data.RecordId + ",";
            }
            return jsonDataEncode;
        }

        var checkInputAnnualLeave = function () {
            if (txtAnnualLeaveDay.getValue() == '' || txtAnnualLeaveDay.getValue().trim == '') {
                alert('Bạn chưa nhập số ngày phép mặc định.');
                return false;
            }
            if (dfExpiredDate.getValue() == '' || dfExpiredDate.getValue() == null) {
                alert('Bạn chưa nhập thời hạn sử dụng phép năm trước.');
                return false;
            }

            return true;
        }

        var ResetwdAnnualLeave = function () {

            grp_ListEmployeeStore.removeAll();
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
            <ext:Hidden runat="server" ID="hdfDepartmentSelectedId" />
        


            <uc1:ucChooseEmployee runat="server" ID="ucChooseEmployee" ChiChonMotCanBo="false" DisplayWorkingStatus="TatCa" />
            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gridAnnualLeave" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="wdAnnualLeave.show(); wdAnnualLeave.setTitle('Tạo mới cấu hình ngày phép');btnUpdate.hide();btnUpdateClose.show();btnUpdateNew.show();grp_ListEmployee.show();txtFullName.hide();
                                                        Ext.net.DirectMethods.ResetForm();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="EditAnnualLeave_Click">
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
                                    <ext:Store ID="storeTimeSheetRule" AutoSave="true" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="AnnualLeave" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departmentSelected" Value="hdfDepartmentSelectedId.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="RecordId"/>
                                                    <ext:RecordField Name="UsedLeaveDay"/>
                                                    <ext:RecordField Name="RemainLeaveDay"/>
                                                    <ext:RecordField Name="AnnualLeaveDay" />
                                                    <ext:RecordField Name="DayAddedStep" />
                                                    <ext:RecordField Name="YearStep" />
                                                    <ext:RecordField Name="AllowUseFirstYear" />
                                                    <ext:RecordField Name="AllowUsePreviousYear" />
                                                    <ext:RecordField Name="ExpiredDate" />
                                                    <ext:RecordField Name="MaximumPerMonth" />
                                                    <ext:RecordField Name="FullName" />
                                                    <ext:RecordField Name="EmployeeCode" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="FullName" Header="Họ và tên" Width="150" Align="Left" Locked="true" DataIndex="FullName" />  
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="120" Align="Left" Locked="true" DataIndex="EmployeeCode" />  
                                        <ext:Column ColumnID="AnnualLeaveDay" Header="Số ngày phép mặc định" Width="150" Align="Left" Locked="true" DataIndex="AnnualLeaveDay" />  
                                        <ext:Column ColumnID="UsedLeaveDay" Header="Số ngày phép đã sử dụng" Width="150" Align="Left" Locked="true" DataIndex="UsedLeaveDay" />
                                        <ext:Column ColumnID="RemainLeaveDay" Header="Số ngày phép còn lại" Width="150" Align="Left" Locked="true" DataIndex="RemainLeaveDay" />
                                        <ext:Column ColumnID="YearStep" Header="Sau số năm được tăng thêm ngày phép" Width="250" Align="Left" Locked="true" DataIndex="YearStep" />
                                        <ext:Column ColumnID="DayAddedStep" Header="Số ngày phép cộng thêm" Width="170" DataIndex="DayAddedStep" />
                                        <ext:Column ColumnID="AllowUseFirstYear" Header="Cho sử dụng phép năm đầu tiên" Width="200" DataIndex="AllowUseFirstYear">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="AllowUsePreviousYear" Header="Cho sử dụng phép năm trước" Width="200" DataIndex="AllowUsePreviousYear">
                                            <Renderer Fn="GetBooleanIcon" />
                                        </ext:Column>
                                        <ext:Column ColumnID="MaximumPerMonth" Header="Số ngày phép tối đa trên tháng" Width="200" DataIndex="MaximumPerMonth" />

                                        <ext:DateColumn ColumnID="ExpiredDate" Header="Thời hạn sử dụng phép năm trước" Width="200" DataIndex="ExpiredDate" Format="dd/MM/yyyy" />
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
            <ext:Window runat="server" Title="Tạo mới cấu hình ngày phép" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdAnnualLeave"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" Height="250" LabelWidth="230">
                        <Items>
                            <ext:TextField runat="server" ID="txtFullName" FieldLabel="Họ và tên" ReadOnly="True" AnchorHorizontal="98%" Hidden="True"/>
                            <ext:TextField runat="server" ID="txtAnnualLeaveDay" FieldLabel="Số ngày phép mặc định"
                                AnchorHorizontal="98%" MaskRe="/[0-9.,]/" MaxLength="10" CtCls="requiredData" />
                            <ext:TextField runat="server" ID="txtUsedLeaveDay" FieldLabel="Số ngày phép đã sử dụng"
                                           AnchorHorizontal="98%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:TextField runat="server" ID="txtRemainLeaveDay" FieldLabel="Số ngày phép còn lại"
                                           AnchorHorizontal="98%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:TextField runat="server" ID="txtYearStep" FieldLabel="Sau số năm được tăng thêm ngày phép"
                                AnchorHorizontal="98%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:TextField runat="server" ID="txtDayAddedStep" FieldLabel="Số ngày phép tăng thêm"
                                AnchorHorizontal="98%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:Checkbox runat="server" FieldLabel="Sử dụng phép năm đầu" BoxLabel="Được sử dụng" ID="chk_AllowUseFirstYear" />
                            <ext:Checkbox runat="server" FieldLabel="Sử dụng phép năm trước" BoxLabel="Được sử dụng" ID="chk_AllowUsePreviousYear" />
                            <ext:TextField runat="server" ID="txtMaximumPerMonth" FieldLabel="Số ngày phép tối đa trên tháng"
                                AnchorHorizontal="98%" MaskRe="/[0-9.,]/" MaxLength="10" />
                            <ext:DateField ID="dfExpiredDate" runat="server" Vtype="daterange" FieldLabel="Thời hạn sử dụng phép năm trước"
                                AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" CtCls="requiredData"/>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" ID="ctn23" Layout="BorderLayout" Height="250">
                    <Items>
                        <ext:GridPanel runat="server" ID="grp_ListEmployee" TrackMouseOver="true" Title="Danh sách cán bộ"
                            StripeRows="true" Border="true" Region="Center" Icon="User" AutoExpandColumn="DepartmentName"
                            AutoExpandMin="150">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="tbListEmployee">
                                    <Items>
                                        <ext:Button runat="server" ID="btnChooseEmployee" Icon="UserAdd" Text="Chọn cán bộ"
                                            TabIndex="12">
                                            <Listeners>
                                                <Click Handler="ucChooseEmployee_wdChooseUser.show();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnDeleteEmployee" Icon="Delete" Text="Xóa" Disabled="true"
                                            TabIndex="13">
                                            <Listeners>
                                                <Click Handler="#{grp_ListEmployee}.deleteSelected(); #{hdfTotalRecord}.setValue(#{hdfTotalRecord}.getValue()*1 - 1);if(hdfTotalRecord.getValue() ==0){btnDeleteEmployee.disable(); }" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="grp_ListEmployeeStore" AutoLoad="false" runat="server" ShowWarningOnFailure="false"
                                    SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="RecordId">
                                            <Fields>
                                                <ext:RecordField Name="RecordId" />
                                                <ext:RecordField Name="EmployeeCode" />
                                                <ext:RecordField Name="FullName" />
                                                <ext:RecordField Name="DepartmentName" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel3" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn Header="STT" Width="40" />
                                    <ext:Column ColumnID="EmployeeCode" Header="Mã cán bộ" Width="100" DataIndex="EmployeeCode" />
                                    <ext:Column ColumnID="FullName" Header="Họ tên" Width="200" DataIndex="FullName" />
                                    <ext:Column ColumnID="DepartmentName" Header="Bộ phận" Width="100" DataIndex="DepartmentName">
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:LockingGridView runat="server" ID="lkv1" />
                            </View>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" ID="chkEmployeeRowSelection">
                                    <Listeners>
                                        <RowSelect Handler="btnDeleteEmployee.enable();" />
                                        <RowDeselect Handler="btnDeleteEmployee.disable();" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Container>

                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateNew" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputAnnualLeave();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                                <ExtraParams>
                                    <ext:Parameter Name="ListRecordId" Value="getPrKeyRecordList()" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnUpdate" Hidden="true" Text="Cập nhật" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputAnnualLeave();" />
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
                            <Click Handler="return checkInputAnnualLeave();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="ListRecordId" Value="getPrKeyRecordList()" Mode="Raw" />
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdAnnualLeave.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetwdAnnualLeave();"></Hide>
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>

