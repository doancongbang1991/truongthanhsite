using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class LicenseData
    {
        public LicenseData() { }
        public const string TableName = "License";
        private const string ViewName = "VW_License";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_LicID = "LicID";
        public const string TBC_LicSerial = "LicSerial";
        public const string TBC_LicKey = "LicKey";
        public const string TBC_LicDes = "LicDes";
        public const string TBC_LicStatus = "LicStatus";
        public const string TBC_LUID = "LUID";
        public const string TBC_LAppDate = "LAppDate";
        public const string TBC_LRegDate = "LRegDate";
        public const string TBC_LicProduct = "LicProduct";
        public const string TBC_LicDomain = "LicDomain";
        public const string TBC_PDes = "PDes";
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_LicID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref LicenseEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_LicKey, TBC_LicSerial, TBC_LicDes,TBC_LicStatus,TBC_LUID,TBC_LRegDate,TBC_LicDomain,TBC_LicProduct };
            dDatas = new object[] { obj.LicKey, obj.LicSerial, obj.LicDes, obj.LicStatus,obj.LUID, obj.LRegDate,obj.LicDomain,obj.LicProduct };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_LicID);
            obj.LicID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.LicID);
            return bResult;
        }
        public bool UpdateClient(LicenseEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_LicDes};
            dDatas = new object[] {obj.LicDes};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_LicID);
            bResult = Convert.ToBoolean(lib.Update(obj.LicID, dFields, dDatas));
            return bResult;
        }
        public bool Update(LicenseEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_LicKey, TBC_LicSerial, TBC_LicDes, TBC_LicStatus,TBC_LAppDate };
            dDatas = new object[] { obj.LicKey, obj.LicSerial, obj.LicDes, obj.LicStatus,obj.LAppDate };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_LicID);
            bResult = Convert.ToBoolean(lib.Update(obj.LicID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_LicID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_LicID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_LicKey, TBC_LicSerial, TBC_LicDes, TBC_LicDomain, TBC_LicProduct,TBC_PDes };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_LicID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_LicID, "DESC");
            return dtResult;
        }
        public DataTable Search1(object[] Datas, string Keyword,string LUID)
        {
            Datas = new string []{LUID};
            string[] Fields = new string [] {TBC_LUID} ;
            string[] SFields = new string[] { TBC_LicKey, TBC_LicSerial, TBC_LicDes, TBC_LicDomain, TBC_LicProduct, TBC_PDes };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_LicID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_LicID, "DESC");
            return dtResult;
        }
        public bool CheckExistLicSerial(string LicSerial)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_LicID);
            string[] Field = new string[] { TBC_LicSerial };
            object[] Data = new object[] { LicSerial };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}