var AddRecordClick = function (idTab) {
    var tabTitle = idTab.getActiveTab().id;
    switch (tabTitle) {
        case "employeeDetail_panelQuaTrinhDaoTao":
            employeeDetail_wdQuaTrinhDaoTao.show();
            break;
        case "employeeDetail_panelBaoHiem":
            employeeDetail_wdBaoHiem.show();
            break;
        case "employeeDetail_panelDaiBieu":
            employeeDetail_wdDaiBieu.show();
            break;
        case "employeeDetail_panelHopDong":
            employeeDetail_wdHopDong.show();
            employeeDetail_hdfTypeWindow.setValue('HopDong');
            break;
        case "employeeDetail_panelKhaNang":
            employeeDetail_wdKhaNang.show();
            break;
        case "employeeDetail_panelKhenThuong":
            employeeDetail_wdKhenThuong.show();
            employeeDetail_hdfTypeWindow.setValue('KhenThuong');
            break;
        case "employeeDetail_panelKiLuat":
            employeeDetail_wdKyLuat.show();
            employeeDetail_hdfTypeWindow.setValue('KyLuat');
            break;
        case "employeeDetail_panelQuanHeGiaDinh":
            employeeDetail_wdQuanHeGiaDinh.show();
            break;
        case "employeeDetail_panelQuaTrinhDieuChuyen":
            employeeDetail_wdQuaTrinhDieuChuyen.show();
            employeeDetail_hdfTypeWindow.setValue('QTDC');
            break;
        case "employeeDetail_panelTaiSan":
            employeeDetail_wdAddTaiSan.show();
            break;
        case "employeeDetail_panelTepDinhKem":
            employeeDetail_wdAttachFile.show();
            break;
        case "employeeDetail_panelBangCapChungChi":
            employeeDetail_wdAddChungChi.show();
            break;
        case "employeeDetail_panelKinhNghiemLamViec":
            employeeDetail_wdKinhNghiemLamViec.show();
            break;
        case "employeeDetail_panelQuaTrinhHocTap":
            employeeDetail_wdAddBangCap.show();
            break;
        case "employeeDetail_panelNgoaiNgu":
            employeeDetail_wdNgoaiNgu.show();
            break;
        case "employeeDetail_panelDiNuocNgoai":
            employeeDetail_wdDiNuocNgoai.show();
            break;
    }
}

var DeleteRecordOnGrid = function () {
    var tabTitle = employeeDetail_TabPanelBottom.getActiveTab().id;
    switch (tabTitle) {
        case "employeeDetail_panelQuaTrinhDaoTao":
            DeleteRecordOnGrid2(employeeDetail_GridPanelQTDT, employeeDetail_StoreQuaTrinhDaoTao);
            break;
        case "employeeDetail_panelBaoHiem":
            DeleteRecordOnGrid2(employeeDetail_GridPanelBaoHiem, employeeDetail_StoreBaoHiem);
            break;
        case "employeeDetail_panelDaiBieu":
            DeleteRecordOnGrid2(employeeDetail_GridPanelDaiBieu, employeeDetail_StoreDaiBieu);
            break;
        case "employeeDetail_panelHopDong":
            DeleteRecordOnGrid2(employeeDetail_GridPanelHopDong, employeeDetail_StoreHopDong);
            break;
        case "employeeDetail_panelKhaNang":
            DeleteRecordOnGrid2(employeeDetail_GridPanelKhaNang, employeeDetail_StoreKhaNang);
            break;
        case "employeeDetail_panelKhenThuong":
            DeleteRecordOnGrid2(employeeDetail_GridPanelKhenThuong, employeeDetail_StoreKhenThuong);
            break;
        case "employeeDetail_panelKiLuat":
            DeleteRecordOnGrid2(employeeDetail_GridPanelKyLuat, employeeDetail_StoreKyLuat);
            break;
        case "employeeDetail_panelQuanHeGiaDinh":
            DeleteRecordOnGrid2(employeeDetail_GridPanelQHGD, employeeDetail_StoreQHGD);
            break;
        case "employeeDetail_panelQuaTrinhDieuChuyen":
            DeleteRecordOnGrid2(employeeDetail_GridPanelQuaTrinhDieuChuyen, employeeDetail_StoreQuaTrinhDieuChuyen);
            break;
        case "employeeDetail_panelTaiSan":
            DeleteRecordOnGrid2(employeeDetail_GridPanelTaiSan, employeeDetail_StoreTaiSan);
            break;
        case "employeeDetail_panelBangCapChungChi":
            DeleteRecordOnGrid2(employeeDetail_GridPanel_ChungChi, employeeDetail_Store_BangCapChungChi);
            break;
        case "employeeDetail_panelTepDinhKem":
            DeleteRecordOnGrid2(employeeDetail_grpTepTinDinhKem, employeeDetail_grpTepTinDinhKemStore);
            break;
        case "employeeDetail_panelKinhNghiemLamViec":
            DeleteRecordOnGrid2(employeeDetail_GridPanelKinhNghiemLamViec, employeeDetail_StoreKinhNghiemLamViec);
            break;
        case "employeeDetail_panelQuaTrinhHocTap":
            DeleteRecordOnGrid2(employeeDetail_GridPanel_BangCap, employeeDetail_Store_BangCap);
            break;
        case "employeeDetail_panelNgoaiNgu":
            DeleteRecordOnGrid2(employeeDetail_grpNgoaiNgu, employeeDetail_StoreNgoaiNgu);
            break;
        case "employeeDetail_panelDienBienLuong":
            DeleteRecordOnGrid2(employeeDetail_GridPanelDienBienLuong, employeeDetail_StoreDienBienLuong);
            break;
        case "employeeDetail_panelDiNuocNgoai":
            DeleteRecordOnGrid2(employeeDetail_grpDiNuocNgoai, employeeDetail_StoregrpDiNuocNgoai);
            break;
    }
}

var DeleteRecordOnGrid2 = function (grid, store) {
    Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
        if (btn == "yes") {
            try {
                grid.getRowEditor().stopEditing();
            } catch (e) {

            }
            var s = grid.getSelectionModel().getSelections();
            for (var i = 0, r; r = s[i]; i++) {
                store.remove(r);
                store.commitChanges();
            }
        }
    });
}

