using System;

namespace System.Globalization
{
	// Token: 0x020001FE RID: 510
	internal class CCJulianCalendar
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x0005ED74 File Offset: 0x0005CF74
		public static bool is_leap_year(int year)
		{
			return CCMath.mod(year, 4) == ((year <= 0) ? 3 : 0);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0005ED90 File Offset: 0x0005CF90
		public static int fixed_from_dmy(int day, int month, int year)
		{
			int num = (year >= 0) ? year : (year + 1);
			int num2 = -2;
			num2 += 365 * (num - 1);
			num2 += CCMath.div(num - 1, 4);
			num2 += CCMath.div(367 * month - 362, 12);
			if (month > 2)
			{
				num2 += ((!CCJulianCalendar.is_leap_year(year)) ? -2 : -1);
			}
			return num2 + day;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0005EE04 File Offset: 0x0005D004
		public static int year_from_fixed(int date)
		{
			int num = CCMath.div(4 * (date - -1) + 1464, 1461);
			return (num > 0) ? num : (num - 1);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0005EE38 File Offset: 0x0005D038
		public static void my_from_fixed(out int month, out int year, int date)
		{
			year = CCJulianCalendar.year_from_fixed(date);
			int num = date - CCJulianCalendar.fixed_from_dmy(1, 1, year);
			int num2;
			if (date < CCJulianCalendar.fixed_from_dmy(1, 3, year))
			{
				num2 = 0;
			}
			else if (CCJulianCalendar.is_leap_year(year))
			{
				num2 = 1;
			}
			else
			{
				num2 = 2;
			}
			month = CCMath.div(12 * (num + num2) + 373, 367);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005EE9C File Offset: 0x0005D09C
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			CCJulianCalendar.my_from_fixed(out month, out year, date);
			day = date - CCJulianCalendar.fixed_from_dmy(1, month, year) + 1;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0005EEB8 File Offset: 0x0005D0B8
		public static int month_from_fixed(int date)
		{
			int result;
			int num;
			CCJulianCalendar.my_from_fixed(out result, out num, date);
			return result;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0005EED0 File Offset: 0x0005D0D0
		public static int day_from_fixed(int date)
		{
			int result;
			int num;
			int num2;
			CCJulianCalendar.dmy_from_fixed(out result, out num, out num2, date);
			return result;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0005EEEC File Offset: 0x0005D0EC
		public static int date_difference(int dayA, int monthA, int yearA, int dayB, int monthB, int yearB)
		{
			return CCJulianCalendar.fixed_from_dmy(dayB, monthB, yearB) - CCJulianCalendar.fixed_from_dmy(dayA, monthA, yearA);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0005EF04 File Offset: 0x0005D104
		public static int day_number(int day, int month, int year)
		{
			return CCJulianCalendar.date_difference(31, 12, year - 1, day, month, year);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005EF18 File Offset: 0x0005D118
		public static int days_remaining(int day, int month, int year)
		{
			return CCJulianCalendar.date_difference(day, month, year, 31, 12, year);
		}

		// Token: 0x04000939 RID: 2361
		private const int epoch = -1;

		// Token: 0x020001FF RID: 511
		public enum Month
		{
			// Token: 0x0400093B RID: 2363
			january = 1,
			// Token: 0x0400093C RID: 2364
			february,
			// Token: 0x0400093D RID: 2365
			march,
			// Token: 0x0400093E RID: 2366
			april,
			// Token: 0x0400093F RID: 2367
			may,
			// Token: 0x04000940 RID: 2368
			june,
			// Token: 0x04000941 RID: 2369
			july,
			// Token: 0x04000942 RID: 2370
			august,
			// Token: 0x04000943 RID: 2371
			september,
			// Token: 0x04000944 RID: 2372
			october,
			// Token: 0x04000945 RID: 2373
			november,
			// Token: 0x04000946 RID: 2374
			december
		}
	}
}
