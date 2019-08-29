<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HJM.Modules.UserControl.Modules_Base_DateTime_DateTime" Codebehind="DateTime.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script type="text/javascript">
    var GetTime = function (fulltime, h, m) {
        //Tue Dec 25 2012 05:00:00 GMT+0700 (SE Asia Standard Time)
        var arr = (fulltime + "").split(" ");
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
        if (h < 10)
            h = "0" + h;
        if (m < 10)
            m = "0" + m;
        date += arr[3] + " " + h + ":" + m + ":00";
        return date;
    }
</script>
<ext:Window runat="server" Width="310" Hidden="true" ID="wdInputTime" Padding="6" Modal="true"
    AutoHeight="true" Icon="Clock" Title="Nhập thời gian" Header="false">
    <Content>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <ext:SpinnerField ID="spinnerHours" AllowDecimals="false" LabelWidth="120" runat="server" MinValue="0"
                        MaxValue="24" FieldLabel="Giờ (từ 0 đến 24)" /> 
                </td>
            </tr>
            <tr>
                <td>
                    <ext:SpinnerField ID="spinnerMin" LabelWidth="120" AllowDecimals="false" runat="server"
                        MinValue="0" MaxValue="60" FieldLabel="Phút" />
                </td>
            </tr>
        </table>
    </Content>
    <Buttons>
        <ext:Button runat="server" Icon="Accept" Text="Đã nhập xong">
            <Listeners>
                <Click Handler="#{wdInputTime}.hide();
                                #{DateField1}.setValue(GetTime(#{DateField1}.getValue(),#{spinnerHours}.getValue(),#{spinnerMin}.getValue()));
                                #{hdfTimeValue}.setValue(#{DateField1}.getValue());
                                " />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
<ext:Hidden runat="server" ID="hdfTimeValue"/>  
<ext:DateField ID="DateField1" Format="dd/MM/yyyy HH:mm:ss" ShowToday="true" NextText="Tháng sau" InvalidText="Định dạng ngày tháng không hợp lệ"
    PrevText="Tháng trước" TodayText="Hôm nay" Editable="false" runat="server">
    <Triggers>
        <ext:FieldTrigger Icon="SimpleTime" Qtip="Định dạng giờ là 24 giờ" />
    </Triggers>
    <Listeners>
        <Hide Handler="this.reset();" />
    </Listeners>
</ext:DateField>
