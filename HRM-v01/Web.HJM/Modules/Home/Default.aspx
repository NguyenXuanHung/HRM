<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.Home.Default" Codebehind="Default.aspx.cs" %>

<%@ Register Src="~/Modules/Base/SendMailForm/SendMail.ascx" TagName="SendMail" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="chart/js/highcharts.js"></script>
    <script src="../../Resource/js/RenderJS.js" type="text/javascript"></script>
    <script type="text/javascript" src="chart/js/modules/exporting.js"></script>
    <script src="../../Resource/js/Extcommon.js" type="text/javascript"></script>
    <link href="css/Chart.css" rel="stylesheet" type="text/css" />
    <link href="css/Homepage.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #grp_sinhnhatnhanvien .x-grid3-cell-inner {
            white-space: normal !important;
        }

        #panelRiseSalary .x-grid3-cell-inner {
            white-space: normal !important;
        }
    </style>
    <script type="text/javascript">
        RenderGender2 = function (value, p, record) {
            if (value == "Nam")
                return "<span style='color:blue;'>" + value + "</span>"
            return "<span style='color:red;'>" + value + "</span>"
        }
      
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                PagingToolbar1.doLoad();
            }
            if (txtSearchKey.getValue() != '')
                this.triggers[0].show();
        }

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
            var keyword = document.getElementById("txtSearchKey").value;
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
            var records = GridPanel1.getSelectionModel().getSelections();
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
        <uc1:SendMail ID="SendMail1" runat="server" />
        <!-- hidden field -->
        <ext:Hidden runat="server" ID="hdfMaBoPhan" />
        <ext:Hidden runat="server" ID="hdfAllNodeID" />
        <ext:Hidden runat="server" ID="hdfCurrentUserID" />
        <ext:Hidden runat="server" ID="hdfNangNgachCCVC" />
        <ext:Hidden runat="server" ID="hdfDepartments" />
        <ext:Hidden runat="server" ID="hdfSalaryRaiseRegularType"/>
        <ext:Hidden runat="server" ID="hdfSalaryRaiseOutFrameType"/>

        <ext:Window ID="wdMoRong" Layout="FormLayout" Icon="ChartBar" runat="server" Modal="true"
            Constrain="true" Title="<%$ Resources:Desktop, chart_expand%>" Maximized="true"
            Hidden="true">
            <Buttons>
                <ext:Button runat="server" Text="Đóng" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{wdMoRong}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Menu ID="RowContextMenu" runat="server">
            <Items>
                <ext:MenuItem ID="MenuItem2" Visible="false" runat="server" Icon="Printer" Text="<%$ Resources:Language, report%>">
                    <Listeners>
                        <Click Handler="if(#{hdfEmployeeId}.getValue()==''){Ext.Msg.alert('<asp:Literal runat=\'server\' Text=\'<%$ Resources:Language, thong_bao%>\' />','<asp:Literal runat=\'server\' Text=\'<%$ Resources:Language, not_selected_record%>\' />');
                                    }else{#{wdShowReport}.show();}" />
                    </Listeners>
                </ext:MenuItem>
                <ext:MenuItem runat="server" Icon="Email" Visible="false" ID="mnuGuiMail" Text="<%$ Resources:Language, send_email%>">
                    <DirectEvents>
                        <Click OnEvent="btnSendEmail_Click">
                            <EventMask ShowMask="true" Msg="<%$ Resources:Language, sending_email_please_wait%>" />
                            <ExtraParams>
                                <ext:Parameter Name="Email" Mode="Raw" Value="#{RowSelectionModel1}.getSelected().data.EMAIL">
                                </ext:Parameter>
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
            </Items>
        </ext:Menu>
        <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="wdShowReport"
            Title="<%$ Resources:Desktop, report_staff_profile%>" Maximized="true" Icon="Printer">
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
        <ext:Window Modal="true" Hidden="true" runat="server" Layout="BorderLayout" ID="Window1"
            Title="<%$ Resources:Desktop, report_staff_birthday%>" Maximized="true" Icon="Printer">
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
        <ext:Viewport ID="Viewport1" runat="server">
            <Items>
                <ext:ColumnLayout ID="ColumnLayout1" runat="server" Split="false" FitHeight="true">
                    <Columns>
                        <ext:LayoutColumn ColumnWidth="0.6">
                            <ext:Panel ID="Panel1" runat="server" Header="false" Border="false">
                                <Items>
                                    <ext:RowLayout ID="RowLayout1" runat="server" Split="false">
                                        <Rows>
                                            <ext:LayoutRow RowHeight="0.55">
                                                <ext:Panel ID="pnlChart" runat="server" Visible="false" Icon="ChartBar" Layout="FitLayout"
                                                    Border="false" Title="<%$ Resources:Desktop, chart%>">
                                                    <TopBar>
                                                        <ext:Toolbar ID="Toolbar1" Visible="false" runat="server">
                                                            <Items>
                                                                <ext:Button ID="btnChonLoaiBieuDo" Visible="false" runat="server" Icon="ChartBar"
                                                                    Text="<%$ Resources:Desktop, select_type_of_chart%>">
                                                                    <Menu>
                                                                        <ext:Menu ID="Menu1" runat="server">
                                                                            <Items>
                                                                                <ext:MenuItem ID="MenuItem1" Visible="false" runat="server" Icon="ChartPie" Text="<%$ Resources:Desktop, chart_by_gender%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=Gender&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=Gender&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuTrinhDoChart" Visible="false" runat="server" Icon="ChartPie"
                                                                                    Text="<%$ Resources:Desktop, chart_by_level%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=Level&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=Level&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="MenuItem8" Visible="false" runat="server" Icon="ChartPie" Text="<%$ Resources:Desktop, chart_by_marriage_status%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=TTHonNhan&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=TTHonNhan&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="MenuItem9" Visible="false" runat="server" Icon="ChartPie" Text="<%$ Resources:Desktop, chart_by_religion%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=TonGiao&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=TonGiao&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="MenuItem10" Visible="false" runat="server" Icon="ChartPie" Text="<%$ Resources:Desktop, chart_by_ethnicity%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=DanToc&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=DanToc&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="MenuItem11" Visible="false" runat="server" Icon="ChartPie" Text="<%$ Resources:Desktop, chart_by_contract_type%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=LoaiHD&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=LoaiHD&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuBieuDoChucVuDoan" Visible="false" runat="server" Icon="ChartPie"
                                                                                    Text="<%$ Resources:Desktop, chart_by_union_office%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=NhanSuTheoChucVuDoan&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=NhanSuTheoChucVuDoan&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuBieuDoChucVuDang" Visible="false" runat="server" Icon="ChartPie"
                                                                                    Text="Thống kê CBCC theo chức vụ Đảng">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=ThongKeChucVuDang&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=ThongKeChucVuDang&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="mnuBieuDoChucVuQuanDoi" Visible="false" runat="server" Icon="ChartPie"
                                                                                    Text="<%$ Resources:Desktop, chart_by_military_grade%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=CapBacQuanDoi&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=CapBacQuanDoi&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="menuThongKeNhanSuTheoThamNienCongTac" Visible="false" runat="server"
                                                                                    Icon="ChartPie" Text="<%$ Resources:Desktop, chart_by_length_of_service%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=ThongKeTheoThamNienCT&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=ThongKeTheoThamNienCT&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <%--<ext:MenuItem ID="mnuLyDoNghiViec" Visible="false" runat="server" Icon="ChartPie"
                                                                                    Text="<%$ Resources:Desktop, chart_by_reason_for_leaving%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/PieChart.aspx?type=LyDoNghiViec&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/PieChart.aspx?type=LyDoNghiViec&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>--%>
                                                                                <ext:MenuSeparator runat="server" ID="sp" />
                                                                                <ext:MenuItem ID="MenuItem3" Visible="false" runat="server" Icon="ChartLine" Text="<%$ Resources:Desktop, chart_by_fluctuation%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="BienDongNhanSu(hdfCurrentUserID.getValue());" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="MenuItem4" Visible="false" runat="server" Icon="ChartBar" Text="<%$ Resources:Desktop, chart_by_age%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/ColumnChart.aspx?type=Age&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/ColumnChart.aspx?type=Age&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                                <ext:MenuItem ID="MenuBieuDoDonVi" Visible="false" runat="server" Icon="ChartBar" Text="<%$ Resources:Desktop, chart_by_unit%>">
                                                                                    <Listeners>
                                                                                        <Click Handler="document.getElementById('iframeChart').src = 'chart/ColumnChart.aspx?type=NSDonVi&userID=' + hdfCurrentUserID.getValue();#{hdfChartUrl}.setValue('chart/ColumnChart.aspx?type=NSDonVi&userID=' + hdfCurrentUserID.getValue());#{tbsChonNam}.hide();#{cbxChonNam}.hide();#{btnFullScreen}.setDisabled(false);" />
                                                                                    </Listeners>
                                                                                </ext:MenuItem>
                                                                            </Items>
                                                                        </ext:Menu>
                                                                    </Menu>
                                                                </ext:Button>
                                                                <ext:Button runat="server" ID="btnSetChartDefault" Visible="false" Text="<%$ Resources:Desktop, set_default_chart%>"
                                                                    Icon="Tick">
                                                                    <DirectEvents>
                                                                        <Click OnEvent="btnSetChartDefault_Click">
                                                                            <Confirmation Title="Thông báo" Message="<%$ Resources:Desktop, confirm_set_default%>"
                                                                                ConfirmRequest="true" />
                                                                        </Click>
                                                                    </DirectEvents>
                                                                </ext:Button>
                                                                <ext:ToolbarSeparator ID="tbsChonNam" Hidden="true" />
                                                                <ext:ComboBox runat="server" SelectedIndex="0" ValueField="ID" DisplayField="Title"
                                                                    AnchorHorizontal="100%" FieldLabel="<%$ Resources:Desktop, select_year%>" LabelWidth="60"
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
                                                                        <Select Handler="BienDongNhanSu(hdfCurrentUserID.getValue());" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <%--<ext:CompositeField runat="server" ID="cpsf1" Width="200" Hidden="true">
                                                                    <Items>
                                                                        <ext:DropDownField ID="ddf_bophan" runat="server" Editable="false" Width="130" TriggerIcon="SimpleArrowDown"
                                                                            EmptyText="<%$ Resources:Language, select_department%>">
                                                                            <Component>
                                                                                <ext:TreePanel ID="trp_bophan" runat="server" Title="" Icon="Accept" Height="300"
                                                                                    Width="350" Shadow="None" UseArrows="true" AutoScroll="true" Animate="true" EnableDD="false"
                                                                                    ContainerScroll="true" RootVisible="false">
                                                                                    <Root>
                                                                                    </Root>
                                                                                </ext:TreePanel>
                                                                            </Component>
                                                                        </ext:DropDownField>
                                                                        <ext:Button runat="server" ID="btnXem" Text="<%$ Resources:Language, accept%>" Icon="Accept">
                                                                            <Listeners>
                                                                                <Click Handler="LoadChart();" />
                                                                            </Listeners>
                                                                        </ext:Button>
                                                                    </Items>
                                                                </ext:CompositeField>--%>
                                                                <ext:Button runat="server" ID="btnFullScreen" Disabled="false" Icon="ArrowOut" Text="<%$ Resources:Desktop, expansion%>">
                                                                    <Listeners>
                                                                        <Click Handler="if (hdfChartUrl.getValue() != '') Ext.net.DirectMethods.showWindowMoRong();" />
                                                                    </Listeners>
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip3" runat="server" Frame="true" Title="<%$ Resources:Language, huong_dan%>"
                                                                            Html="<%$ Resources:Desktop, view_fullscreen%>">
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
                                                <ext:Panel ID="Panel2" runat="server" Visible="false" Layout="BorderLayout" Border="false"
                                                    Icon="Zoom" Title="<%$ Resources:Desktop, search_quick_staff_info%>">
                                                    <TopBar>
                                                        <ext:Toolbar runat="server" Visible="false" ID="tbar">
                                                            <Items>
                                                                <ext:Button runat="server" Visible="false" Disabled="true" ID="btnBaoCaoChiTiet1NV"
                                                                    Text="<%$ Resources:Language, print_cv%>" Icon="Printer">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip2" runat="server" Title="<%$ Resources:Language, huong_dan%>"
                                                                            Html="<%$ Resources:Desktop, select_staff_and_report%>">
                                                                        </ext:ToolTip>
                                                                    </ToolTips>
                                                                    <Listeners>
                                                                        <Click Handler="if(#{hdfEmployeeId}.getValue()==''){Ext.Msg.alert('<asp:Literal runat=\'server\' Text=\'<%$ Resources:Language, thong_bao%>\' />','<asp:Literal runat=\'server\' Text=\'<%$ Resources:Language, not_selected_record%>\' />');
                                                                                    }else{#{wdShowReport}.show();}" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <%--<ext:Button ID="btnSendEmail" Visible="false" runat="server" Disabled="true" Text="<%$ Resources:Language, send_email%>"
                                                                    Icon="Email">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip1" runat="server" Title="<%$ Resources:Language, huong_dan%>"
                                                                            Html="Chức năng gửi Email cho CBCCVC để thông báo kế hoạch của công ty như đi du lịch, ngày nghỉ hoặc yêu cầu nộp hồ sơ...">
                                                                        </ext:ToolTip>
                                                                    </ToolTips>
                                                                    <DirectEvents>
                                                                        <Click OnEvent="btnSendEmail_Click">
                                                                            <EventMask ShowMask="true" Msg="<%$ Resources:Language, sending_email_please_wait%>" />
                                                                            <ExtraParams>
                                                                                <ext:Parameter Name="Email" Mode="Raw" Value="#{RowSelectionModel1}.getSelected().data.EMAIL">
                                                                                </ext:Parameter>
                                                                            </ExtraParams>
                                                                        </Click>
                                                                    </DirectEvents>
                                                                </ext:Button>--%>
                                                                <ext:Button ID="Button5" runat="server" Visible="false" Text="<%$ Resources:Language, commom_help%>"
                                                                    Icon="Help">
                                                                    <Listeners>
                                                                        <Click Handler="window.open('../../HelpBook/Default.aspx?link=TraCuuNhanh','_blank')" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Hidden runat="server" ID="hdfTragThaiHS" />
                                                                <ext:ComboBox ID="cbxTrangThaiHoSo" FieldLabel="Trạng thái" Editable="false" runat="server"
                                                                    DisplayField="Name" ValueField="Id" LoadingText="Đang tải dữ liệu..." ItemSelector="div.list-item"
                                                                    AnchorHorizontal="98%" LabelWidth="70" Width="200">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Template ID="Template38" runat="server">
                                                                        <Html>
                                                                            <tpl for=".">
						                                                        <div class="list-item"> 
							                                                        {Name}
						                                                        </div>
					                                                        </tpl>
                                                                        </Html>
                                                                    </Template>
                                                                    <Store>
                                                                        <ext:Store runat="server" ID="cbxTrangThaiHoSo_store" AutoLoad="false" OnRefreshData="cbxTrangThaiHoSo_OnrefreshData">
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
                                                                        <Expand Handler="if(cbxTrangThaiHoSo.store.getCount()==0) cbxTrangThaiHoSo_store.reload();" />
                                                                        <Select Handler="this.triggers[0].show();hdfTragThaiHS.setValue(cbxTrangThaiHoSo.getValue());#{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide();hdfTragThaiHS.reset(); #{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();}" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:ToolbarFill runat="server" ID="tbarFull" />
                                                                <ext:TriggerField runat="server" EnableKeyEvents="true" ID="txtSearchKey" Width="200"
                                                                    EmptyText="<%$ Resources:Language, enter_keyword%>">
                                                                    <ToolTips>
                                                                        <ext:ToolTip Closable="true" Draggable="true" AutoHide="false" ID="tl" runat="server"
                                                                            Title="<%$ Resources:Language, enter_keyword%>" Header="true" Frame="true" Html="<%$ Resources:Desktop, guild_search_desktop%>">
                                                                        </ext:ToolTip>
                                                                    </ToolTips>
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <Blur Handler="tl.hide();" />
                                                                        <Focus Handler="tl.show();" />
                                                                        <KeyPress Fn="enterKeyPressHandler" />
                                                                        <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); #{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();}" />
                                                                    </Listeners>
                                                                </ext:TriggerField>
                                                                <ext:Button ID="Button2" runat="server" Icon="Zoom" Text="<%$ Resources:Language, search%>">
                                                                    <Listeners>
                                                                        <Click Handler="#{PagingToolbar1}.pageIndex=0;#{PagingToolbar1}.doLoad();#{Store1}.reload();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <Items>
                                                        <ext:Hidden runat="server" ID="hdfMaDonVi" />
                                                        <ext:Hidden runat="server" ID="hdfPrKeyHoSo" />
                                                        <ext:GridPanel Border="false" ID="GridPanel1" Visible="false" Header="false" runat="server"
                                                            StripeRows="true" TrackMouseOver="true" Title="Array Grid" Region="Center" AnchorHorizontal="100%"
                                                            AnchorVertical="100%" AutoExpandColumn="hoten">
                                                            <Store>
                                                                <ext:Store ID="Store1" GroupField="DepartmentName" runat="server" IgnoreExtraFields="false">
                                                                    <Proxy>
                                                                        <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                                    </Proxy>
                                                                    <AutoLoadParams>
                                                                        <ext:Parameter Name="start" Value="={0}" />
                                                                        <ext:Parameter Name="limit" Value="={10}" />
                                                                    </AutoLoadParams>
                                                                    <BaseParams>
                                                                        <ext:Parameter Name="handlers" Value="Home"/>
                                                                        <ext:Parameter Name="SearchKey" Value="#{txtSearchKey}.getValue()" Mode="Raw" />
                                                                        <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                                        <ext:Parameter Name="UserID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                                        <ext:Parameter Name="StatusParam" Value="#{cbxTrangThaiHoSo}.getValue()" Mode="Raw" />
                                                                    </BaseParams>
                                                                    <Reader>
                                                                        <ext:JsonReader Root="Data" IDProperty="Id" TotalProperty="TotalRecords">
                                                                            <Fields>
                                                                                <ext:RecordField Name="Id" />
                                                                                <ext:RecordField Name="EmployeeCode" />
                                                                                <ext:RecordField Name="SexName" />
                                                                                <ext:RecordField Name="FullName" />
                                                                                <ext:RecordField Name="ContractTimeSheetHandlerTypeName" />
                                                                                <ext:RecordField Name="EducationName" />
                                                                                <ext:RecordField Name="Address" />
                                                                                <ext:RecordField Name="CellPhoneNumber" />
                                                                                <ext:RecordField Name="BirthDate" />
                                                                                <ext:RecordField Name="RecruimentDate" />
                                                                                <ext:RecordField Name="ParticipationDate" />
                                                                                <ext:RecordField Name="IDNumber" />
                                                                                <ext:RecordField Name="PersonalEmail" />
                                                                                <ext:RecordField Name="DepartmentName" />
                                                                                <ext:RecordField Name="WorkStatusName" />
                                                                            </Fields>
                                                                        </ext:JsonReader>
                                                                    </Reader>
                                                                </ext:Store>
                                                            </Store>
                                                            <ColumnModel ID="ColumnModel1" runat="server">
                                                                <Columns>
                                                                    <ext:RowNumbererColumn Header="STT" Width="35" />
                                                                    <ext:Column Header="<%$ Resources:HOSO, staff_code%>" Width="50" DataIndex="EmployeeCode">
                                                                        <Renderer Fn="RenderHightLight" />
                                                                    </ext:Column>
                                                                    <ext:Column ColumnID="hoten" Header="Họ tên" Width="130"
                                                                        DataIndex="FullName">
                                                                        <Renderer Fn="RenderHightLight" />
                                                                    </ext:Column>
                                                                    <ext:Column Header="Giới tính" Width="40" DataIndex="SexName">
                                                                        <Renderer Fn="RenderGender2" />
                                                                    </ext:Column>
                                                                    <ext:DateColumn Header="Ngày sinh" Width="70" DataIndex="BirthDate" Format="dd/MM/yyyy" />
                                                                    <ext:GroupingSummaryColumn Header="Phòng ban" Width="85"
                                                                        DataIndex="DepartmentName" />
                                                                    <ext:Column Header="<%$ Resources:HOSO, staff_phone%>" Width="75" DataIndex="CellPhoneNumber">
                                                                        <Renderer Fn="RenderHightLight" />
                                                                    </ext:Column>
                                                                    <ext:Column Header="Trạng thái" Width="80" DataIndex="WorkStatusName">
                                                                    </ext:Column>
                                                                </Columns>
                                                            </ColumnModel>
                                                            <Plugins>
                                                                <ext:RowExpander runat="server" ID="rx">
                                                                    <Template ID="Template1" runat="server">
                                                                        <Html>
                                                                            <table border="0" cellpadding="5" cellspacing="15">
                                                                            <tr>
                                                                                <td>
                                                                                    <b>Địa chỉ</b>
                                                                                </td>
                                                                                <td colspan="2"><tpl if="Address &gt; ''">{Address}</tpl></td> 
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Trình độ</b></td>
                                                                                <td><tpl if="EducationName &gt; ''">{EducationName}</tpl></td>
                                                                                <td><b>Ngày vào đơn vị</b></td>
                                                                                <td><tpl if="ParticipationDate &gt; ''">{ParticipationDate:date("d/m/Y")}</tpl></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Loại hợp đồng</b></td>
                                                                                <td><tpl if="ContractTimeSheetHandlerTypeName &gt; ''">{ContractTimeSheetHandlerTypeName}</tpl></td>
                                                                                <td><b>Số CMT</b></td>
                                                                                <td><tpl if="IDNumber &gt; ''">{IDNumber}</tpl></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Ngày tuyển dụng</b></td>
                                                                                <td><tpl if="RecruimentDate &gt; ''">{RecruimentDate:date("d/m/Y")}</tpl></td>
                                                                                <td><b>Email</b></td>
                                                                                <td><tpl if="PersonalEmail &gt; ''">{PersonalEmail}</tpl></td>
                                                                            </tr>
                                                                        </table>
                                                                        </Html>
                                                                    </Template>
                                                                </ext:RowExpander>
                                                            </Plugins>
                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server">
                                                                    <Listeners>
                                                                        <RowSelect Handler="#{hdfEmployeeId}.setValue(#{RowSelectionModel1}.getSelected().id);btnBaoCaoChiTiet1NV.enable();
                                                                                        #{hdfPrKeyHoSo}.setValue(RowSelectionModel1.getSelected().get('Id'));" />
                                                                        <RowDeselect Handler="hdfPrKeyHoSo.reset(); " />
                                                                        
                                                                    </Listeners>
                                                                </ext:RowSelectionModel>
                                                            </SelectionModel>

                                                            <LoadMask ShowMask="true" Msg="Đang tải" />
                                                            <BottomBar>
                                                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="10">
                                                                    <Items>
                                                                        <ext:Label ID="Label1" runat="server" Text="<%$ Resources:HOSO, number_line_per_page%>" />
                                                                        <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                                                        <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                                                            <Items>
                                                                                <ext:ListItem Text="1" />
                                                                                <ext:ListItem Text="2" />
                                                                                <ext:ListItem Text="10" />
                                                                                <ext:ListItem Text="20" />
                                                                            </Items>
                                                                            <SelectedItem Value="10" />
                                                                            <Listeners>
                                                                                <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                                                            </Listeners>
                                                                        </ext:ComboBox>
                                                                    </Items>
                                                                </ext:PagingToolbar>
                                                            </BottomBar>
                                                            <View>
                                                                <ext:GroupingView ID="GroupingView1" runat="server" ForceFit="true" MarkDirty="false"
                                                                    ShowGroupName="false" EnableNoGroups="true" HideGroupedColumn="true" />
                                                            </View>
                                                            <Listeners>
                                                                <RowContextMenu Handler="e.preventDefault(); #{RowContextMenu}.dataRecord = this.store.getAt(rowIndex);#{RowContextMenu}.showAt(e.getXY());#{GridPanel1}.getSelectionModel().selectRow(rowIndex);" />
                                                            </Listeners>
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
                            <ext:Panel ID="Panel6" Visible="false" runat="server" Border="false" Layout="AccordionLayout"
                                Icon="Build" Title="<%$ Resources:HOSO, utility%>">
                                <Items>
                                    <ext:Panel ID="pnlSinhNhatNhanVien" Visible="false" Layout="BorderLayout" runat="server"
                                        Icon="Cake" Title="<%$ Resources:Desktop, birthdays_staff%>">
                                        <Items>
                                            <ext:Hidden runat="server" ID="hdfIsReload" />
                                            <ext:GridPanel ID="grp_sinhnhatnhanvien" Border="false" TrackMouseOver="true" Region="Center"
                                                AutoExpandColumn="DepartmentName" AutoExpandMin="150" runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" Visible="false" ID="tbReportDanhSach">
                                                        <Items>
                                                            <ext:Button runat="server" Text="<%$ Resources:Desktop, print_list%>" Icon="Printer">
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
                                                                                        <EventMask ShowMask="true" Msg="<%$ Resources:Language, sending_email_please_wait%>" />
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
                                                                                        <EventMask ShowMask="true" Msg="<%$ Resources:Language, sending_email_please_wait%>" />
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
                                                            <ext:Parameter Name="handlers" Value="BirthdayOfEmployee"/>
                                                            <ext:Parameter Name="MaDonVi" Value="#{hdfMaDonVi}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="userID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel3" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column Header="<%$ Resources:HOSO, staff_code%>" Width="70" DataIndex="EmployeeCode" />
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
                                    <ext:Panel ID="pnlNhanVienSapHetHopDong" Layout="BorderLayout" Visible="false" runat="server"
                                        Icon="Page" Title="<%$ Resources:Desktop, out_of_contract%>">
                                        <Items>
                                            <ext:GridPanel ID="GridPanel3" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <TopBar>
                                                    <ext:Toolbar runat="server" Visible="false" ID="Toolbar2">
                                                        <Items>
                                                            <ext:Button ID="Button8" runat="server" Text="<%$ Resources:Desktop, print_list%>"
                                                                Icon="Printer">
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
                                                                    <ext:RecordField Name="ContractTimeSheetHandlerTypeName" />
                                                                    <ext:RecordField Name="ContractDate" />
                                                                    <ext:RecordField Name="SexName" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="EndContract"/>
                                                            <ext:Parameter Name="MaDonVi" Value="#{hdfMaDonVi}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="userID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={30}" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel5" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Locked="true" Width="35" />
                                                        <ext:Column Header="<%$ Resources:HOSO, staff_code%>" Locked="true" Width="70" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Locked="true" Width="130" DataIndex="FullName">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="<%$ Resources:Desktop, signing_date%>"
                                                            Width="100" DataIndex="ContractDate">
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="<%$ Resources:Desktop, expiration_date%>"
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
                                                        <Template ID="Template2" runat="server">
                                                            <Html>
                                                                <br />
                                                                <p><b>Loại hợp đồng:</b> {ContractTimeSheetHandlerTypeName}</p>
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
                                    <ext:Panel ID="pnlHoSoNhanSuCanDuyet" Layout="BorderLayout" Visible="false" runat="server"
                                        Icon="UserTick" Title="Hồ sơ nhân sự cần duyệt">
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
                                                            <ext:Parameter Name="department" Value="#{hdfMaDonVi}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={30}" />
                                                            <ext:Parameter Name="filter" Value="ChuaDuyet" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel6" runat="server">
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
                                    <ext:Panel ID="Panel4" Layout="BorderLayout" runat="server" Icon="User" Title="Cán bộ đến kỳ nghỉ hưu">
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
                                                    <ext:DateField runat="server" ID="dfNgaySinh"
                                                        AnchorHorizontal="95%" TabIndex="1" Editable="true" EnableKeyEvents="true" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                                        RegexText="Định dạng ngày không đúng">
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
                                            <ext:GridPanel ID="grpHoSoCBDenKyNghiHuu" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <Store>
                                                    <ext:Store ID="grpHoSoCBDenKyNghiHuuStore" AutoLoad="false" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="RetirementOfEmployee"/>
                                                            <ext:Parameter Name="dfNgaySinh" Value="#{dfNgaySinh}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="MaDonVi" Value="#{hdfMaDonVi}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="userID" Value="#{hdfCurrentUserID}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="start" Value="={0}" />
                                                            <ext:Parameter Name="limit" Value="={30}" />
                                                        </BaseParams>
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
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel2" runat="server">
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
                                    <ext:Panel ID="panelRiseSalary" Layout="BorderLayout" runat="server" Icon="User"
                                        Title="Cán bộ đến kỳ nâng lương">
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
                                                            <Click Handler="#{PagingToolbar6}.pageIndex = 0; #{PagingToolbar6}.doLoad(); RowSelectionModel6.clearSelections();" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:GridPanel ID="grdNangLuongCBCCVC" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <Store>
                                                    <ext:Store ID="StoreNangLuongCBCCVC" AutoLoad="false" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <Reader>
                                                            <ext:JsonReader TotalProperty="TotalRecords" Root="Data" IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="FullName" />
                                                                    <ext:RecordField Name="QuantumCode" />
                                                                    <ext:RecordField Name="QuantumName" />
                                                                    <ext:RecordField Name="QuantumGrade" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="RaisingSalaryDate" />
                                                                    <ext:RecordField Name="TimeSheetHandlerTypeRecord" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                    <ext:RecordField Name="ParentDepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="RiseSalary"/>
                                                            <ext:Parameter Name="salaryRaiseTimeSheetHandlerType" Value="hdfSalaryRaiseRegularTimeSheetHandlerType.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="fromDate" Value="#{dfFromDate}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="toDate" Value="#{dfToDate}.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel4" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column Header="Mã cán bộ" Width="70" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Width="130" DataIndex="FullName">
                                                        </ext:Column>
                                                        <ext:Column Header="Tên ngạch" Width="120" DataIndex="QuantumName">
                                                        </ext:Column>
                                                        <ext:Column Header="Bậc" Width="40" DataIndex="QuantumGrade">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày bổ nhiệm ngạch" Width="120" DataIndex="DecisionDate">
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày nâng lương" Width="100" DataIndex="RaisingSalaryDate">
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Phòng ban" Width="250" DataIndex="DepartmentName">
                                                        </ext:Column>
                                                        <ext:Column Header="Đơn vị quản lý" Width="200" DataIndex="ParentDepartmentName">
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" />
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar6" runat="server" PageSize="30">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                            <ext:Hidden runat="server" ID="hdfNangLuongCCVC" />
                                        </Items>
                                        <Listeners>
                                            <Activate Handler="if(hdfNangLuongCCVC.getValue()==''){StoreNangLuongCBCCVC.reload();hdfNangLuongCCVC.setValue('True');}" />
                                        </Listeners>
                                    </ext:Panel>
                                     <ext:Panel ID="panelOutFrameSalary" Layout="BorderLayout" runat="server" Icon="User"
                                        Title="Cán bộ đến kỳ xét vượt khung">
                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar5" runat="server">
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
                                                    <ext:Button runat="server" ID="Button7" Text="Lọc" Icon="Zoom">
                                                        <Listeners>
                                                            <Click Handler="#{PagingToolbar7}.pageIndex = 0; #{PagingToolbar7}.doLoad(); RowSelectionModel7.clearSelections();" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:GridPanel ID="gridOutFrameSalary" Border="false" Region="Center" TrackMouseOver="true"
                                                runat="server" StripeRows="true" AnchorHorizontal="100%">
                                                <Store>
                                                    <ext:Store ID="storeOutFrameSalary" AutoLoad="false" runat="server">
                                                        <Proxy>
                                                            <ext:HttpProxy Json="true" Method="GET" Url="~/Services/BaseHandler.ashx" />
                                                        </Proxy>
                                                        <Reader>
                                                            <ext:JsonReader TotalProperty="TotalRecords" Root="Data" IDProperty="Id">
                                                                <Fields>
                                                                    <ext:RecordField Name="Id" />
                                                                    <ext:RecordField Name="EmployeeCode" />
                                                                    <ext:RecordField Name="FullName" />
                                                                    <ext:RecordField Name="QuantumId" />
                                                                    <ext:RecordField Name="QuantumName" />
                                                                    <ext:RecordField Name="QuantumGrade" />
                                                                    <ext:RecordField Name="DecisionDate" />
                                                                    <ext:RecordField Name="RaisingSalaryDate" />
                                                                    <ext:RecordField Name="DepartmentName" />
                                                                </Fields>
                                                            </ext:JsonReader>
                                                        </Reader>
                                                        <BaseParams>
                                                            <ext:Parameter Name="handlers" Value="RiseSalary"/>
                                                            <ext:Parameter Name="salaryRaiseTimeSheetHandlerType" Value="hdfSalaryRaiseOutFrameTimeSheetHandlerType.getValue()" Mode="Raw" />
                                                            <ext:Parameter Name="departments" Value="#{hdfDepartments}.getValue()" Mode="Raw" />
                                                        </BaseParams>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel7" runat="server">
                                                    <Columns>
                                                        <ext:RowNumbererColumn Header="STT" Width="35" />
                                                        <ext:Column Header="Mã cán bộ" Width="70" DataIndex="EmployeeCode">
                                                        </ext:Column>
                                                        <ext:Column Header="Họ tên" Width="130" DataIndex="FullName">
                                                        </ext:Column>
                                                        <ext:Column Header="Tên ngạch" Width="120" DataIndex="QuantumName">
                                                        </ext:Column>
                                                        <ext:Column Header="Bậc" Width="40" DataIndex="QuantumGrade">
                                                        </ext:Column>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Ngày quyết định" Width="120" DataIndex="DecisionDate">
                                                        </ext:DateColumn>
                                                        <ext:DateColumn Format="dd/MM/yyyy" Header="Thời hạn xét vượt khung" Width="100" DataIndex="RaisingSalaryDate">
                                                        </ext:DateColumn>
                                                        <ext:Column Header="Phòng ban" Width="250" DataIndex="DepartmentName">
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel7" runat="server" />
                                                </SelectionModel>
                                                <LoadMask ShowMask="true" Msg="Đang tải dữ liệu" />
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar7" runat="server" PageSize="30">
                                                    </ext:PagingToolbar>
                                                </BottomBar>
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
