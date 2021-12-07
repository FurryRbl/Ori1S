using System;

namespace System.Globalization
{
	// Token: 0x020001FB RID: 507
	internal class CCFixed
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x0005E8D4 File Offset: 0x0005CAD4
		public static DateTime ToDateTime(int date)
		{
			long ticks = (long)(date - 1) * 864000000000L;
			return new DateTime(ticks);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0005E8F8 File Offset: 0x0005CAF8
		public static DateTime ToDateTime(int date, int hour, int minute, int second, double milliseconds)
		{
			return CCFixed.ToDateTime(date).AddHours((double)hour).AddMinutes((double)minute).AddSeconds((double)second).AddMilliseconds(milliseconds);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0005E934 File Offset: 0x0005CB34
		public static int FromDateTime(DateTime time)
		{
			return 1 + (int)(time.Ticks / 864000000000L);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005E94C File Offset: 0x0005CB4C
		public static DayOfWeek day_of_week(int date)
		{
			return (DayOfWeek)CCMath.mod(date, 7);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0005E958 File Offset: 0x0005CB58
		public static int kday_on_or_before(int date, int k)
		{
			return date - (int)CCFixed.day_of_week(date - k);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005E964 File Offset: 0x0005CB64
		public static int kday_on_or_after(int date, int k)
		{
			return CCFixed.kday_on_or_before(date + 6, k);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0005E970 File Offset: 0x0005CB70
		public static int kd_nearest(int date, int k)
		{
			return CCFixed.kday_on_or_before(date + 3, k);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0005E97C File Offset: 0x0005CB7C
		public static int kday_after(int date, int k)
		{
			return CCFixed.kday_on_or_before(date + 7, k);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0005E988 File Offset: 0x0005CB88
		public static int kday_before(int date, int k)
		{
			return CCFixed.kday_on_or_before(date - 1, k);
		}
	}
}
