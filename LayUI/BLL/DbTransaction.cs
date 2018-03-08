using System;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    /// <summary>
    ///     事务处理类--TransactionBL
    /// </summary>
    public class DbTransaction : IDisposable
    {
        private readonly SqlConnection conn;
        private readonly SqlTransaction tran;

        /// <summary>
        ///     事务
        /// </summary>
        public DbTransaction(SqlTransaction Transaction)
        {
            tran = Transaction;
            conn = Transaction.Connection;
        }

        public SqlTransaction Transaction
        {
            get { return tran; }
        }

        /// <summary>
        ///     数据库连接
        /// </summary>
        public SqlConnection Connection
        {
            get { return conn; }
        }

        public void Dispose()
        {
            Close();
            tran.Dispose();
            conn.Dispose();
        }

        // 关闭事务
        private void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        /// <summary>
        ///     提交事务
        /// </summary>
        public void Commit()
        {
            tran.Commit();
            Close();
        }

        /// <summary>
        ///     回滚事务
        /// </summary>
        public void Rollback()
        {
            tran.Rollback();
            Close();
        }
    }
}