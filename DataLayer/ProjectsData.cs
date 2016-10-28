using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class ProjectsData
    {
        public ProjectsData() { }
        public const string TableName = "Projects";
        private const string ViewName = "VW_Projects";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_ProjectID = "ProjectID";
        public const string TBC_ProjectName = "ProjectName";
        public const string TBC_ProjectDetail = "ProjectDetail";
        public const string TBC_ProjectTypeID = "ProjectTypeID";
        public const string TBC_ProjectImg = "ProjectImg";
        public const string TBC_ProjectImgFull = "ProjectImgFull";
        public const string TBC_ProjectTypeName = "ProjectTypeName";
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByID(string id)
        {
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ProjectID);
            DataTable dtResult = lib.GetAllByID(id);
            return dtResult;
        }
        public bool Insert(ref ProjectsEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ProjectName, TBC_ProjectDetail, TBC_ProjectTypeID, TBC_ProjectImg ,TBC_ProjectImgFull};
            dDatas = new object[] { obj.ProjectName, obj.ProjectDetail, obj.ProjectTypeID, obj.ProjectImg,obj.ProjectImgFull };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectID);
            obj.ProjectID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.ProjectID);
            return bResult;
        }

        public bool Update(ProjectsEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_ProjectName, TBC_ProjectDetail, TBC_ProjectTypeID, TBC_ProjectImg ,TBC_ProjectImgFull};
            dDatas = new object[] { obj.ProjectName, obj.ProjectDetail, obj.ProjectTypeID, obj.ProjectImg, obj.ProjectImgFull };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectID);
            bResult = Convert.ToBoolean(lib.Update(obj.ProjectID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_ProjectID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ProjectName, TBC_ProjectDetail, TBC_ProjectTypeID, TBC_ProjectImg};
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ProjectID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ProjectID, "DESC");
            return dtResult;
        }
        public DataTable GetProject(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_ProjectName, TBC_ProjectDetail, TBC_ProjectTypeID, TBC_ProjectImg };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_ProjectID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_ProjectID, "ASC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_ProjectID);
            string[] Field = new string[] { TBC_ProjectID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}