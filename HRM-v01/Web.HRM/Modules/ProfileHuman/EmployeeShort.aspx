<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.ProfileHuman.EmployeeShort" Codebehind="EmployeeShort.aspx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Modules/UC/ResourceCommon.ascx" TagPrefix="CCVC" TagName="ResourceCommon" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" id="ResourceCommon" />
    <script type="text/javascript" src="/Resource/js/EmployeeSetting.js"></script>
    <script type="text/javascript">
        var ValidateInput = function () {
            if (hdfEmployeeTypeId.getValue() == '' || hdfEmployeeTypeId.getValue() == null) {
                alert('Bạn chưa nhập loại cán bộ');
                return false;
            }
            if (hdfDepartmentId.getValue() == '' || hdfDepartmentId.getValue() == null) {
                alert('Bạn chưa nhập Cơ quan, đơn vị sử dụng CBCC');
                return false;
            }
            if (txtEmployeeCode.getValue().trim() == '' || txtEmployeeCode.getValue() == null) {
                alert('Bạn chưa nhập mã cán bộ !');
                txtEmployeeCode.focus();
                return false;
            }

            if (txtFullName.getValue().trim() == '' || txtFullName.getValue() == null) {
                alert('Bạn chưa nhập họ tên !');
                txtFullName.focus();
                return false;
            }
            if (dfBirthDate.getRawValue() == '' || dfBirthDate.getRawValue() == null) {
                alert('Bạn chưa nhập ngày sinh');
                dfBirthDate.focus();
                return false;
            }
            if (ValidateDateField(dfBirthDate) == false) {
                alert('Định dạng ngày sinh không đúng');
                return false;
            }
            if (txt_Address.getValue().trim() == '' || txt_Address.getValue() == null) {
                alert('Bạn chưa nhập nơi ở hiện nay !');
                return false;
            }
            if (txtIDNumber.getValue() == '') {
                alert('Bạn chưa nhập số chứng minh thư');
                txtIDNumber.focus();
                return false;
            }
            if (txtCellPhoneNumber.getValue().trim() == '' || txtCellPhoneNumber.getValue() == null) {
                alert('Bạn chưa nhập số điện thoại di động !');
                txtCellPhoneNumber.focus();
                return false;
            }
        
            return true;
        }
    </script>
