using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--VideomxMDL填写类描述
	/// </summary>
	[Serializable]
	public class VideomxMDL
	{
		public VideomxMDL()
		{			
		}
			
		//主键
		protected int? _id;
		
		//其它属性
		protected DateTime? _createtime;
		protected int? _videoid;
		protected string _title;
		protected string _videopath;
		protected int? _visitnum;
		
		public int? id
		{
			get {return _id;}
			set {_id = value;}
		}

		public DateTime? createtime
		{
			get {return _createtime;}
			set {_createtime = value;}
		}

		public int? videoid
		{
			get {return _videoid;}
			set {_videoid = value;}
		}

		public string title
		{
			get {return _title;}
			set {_title = value;}
		}

		public string videopath
		{
			get {return _videopath;}
			set {_videopath = value;}
		}

		public int? visitnum
		{
			get {return _visitnum;}
			set {_visitnum = value;}
		}
	}
}
