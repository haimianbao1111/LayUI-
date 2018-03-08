using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace UIHelper
{
	public static class Tool
	{
		public class Baidu_Location
		{
			public string lng
			{
				get;
				set;
			}
			public string lat
			{
				get;
				set;
			}
		}
		public class Baidu_Result
		{
			public Tool.Baidu_Location location
			{
				get;
				set;
			}
			public int precise
			{
				get;
				set;
			}
			public int confidence
			{
				get;
				set;
			}
			public string level
			{
				get;
				set;
			}
		}
		public class Baidu_Geocoder
		{
			public int status
			{
				get;
				set;
			}
			public Tool.Baidu_Result result
			{
				get;
				set;
			}
		}
		
		private static string CIV = "aXwL7X2+fgM=";
		private static string CKEY = "FwGQWRRgKCI=";
		public static string CopyFolder(string sPath, string dPath)
		{
			string result = "success";
			try
			{
				bool flag = !Directory.Exists(dPath);
				if (flag)
				{
					Directory.CreateDirectory(dPath);
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(sPath);
				FileInfo[] files = directoryInfo.GetFiles();
				FileInfo[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					FileInfo fileInfo = array[i];
					fileInfo.CopyTo(dPath + "\\" + fileInfo.Name, true);
				}
				DirectoryInfo directoryInfo2 = new DirectoryInfo(dPath);
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				DirectoryInfo[] array2 = directories;
				for (int j = 0; j < array2.Length; j++)
				{
					DirectoryInfo directoryInfo3 = array2[j];
					Tool.CopyFolder(directoryInfo3.FullName, dPath + "//" + directoryInfo3.Name);
				}
			}
			catch (Exception ex)
			{
				result = ex.ToString();
			}
			return result;
		}
		
		public static void BindDDL<T>(DropDownList ddl, IEnumerable<T> list, string textfield, string valuefield)
		{
			ddl.DataTextField = textfield;
			ddl.DataValueField = valuefield;
			ddl.DataSource = list;
			ddl.DataBind();
		}
		public static byte[] SubByte(byte[] srcBytes, int startIndex, int length)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[0];
			bool flag = srcBytes == null;
			byte[] result;
			if (flag)
			{
				result = array;
			}
			else
			{
				bool flag2 = startIndex < 0;
				if (flag2)
				{
					startIndex = 0;
				}
				bool flag3 = startIndex < srcBytes.Length;
				if (flag3)
				{
					bool flag4 = length < 1 || length > srcBytes.Length - startIndex;
					if (flag4)
					{
						length = srcBytes.Length - startIndex;
					}
					memoryStream.Write(srcBytes, startIndex, length);
					array = memoryStream.ToArray();
					memoryStream.SetLength(0L);
					memoryStream.Position = 0L;
				}
				memoryStream.Close();
				memoryStream.Dispose();
				result = array;
			}
			return result;
		}
		public static string GetValueByKeyFromUrlParam(string key, string param)
		{
			param = param.Replace("&", "&");
			string[] array = param.Split(new char[]
			{
				'&'
			});
			string result = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				bool flag = text.Contains(key + "=");
				if (flag)
				{
					result = text.Replace(key + "=", "");
				}
			}
			return result;
		}
		public static byte[] ToBytesByString(string input)
		{
			string[] array = input.Split(new char[]
			{
				' '
			});
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = byte.Parse(Convert.ToInt64("0x" + array[i], 16).ToString());
			}
			return array2;
		}
		public static string ToStringByBytes(byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("-", " ");
		}
		public static DateTime GetNearDateByXingQiInt(int xingqi)
		{
			int dayOfWeek = (int)DateTime.Now.DayOfWeek;
			int num = Math.Abs(dayOfWeek - (xingqi + 7));
			return DateTime.Now.AddDays((double)num);
		}
		public static string DateFormatToString(DateTime dt)
		{
			DateTime now = DateTime.Now;
			bool flag = now.Year == dt.Year;
			string result;
			if (flag)
			{
				bool flag2 = now.ToString("MM-dd") == dt.ToString("MM-dd");
				if (flag2)
				{
					TimeSpan timeSpan = (now - dt).Duration();
					bool flag3 = timeSpan.TotalHours >= 1.0;
					if (flag3)
					{
						result = string.Format("{0}小时前", (int)Math.Floor(timeSpan.TotalHours));
					}
					else
					{
						bool flag4 = timeSpan.TotalMinutes > 1.0;
						if (flag4)
						{
							result = string.Format("{0}分钟前", (int)Math.Floor(timeSpan.TotalMinutes));
						}
						else
						{
							result = "刚刚";
						}
					}
				}
				else
				{
					bool flag5 = now.AddDays(-1.0).ToString("MM-dd") == dt.ToString("MM-dd");
					if (flag5)
					{
						result = "昨天 " + dt.ToString("HH:mm");
					}
					else
					{
						result = dt.ToString("MM-dd HH:mm");
					}
				}
			}
			else
			{
				result = dt.ToString("yyyy-MM-dd HH:mm");
			}
			return result;
		}
		public static DateTime GetTime(string timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(timeStamp + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}
		public static int ConvertDateTimeInt(DateTime time)
		{
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (int)(time - d).TotalSeconds;
		}
		public static bool IsMobileBrowser()
		{
			HttpContext current = HttpContext.Current;
			bool isMobileDevice = current.Request.Browser.IsMobileDevice;
			bool result;
			if (isMobileDevice)
			{
				result = true;
			}
			else
			{
				bool flag = current.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = current.Request.ServerVariables["HTTP_ACCEPT"] != null && current.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap");
					if (flag2)
					{
						result = true;
					}
					else
					{
						bool flag3 = current.Request.ServerVariables["HTTP_USER_AGENT"] != null;
						if (flag3)
						{
							string text = current.Request.ServerVariables["HTTP_USER_AGENT"];
							Regex regex = new Regex("android|avantgo|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\\\/|plucker|pocket|psp|symbian|treo|up\\\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
							Regex regex2 = new Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\\\-(n|u)|c55\\\\/|capi|ccwa|cdm\\\\-|cell|chtm|cldc|cmd\\\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\\\-|_)|g1 u|g560|gene|gf\\\\-5|g\\\\-mo|go(\\\\.w|od)|gr(ad|un)|haie|hcit|hd\\\\-(m|p|t)|hei\\\\-|hi(pt|ta)|hp( i|ip)|hs\\\\-c|ht(c(\\\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\\\-(20|go|ma)|i230|iac( |\\\\-|\\\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\\\/)|klon|kpt |kwc\\\\-|kyo(c|k)|le(no|xi)|lg( g|\\\\/(k|l|u)|50|54|e\\\\-|e\\\\/|\\\\-[a-w])|libw|lynx|m1\\\\-w|m3ga|m50\\\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\\\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\\\-2|po(ck|rt|se)|prox|psio|pt\\\\-g|qa\\\\-a|qc(07|12|21|32|60|\\\\-[2-7]|i\\\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\\\-|oo|p\\\\-)|sdk\\\\/|se(c(\\\\-|0|1)|47|mc|nd|ri)|sgh\\\\-|shar|sie(\\\\-|m)|sk\\\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\\\-|v\\\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\\\-|tdg\\\\-|tel(i|m)|tim\\\\-|t\\\\-mo|to(pl|sh)|ts(70|m\\\\-|m3|m5)|tx\\\\-9|up(\\\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\\\\-|2|g)|yas\\\\-|your|zeto|zte\\\\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
							bool flag4 = regex.IsMatch(text) || regex2.IsMatch(text.Substring(0, 4));
							if (flag4)
							{
								result = true;
								return result;
							}
							string[] array = new string[]
							{
								"midp",
								"j2me",
								"avant",
								"docomo",
								"novarra",
								"palmos",
								"palmsource",
								"240x320",
								"opwv",
								"chtml",
								"pda",
								"windows ce",
								"mmp/",
								"blackberry",
								"mib/",
								"symbian",
								"wireless",
								"nokia",
								"hand",
								"mobi",
								"phone",
								"cdm",
								"up.b",
								"audio",
								"SIE-",
								"SEC-",
								"samsung",
								"HTC",
								"mot-",
								"mitsu",
								"sagem",
								"sony",
								"alcatel",
								"lg",
								"eric",
								"vx",
								"NEC",
								"philips",
								"mmm",
								"xx",
								"panasonic",
								"sharp",
								"wap",
								"sch",
								"rover",
								"pocket",
								"benq",
								"java",
								"pt",
								"pg",
								"vox",
								"amoi",
								"bird",
								"compal",
								"kg",
								"voda",
								"sany",
								"kdd",
								"dbt",
								"sendo",
								"sgh",
								"gradi",
								"jb",
								"dddi",
								"moto",
								"iphone"
							};
							string[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								string text2 = array2[i];
								bool flag5 = current.Request.ServerVariables["HTTP_USER_AGENT"].ToLower().Contains(text2.ToLower());
								if (flag5)
								{
									result = true;
									return result;
								}
							}
						}
						result = false;
					}
				}
			}
			return result;
		}
		public static void GetBaiduMapJWDByAddress(string baidu_ak, string address, out string lng, out string lat)
		{
			string uRL = "http://api.map.baidu.com/geocoder/v2/?address=" + address + "&output=json&ak=" + baidu_ak;
			HttpHelper httpHelper = new HttpHelper();
			HttpResult html = httpHelper.GetHtml(new HttpItem
			{
				URL = uRL,
				Method = "GET"
			});
			Tool.Baidu_Geocoder baidu_Geocoder = JsonConvert.DeserializeObject<Tool.Baidu_Geocoder>(html.Html);
			bool flag = baidu_Geocoder.status == 0;
			if (flag)
			{
				lng = baidu_Geocoder.result.location.lng;
				lat = baidu_Geocoder.result.location.lat;
			}
			else
			{
				lng = "";
				lat = "";
			}
		}
		public static void ExportWord(string bodyhtml)
		{
			HttpContext.Current.Response.ContentType = "application/msword";
			HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
			HttpContext.Current.Response.Charset = "UTF-8";
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=我的创业计划书.doc");
			HttpContext.Current.Response.Write("<html>");
			HttpContext.Current.Response.Write("<head>");
			HttpContext.Current.Response.Write("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=UTF-8\">");
			HttpContext.Current.Response.Write("<meta name=ProgId content=Word.Document>");
			HttpContext.Current.Response.Write("<meta name=Generator content=\"Microsoft Word 9\">");
			HttpContext.Current.Response.Write("<meta name=Originator content=\"Microsoft Word 9\">");
			HttpContext.Current.Response.Write("</head>");
			HttpContext.Current.Response.Write("<body>");
			HttpContext.Current.Response.Write(bodyhtml);
			HttpContext.Current.Response.Write("</body>");
			HttpContext.Current.Response.Write("</html>");
			HttpContext.Current.Response.Flush();
		}
		public static string GetIPAddress(string ip, string ipfilePath)
		{
			string result;
			try
			{
				IPSearch iPSearch = new IPSearch(ipfilePath);
				IPSearch.IPLocation iPLocation = iPSearch.GetIPLocation(ip);
				result = iPLocation.country + " " + iPLocation.area;
			}
			catch (Exception var_3_2A)
			{
				result = "";
			}
			return result;
		}
		public static bool SavePhotoFromUrl(string FileName, string Url)
		{
			bool flag = false;
			bool result;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				WebResponse response = httpWebRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				bool flag2 = !response.ContentType.ToLower().StartsWith("text/");
				if (flag2)
				{
					flag = true;
					byte[] array = new byte[1024];
					try
					{
						bool flag3 = File.Exists(FileName);
						if (flag3)
						{
							File.Delete(FileName);
						}
						Stream stream = File.Create(FileName);
						Stream responseStream2 = response.GetResponseStream();
						int num;
						do
						{
							num = responseStream2.Read(array, 0, array.Length);
							bool flag4 = num > 0;
							if (flag4)
							{
								stream.Write(array, 0, num);
							}
						}
						while (num > 0);
						stream.Close();
						responseStream2.Close();
					}
					catch
					{
						flag = false;
					}
					result = flag;
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			result = flag;
			return result;
		}
		public static string GetAddressIP_CS()
		{
			string result = string.Empty;
			IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			for (int i = 0; i < addressList.Length; i++)
			{
				IPAddress iPAddress = addressList[i];
				bool flag = iPAddress.AddressFamily.ToString() == "InterNetwork";
				if (flag)
				{
					result = iPAddress.ToString();
				}
			}
			return result;
		}
		[DllImport("Iphlpapi.dll")]
		private static extern int SendARP(int dest, int host, ref long mac, ref int length);
		[DllImport("Ws2_32.dll")]
		private static extern int inet_addr(string ip);
		public static string GetMAC()
		{
			string ip = "";
			try
			{
				ip = Tool.GetRealIP();
			}
			catch (Exception)
			{
				ip = Tool.GetAddressIP_CS();
			}
			int dest = Tool.inet_addr(ip);
			long num = 0L;
			int num2 = 6;
			int num3 = Tool.SendARP(dest, 0, ref num, ref num2);
			string text = num.ToString("X");
			while (text.Length < 12)
			{
				text = text.Insert(0, "0");
			}
			string text2 = "";
			for (int i = 0; i < 11; i++)
			{
				bool flag = i % 2 == 0;
				if (flag)
				{
					bool flag2 = i == 10;
					if (flag2)
					{
						text2 = text2.Insert(0, text.Substring(i, 2));
					}
					else
					{
						text2 = "-" + text2.Insert(0, text.Substring(i, 2));
					}
				}
			}
			return text2;
		}
		public static string GetDescription(string classname, string pmname, string op = "m")
		{
			string result;
			try
			{
				Type type = Type.GetType(classname);
				bool flag = op == "m";
				if (flag)
				{
					MethodInfo method = type.GetMethod(pmname);
					result = ((DescriptionAttribute)method.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
				}
				else
				{
					PropertyInfo property = type.GetProperty(pmname);
					result = ((DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
				}
			}
			catch (Exception ex)
			{
				result = "出错: " + ex.Message;
			}
			return result;
		}
		public static string GetRealIP()
		{
			string text = "";
			try
			{
				text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
				bool flag = (string.IsNullOrEmpty(text) || text == "127.0.0.1" || text == "::1") && HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null;
				if (flag)
				{
					text = HttpContext.Current.Request.ServerVariables["HTTP_VIA"].ToString();
				}
				bool flag2 = (string.IsNullOrEmpty(text) || text == "127.0.0.1" || text == "::1") && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null;
				if (flag2)
				{
					text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
				}
				bool flag3 = string.IsNullOrEmpty(text);
				if (flag3)
				{
					text = HttpContext.Current.Request.UserHostAddress;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return text;
		}
		public static string ConvertToGB(string unicodeString)
		{
			string pattern = "(\\\\u[0-9A-Fa-f]{2,})";
			string[] array = Regex.Split(unicodeString, pattern);
			string text = string.Empty;
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			Match match = regex.Match(unicodeString);
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = i % 2 == 0;
				if (flag)
				{
					text += array[i];
				}
				else
				{
					text += char.ConvertFromUtf32(Convert.ToInt32(array[i].Replace("\\u", ""), 16));
				}
				match = match.NextMatch();
			}
			return text;
		}
		public static string ConvertToUnicode(string strGB)
		{
			char[] array = strGB.ToCharArray();
			string text = string.Empty;
			char[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				char c = array2[i];
				text = text + "\\u" + char.ConvertToUtf32(c.ToString(), 0).ToString("x");
			}
			return text;
		}
		public static string GetHostUrl()
		{
			return string.Concat(new object[]
			{
				HttpContext.Current.Request.Url.Scheme,
				"://",
				HttpContext.Current.Request.Url.Host,
				":",
				HttpContext.Current.Request.Url.Port
			});
		}
		public static string GetXingQi(DateTime d)
		{
			string[] array = new string[]
			{
				"星期日",
				"星期一",
				"星期二",
				"星期三",
				"星期四",
				"星期五",
				"星期六"
			};
			return array[Convert.ToInt32(d.DayOfWeek.ToString("d"))].ToString();
		}
		public static DateTime GetNextXingQi(int xingqi)
		{
			DateTime now = DateTime.Now;
			bool flag = xingqi == 0;
			if (flag)
			{
				xingqi = 7;
			}
			bool flag2 = now.DayOfWeek == (DayOfWeek)xingqi;
			DateTime result;
			if (flag2)
			{
				result = now.AddDays(7.0);
			}
			else
			{
				bool flag3 = now.DayOfWeek < (DayOfWeek)xingqi;
				if (flag3)
				{
					result = now.AddDays((double)(xingqi - (int)now.DayOfWeek));
				}
				else
				{
					result = now.AddDays((double)((DayOfWeek)7 - now.DayOfWeek + xingqi));
				}
			}
			return result;
		}
		public static void Alert(string _Msg, Page _Page)
		{
			string text = "<script language=javascript>";
			text = text + "alert('" + _Msg.Replace("'", " ") + "');";
			text += "</script>";
			_Page.ClientScript.RegisterStartupScript(_Page.GetType(), "MsgBox", text);
		}
		public static void AlertAndGo(string _msg, string _href, Page _page)
		{
			string text = "<script language=javascript>";
			text = string.Concat(new string[]
			{
				text,
				"alert('",
				_msg.Replace("'", " "),
				"');location.href='",
				_href,
				"'"
			});
			text += "</script>";
			_page.ClientScript.RegisterStartupScript(_page.GetType(), "MsgBox", text);
		}
		public static void AlertAndGo_MVC(string _msg, string _href)
		{
			string text = "<script language=javascript>";
			text = string.Concat(new string[]
			{
				text,
				"alert('",
				_msg.Replace("'", " "),
				"');location.href='",
				_href,
				"'"
			});
			text += "</script>";
			HttpContext.Current.Response.Write(text);
		}
		public static void AlertAndCloseWin_MVC(string _msg)
		{
			string text = "<script language=javascript>";
			text = text + "alert('" + _msg.Replace("'", " ") + "');window.close();";
			text += "</script>";
			HttpContext.Current.Response.Write(text);
		}
		public static void GetImgWH(string img, out int w, out int h)
		{
			Bitmap bitmap = new Bitmap(img);
			bool flag = bitmap != null;
			if (flag)
			{
				w = bitmap.Width;
				h = bitmap.Height;
			}
			else
			{
				w = 0;
				h = 0;
			}
		}
		public static void CreateImage(string oPath, string tPath, int width, int height, string color = "white", string scale = "no")
		{
			FileInfo fileInfo = new FileInfo(tPath);
			bool flag = fileInfo != null;
			if (flag)
			{
				bool flag2 = !Directory.Exists(fileInfo.DirectoryName);
				if (flag2)
				{
					Directory.CreateDirectory(fileInfo.DirectoryName);
				}
			}
			bool flag3 = string.IsNullOrEmpty(color);
			if (flag3)
			{
				color = "white";
			}
			Bitmap bitmap = new Bitmap(oPath);
			bool flag4 = scale == "yes";
			if (flag4)
			{
				Bitmap bitmap2 = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap2))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.FillRectangle(Brushes.White, 0, 0, width, height);
					graphics.DrawImage(bitmap, 0, 0, width, width);
				}
				bitmap2.Save(tPath, ImageFormat.Jpeg);
				bitmap2.Dispose();
				bitmap.Dispose();
			}
			else
			{
				bool flag5 = bitmap.Width <= width && bitmap.Height <= height;
				if (flag5)
				{
					int x = (int)Math.Round((width - bitmap.Width) / 2m);
					int y = (int)Math.Round((height - bitmap.Height) / 2m);
					Bitmap bitmap3 = new Bitmap(width, height);
					using (Graphics graphics2 = Graphics.FromImage(bitmap3))
					{
						graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics2.Clear(Color.FromName(color));
						graphics2.DrawImage(bitmap, x, y, bitmap.Width, bitmap.Height);
					}
					bitmap3.Save(tPath, ImageFormat.Jpeg);
					bitmap3.Dispose();
					bitmap.Dispose();
				}
				else
				{
					bool flag6 = width * bitmap.Height < height * bitmap.Width;
					int x;
					int y;
					int num;
					int num2;
					if (flag6)
					{
						num = width;
						num2 = (int)Math.Round((double)(bitmap.Height * width) / bitmap.Width);
						x = 0;
						y = (int)Math.Round((height - num2) / 2m);
					}
					else
					{
						num = (int)Math.Round((double)(bitmap.Width * height) / bitmap.Height);
						num2 = height;
						x = (int)Math.Round((width - num) / 2m);
						y = 0;
					}
					Bitmap image = new Bitmap(num, num2);
					using (Graphics graphics3 = Graphics.FromImage(image))
					{
						graphics3.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics3.FillRectangle(Brushes.White, 0, 0, num, num2);
						graphics3.DrawImage(bitmap, 0, 0, num, num2);
					}
					Bitmap bitmap4 = new Bitmap(width, height);
					using (Graphics graphics4 = Graphics.FromImage(bitmap4))
					{
						graphics4.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics4.Clear(Color.FromName(color));
						graphics4.DrawImage(image, x, y);
					}
					bitmap4.Save(tPath, ImageFormat.Jpeg);
					bitmap4.Dispose();
					bitmap.Dispose();
				}
			}
		}
		public static void ZoomImage(string oPath, string tPath, int wh, string op, out int other_wh)
		{
			Bitmap bitmap = new Bitmap(oPath);
			bool flag = op == "width";
			int num;
			int num2;
			if (flag)
			{
				num = wh;
				num2 = (int)Math.Round((double)(bitmap.Height * wh) / bitmap.Width);
				other_wh = num2;
			}
			else
			{
				num = (int)Math.Round((double)(bitmap.Width * wh) / bitmap.Height);
				num2 = wh;
				other_wh = num;
			}
			Bitmap bitmap2 = new Bitmap(num, num2);
			using (Graphics graphics = Graphics.FromImage(bitmap2))
			{
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.FillRectangle(Brushes.White, 0, 0, num, num2);
				graphics.DrawImage(bitmap, 0, 0, num, num2);
			}
			bitmap2.Save(tPath, ImageFormat.Jpeg);
			bitmap2.Dispose();
			bitmap.Dispose();
		}
		public static void ZoomImage(string oPath, string tPath, int wh, string op)
		{
			int num;
			Tool.ZoomImage(oPath, tPath, wh, op, out num);
		}
		public static void ExecJS(string js, Page _Page)
		{
			string text = "<script language='javascript' type='text/javascript'>";
			text += js;
			text += "</script>";
			_Page.ClientScript.RegisterStartupScript(_Page.GetType(), "ExecJS", text);
		}
		public static string GetApplicationPath()
		{
			string applicationPath = HttpContext.Current.Request.ApplicationPath;
			return (applicationPath == "/") ? applicationPath : (applicationPath + "/");
		}
		public static string GenEmabed(string url)
		{
			string text = url.Substring(url.LastIndexOf(".") + 1);
			bool flag = text == "swf" || text == "swf" || text == "flv" || text == "mp3" || text == "wav" || text == "wma" || text == "wmv" || text == "mid" || text == "avi" || text == "mpg" || text == "asf" || text == "rm" || text == "rmvb";
			string result;
			if (flag)
			{
				string a = text;
				string text2;
				if (!(a == "flv") && !(a == "mp3") && !(a == "swf"))
				{
					if (!(a == "rm") && !(a == "rmvb"))
					{
						text2 = "<embed src='" + url + "' type='video/x-ms-asf-plugin' width='550' height='400' autostart='false' loop='true' />";
					}
					else
					{
						text2 = "<embed src='" + url + "' type='audio/x-pn-realaudio-plugin' width='550' height='400' autostart='false' loop='true' />";
					}
				}
				else
				{
					text2 = "<embed src='" + url + "' type='application/x-shockwave-flash' width='550' height='400' autostart='false' loop='true' />";
				}
				result = text2;
			}
			else
			{
				result = "视频格式只能是swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb";
			}
			return result;
		}
		public static string GetNoHTMLString(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace("<", "");
			Htmlstring.Replace(">", "");
			Htmlstring.Replace("\r\n", "");
			Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}
		public static string GetSafeHTMLString(string str)
		{
			str = Regex.Replace(str, "<applet[^>]*?>.*?</applet>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<body[^>]*?>.*?</body>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<embed[^>]*?>.*?</embed>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<frame[^>]*?>.*?</frame>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<frameset[^>]*?>.*?</frameset>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<html[^>]*?>.*?</html>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<iframe[^>]*?>.*?</iframe>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<style[^>]*?>.*?</style>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<layer[^>]*?>.*?</layer>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<link[^>]*?>.*?</link>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<ilayer[^>]*?>.*?</ilayer>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<meta[^>]*?>.*?</meta>", "", RegexOptions.IgnoreCase);
			str = Regex.Replace(str, "<object[^>]*?>.*?</object>", "", RegexOptions.IgnoreCase);
			return str;
		}
		public static string GetSafeSQL(string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				value = value.Trim();
				value = Tool.StringTruncat(value, 40, "");
				value = Regex.Replace(value, ";", string.Empty);
				value = Regex.Replace(value, "'", string.Empty);
				value = Regex.Replace(value, "&", string.Empty);
				value = Regex.Replace(value, "%20", string.Empty);
				value = Regex.Replace(value, "--", string.Empty);
				value = Regex.Replace(value, "==", string.Empty);
				value = Regex.Replace(value, "<", string.Empty);
				value = Regex.Replace(value, ">", string.Empty);
				value = Regex.Replace(value, "%", string.Empty);
				result = value;
			}
			return result;
		}
		public static string HiddenIP(string ip)
		{
			int num = ip.LastIndexOf('.');
			string str = ip.Substring(0, num + 1);
			return str + "*";
		}
		public static string MD5(string str)
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
		}
		public static string SHA1(string str)
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(str, FormsAuthPasswordFormat.SHA1.ToString());
		}
		public static void SendMail(string title, string body, string toAdress, string fromAdress, string userName, string userPwd, string smtpHost)
		{
			try
			{
				MailAddress to = new MailAddress(toAdress);
				MailAddress from = new MailAddress(fromAdress);
				MailMessage mailMessage = new MailMessage(from, to);
				mailMessage.IsBodyHtml = true;
				mailMessage.Subject = title;
				mailMessage.Body = body;
				SmtpClient smtpClient = new SmtpClient();
				smtpClient.UseDefaultCredentials = true;
				smtpClient.Port = 25;
				smtpClient.Credentials = new NetworkCredential(userName, userPwd);
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.Host = smtpHost;
				mailMessage.To.Add(toAdress);
				smtpClient.Send(mailMessage);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public static string Upload(FileUpload myFileUpload, string[] allowExtensions, int maxLength, string savePath, bool gendatedir = true, bool guidname = true, string savename = "")
		{
			bool flag = false;
			bool hasFile = myFileUpload.HasFile;
			if (!hasFile)
			{
				throw new Exception("请选择要上传的文件！");
			}
			bool flag2 = myFileUpload.PostedFile.ContentLength / 1024 / 1024 >= maxLength;
			if (flag2)
			{
				throw new Exception("只能上传小于" + maxLength + "M的文件！");
			}
			string text = Path.GetExtension(myFileUpload.FileName).ToLower();
			string text2 = "";
			for (int i = 0; i < allowExtensions.Length; i++)
			{
				text2 += ((i == allowExtensions.Length - 1) ? allowExtensions[i] : (allowExtensions[i] + ","));
				bool flag3 = text == allowExtensions[i];
				if (flag3)
				{
					flag = true;
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				try
				{
					string text3 = DateTime.Now.ToString("yyyyMMdd");
					bool flag5 = !Directory.Exists(savePath + text3) & gendatedir;
					if (flag5)
					{
						Directory.CreateDirectory(savePath + text3);
					}
					string text4 = guidname ? (Guid.NewGuid() + text) : myFileUpload.FileName;
					bool flag6 = !string.IsNullOrEmpty(savename);
					if (flag6)
					{
						text4 = savename + text;
					}
					string filename = gendatedir ? (savePath + text3 + "/" + text4) : (savePath + text4);
					myFileUpload.SaveAs(filename);
					return gendatedir ? (text3 + "/" + text4) : text4;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			throw new Exception("文件格式不符，可以上传的文件格式为：" + text2);
		}
		public static string Upload_MVC(HttpPostedFileBase PostedFile, string[] allowExtensions, int maxLength, string savePath, bool gendatedir = true, bool guidname = true, string savename = "")
		{
			bool flag = false;
			bool flag2 = PostedFile.ContentLength / 1024 / 1024 >= maxLength;
			if (flag2)
			{
				throw new Exception("只能上传小于" + maxLength + "M的文件！");
			}
			string text = Path.GetExtension(PostedFile.FileName).ToLower();
			string text2 = "";
			for (int i = 0; i < allowExtensions.Length; i++)
			{
				text2 += ((i == allowExtensions.Length - 1) ? allowExtensions[i] : (allowExtensions[i] + ","));
				bool flag3 = text == allowExtensions[i];
				if (flag3)
				{
					flag = true;
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				try
				{
					string text3 = DateTime.Now.ToString("yyyyMMdd");
					bool flag5 = !Directory.Exists(savePath + text3) & gendatedir;
					if (flag5)
					{
						Directory.CreateDirectory(savePath + text3);
					}
					string text4 = guidname ? (Guid.NewGuid() + text) : PostedFile.FileName;
					bool flag6 = !string.IsNullOrEmpty(savename);
					if (flag6)
					{
						text4 = savename + text;
					}
					string filename = gendatedir ? (savePath + text3 + "/" + text4) : (savePath + text4);
					PostedFile.SaveAs(filename);
					return gendatedir ? (text3 + "/" + text4) : text4;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			throw new Exception("文件格式（" + text + "）不符，可以上传的文件格式为：" + text2);
		}
		public static void TxtLog(string logmsg, string absolutefile)
		{
			try
			{
				StreamWriter streamWriter = new StreamWriter(absolutefile, true, Encoding.Default);
				streamWriter.WriteLine("时间：" + DateTime.Now);
				streamWriter.WriteLine(logmsg);
				streamWriter.WriteLine("");
				streamWriter.WriteLine("");
				streamWriter.Flush();
				streamWriter.Close();
				streamWriter.Dispose();
			}
			catch (Exception var_1_62)
			{
			}
		}
		public static string StringTruncat(string oldStr, int maxLength, string endWith)
		{
			bool flag = string.IsNullOrEmpty(oldStr);
			string result;
			if (flag)
			{
				result = oldStr + endWith;
			}
			else
			{
				bool flag2 = maxLength < 1;
				if (flag2)
				{
					throw new Exception("返回的字符串长度必须大于[0] ");
				}
				bool flag3 = oldStr.Length > maxLength;
				if (flag3)
				{
					string text = oldStr.Substring(0, maxLength);
					bool flag4 = string.IsNullOrEmpty(endWith);
					if (flag4)
					{
						result = text;
					}
					else
					{
						result = text + endWith;
					}
				}
				else
				{
					result = oldStr;
				}
			}
			return result;
		}
		
		public static string DBCToSBC(string input)
		{
			char[] array = input.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = array[i] == ' ';
				if (flag)
				{
					array[i] = '\u3000';
				}
				else
				{
					bool flag2 = array[i] < '\u007f';
					if (flag2)
					{
						array[i] += 'ﻠ';
					}
				}
			}
			return new string(array);
		}
		public static string SBCToDBC(string input)
		{
			char[] array = input.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = array[i] == '\u3000';
				if (flag)
				{
					array[i] = ' ';
				}
				else
				{
					bool flag2 = array[i] > '＀' && array[i] < '｟';
					if (flag2)
					{
						array[i] -= 'ﻠ';
					}
				}
			}
			return new string(array);
		}
		private static byte[] StringArgToByteArray(string arg)
		{
			byte[] array = new byte[arg.Length / 2];
			int i = 0;
			int num = 0;
			while (i < arg.Length)
			{
				string value = arg.Substring(i, 2);
				array[num] = Convert.ToByte(value, 16);
				i += 2;
				num++;
			}
			return array;
		}
		public static string EncryptString(string txt, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(txt);
			ICryptoTransform cryptoTransform = new DESCryptoServiceProvider
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(key),
				Mode = CipherMode.ECB
			}.CreateEncryptor();
			byte[] array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				stringBuilder.AppendFormat("{0:X2}", b);
			}
			return stringBuilder.ToString();
		}
		public static string DecryptString(string txt, string key)
		{
			byte[] array = Tool.StringArgToByteArray(txt);
			ICryptoTransform cryptoTransform = new DESCryptoServiceProvider
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(key),
				Mode = CipherMode.ECB
			}.CreateDecryptor();
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			return Encoding.UTF8.GetString(bytes);
		}
		public static string Encrypt(string toEncrypt, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
			int num = (16 - toEncrypt.Length % 16) % 16;
			int num2 = bytes.Length + num;
			Console.WriteLine(string.Concat(new object[]
			{
				"size = ",
				num2,
				", pad = ",
				num
			}));
			bool flag = num == 0;
			if (flag)
			{
				num2 += 16;
			}
			byte[] array = new byte[num2];
			bytes.CopyTo(array, 0);
			bool flag2 = num > 0;
			if (flag2)
			{
				byte[] array2 = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = (byte)num;
				}
				array2.CopyTo(array, bytes.Length);
			}
			else
			{
				for (int j = num2 - 16; j < num2; j++)
				{
					array[j] = 16;
				}
			}
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(key.Substring(0, 16)),
				Mode = CipherMode.CBC,
				Padding = PaddingMode.Zeros
			}.CreateEncryptor();
			byte[] array3 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			StringBuilder stringBuilder = new StringBuilder();
			for (int k = 0; k < array3.Length; k++)
			{
				stringBuilder.AppendFormat("{0:X2}", array3[k]);
			}
			return stringBuilder.ToString();
		}
		public static byte[] Encrypt_byte(string toEncrypt, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
			int num = (16 - toEncrypt.Length % 16) % 16;
			int num2 = bytes.Length + num;
			Console.WriteLine(string.Concat(new object[]
			{
				"size = ",
				num2,
				", pad = ",
				num
			}));
			bool flag = num == 0;
			if (flag)
			{
				num2 += 16;
			}
			byte[] array = new byte[num2];
			bytes.CopyTo(array, 0);
			bool flag2 = num > 0;
			if (flag2)
			{
				byte[] array2 = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = (byte)num;
				}
				array2.CopyTo(array, bytes.Length);
			}
			else
			{
				for (int j = num2 - 16; j < num2; j++)
				{
					array[j] = 16;
				}
			}
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(key.Substring(0, 16)),
				Mode = CipherMode.CBC,
				Padding = PaddingMode.Zeros
			}.CreateEncryptor();
			return cryptoTransform.TransformFinalBlock(array, 0, array.Length);
		}
		public static string Decrypt(string toDecrypt, string key)
		{
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(key.Substring(0, 16)),
				Mode = CipherMode.CBC,
				Padding = PaddingMode.PKCS7
			}.CreateDecryptor();
			byte[] array = Tool.StringArgToByteArray(toDecrypt);
			byte[] array2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < array2.Length && array2[num] != 0 && array2[num] != 16)
			{
				stringBuilder.Append(Encoding.UTF8.GetString(array2, num, 1));
				num++;
			}
			return stringBuilder.ToString();
		}
		public static string Decrypt_byte(byte[] toDecrypt, string key)
		{
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(key.Substring(0, 16)),
				Mode = CipherMode.CBC,
				Padding = PaddingMode.PKCS7
			}.CreateDecryptor();
			byte[] array = cryptoTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < array.Length && array[num] != 0 && array[num] != 16)
			{
				stringBuilder.Append(Encoding.UTF8.GetString(array, num, 1));
				num++;
			}
			return stringBuilder.ToString();
		}
		public static string EncryptString(string Value)
		{
			SymmetricAlgorithm symmetricAlgorithm = new DESCryptoServiceProvider();
			ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor(Convert.FromBase64String(Tool.CKEY), Convert.FromBase64String(Tool.CIV));
			byte[] bytes = Encoding.UTF8.GetBytes(Value);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
		public static string DecryptString(string Value)
		{
			SymmetricAlgorithm symmetricAlgorithm = new DESCryptoServiceProvider();
			ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor(Convert.FromBase64String(Tool.CKEY), Convert.FromBase64String(Tool.CIV));
			byte[] array = Convert.FromBase64String(Value);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}
		public static string GetChineseSpell(string strText)
		{
			return StrToPinyin.GetChineseSpell(strText);
		}
		public static string GetWebresourceFile(string url, string code)
		{
			WebClient webClient = new WebClient();
			webClient.Headers.Add(HttpRequestHeader.UserAgent, "anything");
			byte[] bytes;
			try
			{
				bytes = webClient.DownloadData(url);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			bool flag = "utf8" == code;
			string @string;
			if (flag)
			{
				@string = Encoding.UTF8.GetString(bytes);
			}
			else
			{
				@string = Encoding.Default.GetString(bytes);
			}
			return @string;
		}
		public static string GetCheckBoxListValue(CheckBoxList chkl, char joinstr, string mode = "text")
		{
			string text = "";
			foreach (ListItem listItem in chkl.Items)
			{
				bool selected = listItem.Selected;
				if (selected)
				{
					bool flag = mode == "text";
					if (flag)
					{
						text = text + listItem.Text + joinstr.ToString();
					}
					else
					{
						text = text + listItem.Value + joinstr.ToString();
					}
				}
			}
			bool flag2 = text.Length > 0;
			if (flag2)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}
		public static void SetCheckBoxListValue(CheckBoxList chkl, string values, string mode = "text")
		{
			foreach (ListItem listItem in chkl.Items)
			{
				bool flag = mode == "text";
				if (flag)
				{
					bool flag2 = values.Contains(listItem.Text);
					if (flag2)
					{
						listItem.Selected = true;
					}
					else
					{
						listItem.Selected = false;
					}
				}
				else
				{
					bool flag3 = values.Contains(listItem.Value);
					if (flag3)
					{
						listItem.Selected = true;
					}
					else
					{
						listItem.Selected = false;
					}
				}
			}
		}
		public static void SetDropDownListValue(DropDownList ddl, string values, string findtype)
		{
			bool flag = findtype.ToLower() == "value";
			ListItem listItem;
			if (flag)
			{
				listItem = ddl.Items.FindByValue(values);
			}
			else
			{
				listItem = ddl.Items.FindByText(values);
			}
			bool flag2 = listItem != null;
			if (flag2)
			{
				ddl.ClearSelection();
				listItem.Selected = true;
			}
		}
		public static bool GenHTMLFromURL(string strURL, string strRealPath, string strCode, bool isYaSuo = false)
		{
			bool flag = string.IsNullOrEmpty(strCode);
			if (flag)
			{
				strCode = "utf-8";
			}
			StreamWriter streamWriter = null;
			try
			{
				bool flag2 = File.Exists(strRealPath);
				if (flag2)
				{
					File.Delete(strRealPath);
				}
				streamWriter = new StreamWriter(strRealPath, false, Encoding.GetEncoding(strCode));
				WebRequest webRequest = WebRequest.Create(strURL);
				WebResponse response = webRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(strCode));
				string text = streamReader.ReadToEnd();
				Regex regex = new Regex("<input type=\"hidden\" name=\"__EVENTTARGET\".*/>", RegexOptions.IgnoreCase);
				Regex regex2 = new Regex("<input type=\"hidden\" name=\"__EVENTARGUMENT\".*/>", RegexOptions.IgnoreCase);
				Regex regex3 = new Regex("<input type=\"hidden\" name=\"__VIEWSTATE\".*/>", RegexOptions.IgnoreCase);
				Regex regex4 = new Regex("<form .*id=\"form1\">", RegexOptions.IgnoreCase);
				Regex regex5 = new Regex("</form>");
				Regex regex6 = new Regex("<input type=\"hidden\" name=\"__EVENTVALIDATION\".*/>", RegexOptions.IgnoreCase);
				text = regex.Replace(text, "");
				text = regex2.Replace(text, "");
				text = regex3.Replace(text, "");
				text = regex4.Replace(text, "");
				text = regex5.Replace(text, "");
				text = regex6.Replace(text, "");
				if (isYaSuo)
				{
					text = Regex.Replace(text, "\\n+\\s+", string.Empty);
				}
				streamWriter.Write(text);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				streamWriter.Flush();
				streamWriter.Close();
				streamWriter = null;
			}
			return true;
		}
		public static void GenHTMLFromTemp(string temppath, string genfilepath, List<string> sourcestr, List<string> replacestr, string strcode)
		{
			bool flag = string.IsNullOrEmpty(strcode);
			if (flag)
			{
				strcode = "utf-8";
			}
			Encoding encoding = Encoding.GetEncoding(strcode);
			StreamReader streamReader = null;
			StreamWriter streamWriter = null;
			string text = null;
			try
			{
				streamReader = new StreamReader(temppath, encoding);
				text = streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				streamReader.Close();
			}
			for (int i = 0; i < sourcestr.Count; i++)
			{
				bool flag2 = i <= replacestr.Count;
				if (flag2)
				{
					text = text.Replace(sourcestr[i], replacestr[i]);
				}
			}
			try
			{
				streamWriter = new StreamWriter(genfilepath, false, encoding);
				streamWriter.Write(text);
				streamWriter.Flush();
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			finally
			{
				streamWriter.Close();
			}
		}
		public static string GenRandomCode(string str, int num)
		{
			char[] array = str.ToCharArray();
			string text = "";
			Random random = new Random();
			for (int i = 0; i < num; i++)
			{
				text += str.Substring(random.Next(0, str.Length), 1);
			}
			return text;
		}
		public static bool DownloadFile(string _fileName, string _fullPath, long _speed)
		{
			HttpRequest request = HttpContext.Current.Request;
			HttpResponse response = HttpContext.Current.Response;
			bool result;
			try
			{
				FileStream fileStream = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				try
				{
					response.AddHeader("Accept-Ranges", "bytes");
					response.Buffer = false;
					long length = fileStream.Length;
					long num = 0L;
					int num2 = 10240;
					int millisecondsTimeout = (int)Math.Floor(1000m * num2 / _speed) + 1;
					bool flag = request.Headers["Range"] != null;
					if (flag)
					{
						response.StatusCode = 206;
						string[] array = request.Headers["Range"].Split(new char[]
						{
							'=',
							'-'
						});
						num = Convert.ToInt64(array[1]);
					}
					response.AddHeader("Content-Length", (length - num).ToString());
					bool flag2 = num > 0L;
					if (flag2)
					{
						response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", num, length - 1L, length));
					}
					response.AddHeader("Connection", "Keep-Alive");
					response.ContentType = "application/octet-stream";
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.UTF8;
					HttpBrowserCapabilities browser = request.Browser;
					string text = browser.Browser.ToString();
					string str = text.ToLower().Contains("ie") ? HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(_fileName)) : _fileName;
					response.AddHeader("Content-Disposition", "attachment;filename=" + str);
					binaryReader.BaseStream.Seek(num, SeekOrigin.Begin);
					int num3 = (int)Math.Floor((double)(length - num) / num2) + 1;
					for (int i = 0; i < num3; i++)
					{
						bool isClientConnected = response.IsClientConnected;
						if (isClientConnected)
						{
							response.BinaryWrite(binaryReader.ReadBytes(num2));
							Thread.Sleep(millisecondsTimeout);
						}
						else
						{
							i = num3;
						}
					}
					response.End();
				}
				catch
				{
					result = false;
					return result;
				}
				finally
				{
					binaryReader.Close();
					fileStream.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			result = true;
			return result;
		}
		public static DataTable RenderDataTableFromExcel(string path)
		{
			return DataTableRenderToExcel.RenderDataTableFromExcel(path);
		}
		public static DataTable RenderDataTableFromExcel(string filepath, int sheetnum)
		{
			return DataTableRenderToExcel.RenderDataTableFromExcel(filepath, sheetnum);
		}
		public static DataTable RenderDataTableFromExcel(string filepath, string sheetname)
		{
			return DataTableRenderToExcel.RenderDataTableFromExcel(filepath, sheetname);
		}
		public static Stream RenderDataTableToExcel(DataTable SourceTable)
		{
			return DataTableRenderToExcel.RenderDataTableToExcel(SourceTable);
		}
		public static List<string> GetExcelSheet(string filepath)
		{
			return DataTableRenderToExcel.GetExcelSheet(filepath);
		}
		
	}
}
