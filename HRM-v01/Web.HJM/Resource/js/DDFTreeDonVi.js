var GetSelectedNode = function (tree, hdfAllNodeID, hdfMaBoPhan) {
    if (hdfAllNodeID.getValue().length != 0) {
        hdfMaBoPhan.reset();
        var str = hdfAllNodeID.getValue().split(',');
        for (var i = 0; i < str.length; i++) {
            if (str[i].length != 0) {
                if (tree.getNodeById(str[i]).getUI().checkbox.checked == true) {
                    hdfMaBoPhan.setValue(hdfMaBoPhan.getValue() + ',' + str[i]);
                }
            }
        }
        hdfMaBoPhan.setValue(hdfMaBoPhan.getValue().substring(1));
    }
}
var ResetNodeChecked = function (tree, hdfAllNodeID) {
    if (hdfAllNodeID.getValue().length != 0) {
        var str = hdfAllNodeID.getValue().split(',');
        for (var i = 0; i < str.length; i++) {
            if (str[i].length != 0) {
                tree.getNodeById(str[i]).getUI().checkbox.checked = false;
            }
        }
    }
}
var getTasks = function (tree) {
    var msg = [],
            selNodes = tree.getChecked();
    msg.push("[");

    Ext.each(selNodes, function (node) {
        if (msg.length > 1) {
            msg.push(",");
        }

        msg.push(node.text);
    });

    msg.push("]");

    return msg.join("");
};