using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ProjectTypeData
    {
        public ProjectTypeData()
        {

        }
        public const string TableName = "ProjectType";
        private const string ViewName = "VW_ProjectType";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_ProjectTypeID = "ProjectTypeID";
        public const string TBC_ProjectTypeName = "ProjectTypeName";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectTypeID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref ProjectTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ProjectTypeName };
            dDatas = new object[] { obj.ProjectTypeName };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectTypeID);
            obj.ProjectTypeID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.ProjectTypeID);
            return bResult;
        }

        public bool Update(ProjectTypeEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ProjectTypeName };
            dDatas = new object[] { obj.ProjectTypeName };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectTypeID);
            bResult = Convert.ToBoolean(lib.Update(obj.ProjectTypeID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_ProjectTypeID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectTypeID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ProjectTypeName};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectTypeID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ProjectTypeID, "DESC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectTypeID);
            string[] Field = new string[] { TBC_ProjectTypeID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}