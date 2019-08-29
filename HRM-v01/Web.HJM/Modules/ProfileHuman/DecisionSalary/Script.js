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
var renderPercentage = function (value, p, record)
{ if (value == '' || value == null) { return ''; }else return "<span style='float:right;'>"+value+" %<span>" }
var RenderBacLuong = function (value, p, record) {
    if (value == '')
        return '';
    else
        return 'Bậc ' + value;
}
var enterKeyPressHandler = function (f, e) {
    if (e.getKey() == e.ENTER) {
        PagingToolbar1.pageIndex = 0;
        GridPanel1.getSelectionModel().clearSelections();
        hdfRecordId.setValue('');
        PagingToolbar1.doLoad();
    }
}
var GetDataFromStore = function (storeid) {
    var jsonData = storeid.data.items;
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
    catch (e)
    { return "" }
};
var loaiLuongRenderer = function (value) {
    try {
        var r = cbxLoaiLuongCu_Store.getById(value);

        if (Ext.isEmpty(r)) {
            return "";
        }

        return r.data.TEN_LOAI_LUONG;
    }
    catch (e)
    { return ""; }
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
    //try {
    //    if ((dfNgayHieuLucMoi.getValue() == '' || dfNgayHieuLucMoi.getValue() == null)) {
    //        alert('Bạn chưa chọn ngày hiệu lực');
    //        dfNgayHieuLucMoi.focus();
    //        return false;
    //    }
    //} catch (e) { }
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
        if (records[i].data.SalaryFactor == '') {
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
    //try {
    //    if ((dfNgayHieuLucHL.getValue() == '' || dfNgayHieuLucHL.getValue() == null)) {
    //        alert('Bạn chưa nhập ngày có hiệu lực');
    //        dfNgayHieuLucHL.focus();
    //        return false;
    //    }
    //}
    //catch (e) { }
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
    if (value != null && value != '') {
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