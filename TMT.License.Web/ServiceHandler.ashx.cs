using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
//using TMT.License.Contract;
//using TMT.License.Services;
using DataLayer;
using System.Data;
using Entities;
namespace TMT.License.Web
{
    /// <summary>
    /// Summary description for ServiceHandler
    /// </summary>
    public class ServiceHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var method = context.Request["method"];
            switch (method)
            {
                //case "GetCategoryWithQuotation":
                //    GetCategoryWithQuotation(context);
                //    break;
                //case "GetQuotationDetails":
                //    GetQuotationDetails(context);
                    //break;
                case "GetProject":
                    GetProject(context);
                    break;
                case "GetProjectByID":
                    GetProjectByID(context);
                    break;
                case "GetFur":
                    GetFur(context);
                    break;
                case "GetFurType":
                    GetFurType(context);
                    break;
                case "GetFurByType":
                    GetFurByType(context);
                    break;
                case "GetCon":
                    GetCon(context);
                    break;
                case "GetConType":
                    GetConType(context);
                    break;
                case "GetConByType":
                    GetConByType(context);
                    break;
                case "GetSite":
                    GetSite(context);
                    break;
                case "GetSiteByName":
                    GetSiteByName(context);
                    break;
                case "GetAbout":
                    GetAbout(context);
                    break;
                case "GetArchType":
                    GetArchType(context);
                    break;
                //case "GetFurByType":
                //    GetFurByType(context);
                //    break;
                //other methods
                default:
                    throw new ArgumentException("unknown method");
            }
        }
        
        public void GetProject(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<ProjectsEntities> list = new List<ProjectsEntities>();
            DataTable dt = new ProjectsData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    ProjectsEntities tmp = new ProjectsEntities();
                    tmp.ProjectID =int.Parse( dt.Rows[r][ProjectsData.TBC_ProjectID].ToString());
                    tmp.ProjectName = dt.Rows[r][ProjectsData.TBC_ProjectName].ToString();
                    tmp.ProjectDetail = dt.Rows[r][ProjectsData.TBC_ProjectDetail].ToString();
                    tmp.ProjectImg = dt.Rows[r][ProjectsData.TBC_ProjectImg].ToString();
                    tmp.ProjectTypeID = int.Parse(dt.Rows[r][ProjectsData.TBC_ProjectTypeID].ToString());
                    tmp.ProjectImgFull = dt.Rows[r][ProjectsData.TBC_ProjectImgFull].ToString();
                    tmp.ProjectTypeName = dt.Rows[r][ProjectsData.TBC_ProjectTypeName].ToString();
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetProjectByID(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            var idStr = context.Request["id"];
            if (!string.IsNullOrEmpty(idStr))
            {
                List<ProjectsEntities> list = new List<ProjectsEntities>();
                DataTable dt = new ProjectsData().GetDataByID(idStr);
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        ProjectsEntities tmp = new ProjectsEntities();
                        tmp.ProjectID = int.Parse(dt.Rows[r][ProjectsData.TBC_ProjectID].ToString());
                        tmp.ProjectName = dt.Rows[r][ProjectsData.TBC_ProjectName].ToString();
                        tmp.ProjectDetail = dt.Rows[r][ProjectsData.TBC_ProjectDetail].ToString();
                        tmp.ProjectImg = dt.Rows[r][ProjectsData.TBC_ProjectImg].ToString();
                        tmp.ProjectTypeID = int.Parse(dt.Rows[r][ProjectsData.TBC_ProjectTypeID].ToString());
                        tmp.ProjectImgFull = dt.Rows[r][ProjectsData.TBC_ProjectImgFull].ToString();
                        list.Add(tmp);
                    }
                }
                var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                context.Response.Write(response);
            }
            
        }
        public void GetSite(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<SiteEntities> list = new List<SiteEntities>();
            DataTable dt = new SiteData().GetSite(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    SiteEntities tmp = new SiteEntities();
                    if (dt.Rows[r][SiteData.TBC_SiteHidden].ToString()=="True")
                    {
                        continue;
                    }
                    tmp.SiteID = int.Parse(dt.Rows[r][SiteData.TBC_SiteID].ToString());
                    tmp.SiteName = dt.Rows[r][SiteData.TBC_SiteName].ToString();
                    tmp.SiteNameVi = dt.Rows[r][SiteData.TBC_SiteNameVi].ToString();
                    tmp.SiteDetail = dt.Rows[r][SiteData.TBC_SiteDetail].ToString();
                    tmp.SiteLink = dt.Rows[r][SiteData.TBC_SiteLink].ToString();
                    tmp.SiteDesp = dt.Rows[r][SiteData.TBC_SiteDesp].ToString();
                    tmp.SiteOrder = int.Parse(dt.Rows[r][SiteData.TBC_SiteOrder].ToString());

                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetSiteByName(HttpContext context) {
            context.Response.ContentType = "text/json";
            var idStr = context.Request["name"];
            List<SiteEntities> list = new List<SiteEntities>();
            DataTable dt = new SiteData().GetDataByName(idStr);
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    SiteEntities tmp = new SiteEntities();
                   
                    tmp.SiteID = int.Parse(dt.Rows[r][SiteData.TBC_SiteID].ToString());
                    tmp.SiteName = dt.Rows[r][SiteData.TBC_SiteName].ToString();
                    tmp.SiteNameVi = dt.Rows[r][SiteData.TBC_SiteNameVi].ToString();
                    tmp.SiteDetail = dt.Rows[r][SiteData.TBC_SiteDetail].ToString();
                    tmp.SiteLink = dt.Rows[r][SiteData.TBC_SiteLink].ToString();
                    tmp.SiteDesp = dt.Rows[r][SiteData.TBC_SiteDesp].ToString();
                    tmp.SiteOrder = int.Parse(dt.Rows[r][SiteData.TBC_SiteOrder].ToString());

                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetAbout(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<AboutEntities> list = new List<AboutEntities>();
            DataTable dt = new AboutData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    AboutEntities tmp = new AboutEntities();
                    
                    tmp.AboutID = int.Parse(dt.Rows[r][AboutData.TBC_AboutID].ToString());
                    tmp.AboutName = dt.Rows[r][AboutData.TBC_AboutName].ToString();

                    tmp.AboutDetail = dt.Rows[r][AboutData.TBC_AboutDetail].ToString();
                    

                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetCon(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<ConstructionEntities> list = new List<ConstructionEntities>();
            DataTable dt = new ConstructionData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    ConstructionEntities tmp = new ConstructionEntities();
                    tmp.ConID = int.Parse(dt.Rows[r][ConstructionData.TBC_ConID].ToString());
                    tmp.ConName = dt.Rows[r][ConstructionData.TBC_ConName].ToString();
                    tmp.ConDetail = dt.Rows[r][ConstructionData.TBC_ConDetail].ToString();
                    tmp.ConImg = dt.Rows[r][ConstructionData.TBC_ConImg].ToString();
                    tmp.ConTypeID = int.Parse(dt.Rows[r][ConstructionData.TBC_ConTypeID].ToString());
                    tmp.ConTypeName = dt.Rows[r][ConstructionData.TBC_ConTypeName].ToString();
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetConByType(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<ConTypeEntities> list = new List<ConTypeEntities>();
            DataTable dt = new ConTypeData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    ConTypeEntities tmp = new ConTypeEntities();
                    tmp.ConTypeID = int.Parse(dt.Rows[r][ConTypeData.TBC_ConTypeID].ToString());
                    tmp.ConTypeName = dt.Rows[r][ConTypeData.TBC_ConTypeName].ToString();
                    tmp.listcon = new List<ConstructionEntities>();
                    DataTable dt1 = new ConstructionData().GetDataByType(tmp.ConTypeID.ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        for (int r1 = 0; r1 < dt1.Rows.Count; r1++)
                        {
                            ConstructionEntities tmp1 = new ConstructionEntities();
                            tmp1.ConID = int.Parse(dt1.Rows[r1][ConstructionData.TBC_ConID].ToString());
                            tmp1.ConName = dt1.Rows[r1][ConstructionData.TBC_ConName].ToString();
                            tmp1.ConDetail = dt1.Rows[r1][ConstructionData.TBC_ConDetail].ToString();
                            tmp1.ConImg = dt1.Rows[r1][ConstructionData.TBC_ConImg].ToString();
                            tmp1.ConTypeID = int.Parse(dt1.Rows[r1][ConstructionData.TBC_ConTypeID].ToString());
                            tmp1.ConTypeName = dt.Rows[r][ConstructionData.TBC_ConTypeName].ToString();
                            tmp.listcon.Add(tmp1);
                        }
                    }
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetConType(HttpContext context)
        {

            context.Response.ContentType = "text/json";
            List<ConTypeEntities> list = new List<ConTypeEntities>();
            DataTable dt = new ConTypeData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    ConTypeEntities tmp = new ConTypeEntities();
                    tmp.ConTypeID = int.Parse(dt.Rows[r][ConTypeData.TBC_ConTypeID].ToString());
                    tmp.ConTypeName = dt.Rows[r][ConTypeData.TBC_ConTypeName].ToString();
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetFur(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<FurnitureEntities> list = new List<FurnitureEntities>();
            DataTable dt = new FurnitureData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    FurnitureEntities tmp = new FurnitureEntities();
                    tmp.FurID = int.Parse(dt.Rows[r][FurnitureData.TBC_FurID].ToString());
                    tmp.FurName = dt.Rows[r][FurnitureData.TBC_FurName].ToString();
                    tmp.FurDetail = dt.Rows[r][FurnitureData.TBC_FurDetail].ToString();
                    tmp.FurImg = dt.Rows[r][FurnitureData.TBC_FurImg].ToString();
                    tmp.FurTypeID = int.Parse(dt.Rows[r][FurnitureData.TBC_FurTypeID].ToString());

                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetFurByType(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            List<FurTypeEntities> list = new List<FurTypeEntities>();
            DataTable dt = new FurnitureTypeData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    FurTypeEntities tmp = new FurTypeEntities();
                    tmp.FurTypeID = int.Parse(dt.Rows[r][FurnitureTypeData.TBC_FurTypeID].ToString());
                    tmp.FurTypeName = dt.Rows[r][FurnitureTypeData.TBC_FurTypeName].ToString();
                    tmp.listfur = new List<FurnitureEntities>();
                    DataTable dt1 = new FurnitureData().GetDataByType(tmp.FurTypeID.ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        for (int r1 = 0; r1 < dt1.Rows.Count; r1++)
                        {
                            FurnitureEntities tmp1 = new FurnitureEntities();
                            tmp1.FurID = int.Parse(dt1.Rows[r1][FurnitureData.TBC_FurID].ToString());
                            tmp1.FurName = dt1.Rows[r1][FurnitureData.TBC_FurName].ToString();
                            tmp1.FurDetail = dt1.Rows[r1][FurnitureData.TBC_FurDetail].ToString();
                            tmp1.FurImg = dt1.Rows[r1][FurnitureData.TBC_FurImg].ToString();
                            tmp1.FurTypeID = int.Parse(dt1.Rows[r1][FurnitureData.TBC_FurTypeID].ToString());

                            tmp.listfur.Add(tmp1);
                        }
                    }
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetFurType(HttpContext context)
        {
            
            context.Response.ContentType = "text/json";
            List<FurTypeEntities> list = new List<FurTypeEntities>();
            DataTable dt = new FurnitureTypeData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    FurTypeEntities tmp = new FurTypeEntities();
                    tmp.FurTypeID = int.Parse(dt.Rows[r][FurnitureTypeData.TBC_FurTypeID].ToString());
                    tmp.FurTypeName = dt.Rows[r][FurnitureTypeData.TBC_FurTypeName].ToString();
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        public void GetArchType(HttpContext context)
        {

            context.Response.ContentType = "text/json";
            List<ArchTypeEntities> list = new List<ArchTypeEntities>();
            DataTable dt = new ArchTypeData().Search(null, "");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    ArchTypeEntities tmp = new ArchTypeEntities();
                    tmp.ArchTypeID = int.Parse(dt.Rows[r][ArchTypeData.TBC_ArchTypeID].ToString());
                    tmp.ArchTypeName = dt.Rows[r][ArchTypeData.TBC_ArchTypeName].ToString();
                    
                    list.Add(tmp);
                }
            }
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(response);
        }
        //public void GetCategoryWithQuotation(HttpContext context)
        //{
        //    context.Response.ContentType = "text/json";
        //    var cateServ = new CategoryService();
        //    var list = cateServ.GetCategoryWithQuotation();
        //    var response = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //    context.Response.Write(response);
        //}

        //public void GetQuotationDetails(HttpContext context)
        //{
        //    context.Response.ContentType = "text/json";
        //    var idStr = context.Request["id"];
        //    if (!string.IsNullOrEmpty(idStr))
        //    {
        //        int id;
        //        if (int.TryParse(idStr, out id))
        //        {
        //            var quotationServ = new QuotationService();
        //            var res = quotationServ.GetById(id);
        //            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(res));
        //            return;
        //        }
        //    }
        //    var str = Newtonsoft.Json.JsonConvert.SerializeObject(new {Error="Not Found"});
        //    context.Response.Write(str);
        //}

        
        public void writeJson(object _object, HttpContext context)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            string jsondata = javaScriptSerializer.Serialize(_object);
            writeRaw(jsondata, context);
        }
        public void writeRaw(string text, HttpContext context)
        {
            context.Response.Write(text);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}