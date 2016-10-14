using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Entities;
using SqlLibrary;

namespace DataLayer
{
    public class ForeignKeyData
    {
        public ForeignKeyData() { }

        
        private string[] GetTableFK(string TableName)
        {
            string[] tmp = null;
            switch (TableName)
            {
                //case ClientData.TableName:
                //    tmp = new string[] { MatterListData.TableName };
                //    break;
                //case TaskTypeData.TableName:
                //    tmp = new string[] { TaskData.TableName };
                //    break;
                //case MatterTypeData.TableName:
                //    tmp = new string[] { MatterListData.TableName };
                //    break;
                //case MatterListData.TableName:
                //    tmp = new string[] { TimeSheetData.TableName, TimeSheetData.TableName +"Log" };
                //    break;
                //case TaskData.TableName:
                //    tmp = new string[] { TimeSheetData.TableName, TimeSheetData.TableName + "Log" };
                //    break;
                //case PositionData.TableName:
                //    tmp = new string[] { TimeSheetData.TableName, TimeSheetData.TableName + "Log" };
                //    break;
                //case ContractData.TableName:
                //    tmp = new string[] { InvoiceData.TableName};
                //    break;
            }

            return tmp;
        }
        public bool ForeignKeyCheck(string Field, string Value,string TableName)
        {
            bool bResult = false;
            string[] TableFK = GetTableFK(TableName);
            if (TableFK == null)
                return false;
            for (int i = 0; i < TableFK.Length; i++)
            {
                QueryLibrary lib = new QueryLibrary(TableFK[i], Field);
                bResult = lib.CheckIDBeingUsed(Field,Value);
                if (bResult)
                    break;
            }
            return bResult;
        }
        public bool ForeignKeyCheck(string Value)
        {
            bool bResult = false;
            string[] TableFK = new string[] { 
                //BusinessTypeData.TableName
                //, ClientData.TableName
                //, MatterListData.TableName
                //, MatterListUserData.TableName
                //, MatterTypeData.TableName
                //, PositionData.TableName
                //, TaskData.TableName
                //, TaskTypeData.TableName
                //, TimeSheetData.TableName
                //, TimeSheetData.TableName + "Log" 
            };
            string[] Field = new string[] { 
                //BusinessTypeData.TBC_BTCreatedBy + "," + BusinessTypeData.TBC_BTLastUpdatedBy
                //, ClientData.TBC_CCreatedBy + "," + ClientData.TBC_CLastUpdatedBy
                //, MatterListData.TBC_MLCreatedBy + "," + MatterListData.TBC_MLLastUpdatedBy
                //, MatterListUserData.TBC_UID
                //, MatterTypeData.TBC_MTCreatedBy + "," +    MatterTypeData.TBC_MTLastUpdatedBy
                //, PositionData.TBC_PCreatedBy +","+ PositionData.TBC_PLastUpdatedBy
                //, TaskData.TBC_TCreatedBy + "," + TaskData.TBC_TLastUpdatedBy
                //, TaskTypeData.TBC_TTCreatedBy + "," + TaskTypeData.TBC_TTLastUpdatedBy
                //, TimeSheetData.TBC_UID + "," + TimeSheetData.TBC_TSCreatedBy + "," +TimeSheetData.TBC_TSLastUpdatedBy
                // , TimeSheetData.TBC_UID + "," + TimeSheetData.TBC_TSCreatedBy + "," +TimeSheetData.TBC_TSLastUpdatedBy
            };

            for (int i = 0; i < TableFK.Length; i++)
            {
                QueryLibrary lib = new QueryLibrary(TableFK[i],"ID");
                string[] FieldCheck = Field[i].Split(',');
                bResult = lib.CheckIDBeingUsed(FieldCheck, Value);
                if (bResult)
                    break;
            }
            return bResult;
        }
    }
}