using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary
{
    public class QueryLibrary : DBConnection
    {
        #region >- Properties -<

        public string ID = string.Empty;
        public string TableName = string.Empty;

        #endregion

        #region >- Contructure -<

        public QueryLibrary():base()
        {
            this.TableName = this.GetType().Name.ToLower();
            this.ID = this.GetType().Name.ToLower();
        }
        public QueryLibrary(string TableName,string id):base()
        {
            this.TableName = TableName;
            this.ID = id;
        }
        #endregion

        #region >- Set View -<

        private string getRelations(string r)
        {
            string result = string.Empty;
            switch(r)
            {
                case "i":
                    result="inner join";
                    break;
                case"l":
                    result ="left join";
                    break;
            }
            return result;
        }
        public void View(string[] table, string[] relations, string[] IDMFields, string[] IDJFields, string[] selectintable)
        {
            string s= string.Empty;
            string t = string.Empty;
            
            s = " (SELECT t.*,";
            for (int i = 0; i < table.Length; i++)
            {
                s += selectintable[i];
                t = getRelations(relations[i]) + " " + table[i] + " t" + i.ToString() + " on t." + IDMFields[i] + " = t" + i.ToString() + "." + IDJFields[i] + " " ;
            }
            s += " FROM " + this.TableName+" t ";
            s += t + ") tbv";
            this.TableName = s;
        }
        #endregion

        #region >- Check Exist Data -<

        /// <summary>
        /// Example: Select top 1 <Fields[0]> From <table> where Fields[0] = Datas[0] or Fields[1] = Datas[1] or ...
        /// </summary>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        public bool CheckExistDataWithOR(string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand();
            string StrSql = "Select top 1 " + Fields[0] + " from " + TableName + " where ";
            for (int i = 0; i < Fields.Length; i++)
            {
                if (i > 0)
                    StrSql += " or ";
                StrSql += Fields[i] + "=@" + Fields[i];
                cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
            }
            cmd.CommandText = StrSql;
            DataTable dt = GetData(cmd);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// Example: Select top 1 <Fields[0]> From <table> where Fields[0] = Datas[0] and Fields[1] = Datas[1] and ...
        /// </summary>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        public bool CheckExistDataWithAND(string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand();
            string StrSql = "Select top 1 " + Fields[0] + " from " + TableName + " where ";
            for (int i = 0; i < Fields.Length; i++)
            {
                if (i > 0)
                    StrSql += " and ";
                StrSql += Fields[i] + "=@" + Fields[i];
                cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
            }
            cmd.CommandText = StrSql;
            DataTable dt = GetData(cmd);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// Example: Select top 1 * From <table> where ID in (<NotIDs>)
        /// </summary>
        /// <param name="IDs"></param>
        /// <param name="Field"></param>
        /// <returns></returns>
        public bool CheckExistIDIn(string IDs, string Field)
        {
            CheckValidIds(IDs);
            string StrSQL = "select top 1 * from " + TableName + " where " + Field + " in (" + IDs + ")";
            SqlCommand cmd = new SqlCommand(StrSQL);
            DataTable dt = GetData(cmd);
            return dt.Rows.Count > 0;
        }
        public bool CheckIDBeingUsed(string Field, string Value)
        {
            string StrCondition = " AND " + Field + " = '" + Value + "'";
            string StrSQL = "select top 1 * from " + TableName + " where  1 = 1 " + StrCondition;
            SqlCommand cmd = new SqlCommand(StrSQL);
            DataTable dt = GetData(cmd);
            return dt.Rows.Count > 0;
        }
        public bool CheckIDBeingUsed(string[] Field,string Value)
        {
            string StrCondition = string.Empty;
            for (int i = 0; i < Field.Length; i++)
                StrCondition +=" AND " + Field[i] + " = '" + Value + "'";
            string StrSQL = "select top 1 * from " + TableName + " where  1 = 1 " + StrCondition;
            SqlCommand cmd = new SqlCommand(StrSQL);
            DataTable dt = GetData(cmd);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// Example: Select top 1 <Fields[0]> From <table> where ID not in (<NotIDs>) and Fields[0] = Datas[0] and Fields[1] = Datas[1] and ...
        /// </summary>
        /// <param name="NotID"></param>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        public bool CheckExistMultiDataWithNotID(string NotIDs, string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand();
            string StrSql = "Select top 1 " + Fields[0] + " from " + TableName + " where " + this.ID + " not in (" + NotIDs + ")";
            for (int i = 0; i < Fields.Length; i++)
            {
                StrSql += " and " + Fields[i] + "=@" + Fields[i];
                cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
            }
            cmd.CommandText = StrSql;
            DataTable dt = GetData(cmd);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// Example: Select top 1 * From <table> where <Condition>
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public bool CheckExistDataWithCondition(string Condition)
        {
            string StrSql = "Select top 1 * from " + TableName + " where " + Condition;
            DataTable dt = GetData(StrSql);
            return dt.Rows.Count > 0;
        }

        #endregion

        #region >- Get Data -<

        /// <summary>
        /// Example: Select ID,Name From Table where ID in (ID1,ID2)
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public DataTable GetDataByIDIn(string Select, string IDs)
        {
            //CheckValidIds(IDs);
            String StrSql = "Select " + Select + " from " + TableName + " where " + this.ID + " in (" + IDs + ")";
            SqlCommand cmd = new SqlCommand(StrSql);
            return GetData(cmd);
        }
        public const string SQLDateTimeFormat = "CONVERT(NVARCHAR(10),{0},112)";
        public DataTable GetDataWithDateBy(string Select,string DFields, object DDatas,string Condition)
        {
            SqlCommand cmd = new SqlCommand();

            String StrSql = "Select * from " + TableName;
            if (DDatas != null)
            {
                StrSql += " where " + Condition;
                StrSql += " and " + String.Format(SQLDateTimeFormat,DFields) + "=@" + DFields;
                cmd.Parameters.Add(new SqlParameter(DFields, DDatas));
            }
            cmd.CommandText = StrSql;
            return GetData(cmd);
        }
        public DataTable GetDataWithDateBy(string Select, string DFields, object DDatas, string Condition,string StrOrderby)
        {
            SqlCommand cmd = new SqlCommand();

            String StrSql = "Select * from " + TableName;
            if (DDatas != null)
            {
                StrSql += " where 1 = 1 " + Condition;
                StrSql += " and " + String.Format(SQLDateTimeFormat, DFields) + "=@" + DFields;
                cmd.Parameters.Add(new SqlParameter(DFields, DDatas));
            }
            StrSql += (StrOrderby.Length > 0) ? " order by " + StrOrderby : StrSql;
            cmd.CommandText = StrSql;
            return GetData(cmd);
        }
        /// <summary>
        /// Example AND: Select <select> From <table> where Field[0]=Datas[0] and Field[1]=Datas[1] and ...
        /// Example OR : Select <select> From <table> where Field[0] like N'%' + Datas[0] + '%' or Field[1] like N'%' + Datas[1] + '%' or ...
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        /// 
        public DataTable GetDataBy(string Select, int Top, string OrAnd, string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand();
            string SELECTTOP = (Top == 0) ? "" : " TOP " + Top.ToString() + " ";

            String StrSql = "Select " + SELECTTOP + Select + " from " + TableName;
            if (Datas != null)
            {
                StrSql += " where 1 = 1 ";
                for (int i = 0; i < Fields.Length; i++)
                {
                    if (Datas[i] != null)
                    {
                        if (OrAnd.ToLower().Equals("and"))
                        {

                            StrSql += " and " + Fields[i] + "=@" + Fields[i];
                            cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                        }
                        else
                        {
                            StrSql += " or " + Fields[i] + " like '%'+@" + Fields[i] + "+'%'";
                            cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[0]));
                        }
                    }
                }
            }
            cmd.CommandText = StrSql;
            return GetData(cmd);
        }

        public DataTable GetDataBy(string Select, int Top, string OrAnd, string[] Fields, object[] Datas, string FieldGroup, string FieldOrder)
        {
                SqlCommand cmd = new SqlCommand();
                string SELECTTOP = (Top == 0) ? "" : " TOP " + Top.ToString() + " ";

                String StrSql = "Select " + SELECTTOP + Select + " from " + TableName;
                if (Datas != null)
                {
                    StrSql += " where 1 = 1 ";
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (Datas[i] != null)
                        {
                            if (OrAnd.ToLower().Equals("and"))
                            {

                                StrSql += " and " + Fields[i] + "=@" + Fields[i];
                                cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                            }
                            else
                            {
                                StrSql += " or " + Fields[i] + " like '%'+@" + Fields[i] + "+'%'";
                                cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[0]));
                            }
                        }
                    }
                }
                string GroupBy = (FieldGroup.Length > 0) ? " Group By " + FieldGroup : "";
                StrSql = (GroupBy.Length > 0) ? StrSql + GroupBy : StrSql;
                string OrderBy = (FieldOrder.Length > 0) ? " order by " + FieldOrder : "";
                StrSql = (OrderBy.Length > 0) ? StrSql + OrderBy : StrSql;
                cmd.CommandText = StrSql;
                return GetData(cmd);
        }
        public DataTable GetDataBy(string Select, int Top, string OrAnd, string[] Fields, object[] Datas,string FieldOrder)
        {
            SqlCommand cmd = new SqlCommand();
            string SELECTTOP = (Top == 0) ? "" : " TOP " + Top.ToString() + " ";

            String StrSql = "Select " + SELECTTOP + Select + " from " + TableName;
            if (Datas != null)
            {
                StrSql += " where 1 = 1 ";
                for (int i = 0; i < Fields.Length; i++)
                {
                    if (Datas[i] != null)
                    {
                        if (OrAnd.ToLower().Equals("and"))
                        {

                            StrSql += " and " + Fields[i] + "=@" + Fields[i];
                            cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                        }
                        else
                        {
                            StrSql += " or " + Fields[i] + " like '%'+@" + Fields[i] + "+'%'";
                            cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[0]));
                        }
                    }
                }
            }
            string OrderBy = (FieldOrder.Length > 0) ? " order by " + FieldOrder : "";
            StrSql = (OrderBy.Length > 0) ? StrSql + OrderBy : StrSql;
            cmd.CommandText = StrSql;
            return GetData(cmd);
        }

        /// <summary>
        /// Example: Select * from <table> where <condition>
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public DataTable GetDataBy(string Select,int Top,string Condition)
        {
            string SELECTTOP = (Top == 0) ? "" : " TOP " + Top.ToString() + " ";
            String StrSql = "Select " + SELECTTOP + Select + " from " + TableName + " where " + Condition;
            return GetData(StrSql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="Condition = (and - or - andlike - orlike)"></param>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <param name="TextOrder"></param>
        /// <returns></returns>
        public DataTable GetDataBy(string Select,int Top, string[] Condition, string[] Fields, object[] Datas, string TextOrder)
        {
            SqlCommand cmd = new SqlCommand();
            string SELECTTOP = (Top == 0) ? "" :" TOP "+ Top.ToString()+" ";
            String StrSql = "Select " + SELECTTOP + Select + " from " + TableName;
            StringBuilder SbCondition = new StringBuilder();

            if (Datas != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    if (Datas[i] != null)
                    {
                        switch (Condition[i].ToLower())
                        {
                            case "and":
                                if (i > 0)
                                    SbCondition.Append(" and ");
                                SbCondition.Append(Fields[i] + "=@" + Fields[i]);
                                break;
                            case "or":
                                if (i > 0)
                                    SbCondition.Append(" or ");
                                SbCondition.Append(Fields[i] + "=@" + Fields[i]);
                                break;
                            case "andlike":
                                if (i > 0)
                                    SbCondition.Append(" and ");
                                SbCondition.Append(Fields[i] + " like '%'+@" + Fields[i] + '%');
                                break;
                            case "orlike":
                                if (i > 0)
                                    SbCondition.Append(" or ");
                                SbCondition.Append(Fields[i] + " like '%'+@" + Fields[i] + '%');
                                break;
                        }
                        cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                    }
                }
            }
            if (SbCondition.Length > 0)
                StrSql += " where " + SbCondition.ToString();
            if (!string.IsNullOrEmpty(TextOrder))
                StrSql += " order by " + TextOrder;
            cmd.CommandText = StrSql;
            return GetData(cmd);
        }


        public DataTable GetAll()
        {
            return GetData("Select * from " + TableName);
        }
        public DataTable GetAll(string Select,string TextOrder)
        {
            return GetData("Select " + Select + " from " + TableName + " order by " + TextOrder);
        }
        public DataTable GetAllByID(string ValueID)
        {
            return GetData("Select * from " + TableName + " where " + this.ID + "= '" + ValueID + "'");
        }
        public DataTable GetDataByID(string StrSelect,int ValueID)
        {
            return GetData("Select " + StrSelect + " from " + TableName + " where " + this.ID + "= '" + ValueID + "'");
        }

        #endregion

        #region >- Insert -<

        public int Insert(string[] Fields, object[] Datas)
        {
            string StrInsert = "";
            StrInsert += "Insert into {0}({1}) values({2}) set @Identity=@@Identity";
            SqlCommand cmd = new SqlCommand();
            SqlParameter Identity = new SqlParameter("@Identity", 0);
            Identity.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Identity);
            string StrField = string.Empty;
            string StrValue = string.Empty;
            for (int i = 0; i < Fields.Length; i++)
            {
                if ((Datas[i] != null)&&(Fields[i] != null))
                {
                    if(StrField.Trim().Length>0)
                        StrField += ",";
                    if (StrValue.Trim().Length > 0)
                        StrValue += ",";
                   
                    StrField += Fields[i];
                    StrValue += "@" + Fields[i];
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }

            cmd.CommandText = string.Format(StrInsert, TableName, StrField, StrValue);
            int result = ExecuteNonQuery(cmd);
            if (result > 0 && Identity.Value != DBNull.Value)
                result = Convert.ToInt32(Identity.Value);
            return result;
        }

        #endregion

        #region >- Update -<

        public int Update(int ValueID, string[] Fields, object[] Datas)
        {
            string StrUpdate = "";
            StrUpdate += "Update " + TableName + " set {0} where " + ID + "=" + ValueID;
            SqlCommand cmd = new SqlCommand();
            string strset = string.Empty;
            for (int i = 0; i < Fields.Length; i++)
            {
                if ((Datas[i] != null) && (Fields[i] != null))
                {
                    if (strset.Trim().Length>0)
                        strset += ",";
                    strset += Fields[i] + "=@" + Fields[i];
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }
            
            cmd.CommandText = string.Format(StrUpdate, strset);
            return ExecuteNonQuery(cmd);
        }
        public int Update(string[] Fields, object[] Datas,string Condition)
        {
            string StrUpdate = "";
            StrUpdate += "Update " + TableName + " set {0} where " + Condition;
            SqlCommand cmd = new SqlCommand();
            string strset = "";
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Datas[i] != null)
                {
                    if (i > 0)
                        strset += ",";
                    strset += Fields[i] + "=@" + Fields[i];
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }
            
            cmd.CommandText = string.Format(StrUpdate, strset);
            return ExecuteNonQuery(cmd);
        }
        public int Update(string[] Fields, object[] Datas, string[] FieldsCondition, object[] DatasCondition)
        {
            string StrUpdate = "";
            StrUpdate+="Update " + TableName + " set {0}";
            SqlCommand cmd = new SqlCommand();
            string strset = "";
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Datas[i] != null)
                {
                    if (i > 0)
                        strset += ",";
                    strset += Fields[i] + "=@" + Fields[i];
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }
            if (FieldsCondition != null)
            {
                StrUpdate += " where ";
                for (int i = 0; i < FieldsCondition.Length; i++)
                {
                    if (i > 0)
                        StrUpdate += " and ";
                    StrUpdate += FieldsCondition[i] + "=@" + FieldsCondition[i];
                    cmd.Parameters.Add(new SqlParameter(FieldsCondition[i], DatasCondition[i]));
                }
            }
            
            cmd.CommandText = string.Format(StrUpdate, strset);
            return ExecuteNonQuery(cmd);
        }
        public int UpdateMulti(string IDs, string[] Fields, object[] Datas)
        {
            CheckValidIds(IDs);
            string StrUpdate = "";
            StrUpdate+="Update " + TableName + " set {0} where " + ID + " in (" + IDs + ")";
            SqlCommand cmd = new SqlCommand();
            string strset = "";
            for (int i = 0; i < Fields.Length; i++)
            {
                if (i > 0)
                    strset += ",";
                strset += Fields[i] + "=@" + Fields[i];
                cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
            }
            
            cmd.CommandText = string.Format(StrUpdate, strset);
            return ExecuteNonQuery(cmd);
        }
        public int UpdateMulti(string IDs, string Condition, string[] Fields, object[] Datas)
        {
            CheckValidIds(IDs);
            string StrUpdate = "";
            StrUpdate+="Update " + TableName + " set {0} where " + ID + " in (" + IDs + ")";
            if (!string.IsNullOrEmpty(Condition))
                StrUpdate += " and " + Condition;
            SqlCommand cmd = new SqlCommand();
            string strset = "";
            for (int i = 0; i < Fields.Length; i++)
            {
                if (i > 0)
                    strset += ",";
                strset += Fields[i] + "=@" + Fields[i];
                cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
            }
            
            cmd.CommandText = string.Format(StrUpdate, strset);
            return ExecuteNonQuery(cmd);
        }
        public int UpdateProperties(int ValueID, string FieldUpdate)
        {

            string StrSQL = "Update " + this.TableName + " set " + FieldUpdate + "=" + FieldUpdate + "+1 where " + this.ID + "=" + ValueID;
            return ExecuteNonQuery(StrSQL);
        }
        #endregion

        #region >- Delete -<

        public int Delete(string ID)
        {
            string strDelete = "";
            strDelete += "Delete " + TableName + " where " + this.ID + "=" + ID;
            
            return ExecuteNonQuery(strDelete);
        }
        public int Delete(string[] IDFields, object[] IDDatas)
        {
            string strDelete = "";
            string condition = string.Empty;
            strDelete += "Delete " + TableName + " where 1=1 ";
            if (IDFields.Length > 0)
            {
                for (int i = 0; i < IDFields.Length; i++)
                {
                    if (IDDatas[i] != null)
                        condition += " and " + IDFields[i] + " = '" + IDDatas[i].ToString() + "'";
                }
            }
            strDelete = (condition.Length > 0) ? strDelete + condition : strDelete;
            
            return ExecuteNonQuery(strDelete);
        }
        public int Delete(string ID, string Condition)
        {
            string strDelete = "";
            string StrCondition = string.Empty;
            if (!string.IsNullOrEmpty(Condition))
                StrCondition = " and " + Condition;
            strDelete += "Delete " + TableName + " where " + this.ID + "=" + ID + StrCondition;
            
            return ExecuteNonQuery(strDelete);
        }
       
        public int DeleteMulti(string Condition, string FieldIn, string Values, bool IsNotIn)
        {
            CheckValidIds(Values);
            string strDelete = "";
            strDelete += "Delete " + TableName + " where " + FieldIn + " " + (IsNotIn ? "not in" : "in") + " (" + Values + ")";
            if (!string.IsNullOrEmpty(Condition))
                strDelete += " and " + Condition;
            
            return ExecuteNonQuery(strDelete);
        }
        public int DeleteMulti(string IDs)
        {
            CheckValidIds(IDs);
            return DeleteMulti(null, ID, IDs, false);
        }
        public int DeleteMulti(string IDs, string Condition)
        {
            CheckValidIds(IDs);
            return DeleteMulti(Condition, ID, IDs, false);
        }
        #endregion

        #region >- Excute Command -<

        public bool HasResultInCommand(string Condition, string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand("select top 1 * from " + this.TableName + " where " + Condition);
            if (Fields != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }
            return GetData(cmd).Rows.Count > 0;
        }
        public DataTable ExcuteSelect(string StrSQL, string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand(StrSQL);
            if (Fields != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }
            return GetData(cmd);
        }
        public int ExcuteCommand(string StrSQL, string[] Fields, object[] Datas, bool IsStore)
        {
            SqlCommand cmd = new SqlCommand(StrSQL);
            if (IsStore)
                cmd.CommandType = CommandType.StoredProcedure;
            if (Fields != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }
            return ExecuteNonQuery(cmd);
        }

        #endregion

        #region >- Search -<

        /// <summary>
        /// Example: Select * from <table> where Fields[0]=Datas[0] and Fields[1]=Datas[1] and ... and (SFields[0] like N'%SDatas[0]%' or (SFields[1] like N'%SDatas[1]%' or ...)
        /// </summary>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <param name="SFields"></param>
        /// <param name="SDatas"></param>
        /// <param name="OrderBy"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public DataTable Search(string Select,string[] Fields, object[] Datas, string[] SFields, string Keyword, string OrderBy, string OrderType)
        {
            SqlCommand cmd = new SqlCommand();
            String SbCondition = "";
            String StrSql = "Select "+Select+" from " + TableName;
            StrSql += " where 1 = 1";
            if (Datas != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    if ((Datas[i] != null)&&(Datas[i].ToString() != "0"))
                    {
                        StrSql += " and " + Fields[i] + "=@" + Fields[i];
                        cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                    }
                }
            }
            if (Keyword != null)
            {
                for (int i = 0; i < SFields.Length; i++)
                {
                    if (i > 0)
                        SbCondition += " or ";
                    SbCondition += SFields[i] + " like N'%'+ @" + SFields[i] + " + '%'";
                    cmd.Parameters.Add(new SqlParameter(SFields[i], Keyword));
                }
                if(SFields.Length>1)
                    SbCondition = "(" + SbCondition + ")";
            }

            string SbOrderBy = "";
            if (OrderBy != null)
                SbOrderBy = " ORDER BY " + OrderBy + " " + OrderType+" ";
            if (SbCondition.Length > 0)
                SbCondition = " and " + SbCondition;
            cmd.CommandText = StrSql + SbCondition + SbOrderBy;
            return GetData(cmd);
        }
        public DataTable Search(string Select, string[] Fields, object[] Datas, string[] SFields, string Keyword,string Condition, string OrderBy, string OrderType)
        {
            SqlCommand cmd = new SqlCommand();
            String SbCondition = "";
            String StrSql = "Select " + Select + " from " + TableName;
            StrSql += " where 1 = 1";
            if (Datas != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    if ((Datas[i] != null) && (Datas[i].ToString() != "0"))
                    {
                        StrSql += " and " + Fields[i] + "=@" + Fields[i];
                        cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                    }
                }
            }
            if (Keyword != null)
            {
                for (int i = 0; i < SFields.Length; i++)
                {
                    if (i > 0)
                        SbCondition += " or ";
                    SbCondition += SFields[i] + " like N'%'+ @" + SFields[i] + " + '%'";
                    cmd.Parameters.Add(new SqlParameter(SFields[i], Keyword));
                }
                if (SFields.Length > 1)
                    SbCondition = "(" + SbCondition + ")";
            }

            string SbOrderBy = "";
            if (OrderBy != null)
                SbOrderBy = " ORDER BY " + OrderBy + " " + OrderType + " ";
            if (SbCondition.Length > 0)
                SbCondition = " and " + SbCondition;
            cmd.CommandText = StrSql + SbCondition+ Condition + SbOrderBy;
            return GetData(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromDate is Format (yyyyMMdd HHmmss)"></param>
        /// <param name="ToDate is Format (yyyyMMdd HHmmss)"></param>
        /// <param name="Fields"></param>
        /// <param name="Datas"></param>
        /// <param name="SFields"></param>
        /// <param name="SDatas"></param>
        /// <param name="OrderBy"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public DataTable SearchWithDate(string Select,string[] DFields, string[] DDatasF, string[] DDatasT, string[] Fields, object[] Datas, string[] SFields, string Keyword, string OrderBy, string OrderType)
        {
            SqlCommand cmd = new SqlCommand();
            String SbCondition = "";
            String StrSql = "Select " + Select + " from " + TableName;
            StrSql += " where 1 = 1";
            if (Datas != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    if ((Datas[i] == null) || (Datas[i].ToString() == "0"))
                        continue;
                    StrSql += " and " + Fields[i] + "=@" + Fields[i];
                    cmd.Parameters.Add(new SqlParameter(Fields[i], Datas[i]));
                }
            }
            if (SFields != null)
            {
                for (int i = 0; i < SFields.Length; i++)
                {
                    if (i > 0)
                        SbCondition += " or ";
                    SbCondition += SFields[i] + " like N'%'+ @" + SFields[i] + " + '%'";
                    cmd.Parameters.Add(new SqlParameter(SFields[i],Keyword));
                }
                if (SFields.Length > 1)
                    SbCondition = "(" + SbCondition + ")";
            }
            StringBuilder SbDate = new StringBuilder();
            if ((DDatasF != null)&&(DDatasT !=null))
            {
                for (int i = 0; i < DFields.Length; i++)
                {
                    if ((DDatasF[i] == null) || (DDatasF[i].ToString() == "0"))
                        continue;
                    if (i > 0)
                        SbDate.Append(" and ");
                    SbDate.Append(String.Format(SQLDateTimeFormat, DFields[i]) + " between '" + DDatasF[i] + "' and '" + DDatasT[i] + "'");
                }
            }
       
            string SbOrderBy = "";
            if (OrderBy != null)
                SbOrderBy = " ORDER BY " + OrderBy + " " + OrderType + " ";
            if (SbCondition.Length > 0)
                SbCondition = " and " + SbCondition;
            
            cmd.CommandText = StrSql + SbCondition + " and " + SbDate.ToString() + SbOrderBy;
            return GetData(cmd);
        }

        #endregion

        #region >- Get Properties -<

        public object GetProperties(int ID, string Properties)
        {
            string StrSqL = "Select " + Properties + " from " + TableName + " where " + this.ID + "=" + ID;
            DataTable dt = GetData(StrSqL);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][Properties];
            return string.Empty;
        }

        #endregion
    }
}
