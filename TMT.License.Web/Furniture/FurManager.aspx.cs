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
    public partial class FurManager : System.Web.UI.Page
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
            UserCommon.ReadOnlyControl(txtFurName, false);
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
                string[] Fields = new string[] { FurnitureData.TBC_FurID, FurnitureData.TBC_FurName, FurnitureData.TBC_FurDetail, FurnitureData.TBC_FurTypeID, FurnitureData.TBC_FurImg };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtFurName, true);
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
                    bResult = new FurnitureData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_FurManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new FurnitureData().Search(Datas, Keyword);
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
            this.txtFurName.Text = "";
            this.txtFurDetail.Text = "";
            this.txtFurImg.Text = "";
            UserCommon.SetValueControl(this.cbbFurType, "0");
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtFurName.Text = Value[1];
            this.txtFurDetail.Text = Value[2];
            UserCommon.SetValueControl(this.cbbFurType, Value[3]);
            this.txtFurImg.Text = Value[4];
        }
        private FurnitureEntities GetFur(ref bool Insert, ref string Exception)
        {
            FurnitureEntities res = new FurnitureEntities();
            if (cbbFurType.SelectedItem.Value == null)
	        {
                res.FurTypeID = 1;
	        }
            else res.FurTypeID = int.Parse(cbbFurType.SelectedItem.Value);
        
            int furid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(furid);
            if (!UserCommon.HasValue(this.txtFurName))
            {
                Exception = Message.MSE_WCFieldRequired("Furniture Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new FurnitureData().CheckExistAbout(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Furniture");
                    return null;
                }
            }
            else {
                res.FurID = int.Parse(hiID.Text);
            }
            res.FurName = txtFurName.Text.Trim();
            res.FurDetail = txtFurDetail.Text;
            res.FurImg = txtFurImg.Text;
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            FurnitureEntities objFur = new FurnitureEntities();
            objFur = GetFur(ref Insert, ref _Exception);
            if (objFur == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new FurnitureData().Insert(ref objFur);
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
                bResult = new FurnitureData().Update(objFur);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("Furniture"), UserCommon.INFORMATION);
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
            DataTable dt = new FurnitureTypeData().Search(Datas ,"");
            this.cbbFurType.SelectedItems.Clear();
            
            Store store = this.cbbFurType.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbFurType, "0");
        }
    }
}