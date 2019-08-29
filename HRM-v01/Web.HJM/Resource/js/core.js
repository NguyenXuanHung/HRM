/* tab */
// add page in tab
var addTab = function (tabPanel, id, url, title) {
    var closable = true;
    if (id === 'dashboard') {
        closable = false;
    }
    var tab = tabPanel.getComponent(id);
    if (!tab) {
        tab = tabPanel.add({
            id: id,
            title: title,
            closable: closable,
            autoLoad: {
                showMask: true,
                url: url,
                mode: 'iframe',
                maskMsg: 'Đang tải...'
            }
        });
        tab.on('activate', function() {}, this);
    }
    tabPanel.setActiveTab(tab);
    return tab;
};
// add report in tab
var addTabReport = function(tabPanel, id, url, title) {
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
            maskMsg: 'Đang tải...'
        }
    });
    tab.on('activate', function() {}, this);
    tabPanel.setActiveTab(tab);
    return tab;
};

/* render */
var renderCommonStatus = function (value) {
    if (value === "Active") 
        return "<img src='/Resource/icon/bullet_tick.png'>";
    if (value === "Locked")
        return "<img  src='/Resource/icon/lock.png'>";
    return "";
};
var renderStatus = function (value) {
    if (value === false) {
        return "<img src='/Resource/icon/bullet_tick.png'>";
    }
    return "<img  src='/Resource/icon/bullet_cross.png'>";
};
var renderBooleanIcon = function (value) {
    if (value === true) {
        return "<img  src='/Resource/icon/bullet_tick.png'>";
    }
    return "<img src='/Resource/icon/bullet_cross.png'>";
}
var renderSex = function (value) {
    if (value === true)
        return "<span style='color:blue'>Nam</span>";
    else if (value === false)
        return "<span style='color:red'>Nữ</span>";
    else
        return "";
}




var RenderVND = function (value, p, record) {
    if (value == null || value.length == 0)
        return "";
    value = Math.round(value);
    var l = (value + "").length;
    var s = value + "";
    var rs = "";
    var count = 0;
    for (var i = l - 1; i > 0; i--) {
        count++;
        if (count == 3) {
            rs = "." + s.charAt(i) + rs;
            count = 0;
        }
        else {
            rs = s.charAt(i) + rs;
        }
    }
    rs = s.charAt(0) + rs;
    if (rs.replace(".", "").trim() * 1 == 0) {
        return "";
    }

    return "<span style='float:right;'>" + rs + "</span>";// + " VNĐ";
}
var RenderVNDBold = function (value, p, record) {
    if (value == null || value.length == 0)
        return "";
    value = Math.round(value);
    var l = (value + "").length;
    var s = value + "";
    var rs = "";
    var count = 0;
    for (var i = l - 1; i > 0; i--) {
        count++;
        if (count == 3) {
            rs = "." + s.charAt(i) + rs;
            count = 0;
        }
        else {
            rs = s.charAt(i) + rs;
        }
    }
    rs = s.charAt(0) + rs;
    if (rs.replace(".", "").trim() * 1 == 0) {
        return "";
    }
    return "<span style='float:right;'><b>" + rs + "</b></span>"; // + " VNĐ";
}
var RenderVND0 = function (value, p, record) {
    value = Math.round(value);
    if (value == null || value.length == 0)
        return "<span style='float:right;'>0</span>";
    var l = (value + "").length;
    var s = value + "";
    var rs = "";
    var count = 0;
    for (var i = l - 1; i > 0; i--) {
        count++;
        if (count == 3) {
            rs = "." + s.charAt(i) + rs;
            count = 0;
        }
        else {
            rs = s.charAt(i) + rs;
        }
    }
    rs = s.charAt(0) + rs;
    if (rs.replace(".", "").trim() * 1 == 0) {
        return "<span style='float:right;'>0</span>";
    }
    return "<span style='float:right;'>" + rs + "</span>";// + " VNĐ";
}
var RenderGender = function (value, p, record) {
    var nam = "<span style='color:blue'>Nam</span>";
    var nu = "<span style='color:red'>Nữ</span>";
    if (value == 'M')
        return nam;
    //   else if (value == 'F')
    //      return nu;
    else
        return nu;
}
var RenderGenderNoColor = function (value, p, record) {
    var nam = "Nam";
    var nu = "Nữ";
    if (value == 'M')
        return nam;
    //   else if (value == 'F')
    //      return nu;
    else
        return nu;
}
var RenderDate = function (value, p, record) {
    try {
        if (value == null) return "";
        // kiểm tra có phải kiểu ngày tháng VN ko 
        if (value.indexOf("SA") != -1 || value.indexOf("CH") != -1) {
            var d = value.split(' ')[0];
            var year = d.split('/')[2];
            if (year == "0001" || year == "1900") {
                return "";
            }
            return d;
        }
        //nếu ko phải thì render bình thường
        value = value.replace(" ", "T");
        var temp = value.split("T");
        var date = temp[0].split("-");
        var dateStr = date[2] + "/" + date[1] + "/" + date[0];
        if (date[0] == "1900" || date[0] == "0001") {
            return "";
        }
        if (dateStr != "") {
            return dateStr;
        }
    } catch (e) {

    }
}
var RenderSex = function (value, p, record) {
    if (value === true)
        return "<span style='color:blue'>Nam</span>";
    else if (value === false)
        return "<span style='color:red'>Nữ</span>";
    else
        return "";
}

