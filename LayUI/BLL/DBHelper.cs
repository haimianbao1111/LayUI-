using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Configuration;


namespace BLL
{
    /// <summary>
    ///     Sql数据库操作帮助类
    /// </summary>
    public class DBHelper : IDisposable
    {
        //定义这个类要使用的全局变量
        private static string constr;

        private SqlDataAdapter adapter;

        private SqlCommand cmd;
        private SqlConnection conn;
        private SqlDataReader dr;

        public DBHelper()
        {
            constr = WebConfigurationManager.ConnectionStrings["connStr"].ToString();
        }

        /// <summary>
        ///     数据库连接属性,连接配置文件的字符串为"ConnectionString"
        /// </summary>
        public SqlConnection Connectionstrings
        {
            get
            {
                ConnectionStringSettingsCollection connectionStrings =
                    WebConfigurationManager.ConnectionStrings;

                constr = connectionStrings["connStr"].ToString();

                //DotNet默认打开数据库连接池
                if (conn == null)
                {
                    conn = new SqlConnection(constr);
                }

                if (conn.State == ConnectionState.Open)
                {
                    return conn;
                }
                conn.Open();
                return conn;
            }
        }

        // 先做几个处理 ,该类实现了IDisposable接口，

        // 自动调用非托管堆中释放资源，在由GC自动清理。

        public void Dispose()
        {
            Close();
            cmd.Dispose();
            dr.Dispose();
            conn.Dispose();
        }

        /// <summary>
        ///     取消 Command 执行，并关闭 DataReader 对象和数据连接
        /// </summary>
        public void Close()
        {
            cmd.Cancel();

            if (dr != null && !dr.IsClosed)
                dr.Close();
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        /// <summary>
        ///     打开连接
        /// </summary>
        public void Open()
        {
            if (conn == null) conn = new SqlConnection(constr);
            if (conn.State != ConnectionState.Open) conn.Open();
        }

        /// <summary>
        ///     开启事务处理
        /// </summary>
        public DbTransaction BeginTransaction()
        {
            Open();
            var tran = new DbTransaction(conn.BeginTransaction());
            return tran;
        }
      
        /// <summary>
        ///     创建一个 SQL 参数，主要实现SqlParameter[] 参数列表
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">类型</param>
        /// <param name="value">值</param>
        /// <returns>返回创建完成的参数</returns>
        public SqlParameter CreateParameter(string parameterName, SqlDbType dbType, object value)
        {
            var result = new SqlParameter(parameterName, dbType);
            result.Value = (value == null ? DBNull.Value : value);
            return result;
        }

        public SqlParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            var result = new SqlParameter(parameterName, dbType);
            result.Value = (value == null ? DBNull.Value : value);
            return result;
        }

        /// <summary>
        ///     单向操作，主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(string cmdTxt)
        {
            return GetExcuteNonQuery(cmdTxt, null);
        }

        /// <summary>
        ///     带参数化的　主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="cmdTxt">带参数列表的sql语句</param>
        /// <param name="pars">要传入的参数列表</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        /// <summary>
        ///     单向操作，主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(DbTransaction tran, string cmdTxt)
        {
            return GetExcuteNonQuery(tran, cmdTxt, null);
        }