var EditClick = function (DirectMethods) {
    var tabTitle = employeeDetail_TabPanelBottom.getActiveTab().id;
    switch (tabTitle) {
        case "employeeDetail_panelQuaTrinhDaoTao":
            var id = employeeDetail_GridPanelQTDT.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForDaoTao();
            btnDTInsert.hide();
            btnDTUpdateAndClose.hide();
            btnDTEdit.show();
            //wdQuaTrinhDaoTao.show();
            break;
        case "employeeDetail_panelBaoHiem":
            var id = employeeDetail_GridPanelBaoHiem.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForBaoHiem();
            employeeDetail_btnUpdateCongViec.hide(); employeeDetail_btnEditBaoHiem.show(); employeeDetail_btnCNVaDongBaoHiem.hide();
            break;
        case "employeeDetail_panelDaiBieu":
            var id = employeeDetail_GridPanelDaiBieu.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForDaiBieu(); employeeDetail_btnCapNhatDaiBieu.hide(); employeeDetail_btnEditDaiBieu.show(); employeeDetail_Button7.hide();
            break;
        case "employeeDetail_panelHopDong":
            var id = employeeDetail_GridPanelHopDong.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForHopDong();
            if (employeeDetail_cbHopDongLoaiHopDong.store.getCount() == 0) employeeDetail_cbHopDongLoaiHopDongStore.reload();
            if (employeeDetail_cbHopDongTinhTrangHopDong.store.getCount() == 0) employeeDetail_cbHopDongTinhTrangHopDongStore.reload();
            if (employeeDetail_cbHopDongCongViec.store.getCount() == 0) employeeDetail_storeJobTitle.reload();
            if (employeeDetail_cbx_HopDongChucVu.store.getCount() == 0) employeeDetail_cbx_HopDongChucVu_Store.reload();
            employeeDetail_btnUpdateHopDong.hide(); employeeDetail_btnEditHopDong.show(); employeeDetail_Button20.hide();
            employeeDetail_hdfTypeWindow.setValue('HopDong');
            break;
        case "employeeDetail_panelKhaNang":
            var id = employeeDetail_GridPanelKhaNang.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForKhaNang();
            if (employeeDetail_cbKhaNang.store.getCount() == 0) employeeDetail_cbKhaNangStore.reload();
            if (employeeDetail_cbKhaNangXepLoai.store.getCount() == 0) employeeDetail_cbKhaNangXepLoaiStore.reload();
            employeeDetail_btnUpdateKhaNang.hide();
            employeeDetail_btnEditKhaNang.show();
            employeeDetail_Button22.hide();
            //employeeDetail_wdKhaNang.show();
            break;
        case "employeeDetail_panelKhenThuong":
            var id = employeeDetail_GridPanelKhenThuong.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForKhenThuong();
            if (employeeDetail_cbLyDoKhenThuong.store.getCount() == 0) employeeDetail_cbLyDoKhenThuongStore.reload();
            if (employeeDetail_cbHinhThucKhenThuong.store.getCount() == 0) employeeDetail_cbHinhThucKhenThuongStore.reload();
            employeeDetail_btnUpdateKhenThuong.hide(); employeeDetail_btnEditKhenThuong.show();
            employeeDetail_Button24.hide();
            employeeDetail_hdfTypeWindow.setValue('KhenThuong');
            break;
        case "employeeDetail_panelKiLuat":
            var id = employeeDetail_GridPanelKyLuat.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForKyLuat();
            if (employeeDetail_cbLyDoKyLuat.store.getCount() == 0) { employeeDetail_cbLyDoKyLuatStore.reload(); };
            if (employeeDetail_cbHinhThucKyLuat.store.getCount() == 0) { employeeDetail_cbHinhThucKyLuatStore.reload(); };
            employeeDetail_btnCapNhatKyLuat.hide(); employeeDetail_btnEditKyLuat.show(); employeeDetail_Button26.hide();
            employeeDetail_hdfTypeWindow.setValue('KyLuat');
            break;
        case "employeeDetail_panelQuanHeGiaDinh":
            var id = employeeDetail_GridPanelQHGD.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForQuanHeGiaDinh();
            if (employeeDetail_cbQuanHeGiaDinh.store.getCount() == 0) { employeeDetail_cbQuanHeGiaDinhStore.reload(); };
            employeeDetail_btnUpdateQuanHeGiaDinh.hide();
            employeeDetail_btnUpdate.show();
            employeeDetail_Button28.hide();
            //employeeDetail_wdQuanHeGiaDinh.show();
            break;
        case "employeeDetail_panelQuaTrinhDieuChuyen":
            var id = employeeDetail_GridPanelQuaTrinhDieuChuyen.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForQuaTrinhDieuChuyen();
            employeeDetail_btnCapNhatQuaTrinhDieuChuyen.hide();
            employeeDetail_btnUpdateQuaTrinhDieuChuyen.show();
            employeeDetail_Button32.hide();
            employeeDetail_hdfTypeWindow.setValue('QTDC');
            break;
        case "employeeDetail_panelTaiSan":
            var id = employeeDetail_GridPanelTaiSan.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForTaiSan();
            employeeDetail_Button2.hide();
            employeeDetail_btnEditTaiSan.show();
            employeeDetail_btnUpdateTaiSan.hide();
            if (employeeDetail_cbTaiSan.store.getCount() == 0) { employeeDetail_cbTaiSanStore.reload(); };
            //employeeDetail_wdAddTaiSan.show();
            break;
        case "employeeDetail_panelBangCapChungChi":
            var id = employeeDetail_GridPanel_ChungChi.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForChungChi();
            if (employeeDetail_cbx_tenchungchi.store.getCount() == 0) { employeeDetail_cbx_tenchungchiStore.reload(); }
            if (employeeDetail_cbx_XepLoaiChungChi.store.getCount() == 0) { employeeDetail_cbx_XepLoaiChungChiStore.reload(); }
            employeeDetail_btnUpdateChungChi.hide(); employeeDetail_btnUpdateandCloseChungChi.hide(); employeeDetail_btnEditChungChi.show();
            break;
        case "employeeDetail_panelTepDinhKem":
            var id = employeeDetail_grpTepTinDinhKem.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForTepTin();
            employeeDetail_btnEditAttachFile.show(); employeeDetail_Button10.hide(); employeeDetail_btnUpdateAtachFile.hide();
            break;
        case "employeeDetail_panelKinhNghiemLamViec":
            var id = employeeDetail_GridPanelKinhNghiemLamViec.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForKinhNghiemLamViec();
            employeeDetail_Update.hide(); employeeDetail_UpdateandClose.hide(); employeeDetail_btnEditKinhNghiem.show();
            break;
        case "employeeDetail_panelQuaTrinhHocTap":
            var id = employeeDetail_GridPanel_BangCap.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForQuaTrinhHocTap();
            employeeDetail_btnUpdateBangCap.hide(); employeeDetail_btnUpdateandCloseBangCap.hide(); employeeDetail_btn_EditBangCap.show();
            break;
        case "employeeDetail_panelNgoaiNgu":
            var id = employeeDetail_grpNgoaiNgu.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForNgoaiNgu();
            employeeDetail_btnNgoaiNguInsert.hide(); employeeDetail_btnNgoaiNguClose.hide(); employeeDetail_btnNgoaiNguEdit.show();
            break;
        case "employeeDetail_panelDiNuocNgoai":
            var id = employeeDetail_grpDiNuocNgoai.getSelectionModel().getSelected();
            if (id == null) {
                alert('Bạn chưa chọn bản ghi nào');
                return;
            }
            DirectMethods.GetDataForDiNuocNgoai();
            employeeDetail_btn_InsertDNN.hide(); employeeDetail_btn_UpdateAndCloseDNN.hide(); employeeDetail_btn_updateDNN.show();
            break;
    }
}


