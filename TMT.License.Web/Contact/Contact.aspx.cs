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
    public partial class Contact : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {

            //LoadSiteName();
            LoadMap();
            LoadInfo();
        }
        protected void SendMail(object sender, EventArgs e)
        {
            try
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
                mail.Body = txtMessName.Text.Trim() + "\n" +
                            numMessYear.Text.Trim() + "\n" +
                            txtMessMail.Text.Trim() + "\n" +
                            txtMessPhone.Text.Trim() + "\n" +
                            txtMessBody.Text.Trim() + "\n";
                smtpClient.Send(mail);
            }
            catch (Exception)
            {

                
            }
           

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
        private void ClearField()
        {
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
            if (rMessGenM.Checked == true)
            {
                res.MessGen = true;
            }
            else res.MessGen = false;
            res.MessRead = false;
            res.MessPhone = txtMessPhone.Text;
            res.MessBody = txtMessBody.Text;
            return res;
        }
        private void LoadSiteName()
        {
            try
            {
                DataTable dt = new SiteData().GetDataByName("Contact");
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
        private void LoadMap()
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Home\map\index.html";
            cn.Mode = LoadMode.Frame;

            cn.DisableCaching = true;
            this.pnlmap.LoadContent(cn);
        }
        private void LoadInfo() {
            DataTable dt = new FooterData().GetDataByType("4");
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        Ext.Net.Label lb = new Ext.Net.Label();
                        lb.ID = "Footer" + dt.Rows[r][(string)FooterData.TBC_FooterID].ToString();
                        lb.Text = dt.Rows[r][(string)FooterData.TBC_FooterName].ToString();
                        lb.Icon =(Ext.Net.Icon)Enum.Parse(typeof(Ext.Net.Icon), dt.Rows[r][(string)FooterData.TBC_FooterIcon].ToString());
                        lb.MarginSpec = "10 0 20 10";
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