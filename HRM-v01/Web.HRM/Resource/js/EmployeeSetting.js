$(document).ready(function () {
    // var w = $(window).width();
    // alert(w);
    // $("#RightCommand").css("display", "none");
});

var addDatetoDF2 = function () {
    var today = new Date();
    var month = today.getMonth() + 1;
    var now1 = today.getDate() + '/' + month + '/' + today.getFullYear();
    DateField2.setValue(now1);
}

var ResetForm = function () {
    $('input[type=text]').val('');
    $('#textarea').val('');
    $('input[type=select]').val('');
    $('input[type=radio]').val('');
    $('input[type=checkbox]').val('');
}

var ValidateInput = function () {
    if (txtEmployeeCode.getValue().trim() == '' || txtEmployeeCode.getValue() == null) {
        alert('Bạn chưa nhập mã cán bộ !');
        txtEmployeeCode.focus();
        return false;
    }
    if (txtFullName.getValue().trim() == '' || txtFullName.getValue() == null) {
        alert('Bạn chưa nhập họ tên !');
        txtFullName.focus();
        return false;
    }
    if (dfBirthDate.getRawValue() == '' || dfBirthDate.getRawValue() == null) {
        alert('Bạn chưa nhập ngày sinh');
        dfBirthDate.focus();
        return false;
    }
    if (ValidateDateField(dfBirthDate) == false) {
        alert('Định dạng ngày sinh không đúng');
        return false;
    }
    if (!cbxFolk.getValue()) {
        alert('Bạn chưa nhập dân tộc');
        cbxFolk.focus();
        return false;
    }
    if (txtIDNumber.getValue() == '') {
        alert('Bạn chưa nhập số chứng minh thư');
        txtIDNumber.focus();
        return false;
    }
  
    return true;
}

var ValidateInputHRM = function (type) {
    console.log(type);
    if (type === '0') {
        if (inputEmployee_txtEmployeeCode.getValue().trim() === '' ||
            inputEmployee_txtEmployeeCode.getValue() === null) {
            alert('Bạn chưa nhập mã nhân viên !');
            inputEmployee_txtEmployeeCode.focus();
            return false;
        }
    }

    if (type === '1') {
        if (inputEmployee_txtCandidateCode.getValue().trim() === '' ||
            inputEmployee_txtCandidateCode.getValue() === null) {
            alert('Bạn chưa nhập mã ứng viên !');
            inputEmployee_txtEmployeeCode.focus();
            return false;
        }
        if (inputEmployee_hdfRequiredRecruitmentId.getValue().trim() === '' ||
            inputEmployee_hdfRequiredRecruitmentId.getValue() === null) {
            alert('Bạn chưa chọn yêu cầu tuyển dụng !');
            inputEmployee_cboRequiredRecruitment.focus();
            return false;
        }
        if (inputEmployee_txtDesiredSalary.getValue() === '' ||
            inputEmployee_txtDesiredSalary.getValue() === null) {
            alert('Bạn chưa nhập mức lương mong muốn !');
            inputEmployee_txtDesiredSalary.focus();
            return false;
        }
        if (inputEmployee_dfApplyDate.getRawValue() === '' ||
            inputEmployee_dfApplyDate.getRawValue() === null) {
            alert('Bạn chưa chọn ngày nộp hồ sơ !');
            inputEmployee_dfApplyDate.focus();
            return false;
        }
        if (inputEmployee_hdfCandidateStatus.getValue().trim() === '' ||
            inputEmployee_hdfCandidateStatus.getValue() === null) {
            alert('Bạn chưa chọn trạng thái ứng viên !');
            inputEmployee_cboCandidateStatus.focus();
            return false;
        }
    }

    if (inputEmployee_txtFullName.getValue().trim() === '' || inputEmployee_txtFullName.getValue() === null) {
        alert('Bạn chưa nhập họ tên !');
        inputEmployee_txtFullName.focus();
        return false;
    }
    if (inputEmployee_dfBirthDate.getRawValue() === '' || inputEmployee_dfBirthDate.getRawValue() === null) {
        alert('Bạn chưa nhập ngày sinh');
        inputEmployee_dfBirthDate.focus();
        return false;
    }
  
    if (inputEmployee_txtIDNumber.getValue() === '') {
        alert('Bạn chưa nhập số chứng minh thư');
        inputEmployee_txtIDNumber.focus();
        return false;
    }
  
    return true;
}

