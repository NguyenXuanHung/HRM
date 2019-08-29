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
        return false;
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
        case 'RecordDetail':
            wdShowReport.setTitle('Báo cáo hồ sơ cán bộ');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/ReportEmployeeDetail.aspx?rp=InfoEmployeeDetail&recordId=' + hdfRecordId.getValue(), 'Báo cáo hồ sơ nhân viên');
            break;
        case 'RecordDetailV2':
            wdShowReport.setTitle('Báo cáo hồ sơ cán bộ');
            pnReportPanel.remove(0); addHomePage(pnReportPanel, 'Homepage', '../Report/ReportEmployeeDetail.aspx?rp=InfoEmployeeDetailV2&recordId=' + hdfRecordId.getValue(), 'Báo cáo hồ sơ nhân viên');
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
var ResetControl = function () {
    cbx_bophanquanly.reset(); txt_macb.reset(); cbx_bophan.reset(); txtEmail.reset(); txtEmailRieng.reset();
    txt_hoten.reset(); txt_bidanh.reset(); dfNgaySinh.reset(); cbx_gioitinh.reset(); txtMobile.reset(); txtHomePhone.reset();
    cbx_NoiSinhXa.reset(); cbx_NoiSinhHuyen.reset(); cbx_NoiSinhTinh.reset(); cbx_QueQuanXa.reset(); txtCompanyPhone.reset();
    cbxQueQuanHuyen.reset(); cbx_QueQuanTinh.reset(); cbx_tthonnhan.reset(); cbx_dantoc.reset(); cbx_dantoc.clear();
    cbx_tongiao.reset(); cbx_tpbanthan.reset(); cbx_tpgiadinh.reset(); txt_hokhau.reset(); txt_diachilienhe.reset();
    txtNgheNghiepKhiDuocTuyenDung.reset(); date_tuyendau.reset(); txtCoQuanTuyenDung.reset(); cbx_chucvu.reset();
    txtCongViecChinhDuocGiao.reset(); hdfTrangThaiLamViec.reset(); cbx_TrangThaiLamViec.reset(); chkLaDangVien.reset();
    cbx_tdvanhoa.reset(); cbx_trinhdo.reset(); cbx_trinhdochinhtri.reset(); txtMaSoThueCaNhan.reset(); date_vaodang.disable();
    date_ngayvaodangct.disable(); txtTheDang.disable(); cbx_chuvudang.disable(); txt_noiketnapdang.disable();
    cbx_trinhdoquanly.reset(); cbx_ngoaingu.reset(); cbx_tinhoc.reset(); date_vaodang.reset(); date_ngayvaodangct.reset();
    txt_noiketnapdang.reset(); cbx_chuvudang.reset(); date_ngayvaodoan.reset(); cbx_chucvudoan.reset(); date_nhapngu.reset();
    date_xuatngu.reset(); cbx_bacquandoi.reset(); txtDanhHieuPhongTang.reset(); txt_SoTruongCongTac.reset(); cbx_ttsuckhoe.reset();
    txt_chieucao.reset(); txt_cannang.reset(); cbNhommau.reset(); txt_HangThuongTat.reset(); cbx_huongcs.reset(); txt_socmnd.reset();
    date_capcmnd.reset(); cbx_noicapcmnd.reset(); txt_sothebhxh.reset(); dfNgayCapBHXH.reset(); dfNgayBienChe.reset(); txtGhiChuTrangThaiLamViec.reset();
    txtTheDang.reset(); txtLichSuBanThan.reset(); txtLichSuThamGiaToChuc.reset(); txtLichSuThanNhan.reset(); txtNhanXet.reset();
    txtMaSoThueCaNhan.reset(); hdfTrangThaiLamViec.reset(); cbx_TrangThaiLamViec.reset(); dfNgayTrangThai.reset(); hdfPrKeyHoSo.setValue('');
    resetGrid(); txt_hoten.focus(); 
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


var ReloadStoreOfTabIndex = function () {
    //refresh lại store
    var tabTitle = employeeDetail_TabPanelBottom.getActiveTab().id;
    switch (tabTitle) {
        case "employeeDetail_panelGeneralInformation":
            var record = grp_HoSoNhanSu.getSelectionModel().getSelections();
            employeeDetail_txtEmployeeCode.setValue(record[0].data.EmployeeCode);
            employeeDetail_txtFullName.setValue(record[0].data.FullName);
            if (rowSelection.getSelected().data.ImageUrl != null) {
                employeeDetail_hsImage.setImageUrl(rowSelection.getSelected().data.ImageUrl.replace('~/Modules', '..'));
            }
            else {
                employeeDetail_hsImage.setImageUrl("../../../File/ImagesEmployee/No_person.jpg");
            }
            employeeDetail_txtEmployeeCode.setValue(rowSelection.getSelected().data.EmployeeCode);
            employeeDetail_txtFullName.setValue(rowSelection.getSelected().data.FullName);
            employeeDetail_txtAlias.setValue(rowSelection.getSelected().data.Alias);
            employeeDetail_txtBirthDate.setValue(rowSelection.getSelected().data.BirthDate);
            employeeDetail_txtBirthPlace.setValue(rowSelection.getSelected().data.BirthPlace);
            employeeDetail_txtHometown.setValue(rowSelection.getSelected().data.Hometown);

            employeeDetail_txtPersonalClassName.setValue(rowSelection.getSelected().data.PersonalClassName);
            employeeDetail_txtFamilyClassName.setValue(rowSelection.getSelected().data.FamilyClassName);
            employeeDetail_txtFolkName.setValue(rowSelection.getSelected().data.FolkName);
            employeeDetail_txtReligionName.setValue(rowSelection.getSelected().data.ReligionName);
            employeeDetail_txtResidentPlace.setValue(rowSelection.getSelected().data.ResidentPlace);
            employeeDetail_txtAddress.setValue(rowSelection.getSelected().data.Address);

            employeeDetail_txtBasicEducationName.setValue(rowSelection.getSelected().data.BasicEducationName);
            employeeDetail_txtEducationName.setValue(rowSelection.getSelected().data.EducationName);
            employeeDetail_txtPoliticLevelName.setValue(rowSelection.getSelected().data.PoliticLevelName);
            employeeDetail_txtManagementLevelName.setValue(rowSelection.getSelected().data.ManagementLevelName);
            employeeDetail_txtLanguageLevelName.setValue(record[0].data.LanguageLevelName);
            employeeDetail_txtITLevelName.setValue(rowSelection.getSelected().data.ITLevelName);
            break;
        case "employeeDetail_panelJobInformation":
            var record = grp_HoSoNhanSu.getSelectionModel().getSelections();
            employeeDetail_txtPreviousJob.setValue(rowSelection.getSelected().data.PreviousJob);
            employeeDetail_txtRecruimentDate.setValue(rowSelection.getSelected().data.RecruimentDate);
            employeeDetail_txtRecruimentDepartment.setValue(rowSelection.getSelected().data.RecruimentDepartment);
            employeeDetail_txtPositionName.setValue(rowSelection.getSelected().data.PositionName);
            employeeDetail_txtCPVJoinedDate.setValue(RenderDate(rowSelection.getSelected().data.CPVJoinedDate, null, null));
            employeeDetail_txtCPVOfficialJoinedDate.setValue(RenderDate(rowSelection.getSelected().data.CPVOfficialJoinedDate, null, null));

            employeeDetail_txtCPVCardNumber.setValue(rowSelection.getSelected().data.CPVCardNumber);
            employeeDetail_txtCPVPositionName.setValue(rowSelection.getSelected().data.CPVPositionName);
            employeeDetail_txtVYUJoinedDate.setValue(rowSelection.getSelected().data.VYUJoinedDate);
            employeeDetail_txtVYUPositionName.setValue(rowSelection.getSelected().data.VYUPositionName);
            employeeDetail_txtAssignedWork.setValue(rowSelection.getSelected().data.AssignedWork);
            //Ngach cong chuc
            employeeDetail_txtQuantumName.setValue(record[0].data.QuantumName);
            employeeDetail_txtQuantumCode.setValue(record[0].data.QuantumCode);
            employeeDetail_txtSalaryGrade.setValue(record[0].data.SalaryGrade);
            employeeDetail_txtSalaryFactor.setValue(record[0].data.SalaryFactor);
            employeeDetail_txtPositionAllowance.setValue(record[0].data.PositionAllowance);
            employeeDetail_txtOtherAllowance.setValue(record[0].data.OtherAllowance);
            employeeDetail_txtEffectiveDate.setValue(RenderDate(record[0].data.EffectiveDate, null, null));

            break;
        case "employeeDetail_panelPersonalInformation":
            var record = grp_HoSoNhanSu.getSelectionModel().getSelections();
            employeeDetail_txtIDNumber.setValue(rowSelection.getSelected().data.IDNumber);
            employeeDetail_txtIDIssueDate.setValue(RenderDate(rowSelection.getSelected().data.IDIssueDate, null, null));
            employeeDetail_txtIDIssuePlaceName.setValue(rowSelection.getSelected().data.IDIssuePlaceName);
            employeeDetail_txtArmyJoinedDate.setValue(rowSelection.getSelected().data.ArmyJoinedDate);
            employeeDetail_txtArmyLeftDate.setValue(rowSelection.getSelected().data.ArmyLeftDate);
            employeeDetail_txtArmyLevelName.setValue(rowSelection.getSelected().data.ArmyLevelName);

            employeeDetail_txtTitleAwarded.setValue(rowSelection.getSelected().data.TitleAwarded);
            employeeDetail_txtSkills.setValue(rowSelection.getSelected().data.Skills);
            employeeDetail_txtHealthStatusName.setValue(rowSelection.getSelected().data.HealthStatusName);
            employeeDetail_txtHeight.setValue(rowSelection.getSelected().data.Height);
            employeeDetail_txtWeight.setValue(rowSelection.getSelected().data.Weight);
            employeeDetail_txtBloodGroup.setValue(rowSelection.getSelected().data.BloodGroup);

            employeeDetail_txtRankWounded.setValue(rowSelection.getSelected().data.RankWounded);
            employeeDetail_txtFamilyPolicyName.setValue(rowSelection.getSelected().data.FamilyPolicyName);
            employeeDetail_txtInsuranceNumber.setValue(rowSelection.getSelected().data.InsuranceNumber);
            employeeDetail_txtInsuranceIssueDate.setValue(rowSelection.getSelected().data.InsuranceIssueDate);
            employeeDetail_txtWorkStatusName.setValue(record[0].data.WorkStatusName);
            employeeDetail_txtMaritalStatusName.setValue(rowSelection.getSelected().data.MaritalStatusName);
            break;
        case "employeeDetail_panelOtherInformation":
            employeeDetail_txtPersonalTaxCode.setValue(rowSelection.getSelected().data.PersonalTaxCode);
            employeeDetail_txtCellPhoneNumber.setValue(rowSelection.getSelected().data.CellPhoneNumber);
            employeeDetail_txtHomePhoneNumber.setValue(rowSelection.getSelected().data.HomePhoneNumber);
            employeeDetail_txtWorkPhoneNumber.setValue(rowSelection.getSelected().data.WorkPhoneNumber);
            employeeDetail_txtWorkEmail.setValue(rowSelection.getSelected().data.WorkEmail);
            employeeDetail_txtPersonalEmail.setValue(rowSelection.getSelected().data.PersonalEmail);

            employeeDetail_txtAccountNumber.setValue(rowSelection.getSelected().data.AccountNumber);
            employeeDetail_txtBankName.setValue(rowSelection.getSelected().data.BankName);
            employeeDetail_txtContactPersonName.setValue(rowSelection.getSelected().data.ContactPersonName);
            employeeDetail_txtContactRelation.setValue(rowSelection.getSelected().data.ContactRelation);
            employeeDetail_txtContactPhoneNumber.setValue(rowSelection.getSelected().data.ContactPhoneNumber);
            employeeDetail_txtContactAddress.setValue(rowSelection.getSelected().data.ContactAddress);
            break;
        case "employeeDetail_panelHoSoTuyenDung":
            employeeDetail_StoreHoSoTuyenDung.reload();
            break;
        case "employeeDetail_panelQuaTrinhDaoTao":
            employeeDetail_StoreQuaTrinhDaoTao.reload();
            break;
        case "employeeDetail_panelBaoHiem":
            employeeDetail_StoreBaoHiem.reload();
            break;
        case "employeeDetail_panelDaiBieu":
            employeeDetail_StoreDaiBieu.reload();
            break;
        case "employeeDetail_panelDanhGia":
            employeeDetail_StoreDanhGia.reload();
            break;
        case "employeeDetail_panelDienBienLuong":
            employeeDetail_StoreDienBienLuong.reload();
            break;
        case "employeeDetail_panelDeTai":
            employeeDetail_StoreDetai.reload();
            break;
        case "employeeDetail_panelHopDong":
            employeeDetail_StoreHopDong.reload();
            break;
        case "employeeDetail_panelKhaNang":
            employeeDetail_StoreKhaNang.reload();
            break;
        case "employeeDetail_panelKhenThuong":
            employeeDetail_StoreKhenThuong.reload();
            break;
        case "employeeDetail_panelKiLuat":
            employeeDetail_StoreKyLuat.reload();
            break;
        case "employeeDetail_panelQuanHeGiaDinh":
            employeeDetail_StoreQHGD.reload();
            break;
        case "employeeDetail_panelQuaTrinhCongTac":
            employeeDetail_StoreQuaTrinhCongTac.reload();
            break;
        case "employeeDetail_panelQuaTrinhDieuChuyen":
            employeeDetail_StoreQuaTrinhDieuChuyen.reload();
            break;
        case "employeeDetail_panelTaiSan":
            employeeDetail_StoreTaiSan.reload();
            break;
        case "employeeDetail_panelTaiNanLaoDong":
            employeeDetail_StoreTaiNanLaoDong.reload();
            break;
        case "employeeDetail_panelTepDinhKem":
            employeeDetail_grpTepTinDinhKemStore.reload();
            break;
        case "employeeDetail_panelBangCapChungChi":
            employeeDetail_Store_BangCapChungChi.reload();
            break;
        case "employeeDetail_panelKinhNghiemLamViec":
            employeeDetail_StoreKinhNghiemLamViec.reload();
            break;
        case "employeeDetail_panelQuaTrinhHocTap":
            employeeDetail_Store_BangCap.reload();
            break;
        case "employeeDetail_panelNgoaiNgu":
            employeeDetail_StoreNgoaiNgu.reload();
            break;
        case "employeeDetail_panelTheNganHang":
            employeeDetail_StoregrpATM.reload();
            break;
        case "employeeDetail_panelDiNuocNgoai":
            employeeDetail_StoregrpDiNuocNgoai.reload();
            break;
        case "employeeDetail_panelMoveBusiness":
            employeeDetail_hdfBusinessType.setValue('DieuDongDi');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelTurnover":
            employeeDetail_hdfBusinessType.setValue('LuanChuyenDi');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelSecondment":
            employeeDetail_hdfBusinessType.setValue('BietPhaiDi');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelPlurality":
            employeeDetail_hdfBusinessType.setValue('KiemNhiem');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelDismissal":
            employeeDetail_hdfBusinessType.setValue('MienNhiemBaiNhiem');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelTransfer":
            employeeDetail_hdfBusinessType.setValue('ThuyenChuyenDieuChuyen');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelRetirement":
            employeeDetail_hdfBusinessType.setValue('NghiHuu');
            employeeDetail_storeBusiness.reload();
            break;
        case "employeeDetail_panelEmulationTitle":
            employeeDetail_hdfBusinessType.setValue('DanhHieuThiDua');
            employeeDetail_storeBusiness.reload();
            break;
    }
}