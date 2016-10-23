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
        private void LoadCbbDiaDiem()
        {
            Store store = this.CbbDiaDiem.GetStore();

            store.DataSource = new object[]
        {
            new object[] { "TP.HCM", 0},
            new object[] { "Đà Nẵng", 1},
            new object[] { "Hà Nội", 2}
            
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
            new object[] { "Nhà phố, căn hộ", "Phone",3 }
            
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
            string diadiem = "";
            string congtrinh = "";
            string trangthai = "";
            string dtsodo = "";
            string dtxaydung = "";
            string sotang = "";
            string sophongngu = "";
            string sophongvesinh = "";

            if (CbbCongTrinh.SelectedItem == null)
            {
                UserCommon.SetValueControl(CbbCongTrinh, "0");

            }
            if (CbbDiaDiem.SelectedItem == null)
            {
                UserCommon.SetValueControl(CbbDiaDiem, "0");

            }
            if (CbbTrangThai.SelectedItem == null)
            {
                UserCommon.SetValueControl(CbbTrangThai, "0");

            }
            congtrinh = CbbCongTrinh.SelectedItem.Value;
            diadiem = CbbDiaDiem.SelectedItem.Value;
            trangthai = CbbTrangThai.SelectedItem.Value;
            dtxaydung = txtdtxaydung.Text.Trim();
            sotang = txtsotang.Text.Trim();
            sophongngu = txtsophongngu.Text.Trim();
            dtsodo = txtdtsodo.Text.Trim();
            sophongvesinh = txtsophongvesinh.Text.Trim();

            //use funtion to calculate here




        }
    }
}