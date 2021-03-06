﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserGroupManager.aspx.cs" Inherits="TMT.License.Web.TSSystem.UserGroupManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head ID="Head2" runat="server">
    <link href="../../TMT.License.Web/styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
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
</head>
<body>
    <form ID="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:KeyMap ID="KeyMap1" runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
       <%-- <ext:KeyBinding>
            <Keys>
                <ext:Key Code="BACKSPACE" />
            </Keys>
            <Listeners>
                <Event Handler="var id = Ext.fly(e.getTarget()).getAttribute('id'),fields = Ext.getElementById(id); if(id.toLowerCase().search('input')<0) {e.stopEvent();} else if((id.toLowerCase().search('input')>0)&& (fields.readOnly)) {e.stopEvent();};" />
            </Listeners>
        </ext:KeyBinding>--%>
    </ext:KeyMap>
    <ext:Viewport ID="Viewport1" runat="server" AutoScroll="False" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Split="true" Layout="FitLayout" Region="Center"
                Header="true">
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
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button runat="server" Icon="ApplicationFormEdit" Text="Edit" ID="btEdit">
                                <DirectEvents>
                                    <Click OnEvent="btEdit_Click">
                                        <EventMask ShowMask="true" Msg="Loading..." MinDelay="200" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button runat="server" Icon="Delete" Text="Delete" ID="btDel">
                                <DirectEvents>
                                    <Click OnEvent="btDel_Click">
                                        <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to delete this records?" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                            </ext:ToolbarSeparator>
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
                    <ext:GridPanel ID="grUserGroup" SelectionMemory="false" runat="server" Region="Center"
                        Layout="FitLayout" Title="">
                        <Store>
                            <ext:Store ID="stUserGroup" runat="server" AutoDestroy="true" ShowWarningOnFailure="true">
                                <Model>
                                    <ext:Model ID="mdUserGroup" runat="server" IDProperty="UGRPID" Name="Session">
                                        <Fields>
                                            <ext:ModelField Name="UGRPName" />
                                            <ext:ModelField Name="UGRPParent" />
                                            <ext:ModelField Name="UGRPActive" />
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
                                <ext:Column ID="colUGRPName" runat="server" DataIndex="UGRPName" Text="Group Name"
                                    Width="100" />
                                <ext:Column ID="colUGRPParent" runat="server" DataIndex="UGRPParent" Text="Group Parent"
                                    Width="100" Hidden="true" />
                                <ext:CheckColumn ID="chbUGRPActive" runat="server" DataIndex="UGRPActive" Text="Active"
                                    Width="60" />
                            </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView ID="grvUserGroup" runat="server" StripeRows="true" MarkDirty="false" />
                        </View>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModelUserGroup" runat="server" Mode="Multi">
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
                                <EventMask ShowMask="true" Msg="Loading..." MinDelay="200" />
                            </ItemDblClick>
                        </DirectEvents>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
