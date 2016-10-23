<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectManager.aspx.cs" Inherits="TMT.License.Web.License.ProjectManager" %>

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
                                    <ext:Model ID="mdPosition" runat="server" IDProperty="ProjectID" Name="Session">
                                        <Fields>
                                            <ext:ModelField Name="ProjectID" />
                                            <ext:ModelField Name="ProjectName" />
                                            <ext:ModelField Name="ProjectDetail" />
                                            <ext:ModelField Name="ProjectTypeID" />
                                            <ext:ModelField Name="ProjectImg" />
                                            <ext:ModelField Name="ProjectTypeName" />
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
                                <ext:Column ID="colProjectID" runat="server" DataIndex="ProjectID" Text="ID" Flex="10" Hidden="true"/>
                                <ext:Column ID="colProjectName" runat="server" DataIndex="ProjectName" Text="Name"  Flex="10"  />
                                <ext:Column ID="colProjectDetail" runat="server" DataIndex="ProjectDetail" Text="Detail" Flex="15" />
                                <ext:Column ID="colProjectTypeID" runat="server" DataIndex="ProjectTypeID" Text="Type"  Flex="10"  Hidden="true"/>
                                <ext:Column ID="ColProjectTypeName" runat="server" DataIndex="ProjectTypeName" Text="Type"  Flex="10"  />
                                <ext:Column ID="colProjectImg" runat="server" DataIndex="ProjectImg" Text="Img" Flex="15" />
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
        Height="285" Icon="ApplicationFormEdit" Title="Project Manager" Draggable="true" Width="510"
        Modal="true" BodyPadding="5" Layout="FitLayout">
        
        <Items>
            <ext:FieldSet ID="FieldSet5" Border="false" Width="500" Height="275" runat="server"
                Anchor="100%" Padding="5">
                <Defaults>
                    <ext:Parameter Name="LabelWidth" Value="80" />
                    <ext:Parameter Name="Width" Value="470" />
                </Defaults>
                <Items>
                   <ext:TextField ID="hiID" runat="server" Hidden="true" />
                    
                    <ext:TextField ID="txtProjectName" runat="server" FieldLabel="Project Name" />
                    <ext:TextField ID="txtProjectDetail" runat="server" FieldLabel="Project Detail"/>
                    <ext:ComboBox ID="cbbProjectType" runat="server" TypeAhead="true" QueryMode="Local" ForceSelection="true"
                        Editable="false" TriggerAction="All" FieldLabel="Project Type" DisplayField="ProjectTypeName"
                        ValueField="ProjectTypeID">
                        <Store>
                            <ext:Store ID="stcbbPID" runat="server">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="ProjectTypeID" />
                                            <ext:ModelField Name="ProjectTypeName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                    </ext:ComboBox>
                    <ext:TextField ID="txtProjectImg" runat="server" FieldLabel="Project Image"  Text=""/>
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