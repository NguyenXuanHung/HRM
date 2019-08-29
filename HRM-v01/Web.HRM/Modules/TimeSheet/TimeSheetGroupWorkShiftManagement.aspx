<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.TimeSheet.TimeSheetGroupWorkShiftManagement" CodeBehind="TimeSheetGroupWorkShiftManagement.aspx.cs" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">
        var checkGroupWorkShift = function () {
            if (txtName.getValue() === '' || txtName.getValue().trim === '') {
                alert('Bạn chưa nhập tên nhóm phân ca!');
                return false;
            }
            return true;
        }

        var enableDay = function (value, dayOfWeek) {

            if (value && value == true) {
                if (dayOfWeek == 'Monday') {
                    fsMonday.show();
                    cboStartInMonday.setValue('0');
                    cboEndInMonday.setValue('0');
                    cboStartOutMonday.setValue('0');
                    cboEndOutMonday.setValue('0');
                    cboEndMonday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Monday')
                    fsMonday.hide();
            }

            if (value && value == true) {
                if (dayOfWeek == 'Tuesday') {
                    fsTuesday.show();
                    cboStartInTuesday.setValue('0');
                    cboEndInTuesday.setValue('0');
                    cboStartOutTuesday.setValue('0');
                    cboEndOutTuesday.setValue('0');
                    cboEndTuesday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Tuesday') { }
                fsTuesday.hide();
            }

            if (value && value == true) {
                if (dayOfWeek == 'Wednesday') {
                    fsWednesday.show();
                    cboStartInWednesday.setValue('0');
                    cboEndInWednesday.setValue('0');
                    cboStartOutWednesday.setValue('0');
                    cboEndOutWednesday.setValue('0');
                    cboEndWednesday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Wednesday')
                    fsWednesday.hide();
            }

            if (value && value == true) {
                if (dayOfWeek == 'Thursday') {
                    fsThursday.show();
                    cboStartInThursday.setValue('0');
                    cboEndInThursday.setValue('0');
                    cboStartOutThursday.setValue('0');
                    cboEndOutThursday.setValue('0');
                    cboEndThursday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Thursday')
                    fsThursday.hide();
            }

            if (value && value == true) {
                if (dayOfWeek == 'Friday') {
                    fsFriday.show();
                    cboStartInFriday.setValue('0');
                    cboEndInFriday.setValue('0');
                    cboStartOutFriday.setValue('0');
                    cboEndOutFriday.setValue('0');
                    cboEndFriday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Friday')
                    fsFriday.hide();
            }

            if (value && value == true) {
                if (dayOfWeek == 'Saturday') {
                    fsSaturday.show();
                    cboStartInSaturday.setValue('0');
                    cboEndInSaturday.setValue('0');
                    cboStartOutSaturday.setValue('0');
                    cboEndOutSaturday.setValue('0');
                    cboEndSaturday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Saturday')
                    fsSaturday.hide();
            }

            if (value && value == true) {
                if (dayOfWeek == 'Sunday') {
                    fsSunday.show();
                    cboStartInSunday.setValue('0');
                    cboEndInSunday.setValue('0');
                    cboStartOutSunday.setValue('0');
                    cboEndOutSunday.setValue('0');
                    cboEndSunday.setValue('0');
                }
            } else {
                if (dayOfWeek == 'Sunday')
                    fsSunday.hide();
            }
        }

        var checkInputTimeSheetWorkShift = function () {
            if (txtWorkName.getValue() == '' || txtWorkName.getValue().trim == '') {
                alert('Bạn chưa nhập tên ca!');
                return false;
            }

            if (!txtWorkShiftConvert.getValue()) {
                alert('Bạn chưa nhập công quy đổi!');
                return false;
            }
            if (!txtTimeWorkConvert.getValue()) {
                alert('Bạn chưa nhập thời gian quy đổi!');
                return false;
            }
            if (hdfSymbolWorkId.getValue() == '' || hdfSymbolWorkId.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu hiển thị trên bảng công!');
                return false;
            }

            if (!dfFromDate.getValue()) {
                alert('Bạn chưa chọn ngày bắt đầu !');
                return false;
            }
            if (!dfToDate.getValue()) {
                alert('Bạn chưa chọn ngày kết thúc !');
                return false;
            }

            if (chkEnableMonday.checked) {
                if (!tfStartMonday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInMonday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndMonday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInMonday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutMonday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutMonday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            if (chkEnableTuesday.checked) {
                if (!tfStartTuesday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInTuesday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndTuesday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInTuesday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutTuesday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutTuesday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            if (chkEnableWednesday.checked) {
                if (!tfStartWednesday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInWednesday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndWednesday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInWednesday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutWednesday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutWednesday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            if (chkEnableThursday.checked) {
                if (!tfStartThursday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInThursday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndThursday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInThursday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutThursday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutThursday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            if (chkEnableFriday.checked) {
                if (!tfStartFriday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInFriday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndFriday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInFriday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutFriday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutFriday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            if (chkEnableSaturday.checked) {
                if (!tfStartSaturday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInSaturday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndSaturday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInSaturday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutSaturday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutSaturday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            if (chkEnableSunday.checked) {
                if (!tfStartSunday.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                    return false;
                }
                if (!tfStartInSunday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndSunday.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc chấm công !');
                    return false;
                }
                if (!tfEndInSunday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfStartOutSunday.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutSunday.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }

            return true;
        }
        var checkInputTimeSheetRule = function () {
            if (txtName.getValue() == '' || txtName.getValue().trim == '') {
                alert('Bạn chưa nhập tên ca!');
                return false;
            }

            if (!txtWorkConvert.getValue()) {
                alert('Bạn chưa nhập công quy đổi!');
                return false;
            }
            if (!txtTimeConvert.getValue()) {
                alert('Bạn chưa nhập thời gian quy đổi!');
                return false;
            }
            if (hdfSymbolId.getValue() == '' || hdfSymbolId.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu hiển thị trên bảng công!');
                return false;
            }

            if (!timeSheetFromDate.getValue()) {
                alert('Bạn chưa chọn ngày bắt đầu !');
                return false;
            }
            if (!timeSheetToDate.getValue()) {
                alert('Bạn chưa chọn ngày kết thúc !');
                return false;
            }
            if (!inTime.getValue()) {
                alert('Bạn chưa nhập giờ bắt đầu chấm công !');
                return false;
            }
            if (!outTime.getValue()) {
                alert('Bạn chưa nhập giờ kết thúc chấm công !');
                return false;
            }

            if (!startInTime.getValue()) {
                alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                return false;
            }
            if (!endInTime.getValue()) {
                alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                return false;
            }
            if (!startOutTime.getValue()) {
                alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                return false;
            }
            if (!endOutTime.getValue()) {
                alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                return false;
            }
            return true;
        }
        var checkInputUtil = function () {
            if (txtNameTemplate.getValue() == '' || txtNameTemplate.getValue().trim == '') {
                alert('Bạn chưa nhập tên template!');
                return false;
            }

            if (hdfType.getValue() == '' || hdfType.getValue().trim == '') {
                alert('Bạn chưa chọn loại template!');
                return false;
            }
            if (hdfGroupSymbolUtil.getValue() == '' || hdfGroupSymbolUtil.getValue().trim == '') {
                alert('Bạn chưa chọn nhóm ký hiệu!');
                return false;
            }
            if (hdfSymbolIdUtil.getValue() == '' || hdfSymbolIdUtil.getValue().trim == '') {
                alert('Bạn chưa nhập ký hiệu hiển thị trên bảng công!');
                return false;
            }
            if (!txtWorkConvertUtil.getValue()) {
                alert('Bạn chưa nhập công quy đổi!');
                return false;
            }
            if (!txtTimeConvertUtil.getValue()) {
                alert('Bạn chưa nhập thời gian quy đổi!');
                return false;
            }

            if (!chkMondayUtil.getValue() && !chkTuesdayUtil.getValue()
                && !chkWednesdayUtil.getValue() && !chkThursday.getValue()
                && !chkFridayUtil.getValue() && !chkSaturday.getValue() && !chkSundayUtil.getValue()) {
                alert('Bạn chưa chọn tùy chọn!');
                return false;
            }
            
            //case normal
            if (hdfType.getValue() == '1') {
                if (!tfStartDateNormal.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu làm việc !');
                    return false;
                }
                if (!tfEndDateNormal.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc làm việc !');
                    return false;
                }
                if (!tfStartInNormal.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndInNormal.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
               
                if (!tfStartOutNormal.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutNormal.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            } else {
                //case break
                if (!tfStartDateBreak.getValue()) {
                    alert('Bạn chưa nhập giờ bắt đầu làm việc !');
                    return false;
                }
                if (!tfStartInBreak.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu vào chấm công !');
                    return false;
                }
                if (!tfEndInBreak.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc vào chấm công!');
                    return false;
                }
                if (!tfEndDateBreak.getValue()) {
                    alert('Bạn chưa nhập giờ kết thúc làm việc !');
                    return false;
                }
                if (!tfStartOutBreak.getValue()) {
                    alert('Bạn chưa nhập thời gian bắt đầu ra chấm công!');
                    return false;
                }
                if (!tfEndOutBreak.getValue()) {
                    alert('Bạn chưa nhập thời gian kết thúc ra chấm công!');
                    return false;
                }
            }
           
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
            <ext:Hidden runat="server" ID="hdfDepartments" />
            <ext:Hidden runat="server" ID="hdfName" />
            <ext:Hidden runat="server" ID="hdfNameNormal" />
            <ext:Hidden runat="server" ID="hdfType" /> 
            <ext:Hidden runat="server" ID="hdfTemplateId" />


            <ext:Store runat="server" ID="storeGroupSymbol" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={20}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GroupTimeSheetSymbol" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Group" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeSymbolBreak" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
                    <ext:Parameter Name="GroupSymbolTypeId" Value="hdfGroupSymbolShiftId.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeSymbol" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
                    <ext:Parameter Name="GroupSymbolTypeId" Value="hdfGroupSymbol.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store> 
            <ext:Store runat="server" ID="storeSymbolTemplate" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="TimeSheetSymbol" />
                    <ext:Parameter Name="GroupSymbolTypeId" Value="hdfGroupSymbolUtil.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store runat="server" ID="storeGroupWorkShift" AutoLoad="false">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                </Proxy>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="GroupWorkShift" />
                    <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="CreatedDate" />
                            <ext:RecordField Name="CreatedBy" />
                            <ext:RecordField Name="StartDate" />
                            <ext:RecordField Name="EndDate" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            <ext:Store ID="storeType" runat="server" AutoLoad="False" OnRefreshData="storeType_OnRefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" Mapping="Key" />
                            <ext:RecordField Name="Name" Mapping="Value" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Viewport ID="vp" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel ID="gpGroupWorkShift" TrackMouseOver="true" Header="false" runat="server"
                                StripeRows="true" Border="false" AnchorHorizontal="100%" AutoExpandColumn="Name">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowTimeWork" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnAddWork" Text="Thêm ca gãy" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindowTimeWorkBreak" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa thông tin này" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnConfig" runat="server" Text="Chi tiết" Icon="Table" Disabled="True">
                                                <Listeners>
                                                    <Click Handler=" return CheckSelectedRows(gpGroupWorkShift);btnConfig.show();"></Click>
                                                </Listeners>
                                                <DirectEvents>
                                                    <Click OnEvent="SelectGroupWorkShift_Click">
                                                        <EventMask ShowMask="true" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnUtil" runat="server" Text="Tiện ích" Icon="Build" Disabled="False">
                                                <Menu>
                                                    <ext:Menu runat="server">
                                                        <Items>
                                                            <ext:MenuItem runat="server" ID="mnListTemplate" Icon="Accept" Text="Chọn template">
                                                                <Listeners>
                                                                    <Click Handler="wdTemplate.show();storeTemplate.reload();"/>
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                            <ext:MenuItem runat="server" ID="mnAddTemplate" Icon="Add" Text="Tạo mới template">
                                                                <Listeners>
                                                                    <Click Handler="wdUtil.show();"/>
                                                                </Listeners>
                                                            </ext:MenuItem>
                                                        </Items>
                                                    </ext:Menu>
                                                </Menu>
                                            </ext:Button>
                                            <ext:Button runat="server" Text="Copy dữ liệu bảng phân ca" Icon="DiskMultiple" Disabled="True" ID="btnDuplicate">
                                                <DirectEvents>
                                                    <Click OnEvent="CopyData_Click">
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
                                    <ext:Store ID="storeAsset" runat="server">
                                        <Proxy>
                                            <ext:HttpProxy Method="POST" Url="~/Services/BaseHandler.ashx" />
                                        </Proxy>
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="={0}" />
                                            <ext:Parameter Name="limit" Value="={15}" />
                                        </AutoLoadParams>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="GroupWorkShift" />
                                            <ext:Parameter Name="SearchKey" Value="txtSearch.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="departments" Value="hdfDepartments.getValue()" Mode="Raw" />
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id" />
                                                    <ext:RecordField Name="Name" />
                                                    <ext:RecordField Name="Description" />
                                                    <ext:RecordField Name="CreatedDate" />
                                                    <ext:RecordField Name="CreatedBy" />
                                                    <ext:RecordField Name="StartDate" />
                                                    <ext:RecordField Name="EndDate" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Right" Locked="true" />
                                        <ext:Column ColumnID="Name" Header="Tên bảng phân ca" Width="250" DataIndex="Name" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="200" DataIndex="Description" />
                                        <ext:DateColumn ColumnID="StartDate" Header="Từ ngày" Width="100" DataIndex="StartDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="EndDate" Header="Đến ngày" Width="100" DataIndex="EndDate" Format="dd/MM/yyyy" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Ngày tạo" Width="100" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                        <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="100" DataIndex="CreatedBy" />
                                    </Columns>
                                </ColumnModel>
                                <LoadMask ShowMask="true" Msg="Đang tải...." />
                                <Listeners>
                                    <RowClick Handler="btnDelete.enable();btnConfig.enable();btnDuplicate.enable();" />
                                    <RowDblClick Handler="" />
                                </Listeners>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1">
                                        <Listeners>
                                            <RowSelect Handler="hdfKeyRecord.setValue(RowSelectionModel1.getSelected().get('Id')); " />
                                            <RowDeselect Handler="hdfKeyRecord.reset();btnDelete.disable();btnConfig.disable();btnDuplicate.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="15">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên một trang:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="15" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="25" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="40" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="15" />
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
            <ext:Window runat="server" Title="Tạo mới chi tiết bảng phân ca" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdTimeSheetRule"
                Modal="true" Constrain="true" Height="600" LabelWidth="150" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Height="130">
                        <Items>
                            <ext:Container ID="Container11" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên ca<span style='color:red;'>*</span>"
                                        AnchorHorizontal="98%">
                                        <Listeners>
                                            <Blur Handler="hdfNameNormal.setValue(txtName.getValue());"></Blur>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Hidden runat="server" ID="hdfGroupSymbol" />
                                    <ext:ComboBox runat="server" ID="cbxGroupSymbol" FieldLabel="Nhóm ký hiệu"
                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupSymbol"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData" PageSize="20">
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
                                            <Select Handler="this.triggers[0].show();hdfGroupSymbol.setValue(cbxGroupSymbol.getValue()); cbxSymbol.enable(); #{storeSymbol}.reload(); cbxSymbol.clearValue();txtWorkConvert.reset();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupSymbol.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtWorkConvert" CtCls="requiredData" FieldLabel="Công quy đổi<span style='color:red;'>*</span>" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="5" />
                                    
                                    <ext:DateField ID="timeSheetFromDate" runat="server" Vtype="daterange" FieldLabel="Từ ngày" CtCls="requiredData"
                                        AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                        <CustomConfig>
                                            <ext:ConfigItem Name="endDateField" Value="#{timeSheetToDate}" Mode="Value" />
                                        </CustomConfig>
                                        <Listeners>
                                            <Select Handler="if(timeSheetFromDate.getValue() != '') {txtName.setValue(hdfNameNormal.getValue() + ' Từ ' + timeSheetFromDate.getValue().format('d/m/Y'));};
                                                if(timeSheetFromDate.getValue() != '' && timeSheetToDate.getValue() != '') {txtName.setValue(hdfNameNormal.getValue() + ' Từ ' + timeSheetFromDate.getValue().format('d/m/Y') + ' - ' + timeSheetToDate.getValue().format('d/m/Y'))};"></Select>
                                        </Listeners>
                                    </ext:DateField>
                                    <ext:TextArea runat="server" ID="txtDescriptionNormal" FieldLabel="Mô tả" AnchorHorizontal="98%" Height="22" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container12" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:Hidden runat="server" ID="hdfGroupWorkShift" />
                                    <ext:ComboBox runat="server" ID="cboGroup" FieldLabel="Nhóm phân ca"
                                                  DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupWorkShift"
                                                  LabelWidth="252" Width="422" ItemSelector="div.list-item" PageSize="20">
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
                                            <Expand Handler="storeGroupWorkShift.reload();"></Expand>
                                            <Select Handler="this.triggers[0].show();hdfGroupWorkShift.setValue(cboGroup.getValue());" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupWorkShift.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    
                                    <ext:Hidden runat="server" ID="hdfSymbolId" />
                                    <ext:ComboBox runat="server" ID="cbxSymbol" FieldLabel="Ký hiệu hiển thị<span style='color:red;'>*</span>" DisplayField="Code" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeSymbol" PageSize="15" Disabled="True"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template runat="server">
                                            <Html>
                                                <tpl for=".">
                                                    <div class="list-item"> 
                                                        {Code}
                                                    </div>
                                                </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show(); hdfSymbolId.setValue(cbxSymbol.getValue()); Ext.net.DirectMethods.SetValueSelectSymbol();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSymbolId.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtTimeConvert" CtCls="requiredData" FieldLabel="Thời gian quy đổi (phút)" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="10" />
                                    
                                    <ext:DateField ID="timeSheetToDate" runat="server" Vtype="daterange" FieldLabel="Đến ngày" CtCls="requiredData"
                                        AnchorHorizontal="98%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                        <CustomConfig>
                                            <ext:ConfigItem Name="startDateField" Value="#{timeSheetFromDate}" Mode="Value" />
                                        </CustomConfig>
                                        <Listeners>
                                            <Select Handler="if(timeSheetFromDate.getValue() != '' && timeSheetToDate.getValue() != '') {txtName.setValue(hdfNameNormal.getValue() + ' Từ ' + timeSheetFromDate.getValue().format('d/m/Y') + ' - ' + timeSheetToDate.getValue().format('d/m/Y'))};"></Select>
                                        </Listeners>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Height="80">
                        <Items>
                            <ext:Container ID="Container13" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TimeField ID="inTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                        Format="H:mm" FieldLabel="Giờ vào làm việc<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="startInTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Bắt đầu chấm công (Từ)<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="startOutTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Kết thúc chấm công (Từ)<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container14" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TimeField ID="outTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                        Format="H:mm" FieldLabel="Giờ kết thúc làm việc<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="endInTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Đến<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="endOutTime" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Đến<span style='color:red;'>*</span>" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="containerDay" runat="server">
                        <Items>
                            <ext:CheckboxGroup ID="checkGroupDay" runat="server" ColumnsNumber="7" FieldLabel="Chọn thứ">
                                <Items>
                                    <ext:Checkbox runat="server" ID="chkMonday" BoxLabel="Thứ 2" />
                                    <ext:Checkbox runat="server" ID="chkTuesday" BoxLabel="Thứ 3" />
                                    <ext:Checkbox runat="server" ID="chkWednesday" BoxLabel="Thứ 4" />
                                    <ext:Checkbox runat="server" ID="chkThursday" BoxLabel="Thứ 5" />
                                    <ext:Checkbox runat="server" ID="chkFriday" BoxLabel="Thứ 6" />
                                    <ext:Checkbox runat="server" ID="chkSaturday" BoxLabel="Thứ 7" />
                                    <ext:Checkbox runat="server" ID="chkSunday" BoxLabel="Chủ nhật" />
                                </Items>
                            </ext:CheckboxGroup>
                        </Items>
                    </ext:Container>
                    <ext:Checkbox runat="server" BoxLabel="Bỏ ngày lễ tết" ID="chkTetHoliday" />
                    <ext:Checkbox runat="server" BoxLabel="Tính giờ vào ra" ID="chk_IsHasInOutTime" />
                    <ext:Checkbox runat="server" BoxLabel="Cho phép thêm giờ" ID="chk_IsOverTime" />
                    <ext:Checkbox runat="server" BoxLabel="Giới hạn giờ làm việc tối đa" ID="chk_IsLimitMaxTime" />
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateClose" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetRule();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="Insert">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeSheetRule.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Tạo mới chi tiết bảng phân ca" Layout="FormLayout"
                Padding="6" Width="850" Hidden="true" Icon="UserTick" ID="wdTimeWork" Resizable="True"
                Modal="true" Constrain="true" Height="600" LabelWidth="120" AutoScroll="True" AutoHeight="True">
                <Items>
                    <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Height="110">
                        <Items>
                            <ext:Container ID="Container3" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtWorkName" CtCls="requiredData" FieldLabel="Tên ca<span style='color:red;'>*</span>"
                                        AnchorHorizontal="98%">
                                        <Listeners>
                                            <Blur Handler="hdfName.setValue(txtWorkName.getValue());"></Blur>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Hidden runat="server" ID="hdfGroupSymbolShiftId" />
                                    <ext:ComboBox runat="server" ID="cbxGroupSymbolShift" FieldLabel="Nhóm ký hiệu"
                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupSymbol"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData" PageSize="20">
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
                                            <Select Handler="this.triggers[0].show();hdfGroupSymbolShiftId.setValue(cbxGroupSymbolShift.getValue()); cboSymbolWork.enable(); #{storeSymbolBreak}.reload(); cboSymbolWork.clearValue();txtWorkShiftConvert.reset();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupSymbolShiftId.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>

                                    <ext:TextField runat="server" ID="txtWorkShiftConvert" CtCls="requiredData" FieldLabel="Công quy đổi<span style='color:red;'>*</span>" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="5" />
                                    <ext:DateField ID="dfFromDate" runat="server" Vtype="daterange" FieldLabel="Từ ngày" CtCls="requiredData"
                                        AnchorHorizontal="98%" TabIndex="3" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                        <Listeners>
                                            <Select Handler="if(dfFromDate.getValue() != '') {txtWorkName.setValue(hdfName.getValue() + ' Từ ' + dfFromDate.getValue().format('d/m/Y'));};
                                                if(dfFromDate.getValue() != '' && dfToDate.getValue() != '') {txtWorkName.setValue(hdfName.getValue() + ' Từ ' + dfFromDate.getValue().format('d/m/Y') + ' - ' + dfToDate.getValue().format('d/m/Y'))};"></Select>
                                        </Listeners>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container7" runat="server" LabelWidth="140" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextArea runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="96%" Height="22" />
                                    <ext:Hidden runat="server" ID="hdfSymbolWorkId" />
                                    <ext:ComboBox runat="server" ID="cboSymbolWork" FieldLabel="Ký hiệu hiển thị<span style='color:red;'>*</span>" DisplayField="Code" MinChars="1" ValueField="Id" AnchorHorizontal="96%" Editable="true" StoreID="storeSymbolBreak" PageSize="15" Disabled="True"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template runat="server">
                                            <Html>
                                                <tpl for=".">
                                                    <div class="list-item"> 
                                                        {Code}
                                                    </div>
                                                </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show(); hdfSymbolWorkId.setValue(cboSymbolWork.getValue()); Ext.net.DirectMethods.SetValueSelectSymbol();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSymbolWorkId.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtTimeWorkConvert" CtCls="requiredData" FieldLabel="Thời gian quy đổi (phút)" MaskRe="/[0-9.]/" AnchorHorizontal="96%" MaxLength="10" />

                                    <ext:DateField ID="dfToDate" runat="server" Vtype="daterange" FieldLabel="Đến ngày" CtCls="requiredData"
                                        AnchorHorizontal="96%" TabIndex="4" Editable="true" MaskRe="/[0-9\/]/" Format="dd/MM/yyyy">
                                        <Listeners>
                                            <Select Handler="if(dfFromDate.getValue() != '' && dfToDate.getValue() != '') {txtWorkName.setValue(hdfName.getValue() + ' Từ ' + dfFromDate.getValue().format('d/m/Y') + ' - ' + dfToDate.getValue().format('d/m/Y'))}; "></Select>
                                        </Listeners>
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:CheckboxGroup ID="chk_GroupHasWork" runat="server" ColumnsNumber="4" FieldLabel="">
                        <Items>
                            <ext:Checkbox runat="server" BoxLabel="Bỏ ngày lễ tết" ID="chk_HasWorkHoliday" />
                            <ext:Checkbox runat="server" BoxLabel="Tính giờ vào ra" ID="chk_HasWorkInOutTime" />
                            <ext:Checkbox runat="server" BoxLabel="Cho phép thêm giờ" ID="chk_HasWorkOverTime" />
                            <ext:Checkbox runat="server" BoxLabel="Giờ làm việc tối đa" ID="chk_HasWorkLimitTime" />
                        </Items>
                    </ext:CheckboxGroup>
                    <ext:CheckboxGroup ID="chkDayOfWeek" runat="server" ColumnsNumber="7" FieldLabel="Tùy chọn">
                        <Items>
                            <ext:Checkbox runat="server" BoxLabel="Thứ 2" ID="chkEnableMonday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Monday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                            <ext:Checkbox runat="server" BoxLabel="Thứ 3" ID="chkEnableTuesday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Tuesday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                            <ext:Checkbox runat="server" BoxLabel="Thứ 4" ID="chkEnableWednesday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Wednesday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                            <ext:Checkbox runat="server" BoxLabel="Thứ 5" ID="chkEnableThursday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Thursday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                            <ext:Checkbox runat="server" BoxLabel="Thứ 6" ID="chkEnableFriday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Friday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                            <ext:Checkbox runat="server" BoxLabel="Thứ 7" ID="chkEnableSaturday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Saturday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                            <ext:Checkbox runat="server" BoxLabel="Chủ nhật" ID="chkEnableSunday">
                                <Listeners>
                                    <Check Handler="enableDay(this.getValue(), 'Sunday');"></Check>
                                </Listeners>
                            </ext:Checkbox>
                        </Items>
                    </ext:CheckboxGroup>
                    <ext:FieldSet runat="server" ID="fsMonday" Padding="5" AnchorHorizontal="98%" Title="Thứ 2" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkMonday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartMonday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInMonday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInMonday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInMonday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInMonday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkMonday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndMonday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndMonday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Thứ 2" Value="0" />
                                                    <ext:ListItem Text="Thứ 3" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutMonday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutMonday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutMonday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutMonday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="fsTuesday" runat="server" Padding="5" AnchorHorizontal="98%" Title="Thứ 3" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkTuesday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartTuesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInTuesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInTuesday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInTuesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInTuesday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkTuesday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndTuesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndTuesday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Thứ 3" Value="0" />
                                                    <ext:ListItem Text="Thứ 4" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutTuesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutTuesday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutTuesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutTuesday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="fsWednesday" runat="server" Padding="5" AnchorHorizontal="98%" Title="Thứ 4" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkWednesday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartWednesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInWednesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInWednesday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInWednesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInWednesday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkWednesday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndWednesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndWednesday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Thứ 4" Value="0" />
                                                    <ext:ListItem Text="Thứ 5" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutWednesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutWednesday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutWednesday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutWednesday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="fsThursday" runat="server" Padding="5" AnchorHorizontal="98%" Title="Thứ 5" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkThursday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartThursday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInThursday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInThursday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInThursday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInThursday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkThursday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndThursday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndThursday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Thứ 5" Value="0" />
                                                    <ext:ListItem Text="Thứ 6" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutThursday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutThursday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutThursday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutThursday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="fsFriday" runat="server" Padding="5" AnchorHorizontal="98%" Title="Thứ 6" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkFriday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartFriday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInFriday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInFriday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInFriday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInFriday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkFriday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndFriday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndFriday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Thứ 6" Value="0" />
                                                    <ext:ListItem Text="Thứ 7" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutFriday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutFriday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutFriday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutFriday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="fsSaturday" runat="server" Padding="5" AnchorHorizontal="98%" Title="Thứ 7" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkSaturday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartSaturday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInSaturday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInSaturday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInSaturday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInSaturday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkSaturday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndSaturday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndSaturday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Thứ 7" Value="0" />
                                                    <ext:ListItem Text="Chủ nhật" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutSaturday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutSaturday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutSaturday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutSaturday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="fsSunday" runat="server" Padding="5" AnchorHorizontal="98%" Title="Chủ nhật" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="ctnStartWorkSunday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartSunday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="100" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInSunday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInSunday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInSunday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInSunday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="ctnEndWorkSunday" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndSunday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboEndSunday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Chủ nhật" Value="0" />
                                                    <ext:ListItem Text="Thứ 2" Value="1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutSunday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutSunday" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutSunday" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutSunday" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>

                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateCloseWork" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputTimeSheetWorkShift();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertWork">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseWork" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdTimeWork.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Tạo mới template chi tiết bảng phân ca" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdUtil"
                Modal="true" Constrain="true" Height="600" LabelWidth="150">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="85">
                        <Items>
                            <ext:Container ID="Container4" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtNameTemplate" CtCls="requiredData" FieldLabel="Tên template"
                                        AnchorHorizontal="98%" />
                                    <ext:Hidden runat="server" ID="hdfGroupSymbolUtil" />
                                    <ext:ComboBox runat="server" ID="cbxGroupSymbolUtil" FieldLabel="Nhóm ký hiệu"
                                        DisplayField="Name" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeGroupSymbol"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData" PageSize="20">
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
                                            <Select Handler="this.triggers[0].show();hdfGroupSymbolUtil.setValue(cbxGroupSymbolUtil.getValue()); cbxSymbolUtil.enable(); #{storeSymbolTemplate}.reload(); cbxSymbolUtil.clearValue();txtWorkConvertUtil.reset();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfGroupSymbolUtil.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>

                                    <ext:TextField runat="server" ID="txtWorkConvertUtil" CtCls="requiredData" FieldLabel="Công quy đổi" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="5" />

                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container5" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:ComboBox runat="server" ID="cboType" StoreID="storeType" FieldLabel="Loại" DisplayField="Name" ValueField="Id" AnchorHorizontal="98%" CtCls="requiredData">
                                        <Listeners>
                                            <Expand Handler="if(cboType.store.getCount()==0){storeType.reload();}" />
                                            <Select Handler="hdfType.setValue(this.getValue());if(this.getValue() == '1'){ctnWorkUtil.show();fsDay.hide();}else{ctnWorkUtil.hide();fsDay.show();};"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:Hidden runat="server" ID="hdfSymbolIdUtil" />
                                    <ext:ComboBox runat="server" ID="cbxSymbolUtil" FieldLabel="Ký hiệu hiển thị" DisplayField="Code" MinChars="1" ValueField="Id" AnchorHorizontal="98%" Editable="true" StoreID="storeSymbolTemplate" PageSize="15" Disabled="True"
                                        LabelWidth="252" Width="422" ItemSelector="div.list-item" CtCls="requiredData">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Template runat="server">
                                            <Html>
                                                <tpl for=".">
                                                    <div class="list-item"> 
                                                        {Code}
                                                    </div>
                                                </tpl>
                                            </Html>
                                        </Template>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show(); hdfSymbolIdUtil.setValue(cbxSymbolUtil.getValue()); Ext.net.DirectMethods.SetValueSelectSymbol();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); hdfSymbolIdUtil.reset(); };" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField runat="server" ID="txtTimeConvertUtil" CtCls="requiredData" FieldLabel="Thời gian quy đổi (phút)" MaskRe="/[0-9.]/" AnchorHorizontal="98%" MaxLength="10" />

                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                   
                    <ext:CheckboxGroup ID="checkGroupDayUtil" runat="server" ColumnsNumber="7" FieldLabel="Tùy chọn">
                        <Items>
                            <ext:Checkbox runat="server" ID="chkMondayUtil" BoxLabel="Thứ 2" />
                            <ext:Checkbox runat="server" ID="chkTuesdayUtil" BoxLabel="Thứ 3" />
                            <ext:Checkbox runat="server" ID="chkWednesdayUtil" BoxLabel="Thứ 4" />
                            <ext:Checkbox runat="server" ID="chkThursdayUtil" BoxLabel="Thứ 5" />
                            <ext:Checkbox runat="server" ID="chkFridayUtil" BoxLabel="Thứ 6" />
                            <ext:Checkbox runat="server" ID="chkSaturdayUtil" BoxLabel="Thứ 7" />
                            <ext:Checkbox runat="server" ID="chkSundayUtil" BoxLabel="Chủ nhật" />
                        </Items>
                    </ext:CheckboxGroup>
                    <ext:CheckboxGroup ID="checkGroupOptionUtil" runat="server" ColumnsNumber="4" FieldLabel="">
                        <Items>
                            <ext:Checkbox runat="server" BoxLabel="Bỏ ngày lễ tết" ID="chk_HasWorkHolidayUtil" />
                            <ext:Checkbox runat="server" BoxLabel="Tính giờ vào ra" ID="chk_HasWorkInOutTimeUtil" />
                            <ext:Checkbox runat="server" BoxLabel="Cho phép thêm giờ" ID="chk_HasWorkOverTimeUtil" />
                            <ext:Checkbox runat="server" BoxLabel="Giờ làm việc tối đa" ID="chk_HasWorkLimitTimeUtil" />
                        </Items>
                    </ext:CheckboxGroup>

                    <ext:Container ID="ctnWorkUtil" runat="server" Layout="ColumnLayout" Height="80" Hidden="False">
                        <Items>
                            <ext:Container ID="Container8" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TimeField ID="tfStartDateNormal" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                        Format="H:mm" FieldLabel="Giờ vào làm việc" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="tfStartInNormal" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Bắt đầu chấm công từ" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="tfStartOutNormal" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Kết thúc chấm công từ" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container15" runat="server" LabelWidth="150" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TimeField ID="tfEndDateNormal" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                        Format="H:mm" FieldLabel="Giờ kết thúc làm việc" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="tfEndInNormal" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Đến" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                    <ext:TimeField ID="tfEndOutNormal" runat="server" MinTime="00:00" MaxTime="23:59" Increment="1"
                                        Format="H:mm" FieldLabel="Đến" AnchorHorizontal="98%"
                                        MaskRe="/[0-9:]/" TabIndex="3" CtCls="requiredData" EmptyText="Giờ">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:TimeField>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                    <ext:FieldSet runat="server" ID="fsDay" Padding="5" AnchorHorizontal="98%" AutoHeight="True" Layout="FormLayout" Hidden="True">
                        <Items>
                            <ext:Container runat="server" ID="Container16" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfStartDateBreak" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" CtCls="requiredData"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Bắt đầu làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="50" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartInBreak" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5" CtCls="requiredData"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartInUtil" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndInBreak" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5" CtCls="requiredData"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndInUtil" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container runat="server" ID="Container17" Layout="ColumnLayout" Height="25" AnchorHorizontal="100%">
                                <Items>
                                    <ext:Container runat="server" Width="185" Layout="Form" LabelWidth="100">
                                        <Items>
                                            <ext:TimeField ID="tfEndDateBreak" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" CtCls="requiredData"
                                                MaskRe="/[0-9:]/" TabIndex="3" FieldLabel="Kết thúc làm việc">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Label runat="server" Width="50" Text="&thinsp;"></ext:Label>
                                    <ext:Container runat="server" Width="180" Layout="Form" LabelWidth="80">
                                        <Items>
                                            <ext:TimeField ID="tfStartOutBreak" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5" CtCls="requiredData"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Chấm công từ">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="100" Layout="Form" LabelWidth="1">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cboStartOutUtil" SelectedIndex="0" AllowBlank="False" AnchorHorizontal="96%">
                                                <Items>
                                                    <ext:ListItem Text="Cùng ngày" Value="0" />
                                                    <ext:ListItem Text="Hôm trước" Value="-1" />
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Width="120" Layout="Form" LabelWidth="25">
                                        <Items>
                                            <ext:TimeField ID="tfEndOutBreak" runat="server" MinTime="00:00" MaxTime="23:59" Increment="5" CtCls="requiredData"
                                                Format="H:mm" AnchorHorizontal="98%" EmptyText="Giờ" MaskRe="/[0-9:]/" FieldLabel="Đến">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                                </Listeners>
                                            </ext:TimeField>
                                        </Items>
                                    </ext:Container>
                                    <ext:ComboBox runat="server" ID="cboEndOutUtil" Width="90" SelectedIndex="0" AllowBlank="False">
                                        <Items>
                                            <ext:ListItem Text="Cùng ngày" Value="0" />
                                            <ext:ListItem Text="Hôm sau" Value="1" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateCloseUtil" Text="Cập nhật và đóng lại" Icon="Disk">
                        <Listeners>
                            <Click Handler="return checkInputUtil();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertTemplate">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="True" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCloseUtil" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdUtil.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window runat="server" Title="Chọn từ danh sách template" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdTemplate"
                Modal="true" Constrain="true" AutoHeight="True" LabelWidth="150">
                <Items>
                    <ext:GridPanel runat="server" ID="gpTemplate" Header="true" Title=""
                        AnchorHorizontal="100%" Height="450">
                        <Store>
                            <ext:Store ID="storeTemplate" runat="server">
                                <Proxy>
                                    <ext:HttpProxy Method="POST" Url="~/Services/TimeSheet/HandlerTimeSheetWorkShiftTemplate.ashx" />
                                </Proxy>
                                <AutoLoadParams>
                                    <ext:Parameter Name="start" Value="={0}" />
                                    <ext:Parameter Name="limit" Value="={20}" />
                                </AutoLoadParams>
                                <Reader>
                                    <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                                        <Fields>
                                            <ext:RecordField Name="Id" />
                                            <ext:RecordField Name="Name" />
                                            <ext:RecordField Name="StartInTime" />
                                            <ext:RecordField Name="EndInTime" />
                                            <ext:RecordField Name="StartOutTime" />
                                            <ext:RecordField Name="EndOutTime" />
                                            <ext:RecordField Name="StartDate" />
                                            <ext:RecordField Name="EndDate" />
                                            <ext:RecordField Name="WorkConvert" />
                                            <ext:RecordField Name="TimeConvert" />
                                            <ext:RecordField Name="SymbolCode" />
                                            <ext:RecordField Name="SymbolName" />
                                            <ext:RecordField Name="SymbolColor" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                                <Listeners>
                                    <Load Handler="" />
                                </Listeners>
                            </ext:Store>
                        </Store>
                        <ColumnModel runat="server">
                            <Columns>
                                <ext:RowNumbererColumn Width="30" Header="STT" />
                                <ext:Column ColumnID="Name" Header="Tên template" Width="100" Align="Left" Locked="true" DataIndex="Name" />
                                <ext:DateColumn ColumnID="StartDate" Header="Bắt đầu làm việc" Width="100" DataIndex="StartDate" Format="HH:mm" />
                                <ext:DateColumn ColumnID="StartInTime" Header="Bắt đầu vào" Width="80" DataIndex="StartInTime" Format="HH:mm" />
                                <ext:DateColumn ColumnID="EndInTime" Header="Kết thúc vào" Width="80" DataIndex="EndInTime" Format="HH:mm" />
                                <ext:DateColumn ColumnID="EndDate" Header="Kết thúc làm việc" Width="100" DataIndex="EndDate" Format="HH:mm" />
                                <ext:DateColumn ColumnID="StartOutTime" Header="Bắt đầu ra" Width="80" DataIndex="StartOutTime" Format="HH:mm" />
                                <ext:DateColumn ColumnID="EndOutTime" Header="Kết thúc ra" Width="80" DataIndex="EndOutTime" Format="HH:mm" />
                                <ext:Column ColumnID="WorkConvert" Header="Công quy đổi" Width="90" Align="Left" Locked="true" DataIndex="WorkConvert" />
                                <ext:Column ColumnID="TimeConvert" Header="Thời gian quy đổi" Width="110" Align="Left" Locked="true" DataIndex="TimeConvert" />
                                <ext:Column ColumnID="SymbolCode" Header="Ký hiệu" Width="70" Align="Center" Locked="true" DataIndex="SymbolCode">
                                    <Renderer Fn="RenderSymbol" />
                                </ext:Column>
                                <ext:Column ColumnID="SymbolName" Header="Tên ký hiệu" Width="100" Align="Left" DataIndex="SymbolName" />
                            </Columns>
                        </ColumnModel>
                        <SaveMask ShowMask="true" Msg="Đang tải dữ liệu..." />
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="rowSelectionTemplateModel">
                                <Listeners>
                                    <RowSelect Handler="hdfTemplateId.setValue(rowSelectionTemplateModel.getSelected().get('Id')); " />
                                    <RowDeselect Handler="hdfTemplateId.reset();" />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                PageSize="30" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnAccept" Text="Đồng ý" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="AcceptTemplateClick">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>

