<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteManager.aspx.cs" Inherits="TMT.License.Web.License.SiteManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head ID="Head2" runat="server">
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
   <style>
         .icon-combo-item {
            background-repeat   : no-repeat !important;
            background-position : 3px 50% !important;
            padding-left        : 24px !important;
        }
    </style>
    <script type="text/javascript">

        var searchFieldTriggerClick = function (field, trigger, index) {
            var me = field;
            if (index == 0) {
                me.setValue('');
            }
            CompanyX.Filter();

        };

        var onSpecialKey = function (field, e) {
            if (e.getKey() == e.ENTER) {
                searchFieldTriggerClick(field, null, 1);
                e.stopEvent();
            }
        };
    </script>
    <script>
        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value + "%");
        };

        
    </script> 
</head>
<body>
    <form ID="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    
    <ext:Viewport ID="Viewport1" runat="server" AutoScroll="False" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Split="true" Layout="FitLayout" Region="Center"
                Header="true">
                <DockedItems>
                    <ext:Toolbar runat="server" ID="Toolbar3" Hidden="true" Dock="Top">
                         <Items>
                            <ext:ComboBox 
                                ID="Menu" 
                                runat="server"
                                Flex="1"
                                Editable="false"
                                DisplayField="name"
                                ValueField="name"
                                QueryMode="Local"
                                TriggerAction="All"
                                EmptyText="Select Menu ...">
                                <Store>
                                    <ext:Store runat="server">
                                        <Model>
                                            <ext:Model runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="iconCls" />
                                                    <ext:ModelField Name="name" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>            
                                    </ext:Store>
                                </Store>
                                <ListConfig>
                                    <ItemTpl runat="server">
                                        <Html>
                                            <div class="icon-combo-item {iconCls}">
                                                {name}
                                            </div>
                                        </Html>                    
                                    </ItemTpl>
                                </ListConfig>
                                <Listeners>
                                    <Change Handler="if(this.valueModels.length>0){this.setIconCls(this.valueModels[0].get('iconCls'));}" />
                                </Listeners>  
                            </ext:ComboBox>  
                             
                        </Items>
                    </ext:Toolbar>
                </DockedItems>
                <TopBar>
                    <ext:Toolbar runat="server" Region="North" ID="ctl340">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="Add" ID="btAdd">
                                <DirectEvents>
                                    <Click OnEvent="btAdd_Click">
                                        <EventMask ShowMask="true" Msg="Loading..." MinDelay="100" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                           
                            <ext:Button runat="server" Icon="ApplicationFormEdit" Text="Edit" ID="btEdit">
                                <DirectEvents>
                                    <Click OnEvent="btEdit_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="grPosition_Select_Values" Value="#{grPosition}.getRowsValues({selectedOnly:true})"
                                                Encode="true" Mode="Raw" />
                                        </ExtraParams>
                                        <EventMask Msg="Loading..." MinDelay="200" ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            
                            <ext:Button runat="server" Icon="Delete" Text="Delete" ID="btDel">
                                <DirectEvents>
                                    <Click OnEvent="btDel_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="grPosition_Select_Values" Value="#{grPosition}.getRowsValues({selectedOnly:true})"
                                                    Encode="true" Mode="Raw" />
                                        </ExtraParams>
                                        <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to delete this records?" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                           
                            <ext:Button runat="server" Icon="ArrowRefresh" Text="Refresh" ID="btRefresh">
                                <DirectEvents>
                                    <Click OnEvent="btRefresh_Click">
                                        <EventMask ShowMask="true" Msg="Loading..." MinDelay="200" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                
                    
                
                <Items>
                    
                    <ext:GridPanel ID="grPosition" SelectionMemory="false" runat="server" Region="East" 
                        Layout="FitLayout" Title="" MinWidth="1000">
                        <Store>
                            <ext:Store ID="stPosition" runat="server" AutoDestroy="true" >
                                <Model>
                                    <ext:Model ID="mdPosition" runat="server" IDProperty="SiteID" Name="Session">
                                        <Fields>
                                            <ext:ModelField Name="SiteID" />
                                            <ext:ModelField Name="SiteName" />
                                            <ext:ModelField Name="SiteDetail" />
                                            <ext:ModelField Name="SiteDesp" />
                                            <ext:ModelField Name="SiteLink" />
                                            <ext:ModelField Name="SiteOrder" />
                                            <ext:ModelField Name="SiteHidden" />
                                            <ext:ModelField Name="SiteNameVi" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Listeners>
                                    <Exception Handler="Ext.Msg.alert('Operation failed', operation.getError());" />
                                </Listeners>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" Width="40" runat="server" />
                                
                                <ext:Column ID="colSiteID" runat="server" DataIndex="SiteID" Text="ID" Flex="10" Hidden="true"/>
                                <ext:Column ID="colSiteName" runat="server" DataIndex="SiteName" Text="Name"  Flex="10"  />
                                <ext:Column ID="colSiteNameVi" runat="server" DataIndex="SiteNameVi" Text="Name Vi"  Flex="10"  />
                                <ext:Column ID="colSiteDetail" runat="server" DataIndex="SiteDetail" Text="Detail" Flex="15" />
                                <ext:Column ID="colSiteDesp" runat="server" DataIndex="SiteDesp" Text="Description"  Flex="10"  />
                                <ext:Column ID="colSiteLink" runat="server" DataIndex="SiteLink" Text="Link" Flex="15" />
                                <ext:Column ID="colSiteOrder" runat="server" DataIndex="SiteOrder" Text="Order"  Flex="10"  />
                                <ext:CheckColumn ID="colSiteHidden" runat="server" DataIndex="SiteHidden" Text="Hidden"  Flex="10"  />
                                </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView ID="grvPosition" runat="server" StripeRows="true" MarkDirty="false" />
                        </View>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModelPosition" runat="server" Mode="Multi">
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <DockedItems>
                            <ext:Toolbar ID="Toolbar1" runat="server" Padding="5" Dock="Top">
                                <Items>
                                    <ext:TextField ID="txtKeyword" runat="server" Width="350" FieldLabel="Search"
                                        LabelWidth="50">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="searchFieldTriggerClick" />
                                            <SpecialKey Fn="onSpecialKey" />
                                        </Listeners>
                                    </ext:TextField>
                                </Items>
                            </ext:Toolbar>
                        </DockedItems>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="True" />
                        </BottomBar>
                        <DirectEvents>
                            <ItemDblClick OnEvent="btEdit_Click">
                                <ExtraParams>
                                    <ext:Parameter Name="grPosition_Select_Values" Value="#{grPosition}.getRowsValues({selectedOnly:true})"
                                        Encode="true" Mode="Raw" />
                                </ExtraParams>
                                <EventMask Msg="Loading..." MinDelay="200" ShowMask="true" />
                            </ItemDblClick>
                        </DirectEvents>
                    </ext:GridPanel>
                    
                </Items>
            </ext:Panel>
        </Items>
        
    </ext:Viewport>
    <ext:Window ID="winDetails" runat="server" Hidden="true" Closable="false" Resizable="false"
        Height="405" Icon="ApplicationFormEdit" Title="Site Manager" Draggable="true" Width="510"
        Modal="true" BodyPadding="5" Layout="FitLayout">
        
        <Items>
            <ext:FieldSet ID="FieldSet5" Border="false" Width="500" Height="395" runat="server"
                Anchor="100%" Padding="5">
                <Defaults>
                    <ext:Parameter Name="LabelWidth" Value="80" />
                    <ext:Parameter Name="Width" Value="470" />
                </Defaults>
                <Items>
                   <ext:TextField ID="hiID" runat="server" Hidden="true" />
                    
                   <ext:TextField ID="txtSiteName" runat="server" FieldLabel="Site Name" />
                    <ext:TextField ID="txtSiteNameVi" runat="server" FieldLabel="Site Name" />
                    <ext:TextField ID="txtSiteDetail" runat="server" FieldLabel="Site Detail" />
                    
                    <ext:TextField ID="txtSiteLink" runat="server" FieldLabel="Site Link" />
                    <ext:TextArea ID="txtSiteDesp" runat="server" FieldLabel="Site Description" />
                    <ext:NumberField ID="numSiteOrder" runat="server" FieldLabel="Order" />
                     <ext:RadioGroup
                            ID="radiohidden"
                            runat="server"
                            FieldLabel="Is Hidden"
                            ColumnsNumber="3"
                            Vertical="true">
                            <Items>
                                <ext:Radio ID="rHiddenTrue" runat="server" BoxLabel="True" />
                                <ext:Radio ID="rHiddenFalse" runat="server" BoxLabel="False" Checked="true" />
                            </Items>
                        </ext:RadioGroup>
                </Items>
            </ext:FieldSet>
        </Items>
        <Buttons>
            <ext:Button runat="server" Icon="Accept" Text="Approve" ID="btApprove">
                <DirectEvents>
                    <Click OnEvent="btApprove_Click">
                        <EventMask ShowMask="true" Msg="Saving..." MinDelay="200" />
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button runat="server" Icon="Cancel" Text="Cancel" ID="btCancel">
                <DirectEvents>
                    <Click OnEvent="btCancel_Click" />
                </DirectEvents>
            </ext:Button>
        </Buttons>
    </ext:Window>
   
    </form>
</body>
</html>