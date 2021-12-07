using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x02000320 RID: 800
	internal sealed class HttpUtility
	{
		// Token: 0x06001BFB RID: 7163 RVA: 0x00050ABC File Offset: 0x0004ECBC
		private HttpUtility()
		{
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00050AC4 File Offset: 0x0004ECC4
		public static string UrlDecode(string s)
		{
			return HttpUtility.UrlDecode(s, null);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00050AD0 File Offset: 0x0004ECD0
		private static char[] GetChars(MemoryStream b, Encoding e)
		{
			return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		public static string UrlDecode(string s, Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
			{
				return s;
			}
			if (e == null)
			{
				e = Encoding.GetEncoding(28591);
			}
			StringBuilder stringBuilder = new StringBuilder();
			long num = (long)s.Length;
			NumberStyles style = NumberStyles.HexNumber;
			MemoryStream memoryStream = new MemoryStream();
			int num2 = 0;
			while ((long)num2 < num)
			{
				if (s[num2] == '%' && (long)(num2 + 2) < num)
				{
					if (s[num2 + 1] == 'u' && (long)(num2 + 5) < num)
					{
						if (memoryStream.Length > 0L)
						{
							stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
							memoryStream.SetLength(0L);
						}
						stringBuilder.Append((char)int.Parse(s.Substring(num2 + 2, 4), style));
						num2 += 5;
					}
					else
					{
						memoryStream.WriteByte((byte)int.Parse(s.Substring(num2 + 1, 2), style));
						num2 += 2;
					}
				}
				else
				{
					if (memoryStream.Length > 0L)
					{
						stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
						memoryStream.SetLength(0L);
					}
					if (s[num2] == '+')
					{
						stringBuilder.Append(' ');
					}
					else
					{
						stringBuilder.Append(s[num2]);
					}
				}
				num2++;
			}
			if (memoryStream.Length > 0L)
			{
				stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
			}
			return stringBuilder.ToString();
		}
	}
}
