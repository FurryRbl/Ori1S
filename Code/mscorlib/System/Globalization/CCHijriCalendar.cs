using System;

namespace System.Globalization
{
	// Token: 0x02000202 RID: 514
	internal class CCHijriCalendar
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x0005F2E4 File Offset: 0x0005D4E4
		public static bool is_leap_year(int year)
		{
			return CCMath.mod(14 + 11 * year, 30) < 11;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
		public static int fixed_from_dmy(int day, int month, int year)
		{
			int num = 227013;
			num += 354 * (year - 1);
			num += CCMath.div(3 + 11 * year, 30);
			num += (int)Math.Ceiling(29.5 * (double)(month - 1));
			return num + day;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005F344 File Offset: 0x0005D544
		public static int year_from_fixed(int date)
		{
			return CCMath.div(30 * (date - 227014) + 10646, 10631);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005F360 File Offset: 0x0005D560
		public static void my_from_fixed(out int month, out int year, int date)
		{
			year = CCHijriCalendar.year_from_fixed(date);
			int num = 1 + (int)Math.Ceiling((double)(date - 29 - CCHijriCalendar.fixed_from_dmy(1, 1, year)) / 29.5);
			month = ((num >= 12) ? 12 : num);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005F3AC File Offset: 0x0005D5AC
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			CCHijriCalendar.my_from_fixed(out month, out year, date);
			day = date - CCHijriCalendar.fixed_from_dmy(1, month, year) + 1;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005F3C8 File Offset: 0x0005D5C8
		public static int month_from_fixed(int date)
		{
			int result;
			int num;
			CCHijriCalendar.my_from_fixed(out result, out num, date);
			return result;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005F3E0 File Offset: 0x0005D5E0
		public static int day_from_fixed(int date)
		{
			int result;
			int num;
			int num2;
			CCHijriCalendar.dmy_from_fixed(out result, out num, out num2, date);
			return result;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005F3FC File Offset: 0x0005D5FC
		public static int date_difference(int dayA, int monthA, int yearA, int dayB, int monthB, int yearB)
		{
			return CCHijriCalendar.fixed_from_dmy(dayB, monthB, yearB) - CCHijriCalendar.fixed_from_dmy(dayA, monthA, yearA);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005F414 File Offset: 0x0005D614
		public static int day_number(int day, int month, int year)
		{
			return CCHijriCalendar.date_difference(31, 12, year - 1, day, month, year);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0005F428 File Offset: 0x0005D628
		public static int days_remaining(int day, int month, int year)
		{
			return CCHijriCalendar.date_difference(day, month, year, 31, 12, year);
		}

		// Token: 0x04000957 RID: 2391
		private const int epoch = 227014;

		// Token: 0x02000203 RID: 515
		public enum Month
		{
			// Token: 0x04000959 RID: 2393
			muharram = 1,
			// Token: 0x0400095A RID: 2394
			safar,
			// Token: 0x0400095B RID: 2395
			rabi_I,
			// Token: 0x0400095C RID: 2396
			rabi_II,
			// Token: 0x0400095D RID: 2397
			jumada_I,
			// Token: 0x0400095E RID: 2398
			jumada_II,
			// Token: 0x0400095F RID: 2399
			rajab,
			// Token: 0x04000960 RID: 2400
			shaban,
			// Token: 0x04000961 RID: 2401
			ramadan,
			// Token: 0x04000962 RID: 2402
			shawwal,
			// Token: 0x04000963 RID: 2403
			dhu_al_quada,
			// Token: 0x04000964 RID: 2404
			dhu_al_hijja
		}
	}
}
