<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoDetails.aspx.cs" Inherits="TMT.License.Web.TSSystem.UserInfoDetails" %>

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
                    <ext:Hidden ID="hiErrorMessage" runat="server" />
                    <ext:Panel ID="Panel1" runat="server" Layout="FitLayout" Icon="User" Header="false">
                        <TopBar>
                            <ext:Toolbar runat="server" Region="North" ID="ctl340">
                                <Items>
                                    <ext:Button runat="server" Icon="ArrowUndo" Text="Back" ID="btBack">
                                        <DirectEvents>
                                            <Click OnEvent="btBack_Click" >
                                                <EventMask ShowMask="true" Msg="Loading..." MinDelay="100" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                    </ext:ToolbarSeparator>
                                    <ext:Button runat="server" Icon="AwardStarBronze1" Text="New" ID="btAdd">
                                        <DirectEvents>
                                            <Click OnEvent="btAdd_Click">
                                                <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to create new user?" />
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
                                    <ext:ToolbarSeparator ID="ToolbarSeparatorbtResetPass" runat="server">
                                    </ext:ToolbarSeparator>
                                    <ext:Button runat="server" Icon="TableKey" Text="Reset Password" ID="btResetPass">
                                        <DirectEvents>
                                            <Click OnEvent="btResetPass_Click">
                                                <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to reset user's password?" />
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
                                    <ext:FormPanel ID="Window1" runat="server" Icon="User" Closable="false" Title=""
                                                   Width="520" Height="350" BaseCls="x-plain" Resizable="false" Modal="false" BodyPadding="0"
                                                   Layout="AutoLayout">
                                        <Items>
                                            <ext:Panel ID="pnProfile" Width="520" HideMode="Offsets" Border="true" Height="350"
                                                       Title="Profile" runat="server" BodyPadding="5" Layout="BorderLayout">
                                                <Items>
                                                    <ext:FieldSet ID="FieldSet5" Width="500" Border="false" Height="340" runat="server"
                                                                  Anchor="100%" Padding="10">
                                                        <Defaults>
                                                            <ext:Parameter Name="LabelWidth" Value="100" />
                                                            <ext:Parameter Name="Width" Value="475" />
                                                        </Defaults>
                                                        <Items>
                                                            <ext:Hidden ID="hiID" runat="server" />
                                                            <ext:TextField ID="txtUUsername" runat="server" FieldLabel="Username" />
                                                            <ext:TextField ID="txtUFullName" runat="server" FieldLabel="Full Name" />
                                                            <ext:ComboBox ID="cbbPID" runat="server" TypeAhead="true" QueryMode="Local" ForceSelection="true"
                                                                          Editable="false" TriggerAction="All" FieldLabel="Position" DisplayField="PName"
                                                                          ValueField="PID">
                                                                <Store>
                                                                    <ext:Store ID="stcbbPID" runat="server">
                                                                        <Model>
                                                                            <ext:Model ID="Model1" runat="server">
                                                                                <Fields>
                                                                                    <ext:ModelField Name="PID" />
                                                                                    <ext:ModelField Name="PName" />
                                                                                </Fields>
                                                                            </ext:Model>
                                                                        </Model>
                                                                    </ext:Store>
                                                                </Store>
                                                            </ext:ComboBox>
                                                            <ext:TextField ID="txtUAddress" runat="server" FieldLabel="Address" />
                                                            <ext:TextField ID="txtUPhone" runat="server" FieldLabel="Phone" />
                                                            <ext:TextField ID="txtUMobilePhone" runat="server" FieldLabel="Mobile Phone" />
                                                            <ext:TextField ID="txtUEmail" runat="server" FieldLabel="Email" />
                                                            <ext:TextField ID="txtUNotes" runat="server" FieldLabel="Notes" />
                                                            <ext:ComboBox ID="cbbUGRPID" runat="server" TypeAhead="true" QueryMode="Local" ForceSelection="true"
                                                                          Editable="false" TriggerAction="All" FieldLabel="Group" DisplayField="UGRPName"
                                                                          ValueField="UGRPID">
                                                                <Store>
                                                                    <ext:Store ID="stcbbUGRPID" runat="server">
                                                                        <Model>
                                                                            <ext:Model ID="ModelUGRPID" runat="server">
                                                                                <Fields>
                                                                                    <ext:ModelField Name="UGRPID" />
                                                                                    <ext:ModelField Name="UGRPName" />
                                                                                </Fields>
                                                                            </ext:Model>
                                                                        </Model>
                                                                    </ext:Store>
                                                                </Store>
                                                            </ext:ComboBox>
                                                            <ext:Checkbox ID="chbuActive" runat="server" FieldLabel="Active" Checked="true" />
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
            <ext:Window ID="wReset" runat="server" Hidden="true" Closable="false" Resizable="false"
                        Height="150" Icon="KeyStart" Title="Reset Pasword" Draggable="true" Width="380"
                        Modal="true" BodyPadding="2" Layout="FitLayout">
                <Items>
                    <ext:FieldSet ID="FieldSet1" Region="Center" Border="false" runat="server" Flex="1"
                                  Title="" Padding="7" Layout="AnchorLayout" DefaultAnchor="100%">
                        <Items>
                            <ext:Hidden ID="Hidden1" runat="server" />
                            <ext:TextField ID="txtNewPassword" runat="server" FieldLabel="New Password" InputType="Password" />
                            <ext:TextField ID="txtConfirmPassword" runat="server" FieldLabel="Confirm Password"
                                           InputType="Password" />
                        </Items>
                    </ext:FieldSet>
                </Items>
                <Buttons>
                    <ext:Button runat="server" Icon="Accept" Text="OK" ID="btChange">
                        <DirectEvents>
                            <Click OnEvent="btChange_Click">
                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="100" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" Icon="Cancel" Text="Cancel" ID="btCancelChange">
                        <DirectEvents>
                            <Click OnEvent="btCancelChange_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
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