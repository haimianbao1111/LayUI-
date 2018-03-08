using System;
namespace UIHelper
{
	public class TypeChange
	{
		public static double StringToDouble(string str, double d = 0.0)
		{
			double.TryParse(str, out d);
			return d;
		}
		public static int StringToInt(string str, int i = 0)
		{
			int.TryParse(str, out i);
			return i;
		}
	}
}
