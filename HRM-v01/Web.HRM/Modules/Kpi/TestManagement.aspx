<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestManagement.aspx.cs" Inherits="Web.HRM.Modules.Kpi.TestManagement" %>
<%@ Register Src="/Modules/UC/ResourceCommon.ascx" TagName="ResourceCommon" TagPrefix="CCVC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <CCVC:ResourceCommon runat="server" ID="ResourceCommon" />
    <ext:ResourcePlaceHolder runat="server" Mode="Script" />
    <script type="text/javascript">       
        window.lookup = [];
        
        var clean = function () {
            if (window.lookup.length > 0) {
                RowExpander1.collapseAll();
                Ext.each(window.lookup, function (control) {
                    if (control) {
                        control.destroy();
                    }
                });
                
                window.lookup = [];
            }            
        };
        
        var removeFromCache = function(c){             
            var c = window[c];
            window.lookup.remove(c);   
            if (c){
                c.destroy();                
            }       
        }
        
        var addToCache = function(c){
            window.lookup.push(window[c]);      
        }
       

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <ext:ResourceManager ID="ResourceManager1" runat="server">
            </ext:ResourceManager>
            <ext:Hidden runat="server" ID="hdfId" />
            <ext:Hidden runat="server" ID="hdfMonth" />
            <ext:Hidden runat="server" ID="hdfYear" />
            <ext:Hidden runat="server" ID="hdfCriterionArgumentId" />
            
            <!-- store -->
            <ext:Store runat="server" ID ="Store1" >
                <Reader>
                    <ext:JsonReader IDProperty="ID">
                        <Fields>
                            <ext:RecordField Name="ID" />
                            <ext:RecordField Name="Name" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>
           
        

          
            <!-- viewport -->
            <ext:Viewport ID="viewport" runat="server" HideBorders="true">
                <Items>
                    <ext:BorderLayout runat="server" ID="brlayout">
                        <Center>
                            <ext:GridPanel 
                                ID="GridPanel1" 
                                runat="server" 
                                StoreID="Store1" 
                                TrackMouseOver="true"
                                Title="Expander Rows with ListView" 
                                Collapsible="true"
                                AnimCollapse="true" 
                                Icon="Table" 
                                Width="600" 
                                Height="600">
                                <ColumnModel runat="server">
                                    <Columns>
                                        <ext:Column Header="Họ và tên" DataIndex="Name" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView ID="GridView1" runat="server" ForceFit="true">
                                        <Listeners>
                                            <BeforeRefresh Fn="clean" />
                                        </Listeners>
                                    </ext:GridView>
                                </View>
                                <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                                </SelectionModel>
                                <Plugins>
                                    <ext:RowExpander ID="RowExpander1" runat="server">
                                        <Template runat="server">
                                            <Html>
                                            <div id="row-{ID}" style="background-color:White;"></div>
                                            </Html>
                                        </Template>
                    
                                        <DirectEvents>
                                            <BeforeExpand OnEvent="BeforeExpand">
                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={GridPanel1.body}" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="id" Value="record.id" Mode="Raw" />
                                                </ExtraParams>
                                            </BeforeExpand>
                                        </DirectEvents>
                                    </ext:RowExpander>
                                </Plugins>
                            </ext:GridPanel>
                        </Center>
                    </ext:BorderLayout>
                </Items>
            </ext:Viewport>

        </div>
    </form>
</body>
</html>
