<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cost.aspx.cs" Inherits="TMT.License.Web.License.Cost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <link href="../styles/StyleCustomize.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" AutoScroll="False" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Layout="AutoLayout">
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
                                <ext:Label runat="server" ID="lbsitedetail"/>
                                <ext:Label runat="server" ID="lbsitedesp" MarginSpec="25 0 5 0"/>
                            </Items>
                        </ext:Panel>
                    
                
                <ext:Panel ID="pnlDuTru" Region="North" runat="server" Title="Ước Tính Dự Trù Chi Phí" Layout="AnchorLayout" MarginSpec = "20 0 20 0" >
                    <TabConfig ID="TabConfig1" runat="server" UI="Info" />
                    <Items>
                        <ext:Container ID="Container1"
                            runat="server"
                            AnchorHorizontal="100%"
                            Layout="HBoxLayout"
                            Margin="10">
                            <Items>
                                <ext:FieldContainer ID="FieldSet2" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label1" Html="<i>Chọn Địa Điểm</i>" runat="server" />
                                        <ext:ComboBox
                                            ID="CbbDiaDiem"
                                            runat="server"
                                            Editable="false"
                                            DisplayField="name"
                                            ValueField="value"
                                            QueryMode="Local"
                                            TriggerAction="All"
                                            EmptyText="Chọn địa điểm...">
                                            <Store>
                                                <ext:Store ID="Store1" runat="server">
                                                    <Model>
                                                        <ext:Model ID="Model2" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="name" />
                                                                <ext:ModelField Name="value" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>

                                        </ext:ComboBox>
                                        <ext:Label ID="Label2" Html="<i>Diện Tích Xây Dựng (Tầng Trệt)</i>" runat="server" />
                                        <ext:NumberField runat="server" Text="0" ID="txtdtxaydung" />

                                    </Items>
                                </ext:FieldContainer>
                                <ext:FieldContainer ID="FieldSet1" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label3" Html="<i>Chọn Công Trình</i>" runat="server" />
                                        <ext:ComboBox
                                            ID="CbbCongTrinh"
                                            runat="server"
                                            Editable="false"
                                            DisplayField="name"
                                            ValueField="value"
                                            QueryMode="Local"
                                            TriggerAction="All"
                                            EmptyText="Chọn công trình...">
                                            <Store>
                                                <ext:Store ID="Store2" runat="server">
                                                    <Model>
                                                        <ext:Model ID="Model1" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="name" />
                                                                <ext:ModelField Name="value" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>

                                        </ext:ComboBox>
                                        <ext:Label ID="Label4" Html="<i>Số Tầng (Bao Gồm Tầng Trệt)</i>" runat="server" />
                                        <ext:NumberField runat="server" Text="0" ID="txtsotang" />

                                    </Items>
                                </ext:FieldContainer>
                                <ext:FieldContainer ID="FieldSet3" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label5" Html="<i>Trạng Thái</i>" runat="server" />
                                        <ext:ComboBox
                                            ID="CbbTrangThai"
                                            runat="server"
                                            Editable="false"
                                            DisplayField="name"
                                            ValueField="value"
                                            QueryMode="Local"
                                            TriggerAction="All"
                                            EmptyText="Chọn trạng thái...">
                                            <Store>
                                                <ext:Store ID="Store3" runat="server">
                                                    <Model>
                                                        <ext:Model ID="Model3" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="name" />
                                                                <ext:ModelField Name="value" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>

                                        </ext:ComboBox>
                                        <ext:Label ID="Label6" Html="<i>Số Phòng Ngủ</i>" runat="server" />
                                        <ext:NumberField runat="server" Text="0" ID="txtsophongngu" />

                                    </Items>
                                </ext:FieldContainer>
                                <ext:FieldContainer ID="FieldSet4" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label7" Html="<i>Diện Tích Trong Sổ Đỏ (m2)</i>" runat="server" />
                                        <ext:NumberField runat="server" Text="0" ID="txtdtsodo" />
                                        <ext:Label ID="Label8" Html="<i>Số Phòng Vệ Sinh</i>" runat="server" />
                                        <ext:NumberField runat="server" ID="txtsophongvesinh" Text="0" />

                                    </Items>
                                </ext:FieldContainer>
                            </Items>
                        </ext:Container>


                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlDauTu" Region="South" runat="server" Title="Khải Toán Mức Đầu Tư" Layout="AnchorLayout">
                    <TabConfig ID="TabConfig2" runat="server" UI="Info" />
                    <Items>


                        <ext:Container ID="Container2"
                            runat="server"
                            AnchorHorizontal="100%"
                            Layout="HBoxLayout"
                            Margin="10">
                            <Items>
                                <ext:FieldContainer ID="FieldSet5" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label9" Html="<i>Thiết Kế Trọng Gói</i>" runat="server" />
                                        <ext:TextField ID="txtThietKe" runat="server" Text="0" Editable="false" />
                                        <ext:Label ID="Label10" Html="<i>Dự Phòng Phí</i>" runat="server" />
                                        <ext:TextField ID="txtDuPhong" runat="server" Text="0" Editable="false" />

                                    </Items>
                                </ext:FieldContainer>
                                <ext:FieldContainer ID="FieldSet6" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label11" Html="<i>Phần Thô</i>" runat="server" />
                                        <ext:TextField ID="txtPhanTho" runat="server" Text="0" Editable="false" />
                                        <ext:Label ID="Label12" Html="<i>Tổng Cộng</i>" runat="server" />
                                        <ext:TextField ID="txtTongCong" runat="server" Text="0" Editable="false" />

                                    </Items>
                                </ext:FieldContainer>
                                <ext:FieldContainer ID="FieldSet7" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label13" Html="<i>Hoàn Thiện</i>" runat="server" />
                                        <ext:TextField ID="txtHoanThien" runat="server" Text="0" Editable="false" />


                                    </Items>
                                </ext:FieldContainer>
                                <ext:FieldContainer ID="FieldSet8" Region="Center" runat="server" Flex="1" Title="" Padding="7"
                                    Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Label ID="Label15" runat="server" Html="<i>Nội Thất</i>" />
                                        <ext:TextField ID="txtNoiThat" runat="server" Text="0" Editable="false" />
                                        <ext:Label ID="Label14" runat="server" Text="." />
                                        <ext:Button ID="btncalc" runat="server" Text="Tính Toán">
                                            <DirectEvents>
                                                <Click OnEvent="Calc" />
                                            </DirectEvents>
                                        </ext:Button>

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
