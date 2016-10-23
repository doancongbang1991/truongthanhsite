using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class AboutData
    {
        public AboutData()
        {

        }
        public const string TableName = "About";
        
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_AboutID = "AboutID";
        public const string TBC_AboutName = "AboutName";
        public const string TBC_AboutDetail = "AboutDetail";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_AboutID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref AboutEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_AboutName, TBC_AboutDetail };
            dDatas = new object[] {  obj.AboutName, obj.AboutDetail};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_AboutID);
            obj.AboutID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.AboutID);
            return bResult;
        }
        public bool Update(AboutEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_AboutName, TBC_AboutDetail };
            dDatas = new object[] { obj.AboutName, obj.AboutDetail};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_AboutID);
            bResult = Convert.ToBoolean(lib.Update(obj.AboutID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_AboutID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_AboutID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_AboutID, TBC_AboutName, TBC_AboutDetail };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_AboutID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_AboutID, "DESC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_AboutID);
            string[] Field = new string[] { TBC_AboutID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}