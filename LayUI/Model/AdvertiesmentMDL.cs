using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--AdvertiesmentMDL填写类描述
	/// </summary>
	[Serializable]
	public class AdvertiesmentMDL
	{
		public AdvertiesmentMDL()
		{			
		}
			
		//主键
		protected int? _id;
		
		//其它属性
		protected DateTime? _createtime;
		protected string _path;
		protected string _type;
		protected string _link;
		protected int? _time;
		protected string _remark;
		
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

		public string path
		{
			get {return _path;}
			set {_path = value;}
		}

		public string type
		{
			get {return _type;}
			set {_type = value;}
		}

		public string link
		{
			get {return _link;}
			set {_link = value;}
		}

		public int? time
		{
			get {return _time;}
			set {_time = value;}
		}

		public string remark
		{
			get {return _remark;}
			set {_remark = value;}
		}
	}
}
