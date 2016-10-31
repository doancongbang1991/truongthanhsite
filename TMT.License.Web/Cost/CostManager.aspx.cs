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
    public partial class CostManager : System.Web.UI.Page
    {
        #region >- Page -<

        private static string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
        }
        private void LoadConfig()
        {

            DataTable dt = new CostConfigData().Search(null, "");
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                switch (dt.Rows[r][(string)CostConfigData.TBC_CostTag].ToString())
                {
                    case "Location":
                        {
                            TextField txt = AddTextField(dt.Rows[r]);
                            fieldsetLocation.Add(txt);
                            break;
                        }
                    case "Type":
                        {
                            TextField txt = AddTextField(dt.Rows[r]);
                            fieldsetType.Add(txt);
                            break;
                            
                        }
                    case "Status":
                        {
                            TextField txt = AddTextField(dt.Rows[r]);
                            fieldsetStatus.Add(txt);
                            break;
                        }
                    case "BasePrice":
                        {
                            TextField txt = AddTextField(dt.Rows[r]);
                            fieldsetBasePrice.Add(txt);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private TextField AddTextField(DataRow dtrow)
        {
            Ext.Net.TextField txt = new Ext.Net.TextField();
            txt.ID = "Cost" + dtrow[(string)CostConfigData.TBC_CostID].ToString();
            txt.Text = dtrow[(string)CostConfigData.TBC_CostDetail].ToString();
            txt.FieldLabel = dtrow[(string)CostConfigData.TBC_CostName].ToString();
            txt.MarginSpec = "1 0 0 1";
            return txt;
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
            LoadConfig();
        }

        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {

            Response.Redirect(UserCommon.TT_CostManager);
        }
        #endregion

        #region >- UserInfo -<

        protected void btSave_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            UserInfoEntities objUserInfo = new UserInfoEntities();
            objUserInfo = null; //GetUserInfo(ref Insert, ref _Exception);
            if (objUserInfo == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }
            if (Insert)
            {
                bResult = new UserInfoData().Insert(ref objUserInfo);
                if (bResult)
                    UserCommon.MsbShow("Succeed", UserCommon.INFORMATION);
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


        //private UserInfoEntities GetUserInfo(ref bool Insert, ref string Exception)
        //{
        //    UserInfoEntities res = new UserInfoEntities();
        //    int UID = UserCommon.ToInt(this.hiID.Value);
        //    Insert = !UserCommon.ToBoolean(UID);

        //    if (!UserCommon.HasValue(this.txtUUsername))
        //    {
        //        Exception = Message.MSE_WCFieldRequired("UserName");
        //        return null;
        //    }
        //    if (this.txtUUsername.Text.Trim().Contains(";"))
        //    {
        //        Exception = Message.MSE_WCFieldNotVaild("UserName");
        //        return null;
        //    }
        //    if (Insert)
        //    {
        //        bool bExist = new UserInfoData().CheckExistUUserName(this.txtUUsername.Text);
        //        if (bExist)
        //        {
        //            Exception = Message.MSE_WCFieldExist("UserName");
        //            return null;
        //        }
        //    }


        //    res.UUserName = txtUUsername.Text.Trim();
        //    if (Insert)
        //    {
        //        string DefaultPassword = ConfigurationManager.AppSettings["PASSWORDDEFAULT"].ToString().Trim();
        //        res.UPassword = UserCommon.Encrypt(DefaultPassword);
        //    }
        //    res.UID = UID;
        //    //res.UFullName = UserCommon.ToUpperFisrtChar(txtUFullName.Text);
        //    res.UFullName = txtUFullName.Text;
        //    res.PID = UserCommon.ToInt(this.cbbPID.SelectedItem.Value);
        //    res.UAddress = txtUAddress.Text;
        //    res.UPhone = txtUPhone.Text;
        //    res.UMobilePhone = txtUMobilePhone.Text;
        //    res.UEmail = txtUEmail.Text;
        //    res.UNotes = txtUNotes.Text;
        //    res.UGRPID = UserCommon.ToInt(this.cbbUGRPID.SelectedItem.Value);
        //    res.UActive = UserCommon.ToInt(chbuActive.Checked);
        //    return res;
        //}

        #endregion



    }
}