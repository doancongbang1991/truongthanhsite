using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using Entities;
using Ext.Net;
using TMT.License.Core;

namespace TMT.License.Web.License
{
    public partial class Cost : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCbbCongTrinh();
            LoadCbbDiaDiem();
            LoadCbbTrangThai();
            LoadSiteName();
        }
        private void LoadSiteName()
        {
            try
            {
                DataTable dt = new SiteData().GetDataByName("Cost");
                string detail = dt.Rows[0][(string)SiteData.TBC_SiteDetail].ToString();
                lbsitedesp.Text = dt.Rows[0][(string)SiteData.TBC_SiteDesp].ToString();
                this.lbsitedetail.Html = "<h1>" + detail + "</h1>";

                //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";
            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void Checkdt(object sender, DirectEventArgs e)
        {
            float dtsodo = float.Parse(txtdtsodo.Text);
            float dtxaydung = float.Parse(txtdtxaydung.Text);
            if (dtxaydung > dtsodo)
            {
                UserCommon.MsbShow("Diện Tích Xây Dựng Phải Nhỏ Hơn Diện Tích Sổ Đỏ", UserCommon.ERROR);
                txtdtxaydung.Text = txtdtsodo.Text;
            }
        }
        private void LoadCbbDiaDiem()
        {
            Store store = this.CbbDiaDiem.GetStore();

            store.DataSource = new object[]
        {
            new object[] { "TP.HCM", 0},
            new object[] { "Đà Nẵng", 1},
            new object[] { "Hà Nội", 2},
            new object[] { "Khác", 3}
           
            
        };

            store.DataBind();

        }
        private void LoadCbbCongTrinh()
        {
            Store store = this.CbbCongTrinh.GetStore();

            store.DataSource = new object[]
        {
            new object[] { "Khách sạn, Resort, Karaoke", 0},
            new object[] { "Bar, Cafe, Nhà hàng, Showroom, Shop, Office", 1},
            new object[] { "Biệt thự, nhà từ 2 mặt tiền trở lên", 2},
            new object[] { "Nhà phố, căn hộ",3 }
            
        };

            store.DataBind();


        }
        private void LoadCbbTrangThai()
        {
            Store store = this.CbbTrangThai.GetStore();

            store.DataSource = new object[]
        {
            new object[] { "Cải tạo", 0},
            new object[] { "Xây mới", 1}
            
        };

            store.DataBind();


        }
        protected void Calc(object sender, DirectEventArgs e)
        {
            List<CostConfigEntities> list = new List<CostConfigEntities>();
            DataTable dtbaseprice = new CostConfigData().GetDataByType("BasePrice");
            DataTable dtlocation = new CostConfigData().GetDataByType("Location");
            DataTable dttype = new CostConfigData().GetDataByType("Type");
            DataTable dtstatus = new CostConfigData().GetDataByType("Status");

            int diadiem = 0;
            int congtrinh = 0;
            int trangthai = 0;
            float dtsodo = 0;
            float dtxaydung = 0;
            int sotang = 0;
            int sophongngu = 0;
            int sophongvesinh = 0;
            int basePricem2 = int.Parse(dtbaseprice.Rows[0][CostConfigData.TBC_CostDetail].ToString());
            int basePricePhongngu = int.Parse(dtbaseprice.Rows[1][CostConfigData.TBC_CostDetail].ToString());
            int basePriceVesinh = int.Parse(dtbaseprice.Rows[2][CostConfigData.TBC_CostDetail].ToString());
            if (CbbCongTrinh.SelectedItem.Value == null)
            {
                UserCommon.SetValueControl(CbbCongTrinh, "0");

            }
            if (CbbDiaDiem.SelectedItem.Value == null)
            {
                UserCommon.SetValueControl(CbbDiaDiem, "0");

            }
            if (CbbTrangThai.SelectedItem.Value == null)
            {
                UserCommon.SetValueControl(CbbTrangThai, "0");

            }
            try
            {
                congtrinh = int.Parse(CbbCongTrinh.SelectedItem.Value);
                diadiem = int.Parse(CbbDiaDiem.SelectedItem.Value);
                trangthai = int.Parse(CbbTrangThai.SelectedItem.Value);
                dtxaydung = float.Parse(txtdtxaydung.Text.Trim());
                sotang = int.Parse(txtsotang.Text.Trim());
                sophongngu = int.Parse(txtsophongngu.Text.Trim());
                dtsodo = float.Parse(txtdtsodo.Text.Trim());
                sophongvesinh = int.Parse(txtsophongvesinh.Text.Trim());
            }
            catch (Exception)
            {
            }
            //use funtion to calculate here
            float giacongtrinh = int.Parse(dttype.Rows[congtrinh][CostConfigData.TBC_CostDetail].ToString());
            float giadiadiem = int.Parse(dtlocation.Rows[diadiem][CostConfigData.TBC_CostDetail].ToString());
            float giatrangthai = int.Parse(dtstatus.Rows[trangthai][CostConfigData.TBC_CostDetail].ToString());
            float vesinh = sophongvesinh * basePriceVesinh * giacongtrinh/100 * giadiadiem/100 * giatrangthai/100;
            float phongngu = sophongngu * basePricePhongngu * giacongtrinh / 100 * giadiadiem / 100 * giatrangthai / 100;
            float phantho = basePricem2 * dtxaydung * sotang * giacongtrinh / 100 * giadiadiem / 100 * giatrangthai / 100;
            float thietke = phantho * 4 / 100;
            float duphong = phantho * 10 / 100;
            float noithat = phantho * 30 / 100;
            float hoanthien = phantho * 90 / 100;
            float tongcong = phantho + thietke + duphong + noithat + hoanthien + vesinh + phongngu;
            txtPhanTho.Text = phantho.ToString("n") + " vnđ";
            txtThietKe.Text = thietke.ToString("n") + " vnđ";
            txtDuPhong.Text = duphong.ToString("n") + " vnđ";
            txtNoiThat.Text = noithat.ToString("n") + " vnđ";
            txtHoanThien.Text = hoanthien.ToString("n") + " vnđ";
            txtTongCong.Text = tongcong.ToString("n") + " vnđ";
        }
    }
}