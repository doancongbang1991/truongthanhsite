<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FooterManager.aspx.cs" Inherits="TMT.License.Web.License.FooterManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
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
    <form id="Form1" runat="server">
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
                                    <ext:Model ID="mdPosition" runat="server" IDProperty="FooterID" Name="Session">
                                        <Fields>
                                            <ext:ModelField Name="FooterID" />
                                            <ext:ModelField Name="FooterName" />
                                            <ext:ModelField Name="FooterIcon" />
                                            <ext:ModelField Name="FooterAllowLink" />
                                            <ext:ModelField Name="FooterLink" />
                                            <ext:ModelField Name="FooterTypeID" />
                                            <ext:ModelField Name="FooterTypeName" />
                                            <ext:ModelField Name="FooterSubMenu" />
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
                                <ext:Column ID="colFooterID" runat="server" DataIndex="FooterID" Text="ID" Flex="10" Hidden="true"/>
                                <ext:Column ID="colFooterName" runat="server" DataIndex="FooterName" Text="Name"  Flex="10"  />
                                <ext:Column ID="colFooterIcon" runat="server" DataIndex="FooterIcon" Text="Icon" Flex="15" />
                                <ext:CheckColumn ID="colFooterAllowLink" runat="server" DataIndex="FooterAllowLink" Text="Allow Link"  Flex="10"  />
                                <ext:HyperlinkColumn ID="colFooterLink" runat="server" DataIndex="FooterLink" Text="Link"  Flex="20"  />
                                <ext:Column ID="colFooterTypeID" runat="server" DataIndex="FooterTypeID" Text="Type"  Flex="10" Hidden="true" />
                                <ext:Column ID="colFooterTypeName" runat="server" DataIndex="FooterTypeName" Text="Type Name" Flex="15" />
                                <ext:CheckColumn ID="colFooterSubMenu" runat="server" DataIndex="FooterSubMenu" Text="Is Sub Menu" Flex="15" />
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
        Height="325" Icon="ApplicationFormEdit" Title="Footer Manager" Draggable="true" Width="510"
        Modal="true" BodyPadding="5" Layout="FitLayout">
        
        <Items>
            <ext:FieldSet ID="FieldSet5" Border="false" Width="500" Height="325" runat="server"
                Anchor="100%" Padding="5">
                <Defaults>
                    <ext:Parameter Name="LabelWidth" Value="80" />
                    <ext:Parameter Name="Width" Value="470" />
                </Defaults>
                <Items>
                   <ext:TextField ID="hiID" runat="server" Hidden="true" />
                    
                    <ext:TextField ID="txtFooterName" runat="server" FieldLabel="Footer Name" />
                    
                    <ext:RadioGroup
                            ID="radiogallowlink"
                            runat="server"
                            FieldLabel="Allow Link"
                            ColumnsNumber="3"
                            Vertical="true">
                            <Items>
                                <ext:Radio ID="rAllowlinktrue" runat="server" BoxLabel="True" />
                                <ext:Radio ID="rAllowlinkfalse" runat="server" BoxLabel="False" Checked="true" />
                            </Items>
                            <DirectEvents>
                                <Change OnEvent="RadioAllow_Change" />
                            </DirectEvents>
                        </ext:RadioGroup>
                    <ext:TextField ID="txtFooterLink" runat="server" FieldLabel="Footer Link"  />
                    
                     <ext:RadioGroup
                            ID="radiosubmenu"
                            runat="server"
                            FieldLabel="Is Sub Menu"
                            ColumnsNumber="3"
                            Vertical="true">
                            <Items>
                                <ext:Radio ID="rSubMenuTrue" runat="server" BoxLabel="True" />
                                <ext:Radio ID="rSubMenuFalse" runat="server" BoxLabel="False" Checked="true" />
                            </Items>
                        </ext:RadioGroup>
                    <ext:ComboBox
                        ID="CbbIcon"
                        runat="server"
                        
                        Editable="false"
                        DisplayField="name"
                        ValueField="name"
                        QueryMode="Local"
                        TriggerAction="All"
                        FieldLabel="Icon"
                        EmptyText="Select an icon...">
                        <Store>
                            <ext:Store ID="Store1" runat="server">
                                <Model>
                                    <ext:Model ID="Model2" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="iconCls" />
                                            <ext:ModelField Name="name" />
                                            
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ListConfig>
                            <ItemTpl ID="ItemTpl1" runat="server">
                                <Html>
                                    <div class="icon-combo-item {iconCls}">
                            {name}
                        </div>
                                </Html>
                            </ItemTpl>
                        </ListConfig>
                        
                    </ext:ComboBox>
                    <ext:ComboBox ID="cbbFooter" runat="server" TypeAhead="true" QueryMode="Local" ForceSelection="true"
                        Editable="false" TriggerAction="All" FieldLabel="Footer Type" DisplayField="FooterTypeName"
                        ValueField="FooterTypeID">
                        <Store>
                            <ext:Store ID="stcbbPID" runat="server">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="FooterTypeID" />
                                            <ext:ModelField Name="FooterTypeName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                    </ext:ComboBox>
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