using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class FurnitureTypeData
    {
        public FurnitureTypeData()
        {

        }
        public const string TableName = "FurnitureType";
        private const string ViewName = "VW_FurnitureType";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_FurTypeID = "FurTypeID";
        public const string TBC_FurTypeName = "FurTypeName";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref FurTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FurTypeName};
            dDatas = new object[] { obj.FurTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
            obj.FurTypeID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.FurTypeID);
            return bResult;
        }
        
        public bool Update(FurTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FurTypeName };
            dDatas = new object[] { obj.FurTypeName };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
            bResult = Convert.ToBoolean(lib.Update(obj.FurTypeID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_FurTypeID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_FurTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_FurTypeID, "ASC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
            string[] Field = new string[] { TBC_FurTypeID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}