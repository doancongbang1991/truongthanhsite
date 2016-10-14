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

namespace TMT.License.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private static string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }
        private void LoadPage()
        {
            UserCommon.ClearCookieUserInfo();
            ResourceManager.SetTheme(UserCommon.GetCurrentTheme());
            FormatWebControl();
        }
        private void FormatWebControl()
        {
            string[] Exception = new string[] { Message.MSE_WCFieldNotVaild("UserName"), Message.MSE_WCFieldNotVaild("Password") };
            object[] Control = new object[] { this.txtUsername.ID, this.txtPassword.ID };
            UserCommon.SetValidate(this.btnLogin, Control, Exception);
        }
        protected void btnLogin_Click(object sender, DirectEventArgs e)
        {
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Text;
            if (!UserLogin(username, password))
                UserCommon.MsbShow(hiErrorMessage.Value.ToString(), UserCommon.ERROR);
            else
                Response.Redirect(UserCommon.Web_MainPage, true);
        }
        protected void btnSignup_Click(object sender, DirectEventArgs e)
        {
            ClearAllFields_UserInfo();
            this.wSingUp.Show();
            this.txtUUsername.Focus();
        }
        protected void btOk_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            UserInfoEntities objUserInfo = new UserInfoEntities();
            objUserInfo = GetUserInfo(ref Insert, ref _Exception);
            if (objUserInfo == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }
            if (Insert)
            {
                bResult = new UserInfoData().Insert(ref objUserInfo);
                if (bResult)
                {
                    ClearAllFields_UserInfo();
                    this.wSingUp.Hide();
                }
                else
                    UserCommon.MsbShow(Message.MSE_SQLADD, UserCommon.ERROR);
            }
            else
            {
                //bResult = new UserInfoData().Update(objUserInfo);
                //if (bResult)
                //    UserCommon.MsbShow(Message.MSI_WCSave("User"), UserCommon.INFORMATION);
                //else
                //    UserCommon.MsbShow(Message.MSE_SQLEDIT, UserCommon.ERROR);
            }
        }
        protected void btCancel_Click(object sender, DirectEventArgs e)
        {
            this.wSingUp.Hide();
        }
        private bool UserLogin(string uid, string pwd)
        {
            bool bResult = false;
            try
            {
                DataTable dt = new UserInfoData().GetDataByUUserName(uid);
                if (dt.Rows.Count > 0)
                {
                    string PassInDB = dt.Rows[0][(string)UserInfoData.TBC_UPassword].ToString();
                    if (UserCommon.Decrypt(PassInDB).Equals(pwd))
                    {
                        bool bActive = UserCommon.ToBoolean(dt.Rows[0][(string)UserInfoData.TBC_UActive]);
                        if (bActive)
                        {
                            UserCommon.SetCookieUserInfo(new string[] { dt.Rows[0][(string)UserInfoData.TBC_UID].ToString(), dt.Rows[0][(string)UserInfoData.TBC_UGRPID].ToString() });
                            bResult = true;
                        }
                        else
                            hiErrorMessage.Value = Message.MSE_UIBlockedUser;
                    }
                    else
                        hiErrorMessage.Value = Message.MSE_LGWrongUserPass;
                }
                else
                    hiErrorMessage.Value = Message.MSE_SQLNullData("User");
            }
            catch (Exception ex)
            {
                hiErrorMessage.Value = ex;
                bResult = false;
            }
            return bResult;
        }
        private void ClearAllFields_UserInfo()
        {
            txtUUsername.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtUFullName.Text = "";
            txtUMobilePhone.Text = "";
            txtUPhone.Text = "";
            txtUEmail.Text = "";
            txtUAddress.Text = "";
        }
        private UserInfoEntities GetUserInfo(ref bool Insert, ref string Exception)
        {
            UserInfoEntities res = new UserInfoEntities();
            if (!UserCommon.HasValue(this.txtUUsername))
            {
                Exception = Message.MSE_WCFieldRequired("UserName");
                return null;
            }
            if (this.txtUUsername.Text.Trim().Contains(";"))
            {
                Exception = Message.MSE_WCFieldNotVaild("UserName");
                return null;
            }
            if (Insert)
            {
                bool bExist = new UserInfoData().CheckExistUUserName(this.txtUUsername.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("UserName");
                    return null;
                }
            }
            if (!UserCommon.HasValue(this.txtUFullName))
            {
                Exception = Message.MSE_WCFieldRequired("FullName");
                return null;
            }
            res.UUserName = txtUUsername.Text.Trim();
            if (Insert)
            {
                //string DefaultPassword = ConfigurationManager.AppSettings["PASSWORDDEFAULT"].ToString().Trim();
                string DefaultPassword = txtUUsername.Text;
                res.UPassword = UserCommon.Encrypt(DefaultPassword);
            }
            //res.UFullName = UserCommon.ToUpperFisrtChar(txtUFullName.Text);
            res.UFullName = txtUFullName.Text;
            res.UAddress = txtUAddress.Text;
            res.UPhone = txtUPhone.Text;
            res.UMobilePhone = txtUMobilePhone.Text;
            res.UEmail = txtUEmail.Text;
            res.UActive = 1;
            res.UGRPID = 2;
            res.PID = 4;
            return res;
        }

    }
}