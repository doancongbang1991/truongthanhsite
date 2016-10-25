using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Entities;
using Ext.Net;

namespace TMT.License.Core
{
    /// <summary>
    /// Summary description for UserCommon
    /// </summary>
    public class UserCommon
    {
        public UserCommon()
        { }
        //
        // TODO: Add constructor logic here
        //
        #region >- PrimaryKey -<

        private static string PrimaryKey = "tA1M1nh@#";
        #endregion

        #region >- CookieName -<
        private static string Cookie_UserInfo = "TMTTS_USERINFO";
        private static string Cookie_Themes = "TMTTS_THEMES";
        #endregion

        #region >- Page -<
        public static string Web_MainPage = "~/MainPage.aspx";
        public static string Web_ErrorPage = "~/ErrorPage.aspx";
        public static string Web_LoginPage = "~/Login.aspx";
        public static string Web_DefaultPage = "~/Default.aspx";

        public static string System_UserInfoManager = "~/System/UserInfoManager.aspx";
        public static string System_UserInfoDetails = "~/System/UserInfoDetails.aspx";
        public static string System_UserProfile = "~/System/UserProfile.aspx";
        public static string System_UserGroupManager = "~/System/UserGroupManager.aspx";
        public static string System_UserGroupDetails = "~/System/UserGroupDetails.aspx";

        public static string TT_AboutManager = "~/About/AboutManager.aspx";

        public static string TT_ConstructionManager = "~/Construction/ConManager.aspx";

        public static string TT_Contact = "~/Contact/Contact.aspx";

        public static string TT_Cost = "~/Cost/Cost.aspx";

        public static string TT_FooterManager = "~/Footer/FooterManager.aspx";

        public static string TT_ArchManager = "~/Arch/ArchManager.aspx";

        public static string TT_FurManager = "~/Furniture/FurManager.aspx";

        public static string TT_ProjectManager = "~/Project/ProjectManager.aspx";

        public static string TT_SiteManager = "~/Site/SiteManager.aspx";
        public static string TT_MessManager = "~/Message/MessageManager.aspx";
        public static string TestManager = "~/TestManager.aspx";
        public static string TestDetails = "~/TestDetails.aspx";

        #endregion

        #region >- Session Name -<
        public static string SS_URLReturn = "SS_URLReturn";
        public static string suserRight = "tmtiTS_userRight";
        public static string SS_Message = "tmtiTS_message";
        public static string spreValue = "tmtiTS_preValue";
        public static string spreValueUserInfo = "tmtiTS_preValueuf";
        public static string spreValueclient = "tmtiTS_preValuecm";
        public static string spreValueMT = "tmtiTS_preValueMT";
        public static string spreValueTask = "tmtiTS_preValueT";
        public static string spreValueMatterList = "tmtiTS_preValueMTS";
        public static string spreValueTaskGroup = "tmtiTS_spreValueTaskGroup";
        public static string spreValuePosition = "tmtiTS_spreValuePosition";
        public static string spreValueInvoice = "tmtiTS_spreValueInvoice";
        public static string DataExport = "tmtiTS_DataExport";
        public static string spreValueCompany = "tmtiTS_spreValueCompany";



        #endregion

