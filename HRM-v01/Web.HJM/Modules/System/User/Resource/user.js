var enterKeyPressHandler = function (f, e) {
    if (e.getKey() == e.ENTER) {
        storeUsersForRole.reload();
    }
}

var RemoveItemOnGrid = function (grid, Store) {
    Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
        if (btn == "yes") {
            try {
                grid.getRowEditor().stopEditing();
            } catch (e) {

            }
            var s = grid.getSelectionModel().getSelections();
            for (var i = 0, r; r = s[i]; i++) {
                Store.remove(r);
                Store.commitChanges();
                Ext.net.DirectMethods.DeleteUser(r.data.Id);
                Store.commitChanges();
            }
        }
    });
}

var GetListUserID = function (grid, store) {
    var s = grid.getSelectionModel().getSelections();
    var str = '';
    for (var i = 0, r; r = s[i]; i++) {
        str += r.data.ID + ',';
    }
    hdfListUserID.setValue(str);
}



var checkChangePassword = function () {
    if (txtNewPassword.getValue() == '' || TextField1.getValue() == '') {
        window.Ext.Msg.alert("Cảnh báo", "Bạn chưa nhập đầy đủ thông tin");
        return false;
    }
    if (txtNewPassword.getValue() != TextField1.getValue()) {
        window.Ext.Msg.alert("Cảnh báo", "Mật khẩu mới không khớp nhau");
        return false;
    }
    return true;
}
//Bắt lỗi nhập liệu


var getSelectedIndexRow = function () {
    var record = gridUsersForRole.getSelectionModel().getSelected();
    var index = gridUsersForRole.store.indexOf(record);
    if (index == -1)
        return 0;
    return index;
}

var addRecord = function (ID, Gender, LastName, IsLock, Phone, UserName, FirstName, CreatedOn, DisplayName, Address, Birthday, Email) {
    var rowindex = getSelectedIndexRow();
    gridUsersForRole.insertRecord(rowindex, {
        ID: ID,
        Gender: Gender,
        LastName: LastName,
        IsLock: IsLock,
        Phone: Phone,
        UserName: UserName,
        FirstName: FirstName,
        CreatedOn: CreatedOn,
        DisplayName: DisplayName,
        Address: Address,
        Birthday: Birthday,
        Email: Email
    });
    gridUsersForRole.getView().refresh();
    gridUsersForRole.getSelectionModel().selectRow(rowindex);
    storeUsersForRole.commitChanges();
}

var addUpdatedRecord = function (ID, Gender, LastName, IsLock, Phone, UserName, FirstName, CreatedOn, DisplayName, Address, Birthday, Email) {
    var rowindex = getSelectedIndexRow();
    //xóa bản ghi cũ 
    var s = gridUsersForRole.getSelectionModel().getSelections();
    for (var i = 0, r; r = s[i]; i++) {
        storeUsersForRole.remove(r);
        storeUsersForRole.commitChanges();
    }

    //Thêm bản ghi đã update
    gridUsersForRole.insertRecord(rowindex, {
        ID: ID,
        Gender: Gender,
        LastName: LastName,
        IsLock: IsLock,
        Phone: Phone,
        UserName: UserName,
        FirstName: FirstName,
        CreatedOn: CreatedOn,
        DisplayName: DisplayName,
        Address: Address,
        Birthday: Birthday,
        Email: Email
    });
    gridUsersForRole.getView().refresh();
    gridUsersForRole.getSelectionModel().selectRow(rowindex);
    storeUsersForRole.commitChanges();
}




/* tree panel */
//
// select role, reload grid panel
function reloadUsersForRole(roleId) {
    hdfSeletedRoleId.setValue(roleId);
    hdfSeletedUserId.setValue('');
    storeUsersForRole.reload();
    btnAssignRole.disable();
    btnAssignDepartment.disable();
    btnEditUser.disable();
    btnDeleteUser.disable();
}

/* grid panel */

