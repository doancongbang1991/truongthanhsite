using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class UserInfoData
    {
        public UserInfoData() { }
        public const string TableName = "UserInfo";
        private const string ViewName = "VW_UserInfo";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_UID = "UID";
        public const string TBC_UUserName = "UUserName";
        public const string TBC_UPassword = "UPassword";
        public const string TBC_UFullName = "UFullName";
        public const string TBC_PID = "PID";
        public const string TBC_UAddress = "UAddress";
        public const string TBC_UPhone = "UPhone";
        public const string TBC_UMobilePhone = "UMobilePhone";
        public const string TBC_UEmail = "UEmail";
        public const string TBC_UNotes = "UNotes";
        public const string TBC_UGRPID = "UGRPID";
        public const string TBC_UActive = "UActive";


        private void GetObj(UserInfoEntities obj)
        {
            dFields = new string[] { TBC_UUserName, TBC_UPassword, TBC_UFullName, TBC_PID, TBC_UAddress, TBC_UPhone, TBC_UMobilePhone, TBC_UEmail, TBC_UNotes, TBC_UGRPID, TBC_UActive };
            dDatas = new object[] { obj.UUserName, obj.UPassword, obj.UFullName, obj.PID, obj.UAddress, obj.UPhone, obj.UMobilePhone, obj.UEmail, obj.UNotes, obj.UGRPID, obj.UActive };
        }

        public bool CheckExistUUserName(string UUserName)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
            string[] Field = new string[] { TBC_UUserName };
            object[] Data = new object[] { UUserName };
            bool bResult = lib.CheckExistDataWithAND(Field,Data);
            return bResult;
        }

        public DataTable GetDataBy(string PID, string UGRPID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
            string[] Fields = new string[] { TBC_PID, TBC_UGRPID };
            object[] Datas = new object[] { PID, UGRPID };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string ID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
            DataTable dtResult = lib.GetAllByID(ID);
            return dtResult;
        }
        public DataTable GetDataByUUserName(string UUserName)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UUserName);
            DataTable dtResult = lib.GetAllByID(UUserName);
            return dtResult;
        }
        public DataTable GetDataViewByID(string UID)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_UID);
            DataTable dtResult = lib.GetAllByID(UID);
            return dtResult;
        }
        public bool Insert(ref UserInfoEntities obj)
        {
            bool bResult = false;
            GetObj(obj);
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
            obj.UID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.UID);
            return bResult;
        }
        public bool Update(UserInfoEntities obj)
        {
            bool bResult = false;
            GetObj(obj);
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
            bResult = Convert.ToBoolean(lib.Update(obj.UID, dFields, dDatas));
            return bResult;
        }
        public bool UpdatePassword(int UID, string UPassword)
        {
            bool bResult = false;
            QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
            string[] Field = new string[]{TBC_UPassword};
            object[] Data = new object[]{UPassword};
            bResult = Convert.ToBoolean(lib.Update(UID,Field ,Data));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(ID);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_UID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = new string[] { TBC_UGRPID };
            string[] SFields = new string[] { TBC_UUserName, TBC_UFullName };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_UID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_UID, "ASC");
            return dtResult;
        }
    }
}