using System.Data;
using DataLayer;

namespace TMT.License.Core
{
    /// <summary>
    /// Summary description for UserCommon
    /// </summary>
    public class WebPermission
    {
        public WebPermission()
        { }
        //
        // TODO: Add constructor logic here
        //

        public const  string SYSTEM_USER = "USER";
        public const string SYSTEM_USERGROUP = "USERGROUP";

        public const  string BASICDATA_MASTERTYPE = "MASTERTYPE";
        public const  string BASICDATA_TASK = "TASK";
        public const string BASICDATA_TASKTYPE = "TASKTYPE";
        public const  string BASICDATA_CLIENT = "CLIENT";
        public const  string BASICDATA_COMPANY = "COMPANY";
        public const string BASICDATA_POSITION = "POSITION";
        public const string BASICDATA_BUSINESSTYPE = "BUSINESSTYPE";

        public const string TIMESHEET_MASTERLIST = "MASTERLIST";
        public const  string TIMESHEET_TIMESHEET = "TIMESHEET";
    
        public const string ADMIN_TIMESHEET = "ADTIMESHEET";
        public const string ADMIN_MASTERLIST = "ADMASTERLIST";
        public const string ADMIN_MEMU = "ADMENURIGHT";
        public const string ADMIN_FUNCTION = "ADFUNCTION";

        public const string INVOICE_CONTRACT = "CONTRACT";
        public const string INVOICE_INVOICE = "INVOICES";

        public const string TEST = "TEST";


        public static bool ViewPermission(string KEY)
        {
            bool bResult = false;
            try
            {
                string GRPID = UserCommon.GetCookie_GRPID();
                DataTable objRight = new FunctionsRightData().GetDataBy(KEY, GRPID);
                bResult = UserCommon.ToBoolean(objRight.Rows[0]["FRView"]);
            }
            catch { bResult = false; }
            return bResult;
        }
        public static bool AddPermission(string KEY)
        {
            bool bResult = false;
            try
            {
                string GRPID = UserCommon.GetCookie_GRPID();
                DataTable objRight = new FunctionsRightData().GetDataBy(KEY, GRPID);
                bResult = UserCommon.ToBoolean(objRight.Rows[0]["FRAdd"]);
            }
            catch { bResult = false; }
            return bResult;
        }
        public static bool EditPermission(string KEY)
        {
            bool bResult = false;
            try
            {
                string GRPID = UserCommon.GetCookie_GRPID();
                DataTable objRight = new FunctionsRightData().GetDataBy(KEY, GRPID);
                bResult = UserCommon.ToBoolean(objRight.Rows[0]["FREdit"]);
            }
            catch { bResult = false; }
            return bResult;
        }
        public static bool DeletePermission(string KEY)
        {
            bool bResult = false;
            try
            {
                string GRPID = UserCommon.GetCookie_GRPID();
                DataTable objRight = new FunctionsRightData().GetDataBy(KEY, GRPID);
                bResult = UserCommon.ToBoolean(objRight.Rows[0]["FRDelete"]);
            }
            catch { bResult = false; }
            return bResult;
        }
    
    }
}