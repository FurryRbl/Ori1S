using System;

namespace System.Globalization
{
	// Token: 0x020001FC RID: 508
	internal class CCGregorianCalendar
	{
		// Token: 0x06001953 RID: 6483 RVA: 0x0005E99C File Offset: 0x0005CB9C
		public static bool is_leap_year(int year)
		{
			if (CCMath.mod(year, 4) != 0)
			{
				return false;
			}
			int num = CCMath.mod(year, 400);
			return num != 100 && num != 200 && num != 300;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0005E9F0 File Offset: 0x0005CBF0
		public static int fixed_from_dmy(int day, int month, int year)
		{
			int num = 0;
			num += 365 * (year - 1);
			num += CCMath.div(year - 1, 4);
			num -= CCMath.div(year - 1, 100);
			num += CCMath.div(year - 1, 400);
			num += CCMath.div(367 * month - 362, 12);
			if (month > 2)
			{
				num += ((!CCGregorianCalendar.is_leap_year(year)) ? -2 : -1);
			}
			return num + day;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0005EA70 File Offset: 0x0005CC70
		public static int year_from_fixed(int date)
		{
			int x = date - 1;
			int num = CCMath.div_mod(out x, x, 146097);
			int num2 = CCMath.div_mod(out x, x, 36524);
			int num3 = CCMath.div_mod(out x, x, 1461);
			int num4 = CCMath.div(x, 365);
			int num5 = 400 * num + 100 * num2 + 4 * num3 + num4;
			return (num2 != 4 && num4 != 4) ? (num5 + 1) : num5;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005EAE8 File Offset: 0x0005CCE8
		public static void my_from_fixed(out int month, out int year, int date)
		{
			year = CCGregorianCalendar.year_from_fixed(date);
			int num = date - CCGregorianCalendar.fixed_from_dmy(1, 1, year);
			int num2;
			if (date < CCGregorianCalendar.fixed_from_dmy(1, 3, year))
			{
				num2 = 0;
			}
			else if (CCGregorianCalendar.is_leap_year(year))
			{
				num2 = 1;
			}
			else
			{
				num2 = 2;
			}
			month = CCMath.div(12 * (num + num2) + 373, 367);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0005EB4C File Offset: 0x0005CD4C
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			CCGregorianCalendar.my_from_fixed(out month, out year, date);
			day = date - CCGregorianCalendar.fixed_from_dmy(1, month, year) + 1;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0005EB68 File Offset: 0x0005CD68
		public static int month_from_fixed(int date)
		{
			int result;
			int num;
			CCGregorianCalendar.my_from_fixed(out result, out num, date);
			return result;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005EB80 File Offset: 0x0005CD80
		public static int day_from_fixed(int date)
		{
			int result;
			int num;
			int num2;
			CCGregorianCalendar.dmy_from_fixed(out result, out num, out num2, date);
			return result;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005EB9C File Offset: 0x0005CD9C
		public static int date_difference(int dayA, int monthA, int yearA, int dayB, int monthB, int yearB)
		{
			return CCGregorianCalendar.fixed_from_dmy(dayB, monthB, yearB) - CCGregorianCalendar.fixed_from_dmy(dayA, monthA, yearA);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005EBB4 File Offset: 0x0005CDB4
		public static int day_number(int day, int month, int year)
		{
			return CCGregorianCalendar.date_difference(31, 12, year - 1, day, month, year);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0005EBC8 File Offset: 0x0005CDC8
		public static int days_remaining(int day, int month, int year)
		{
			return CCGregorianCalendar.date_difference(day, month, year, 31, 12, year);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		public static DateTime AddMonths(DateTime time, int months)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			int num2;
			int num3;
			CCGregorianCalendar.dmy_from_fixed(out num, out num2, out num3, date);
			num2 += months;
			num3 += CCMath.div_mod(out num2, num2, 12);
			int daysInMonth = CCGregorianCalendar.GetDaysInMonth(num3, num2);
			if (num > daysInMonth)
			{
				num = daysInMonth;
			}
			date = CCGregorianCalendar.fixed_from_dmy(num, num2, num3);
			return CCFixed.ToDateTime(date).Add(time.TimeOfDay);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0005EC3C File Offset: 0x0005CE3C
		public static DateTime AddYears(DateTime time, int years)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			int month;
			int num2;
			CCGregorianCalendar.dmy_from_fixed(out num, out month, out num2, date);
			num2 += years;
			int daysInMonth = CCGregorianCalendar.GetDaysInMonth(num2, month);
			if (num > daysInMonth)
			{
				num = daysInMonth;
			}
			date = CCGregorianCalendar.fixed_from_dmy(num, month, num2);
			return CCFixed.ToDateTime(date).Add(time.TimeOfDay);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0005EC94 File Offset: 0x0005CE94
		public static int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.day_from_fixed(CCFixed.FromDateTime(time));
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
		public static int GetDayOfYear(DateTime time)
		{
			int num = CCFixed.FromDateTime(time);
			int year = CCGregorianCalendar.year_from_fixed(num);
			int num2 = CCGregorianCalendar.fixed_from_dmy(1, 1, year);
			return num - num2 + 1;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005ECD0 File Offset: 0x0005CED0
		public static int GetDaysInMonth(int year, int month)
		{
			int num = CCGregorianCalendar.fixed_from_dmy(1, month, year);
			int num2 = CCGregorianCalendar.fixed_from_dmy(1, month + 1, year);
			return num2 - num;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0005ECF4 File Offset: 0x0005CEF4
		public static int GetDaysInYear(int year)
		{
			int num = CCGregorianCalendar.fixed_from_dmy(1, 1, year);
			int num2 = CCGregorianCalendar.fixed_from_dmy(1, 1, year + 1);
			return num2 - num;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005ED18 File Offset: 0x0005CF18
		public static int GetMonth(DateTime time)
		{
			return CCGregorianCalendar.month_from_fixed(CCFixed.FromDateTime(time));
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0005ED28 File Offset: 0x0005CF28
		public static int GetYear(DateTime time)
		{
			return CCGregorianCalendar.year_from_fixed(CCFixed.FromDateTime(time));
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0005ED38 File Offset: 0x0005CF38
		public static bool IsLeapDay(int year, int month, int day)
		{
			return CCGregorianCalendar.is_leap_year(year) && month == 2 && day == 29;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005ED54 File Offset: 0x0005CF54
		public static DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int milliseconds)
		{
			return CCFixed.ToDateTime(CCGregorianCalendar.fixed_from_dmy(day, month, year), hour, minute, second, (double)milliseconds);
		}

		// Token: 0x0400092B RID: 2347
		private const int epoch = 1;

		// Token: 0x020001FD RID: 509
		public enum Month
		{
			// Token: 0x0400092D RID: 2349
			january = 1,
			// Token: 0x0400092E RID: 2350
			february,
			// Token: 0x0400092F RID: 2351
			march,
			// Token: 0x04000930 RID: 2352
			april,
			// Token: 0x04000931 RID: 2353
			may,
			// Token: 0x04000932 RID: 2354
			june,
			// Token: 0x04000933 RID: 2355
			july,
			// Token: 0x04000934 RID: 2356
			august,
			// Token: 0x04000935 RID: 2357
			september,
			// Token: 0x04000936 RID: 2358
			october,
			// Token: 0x04000937 RID: 2359
			november,
			// Token: 0x04000938 RID: 2360
			december
		}
	}
}
