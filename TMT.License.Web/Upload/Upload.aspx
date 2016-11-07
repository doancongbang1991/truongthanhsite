<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="TMT.License.Web.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/organizer.css" rel="stylesheet" />
    <link rel="stylesheet" href="../Home/dist/css/lightbox.css" />
    <script type="text/javascript" src="../Home/bootstrap/js/jquery.min.js"></script>
    <link href="../Home/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../Home/bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <script src="../Home/bootstrap/js/bootstrap.js"></script>
</head>

<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" SourceFormatting="True" ScriptMode="Debug" />
        <ext:Window ID="winDetails" runat="server" Hidden="true" Closable="false" Resizable="false"
            Height="325" Icon="ApplicationFormEdit" Draggable="true" Width="510" Title="File Upload Form"
            Modal="true" BodyPadding="5" Layout="FitLayout">
            <Items>
                <ext:FormPanel
                    ID="BasicForm"
                    runat="server"
                    Width="500"
                    Frame="true"
                    PaddingSummary="10px 10px 0 10px"
                    LabelWidth="50">

                    <Defaults>
                        <ext:Parameter Name="anchor" Value="95%" Mode="Value" />
                        <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                        <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
                    </Defaults>
                    <Items>
                        <ext:TextField ID="txtPhotoName" runat="server" FieldLabel="Name" MarginSpec="10 0 10 30" />
                        <ext:FileUploadField
                            ID="FileUploadField1"
                            runat="server"
                            EmptyText="Select an image"
                            FieldLabel="Photo"
                            ButtonText=""
                            Icon="ImageAdd"
                            MarginSpec="10 0 10 30" />
                    </Items>
                    <Listeners>
                        <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
                    </Listeners>
                    <Buttons>
                        <ext:Button ID="btnSave" runat="server" Text="Save" Disabled="true">
                            <DirectEvents>
                                <Click
                                    OnEvent="UploadClick"
                                    Failure="Ext.Msg.show({
                                title   : 'Error',
                                msg     : 'Error during uploading',
                                minWidth: 200,
                                modal   : true,
                                icon    : Ext.Msg.ERROR,
                                buttons : Ext.Msg.OK
                            });">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnReset" runat="server" Text="Reset">
                            <Listeners>
                                <Click Handler="#{BasicForm}.getForm().reset();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnImageCancel" runat="server" Text="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnImageCancel_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
        <ext:Panel runat="server"
            Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1"
                    runat="server"
                    Width="700"
                    Height="600"
                    Layout="BorderLayout">
                    <Items>
                        <ext:TreePanel
                            ID="TreePanel1"
                            runat="server"
                            Region="West"
                            Width="200"
                            Padding="5"
                            Title="My Albums"
                            RootVisible="false"
                            DisplayField="name" Collapsible="true">

                            <View>
                                <ext:TreeView ID="TreeView1" runat="server">

                                    <DirectEvents>
                                        <%--  <CellClick OnEvent="TreeViewClick" />--%>
                                        <ItemClick OnEvent="TreeViewClick">
                                            <ExtraParams>
                                                <ext:Parameter Name="grPosition_Select_Values" Value="#{TreeView1}.getRowsValues({selectedOnly:true})"
                                                    Encode="true" Mode="Raw" />
                                            </ExtraParams>
                                        </ItemClick>
                                    </DirectEvents>


                                </ext:TreeView>
                            </View>


                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="Button1" runat="server" Text="New Album" Icon="Add">
                                            <DirectEvents>
                                                <Click OnEvent="NewAlbumClick" />
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>

                            <Store>
                                <ext:TreeStore ID="TreeStore1" runat="server">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="name" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                    <Root>
                                    </Root>
                                </ext:TreeStore>
                            </Store>

                            <Editor>
                                <ext:TextField ID="TextField1"
                                    runat="server"
                                    AllowBlank="false"
                                    BlankText="A name is required"
                                    SelectOnFocus="true" />
                            </Editor>


                        </ext:TreePanel>

                        <ext:Panel ID="Panel2"
                            runat="server"
                            Region="Center"
                            Title="My Images"
                            Layout="FitLayout"
                            PaddingSpec="5 5 5 0">
                            <Items>
                                <ext:DataView
                                    ID="ImageView"
                                    runat="server"
                                    TrackOver="true"
                                    Cls="x-image-view"
                                    AutoScroll="true"
                                    ItemSelector="div.thumb-wrap"
                                    MultiSelect="true"
                                    SingleSelect="false">
                                    <Tpl ID="Tpl1" runat="server">
                                        <Html>
                                            <tpl for=".">
                                        <div class="thumb-wrap text-center center-block">
                                            <div class="thumb text-center center-block">
                                                 <tpl if="!Ext.isIE6" style="border-radius: 18px 18px 18px 18px;-moz-border-radius: 18px 18px 18px 18px;-webkit-border-radius: 18px 18px 18px 18px;border: 2px groove #000000;">
                                                    
                                                     <a class="center-block text-center" href="{url}" data-lightbox="image-1" data-title="<span><b>Tên File:</b> {name}</span>
                                            <br>
                                                <span><b>Đường dẫn:</b> {index}</span>
                                            <span><b>Đường dẫn:</b> {link}</span>
                                            "><img class="img-responsive center-block" style="max-width: 60%;" src="{url}" /></a>
                                                       
                                                </tpl>
                                                <tpl if="Ext.isIE6">
                                                    <div style="width:76px;height:76px;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\"{url}\")"></div>
                                                </tpl>
                                            </div>
                                            <span><b>Tên File:</b> {name}</span>
                                            <br></br>
                                            <span><b>Đường dẫn:</b> {link}</span>
                                            <br></br>
                                        </div>
                                    </tpl>
                                        </Html>
                                    </Tpl>
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model2" runat="server" IDProperty="name">
                                                    <Fields>
                                                        <ext:ModelField Name="index" />
                                                        <ext:ModelField Name="name" />
                                                        <ext:ModelField Name="url" />
                                                        <ext:ModelField Name="link" />
                                                        <ext:ModelField Name="leaf" DefaultValue="true" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                </ext:DataView>
                            </Items>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <ext:Button ID="btnAddImage" runat="server" Icon="Add" Text="New Image">
                                            <DirectEvents>
                                                <Click OnEvent="btAddImage_Click">
                                                    <EventMask ShowMask="true" Msg="Loading..." MinDelay="100" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnDelImage" runat="server" Icon="Delete" Text="Delete Image">
                                            <DirectEvents>
                                                <Click OnEvent="btnDel_Click">
                                                    <ExtraParams>
                                                        <ext:Parameter Name="ImageView_Select_Values" Value="#{ImageView}.getRowsValues({selectedOnly:true})"
                                                            Encode="true" Mode="Raw" />
                                                    </ExtraParams>
                                                    <Confirmation ConfirmRequest="true" Title="Question" Message="Do you really want to delete this records?" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Panel>
        <ext:Window ID="WindowNewAlbum" runat="server" Hidden="true" Closable="false" Resizable="false"
            Height="325" Icon="ApplicationFormEdit" Title="Add New Album" Draggable="true" Width="510"
            Modal="true" BodyPadding="5" Layout="FitLayout">

            <Items>
                <ext:FieldSet ID="FieldSet5" Border="false" Width="500" Height="325" runat="server"
                    Anchor="100%" Padding="5">
                    <Defaults>
                        <ext:Parameter Name="LabelWidth" Value="80" />
                        <ext:Parameter Name="Width" Value="470" />
                    </Defaults>
                    <Items>


                        <ext:TextField ID="txtAlbumName" runat="server" FieldLabel="Footer Name" />


                    </Items>
                </ext:FieldSet>
            </Items>
            <Buttons>
                <ext:Button runat="server" Icon="Accept" Text="Add" ID="btApprove">
                    <DirectEvents>
                        <Click OnEvent="btnAlbumApprove_Click">
                            <EventMask ShowMask="true" Msg="Saving..." MinDelay="200" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Icon="Cancel" Text="Cancel" ID="btCancel">
                    <DirectEvents>
                        <Click OnEvent="btnAlbumCancel_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
    <script src="../Home/dist/js/lightbox.js"></script>
</body>
</html>
