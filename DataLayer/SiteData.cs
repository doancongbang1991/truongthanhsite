using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class SiteData
    {
        public SiteData()
        {

        }
        public const string TableName = "Site";
        private const string ViewName = "VW_Site";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_SiteID = "SiteID";
        public const string TBC_SiteName = "SiteName";
        public const string TBC_SiteDetail = "SiteDetail";
        public const string TBC_SiteDesp = "SiteDesp";
        public const string TBC_SiteLink = "SiteLink";
        public DataTable GetDataByName()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteID);
            string[] Fields = new string[] {  };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByName(string name)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteName);
            DataTable dtResult = lib.GetAllByID(name);
            return dtResult;
        }
        public bool Insert(ref SiteEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_SiteName, TBC_SiteDetail, TBC_SiteDesp, TBC_SiteLink};
            dDatas = new object[] { obj.SiteName, obj.SiteDetail, obj.SiteDesp, obj.SiteLink };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteID);
            obj.SiteID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.SiteID);
            return bResult;
        }

        public bool Update(SiteEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_SiteName, TBC_SiteDetail, TBC_SiteDesp, TBC_SiteLink };
            dDatas = new object[] { obj.SiteName, obj.SiteDetail, obj.SiteDesp, obj.SiteLink };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteID);
            bResult = Convert.ToBoolean(lib.Update(obj.SiteID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_SiteLink, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_SiteName, TBC_SiteDetail, TBC_SiteDesp, TBC_SiteLink};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_SiteID, "DESC");
            return dtResult;
        }
        public bool CheckExistSite(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SiteID);
            string[] Field = new string[] { TBC_SiteID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}