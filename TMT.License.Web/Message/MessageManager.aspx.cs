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
    public partial class MessageManager : System.Web.UI.Page
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
            
            btEdit.Disabled = !WebPermission.EditPermission(WebPermission.SYSTEM_USERGROUP);
            btDel.Disabled = !WebPermission.DeletePermission(WebPermission.SYSTEM_USERGROUP);
            string[] sKeyword = UserCommon.GetValueParam_KeyWord();
            if (sKeyword != null)
            {
                this.txtKeyword.Text = sKeyword[0];
            }
            LoadGrid_Position();
            
        }
        
        protected void btEdit_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grPosition, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                string json = e.ExtraParams["grPosition_Select_Values"];
                string[] Fields = new string[] { MessageData.TBC_MessID, MessageData.TBC_MessName, MessageData.TBC_MessYear, MessageData.TBC_MessMail, MessageData.TBC_MessGen, MessageData.TBC_MessPhone, MessageData.TBC_MessBody };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                
                
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
                    bResult = new MessageData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_MessManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new MessageData().Search(Datas, Keyword);
            this.stPosition.DataSource = dt;
            this.stPosition.DataBind();
        }
        [DirectMethod(Namespace = "CompanyX")]
        public void Filter()
        {
            LoadGrid_Position();

        }


       
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];

            txtMessName.Text = Value[1];
            txtMessYear.Text = Value[2];
            txtMessMail.Text = Value[3];
            txtMessPhone.Text = Value[5];
            txtMessBody.Text = Value[6];
        }
        private MessageEntities GetMess(ref bool Insert, ref string Exception)
        {
            MessageEntities res = new MessageEntities();


            int messid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(messid);
            if (!UserCommon.HasValue(this.txtMessName))
            {
                Exception = Message.MSE_WCFieldRequired("Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new MessageData().CheckExistAbout(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Message");
                    return null;
                }
            }
            else
            {
                res.MessID = int.Parse(hiID.Text);
            }
            res.MessName = txtMessName.Text.Trim();
            res.MessYear = txtMessYear.Text;
            res.MessMail = txtMessMail.Text;
            res.MessRead = true;
            res.MessPhone = txtMessPhone.Text;
            res.MessBody = txtMessBody.Text;
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            MessageEntities objProject = new MessageEntities();
            objProject = GetMess(ref Insert, ref _Exception);
            if (objProject == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new MessageData().Insert(ref objProject);
                if (bResult)
                {
                    LoadGrid_Position();
                    
                    this.winDetails.Hide();
                }
                else
                    UserCommon.MsbShow(Message.MSE_SQLADD, UserCommon.ERROR);
            }
            else
            {
                bResult = new MessageData().Update(objProject);
                if (bResult)
                {
                    
                    this.winDetails.Hide();
                    LoadGrid_Position();
                    
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