using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace UIHelper
{
	public static class Totp
	{
		private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private static TimeSpan _timestep = TimeSpan.FromMinutes(3.0);
		private static readonly Encoding _encoding = new UTF8Encoding(false, true);
		private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
			byte[] array = hashAlgorithm.ComputeHash(Totp.ApplyModifier(bytes, modifier));
			int num = (int)(array[array.Length - 1] & 15);
			Debug.Assert(num + 4 < array.Length);
			int num2 = (int)(array[num] & 127) << 24 | (int)(array[num + 1] & 255) << 16 | (int)(array[num + 2] & 255) << 8 | (int)(array[num + 3] & 255);
			return num2 % 1000000;
		}
		private static byte[] ApplyModifier(byte[] input, string modifier)
		{
			bool flag = string.IsNullOrEmpty(modifier);
			byte[] result;
			if (flag)
			{
				result = input;
			}
			else
			{
				byte[] bytes = Totp._encoding.GetBytes(modifier);
				byte[] array = new byte[checked(input.Length + bytes.Length)];
				Buffer.BlockCopy(input, 0, array, 0, input.Length);
				Buffer.BlockCopy(bytes, 0, array, input.Length, bytes.Length);
				result = array;
			}
			return result;
		}
		private static ulong GetCurrentTimeStepNumber()
		{
			return (ulong)((DateTime.UtcNow - Totp._unixEpoch).Ticks / Totp._timestep.Ticks);
		}
		public static int GenerateCode(byte[] securityToken, string modifier = null)
		{
			bool flag = securityToken == null;
			if (flag)
			{
				throw new ArgumentNullException("securityToken");
			}
			ulong currentTimeStepNumber = Totp.GetCurrentTimeStepNumber();
			int result;
			using (HMACSHA1 hMACSHA = new HMACSHA1(securityToken))
			{
				result = Totp.ComputeTotp(hMACSHA, currentTimeStepNumber, modifier);
			}
			return result;
		}
		public static bool ValidateCode(byte[] securityToken, int code, string modifier = null)
		{
			bool flag = securityToken == null;
			if (flag)
			{
				throw new ArgumentNullException("securityToken");
			}
			ulong currentTimeStepNumber = Totp.GetCurrentTimeStepNumber();
			bool result;
			using (HMACSHA1 hMACSHA = new HMACSHA1(securityToken))
			{
				for (int i = -2; i <= 2; i++)
				{
					int num = Totp.ComputeTotp(hMACSHA, currentTimeStepNumber + (ulong)((long)i), modifier);
					bool flag2 = num == code;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}
		public static int GenerateCode(string securityToken, string modifier = null)
		{
			return Totp.GenerateCode(Encoding.Unicode.GetBytes(securityToken), modifier);
		}
		public static bool ValidateCode(string securityToken, int code, string modifier = null)
		{
			return Totp.ValidateCode(Encoding.Unicode.GetBytes(securityToken), code, modifier);
		}
	}
}