var updateQuanHeGiaDinh = function (field, value) {
    var db = storeFamilyRelationship.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModelQHGD.getSelected().data.Id) {
            switch (field) {
                case "RelationshipId":
                    db[i].data.RelationshipId = value;
                    break;
            }
        }
    }
}
var updateSalaryProcess = function (field, value) {
    var db = StoreSalary.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel4.getSelected().data.Id) {
            switch (field) {
                case "QuantumId":
                    db[i].data.QuantumId = value;
                    break;
                case "GroupQuantumId":
                    db[i].data.GroupQuantumId = value;
                    break;
                case "Grade":
                    db[i].data.Grade = value;
                    break;
                case "Factor":
                    db[i].data.Factor = value;
                    break;
                case "QuantumCode":
                    db[i].data.QuantumCode = value;
                    break;
                case "QuantumName":
                    db[i].data.QuantumName = value;
                    break;
            }
        }
    }
}
var updateQTDaoTao = function (field, value) {
    var db = storeTrainingHistory.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModelQuaTrinhDaoTao.getSelected().data.Id) {
            switch (field) {
                case "NationId":
                    db[i].data.NationId = value;
                    break;
                case "TrainingSystemId":
                    db[i].data.TrainingSystemId = value;
                    break;
            }
        }
    }
}
var updateInsurance = function (field, value) {
    var db = storeInsurance.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel5.getSelected().data.Id) {
            switch (field) {
                case "PositionId":
                    db[i].data.PositionId = value;
                    break;
            }
        }
    }
}
var updateKhenThuong = function (field, value) {
    var db = storeReward.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel6.getSelected().data.Id) {
            switch (field) {
                case "LevelRewardId":
                    db[i].data.LevelRewardId = value;
                    break;
                case "FormRewardId":
                    db[i].data.FormRewardId = value;
                    break;
                default:
            }
        }
    }
}
var updateKyLuat = function (field, value) {
    var db = storeDiscipline.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel7.getSelected().data.Id) {
            switch (field) {
                case "LevelDisciplineId":
                    db[i].data.LevelDisciplineId = value;
                    break;
                case "FormDisciplineId":
                    db[i].data.FormDisciplineId = value;
                    break;
                default:
            }
        }
    }
}

