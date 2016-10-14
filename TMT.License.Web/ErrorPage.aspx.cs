using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMT.License.Core;

namespace TMT.License.Web
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string messSession = "";

                if (UserCommon.GetSession(UserCommon.SS_Message) != null)
                    messSession = UserCommon.GetSession(UserCommon.SS_Message).ToString();
                else
                    messSession = "Your session has expired. Please login again!";
                this.btLogin.Text = "Login again";
                this.btHome.Visible = false;
                this.lblTitle.Text = "Error Message";
                this.lblMessenger.Text = messSession;
                UserCommon.ClearCookieUserInfo();
            }
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myUniqueKey", "self.parent.location='Login.aspx';", true);
        }

        protected void btHome_Click(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myUniqueKey", "self.parent.location='MainPage.aspx';", true);
        }

    }
}