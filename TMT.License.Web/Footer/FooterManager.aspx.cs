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
    public partial class FooterManager : System.Web.UI.Page
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
            LoadCbbIcon();
        }
        protected void btAdd_Click(object sender, DirectEventArgs e)
        {
            UserCommon.ReadOnlyControl(txtFooterName, false);
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
                string[] Fields = new string[] { FooterData.TBC_FooterID, FooterData.TBC_FooterName, FooterData.TBC_FooterIcon, FooterData.TBC_FooterAllowLink, FooterData.TBC_FooterLink, FooterData.TBC_FooterTypeID, FooterData.TBC_FooterSubMenu };
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtFooterName, true);
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
                    bResult = new FooterData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_FooterManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new FooterData().Search(Datas, Keyword);
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
            this.txtFooterName.Text = "";
            this.txtFooterLink.Text = "";
            UserCommon.ReadOnlyControl(this.txtFooterLink, true);
            UserCommon.SetValueControl(this.cbbFooter, "0");
            rAllowlinkfalse.Checked = true;
            rSubMenuFalse.Checked = true;

            CbbIcon.SelectedItems.Clear();
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = "None";
            CbbIcon.SelectedItems.Add(item);
            CbbIcon.UpdateSelectedItems();
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtFooterName.Text = Value[1];

            CbbIcon.SelectedItems.Clear();
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = Value[2];
            CbbIcon.SelectedItems.Add(item);
            CbbIcon.UpdateSelectedItems();

            string allowlink = Value[3];
            if (allowlink == "true")
            {
                rAllowlinktrue.Checked = true;
                UserCommon.ReadOnlyControl(txtFooterLink,false);
            }
            else
            {
                rAllowlinkfalse.Checked = true;
                UserCommon.ReadOnlyControl(txtFooterLink, true);
            }

            this.txtFooterLink.Text = Value[4];
            UserCommon.SetValueControl(this.cbbFooter, Value[5]);
            string submenu = Value[6];
            if (submenu == "true")
            {
                rSubMenuTrue.Checked = true;
            }
            else
            {
                rSubMenuFalse.Checked = true;
            }
        }
        private FooterEntities GetCon(ref bool Insert, ref string Exception)
        {
            FooterEntities res = new FooterEntities();
            if (cbbFooter.SelectedItem.Value == null)
            {
                res.FooterTypeID = 1;
            }
            else res.FooterTypeID = int.Parse(cbbFooter.SelectedItem.Value);

            int conid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(conid);
            if (!UserCommon.HasValue(this.txtFooterName))
            {
                Exception = Message.MSE_WCFieldRequired("Footer Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new FooterData().CheckExistAbout(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Footer");
                    return null;
                }
            }
            else
            {
                res.FooterID = int.Parse(hiID.Text);
            }

            res.FooterName = txtFooterName.Text.Trim();
            
            res.FooterIcon = CbbIcon.SelectedItem.Value;
            if (rSubMenuTrue.Checked)
            {
                res.FooterSubMenu = true;

            }
            if (rAllowlinktrue.Checked)
            {
                res.FooterAllowLink = true;
                res.FooterLink = txtFooterLink.Text.Trim();
            }
            else {
                res.FooterLink = "";
            }
            
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            FooterEntities objCon = new FooterEntities();
            objCon = GetCon(ref Insert, ref _Exception);
            if (objCon == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new FooterData().Insert(ref objCon);
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
                bResult = new FooterData().Update(objCon);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("Footer"), UserCommon.INFORMATION);
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
            DataTable dt = new FooterTypeData().Search(Datas, "");
            cbbFooter.SelectedItems.Clear();
            Store store = this.cbbFooter.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbFooter, "0");
            
        }
        private void LoadCbbIcon()
        {
            Store store = this.CbbIcon.GetStore();

            store.DataSource = new object[]
        {
            new object[] { null, "None"},
            new object[] { ResourceManager.GetIconClassName(Icon.Link), "Link"},
            new object[] { ResourceManager.GetIconClassName(Icon.House), "House"},
            new object[] { ResourceManager.GetIconClassName(Icon.Phone), "Phone"},
            new object[] { ResourceManager.GetIconClassName(Icon.ServerGo), "ServerGo"},
            new object[] { ResourceManager.GetIconClassName(Icon.Email), "Email"},
            new object[] { ResourceManager.GetIconClassName(Icon.Accept), "Accept"}
        };

            store.DataBind();

            this.ResourceManager1.RegisterIcon(Icon.Link);
            this.ResourceManager1.RegisterIcon(Icon.House);
            this.ResourceManager1.RegisterIcon(Icon.Phone);
            this.ResourceManager1.RegisterIcon(Icon.ServerGo);
            this.ResourceManager1.RegisterIcon(Icon.Email);
            this.ResourceManager1.RegisterIcon(Icon.Accept);
        }
        protected void RadioAllow_Change(object sender, DirectEventArgs e)
        {
            if (rAllowlinktrue.Checked)
            {
                UserCommon.ReadOnlyControl(txtFooterLink, false);
            }
            else
            {
                UserCommon.ReadOnlyControl(txtFooterLink, true);
            }
        }
    }
}