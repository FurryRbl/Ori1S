using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Hebrew calendar.</summary>
	// Token: 0x02000218 RID: 536
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with.NET")]
	[Serializable]
	public class HebrewCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.HebrewCalendar" /> class.</summary>
		// Token: 0x06001AE7 RID: 6887 RVA: 0x00063FBC File Offset: 0x000621BC
		public HebrewCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"A.M."
			};
			this.M_EraNames = new string[]
			{
				"Anno Mundi"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 5790;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00064048 File Offset: 0x00062248
		internal override int M_MaxYear
		{
			get
			{
				return 6000;
			}
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.HebrewCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.HebrewCalendar" /> type. The return value is always an array containing one element equal to <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />. </returns>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00064050 File Offset: 0x00062250
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					HebrewCalendar.HebrewEra
				};
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.HebrewCalendar" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the Hebrew calendar year value is less than 5343 but is not 99, or the year value is greater than 5999. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00064060 File Offset: 0x00062260
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00064068 File Offset: 0x00062268
		public override int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				base.CheckReadOnly();
				base.M_ArgumentInRange("value", value, 5343, this.M_MaxYear);
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0006409C File Offset: 0x0006229C
		internal void M_CheckDateTime(DateTime time)
		{
			if (time.Ticks < 499147488000000000L || time.Ticks > 706783967999999999L)
			{
				throw new ArgumentOutOfRangeException("time", "Only hebrew years between 5343 and 6000, inclusive, are supported.");
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000640E4 File Offset: 0x000622E4
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = HebrewCalendar.HebrewEra;
			}
			if (era != HebrewCalendar.HebrewEra)
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0006410C File Offset: 0x0006230C
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			if (year < 5343 || year > this.M_MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", "Only hebrew years between 5343 and 6000, inclusive, are supported.");
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00064148 File Offset: 0x00062348
		internal void M_CheckYME(int year, int month, ref int era)
		{
			this.M_CheckYE(year, ref era);
			int num = CCHebrewCalendar.last_month_of_year(year);
			if (month < 1 || month > num)
			{
				StringWriter stringWriter = new StringWriter();
				stringWriter.Write("Month must be between 1 and {0}.", num);
				throw new ArgumentOutOfRangeException("month", stringWriter.ToString());
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006419C File Offset: 0x0006239C
		internal void M_CheckYMDE(int year, int month, int day, ref int era)
		{
			this.M_CheckYME(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, era));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000. </exception>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000641CC File Offset: 0x000623CC
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime dateTime;
			if (months == 0)
			{
				dateTime = time;
			}
			else
			{
				int date = CCFixed.FromDateTime(time);
				int day;
				int num;
				int num2;
				CCHebrewCalendar.dmy_from_fixed(out day, out num, out num2, date);
				num = this.M_Month(num, num2);
				if (months < 0)
				{
					while (months < 0)
					{
						if (num + months > 0)
						{
							num += months;
							months = 0;
						}
						else
						{
							months += num;
							num2--;
							num = this.GetMonthsInYear(num2);
						}
					}
				}
				else
				{
					while (months > 0)
					{
						int monthsInYear = this.GetMonthsInYear(num2);
						if (num + months <= monthsInYear)
						{
							num += months;
							months = 0;
						}
						else
						{
							months -= monthsInYear - num + 1;
							num = 1;
							num2++;
						}
					}
				}
				dateTime = this.ToDateTime(num2, num, day, 0, 0, 0, 0).Add(time.TimeOfDay);
			}
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x000642A4 File Offset: 0x000624A4
		public override DateTime AddYears(DateTime time, int years)
		{
			int date = CCFixed.FromDateTime(time);
			int day;
			int month;
			int num;
			CCHebrewCalendar.dmy_from_fixed(out day, out month, out num, date);
			num += years;
			date = CCHebrewCalendar.fixed_from_dmy(day, month, num);
			DateTime dateTime = CCFixed.ToDateTime(date).Add(time.TimeOfDay);
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 30 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x000642F4 File Offset: 0x000624F4
		public override int GetDayOfMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			return CCHebrewCalendar.day_from_fixed(date);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x00064318 File Offset: 0x00062518
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 385 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than September 17, 1583 in the Gregorian calendar, or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />. </exception>
		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006433C File Offset: 0x0006253C
		public override int GetDayOfYear(DateTime time)
		{
			this.M_CheckDateTime(time);
			int num = CCFixed.FromDateTime(time);
			int year = CCHebrewCalendar.year_from_fixed(num);
			int num2 = CCHebrewCalendar.fixed_from_dmy(1, 7, year);
			return num - num2 + 1;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0006436C File Offset: 0x0006256C
		internal int M_CCMonth(int month, int year)
		{
			if (month <= 6)
			{
				return 6 + month;
			}
			int num = CCHebrewCalendar.last_month_of_year(year);
			if (num == 12)
			{
				return month - 6;
			}
			return (month > 7) ? (month - 7) : (6 + month);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000643AC File Offset: 0x000625AC
		internal int M_Month(int ccmonth, int year)
		{
			if (ccmonth >= 7)
			{
				return ccmonth - 6;
			}
			int num = CCHebrewCalendar.last_month_of_year(year);
			return ccmonth + ((num != 12) ? 7 : 6);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x06001AF9 RID: 6905 RVA: 0x000643DC File Offset: 0x000625DC
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			int month2 = this.M_CCMonth(month, year);
			return CCHebrewCalendar.last_day_of_month(month2, year);
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x06001AFA RID: 6906 RVA: 0x00064404 File Offset: 0x00062604
		public override int GetDaysInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			int num = CCHebrewCalendar.fixed_from_dmy(1, 7, year);
			int num2 = CCHebrewCalendar.fixed_from_dmy(1, 7, year + 1);
			return num2 - num;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />. The return value is always <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AFB RID: 6907 RVA: 0x00064434 File Offset: 0x00062634
		public override int GetEra(DateTime time)
		{
			this.M_CheckDateTime(time);
			return HebrewCalendar.HebrewEra;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. The return value is 7 if the <paramref name="year" /> and <paramref name="era" /> parameters specify a leap year, or 0 if the year is not a leap year.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is not <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.-or-<paramref name="year" /> is less than the Hebrew calendar year 5343 or greater than the Hebrew calendar year 5999.</exception>
		// Token: 0x06001AFC RID: 6908 RVA: 0x00064444 File Offset: 0x00062644
		public override int GetLeapMonth(int year, int era)
		{
			return (!this.IsLeapMonth(year, 7, era)) ? 0 : 7;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 13 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06001AFD RID: 6909 RVA: 0x0006445C File Offset: 0x0006265C
		public override int GetMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			int ccmonth;
			int year;
			CCHebrewCalendar.my_from_fixed(out ccmonth, out year, date);
			return this.M_Month(ccmonth, year);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era. The return value is either 12 in a common year, or 13 in a leap year.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x06001AFE RID: 6910 RVA: 0x0006448C File Offset: 0x0006268C
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCHebrewCalendar.last_month_of_year(year);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" /> value.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x06001AFF RID: 6911 RVA: 0x000644A0 File Offset: 0x000626A0
		public override int GetYear(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			return CCHebrewCalendar.year_from_fixed(date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="day">An integer from 1 to 30 that represents the day. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B00 RID: 6912 RVA: 0x000644C4 File Offset: 0x000626C4
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			return this.IsLeapYear(year) && (month == 7 || (month == 6 && day == 30));
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>true if the specified month is a leap month; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B01 RID: 6913 RVA: 0x00064504 File Offset: 0x00062704
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			return this.IsLeapYear(year) && month == 7;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B02 RID: 6914 RVA: 0x00064524 File Offset: 0x00062724
		public override bool IsLeapYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCHebrewCalendar.is_leap_year(year);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="day">An integer from 1 to 30 that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see cref="F:System.Globalization.Calendar.CurrentEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		// Token: 0x06001B03 RID: 6915 RVA: 0x00064538 File Offset: 0x00062738
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			int month2 = this.M_CCMonth(month, year);
			int date = CCHebrewCalendar.fixed_from_dmy(day, month2, year);
			return CCFixed.ToDateTime(date, hour, minute, second, (double)millisecond);
		}

		/// <summary>Converts the specified year to a 4-digit year by using the <see cref="P:System.Globalization.HebrewCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>If the <paramref name="year" /> parameter is a 2-digit year, the return value is the corresponding 4-digit year. If the <paramref name="year" /> parameter is a 4-digit year, the return value is the unchanged <paramref name="year" /> parameter.</returns>
		/// <param name="year">A 2-digit year from 0 through 99, or a 4-digit Hebrew calendar year from 5343 through 5999.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0.-or-<paramref name="year" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />. </exception>
		// Token: 0x06001B04 RID: 6916 RVA: 0x00064580 File Offset: 0x00062780
		public override int ToFourDigitYear(int year)
		{
			base.M_ArgumentInRange("year", year, 0, this.M_MaxYear - 1);
			int num = this.twoDigitYearMax % 100;
			int num2 = this.twoDigitYearMax - num;
			if (year >= 100)
			{
				return year;
			}
			if (year <= num)
			{
				return num2 + year;
			}
			return num2 + year - 100;
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>This property always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" /> value.</returns>
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000645D0 File Offset: 0x000627D0
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the first moment of January, 1, 1583 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000645D4 File Offset: 0x000627D4
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HebrewCalendar.Min;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the last moment of September, 29, 2239 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x000645DC File Offset: 0x000627DC
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HebrewCalendar.Max;
			}
		}

		// Token: 0x04000A23 RID: 2595
		internal const long M_MinTicks = 499147488000000000L;

		// Token: 0x04000A24 RID: 2596
		internal const long M_MaxTicks = 706783967999999999L;

		// Token: 0x04000A25 RID: 2597
		internal const int M_MinYear = 5343;

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000A26 RID: 2598
		public static readonly int HebrewEra = 1;

		// Token: 0x04000A27 RID: 2599
		private static DateTime Min = new DateTime(1583, 1, 1, 0, 0, 0);

		// Token: 0x04000A28 RID: 2600
		private static DateTime Max = new DateTime(2239, 9, 29, 11, 59, 59);
	}
}
