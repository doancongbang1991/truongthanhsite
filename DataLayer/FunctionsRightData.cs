using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Entities;
using SqlLibrary;

namespace DataLayer
{
    public class FunctionsRightData
    {
        public FunctionsRightData() { }
        public const string TableName = "FunctionsRight";
        private const string ViewName = "VW_FunctionsRight";
        public const string TBC_FRID = "FRID";
        public const string TBC_UGRPID = "UGRPID";
        public const string TBC_FID = "FID";
        public const string TBC_FRView = "FRView";
        public const string TBC_FRAdd = "FRAdd";
        public const string TBC_FREdit = "FREdit";
        public const string TBC_FRDelete = "FRDelete";
        public const string TBC_FRActive = "FRActive";
        private static string[] dFields;
        private static object[] dDatas;

        public void GetObj(FunctionsRightEntities obj)
        {
            dFields = new string[] { TBC_UGRPID, TBC_FID, TBC_FRView, TBC_FRAdd, TBC_FREdit, TBC_FRDelete, TBC_FRActive };
            dDatas = new object[] { obj.UGRPID, obj.FID, obj.FRView, obj.FRAdd, obj.FREdit, obj.FRDelete, obj.FRActive };
        }
        public DataTable GetDataBy(string FID, string UGRPID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FRID);
            string[] Fields = new string[] { TBC_FID, TBC_UGRPID, TBC_FRActive };
            object[] Datas = new object[] { FID, UGRPID, "True" };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string FID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FRID);
            DataTable dtResult = lib.GetAllByID(FID);
            return dtResult;
        }
        public DataTable GetDataViewByID(string FID)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_FRID);
            DataTable dtResult = lib.GetAllByID(FID);
            return dtResult;
        }

        public bool Insert(ref FunctionsRightEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_UGRPID, TBC_FID, TBC_FRView, TBC_FRAdd, TBC_FREdit, TBC_FRDelete, TBC_FRActive };
            dDatas = new object[] { obj.UGRPID, obj.FID, obj.FRView, obj.FRAdd, obj.FREdit, obj.FRDelete, obj.FRActive };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FRID);
            obj.FRID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.FRID);
            return bResult;
        }
        public bool Update(FunctionsRightEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_UGRPID, TBC_FID, TBC_FRView, TBC_FRAdd, TBC_FREdit, TBC_FRDelete, TBC_FRActive };
            dDatas = new object[] { obj.UGRPID, obj.FID, obj.FRView, obj.FRAdd, obj.FREdit, obj.FRDelete, obj.FRActive };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FRID);
            bResult = Convert.ToBoolean(lib.Update(obj.FRID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_FRID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_FRID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = new string[] { };
            string[] SFields = new string[] { TBC_UGRPID, TBC_FID };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_FRID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_FRID, "DESC");
            return dtResult;

        }
    }
}