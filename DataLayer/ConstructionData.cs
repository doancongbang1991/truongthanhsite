using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ConstructionData
    {
        public ConstructionData()
        {

        }
        public const string TableName = "Construction";
        private const string ViewName = "VW_Construction";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_ConID = "ConID";
        public const string TBC_ConName = "ConName";
        public const string TBC_ConDetail = "ConDetail";
        public const string TBC_ConTypeID = "ConTypeID";
        public const string TBC_ConImg = "ConImg";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByType(string typeid)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
            DataTable dtResult = lib.GetAllByID(typeid);
            return dtResult;
        }
        public bool Insert(ref ConstructionEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ConName, TBC_ConDetail, TBC_ConTypeID, TBC_ConImg };
            dDatas = new object[] { obj.ConName, obj.ConDetail, obj.ConTypeID, obj.ConImg };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConID);
            obj.ConID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.ConID);
            return bResult;
        }
        
        public bool Update(ConstructionEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ConName, TBC_ConDetail, TBC_ConTypeID, TBC_ConImg };
            dDatas = new object[] { obj.ConName, obj.ConDetail, obj.ConTypeID, obj.ConImg };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConID);
            bResult = Convert.ToBoolean(lib.Update(obj.ConID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_ConID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_ConID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ConName, TBC_ConDetail, TBC_ConTypeID, TBC_ConImg };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ConID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ConID, "DESC");
            return dtResult;
        }
        public bool CheckExistAbout(string Con )
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConID);
            string[] Field = new string[] { TBC_ConID };
            object[] Data = new object[] { Con };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}