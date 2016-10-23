using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using Ext.Net;
using TMT.License.Core;

namespace TMT.License.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //if (UserCommon.GetCookie_UIDForLogin() == null)
            //    HttpContext.Current.Response.Redirect(UserCommon.Web_LoginPage, true);
            if (!IsPostBack)
                LoadPage();
            //if (LoadMenu())
            //    LoadPageInFrame();
            //else
            //    HttpContext.Current.Response.Redirect(UserCommon.Web_ErrorPage, true);

            
        }
        private void LoadPage()
        {
            
           
        }
        protected void Show(object sender, DirectEventArgs e)
        {
           
            //if (UserCommon.GetCookie_UIDForLogin() == null)
            //    HttpContext.Current.Response.Redirect(UserCommon.Web_LoginPage, true);
            //if (!IsPostBack)
            //    LoadPage();
            //if (LoadMenu())
            //    LoadPageInFrame();
            //else
            //    HttpContext.Current.Response.Redirect(UserCommon.Web_ErrorPage, true);
        }
        protected void ShowCost(object sender, DirectEventArgs e)
        {
            
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Cost\Cost.aspx";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlCost.LoadContent(cn);
        }
        protected void ShowAbout(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\About\About.aspx";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlAbout.LoadContent(cn);
        }
        protected void ShowArch(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Arch\Arch.aspx";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlArch.LoadContent(cn);
        }
        protected void ShowCon(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Home\Cons.html";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlCon.LoadContent(cn);
        }
        protected void ShowProject(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Home\Projects.html";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlProject.LoadContent(cn);
        }
        protected void ShowFur(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Home\Fur.html";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlFur.LoadContent(cn);
        }
        protected void ShowContact(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = @"~\Contact\Contact.aspx";
            cn.Mode = LoadMode.Html;
            cn.DisableCaching = true;
            this.pnlContact.LoadContent(cn);
        }
        protected void ShowHotLine(object sender, DirectEventArgs e)
        {
            //pnlHome.Show();
            TabPanelMain.SetActiveTab(pnlHome);
        }
        private void LoadPageInFrame()
        {
            object url = UserCommon.GetSession(UserCommon.SS_URLReturn);
            if (url != null)
            {
                if (url.ToString().Replace("/", "").Trim().Length > 0)
                {
                    ComponentLoader cn = new ComponentLoader();
                    cn.Url = url.ToString();
                    cn.Mode = LoadMode.Html;
                    cn.DisableCaching = true;
                    //this.pnlCenter.LoadContent(cn);
                }
            }
            else
            {
                ComponentLoader cn = new ComponentLoader();
                if (UserCommon.GetCookie_GRPID() != "11239")
                    cn.Url = UserCommon.System_UserInfoDetails;
                else cn.Url = UserCommon.System_UserInfoManager;
                cn.Mode = LoadMode.Html;
                cn.DisableCaching = true;
                //this.pnlCenter.LoadContent(cn);
            }

        }

        
        
    }
}