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
    public partial class UserInfoManager : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }

        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.SYSTEM_USER);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.SetTheme(UserCommon.GetCurrentTheme());
            btAdd.Disabled = !WebPermission.AddPermission(WebPermission.SYSTEM_USER);
            bool rEdit = WebPermission.EditPermission(WebPermission.SYSTEM_USER);
            if (rEdit)
                this.btEdit.Text = "Edit";
            else
                this.btEdit.Text = "Details";
            btDel.Disabled = !WebPermission.DeletePermission(WebPermission.SYSTEM_USER);
            LoadcbbUGRPID(true);
            string[] sKeyword = UserCommon.GetValueParam_KeyWord();
            if (sKeyword != null)
            {
                UserCommon.SetValueControl(this.cbbUGRPID, sKeyword[0]);
                this.txtKeyword.Text = sKeyword[1];
            }
            LoadGrid_UserInfo();

        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.System_UserInfoDetails, true);
        }
        protected void btEdit_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grUserInfo, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                int UGRPID = UserCommon.ToInt(this.cbbUGRPID.SelectedItem.Value);
                string Keyword = UserCommon.FormatKeyword(new object[] { UGRPID, this.txtKeyword.Text.Trim() });
                string RedirectPage = UserCommon.FormatDetailsPage(UserCommon.System_UserInfoDetails, oRecordID[0].ToString(), Keyword);
                Response.Redirect(RedirectPage, true);
            }
        }
        protected void btDel_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grUserInfo, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                bool bResult = false;
                for (int i = 0; i < oRecordID.Length; i++)
                {
                    bResult = new UserInfoData().Delete(oRecordID[i].ToString());
                    if (!bResult)
                        break;
                }
                LoadGrid_UserInfo();
                if (!bResult)
                    UserCommon.MsbShow(Message.MSE_WCNoDelete, UserCommon.ERROR);
            }
        }
        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.System_UserInfoManager, true);
        }

        protected void cbbUGRPID_Select(object sender, DirectEventArgs e)
        {
            LoadGrid_UserInfo();
        }
        private void LoadcbbUGRPID(bool IsAllData)
        {
            object Datas = (IsAllData) ? null : "true";
            DataTable dt = new UserGroupData().GetDataBy(Datas);
            this.cbbUGRPID.SelectedItems.Clear();
            this.stcbbUGRPID.DataSource = dt;
            this.stcbbUGRPID.DataBind();
            UserCommon.AddItemFilterInCombobox(this.cbbUGRPID, this.stcbbUGRPID);
            UserCommon.SetValueControl(this.cbbUGRPID, "0");
        }
        private void LoadGrid_UserInfo()
        {
            this.RowSelectionModelUserInfo.ClearSelection();
            this.grUserInfo.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            int UGRPID = UserCommon.ToInt(this.cbbUGRPID.SelectedItem.Value);
            object[] Datas = new object[] { UGRPID };
            DataTable dt = new UserInfoData().Search(Datas, Keyword);
            this.stUserInfo.DataSource = dt;
            this.stUserInfo.DataBind();
        }
        [DirectMethod(Namespace = "CompanyX")]
        public void Filter()
        {
            LoadGrid_UserInfo();

        }

    }
}