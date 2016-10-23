<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TMT.License.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=ConfigurationManager.AppSettings["APPNAME"].ToString().Trim()%></title>
    <link href="../images/iconmain.png" rel="shortcut icon" type="image/x-icon" />
    <script src="../scripts/date.js" type="text/javascript"></script>
    <script src="../scripts/jquery.js" type="text/javascript"></script>
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="rsmain" runat="server">
        </ext:ResourceManager>
        <ext:KeyMap ID="KeyMap1" runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
        </ext:KeyMap>
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" AutoScroll="true">
            <Items>

                <ext:Panel ID="pnlCenter" Region="Center" runat="server" Layout="FitLayout" AutoScroll="true">
                    <Items>
                        <ext:TabPanel ID="TabPanelMain"
                            runat="server"
                            MarginSpec="0 0 0 0"
                            TabAlign="Right"
                            Plain="true"
                            Title=""
                            TabBarHeaderPosition="1">
                            <Defaults>
                            </Defaults>
                            <Items>
                                <ext:Panel ID="pnlHome" Region="Center" runat="server" Title="Home">
                                    <Loader ID="LoaderHome" runat="server" Url="~/Home/index.html" Mode="Frame" AutoLoad="true" ShowMask="true">
                                    </Loader>

                                    <TabConfig ID="TabConfig1" runat="server" UI="Info" />
                                </ext:Panel>
                                <ext:Panel ID="pnlAbout" Region="Center" runat="server" Title="Giới Thiệu">
                                    <Loader ID="LoaderAbout" runat="server" Url="" Mode="Frame" AutoLoad="true" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <TabConfig ID="TabConfig3" runat="server" UI="Info" />
                                    <DirectEvents>
                                        <Show OnEvent="ShowAbout" />
                                    </DirectEvents>
                                </ext:Panel>
                                <ext:Panel ID="pnlArch" Region="Center" runat="server" Title="Kiến Trúc">
                                    <Loader ID="LoaderArch" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <TabConfig ID="TabConfig2" runat="server" UI="Info" />
                                    <DirectEvents>
                                        <Show OnEvent="ShowArch" />
                                    </DirectEvents>
                                </ext:Panel>
                                <ext:Panel ID="pnlCon" Region="Center" runat="server" Title="Xây Dựng">
                                    <Loader ID="LoaderCon" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <TabConfig ID="TabConfig4" runat="server" UI="Info" />
                                    <DirectEvents>
                                        <Show OnEvent="ShowCon" />
                                    </DirectEvents>
                                </ext:Panel>
                                <ext:Panel ID="pnlFur" Region="Center" runat="server" Title="Nội Thất">
                                    <Loader ID="LoaderFur" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <TabConfig ID="TabConfig5" runat="server" UI="Info" />
                                    <DirectEvents>
                                        <Show OnEvent="ShowFur" />
                                    </DirectEvents>
                                </ext:Panel>
                                <ext:Panel ID="pnlCost" Region="Center" runat="server" Title="Khải Toán">
                                    <Loader ID="LoaderCost" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <DirectEvents>
                                        <Show OnEvent="ShowCost" />
                                    </DirectEvents>
                                    <TabConfig ID="TabConfig6" runat="server" UI="Info" />
                                </ext:Panel>
                                <ext:Panel ID="pnlProject" Region="Center" runat="server" Title="Dự Án">
                                    <Loader ID="LoaderProject" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <TabConfig runat="server" UI="Info" />
                                    <DirectEvents>
                                        <Show OnEvent="ShowProject" />
                                    </DirectEvents>
                                </ext:Panel>
                                <ext:Panel ID="pnlContact" Region="Center" runat="server" Title="Liên Hệ">
                                    <Loader ID="LoaderContact" runat="server" Url="" Mode="Frame" AutoLoad="false" ShowMask="true">
                                        <LoadMask ShowMask="true" />
                                    </Loader>
                                    <TabConfig runat="server" UI="Info" />
                                    <DirectEvents>
                                        <Show OnEvent="ShowContact" />
                                    </DirectEvents>
                                </ext:Panel>
                                <ext:Panel ID="Panel1" Region="Center" runat="server" Title="Hotline: 0909.xxx.xxx">
                                    <TabConfig ID="TabConfig7" runat="server" UI="Danger" />
                                    <DirectEvents>

                                        <Activate OnEvent="ShowHotLine" />
                                    </DirectEvents>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>



                    </Items>

                </ext:Panel>
                <ext:Panel ID="Panel3" runat="server" Region="South" AutoScroll="true" Collapsible="true" Collapsed="true" Title="© 2016 - Công ty Xây Dựng Trường Thành" Height="450">
                    <Loader ID="pnlfooterFrame" runat="server" Url="~\Footer\Footer.aspx" Mode="Frame" AutoScroll="false">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>

            </Items>

        </ext:Viewport>
    </form>
    <style>
        .x-tab-inner-info {
            font-size: 14px;
        }

        #TabPanelMain_header-title {
        }

        #TabPanelMain_header-title-textEl {
            
            height: 56px;
            background-image: url(./images/Logo.png);
            background-repeat: no-repeat;
        }

        #tab-1010 {
            visibility: hidden;
        }
    </style>
</body>
</html>