        /// <summary>
        ///     带参数化的　主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdTxt">带参数列表的sql语句</param>
        /// <param name="pars">要传入的参数列表</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                if (pars != null) cmd.Parameters.AddRange(pars);
                if (tran != null) cmd.Transaction = tran.Transaction;

                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        /// <summary>
        ///     对连接执行 Transact-SQL 语句或者存储过程并返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype"></param>
        /// <param name="pars">参数</param>
        /// <returns>受影响的行数</returns>
        public int GetExcuteNonQuery(string cmdText, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdText, Connectionstrings))
            {
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public int GetExcuteNonQuery(DbTransaction tran, string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                cmd.CommandType = cmdtype;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }
       
        public int GetExcuteNonQuery(DbTransaction tran, int cmdTimeOut, string cmdTxt, CommandType cmdtype,
            params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                cmd.CommandType = cmdtype;
                cmd.CommandTimeout = cmdTimeOut;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="tran"></param>
        /// <param name="cmdText">SQL 语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>结果集中第一行的第一列或空引用</returns>
        public T ExecuteScalar<T>(DbTransaction tran, string cmdText, params SqlParameter[] pars)
        {
            return ExecuteScalar<T>(tran, cmdText, CommandType.Text, pars);
        }
        
        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="cmdText">SQL 语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>结果集中第一行的第一列或空引用</returns>
        public T ExecuteScalar<T>(string cmdText, params SqlParameter[] pars)
        {
            return ExecuteScalar<T>(null, cmdText, pars);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">参数类型[范型]</typeparam>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(string cmdText)
        {
            return ExecuteScalar<T>(cmdText, null);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">参数类型[范型]</typeparam>
        /// <param name="tran"></param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(DbTransaction tran, string cmdText)
        {
            return ExecuteScalar<T>(tran, cmdText, null);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdTxt">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype">决定是存储过程还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            return ExecuteScalar<T>(null, cmdTxt, cmdtype, pars);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="cmdTxt">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype">决定是存储过程还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(DbTransaction tran, string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                cmd.CommandType = cmdtype;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                T result;

                try
                {
                    result = (T)cmd.ExecuteScalar();
                }
                catch (Exception)
                {
                    Close();
                    throw;
                }

                if (tran == null) Close();
                return result;
            }
        }

        /// <summary>
        ///     将 cmdText 发送到 System.Data.SqlClient.SqlCommand.Connection，并使用 System.Data.CommandBehavior 值之一生成一个 DataReader
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns>一个 DataReader 对象</returns>
        public SqlDataReader GetDataReader(string cmdTxt)
        {
            return GetDataReader(cmdTxt, null);
        }

        /// <summary>
        ///     将 cmdText 发送到 System.Data.SqlClient.SqlCommand.Connection，并使用 System.Data.CommandBehavior 值之一生成一个 DataReader
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <param name="pars">参数</param>
        /// <returns>一个 DataReader 对象</returns>
        public SqlDataReader GetDataReader(string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                if (pars != null) cmd.Parameters.AddRange(pars);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
        }

        public SqlDataReader GetDataReader(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                dr = cmd.ExecuteReader();
                return dr;
            }
        }

        /// <summary>
        ///     将 cmdText 发送到 System.Data.SqlClient.SqlCommand.Connection，并使用 System.Data.CommandBehavior 值之一生成一个 DataReader
        /// </summary>
        /// <param name="cmdTxt">存储过程名称或者sql语句</param>
        /// <param name="cmdtype">决定是存储过程类型还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回一个DataReader对象</returns>
        public SqlDataReader GetDataReader(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
        }

        /// <summary>
        ///     做数据绑定显示作用，一般绑定的是数据查看控件
        /// </summary>
        /// <param name="cmdTxt">sql语句</param>
        public DataTable GetFillData(string cmdTxt)
        {
            return GetFillData(cmdTxt, null);
        }

        public DataTable GetFillData(DbTransaction tran, string cmdTxt)
        {
            return GetFillData(tran, cmdTxt, null);
        }

        /// <summary>
        ///     做数据绑定显示作用，一般绑定的是数据查看控件
        /// </summary>
        /// <param name="cmdTxt">带参数的sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回是一个数据表</returns>
        public DataTable GetFillData(string cmdTxt, params SqlParameter[] pars)
        {
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                    conn.Close();
                }
            }

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetFillData(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
            }

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        ///     做数据绑定显示作用，一般绑定的是数据查看控件
        /// </summary>
        /// <param name="cmdTxt">存储过程名称或者sql语句</param>
        /// <param name="cmdtype">决定是存储过程类型还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetFillData(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            var ds = GetDataSet(cmdTxt, cmdtype, pars);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 获取一个数据集
        /// </summary>
        /// <param name="cmdTxt">存储过程名称或者sql语句</param>
        /// <param name="cmdtype">决定是存储过程类型还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回一个DataSet</returns>
        public DataSet GetDataSet(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                    conn.Close();
                }
            }
            return ds;
        }

        /// <summary>
        /// 使用SqlBulkCopy批量插入数据
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="dt">源数据</param>
        public void SqlBulkCopyByDatatable(string tableName, DataTable dt)
        {
            using (var sqlbulkcopy = new SqlBulkCopy(Connectionstrings,SqlBulkCopyOptions.UseInternalTransaction,null))
            {
                sqlbulkcopy.BatchSize = 1000;
                sqlbulkcopy.BulkCopyTimeout = 5000;
                sqlbulkcopy.DestinationTableName = tableName;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                }
                sqlbulkcopy.WriteToServer(dt);
            }
        }
        #region 扩展
        public int ExecuteNonQuery(DbCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
           
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        #endregion

        #region 增加参数
        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            using (cmd = new SqlCommand(storedProcedure, Connectionstrings))
            {
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd;
            }
        }
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }
        #endregion
    }
}