        #region >- Parameter -<
        public static string Param_URLReturn = "URLReturn";
        public static string Param_ID = "IDREF";
        public static string Param_KeyWord = "Keyword";
        public static string FormatDetailsPage(string PageDetails, string ID, string Keyword)
        {
            return PageDetails + "?" + Param_ID + "=" + ID + "&" + Param_KeyWord + "=" + Keyword;
        }
        public static string FormatPageWithParameter(string ManagerPage)
        {
            string QueryString = GetQueryString();
            return ManagerPage + QueryString;
        }
        public static string FormatKeyword(object[] Keyword)
        {
            string tmp = string.Empty;
            for (int i = 0; i < Keyword.Length; i++)
            {
                if (i > 0)
                    tmp += "\r\n";
                tmp += Keyword[i];
            }
            return Encrypt(tmp);
        }
        public static string GetQueryString()
        {
            string Value = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(HttpContext.Current.Request.Url.Query))
                    Value = HttpContext.Current.Request.Url.Query;
            }
            catch { Value = string.Empty; }
            return Value;
        }
        public static string GetValueParam_URLReturn()
        {
            string Value = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString[Param_URLReturn]))
                    Value = HttpContext.Current.Request.QueryString[Param_URLReturn].ToString();
            }
            catch { Value = string.Empty; }
            return Value;
        }
        public static string GetValueParam_ID()
        {
            string Value = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString[Param_ID]))
                    Value = HttpContext.Current.Request.QueryString[Param_ID].ToString();
            }
            catch { Value = string.Empty; }
            return Value;
        }
        public static string[] GetValueParam_KeyWord()
        {
            string[] oResult = null;
            try
            {
                string tmp = string.Empty;
                if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString[Param_KeyWord]))
                    tmp = HttpContext.Current.Request.QueryString[Param_KeyWord].ToString();
                tmp = Decrypt(tmp);
                oResult = Regex.Split(tmp, "\r\n");
            }
            catch { oResult = null; }
            return oResult;
        }

        #endregion

        #region >- Common -<

        public static string Encrypt(string toEncrypt)
        {

            string keyconfig = "b1zL@w1t1Me$heet";// "T@1m1nhL1c3nsE"; //ConfigurationManager.AppSettings["SECONDKEY"].ToString().Trim();
            string key = keyconfig + PrimaryKey;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string toDecrypt)
        {
            toDecrypt = toDecrypt.Replace(" ", "+");
            string keyconfig = "b1zL@w1t1Me$heet"; //ConfigurationManager.AppSettings["SECONDKEY"].ToString().Trim();
            string key = keyconfig + PrimaryKey;

            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string ToUpperFisrtChar(string str)
        {
            string[] tmp = str.Split(' ');
            string fnstr = "";
            for (int c = 0; c < tmp.Length; c++)
            {
                if (tmp[c].Length > 0)
                    fnstr = tmp[c].Substring(0, 1) + tmp[c].Substring(1, tmp[c].Length - 1) + " ";
            }
            return fnstr.Trim();
        }

        #endregion

        #region >- Theme -<

        public static Theme GetCurrentTheme()
        {
            string CurTheme = string.Empty;
            if (HttpContext.Current.Request.Cookies[Cookie_Themes] == null)
                CurTheme = "Triton";
            else
                CurTheme = HttpContext.Current.Request.Cookies[Cookie_Themes].Value;
            Theme objTheme = (Theme)Enum.Parse(typeof(Theme), CurTheme);
            return objTheme;
        }
        public static void SetCookieTheme(string Name, bool IsNew)
        {
            HttpCookie tcookie = new HttpCookie(Cookie_Themes);
            tcookie.Value = Name;
            tcookie.Expires.AddDays(365);
            if (IsNew)
                HttpContext.Current.Response.Cookies.Add(tcookie);
            else
                HttpContext.Current.Response.Cookies.Set(tcookie);
        }

        #endregion

        #region >- Cookie User -<

        public static void SetCookieUserInfo(string[] value)
        {
            string[] fields = new string[] { "uid", "gruid" };
            HttpCookie ucookie = new HttpCookie(Cookie_UserInfo);
            int count = fields.Length;
            for (int f = 0; f < count; f++)
                ucookie[fields[f].ToString()] = value[f].ToString();
            ucookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(ucookie);
        }
        public static void ClearCookieUserInfo()
        {
            HttpCookie ucookie = HttpContext.Current.Request.Cookies[Cookie_UserInfo];
            if (ucookie != null)
            {
                ucookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(ucookie);
            }
        }
        public static string GetCookie_UIDForLogin()
        {
            string sResult = string.Empty;
            HttpCookie ucookie = HttpContext.Current.Request.Cookies[Cookie_UserInfo];
            if (ucookie == null)
                return null;
            sResult = ucookie["uid"].ToString();
            return sResult;
        }
        public static string GetCookie_UID()
        {
            string sResult = string.Empty;
            HttpCookie ucookie = HttpContext.Current.Request.Cookies[Cookie_UserInfo];
            if (ucookie == null)
            {
                SetSession(SS_Message, "Your session has expired. Please login again!");
                HttpContext.Current.Response.Redirect(Web_ErrorPage, true);
            }
            sResult = ucookie["uid"].ToString();
            return sResult;
        }
        public static string GetCookie_GRPID()
        {
            string sResult = string.Empty;
            HttpCookie ucookie = HttpContext.Current.Request.Cookies[Cookie_UserInfo];
            if (ucookie == null)
            {
                SetSession(SS_Message, "Your session has expired. Please login again!");
                HttpContext.Current.Response.Redirect(Web_ErrorPage, true);
            }
            sResult = ucookie["gruid"].ToString();
            return sResult;
        }

        #endregion

        #region >- Session -<
        public static void SetSession(string Name, object value)
        {
            HttpContext.Current.Session[Name] = value;
        }
        public static object GetSession(string Name)
        {
            object objResutl = null;
            if (HttpContext.Current.Session[Name] != null)
                objResutl = HttpContext.Current.Session[Name];
            return objResutl;
        }

        #endregion

        #region >- Convert -<
        public static decimal ToDecimal(object d)
        {
            try
            {
                return Convert.ToDecimal(d);
            }
            catch
            { return 0; }
        }

        public static double ToDouble(object d)
        {
            try
            {
                return Convert.ToDouble(d);
            }
            catch
            { return 0; }
        }
        public static int ToInt(object i)
        {
            try
            {
                return Convert.ToInt32(i);
            }
            catch
            { return 0; }
        }
        public static bool ToBoolean(object value)
        {
            return Convert.ToBoolean((int) ToInt(value));
        }
        public static DateTime VerifyDateTime(object value)
        {
            DateTime dt;
            try { dt = (DateTime)value; }
            catch { dt = DateTime.MinValue; }
            return dt;
        }
        public static string ToMoneyString(object value)
        {
            try
            {
                double d = ToDouble(value);
                if (d.ToString().Length == 1)
                    return d.ToString();
                return String.Format("{0:0,0}", d);
            }
            catch { return "0"; }
        }

        #endregion




        public static string formatDateToExcel(DateTime dt)
        {
            return string.Format("{0:yyyy-MM-dd}", dt);
        }

        public static string formatMoney(double d)
        {
            return String.Format("{0:0,0.00}", d);
        }

        private static string DayOfWeek_VN(string DOW)
        {
            switch (DOW.ToLower())
            {
                case "monday":
                    return "Thứ 2";
                case "tuesday":
                    return "Thứ 3";
                case "wednesday":
                    return "Thứ 4";
                case "thursday":
                    return "Thứ 5";
                case "friday":
                    return "Thứ 6";
                case "saturday":
                    return "Thứ 7";
                default:
                    return "Chủ Nhật";
            }
        }



        #region >- DateTime Format -<

        public const string DateFormat = "dd-MM-yyyy";
        public const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        public const string TimeFormat = "HH:mm:ss";

        public const string DateCompareSQLFormat = "yyyyMMdd";

        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static string ToDateCompareSQLString(DateTime dt)
        {
            return dt.ToString((string) DateCompareSQLFormat);
        }

        public static string ToTimeString(DateTime dt)
        {
            return dt.ToString((string) TimeFormat);
        }
        public static string ToDateString(DateTime dt)
        {
            string m = dt.ToString((string) DateFormat);
            return m;
        }
        public static string ToDateTimeString(DateTime dt)
        {
            return dt.ToString((string) DateTimeFormat);
        }
        public static DateTime ToDate(string stringdt)
        {
            try
            {
                DateTime dt = DateTime.MinValue;
                dt = DateTime.ParseExact(stringdt, DateFormat, null);
                return dt;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ToDateTime(string stringdt)
        {
            try
            {
                DateTime dt = DateTime.MinValue;
                dt = DateTime.ParseExact(stringdt, DateTimeFormat, null);
                return dt;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        #endregion

        #region >- MessageBox.Show -<
        public const string INFORMATION = "INFORMATION";
        public const string ERROR = "ERROR MESSAGE";
        public const string WARNING = "WARNING";

        public static void MsbShow(string TEXT, string TYPE)
        {
            Ext.Net.MessageBoxConfig mbcf = new Ext.Net.MessageBoxConfig();
            Ext.Net.MessageBox mb = new Ext.Net.MessageBox();
            mbcf.Buttons = Ext.Net.MessageBox.Button.OK;
            switch (TYPE)
            {
                case INFORMATION:
                    mbcf.Title = INFORMATION;
                    mbcf.Icon = Ext.Net.MessageBox.Icon.INFO;
                    break;
                case ERROR:
                    mbcf.Title = ERROR;
                    mbcf.Icon = Ext.Net.MessageBox.Icon.ERROR;
                    break;
                case WARNING:
                    mbcf.Title = WARNING;
                    mbcf.Icon = Ext.Net.MessageBox.Icon.WARNING;
                    mbcf.Buttons = Ext.Net.MessageBox.Button.OK;
                    break;
            }
            mbcf.Message = TEXT;
            mb.Configure(mbcf);
            mb.Render();
            mb.Show();

        }
        public static void MsbNotify(string title, string str, int hide)
        {
            NotificationConfig notify = new NotificationConfig();
            notify.Title = title;
            notify.Html = str;
            notify.Icon = Icon.Bug;
            notify.HideDelay = hide;
            Notification.Show(notify);
        }
        #endregion

        #region >- HasValue Control -<
        public static bool HasValue(TextField txt)
        {
            bool bResult = false;
            try
            {
                if (txt.Text.Trim().Length > 0)
                    bResult = true;
            }
            catch { bResult = false; }
            return bResult;
        }
        //public static bool HasValue(TriggerField trg)
        //{
        //    bool bResult = false;
        //    try
        //    {
        //        if (trg.Text.Trim().Length > 0)
        //            bResult = true;
        //    }
        //    catch { bResult = false; }
        //    return bResult;
        //}
        public static bool HasValue(ComboBox cbb)
        {
            bool bResult = false;
            try
            {
                if (cbb.SelectedItem.Value.ToString() != "0")
                    bResult = true;
            }
            catch { bResult = false; }
            return bResult;
        }
        public static bool HasValue(DateField dat)
        {
            bool bResult = false;
            try
            {
                DateTime dtime = ToDate(dat.RawText);
                if (dtime == dat.MinDate)
                    bResult = false;
                else bResult = true;
            }
            catch { bResult = false; }
            return bResult;
        }
        public static bool HasValue(NumberField num)
        {
            bool bResult = false;
            try
            {
                if (UserCommon.ToDecimal(num.Value) == 0)
                    bResult = false;
                else bResult = true;
            }
            catch { bResult = false; }
            return bResult;
        }
        public static object[] GetRecordIDInGridPanel(GridPanel GrdSelected, bool IsRowSelecttionMode)
        {
            try
            {
                object[] Result = null;
                RowSelectionModel rRowSelectMode;
                CheckboxSelectionModel rCheckSelectMode;
                SelectedRow row;
                if (IsRowSelecttionMode)
                {
                    rRowSelectMode = GrdSelected.GetSelectionModel() as RowSelectionModel;
                    if (rRowSelectMode.SelectedRows.Count > 0)
                    {
                        Result = new object[rRowSelectMode.SelectedRows.Count];
                        for (int i = 0; i < rRowSelectMode.SelectedRows.Count; i++)
                        {
                            row = rRowSelectMode.SelectedRows[i];
                            string ID = row.RecordID.ToString();
                            Result[i] = ID;
                        }
                    }
                }
                else
                {
                    rCheckSelectMode = GrdSelected.GetSelectionModel() as CheckboxSelectionModel;
                    if (rCheckSelectMode.SelectedRows.Count > 0)
                    {
                        Result = new object[rCheckSelectMode.SelectedRows.Count];
                        for (int i = 0; i < rCheckSelectMode.SelectedRows.Count; i++)
                        {
                            row = rCheckSelectMode.SelectedRows[i];
                            string ID = row.RecordID.ToString();
                            Result[i] = ID; ;
                        }
                    }
                }
                return Result;
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region >- ReadOnly Coltrol -<
        public static void ReadOnlyControl(ComboBox cb, bool RO)
        {
            if (!RO)
            {
                cb.ReadOnly = false;
                cb.RemoveCls("xReadOnly");
            }
            else
            {

                cb.AddCls("xReadOnly");
                cb.ReadOnly = true;
            }
        }
        //public static void ReadOnlyControl(TriggerField trg, bool RO)
        //{
        //    if (!RO)
        //    {
        //        trg.ReadOnly = false;
        //        trg.RemoveCls("xReadOnly");
        //    }
        //    else
        //    {

        //        trg.AddCls("xReadOnly");
        //        trg.ReadOnly = true;
        //    }
        //}
        public static void ReadOnlyControl(Checkbox trg, bool RO)
        {
            if (!RO)
            {
                trg.ReadOnly = false;
                trg.RemoveCls("xReadOnly");
            }
            else
            {

                trg.AddCls("xReadOnly");
                trg.ReadOnly = true;
            }
        }
        public static void ReadOnlyControl(NumberField num, bool RO)
        {
            if (!RO)
            {
                num.ReadOnly = false;
                num.RemoveCls("xReadOnly");
            }
            else
            {

                num.AddCls("xReadOnly");
                num.ReadOnly = true;
            }
        }
        public static void ReadOnlyControl(TextField txt, bool RO)
        {
            if (!RO)
            {
                txt.ReadOnly = false;
                txt.RemoveCls("xReadOnly");
            }
            else
            {

                txt.AddCls("xReadOnly");
                txt.ReadOnly = true;
            }
        }
        public static void ReadOnlyControl(TextArea txt, bool RO)
        {
            if (!RO)
            {
                txt.ReadOnly = false;
                txt.RemoveCls("xReadOnly");
            }
            else
            {
                txt.AddCls("xReadOnly");
                txt.ReadOnly = true;
            }
        }
        public static void ReadOnlyControl(DateField dt, bool RO)
        {
            if (!RO)
            {
                dt.ReadOnly = false;
                dt.RemoveCls("xReadOnly");
            }
            else
            {

                dt.AddCls("xReadOnly");
                dt.ReadOnly = true;
            }
        }

        #endregion

        #region >- Format Control -<
    
        public static void DateFieldFormat(DateField df)
        {
            df.InvalidText = String.Format((string) "DateField must be in the format {0}", (object) UserCommon.DateFormat);
            df.Format = UserCommon.DateFormat;
            df.MinDate = DateTime.MinValue;
            df.IndicatorTip = "Input format: " + UserCommon.DateFormat;
            ToolTip tp = new ToolTip();
            tp.Html = "Input format: " + UserCommon.DateFormat;
            df.ToolTips.Add(tp);
        }
        public static void AddItemOptionInCombobox(ComboBox cbb, Store str)
        {
            string handler = "";
            handler += @"#{" + cbb.ID + "}.insertItem(0, '- - Option - -', 0);#{" + cbb.ID + "}.setValue(#{" + cbb.ID + "}.store.getAt(0).get('" + cbb.ValueField.ToString() + "'));";
            str.Listeners.Load.Handler = handler;

        }
        public static void AddItemFilterInCombobox(ComboBox cbb, Store str)
        {
            string handler = "";
            handler += @"#{" + cbb.ID + "}.insertItem(0, '- - All Filter - -', 0);#{" + cbb.ID + "}.setValue(#{" + cbb.ID + "}.store.getAt(0).get('" + cbb.ValueField.ToString() + "'));";
            str.Listeners.Load.Handler = handler;

        }
        public static void SetValidate(Button bt, object[] Controlname, string[] ExceptionMessage)
        {
            string handler = "";
            for (int cn = 0; cn < Controlname.Length; cn++)
            {
                if (cn > 0)
                    handler += ";";
                handler += @"if(!#{" + Controlname[cn].ToString() + @"}.validate()) {
                            Ext.MessageBox.show({
                           title:'"+ ERROR + @"',
                           msg: '" + ExceptionMessage[cn] + @"',
                           buttons: Ext.MessageBox.OK,
                           icon: Ext.MessageBox.ERROR});            
                        return false; 
                    }";
            }
            bt.Listeners.Click.Handler = handler;
        }
        public static List<StatusEntities> ListStatus()
        {
            List<StatusEntities> Datas = new List<StatusEntities>();
            StatusEntities obj = new StatusEntities();
            obj.ID = "False";
            obj.Name = "Processing";
            Datas.Add(obj);

            obj = new StatusEntities();
            obj.ID = "True";
            obj.Name = "Completed";
            Datas.Add(obj);
            return Datas;
        }
        public static List<StatusEntities> ListGroupBy()
        {
            List<StatusEntities> Datas = new List<StatusEntities>();
            StatusEntities obj = new StatusEntities();
            obj.ID = "1";
            obj.Name = "Group Client";
            Datas.Add(obj);

            obj = new StatusEntities();
            obj.ID = "2";
            obj.Name = "Group Matter";
            Datas.Add(obj);

            obj = new StatusEntities();
            obj.ID = "3";
            obj.Name = "Group Task";
            Datas.Add(obj);

            obj = new StatusEntities();
            obj.ID = "4";
            obj.Name = "Group Date";
            Datas.Add(obj);
            return Datas;
        }

        #endregion

        #region >- SetValue Control -<
        public static void SetValueControl(ComboBox cb, string Value)
        {
            Value = ToInt(Value).ToString();
            if (Value == "0")
            {
                cb.SelectedItems.Clear();
                ListItem item = new Ext.Net.ListItem();
                item.Value = "0";
                item.Mode = ParameterMode.Raw;
                cb.SelectedItems.Add(item);
                cb.UpdateSelectedItems();
            }
            else
            {
                cb.SelectedItems.Clear();
                ListItem item = new Ext.Net.ListItem();
                item.Value = Value;
                item.Mode = ParameterMode.Raw;
                cb.SelectedItems.Add(item);
                cb.UpdateSelectedItems();
            }
        }
        public static void SetValueControl(DateField ct, object Value)
        {
            DateTime DT = VerifyDateTime(Value);
            ct.Text = (DT==DateTime.MinValue)? "": ToDateString(DT);
            ct.RawText = (DT == DateTime.MinValue) ? "" : ToDateString(DT);
            ct.SelectedDate = DT;
        }
        public static void SetValueControl(TimeField ct, object Value)
        {
            DateTime DT = VerifyDateTime(Value);
            if (DT == DateTime.MinValue)
            {
                ct.Text = "00:00:00";
                ct.RawText = "00:00:00";
            }
            else
            {
                ct.Text = ToTimeString(DT);
                ct.RawText = ToTimeString(DT);
            }
        }
        #endregion

        #region >- User Define -<

        public static void VerifyPage()
        {
            if (GetCookie_UID() == null)
            {
                SetSession(SS_Message, Message.MSE_UIExpired);
                HttpContext.Current.Response.Redirect(Web_ErrorPage, true);
            }
        }
        public static string[] GetValueFromJson(string JSon,string[] Key)
        {
            string[] tmp = null;
            int countKey = 0;
            Dictionary<string, string>[] DicJson = JSON.Deserialize<Dictionary<string, string>[]>(JSon);
            if (DicJson.Length > 0)
            {
                tmp = new string[Key.Length];
                foreach (Dictionary<string, string> row in DicJson)
                {
                    foreach (KeyValuePair<string, string> keyValuePair in row)
                    {
                        for (int i = 0; i < Key.Length; i++)
                        {
                            if (keyValuePair.Key.Trim().ToUpper().Equals(Key[i].ToUpper()))
                            {
                                tmp[i] = (keyValuePair.Value == null) ? "" : keyValuePair.Value.Trim();
                                countKey++;
                                break;
                            }
                        }
                        if (countKey == Key.Length)
                            break;
                    }
                    if (countKey == Key.Length)
                        break;
                }
            }
            return tmp;
        }
        #endregion

    }
}