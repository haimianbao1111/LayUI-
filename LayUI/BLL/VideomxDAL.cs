using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace BLL
{
	/// <summary>
	/// 业务类实现--VideomxDAL(填写类描述)
	/// </summary>
    public class VideomxDAL
    {
        public VideomxDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="VideomxMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(VideomxMDL _VideomxMDL)
		{
		    return Insert(null,_VideomxMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="VideomxMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,VideomxMDL _VideomxMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.Videomx([ID],createtime,videoid,title,videopath,visitnum)
			VALUES (@id,@createtime,@videoid,@title,@videopath,@visitnum)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32, _VideomxMDL.id));
			p.Add(db.CreateParameter("createtime",DbType.DateTime, _VideomxMDL.createtime));
			p.Add(db.CreateParameter("videoid",DbType.Int32, _VideomxMDL.videoid));
			p.Add(db.CreateParameter("title",DbType.String, _VideomxMDL.title));
			p.Add(db.CreateParameter("videopath",DbType.String, _VideomxMDL.videopath));
			p.Add(db.CreateParameter("visitnum",DbType.Int32, _VideomxMDL.visitnum));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="VideomxMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(VideomxMDL _VideomxMDL)
		{
			return Update(null,_VideomxMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="VideomxMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,VideomxMDL _VideomxMDL)
		{
			string sql = @"
			UPDATE dbo.Videomx
				SET	createtime = @createtime,videoid = @videoid,title = @title,videopath = @videopath,visitnum = @visitnum
			WHERE
				id = @id";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32, _VideomxMDL.id));
			p.Add(db.CreateParameter("createtime",DbType.DateTime, _VideomxMDL.createtime));
			p.Add(db.CreateParameter("videoid",DbType.Int32, _VideomxMDL.videoid));
			p.Add(db.CreateParameter("title",DbType.String, _VideomxMDL.title));
			p.Add(db.CreateParameter("videopath",DbType.String, _VideomxMDL.videopath));
			p.Add(db.CreateParameter("visitnum",DbType.Int32, _VideomxMDL.visitnum));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="VideomxID">主键</param>
		/// <returns></returns>
        public static int Delete( int id )
		{
			return Delete(null, id);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="VideomxID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int id)
		{
			string sql=@"DELETE FROM dbo.Videomx WHERE id = @id";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32,id));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="VideomxMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(VideomxMDL _VideomxMDL)
		{
			return Delete(null,_VideomxMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="VideomxMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,VideomxMDL _VideomxMDL)
		{
			string sql=@"DELETE FROM dbo.Videomx WHERE id = @id";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("id",DbType.Int32,_VideomxMDL.id));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="VideomxID">主键</param>
        public static VideomxMDL GetObject( int id )
		{
			string sql=@"
			SELECT [ID],createtime,videoid,title,videopath,visitnum
			FROM dbo.Videomx
			WHERE id = @id";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("id", DbType.Int32, id));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                VideomxMDL _VideomxMDL = null;
                if (reader.Read())
                {
                    _VideomxMDL = new VideomxMDL();
					if (reader["id"] != DBNull.Value) _VideomxMDL.id = Convert.ToInt32(reader["id"]);
					if (reader["createtime"] != DBNull.Value) _VideomxMDL.createtime = Convert.ToDateTime(reader["createtime"]);
					if (reader["videoid"] != DBNull.Value) _VideomxMDL.videoid = Convert.ToInt32(reader["videoid"]);
					if (reader["title"] != DBNull.Value) _VideomxMDL.title = Convert.ToString(reader["title"]);
					if (reader["videopath"] != DBNull.Value) _VideomxMDL.videopath = Convert.ToString(reader["videopath"]);
					if (reader["visitnum"] != DBNull.Value) _VideomxMDL.visitnum = Convert.ToInt32(reader["visitnum"]);
                }
				reader.Close();
                db.Close();
                return _VideomxMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Videomx", "*", filterWhereString, orderBy == "" ? " id" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Videomx", filterWhereString);
        }
        	    /// <summary>
       
        #region 自定义方法
        
        #endregion
    }
}
