using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class FurnitureData
    {
        public FurnitureData()
        {

        }
        public const string TableName = "Furniture";
        private const string ViewName = "VW_Furniture";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_FurID = "FurID";
        public const string TBC_FurName = "FurName";
        public const string TBC_FurTypeID = "FurTypeID";
        public const string TBC_FurDetail = "FurDetail";
        public const string TBC_FurImg = "FurImg";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByType(string typeid)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurTypeID);
            DataTable dtResult = lib.GetAllByID(typeid);
            return dtResult;
        }
        public bool Insert(ref FurnitureEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FurName, TBC_FurTypeID, TBC_FurDetail, TBC_FurImg };
            dDatas = new object[] { obj.FurName, obj.FurTypeID, obj.FurDetail, obj.FurImg };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurID);
            obj.FurID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.FurID);
            return bResult;
        }

        public bool Update(FurnitureEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FurName, TBC_FurTypeID, TBC_FurDetail, TBC_FurImg };
            dDatas = new object[] { obj.FurName, obj.FurTypeID, obj.FurDetail, obj.FurImg };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurID);
            bResult = Convert.ToBoolean(lib.Update(obj.FurID, dFields, dDatas));
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
                QueryLibrary lib = new QueryLibrary(TableName, TBC_FurID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_FurName, TBC_FurTypeID, TBC_FurDetail, TBC_FurImg};
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_FurID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_FurID, "ASC");
            return dtResult;
        }
        public bool CheckExistAbout(string Fur)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FurID);
            string[] Field = new string[] { TBC_FurID };
            object[] Data = new object[] { Fur };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}