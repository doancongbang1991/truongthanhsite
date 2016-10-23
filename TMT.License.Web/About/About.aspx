<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TMT.License.Web.License.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
    <style>
        .x-label-value {
            font-size: 14px;
        }
        
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel Layout="AutoLayout" runat="server" AutoScroll="true">
                    <Items>
                    <ext:Panel
                    ID="pnlAlignMiddle"
                    runat="server"
                    Layout="VBoxLayout"
                    BodyPadding="5" >
                    <Defaults>
                        <ext:Parameter Name="margin" Value="0 0 5 0" Mode="Value" />
                    </Defaults>
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Center" />
                    </LayoutConfig>
                    <Items>
                        <ext:Label runat="server" ID="lbsitedetail" />
                        <ext:Label runat="server" ID="lbsitedesp" MarginSpec="25 0 5 0" />
                    </Items>
                </ext:Panel >
                        <ext:Panel ID="pnlAbout" runat="server" AutoScroll="true">
                            <Items></Items>
                        </ext:Panel>

               

                    </Items>
                </ext:Panel>

              


            </Items>
        </ext:Viewport>


    </form>
</body>
</html>
