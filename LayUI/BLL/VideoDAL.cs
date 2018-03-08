using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace BLL
{
	/// <summary>
	/// 业务类实现--VideoDAL(填写类描述)
	/// </summary>
    public class VideoDAL
    {
        public VideoDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="VideoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(VideoMDL _VideoMDL)
		{
		    return Insert(null,_VideoMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="VideoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,VideoMDL _VideoMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.Video([ID],createtime,title,img,body,visitnum)
			VALUES (@id,@createtime,@title,@img,@body,@visitnum)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32, _VideoMDL.id));
			p.Add(db.CreateParameter("createtime",DbType.DateTime, _VideoMDL.createtime));
			p.Add(db.CreateParameter("title",DbType.String, _VideoMDL.title));
			p.Add(db.CreateParameter("img",DbType.String, _VideoMDL.img));
			p.Add(db.CreateParameter("body",DbType.String, _VideoMDL.body));
			p.Add(db.CreateParameter("visitnum",DbType.Int32, _VideoMDL.visitnum));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="VideoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(VideoMDL _VideoMDL)
		{
			return Update(null,_VideoMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="VideoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,VideoMDL _VideoMDL)
		{
			string sql = @"
			UPDATE dbo.Video
				SET	createtime = @createtime,title = @title,img = @img,body = @body,visitnum = @visitnum
			WHERE
				id = @id";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32, _VideoMDL.id));
			p.Add(db.CreateParameter("createtime",DbType.DateTime, _VideoMDL.createtime));
			p.Add(db.CreateParameter("title",DbType.String, _VideoMDL.title));
			p.Add(db.CreateParameter("img",DbType.String, _VideoMDL.img));
			p.Add(db.CreateParameter("body",DbType.String, _VideoMDL.body));
			p.Add(db.CreateParameter("visitnum",DbType.Int32, _VideoMDL.visitnum));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="VideoID">主键</param>
		/// <returns></returns>
        public static int Delete( int id )
		{
			return Delete(null, id);
		}
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public static int Delete(string[] primaryKeys)
        {
            return Delete(null,primaryKeys);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="VideoID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int id)
		{
			string sql=@"DELETE FROM dbo.Video WHERE id = @id";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32,id));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 根据主键批量删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, params string[] primaryKeys)
        {
            DBHelper db = new DBHelper();
            int res = 0;
            for (int i = 0; i < primaryKeys.Length; i++)
            {
                StringBuilder sql = new StringBuilder();
                List<SqlParameter> p = new List<SqlParameter>();
                sql.Append(@"DELETE FROM dbo.Video WHERE id = @id");
                p.Add(db.CreateParameter("id", DbType.Int32, primaryKeys[i]));
                res=db.GetExcuteNonQuery(tran, sql.ToString(), p.ToArray());
            }

            return res;
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="VideoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(VideoMDL _VideoMDL)
		{
			return Delete(null,_VideoMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="VideoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,VideoMDL _VideoMDL)
		{
			string sql=@"DELETE FROM dbo.Video WHERE id = @id";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32,_VideoMDL.id));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="VideoID">主键</param>
        public static VideoMDL GetObject( int id )
		{
			string sql=@"
			SELECT [ID],createtime,title,img,body,visitnum
			FROM dbo.Video
			WHERE id = @id";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, id));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                VideoMDL _VideoMDL = null;
                if (reader.Read())
                {
                    _VideoMDL = new VideoMDL();
					if (reader["id"] != DBNull.Value) _VideoMDL.id = Convert.ToInt32(reader["id"]);
					if (reader["createtime"] != DBNull.Value) _VideoMDL.createtime = Convert.ToDateTime(reader["createtime"]);
					if (reader["title"] != DBNull.Value) _VideoMDL.title = Convert.ToString(reader["title"]);
					if (reader["img"] != DBNull.Value) _VideoMDL.img = Convert.ToString(reader["img"]);
					if (reader["body"] != DBNull.Value) _VideoMDL.body = Convert.ToString(reader["body"]);
					if (reader["visitnum"] != DBNull.Value) _VideoMDL.visitnum = Convert.ToInt32(reader["visitnum"]);
                }
				reader.Close();
                db.Close();
                return _VideoMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Video", "*", filterWhereString, orderBy == "" ? " id" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Video", filterWhereString);
        }
        /// <summary>

        #region 自定义方法

        public static LayPadding<VideoMDL> GetVideoPageList(string fileds, string orderstr, int PageSize, int PageIndex, string strWhere)
        {
            LayPadding<VideoMDL> ulist = new LayPadding<VideoMDL>();
            var list = new List<VideoMDL>();

            string cond = string.IsNullOrEmpty(strWhere) ? "" : string.Format(" where {0}", strWhere);
            string sql = string.Format("SELECT * FROM ( SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS pos, {1} FROM  [video] {2}  ) AS sp WHERE pos BETWEEN {3} AND {4}", orderstr, fileds, cond, (((PageIndex - 1) * PageSize) + 1), PageSize * PageIndex);

            DBHelper db = new DBHelper();
            using (SqlDataReader reader = db.GetDataReader(sql))
            {
                VideoMDL _VideoMDL = null;
                while (reader.Read())
                {
                    _VideoMDL = new VideoMDL();
                    if (reader["id"] != DBNull.Value) _VideoMDL.id = Convert.ToInt32(reader["id"]);
                    if (reader["createtime"] != DBNull.Value) _VideoMDL.createtime = Convert.ToDateTime(reader["createtime"]);
                    if (reader["title"] != DBNull.Value) _VideoMDL.title = Convert.ToString(reader["title"]);
                    if (reader["img"] != DBNull.Value) _VideoMDL.img = Convert.ToString(reader["img"]);
                    if (reader["body"] != DBNull.Value) _VideoMDL.body = Convert.ToString(reader["body"]);
                    if (reader["visitnum"] != DBNull.Value) _VideoMDL.visitnum = Convert.ToInt32(reader["visitnum"]);
                    list.Add(_VideoMDL);
                }
                reader.Close();
                db.Close();
                ulist.data = list;
                return ulist;
            }

        }
        public static List<VideoMDL> GetListObject(int id)
        {
            string sql = @"
			SELECT [ID],createtime,title,img,body,visitnum
			FROM dbo.Video
			WHERE id = @id";

            DBHelper db = new DBHelper();
            List<VideoMDL> listmodel = new List<VideoMDL>();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, id));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                VideoMDL _VideoMDL = null;
                while (reader.Read())
                {
                    _VideoMDL = new VideoMDL();
                    if (reader["id"] != DBNull.Value) _VideoMDL.id = Convert.ToInt32(reader["id"]);
                    if (reader["createtime"] != DBNull.Value) _VideoMDL.createtime = Convert.ToDateTime(reader["createtime"]);
                    if (reader["title"] != DBNull.Value) _VideoMDL.title = Convert.ToString(reader["title"]);
                    if (reader["img"] != DBNull.Value) _VideoMDL.img = Convert.ToString(reader["img"]);
                    if (reader["body"] != DBNull.Value) _VideoMDL.body = Convert.ToString(reader["body"]);
                    if (reader["visitnum"] != DBNull.Value) _VideoMDL.visitnum = Convert.ToInt32(reader["visitnum"]);
                    listmodel.Add(_VideoMDL);
                }
                reader.Close();
                db.Close();
                return listmodel;
            }
        }
        public static List<VideoMDL> GetListPage()
        {
            string sql = @"
			SELECT [ID],createtime,title,img,body,visitnum
			FROM dbo.Video
			";

            DBHelper db = new DBHelper();
            List<VideoMDL> listmodel = new List<VideoMDL>();
            List<SqlParameter> p = new List<SqlParameter>();
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                VideoMDL _VideoMDL = null;
                while (reader.Read())
                {
                    _VideoMDL = new VideoMDL();
                    if (reader["id"] != DBNull.Value) _VideoMDL.id = Convert.ToInt32(reader["id"]);
                    if (reader["createtime"] != DBNull.Value) _VideoMDL.createtime = Convert.ToDateTime(reader["createtime"]);
                    if (reader["title"] != DBNull.Value) _VideoMDL.title = Convert.ToString(reader["title"]);
                    if (reader["img"] != DBNull.Value) _VideoMDL.img = Convert.ToString(reader["img"]);
                    if (reader["body"] != DBNull.Value) _VideoMDL.body = Convert.ToString(reader["body"]);
                    if (reader["visitnum"] != DBNull.Value) _VideoMDL.visitnum = Convert.ToInt32(reader["visitnum"]);
                    listmodel.Add(_VideoMDL);
                }
                reader.Close();
                db.Close();
                return listmodel;
            }
        }
        #endregion
    }
}
