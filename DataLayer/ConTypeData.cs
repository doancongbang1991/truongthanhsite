using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ConTypeData
    {
        public ConTypeData() { }
        public const string TableName = "ConType";
        private const string ViewName = "VW_ConType";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_ConTypeID = "ConTypeID";
        public const string TBC_ConTypeName = "ConTypeName";
       
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
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
        public bool Insert(ref ConTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ConTypeName, };
            dDatas = new object[] { obj.ConTypeName, };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
            obj.ConTypeID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.ConTypeID);
            return bResult;
        }
        
        public bool Update(ConTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ConTypeName, };
            dDatas = new object[] { obj.ConTypeName, };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
            bResult = Convert.ToBoolean(lib.Update(obj.ConTypeID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_ConTypeID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ConTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ConTypeID, "DESC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ConTypeID);
            string[] Field = new string[] { TBC_ConTypeID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}