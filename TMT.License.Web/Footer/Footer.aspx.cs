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
    public partial class Footer : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLink();


            LoadInfo();
        }
        private void LoadLink()
        {
            for (int i = 0; i < 4; i++)
            {
                DataTable dt = new FooterData().GetDataByType(i.ToString());
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        switch (dt.Rows[r][(string)FooterData.TBC_FooterTypeID].ToString())
	                    {
                            case "1":
                                {
                                    Ext.Net.Label lb = new Ext.Net.Label();
                                    lb.ID = "Footer" + dt.Rows[r][(string)FooterData.TBC_FooterID].ToString();
                                    lb.Text = dt.Rows[r][(string)FooterData.TBC_FooterName].ToString();
                                    try
                                    {
                                        lb.Icon = (Ext.Net.Icon)Enum.Parse(typeof(Ext.Net.Icon), dt.Rows[r][(string)FooterData.TBC_FooterIcon].ToString());
                                    }
                                    catch (Exception)
                                    {


                                    }
                                    lb.MarginSpec = "5 0 20 5";
                                    lb.AddCls("lbinfo");
                                    pnlNhansu.Add(lb);
                                    break;
                                }
                            case "2":
                                Hyperlink hplink = new Hyperlink();
                                hplink.ID = "hplink" + dt.Rows[r][(string)FooterData.TBC_FooterID].ToString();
                                hplink.Text = dt.Rows[r][(string)FooterData.TBC_FooterName].ToString();
                                hplink.Height = new Unit("92"); 
                                try
                                {
                                   // hplink.Icon = (Ext.Net.Icon)Enum.Parse(typeof(Ext.Net.Icon), dt.Rows[r][(string)FooterData.TBC_FooterIcon].ToString());
                                }
                                catch (Exception)
                                {


                                }
                               
                                
                                try
                                {
                                    hplink.NavigateUrl = dt.Rows[r][(string)FooterData.TBC_FooterLink].ToString();
                                    hplink.ImageUrl = @"~\images\extnet.png";
                                }
                                catch (Exception)
                                {


                                }
                                pnlWebsite.Items.Add(hplink);
                                break;
                            //case "3":
                            //    Hyperlink hplink1 = new Hyperlink();
                            //    hplink1.ID = "hplink1" + dt.Rows[r][(string)FooterData.TBC_FooterID].ToString();
                            //    hplink1.Text = dt.Rows[r][(string)FooterData.TBC_FooterName].ToString();
                            //    hplink1.Height = new Unit("92"); 
                            //    try
                            //    {
                            //        //hplink.Icon = (Ext.Net.Icon)Enum.Parse(typeof(Ext.Net.Icon), dt.Rows[r][(string)FooterData.TBC_FooterIcon].ToString());
                            //    }
                            //    catch (Exception)
                            //    {


                            //    }
                                
                                
                            //    try
                            //    {
                            //        hplink1.NavigateUrl = dt.Rows[r][(string)FooterData.TBC_FooterLink].ToString();
                            //        hplink1.ImageUrl = @"~\images\extnet.png";
                            //    }
                            //    catch (Exception)
                            //    {


                            //    }
                            //    pnlDoitac.Items.Add(hplink1);
                            //    break;
		                    default:
                            break;
	                    }
                        
                        
                       
                    }


                }
                else
                    UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
            }
            

            //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";

        }


        private void LoadInfo()
        {
            DataTable dt = new FooterData().GetDataByType("4");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    Ext.Net.Label lb = new Ext.Net.Label();
                    lb.ID = "Footer" + dt.Rows[r][(string)FooterData.TBC_FooterID].ToString();
                    lb.Text = dt.Rows[r][(string)FooterData.TBC_FooterName].ToString();
                    try
                    {
                        lb.Icon = (Ext.Net.Icon)Enum.Parse(typeof(Ext.Net.Icon), dt.Rows[r][(string)FooterData.TBC_FooterIcon].ToString());
                    }
                    catch (Exception)
                    {


                    }

                    lb.MarginSpec = "5 0 20 5";
                    lb.AddCls("lbinfo");
                    pnlInfo.Add(lb);
                }


            }
            else
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);

            //this.lbsitedesp.Html = "<h6>" + desp + "</h6>";

        }

    }

}