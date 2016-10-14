using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class UserGroupData
    {
        public UserGroupData() { }
        private const string TableName = "UserGroup";
        private const string ViewName = "VW_UserGroup";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_UGRPID = "UGRPID";
        public const string TBC_UGRPName = "UGRPName";
        public const string TBC_UGRPParent = "UGRPParent";
        public const string TBC_UGRPActive = "UGRPActive";


        private void GetObj(UserGroupEntities obj)
        {
            dFields = new string[] { TBC_UGRPName, TBC_UGRPParent, TBC_UGRPActive };
            dDatas = new object[] { obj.UGRPName, obj.UGRPParent, obj.UGRPActive };
        }
        public DataTable GetDataBy(object UGRPActive)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UGRPID);
            string[] Fields = new string[] { TBC_UGRPActive };
            object[] Datas = new object[] { UGRPActive };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string UGRPID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UGRPID);
            DataTable dtResult = lib.GetAllByID(UGRPID);
            return dtResult;
        }
        public DataTable GetDataViewByID(string UGRPID)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_UGRPID);
            DataTable dtResult = lib.GetAllByID(UGRPID);
            return dtResult;
        }
        public bool Insert(ref UserGroupEntities obj)
        {
            bool bResult = false;
            GetObj(obj);
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UGRPID);
            obj.UGRPID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.UGRPID);
            return bResult;
        }
        public bool Update(UserGroupEntities obj)
        {
            bool bResult = false;
            GetObj(obj);
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UGRPID);
            bResult = Convert.ToBoolean(lib.Update(obj.UGRPID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_UGRPID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_UGRPID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = new string[] { };
            string[] SFields = new string[] { TBC_UGRPName };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_UGRPID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_UGRPID, "ASC");
            return dtResult;
        }
    }
}