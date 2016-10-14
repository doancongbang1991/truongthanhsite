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
    public partial class UserGroupManager : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }

        //private void FormatWebControl()
        //{
        //this.colUGRPCreatedD.Format = UserCommon.DateTimeFormat;
        //this.colUGRPLastUpdatedD.Format = UserCommon.DateTimeFormat;
        //}
        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.SYSTEM_USERGROUP);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.SetTheme(UserCommon.GetCurrentTheme());
            //FormatWebControl();
            btAdd.Disabled = !WebPermission.AddPermission(WebPermission.SYSTEM_USERGROUP);
            btEdit.Disabled = !WebPermission.EditPermission(WebPermission.SYSTEM_USERGROUP);
            btDel.Disabled = !WebPermission.DeletePermission(WebPermission.SYSTEM_USERGROUP);
            string[] sKeyword = UserCommon.GetValueParam_KeyWord();
            if (sKeyword != null)
            {
                this.txtKeyword.Text = sKeyword[0];
            }
            LoadGrid_UserGroup();

        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.System_UserGroupDetails, true);
        }
        protected void btEdit_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grUserGroup, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                string Keyword = UserCommon.FormatKeyword(new object[] { this.txtKeyword.Text.Trim() });
                string RedirectPage = UserCommon.FormatDetailsPage(UserCommon.System_UserGroupDetails, oRecordID[0].ToString(), Keyword);
                Response.Redirect(RedirectPage, true);
            }
        }
        protected void btDel_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grUserGroup, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                bool bResult = false;
                for (int i = 0; i < oRecordID.Length; i++)
                {
                    bResult = new UserGroupData().Delete(oRecordID[i].ToString());
                    if (!bResult)
                        break;
                }
                LoadGrid_UserGroup();
                if (!bResult)
                    UserCommon.MsbShow(Message.MSE_WCNoDelete, UserCommon.ERROR);
            }
        }
        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.System_UserGroupManager, true);
        }

        private void LoadGrid_UserGroup()
        {
            this.RowSelectionModelUserGroup.ClearSelection();
            this.grUserGroup.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = new object[] { };
            DataTable dt = new UserGroupData().Search(Datas, Keyword);
            this.stUserGroup.DataSource = dt;
            this.stUserGroup.DataBind();
        }
        [DirectMethod(Namespace = "CompanyX")]
        public void Filter()
        {
            LoadGrid_UserGroup();

        }

    }
}