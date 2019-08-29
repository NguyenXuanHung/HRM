var CheckSelectedRecord = function (grid, Store) {
    if (hdfRecordId.getValue() == '') {
        alert('Bạn chưa chọn cán bộ nào');
        return false;
    }

    var s = grid.getSelectionModel().getSelections();
    var count = 0;
    for (var i = 0, r; r = s[i]; i++) {
        count++;
    }
    if (count > 1) {
        alert('Bạn chỉ được chọn một cán bộ');
        return false;
    }
    return true;
}
var checkNgaySinh = function (datefield, tuoi) {
    var date = datefield.getRawValue();
    var age = getAge(date);
    if (age < tuoi) {
        alert("Nhập tuổi của CCVC phải lớn hơn " + tuoi + " tuổi! ");
        datefield.focus();
        return false
    } else {
        return true;
    }
}
function getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (age > 0) {
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
    }
    return age;
}
var ShowReportAction = function () {
    var type = hdfTypeReport.getValue();
    switch (type) {
        case 'HoSo':
            wdShowReport.setTitle('Báo cáo hồ sơ cán bộ');
            Console('TEST 1111');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/Baocao_Nhansu_Chitiet.aspx?prkey=' + hdfRecordId.getValue() + '&type=', 'Báo cáo hồ sơ nhân viên');
            break;
        case 'HoSo36':
            wdShowReport.setTitle('Báo cáo hồ sơ cán bộ');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/Soyeu_lylich_36.aspx?prkey=' + hdfRecordId.getValue(), 'Báo cáo hồ sơ nhân viên');
            break;
        case 'HoSoNew':
            wdShowReport.setTitle('Báo cáo hồ sơ cán bộ');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/Baocao_Nhansu_Chitiet.aspx?prkey=' + hdfRecordId.getValue() + '&type=new', 'Báo cáo hồ sơ nhân viên');
            break;
        case 'HoSo98':
            wdShowReport.setTitle('Báo cáo hồ sơ cán bộ');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/Soyeu_lylich_tctw98.aspx?prkey=' + hdfRecordId.getValue(), 'Báo cáo hồ sơ nhân viên');
            break;
        case 'TaiSan':
            wdShowReport.setTitle('Báo cáo tài sản cấp phát cho nhân viên');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/BaoCao_Main.aspx?type=DanhSachTaiSanCapPhatChoNhanVien&prkey=' + hdfRecordId.getValue(), 'Báo cáo tài sản cấp phát cho nhân viên');
            break;
        case 'DanhSachNhanSu':
            wdShowReport.setTitle('Báo cáo danh sách cán bộ');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/BaoCao_Main.aspx?type=DanhSachNhanVien&' + hdfQueryReport.getValue(), 'Báo cáo danh sách nhân viên');
            break;
    }
}
var enterKeyPressHandler = function (f, e) {
    if (e.getKey() == e.ENTER) {
        PagingToolbar1.pageIndex = 0;
        PagingToolbar1.doLoad();
        grp_HoSoNhanSu.getSelectionModel().clearSelections();
        hdfRecordId.setValue('');
    }
    if (txtSearch.getValue() != '')
        this.triggers[0].show();
}

var RenderGender = function (value, p, record) {
    var nam = "<span style='color:blue'>Nam</span>";
    var nu = "<span style='color:red'>Nữ</span>";
    if (value == 'M')
        return nam;
    else
        return nu;
}
var enterKeyPressHandler1 = function (f, e) {
    if (e.getKey() == e.ENTER) {
        Ext.net.DirectMethods.SetValueQuery();
    }
}

var GetAge = function (birthday) {
    if (birthday == null) return "";
    birthday = birthday.replace(" ", "T");
    var temp = birthday.split("T");
    var date = temp[0].split("-");
    return new Date().getFullYear() * 1 - (date[0] * 1);
}

var getSelectedIndexRow = function () {
    var record = grp_HoSoNhanSu.getSelectionModel().getSelected();
    var index = grp_HoSoNhanSu.store.indexOf(record);
    if (index == -1)
        return 0;
    return index;
}

addUpdatedRecord = function (ma_cb, ho_ten, ten_gioitinh, ngay_sinh, ten_bophan, ten_chucvu, ten_trinhdo, ten_chuyennganh, ten_ngach, ten_loai_hdong, dia_chi_lh, di_dong, email,
        bi_danh, ngaycap_hochieu, ngay_tuyen_dtien, ngay_tuyen_chinhthuc, ngaycap_cmnd, noi_cap_hc, noi_cap_cmnd, dt_cq, dt_nha) {
    var rowindex = getSelectedIndexRow();
    var prkey = 0;
    //xóa bản ghi cũ
    var s = grp_HoSoNhanSu.getSelectionModel().getSelections();
    for (var i = 0, r; r = s[i]; i++) {
        prkey = r.data.PR_KEY;
        store_HoSoNhanSu.remove(r);
        store_HoSoNhanSu.commitChanges();
    }
    //Thêm bản ghi đã update
    grp_HoSoNhanSu.insertRecord(rowindex, {
        PR_KEY: prkey,
        MA_CB: ma_cb,
        HO_TEN: ho_ten,
        MA_GIOITINH: ten_gioitinh,
        NGAY_SINH: ngay_sinh,
        TEN_BOPHAN: ten_bophan,
        TEN_CHUCVU: ten_chucvu,
        TEN_TRINHDO: ten_trinhdo,
        TEN_CHUYENNGANH: ten_chuyennganh,
        TEN_NGACH: ten_ngach,
        TEN_LOAI_HDONG: ten_loai_hdong,
        DIA_CHI_LH: dia_chi_lh,
        DI_DONG: di_dong,
        EMAIL: email,
        TUOI: 25,
        BI_DANH: bi_danh,
        NGAYCAP_HOCHIEU: ngaycap_hochieu,
        NGAY_TUYEN_DTIEN: ngay_tuyen_dtien,
        NGAY_TUYEN_CHINHTHUC: ngay_tuyen_chinhthuc,
        NGAYCAP_CMND: ngaycap_cmnd,
        TEN_NOICAP_HOCHIEU: noi_cap_hc,
        TEN_NOICAP_CMND: noi_cap_cmnd,
        DT_NHA: dt_nha,
        DT_CQUAN: dt_cq
    });
    grp_HoSoNhanSu.getView().refresh();
    grp_HoSoNhanSu.getSelectionModel().selectRow(rowindex);
    store_HoSoNhanSu.commitChanges();
}

