<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Web.HJM.Modules.Catalog.SalaryLevelQuantum" Codebehind="SalaryLevelQuantum.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .hsl_template
        {
            line-height: 18px;
            border-bottom: 1px solid #DDE7FC;
        }
        .ml_template
        {
            line-height: 18px;
            border-bottom: 1px solid #C1C1C1;
        }
        #GridPanel1 .x-grid3-cell-inner
        {
            height: 36px;
            padding-left: 2px;
            padding-right: 0;
        }
        .list-item
        {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            white-space: normal;
            color: #000;
        }
        #GridPanel1 td
        {
            vertical-align: middle !important;
        }
    </style>
    <script type="text/javascript">
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                PagingToolbar1.pageIndex = 0;
                GridPanel1.getSelectionModel().clearSelections();
                hdfRecordId.setValue('');
                Store1.reload();
            }
        }
        var GenerateMucLuong = function () {
            var ml = (txtHeSoLuong.getValue().replace(',', '.') * 1) * (hdfLuongCoBan.getValue() * 1);
            txtMucLuong.setValue(Math.round(ml));
        }
        var ResetWdThemMucLuongNgach = function () {
            hdfGroupQuantumId.reset();
            hdfQuantumId.reset();
            cbxNhomNgach.reset();
            hdfBac.reset();
            cbxBac.reset();
            txtHeSoLuong.reset();
            txtMucLuong.reset();
            txtGhiChu.reset();
        }
        var CheckInputThemMucLuongNgach = function () {
            if (cbxNhomNgach.getValue() == '' || cbxNhomNgach.getValue() == null) {
                alert('Bạn chưa chọn nhóm ngạch');
                cbxNhomNgach.focus();
                return false;
            }
            if (cbxBac.getValue() == '' || cbxBac.getValue() == null) {
                alert('Bạn chưa chọn bậc');
                cbxBac.focus();
                return false;
            }
            if (txtHeSoLuong.getValue().trim() == '') {
                alert('Bạn chưa nhập hệ số lương');
                txtHeSoLuong.focus();
                return false;
            }
            if (txtMucLuong.getValue().trim() == '') {
                alert('Bạn chưa nhập mức lương');
                txtMucLuong.focus();
                return false;
            }
            return true;
        }
        var RenderCap = function (value, p, record) {
            if (value == null)
                arr = new Array(0);
            else
                var arr = value.split('#');
            if (arr.length > 1) {
                var rs = "<div class='hsl_template'>";
                rs += arr[0];
                rs += "</div><div class='ml_template'>";
                rs += arr[1];
                rs += "</div>";
            }
            else
                rs = '';
            return rs;
        }
        var RenderName = function (value, p, record) {
            if (value == null)
                arr = new Array(0);
            else
                var arr = value.split('#');
            if (arr.length > 1) {
                var rs = "<div class='hsl_template'>";
                rs += arr[0];
                rs += "</div><div class='ml_template'>";
                rs += arr[1];
                rs += " tháng</div>";
            }
            else
                rs = '';
            return rs;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="RM" runat="server" />
        <ext:Hidden runat="server" ID="hdfMax" />
        <ext:Hidden runat="server" ID="hdfLuongCoBan" />
        <ext:Hidden runat="server" ID="hdfIsSelected" />
        <ext:Hidden runat="server" ID="hdfRecordId" />
        <ext:Menu ID="CellContextMenu" runat="server">
            <Items>
                <ext:MenuItem ID="mnuThemMoi" runat="server" Icon="Add" Text="Thêm mới" Disabled="true">
                    <Listeners>
                        <Click Handler="btnCapNhat.show();btnEdit.hide();btnCapNhatDongLai.show();cbxNhomNgach.enable();cbxBac.enable();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="mnuThemMoi_Click">
                            <EventMask ShowMask="true" />
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuItem ID="mnuSua" runat="server" Icon="Pencil" Text="Sửa" Disabled="true">
                    <Listeners>
                        <Click Handler="if (#{hdfIsSelected}.getValue() != 'true') { alert('Bạn chưa chọn mức lương nào!'); return false; } 
                                                    btnCapNhat.hide();btnEdit.show();btnCapNhatDongLai.hide(); cbxNhomNgach.disable();cbxBac.disable();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="btnEditQuantum_Click">
                            <EventMask ShowMask="true" />
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuItem ID="mnuXoa" runat="server" Icon="Delete" Text="Xóa" Disabled="true">
                    <Listeners>
                        <Click Handler="if (#{hdfIsSelected}.getValue() != 'true') { alert('Bạn chưa chọn mức lương nào!'); return false; }" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="btnDeleteQuantum_Click">
                            <EventMask ShowMask="true" />
                            <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa?" ConfirmRequest="true" />
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
            </Items>
        </ext:Menu>
        <ext:Window runat="server" ID="wdThemMoiMucLuongNgach" Title="Thêm mới thông tin lương cho ngạch"
            Icon="Add" AutoHeight="true" Layout="FormLayout" Width="600" Padding="6" Hidden="true"
            Constrain="true" Modal="true">
            <Items>
                <ext:Container runat="server" ID="ctn10" AnchorHorizontal="100%" Layout="ColumnLayout"
                    Height="80">
                    <Items>
                        <ext:Container runat="server" ID="ctn11" Layout="FormLayout" ColumnWidth="0.48">
                            <Items>
                                <ext:Hidden runat="server" ID="hdfGroupQuantumId" />
                                <ext:ComboBox runat="server" ID="cbxNhomNgach" FieldLabel="Nhóm ngạch<span style='color:red;'>*</span>"
                                    CtCls="requiredData" DisplayField="Name" ValueField="Id" AnchorHorizontal="98%"
                                    Editable="false" ItemSelector="div.list-item">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Template ID="Template6" runat="server">
                                        <Html>
                                            <tpl for=".">
						                        <div class="list-item"> 
							                        {Name}
						                        </div>
					                        </tpl>
                                        </Html>
                                    </Template>
                                    <Store>
                                        <ext:Store runat="server" ID="cbxNhomNgachStore" AutoLoad="false">
                                            <Proxy>
                                                <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                                            </Proxy>
                                            <BaseParams>
                                                <ext:Parameter Name="objname" Value="cat_GroupQuantum" />
                                            </BaseParams>
                                            <Reader>
                                                <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                    <Fields>
                                                        <ext:RecordField Name="Id" />
                                                        <ext:RecordField Name="Name" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="this.triggers[0].show(); hdfGroupQuantumId.setValue(cbxNhomNgach.getValue()); cbxBacStore.reload();" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:Hidden runat="server" ID="hdfQuantumId" />
                                <%--<ext:ComboBox runat="server" ID="cbxNgach" FieldLabel="Ngạch<span style='color:red;'>*</span>"
                                    CtCls="requiredData" DisplayField="Name" ValueField="Id" AnchorHorizontal="98%"
                                    Editable="false" ItemSelector="div.list-item">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Template ID="Template9" runat="server">
                                        <Html>
                                            <tpl for=".">
						                        <div class="list-item"> 
							                        {Name}
						                        </div>
					                        </tpl>
                                        </Html>
                                    </Template>
                                    <Store>
                                        <ext:Store runat="server" ID="cbxNgachStore" AutoLoad="false">
                                            <Proxy>
                                                <ext:HttpProxy Method="POST" Url="~/Services/HandlerCatalog.ashx" />
                                            </Proxy>
                                            <BaseParams>
                                                <ext:Parameter Name="objname" Value="cat_Quantum" />
                                            </BaseParams>
                                            <Reader>
                                                <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                                                    <Fields>
                                                        <ext:RecordField Name="Id" />
                                                        <ext:RecordField Name="Name" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Expand Handler="if (cbxNhomNgach.getValue() == '') { cbxNgachStore.removeAll(); alert('Bạn phải chọn nhóm ngạch trước');}" />
                                        <Select Handler="this.triggers[0].show(); hdfQuantumId.setValue(cbxNgach.getValue()); cbxBacStore.reload();" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>--%>
                                <ext:TextField runat="server" ID="txtHeSoLuong" FieldLabel="Hệ số lương<span style='color:Red;'>*</span>"
                                    MaskRe="/[0-9.,]/" AnchorHorizontal="98%" MaxLength="10" CtCls="requiredData">
                                    <Listeners>
                                        <Blur Handler="GenerateMucLuong();" />
                                    </Listeners>
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Title="Hướng dẫn" Html="Sau khi nhập hệ số lương phần mềm sẽ tự động tính mức lương dựa vào lương cơ bản" />
                                    </ToolTips>
                                </ext:TextField>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" ID="Container1" Layout="FormLayout" ColumnWidth="0.52"
                            LabelWidth="125">
                            <Items>
                                <ext:Container runat="server" ID="ctn" Height="27" />
                                <ext:Hidden runat="server" ID="hdfBac" />
                                <ext:ComboBox runat="server" ID="cbxBac" FieldLabel="Bậc<span style='color:red;'>*</span>"
                                    CtCls="requiredData" DisplayField="TEN" ValueField="MA" AnchorHorizontal="100%"
                                    Editable="false" ItemSelector="div.list-item">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                    <Template ID="Template2" runat="server">
                                        <Html>
                                            <tpl for=".">
						                        <div class="list-item"> 
							                        {TEN}
						                        </div>
					                        </tpl>
                                        </Html>
                                    </Template>
                                    <Store>
                                        <ext:Store runat="server" ID="cbxBacStore" AutoLoad="false" OnRefreshData="cbxBacStore_OnRefreshData">
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
                                        <Expand Handler="if (cbxNhomNgach.getValue() == '') { cbxBacStore.removeAll(); alert('Bạn phải chọn nhóm ngạch trước');}" />
                                        <Select Handler="this.triggers[0].show(); hdfBac.setValue(cbxBac.getValue());" />
                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                    </Listeners>
                                </ext:ComboBox>
                                <%--<ext:TextField runat="server" ID="txtCap" FieldLabel="Cấp" AnchorHorizontal="100%"
                                    MaskRe="/[0-9.,]/" MaxLength="10" />--%>
                                <%--<ext:TextField runat="server" ID="txtThoiHanNangLuong" FieldLabel="Thời hạn nâng lương<span style='color:Red;'>*</span>"
                                    MaskRe="/[0-9]/" AnchorHorizontal="100%" MaxLength="5" CtCls="requiredData" />--%>
                                <ext:TextField runat="server" ID="txtMucLuong" FieldLabel="Mức lương<span style='color:Red;'>*</span>"
                                    MaskRe="/[0-9]/" AnchorHorizontal="100%" MaxLength="20" CtCls="requiredData" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:TextArea runat="server" ID="txtGhiChu" FieldLabel="Ghi chú" AnchorHorizontal="100%" />
            </Items>
            <Buttons>
                <ext:Button runat="server" ID="btnCapNhat" Text="Cập nhật" Icon="Disk">
                    <Listeners>
                        <Click Handler="return CheckInputThemMucLuongNgach();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="btnCapNhat_Click">
                            <EventMask ShowMask="true" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnEdit" Text="Cập nhật" Icon="Disk" Hidden="true">
                    <Listeners>
                        <Click Handler="return CheckInputThemMucLuongNgach();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="btnCapNhat_Click">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="Command" Value="Edit" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnCapNhatDongLai" Text="Cập nhật và đóng lại" Icon="Disk">
                    <Listeners>
                        <Click Handler="return CheckInputThemMucLuongNgach();" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="btnCapNhat_Click">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="Close" Value="True" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" ID="btnDL" Text="Đóng lại" Icon="Decline">
                    <Listeners>
                        <Click Handler="wdThemMoiMucLuongNgach.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Hide Handler="ResetWdThemMucLuongNgach();" />
            </Listeners>
        </ext:Window>
        <ext:Window runat="server" ID="wdConfig" Title="Thiết lập thang bảng lương" Icon="CogEdit"
            AutoHeight="true" Layout="FormLayout" Width="450" Padding="6" Hidden="true" Constrain="true"
            Modal="true" LabelWidth="5">
            <Items>
                <ext:DisplayField runat="server" ID="dpf2" Text="Chọn cấu hình thang bảng lương:" />
                <ext:RadioGroup ID="rdLoaiTenDN" runat="server" LabelAlign="Top" ColumnsNumber="1"
                    ItemCls="x-check-group-alt">
                    <Items>
                        <ext:Radio ID="rdSuDungTenTuSinh" runat="server" BoxLabel="Sử dụng thang bảng lương theo quy định ... của nhà nước"
                            Checked="true" Note="">
                        </ext:Radio>
                        <ext:Radio ID="rdSuDungEmail" runat="server" BoxLabel="Sử dụng thang bảng lương tự tạo">
                        </ext:Radio>
                    </Items>
                </ext:RadioGroup>
            </Items>
        </ext:Window>
        <ext:Store ID="Store1" AutoLoad="true" runat="server">
            <Proxy>
                <ext:HttpProxy Method="GET" Url="~/Services/HandlerSalaryLevelQuantum.ashx" />
            </Proxy>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="={0}" />
                <ext:Parameter Name="limit" Value="={80}" />
            </AutoLoadParams>
            <BaseParams>
                <ext:Parameter Name="max" Value="hdfMax.getValue()" Mode="Raw" />
                <ext:Parameter Name="searchKey" Value="txtSearchKey.getValue()" Mode="Raw" />
            </BaseParams>
            <Reader>
                <ext:JsonReader Root="Data" TotalProperty="TotalRecords" IDProperty="GroupQuantumId">
                    <Fields>
                        <ext:RecordField Name="GroupQuantumId" />
                        <ext:RecordField Name="QuantumId" />
                        <ext:RecordField Name="QuantumCode" />
                        <ext:RecordField Name="QuantumName" />
                       <%-- <ext:RecordField Name="TenNhomNgach" />--%>
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Viewport runat="server" ID="vp">
            <Items>
                <ext:BorderLayout runat="server" ID="brlayout">
                    <Center>
                        <ext:GridPanel ID="GridPanel1" runat="server" StripeRows="true" TrackMouseOver="true"
                            Title="Thang bảng lương" Icon="BookOpenMark" Width="1000" Height="400" Border="false"
                            StoreID="Store1">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="toolbarFn">
                                    <Items>
                                        <ext:Button runat="server" ID="btnAddQuantumn" Text="Thêm mới" Icon="Add">
                                            <Listeners>
                                                <Click Handler="btnCapNhat.show();btnEdit.hide();btnCapNhatDongLai.show();cbxNhomNgach.enable();cbxBac.enable();" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="mnuThemMoi_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnEditQuantum" Text="Sửa" Icon="Pencil" Disabled="true">
                                            <Listeners>
                                                <Click Handler="if (#{hdfIsSelected}.getValue() != 'true') { alert('Bạn chưa chọn mức lương nào!'); return false; } 
                                                    btnCapNhat.hide();btnEdit.show();btnCapNhatDongLai.hide(); cbxNhomNgach.disable();cbxBac.disable();" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="btnEditQuantum_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" ID="btnDeleteQuantum" Text="Xóa" Icon="Delete" Disabled="true">
                                            <Listeners>
                                                <Click Handler="if (#{hdfIsSelected}.getValue() != 'true') { alert('Bạn chưa chọn mức lương nào!'); return false; }" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="btnDeleteQuantum_Click">
                                                    <EventMask ShowMask="true" />
                                                    <Confirmation Title="Xác nhận" Message="Bạn có chắc chắn muốn xóa?" ConfirmRequest="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarFill runat="server" />
                                        <ext:TriggerField runat="server" Width="200" EnableKeyEvents="true" EmptyText="Nhập tên ngạch"
                                            ID="txtSearchKey">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <KeyPress Fn="enterKeyPressHandler" />
                                                <KeyUp Handler="if (txtSearchKey.getValue() != '') this.triggers[0].show();" />
                                                <TriggerClick Handler="if (index == 0) { this.reset(); this.triggers[0].hide(); PagingToolbar1.pageIndex = 0; PagingToolbar1.pageIndex = 0; #{Store1}.reload(); }" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:Button Text="Tìm kiếm" Icon="Zoom" runat="server" ID="btnSearch">
                                            <Listeners>
                                                <Click Handler="PagingToolbar1.pageIndex = 0; PagingToolbar1.pageIndex = 0; #{Store1}.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <ColumnModel runat="server" ID="ColumnModel1">
                                <Columns>
                                    <ext:RowNumbererColumn Width="35" Header="STT" Locked="true" />
                                    <ext:Column ColumnID="QuantumName" Width="200" Header="Tên ngạch" DataIndex="QuantumName"
                                        Locked="true">
                                        <Renderer Fn="RenderName" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CellSelectionModel runat="server" ID="CellSelectionModel1">
                                    <Listeners>
                                        <CellSelect Handler="#{hdfIsSelected}.setValue('true'); try{btnEditQuantum.enable();}catch(e){} try{btnDeleteQuantum.enable();}catch(e){}
                                            try{mnuThemMoi.enable();}catch(e){} try{mnuSua.enable();}catch(e){} try{mnuXoa.enable();}catch(e){}" />
                                    </Listeners>
                                </ext:CellSelectionModel>
                            </SelectionModel>
                            <View>
                                <ext:LockingGridView runat="server" ID="lkv">
                                </ext:LockingGridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                                    PageSize="30" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                                    DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="Số bản ghi trên 1 trang:" />
                                        <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                        <ext:ComboBox ID="ComboBoxPaging" runat="server" Width="80">
                                            <Items>
                                                <ext:ListItem Text="100" />
                                                <ext:ListItem Text="150" />
                                                <ext:ListItem Text="200" />
                                                <ext:ListItem Text="250" />
                                            </Items>
                                            <SelectedItem Value="80" />
                                            <Listeners>
                                                <Select Handler="#{PagingToolbar1}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.doLoad();" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:PagingToolbar>
                            </BottomBar>
                            <LoadMask ShowMask="true" Msg="Đang tải dữ liệu. Vui lòng chờ..." />
                            <Listeners>
                                <CellContextMenu Handler="
                                    e.preventDefault(); #{CellContextMenu}.showAt(e.getXY());"></CellContextMenu>
                            </Listeners>
                        </ext:GridPanel>
                    </Center>
                </ext:BorderLayout>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
