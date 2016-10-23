using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using Entities;
using Ext.Net;
using TMT.License.Core;

namespace TMT.License.Web.TSSystem
{
    public partial class UserInfoDetails : System.Web.UI.Page
    {
        #region >- Page -<

        private static string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }
        private void CheckRightRecords()
        {
            bool Insert = !UserCommon.ToBoolean(UserCommon.ToInt(this.hiID.Value));
            bool RAdd = WebPermission.AddPermission(WebPermission.SYSTEM_USER);
            bool REdit = WebPermission.EditPermission(WebPermission.SYSTEM_USER);

            this.btAdd.Disabled = !RAdd;
            if (Insert)
                this.btSave.Disabled = !RAdd;
            else
            {
                this.btSave.Disabled = !REdit;
                this.btResetPass.Disabled = !REdit;
            }

        }
        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.SYSTEM_USER);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.Theme = UserCommon.GetCurrentTheme();
            LoadcbbPID();
            string UID = UserCommon.GetValueParam_ID();
            if (UID.Length > 0)
            {
                this.ToolbarSeparatorbtResetPass.Show();
                this.btResetPass.Show();
                ShowDetails_UserInfo(UID);
            }
            else
            {
                this.btResetPass.Hide();
                this.ToolbarSeparatorbtResetPass.Hide();
                ClearAllFields_UserInfo();
            }
            CheckRightRecords();
        }
        protected void btBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.FormatPageWithParameter(UserCommon.System_UserInfoManager));
        }
        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            int UID = UserCommon.ToInt(this.hiID.Value);
            if (UID > 0)
                Response.Redirect(UserCommon.FormatPageWithParameter(UserCommon.System_UserInfoDetails));
            else
                Response.Redirect(UserCommon.System_UserInfoDetails);
        }
        #endregion

        #region >- UserInfo -<
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            ClearAllFields_UserInfo();
            CheckRightRecords();
        }
        protected void btSave_Click(object sender, DirectEventArgs e)
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
                    ClearAllFields_UserInfo();
                else
                    UserCommon.MsbShow(Message.MSE_SQLADD, UserCommon.ERROR);
            }
            else
            {
                bResult = new UserInfoData().Update(objUserInfo);
                if (bResult)
                    UserCommon.MsbShow(Message.MSI_WCSave("User"), UserCommon.INFORMATION);
                else
                    UserCommon.MsbShow(Message.MSE_SQLEDIT, UserCommon.ERROR);
            }
        }

        private void ClearAllFields_UserInfo()
        {
            LoadcbbUGRPID(false);
            this.hiID.Value = "";
            this.txtUUsername.Text = "";
            this.txtUFullName.Text = "";
            UserCommon.SetValueControl(this.cbbPID, "0");
            this.txtUAddress.Text = "";
            this.txtUPhone.Text = "";
            this.txtUMobilePhone.Text = "";
            this.txtUEmail.Text = "";
            this.txtUNotes.Text = "";
            UserCommon.SetValueControl(this.cbbUGRPID, "0");
            chbuActive.Checked = true;
            //UserCommon.ReadOnlyControl(this.txtUUsername, false);
            this.txtUUsername.Focus();
        }
        private void ShowDetails_UserInfo(string UID)
        {
            //UserCommon.ReadOnlyControl(this.txtUUsername, true);
            LoadcbbUGRPID(true);
            DataTable dt = new UserInfoData().GetDataByID(UID);
            int i = 0;
            this.hiID.Value = dt.Rows[i][(string)UserInfoData.TBC_UID].ToString();
            this.txtUUsername.Text = dt.Rows[i][(string)UserInfoData.TBC_UUserName].ToString();
            this.txtUFullName.Text = dt.Rows[i][(string)UserInfoData.TBC_UFullName].ToString();
            UserCommon.SetValueControl(this.cbbPID, dt.Rows[i][(string)UserInfoData.TBC_PID].ToString());
            this.txtUAddress.Text = dt.Rows[i][(string)UserInfoData.TBC_UAddress].ToString();
            this.txtUPhone.Text = dt.Rows[i][(string)UserInfoData.TBC_UPhone].ToString();
            this.txtUMobilePhone.Text = dt.Rows[i][(string)UserInfoData.TBC_UMobilePhone].ToString();
            this.txtUEmail.Text = dt.Rows[i][(string)UserInfoData.TBC_UEmail].ToString();
            this.txtUNotes.Text = dt.Rows[i][(string)UserInfoData.TBC_UNotes].ToString();
            UserCommon.SetValueControl(this.cbbUGRPID, dt.Rows[i][(string)UserInfoData.TBC_UGRPID].ToString());
            this.chbuActive.Checked = UserCommon.ToBoolean(dt.Rows[i][(string)UserInfoData.TBC_UActive]);
        }
        private void LoadcbbUGRPID(bool IsAllData)
        {
            object Datas = (IsAllData) ? null : "true";
            DataTable dt = new UserGroupData().GetDataBy(Datas);
            this.cbbUGRPID.SelectedItems.Clear();
            Store store = this.cbbUGRPID.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbUGRPID, "0");
        }
        private void LoadcbbPID()
        {
            DataTable dt = new PositionData().GetDataBy();
            this.cbbPID.SelectedItems.Clear();
            Store store = this.cbbPID.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbPID, "0");
        }
        private UserInfoEntities GetUserInfo(ref bool Insert, ref string Exception)
        {
            UserInfoEntities res = new UserInfoEntities();
            int UID = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(UID);

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
            if (!UserCommon.HasValue(this.cbbPID))
            {
                Exception = Message.MSE_WCFieldRequired("Position");
                return null;
            }
            if (!UserCommon.HasValue(this.cbbUGRPID))
            {
                Exception = Message.MSE_WCFieldRequired("Group");
                return null;
            }
            res.UUserName = txtUUsername.Text.Trim();
            if (Insert)
            {
                string DefaultPassword = ConfigurationManager.AppSettings["PASSWORDDEFAULT"].ToString().Trim();
                res.UPassword = UserCommon.Encrypt(DefaultPassword);
            }
            res.UID = UID;
            //res.UFullName = UserCommon.ToUpperFisrtChar(txtUFullName.Text);
            res.UFullName = txtUFullName.Text;
            res.PID = UserCommon.ToInt(this.cbbPID.SelectedItem.Value);
            res.UAddress = txtUAddress.Text;
            res.UPhone = txtUPhone.Text;
            res.UMobilePhone = txtUMobilePhone.Text;
            res.UEmail = txtUEmail.Text;
            res.UNotes = txtUNotes.Text;
            res.UGRPID = UserCommon.ToInt(this.cbbUGRPID.SelectedItem.Value);
            res.UActive = UserCommon.ToInt(chbuActive.Checked);
            return res;
        }

        #endregion

        #region >- Change Password -<
        protected void btResetPass_Click(object sender, DirectEventArgs e)
        {
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            this.wReset.Show();
            this.txtNewPassword.Focus();
        }
        private bool ChangePassword(ref string Exception)
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
        protected void btChange_Click(object sender, DirectEventArgs e)
        {
            UserCommon.VerifyPage();
            if (!ChangePassword(ref _Exception))
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
            else
            {
                this.wReset.Hide();
                UserCommon.MsbShow(Message.MSI_UIChangePass, UserCommon.INFORMATION);
            }
        }
        protected void btCancelChange_Click(object sender, DirectEventArgs e)
        {
            this.wReset.Hide();
        }

        #endregion

    }
}