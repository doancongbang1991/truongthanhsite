using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ArchTypeData
    {
        public ArchTypeData()
        {

        }
        public const string TableName = "ArchType";
        private const string ViewName = "VW_ArchType";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_ArchTypeID = "ArchTypeID";
        public const string TBC_ArchTypeName = "ArchTypeName";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchTypeID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref FurTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ArchTypeName};
            dDatas = new object[] { obj.FurTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchTypeID);
            obj.FurTypeID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.FurTypeID);
            return bResult;
        }
        
        public bool Update(FurTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ArchTypeName };
            dDatas = new object[] { obj.FurTypeName };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchTypeID);
            bResult = Convert.ToBoolean(lib.Update(obj.FurTypeID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_ArchTypeID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchTypeID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ArchTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchTypeID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ArchTypeID, "ASC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchTypeID);
            string[] Field = new string[] { TBC_ArchTypeID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}