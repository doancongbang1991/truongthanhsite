<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer.aspx.cs" Inherits="TMT.License.Web.License.Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
    <style>
        .lbinfo {
            font-size: 15px;
        }

        .x-panel-header-default {
            background-color: transparent;
        }

        .x-panel-body-default {
            background-color: transparent;
        }
        .x-body {
            background-color: transparent;
        }
        .x-panel-header-title-default {
            color:yellow;
            font-family:Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            font-weight: bold;
            text-decoration: underline;
            -moz-text-decoration-color: white; /* Code for Firefox */
            text-decoration-color: white;
        }
        #pnlNhansu .x-label-value{
            color:#fff;
            
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel runat="server" Layout="FitLayout" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="pnlFooter" Region="North" runat="server" Layout="FitLayout" MarginSpec="0 0 0 0">
                            <TabConfig ID="TabConfig1" runat="server" UI="Info" />
                            <Items>
                                <ext:Container ID="Container1"
                                    runat="server"
                                    AnchorHorizontal="100%"
                                    Layout="HBoxLayout"
                                    Margin="10">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1" Region="Center" runat="server" Flex="10" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%">
                                            <Items>
                                                <ext:Panel ID="pnlNhansu" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="NHÂN SỰ">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>

                                                <%--<ext:Panel ID="pnlDoitac" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="Liên Kết Đối Tác" Hidden="true">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>--%>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2" Region="Center" runat="server" Flex="13" Title="" Padding="7"
                                            Layout="AnchorLayout" DefaultAnchor="100%">
                                            <Items>
                                                <ext:Panel ID="pnlWebsite" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="LIÊN KẾT WEBSITE">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>
                                                 <ext:Panel ID="pnlInfo" runat="server" Layout="VBoxLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="LIÊN KẾT ĐỐI TÁC">
                                                    <Items>
                                                    </Items>

                                                </ext:Panel>
                                            </Items>
                                        </ext:FieldContainer>
                                        
                                        <ext:FieldContainer ID="FieldSet1" Region="Center" runat="server" Flex="20" Title="" 
                                            Layout="AnchorLayout" DefaultAnchor="100%">

                                            <Items>

                                                <ext:Panel ID="pnlemail" runat="server" Layout="AnchorLayout" Region="Center" DefaultAnchor="100%" Flex="1" Title="GỬI EMAIL CHO CHÚNG TÔI">
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
