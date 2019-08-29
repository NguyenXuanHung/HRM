function addTab(tabPanel, id, url, title) {
    var tab = tabPanel.getComponent(id);
    if (!tab) {
        tab = tabPanel.add({
            id: id,
            title: title,
            closable: true,
            autoLoad: {
                showMask: true,
                url: url,
                mode: 'iframe',
                maskMsg: ' Đang nạp dữ liệu, vui lòng chờ...'
            }
        });
        tab.on('activate', function () {}, this);
    }
    tabPanel.setActiveTab(tab);
    return tab;
}

function addTabReport(tabPanel, id, url, title) {
    var tab = tabPanel.getComponent(id);
    if (tab) tabPanel.remove(tab, true);
    tab = tabPanel.add({
        id: id,
        title: title,
        closable: true,
        autoLoad: {
            showMask: true,
            url: url,
            mode: 'iframe',
            maskMsg: ' Đang nạp dữ liệu, vui lòng chờ...'
        }
    });
    tab.on('activate', function () {
        //var item = MenuPanel1.menu.items.get(id + "_item");
        //                    if (item) {
        ////                        MenuPanel1.setSelection(item);
        //                    }
    }, this);
    tabPanel.setActiveTab(tab);
    return tab;
}


function addHomePage(tabPanel, id, url, title) {
    var tab = tabPanel.getComponent(id);
    if (!tab) {
        tab = tabPanel.add({
            id: id,
            title: title,
            closable: false,
            autoLoad: {
                showMask: true,
                url: url,
                mode: 'iframe',
                maskMsg: ' Đang nạp dữ liệu, vui lòng chờ...'
            }
        });
        tab.on('activate', function () {
        }, this);
    }
    tabPanel.setActiveTab(tab);
    return tab;
}

//Tạo tab mặc định (force: neu ton tai thi dong, con ko thi tao moi ) 
function addDefaultTab(id, url, title) {
    var tabPanel = pnTabPanel;
    var tab = tabPanel.getComponent(id);
    if (tab) {
        tabPanel.remove(tab);
    }
    addTab(tabPanel, id, url, title);
}


// Toggle tab (đóng Tab, mở tab khác nếu chưa có hoặc reload lại tab nếu đã tồn tại)
function toggleTab(closeId, openId, url, title) {
    var tabPanel = pnTabPanel;
    var tab = tabPanel.getComponent(closeId);
    if (tab != null) { tabPanel.remove(tab); }
    addDefaultTab(openId, url, title);
}


//add new window
function newWindow(url, config) {
    new Ext.Window(Ext.apply({
        renderTo: Ext.getBody(),
        resizable: false,
        closable: false,
        layout: "fit",
        maximizable: false,
        constrain: true,
        closeAction: "hide",
        frame: true,
        id: "new_window",
        listeners: {
            beforeshow: {
                fn: function (el) {
                    var height = Ext.getBody().getViewSize().height;
                    var width = Ext.getBody().getViewSize().width;
                    if (el.getSize().height > height) {
                        el.setHeight(height - 20)
                    }
                    if (el.getSize().width > width) {
                        el.setWidth(width - 100);
                    }
                }

            }
        },
        buttons: [
            {
                text: "Đóng",
                iconCls: "icon-accept",
                listeners: {
                    click: {
                        fn: function (el, e) {
                            var win = eval("new_window");
                            win.destroy();
                        }
                    }
                }
            }
        ],
        autoLoad: {
            maskMsg: "Đang tải dữ liệu, vui lòng chờ...",
            showMask: true,
            mode: "iframe",
            url: url
        }
    }, config)).show();
}

//Move tree node
var moveNode = function (tree, node, oldParent, newParent, index) {
    var buf = [];
    buf.push("Node = " + node.text);
    buf.push("<br/>");
    buf.push("Old parent = " + oldParent.text);
    buf.push("<br/>");
    buf.push("New parent = " + newParent.text);
    buf.push("<br/>");
    buf.push("Index = " + index);

    Ext.Msg.alert("Node droped", buf.join(""));
}
var CheckSelectedRow = function (grid) {
    var s = grid.getSelectionModel().getSelections();
    var count = 0;
    for (var i = 0, r; r = s[i]; i++) {
        count++;
    }
    if (count == 0) {
        alert('Bạn chưa chọn bản ghi nào!');
        return false;
    }
    return true;
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