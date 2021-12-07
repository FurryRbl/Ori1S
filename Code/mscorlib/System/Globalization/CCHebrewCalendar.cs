using System;

namespace System.Globalization
{
	// Token: 0x02000200 RID: 512
	internal class CCHebrewCalendar
	{
		// Token: 0x06001973 RID: 6515 RVA: 0x0005EF30 File Offset: 0x0005D130
		public static bool is_leap_year(int year)
		{
			return CCMath.mod(7 * year + 1, 19) < 7;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0005EF44 File Offset: 0x0005D144
		public static int last_month_of_year(int year)
		{
			return (!CCHebrewCalendar.is_leap_year(year)) ? 12 : 13;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0005EF5C File Offset: 0x0005D15C
		public static int elapsed_days(int year)
		{
			int num = CCMath.div(235 * year - 234, 19);
			int num3;
			int num2 = CCMath.div_mod(out num3, num, 1080);
			int x = 204 + 793 * num3;
			int x2 = 11 + 12 * num + 793 * num2 + CCMath.div(x, 1080);
			int num4 = 29 * num + CCMath.div(x2, 24);
			if (CCMath.mod(3 * (num4 + 1), 7) < 3)
			{
				num4++;
			}
			return num4;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
		public static int new_year_delay(int year)
		{
			int num = CCHebrewCalendar.elapsed_days(year);
			int num2 = CCHebrewCalendar.elapsed_days(year + 1);
			if (num2 - num == 356)
			{
				return 2;
			}
			int num3 = CCHebrewCalendar.elapsed_days(year - 1);
			if (num - num3 == 382)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0005F02C File Offset: 0x0005D22C
		public static int last_day_of_month(int month, int year)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", "Month should be between One and Thirteen.");
			}
			switch (month)
			{
			case 2:
				return 29;
			case 4:
				return 29;
			case 6:
				return 29;
			case 8:
				if (!CCHebrewCalendar.long_heshvan(year))
				{
					return 29;
				}
				break;
			case 9:
				if (CCHebrewCalendar.short_kislev(year))
				{
					return 29;
				}
				break;
			case 10:
				return 29;
			case 12:
				if (!CCHebrewCalendar.is_leap_year(year))
				{
					return 29;
				}
				break;
			case 13:
				return 29;
			}
			return 30;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		public static bool long_heshvan(int year)
		{
			return CCMath.mod(CCHebrewCalendar.days_in_year(year), 10) == 5;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005F0F8 File Offset: 0x0005D2F8
		public static bool short_kislev(int year)
		{
			return CCMath.mod(CCHebrewCalendar.days_in_year(year), 10) == 3;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005F10C File Offset: 0x0005D30C
		public static int days_in_year(int year)
		{
			return CCHebrewCalendar.fixed_from_dmy(1, 7, year + 1) - CCHebrewCalendar.fixed_from_dmy(1, 7, year);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005F124 File Offset: 0x0005D324
		public static int fixed_from_dmy(int day, int month, int year)
		{
			int num = -1373428;
			num += CCHebrewCalendar.elapsed_days(year);
			num += CCHebrewCalendar.new_year_delay(year);
			if (month < 7)
			{
				int num2 = CCHebrewCalendar.last_month_of_year(year);
				for (int i = 7; i <= num2; i++)
				{
					num += CCHebrewCalendar.last_day_of_month(i, year);
				}
				for (int i = 1; i < month; i++)
				{
					num += CCHebrewCalendar.last_day_of_month(i, year);
				}
			}
			else
			{
				for (int i = 7; i < month; i++)
				{
					num += CCHebrewCalendar.last_day_of_month(i, year);
				}
			}
			return num + day;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0005F1B8 File Offset: 0x0005D3B8
		public static int year_from_fixed(int date)
		{
			int num = (int)Math.Floor((double)(date - -1373427) / 365.24682220597794);
			int num2 = num;
			while (date >= CCHebrewCalendar.fixed_from_dmy(1, 7, num2))
			{
				num2++;
			}
			return num2 - 1;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		public static void my_from_fixed(out int month, out int year, int date)
		{
			year = CCHebrewCalendar.year_from_fixed(date);
			int num = (date >= CCHebrewCalendar.fixed_from_dmy(1, 1, year)) ? 1 : 7;
			month = num;
			while (date > CCHebrewCalendar.fixed_from_dmy(CCHebrewCalendar.last_day_of_month(month, year), month, year))
			{
				month++;
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0005F250 File Offset: 0x0005D450
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			CCHebrewCalendar.my_from_fixed(out month, out year, date);
			day = date - CCHebrewCalendar.fixed_from_dmy(1, month, year) + 1;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005F26C File Offset: 0x0005D46C
		public static int month_from_fixed(int date)
		{
			int result;
			int num;
			CCHebrewCalendar.my_from_fixed(out result, out num, date);
			return result;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0005F284 File Offset: 0x0005D484
		public static int day_from_fixed(int date)
		{
			int result;
			int num;
			int num2;
			CCHebrewCalendar.dmy_from_fixed(out result, out num, out num2, date);
			return result;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005F2A0 File Offset: 0x0005D4A0
		public static int date_difference(int dayA, int monthA, int yearA, int dayB, int monthB, int yearB)
		{
			return CCHebrewCalendar.fixed_from_dmy(dayB, monthB, yearB) - CCHebrewCalendar.fixed_from_dmy(dayA, monthA, yearA);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005F2B8 File Offset: 0x0005D4B8
		public static int day_number(int day, int month, int year)
		{
			return CCHebrewCalendar.date_difference(1, 7, year, day, month, year) + 1;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005F2C8 File Offset: 0x0005D4C8
		public static int days_remaining(int day, int month, int year)
		{
			return CCHebrewCalendar.date_difference(day, month, year, 1, 7, year + 1) - 1;
		}

		// Token: 0x04000947 RID: 2375
		private const int epoch = -1373427;

		// Token: 0x02000201 RID: 513
		public enum Month
		{
			// Token: 0x04000949 RID: 2377
			nisan = 1,
			// Token: 0x0400094A RID: 2378
			iyyar,
			// Token: 0x0400094B RID: 2379
			sivan,
			// Token: 0x0400094C RID: 2380
			tammuz,
			// Token: 0x0400094D RID: 2381
			av,
			// Token: 0x0400094E RID: 2382
			elul,
			// Token: 0x0400094F RID: 2383
			tishri,
			// Token: 0x04000950 RID: 2384
			heshvan,
			// Token: 0x04000951 RID: 2385
			kislev,
			// Token: 0x04000952 RID: 2386
			teveth,
			// Token: 0x04000953 RID: 2387
			shevat,
			// Token: 0x04000954 RID: 2388
			adar,
			// Token: 0x04000955 RID: 2389
			adar_I = 12,
			// Token: 0x04000956 RID: 2390
			adar_II
		}
	}
}
