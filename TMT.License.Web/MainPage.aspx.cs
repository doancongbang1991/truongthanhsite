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
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUrlReturn();
            if (UserCommon.GetCookie_UIDForLogin() == null)
                HttpContext.Current.Response.Redirect(UserCommon.Web_LoginPage, true);
            if (!IsPostBack)
                LoadPage();
            if (LoadMenu())
                LoadPageInFrame();
            else
                HttpContext.Current.Response.Redirect(UserCommon.Web_ErrorPage, true);
        }
        private void LoadPage()
        {
            LoadThemeDefault();
            SetInfoStatusBar();

        }
        private void CheckUrlReturn()
        {
            string Url = UserCommon.GetValueParam_URLReturn();
            if (Url.Trim().Replace("/", "").Trim().Length > 0)
            {
                UserCommon.SetSession(UserCommon.SS_URLReturn, Url);
                Response.Redirect(UserCommon.Web_MainPage);
            }
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
                    this.pnlCenter.LoadContent(cn);
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
                this.pnlCenter.LoadContent(cn);
            }

        }
        private void LoadThemeDefault()
        {
            Theme currentTheme = UserCommon.GetCurrentTheme();
            if (currentTheme.ToString().Equals("Gray"))
                ThemeLabel.Text = "Blue Themes";
            else
                ThemeLabel.Text = "Default Themes";
            rsmain.Theme = currentTheme;
        }
        private void SetInfoStatusBar()
        {
            try
            {
                DateTime CurrentDate = UserCommon.GetDateTime();
                txtDateTime.Text = "Today: " + UserCommon.ToDateString(CurrentDate);
                string UID = UserCommon.GetCookie_UID();
                DataTable dt = new UserInfoData().GetDataByID(UID);
                if (dt.Rows.Count > 0)
                    txtUserStatus.Text = dt.Rows[0][(string)UserInfoData.TBC_UUserName].ToString().ToUpper() + " - " + dt.Rows[0][(string)UserInfoData.TBC_UFullName].ToString();
                else
                {
                    UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_SQLNullData("User"));
                    Response.Redirect(UserCommon.Web_ErrorPage, true);
                }
            }
            catch
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_SQLNullData("User"));
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
        }
        private bool LoadMenu()
        {
            bool bResult = false;
            try
            {
                string UGRPID = UserCommon.GetCookie_GRPID();
                string ParentID = "0";
                DataTable dt = new MenuData().GetDataViewBy(ParentID, UGRPID);
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        Ext.Net.MenuPanel prmenu = new MenuPanel(); // define a menu panel
                        prmenu.Icon = (Icon)Enum.Parse(typeof(Icon), dt.Rows[r][(string)MenuData.TBC_MIcon].ToString());
                        prmenu.Title = dt.Rows[r][(string)MenuData.TBC_MDecription].ToString();
                        ParentID = dt.Rows[r][(string)MenuData.TBC_MID].ToString();
                        DataTable dtSub = new MenuData().GetDataViewBy(ParentID, UGRPID);
                        if (dtSub.Rows.Count > 0)
                        {
                            for (int rc = 0; rc < dtSub.Rows.Count; rc++)
                            {
                                Ext.Net.MenuItem item = new Ext.Net.MenuItem();
                                item.Text = dtSub.Rows[rc][(string)MenuData.TBC_MDecription].ToString();
                                item.Icon = Icon.Rgb;
                                item.DirectEvents.Click.Event += click;

                                Ext.Net.Parameter prmurl = new Ext.Net.Parameter();
                                prmurl.Name = "url";
                                prmurl.Value = dtSub.Rows[rc][(string)MenuData.TBC_MUrl].ToString();
                                prmurl.Mode = ParameterMode.Value;

                                Ext.Net.Parameter prmName = new Ext.Net.Parameter();
                                prmName.Name = "Name";
                                prmName.Value = dtSub.Rows[rc][(string)MenuData.TBC_MDecription].ToString();
                                prmName.Mode = ParameterMode.Value;

                                item.DirectEvents.Click.ExtraParams.Add(prmurl);
                                item.DirectEvents.Click.ExtraParams.Add(prmName);

                                prmenu.Menu.Items.Add(item);
                            }
                        }
                        pnlMenu.Items.Add(prmenu);
                    }
                    bResult = true;
                }
                else
                    UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
            }
            catch (Exception ex)
            {
                UserCommon.SetSession(UserCommon.SS_Message, ex.ToString());
            }
            return bResult;
        }
        protected void Theme_Click(object sender, DirectEventArgs e)
        {
            if (UserCommon.GetCurrentTheme().ToString().Equals("Gray"))
            {
                UserCommon.SetCookieTheme("Default", false);
                ThemeLabel.Text = "Gray Themes";
            }
            else
            {
                UserCommon.SetCookieTheme("Gray", false);
                ThemeLabel.Text = "Default Themes";
            }
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);

        }
        protected void btLogout_Click(object sender, DirectEventArgs e)
        {
            UserCommon.ClearCookieUserInfo();
            Response.Redirect(UserCommon.Web_LoginPage, true);
        }
        protected void click(object sender, DirectEventArgs e)
        {
            ComponentLoader cn = new ComponentLoader();
            cn.Url = e.ExtraParams["url"].ToString();
            cn.Mode = LoadMode.Frame;
            cn.DisableCaching = true;
            this.pnlCenter.LoadContent(cn);
            this.pnlCenter.Title = e.ExtraParams["Name"].ToString();

        }
    }
}