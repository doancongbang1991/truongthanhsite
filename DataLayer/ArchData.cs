using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ArchData
    {
        public ArchData() { }
        public const string TableName = "Arch";
        private const string ViewName = "VW_Arch";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_ArchID = "ArchID";
        public const string TBC_ArchName = "ArchName";
        public const string TBC_ArchDetail = "ArchDetail";
        public const string TBC_ArchTypeID = "ArchTypeID";
        public const string TBC_ArchTypeName = "ArchTypeName";
        public const string TBC_ArchThump = "ArchThump";
        public const string TBC_ArchDesp= "ArchDesp";
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string id)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ArchID);
            DataTable dtResult = lib.GetAllByID(id);
            return dtResult;
        }
        public DataTable GetDataByType(string typeid)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ArchTypeID);
            DataTable dtResult = lib.GetAllByID(typeid);
            return dtResult;
        }
        
        
        public bool Insert(ref ArchEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ArchName, TBC_ArchDetail, TBC_ArchTypeID, TBC_ArchThump, TBC_ArchDesp };
            dDatas = new object[]  { obj.ArchName, obj.ArchDetail, obj.ArchTypeID, obj.ArchThump, obj.ArchDesp };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchID);
            obj.ArchID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.ArchID);
            return bResult;
        }

        public bool Update(ArchEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ArchName, TBC_ArchDetail, TBC_ArchTypeID, TBC_ArchThump, TBC_ArchDesp };
            dDatas = new object[] { obj.ArchName, obj.ArchDetail, obj.ArchTypeID, obj.ArchThump, obj.ArchDesp };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchID);
            bResult = Convert.ToBoolean(lib.Update(obj.ArchID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_ArchID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ArchID, TBC_ArchName, TBC_ArchDetail};
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ArchID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ArchID, "DESC");
            return dtResult;
        }
        public bool CheckExistArch(string Arch)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ArchID);
            string[] Field = new string[] { TBC_ArchID };
            object[] Data = new object[] { Arch };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}