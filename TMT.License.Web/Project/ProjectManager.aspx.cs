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
    public partial class ProjectManager : System.Web.UI.Page
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
            UserCommon.ReadOnlyControl(txtProjectName, false);
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
                string[] Fields = new string[] { ProjectsData.TBC_ProjectID, ProjectsData.TBC_ProjectName, ProjectsData.TBC_ProjectDetail, ProjectsData.TBC_ProjectTypeID, ProjectsData.TBC_ProjectImg,ProjectsData.TBC_ProjectImgFull};
                string[] value = UserCommon.GetValueFromJson(json, Fields);
                ClearAllFields_Details();
                UserCommon.ReadOnlyControl(txtProjectName, true);
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
                    bResult = new ProjectsData().Delete(oRecordID[i].ToString());
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
            Response.Redirect(UserCommon.TT_ProjectManager, true);
        }

        private void LoadGrid_Position()
        {
            this.RowSelectionModelPosition.ClearSelection();
            this.grPosition.Call("clearMemory");
            string Keyword = txtKeyword.Text.ToLower();
            object[] Datas = null;
            DataTable dt = new ProjectsData().Search(Datas, Keyword);
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
            this.txtProjectName.Text = "";
            this.txtProjectDetail.Text = "";
            this.txtProjectImg.Text = "";
            UserCommon.SetValueControl(this.cbbProjectType, "0");
            this.txtProjectImgFull.Text = "";
        }
        private void ShowDetails_Details(string[] Value)
        {
            this.hiID.Text = Value[0];
            this.txtProjectName.Text = Value[1];
            this.txtProjectDetail.Text = Value[2];
            UserCommon.SetValueControl(this.cbbProjectType, Value[3]);
            this.txtProjectImg.Text = Value[4];
            this.txtProjectImg.Text = Value[5];
        }
        private ProjectsEntities GetProject(ref bool Insert, ref string Exception)
        {
            ProjectsEntities res = new ProjectsEntities();
            if (cbbProjectType.SelectedItem.Value == null)
	        {
                res.ProjectTypeID = 1;
	        }
            else res.ProjectTypeID = int.Parse(cbbProjectType.SelectedItem.Value);
        
            int projectid = UserCommon.ToInt(this.hiID.Value);
            Insert = !UserCommon.ToBoolean(projectid);
            if (!UserCommon.HasValue(this.txtProjectName))
            {
                Exception = Message.MSE_WCFieldRequired("Project Name");
                return null;
            }

            if (Insert)
            {
                bool bExist = new ProjectsData().CheckExistAbout(this.hiID.Text);
                if (bExist)
                {
                    Exception = Message.MSE_WCFieldExist("Project");
                    return null;
                }
            }
            else {
                res.ProjectID = int.Parse(hiID.Text);
            }
            res.ProjectName = txtProjectName.Text.Trim();
            res.ProjectDetail = txtProjectDetail.Text;
            res.ProjectImg = txtProjectImg.Text;
            res.ProjectImgFull = txtProjectImgFull.Text;
            return res;
        }
        protected void btApprove_Click(object sender, DirectEventArgs e)
        {
            bool Insert = true;
            bool bResult = false;
            ProjectsEntities objProject = new ProjectsEntities();
            objProject = GetProject(ref Insert, ref _Exception);
            if (objProject == null)
            {
                UserCommon.MsbShow(_Exception, UserCommon.ERROR);
                return;
            }

            if (Insert)
            {

                bResult = new ProjectsData().Insert(ref objProject);
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
                bResult = new ProjectsData().Update(objProject);
                if (bResult)
                {
                    UserCommon.MsbShow(Message.MSI_WCSave("Project"), UserCommon.INFORMATION);
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
            DataTable dt = new ProjectTypeData().Search(Datas, "");
            this.cbbProjectType.SelectedItems.Clear();
            Store store = this.cbbProjectType.GetStore();
            store.DataSource = dt;
            store.DataBind();
            UserCommon.SetValueControl(cbbProjectType, "0");
        }
    }
}