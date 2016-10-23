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
    public partial class AboutManager : System.Web.UI.Page
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
            UserCommon.ReadOnlyControl(txtAboutName, false);
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
                string[] Fields = new string[] {AboutData.TBC_AboutID, AboutData.TBC_AboutName, AboutData.TBC_AboutDetail };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtAboutName, true);
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
                    bResult = new AboutData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_AboutManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new AboutData().Search(Datas, Keyword);
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
            this.txtAboutName.Text = "";
            this.txtAboutDetail.Text = "";
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtAboutName.Text = Value[1];
            this.txtAboutDetail.Text = Value[2];
            
        }
        private AboutEntities GetAbout(ref bool Insert, ref string Exception)
        {
            AboutEntities res = new AboutEntities();
            int Aboutid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(Aboutid);
            if (!UserCommon.HasValue(this.txtAboutName))
            {
                Exception = Message.MSE_WCFieldRequired("About Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new AboutData().CheckExistAbout(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Serial");
                    return null;
                }
            }
            else {
                res.AboutID = int.Parse(hiID.Text);
            }
            res.AboutName = txtAboutName.Text.Trim();
            res.AboutDetail = txtAboutDetail.Text;
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            AboutEntities objAbout = new AboutEntities();
            objAbout = GetAbout(ref Insert, ref _Exception);
            if (objAbout == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new AboutData().Insert(ref objAbout);
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
                bResult = new AboutData().Update(objAbout);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("About"), UserCommon.INFORMATION);
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