using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Julian calendar.</summary>
	// Token: 0x0200021F RID: 543
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class JulianCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		// Token: 0x06001B70 RID: 7024 RVA: 0x00065E34 File Offset: 0x00064034
		public JulianCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"C.E."
			};
			this.M_EraNames = new string[]
			{
				"Common Era"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 2029;
			}
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.JulianCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.JulianCalendar" />.</returns>
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x00065EBC File Offset: 0x000640BC
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					JulianCalendar.JulianEra
				};
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00065ECC File Offset: 0x000640CC
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x00065ED4 File Offset: 0x000640D4
		public override int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				base.CheckReadOnly();
				base.M_ArgumentInRange("value", value, 100, this.M_MaxYear);
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00065F04 File Offset: 0x00064104
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = JulianCalendar.JulianEra;
			}
			if (era != JulianCalendar.JulianEra)
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00065F2C File Offset: 0x0006412C
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			base.M_ArgumentInRange("year", year, 1, 9999);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00065F54 File Offset: 0x00064154
		internal void M_CheckYME(int year, int month, ref int era)
		{
			this.M_CheckYE(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00065F80 File Offset: 0x00064180
		internal void M_CheckYMDE(int year, int month, int day, ref int era)
		{
			this.M_CheckYME(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, era));
			if (year == 9999 && ((month == 10 && day > 19) || month > 10))
			{
				throw new ArgumentOutOfRangeException("The maximum Julian date is 19. 10. 9999.");
			}
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x06001B79 RID: 7033 RVA: 0x00065FE0 File Offset: 0x000641E0
		public override DateTime AddMonths(DateTime time, int months)
		{
			int date = CCFixed.FromDateTime(time);
			int day;
			int num;
			int num2;
			CCJulianCalendar.dmy_from_fixed(out day, out num, out num2, date);
			num += months;
			num2 += CCMath.div_mod(out num, num, 12);
			date = CCJulianCalendar.fixed_from_dmy(day, num, num2);
			return CCFixed.ToDateTime(date).Add(time.TimeOfDay);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06001B7A RID: 7034 RVA: 0x00066030 File Offset: 0x00064230
		public override DateTime AddYears(DateTime time, int years)
		{
			int date = CCFixed.FromDateTime(time);
			int day;
			int month;
			int num;
			CCJulianCalendar.dmy_from_fixed(out day, out month, out num, date);
			num += years;
			date = CCJulianCalendar.fixed_from_dmy(day, month, num);
			return CCFixed.ToDateTime(date).Add(time.TimeOfDay);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B7B RID: 7035 RVA: 0x00066074 File Offset: 0x00064274
		public override int GetDayOfMonth(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCJulianCalendar.day_from_fixed(date);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B7C RID: 7036 RVA: 0x00066090 File Offset: 0x00064290
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B7D RID: 7037 RVA: 0x000660AC File Offset: 0x000642AC
		public override int GetDayOfYear(DateTime time)
		{
			int num = CCFixed.FromDateTime(time);
			int year = CCJulianCalendar.year_from_fixed(num);
			int num2 = CCJulianCalendar.fixed_from_dmy(1, 1, year);
			return num - num2 + 1;
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B7E RID: 7038 RVA: 0x000660D8 File Offset: 0x000642D8
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			int num = CCJulianCalendar.fixed_from_dmy(1, month, year);
			int num2 = CCJulianCalendar.fixed_from_dmy(1, month + 1, year);
			return num2 - num;
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B7F RID: 7039 RVA: 0x00066108 File Offset: 0x00064308
		public override int GetDaysInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			int num = CCJulianCalendar.fixed_from_dmy(1, 1, year);
			int num2 = CCJulianCalendar.fixed_from_dmy(1, 1, year + 1);
			return num2 - num;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B80 RID: 7040 RVA: 0x00066138 File Offset: 0x00064338
		public override int GetEra(DateTime time)
		{
			return JulianCalendar.JulianEra;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. Alternatively, this method returns zero if the calendar does not support a leap month, or if <paramref name="year" /> and <paramref name="era" /> do not specify a leap year.</returns>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		// Token: 0x06001B81 RID: 7041 RVA: 0x00066140 File Offset: 0x00064340
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B82 RID: 7042 RVA: 0x00066144 File Offset: 0x00064344
		public override int GetMonth(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCJulianCalendar.month_from_fixed(date);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B83 RID: 7043 RVA: 0x00066160 File Offset: 0x00064360
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B84 RID: 7044 RVA: 0x00066170 File Offset: 0x00064370
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCJulianCalendar.year_from_fixed(date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B85 RID: 7045 RVA: 0x0006618C File Offset: 0x0006438C
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			return this.IsLeapYear(year) && month == 2 && day == 29;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>This method always returns false, unless overridden by a derived class.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B86 RID: 7046 RVA: 0x000661C0 File Offset: 0x000643C0
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B87 RID: 7047 RVA: 0x000661D0 File Offset: 0x000643D0
		public override bool IsLeapYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCJulianCalendar.is_leap_year(year);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B88 RID: 7048 RVA: 0x000661E4 File Offset: 0x000643E4
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			int date = CCJulianCalendar.fixed_from_dmy(day, month, year);
			return CCFixed.ToDateTime(date, hour, minute, second, (double)millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.JulianCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B89 RID: 7049 RVA: 0x00066224 File Offset: 0x00064424
		public override int ToFourDigitYear(int year)
		{
			return base.ToFourDigitYear(year);
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" /> type.</returns>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00066230 File Offset: 0x00064430
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class, which is equivalent to the first moment of January 1, 0001 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00066234 File Offset: 0x00064434
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JulianCalendar.JulianMin;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0006623C File Offset: 0x0006443C
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JulianCalendar.JulianMax;
			}
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000A41 RID: 2625
		public static readonly int JulianEra = 1;

		// Token: 0x04000A42 RID: 2626
		private static DateTime JulianMin = new DateTime(1, 1, 1, 0, 0, 0);

		// Token: 0x04000A43 RID: 2627
		private static DateTime JulianMax = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