var RenderAllowMinusVND = function (value, p, record) {
    if (value == null || value.length === 0)
        return "";
    value = Math.round(value);
    console.log(value);
    var sign = "";
    if (value < 0) {
        value = Math.abs(value);
        sign = "-";
    }

    var l = (value + "").length;
    var s = value + "";
    var rs = "";
    var count = 0;
    for (var i = l - 1; i > 0; i--) {
        count++;
        if (count === 3) {
            rs = "." + s.charAt(i) + rs;
            count = 0;
        }
        else {
            rs = s.charAt(i) + rs;
        }
    }
    rs = s.charAt(0) + rs;
    if (rs.replace(".", "").trim() * 1 === 0) {
        return "";
    }

    return "<span style='float:right;'>" + sign + rs + "</span>";
}
var RenderPercent = function (value, p, record) {
    if (value == null || value == "") {
        return "";
    }
    return "<span style='float:right;'>" + value + " %</span>";
}

var GetTimeSheetEarlyOrLate = function (value, p, record) {
    if (value == "1") {
        return "Về sớm";
    } else if (value == "0") {
        return "Đi muộn";
    } else if (value == "2") {
        return "Đi sớm";
    } else if (value == "3") {
        return "Về muộn";
    }
    return "";
}
var GetBooleanIcon = function (value, p, record) {
    var sImageCheck = "<img  src='/Resource/Images/check.png'>";
    var sImageUnCheck = "<img src='/Resource/Images/uncheck.gif'>";
    if (value == "1") {
        return sImageCheck;
    } else if (value == "0") {
        return sImageUnCheck;
    }
    return "";
}
var GetMirrorBooleanIcon = function (value, p, record) { //Giống với GetBooleanIcon nhưng Icon thì trái ngược
    var sImageCheck = "<img  src='../../Resource/Images/check.png'>";
    var sImageUnCheck = "<img src='../../Resource/Images/uncheck.gif'>";
    if (value == "1") {
        return sImageUnCheck;
    } else {
        return sImageCheck;
    }
}
var GetGender = function (value, p, record) {
    var nam = "<span style='color:blue'>Nam</span>";
    var nu = "<span style='color:red'>Nữ</span>";
    if (value == 'M')
        return nam;
    else
        return nu;
}
var GetGenderBoolean = function (value, p, record) {
    var nam = "<span style='color:blue'>Nam</span>";
    var nu = "<span style='color:red'>Nữ</span>";
    if (value == '1')
        return nam;
    else if (value == '0')
        return nu;
    else
        return '';
}
var GetGenderChar = function (value, p, record) {
    if (value == "M")
        return "<span style='color:blue'>Nam</span>";
    else
        return "<span style='color:red'>Nữ</span>";
}

/* grid */
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

/* validate */
var ChuanHoaTen = function(idTextField) {
    if (idTextField.getValue() != '') {
        var hoten = idTextField.getValue().toLowerCase().trim();
        var arrStr = hoten.split(' ');
        var rs = '';
        for (var i = 0; i < arrStr.length; i++) {
            var item = arrStr[i].trim();
            if (item != '') {
                var firstChar = item.substring(0, 1);
                rs += firstChar.toUpperCase() + item.slice(1, item.length) + ' ';
            }
        }
        idTextField.setValue(rs.trim());
    }
};
var ValidateDateField = function(idDateField) {
    reg = /^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/;
    testdf = reg.test(idDateField.getRawValue());
    if (!testdf && idDateField.getRawValue() != '' && idDateField.getRawValue() != null) {
        idDateField.focus();
        return false;
    }
    return true;
};