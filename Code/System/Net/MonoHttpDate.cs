using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000356 RID: 854
	internal class MonoHttpDate
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x0005CA74 File Offset: 0x0005AC74
		internal static DateTime Parse(string dateStr)
		{
			return DateTime.ParseExact(dateStr, MonoHttpDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces).ToLocalTime();
		}

		// Token: 0x040012CB RID: 4811
		private static readonly string rfc1123_date = "r";

		// Token: 0x040012CC RID: 4812
		private static readonly string rfc850_date = "dddd, dd-MMM-yy HH:mm:ss G\\MT";

		// Token: 0x040012CD RID: 4813
		private static readonly string asctime_date = "ddd MMM d HH:mm:ss yyyy";

		// Token: 0x040012CE RID: 4814
		private static readonly string[] formats = new string[]
		{
			MonoHttpDate.rfc1123_date,
			MonoHttpDate.rfc850_date,
			MonoHttpDate.asctime_date
		};
	}
}
