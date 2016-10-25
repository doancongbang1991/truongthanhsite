<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer.aspx.cs" Inherits="TMT.License.Web.License.Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
    <style>
        .lbinfo {
            font-size: 15px;
        }
    </style>
</head>
<body>

    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" AutoScroll="False" Layout="FitLayout">
            <Items>
                <ext:Panel runat="server" Layout="AutoLayout">
                    <Items>
                        <ext:Panel Hidden="true"
                            ID="pnlAlignMiddle"
                            runat="server"
                            Layout="VBoxLayout"
                            BodyPadding="5">
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
                        </ext:Panel>


                        <ext:Panel ID="pnlFooter" Region="North" runat="server" Layout="AnchorLayout" MarginSpec="20 0 20 0">
                            <TabConfig ID="TabConfig1" runat="server" UI="Info" />
                            <Items>
                                <ext:Container ID="Container1"
                                    runat="server"
                                    AnchorHorizontal="100%"
                                    Layout="HBoxLayout">
                                    <Items>
                                        <ext:FieldContainer ID="FieldSet2" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%">
                                            <Items>
                                                <ext:Panel ID="pnlInfo" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="Thông Tin Liên Hệ">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldSet1" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%">

                                            <Items>

                                                <ext:Panel ID="pnlemail" runat="server" Layout="AnchorLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="Gửi mail cho chúng tôi">
                                                    <Items>
                                                        <ext:TextField ID="txtMessName" runat="server" EmptyText="Họ Và Tên" />
                                                        <ext:NumberField ID="numMessYear" runat="server" EmptyText="Năm Sinh" />
                                                        <ext:TextField ID="txtMessMail" runat="server" EmptyText="Email" InputType="Email" />
                                                        <ext:FieldSet ID="FieldSet11" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:RadioGroup ID="rMessGen" runat="server">
                                                                    <Items>
                                                                        <ext:Radio ID="rMessGenM" runat="server" BoxLabel="Nam" Checked="true" MarginSpec="10 20 10 20" />
                                                                        <ext:Radio ID="rMessGenF" runat="server" BoxLabel="Nữ" MarginSpec="10 20 10 20" />
                                                                    </Items>
                                                                </ext:RadioGroup>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:TextField ID="txtMessPhone" runat="server" EmptyText="Số Điện Thoại" InputType="Tel" />
                                                        <ext:TextArea ID="txtMessBody" runat="server" EmptyText="Nội Dung" />
                                                        <ext:Button ID="Button1" runat="server" Text="Email">
                                                            <DirectEvents>
                                                                <Click OnEvent="SendMail" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>

                                                </ext:Panel>
                                            </Items>
                                        </ext:FieldContainer>

                                        <ext:FieldContainer ID="FieldSet3" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%" AutoScroll="false">
                                            <Items>

                                                <ext:Panel ID="pnlmap" runat="server" Height="350" Region="Center" Title="Bản Đồ" AutoScroll="false">
                                                    <Loader ID="Loader1" runat="server" Url="" Mode="Frame" AutoLoad="false">
                                                        <LoadMask ShowMask="true" />
                                                    </Loader>

                                                </ext:Panel>
                                            </Items>
                                        </ext:FieldContainer>

                                    </Items>
                                </ext:Container>


                            </Items>
                        </ext:Panel>

                    </Items>
                </ext:Panel>




            </Items>
        </ext:Viewport>


    </form>
</body>
</html>
