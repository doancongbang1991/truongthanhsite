using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using Ext.Net;
using TMT.License.Core;

namespace TMT.License.Web.TSSystem
{
    public partial class UserProfile : System.Web.UI.Page
    {

        private static string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }

        #region >- UserInfo -<
        private void LoadPage()
        {
            UserCommon.VerifyPage();
            bool bRightView = WebPermission.ViewPermission(WebPermission.SYSTEM_USER);
            if (!bRightView)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.Theme = UserCommon.GetCurrentTheme();
            ReadOnlyControl(true);
            pnChangePass.Hide();
            pnProfile.Show();
            if (!ShowDetails_UserInfo(ref _Exception))
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
            Menu_Load();
        }
        private bool ShowDetails_UserInfo(ref string Exception)
        {
            try
            {
                bool bResult = false;
                string UID = UserCommon.GetCookie_UID();
                DataTable dt = new UserInfoData().GetDataViewByID(UID);
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    this.hiID.Value = dt.Rows[i][(string)UserInfoData.TBC_UID].ToString();
                    this.txtUUsername.Text = dt.Rows[i][(string)UserInfoData.TBC_UUserName].ToString();
                    this.hiUPassword.Value = UserCommon.Decrypt(dt.Rows[i][(string)UserInfoData.TBC_UPassword].ToString());
                    this.txtUFullName.Text = dt.Rows[i][(string)UserInfoData.TBC_UFullName].ToString();
                    this.hiPID.Value = dt.Rows[i][(string)UserInfoData.TBC_PID].ToString();
                    this.txtUPosition.Text = dt.Rows[i][(string)PositionData.TBC_PName].ToString();
                    this.txtUAddress.Text = dt.Rows[i][(string)UserInfoData.TBC_UAddress].ToString();
                    this.txtUPhone.Text = dt.Rows[i][(string)UserInfoData.TBC_UPhone].ToString();
                    this.txtUMobilePhone.Text = dt.Rows[i][(string)UserInfoData.TBC_UMobilePhone].ToString();
                    this.txtUEmail.Text = dt.Rows[i][(string)UserInfoData.TBC_UEmail].ToString();
                    this.txtUNotes.Text = dt.Rows[i][(string)UserInfoData.TBC_UNotes].ToString();
                    this.txtUGRPName.Text = dt.Rows[i][(string)UserGroupData.TBC_UGRPName].ToString();
                    this.hiUGRPID.Value = dt.Rows[i][(string)UserInfoData.TBC_UGRPID].ToString();
                    bResult = true;
                }
                else
                    Exception = Message.MSE_SQLNullData("User");
                return bResult;
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                return false;
            }
        }
        private void ReadOnlyControl(bool ReadOnly)
        {
            UserCommon.ReadOnlyControl(this.txtUAddress, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUUsername, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUFullName, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUPosition, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUPhone, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUMobilePhone, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUEmail, ReadOnly);
            UserCommon.ReadOnlyControl(this.txtUNotes, ReadOnly);
        }
        
        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.System_UserProfile, true);
        }

        #endregion

        #region >- Change Password -<
        private void ClearAllFields_ChangePassword()
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
        private bool ChangePassword(ref string Exception)
        {
            string OldPass = txtOldPassword.Text.Trim();
            if (!OldPass.Equals(hiUPassword.Value.ToString()))
            {
                Exception = Message.MSE_UIWrongOldPass;
                return false;
            }
            else
            {
                if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
                {
                    Exception = Message.MSE_UIWrongNewPass;
                    return false;
                }
                else
                {
                    string NewPass = UserCommon.Encrypt(txtNewPassword.Text);
                    int UID = UserCommon.ToInt(hiID.Value);
                    bool objResult = new UserInfoData().UpdatePassword(UID, NewPass);
                    return objResult;
                }
            }
        }
        protected void btChangePass_Click(object sender, DirectEventArgs e)
        {
            pnProfile.Hide();
            pnChangePass.Show();
            ClearAllFields_ChangePassword();
            this.txtOldPassword.Focus();
        }
        protected void btChange_Click(object sender, DirectEventArgs e)
        {
            UserCommon.VerifyPage();
            if (!ChangePassword(ref _Exception))
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
            else
            {
                hiUPassword.Value = UserCommon.Encrypt(txtNewPassword.Text);
                pnProfile.Show();
                pnChangePass.Hide();
                UserCommon.MsbShow(Message.MSI_UIChangePass, UserCommon.INFORMATION);
            }
        }
        protected void btCancelChange_Click(object sender, DirectEventArgs e)
        {
            pnProfile.Show();
            pnChangePass.Hide();
        }

        #endregion

        [DirectMethod(Namespace = "CompanyX")]
        public void narrow()
        {
            txtUFullName.LabelAlign = Ext.Net.LabelAlign.Top;
            foreach (AbstractComponent Component in FieldSet5.Items)
            {
                if (Component.GetType().ToString() == "Ext.Net.TextField")
                {
                    Ext.Net.TextField tftmp = (Ext.Net.TextField)Component;
                    tftmp.LabelAlign = Ext.Net.LabelAlign.Top;
                    //tftmp.Cls = "textfield-top";
                }
            }
            
            pnProfile.Update();
        }
        [DirectMethod(Namespace = "CompanyX")]
        public void wide()
        {
            
            foreach (AbstractComponent Component in FieldSet5.Items)
            {
                if (Component.GetType().ToString() == "Ext.Net.TextField")
                {
                    Ext.Net.TextField tftmp = (Ext.Net.TextField)Component;
                    tftmp.LabelAlign = Ext.Net.LabelAlign.Left;
                    
                 // tftmp.Cls = "textfield-left";
                }
            }
            pnProfile.Update();
        }
        protected void Menu_Load()
        {
            Store store = this.Menu.GetStore();
            var objs = new List<object>{};
            foreach (AbstractComponent Component in ctl340.Items)
            {
                if (Component.GetType().ToString() == "Ext.Net.Button")
                {
                    Ext.Net.Button bttmp = (Ext.Net.Button)Component;
                    object[] obj = new object[] { ResourceManager.GetIconClassName(bttmp.Icon), bttmp.Text };
                    this.ResourceManager1.RegisterIcon(bttmp.Icon);
                    objs.Add(obj);
                }
            }
            store.DataSource = objs;
            store.DataBind();
        }
    }
}