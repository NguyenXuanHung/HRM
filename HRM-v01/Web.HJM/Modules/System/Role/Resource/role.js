function deleteRole(grid, store) {
    window.Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa quyền này không?', function (btn) {
        if (btn == "yes") {
            try {
                grid.getRowEditor().stopEditing();
            } catch (e) { }
            var s = grid.getSelectionModel().getSelections();
            for (var i = 0, r; r = s[i]; i++) {
                store.remove(r);
                window.Ext.net.DirectMethods.DeleteRole(r.data.Id);
                btnDeleteRole.disable();
                btnEditRole.disable();
                store.commitChanges();
            }
        }
    });
}

function loadMenusForRole() {
    // uncheck all
    $("#jqxMenu").jqxTree("uncheckAll");
    $('.permissionCheckbox').prop("checked", false);
    // parse json menus for role
    var menusForRole = jQuery.parseJSON(hdfMenusForRole.getValue());
    // set value for checked role
    for (var i = 0; i < menusForRole.length; i++) {
        $("#jqxMenu").jqxTree("checkItem", $("#" + menusForRole[i].MenuId)[0], true);
        $("#read_" + menusForRole[i].MenuId).prop('checked', menusForRole[i].Permission[0] === "1");
        $("#edit_" + menusForRole[i].MenuId).prop('checked', menusForRole[i].Permission[1] === "1");
        $("#del_" + menusForRole[i].MenuId).prop('checked', menusForRole[i].Permission[2] === "1");
        $("#full_" + menusForRole[i].MenuId).prop('checked', menusForRole[i].Permission[3] === "1");
    }
}

function saveMenusForRole() {
    // check role id
    if (hdfRecordId.getValue() === "") {
        alert("Bạn chưa chọn quyền !");
        return;
    }
    // update json data
    hdfMenusForRole.setValue(generateJson());
    // call server method
    window.Ext.net.DirectMethods.SaveMenusForRole(parseInt(hdfRecordId.getValue()));
    // reload menu for role
    loadMenusForRole();
}

function resetForm() {
    txtRoleName.reset();
    txtDescription.reset();
    txtRoleCommand.reset();
    btnUpdateRole.show();
    btnUpdateRoleAndClose.show();
    btnEdit.hide();
    btnDuplicateRole.hide();
    window.Ext.net.DirectMethods.CloseAddRoleWindow();
}

function generateJson() {
    var jsonData = "[";
    var checkedItems = $("#jqxMenu").jqxTree("getCheckedItems");
    for (var i = 0; i < checkedItems.length; i++)
    {
        var permission = $("#read_" + checkedItems[i].element.id).prop('checked') === true ? "1" : "0";
        permission += $("#edit_" + checkedItems[i].element.id).prop('checked') === true ? "1" : "0";
        permission += $("#del_" + checkedItems[i].element.id).prop('checked') === true ? "1" : "0";
        permission += $("#full_" + checkedItems[i].element.id).prop('checked') === true ? "1" : "0";
        jsonData += '{"Id":0,' + '"MenuId":' + checkedItems[i].element.id + ',"RoleId":' + hdfRecordId.getValue() + ',"Permission":"' + permission + '"},';
    }
    if (jsonData.endsWith(","))
        jsonData = jsonData.substr(0, jsonData.length - 1);
    jsonData += "]";
    return jsonData;
}

$(document).ready(function () {
    // get theme
    var theme = getDemoTheme();
    // init height
    var h = $(window).height() - 60;
    // init jqxTree for menu
    $("#jqxMenu").jqxTree({ height: h + "px", hasThreeStates: true, checkboxes: true, width: "400px", theme: theme });
    // bind menu premission change
    $('.permissionCheckbox').change(function (event) {
        var menuId = this.id.split('_')[1];
        if (this.checked)
        {
            if ($("#full_" + menuId).prop('checked') === true)
                $("#del_" + menuId).prop('checked', true);
            if ($("#del_" + menuId).prop('checked') === true)
                $("#edit_" + menuId).prop('checked', true);
            if ($("#edit_" + menuId).prop('checked') === true)
                $("#read_" + menuId).prop('checked', true);
        }
        else
        {
            if ($("#read_" + menuId).prop('checked') === false)
                $("#edit_" + menuId).prop('checked', false);
            if ($("#edit_" + menuId).prop('checked') === false)
                $("#del_" + menuId).prop('checked', false);
            if ($("#del_" + menuId).prop('checked') === false)
                $("#full_" + menuId).prop('checked', false);
        }
    });
});