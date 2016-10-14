<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConDetails.aspx.cs" Inherits="TMT.License.Web.License.ConDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head ID="Head2" runat="server">
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
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
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:KeyMap ID="KeyMap2" runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
        <%--<ext:KeyBinding>
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
                                        <ExtraParams>
                                            <ext:Parameter Name="grPosition_Select_Values" Value="#{grPosition}.getRowsValues({selectedOnly:true})"
                                                Encode="true" Mode="Raw" />
                                        </ExtraParams>
                                        <EventMask Msg="Loading..." MinDelay="200" ShowMask="true" />
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
                    <ext:GridPanel ID="grPosition" SelectionMemory="false" runat="server" Region="Center"
                        Layout="FitLayout" Title="">
                        <Store>
                            <ext:Store ID="stPosition" runat="server" AutoDestroy="true" ShowWarningOnFailure="true">
                                <Model>
                                    <ext:Model ID="mdPosition" runat="server" IDProperty="LicID" Name="Session">
                                        <Fields>
                                            <ext:ModelField Name="LicID" />
                                            <ext:ModelField Name="LicSerial" />
                                            <ext:ModelField Name="LicKey" />
                                            <ext:ModelField Name="LicDes" />
                                            <ext:ModelField Name="LicStatus" />
                                            <ext:ModelField Name="LRegDate" Type="Date" />
                                            <ext:ModelField Name="LAppDate" Type="Date" />
                                            <ext:ModelField Name="PDes"/>
                                            <ext:ModelField Name="LicProduct"/>
                                            <ext:ModelField Name="LicDomain"/>
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
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" Width="30" runat="server" />
                                <ext:Column ID="colLicID" runat="server" DataIndex="LicID" Text="ID" Width="200" Hidden="true"/>
                                <ext:Column ID="colLicSerial" runat="server" DataIndex="LicSerial" Text="Serial" Flex="10" />
                                <ext:Column ID="colLicKey" runat="server" DataIndex="LicKey" Text="License Key" Flex="15" />
                                <ext:Column ID="colLicDes" runat="server" DataIndex="LicDes" Text="License Description" Flex="10"/>
                                <ext:Column ID="colLicStatus" runat="server" DataIndex="LicStatus" Text="Status" Flex="4" />
                                <ext:Column ID="colProduct" runat="server" DataIndex="LicProduct" Text="Product" Width="200" Hidden="true" />
                                <ext:Column ID="colPName" runat="server" DataIndex="PDes" Text="Product Name" Width="200" />
                                <ext:Column ID="colDomain" runat="server" DataIndex="LicDomain" Text="Domain" Flex="6"/>
                                <ext:DateColumn ID="colLRegDate" runat="server" DataIndex="LRegDate" Text="Register Date" Flex="6" />
                                <ext:DateColumn ID="colLAppDate" runat="server" DataIndex="LAppDate" Text="Approved Date" Flex="6" />
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
                                    <ext:TriggerField ID="txtKeyword" runat="server" Width="350" FieldLabel="Search"
                                        LabelWidth="50">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="searchFieldTriggerClick" />
                                            <SpecialKey Fn="onSpecialKey" />
                                        </Listeners>
                                    </ext:TriggerField>
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
        Height="285" Icon="ApplicationFormEdit" Title="Register Serial" Draggable="true" Width="510"
        Modal="true" BodyPadding="5" Layout="FitLayout">
        <TopBar>
            <ext:Toolbar ID="Toolbar2" runat="server">
                <Items>
                    <ext:Button runat="server" Icon="AwardStarBronze1" Text="New" ID="btNew">
                        <DirectEvents>
                            <Click OnEvent="btNew_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Items>
            <ext:FieldSet ID="FieldSet5" Border="false" Width="500" Height="275" runat="server"
                Anchor="100%" Padding="5">
                <Defaults>
                    <ext:Parameter Name="LabelWidth" Value="80" />
                    <ext:Parameter Name="Width" Value="470" />
                </Defaults>
                <Items>
                    <ext:TextField ID="hiID" runat="server" Hidden="true" />
                    <ext:TextField ID="txtSerial" runat="server" FieldLabel="Serial" />
                    <ext:TextField ID="txtLicKey" runat="server" FieldLabel="License Key" />
                    <ext:TextField ID="txtDomain" runat="server" FieldLabel="Domain" />
                    <ext:TextField ID="txtDes" runat="server" FieldLabel="Description" />
                    <ext:ComboBox ID="cbbProduct" runat="server" TypeAhead="true" QueryMode="Local" ForceSelection="true"
                            Editable="false" TriggerAction="All" FieldLabel="Product" DisplayField="PDes"
                            ValueField="PID">
                        <Store>
                            <ext:Store ID="stcbbPID" runat="server">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="PID" />
                                            <ext:ModelField Name="PDes" />
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
            <ext:Button runat="server" Icon="Accept" Text="OK" ID="btOK">
                <DirectEvents>
                    <Click OnEvent="btOK_Click">
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
