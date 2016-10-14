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

namespace TMT.License.Web.License
{
    public partial class Footer : System.Web.UI.Page
    {
        private string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                LoadPage();
            LoadcbProduct();
        }

        private void FormatWebControl()
        {
            this.colLAppDate.Format = UserCommon.DateTimeFormat;
            this.colLRegDate.Format = UserCommon.DateTimeFormat;
        }
        protected void LoadcbProduct()
        {
            DataTable dt = new ProductData().GetDataBy();
            this.cbbProduct.SelectedItems.Clear();
            this.stcbbPID.DataSource = dt;
            this.stcbbPID.DataBind();
            UserCommon.AddItemOptionInCombobox(this.cbbProduct, this.stcbbPID);
            UserCommon.SetValueControl(this.cbbProduct, "0");
        }
        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.TIMESHEET_TIMESHEET);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.SetTheme(UserCommon.GetCurrentTheme());
            FormatWebControl();
            //btAdd.Disabled = !WebPermission.AddPermission(WebPermission.TIMESHEET_TIMESHEET);
            //btEdit.Disabled = !WebPermission.EditPermission(WebPermission.TIMESHEET_TIMESHEET);
            string[] sKeyword = UserCommon.GetValueParam_KeyWord();
            if (sKeyword != null)
            {
                this.txtKeyword.Text = sKeyword[0];
            }
            LoadGrid_Position();

        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            ClearAllFields_Details();
            txtLicKey.Hidden = true;
            UserCommon.ReadOnlyControl(txtSerial, false);
            UserCommon.ReadOnlyControl(txtDomain,false);
            UserCommon.ReadOnlyControl(cbbProduct, false);
            this.winDetails.Show();
        }
        protected void btEdit_Click(object sender, DirectEventArgs e)
        {
            //if (!WebPermission.EditPermission(WebPermission.BASICDATA_TASKTYPE))
            //{
            //    UserCommon.MsbShow(Message.MSE_RGNoPermissionEdit, UserCommon.ERROR);
            //    return;
            //}
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grPosition, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                string json = e.ExtraParams["grPosition_Select_Values"];
                string[] Fields = new string[] { LicenseData.TBC_LicID, LicenseData.TBC_LicSerial, LicenseData.TBC_LicKey, LicenseData.TBC_LicDes, LicenseData.TBC_LicProduct, LicenseData.TBC_LicDomain };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtSerial, true);
                UserCommon.ReadOnlyControl(txtLicKey, true);
                UserCommon.ReadOnlyControl(txtDomain, true);
                UserCommon.ReadOnlyControl(cbbProduct, true); 
                txtLicKey.Hidden = false;
                ShowDetails_Details(value);
                this.winDetails.Show();
            }
        }

        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(UserCommon.TT_Footer, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new LicenseData().Search1(Datas, Keyword, UserCommon.GetCookie_UID());
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
            this.txtSerial.Text = "";
            this.txtLicKey.Text = "";
            this.txtDes.Text = "";
            this.txtDomain.Text = "";
            UserCommon.SetValueControl(cbbProduct, "0");
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtSerial.Text = Value[1];
            this.txtLicKey.Text = Value[2];
            this.txtDes.Text = Value[3];
            UserCommon.SetValueControl(cbbProduct,Value[4]);
            this.txtDomain.Text = Value[5];
        }
        private LicenseEntities GetLicense(ref bool Insert, ref string Exception)
        {
            LicenseEntities res = new LicenseEntities();
            int LicID = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(LicID);
            if (!UserCommon.HasValue(this.txtSerial))
            {
                Exception = Message.MSE_WCFieldRequired("Serial");
                return null;
            }
            bool valid = checkvalidSerial(txtSerial.Text.Trim());
            if (!valid)
            {
                Exception = Message.MSE_WCNovalid("Serial");
                return null;
            }
            if (Insert)
            {
                bool bExist = new LicenseData().CheckExistLicSerial(this.txtSerial.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Serial");
                    return null;
                }
            }
            res.LicID = LicID;
            res.LicSerial = txtSerial.Text.Trim().ToUpper();
            res.LicKey = "************************************";
            res.LicDes = txtDes.Text.Trim();
            res.LicStatus = "Pending";
            res.LUID = UserCommon.GetCookie_UID();
            res.LRegDate = UserCommon.GetDateTime();
            return res;
        }
        protected void btNew_Click(object sender, DirectEventArgs e)
        {

            UserCommon.ReadOnlyControl(txtSerial, false);
            UserCommon.ReadOnlyControl(txtDomain, false);
            UserCommon.ReadOnlyControl(cbbProduct, false);
            txtLicKey.Hidden = true;
            ClearAllFields_Details();
        }
        protected void btOK_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            LicenseEntities objLicense = new LicenseEntities();
            objLicense = GetLicense(ref Insert, ref _Exception);
            if (objLicense == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }
            if (Insert)
            {
                bResult = new LicenseData().Insert(ref objLicense);
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
                bResult = new LicenseData().UpdateClient(objLicense);
                if (bResult)
                {
                    LoadGrid_Position();
                    this.winDetails.Hide();
                }
                else
                    UserCommon.MsbShow(Message.MSE_SQLEDIT, UserCommon.ERROR);
            }
        }
        protected void btCancel_Click(object sender, DirectEventArgs e)
        {
            this.winDetails.Hide();
        }
        private bool checkvalidSerial(string hash)
        {
            bool res = false;
            try
            {
                string[] ids = hash.Split('-');
                if (ids.Length == 4)
                    res = true;
                else res = false;

            }
            catch
            {
                res = false;
            }
            return res;
        }
    }
}