</head>
<body>
    <form id="frmEmployeeShort" runat="server">
        <div class="main">
            <ext:ResourceManager ID="RM" runat="server"/>
            
            <!-- Store lấy đơn vị theo người dùng đăng nhập -->
            <ext:Store runat="server" ID="storeDepartment" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
             <!-- Store lấy loại cán bộ -->
            <ext:Store runat="server" ID="storeEmployeeType" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="Id">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <script type="text/javascript">
                function changeDepartment(id) {
                    Ext.net.DirectMethods.ChangeDepartment(id);
                }
            </script>
            <ext:Window runat="server" Title="Thêm nhanh hồ sơ thu gọn" Resizable="true" Layout="FormLayout"
                Padding="6" Width="870" Hidden="true" Icon="UserTick" ID="wdEmployeeShort" Draggable="false"
                Modal="true" Constrain="true" Height="400">
                <Items>
                    <ext:FieldSet runat="server" ID="fdCurriculumVitae"  Layout="FormLayout"
                        AnchorHorizontal="100%" LabelWidth="200" Height="300" Padding="5">
                        <Items>
                            <ext:Hidden runat="server" ID="hdfManagementDepartmentId" />
                            <ext:TextField runat="server" ID="txtManagementDepartment" FieldLabel="Cơ quan, đơn vị có thẩm quyền quản lý CBCC"
                                AllowBlank="false" Width="440"  Disabled="True" AnchorHorizontal="100%">
                            </ext:TextField>
                            <ext:TextField ID="txtEmployeeCode" runat="server" FieldLabel="Số hiệu cán bộ, công chức"
                                CtCls="requiredData" AllowBlank="false" MaxLength="20" AnchorHorizontal="100%"
                                MaxLengthText="Bạn chỉ được nhập tối đa 20 ký tự" LabelWidth="260" Width="440"/>
                            <ext:Hidden ID="hdfEmployeeTypeId" runat="server"></ext:Hidden>
                            <ext:ComboBox runat="server" ID="cboEmployeeType" FieldLabel="Loại cán bộ" EmptyText="Chọn loại cán bộ"
                                LabelWidth="260" Width="440" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                ItemSelector="div.list-item" Editable="false" StoreID="storeEmployeeType" AnchorHorizontal="100%">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template runat="server">
                                    <Html>
                                        <tpl for=".">
						                        <div class="list-item"> 
							                        {Name}
						                        </div>
					                        </tpl>
                                    </Html>
                                </Template>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();hdfEmployeeTypeId.setValue(cboEmployeeType.getValue());" />
                                    <BeforeQuery Handler="this.triggers[0][this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                    <TriggerClick Handler="if (index == 0) {this.clearValue(); this.triggers[0].hide();}" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="hdfDepartmentId" />
                            <ext:ComboBox runat="server" ID="cboDepartment" FieldLabel="Cơ quan, đơn vị sử dụng CBCC" EmptyText="Chọn đơn vị"
                                LabelWidth="260" Width="440" AllowBlank="false" DisplayField="Name" ValueField="Id" CtCls="requiredData"
                                ItemSelector="div.list-item" Editable="false" StoreID="storeDepartment" AnchorHorizontal="100%">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Template runat="server">
                                    <Html>
                                        <tpl for=".">
						                    <div class="list-item">
						                        {Name} 
						                    </div>
					                    </tpl>
                                    </Html>
                                </Template>
                                <Listeners>
                                    <Select Handler="this.triggers[0].show();warningUseDepartment(cboDepartment, hdfDepartmentId);changeDepartment(cboDepartment.getValue());" />
                                    <TriggerClick Handler="if (index == 0) {this.clearValue();this.triggers[0].hide();};hdfDepartmentId.reset();" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:TextField ID="txtFullName" runat="server" CtCls="requiredData" FieldLabel="Họ và tên khai sinh"
                                AllowBlank="false" MaxLength="50" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự"
                                LabelWidth="152" Width="440" AnchorHorizontal="100%">
                                <Listeners>
                                    <Blur Handler="ChuanHoaTen(#{txtFullName});" />
                                </Listeners>
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Title="Hướng dẫn" Html="Phần mềm sẽ tự động chuẩn hóa họ và tên của bạn. Ví dụ: bạn nhập nguyễn văn huy, kết quả trả về Nguyễn Văn Huy." />
                                </ToolTips>
                            </ext:TextField>
                            <ext:CompositeField runat="server" ID="cps" LabelWidth="152" FieldLabel="Ngày sinh" AnchorHorizontal="100%">
                                <Items>
                                    <ext:DateField runat="server" ID="dfBirthDate" CtCls="requiredData"
                                        AnchorHorizontal="100%" MaskRe="/[0-9\/]/" Format="d/M/yyyy" Regex="/^(3[0-1]|[0-2]?[0-9])\/(1[0-2]|0?[0-9])\/[0-9]{4}$/"
                                        RegexText="Định dạng ngày sinh không đúng" Width="147">
                                        <Listeners>
                                            <Blur Handler="checkNgaySinh(dfBirthDate,18);" />
                                        </Listeners>
                                    </ext:DateField>
                                    <ext:DisplayField runat="server" Text="Giới tính" />
                                    <ext:ComboBox ID="cbxSex" runat="server"
                                        AnchorHorizontal="100%" SelectedIndex="0" AllowBlank="false" Width="68">
                                        <Items>
                                            <ext:ListItem Text="Nam" Value="M" />
                                            <ext:ListItem Text="Nữ" Value="F" />
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:CompositeField>
                            <ext:TextField ID="txt_Address" runat="server" FieldLabel="Nơi ở hiện nay"
                                Width="440" LabelWidth="152" CtCls="requiredData" AnchorHorizontal="100%">
                            </ext:TextField>
                            <ext:TextField ID="txtIDNumber" CtCls="requiredDataWG" runat="server" MaskRe="/[0-9]/"
                                FieldLabel="Số chứng minh nhân dân" AllowBlank="true"
                                MaxLength="12" MaxLengthText="Bạn chỉ được nhập tối đa 50 ký tự" LabelWidth="200"
                                Width="440" AnchorHorizontal="100%">
                            </ext:TextField>
                            <ext:TextField ID="txtCellPhoneNumber" runat="server" MaxLength="50" LabelWidth="152" Width="440" CtCls="requiredData" MaskRe="/[0-9]/" FieldLabel="Di động" AnchorHorizontal="100%"/>                                   
                            <ext:TextField runat="server" MaxLength="200" ID="txtPersonalEmail" LabelWidth="152" Width="440" FieldLabel="Email riêng" AnchorHorizontal="100%"/>
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Buttons>
                     <ext:Button runat="server" ID="btnSave" Text="Lưu hồ sơ" Icon="Disk">
                        <Listeners>
                            <Click Handler="return ValidateInput();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnSave_Click">
                                <EventMask ShowMask="true" Msg="Đang cập nhật dữ liệu..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnClose" Text="Đóng lại" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdEmployeeShort.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
