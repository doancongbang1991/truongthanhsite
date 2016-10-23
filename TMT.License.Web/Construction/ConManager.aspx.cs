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
    public partial class ConManager : System.Web.UI.Page
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
            LoadcbbPID();
        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            UserCommon.ReadOnlyControl(txtConName, false);
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
                string[] Fields = new string[] { ConstructionData.TBC_ConID, ConstructionData.TBC_ConName, ConstructionData.TBC_ConDetail, ConstructionData.TBC_ConTypeID, ConstructionData.TBC_ConImg};
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtConName, true);
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
                    bResult = new ConstructionData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_ConstructionManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new ConstructionData().Search(Datas, Keyword);
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
            this.txtConName.Text = "";
            this.txtConDetail.Text = "";
            this.txtConImg.Text = "";
            UserCommon.SetValueControl(this.cbbConType, "0");
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtConName.Text = Value[1];
            this.txtConDetail.Text = Value[2];
            UserCommon.SetValueControl(this.cbbConType, Value[3]);
            this.txtConImg.Text = Value[4];
        }
        private ConstructionEntities GetCon(ref bool Insert, ref string Exception)
        {
            ConstructionEntities res = new ConstructionEntities();
            if (cbbConType.SelectedItem.Value == null)
	        {
		         res.ConTypeID = 1;
	        }
            else res.ConTypeID = int.Parse(cbbConType.SelectedItem.Value);
        
            int conid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(conid);
            if (!UserCommon.HasValue(this.txtConName))
            {
                Exception = Message.MSE_WCFieldRequired("Construction Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new ConstructionData().CheckExistAbout(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Construction");
                    return null;
                }
            }
            else {
                res.ConID = int.Parse(hiID.Text);
            }
            res.ConName = txtConName.Text.Trim();
            res.ConDetail = txtConDetail.Text;
            res.ConImg = txtConImg.Text;
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            ConstructionEntities objCon = new ConstructionEntities();
            objCon = GetCon(ref Insert, ref _Exception);
            if (objCon == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new ConstructionData().Insert(ref objCon);
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
                bResult = new ConstructionData().Update(objCon);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("Construction"), UserCommon.INFORMATION);
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
        private void LoadcbbPID()
        {
            object[] Datas = null;
            DataTable dt = new ConTypeData().Search(Datas ,"");
            this.cbbConType.SelectedItems.Clear();
            
            Store store = this.cbbConType.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbConType, "0");
        }
    }
}