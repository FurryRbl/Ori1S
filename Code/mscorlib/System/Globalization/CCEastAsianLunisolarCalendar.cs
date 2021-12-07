using System;

namespace System.Globalization
{
	// Token: 0x02000204 RID: 516
	internal class CCEastAsianLunisolarCalendar
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x0005F440 File Offset: 0x0005D640
		public static int fixed_from_dmy(int day, int month, int year)
		{
			throw new Exception("fixed_from_dmy");
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0005F44C File Offset: 0x0005D64C
		public static int year_from_fixed(int date)
		{
			throw new Exception("year_from_fixed");
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0005F458 File Offset: 0x0005D658
		public static void my_from_fixed(out int month, out int year, int date)
		{
			throw new Exception("my_from_fixed");
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0005F464 File Offset: 0x0005D664
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			throw new Exception("dmy_from_fixed");
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0005F470 File Offset: 0x0005D670
		public static DateTime AddMonths(DateTime date, int months)
		{
			throw new Exception("AddMonths");
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0005F47C File Offset: 0x0005D67C
		public static DateTime AddYears(DateTime date, int years)
		{
			throw new Exception("AddYears");
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0005F488 File Offset: 0x0005D688
		public static int GetDayOfMonth(DateTime date)
		{
			throw new Exception("GetDayOfMonth");
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005F494 File Offset: 0x0005D694
		public static int GetDayOfYear(DateTime date)
		{
			throw new Exception("GetDayOfYear");
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0005F4A0 File Offset: 0x0005D6A0
		public static int GetDaysInMonth(int gyear, int month)
		{
			throw new Exception("GetDaysInMonth");
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0005F4AC File Offset: 0x0005D6AC
		public static int GetDaysInYear(int year)
		{
			throw new Exception("GetDaysInYear");
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005F4B8 File Offset: 0x0005D6B8
		public static int GetMonth(DateTime date)
		{
			throw new Exception("GetMonth");
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005F4C4 File Offset: 0x0005D6C4
		public static bool IsLeapMonth(int gyear, int month)
		{
			int num = gyear % 19;
			bool flag = false;
			bool flag2 = false;
			double num2 = 0.0;
			for (int i = 0; i < num; i++)
			{
				int num3 = 0;
				for (int j = 1; j <= month; j++)
				{
					if (flag2)
					{
						num3 += 30;
						flag2 = false;
						if (i == num && j == month)
						{
							return true;
						}
					}
					else
					{
						num3 += ((!flag) ? 29 : 30);
						flag = !flag;
						num2 += 30.44;
						if (num2 - (double)num3 > 29.0)
						{
							flag2 = true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005F574 File Offset: 0x0005D774
		public static bool IsLeapYear(int gyear)
		{
			int num = gyear % 19;
			int num2 = num;
			switch (num2)
			{
			case 6:
			case 9:
			case 11:
				break;
			default:
				switch (num2)
				{
				case 0:
				case 3:
					break;
				default:
					switch (num2)
					{
					case 14:
					case 17:
						return true;
					}
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
		public static DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			throw new Exception("ToDateTime");
		}

		// Token: 0x04000965 RID: 2405
		private const int initial_epact = 29;
	}
}
