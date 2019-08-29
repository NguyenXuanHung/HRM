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

var renderHasHusband = function (value) {
    if (value === false) {
        return "<img src='/Resource/icon/bullet_cross.png'>";
    }
    return "<img  src='/Resource/icon/bullet_tick.png'>";
};
var renderBooleanIcon = function (value) {
    console.log('IsMinority: ' + value);
    if (value == true) {
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

var RenderTrueFalseIcon = function (value, p, record) {
    if (value == "1") {
        return "<img  src='/Resource/Images/check.png'>";
    } else if (value == "0") {
        return "<img src='/Resource/Images/uncheck.gif'>";
    } else {
        return "";
    }
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

var RenderTime = function (value, p, record) {
    try {
        if (value == null) return "";
        var timeStr = value.substring(0, 2) + ":" + value.substring(2, 4);
        if (timeStr != "") {
            return timeStr;
        }
    } catch (e) {

    }
}

var RenderPercent = function (value, p, record) {
    if (value == null || value == "") {
        return "";
    }
    return "<span style='float:right;'>" + value + " %</span>";
}

var GetTimeSheetEarlyOrLate = function (value, p, record) {
    console.log(record);
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

var RenderGroupSymbol = function (value) {
    return "<b>" + value + "</b>";
}

var RenderSymbol = function (value, p, record) {
    var color = record.data.Color || record.data.SymbolColor
    return "<span class='badge' style='background:" + color + "'>" + value + "</span>";
}

var RenderErrorRow = function (value, p, record) {
    if ((value + "") == "true") {
        return "<img src='../../Resource/images/uncheck.gif' alt='' /> <b style='color:red;'>Lỗi</b>";
    }
    return "";
}

var RenderStatusEvent = function (value) {
    if (value === 2) {
        return "<b style='color:red;'>" + 'Đã xóa' + "</b>";
    } else {
        return "<b style='color:#222;'>" + 'Đang sử dụng' + "</b>";
    }
}

function RenderDay(value, p, record) {
    var symbol = "　　";
    if (value.SymbolDisplay != null) {
        symbol = value.SymbolDisplay;
    }

    return "<div style='width:100%;height:100%;' onclick='intTimeSheetInput(" +
        value.TimeSheetModelId +
        "," +
        value.Id +
        "); '>" +
        symbol +
        "</div>";
}

function RenderTotal(value) {
    if (value != null && (value > 0 || value < 0)) {
        return "<span style='color:red'>" + value.toString() + "</span>";
    } else {
        return value;
    }
}

var RenderCap = function (value, p, record) {
    if (value == null)
        arr = new Array(0);
    else
        var arr = value.split('#');
    if (arr.length > 1) {
        var rs = "<div class='hsl_template'>";
        rs += arr[0];
        rs += "</div><div class='ml_template'>";
        rs += arr[1];
        rs += "</div>";
    }
    else
        rs = '';
    return rs;
}
var RenderName = function (value, p, record) {
    if (value == null)
        arr = new Array(0);
    else
        var arr = value.split('#');
    if (arr.length > 1) {
        var rs = "<div class='hsl_template'>";
        rs += arr[0];
        rs += "</div><div class='ml_template'>";
        rs += arr[1];
        rs += " tháng</div>";
    }
    else
        rs = '';
    return rs;
}

var RenderGender2 = function (value, p, record) {
    if (value == "Nam")
        return "<span style='color:blue;'>" + value + "</span>"
    return "<span style='color:red;'>" + value + "</span>"
}

var RenderLoaiLuong = function (value, p, record) {
    if (value == 'NangLuong') {
        return "Nâng lương";
    }
    else if (value == 'VuotKhung') {
        return "Vượt khung";
    }
    else {
        return "";
    }
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
var ChuanHoaDateField = function (idDateField) {
    if (idDateField.getValue() != '') {
        var dateStr = idDateField.getRawValue();
        var arrStr = dateStr.split('/');
        var rs = '';
        for (var i = 0; i < arrStr.length; i++) {
            var item = arrStr[i].trim();
            if (item.length == 1)
                item = '0' + item;
            if (item.length < 4 && item.length > 0)
                rs += item + '/';
            else
                rs += item;
        }
        idDateField.setValue(rs.trim());
    }
}
var ValidateDateField = function(idDateField) {
    reg = /^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/;
    testdf = reg.test(idDateField.getRawValue());
    if (!testdf && idDateField.getRawValue() != '' && idDateField.getRawValue() != null) {
        idDateField.focus();
        return false;
    }
    return true;
};
var removeSeparator = function (text, separator) {
    if (text.length == 1)
        return text;
    var rs = '';
    for (var i = 0; i < text.length; i++) {
        if (text[i] != separator) {
            rs += text[i];
        }
    }
    return rs;
}
var RenderNumberInTextField = function (idTextField, fractional, separator) {
    var text = idTextField.getValue();
    text = removeSeparator(text, separator);
    var result = '';
    // has fractional
    if (text.indexOf(fractional) != -1) {
        var arrStr = text.split(fractional);
        // too much fractional
        if (arrStr.length > 2) {
            alert('Bạn không được nhập quá một dấu "' + fractional + '"');
            result = arrStr[0] + fractional + arrStr[1];
        }
        else {
            var count = 1;
            for (var i = arrStr[0].length - 1; i >= 0; i--) {
                result = arrStr[0][i] + result;
                if (count % 3 == 0 && count != arrStr[0].length) {
                    result = separator + result;
                }
                count++;
            }
            result += fractional;
            count = 1;
            for (var j = 0; j < arrStr[1].length; j++) {
                result += arrStr[1][j];
                if (count % 3 == 0 && count != arrStr[1].length) {
                    result += separator;
                }
                count++;
            }
        }
    }
    else {
        var count = 1;
        for (var k = text.length - 1; k >= 0; k--) {
            result = text[k] + result;
            if (count % 3 == 0 && count != text.length) {
                result = separator + result;
            }
            count++;
        }
    }
    idTextField.setValue(result);
}
//Lấy ngày hôm nay, tùy theo định dạng Quốc tế hay việt nam
var GetTodayTime = function (culture) {
    if (culture == "vi") {
        return "'" + new Date().getDate() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getFullYear() + "'";
    }
    else {
        return "'" + (new Date().getMonth() + 1) + '-' + new Date().getDate() + '-' + new Date().getFullYear() + "'";
    }
    // return '01-01-2011';
}

var RenderValueTypeName = function (value, p, record) {
    var icon = '';
    var style = 'font-style:italic;' +
                'font-weight:bold;' +
                'letter-spacing:-1px;';
    console.log(record.data.ValueType);
    switch (record.data.ValueType) {
    case 'Number':
        icon = "<span style='color:green;" + style + "'>123</span>";
        break;
    case 'String':
        icon = iconImg('text_ab');
        break;
    case 'Percent':
        icon = "<span style='color:red;" + style + "'>%</span>";
        break;
    case 'Formula':
        icon = iconImg('sum');
        break;
    }
    return icon;
}