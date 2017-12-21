using FEUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEDAL
{
    public class BaseDal<T> where T : class, new()
    {
        #region 根据传的字段名删除数据 或 在事务中执行，删除数据
        /// <summary>
        ///  根据传的字段名删除数据 或 在事务中执行，删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity, string fieldname, int id, SqlTransaction trans = null)
        {
            string sql = string.Format("delete from {0} where " + fieldname + "=@Id", entity.GetType().Name);
            SqlParameter pms = new SqlParameter("@Id", id);
            if (trans == null)
            {
                return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms) > 0;
            }
            else
            {
                return SQLHelp.ExecuteNonQuery(trans, CommandType.Text, sql, pms) > 0;
            }
        }
        #endregion
        #region 在事务中执行，返回执行增加、删除、修改操作后造成影响的行数
        /// <summary>
        /// 在事务中执行，返回执行增加、删除、修改操作后造成影响的行数
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <param name="cmdType">要执行的命令类型</param>
        /// <param name="pms">传入的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction trans, string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand(sql, trans.Connection);
            PrepareCommand(cmd, trans.Connection, trans, cmdType, pms);
            count = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return count;
        }
        #endregion

        #region 生成要执行的命令
        /// <summary>
        /// 生成要执行的命令
        /// </summary>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, SqlParameter[] pms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (pms != null)
            {
                cmd.Parameters.AddRange(pms);
            }
        }
        #endregion

        #region 增删改查的辅助方法
        /// <summary>
        /// 添加数据的辅助方法
        /// </summary>
        /// <param name="entity">要添加的实体对象</param>
        /// <param name="sql">返回添加的SQL语句</param>
        /// <returns>返回Parameter的集合</returns>
        public List<SqlParameter> DalAddHelp(T entity, out string sql)
        {
            Type ty = entity.GetType();
            StringBuilder strFirst = new StringBuilder();
            StringBuilder strSecond = new StringBuilder();
            PropertyInfo[] pros = ty.GetProperties();
            List<SqlParameter> pms = new List<SqlParameter>();
            for (int i = 0; i < pros.Count(); i++)
            {
                if (ty.GetProperties()[i].Name.ToUpper() != "ID" && pros[i].GetValue(entity, null) != null)
                {
                    if (pros[i].Name == "Key")
                    {
                        strFirst.Append("[Key]" + ",");
                    }
                    else
                    {
                        strFirst.Append(pros[i].Name + ",");
                    }
                    strSecond.Append("@" + pros[i].Name + ",");
                    SqlParameter para = new SqlParameter("@" + pros[i].Name, pros[i].GetValue(entity, null));
                    pms.Add(para);
                }
            }
            sql = string.Format("insert into {0}({1}) values({2});select @@identity", ty.Name, strFirst.ToString().TrimEnd(','), strSecond.ToString().TrimEnd(','));
            return pms;
        }

        /// <summary>
        /// 添加数据的辅助方法
        /// </summary>
        /// <param name="entity">要添加的实体对象</param>
        /// <param name="sql">返回添加的SQL语句</param>
        /// <returns>返回Parameter的集合</returns>
        public List<SqlParameter> DalAddHelp_IncludeId(T entity, out string sql)
        {
            Type ty = entity.GetType();
            StringBuilder strFirst = new StringBuilder();
            StringBuilder strSecond = new StringBuilder();
            PropertyInfo[] pros = ty.GetProperties();
            List<SqlParameter> pms = new List<SqlParameter>();
            for (int i = 0; i < pros.Count(); i++)
            {
                if ( pros[i].GetValue(entity, null) != null)
                {
                    if (pros[i].Name == "Key")
                    {
                        strFirst.Append("[Key]" + ",");
                    }
                    else
                    {
                        strFirst.Append(pros[i].Name + ",");
                    }
                    strSecond.Append("@" + pros[i].Name + ",");
                    SqlParameter para = new SqlParameter("@" + pros[i].Name, pros[i].GetValue(entity, null));
                    pms.Add(para);
                }
            }
            sql = string.Format("insert into {0}({1}) values({2});select @@identity", ty.Name, strFirst.ToString().TrimEnd(','), strSecond.ToString().TrimEnd(','));
            return pms;
        }

        /// <summary>
        /// 更新数据的辅助方法
        /// </summary>
        /// <param name="entity">要更新的实体对象</param>
        /// <param name="sql">返回更新的SQL语句</param>
        /// <returns>返回Parameter的集合</returns>
        public List<SqlParameter> DalUpdateHelp(T entity, out string sql)
        {

            Type ty = entity.GetType();
            StringBuilder strFirst = new StringBuilder();

            PropertyInfo[] pros = ty.GetProperties();
            List<SqlParameter> pms = new List<SqlParameter>();
            for (int i = 0; i < pros.Count(); i++)
            {
                if (ty.GetProperties()[i].Name.ToUpper() != "ID" && pros[i].GetValue(entity, null) != null)
                {
                    if (pros[i].Name == "Key")
                    {                       
                        strFirst.Append("[Key]" + "=@" + pros[i].Name + ",");
                    }
                    else
                    {
                        strFirst.Append(pros[i].Name + "=@" + pros[i].Name + ",");
                    }
                   
                }

                if (pros[i].GetValue(entity, null) != null)
                {
                    SqlParameter para = new SqlParameter("@" + pros[i].Name, pros[i].GetValue(entity, null));
                    pms.Add(para);
                }


            }
            sql = string.Format("update {0} set {1} where id=@id", ty.Name, strFirst.ToString().TrimEnd(','));
            return pms;
        }

        /// <summary>
        /// 删除数据的辅助方法
        /// </summary>
        /// <param name="pros">要删除对象的属性集合</param>
        /// <param name="entity">要删除的实体</param>
        /// <returns>返回实体属性和属性值得对应集合</returns>
        private Dictionary<string, object> DalDeleteHelp(PropertyInfo[] pros, T entity)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (PropertyInfo pro in pros)
            {
                dic.Add(pro.Name, pro.GetValue(entity, null));
            }
            return dic;
        }

        /// <summary>
        /// 批量删除数据的辅助方法
        /// </summary>
        /// <param name="entity">对象实体</param>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="ids">要删除的对象Id集合</param>
        /// <returns></returns>
        public List<SqlParameter> DalDeleteBatchHelp(T entity, out string sql, params int[] ids)
        {
            StringBuilder strFirst = new StringBuilder();
            List<SqlParameter> pms = new List<SqlParameter>();
            foreach (int item in ids)
            {
                strFirst.Append("@id" + item.ToString() + ",");
                pms.Add(new SqlParameter("@id" + item.ToString(), item));
            }
            sql = string.Format("delete from {0} where id in({1})", entity.GetType().Name, strFirst.ToString().TrimEnd(','));
            return pms;
        }
        #endregion

        #region 获得事务对象
        /// <summary>
        /// 获得事务对象
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return SQLHelp.BeginTransaction();
        }
        #endregion

        public virtual bool GetInfoById(T entity, int id)
        {
            string sql = "select * from " + entity + "where ID=" + id;
            return SQLHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text, null) > 0;
        }
        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(T entity)
        {
            string sql = string.Empty;
            List<SqlParameter> pms = DalAddHelp(entity, out sql);
            return Convert.ToInt32(SQLHelp.ExecuteScalar(sql, System.Data.CommandType.Text, pms.ToArray()));

        }

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add_IncludeId(T entity)
        {
            string sql = string.Empty;
            List<SqlParameter> pms = DalAddHelp_IncludeId(entity, out sql);
            return Convert.ToInt32(SQLHelp.ExecuteScalar(sql, System.Data.CommandType.Text, pms.ToArray()));

        }
        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(T entity, SqlTransaction trans = null)
        {
            string sql = string.Empty;
            List<SqlParameter> pms = DalAddHelp(entity, out sql);
            if (trans == null)
            {
                return Convert.ToInt32(SQLHelp.ExecuteScalar(sql, System.Data.CommandType.Text, pms.ToArray()));
            }
            else
            {
                return Convert.ToInt32(SQLHelp.ExecuteScalar(trans, sql, System.Data.CommandType.Text, pms.ToArray()));
            }
        }
        /// <summary>
        /// 更新单挑数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Update(T entity, SqlTransaction trans = null)
        {
            string sql = string.Empty;
            List<SqlParameter> pms = DalUpdateHelp(entity, out sql);
            if (trans == null)
                return SQLHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms.ToArray()) > 0;
            else
                return SQLHelp.ExecuteNonQuery(trans, System.Data.CommandType.Text, sql, pms.ToArray()) > 0;
        }

        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetEntityById(T entity, int id)
        {
            try
            {
                string sql = string.Format("select * from {0} where Id=@id", entity.GetType().Name);
                SqlParameter pms = new SqlParameter("@id", id);
                SqlDataReader reader = SQLHelp.ExecuteReader(sql, CommandType.Text, pms);
                if (reader.HasRows)
                {
                    PropertyInfo[] pros = entity.GetType().GetProperties();
                    while (reader.Read())
                    {

                        foreach (PropertyInfo item in pros)
                        {
                            item.SetValue(entity, reader.IsDBNull(reader.GetOrdinal(item.Name)) ? null : reader[item.Name]);
                        }
                    }
                }
                else { entity = null; }
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public virtual List<T> GetEntityListByField(T entity, string filed, string value)
        {
            List<T> tlist = new List<T>();
            string sql = string.Format("select * from {0} where {1}=@id", entity.GetType().Name, filed);
            SqlParameter pms = new SqlParameter("@id", value);
            SqlDataReader reader = SQLHelp.ExecuteReader(sql, CommandType.Text, pms);
            if (reader.HasRows)
            {
                PropertyInfo[] pros = entity.GetType().GetProperties();
                while (reader.Read())
                {
                    T newentity = new T();
                    foreach (PropertyInfo item in pros)
                    {
                        if (string.IsNullOrEmpty(reader[item.Name].SafeToString()))
                        {
                            // item.SetValue(newentity, "");

                        }
                        else
                        {
                            item.SetValue(newentity, reader[item.Name]);
                        }
                    }
                    tlist.Add(newentity);
                }
            }
            return tlist;
        }



        /// <summary>
        /// 批量伪删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual int DeleteBatchFalse(T entity, params int[] ids)
        {
            return 0;

        }


        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity, int id)
        {
            string sql = string.Format("delete from {0} where id=@Id", entity.GetType().Name);
            SqlParameter pms = new SqlParameter("@Id", id);
            return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms) > 0;

        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual int DeleteBatch(T entity, params string[] ids)
        {
            string sql = string.Empty;
            int length = ids.Length;
            int[] intids = new int[length];
            for (int i = 0; i < ids.Length; i++)
            {
                string item = ids[i];
                intids[i] = int.Parse(item);
            }
            List<SqlParameter> pms = DalDeleteBatchHelp(entity, out sql, intids);
            return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms.ToArray());
        }



        /// <summary>
        /// 根据条件获取所有数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="where">条件（例如：id>1）</param>
        /// <param name="order">排序（例如：createtime desc）</param>
        /// <returns></returns>
        public virtual DataTable GetData(T entity, string where, string order)
        {
            string sql = string.Format(@"select * from {0} ", entity.GetType().Name);
            StringBuilder strFirst = new StringBuilder();
            List<SqlParameter> pms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(where))
            {
                sql += string.Format(@" where {0}", where);
            }
            if (!string.IsNullOrEmpty(order))
            {
                sql += string.Format(@" Order by {0}", order);
            }
            DataTable dt = SQLHelp.ExecuteDataTable(sql, CommandType.Text, null);
            return dt;
        }


        public virtual DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")// string TableName, string strWhere, string orderby, int startIndex, int endIndex, SqlParameter[] parms4org)
        {
            RowCount = 0;
            SqlParameter[] parms4org = { };
            int StartIndex = 0;
            int EndIndex = 0;
            if (IsPage)
            {
                StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
            }
            try
            {
                string order = "";
                if (ht.ContainsKey("Order") && !string.IsNullOrWhiteSpace(Convert.ToString(ht["Order"]))) order = ht["Order"].ToString();
                DataTable dt = SQLHelp.GetListByPage((string)ht["TableName"], Where, order, StartIndex, EndIndex, IsPage, parms4org, out RowCount);

                //DataTable dt = SQLHelp.GetListByPage(ht, parms4org);
                return dt;
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;
                return null;
            }

        }

        /// <summary>
        /// 伪删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool DeleteFalse(T entity, int id)
        {
            string sql = string.Format("update {0} set IsDelete=1 where id=@Id", entity.GetType().Name);
            SqlParameter pms = new SqlParameter("@Id", id);
            return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms) > 0;
        }

        /// <summary>
        /// 伪删除批量数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual int DeleteBatchFalse(T entity, int isenable, params string[] ids)
        {
            string sql = string.Empty;
            StringBuilder strFirst = new StringBuilder();
            List<SqlParameter> pms = new List<SqlParameter>();
            foreach (string item in ids)
            {
                strFirst.Append("@id" + item.ToString() + ",");
                pms.Add(new SqlParameter("@id" + item.ToString(), item));
            }
            sql = string.Format("update {0} set IsDelete=@IsDelete where id in({1})", entity.GetType().Name, strFirst.ToString().TrimEnd(','));
            pms.Add(new SqlParameter("@IsDelete", isenable));
            return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms.ToArray());
        }
        #region 判断名称是否已存在
        /// <summary>
        /// 判断名称是否已存在
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldvalue">字段值</param>
        /// <param name="fieldname">字段名称</param>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public virtual bool IsNameExists(T entity, string fieldvalue, Int32 Id, string fieldname)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Format("select COUNT(1) from {0} where IsDelete=0 and {1}=@FieldValue ", entity.GetType().Name, fieldname);
            if (Id != 0)
            {
                sql += " and Id!=@Id ";
            }
            pms.Add(new SqlParameter("@FieldValue", fieldvalue));
            pms.Add(new SqlParameter("@Id", Id));
            object obj = SQLHelp.ExecuteScalar(sql, CommandType.Text, pms.ToArray());
            return int.Parse(obj.ToString()) > 0;
        }
        #endregion              
    }
}
