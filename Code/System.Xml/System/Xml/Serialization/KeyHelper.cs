using System;
using System.Globalization;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x02000259 RID: 601
	internal class KeyHelper
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x0007A360 File Offset: 0x00078560
		public static void AddField(StringBuilder sb, int n, string val)
		{
			KeyHelper.AddField(sb, n, val, null);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0007A36C File Offset: 0x0007856C
		public static void AddField(StringBuilder sb, int n, string val, string def)
		{
			if (val != def)
			{
				sb.Append(n.ToString());
				sb.Append(val.Length.ToString(CultureInfo.InvariantCulture));
				sb.Append(val);
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0007A3B8 File Offset: 0x000785B8
		public static void AddField(StringBuilder sb, int n, bool val)
		{
			KeyHelper.AddField(sb, n, val, false);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0007A3C4 File Offset: 0x000785C4
		public static void AddField(StringBuilder sb, int n, bool val, bool def)
		{
			if (val != def)
			{
				sb.Append(n.ToString());
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0007A3DC File Offset: 0x000785DC
		public static void AddField(StringBuilder sb, int n, int val, int def)
		{
			if (val != def)
			{
				sb.Append(n.ToString());
				sb.Append(val.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0007A414 File Offset: 0x00078614
		public static void AddField(StringBuilder sb, int n, Type val)
		{
			if (val != null)
			{
				sb.Append(n.ToString(CultureInfo.InvariantCulture));
				sb.Append(val.ToString());
			}
		}
	}
}
