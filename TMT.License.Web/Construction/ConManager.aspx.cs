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
            LoadcbProduct();
        }

        private void FormatWebControl()
        {
            this.colLAppDate.Format = UserCommon.DateTimeFormat;
            this.colLRegDate.Format = UserCommon.DateTimeFormat;
        }
        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.SYSTEM_USERGROUP);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.SetTheme(UserCommon.GetCurrentTheme());
            FormatWebControl();
            btAdd.Disabled = !WebPermission.AddPermission(WebPermission.SYSTEM_USERGROUP);
            btEdit.Disabled = !WebPermission.EditPermission(WebPermission.SYSTEM_USERGROUP);
            btDel.Disabled = !WebPermission.DeletePermission(WebPermission.SYSTEM_USERGROUP);
            string[] sKeyword = UserCommon.GetValueParam_KeyWord();
            if (sKeyword != null)
            {
                this.txtKeyword.Text = sKeyword[0];
            }
            LoadGrid_Position();

            Menu_Load();
        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            btApprove.Text = "OK";
            ClearAllFields_Details();
            txtLicKey.Hidden = true;
            UserCommon.ReadOnlyControl(txtSerial, false);
            UserCommon.ReadOnlyControl(txtDomain, false);
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
            btApprove.Text = "Approve";
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grPosition, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                string json = e.ExtraParams["grPosition_Select_Values"];
                string[] Fields = new string[] { LicenseData.TBC_LicID, LicenseData.TBC_LicSerial, LicenseData.TBC_LicKey, LicenseData.TBC_LicDes, LicenseData.TBC_LicStatus, LicenseData.TBC_LicProduct, LicenseData.TBC_LicDomain };
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
        protected void btDel_Click(object sender, DirectEventArgs e)
        {
            object[] oRecordID = UserCommon.GetRecordIDInGridPanel(this.grPosition, true);
            if (oRecordID == null)
                UserCommon.MsbShow(Message.MSE_WCSelectRowRequired, UserCommon.ERROR);
            else
            {
                string json = e.ExtraParams["grPosition_Select_Values"];
                string[] Fields = new string[] { LicenseData.TBC_LicID, LicenseData.TBC_LicStatus };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                if ((value[1] == "Approved") || (value[1] == "Actived") || (value[1] != "Pending"))
                {
                    UserCommon.MsbShow(Message.MSE_WCDeleteApprove, UserCommon.ERROR);
                }
                else
                {
                    bool bResult = false;
                    for (int i = 0; i < oRecordID.Length; i++)
                    {
                        bResult = new LicenseData().Delete(oRecordID[i].ToString());
                        if (!bResult)
                            break;
                    }
                    LoadGrid_Position();
                    if (!bResult)
                        UserCommon.MsbShow(Message.MSE_WCNoDelete, UserCommon.ERROR);
                }
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
            DataTable dt = new LicenseData().Search(Datas, Keyword);
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
            this.txtStatus.Text = "";
            this.txtDomain.Text = "";
            UserCommon.SetValueControl(cbbProduct, "0");
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtSerial.Text = Value[1];
            this.txtLicKey.Text = Value[2];
            this.txtDes.Text = Value[3];
            this.txtStatus.Text = Value[4];
            UserCommon.SetValueControl(this.cbbProduct, Value[5]);
            this.txtDomain.Text = Value[6];
            //this.cbbProduct.Value = Value[5];
            //this.txtDomain.Text = Value[6];
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
            res.LicSerial = txtSerial.Text.Trim();
            res.LicKey = "************************************";
            res.LicDes = txtDes.Text.Trim().ToUpper();
            res.LicStatus = "Pending";
            res.LUID = UserCommon.GetCookie_UID();
            res.LRegDate = UserCommon.GetDateTime();
            res.LAppDate = UserCommon.GetDateTime();
            return res;
        }
        protected void btNew_Click(object sender, DirectEventArgs e)
        {
            UserCommon.ReadOnlyControl(txtSerial, false);
            UserCommon.ReadOnlyControl(txtDomain, false);
            UserCommon.ReadOnlyControl(cbbProduct, false);
            txtLicKey.Hidden = true;
            ClearAllFields_Details();
            btApprove.Text = "OK";
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
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

                if (txtStatus.Text == "Approved")
                {
                    UserCommon.MsbShow("This serial is approved", UserCommon.ERROR);
                }
                else
                {
                    string id = txtSerial.Text.Trim();
                    string[] ids = id.Split('-');
                    string source = ids[0].ToLower() + ids[1].ToLower() + ids[2].ToLower() + ids[3].ToLower() + txtDomain.Text.Trim();
                    using (SHA1 shaHash = SHA1.Create())
                    {
                        string hash = GetSHAHash(shaHash, source);

                        hash = hash.ToUpper();
                        string hash1 = hash.Substring(0, 8) + "-";
                        string hash2 = hash.Substring(8, 8) + "-";
                        string hash3 = hash.Substring(16, 8) + "-";
                        string hash4 = hash.Substring(24, 8) + "-";
                        string hash5 = hash.Substring(32, 8);
                        hash = hash1 + hash2 + hash3 + hash4 + hash5;
                        objLicense.LicStatus = "Approved";
                        objLicense.LicKey = hash;
                        txtLicKey.Text = hash;
                    }
                    bResult = new LicenseData().Update(objLicense);
                    if (bResult)
                    {
                        LoadGrid_Position();
                        this.winDetails.Hide();
                    }
                    else
                        UserCommon.MsbShow(Message.MSE_SQLEDIT, UserCommon.ERROR);
                }
            }
        }
        protected void btCancel_Click(object sender, DirectEventArgs e)
        {
            this.winDetails.Hide();
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

        static string GetSHAHash(SHA1 shaHash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = shaHash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
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
        protected void Menu_Load()
        {
            Store store = this.Menu.GetStore();
            var objs = new List<object> { };
            foreach (AbstractComponent Component in ctl340.Items)
            {
                if (Component.GetType().ToString() == "Ext.Net.Button")
                {
                    Ext.Net.Button bttmp = (Ext.Net.Button)Component;
                    object[] obj = new object[] { ResourceManager.GetIconClassName(bttmp.Icon), bttmp.Text };
                    this.ResourceManager1.RegisterIcon(bttmp.Icon);
                    objs.Add(obj);
                }
            }
            store.DataSource = objs;
            store.DataBind();
        }
       
    }
}