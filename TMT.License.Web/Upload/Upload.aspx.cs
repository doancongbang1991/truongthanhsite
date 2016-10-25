using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using TMT.License.Core;
namespace TMT.License.Web
{
    public partial class Upload : System.Web.UI.Page
    {
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
            
        }
        protected void UploadClick(object sender, DirectEventArgs e)
        {
            if (this.FileUploadField1.HasFile)
            {
                HttpPostedFile file = FileUploadField1.PostedFile;
                //UserCommon.MsbShow(HttpRuntime.AppDomainAppPath, UserCommon.INFORMATION);
                //+ "."+ file.ContentType
                string filename = "";
                if (txtPhotoName.Text.Trim() == "")
                {
                    filename = file.FileName;
                }
                else
                {
                    string[] type = file.ContentType.Split('/');
                    filename = txtPhotoName.Text.Trim() + "." + type[1];
                }

                file.SaveAs(HttpRuntime.AppDomainAppPath + "images/Content/" + filename);
                UserCommon.MsbShow("Upload Succeeded", UserCommon.INFORMATION);
                BasicForm.Reset();
            }
        }
    }
}