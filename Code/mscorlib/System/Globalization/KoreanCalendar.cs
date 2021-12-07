using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Korean calendar.</summary>
	// Token: 0x02000220 RID: 544
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class KoreanCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.KoreanCalendar" /> class.</summary>
		// Token: 0x06001B8D RID: 7053 RVA: 0x00066244 File Offset: 0x00064444
		public KoreanCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"K.C.E."
			};
			this.M_EraNames = new string[]
			{
				"Korean Current Era"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 4362;
			}
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00066298 File Offset: 0x00064498
		static KoreanCalendar()
		{
			KoreanCalendar.M_EraHandler = new CCGregorianEraHandler();
			KoreanCalendar.M_EraHandler.appendEra(1, CCGregorianCalendar.fixed_from_dmy(1, 1, -2332));
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.KoreanCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.KoreanCalendar" />.</returns>
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000662F0 File Offset: 0x000644F0
		public override int[] Eras
		{
			get
			{
				return (int[])KoreanCalendar.M_EraHandler.Eras.Clone();
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00066308 File Offset: 0x00064508
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x00066310 File Offset: 0x00064510
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

		// Token: 0x06001B92 RID: 7058 RVA: 0x00066340 File Offset: 0x00064540
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = 1;
			}
			if (!KoreanCalendar.M_EraHandler.ValidEra(era))
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00066374 File Offset: 0x00064574
		internal int M_CheckYEG(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			return KoreanCalendar.M_EraHandler.GregorianYear(year, era);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0006638C File Offset: 0x0006458C
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckYEG(year, ref era);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00066398 File Offset: 0x00064598
		internal int M_CheckYMEG(int year, int month, ref int era)
		{
			int result = this.M_CheckYEG(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			return result;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000663D0 File Offset: 0x000645D0
		internal int M_CheckYMDEG(int year, int month, int day, ref int era)
		{
			int result = this.M_CheckYMEG(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, era));
			return result;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x06001B97 RID: 7063 RVA: 0x00066404 File Offset: 0x00064604
		public override DateTime AddMonths(DateTime time, int months)
		{
			return CCGregorianCalendar.AddMonths(time, months);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> or <paramref name="time" /> is out of range.</exception>
		// Token: 0x06001B98 RID: 7064 RVA: 0x00066410 File Offset: 0x00064610
		public override DateTime AddYears(DateTime time, int years)
		{
			return CCGregorianCalendar.AddYears(time, years);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B99 RID: 7065 RVA: 0x0006641C File Offset: 0x0006461C
		public override int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B9A RID: 7066 RVA: 0x00066424 File Offset: 0x00064624
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B9B RID: 7067 RVA: 0x00066440 File Offset: 0x00064640
		public override int GetDayOfYear(DateTime time)
		{
			return CCGregorianCalendar.GetDayOfYear(time);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B9C RID: 7068 RVA: 0x00066448 File Offset: 0x00064648
		public override int GetDaysInMonth(int year, int month, int era)
		{
			int year2 = this.M_CheckYMEG(year, month, ref era);
			return CCGregorianCalendar.GetDaysInMonth(year2, month);
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B9D RID: 7069 RVA: 0x00066468 File Offset: 0x00064668
		public override int GetDaysInYear(int year, int era)
		{
			int year2 = this.M_CheckYEG(year, ref era);
			return CCGregorianCalendar.GetDaysInYear(year2);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B9E RID: 7070 RVA: 0x00066488 File Offset: 0x00064688
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			KoreanCalendar.M_EraHandler.EraYear(out result, date);
			return result;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>The return value is always 0 because the <see cref="T:System.Globalization.KoreanCalendar" /> class does not support the notion of a leap month.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		// Token: 0x06001B9F RID: 7071 RVA: 0x000664AC File Offset: 0x000646AC
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001BA0 RID: 7072 RVA: 0x000664B0 File Offset: 0x000646B0
		public override int GetMonth(DateTime time)
		{
			return CCGregorianCalendar.GetMonth(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001BA1 RID: 7073 RVA: 0x000664B8 File Offset: 0x000646B8
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYEG(year, ref era);
			return 12;
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A 1-based integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <param name="rule">One of the <see cref="T:System.Globalization.CalendarWeekRule" /> values that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> or <paramref name="firstDayOfWeek" /> is outside the range supported by the calendar.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x06001BA2 RID: 7074 RVA: 0x000664C8 File Offset: 0x000646C8
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return base.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001BA3 RID: 7075 RVA: 0x000664D4 File Offset: 0x000646D4
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			return KoreanCalendar.M_EraHandler.EraYear(out num, date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x000664F8 File Offset: 0x000646F8
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int year2 = this.M_CheckYMDEG(year, month, day, ref era);
			return CCGregorianCalendar.IsLeapDay(year2, month, day);
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>This method always returns false, unless overridden by a derived class.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x0006651C File Offset: 0x0006471C
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.M_CheckYMEG(year, month, ref era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006652C File Offset: 0x0006472C
		public override bool IsLeapYear(int year, int era)
		{
			int year2 = this.M_CheckYEG(year, ref era);
			return CCGregorianCalendar.is_leap_year(year2);
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
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006654C File Offset: 0x0006474C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int year2 = this.M_CheckYMDEG(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			return CCGregorianCalendar.ToDateTime(year2, month, day, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.KoreanCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001BA8 RID: 7080 RVA: 0x00066584 File Offset: 0x00064784
		public override int ToFourDigitYear(int year)
		{
			return base.ToFourDigitYear(year);
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" /> value.</returns>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00066590 File Offset: 0x00064790
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.KoreanCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.KoreanCalendar" /> class, which is equivalent to the first moment of January 1, 0001 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00066594 File Offset: 0x00064794
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return KoreanCalendar.KoreanMin;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.KoreanCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.KoreanCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0006659C File Offset: 0x0006479C
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return KoreanCalendar.KoreanMax;
			}
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000A44 RID: 2628
		public const int KoreanEra = 1;

		// Token: 0x04000A45 RID: 2629
		internal static readonly CCGregorianEraHandler M_EraHandler;

		// Token: 0x04000A46 RID: 2630
		private static DateTime KoreanMin = new DateTime(1, 1, 1, 0, 0, 0);

		// Token: 0x04000A47 RID: 2631
		private static DateTime KoreanMax = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
