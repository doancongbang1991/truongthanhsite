<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="TMT.License.Web.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=ConfigurationManager.AppSettings["APPNAME"].ToString().Trim()%></title>
    <link href="../TMT.License.Web/images/iconmain.png" rel="shortcut icon" type="image/x-icon" />
    <script src="../TMT.License.Web/scripts/date.js" type="text/javascript"></script>
    <link href="../TMT.License.Web/styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form ID="Form1" runat="server">
    <ext:ResourceManager ID="rsmain" runat="server">
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
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="pnlTop" runat="server" Title="Top" Header="false" Region="North" Split="false"
                Height="70" BodyPadding="2" Html="<a href='MainPage.aspx'><img src='images/Logo.png' /></a>"
                BodyCls="my-background" Collapsible="false" BaseCls="x-plain" >
            </ext:Panel>
            <ext:Panel ID="pnlMenu" runat="server" Title="Functional" Region="West" Layout="AccordionLayout"
                Width="225" MinWidth="225" MaxWidth="400" Split="true" Collapsible="true">
                <Items>
                    
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlCenter" Region="Center" runat="server" Title="Home">
                <Loader ID="Loader1" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                    <LoadMask ShowMask="true" />
                </Loader>
            </ext:Panel>
            <ext:Panel ID="pnlBottom" runat="server" Title="" Region="South" Split="false" Collapsible="false"
                Height="40" BodyPadding="0" Html="">
                <BottomBar>
                    <ext:StatusBar ID="StatusBar4" CtCls="word-status" runat="server" DefaultText="Truong Thanh Company">
                        <Items>
                            <ext:ToolbarTextItem ID="txtDateTime" runat="server" AutoDataBind="true" Text="" />
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" />
                            <ext:ToolbarTextItem ID="txtUserStatus" runat="server" AutoDataBind="true" Text="" />
                            <ext:ToolbarSeparator ID="ToolbarSeparator3" runat="server" />
                            <ext:Button ID="ThemeLabel" runat="server" Icon="Theme" Text="Themes">
                                <DirectEvents>
                                    <Click OnEvent="Theme_Click">
                                        <Confirmation ConfirmRequest="true" Title="Thông Báo" Message="All data in site will be lost and website will return homepage to apply new themes. Do you want to continue?" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server" />
                            <ext:Button ID="btLogout" runat="server" Icon="LorryGo" Text="Log Out">
                                <DirectEvents>
                                    <Click OnEvent="btLogout_Click">
                                        <Confirmation ConfirmRequest="true" Title="Question" Message="Do you want to logout website?" />
                                        <EventMask MinDelay="50" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:StatusBar>
                </BottomBar>
            </ext:Panel>
        </Items>
       
    </ext:Viewport>
    </form>
</body>
</html>
