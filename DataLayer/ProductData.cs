using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ProductData
    {
        public ProductData() { }
        public const string TableName = "Products";
        private const string ViewName = "VW_Product";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_PID = "PID";
        public const string TBC_PName = "PName";
        public const string TBC_PDes = "PDes";
        public const string TBC_PVer = "PVer";

        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref ProductEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_PName, TBC_PDes, TBC_PVer };
            dDatas = new object[] { obj.PNAME, obj.PDES, obj.PVER };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            obj.PID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.PID);
            return bResult;
        }

        public bool Update(ProductEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_PName, TBC_PDes, TBC_PVer };
            dDatas = new object[] { obj.PNAME, obj.PDES, obj.PVER };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            bResult = Convert.ToBoolean(lib.Update(obj.PID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_PID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_PName, TBC_PDes, TBC_PVer };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_PID, "DESC");
            return dtResult;
        }
        
        
    }
}