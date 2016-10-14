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

namespace TMT.License.Web.TSSystem
{
    public partial class UserGroupDetails : System.Web.UI.Page
    {
        #region >- Page -<

        private static string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }
        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.SYSTEM_USERGROUP);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.SetTheme(UserCommon.GetCurrentTheme());
            btAdd.Disabled = !WebPermission.AddPermission(WebPermission.SYSTEM_USERGROUP);
            string UGRPID = UserCommon.GetValueParam_ID();
            if (UGRPID.Length > 0)
            {
                ShowDetails_UserGroup(UGRPID);
            }
            else
            {
                ClearAllFields_UserGroup();
            }
        }
        protected void btBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.FormatPageWithParameter(UserCommon.System_UserGroupManager));
        }
        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            int UGRPID = UserCommon.ToInt(this.hiID.Value);
            if (UGRPID > 0)
                Response.Redirect(UserCommon.FormatPageWithParameter(UserCommon.System_UserGroupDetails));
            else
                Response.Redirect(UserCommon.System_UserGroupDetails);
        }
        #endregion

        #region >- UserGroup -<
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            ClearAllFields_UserGroup();
        }
        protected void btSave_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            UserGroupEntities objUserGroup = new UserGroupEntities();
            objUserGroup = GetUserGroup(ref Insert, ref _Exception);
            if (objUserGroup == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }
            if (Insert)
            {
                bResult = new UserGroupData().Insert(ref objUserGroup);
                if (bResult)
                    ClearAllFields_UserGroup();
                else
                    UserCommon.MsbShow(Message.MSE_SQLADD, UserCommon.ERROR);
            }
            else
            {
                bResult = new UserGroupData().Update(objUserGroup);
                if (bResult)
                    UserCommon.MsbShow(Message.MSI_WCSave("User"), UserCommon.INFORMATION);
                else
                    UserCommon.MsbShow(Message.MSE_SQLEDIT, UserCommon.ERROR);
            }
        }

        private void ClearAllFields_UserGroup()
        {
            this.hiID.Value = "";
            this.txtUGRPName.Text = "";
            this.txtUGRPParent.Text = "";
            chbUGRPActive.Checked = true;
        }
        private void ShowDetails_UserGroup(string UGRPID)
        {
            DataTable dt = new UserGroupData().GetDataByID(UGRPID);
            int i = 0;
            this.hiID.Value = dt.Rows[i][(string)UserGroupData.TBC_UGRPID].ToString();
            this.txtUGRPName.Text = dt.Rows[i][(string)UserGroupData.TBC_UGRPName].ToString();
            this.txtUGRPParent.Text = dt.Rows[i][(string)UserGroupData.TBC_UGRPParent].ToString();
            this.chbUGRPActive.Checked = UserCommon.ToBoolean(dt.Rows[i][(string)UserGroupData.TBC_UGRPActive]);
        }
        private UserGroupEntities GetUserGroup(ref bool Insert, ref string Exception)
        {
            UserGroupEntities res = new UserGroupEntities();
            int UGRPID = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(UGRPID);

            int CookieID = UserCommon.ToInt(UserCommon.GetCookie_UID());
            res.UGRPID = UGRPID;
            res.UGRPName = txtUGRPName.Text;
            res.UGRPParent = "<1>";
            res.UGRPActive = UserCommon.ToInt(this.chbUGRPActive.Checked);
            //res.UGRPParent = txtUGRPParent.Text;
            //res.UGRPCreatedBy = CookieID;
            //res.UGRPCreatedD = UserCommon.GetDateTime();
            //res.UGRPLastUpdatedBy = CookieID;
            //res.UGRPLastUpdatedD = UserCommon.GetDateTime();
            return res;
        }

        #endregion


    }
}