var updateContract = function (field, value) {
    var db = storeContract.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel3.getSelected().data.Id) {
            switch (field) {
                case "ContractTypeId":
                    db[i].data.ContractTypeId = value;
                    if (db[i].data.Id < 0) {
                        db[i].data.Id = -i;
                    }
                    break;
                case "ContractStatusId":
                    db[i].data.ContractStatusId = value;
                    if (db[i].data.Id <= 0) {
                        db[i].data.Id = -i;
                    }
                    break;
                default:
            }
        }
    }
}
var updateGridCongTac = function (field, value) {
    var db = storeWorkHistory.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel1.getSelected().data.Id) {
            switch (field) {
                case "MaBoPhanMoi":
                    db[i].data.MaBoPhanMoi = value;
                    break;
                case "MaCongViecMoi":
                    db[i].data.MaCongViecMoi = value;
                    break;
                case "MaChucVuMoi":
                    db[i].data.MaChucVuMoi = value;
                    break;
                default:
            }
        }
    }
}
var updateGridNangLucST = function (field, value) {
    var db = storeAbility.getRange();
    console.log('tesst' + db + '---' + RowSelectionModelKhaNang.getSelected().data.Id);
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModelKhaNang.getSelected().data.Id) {
            switch (field) {
                case "AbilityId":
                    db[i].data.AbilityId = value;
                    break;
                case "GraduationTypeId":
                    db[i].data.GraduationTypeId = value;
                    break;
                default:
            }
        }
    }
}
var updateGridDatao = function (field, value) {
    var db = StoreEducation.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == RowSelectionModel2.getSelected().data.Id) {
            switch (field) {
                case "NationId":
                    db[i].data.NationId = value;
                    break;
                case "UniversityId":
                    db[i].data.UniversityId = value;
                    break;
                case "IndustryId":
                    db[i].data.IndustryId = value;
                    break;
                case "EducationId":
                    db[i].data.EducationId = value;
                    break;
                case "TrainingSystemId":
                    db[i].data.TrainingSystemId = value;
                    break;
                default:
            }
        }
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
var loadGridPanel = function () {
    if (hdfPrKeyHoSo.getValue() == '') {
        return;
    }
    if (StoreEducation.getCount() == 0) {
        StoreEducation.reload();
    }
    if (storeWorkHistory.getCount() == 0) {
        storeWorkHistory.reload();
    }
    if (storeFamilyRelationship.getCount() == 0) {
        storeFamilyRelationship.reload();
    }
    if (StoreSalary.getCount() == 0) {
        StoreSalary.reload();
    }
    if (storeTrainingHistory.getCount() == 0) {
        storeTrainingHistory.reload();
    }
    if (storeAbility.getCount() == 0) {
        storeAbility.reload();
    }
    if (storeInsurance.getCount() == 0) {
        storeInsurance.reload();
    }
    if (storeReward.getCount() == 0) {
        storeReward.reload();
    }
    if (storeDiscipline.getCount() == 0) {
        storeDiscipline.reload();
    }
    if (storeContract.getCount() == 0) {
        storeContract.reload();
    }
    if (StoreWorkProcess.getCount() == 0) {
        StoreWorkProcess.reload();
    }
}
var hideLeftCommand = function () {
    if (hdfMaCB.getValue() != '') {
        $("#LeftCommand").css("display", "none");
    }
}
var warningUseDepartment = function (combo, hdf) {
    if (combo.getValue() * 1 < 0) {
        alert('Bạn không có quyền thao tác với đơn vị này');
        hdf.reset(); combo.reset();
        return false;
    }
    else {
        hdf.setValue(combo.getValue());
    }
}
var searchBoxKT = function (f, e) {
    hdfLyDoKTTemp.setValue(cbLyDoKhenThuong.getRawValue());
    if (hdfIsDanhMuc.getValue() == '1') {
        hdfIsDanhMuc.setValue('2');
    }
    if (cbLyDoKhenThuong.getRawValue() == '') {
        hdfIsDanhMuc.reset();
    }
}
var showCreateSample = function () {
    iBtnSaveSample.show();
    iBtnCancelSample.show();
    iBtnSave.hide();
    iBtnSaveAndClear.hide();
    iBtnClear.hide();
}
var hideCreateSample = function () {
    iBtnSaveSample.hide();
    iBtnCancelSample.hide();
    iBtnSave.show();
    iBtnSaveAndClear.show();
    iBtnClear.show();
    $("#aLinkTaoMau").css("display", "block");
    $("#help").css("display", "none");
    //ResetControl();
    ResetForm();
}
var gridsSave = function () {
    GridInsurance.save();
    GridEducation.save();
    GridPanelFamilyRelationship.save();
    GridPanelSalary.save();
    gridAbility.save();
    GridWorkHistory.save();
    gridTrainingHistory.save();
    GridContract.save();
    GridReward.save();
    GridDiscipline.save();
    GridWorkProcess.save();
}
var GetRendererTrueFalse = function (value) {
    var sImageCheck = "<img src='../../Resource/images/check.png' />"
    var sImageUnCheck = "<img src='../../Resource/Images/uncheck.gif'>"
    switch (value) {
        case true:
            return sImageCheck;
            break;
        default:
            return sImageUnCheck;
            break;
    }
}
var nationRenderer = function (value) {
    var result = storeNation.getById(value);
    if (Ext.isEmpty(result)) {
        return value;
    }
    return result.data.Name;
}

var loaiHinhDaoTaoRenderer = function (value) {
    var r = cbxLoaiHinhDaoTaoStore.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}

