<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArchPhongThuy.aspx.cs" Inherits="TMT.License.Web.License.ArchPhongThuy" %>

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
                            BodyPadding="5">
                            <Defaults>
                                <ext:Parameter Name="margin" Value="0 0 5 0" Mode="Value" />
                            </Defaults>
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:DatePicker ID="dtpicker" runat="server" SelectedDate="01.04.2010" />
                                <ext:FieldContainer runat="server">
                                    <Items>
                                        <ext:RadioGroup
                                            ID="radiogallowlink"
                                            runat="server"
                                            FieldLabel="Giới Tính"
                                            MarginSpec="0 20 0 20"
                                            Vertical="true">
                                            <Items>
                                                <ext:Radio ID="rMale" runat="server" BoxLabel="Nam" Checked="true" MarginSpec="0 20 0 20" />
                                                <ext:Radio ID="rFemale" runat="server" BoxLabel="Nữ" MarginSpec="0 20 0 20" />
                                            </Items>

                                        </ext:RadioGroup>
                                    </Items>
                                </ext:FieldContainer>

                                <ext:Button ID="btnPhongThuy" runat="server" Text="Xem Phong Thủy">
                                    <DirectEvents>
                                        <Click OnEvent="btnPhongThuy_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:FieldContainer runat="server" ID="fieldresult" Layout="HBoxLayout">
                                    <Items>
                                        <ext:FieldContainer runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer runat="server" Layout="HBoxLayout"  MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label runat="server" Text="Bắc" />
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer4" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label1" runat="server" Text="Đông Bắc" />
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer11" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer12" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpBac" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer13" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpDongBac" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer16" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer17" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label8" runat="server" Text="Đông" />

                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer18" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label9" runat="server" Text="Đông Nam" />

                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer14" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpDong" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer15" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpDongNam" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer7" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label4" runat="server" Text="Nam" />

                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer8" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label5" runat="server" Text="Tây Nam" />

                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer5" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer6" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpNam" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer19" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpTayNam" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer9" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label6" runat="server" Text="Tây" />

                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer10" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Label ID="Label7" runat="server" Text="Tây Bắc" />

                                                    </Items>
                                                </ext:FieldContainer>


                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer20" runat="server" Layout="VBoxLayout">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer21" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpTay" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer22" runat="server" Layout="HBoxLayout" MarginSpec="10 20 10 10">
                                                    <Items>
                                                        <ext:Hyperlink ID="hpTayBac" runat="server" ImageUrl="http://www.xemngay.com/phongthuy.aspx/05041977/nu/dongnam/2534.png"/>
                                                    </Items>
                                                </ext:FieldContainer>


                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldContainer>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>




            </Items>
        </ext:Viewport>


    </form>
</body>
</html>
