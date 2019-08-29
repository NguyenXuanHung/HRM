<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CriterionManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.CriterionManagement" %>

<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <script type="text/javascript">       
        var handlerKeyPressEnter = function (f, e) {
            if (e.getKey() === e.ENTER) {
                // reload grid
                reloadGrid();
                // show keyword trigger
                if (this.getValue() === '')
                    this.triggers[0].hide();
            }
            if (this.getValue() !== '') {
                this.triggers[0].show();
            }
        };
        var reloadGrid = function () {
            gpCriterion.getSelectionModel().clearSelections();
            pagingToolbar.pageIndex = 0;
            pagingToolbar.doLoad();

        }
        var validateForm = function () {
            if (txtName.getValue() === '' || txtName.getValue().trim === '') {
                alert('Bạn chưa nhập tên tiêu chí!');
                return false;
            }

            if (hdfValueType.getValue() === '' || hdfValueType.getValue().trim === '') {
                alert('Bạn chưa chọn kiểu dữ liệu!');
                return false;
            }

            return true;
        }
        var validateFormula = function() {
            if (txtCriterionValue.getValue().trim() === '' || txtCriterionValue.getValue() === '') {
                alert('Bạn chưa nhập mã tính toán tham số');
                return false;
            }
            return true;
        }
        var iconImg = function (name) {
            return "<img src='/Resource/icon/" + name + ".png'>";
        }
        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('cross');
            }
            return '';
        }
        var showTip = function () {
            var rowIndex = gpCriterion.view.findRowIndex(this.triggerElement),
                cellIndex = gpCriterion.view.findCellIndex(this.triggerElement),
                record = storeCriterion.getAt(rowIndex);
            var fieldName = gpCriterion.getColumnModel().getDataIndex(cellIndex);
            if (!isNaN(parseInt(rowIndex)))
                data = record.get(fieldName);
            else
                data = '';
            this.body.dom.innerHTML = data;
        };
        var RenderValueTypeName = function (value, p, record) {
            var icon = '';
            var style = 'font-style:italic;' +
                'color:green;' +
                'font-weight:bold;' +
                'letter-spacing:-1px;';
            switch (record.data.ValueType) {
                case 'Number':
                    icon = "<span style='" + style + "'>123</span>";
                    break;
                case 'String':
                    icon = iconImg('text_ab');
                    break;
                case 'Percent':
                    icon = "<span style='" + style + "'>%</span>";
                    break;
                case 'Formula':
                    icon = iconImg('sum');
                    break;
            }
            return icon;
        }
        var RenderRowStatus = function (value, p, record) {
            if (record.data.Status === "Active") {
                return iconImg('tick');
            } else if (record.data.Status === "Locked") {
                return iconImg('cross');
            }
            return '';
        }
        var RenderFormula = function (value, p, record) {
            var result = '';
            var formulaRanges = JSON.parse(record.data.FormulaRange);
            if (formulaRanges) {
                for (var f of formulaRanges) {
                    result += `<span style='color:${f.Color};font-weight:bold;'>${f.Result}</span>: `;
                    if (!f.Greater)
                        result += `> ${f.Smaller - 1}`;
                    else if (!f.Smaller)
                        result += `< ${f.Greater} | `;
                    else if (f.Smaller === f.Greater - 1)
                        result += `${f.Smaller} | `;
                    else
                        result += `${f.Smaller} - ${f.Greater - 1} | `;
                    result = `<span>${result}</span>`;
                }
                result = `<p><b>${formulaRanges[0].Value}</b> : ${result}</p>`;
            }

            return result;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ext:ResourceManager ID="RM" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfOrder" />
            <ext:Hidden runat="server" ID="hdfFormulaRange" />

            <!-- store -->
            <ext:Store runat="server" ID="storeArgument">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx"></ext:HttpProxy>
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={10}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Argument" Mode="Value" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="CalculateCode" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <ext:Store runat="server" ID="storeCriterion" AutoSave="True">
                <Proxy>
                    <ext:HttpProxy Method="GET" Url="~/Services/Kpi/HandlerKpi.ashx" />
                </Proxy>
                <AutoLoadParams>
                    <ext:Parameter Name="start" Value="={0}" />
                    <ext:Parameter Name="limit" Value="={30}" />
                </AutoLoadParams>
                <BaseParams>
                    <ext:Parameter Name="handlers" Value="Criterion" Mode="Value" />
                    <ext:Parameter Name="query" Value="txtKeyword.getValue()" Mode="Raw" />
                    <ext:Parameter Name="order" Value="hdfOrder.getValue()" Mode="Raw" />
                </BaseParams>
                <Reader>
                    <ext:JsonReader IDProperty="Id" Root="Data" TotalProperty="TotalRecords">
                        <Fields>
                            <ext:RecordField Name="Id" />
                            <ext:RecordField Name="Code" />
                            <ext:RecordField Name="Name" />
                            <ext:RecordField Name="Formula" />
                            <ext:RecordField Name="FormulaRange" />
                            <ext:RecordField Name="Description" />
                            <ext:RecordField Name="Status" />
                            <ext:RecordField Name="ValueType" />
                            <ext:RecordField Name="ValueTypeName" />
                            <ext:RecordField Name="Order" />
                            <ext:RecordField Name="CreatedDate" />
                            <ext:RecordField Name="CreatedBy" />
                            <ext:RecordField Name="EditedDate" />
                            <ext:RecordField Name="EditedBy" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel runat="server" ID="gpCriterion" StoreID="storeCriterion" TrackMouseOver="true" Header="false" StripeRows="true" Border="false" AnchorHorizontal="100%" Layout="Fit" AutoScroll="True" AutoExpandColumn="Formula">
                                <TopBar>
                                    <ext:Toolbar runat="server" ID="toolbarFn">
                                        <Items>
                                            <ext:Button runat="server" ID="btnAdd" Text="Thêm" Icon="Add">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEdit" Text="Sửa" Icon="Pencil" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="InitWindow">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Command" Value="Update" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnDelete" Text="Xóa" Icon="Delete" Disabled="true">
                                                <DirectEvents>
                                                    <Click OnEvent="Delete">
                                                        <EventMask ShowMask="true" Msg="Đang tải..." />
                                                        <Confirmation Title="Thông báo" ConfirmRequest="true" Message="Bạn có chắc chắn muốn xóa dữ liệu này?" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="btnEvaluation" Text="Đánh giá" Icon="Calculator" Disabled="true" Hidden="True">
                                                <DirectEvents>
                                                    <Click OnEvent="EvaluationClick">
                                                        <EventMask ShowMask="true" Msg="Đang xử lý dữ liệu. Vui lòng chờ..." />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:ToolbarSpacer Width="5" />
                                            <ext:ToolbarSeparator />
                                            <ext:Button runat="server" ID="btnRefresh" Text="Tải lại" Icon="ArrowRefresh">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();"></Click>
                                                </Listeners>
                                            </ext:Button>
                                            <ext:ToolbarFill />
                                            <ext:TriggerField runat="server" ID="txtKeyword" EnableKeyEvents="true" Width="220" EmptyText="Nhập từ khóa tìm kiếm">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="True" />
                                                </Triggers>
                                                <Listeners>
                                                    <KeyPress Fn="handlerKeyPressEnter" />
                                                    <TriggerClick Handler="this.triggers[0].hide(); this.clear(); reloadGrid();" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="Zoom">
                                                <Listeners>
                                                    <Click Handler="reloadGrid();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn Header="STT" Width="30" Align="Center" />
                                        <ext:Column ColumnID="Name" Header="Tên tiêu chí" Width="250" Align="Left" DataIndex="Name" />
                                        <ext:Column ColumnID="Code" Header="Mã tiêu chí" Width="200" Align="Left" DataIndex="Code" />
                                        <ext:Column ColumnID="Description" Header="Mô tả" Width="200" Align="Left" DataIndex="Description" />
                                        <ext:Column ColumnID="Formula" Header="Công thức" Width="250" Align="Left" DataIndex="Formula">
                                            <Renderer Fn="RenderFormula"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="ValueTypeName" Header="Kiểu dữ liệu" Width="80" Align="Center" DataIndex="ValueTypeName">
                                            <Renderer Fn="RenderValueTypeName"></Renderer>
                                        </ext:Column>
                                        <ext:Column ColumnID="Order" Header="Thứ tự" Width="55" Align="Right" DataIndex="Order" />
                                        <ext:Column ColumnID="Status" Width="100" Header="Trạng thái" Align="Center" DataIndex="Status">
                                            <Renderer Fn="RenderRowStatus" />
                                        </ext:Column>
                                        <ext:Column ColumnID="CreatedBy" Header="Người tạo" Width="80" DataIndex="CreatedBy" />
                                        <ext:DateColumn ColumnID="CreatedDate" Header="Ngày tạo" Width="80" DataIndex="CreatedDate" Format="dd/MM/yyyy" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="rowSelectionModel" SingleSelect="True">
                                        <Listeners>
                                            <RowSelect Handler="hdfId.setValue(rowSelectionModel.getSelected().get('Id'));btnEdit.enable();btnDelete.enable();btnEvaluation.enable();" />
                                            <RowDeselect Handler="btnEdit.disable();btnDelete.disable();hdfId.reset();btnEvaluation.disable();" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="pagingToolbar" PageSize="30" DisplayInfo="true" DisplayMsg="Từ {0} - {1} / {2}" EmptyMsg="Không có dữ liệu">
                                        <Items>
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:Label runat="server" Text="Số bản ghi trên trang:" />
                                            <ext:ToolbarSpacer runat="server" Width="10" />
                                            <ext:ComboBox runat="server" Width="80">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="20" />
                                                    <ext:ListItem Text="30" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                </Items>
                                                <SelectedItem Value="30" />
                                                <Listeners>
                                                    <Select Handler="#{pagingToolbar}.pageSize=parseInt(this.getValue());#{pagingToolbar}.doLoad();"></Select>
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Listeners>
                                            <Change Handler="rowSelectionModel.clearSelections();btnEdit.disable();btnDelete.disable();" />
                                        </Listeners>
                                    </ext:PagingToolbar>
                                </BottomBar>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

            <%--tooltips grid--%>
            <ext:ToolTip
                ID="RowTip"
                runat="server"
                Target="={gpCriterion.getView().mainBody}"
                Delegate=".x-grid3-cell"
                TrackMouse="true" AutoWidth="True">
                <Listeners>
                    <Show Fn="showTip" />
                </Listeners>
            </ext:ToolTip>

            <ext:Window runat="server" ID="wdSetting" Title="Thêm mới tiêu chí đánh giá" Resizable="true" Layout="FormLayout"
                Padding="6" Width="800" Hidden="true" Icon="UserTick"
                Modal="true" Constrain="true" Height="600">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="Form">
                        <Items>
                            <ext:Container ID="Container2" runat="server" LabelWidth="120" LabelAlign="left"
                                Layout="Form" ColumnWidth="0.5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtName" CtCls="requiredData" FieldLabel="Tên tiêu chí"
                                        AnchorHorizontal="100%" />
                                    <ext:TextField runat="server" ID="txtCode" CtCls="requiredData" FieldLabel="Mã tiêu chí"
                                        AnchorHorizontal="100%" Hidden="True" />
                                    <ext:Hidden runat="server" ID="hdfValueType" />
                                    <ext:ComboBox runat="server" ID="cboValueType" FieldLabel="Loại dữ liệu" CtCls="requiredData" AnchorHorizontal="100%"
                                        DisplayField="Name" ValueField="Id">
                                        <Store>
                                            <ext:Store runat="server" ID="storeValueType" OnRefreshData="storeValueType_OnRefreshData" AutoLoad="False">
                                                <Reader>
                                                    <ext:JsonReader IDProperty="Id">
                                                        <Fields>
                                                            <ext:RecordField Name="Id" Mapping="Key" />
                                                            <ext:RecordField Name="Name" Mapping="Value" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <Listeners>
                                            <Expand Handler="if(#{cboValueType}.store.getCount() == 0){storeValueType.reload();}"></Expand>
                                            <Select Handler="hdfValueType.setValue(cboValueType.getValue());"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextArea runat="server" ID="txtFormula" FieldLabel="Công thức" AnchorHorizontal="100%" />
                                    <ext:Button runat="server" ID="btnAddRange" FieldLabel="Tạo công thức" Icon="Add">
                                        <DirectEvents>
                                            <Click OnEvent="btnAddRange_Click"></Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:TextField runat="server" ID="txtDescription" FieldLabel="Mô tả" AnchorHorizontal="100%" />
                                    <ext:NumberField runat="server" ID="txtOrder" FieldLabel="Thứ tự" AnchorHorizontal="100%" />
                                    <ext:Checkbox runat="server" FieldLabel="Trạng thái" BoxLabel="Đang sử dụng" ID="chkIsActive" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSave" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validateForm();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="InsertOrUpdate">
                                <EventMask ShowMask="true" Msg="Đang tải..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnCancel" Text="Hủy" Icon="Decline" TabIndex="13">
                        <Listeners>
                            <Click Handler="wdSetting.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>

            <ext:Window runat="server" ID="wdFormula" Height="400" Width="600" Hidden="True" Title="Tạo công thức" Resizable="true" Padding="5">
                <Items>
                    <ext:Container ID="ctnFormula" runat="server" Layout="Form" Height="400" LabelWidth="120">
                        <Items>
                            <ext:CompositeField runat="server" AnchorHorizontal="100%" FieldLabel="Mã tính toán tham số">
                                <Items>
                                    <ext:TextField runat="server" ID="txtCriterionValue" Width="220" EmptyText="Nhập các tham số">
                                        <DirectEvents>
                                            <Change OnEvent="txtCriterionValue_OnChange"></Change>
                                        </DirectEvents>
                                    </ext:TextField>
                                    <ext:ComboBox runat="server" ID="cboCriterionValue" StoreID="storeArgument" EmptyText="Chọn mã tính toán" PageSize="10"
                                        Width="220" ItemSelector="div.list-item" DisplayField="CalculateCode" ValueField="CalculateCode">
                                        <Template runat="server">
                                            <Html>
                                                <tpl for=".">
                                                <div class="list-item"><b>{CalculateCode}</b> - {Name}</div>
                                            </tpl>
                                            </Html>
                                        </Template>
                                        <DirectEvents>
                                            <Select OnEvent="cboCriterionValue_OnSelect"></Select>
                                        </DirectEvents>
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        </Triggers>
                                        <Listeners>
                                            <Select Handler="this.triggers[0].show();" />
                                            <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:CompositeField>
                            <ext:CompositeField ID="CompositeField1" runat="server" AnchorHorizontal="100%" FieldLabel="Giới hạn">
                                <Items>
                                    <ext:NumberField runat="server" ID="txtSmaller1" Width="50" Disabled="True" />
                                    <ext:DisplayField runat="server" Text="<=" />
                                    <ext:TextField runat="server" ID="txtValue1" Width="50" Disabled="True" />
                                    <ext:DisplayField runat="server" Text="<" />
                                    <ext:NumberField runat="server" ID="txtGreater1" Width="50">
                                        <DirectEvents>
                                            <Change OnEvent="txtGreater_OnChange">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Id" Value="1"/>
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>
                                    </ext:NumberField>
                                    <ext:TextField runat="server" ID="txtResult1" Width="150" EmptyText="Kết quả hiển thị" />
                                    <ext:DropDownField runat="server" ID="dropDownField1" Editable="false" EmptyText="Màu kết quả" Width="90">
                                        <Component>
                                            <ext:Panel runat="server" Width="160">
                                                <Items>
                                                    <ext:ColorPalette ID="colorPalette1" runat="server">
                                                        <Listeners>
                                                            <Select Handler="dropDownField1.setValue(colorPalette1.value)"></Select>
                                                        </Listeners>
                                                    </ext:ColorPalette>
                                                </Items>
                                            </ext:Panel>
                                        </Component>
                                    </ext:DropDownField>
                                </Items>
                            </ext:CompositeField>
                            <ext:CompositeField ID="CompositeField2" runat="server" AnchorHorizontal="100%">
                                <Items>
                                    <ext:NumberField runat="server" ID="txtSmaller2" Width="50" />
                                    <ext:DisplayField runat="server" Text="<=" />
                                    <ext:TextField runat="server" ID="txtValue2" Width="50" Disabled="True" />
                                    <ext:DisplayField runat="server" Text="<" />
                                    <ext:NumberField runat="server" ID="txtGreater2" Width="50">
                                        <DirectEvents>
                                            <Change OnEvent="txtGreater_OnChange">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Id" Value="2"/>
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>
                                    </ext:NumberField>
                                    <ext:TextField runat="server" ID="txtResult2" Width="150" EmptyText="Kết quả hiển thị" />
                                    <ext:DropDownField runat="server" ID="dropDownField2" Editable="false" EmptyText="Màu kết quả" Width="90">
                                        <Component>
                                            <ext:Panel runat="server" Width="160">
                                                <Items>
                                                    <ext:ColorPalette ID="colorPalette2" runat="server">
                                                        <Listeners>
                                                            <Select Handler="dropDownField2.setValue(colorPalette2.value)"></Select>
                                                        </Listeners>
                                                    </ext:ColorPalette>
                                                </Items>
                                            </ext:Panel>
                                        </Component>
                                    </ext:DropDownField>
                                </Items>
                            </ext:CompositeField>
                            <ext:CompositeField ID="CompositeField3" runat="server" AnchorHorizontal="100%">
                                <Items>
                                    <ext:NumberField runat="server" ID="txtSmaller3" Width="50" />
                                    <ext:DisplayField runat="server" Text="<=" />
                                    <ext:TextField runat="server" ID="txtValue3" Width="50" Disabled="True" />
                                    <ext:DisplayField runat="server" Text="<" />
                                    <ext:NumberField runat="server" ID="txtGreater3" Width="50">
                                        <DirectEvents>
                                            <Change OnEvent="txtGreater_OnChange">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Id" Value="3"/>
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>
                                    </ext:NumberField>
                                    <ext:TextField runat="server" ID="txtResult3" Width="150" EmptyText="Kết quả hiển thị" />
                                    <ext:DropDownField runat="server" ID="dropDownField3" Editable="false" EmptyText="Màu kết quả" Width="90">
                                        <Component>
                                            <ext:Panel runat="server" Width="160">
                                                <Items>
                                                    <ext:ColorPalette ID="colorPalette3" runat="server">
                                                        <Listeners>
                                                            <Select Handler="dropDownField3.setValue(colorPalette3.value)"></Select>
                                                        </Listeners>
                                                    </ext:ColorPalette>
                                                </Items>
                                            </ext:Panel>
                                        </Component>
                                    </ext:DropDownField>
                                </Items>
                            </ext:CompositeField>
                            <ext:CompositeField ID="CompositeField4" runat="server" AnchorHorizontal="100%">
                                <Items>
                                    <ext:NumberField runat="server" ID="txtSmaller4" Width="50" />
                                    <ext:DisplayField runat="server" Text="<=" />
                                    <ext:TextField runat="server" ID="txtValue4" Width="50" Disabled="True" />
                                    <ext:DisplayField runat="server" Text="<" />
                                    <ext:NumberField runat="server" ID="txtGreater4" Width="50">
                                        <DirectEvents>
                                            <Change OnEvent="txtGreater_OnChange">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Id" Value="4"/>
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>
                                    </ext:NumberField>
                                    <ext:TextField runat="server" ID="txtResult4" Width="150" EmptyText="Kết quả hiển thị" />
                                    <ext:DropDownField runat="server" ID="dropDownField4" Editable="false" EmptyText="Màu kết quả" Width="90">
                                        <Component>
                                            <ext:Panel runat="server" Width="160">
                                                <Items>
                                                    <ext:ColorPalette ID="colorPalette4" runat="server">
                                                        <Listeners>
                                                            <Select Handler="dropDownField4.setValue(colorPalette4.value)"></Select>
                                                        </Listeners>
                                                    </ext:ColorPalette>
                                                </Items>
                                            </ext:Panel>
                                        </Component>
                                    </ext:DropDownField>
                                </Items>
                            </ext:CompositeField>
                            <ext:CompositeField ID="CompositeField5" runat="server" AnchorHorizontal="100%">
                                <Items>
                                    <ext:NumberField runat="server" ID="txtSmaller5" Width="50" />
                                    <ext:DisplayField runat="server" Text="<=" />
                                    <ext:TextField runat="server" ID="txtValue5" Width="50" Disabled="True" />
                                    <ext:DisplayField runat="server" Text="<" />
                                    <ext:NumberField runat="server" ID="txtGreater5" Width="50" Disabled="True">
                                        <DirectEvents>
                                            <Change OnEvent="txtGreater_OnChange">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Id" Value="5"/>
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>
                                    </ext:NumberField>
                                    <ext:TextField runat="server" ID="txtResult5" Width="150" EmptyText="Kết quả hiển thị" />
                                    <ext:DropDownField runat="server" ID="dropDownField5" Editable="false" EmptyText="Màu kết quả" Width="90">
                                        <Component>
                                            <ext:Panel runat="server" Width="160">
                                                <Items>
                                                    <ext:ColorPalette ID="colorPalette5" runat="server">
                                                        <Listeners>
                                                            <Select Handler="dropDownField5.setValue(colorPalette5.value)"></Select>
                                                        </Listeners>
                                                    </ext:ColorPalette>
                                                </Items>
                                            </ext:Panel>
                                        </Component>
                                    </ext:DropDownField>
                                </Items>
                            </ext:CompositeField>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnFormula" Text="Lưu" Icon="Disk">
                        <Listeners>
                            <Click Handler="return validateFormula();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="btnFormula_Click"></Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button2" Text="Hủy" Icon="Decline">
                        <Listeners>
                            <Click Handler="wdFormula.hide()" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </div>
    </form>
</body>
</html>
