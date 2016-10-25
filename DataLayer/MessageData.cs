using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
    public class MessageData
    {
        public MessageData() { }
        public const string TableName = "Message";
        private const string ViewName = "VW_Message";
        private static string[] dFields;
        private static object[] dDatas;

        public const string TBC_MessID = "MessID";
        public const string TBC_MessName = "MessName";
        public const string TBC_MessYear = "MessYear";
        public const string TBC_MessMail = "MessMail";
        public const string TBC_MessGen = "MessGen";
        public const string TBC_MessPhone = "MessPhone";
       public const string TBC_MessBody = "MessBody";
       public const string TBC_MessRead = "MessRead";
        public DataTable GetDataBy()
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MessID);
            string[] Fields = new string[] { };
            object[] Datas = new object[] { };
            DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
            return dtResult;
        }

        public bool Insert(ref MessageEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_MessName, TBC_MessYear, TBC_MessMail, TBC_MessGen, TBC_MessPhone, TBC_MessBody, TBC_MessRead };
            dDatas = new object[] { obj.MessName, obj.MessYear, obj.MessMail, obj.MessGen, obj.MessPhone,obj.MessBody ,obj.MessRead};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MessID);
            obj.MessID = lib.Insert(dFields, dDatas);
            bResult = Convert.ToBoolean(obj.MessID);
            return bResult;
        }

        public bool Update(MessageEntities obj)
        {
            bool bResult = false;
            dFields = new string[] { TBC_MessName, TBC_MessYear, TBC_MessMail, TBC_MessPhone, TBC_MessBody, TBC_MessRead };
            dDatas = new object[] { obj.MessName, obj.MessYear, obj.MessMail, obj.MessPhone, obj.MessBody, obj.MessRead };
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MessID);
            bResult = Convert.ToBoolean(lib.Update(obj.MessID, dFields, dDatas));
            return bResult;
        }
        public bool Delete(string ID)
        {
            bool bResult = false;
            ForeignKeyData FKData = new ForeignKeyData();
            bool bFK = false;
            bFK = FKData.ForeignKeyCheck(TBC_MessID, ID, TableName);
            if (!bFK)
            {
                QueryLibrary lib = new QueryLibrary(TableName, TBC_MessID);
                lib.Delete(ID);
                bResult = true;
            }
            return bResult;
        }
        public DataTable Search(object[] Datas, string Keyword)
        {
            string[] Fields = null;
            string[] SFields = new string[] { TBC_MessName, TBC_MessYear, TBC_MessMail, TBC_MessPhone, TBC_MessBody};
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MessID);
            DataTable dtResult = lib.Search("*", Fields, Datas, SFields, Keyword, TBC_MessID, "DESC");
            return dtResult;
        }
        
        public bool CheckExistAbout(string About)
        {
            QueryLibrary lib = new QueryLibrary(TableName, TBC_MessID);
            string[] Field = new string[] { TBC_MessID };
            object[] Data = new object[] { About };
            bool bResult = lib.CheckExistDataWithAND(Field, Data);
            return bResult;
        }
    }
}