using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class SubMenuData
    {
        public SubMenuData()
        {

        }
        public const string TableName = "SubMenu";
        private const string ViewName = "VW_SubMenu";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_SubMenuID = "SubMenuID";
        public const string TBC_SubMenuName = "SubMenuName";
        public const string TBC_SubMenuDetail = "SubMenuDetail";
        public const string TBC_SubMenuLink = "SubMenuLink";
        public const string TBC_SubMenuParentID = "SubMenuParentID";
       
        
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_SubMenuName, TBC_SubMenuDetail, TBC_SubMenuLink, TBC_SubMenuParentID };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SubMenuID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_SubMenuID, "ASC");
            return dtResult;
        }
        public DataTable GetDataByParentID(string parent)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_SubMenuParentID);
            DataTable dtResult = lib.GetAllByID(parent);
            return dtResult;
        }
    }
}