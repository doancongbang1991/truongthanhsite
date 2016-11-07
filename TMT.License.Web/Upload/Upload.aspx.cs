using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using TMT.License.Core;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
namespace TMT.License.Web
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                LoadPage();
                LoadRootNode();
                LoadImage();
            }

           
        }
        public void LoadRootNode()
        {
                TreePanel1.GetRootNode().AppendChild(new Node
                {
                    NodeID = (++NewIndex).ToString(),
                    CustomAttributes =
                        {
                            new ConfigItem("name", "Root")
                        },
                    Icon = Icon.House,
                    AllowDrag = false,
                    EmptyChildren = true
                });
        }
        public void LoadImage() {
            string path = Server.MapPath("../images/Content/");
            string[] files = System.IO.Directory.GetFiles(path);
            string[] folders = System.IO.Directory.GetDirectories(path);
            List<object> data = new List<object>(files.Length);
            foreach (string folder in folders)
            {
                System.IO.DirectoryInfo fd = new System.IO.DirectoryInfo(folder);



                TreePanel1.GetRootNode().AppendChild(new Node
                {
                    NodeID = (++NewIndex).ToString(),
                    CustomAttributes =
                        {
                            new ConfigItem("name", fd.Name)
                        },
                    Icon = Icon.Attach,
                    AllowDrag = false,
                    EmptyChildren = true
                });
            }
            int id = 0;
            foreach (string fileName in files)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

                data.Add(new
                {
                    index = id,
                    name = fi.Name,
                    url = "../images/Content/" + fi.Name,
                    link = path.Replace(@"\", @"/") + fi.Name
                });
                id++;
            }

            this.Store1.DataSource = data;
            this.Store1.DataBind();
        }
        protected void NewAlbumClick(object sender, DirectEventArgs e)
        {
            txtAlbumName.Reset();
            WindowNewAlbum.Show();
            
        }
        protected void btnAlbumApprove_Click(object sender, DirectEventArgs e)
        {
            string path = Server.MapPath("../images/Content/");
            Directory.CreateDirectory(path +"/"+ txtAlbumName.Text.Trim());
            TreePanel1.GetRootNode().AppendChild(new Node
            {
                NodeID = (++NewIndex).ToString(),
                CustomAttributes =
            {
                new ConfigItem("name", txtAlbumName.Text.Trim(), ParameterMode.Value)

            },
                IconCls = "album-btn",
                AllowDrag = false,
                EmptyChildren = true
            });

            WindowNewAlbum.Hide();

        }
        protected void btnAlbumCancel_Click(object sender, DirectEventArgs e)
        {
            WindowNewAlbum.Hide();
            
        }
        protected void TreeViewClick(object sender, DirectEventArgs e)
        {

            string json = e.ExtraParams["grPosition_Select_Values"].Replace("\"", "");
            json = json.Substring(2,json.Length-4);
            string[] jsondict = json.Split(',') ;
            string[] name = jsondict[4].Split(':');
            string path = "";
            string tempurl = "";
            string templink = "";
            if (name[1] == "Root"){
                path = Server.MapPath("../images/Content/");
                tempurl = "../images/Content/" ;
                templink = path.Replace(@"\", @"/");
            }
            else{
                path = Server.MapPath("../images/Content/"+name[1]);
                tempurl = "../images/Content/" + name[1] + @"/";
                templink = path.Replace(@"\", @"/") + "/";
            }
            
            string[] files = System.IO.Directory.GetFiles(path);
            
            List<object> data = new List<object>(files.Length);
            int id = 0;
            foreach (string fileName in files)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

                data.Add(new
                {
                    index = id,
                    name = fi.Name,
                    url =  tempurl + fi.Name,
                    link = templink + fi.Name
                });
                id++;
            }

            this.Store1.DataSource = data;
            this.Store1.DataBind();
        }
        protected void btnDel_Click(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["ImageView_Select_Values"].Replace("\"", "");
            json = json.Substring(2, json.Length - 4);
            string[] jsondict = json.Split(',');
            string link = jsondict[3].Substring(5,jsondict[3].Length-5);
            string id = jsondict[0].Substring(6, jsondict[0].Length - 6);
            System.IO.File.Delete(link);
            Store store = this.ImageView.GetStore();
            store.RemoveAt(int.Parse(id));
            
            //ImageView.Refresh();
            Panel2.Reload();
        }
        private int NewIndex
        {
            get
            {
                return (int)(Session["newIndex"] ?? 1);
            }
            set
            {
                Session["newIndex"] = value;
            }
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

        protected void btAddImage_Click(object sender, DirectEventArgs e)
        {
            this.winDetails.Show();
        }
        protected void btnImageCancel_Click(object sender, DirectEventArgs e)
        {
            this.winDetails.Hide();
        }
    }
}