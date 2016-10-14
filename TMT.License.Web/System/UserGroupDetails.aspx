<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserGroupDetails.aspx.cs" Inherits="TMT.License.Web.TSSystem.UserGroupDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head ID="Head2" runat="server">
    <link href="../../TMT.License.Web/styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form ID="Form1" runat="server">
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
            <ext:Panel ID="pnMainUserGroup" runat="server" Layout="FitLayout" Icon="ApplicationFormEdit"
                Header="false">
                <TopBar>
                    <ext:Toolbar runat="server" Region="North" ID="ctl340">
                        <Items>
                            <ext:Button runat="server" Icon="ArrowUndo" Text="Back" ID="btBack">
                                <DirectEvents>
                                    <Click OnEvent="btBack_Click">
                                        <EventMask ShowMask="true" Msg="Loading..." MinDelay="100" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button runat="server" Icon="Add" Text="New" ID="btAdd">
                                <DirectEvents>
                                    <Click OnEvent="btAdd_Click">
                                        <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to create new record?" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator4" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button ID="btSave" runat="server" Text="Save" Icon="Disk">
                                <DirectEvents>
                                    <Click OnEvent="btSave_Click">
                                        <EventMask ShowMask="true" Msg="Saving..." MinDelay="200" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
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
                    <ext:Panel ID="panel" runat="server" Layout="BorderLayout" BaseCls="x-plain" BodyPadding="5">
                        <Items>
                            <ext:FormPanel ID="WinUserGroup" runat="server" Icon="ApplicationFormEdit" Closable="false"
                                Title="" Width="520" Height="350" BaseCls="x-plain" Resizable="false" Modal="false"
                                BodyPadding="0" Layout="AutoLayout">
                                <Items>
                                    <ext:Panel ID="pnUserGroup" Width="520" HideMode="Offsets" Border="true" Height="350"
                                        Title="UserGroup" runat="server" BodyPadding="5" Layout="BorderLayout">
                                        <Items>
                                            <ext:FieldSet ID="FieldSet5" Width="500" Border="false" Height="340" runat="server"
                                                Anchor="100%" Padding="10">
                                                <Defaults>
                                                    <ext:Parameter Name="LabelWidth" Value="100" />
                                                    <ext:Parameter Name="Width" Value="475" />
                                                </Defaults>
                                                <Items>
                                                    <ext:Hidden ID="hiID" runat="server" />
                                                    <ext:TextField ID="txtUGRPName" runat="server" FieldLabel="Group Name" />
                                                    <ext:TextField ID="txtUGRPParent" runat="server" FieldLabel="Group Parent" Hidden="true" />
                                                    <ext:Checkbox ID="chbUGRPActive" runat="server" FieldLabel="Active" Checked="true" />
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
    </ext:Viewport>
    </form>
</body>
</html>