var CheckSelectedRecord = function (grid, Store) {
    if (hdfRecordId.getValue() === '') {
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
    if (value === 'M')
        return nam;
    else
        return nu;
}
var enterKeyPressHandler1 = function (f, e) {
    if (e.getKey() === e.ENTER) {
        Ext.net.DirectMethods.SetValueQuery();
    }
}

var GetAge = function (birthday) {
    if (birthday === null) return "";
    birthday = birthday.replace(" ", "T");
    var temp = birthday.split("T");
    var date = temp[0].split("-");
    return new Date().getFullYear() * 1 - (date[0] * 1);
}

var getSelectedIndexRow = function () {
    var record = grp_HoSoNhanSu.getSelectionModel().getSelected();
    var index = grp_HoSoNhanSu.store.indexOf(record);
    if (index === -1)
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
    if (value === "1") {
        return sImageUnCheck;
    }
    else if (value === "0") {
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
    //resetGridPanel();
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
    if (value === null || value === "") {
        return "";
    }
    var keyword = document.getElementById("txtSearch").value;
    if (keyword === "" || keyword === "Nhập tên hoặc mã cán bộ")
        return value;

    var rs = "<p>" + value + "</p>";
    var keys = keyword.split(" ");
    for (i = 0; i < keys.length; i++) {
        if ($.trim(keys[i]) !== "") {
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

    if (o.words.length === 0) { return; }
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
                var url = "../../File/ImagesEmployee/" + rowSelection.getSelected().data.ImageUrl.replace('~/Modules', '..');
                employeeDetail_hsImage.setImageUrl(url);
            }
            else {
                employeeDetail_hsImage.setImageUrl("../../File/ImagesEmployee/No_person.jpg");
            }
            
            employeeDetail_txtEmployeeCode.setValue(rowSelection.getSelected().data.EmployeeCode);
            employeeDetail_txtFullName.setValue(rowSelection.getSelected().data.FullName);
            employeeDetail_txtAlias.setValue(rowSelection.getSelected().data.Alias);
            employeeDetail_txtBirthDate.setValue(RenderDate(rowSelection.getSelected().data.BirthDate, null, null));
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
            employeeDetail_txtRecruimentDate.setValue(RenderDate(rowSelection.getSelected().data.RecruimentDate, null, null));
            employeeDetail_txtRecruimentDepartment.setValue(rowSelection.getSelected().data.RecruimentDepartment);
            employeeDetail_txtPositionName.setValue(rowSelection.getSelected().data.PositionName);
            employeeDetail_txtCPVJoinedDate.setValue(RenderDate(rowSelection.getSelected().data.CPVJoinedDate, null, null));
            employeeDetail_txtCPVOfficialJoinedDate.setValue(RenderDate(rowSelection.getSelected().data.CPVOfficialJoinedDate, null, null));

            employeeDetail_txtCPVCardNumber.setValue(rowSelection.getSelected().data.CPVCardNumber);
            employeeDetail_txtCPVPositionName.setValue(rowSelection.getSelected().data.CPVPositionName);
            employeeDetail_txtVYUJoinedDate.setValue(RenderDate(rowSelection.getSelected().data.VYUJoinedDate, null, null));
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
            employeeDetail_txtArmyJoinedDate.setValue(RenderDate(rowSelection.getSelected().data.ArmyJoinedDate, null, null));
            employeeDetail_txtArmyLeftDate.setValue(RenderDate(rowSelection.getSelected().data.ArmyLeftDate, null, null));
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
            employeeDetail_txtInsuranceIssueDate.setValue(RenderDate(rowSelection.getSelected().data.InsuranceIssueDate, null, null));
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

function getJsonOfStore(store) {
    var datar = new Array();
    var jsonDataEncode = "";
    var records = store.getRange();
    for (var i = 0; i < records.length; i++) {
        datar.push(records[i].data);
    }
    jsonDataEncode = Ext.util.JSON.encode(datar);
    return jsonDataEncode;
}
var renderPercentage = function (value, p, record) { if (value == '' || value == null) { return ''; } else return "<span style='float:right;'>" + value + " %<span>" }
var RenderBacLuong = function (value, p, record) {
    if (value == '')
        return '';
    else
        return 'Bậc ' + value;
}
//var enterKeyPressHandler = function (f, e) {
//    if (e.getKey() == e.ENTER) {
//        PagingToolbar1.pageIndex = 0;
//        GridPanel1.getSelectionModel().clearSelections();
//        hdfRecordId.setValue('');
//        PagingToolbar1.doLoad();
//    }
//}
var GetDataFromStore = function (storeid) {
    var jsonData = storeid.data.items;
    var selectedItem = cbLoaiPhuCap.getValue();
    for (var i = 0; i < jsonData.length; i++) {
        if (selectedItem == jsonData[i].data.ID) {
            txtHeSoPhuCap.setValue(jsonData[i].data.HeSo);
            txtSoTien.setValue(jsonData[i].data.SoTien);
        }
    }
}
var updateRecord = function (grid) {
    try {
        if (cbxNgachHL.getValue() == '') {
            alert('Bạn chưa chọn Ngạch');
            cbxNgachHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (cbxBacLuongHL.getValue() == '') {
            alert('Bạn chưa chọn Bậc lương');
            cbxBacLuongHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (txtHeSoLuongHL.getValue() == '') {
            alert('Bạn chưa nhập hệ số lương');
            txtHeSoLuongHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (dfNgayHuongLuongHL.getValue() != '' && ValidateDateField(dfNgayHuongLuongHL) == false) {
            alert('Định dạng ngày hưởng lương không đúng');
            return false;
        }
    } catch (e) { }
    //    try {
    //        if (dfNgayHuongLuongHL.getValue() != '' && dfNgayHieuLucHL.getValue() != '' && (dfNgayHieuLucHL.getValue() > dfNgayHuongLuongHL.getValue())) {
    //            alert('Ngày hiệu lực phải trước hoặc trong ngày hưởng lương!');
    //            dfNgayHieuLucHL.focus();
    //            return false;
    //        }
    //    } catch (e) { }
    try {
        var maNgach = cbxNgachHL.getValue();
    } catch (e) { }
    try {
        var heSoLuong = isNaN(parseFloat(txtHeSoLuongHL.getValue())) == false ? parseFloat(txtHeSoLuongHL.getValue()) : 0;
    } catch (e) { }
    try {
        var luongDongBH = isNaN(parseFloat(txtLuongDongBHHL.getValue())) == false ? parseFloat(txtLuongDongBHHL.getValue()) : 0;
    } catch (e) { }
    try {
        var phucapcv = isNaN(parseFloat(txtPhuCapChucVuHL.getValue())) == false ? parseFloat(txtPhuCapChucVuHL.getValue()) : 0;
    } catch (e) { }
    try {
        var phuCapKhac = isNaN(parseFloat(txtPhuCapKhacHL.getValue())) == false ? parseFloat(txtPhuCapKhacHL.getValue()) : 0;
    } catch (e) { }
    try {
        var bacLuong = isNaN(parseFloat(cbxBacLuongHL.getValue())) == false ? parseFloat(cbxBacLuongHL.getValue()) : 0;
    } catch (e) { }
    try {
        var mucLuong = isNaN(parseFloat(txtMucLuongHL.getValue())) == false ? parseFloat(txtMucLuongHL.getValue()) : 0;
    } catch (e) { }
    try {
        var vuotKhung = isNaN(parseFloat(txtVuotKhungHL.getValue())) == false ? parseFloat(txtVuotKhungHL.getValue()) : 0;
    } catch (e) { }
    // datetime
    try {
        var tmp = new Date(dfNgayHuongLuongHL.getValue());
    } catch (e) { }
    var ngayHL;
    if (!isNaN(tmp))
        ngayHL = tmp;
    try {
        // update data
        var record = grid.getSelectionModel().getSelected();
        // update data for record
        addUpdatedRecord(record.data.RecordId, record.data.EmployeeCode, record.data.FullName, record.data.DepartmentName, record.data.PositionName, maNgach, bacLuong, heSoLuong, mucLuong, luongDongBH, ngayHL, phucapcv, phuCapKhac, vuotKhung);
    }
    catch (e) {
        alert('Bạn chưa chọn cán bộ nào');
    }
};

var updateAllRecord = function (grid) {
    try {
        if (cbxNgachHL.getValue() == '') {
            alert('Bạn chưa chọn Ngạch');
            cbxNgachHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (cbxBacLuongHL.getValue() == '') {
            alert('Bạn chưa chọn Bậc lương');
            cbxBacLuongHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (txtHeSoLuongHL.getValue() == '') {
            alert('Bạn chưa nhập hệ số lương');
            txtHeSoLuongHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (dfNgayHuongLuongHL.getValue() != '' && ValidateDateField(dfNgayHuongLuongHL) == false) {
            alert('Định dạng ngày hưởng lương không đúng');
            return false;
        }
    } catch (e) { }
    Ext.Msg.confirm('Xác nhận', 'Thông tin lương sẽ được cập nhật cho tất cả cán bộ trong danh sách. Bạn có chắc chắn muốn thực hiện không?', function (btn) {
        if (btn == "yes") {
            // get salary information
            try {
                var maNgach = cbxNgachHL.getValue();
            } catch (e) { }
            try {
                var heSoLuong = isNaN(parseFloat(txtHeSoLuongHL.getValue())) == false ? parseFloat(txtHeSoLuongHL.getValue()) : 0;
            } catch (e) { }
            try {
                var luongDongBH = isNaN(parseFloat(txtLuongDongBHHL.getValue())) == false ? parseFloat(txtLuongDongBHHL.getValue()) : 0;
            } catch (e) { }
            try {
                var phucapcv = isNaN(parseFloat(txtPhuCapChucVuHL.getValue())) == false ? parseFloat(txtPhuCapChucVuHL.getValue()) : 0;
            } catch (e) { }
            try {
                var phuCapKhac = isNaN(parseFloat(txtPhuCapKhacHL.getValue())) == false ? parseFloat(txtPhuCapKhacHL.getValue()) : 0;
            } catch (e) { }
            try {
                var bacLuong = isNaN(parseFloat(cbxBacLuongHL.getValue())) == false ? parseFloat(cbxBacLuongHL.getValue()) : 0;
            } catch (e) { }
            try {
                var mucLuong = isNaN(parseFloat(txtMucLuongHL.getValue())) == false ? parseFloat(txtMucLuongHL.getValue()) : 0;
            } catch (e) { }
            try {
                var vuotKhung = isNaN(parseFloat(txtVuotKhungHL.getValue())) == false ? parseFloat(txtVuotKhungHL.getValue()) : 0;
            } catch (e) { }
            // datetime
            try {
                var tmp = new Date(dfNgayHuongLuongHL.getValue());
            } catch (e) { }
            var ngayHL;
            if (!isNaN(tmp))
                ngayHL = tmp;
            // update data
            // var total = hdfTotalRecord.getValue() * 1;
            var total = grp_DanhSachQuyetDinh_Store.getCount();
            try {
                for (var i = 0; i < total; i++) {
                    grid.getSelectionModel().selectRow(i, true);
                    var record = grid.getSelectionModel().getSelected();
                    addUpdatedRecord(record.data.RecordId, record.data.EmployeeCode, record.data.FullName, record.data.DepartmentName, record.data.PositionName, maNgach, bacLuong, heSoLuong, mucLuong, luongDongBH, ngayHL, phucapcv, phuCapKhac, vuotKhung);
                }
            }
            catch (e) {
                alert('Bạn chưa chọn cán bộ nào');
            }
        }
    });
}

var updateRecordEnterprise = function (grid) {
    var maNgach = cbxNgachHL.getValue() != '' ? cbxNgachHL.getValue() : 0;
    try {
        var heSoLuong = isNaN(parseFloat(txtHeSoLuongHL.getValue())) == false ? parseFloat(txtHeSoLuongHL.getValue()) : 0;
    } catch (e) { }
    try {
        var luongDongBH = isNaN(parseFloat(txtLuongDongBHHL.getValue())) == false ? parseFloat(txtLuongDongBHHL.getValue()) : 0;
    } catch (e) { }
    try {
        var phucapcv = isNaN(parseFloat(txtPhuCapChucVuHL.getValue())) == false ? parseFloat(txtPhuCapChucVuHL.getValue()) : 0;
    } catch (e) { }
    try {
        var phuCapKhac = isNaN(parseFloat(txtPhuCapKhacHL.getValue())) == false ? parseFloat(txtPhuCapKhacHL.getValue()) : 0;
    } catch (e) { }
    try {
        var bacLuong = isNaN(parseFloat(cbxBacLuongHL.getValue())) == false ? parseFloat(cbxBacLuongHL.getValue()) : 0;
    } catch (e) { }
    try {
        var mucLuong = isNaN(parseFloat(txtMucLuongHL.getValue())) == false ? parseFloat(txtMucLuongHL.getValue()) : 0;
    } catch (e) { }
    try {
        var vuotKhung = isNaN(parseFloat(txtVuotKhungHL.getValue())) == false ? parseFloat(txtVuotKhungHL.getValue()) : 0;
    } catch (e) { }
    // datetime
    try {
        var tmp = new Date(dfNgayHuongLuongHL.getValue());
    } catch (e) { }
    var ngayHL;
    if (!isNaN(tmp))
        ngayHL = tmp;
    try {
        // update data
        var record = grid.getSelectionModel().getSelected();
        // update data for record
        addUpdatedRecord(record.data.RecordId, record.data.EmployeeCode, record.data.FullName, record.data.DepartmentName, record.data.PositionName, maNgach, bacLuong, heSoLuong, mucLuong, luongDongBH, ngayHL, phucapcv, phuCapKhac, vuotKhung);
    }
    catch (e) {
        alert('Bạn chưa chọn cán bộ nào');
    }
};

var updateAllRecordEnterprise = function (grid) {
    Ext.Msg.confirm('Xác nhận', 'Thông tin lương sẽ được cập nhật cho tất cả cán bộ trong danh sách. Bạn có chắc chắn muốn thực hiện không?', function (btn) {
        if (btn == "yes") {
            // get salary information
            try {
                var maNgach = cbxNgachHL.getValue() != '' ? cbxNgachHL.getValue() : 0;
            } catch (e) { }
            try {
                var heSoLuong = isNaN(parseFloat(txtHeSoLuongHL.getValue())) == false ? parseFloat(txtHeSoLuongHL.getValue()) : 0;
            } catch (e) { }
            try {
                var luongDongBH = isNaN(parseFloat(txtLuongDongBHHL.getValue())) == false ? parseFloat(txtLuongDongBHHL.getValue()) : 0;
            } catch (e) { }
            try {
                var phucapcv = isNaN(parseFloat(txtPhuCapChucVuHL.getValue())) == false ? parseFloat(txtPhuCapChucVuHL.getValue()) : 0;
            } catch (e) { }
            try {
                var phuCapKhac = isNaN(parseFloat(txtPhuCapKhacHL.getValue())) == false ? parseFloat(txtPhuCapKhacHL.getValue()) : 0;
            } catch (e) { }
            try {
                var bacLuong = isNaN(parseFloat(cbxBacLuongHL.getValue())) == false ? parseFloat(cbxBacLuongHL.getValue()) : 0;
            } catch (e) { }
            try {
                var mucLuong = isNaN(parseFloat(txtMucLuongHL.getValue())) == false ? parseFloat(txtMucLuongHL.getValue()) : 0;
            } catch (e) { }
            try {
                var vuotKhung = isNaN(parseFloat(txtVuotKhungHL.getValue())) == false ? parseFloat(txtVuotKhungHL.getValue()) : 0;
            } catch (e) { }
            // datetime
            try {
                var tmp = new Date(dfNgayHuongLuongHL.getValue());
            } catch (e) { }
            var ngayHL;
            if (!isNaN(tmp))
                ngayHL = tmp;
            // update data
            // var total = hdfTotalRecord.getValue() * 1;
            var total = grp_DanhSachQuyetDinh_Store.getCount();
            try {
                for (var i = 0; i < total; i++) {
                    grid.getSelectionModel().selectRow(i, true);
                    var record = grid.getSelectionModel().getSelected();
                    addUpdatedRecord(record.data.RecordId, record.data.EmployeeCode, record.data.FullName, record.data.DepartmentName, record.data.PositionName, maNgach, bacLuong, heSoLuong, mucLuong, luongDongBH, ngayHL, phucapcv, phuCapKhac, vuotKhung);
                }
            }
            catch (e) {
                alert('Bạn chưa chọn cán bộ nào');
            }
        }
    });
}

var onKeyUp = function (field) {
    var v = this.processValue(this.getValue()),
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

var ngachRenderer = function (value) {
    try {
        var r = cbxQuantumStore.getById(value);

        if (Ext.isEmpty(r)) {
            return "";
        }

        return r.data.Name;
    }
    catch (e) { return "" }
};
var loaiLuongRenderer = function (value) {
    try {
        var r = cbxLoaiLuongCu_Store.getById(value);

        if (Ext.isEmpty(r)) {
            return "";
        }

        return r.data.TEN_LOAI_LUONG;
    }
    catch (e) { return ""; }
};

var ValidateWdTaoQuyetDinhLuong = function () {
    try {
        if (cbxChonCanBo.getValue() == '') {
            alert('Bạn chưa chọn cán bộ nhận quyết định lương');
            cbxChonCanBo.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (txtSoQDMoi.getValue() == '') {
            alert('Bạn chưa nhập số quyết định');
            txtSoQDMoi.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (txtTenQDMoi.getValue() == '') {
            alert('Bạn chưa nhập tên quyết định');
            txtTenQDMoi.focus();
            return false;
        }
    } catch (e) { }
    try {
        if ((dfNgayHieuLucMoi.getValue() == '' || dfNgayHieuLucMoi.getValue() == null)) {
            alert('Bạn chưa chọn ngày hiệu lực');
            dfNgayHieuLucMoi.focus();
            return false;
        }
    } catch (e) { }
    //if (cbTrangThaiMoi.getValue() == '' || cbTrangThaiMoi.getValue() == null) {
    //    alert('Bạn chưa chọn trạng thái quyết định');
    //    cbTrangThaiMoi.focus();
    //    return false;
    //}
    try {
        if (!cbx_ngachMoi.getValue()) {
            alert('Bạn chưa chọn ngạch lương');
            cbx_ngachMoi.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (!cbxBacMoi.getValue()) {
            alert('Bạn chưa chọn bậc lương');
            cbxBacMoi.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (txtHeSoLuongMoi.getValue() == '') {
            alert('Bạn chưa nhập hệ số lương');
            txtHeSoLuongMoi.focus();
            return false;
        }
    } catch (e) { }
    //    try {
    //        if (txtMucLuongMoi.getValue() == '') {
    //            alert('Bạn chưa nhập mức lương');
    //            txtMucLuongMoi.focus();
    //            return false;
    //        }
    //    } catch (e) { }
    try {
        if (cbLoaiLuong.getValue() == '' || cbLoaiLuong.getValue() == null) {
            alert('Bạn chưa nhập loại lương');
            cbLoaiLuong.focus();
            return false;
        }
    } catch (e) { }
    // validate datefield
    try {
        if (ValidateDateField(dfNgayQDMoi) == false) {
            alert('Định dạng ngày quyết định không đúng');
            return false;
        }
    } catch (e) { }
    try {
        if (ValidateDateField(dfNgayHieuLucMoi) == false) {
            alert('Định dạng ngày hiệu lực không đúng');
            return false;
        }
    } catch (e) { }
    //    try {
    //        if (dfNgayQDMoi.getValue() != '' && dfNgayHieuLucMoi.getValue() != '' && (dfNgayQDMoi.getValue() > dfNgayHieuLucMoi.getValue())) {
    //            alert('Ngày quyết định phải trước hoặc trong ngày hiệu lực!');
    //            dfNgayQDMoi.focus();
    //            return false;
    //        }
    //    } catch (e) { }
    try {
        if (ValidateDateField(dfNgayHetHieuLucMoi) == false) {
            alert('Định dạng ngày hết hiệu lực không đúng');
            return false;
        }
    } catch (e) { }
    try {
        if (dfNgayHetHieuLucMoi.getValue() != '' && dfNgayHieuLucMoi.getValue() != '' && (dfNgayHieuLucMoi.getValue() > dfNgayHetHieuLucMoi.getValue())) {
            alert('Ngày hiệu lực phải trước hoặc trong ngày hết hiệu lực!');
            dfNgayHieuLucMoi.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (ValidateDateField(dfNgayHLMoi) == false) {
            alert('Định dạng ngày hưởng lương không đúng');
            return false;
        }
    } catch (e) { }
    //    try {
    //        if (dfNgayHLMoi.getValue() != '' && dfNgayHieuLucMoi.getValue() != '' && (dfNgayHieuLucMoi.getValue() > dfNgayHLMoi.getValue())) {
    //            alert('Ngày hiệu lực phải trước hoặc trong ngày hưởng lương!');
    //            dfNgayHieuLucMoi.focus();
    //            return false;
    //        }
    //    } catch (e) { }
    try {
        if (ValidateDateField(dfNgayHLNBMoi) == false) {
            alert('Định dạng ngày hưởng lương nội bộ không đúng');
            return false;
        }
    } catch (e) { }
    //    try {
    //        if (dfNgayHLNBMoi.getValue() != '' && dfNgayHieuLucMoi.getValue() != '' && (dfNgayHieuLucMoi.getValue() > dfNgayHLNBMoi.getValue())) {
    //            alert('Ngày hiệu lực phải trước hoặc trong ngày hưởng lương nội bộ!');
    //            dfNgayHieuLucMoi.focus();
    //            return false;
    //        }
    //    } catch (e) { }
    return true;
}
var checkInputThongTinLuongBeforeUpdate = function () {
    var records = grp_DanhSachQuyetDinh_Store.getRange();
    var rs = '';
    for (var i = 0; i < records.length; i++) {
        if (records[i].data.HeSoLuong == '') {
            alert('Bạn chưa nhập đầu đủ thông tin lương cho cán bộ!');
            cbxNgachHL.focus();
            return false;
        }
    }
    return true;
}

var ValidateWdTaoQuyetDinhLuongHangLoat = function () {
    try {
        if (txtSoQDHL.getValue() == '') {
            alert('Bạn chưa nhập số quyết định');
            txtSoQDHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if (txtTenQDHL.getValue() == '') {
            alert('Bạn chưa nhập tên quyết định');
            txtTenQDHL.focus();
            return false;
        }
    } catch (e) { }
    try {
        if ((dfNgayHieuLucHL.getValue() == '' || dfNgayHieuLucHL.getValue() == null)) {
            alert('Bạn chưa nhập ngày có hiệu lực');
            dfNgayHieuLucHL.focus();
            return false;
        }
    }
    catch (e) { }
    try {
        if (grp_DanhSachQuyetDinh_Store.getCount() == 0) {
            alert('Bạn chưa chọn cán bộ nhận quyết định');
            ucChooseEmployee1_wdChooseUser.show();
            return false;
        }
    } catch (e) { }
    //Kiểm tra thông tin lương đã được nhập đầy đủ hay chưa
    if (checkInputThongTinLuongBeforeUpdate() == false) {
        return false;
    }
    // validate datefield
    try {
        if (ValidateDateField(dfNgayQDHL) == false) {
            alert('Định dạng ngày quyết định không đúng');
            return false;
        }
    } catch (e) { }
    try {
        if (ValidateDateField(dfNgayHieuLucHL) == false) {
            alert('Định dạng ngày hiệu lực không đúng');
            return false;
        }
    } catch (e) { }
    //    try {
    //        if (dfNgayHieuLucHL.getValue() != '' && dfNgayQDHL.getValue() != '' && (dfNgayQDHL.getValue() > dfNgayHieuLucHL.getValue())) {
    //            alert('Ngày quyết định phải trước hoặc trong ngày hiệu lực!');
    //            dfNgayQDHL.focus();
    //            return false;
    //        }
    //    } catch (e) { }
    try {
        if (ValidateDateField(dfNgayHetHieuLucHL) == false) {
            alert('Định dạng ngày hết hiệu lực không đúng');
            return false;
        }
    } catch (e) { }
    try {
        if (dfNgayHetHieuLucHL.getValue() != '' && dfNgayHieuLucHL.getValue() != '' && (dfNgayHieuLucHL.getValue() > dfNgayHetHieuLucHL.getValue())) {
            alert('Ngày hiệu lực phải trước hoặc trong ngày hết hiệu lực!');
            dfNgayHieuLucHL.focus();
            return false;
        }
    } catch (e) { }
    return true;
}


var ResetWdTaoQuyetDinhLuong = function () {
    $('input[type=text]').val('');
    $('#textarea').val('');
    $('input[type=select]').val('');
    $('input[type=radio]').val('');
    $('input[type=checkbox]').val('');
}

var ResetWdTaoQuyetDinhLuongHangLoat = function () {
    grp_DanhSachQuyetDinh_Store.removeAll();
    fpTTQD.getForm().reset();
    fpn_QDLuongHL.getForm().reset();
    hdfTepTinDinhKemHL.reset();
    btnXoaCanBo.disable();
}

var ResetWdConfig = function () {
    chkLuongCung.reset(); chkHeSoLuong.reset(); chkPhuCapKhac.reset();
    chkLuongDongBHXH.reset(); chkBacLuong.reset(); chkBacLuongNB.reset(); chkNgayHL.reset();
    chkNgayHLNB.reset(); chkSoQD.reset(); chkNgayQD.reset(); chkNgayHieuLuc.reset();
    chkNgayHetHieuLuc.reset(); chkNguoiQD.reset();
}
var checkChooseEmployeeFirst = function () {
    if (grp_DanhSachQuyetDinh_Store.getCount() == 0) {
        alert('Bạn phải chọn cán bộ nhận quyết định trước');
        ucChooseEmployee1_wdChooseUser.show();
        return false;
    }
}
var resetFormQuyetDinhLuong = function () {
    txtSoQuyetDinh.reset(); dfNgayQuyetDinh.reset(); dfNgayHieuLuc.reset();
    txtTenQuyetDinh.reset(); cbNguoiQuyetDinh.reset(); txtHeSoLuong.reset();
    txtBacLuong.reset(); dfNgayHuongLuong.reset(); txtPhuCapKhacMoi.reset();
    cbLoaiLuong.reset(); cbHoTenNhanVien.reset(); txtLuongCung.reset();
    txtBacLuongNB.reset(); dfNgayHuongLuongNB.reset(); txtLuongDongBH.reset();
    hdfHoTenNhanVien.reset(); hdfNguoiQuyetDinh.reset(); txtVuotKhungMoi.reset();
}

var getSelectedIndexRow = function () {
    var record = grp_DanhSachQuyetDinh.getSelectionModel().getSelected();
    var index = grp_DanhSachQuyetDinh.store.indexOf(record);
    if (index == -1)
        return 0;
    return index;
}

var addRecord = function (RecordId, EmployeeCode, FullName, DepartmentName, PositionName, QuantumId, SalaryGrade, SalaryFactor, SalaryBasic,
    SalaryInsurance, SalaryPayDate, PositionAllowance, OtherAllowance, OutFrame) {

    var rowindex = getSelectedIndexRow();
    grp_DanhSachQuyetDinh.insertRecord(rowindex, {
        RecordId: RecordId,
        EmployeeCode: EmployeeCode,
        FullName: FullName,
        DepartmentName: DepartmentName,
        PositionName: PositionName,
        QuantumId: QuantumId,
        SalaryGrade: SalaryGrade,
        SalaryFactor: SalaryFactor,
        SalaryBasic: SalaryBasic,
        SalaryInsurance: SalaryInsurance,
        SalaryPayDate: SalaryPayDate,
        PositionAllowance: PositionAllowance,
        OtherAllowance: OtherAllowance,
        OutFrame: OutFrame
    });
    grp_DanhSachQuyetDinh.getView().refresh();
    grp_DanhSachQuyetDinh.getSelectionModel().selectRow(rowindex);
    grp_DanhSachQuyetDinh_Store.commitChanges();
}

addUpdatedRecord = function (RecordId, EmployeeCode, FullName, DepartmentName, PositionName, QuantumId, SalaryGrade, SalaryFactor, SalaryBasic,
    SalaryInsurance, SalaryPayDate, PositionAllowance, OtherAllowance, OutFrame) {
    var rowindex = getSelectedIndexRow();
    var prkey = 0;
    //xóa bản ghi cũ
    var s = grp_DanhSachQuyetDinh.getSelectionModel().getSelections();
    for (var i = 0, r; r = s[i]; i++) {
        prkey = r.data.RecordId;
        grp_DanhSachQuyetDinh_Store.remove(r);
        //grp_DanhSachQuyetDinh_Store.commitChanges();
    }
    //Thêm bản ghi đã update
    grp_DanhSachQuyetDinh.insertRecord(rowindex, {
        RecordId: RecordId,
        EmployeeCode: EmployeeCode,
        FullName: FullName,
        DepartmentName: DepartmentName,
        PositionName: PositionName,
        QuantumId: QuantumId,
        SalaryGrade: SalaryGrade,
        SalaryFactor: SalaryFactor,
        SalaryBasic: SalaryBasic,
        SalaryInsurance: SalaryInsurance,
        SalaryPayDate: SalaryPayDate,
        PositionAllowance: PositionAllowance,
        OtherAllowance: OtherAllowance,
        OutFrame: OutFrame
    });
    grp_DanhSachQuyetDinh.getView().refresh();
    grp_DanhSachQuyetDinh.getSelectionModel().selectRow(rowindex);
}

var RenderTepTinDinhKem = function (value, p, record) {
    if (value !== null && value !== '') {
        return "<img style='height:13px;' src='../../../Resource/images/attach.png' title='Bản ghi có tệp tin đính kèm'>";
    }
    return '';
}

var prepare = function (grid, command, record, row, col, value) {
    if (record.data.TepTinDinhKem == '' && command.command == "Download") {
        command.hidden = true;
        command.hideMode = "visibility";
    }
}

var updateDecisionSalaryEnterprise = function (grid) {
    var salaryBasic = isNaN(parseFloat(txtSalaryBasicHL.getValue())) == false ? parseFloat(txtSalaryBasicHL.getValue()) : 0;
    var salaryContract = isNaN(parseFloat(txtSalaryContractHL.getValue())) == false ? parseFloat(txtSalaryContractHL.getValue()) : 0;
    var salaryInsurance = isNaN(parseFloat(txtSalaryInsuranceHL.getValue())) == false ? parseFloat(txtSalaryInsuranceHL.getValue()) : 0;
    // datetime
    var tmp = new Date(SalaryPayDateHL.getValue());
    var salaryPayDate = null;
    if (!isNaN(tmp))
        salaryPayDate = tmp;
    try {
        // update data
        var record = grid.getSelectionModel().getSelected();
        // update data for record
        addUpdatedRecordSalary(record.data.RecordId, record.data.EmployeeCode, record.data.FullName, record.data.DepartmentName, record.data.PositionName, salaryBasic,
            salaryInsurance, salaryContract, salaryPayDate);
    }
    catch (e) {
        alert('Bạn chưa chọn cán bộ nào');
    }
};

var updateAllDecisionSalaryEnterprise = function (grid) {
    Ext.Msg.confirm('Xác nhận', 'Thông tin lương sẽ được cập nhật cho tất cả cán bộ trong danh sách. Bạn có chắc chắn muốn thực hiện không?', function (btn) {
        if (btn == "yes") {
            // get salary information
            var salaryBasic = isNaN(parseFloat(txtSalaryBasicHL.getValue())) == false ? parseFloat(txtSalaryBasicHL.getValue()) : 0;
            var salaryContract = isNaN(parseFloat(txtSalaryContractHL.getValue())) == false ? parseFloat(txtSalaryContractHL.getValue()) : 0;
            var salaryInsurance = isNaN(parseFloat(txtSalaryInsuranceHL.getValue())) == false ? parseFloat(txtSalaryInsuranceHL.getValue()) : 0;
            // datetime
            var tmp = new Date(SalaryPayDateHL.getValue());
            var salaryPayDate = null;
            if (!isNaN(tmp))
                salaryPayDate = tmp;
            // update data
            var total = gridListDecisionEmployee_Store.getCount();
            try {
                for (var i = 0; i < total; i++) {
                    grid.getSelectionModel().selectRow(i, true);
                    var record = grid.getSelectionModel().getSelected();
                    addUpdatedRecordSalary(record.data.RecordId, record.data.EmployeeCode, record.data.FullName, record.data.DepartmentName, record.data.PositionName, salaryBasic, salaryInsurance, salaryContract, salaryPayDate);
                }
            }
            catch (e) {
                alert('Bạn chưa chọn cán bộ nào');
            }
        }
    });
}

addUpdatedRecordSalary = function (RecordId, EmployeeCode, FullName, DepartmentName, PositionName, SalaryBasic,
    SalaryInsurance, SalaryContract, SalaryPayDate) {
    var rowindex = getSelectedIndexRowDecisionSalary();
    var prkey = 0;
    //xóa bản ghi cũ
    var s = gridListDecisionEmployee.getSelectionModel().getSelections();
    for (var i = 0, r; r = s[i]; i++) {
        prkey = r.data.RecordId;
        gridListDecisionEmployee_Store.remove(r);
    }
    //Thêm bản ghi đã update
    gridListDecisionEmployee.insertRecord(rowindex, {
        RecordId: RecordId,
        EmployeeCode: EmployeeCode,
        FullName: FullName,
        DepartmentName: DepartmentName,
        PositionName: PositionName,
        SalaryBasic: SalaryBasic,
        SalaryContract: SalaryContract,
        SalaryInsurance: SalaryInsurance,
        SalaryPayDate: SalaryPayDate
    });
    gridListDecisionEmployee.getView().refresh();
    gridListDecisionEmployee.getSelectionModel().selectRow(rowindex);
}

var addRecordDecisionSalary = function (RecordId, EmployeeCode, FullName, DepartmentName, PositionName, SalaryBasic, SalaryContract,
    SalaryInsurance, SalaryPayDate) {
    var rowindex = getSelectedIndexRowDecisionSalary();
    gridListDecisionEmployee.insertRecord(rowindex, {
        RecordId: RecordId,
        EmployeeCode: EmployeeCode,
        FullName: FullName,
        DepartmentName: DepartmentName,
        PositionName: PositionName,
        SalaryBasic: SalaryBasic,
        SalaryContract: SalaryContract,
        SalaryInsurance: SalaryInsurance,
        SalaryPayDate: SalaryPayDate
    });
    gridListDecisionEmployee.getView().refresh();
    gridListDecisionEmployee.getSelectionModel().selectRow(rowindex);
    gridListDecisionEmployee_Store.commitChanges();
}

var getSelectedIndexRowDecisionSalary = function () {
    var record = gridListDecisionEmployee.getSelectionModel().getSelected();
    var index = gridListDecisionEmployee.store.indexOf(record);
    if (index == -1)
        return 0;
    return index;
}

var checkChooseEmployeeDecisionSalaryFirst = function () {
    if (gridListDecisionEmployee_Store.getCount() == 0) {
        alert('Bạn phải chọn cán bộ nhận quyết định trước');
        ucChooseEmployee1_wdChooseUser.show();
        return false;
    }
}

var keyPresstxtSearch = function (f, e) {
    if (e.getKey() === e.ENTER) {
        ReloadGrid();
        if (this.getValue() === '') {
            this.triggers[0].hide();
        }
    }
    if (this.getValue() !== '') {
        this.triggers[0].show();
    }
}

var ReloadGrid = function () {
    PagingToolbar1.pageIndex = 0;
    PagingToolbar1.doLoad();
    RowSelectionModel1.clearSelections();
}

//Lấy định dạng ngày tháng bao gồm cả giờ 
var GetDateFormatIncludeTime = function (value, p, record) {
    try {
        if (value == null) return "";
        value = value.replace(" ", "T");
        var temp = value.split("T");
        var date = temp[0].split("-");
        var dateStr = date[2] + "/" + date[1] + "/" + date[0] + " " + temp[1];
        return dateStr;
    } catch (e) {

    }
}

var iconImg = function (name) {
    return "<img src='/Resource/icon/" + name + ".png'>";
}