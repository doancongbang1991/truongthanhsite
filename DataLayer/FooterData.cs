using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class FooterData
    {
        public FooterData() { }
        public const string TableName = "Footer";
        private const string ViewName = "VW_Footer";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_FooterID = "FooterID";
        public const string TBC_FooterName = "FooterName";
        public const string TBC_FooterIcon = "FooterIcon";
        public const string TBC_FooterAllowLink = "FooterAllowLink";
        public const string TBC_FooterLink = "FooterLink";
        public const string TBC_FooterTypeID = "FooterTypeID";
        public const string TBC_FooterSubMenu = "FooterSubMenu";
        
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }
        public DataTable GetDataByType(string FooterTypeID)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterTypeID);
            DataTable dtResult = lib.GetAllByID(FooterTypeID);
            return dtResult;
        }
        public bool Insert(ref FooterEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FooterName, TBC_FooterIcon, TBC_FooterAllowLink, TBC_FooterLink, TBC_FooterTypeID, TBC_FooterSubMenu };
            dDatas = new object[] { obj.FooterName, obj.FooterIcon, obj.FooterAllowLink, obj.FooterLink, obj.FooterTypeID, obj.FooterSubMenu };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterID);
            obj.FooterID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.FooterID);
            return bResult;
        }

        public bool Update(FooterEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_FooterName, TBC_FooterIcon, TBC_FooterAllowLink, TBC_FooterLink, TBC_FooterTypeID, TBC_FooterSubMenu };
            dDatas = new object[] { obj.FooterName, obj.FooterIcon, obj.FooterAllowLink, obj.FooterLink, obj.FooterTypeID, obj.FooterSubMenu };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterID);
            bResult = Convert.ToBoolean(lib.Update(obj.FooterID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_FooterID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_FooterName, TBC_FooterIcon, TBC_FooterAllowLink, TBC_FooterLink, TBC_FooterTypeID, TBC_FooterSubMenu };
            QueryLibrary lib = new QueryLibrary(ViewName, TBC_FooterID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_FooterID, "DESC");
            return dtResult;
        }
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_FooterID);
            string[] Field = new string[] { TBC_FooterID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}