/* window add user */
//
// validate input value
function validateUserData() {
    if (txtFirstName.getValue() == '') {
        window.Ext.Msg.alert("Cảnh báo", "Bạn chưa nhập họ đệm");
        txtFirstName.focus();
        return false;
    }
    if (txtLastName.getValue() == '') {
        window.Ext.Msg.alert("Cảnh báo", "Bạn chưa nhập tên");
        txtLastName.focus();
        return false;
    }
    if (txtDisplayName.getValue() == '') {
        window.Ext.Msg.alert("Cảnh báo", "Bạn chưa nhập tên hiển thị");
        txtDisplayName.focus();
        return false;
    }
    if (txtUserName.getValue() == '') {
        window.Ext.Msg.alert("Cảnh báo", "Bạn chưa nhập tài khoản");
        txtUserName.focus();
        return false;
    }
    if (hdfUserCommand.getValue() == 'Insert') {
        if (txtPassword.getValue() == '') {
            window.Ext.Msg.alert("Cảnh báo", "Bạn chưa nhập mật khẩu");
            txtPassword.focus();
            return false;
        }
        if (txtConfirmPassword.getValue() == '') {
            window.Ext.Msg.alert("Cảnh báo", "Bạn chưa xác nhận mật khẩu");
            txtConfirmPassword.focus();
            return false;
        }
    }
    if (txtPassword.getValue() != txtConfirmPassword.getValue()) {
        window.Ext.Msg.alert("Cảnh báo", "Xác nhận mật khẩu không đúng");
        txtConfirmPassword.focus();
        return false;
    }
    if (cboDepartment.getValue() == '') {
        window.Ext.Msg.alert("Cảnh báo", "Bạn chưa chọn bộ phận");
        return false;
    }
    return true;
}

// reset form
function resetForm () {
    txtUserName.reset();
    txtFirstName.reset();
    txtLastName.reset();
    txtDisplayName.reset();
    txtEmail.reset();
    txtPhone.reset();
    txtAddress.reset();
    dfBirthday.reset();
    rdgSex.reset();
    txtPassword.reset();
    txtPassword.enable();
    txtConfirmPassword.reset();
    txtConfirmPassword.enable();
    cboDepartment.reset();
    hdfSelectedDepartmentID.reset();
    btnAddUser.show();
    btnAddUserAndClose.show();
    btnUpdateUser.hide();
    hdfUserCommand.reset();
    window.Ext.net.DirectMethods.ResetWindowTitle();
}

/* window assign roles, departments for users */
//
function getCheckedNode(tree, allIds) {
    // init return value
    var checkedIds = "";
    // check tree to get checked nodes
    if (allIds.length !== 0) {
        var arrAllIds = allIds.split(',');
        for (var i = 0; i < arrAllIds.length; i++) {
            if (arrAllIds[i].length !== 0) {
                if (tree.getNodeById(arrAllIds[i])) {
                    if (tree.getNodeById(arrAllIds[i]).getUI().checkbox.checked === true) {
                        checkedIds = checkedIds.length === 0 ? arrAllIds[i] : checkedIds + "," + arrAllIds[i];
                    }
                }
            }
        }
    }
    // return list ids of checked node
    return checkedIds;
}

function setCheckedNode(tree, checkedIds, allIds) {
    // reset tree
    if (allIds.length !== 0) {
        var arrAllIds = allIds.split(',');
        for (var i = 0; i < arrAllIds.length; i++) {
            if (arrAllIds[i].length !== 0) {
                if (tree.getNodeById(arrAllIds[i]))
                tree.getNodeById(arrAllIds[i]).getUI().checkbox.checked = false;
            }
        }
    }
    // set checked nodes
    if (checkedIds.length !== 0) {
        var arrCheckedIds = checkedIds.split(',');
        for (var j = 0; j < arrCheckedIds.length; j++) {
            if (arrCheckedIds[j].length !== 0) {
                if (tree.getNodeById(arrCheckedIds[j])) {
                    tree.getNodeById(arrCheckedIds[j]).getUI().checkbox.checked = true;
                }
            }
        }
    }
}