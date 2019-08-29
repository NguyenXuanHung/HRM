<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.HRM.Modules.UC.Sample" Codebehind="Sample.ascx.cs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script>
    var checkInputWdSample = function () {
        if (ucSample_txtSampleName.getValue() == '') {
            alert("Bạn phải nhập vào tên của mẫu");
            ucSample_txtSampleName.focus();
            return false;
        }
        return true;
    }
    var setValueWdSample = function () {
        var data = ucSample_chkSampleRowSelection.getSelected().data;
        if (data == null) {
            return;
        }
        ucSample_txtSampleName.setValue(data.Name);
        ucSample_txtSampleNote.setValue(data.Note);
        ucSample_btnInsertSample.hide();
        ucSample_btnUpdateSample.show(); 
    }
    var resetWdSample = function () {
        ucSample_txtSampleName.reset();
        ucSample_txtSampleNote.reset();
        ucSample_btnInsertSample.show();
        ucSample_btnUpdateSample.hide();
    }
</script>
<script src="../../../Resource/js/Extcommon.js"></script>
<ext:Hidden runat="server" ID="hdfDepartment" />
<ext:Hidden runat="server" ID="hdfIDSample" />
<ext:Window Modal="true" Resizable="false" Hidden="true" runat="server" ID="wdSample"
    Constrain="true" Title="Mẫu" Icon="Database" Width="400"
    Padding="6" AutoHeight="true">
    <Items>
        <ext:Container runat="server" AnchorHorizontal="100%" ID="ctv1" Layout="FormLayout">
            <Items>
                <ext:TextField runat="server" ID="txtSampleName" FieldLabel="Tên mẫu<span style='color:red'>*</span>"
                    AnchorHorizontal="100%" CtCls="requiredData">
                </ext:TextField>
                <ext:TextArea runat="server" ID="txtSampleNote" FieldLabel="Ghi chú" AnchorHorizontal="100%">
                </ext:TextArea>
            </Items>
        </ext:Container>
    </Items>
    <Buttons>
        <ext:Button runat="server" Text="Đồng ý" ID="btnInsertSample" Icon="Accept">
            <Listeners>
                <Click Handler="return checkInputWdSample();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnAddSample_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" Text="Đồng ý" ID="btnUpdateSample" Icon="Accept" Hidden="true">
            <Listeners>
                <Click Handler="return checkInputWdSample();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnAddSample_Click">
                    <EventMask ShowMask="true" Msg="Chờ trong giây lát..." />
                    <ExtraParams>
                        <ext:Parameter Name="command" Value="edit" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="Button4" runat="server" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdSample}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <Hide Handler="resetWdSample()" />
    </Listeners>
</ext:Window>
<ext:Window Modal="true" Resizable="false" Hidden="true" runat="server" ID="wdSampleList"
    Constrain="true" Title="Danh sách mẫu" Icon="Database" Width="650" AutoHeight="true">
    <Items>
        <ext:GridPanel runat="server" ID="grp_SampleList" Icon="Bookmark" Border="false" Header="false" Title="Danh sách mẫu"
            AnchorHorizontal="100%" Height="300" AutoExpandColumn="Name">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items> 
                        <ext:Button runat="server" Text="Sửa" ID="btnEditSample" Icon="Pencil">
                            <Listeners>
                                <Click Handler="if(CheckSelectedRows(#{grp_SampleList})){setValueWdSample(); #{wdSample}.show();}" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button runat="server" Text="Xóa" ID="btnDeleteSample" Icon="Delete">
                            <Listeners>
                                <Click Handler="return CheckSelectedRow(#{grp_SampleList});" />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="btnDeleteSample_Click">
                                    <Confirmation Title="Thông báo từ hệ thống" Message="Bạn có chắc chắn muốn xóa?"
                                        ConfirmRequest="true" />
                                    <EventMask ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Store>
                <ext:Store runat="server" ID="stSampleList">
                    <Proxy>
                        <ext:HttpProxy Method="POST" Url="/Services/HandlerSampleList.ashx" />
                    </Proxy>
                    <BaseParams>
                        <ext:Parameter Name="Departments" Value="#{hdfDepartment}.getValue()" Mode="Raw" />
                    </BaseParams>
                    <AutoLoadParams>
                        <ext:Parameter Name="start" Value="={0}" />
                        <ext:Parameter Name="limit" Value="={15}" />
                    </AutoLoadParams>
                    <Reader>
                        <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                            <Fields>
                                <ext:RecordField Name="Id" />
                                <ext:RecordField Name="Name" />
                                <ext:RecordField Name="Note" />
                                <ext:RecordField Name="CreatedDate" />
                                <ext:RecordField Name="CreatedBy" />
                            </Fields>
                        </ext:JsonReader>
                    </Reader>
                </ext:Store>
            </Store>
            <ColumnModel>
                <Columns>
                    <ext:RowNumbererColumn Header="STT" Align="Right" Width="35" />
                    <ext:TemplateColumn ColumnID="Name" Header="Tên mẫu" Align="Left" Width="150" DataIndex="Name">
                        <Template runat="server">
                            <Html>
                                <div style="line-height: 18px;">
                                    <b>{Name}</b><br />
                                <i>{Note}</i>
                                </div>
                            </Html>
                        </Template>
                    </ext:TemplateColumn>
                    <ext:Column Header="Người tạo" Align="Left" Width="110" DataIndex="CreatedBy"></ext:Column>
                    <ext:DateColumn Header="Ngày tạo" Align="Center" Width="85" DataIndex="CreatedDate" Format="dd/MM/yyyy"/>
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" ID="chkSampleRowSelection">
                    <Listeners>
                        <RowSelect Handler="#{hdfIDSample}.setValue(#{chkSampleRowSelection}.getSelected().id); #{btnEditSample}.enable(); #{btnDeleteSample}.enable();" />
                        <RowDeselect Handler="#{hdfIDSample}.reset(); #{btnEditSample}.disable(); #{btnDeleteSample}.disable();" />
                    </Listeners>
                </ext:RowSelectionModel>
            </SelectionModel>
            <Listeners>
                <RowDblClick Handler="return CheckSelectedRows(#{grp_SampleList});" />
            </Listeners>
            <DirectEvents>
                <RowDblClick OnEvent="btnDongY_Click" />
            </DirectEvents>
            <BottomBar>
                <ext:PagingToolbar ID="PagingToolbar1" EmptyMsg="Hiện không có dữ liệu" NextText="Trang sau"
                    PageSize="15" PrevText="Trang trước" LastText="Trang cuối cùng" FirstText="Trang đầu tiên"
                    DisplayMsg="Dòng {0} đến dòng {1} / {2} dòng" runat="server">
                </ext:PagingToolbar>
            </BottomBar>
        </ext:GridPanel>
    </Items>
    <Buttons>
        <ext:Button runat="server" ID="btnDongY" Text="Đồng ý" Icon="Accept">
            <Listeners>
                <Click Handler="return CheckSelectedRows(#{grp_SampleList});" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnDongY_Click" />
            </DirectEvents>
        </ext:Button>
        <ext:Button runat="server" ID="Button1" Text="Đóng lại" Icon="Decline">
            <Listeners>
                <Click Handler="#{wdSampleList}.hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
    <Listeners>
        <BeforeShow Handler="#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();" />
    </Listeners>
</ext:Window>
