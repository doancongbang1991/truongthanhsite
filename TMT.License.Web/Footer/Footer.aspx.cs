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
using System.Net.Mail;

namespace TMT.License.Web.License
{
    public partial class Footer : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLink();


            //LoadInfo();
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
                                    lb.MarginSpec = "1 0 20 1";
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
                                    hplink.ImageUrl = @"~\images\Logo.png";
                                }
                                catch (Exception)
                                {


                                }
                                pnlWebsite.Items.Add(hplink);
                                break;
                            case "3":
                                Hyperlink hplink1 = new Hyperlink();
                                hplink1.ID = "hplink" + dt.Rows[r][(string)FooterData.TBC_FooterID].ToString();
                                hplink1.Text = dt.Rows[r][(string)FooterData.TBC_FooterName].ToString();
                                hplink1.Height = new Unit("92");
                                try
                                {
                                    // hplink.Icon = (Ext.Net.Icon)Enum.Parse(typeof(Ext.Net.Icon), dt.Rows[r][(string)FooterData.TBC_FooterIcon].ToString());
                                }
                                catch (Exception)
                                {


                                }


                                try
                                {
                                    hplink1.NavigateUrl = dt.Rows[r][(string)FooterData.TBC_FooterLink].ToString();
                                    hplink1.ImageUrl = @"~\images\Logo.png";
                                }
                                catch (Exception)
                                {


                                }
                                pnlInfo.Items.Add(hplink1);
                                break;
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
        protected void SendMail(object sender, EventArgs e)
        {
            SmtpClient smtpClient = new SmtpClient("mail.MyWebsiteDomainName.com", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("info@MyWebsiteDomainName", "MyWeb Site");
            mail.To.Add(new MailAddress("info@MyWebsiteDomainName"));
            mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));

            //smtpClient.Send(mail);

            bool Insert = true;
            bool bResult = false;
            MessageEntities objProject = new MessageEntities();
            objProject = GetMess();
            if (objProject == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new MessageData().Insert(ref objProject);
                if (bResult)
                {
                    UserCommon.MsbShow("Thông Tin đã được ghi nhận", UserCommon.INFORMATION);
                }
                else
                    UserCommon.MsbShow(Message.MSE_SQLADD, UserCommon.ERROR);
            }
            ClearField();
        }
        private void ClearField(){
            txtMessName.Text = "";
            numMessYear.Text = "0";
            txtMessMail.Text = "";
            rMessGenM.Checked = true;
            
           
            
            txtMessPhone.Text = "";
            txtMessBody.Text = "";
        }
        private MessageEntities GetMess()
        {
            MessageEntities res = new MessageEntities();
            res.MessName = txtMessName.Text.Trim();
            res.MessYear = numMessYear.Text;
            res.MessMail = txtMessMail.Text;
            if (rMessGenM.Checked == true) {
                res.MessGen = true;
            }
            else res.MessGen = false;
            res.MessRead = false;
            res.MessPhone = txtMessPhone.Text;
            res.MessBody = txtMessBody.Text;
            return res;
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