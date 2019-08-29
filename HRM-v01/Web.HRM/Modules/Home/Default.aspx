<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.Home.Default" CodeBehind="Default.aspx.cs" %>

<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript" src="../../Resource/js/Chart/highcharts.js"></script>
    <script type="text/javascript" src="../../Resource/js/Chart/modules/exporting.js"></script>
    <script src="../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var enterKeypressRecordHandler = function(f, e) {
            if (e.getKey() === e.ENTER) {
                reloadGridRecord();
                if (txtKeywordRecord.getValue() === '') {
                    txtKeywordRecord.triggers[0].hide();
                }
            }
            if (txtKeywordRecord.getValue() !== '')
                txtKeywordRecord.triggers[0].show();
        };

        var reloadGridRecord = function () {
            rowSelectionModelRecord.clearSelections();
            pagingToolbarRecord.pageIndex = 0;
            pagingToolbarRecord.doLoad();
        };

        var OKSearch = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar2.pageIndex = 0; PagingToolbar2.doLoad();
            }
        }

        var onKeyUp = function (field) {
            var v = this.processValue(this.getRawValue()),
                field;

            if (this.startDateField) {
                field = Ext.getCmp(this.startDateField);
                field.setMaxValue();
                this.dateRangeMax = null;
            } else if (this.endDateField) {
                field = Ext.getCmp(this.endDateField);
                field.setMinValue();
                this.dateRangeMin = null;
            }

            field.validate();
        };

        var RenderHightLight = function (value, p, record) {
            if (value == null || value == "") {
                return "";
            }
            var keyword = document.getElementById("txtSearchKeyRecord").value;
            if (keyword == "" || keyword == "Nhập từ khóa tìm kiếm...")
                return value;

            var rs = "<p>" + value + "</p>";
            var keys = keyword.split(" ");
            for (i = 0; i < keys.length; i++) {
                if ($.trim(keys[i]) != "") {
                    var o = { words: keys[i] };
                    rs = highlight(o, rs);
                }
            }
            return rs;
        }

        function highlight(options, content) {
            var o = {
                words: '',
                caseSensitive: false,
                wordsOnly: true,
                template: '$1<span class="highlight">$2</span>$3'
            }, pattern;
            $.extend(true, o, options || {});

            if (o.words.length == 0) { return; }
            pattern = new RegExp('(>[^<.]*)(' + o.words + ')([^<.]*)', o.caseSensitive ? "" : "ig");

            return content.replace(pattern, o.template);
        }


        var SetDefaultYear = function () {
            var CurYear = new Date().getFullYear();
            var item = cbxChonNam.Items.IndexOf(CurYear);
            if (item == -1) {
                cbxChonNam.SelectedIndex = -1;
            }
            else {
                cbxChonNam.SelectedIndex = item;
            }
            alert(item);
        }

        var BienDongNhanSu = function (userID) {
            var id = cbxChonNam.getValue();
            document.getElementById('iframeChart').src = 'chart/LineChart.aspx?type=BDNhanSu&year=' + id + '&userID=' + userID;
            hdfChartUrl.setValue('chart/LineChart.aspx?type=BDNhanSu&year=' + id + '&userID=' + userID);
            cbxChonNam.show();
            tbsChonNam.show();
            btnFullScreen.setDisabled(false);
        }

        var createWindow = function (id) {
            new parent.Ext.Window({
                id: 'Window1',
                renderTo: parent.Ext.getBody(),
                hidden: false,
                maximized: true,
                layout: 'fit',
                content: id
            });
            new parent.Ext.Window({
                id: 'Window2',
                renderTo: parent.Ext.getBody(),
                hidden: false,
                maximized: true,
                layout: 'fit',
                content: id
            });
        }

        var getRecordIdList = function () {
            var jsonDataEncode = "";
            var records = gpRecord.getSelectionModel().getSelections();
            for (var i = 0; i < records.length; i++) {
                jsonDataEncode += records[i].data.Id + ",";
            }
            return jsonDataEncode;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="RM" runat="server" />
        <!-- hidden field -->
        <ext:Hidden runat="server" ID="hdfMaBoPhan" />
        <ext:Hidden runat="server" ID="hdfAllNodeID" />
        <ext:Hidden runat="server" ID="hdfCurrentUserID" />
        <ext:Hidden runat="server" ID="hdfNangNgachCCVC" />
        <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfSalaryRaiseRegularType" />
        <ext:Hidden runat="server" ID="hdfSalaryRaiseOutFrameType" />

        <ext:Hidden runat="server" ID="hdfDepartmentIds" />
        <ext:Hidden runat="server" ID="hdfRecordFilter" />
        <ext:Hidden runat="server" ID="hdfRecordId" />    

        <!-- store -->
        <ext:Store ID="storeRecord" runat="server" AutoSave="True" GroupField="DepartmentName">
            <Proxy>
                <ext:HttpProxy Json="true" Method="GET" Url="~/Services/HumanRecord/HandlerRecord.ashx" />
            </Proxy>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="={0}" />
                <ext:Parameter Name="limit" Value="={10}" />
            </AutoLoadParams>
            <BaseParams>
                <ext:Parameter Name="query" Value="#{txtKeywordRecord}.getValue()" Mode="Raw" />
                <ext:Parameter Name="filter" Value="#{hdfRecordFilter}.getValue()" Mode="Raw" />
                <ext:Parameter Name="departmentIds" Value="#{hdfDepartmentIds}.getValue()" Mode="Raw" />
            </BaseParams>
            <Reader>
                <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                    <Fields>
                        <ext:RecordField Name="Id" />
                        <ext:RecordField Name="EmployeeCode" />
                        <ext:RecordField Name="FullName" />
                        <ext:RecordField Name="SexName" />
                        <ext:RecordField Name="BirthDateVn" />
                        <ext:RecordField Name="CellPhoneNumber" />
                        <ext:RecordField Name="WorkStatusName" />
                        <ext:RecordField Name="Address" />
                        <ext:RecordField Name="EducationName" />
                        <ext:RecordField Name="ParticipationDateVn" />
                        <ext:RecordField Name="ContractTypeName" />
                        <ext:RecordField Name="IDNumber" />
                        <ext:RecordField Name="RecruimentDateVn" />
                        <ext:RecordField Name="PersonalEmail" />
                        <ext:RecordField Name="DepartmentId" />
                        <ext:RecordField Name="DepartmentName" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Store runat="server" ID="storeRecordStatus" AutoLoad="false" OnRefreshData="storeRecordStatus_OnRefreshData">
            <Reader>
                <ext:JsonReader IDProperty="Id">
                    <Fields>
                        <ext:RecordField Name="Id" Mapping="Key" />
                        <ext:RecordField Name="Name" Mapping="Value" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Window ID="wdMoRong" Layout="FormLayout" Icon="ChartBar" runat="server" Modal="true" Constrain="true" Title="Biểu đồ mở rộng" Maximized="true" Hidden="true">
            <Buttons>
                <ext:Button runat="server" Text="Đóng" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdMoRong}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        
        <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wdShowReport" Title="Báo cáo hồ sơ cán bộ" Maximized="true" Icon="Printer">
            <Items>
                <ext:Hidden runat="server" ID="hdfEmployeeId" />
                <ext:TabPanel ID="pnReportPanel" Region="Center" AnchorVertical="100%" Border="false"
                    runat="server">
                </ext:TabPanel>
            </Items>
            <Listeners>
                <BeforeShow Handler="#{pnReportPanel}.remove(0);addHomePage(#{pnReportPanel},'Homepage','../Report/ReportEmployeeDetail.aspx?rp=CurriculumVitae&departments='+ #{hdfDepartments}.getValue() +'&recordIds='+ getRecordIdList(), 'Báo cáo hồ sơ cán bộ')" />
            </Listeners>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Đóng" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdShowReport}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="Window1" Title="Báo cáo danh sách cán bộ sinh nhật trong tháng" Maximized="true" Icon="Printer">
            <Items>
                <ext:TabPanel ID="TabPanel1" Region="Center" AnchorVertical="100%" Border="false"
                    runat="server">
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button4" runat="server" Text="Đóng" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{Window1}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    
        <!-- viewport -->
        <ext:Viewport runat="server">
            <Items>
                <ext:ColumnLayout runat="server" Split="false" FitHeight="true">
                    <Columns>
                        <ext:LayoutColumn ColumnWidth="0.6">
                            <ext:Panel runat="server" Header="false" Border="false">
                                <Items>
                                    <ext:RowLayout runat="server" Split="false">
                                        <Rows>
                                            <ext:LayoutRow RowHeight="0.55">
                                                <ext:Panel ID="pnlChart" runat="server" Visible="false" Icon="ChartBar" Layout="FitLayout" Border="false" Title="Biểu đồ">
                                                    <TopBar>
                                                        <ext:Toolbar ID="Toolbar1" Visible="false" runat="server">
                                                            <Items>
                                                                <ext:Button ID="btnChonLoaiBieuDo" Visible="false" runat="server" Icon="ChartBar" Text="Chọn loại biểu đồ">
                                                                    <Menu>
                                                                        <ext:Menu ID="Menu1" runat="server">
                                                                            <Items>
                                                                                <ext:MenuItem ID="mnuChartBySex" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=Gender&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=Gender&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuChartByDegree" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=Level&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=Level&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuMatrimony" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=TTHonNhan&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=TTHonNhan&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuReligion" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=TonGiao&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=TonGiao&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuFolk" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=DanToc&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=DanToc&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuContractType" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=LoaiHD&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=LoaiHD&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuTitle" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=NhanSuTheoChucVuDoan&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=NhanSuTheoChucVuDoan&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuPartyLevel" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=ThongKeChucVuDang&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=ThongKeChucVuDang&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuArmyLevel" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=CapBacQuanDoi&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=CapBacQuanDoi&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuSeniority" Visible="false" runat="server" Icon="ChartPie">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=ThongKeTheoThamNienCT&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=ThongKeTheoThamNienCT&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>

                                                                                <ext:MenuSeparator runat="server" ID="sp" />
                                                                                <ext:MenuItem ID="mnuVolatility" Visible="false" runat="server" Icon="ChartLine">
                                                                                    <Listeners>
                                                                                        <Click Handler="BienDongNhanSu(hdfCurrentUserID.getValue());" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuAge" Visible="false" runat="server" Icon="ChartBar">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/ColumnChart.aspx?type=Age&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/ColumnChart.aspx?type=Age&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuWorkUnit" Visible="false" runat="server" Icon="ChartBar">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/ColumnChart.aspx?type=NSDonVi&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/ColumnChart.aspx?type=NSDonVi&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                            </Items>
                                                                        </ext:Menu>
                                                                    </Menu>
                                                                </ext:Button>
                                                                <ext:Button runat="server" ID="btnSetChartDefault" Visible="false" Text="Đặt làm biểu đồ mặc định"
                                                                    Icon="Tick">
                                                                    <DirectEvents>
                                                                        <Click OnEvent="btnSetChartDefault_Click">
                                                                            <Confirmation Title="Thông báo" Message="Bạn có chắc chắn muốn đặt biểu đồ này làm mặc định?"
                                                                                ConfirmRequest="true" />
                                                                        </Click>
                                                                    </DirectEvents>
                                                                </ext:Button>
                                                                <ext:ToolbarSeparator ID="tbsChonNam" Hidden="true" />
                                                                <ext:ComboBox runat="server" SelectedIndex="0" ValueField="ID" DisplayField="Title"
                                                                    AnchorHorizontal="100%" FieldLabel="Chọn năm" LabelWidth="60"
                                                                    Width="120" ID="cbxChonNam" Hidden="true" Editable="false">
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="cbxChonNamStore" AutoLoad="true" OnRefreshData="cbxChonNamStore_OnRefreshData">
                                                                            <Reader>
                                                                                <ext:JsonReader IDProperty="ID">
                                                                                    <Fields>
                                                                                        <ext:RecordField Name="ID" />
                                                                                        <ext:RecordField Name="Title" />
                                                                                    </Fields>
                                                                                </ext:JsonReader>
                                                                            </Reader>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Listeners>
                                                                        <Select Handler="BienDongNhanSu(hdfCurrentUserID.getValue());"></Select>
                                                                    </Listeners>
                                                                </ext:ComboBox>

                                                                <ext:Button runat="server" ID="btnFullScreen" Disabled="false" Icon="ArrowOut" Text="Mở rộng">
                                                                    <Listeners>
                                                                        <Click Handler="if (hdfChartUrl.getValue() != '') Ext.net.DirectMethods.showWindowMoRong();" />
                                                                    </Listeners>
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip3" runat="server" Frame="true" Title="Hướng dẫn>"
                                                                            Html="Bấm vào đây để xem toàn màn hình">
                                                                        </ext:ToolTip>
                                                                    </ToolTips>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <Content>
                                                        <ext:Hidden runat="server" ID="hdfChartUrl" />
                                                        <ext:Label ID="lblIframe" Visible="false" runat="server" />
                                                    </Content>
                                                </ext:Panel>
                                            </ext:LayoutRow>
                                            <ext:LayoutRow RowHeight="0.45">
                                                <ext:Panel ID="pnlQuickSearch" runat="server" Visible="false" Layout="BorderLayout" Border="false" Icon="Zoom" Title="Tra cứu nhanh thông tin CNV">
                                                    <TopBar>
                                                        <ext:Toolbar runat="server" Visible="false" ID="tbar">
                                                            <Items>
                                                                <ext:Button runat="server" ID="btnPrintRecord" Text=" In lý lịch (mẫu 2C)" Icon="Printer" Hidden="True" Disabled="true" >
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Title="Hướng dẫn" Html="Chọn một cán bộ để in ra thông tin cá nhân" />
                                                                    </ToolTips>
                                                                    <Listeners>
                                                                        <Click Handler="if(hdfRecordId.getValue()=='') {alert('Chưa chọn cán bộ');} else{#{wdShowReport}.show();}" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:ToolbarFill runat="server" />
                                                                <ext:ComboBox ID="cboRecordStatus" StoreID="storeRecordStatus" FieldLabel="Trạng thái" Editable="false" runat="server"
                                                                    DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                                                                    AnchorHorizontal="98%" LabelWidth="70" Width="200">
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
                                                                        <Expand Handler="if(cboRecordStatus.store.getCount()==0) storeRecordStatus.reload();" />
                                                                        <Select Handler="this.triggers[0].show();hdfRecordFilter.setValue(' AND rc.[Status]=' + cboRecordStatus.getValue());pagingToolbarRecord.pageIndex=0;pagingToolbarRecord.doLoad();"></Select>
                                                                        <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();hdfRecordFilter.reset();pagingToolbarRecord.pageIndex=0;pagingToolbarRecord.doLoad();}" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:ToolbarSpacer runat="server" Width="5" />
                                                                <ext:TriggerField runat="server" EnableKeyEvents="true" ID="txtKeywordRecord" Width="150" EmptyText="Nhập từ khóa">
                                                                    <ToolTips>
                                                                        <ext:ToolTip Closable="true" Draggable="true" AutoHide="false" runat="server"
                                                                            Title="Nhập từ khóa" Header="true" Frame="true" Html="Bạn có thể tìm kiếm theo mã cán bộ, họ tên, số điện thoại, số CMT hoặc địa chỉ email">
                                                                        </ext:ToolTip>
                                                                    </ToolTips>
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <KeyPress Fn="enterKeyPressHandler" />
                                                                        <TriggerClick Handler="if (index == 0) {this.reset();this.triggers[0].hide();pagingToolbarRecord.pageIndex=0;pagingToolbarRecord.doLoad();}" />
                                                                    </Listeners>
                                                                </ext:TriggerField>
                                                                <ext:Button ID="btnSearchRecord" runat="server" Icon="Zoom" Text="Tìm kiếm">
                                                                    <Listeners>
                                                                        <Click Handler="pagingToolbarRecord.pageIndex=0;pagingToolbarRecord.doLoad();storeRecord.reload();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:ToolbarSpacer runat="server" Width="15" />
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <Items>
                                                        <ext:GridPanel runat="server" ID="gpRecord" StoreID="storeRecord" Header="false" Border="false" StripeRows="true" TrackMouseOver="true" 
                                                                       Region="Center" AnchorHorizontal="100%" AnchorVertical="100%" AutoExpandColumn="FullName">
                                                            <ColumnModel runat="server">
                                                                <Columns>
                                                                    <ext:RowNumbererColumn Header="STT" Width="35" />
                                                                    <ext:Column ColumnID="EmployeeCode" Header="Mã nhân viên" Width="80" DataIndex="EmployeeCode" />
                                                                    <ext:Column ColumnID="FullName" Header="Họ tên" Width="150" DataIndex="FullName" />
                                                                    <ext:Column ColumnID="SexName" Header="Giới tính" Width="80" DataIndex="SexName" Align="Center" />
                                                                    <ext:Column ColumnID="BirthDateVn" Header="Ngày sinh" Width="100" DataIndex="BirthDateVn" Align="Center" />
                                                                    <ext:GroupingSummaryColumn ColumnID="DepartmentName" Header="Phòng ban" Width="85" DataIndex="DepartmentName" />
                                                                    <ext:Column ColumnID="CellPhoneNumber" Header="Điện thoại" Width="100" DataIndex="CellPhoneNumber" /> 
                                                                    <ext:Column ColumnID="WorkStatusName" Header="Trạng thái" Width="100" DataIndex="WorkStatusName" />
                                                                </Columns>
                                                            </ColumnModel>
                                                            <Plugins>
                                                                <ext:RowExpander runat="server" ID="rx">
                                                                    <Template runat="server">
                                                                        <Html>
                                                                            <table border="0" cellpadding="5" cellspacing="15">
                                                                            <tr>
                                                                                <td><b>Địa chỉ: </b></td>
                                                                                <td colspan="2">{Address}</td> 
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Số CMT: </b></td>
                                                                                <td>{IDNumber}</td>
                                                                                <td><b>Email: </b></td>
                                                                                <td>{PersonalEmail}</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Trình độ: </b></td>
                                                                                <td>{EducationName}</td>
                                                                                <td><b>Loại hợp đồng: </b></td>
                                                                                <td>{ContractTypeName}</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Ngày tuyển dụng: </b></td>
                                                                                <td>{RecruimentDateVn}</td>
                                                                                <td><b>Ngày vào đơn vị: </b></td>
                                                                                <td>{ParticipationDateVn}</td>
                                                                            </tr>
                                                                        </table>
                                                                        </Html>
                                                                    </Template>
                                                                </ext:RowExpander>
                                                            </Plugins>
                                                            <SelectionModel>
                                                                <ext:RowSelectionModel runat="server" ID="rowSelectionModelRecord" SingleSelect="True">
                                                                    <Listeners>
                                                                        <RowSelect Handler="hdfRecordId.setValue(rowSelectionModelRecord.getSelected().get('Id'));btnPrintRecord.enable();" />
                                                                        <RowDeselect Handler="hdfRecordId.reset();btnPrintRecord.disable();" />
                                                                    </Listeners>
                                                                </ext:RowSelectionModel>
                                                            </SelectionModel>
                                                            <LoadMask ShowMask="true" Msg="Đang tải..." />
                                                            <BottomBar>
                                                                <ext:PagingToolbar ID="pagingToolbarRecord" runat="server" PageSize="10" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                                                    <Items>
                                                                        <ext:ToolbarSpacer runat="server" Width="10" />
                                                                        <ext:Label runat="server" Text="Số bản ghi trên trang:" />
                                                                        <ext:ToolbarSpacer runat="server" Width="10" />
                                                                        <ext:ComboBox runat="server" Width="80">
                                                                            <Items>
                                                                                <ext:ListItem Text="10" />
                                                                                <ext:ListItem Text="20" />
                                                                                <ext:ListItem Text="30" />
                                                                                <ext:ListItem Text="50" />
                                                                                <ext:ListItem Text="100" />
                                                                            </Items>
                                                                            <SelectedItem Value="10" />
                                                                            <Listeners>
                                                                                <Select Handler="#{pagingToolbarRecord}.pageSize = parseInt(this.getValue()); #{pagingToolbarRecord}.doLoad();"></Select>
                                                                            </Listeners>
                                                                        </ext:ComboBox>
                                                                    </Items>
                                                                    <Listeners>
                                                                        <Change Handler="rowSelectionModelRecord.clearSelections();hdfRecordId.reset();btnPrintRecord.disable();" />
                                                                    </Listeners>
                                                                </ext:PagingToolbar>
                                                            </BottomBar>
                                                            <View>
                                                                <ext:GroupingView ID="groupView" runat="server" ForceFit="true" MarkDirty="false" ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                                            </View>
                                                        </ext:GridPanel>
                                                    </Items>
                                                </ext:Panel>
                                            </ext:LayoutRow>
                                        </Rows>
                                    </ext:RowLayout>
                                </Items>
                            </ext:Panel>
                        </ext:LayoutColumn>
                        <ext:LayoutColumn ColumnWidth="0.4">
                            <ext:Panel runat="server" Border="false" Layout="AccordionLayout" Icon="Build" Title="Tiện ích">
                                <Items>
                                    <ext:Panel ID="pnlSinhNhatNhanVien" Visible="false" Layout="BorderLayout" runat="server" Icon="Cake" Title="Thông báo sinh nhật">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfIsReload" />
                                            <ext:GridPanel ID="grp_sinhnhatnhanvien" Border="false" TrackMouseOver="true" Region="Center"
                                                AutoExpandColumn="DepartmentName" AutoExpandMin="150" runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" Visible="false" ID="tbReportDanhSach">
                                                        <Items>
                                                            <ext:Button runat="server" Text="In danh sách" Icon="Printer">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ShowBirthDayReport">
                                                                        <EventMask ShowMask="true" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" Text="Gửi Email chúc mừng" ID="btn_SentEmail_SinhNhat"
                                                                Icon="Email">
                                                                <Menu>
                                                                    <ext:Menu runat="server">
                                                                        <Items>
                                                                            <ext:MenuItem runat="server" Text="CBCCVC đã chọn" Disabled="true" ID="btn_SentEmail_HappyBirthDay"
                                                                                Icon="Email">
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="SendEmailHappyBirthday">
                                                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                                        <%--<ExtraParams>
                                                                                <ext:Parameter Name="Ma_CB" Mode="Raw" Value="#{RowSelectionModel3}.getSelected().data.MA_CB" />
                                                                            </ExtraParams>--%>
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:MenuItem>
                                                                            <ext:MenuItem runat="server" Text="Tất cả CBCCVC" Disabled="false" ID="btn_SentEmail_HappyBirthDay_All"
                                                                                Icon="Email">
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="SendEmailHappyBirthday">
                                                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                                                        <ExtraParams>
                                                                                            <ext:Parameter Name="All" Value="True" Mode="Value" />
                                                                                        </ExtraParams>
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:MenuItem>
                                                                        </Items>
                                                                    </ext:Menu>
                                                                </Menu>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="Store3" AutoLoad="true" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <Reader>
                                                            <ext:JsonReader TotalProperty="TotalRecords" Root="Data" IDProperty="EmployeeCode">
                                                                <Fields>
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="FullName" />
                                                                    <ext:RecordField Name="SexName" />
                                                                    <ext:RecordField Name="BirthDate" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                    <ext:RecordField Name="ParentDepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        <AutoLoadParams>
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={20}" />
                                                        </AutoLoadParams>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="BirthdayOfEmployee" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="userID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column Header="Mã nhân viên" Width="70" DataIndex="EmployeeCode" />
                                                        <ext:Column Header="Họ tên" Width="130" DataIndex="FullName" />
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày sinh" Width="80" DataIndex="BirthDate" />
                                                        <ext:Column Header="Giới tính" Width="55" DataIndex="SexName">
                                                        </ext:Column>
                                                        <ext:Column Header="Phòng ban" Width="200" DataIndex="DepartmentName" />
                                                        <ext:Column Header="Đơn vị" Width="160" DataIndex="ParentDepartmentName" />
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel3" runat="server">
                                                        <Listeners>
                                                            <RowSelect Handler="btn_SentEmail_HappyBirthDay.enable();" />
                                                            <RowDeselect Handler="btn_SentEmail_HappyBirthDay.disable();" />
                                                        </Listeners>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải" />
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar3" runat="server" PageSize="20">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(hdfIsReload.getValue()==''){
                                                               #{Store3}.reload();
                                                               hdfIsReload.setValue('True');
                                                           }" />
                                        </Listeners>
                                    </ext:Panel>
                                    <ext:Panel ID="pnEmployeeExpried" Layout="BorderLayout" Visible="false" runat="server" Icon="Page">
                                        <Items>
                                            <ext:GridPanel ID="GridPanel3" Border="false" Region="Center" TrackMouseOver="true" runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" Visible="false" ID="Toolbar2">
                                                        <Items>
                                                            <ext:Button ID="Button8" runat="server" Text="In danh sách" Icon="Printer">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ShowEndContractReport">
                                                                        <EventMask ShowMask="true" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="Store5" AutoLoad="false" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <Reader>
                                                            <ext:JsonReader TotalProperty="TotalRecords" Root="Data" IDProperty="EmployeeCode">
                                                                <Fields>
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="FullName" />
                                                                    <ext:RecordField Name="ContractEndDate" />
                                                                    <ext:RecordField Name="ContractTypeName" />
                                                                    <ext:RecordField Name="ContractDate" />
                                                                    <ext:RecordField Name="SexName" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="EndContract" />
                                                            <ext:Parameter Name="userID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={30}" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Locked="true" Width="35" />
                                                        <ext:Column Header="Mã nhân viên" Locked="true" Width="70" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Locked="true" Width="130" DataIndex="FullName">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày ký"
                                                            Width="100" DataIndex="ContractDate">
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày hết hạn"
                                                            Width="100" DataIndex="ContractEndDate">
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Giới tính" Width="75" DataIndex="SexName">
                                                            <Renderer Fn="GetGender" />
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel4" runat="server" />
                                                </SelectionModel>
                                                <Plugins>
                                                    <ext:RowExpander ID="RowExpander1" runat="server">
                                                        <Template runat="server">
                                                            <Html>
                                                                <br />
                                                                <p><b>Loại hợp đồng:</b> {ContractTypeName}</p>
                                                                <br />
                                                                <p><b>Bộ phận:</b> {DepartmentName}</p>
                                                                <br />
                                                            </Html>
                                                        </Template>
                                                    </ext:RowExpander>
                                                </Plugins>
                                                <LoadMask ShowMask="true" Msg="Đang tải" />
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="30">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                            <ext:Hidden runat="server" ID="hdfIsLoadDS" />
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(hdfIsLoadDS.getValue()==''){Store5.reload();hdfIsLoadDS.setValue('True');}" />
                                        </Listeners>
                                    </ext:Panel>
                                    <ext:Panel ID="pnlHoSoNhanSuCanDuyet" Layout="BorderLayout" Visible="false" runat="server" Icon="UserTick" Title="Hồ sơ nhân sự cần duyệt">
                                        <Items>
                                            <ext:GridPanel ID="grpHoSoNhanSuCanDuyet" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" Visible="false" ID="Toolbar3">
                                                        <Items>
                                                            <ext:Button ID="Button10" runat="server" Text="In danh sách" Icon="Printer">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ShowEndContractReport">
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="grpHoSoNhanSuCanDuyetStore" AutoLoad="false" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="../ProfileHuman/DuyetHoSo/Handler.ashx" />
                                                        </Proxy>
                                                        <Reader>
                                                            <ext:JsonReader TotalProperty="TotalRecords" Root="Data" IDProperty="MaCB">
                                                                <Fields>
                                                                    <ext:RecordField Name="BoPhan" />
                                                                    <ext:RecordField Name="HoTen" />
                                                                    <ext:RecordField Name="MaCB" />
                                                                    <ext:RecordField Name="CapNhatLanCuoi" />
                                                                    <ext:RecordField Name="TaiKhoanDangNhap" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        <BaseParams>
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={30}" />
                                                            <ext:Parameter Name="filter" Value="ChuaDuyet" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Locked="true" Width="35" />
                                                        <ext:Column Header="Mã cán bộ" Locked="true" Width="70" DataIndex="MaCB">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Locked="true" Width="130" DataIndex="HoTen">
                                                        </ext:Column>
                                                        <ext:Column Header="Bộ phận" Locked="true" Width="130" DataIndex="BoPhan">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Cập nhật lần cuối" Width="100" DataIndex="CapNhatLanCuoi">
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Tài khoản" Width="75" DataIndex="TaiKhoanDangNhap">
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel5" runat="server" />
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar5" runat="server" PageSize="30">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                            <ext:Hidden runat="server" ID="Hidden1" />
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(Hidden1.getValue()==''){grpHoSoNhanSuCanDuyetStore.reload();Hidden1.setValue('True');}" />
                                        </Listeners>
                                    </ext:Panel>
                                    <ext:Panel runat="server" ID="pnlEmployeeRetired" Layout="BorderLayout" Icon="User" Title="Cán bộ đến kỳ nghỉ hưu" Visible="False">
                                        <TopBar>
                                            <ext:Toolbar runat="server">
                                                <Items>
                                                    <ext:Button runat="server" Text="In danh sách" Icon="Printer">
                                                        <DirectEvents>
                                                            <Click OnEvent="ShowRetirementReport">
                                                                <EventMask ShowMask="true" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:ToolbarSpacer Width="5" />
                                                    <ext:ToolbarSeparator />
                                                    <ext:ToolbarFill />
                                                    <ext:DateField runat="server" ID="dfNgaySinh" AnchorHorizontal="95%" TabIndex="1" Editable="true" EnableKeyEvents="true" Format="d/M/yyyy" 
                                                                   MaskRe="/[0-9\/]/" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                    </ext:DateField>
                                                    <ext:Button runat="server" ID="btnSearch" Text="Lọc" Icon="Zoom">
                                                        <Listeners>
                                                            <Click Handler="#{PagingToolbar2}.pageIndex = 0; #{PagingToolbar2}.doLoad(); RowSelectionModel2.clearSelections();" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:GridPanel runat="server" ID="grpHoSoCBDenKyNghiHuu" Border="false" Region="Center" TrackMouseOver="true" StripeRows="true" AnchorHorizontal="100%">
                                                <Store>
                                                    <ext:Store ID="grpHoSoCBDenKyNghiHuuStore" AutoLoad="false" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <Reader>
                                                            <ext:JsonReader TotalProperty="TotalRecords" Root="Data" IDProperty="EmployeeCode">
                                                                <Fields>
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="FullName" />
                                                                    <ext:RecordField Name="SexName" />
                                                                    <ext:RecordField Name="BirthDate" />
                                                                    <ext:RecordField Name="Age" />
                                                                    <ext:RecordField Name="RecruimentDate" />
                                                                    <ext:RecordField Name="Seniority" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                    <ext:RecordField Name="ParentDepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="RetirementOfEmployee" />
                                                            <ext:Parameter Name="dfNgaySinh" Value="#{dfNgaySinh}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="userID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={30}" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Locked="true" Align="Left" Width="35" />
                                                        <ext:Column Header="Mã cán bộ" Locked="true" Width="70" Align="Left" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Locked="true" Width="130" DataIndex="FullName">
                                                        </ext:Column>
                                                        <ext:Column Header="Giới tính" Width="70" Align="Left" DataIndex="SexName">
                                                            <Renderer Fn="RenderGender2" />
                                                        </ext:Column>
                                                        <ext:DateColumn Header="Năm sinh" Format="dd/MM/yyyy" Width="70" Align="Left" DataIndex="BirthDate">
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Tuổi" Width="70" DataIndex="Age" Align="Left">
                                                        </ext:Column>
                                                        <%--<ext:DateColumn Format="dd/MM/yyyy" Header="Ngày tuyển dụng" Width="100" DataIndex="NGAY_TUYEN_DTIEN">
                                                    </ext:DateColumn>--%>
                                                        <%--<ext:DateColumn Format="dd/MM/yyyy" Header="Ngày tuyển chính thức" Width="100" DataIndex="NGAY_TUYEN_CHINHTHUC">
                                                    </ext:DateColumn>--%>
                                                        <ext:Column Header="Thâm niên" Width="190" DataIndex="Seniority">
                                                        </ext:Column>
                                                        <ext:Column Header="Phòng ban" Width="250" DataIndex="DepartmentName">
                                                        </ext:Column>
                                                        <ext:Column Header="Đơn vị quản lý" Width="200" DataIndex="ParentDepartmentName">
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" />
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="30">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                            <ext:Hidden runat="server" ID="Hidden2" />
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(Hidden2.getValue()==''){grpHoSoCBDenKyNghiHuuStore.reload();Hidden2.setValue('True');}" />
                                        </Listeners>
                                    </ext:Panel>
                                    <ext:Panel ID="pnlNextRaise" Layout="BorderLayout" runat="server" Icon="User" Title="Cán bộ đến kỳ nâng lương">
                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar4" runat="server">
                                                <Items>
                                                    <ext:Button ID="btnInNangLuong" runat="server" Text="In danh sách" Icon="Printer">
                                                        <DirectEvents>
                                                            <Click OnEvent="ShowRiseSalaryReport">
                                                                <EventMask ShowMask="true" />
                                                            </Click>
                                                        </DirectEvents>
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
                                                    <ext:Button runat="server" ID="Button6" Text="Lọc" Icon="Zoom">
                                                        <Listeners>
                                                            <Click Handler="RowSelectionModel6.clearSelections(); storeNextRaise.reload()" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:GridPanel ID="grdNangLuongCBCCVC" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <Store>
                                                    <ext:Store ID="storeNextRaise" AutoLoad="false" runat="server" OnRefreshData="storeNextRaise_OnRefreshData">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" />
                                                                    <ext:RecordField Name="ContractId" />
                                                                    <ext:RecordField Name="Name" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="EffectiveDate" />
                                                                    <ext:RecordField Name="SignerName" />
                                                                    <ext:RecordField Name="SignerPosition" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="GroupQuantumId" />
                                                                    <ext:RecordField Name="QuantumId" />
                                                                    <ext:RecordField Name="Grade" />
                                                                    <ext:RecordField Name="Factor" />
                                                                    <ext:RecordField Name="InsuranceSalary" />
                                                                    <ext:RecordField Name="ContractSalary" />
                                                                    <ext:RecordField Name="GrossSalary" />
                                                                    <ext:RecordField Name="NetSalary" />
                                                                    <ext:RecordField Name="PercentageSalary" />
                                                                    <ext:RecordField Name="PercentageOverGrade" />
                                                                    <ext:RecordField Name="Allowance" />
                                                                    <ext:RecordField Name="NextRaiseDate" />
                                                                    <ext:RecordField Name="Type" />
                                                                    <ext:RecordField Name="Status" />
                                                                    <ext:RecordField Name="StatusName" />
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="EmployeeName" />
                                                                    <ext:RecordField Name="EmployeeSex" />
                                                                    <ext:RecordField Name="EmpoyeeBirthDate" />
                                                                    <ext:RecordField Name="ContractTypeName" />
                                                                    <ext:RecordField Name="GroupQuantumName" />
                                                                    <ext:RecordField Name="QuantumName" />
                                                                    <ext:RecordField Name="TypeName" />
                                                                    <ext:RecordField Name="EmpoyeeBirthDate" />
                                                                    <ext:RecordField Name="BasicSalary" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column Header="Mã cán bộ" Width="70" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Width="100" DataIndex="EmployeeName">
                                                        </ext:Column>
                                                        <ext:Column Header="Phòng ban" Width="80" DataIndex="DepartmentName">
                                                        </ext:Column>
                                                        <ext:Column Header="Đơn vị quản lý" Width="100" DataIndex="ParentDepartmentName">
                                                        </ext:Column>
                                                        <ext:Column Header="Ngạch" Width="60" DataIndex="QuantumName">
                                                        </ext:Column>
                                                        <ext:Column Header="Bậc" Width="40" DataIndex="Grade">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày quyết định" Width="100" DataIndex="DecisionDate">
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày nâng lương" Width="100" DataIndex="EffectiveDate">
                                                        </ext:DateColumn>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" />
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
                                            </ext:GridPanel>
                                            <ext:Hidden runat="server" ID="hdfNangLuongCCVC" />
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(hdfNangLuongCCVC.getValue()==''){storeNextRaise.reload();hdfNangLuongCCVC.setValue('True');}" />
                                        </Listeners>
                                    </ext:Panel>
                                    <ext:Panel ID="pnlOverGrade" Layout="BorderLayout" runat="server" Icon="User" Title="Cán bộ đến kỳ xét vượt khung" Hidden="True">
                                        <TopBar>
                                            <ext:Toolbar runat="server">
                                                <Items>
                                                    <ext:Button ID="btnPrinterOutFrame" runat="server" Text="In danh sách" Icon="Printer">
                                                        <DirectEvents>
                                                            <Click OnEvent="ShowOutFrameSalaryReport">
                                                                <EventMask ShowMask="true" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:ToolbarSpacer Width="5" />
                                                    <ext:ToolbarSeparator />
                                                    <ext:ToolbarFill />
                                                    <ext:Label ID="lblFromDate" runat="server" Text="Từ ngày: "></ext:Label>
                                                    <ext:DateField runat="server" ID="dateFieldFromDate" AnchorHorizontal="95%" TabIndex="1" Editable="true" EnableKeyEvents="true" Format="d/M/yyyy" 
                                                                   MaskRe="/[0-9\/]/" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                    </ext:DateField>
                                                    <ext:ToolbarSpacer Width="5" />
                                                    <ext:Label ID="lblToDate" runat="server" Text="Đến ngày: "></ext:Label>
                                                    <ext:DateField runat="server" ID="dateFieldToDate" AnchorHorizontal="95%" TabIndex="1" Editable="true" EnableKeyEvents="true" Format="d/M/yyyy" 
                                                                   MaskRe="/[0-9\/]/" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/" RegexText="Định dạng ngày không đúng">
                                                    </ext:DateField>
                                                    <ext:Button runat="server" ID="Button3" Text="Lọc" Icon="Zoom">
                                                        <Listeners>
                                                            <Click Handler="RowSelectionModel6.clearSelections(); storeOutFrameSalary.reload()" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:GridPanel ID="gridOutFrameSalary" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <Store>
                                                    <ext:Store ID="storeOutFrameSalary" AutoLoad="false" runat="server" OnRefreshData="storeOutFrameSalary_OnRefreshData">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="RecordId" />
                                                                    <ext:RecordField Name="ContractId" />
                                                                    <ext:RecordField Name="Name" />
                                                                    <ext:RecordField Name="DecisionNumber" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="EffectiveDate" />
                                                                    <ext:RecordField Name="SignerName" />
                                                                    <ext:RecordField Name="SignerPosition" />
                                                                    <ext:RecordField Name="AttachFileName" />
                                                                    <ext:RecordField Name="Note" />
                                                                    <ext:RecordField Name="GroupQuantumId" />
                                                                    <ext:RecordField Name="QuantumId" />
                                                                    <ext:RecordField Name="Grade" />
                                                                    <ext:RecordField Name="Factor" />
                                                                    <ext:RecordField Name="InsuranceSalary" />
                                                                    <ext:RecordField Name="ContractSalary" />
                                                                    <ext:RecordField Name="GrossSalary" />
                                                                    <ext:RecordField Name="NetSalary" />
                                                                    <ext:RecordField Name="PercentageSalary" />
                                                                    <ext:RecordField Name="PercentageOverGrade" />
                                                                    <ext:RecordField Name="Allowance" />
                                                                    <ext:RecordField Name="NextRaiseDate" />
                                                                    <ext:RecordField Name="Type" />
                                                                    <ext:RecordField Name="Status" />
                                                                    <ext:RecordField Name="StatusName" />
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="EmployeeName" />
                                                                    <ext:RecordField Name="EmployeeSex" />
                                                                    <ext:RecordField Name="EmpoyeeBirthDate" />
                                                                    <ext:RecordField Name="ContractTypeName" />
                                                                    <ext:RecordField Name="GroupQuantumName" />
                                                                    <ext:RecordField Name="QuantumName" />
                                                                    <ext:RecordField Name="TypeName" />
                                                                    <ext:RecordField Name="EmpoyeeBirthDate" />
                                                                    <ext:RecordField Name="BasicSalary" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column Header="Mã cán bộ" Width="70" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Width="130" DataIndex="EmployeeName">
                                                        </ext:Column>
                                                        <ext:Column Header="Tên ngạch" Width="120" DataIndex="QuantumName">
                                                        </ext:Column>
                                                        <ext:Column Header="Bậc" Width="40" DataIndex="Grade">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày quyết định" Width="100" DataIndex="DecisionDate">
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Thời hạn xét vượt khung" Width="120" DataIndex="RaisingSalaryDate">
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Phòng ban" Width="80" DataIndex="DepartmentName">
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel7" runat="server" />
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
                                            </ext:GridPanel>
                                            <ext:Hidden runat="server" ID="hdfOutFrameSalary" />
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(hdfOutFrameSalary.getValue()==''){storeOutFrameSalary.reload();hdfOutFrameSalary.setValue('True');}" />
                                        </Listeners>
                                    </ext:Panel>
                                </Items>
                            </ext:Panel>
                        </ext:LayoutColumn>
                    </Columns>
                </ext:ColumnLayout>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
