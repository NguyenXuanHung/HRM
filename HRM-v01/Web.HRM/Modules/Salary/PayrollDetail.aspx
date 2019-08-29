<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollDetail.aspx.cs" Inherits="Web.HRM.Modules.Salary.PayrollDetail" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="uc1" TagName="ResourceCommon" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:ResourceCommon runat="server" id="ResourceCommon" />
    <script src="/Resource/js/Extcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                grpPayrollDetail.reload();
                if (this.getValue() === '') {
                    this.triggers[0].hide();
                }
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        };

        var RenderValue = function(value, p, record) {
            if (value.isNaN) {
                return value;
            }
            if (value.toString().startsWith('#')) {
                return "<span style='color:red'>" + value + "</span>";
            }
            console.log(p)
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        var afterEdit = function(e) {
            /*
            Properties of 'e' include:
                e.grid - This grid
                e.record - The record being edited
                e.field - The field name being edited
                e.value - The value being set
                e.originalValue - The original value for the field, before the edit.
                e.row - The grid row index
                e.column - The grid column index
            */
            Ext.net.DirectMethods.SetEditValues(e.record.data.RecordId, e.field, e.value);
        }

        var ResetWdImportExcelFile = function() {

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager runat="server" ID="RM"></ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfPayrollId"></ext:Hidden>
            <ext:Hidden runat="server" ID="hdfConfigId"></ext:Hidden>
            <ext:Hidden runat="server" ID="hdfEditList"></ext:Hidden>
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server">
                        <Center>
                            <ext:GridPanel runat="server" ID="grpPayrollDetail" TrackMouseOver="True" StripeRows="True" Border="False" Layout="Fit" Icon="Table">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnBack" Icon="ArrowLeft" Text="Quay lại">
                                                <DirectEvents>
                                                    <Click OnEvent="btnBack_Click"></Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server"/>
                                            <ext:Button runat="server" ID="btnLock" Icon="Lock" Text="Khóa bảng lương">
                                                <DirectEvents>
                                                    <Click OnEvent="btnLock_Click">
                                                        <EventMask Msg="Đang xử lý..." ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                                <Listeners>
                                                    <Click Handler="Ext.net.DirectMethods.SaveData(storePayrollDetail.getRecordsValues())"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnUnlock" Icon="LockOpen" Text="Mở bảng lương" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="btnUnlock_Click">
                                                        <EventMask Msg="Đang xử lý..." ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="btnEditByExcel" runat="server" Text="Nhập từ excel" Icon="PageExcel">
                                                <Listeners>
                                                    <Click Handler="#{wdImportExcelFile}.show();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarSeparator runat="server"/>
                                            <ext:Button runat="server" ID="btnAcceptChange" Text="Xác nhận thay đổi" Icon="Accept" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="btnAccept_Click">
                                                        <EventMask Msg="Đang xử lý..." ShowMask="True"></EventMask>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" Text="Hủy bỏ" Icon="Cancel" ID="btnCancelChange" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="btnCancelChange_Click">
                                                        <EventMask ShowMask="true" Msg="Đang xử lý..."/>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarFill runat="server"/>
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="True" Width="200" EmptyText="Nhập tên hoặc mã nhân viên">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True"/>
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter"></KeyPress>
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();grpPayrollDetail.reload();"></TriggerClick>
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="grpPayrollDetail.reload();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store runat="server" ID="storePayrollDetail">
                                        <AutoLoadParams>
                                            <ext:Parameter Name="start" Value="0" Mode="Raw"/>
                                            <ext:Parameter Name="limit" Value="15" Mode="Raw"/>
                                        </AutoLoadParams>
                                        <Proxy>
                                            <ext:HttpProxy Method="GET" Url="~/Services/BaseHandler.ashx"></ext:HttpProxy>
                                        </Proxy>
                                        <BaseParams>
                                            <ext:Parameter Name="handlers" Value="PayrollDetail"/>
                                            <ext:Parameter Name="keyword" Value="txtKeyword.getValue()" Mode="Raw"/>
                                            <ext:Parameter Name="payrollId" Value="hdfPayrollId.getValue()" Mode="Raw"/>
                                        </BaseParams>
                                        <Reader>
                                            <ext:JsonReader IDProperty="ID" Root="Data" TotalProperty="TotalRecords">
                                                <Fields>
                                                    <ext:RecordField Name="Id"></ext:RecordField>
                                                    <ext:RecordField Name="RecordId"></ext:RecordField>
                                                    <ext:RecordField Name="FullName"></ext:RecordField>
                                                    <ext:RecordField Name="EmployeeCode"></ext:RecordField>
                                                    <ext:RecordField Name="PositionName"></ext:RecordField>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <Listeners>
                                    <AfterEdit Fn="afterEdit"></AfterEdit>
                                </Listeners>
                                <View>
                                    <ext:LockingGridView runat="server"/>
                                </View>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="35" Locked="True"/>
                                        <ext:Column Header="Họ Tên" Width="200" Align="Left" DataIndex="FullName" Locked="True"></ext:Column>
                                        <ext:Column Header="Mã nhân viên" Width="100" Align="Left" DataIndex="EmployeeCode" Locked="True"></ext:Column>
                                        <ext:Column Header="Chức vụ" Width="100" Align="Left" DataIndex="PositionName" Locked="True"></ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModel1"></ext:RowSelectionModel>
                                </SelectionModel>
                                <LoadMask ShowMask="True" Msg="Đang xử lý..."></LoadMask>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbar1" PageSize="15" DisplayInfo="True" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu" Hidden="True">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:Label runat="server" Text="Số bản ghi trên trang:" />
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="15" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="30" />
                                                <Listeners>
                                                    <Select Handler="#{PagingToolbar1}.pageSize=parseInt(this.getValue());#{PagingToolbar1}.doLoad();"></Select>
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
            
            <ext:Window runat="server" Title="Nhập dữ liệu từ file excel" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick" ID="wdImportExcelFile"
                Modal="true" Constrain="true" Height="450">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" LabelWidth="150">
                        <Items>
                            <ext:Label runat="server" ID="labelDownload" FieldLabel="Tải tệp tin mẫu">
                            </ext:Label>
                            <ext:Button runat="server" ID="btnDownloadTemplate" Icon="ArrowDown" ToolTip="Tải về" Text="Tải về" Width="100">
                                <DirectEvents>
                                    <Click OnEvent="DownloadTemplate_Click" IsUpload="true" />
                                </DirectEvents>
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Nếu bạn chưa có tệp tin Excel mẫu để nhập liệu. Hãy ấn nút này để tải tệp tin mẫu về máy">
                                    </ext:ToolTip>
                                </ToolTips>
                            </ext:Button>
                            <ext:FileUploadField ID="fileExcel" runat="server" FieldLabel="Tệp tin đính kèm<span style='color:red;'>*</span>"
                                CtCls="requiredData" AnchorHorizontal="98%" Icon="Attach">
                            </ext:FileUploadField>
                            <ext:TextField runat="server" ID="txtSheetName" FieldLabel="Tên sheet Excel" AnchorHorizontal="98%" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnUpdateImportExcel" Text="Cập nhật" Icon="Disk">
                        <DirectEvents>
                            <Click OnEvent="btnUpdateImportExcel_Click">
                                <EventMask ShowMask="true" Msg="Đang lưu dữ liệu. Vui lòng chờ..." />
                                <ExtraParams>
                                    <ext:Parameter Name="Close" Value="False" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>

                    <ext:Button runat="server" ID="btnCloseImport" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdImportExcelFile.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Hide Handler="ResetWdImportExcelFile();" />
                </Listeners>
            </ext:Window>
        </div>
    </form>
</body>
</html>
