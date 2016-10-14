using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class PositionData
    {
        public PositionData() { }
        public const string TableName = "Position";
        private const string ViewName = "VW_Position";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_PID = "PID";
        public const string TBC_PName = "PName";
        public const string TBC_PDescr = "PDescr";
        public const string TBC_PCreatedBy = "PCreatedBy";
        public const string TBC_PCreatedD = "PCreatedD";
        public const string TBC_PLastUpdatedBy = "PLastUpdatedBy";
        public const string TBC_PLastUpdatedD = "PLastUpdatedD";


        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string PID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            DataTable dtResult = lib.GetAllByID(PID);
            return dtResult;
        }
        public DataTable GetDataViewByID(string PID)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_PID);
            DataTable dtResult = lib.GetAllByID(PID);
            return dtResult;
        }
        public bool Insert(ref PositionEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_PName, TBC_PDescr, TBC_PCreatedBy, TBC_PCreatedD };
            dDatas = new object[] { obj.PName, obj.PDescr, obj.PCreatedBy, obj.PCreatedD };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_PID);
            obj.PID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.PID);
            return bResult;
        }
        public bool Update(PositionEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_PName, TBC_PDescr, TBC_PLastUpdatedBy, TBC_PLastUpdatedD };
            dDatas = new object[] { obj.PName, obj.PDescr, obj.PLastUpdatedBy, obj.PLastUpdatedD };
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
            string[] SFields = new string[] { TBC_PName };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_PID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_PID, "DESC");
            return dtResult;
        }
    }
}