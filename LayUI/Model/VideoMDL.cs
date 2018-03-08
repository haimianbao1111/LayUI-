using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--VideoMDL填写类描述
	/// </summary>
	[Serializable]
	public class VideoMDL
	{
		public VideoMDL()
		{			
		}
			
		//主键
		protected int? _id;
		
		//其它属性
		protected DateTime _createtime;
		protected string _title;
		protected string _img;
		protected string _body;
		protected int? _visitnum;
		
		public int? id
		{
			get {return _id;}
			set {_id = value;}
		}

		public DateTime createtime
		{
			get {return _createtime;}
			set {_createtime = value;}
		}

		public string title
		{
			get {return _title;}
			set {_title = value;}
		}

		public string img
		{
			get {return _img;}
			set {_img = value;}
		}

		public string body
		{
			get {return _body;}
			set {_body = value;}
		}

		public int? visitnum
		{
			get {return _visitnum;}
			set {_visitnum = value;}
		}
	}
}
