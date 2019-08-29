function getReportPreview(imgUrl) {
    if (imgUrl === '') {
        document.getElementById('divReportPreview').style.display = 'none';
        return;
    }
    document.getElementById('divReportPreview').style.display = 'block';
    document.getElementById("imgReportPreview").src = "ReportPreview/" + imgUrl;
}
var filterTree = function (el, e) {
    try {
        var tree = TreePanel1,
        text = this.getRawValue();

        tree.clearFilter();

        if (Ext.isEmpty(text, false)) {
            return;
        }

        if (e.getKey() === Ext.EventObject.ESC) {
            clearFilter();
        } else {
            var re = new RegExp(".*" + text + ".*", "i");

            tree.filterBy(function (node) {
                return re.test(node.text);
            });
        }
    } catch (e) {

    }
}

var clearFilter = function () {
    try {
        var field = TriggerField1,
        tree = TreePanel1;

        field.setValue("");
        tree.clearFilter();
        tree.getRootNode().collapseChildNodes(true);
        tree.getRootNode().ensureVisible();
    } catch (e) {

    }
};

var RemoveAndAddNewTab = function (tabid) {
    try {
        //   alert(tabid);
        var tab = pnTabReport.getComponent(tabid);
        //  pnTabReport.remove(tab);
    } catch (e) {

    }
}
$(document).ready(function () {
    // Create jqxTree 
    var theme = getDemoTheme();
    // create jqxTree
    $('#jqxTree').jqxTree({ height: '330px', hasThreeStates: true, checkboxes: true, width: '330px', theme: theme });
    $('#jqxCheckBox').jqxCheckBox({ width: '200px', height: '25px', checked: true, theme: theme });
    $('#jqxCheckBox').on('change', function (event) {
        var checked = event.args.checked;
        $('#jqxTree').jqxTree({ hasThreeStates: checked });
    });
    $("#jqxTree").jqxTree('selectItem', $("#home")[0]);
});
function getSelectedNode() {
    //Lấy các menu được check
    var items = $('#jqxTree').jqxTree('getItems');
    var rs = "";       // các node được checked 
    var selectedFunction = "";      // các node được chọn (checked + undefined)
    var str = "";
    for (var i = 0; i < items.length; i++) {
        if (items[i].checked == true) {
            rs += items[i].element.id + ","; 
            str += items[i].label + ",";
            maDonVi = items[i].element.id;
        }
    }
    hdfSelectedDepartmentIdList.setValue(rs);
    cbChonPhongBan.setValue(str); 
}

function UnCheckAllFunction() {
    try {
        var item = $('#jqxTree').jqxTree('getSelectedItem');
        $('#functionTree' + item.id).jqxTree('uncheckAll');
    } catch (e) {

    }
}