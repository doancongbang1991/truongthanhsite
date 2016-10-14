using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlLibrary
{
    public class DBConnection
    {
        private SqlConnection con;
        public DBConnection()
        {
            con = new SqlConnection(getStringConnnect());
            OpenConnect();
        }
        private String getStringConnnect()
        {
            String strCon = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            return strCon;
        }
        private void OpenConnect()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            OpenConnect();
            int number = -1;
            try
            {
                cmd.Connection = con;
                number = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return number;
        }
        public int ExecuteNonQuery(String strSQL)
        {
            OpenConnect();
            int number = -1;
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Connection = con;
                number = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return number;
        }
        public DataTable GetData(SqlCommand cmd)
        {
            OpenConnect();
            DataTable dt = null;
            try
            {
                cmd.Connection = con;
                dt = new DataTable();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception(cmd.CommandText);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetData(String strSQL)
        {
            OpenConnect();
             DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlDataAdapter adap = new SqlDataAdapter(strSQL, con);
                adap.Fill(dt);
            }
           
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public void CheckValidIds(string IDs)
        {
            System.Text.RegularExpressions.MatchCollection m = System.Text.RegularExpressions.Regex.Matches(IDs, "^[0-9,]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (m.Count <= 0)
                throw new Exception("ID not valid !");
        }
    }
}