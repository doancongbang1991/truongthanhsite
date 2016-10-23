<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer.aspx.cs" Inherits="TMT.License.Web.License.Footer" %>

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
        <ext:Viewport ID="Viewport1" runat="server"  Layout="FitLayout">
            <Items>
                <ext:Panel runat="server" Layout="FitLayout" AutoScroll="true"> 
                    <Items>
                        <ext:Panel ID="pnlFooter" Region="North" runat="server" Layout="FitLayout" MarginSpec="0 20 0 20"  >
                            <TabConfig ID="TabConfig1" runat="server" UI="Info" />
                            <Items>
                                <ext:Container ID="Container1"
                                    runat="server"
                                    AnchorHorizontal="100%"
                                    Layout="HBoxLayout"
                                    Margin="10">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%">
                                            <Items>
                                                <ext:Panel ID="pnlNhansu" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="Nhân Sự">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>
                                                
                                                <%--<ext:Panel ID="pnlDoitac" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="Liên Kết Đối Tác" Hidden="true">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>--%>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%">
                                            <Items>
                                                <ext:Panel ID="pnlWebsite" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="Liên Kết WebSite">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>
                                                </Items>
                                        </ext:FieldContainer>
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
                                                        <ext:TextField ID="TextField10" runat="server" EmptyText="Họ Và Tên" />

                                                        <ext:NumberField ID="TextField6" runat="server" EmptyText="Năm Sinh" />

                                                        <ext:TextField ID="TextField1" runat="server" EmptyText="Email" InputType="Email" />

                                                        <ext:FieldSet ID="FieldSet11" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:RadioGroup ID="RadioGroup1" runat="server">
                                                                    <Items>
                                                                        <ext:Radio ID="Radio1" runat="server" BoxLabel="Nam" Checked="true" MarginSpec="10 20 10 20" />
                                                                        <ext:Radio ID="Radio2" runat="server" BoxLabel="Nữ" MarginSpec="10 20 10 20" />
                                                                    </Items>
                                                                </ext:RadioGroup>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:TextField ID="TextField3" runat="server" EmptyText="Số Điện Thoại" InputType="Tel" />
                                                        <ext:TextArea ID="TextField2" runat="server" EmptyText="Nội Dung" />
                                                        <ext:Button ID="Button1" runat="server" Text="Email"></ext:Button>
                                                    </Items>

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
