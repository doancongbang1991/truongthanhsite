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
    public partial class ArchPhongThuy : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {

           
        }
        
        protected void btnPhongThuy_Click(object sender, DirectEventArgs e)
        {
            DateTime dt = dtpicker.SelectedDate;
            string day = dt.ToString("dd");
            string month = dt.ToString("MM");
            string year = dt.ToString("yyyy");
            string gioitinh = "";
            if (rMale.Checked && rFemale.Checked == false) {
                gioitinh = "nam" ;
            }
            else gioitinh = "nu";
            string dateurl = day + month + year;
            hpBac.ImageUrl          = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "bac/2534.png";
            hpDongBac.ImageUrl      = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "dongbac/2534.png";
            hpDong.ImageUrl         = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "dong/2534.png";
            hpDongNam.ImageUrl      = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "dongnam/2534.png";
            hpNam.ImageUrl          = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "nam/2534.png";
            hpTayNam.ImageUrl       = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "taynam/2534.png";
            hpTay.ImageUrl          = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "tay/2534.png";
            hpTayBac.ImageUrl       = "http://www.xemngay.com/phongthuy.aspx/" + dateurl + "/" + gioitinh + "/" + "taybac/2534.png";
            pnlAlignMiddle.Reload();
        }
        
    }
}