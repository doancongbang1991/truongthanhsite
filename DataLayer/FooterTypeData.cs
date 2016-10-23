using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class FooterTypeData
    {
        public FooterTypeData()
        {

        }
        public const string TableName = "FooterType";
        private const string ViewName = "VW_FooterType";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_FooterTypeID = "FooterTypeID";
        public const string TBC_FooterTypeName = "FooterTypeName";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref FooterTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FooterTypeName };
            dDatas = new object[] { obj.FooterTypeName };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
            obj.FooterTypeID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.FooterTypeID);
            return bResult;
        }

        public bool Update(FooterTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FooterTypeName };
            dDatas = new object[] { obj.FooterTypeName };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
            bResult = Convert.ToBoolean(lib.Update(obj.FooterTypeID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_FooterTypeID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_FooterTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_FooterTypeID, "DESC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
            string[] Field = new string[] { TBC_FooterTypeID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}