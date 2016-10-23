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
                case "GetFur":
                    GetFur(context);
                    break;
                case "GetCon":
                    GetCon(context);
                    break;
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