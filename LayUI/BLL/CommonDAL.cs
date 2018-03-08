using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;


namespace BLL
{
    public class CommonDAL
    {
        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int SelectRowCount(string tableName, string WhereString)
        {
            return SelectRowCount(null, tableName, WhereString);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int SelectRowCount(DbTransaction tran, string tableName, string WhereString)
        {
            string sql = "SELECT COUNT(*) FROM {0} WHERE  {1}";
            sql = string.Format(sql, tableName, WhereString);
            return (new DBHelper()).ExecuteScalar<int>(sql);
        }
        
        /// <summary>
        ///     获取任意表或试图记录
        /// </summary>
        /// <param name="tableName">表或试图名</param>
        /// <param name="WhereString">查询条件，格式：and xxx >1</param>
        /// <returns></returns>
        public static DataTable Select(string tableName, string WhereString)
        {
            string sql = "SELECT * FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, WhereString);
            return (new DBHelper()).GetFillData(sql);
        }

        #region LL添加

        /// <summary>
        ///     物理分页查询
        /// </summary>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTable(int startRowIndex, int maximumRows, string tableName, string clolumnList,
            string filterWhereString, string orderBy)
        {
            return GetDataTable(null, startRowIndex, maximumRows, tableName, clolumnList, filterWhereString, orderBy);
        }

        /// <summary>
        ///     物理分页查询
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，视图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTable(DbTransaction tran, int startRowIndex, int maximumRows, string tableName,
            string clolumnList, string filterWhereString, string orderBy)
        {
            //string sql = "SELECT * FROM( SELECT {0},row_number() over(order by {4} ) as RowNum FROM {2} WHERE 1=1 {3}) as t WHERE RowNum between {1} and {5}";
            //sql = string.Format(sql, clolumnList, Convert.ToString(startRowIndex + 1), tableName, filterWhereString, orderBy, Convert.ToString(startRowIndex + maximumRows));

            string sql =
                @"SELECT t2.*  FROM (SELECT t1.*,row_number() over(order by {3} ) as RowNum FROM (SELECT {0} FROM {1} WHERE 1=1 {2}) t1) t2 WHERE RowNum between {5} and {4}";
            sql = string.Format(sql, clolumnList, tableName, filterWhereString, orderBy,
                Convert.ToString(startRowIndex + maximumRows), Convert.ToString(startRowIndex + 1));

            var db = new DBHelper();
            DataTable dt = null;

            if (tran != null)
                dt = db.GetFillData(tran, sql);
            else
                dt = db.GetFillData(sql);
            return dt;
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int GetRowCount(string tableName, string clolumnList, string filterWhereString)
        {
            return GetRowCount(null, tableName, clolumnList, filterWhereString);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int GetRowCount(DbTransaction tran, string tableName, string clolumnList, string filterWhereString)
        {
            string sql = @"SELECT COUNT(*) FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, filterWhereString);
            var db = new DBHelper();
            if (tran != null)
                return db.ExecuteScalar<int>(tran, sql);
            return db.ExecuteScalar<int>(sql);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecSQL(string sql)
        {
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecSQL(DbTransaction tran, string sql)
        {
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(tran,sql) > 0 ? true : false;
        }
        /// <summary>
        /// 执行sql，返回结果集table
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>table</returns>
        public static DataTable GetDataTable(string sql)
        {
            return (new DBHelper()).GetFillData(sql);
        }

        /// <summary>
        /// 执行sql，返回结果集table
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>table</returns>
        public static DataTable GetDataTable(DbTransaction tran,string sql)
        {
            return (new DBHelper()).GetFillData(tran,sql);
        }
        #endregion LL添加

        #region 导出Excel

        private const string HtmlStartString = @"
            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html xmlns=""http://www.w3.org/1999/xhtml"">
            <head>
            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
            <style>
            td{mso-number-format:\@;border-width:0.1pt;border-style:solid;border-color:#CCCCCC;}
            </style>
            </head>
            <body>
            <table cellspacing=""0"" border=""0"" style=""width:100%;table-layout:auto;empty-cells:show;"">";

        private const string HtmlEndString = @"</table></body></html>";

        /// <summary>
        /// 导出可用excel打开的用html格式化的数据表格文件（伪excel，数据库直接导出，速度快）
        /// </summary>
        /// <param name="saveFileFullName">保存文件带路径的文件名</param>
        /// <param name="tableName">要导出的表名或试图名称</param>
        /// <param name="filterSql">过滤条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="headList">导出表格行头显示名称列表，用英文反斜杠\分隔</param>
        /// <param name="columnList">导出列名称列表，用英文反斜杠\分隔</param>
        public static void OutputXls(string saveFileFullName, string tableName, string filterSql, string orderBy, string headList, string columnList)
        {
            string sql = @"select {0} FROM {1} where {2} order by {3} for xml path('') ";
            StringBuilder sb = new StringBuilder();

            try
            {
                SqlDataReader sdr = new DBHelper().GetDataReader(string.Format(sql, FormatColumnWithHtml(columnList, false), tableName, filterSql, orderBy));

                while (sdr.Read())
                {
                    sb.Append(sdr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                
                //FileLog.WriteLog("导出数据失败", ex);
                throw ex;
            }
            finally
            {

            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileFullName, false, System.Text.Encoding.UTF8))
            {
                sw.Write(string.Format("{0}{1}{2}{3}"
                    , HtmlStartString
                     , FormatColumnWithHtml(headList, true)

                    , sb.ToString().Replace("&lt;", "<").Replace("&gt;", ">")
                    , HtmlEndString)
                    );
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 获取文件大小KB
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileSize(string filePath)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            if (file.Exists == false) return "0k";
            if (file.Length < 1024)
                return string.Format("{0} Byte", file.Length.ToString());
            else
                return string.Format("{0} KB", (file.Length / 1024).ToString());
        }


        /// <summary>
        /// 导出html形式伪excel前格式化输出列
        /// 格式如：<tr><td>列1</td><td>列2</td><td>列3</td></tr>
        /// </summary>
        /// <param name="columnsList">列集合（用英文竖杠|分隔）</param>
        /// <param name="ifHead">列头：true，数据行：false</param>
        /// <returns></returns>
        private static string FormatColumnWithHtml(string columnsList, bool ifHead)
        {
            StringBuilder sb = new StringBuilder();
            if (ifHead == true)//列头
            {
                sb.Append("<tr><th>");
                sb.Append(columnsList.Replace(@"\", "</th><th>"));
                sb.Append("</th></tr>");
            }
            else//数据行
            {
                sb.Append("'<tr><td>'+isnull(cast(");
                sb.Append(columnsList.Replace(@"\", " as varchar(max)),'')+'</td><td>'+isnull(cast("));
                sb.Append(" as varchar(max)),'')+'</td></tr>'");
            }
            return sb.ToString();
        }

        #endregion

        #region CK添加子查询分页
        /// <summary>
        /// 分页调用方法
        /// </summary>
        /// <param name="num">分页字段</param>
        /// <param name="sql">sql字符串</param>
        /// <param name="PageIndex">开始</param>
        /// <param name="PageSize">每页最大数</param>
        /// <returns></returns>
        public static DataTable GetPagingData(string num, string sql, int PageIndex, int PageSize)
        {
            string strsql = string.Format(@"select * from (select row_number()over(order by {0}) as RowNum,* from ({1}) a) t where RowNum between {2} and {3}", num, sql, Convert.ToString(PageIndex + 1), Convert.ToString(PageIndex + PageSize));
            return CommonDAL.GetDataTable(strsql);
        }
        #endregion
    }
}