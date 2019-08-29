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
