using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace UIHelper
{
	internal class IPSearch
	{
		public struct IPLocation
		{
			public string country;
			public string area;
		}
		private FileStream ipFile;
		private long ip;
		private string ipfilePath;
		public IPSearch(string ipfilePath)
		{
			this.ipfilePath = ipfilePath;
		}
		public IPSearch.IPLocation GetIPLocation(string strIP)
		{
			this.ip = this.IPToLong(strIP);
			this.ipFile = new FileStream(this.ipfilePath, FileMode.Open, FileAccess.Read);
			long[] array = this.BlockToArray(this.ReadIPBlock());
			long num = (long)(this.SearchIP(array, 0, array.Length - 1) * 7 + 4);
			this.ipFile.Position += num;
			this.ipFile.Position = this.ReadLongX(3) + 4L;
			IPSearch.IPLocation result = default(IPSearch.IPLocation);
			int num2 = this.ipFile.ReadByte();
			bool flag = num2 == 1;
			if (flag)
			{
				this.ipFile.Position = this.ReadLongX(3);
				num2 = this.ipFile.ReadByte();
			}
			long position = this.ipFile.Position;
			result.country = this.ReadString(num2);
			bool flag2 = num2 == 2;
			if (flag2)
			{
				this.ipFile.Position = position + 3L;
			}
			num2 = this.ipFile.ReadByte();
			result.area = this.ReadString(num2);
			this.ipFile.Close();
			this.ipFile = null;
			return result;
		}
		public long IPToLong(string strIP)
		{
			byte[] array = new byte[8];
			string[] array2 = strIP.Split(new char[]
			{
				'.'
			});
			for (int i = 0; i < 4; i++)
			{
				array[i] = byte.Parse(array2[3 - i]);
			}
			return BitConverter.ToInt64(array, 0);
		}
		private long[] BlockToArray(byte[] ipBlock)
		{
			long[] array = new long[ipBlock.Length / 7];
			int num = 0;
			byte[] array2 = new byte[8];
			for (int i = 0; i < ipBlock.Length; i += 7)
			{
				Array.Copy(ipBlock, i, array2, 0, 4);
				array[num] = BitConverter.ToInt64(array2, 0);
				num++;
			}
			return array;
		}
		private int SearchIP(long[] ipArray, int start, int end)
		{
			int num = (start + end) / 2;
			bool flag = num == start;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				bool flag2 = this.ip < ipArray[num];
				if (flag2)
				{
					result = this.SearchIP(ipArray, start, num);
				}
				else
				{
					result = this.SearchIP(ipArray, num, end);
				}
			}
			return result;
		}
		private byte[] ReadIPBlock()
		{
			long num = this.ReadLongX(4);
			long num2 = this.ReadLongX(4);
			long num3 = (num2 - num) / 7L + 1L;
			this.ipFile.Position = num;
			byte[] array = new byte[num3 * 7L];
			this.ipFile.Read(array, 0, array.Length);
			this.ipFile.Position = num;
			return array;
		}
		private long ReadLongX(int bytesCount)
		{
			byte[] array = new byte[8];
			this.ipFile.Read(array, 0, bytesCount);
			return BitConverter.ToInt64(array, 0);
		}
		private string ReadString(int flag)
		{
			bool flag2 = flag == 1 || flag == 2;
			if (flag2)
			{
				this.ipFile.Position = this.ReadLongX(3);
			}
			else
			{
				this.ipFile.Position -= 1L;
			}
			List<byte> list = new List<byte>();
			for (byte b = (byte)this.ipFile.ReadByte(); b > 0; b = (byte)this.ipFile.ReadByte())
			{
				list.Add(b);
			}
			return Encoding.Default.GetString(list.ToArray());
		}
	}
}
