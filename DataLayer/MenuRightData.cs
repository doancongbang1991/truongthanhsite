using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entities;
using SqlLibrary;


namespace DataLayer
{
public class MenuRightData
{
public MenuRightData(){}
private const string TableName = "MenuRight";
private const string ViewName = "VW_MenuRight";
private static string[] dFields;
private static object[] dDatas;

public const string TBC_MRID = "MRID";
public const string TBC_UGRPID = "UGRPID";
public const string TBC_MID = "MID";
public const string TBC_MRView = "MRView";
public const string TBC_MRActive = "MRActive";
public const string TBC_UGRPName = "UGRPName";
public const string TBC_MDecription = "MDecription";

public DataTable GetDataBy()
{
    QueryLibrary lib = new QueryLibrary(TableName, TBC_MRID);
    string[] Fields = null;
    object[] Datas = null;
    DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
    return dtResult;
}

public DataTable GetDataBy(string UGRPID, string MID)
{
QueryLibrary lib = new QueryLibrary(TableName, TBC_MRID);
string[] Fields = new string[] { TBC_UGRPID, TBC_MID };
object[] Datas = new object[] { UGRPID, MID };
DataTable dtResult = lib.GetDataBy("*", 0, "AND", Fields, Datas);
return dtResult;
}
public DataTable GetDataByID(string MRID)
{
QueryLibrary lib = new QueryLibrary(TableName, TBC_MRID);
DataTable dtResult = lib.GetAllByID(MRID);
return dtResult;
}
public DataTable GetDataViewByID(string MRID)
{
QueryLibrary lib = new QueryLibrary(ViewName, TBC_MRID);
DataTable dtResult = lib.GetAllByID(MRID);
return dtResult;
}
public bool Insert(ref MenuRightEntities obj)
{
bool bResult = false;
dFields = new string[] {TBC_UGRPID, TBC_MID, TBC_MRView, TBC_MRActive};
dDatas = new object[] {obj.UGRPID,obj.MID,obj.MRView,obj.MRActive};
QueryLibrary lib = new QueryLibrary(TableName, TBC_MRID);
obj.MRID = lib.Insert(dFields, dDatas);
bResult = Convert.ToBoolean(obj.MRID);
return bResult;
}
public bool Update(MenuRightEntities obj)
{
bool bResult = false;
dFields = new string[] {TBC_UGRPID, TBC_MID, TBC_MRView, TBC_MRActive};
dDatas = new object[] {obj.UGRPID,obj.MID,obj.MRView,obj.MRActive};
QueryLibrary lib = new QueryLibrary(TableName, TBC_MRID);
bResult = Convert.ToBoolean(lib.Update(obj.MRID, dFields, dDatas));
return bResult;
}
public bool Delete(string ID)
{
bool bResult = false;
ForeignKeyData FKData = new ForeignKeyData();
bool bFK = false;
bFK = FKData.ForeignKeyCheck(TBC_MRID, ID, TableName);
if (!bFK)
{
 QueryLibrary lib = new QueryLibrary(TableName, TBC_MRID);
lib.Delete(ID);
bResult = true;
}
return bResult;
}
public DataTable Search(object[] Datas,string Keyword)
{
string[] Fields = new string[] {};
string[] SFields = new string[] { TBC_UGRPName, TBC_MDecription };
QueryLibrary lib = new QueryLibrary(ViewName, TBC_MRID);
DataTable dtResult = lib.Search("*",Fields, Datas, SFields, Keyword, TBC_MRID,"DESC");
return dtResult;
    
}

//public DataTable SearchWithUserCondition(object[] Datas, string Keyword, int UID)
//{


//    string Select = @"distinct   [MRID]
//                                  ,[UGRPID]
//                                  ,[MID]
//                                  ,[MRView]
//                                  ,[MRActive]
//                                  ,[UGRPName]
//                                  ,[MDecription]";
//    string Condition = (UID == 0) ? "" : " AND (" + TBC_MLCreatedBy + " = " + UID.ToString() + " OR " + MatterListUserData.TBC_UID + " = " + UID.ToString() + ") ";
//    string[] Fields = new string[] { TBC_UGRPID, TBC_MID };
//    string[] SFields = new string[] { TBC_MLName, TBC_MLDescr, ClientData.TBC_CName_EN };
//    QueryLibrary lib = new QueryLibrary(ViewTaskUserCondition, TBC_MLID);
//    DataTable dtResult = lib.Search(Select, Fields, Datas, SFields, Keyword, Condition, TBC_MLID, "DESC");
//    return dtResult;
//}
}
}