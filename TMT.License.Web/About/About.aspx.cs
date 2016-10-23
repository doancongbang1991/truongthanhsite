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
    public partial class About : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            LoadSiteName();
            LoadAbout();
            
        }
        private void LoadSiteName() {
            try
            {
                DataTable dt = new SiteData().GetDataByName("About");
                string detail = dt.Rows[0][(string)SiteData.TBC_SiteDetail].ToString();
                lbsitedesp.Text = dt.Rows[0][(string)SiteData.TBC_SiteDesp].ToString();
               
                this.lbsitedetail.Html = "<h1>" + detail + "</h1>" ;
                
                //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        private void LoadAbout()
        {
            try
            {

                DataTable dt = new AboutData().Search(null,"");
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        Ext.Net.Panel prmenu = new Ext.Net.Panel(); // define a menu panel
                        
                        prmenu.Title = dt.Rows[r][(string)AboutData.TBC_AboutName].ToString();
                        prmenu.MarginSpec = "20 0 20 0";
                        //prmenu.UI = Ext.Net.UI.Primary;
                        prmenu.TitleAlign = TitleAlign.Center;

                        Ext.Net.Label lb = new Ext.Net.Label();
                        lb.Text = dt.Rows[r][(string)AboutData.TBC_AboutDetail].ToString();
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

                //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}