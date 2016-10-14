using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class MenuData
    {
        public MenuData() { }
        private const string TableName = "Menu";
        private const string ViewName = "VW_Menu";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_MID = "MID";
        public const string TBC_MParentID = "MParentID";
        public const string TBC_MDecription = "MDecription";
        public const string TBC_MUrl = "MUrl";
        public const string TBC_MOrder = "MOrder";
        public const string TBC_MIcon = "MIcon";
        public const string TBC_MIsHidden = "MIsHidden";
        public const string TBC_MActive = "MActive";

        public const string TBC_MRID = "MRID";
        public const string TBC_UGRPID = "UGRPID";
        public const string TBC_MRView = "MRView";
        public const string TBC_MRActive = "MRActive";


        private void GetObj(MenuEntities obj)
        {
            dFields = new string[] { TBC_MParentID, TBC_MDecription, TBC_MUrl, TBC_MOrder, TBC_MIcon, TBC_MIsHidden, TBC_MActive };
            dDatas = new object[] { obj.MParentID, obj.MDecription, obj.MUrl, obj.MOrder, obj.MIcon, obj.MIsHidden, obj.MActive };
        }

        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MID);
            string[] Fields = null;
            object[] Datas = null;
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public DataTable GetDataBy(string MParentID, string MIsHidden)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MID);
            string[] Fields = new string[] { TBC_MParentID, TBC_MIsHidden };
            object[] Datas = new object[] { MParentID, MIsHidden };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public DataTable GetDataViewBy(string MParentID, string UGRPID)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_MID);
            string[] Fields = new string[] { TBC_MParentID,TBC_UGRPID,TBC_MActive,TBC_MIsHidden };
            object[] Datas = new object[] { MParentID, UGRPID,"True","False" };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas, TBC_MOrder );
            return dtResult;
        }

        public DataTable GetDataByID(string MID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MID);
            DataTable dtResult = lib.GetAllByID(MID);
            return dtResult;
        }
        public DataTable GetDataViewByID(string MID)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_MID);
            DataTable dtResult = lib.GetAllByID(MID);
            return dtResult;
        }
        public bool Insert(ref MenuEntities obj)
        {
            bool bResult = false;
            GetObj(obj);
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MID);
            bResult = Convert.ToBoolean(lib.Insert(dFields, dDatas));
            return bResult;
        }
        public bool Update(ref MenuEntities obj)
        {
            bool bResult = false;
            GetObj(obj);
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MID);
            bResult = Convert.ToBoolean(lib.Update(obj.MID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_MID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_MID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = new string[] { TBC_MParentID, TBC_MIsHidden };
            string[] SFields = new string[] { TBC_MDecription };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_MID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_MID, "ASC");
            return dtResult;
        }
    }
}