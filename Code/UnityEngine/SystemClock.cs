using System;

namespace UnityEngine
{
	// Token: 0x0200030A RID: 778
	internal class SystemClock
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x00036EC4 File Offset: 0x000350C4
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00036ECC File Offset: 0x000350CC
		public static long ToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00036EF8 File Offset: 0x000350F8
		public static long ToUnixTimeSeconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalSeconds);
		}

		// Token: 0x04000C0B RID: 3083
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
