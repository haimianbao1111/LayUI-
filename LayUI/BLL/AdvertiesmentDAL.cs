using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace BLL
{
    /// <summary>
    /// 业务类实现--AdvertiesmentDAL(填写类描述)
    /// </summary>
    public class AdvertiesmentDAL
    {
        public AdvertiesmentDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="AdvertiesmentMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(AdvertiesmentMDL _AdvertiesmentMDL)
        {
            return Insert(null, _AdvertiesmentMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="AdvertiesmentMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, AdvertiesmentMDL _AdvertiesmentMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.Advertiesment([ID],createtime,path,[TYPE],link,[TIME],remark)
			VALUES (@id,@createtime,@path,@type,@link,@time,@remark)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, _AdvertiesmentMDL.id));
            p.Add(db.CreateParameter("createtime", DbType.DateTime, _AdvertiesmentMDL.createtime));
            p.Add(db.CreateParameter("path", DbType.String, _AdvertiesmentMDL.path));
            p.Add(db.CreateParameter("type", DbType.String, _AdvertiesmentMDL.type));
            p.Add(db.CreateParameter("link", DbType.String, _AdvertiesmentMDL.link));
            p.Add(db.CreateParameter("time", DbType.Int32, _AdvertiesmentMDL.time));
            p.Add(db.CreateParameter("remark", DbType.String, _AdvertiesmentMDL.remark));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="AdvertiesmentMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(AdvertiesmentMDL _AdvertiesmentMDL)
        {
            return Update(null, _AdvertiesmentMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="AdvertiesmentMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, AdvertiesmentMDL _AdvertiesmentMDL)
        {
            string sql = @"
			UPDATE dbo.Advertiesment
				SET	createtime = @createtime,path = @path,""TYPE"" = @type,link = @link,""TIME"" = @time,remark = @remark
			WHERE
				id = @id";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, _AdvertiesmentMDL.id));
            p.Add(db.CreateParameter("createtime", DbType.DateTime, _AdvertiesmentMDL.createtime));
            p.Add(db.CreateParameter("path", DbType.String, _AdvertiesmentMDL.path));
            p.Add(db.CreateParameter("type", DbType.String, _AdvertiesmentMDL.type));
            p.Add(db.CreateParameter("link", DbType.String, _AdvertiesmentMDL.link));
            p.Add(db.CreateParameter("time", DbType.Int32, _AdvertiesmentMDL.time));
            p.Add(db.CreateParameter("remark", DbType.String, _AdvertiesmentMDL.remark));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="AdvertiesmentID">主键</param>
        /// <returns></returns>
        public static int Delete(int id)
        {
            return Delete(null, id);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="AdvertiesmentID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int id)
        {
            string sql = @"DELETE FROM dbo.Advertiesment WHERE id = @id";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, id));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="AdvertiesmentMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(AdvertiesmentMDL _AdvertiesmentMDL)
        {
            return Delete(null, _AdvertiesmentMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="AdvertiesmentMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, AdvertiesmentMDL _AdvertiesmentMDL)
        {
            string sql = @"DELETE FROM dbo.Advertiesment WHERE id = @id";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, _AdvertiesmentMDL.id));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="AdvertiesmentID">主键</param>
        public static AdvertiesmentMDL GetObject(int id)
        {
            string sql = @"
			SELECT [ID],createtime,path,[TYPE],link,[TIME],remark
			FROM dbo.Advertiesment
			WHERE id = @id";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, id));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                AdvertiesmentMDL _AdvertiesmentMDL = null;
                if (reader.Read())
                {
                    _AdvertiesmentMDL = new AdvertiesmentMDL();
                    if (reader["id"] != DBNull.Value) _AdvertiesmentMDL.id = Convert.ToInt32(reader["id"]);
                    if (reader["createtime"] != DBNull.Value) _AdvertiesmentMDL.createtime = Convert.ToDateTime(reader["createtime"]);
                    if (reader["path"] != DBNull.Value) _AdvertiesmentMDL.path = Convert.ToString(reader["path"]);
                    if (reader["type"] != DBNull.Value) _AdvertiesmentMDL.type = Convert.ToString(reader["type"]);
                    if (reader["link"] != DBNull.Value) _AdvertiesmentMDL.link = Convert.ToString(reader["link"]);
                    if (reader["time"] != DBNull.Value) _AdvertiesmentMDL.time = Convert.ToInt32(reader["time"]);
                    if (reader["remark"] != DBNull.Value) _AdvertiesmentMDL.remark = Convert.ToString(reader["remark"]);
                }
                reader.Close();
                db.Close();
                return _AdvertiesmentMDL;
            }
        }
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Advertiesment", "*", filterWhereString, orderBy == "" ? " id" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Advertiesment", filterWhereString);
        }
        /// <summary>

        #region 自定义方法
        public static LayPadding<AdvertiesmentMDL> GetAdvertiesmentPageList(string fileds, string orderstr, int PageSize, int PageIndex, string strWhere)
        {
            LayPadding<AdvertiesmentMDL> ulist = new LayPadding<AdvertiesmentMDL>();
            var list = new List<AdvertiesmentMDL>();

            string cond = string.IsNullOrEmpty(strWhere) ? "" : string.Format(" where {0}", strWhere);
            string sql = string.Format("SELECT * FROM ( SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS pos, {1} FROM  [Advertiesment] {2}  ) AS sp WHERE pos BETWEEN {3} AND {4}", orderstr, fileds, cond, (((PageIndex - 1) * PageSize) + 1), PageSize * PageIndex);

            DBHelper db = new DBHelper();
            using (SqlDataReader reader = db.GetDataReader(sql))
            {
                AdvertiesmentMDL _AdvertiesmentMDL = null;
                while (reader.Read())
                {
                    _AdvertiesmentMDL = new AdvertiesmentMDL();
                    if (reader["id"] != DBNull.Value) _AdvertiesmentMDL.id = Convert.ToInt32(reader["id"]);
                    if (reader["createtime"] != DBNull.Value) _AdvertiesmentMDL.createtime = Convert.ToDateTime(reader["createtime"]);
                    if (reader["path"] != DBNull.Value) _AdvertiesmentMDL.path = Convert.ToString(reader["path"]);
                    if (reader["type"] != DBNull.Value) _AdvertiesmentMDL.type = Convert.ToString(reader["type"]);
                    if (reader["link"] != DBNull.Value) _AdvertiesmentMDL.link = Convert.ToString(reader["link"]);
                    if (reader["time"] != DBNull.Value) _AdvertiesmentMDL.time = Convert.ToInt32(reader["time"]);
                    if (reader["remark"] != DBNull.Value) _AdvertiesmentMDL.remark = Convert.ToString(reader["remark"]);
                }
                reader.Close();
                db.Close();
                ulist.data = list;
                ulist.count = SelectCount(cond);
                return ulist;
            }

        }
        #endregion
    }
}
