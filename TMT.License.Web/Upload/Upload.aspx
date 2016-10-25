<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="TMT.License.Web.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" SourceFormatting="True" ScriptMode="Debug" />
        <ext:FormPanel
            ID="BasicForm"
            runat="server"
            Width="500"
            Frame="true"
            Title="File Upload Form"
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
                    MarginSpec="10 0 10 30"/>
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
            </Buttons>
        </ext:FormPanel>
    </form>
</body>
</html>
