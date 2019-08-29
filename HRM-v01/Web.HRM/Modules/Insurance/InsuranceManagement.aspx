<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceManagement.aspx.cs" Inherits="Web.HRM.Modules.Insurance.InsuranceManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var reloadGrid = function() {
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();
        }
        var handlerKeyPressEnter = function(f, e) {
            if (e.getKey() === e.ENTER) {
                // reload grid
                reloadGrid();
                // show keyword trigger
                if (this.getValue() === '')
                    this.triggers[0].hide();
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        }
        var iconImg = function (name) {
            return "<img src='/Resource/icon/" + name + ".png'>";
        }
        var RenderDate = function (value, p, record) {
            var style = 'align-items: center; ' +
                'display: flex;';
            return "<div style='" + style + "'>" + (value ? iconImg('calendar') + value : '') + "</div>";
        }

        var RenderSex = function(value, p, record) {
            return value === "Nam" ? iconImg('male') : iconImg('female');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfOrder"/>
            <ext:Hidden runat="server" ID="hdfDepartmentSelected"/>

            <ext:Store runat="server" ID="storeInsurance">
                <Proxy>
                    <ext:HttpProxy Method="POST" Url="~/Services/Insurance/HandlerInsuranceManagement.ashx"></ext:HttpProxy>
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}"/>
                    <ext:Parameter Name="limit" Value="={30}"/>
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="keyword" Value="txtKeyword.getValue()" Mode="Raw"/>
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw"/>
                    <ext:Parameter Name="departmentIds" Value="hdfDepartmentSelected.getValue()" Mode="Raw"/>
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id"></ext:RecordField>
                            <ext:RecordField Name="RecordId"></ext:RecordField>
                            <ext:RecordField Name="SexName"></ext:RecordField>
                            <ext:RecordField Name="EmployeeCode"></ext:RecordField>
                            <ext:RecordField Name="FullName"></ext:RecordField>
                            <ext:RecordField Name="PositionId"></ext:RecordField>
                            <ext:RecordField Name="PositionName"></ext:RecordField>
                            <ext:RecordField Name="DepartmentId"></ext:RecordField>
                            <ext:RecordField Name="DepartmentName"></ext:RecordField>
                            <ext:RecordField Name="InsuranceNumber"></ext:RecordField>
                            <ext:RecordField Name="HealthInsuranceNumber"></ext:RecordField>
                            <ext:RecordField Name="InsuranceIssueDate"></ext:RecordField>
                            <ext:RecordField Name="InsuranceIssueDateVn"></ext:RecordField>
                            <ext:RecordField Name="HealthJoinedDate"></ext:RecordField>
                            <ext:RecordField Name="HealthJoinedDateVn"></ext:RecordField>
                            <ext:RecordField Name="HealthExpiredDate"></ext:RecordField>
                            <ext:RecordField Name="HealthExpiredDateVn"></ext:RecordField>
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
            
            <ext:Viewport runat="server" ID="vp">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpInsuranceManagement" StoreID="storeInsurance" Header="False" TrackMouseOver="True" Layout="Fit"
                                           Border="False" AnchorHorizontal="100%" AutoScroll="True">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:ToolbarFill runat="server"/>
                                            <ext:TriggerField runat="server" ID="txtKeyword" Width="220" EnableKeyEvents="True" EmptyText="Nhập họ tên, mã nhân viên, số BHXH, BHYT">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True"/>
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter"></KeyPress>
                                                    <TriggerClick Handler="this.triggers[0].hide();this.clear();reloadGrid()"></TriggerClick>
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center"/>
                                        <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" DataIndex="EmployeeCode"></ext:Column>
                                        <ext:Column ColumnID="FullName" Header="Tên nhân viên" Width="200" DataIndex="FullName"></ext:Column>
                                        <ext:Column ColumnID="SexName" Header="Giới tính" Width="80" DataIndex="SexName" Align="Center">
                                            <Renderer Fn="RenderSex"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="DepartmentName" Header="Phòng ban" Width="200" DataIndex="DepartmentName"></ext:Column>
                                        <ext:Column ColumnID="PositionName" Header="Chức vụ" DataIndex="PositionName"></ext:Column>
                                        <ext:Column ColumnID="InsuranceNumber" Header="Số sổ BHXH" DataIndex="InsuranceNumber"></ext:Column>
                                        <ext:Column ColumnID="InsuranceIssueDateVn" Header="Ngày tham gia BHXH" Width="120" DataIndex="InsuranceIssueDateVn">
                                            <Renderer Fn="RenderDate"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="HealthInsuranceNumber" Header="Số thẻ BHYT" DataIndex="HealthInsuranceNumber"></ext:Column>
                                        <ext:Column ColumnID="HealthJoinedDateVn" Header="Ngày đóng BHYT" Width="120" DataIndex="HealthJoinedDateVn">
                                            <Renderer Fn="RenderDate"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="HealthExpiredDateVn" Header="Ngày hết hạn BHYT" Width="120" DataIndex="HealthExpiredDateVn">
                                            <Renderer Fn="RenderDate"></Renderer>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="30" DisplayInfo="True" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10"/>
                                            <ext:Label runat="server" Text="Số bản ghi trên trang:"></ext:Label>
                                            <ext:ToolbarSpacer runat="server" Width="10"/>
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10"/>
                                                    <ext:ListItem Text="20"/>
                                                    <ext:ListItem Text="30"/>
                                                    <ext:ListItem Text="50"/>
                                                    <ext:ListItem Text="100"/>
                                                </Items>
                                                <SelectedItem Value="30"></SelectedItem>
                                                <Listeners>
                                                    <Select Handler="#{pagingToolbar}.pageSize=parseInt(this.getValue());#{pagingToolbar}.doLoad();"></Select>
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
        </div>
    </form>
</body>
</html>