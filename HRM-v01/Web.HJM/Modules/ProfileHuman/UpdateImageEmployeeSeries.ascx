<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.HJM.Modules.UserControl.UpdateImageEmployeeSeries" Codebehind="UpdateImageEmployeeSeries.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">
    var CheckValidFile = function (el) {
        var regex = /^.+\.(?:(?:[zZ][iI][pP]?)|(?:[rR][aA][rR]))$/;
        testFile = regex.test(CapNhatAnhHangLoat1_fufChonAnh.getValue());
        if (!testFile && CapNhatAnhHangLoat1_fufChonAnh.getValue().trim() != '') {
            alert('Phần mềm chỉ hỗ trợ hai định dạng nén .rar và .zip');
            CapNhatAnhHangLoat1_fufChonAnh.focus();
        }
        var size = 0;
        for (var num1 = 0; num1 < el.files.length; num1++) {
            var file = el.files[num1];
            size += file.size;
        }
        if (size > 0 && size <= 10485760) {
            CapNhatAnhHangLoat1_dpfSizeNow.setValue('<span style=\'color:green\'>Kích thước hiện tại: ' + parseFloat(size / 1048576).toFixed(2) + ' MB</span>');
        }
        else if (size > 10485760) {
            CapNhatAnhHangLoat1_dpfSizeNow.setValue('<span style=\'color:red\'>Kích thước hiện tại: ' + parseFloat(size / 1048576).toFixed(2) + ' MB</span>');
            alert('Kích thước của tệp tin vượt quá quy định');
        }
        else
            CapNhatAnhHangLoat1_dpfSizeNow.setValue('');
    }
    var CheckBeforeUpload = function (el) {
        if (CapNhatAnhHangLoat1_rdMaCanBo.checked == false && CapNhatAnhHangLoat1_rdMaPhong_Ten.checked == false && CapNhatAnhHangLoat1_rdSoCMND.checked == false) {
            alert('Bạn chưa chọn loại định dạng tên ảnh');
            CapNhatAnhHangLoat1_rdMaCanBo.focus();
            return false;
        }
        var size = 0;
        for (var num1 = 0; num1 < el.files.length; num1++) {
            var file = el.files[num1];
            size += file.size;
        }
        if (size > 10485760) {
            alert('Kích thước của tệp tin vượt quá quy định (10MB)');
            return false;
        }
        return true;
    }
</script>
<ext:Window runat="server" ID="wdCapNhatAnhHangLoat" Width="500" Icon="Images" Title="Cập nhật ảnh hàng loạt"
    AutoHeight="true" Padding="6" Modal="false" Constrain="true" Hidden="true" Layout="FormLayout">
    <Items>
        <ext:FormPanel ID="BasicForm" runat="server" Border="false" MonitorValid="true">
            <Items>
                <ext:FileUploadField ID="fufChonAnh" runat="server" Text="" FieldLabel="Chọn file nén"
                    Icon="ImageAdd" AnchorHorizontal="99%" AllowBlank="false" Regex="^.+\.(?:(?:[zZ][iI][pP]?)|(?:[rR][aA][rR]))$"
                    RegexText="Chỉ chấp nhận định dạng .zip và .rar" EmptyText="Chọn file zip hoặc file rar">
                    <Listeners>
                        <FileSelected Handler="CheckValidFile(this.fileInput.dom);" />
                    </Listeners>
                </ext:FileUploadField>
                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                    <Items>
                        <ext:Container runat="server" Layout="FitLayout" ColumnWidth="0.5" Height="20">
                            <Items>
                                <ext:DisplayField ID="DisplayField2" runat="server" Text="<span style='color:red'>(*)</span> Kích thước tối đa 10MB" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="FitLayout" ColumnWidth="0.5"
                            Height="20">
                            <Items>
                                <ext:DisplayField runat="server" ID="dpfSizeNow" Text="" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:FieldSet runat="server" Title="Chọn định dạng tên ảnh" AnchorHorizontal="99%"
                    Layout="RowLayout">
                    <Items>
                        <ext:RadioGroup ID="rdLoaiDinhDang" runat="server" LabelAlign="Top" ColumnsNumber="1"
                            ItemCls="x-check-group-alt">
                            <Items>
                                <ext:Radio ID="rdMaCanBo" runat="server" BoxLabel="[ Mã cán bộ ] (Ví dụ: DTH00001.jpg)" />
                                <ext:Radio ID="rdMaPhong_Ten" runat="server" BoxLabel="[ Mã phòng ] _ [ Họ và tên cán bộ ] (Ví dụ: PT_HoangThiHuyen.jpg)" />
                                <ext:Radio ID="rdSoCMND" runat="server" BoxLabel="[ Số chứng minh nhân dân ] (Ví dụ: 151777686.jpg)" />
                            </Items>
                        </ext:RadioGroup>
                    </Items>
                </ext:FieldSet>
                <ext:Container runat="server" Layout="FitLayout">
                    <Items>
                        <ext:DisplayField ID="DisplayField1" runat="server" AutoHeight="true" AnchorHorizontal="99%"
                            Text="<span style='color:red;'>Lưu ý: Phần mềm chỉ chấp nhận các định dạng ảnh jpg, png, gif, bmp, jpeg. Tên ảnh trong hai định dạng ở trên phải viết liền không dấu.</span>" />
                    </Items>
                </ext:Container>
            </Items>
            <Listeners>
                <ClientValidation Handler="#{btnCapNhatAnh}.setDisabled(!valid || (#{rdMaCanBo}.checked == false && #{rdMaPhong_Ten}.checked == false && #{rdSoCMND}.checked == false));" />
            </Listeners>
        </ext:FormPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnXemCBChuaCoAnh" Text="Xem cán bộ chưa có ảnh" Icon="Zoom">
            <DirectEvents>
                <Click OnEvent="btnXemCBChuaCoAnh_Click" />
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnCapNhatAnh" Text="Cập nhật" Icon="Disk">
            <Listeners>
                <Click Handler="return CheckBeforeUpload(#{fufChonAnh}.fileInput.dom);" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnCapNhatAnh_Click" Failure="Ext.Msg.show({ 
                                title   : 'Thông báo', 
                                msg     : 'Có lỗi xảy ra trong quá trình tải dữ liệu lên server', 
                                minWidth: 200, 
                                modal   : true, 
                                icon    : Ext.Msg.ERROR, 
                                buttons : Ext.Msg.OK 
                            });" IsUpload="true">
                    <EventMask ShowMask="true" />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="btnDongLai" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdCapNhatAnhHangLoat}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <DirectEvents>
        <Hide OnEvent="HideCapNhatAnhHangLoat" />
    </DirectEvents>
    <Listeners>
        <Hide Handler="#{fufChonAnh}.reset();#{rdMaCanBo}.reset();#{rdMaPhong_Ten}.reset();#{dpfSizeNow}.reset();" />
    </Listeners>
</ext:Window>
