var CheckSelectUser = function (hdf) {
    if (hdf.getValue() == '') {
        Ext.Msg.alert('Thông báo', 'Bạn chưa chọn nhân viên nào');
        return false;
    }
    return true;
}

var RenderTinhTrang = function (value, p, record) {
    if (value == "ChoDuyet") {
        return "Chờ duyệt";
    }
    else if (value == "DaDuyet") {
        return "<span style='color:blue;'>Đã duyệt</span>";
    }
    else if (value == "KhongDuyet")
        return "<span style='color:red;'>Không duyệt</span>";
    return value;
}

var SetSelectedKhoahoc = function (hiddenfield, gridPanel) {
    var s = gridPanel.getSelectionModel().getSelections();
    hiddenfield.reset();
    var t = "";
    for (var i = 0, r; r = s[i]; i++) {
        t += r.data.MaKeHoach + "," + r.data.KinhPhiCongTyHoTro + "," + r.data.KinhPhiNhanVienDong + "," + r.data.TenkeHoach + ";";
    }
    hiddenfield.setValue(t);
}

var CheckInputBaoHiem = function () {
    if (!employeeDetail_cbBHChucVu.getValue()) {
        alert('Bạn chưa chọn chức vụ');
        employeeDetail_cbBHCongViec.focus();
        return false;
    }
    if (employeeDetail_txtBHTyle.getValue() > 100 || employeeDetail_txtBHTyle.getValue() < 0) {
        alert("Tỷ lệ bảo hiểm trong khoảng từ 0 -> 100%");
        employeeDetail_txtBHTyle.focus();
        return false;
    }
    // bắt lỗi datefield
    if (ValidateDateField(employeeDetail_dfBHTuNgay) == false) {
        alert('Định dạng từ ngày không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfBHDenNgay) == false) {
        alert('Định dạng đến ngày không đúng');
        return false;
    }
    return true;
}
var CheckInputDaoTao = function () {
    if (employeeDetail_txtDTTenKhoaDaoTao.getValue().trim() == '') {
        alert('Bạn chưa nhập tên khóa đào tạo');
        employeeDetail_txtDTTenKhoaDaoTao.focus();
        return false;
    }
    // bắt lỗi datefield
    if (ValidateDateField(employeeDetail_dfDTTuNgay) == false) {
        alert('Định dạng từ ngày không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfDTDenNgay) == false) {
        alert('Định dạng đến ngày không đúng');
        return false;
    }
    return true;
}

var CheckInputDaiBieu = function () {
    if (employeeDetail_txtDBLoaiHinh.getValue().trim() == '') {
        alert('Bạn chưa nhập loại hình');
        employeeDetail_txtDBLoaiHinh.focus();
        return false;
    }
    if (employeeDetail_txtDBNhiemKy.getValue().trim() == '') {
        alert('Bạn chưa nhập nhiệm kỳ');
        employeeDetail_txtDBNhiemKy.focus();
        return false;
    }
    // bắt lỗi datefield
    if (ValidateDateField(employeeDetail_dfDBTuNgay) == false) {
        alert('Định dạng từ ngày không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfDBDenNgay) == false) {
        alert('Định dạng đến ngày không đúng');
        return false;
    }
    return true;
}

var CheckInputHopDong = function (el) {
    if (employeeDetail_txtHopDongSoHopDong.getValue().trim() == '') {
        alert('Bạn chưa nhập số hợp đồng');
        employeeDetail_txtHopDongSoHopDong.focus();
        return false;
    }
    if (!employeeDetail_cbHopDongLoaiHopDong.getValue()) {
        alert('Bạn chưa chọn loại hợp đồng');
        employeeDetail_cbHopDongLoaiHopDong.focus();
        return false;
    }
    if (employeeDetail_dfHopDongNgayHopDong.getValue() == '') {
        alert('Bạn chưa chọn ngày ký hợp đồng');
        employeeDetail_dfHopDongNgayHopDong.focus();
        return false;
    }
    if (employeeDetail_dfNgayCoHieuLuc.getValue() == '') {
        alert('Bạn chưa nhập ngày hợp đồng có hiệu lực');
        employeeDetail_dfNgayCoHieuLuc.focus();
        return false;
    }
    if (employeeDetail_txt_NguoiKyHD.getValue() == '') {
        alert('Bạn chưa nhập người đại diện đơn vị ký hợp đồng');
        employeeDetail_txt_NguoiKyHD.focus();
        return false;
    }
    // bắt lỗi datefield
    if (ValidateDateField(employeeDetail_dfHopDongNgayHopDong) == false) {
        alert('Định dạng ngày ký kết không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfNgayCoHieuLuc) == false) {
        alert('Định dạng ngày hiệu lực không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfHopDongNgayKiKet) == false) {
        alert('Định dạng ngày hết hợp đồng không đúng');
        return false;
    }
    var size = 0;
    for (var num1 = 0; num1 < el.files.length; num1++) {
        var file = el.files[num1];
        size += file.size;
    }
    if (size > 10485760) {
        alert('Phần mềm chỉ hỗ trợ các tệp tin đính kèm nhỏ hơn 10MB');
        return false;
    }
    return true;
}

var CheckInputKhaNang = function () {
    if (!employeeDetail_cbKhaNang.getValue()) {
        alert('Bạn chưa chọn khả năng');
        employeeDetail_cbKhaNang.focus();
        return false;
    }
    return true;
}

var CheckInputKhenThuong = function (el) { 
    if (!employeeDetail_cbLyDoKhenThuong.getValue() && !employeeDetail_cbLyDoKhenThuong.getRawValue()) {
        alert('Bạn chưa chọn lý do khen thưởng');
        employeeDetail_cbLyDoKhenThuong.focus();
        return false;
    } 
    if (!employeeDetail_cbHinhThucKhenThuong.getValue()) {
        alert('Bạn chưa chọn hình thức khen thưởng');
        employeeDetail_cbHinhThucKhenThuong.focus();
        return false;
    }
    //////
    if (ValidateDateField(employeeDetail_dfKhenThuongNgayQuyetDinh) == false) {
        alert('Định dạng ngày quyết định không đúng');
        return false;
    }
    var size = 0;
    for (var num1 = 0; num1 < el.files.length; num1++) {
        var file = el.files[num1];
        size += file.size;
    }
    if (size > 10485760) {
        alert('Phần mềm chỉ hỗ trợ các tệp tin đính kèm nhỏ hơn 10MB');
        return false;
    }
    return true;
}

var CheckInputKyLuat = function (el) { 
    if (!employeeDetail_cbLyDoKyLuat.getValue()) {
        alert('Bạn chưa chọn lý do kỷ luật');
        employeeDetail_cbLyDoKyLuat.focus();
        return false;
    }
    if (!employeeDetail_cbHinhThucKyLuat.getValue()) {
        alert('Bạn chưa chọn hình thức kỷ luật');
        employeeDetail_cbHinhThucKyLuat.focus();
        return false;
    }
    ///////
    if (ValidateDateField(employeeDetail_dfKyLuatNgayQuyetDinh) == false) {
        alert('Định dạng ngày quyết định không đúng');
        return false;
    }
    var size = 0;
    for (var num1 = 0; num1 < el.files.length; num1++) {
        var file = el.files[num1];
        size += file.size;
    }
    if (size > 10485760) {
        alert('Phần mềm chỉ hỗ trợ các tệp tin đính kèm nhỏ hơn 10MB');
        return false;
    }
    return true;
}

var CheckInputQHGD = function () {
    if (employeeDetail_txtQHGDHoTen.getValue().trim() == '') {
        alert('Bạn chưa nhập họ tên');
        employeeDetail_txtQHGDHoTen.focus();
        return false;
    }
    if (employeeDetail_cbQHGDGioiTinh.getValue().trim() == '') {
        alert('Bạn chưa chọn giới tính');
        employeeDetail_cbQHGDGioiTinh.focus();
        return false;
    }
    if (!employeeDetail_cbQuanHeGiaDinh.getValue()) {
        alert('Bạn chưa chọn quan hệ');
        employeeDetail_cbQuanHeGiaDinh.focus();
        return false;
    }
    if (employeeDetail_chkQHGDLaNguoiPhuThuoc.checked == true) {
        if (employeeDetail_dfQHGDBatDauGiamTru.getValue() == '') {
            alert('Bạn chưa nhập ngày bắt đầu giảm trừ');
            employeeDetail_dfQHGDBatDauGiamTru.focus();
            return false;
        }
    }
    ////////
    if (ValidateDateField(employeeDetail_dfQHGDBatDauGiamTru) == false) {
        alert('Định dạng ngày bắt đầu giảm trừ không đúng định dạng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfQHGDKetThucGiamTru) == false) {
        alert('Định dạng ngày kết thúc giảm trừ không đúng');
        return false;
    }
    return true;
}

var CheckInputQuaTrinhDieuChuyen = function (el) { 
    if (employeeDetail_dfQTDCNgayQuyetDinh.getValue() == '') {
        alert('Bạn chưa chọn ngày quyết định');
        employeeDetail_dfQTDCNgayQuyetDinh.focus();
        return false;
    }
    if (employeeDetail_dfQTDCNgayCoHieuLuc.getValue() == '') {
        alert('Bạn chưa nhập ngày có hiệu lực');
        employeeDetail_dfQTDCNgayCoHieuLuc.focus();
        return false;
    }

    if (employeeDetail_cbxQTDCBoPhanMoi.getValue() == null) {
        alert('Bạn chưa nhập bộ phận');
        employeeDetail_cbxQTDCBoPhanMoi.focus();
        return false;
    }
    ///////
    if (ValidateDateField(employeeDetail_dfQTDCNgayCoHieuLuc) == false) {
        alert('Định dạng ngày hiệu lực không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfQTDCNgayQuyetDinh) == false) {
        alert('Định dạng ngày quyết định không đúng');
        return false;
    }
    var size = 0;
    for (var num1 = 0; num1 < el.files.length; num1++) {
        var file = el.files[num1];
        size += file.size;
    }
    if (size > 10485760) {
        alert('Phần mềm chỉ hỗ trợ các tệp tin đính kèm nhỏ hơn 10MB');
        return false;
    }
    return true;
}

var CheckInputTaiSan = function () {
    if (employeeDetail_txtAssetName.getValue().trim() == '' || employeeDetail_txtAssetName.getValue() == null) {
        alert('Bạn chưa nhập tài sản');
        employeeDetail_txtAssetName.focus();
        return false;
    }
    if (employeeDetail_txtTaiSanSoLuong.getValue() == '' || employeeDetail_txtTaiSanSoLuong.getValue() == null) {
        alert('Bạn chưa nhập số lượng tài sản');
        employeeDetail_txtTaiSanSoLuong.focus();
        return false;
    }
    if (employeeDetail_txtTheTaiSan.getValue() == '' || employeeDetail_txtTheTaiSan.getValue() == null) {
        alert('Bạn chưa nhập thẻ tài sản');
        employeeDetail_txtTheTaiSan.focus();
        return false;
    }
    if (employeeDetail_txtUnitCode.getValue() == '' || employeeDetail_txtUnitCode.getValue() == null) {
        alert('Bạn chưa nhập đơn vị tính');
        employeeDetail_txtUnitCode.focus();
        return false;
    }
    if (employeeDetail_tsTxtinhTrang.getValue().trim() == '') {
        alert('Bạn chưa nhập tình trạng tài sản');
        employeeDetail_tsTxtinhTrang.focus();
        return false;
    }
    if (ValidateDateField(employeeDetail_tsDateField) == false) {
        alert('Định dạng ngày nhận không đúng');
        return false;
    }
    return true;
}

var CheckInputAttachFile = function () {
    if (employeeDetail_txtFileName.getValue().trim() == '') {
        alert('Bạn chưa nhập tên hiển thị');
        employeeDetail_txtFileName.focus();
        return false;
    }
    if (employeeDetail_file_cv.getValue().trim() == '') {
        alert('Bạn chưa chọn file đính kèm');
        return false;
    }
    return true;
}

var CheckInputBangCap = function () {
    if (employeeDetail_hdfMaTruongDaoTao.getValue() == '' || employeeDetail_hdfMaTruongDaoTao.getValue() == null) {
        alert('Bạn chưa chọn trường đào tạo');
        employeeDetail_cbx_NoiDaoTaoBangCap.focus();
        return false;
    }
    if (employeeDetail_cbx_hinhthucdaotaobang.getValue() == '' || employeeDetail_cbx_hinhthucdaotaobang.getValue() == null) {
        alert('Bạn chưa chọn hình thức đào tạo');
        employeeDetail_cbx_hinhthucdaotaobang.focus();
        return false;
    }
    if (employeeDetail_hdfMaChuyenNganh.getValue().trim() == '') {
        alert('Bạn chưa chọn chuyên ngành đào tạo');
        employeeDetail_cbx_ChuyenNganhBangCap.focus();
        return false;
    }
    if (employeeDetail_cbx_trinhdobangcap.getValue() == '' || employeeDetail_cbx_trinhdobangcap.getValue() == null) {
        alert('Bạn chưa chọn trình độ đào tạo');
        employeeDetail_cbx_trinhdobangcap.focus();
        return false;
    }
    if (employeeDetail_txtTuNam.getValue() != '' && employeeDetail_txtTuNam.getValue().length != 4) {
        alert('Định dạng từ năm không đúng');
        employeeDetail_txtTuNam.focus();
        return false;
    }
    if (employeeDetail_txtDenNam.getValue() != '' && employeeDetail_txtDenNam.getValue().length != 4) {
        alert('Định dạng đến năm không đúng');
        employeeDetail_txtDenNam.focus();
        return false;
    } 
    return true;
}

var CheckInputNgoaiNgu = function () {
    if (!employeeDetail_cbxNgoaiNgu.getValue()) {
        alert('Bạn chưa chọn loại ngoại ngữ');
        employeeDetail_cbxNgoaiNgu.focus();
        return false;
    }
    return true;
}

var CheckInputKinhNghiemLamViec = function () {
    if (employeeDetail_txt_noilamviec.getValue().trim() == '') {
        alert('Bạn chưa nhập nơi làm việc');
        employeeDetail_txt_noilamviec.focus();
        return false;
    }
    if (employeeDetail_txt_vitriconviec.getValue().trim() == '') {
        alert('Bạn chưa nhập vị trí công việc');
        employeeDetail_txt_vitriconviec.focus();
        return false;
    }
    if (employeeDetail_dfKNLVTuNgay.getValue() == '' || employeeDetail_dfKNLVTuNgay.getValue() == null) {
        alert('Bạn chưa nhập từ ngày');
        employeeDetail_dfKNLVTuNgay.focus();
        return false;
    }
    // validate datefield
    if (ValidateDateField(employeeDetail_dfKNLVTuNgay) == false) {
        alert('Định dạng từ ngày không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfKNLVDenNgay) == false) {
        alert('Định dạng đến ngày không đúng');
        return false;
    }
    return true;
}

var CheckInputChungChi = function () {
    if (!employeeDetail_cbx_certificate.getValue()) {
        alert('Bạn chưa chọn chứng chỉ');
        employeeDetail_cbx_certificate.focus();
        return false;
    }
    if (!employeeDetail_cbx_XepLoaiChungChi.getValue()) {
        alert('Bạn chưa chọn xếp loại');
        employeeDetail_cbx_XepLoaiChungChi.focus();
        return false;
    }
    
    if (ValidateDateField(employeeDetail_df_NgayCap) == false) {
        alert('Định dạng ngày cấp không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_df_NgayHetHan) == false) {
        alert('Định dạng ngày hết hạn không đúng');
        return false;
    }
    return true;
}
var CheckInputDiNuocNgoai = function () {
    if (!employeeDetail_cbx_DNN_MaNuoc.getValue()) {
        alert('Bạn chưa chọn quốc gia');
        employeeDetail_cbx_DNN_MaNuoc.focus();
        return false;
    }
    ///////
    if (ValidateDateField(employeeDetail_dfNgayBatDau) == false) {
        alert('Định dạng ngày bắt đầu không đúng');
        return false;
    }
    if (ValidateDateField(employeeDetail_dfNgayKetThuc) == false) {
        alert('Định dạng ngày ket thúc không đúng');
        return false;
    }
    return true;
}

var ConvertStringToDateFormat = function (value, p, record) {
    if (value == '' || value == null) return "";
    value = value.replace(" ", "T");
    var tmp = value.split("T");
    if (tmp[0] == '01/01/0001') return "";
    return tmp[0];
}

var ConvertStringToNumberFormart1 = function (inputControl) {
    try {
        var value = inputControl.getValue();
        if (value == null)
            return "";
        aler(inputControl.getValue());
        var l = (value + "").length - 3;
        var s = value + "";
        var rs = "";
        var count = 0;
        for (var i = l - 1; i >= 0; i--) {
            count++;
            if (count == 3) {
                rs = "," + s.charAt(i) + rs;
                count = 0;
            }
            else {
                rs = s.charAt(i) + rs;
            }
        }
        if (rs.charAt(0) == ',') {
            inputControl.setValue(rs.substring(1, rs.length));
        }
        inputControl.setValue(rs);
    } catch (e) {

    }
}

var ResetWdBaoHiem = function () {
     employeeDetail_dfBHTuNgay.reset(); employeeDetail_txtBHPhuCap.reset();
    employeeDetail_txtBHMucLuong.reset(); employeeDetail_cbBHChucVu.reset(); employeeDetail_dfBHDenNgay.reset();
    employeeDetail_txtBHHSLuong.reset(); employeeDetail_txtBHTyle.reset(); employeeDetail_txtBHGhiChu.reset();
}
var ResetWdQuaTrinhDaoTao = function () {
    employeeDetail_txtDTTenKhoaDaoTao.reset(); employeeDetail_dfDTTuNgay.reset(); employeeDetail_cbxDTQuocGia.reset();
    employeeDetail_dfDTDenNgay.reset(); employeeDetail_txtLyDoDaoTao.reset(); employeeDetail_txtNoiDaoTao.reset(); employeeDetail_txtDTGhiChu.reset(); 
}
var ResetWdDaiBieu = function () {
    employeeDetail_txtDBLoaiHinh.reset(); employeeDetail_dfDBTuNgay.reset(); employeeDetail_txtDBNhiemKy.reset();
    employeeDetail_dfDBDenNgay.reset(); employeeDetail_txtDBGhiChu.reset();
}

var ResetWdHopDong = function () {
    employeeDetail_txtHopDongSoHopDong.reset(); employeeDetail_cbHopDongLoaiHopDong.reset(); employeeDetail_cbHopDongTinhTrangHopDong.reset();
    employeeDetail_cbHopDongCongViec.reset(); employeeDetail_dfHopDongNgayHopDong.reset(); employeeDetail_dfHopDongNgayKiKet.reset();
    employeeDetail_dfNgayCoHieuLuc.reset();  employeeDetail_cbx_HopDongChucVu.reset();
    employeeDetail_fufHopDongTepTin.reset(); employeeDetail_cbx_HopDongTrangThai.reset(); employeeDetail_txtHopDongGhiChu.reset();
    employeeDetail_txt_NguoiKyHD.reset(); employeeDetail_hdfHopDongTepTinDK.reset();
}

var ResetWdKhaNang = function () {
    employeeDetail_cbKhaNang.reset();
    employeeDetail_cbKhaNangXepLoai.reset();
    employeeDetail_txtKhaNangGhiChu.reset();
}

var ResetWdKhenThuong = function () {
    employeeDetail_dfKhenThuongNgayQuyetDinh.reset();
     employeeDetail_fufKhenThuongTepTinDinhKem.reset();
     employeeDetail_hdfKhenThuongTepTinDinhKem.reset(); 
     ResetWindow();
     employeeDetail_hdfLyDoKTTemp.reset();
     employeeDetail_hdfIsDanhMuc.reset();
}

var ResetWdKyLuat = function () {
    employeeDetail_txtKyLuatGhiChu.reset();
    ResetWindow();
}

var ResetWindow = function () {
    $('input[type=text]').val('');
    $('#textarea').val('');
    $('input[type=select]').val('');
    $('input[type=radio]').val('');
    $('input[type=checkbox]').val('');
} 

var ResetWdQuanHeGiaDinh = function () {
    ResetWindow();
} 
var ResetWdQuaTrinhDieuChuyen = function () {
    ResetWindow();
    employeeDetail_txtDieuChuyenGhiChu.reset();
}

var ResetWdTaiSan = function () {
    employeeDetail_txtAssetName.reset();
    employeeDetail_tsDateField.reset();
    employeeDetail_tsTxtinhTrang.reset();
    employeeDetail_tsGhiChu.reset();
    employeeDetail_txtTaiSanSoLuong.reset();
    employeeDetail_txtUnitCode.reset();
    employeeDetail_txtTheTaiSan.reset();
}

var ResetWdAttachFile = function () {
    employeeDetail_txtFileName.reset(); employeeDetail_txtGhiChu.reset(); employeeDetail_file_cv.reset();
}

var ResetWdBangCap = function () {
    employeeDetail_cbx_NoiDaoTaoBangCap.reset(); employeeDetail_cbx_hinhthucdaotaobang.reset();
    employeeDetail_txt_khoa.reset(); employeeDetail_Chk_DaTotNghiep.Checked = false; employeeDetail_cbx_ChuyenNganhBangCap.reset();
    employeeDetail_cbx_trinhdobangcap.reset(); employeeDetail_cbx_xeploaiBangCap.reset(); employeeDetail_txtTuNam.reset(); employeeDetail_hdfQuocGia.reset();
    employeeDetail_txtDenNam.reset(); employeeDetail_cbx_quocgia.reset(); employeeDetail_hdfMaTruongDaoTao.reset(); employeeDetail_hdfMaChuyenNganh.reset();
}

var ResetWdNgoaiNgu = function () {
    employeeDetail_cbxNgoaiNgu.reset(); employeeDetail_txtXepLoaiNgoaiNgu.reset(); employeeDetail_txtGhiChuNgoaiNgu.reset();
}

var ResetWdKinhNghiemLamViec = function () {
    employeeDetail_txtGhiChuKinhNghiemLamViec.reset();
    employeeDetail_txtLyDoThoiViec.reset();
    employeeDetail_nbfMucLuong.reset();
    employeeDetail_txtThanhTichTrongCongViec.reset();
    employeeDetail_txt_vitriconviec.reset();
    employeeDetail_dfKNLVDenNgay.reset();
    employeeDetail_txt_noilamviec.reset();
    employeeDetail_dfKNLVTuNgay.reset();
}

var ResetWdChungChi = function () {
    employeeDetail_df_NgayCap.reset();
    employeeDetail_df_NgayHetHan.reset();
    employeeDetail_cbx_certificate.reset();
    employeeDetail_txt_EducationPlace.reset();
    employeeDetail_cbx_XepLoaiChungChi.reset();
    employeeDetail_txtGhiChuChungChi.reset();
}

var resetWdDiNuocNgoai = function () {
    employeeDetail_cbx_DNN_MaNuoc.reset();
    employeeDetail_dfNgayBatDau.reset();
    employeeDetail_dfNgayKetThuc.reset();
    employeeDetail_txtGoAboardReason.reset();
    employeeDetail_txtGoAboardNote.reset();
    employeeDetail_btn_updateDNN.hide();
    employeeDetail_btn_InsertDNN.show();
    employeeDetail_btn_UpdateAndCloseDNN.show();
}

var RenderTrangThaiHopDong = function (value, p, record) {
    if (value == 'DaDuyet')
        return '<span style="color:blue;"><b>Đã duyệt</b></span>';
    if (value == 'ChuaDuyet')
        return '<span style="color:red;"><b>Chưa duyệt</b></span>';
}

var RenderTepTinDinhKem = function (value, p, record) {
    if (value != null && value != '') {
        return "<img src='../../Resource/images/attach.png'>";
    }
    return '';
}    
var RemoveCanceledRecord = function (grid, Store) {
    if (employeeDetail_hdfButton.getValue() == 'insert') {
        try {
            grid.getRowEditor().stopEditing();
        } catch (e) {
            alert(e.Message.toString());
        }
        var s = grid.getSelectionModel().getSelections();
        for (var i = 0, r; r = s[i]; i++) {
            Store.remove(r);
        }
    }
}

var getSelectedIndexRow = function (grid) {
    var record = grid.getSelectionModel().getSelected();
    var index = grid.store.indexOf(record);
    if (index == -1)
        return 0;
    return index;
}

var GetRenderGT = function (value, p, record) {
    if (value == 'Nam')
        return '<span style="color:blue;">Nam</span>';
    if (value == 'Nữ')
        return '<span style="color:red;">Nữ</span>';
}
var GetFileNameUpload = function () {
    var fullPath = employeeDetail_file_cv.getValue();
    var arrStr = fullPath.split('.');
    if (arrStr.length >= 2) {
        employeeDetail_txtFileName.setValue(arrStr[arrStr.length - 2]);
    }
    else {
        employeeDetail_txtFileName.setValue(fullPath);
    }
}