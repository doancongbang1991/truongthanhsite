namespace TMT.License.Core
{
    /// <summary>
    /// Summary description for UserCommon
    /// </summary>
    public class Message
    {
        public Message()
        { }
        //
        // TODO: Add constructor logic here
        //
        #region UserInfo

        public static string MSE_UIExpired = "Your Session is expired. Please login again!";
        public static string MSE_LGWrongUserPass = "Username or Password is wrong. Try again!";
        public static string MSE_UIWrongOldPass = "Old Password is wrong!";
        public static string MSE_UIWrongNewPass = "The new passwords do not match!";
        public static string MSE_UIBlockedUser = "This account is blocked. Please contact Administrator!";
        public static string MSI_UIResetPass = "User's password has been reset successfully!";
        public static string MSI_UIChangePass = "User's password has been change successfully!";
        #endregion

        #region Right
        public const string ADD = "Add";
        public const string EDIT = "Edit";
        public const string DELETE = "Delete";
        public const string VIEW = "View";

        public static string MSE_RGNoPermissionView = "You do not have permission to view this page!";
        public static string MSE_RGNoPermissionEdit = "You do not have permission to edit this records!";
   
        #endregion

        #region WebCommon

        public static string MSE_WCSelectRowRequired = "Please select a row from the lists!";
        public static string MSE_WCNoDelete = "Can't delete this record because it's being used by another object!";
        public static string MSE_WCNoEdit = "Can't edit this record because it's being used by another object!";
        public static string MSE_WCDeleteApprove = "Can't delete this record because it's approved or actived!";
        public static string MSI_WCSave(string Info)
        {
            return Info + "'s information is saved successfull!";
        }
        public static string MSE_WCFieldExist(string Fields)
        {
            return Fields + " fields exists in the Database!";
        }
        public static string MSE_WCFieldRequired(string Fields)
        {
            return Fields + " is required field!";
        }
        public static string MSE_WCFieldNotVaild(string Fields)
        {
            return Fields + " is not valid!";
        }
        public static string MSE_WCNovalid(string Fields)
        {
            return Fields + " fields must like XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX!";
        }
        #endregion

        #region Matter List

        public static string msmtBFAssignTaskAndUser = "Please input info and press OK to create Matter List!";
        public static string msmtAssignTaskAndUser = "Matter List is created successfull! Please assign task & user for this Matter list";

        #endregion

        #region TimeSheet

        public static string mstsErrorStart = "An error occurred,TimeSheet can't start, Please try again!";
        public static string mstsNotFound = "No Timesheet's data for this client and matter list!";
        public static string mstsRunning = "A Timesheet is running . Please complete it!";

        #endregion

        #region Invoice
        public static string msiBFPreviewInvoice = "Please input info and press OK to create Invoice!";
        public static string MSE_ExportError = "An error occurred,Can't export to excel!";
        public static string MSE_EmptyDataExport = "Data Export is empty!";
        public static string MSI_CancelInvoice = "Invoice is canceled successfully!";
        #endregion

        #region Contract
        public static string MSE_CTTotalFull = "You can't add more record for this contract. Please check your total contract fee";
        public static string MSE_CTTotalOver = "Total Fee In hour can't larger than the total fee!";
        public static string MSE_CTNotChangeFee = "Can't change Fee Type of this Contract because it had exported invoice !";
        #endregion

        #region SQL Insert, Update, Delete
        public static string MSE_SQLADD = "An error occurred,Can't add this record in the Database!";
        public static string MSE_SQLEDIT = "An error occurred,Can't modify this record in the Database!";
        public static string MSE_SQLDEL = "An error occurred,Can't delete this record in the Database!";
    
        public static string MSE_SQLNullData(string Data)
        {
            return Data + " not found in the Database!";
        }
        #endregion
  
    
    }
}