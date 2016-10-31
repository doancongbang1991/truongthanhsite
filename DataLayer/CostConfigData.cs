using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class CostConfigData
    {
        public CostConfigData() { }
        public const string TableName = "CostConfig";
        private const string ViewName = "VW_CostConfig";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_CostID = "CostID";
        public const string TBC_CostName = "CostName";
        public const string TBC_CostDetail = "CostDetail";
        public const string TBC_CostTag = "CostTag";
       
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string typeID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
            DataTable dtResult = lib.GetAllByID(typeID);
            return dtResult;
        }
        public DataTable GetDataByType(string typeid)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostTag);
            DataTable dtResult = lib.GetAllByID(typeid);
            return dtResult;
        }
        public bool Insert(ref CostConfigEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_CostName, TBC_CostDetail };
            dDatas = new object[] { obj.CostName, obj.CostDetail};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
            obj.CostID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.CostID);
            return bResult;
        }

        public bool Update(CostConfigEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_CostName, TBC_CostDetail };
            dDatas = new object[] { obj.CostName, obj.CostDetail };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
            bResult = Convert.ToBoolean(lib.Update(obj.CostID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_CostID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_CostName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_CostID, "ASC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_CostID);
            string[] Field = new string[] { TBC_CostID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}