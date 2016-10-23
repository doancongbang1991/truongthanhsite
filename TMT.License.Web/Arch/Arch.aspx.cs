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
    public partial class Arch : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSiteName();
            LoadArch();
        }
        private void LoadSiteName()
        {
            try
            {
                DataTable dt = new SiteData().GetDataByName("Arch");
                string detail = dt.Rows[0][(string)SiteData.TBC_SiteDetail].ToString();
                string desp = dt.Rows[0][(string)SiteData.TBC_SiteDesp].ToString();
                this.lbsitedetail.Html = "<h1>" + detail + "</h1>";

                string[] tmps = desp.Split('\n');
                foreach (string tmp in tmps)
                {
                    if (tmp != "")
                    {
                        Ext.Net.Label lb = new Ext.Net.Label();
                        lb.Text = tmp;
                        pnlAlignMiddle.Items.Add(lb);

                    }

                }
                //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void LoadArch()
        {
            try
            {

                DataTable dt = new ArchData().Search(null, "");
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        Ext.Net.Panel prmenu = new Ext.Net.Panel(); // define a menu panel

                        prmenu.Title = dt.Rows[r][(string)ArchData.TBC_ArchName].ToString();
                        prmenu.MarginSpec = "20 10 20 10";
                        //prmenu.UI = Ext.Net.UI.Primary;
                        prmenu.TitleAlign = TitleAlign.Center;

                        Ext.Net.Label lb = new Ext.Net.Label();
                        lb.Text = dt.Rows[r][(string)ArchData.TBC_ArchDetail].ToString();
                        //lb.MarginSpec = "20 60 20 60";

                        // lb.Text = "sdasdsa";
                        //prmenu.Items.Add(lb);

                        Ext.Net.FieldContainer field = new FieldContainer();
                        field.MarginSpec = "20 60 20 60";
                        field.Items.Add(lb);
                        prmenu.Items.Add(field);


                        pnlAbout.Items.Add(prmenu);
                    }


                }
                else
                    UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Ext.Net.Panel freemenu = new Ext.Net.Panel();
                pnlAbout.Items.Add(freemenu);

                //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}