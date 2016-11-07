<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostManager.aspx.cs" Inherits="TMT.License.Web.TSSystem.CostManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <link href="../../TMT.License.Web/styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
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
                <ext:Hidden ID="hiErrorMessage" runat="server" />
                <ext:Panel Layout="VBoxLayout" runat="server" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" Layout="HBoxLayout" Icon="User" Header="false" AutoScroll="true">
                            <TopBar>
                                <ext:Toolbar runat="server" Region="North" ID="ctl340">
                                    <Items>
                                        <ext:Button ID="btSave" runat="server" Text="Save" Icon="Disk">
                                            <DirectEvents>
                                                <Click OnEvent="btSave_Click">
                                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="200" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="ToolbarSeparatorbtResetPass" runat="server">
                                        </ext:ToolbarSeparator>

                                        <ext:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                                        </ext:ToolbarSeparator>
                                        <ext:Button runat="server" Icon="ArrowRefresh" Text="Refresh" ID="btRefresh">
                                            <DirectEvents>
                                                <Click OnEvent="btRefresh_Click">
                                                    <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to refresh this page?" />
                                                    <EventMask ShowMask="true" Msg="Loading..." MinDelay="200" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Panel ID="panel" runat="server" Layout="AutoLayout" BaseCls="x-plain" BodyPadding="5" AutoScroll="true" Region="East">
                                    <Items>
                                        <ext:Panel ID="pnlLocation" Width="580" HideMode="Offsets" Border="true" Height="230"
                                            Title="Location" runat="server" BodyPadding="5" Layout="BorderLayout">
                                            <Items>
                                                <ext:FieldSet ID="fieldsetLocation" Width="500" Border="false" Height="230" runat="server"
                                                    Anchor="100%" Padding="10">
                                                    <Defaults>
                                                        <ext:Parameter Name="LabelWidth" Value="180" />
                                                        <ext:Parameter Name="Width" Value="475" />
                                                    </Defaults>
                                                    <Items>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="pnlStatus" Width="580" HideMode="Offsets" Border="true" Height="190"
                                            Title="Status" runat="server" BodyPadding="5" Layout="BorderLayout">
                                            <Items>
                                                <ext:FieldSet ID="fieldsetStatus" Width="500" Border="false" Height="190" runat="server"
                                                    Anchor="100%" Padding="10">
                                                    <Defaults>
                                                        <ext:Parameter Name="LabelWidth" Value="180" />
                                                        <ext:Parameter Name="Width" Value="475" />
                                                    </Defaults>
                                                    <Items>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="panel2" runat="server" Layout="AutoLayout" BaseCls="x-plain" BodyPadding="5" AutoScroll="true" Region="West">
                                    <Items>
                                        <ext:Panel ID="pnlType" Width="580" HideMode="Offsets" Border="true" Height="230"
                                            Title="Type" runat="server" BodyPadding="5" Layout="BorderLayout">
                                            <Items>
                                                <ext:FieldSet ID="fieldsetType" Width="500" Border="false" Height="230" runat="server"
                                                    Anchor="100%" Padding="10">
                                                    <Defaults>
                                                        <ext:Parameter Name="LabelWidth" Value="180" />
                                                        <ext:Parameter Name="Width" Value="475" />
                                                    </Defaults>
                                                    <Items>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="pnlBasePrice" Width="580" HideMode="Offsets" Border="true" Height="190"
                                            Title="Base Price" runat="server" BodyPadding="5" Layout="BorderLayout">
                                            <Items>
                                                <ext:FieldSet ID="fieldsetBasePrice" Width="500" Border="false" Height="190" runat="server"
                                                    Anchor="100%" Padding="10">
                                                    <Defaults>
                                                        <ext:Parameter Name="LabelWidth" Value="180" />
                                                        <ext:Parameter Name="Width" Value="475" />
                                                    </Defaults>
                                                    <Items>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>

                            </Items>
                        </ext:Panel>
                        
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>

        <ext:KeyMap ID="KeyMapPanel1" runat="server" Target="#{wReset}">
            <Binding>
                <ext:KeyBinding Handler="#{btChange}.fireEvent('click')">
                    <Keys>
                        <ext:Key Code="ENTER" />
                    </Keys>
                </ext:KeyBinding>
            </Binding>
        </ext:KeyMap>
    </form>
</body>
</html>