var ngachRenderer = function (value) {
    var r = storeQuantum.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var bacRenderer = function (value) {
    var r = cbxSalaryGradeStore.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.TEN;
}
var truongRenderer = function (value) {
    var r = storeUniversity.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var ChuyenNganhRenderer = function (value) {
    var r = storeIndustry.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var TrinhDoRenderer = function (value) {
    var r = storeEducation2.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var HinhThucDaoTaoRenderer = function (value) {
    var r = storeTrainingSystem.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var XepLoatRenderer = function (value) {
    var r = cbx_xeploai_Store.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.TEN;
}
var QuanHeGiaDinhRenderer = function (value) {
    var r = store_CbxFamilyRelationship.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}

var MucDatRenderer = function (value) {
    var r = cbKhaNangXepLoaiStore.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var CongViecRenderer = function (value) {
    var r = Store13.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.TEN;
}
var PositionRenderer = function (value) {
    var r = StorePosition.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var LevelRewardDisciplineRenderer = function (value) {
    var r = StoreLevelRewardDiscipline.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var HinhThucKhenThuongRenderer = function (value) {
    var r = store_CbxFormReward.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var HinhThucKyLuatRenderer = function (value) {
    var r = store_CbxFormDiscipline.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var BoPhanMoiRenderer = function (value) {
    var r = cbxQTDCBoPhanCu_Store.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.TEN;
}
var ChucVuMoiRenderer = function (value) {
    var r = cbxQTDCChucVuCu_Store.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.TEN;
}
var CongViecMoiRenderer = function (value) {
    var r = cbxCongViecMoi_Store.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.TEN;
}
var LoaiHopDongRenderer = function (value) {
    var r = store_CbxContract.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var TinhTrangHopDongRenderer = function (value) {
    var r = store_CbxContractStatus.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}
var KhaNangRenderer = function (value) {
    var r = store_CbxAbility.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}

var getSelectedIndexRow = function (gird) {
    var record = gird.getSelectionModel().getSelected();
    var index = gird.store.indexOf(record);
    if (index == -1)
        return 0;
    return index;
}
var prepare = function (grid, command, record, row, col, value) {
    if (record.data.TepTinDinhKem == '' && command.command == "Download") {
        command.hidden = true;
        command.hideMode = "visibility";
    }
}

var CheckInputCreateSample = function () {
    if (txtSampleName.getValue() == '') {
        alert('Bạn chưa nhập tên mẫu');
        txtSampleName.focus();
        return false;
    } else {
        showCreateSample();
        wdCreateSample.hide();
        $("#help").css("display", "block");
        $("#aLinkTaoMau").css("display", "none");
        if (ckClearValue.getValue() == true) {
            //ResetControl();
            ResetForm();
        }
    }
    return true;
}
var iBtnCancelSample_Click = function () {
    hideCreateSample();
    $("#aLinkTaoMau").css("display", "block");
    $("#help").css("display", "none");
}
var showWdAddCategory = function (name) {
    switch (name) {
        case "DanToc":
            hdfTableDM.setValue("cat_Folk");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TonGiao":
            hdfTableDM.setValue("cat_Religion");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ThanhPhanBanThan":
            hdfTableDM.setValue("cat_PersonalClass");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ThanhPhanGiaDinh":
            hdfTableDM.setValue("cat_FamilyClass");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ChucVu":
            hdfTableDM.setValue("cat_Position");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ChucDanh":
            hdfTableDM.setValue("cat_JobTitle");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TrinhDoVanHoa":
            hdfTableDM.setValue("cat_BasicEducation");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TrinhDo":
            hdfTableDM.setValue("cat_Education");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TrinhDoChinhTri":
            hdfTableDM.setValue("cat_PoliticLevel");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TrinhDoQuanLy":
            hdfTableDM.setValue("cat_ManagementLevel");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "NgoaiNgu":
            hdfTableDM.setValue("cat_LanguageLevel");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TinHoc":
            hdfTableDM.setValue("cat_ITLevel");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ChucVuDang":
            hdfTableDM.setValue("cat_CPVPosition");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ChucVuDoan":
            hdfTableDM.setValue("cat_VYUPosition");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "CapBacQuanDoi":
            hdfTableDM.setValue("cat_ArmyLevel");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "TinhTrangSucKhoe":
            hdfTableDM.setValue("cat_HealthStatus");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "ChinhSach":
            hdfTableDM.setValue("cat_FamilyPolicy");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
        case "NoiCapCMND":
            hdfTableDM.setValue("cat_IDIssuePlace");
            hdfColMa.setValue("Id");
            hdfColTen.setValue("Name");
            break;
    }
    wdAddCategory.show();
}

var resetInputCategory = function () {
    btnCancel.hide();
    btnSave.hide();
    txtTenDM.hide();
    txtTenDM.reset();
    btnAddCategory.enable();
}

var CheckSelectedRows = function (grid) {
    var s = grid.getSelectionModel().getSelections();
    var count = 0;
    for (var i = 0, r; r = s[i]; i++) {
        count++;
    }
    if (count == 0) {
        alert('Bạn chưa chọn bản ghi nào!');
        return false;
    }
    if (count > 1) {
        alert('Bạn chỉ được chọn một bản ghi');
        return false;
    }
    return true;
}
var selectedCategory = function () {
    var data = RowSelectionModelCategory.getSelected().data;
    if (data == null) {
        return;
    }
    switch (hdfCurrentCategory.getValue()) {
        case "cat_Folk":
            hdfFolkId.setValue(data.Id);
            cbxFolk.setValue(data.Name);
            break;
        case "cat_Religion":
            hdfReligionId.setValue(data.Id);
            cbxReligion.setValue(data.Name);
            break;
        case "cat_PersonalClass":
            hdfPersonalClassId.setValue(data.Id);
            cbxPersonalClass.setValue(data.Name);
            break;
        case "cat_FamilyClass":
            hdfFamilyClassId.setValue(data.Id);
            cbxFamilyClass.setValue(data.Name);
            break;
        case "cat_Position":
            hdfPositionId.setValue(data.Id);
            cbxPosition.setValue(data.Name);
            break;      
        case "cat_CPVPosition":
            hdfCPVPositionId.setValue(data.Id);
            cbxCPVPosition.setValue(data.Name);
            break;
        case "cat_VYUPosition":
            hdfVYUPositionId.setValue(data.Id);
            cbxVYUPosition.setValue(data.Name);
            break;
        case "cat_ArmyLevel":
            hdfArmyLevelId.setValue(data.Id);
            cbxArmyLevel.setValue(data.Name);
            break;
        case "cat_HealthStatus":
            hdfHealthStatusId.setValue(data.Id);
            cbxHealthStatus.setValue(data.Name);
            break;
        case "cat_FamilyPolicy":
            hdfFamilyPolicyId.setValue(data.Id);
            cbxFamilyPolicy.setValue(data.Name);
            break;
        case "cat_IDIssuePlace":
            hdfIDIssuePlaceId.setValue(data.Id);
            cbxIDIssuePlace.setValue(data.Name);
            break;
        case "cat_JobTitle":
            hdfJobTitleId.setValue(data.Id);
            cbxJobTitle.setValue(data.Name);
            break;
    }
    wdAddCategory.hide();
}

var beforeShowWdCategory = function () {
    if (hdfCurrentCategory.getValue() != hdfTableDM.getValue()) {
        stCategory.removeAll();
        RowSelectionModelCategory.clearSelections();
        PagingToolbar1.pageIndex = 0;
        PagingToolbar1.doLoad();
        hdfCurrentCategory.setValue(hdfTableDM.getValue());
    }
}

var beforeShowWdCategoryGroup = function () {
    StoreCategoryGroup.removeAll();
    RowSelectionModelCategoryGroup.clearSelections();
    PagingToolbarCategoryGroup.pageIndex = 0;
    PagingToolbarCategoryGroup.doLoad();
}

var selectedCategoryGroup = function () {
    var data = RowSelectionModelCategoryGroup.getSelected().data;
    if (data == null) {
        return;
    }
    switch (hdfCurrentCatalogName.getValue()) {
        case "cat_BasicEducation":
            hdfBasicEducationId.setValue(data.Id);
            cbxBasicEducation.setValue(data.Name);
            break;
        case "cat_Education":
            hdfEducationId.setValue(data.Id);
            cbxEducation.setValue(data.Name);
            break;
        case "cat_PoliticLevel":
            hdfPoliticLevelId.setValue(data.Id);
            cbxPoliticLevel.setValue(data.Name);
            break;
        case "cat_ManagementLevel":
            hdfManagementLevelId.setValue(data.Id);
            cbxManagementLevel.setValue(data.Name);
            break;
        case "cat_LanguageLevel":
            hdfLanguageLevelId.setValue(data.Id);
            cbxLanguageLevel.setValue(data.Name);
            break;
        case "cat_ITLevel":
            hdfITLevelId.setValue(data.Id);
            cbxITLevel.setValue(data.Name);
            break;
    }
    wdAddCategoryGroup.hide();
}

var resetInputCategoryGroup = function () {
    txtTenDMGroup.reset();
    txtTenDMGroup.hide();
    cboCategoryGroup.clear();
    cboCategoryGroup.hide();
    btnSaveGroupCategory.hide();
    btnCancelGroupCategory.hide();
    btnAddCategoryGroup.enable();
}

var updateFamilyRelationHRM = function (field, value) {
    var db = inputEmployee_storeFamilyRelationship.getRange();
    for (var i = 0; i < db.length; i++) {
        if (db[i].data.Id == inputEmployee_RowSelectionModelQHGD.getSelected().data.Id) {
            switch (field) {
            case "RelationshipId":
                db[i].data.RelationshipId = value;
                break;
            }
        }
    }
}

var FamilyRelationRendererHRM = function (value) {
    var r = inputEmployee_store_CbxFamilyRelationship.getById(value);
    if (Ext.isEmpty(r)) {
        return value;
    }
    return r.data.Name;
}

var showWdAddCategoryHRM = function (name) {
    switch (name) {
        case "DanToc":
            inputEmployee_hdfTableDM.setValue("cat_Folk");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TonGiao":
            inputEmployee_hdfTableDM.setValue("cat_Religion");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ThanhPhanBanThan":
            inputEmployee_hdfTableDM.setValue("cat_PersonalClass");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ThanhPhanGiaDinh":
            inputEmployee_hdfTableDM.setValue("cat_FamilyClass");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ChucVu":
            inputEmployee_hdfTableDM.setValue("cat_Position");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ChucDanh":
            inputEmployee_hdfTableDM.setValue("cat_JobTitle");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TrinhDoVanHoa":
            inputEmployee_hdfTableDM.setValue("cat_BasicEducation");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TrinhDo":
            inputEmployee_hdfTableDM.setValue("cat_Education");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TrinhDoChinhTri":
            inputEmployee_hdfTableDM.setValue("cat_PoliticLevel");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TrinhDoQuanLy":
            inputEmployee_hdfTableDM.setValue("cat_ManagementLevel");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "NgoaiNgu":
            inputEmployee_hdfTableDM.setValue("cat_LanguageLevel");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TinHoc":
            inputEmployee_hdfTableDM.setValue("cat_ITLevel");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ChucVuDang":
            inputEmployee_hdfTableDM.setValue("cat_CPVPosition");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ChucVuDoan":
            inputEmployee_hdfTableDM.setValue("cat_VYUPosition");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "CapBacQuanDoi":
            inputEmployee_hdfTableDM.setValue("cat_ArmyLevel");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "TinhTrangSucKhoe":
            inputEmployee_hdfTableDM.setValue("cat_HealthStatus");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "ChinhSach":
            inputEmployee_hdfTableDM.setValue("cat_FamilyPolicy");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
        case "NoiCapCMND":
            inputEmployee_hdfTableDM.setValue("cat_IDIssuePlace");
            inputEmployee_hdfColMa.setValue("Id");
            inputEmployee_hdfColTen.setValue("Name");
            break;
    }
    inputEmployee_wdAddCategory.show();
}

var beforeShowWdCategoryHRM = function () {
    if (inputEmployee_hdfCurrentCategory.getValue() != inputEmployee_hdfTableDM.getValue()) {
        inputEmployee_stCategory.removeAll();
        inputEmployee_RowSelectionModelCategory.clearSelections();
        inputEmployee_PagingToolbarCategory.pageIndex = 0;
        inputEmployee_PagingToolbarCategory.doLoad();
        inputEmployee_hdfCurrentCategory.setValue(inputEmployee_hdfTableDM.getValue());
    }
}

var beforeShowWdCategoryGroupHRM = function () {
    inputEmployee_StoreCategoryGroup.removeAll();
    inputEmployee_RowSelectionModelCategoryGroup.clearSelections();
    inputEmployee_PagingToolbarCategoryGroup.pageIndex = 0;
    inputEmployee_PagingToolbarCategoryGroup.doLoad();
}

var selectedCategoryHRM = function () {
    var data = inputEmployee_RowSelectionModelCategory.getSelected().data;
    if (data == null) {
        return;
    }
    switch (inputEmployee_hdfCurrentCategory.getValue()) {
        case "cat_Folk":
            inputEmployee_hdfFolkId.setValue(data.Id);
            inputEmployee_cbxFolk.setValue(data.Name);
            break;
        case "cat_Religion":
            inputEmployee_hdfReligionId.setValue(data.Id);
            inputEmployee_cbxReligion.setValue(data.Name);
            break;
        case "cat_PersonalClass":
            inputEmployee_hdfPersonalClassId.setValue(data.Id);
            inputEmployee_cbxPersonalClass.setValue(data.Name);
            break;
        case "cat_FamilyClass":
            inputEmployee_hdfFamilyClassId.setValue(data.Id);
            inputEmployee_cbxFamilyClass.setValue(data.Name);
            break;
        case "cat_Position":
            inputEmployee_hdfPositionId.setValue(data.Id);
            inputEmployee_cbxPosition.setValue(data.Name);
            break;
        case "cat_CPVPosition":
            inputEmployee_hdfCPVPositionId.setValue(data.Id);
            inputEmployee_cbxCPVPosition.setValue(data.Name);
            break;
        case "cat_VYUPosition":
            inputEmployee_hdfVYUPositionId.setValue(data.Id);
            inputEmployee_cbxVYUPosition.setValue(data.Name);
            break;
        case "cat_ArmyLevel":
            inputEmployee_hdfArmyLevelId.setValue(data.Id);
            inputEmployee_cbxArmyLevel.setValue(data.Name);
            break;
        case "cat_HealthStatus":
            inputEmployee_hdfHealthStatusId.setValue(data.Id);
            inputEmployee_cbxHealthStatus.setValue(data.Name);
            break;
        case "cat_FamilyPolicy":
            inputEmployee_hdfFamilyPolicyId.setValue(data.Id);
            inputEmployee_cbxFamilyPolicy.setValue(data.Name);
            break;
        case "cat_IDIssuePlace":
            inputEmployee_hdfIDIssuePlaceId.setValue(data.Id);
            inputEmployee_cbxIDIssuePlace.setValue(data.Name);
            break;
        case "cat_JobTitle":
            inputEmployee_hdfJobTitleId.setValue(data.Id);
            inputEmployee_cbxJobTitle.setValue(data.Name);
            break;
    }
    inputEmployee_wdAddCategory.hide();
}

var selectedCategoryGroupHRM = function () {
    var data = inputEmployee_RowSelectionModelCategoryGroup.getSelected().data;
    if (data == null) {
        return;
    }
    switch (inputEmployee_hdfCurrentCatalogName.getValue()) {
    case "cat_BasicEducation":
        inputEmployee_hdfBasicEducationId.setValue(data.Id);
        inputEmployee_cbxBasicEducation.setValue(data.Name);
        break;
    case "cat_Education":
        inputEmployee_hdfEducationId.setValue(data.Id);
        inputEmployee_cbxEducation.setValue(data.Name);
        break;
    case "cat_PoliticLevel":
        inputEmployee_hdfPoliticLevelId.setValue(data.Id);
        inputEmployee_cbxPoliticLevel.setValue(data.Name);
        break;
    case "cat_ManagementLevel":
        inputEmployee_hdfManagementLevelId.setValue(data.Id);
        inputEmployee_cbxManagementLevel.setValue(data.Name);
        break;
    case "cat_LanguageLevel":
        inputEmployee_hdfLanguageLevelId.setValue(data.Id);
        inputEmployee_cbxLanguageLevel.setValue(data.Name);
        break;
    case "cat_ITLevel":
        inputEmployee_hdfITLevelId.setValue(data.Id);
        inputEmployee_cbxITLevel.setValue(data.Name);
        break;
    }
    inputEmployee_wdAddCategoryGroup.hide();
}