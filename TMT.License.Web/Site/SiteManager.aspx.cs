using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using Entities;
using Ext.Net;
using TMT.License.Core;

namespace TMT.License.Web.License
{
    public partial class SiteManager : System.Web.UI.Page
    {
        private string _Exception;
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
            ResourceManager1.Theme = UserCommon.GetCurrentTheme();
            btAdd.Disabled = !WebPermission.AddPermission(WebPermission.SYSTEM_USERGROUP);
            btEdit.Disabled = !WebPermission.EditPermission(WebPermission.SYSTEM_USERGROUP);
            btDel.Disabled = !WebPermission.DeletePermission(WebPermission.SYSTEM_USERGROUP);
            string[] sKeyword = UserCommon.GetValueParam_KeyWord();
            if (sKeyword != null)
            {
                this.txtKeyword.Text = sKeyword[0];
            }
            LoadGrid_Position();

        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            UserCommon.ReadOnlyControl(txtSiteName, false);
            ClearAllFields_Details();
            this.winDetails.Show();
        }
        protected void btEdit_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grPosition, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                string json = e.ExtraParams["grPosition_Select_Values"];
                string[] Fields = new string[] { SiteData.TBC_SiteID, SiteData.TBC_SiteName, SiteData.TBC_SiteNameVi, SiteData.TBC_SiteDetail, SiteData.TBC_SiteDesp, SiteData.TBC_SiteLink, SiteData.TBC_SiteOrder, SiteData.TBC_SiteHidden };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtSiteName, true);
                ShowDetails_Details(value);
                this.winDetails.Show();
            }
        }
        protected void btDel_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grPosition, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                bool bResult = false;
                for (int i = 0; i < oRecordID.Length; i++)
                {
                    bResult = new SiteData().Delete(oRecordID[i].ToString());
                    if (!bResult)
                        break;
                }
                LoadGrid_Position();
                if (!bResult)
                    UserCommon.MsbShow(Message.MSE_WCNoDelete, UserCommon.ERROR);
            }
        }
        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.TT_SiteManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new SiteData().Search(Datas, Keyword);
            this.stPosition.DataSource = dt;
            this.stPosition.DataBind();
        }
        [DirectMethod(Namespace = "CompanyX")]
        public void Filter()
        {
            LoadGrid_Position();

        }


        private void ClearAllFields_Details()
        {
            this.hiID.Value = "";
            this.txtSiteName.Text = "";
            this.txtSiteNameVi.Text = "";
            this.txtSiteDetail.Text = "";
            this.txtSiteDesp.Text = "";
            this.txtSiteLink.Text = "";
            this.numSiteOrder.Text = "0";
            rHiddenFalse.Checked = true;

        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtSiteName.Text = Value[1];
            this.txtSiteNameVi.Text = Value[2];
            this.txtSiteDetail.Text = Value[3];
            this.txtSiteDesp.Text = Value[4];
            this.txtSiteLink.Text = Value[5];
            this.numSiteOrder.Text = Value[6];
            string hidden = Value[7];
            if (hidden == "true")
            {
                rHiddenTrue.Checked = true;
            }
            else
            {
                rHiddenFalse.Checked = true;
            }
        }
        private SiteEntities GetArch(ref bool Insert, ref string Exception)
        {
            SiteEntities res = new SiteEntities();
            int siteid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(siteid);
            if (!UserCommon.HasValue(this.txtSiteName))
            {
                Exception = Message.MSE_WCFieldRequired("Site Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new SiteData().CheckExistSite(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Site");
                    return null;
                }
            }
            else {
                res.SiteID = int.Parse(hiID.Text);
            }
            res.SiteName = txtSiteName.Text.Trim();
            res.SiteNameVi = txtSiteNameVi.Text.Trim();
            res.SiteDetail = txtSiteDetail.Text;
            res.SiteDesp = txtSiteDesp.Text.Trim();
            res.SiteLink = txtSiteLink.Text;
            res.SiteOrder = int.Parse(numSiteOrder.Text);
            if (rHiddenTrue.Checked)
            {
                res.SiteHidden = true;

            }
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            SiteEntities objArch = new SiteEntities();
            objArch = GetArch(ref Insert, ref _Exception);
            if (objArch == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new SiteData().Insert(ref objArch);
                if (bResult)
                {
                    LoadGrid_Position();
                    ClearAllFields_Details();
                    this.winDetails.Hide();
                }
                else
                    UserCommon.MsbShow(Message.MSE_SQLADD, UserCommon.ERROR);
            }
            else
            {
                bResult = new SiteData().Update(objArch);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("Site"), UserCommon.INFORMATION);
                    this.winDetails.Hide();
                    LoadGrid_Position();
                    ClearAllFields_Details();
                }

                else
                    UserCommon.MsbShow(Message.MSE_SQLEDIT, UserCommon.ERROR);
            }
        }
        protected void btCancel_Click(object sender, DirectEventArgs e)
        {
            this.winDetails.Hide();
        }

    }
}