<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TMT.License.Web.TSSystem.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head ID="Head2" runat="server">
    <link href="../../TMT.License.Web/styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
     <style>
         .icon-combo-item {
            background-repeat   : no-repeat !important;
            background-position : 3px 50% !important;
            padding-left        : 24px !important;
        }
          .textfield-top{
            text-align: center;
            xtype:'textfield';
            labelAlign: 'top';
            fieldStyle: 'text-align: center;';
            flex:1 
          }
           .textfield-left{
            text-align: left;
            xtype:'textfield';
            labelAlign: 'left';
            fieldStyle: 'text-align: left;';
            flex:1 
          }
           
    </style>
    <script>
        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value + "%");
        };

        var changesize = function () {
            var prevWidth = 10000,
                viewPort,
                thresholdWidth = 1000,
                resizeHandler = function (width, height) {
                    var portal,
                        menu,
                        breadcrumb,
                        wide,
                        narrow,
                        north;
                    viewPort = viewPort || Ext.ComponentQuery.query("#Viewport1")[0];
                    
                    if (!viewPort) {
                        return;
                    }
                    narrow = width < thresholdWidth && thresholdWidth <= prevWidth;
                    wide = width >= thresholdWidth && thresholdWidth > prevWidth;

                    if (wide || narrow) {
                        Ext.suspendLayouts();
                        portal = viewPort.down("#Panel1");
                        menu = viewPort.down("#ctl340");
                        breadcrumb = viewPort.down("#Toolbar3");
                        var txtfi = Ext.ComponentQuery.query("#txtUUsername")[0];
                        if (narrow) {
                            portal.items.each(function (column) {
                                column._columnWidth = column.columnWidth;
                                column.columnWidth = 1;

                                column.items.each(function (item) {
                                    item._minHeight = item.minHeight;
                                    item.minHeight = item.minHeight >= 500 ? item.minHeight : 500;
                                });
                            });
                            breadcrumb.show();
                            menu.hide();
                            north = viewPort.down("#Panel1");
                            north.setHeight(150);
                            CompanyX.narrow();
                            console.log(txtfi);
                            //north.getComponent(1).add(north.getComponent(0).removeAll(false));
                            
                        }
                        else if (wide) {
                            portal.items.each(function (column) {
                                column.columnWidth = column._columnWidth;

                                column.items.each(function (item) {
                                    item.minHeight = item._minHeight;
                                });
                            });

                            breadcrumb.hide();
                            menu.show();
                            north = viewPort.down("#Panel1");
                            CompanyX.wide();
                            //north.getComponent(0).add(north.getComponent(1).removeAll(false));
                            north.setHeight(100);
                            
                            console.log(txtfi);
                            txtfi.labelAlign = 'Left';
                        }

                        Ext.resumeLayouts(false);
                        viewPort.updateLayout();
                    }

                    prevWidth = width;
                };
            
            Ext.EventManager.onWindowResize(resizeHandler, window, { buffer: 100 });
            Ext.net.Bus.subscribe("App.resize", function () {
                resizeHandler(Ext.Element.getViewportWidth());
            });
        };

    </script> 
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" Theme="Default" runat="server">
    </ext:ResourceManager>
    <ext:KeyMap ID="KeyMap1" runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
        <%--<ext:KeyBinding>
            <Keys>
                <ext:Key Code="BACKSPACE" />
            </Keys>
            <Listeners>
                <Event Handler="var id = Ext.fly(e.getTarget()).getAttribute('id'),fields = Ext.getElementById(id); if(id.toLowerCase().search('input')<0) {e.stopEvent();} else if((id.toLowerCase().search('input')>0)&& (fields.readOnly)) {e.stopEvent();};" />
            </Listeners>
        </ext:KeyBinding>--%>
    </ext:KeyMap>
    <ext:Viewport ID="Viewport1" runat="server" AutoScroll="true" Layout="FitLayout">
        <Items>

            <ext:Panel ID="Panel1" runat="server" Layout="FitLayout" Icon="User" Header="false">
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
                             <ext:Button runat="server" Text="Do Fucking This"/>  
                        </Items>
                    </ext:Toolbar>
                </DockedItems>
                <TopBar>
                    <ext:Toolbar runat="server" Region="North" ID="ctl340">
                        <Items>
                            <ext:Button runat="server" Icon="TableKey" Text="Change Password" ID="btChangePass">
                                <DirectEvents>
                                    <Click OnEvent="btChangePass_Click" >
                                        <EventMask ShowMask="true" Msg="Loading..." MinDelay="100" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button runat="server" Icon="ArrowRefresh" Text="Refresh" ID="btRefresh">
                                <DirectEvents>
                                    <Click OnEvent="btRefresh_Click">
                                        <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to refresh this page?" />
                                        <EventMask ShowMask="true" Msg="Loading..." MinDelay="500" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                
                <Items>
                    <ext:Panel ID="panel" runat="server" Layout="BorderLayout" BaseCls="x-plain" BodyPadding="5">
                        <Items>
                            <ext:FormPanel ID="Window1" runat="server" Icon="User" Closable="false" Title=""
                                Width="520" Height="300" BaseCls="x-plain" Resizable="false" Modal="false" BodyPadding="0"
                                Layout="AutoLayout">
                                <Items>
                                    <ext:Panel ID="pnChangePass" Width="520" HideMode="Offsets" Border="true" Height="170"
                                        Header="true" Title="Change Password" runat="server" Layout="BorderLayout">
                                        <TopBar>
                                            <ext:Toolbar runat="server">
                                                <Items>
                                                    <ext:Button runat="server" Icon="ArrowUndo" Text="Back" ID="btCancelChange">
                                                        <DirectEvents>
                                                            <Click OnEvent="btCancelChange_Click" />
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:ToolbarSeparator />
                                                    <ext:Button runat="server" Icon="Disk" Text="Save" ID="btChange">
                                                        <DirectEvents>
                                                            <Click OnEvent="btChange_Click">
                                                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="100" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:FieldSet ID="FieldSet1" Width="500" Height="170" Border="false" runat="server"
                                                Anchor="100%" Padding="10">
                                                <Defaults>
                                                    <ext:Parameter Name="LabelWidth" Value="100" />
                                                    <ext:Parameter Name="Width" Value="475" />
                                                </Defaults>
                                                <Items>
                                                    <ext:Hidden ID="Hidden1" runat="server" />
                                                    <ext:TextField ID="txtOldPassword" runat="server" FieldLabel="Old Password" InputType="Password" />
                                                    <ext:TextField ID="txtNewPassword" runat="server" FieldLabel="New Password" InputType="Password" />
                                                    <ext:TextField ID="txtConfirmPassword" runat="server" FieldLabel="Confirm Password"
                                                        InputType="Password" />
                                                </Items>
                                            </ext:FieldSet>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="pnProfile" Width="520" HideMode="Offsets" Border="true" Height="300" 
                                        Title="Profile" runat="server" BodyPadding="5" Layout="BorderLayout">
                                        <Items>
                                            <ext:FieldSet ID="FieldSet5" Width="500" Border="false" Height="290" runat="server" AutoScroll="true"
                                                Anchor="100%" Padding="10">
                                                <Defaults>
                                                    <ext:Parameter Name="LabelWidth" Value="100" />
                                                    <ext:Parameter Name="Width" Value="475" />
                                                </Defaults>
                                                <Items>
                                                    <ext:Hidden ID="hiID" runat="server" />
                                                    <ext:TextField ID="txtUUsername" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly"
                                                        FieldLabel="Username" />
                                                    <ext:Hidden ID="hiUPassword" runat="server" />
                                                    <ext:TextField ID="txtUFullName" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Full Name" />
                                                    <ext:Hidden ID="hiPID" runat="server" />
                                                    <ext:TextField ID="txtUPosition" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Position" />
                                                    <ext:TextField ID="txtUAddress" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Address" />
                                                    <ext:TextField ID="txtUPhone" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Phone" />
                                                    <ext:TextField ID="txtUMobilePhone" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Mobile Phone" />
                                                    <ext:TextField ID="txtUEmail" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Email" />
                                                    <ext:TextField ID="txtUNotes" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="Notes" />
                                                    <ext:TextField ID="txtUGRPName" runat="server" ReadOnly="true" ReadOnlyCls="xReadOnly" 
                                                        FieldLabel="User Of Group" />
                                                    <ext:Hidden ID="hiUGRPID" runat="server" />
                                                </Items>
                                            </ext:FieldSet>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
        <Listeners>
            <Resize Fn="changesize"/>
        </Listeners>
    </ext:Viewport>
    <ext:KeyMap ID="KeyMapPanel1" runat="server" Target="#{pnChangePass}">
        <Binding>
        <ext:KeyBinding Handler="#{btChange}.fireEvent('click')">
            <Keys>
                <ext:Key Code="ENTER" />
            </Keys>
        </ext:KeyBinding>
            </Binding>
    </ext:KeyMap>
</body>
</html>