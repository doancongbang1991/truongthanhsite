<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TMT.License.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head ID="Head1" runat="server">
        <title>Login -
            <%= ConfigurationManager.AppSettings["APPNAME"].Trim() %></title>
        <link href="../TMT.License.Web/images/Login.png" rel="shortcut icon" type="image/x-icon" />
        <link href="../TMT.License.Web/styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form ID="Form1" runat="server">
            <ext:ResourceManager ID="ResourceManager" runat="server" />
            <ext:Hidden ID="hiErrorMessage" runat="server" />
            <ext:Window ID="Window1" runat="server" Closable="false" Resizable="false" Height="210"
                        Icon="Lock" Title="Login" Draggable="true" Width="390" Modal="true" BodyPadding="2"
                        >
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Center" />
                </LayoutConfig>
                <Items>
                    <ext:Panel ID="pnlTop" runat="server" Title="Top" Header="false" Region="North" Split="false"
                               Height="70" BodyPadding="2" Html="<img src='images/logoTT.png'/>" BodyCls="my-background"
                               Collapsible="false" BaseCls="x-plain" >
                    </ext:Panel>
                    <ext:FieldSet ID="FieldSet1" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                  Layout="AnchorLayout" DefaultAnchor="100%" Width="360">
                        <Items>
                            <ext:TextField ID="txtUsername" runat="server" FieldLabel="Username" AllowBlank="false"
                                           BlankText="Your username is required." Text="" EnableKeyEvents="true" >
                            </ext:TextField>
                            <ext:TextField ID="txtPassword" runat="server" InputType="Password" FieldLabel="Password"
                                           Text="">
                            </ext:TextField>
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Buttons>
                    <ext:Button ID="btnSignup" runat="server" Text="Sign Up" Icon="Add" Hidden="true">
                        <DirectEvents>
                            <Click OnEvent="btnSignup_Click">
                                <EventMask ShowMask="true" Msg="Loading..." MinDelay="100" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnLogin" runat="server" Text="Submit" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="btnLogin_Click">
                                <EventMask ShowMask="true" Msg="Checking..." MinDelay="100" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:KeyMap ID="KeyMapPanel1" runat="server" Target="#{Window1}">
                <%--<ext:KeyBinding StopEvent="true">
            <Keys>
                <ext:Key Code="ENTER" />
            </Keys>
            <Listeners>
                <Event Handler="#{btnLogin}.fireEvent('click')" />
            </Listeners>
        </ext:KeyBinding>--%>
                <Binding>
                    <ext:KeyBinding Handler="#{btnLogin}.fireEvent('click')">
                        <Keys>
                            <ext:Key Code="ENTER" />
                        </Keys>
                    </ext:KeyBinding>
                </Binding>
            </ext:KeyMap>
            <ext:KeyMap ID="KeyMap1" runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
                <%--<ext:KeyBinding StopEvent="true">
            <Keys>
                <ext:Key Code="BACKSPACE" />
            </Keys>
            <Listeners>
                <Event Handler="var t = e.getTarget();if(t.toString().indexOf('Input')<0) {e.stopEvent();}" />
            </Listeners>
        </ext:KeyBinding>--%>
            </ext:KeyMap>
            <ext:Window ID="wSingUp" runat="server" Hidden="true" Closable="false" Resizable="false"
                        Height="275" Icon="KeyStart" Title="Sign Up Window" Draggable="true" Width="380"
                        Modal="true" BodyPadding="2" Layout="FitLayout">
                <Items>
                    <ext:FieldSet ID="FieldSet2" Region="Center" Border="false" runat="server" Flex="1"
                                  Title="" Padding="7" Layout="AnchorLayout" DefaultAnchor="100%">
                        <Items>
                            <ext:Hidden ID="Hidden1" runat="server" />
                            <ext:TextField ID="txtUUsername" runat="server" FieldLabel="Username" />
                            <ext:TextField ID="txtUFullName" runat="server" FieldLabel="Full Name" />
                            <ext:TextField ID="txtNewPassword" runat="server" FieldLabel="New Password" InputType="Password" Hidden="true"/>
                            <ext:TextField ID="txtConfirmPassword" runat="server" FieldLabel="Confirm Password" Hidden="true"
                                           InputType="Password" />
                            <ext:TextField ID="txtUAddress" runat="server" FieldLabel="Address" />
                            <ext:TextField ID="txtUPhone" runat="server" FieldLabel="Phone" />
                            <ext:TextField ID="txtUMobilePhone" runat="server" FieldLabel="Mobile Phone" />
                            <ext:TextField ID="txtUEmail" runat="server" FieldLabel="Email" />
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Icon="Accept" Text="OK" ID="btChange">
                        <DirectEvents>
                            <Click OnEvent="btOk_Click">
                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="100" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Icon="Cancel" Text="Cancel" ID="btCancelChange">
                        <DirectEvents>
                            <Click OnEvent="btCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </form>
    </body>
</html>