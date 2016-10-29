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
    public partial class ArchManager : System.Web.UI.Page
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
            LoadcbbArchType();
        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            UserCommon.ReadOnlyControl(txtArchName, false);
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
                string[] Fields = new string[] { ArchData.TBC_ArchID, ArchData.TBC_ArchName, ArchData.TBC_ArchDetail };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtArchName, true);
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
                    bResult = new ArchData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_ArchManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new ArchData().Search(Datas, Keyword);
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
            this.txtArchName.Text = "";
            this.txtArchThump.Text = "";
            this.txtArchDesp.Text = "";
            this.edArchDetail.Text = "";

        }
        private void ShowDetails_Details(string[] Value)
        {
            string id = Value[0];


            DataTable dt = new ArchData().GetDataByID(id);
            ArchEntities tmp = new ArchEntities();
            tmp.ArchID = int.Parse(dt.Rows[0][ArchData.TBC_ArchID].ToString());
            tmp.ArchName = dt.Rows[0][ArchData.TBC_ArchName].ToString();
            tmp.ArchDetail = dt.Rows[0][ArchData.TBC_ArchDetail].ToString();
            tmp.ArchTypeName = dt.Rows[0][ArchData.TBC_ArchTypeName].ToString();
            tmp.ArchThump = dt.Rows[0][ArchData.TBC_ArchThump].ToString();
            tmp.ArchTypeID = int.Parse(dt.Rows[0][ArchData.TBC_ArchTypeID].ToString());
            tmp.ArchDesp = dt.Rows[0][ArchData.TBC_ArchDesp].ToString();
            hiID.Text = tmp.ArchID.ToString();
            txtArchName.Text = tmp.ArchName;
            txtArchDesp.Text = tmp.ArchDesp;
            txtArchThump.Text = tmp.ArchThump;
            edArchDetail.Text = tmp.ArchDetail;
            UserCommon.SetValueControl(this.cbbArchType, tmp.ArchTypeID.ToString());
            
        }
        private ArchEntities GetArch(ref bool Insert, ref string Exception)
        {
            ArchEntities res = new ArchEntities();
            if (cbbArchType.SelectedItem.Value == null)
            {
                res.ArchTypeID = 1;
            }
            else res.ArchTypeID = int.Parse(cbbArchType.SelectedItem.Value);
        
            int Archid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(Archid);
            if (!UserCommon.HasValue(this.txtArchName))
            {
                Exception = Message.MSE_WCFieldRequired("Arch Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new ArchData().CheckExistArch(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Arch");
                    return null;
                }
            }
            else
            {
                res.ArchID = int.Parse(hiID.Text);
            }
            res.ArchName = txtArchName.Text.Trim();
            res.ArchDetail = edArchDetail.Text;
            res.ArchThump = txtArchThump.Text;
            res.ArchDesp = txtArchDesp.Text;
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            ArchEntities objArch = new ArchEntities();
            objArch = GetArch(ref Insert, ref _Exception);
            if (objArch == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new ArchData().Insert(ref objArch);
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
                bResult = new ArchData().Update(objArch);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("Arch"), UserCommon.INFORMATION);
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
        private void LoadcbbArchType()
        {
            object[] Datas = null;
            DataTable dt = new ArchTypeData().Search(Datas, "");
            this.cbbArchType.SelectedItems.Clear();

            Store store = this.cbbArchType.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbArchType, "0");
        }
    }
}