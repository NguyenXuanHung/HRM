
/*
Xóa 1 bản ghi trên Grid
VD gọi hàm : RemoveItemOnGrid(#{GridPanel1},#{Store1});
*/
var RemoveItemOnGrid = function (grid, Store) {
    Ext.Msg.confirm('Xác nhận', 'Bạn có chắc chắn muốn xóa không ?', function (btn) {
        if (btn == "yes") {
            try {
                grid.getRowEditor().stopEditing();
            } catch (e) {
                alert(e.Message.toString());
            }
            var s = grid.getSelectionModel().getSelections();
            for (var i = 0, r; r = s[i]; i++) {
                Store.remove(r);
            }
        }
    });
}

/*
Cho hiện form sửa
*/
var EditItemOnGrid = function (grid) {
    try {
        grid.getRowEditor().stopEditing();
    } catch (e) {

    }
    grid.getView().refresh();
    var s = grid.getSelectionModel().getSelections();

    for (var i = 0, r; r = s[i]; i++) {
        grid.getSelectionModel().selectRow(r);
        grid.getRowEditor().startEditing(r);
    }
}

/*
thêm mới bản ghi trên grid
*/
var AddNewItem = function (grid) {
    grid.getRowEditor().stopEditing();

    grid.insertRecord(0, {
});

grid.getView().refresh();
grid.getSelectionModel().selectRow(0);
grid.getRowEditor().startEditing(0);
}

/*
Lấy biểu tượng True, False để hiển thị lên grid
*/

var GetBooleanIcon = function (value, p, record) {
    var sImageCheck = "<img  src='../../Resource/Images/check.png'>"
    var sImageUnCheck = "<img src='../../Resource/Images/uncheck.gif'>"
    if (value == "1") {
        return sImageCheck;
    } else {
        return sImageUnCheck;
    }
}
/*
Lấy giới tính
*/
var GetGender = function (value, p, record) {
    if (value == "1")
        return "<span style='color:blue'>Nam</span>";
    else
        return "<span style='color:red'>Nữ</span>";
}
var GetGenderChar = function (value, p, record) {
    if (value == "M")
        return "<span style='color:blue'>Nam</span>";
    else
        return "<span style='color:red'>Nữ</span>";
}
var RenderVND = function (value, p, record) {
    return value + "VNĐ";
}
//Lấy định dạng ngày tháng bao gồm cả giờ 
var GetDateFormatIncludeTime = function (value, p, record) {
    try {
        if (value == null) return "";
        value = value.replace(" ", "T");
        var temp = value.split("T");
        var date = temp[0].split("-");
        var dateStr = date[2] + "/" + date[1] + "/" + date[0] + " " + temp[1];
        return dateStr;
    } catch (e) {

    }
}
/*
Get date format
*/
var GetDateFormat = function (value, p, record) {
    try {
        if (value == null) return "";
        value = value.replace(" ", "T");
        var temp = value.split("T");
        var date = temp[0].split("-");
        var dateStr = date[2] + "/" + date[1] + "/" + date[0]; // +" - " + temp[1].split(":")[0] + ":" + temp[1].split(":")[1]; 
        if (dateStr == '01/01/1900') {
            return "";
        }
        return dateStr;
    } catch (e) {
        var arr = (value + "").split(" ");
        var date = arr[2] + "/";
        switch (arr[1]) {
            case "Jan":
                date += "01/";
                break;
            case "Feb":
                date += "02/";
                break;
            case "Mar":
                date += "03/";
                break;
            case "Apr":
                date += "04/";
                break;
            case "May":
                date += "05/";
                break;
            case "Jul":
                date += "07/";
                break;
            case "Jun":
                date += "06/";
                break;
            case "Aug":
                date += "08/";
                break;
            case "Sep":
                date += "09/";
                break;
            case "Oct":
                date += "10/";
                break;
            case "Nov":
                date += "11/";
                break;
            case "Dec":
                date += "12/";
                break;
        }
        date += arr[3];
        if (date == '01/01/1900') {
            return "";
        }
        return date;
    }
}