var addRecord = function (ma_cb, ho_ten, ten_gioitinh, ngay_sinh, ten_bophan, ten_chucvu, ten_trinhdo, ten_chuyennganh, ten_ngach, ten_loai_hdong, dia_chi_lh, di_dong, email,
        bi_danh, ngaycap_hochieu, ngay_tuyen_dtien, ngay_tuyen_chinhthuc, ngaycap_cmnd, noi_cap_hc, noi_cap_cmnd, dt_cq, dt_nha) {
    var rowindex = getSelectedIndexRow();
    grp_HoSoNhanSu.insertRecord(rowindex, {
        MA_CB: ma_cb,
        HO_TEN: ho_ten,
        MA_GIOITINH: ten_gioitinh,
        NGAY_SINH: ngay_sinh,
        TEN_PHONG: ten_bophan,
        TEN_CHUCVU: ten_chucvu,
        TEN_TRINHDO: ten_trinhdo,
        TEN_CHUYENNGANH: ten_chuyennganh,
        TEN_NGACH: ten_ngach,
        TEN_LOAI_HDONG: ten_loai_hdong,
        DIA_CHI_LH: dia_chi_lh,
        DI_DONG: di_dong,
        EMAIL: email,
        TUOI: 25,
        BI_DANH: bi_danh,
        NGAYCAP_HOCHIEU: ngaycap_hochieu,
        NGAY_TUYEN_DTIEN: ngay_tuyen_dtien,
        NGAY_TUYEN_CHINHTHUC: ngay_tuyen_chinhthuc,
        NGAYCAP_CMND: ngaycap_cmnd,
        TEN_NOICAP_HOCHIEU: noi_cap_hc,
        TEN_NOICAP_CMND: noi_cap_cmnd,
        DT_NHA: dt_nha,
        DT_CQUAN: dt_cq
    });
    grp_HoSoNhanSu.getView().refresh();
    grp_HoSoNhanSu.getSelectionModel().selectRow(rowindex);
    store_HoSoNhanSu.commitChanges();
}

var RemoveItemOnGrid = function (grid, Store) {
    Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
        if (btn == "yes") {
            Ext.net.DirectMethods.DeleteRecord();
        }
    });
}
var GetMirrorBooleanIcon = function (value, p, record) {
    var sImageCheck = "<img  src='../../Resource/Images/check.png'>"
    var sImageUnCheck = "<img src='../../Resource/Images/uncheck.gif'>"
    if (value == "1") {
        return sImageUnCheck;
    }
    else if (value == "0") {
        return sImageCheck;
    }
    return "";
}
var resetGrid = function () {
    StoreDaoTao.removeAll();
    stQuaTrinhCongTacTruocDonVi.removeAll();
    StoreQHGD.removeAll();
    StoreDienBienLuong.removeAll();
    stNhanVienThamGiaDaoTao.removeAll();
    stKhaNang.removeAll();
    stBaoHiem.removeAll();
    stKhenThuong.removeAll();
    stKyLuat.removeAll();
    StoreQuaTrinhDieuChuyen.removeAll();
    stHopDongLaoDong.removeAll();
}

var ResetForm = function () {
    $('input[type=text]').val('');
    $('#textarea').val('');
    $('input[type=select]').val('');
    $('input[type=radio]').val('');
    $('input[type=checkbox]').val('');  
    resetGridPanel();
}
var ResetFormBusiness = function () {
    $('input[type=text]').val('');
    $('#textarea').val('');
    $('input[type=select]').val('');
    $('input[type=radio]').val('');
    $('input[type=checkbox]').val('');
}
var resetGridPanel = function () {
    storeInsurance.removeAll();
    StoreEducation.removeAll();
    storeWorkHistory.removeAll();
    storeFamilyRelationship.removeAll();
    StoreSalary.removeAll();
    storeTrainingHistory.removeAll();
    storeAbility.removeAll();
    storeReward.removeAll();
    storeDiscipline.removeAll();
    storeContract.removeAll();
    StoreWorkProcess.removeAll();
}
var RenderHightLight = function (value, p, record) {
    if (value == null || value == "") {
        return "";
    }
    var keyword = document.getElementById("txtSearch").value;
    if (keyword == "" || keyword == "Nhập tên hoặc mã cán bộ")
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
