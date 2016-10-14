using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
public class FunctionsData
{
public FunctionsData(){}
private const string TableName = "Functions";
private const string ViewName = "VW_Functions";
private static string[] dFields;
private static object[] dDatas;

public const string TBC_FID = "FID";
public const string TBC_FName = "FName";
public const string TBC_FParent = "FParent";
public const string TBC_FOrder = "FOrder";
public const string TBC_FActive = "FActive";


public DataTable GetDataBy()
{
QueryLibrary lib = new QueryLibrary(TableName, TBC_FID);
string[] Fields = new string[] {  };
object[] Datas = new object[] {  };
DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
return dtResult;
}
public DataTable GetDataByID(string FID)
{
QueryLibrary lib = new QueryLibrary(TableName, TBC_FID);
DataTable dtResult = lib.GetAllByID(FID);
return dtResult;
}
public DataTable GetDataBy(object FActive)
{
    QueryLibrary lib = new QueryLibrary(TableName, TBC_FID);
    string[] Fields = new string[] { TBC_FActive };
    object[] Datas = new object[] { FActive };
    DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
    return dtResult;
}
public DataTable GetDataViewByID(string FID)
{
QueryLibrary lib = new QueryLibrary(ViewName, TBC_FID);
DataTable dtResult = lib.GetAllByID(FID);
return dtResult;
}
//public bool Insert(ref FunctionsEntities obj)
//{
//bool bResult = false;
//dFields = new string[] {TBC_FName, TBC_FParent, TBC_FOrder, TBC_FActive};
//dDatas = new object[] {obj.FName,obj.FParent,obj.FOrder,obj.FActive};
//QueryLibrary lib = new QueryLibrary(TableName, TBC_FID);
//obj.FID = lib.Insert(dFields, dDatas);
//bResult = Convert.ToBoolean(obj.FID);
//return bResult;
//}
//public bool Update(FunctionsEntities obj)
//{
//bool bResult = false;
//dFields = new string[] {TBC_FName, TBC_FParent, TBC_FOrder, TBC_FActive};
//dDatas = new object[] {obj.FName,obj.FParent,obj.FOrder,obj.FActive};
//QueryLibrary lib = new QueryLibrary(TableName, TBC_FID);
//bResult = Convert.ToBoolean(lib.Update(obj.FID, dFields, dDatas));
//return bResult;
//}
//public bool Delete(string ID)
//{
//bool bResult = false;
//ForeignKeyData FKData = new ForeignKeyData();
//bool bFK = false;
//bFK = FKData.ForeignKeyCheck(TBC_FID, ID, TableName);
//if (!bFK)
//{
// QueryLibrary lib = new QueryLibrary(TableName, TBC_FID);
//lib.Delete(ID);
//bResult = true;
//}
//return bResult;
//}
public DataTable Search(object[] Datas,string Keyword)
{
string[] Fields = new string[] { };
 string[] SFields = new string[] { TBC_FName };
QueryLibrary lib = new QueryLibrary(ViewName, TBC_FID);
DataTable dtResult = lib.Search("*",Fields, Datas, SFields, Keyword, TBC_FID,"DESC");
return dtResult;
}
}
}