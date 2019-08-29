//  normalized fullname of employee
//  idTextField: ID of TextField fullname
var ChuanHoaTen = function (idTextField) {
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
}
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
// validate input data in date field
// return true if input data is valid and false if input data is invalid
var ValidateDateField = function (idDateField) {
    reg = /^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/;
    testdf = reg.test(idDateField.getRawValue());
    if (!testdf && idDateField.getRawValue() != '' && idDateField.getRawValue() != null) {
        idDateField.focus();
        return false;
    }
    return true;
}
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