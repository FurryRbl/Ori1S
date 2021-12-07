using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Thai Buddhist calendar.</summary>
	// Token: 0x0200022C RID: 556
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with.NET")]
	[Serializable]
	public class ThaiBuddhistCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		// Token: 0x06001C85 RID: 7301 RVA: 0x00069660 File Offset: 0x00067860
		public ThaiBuddhistCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"T.B.C.E."
			};
			this.M_EraNames = new string[]
			{
				"ThaiBuddhist current era"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 2572;
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000696B4 File Offset: 0x000678B4
		static ThaiBuddhistCalendar()
		{
			ThaiBuddhistCalendar.M_EraHandler = new CCGregorianEraHandler();
			ThaiBuddhistCalendar.M_EraHandler.appendEra(1, CCGregorianCalendar.fixed_from_dmy(1, 1, -542));
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		/// <returns>An array that consists of a single element having a value that is always the current era.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0006970C File Offset: 0x0006790C
		public override int[] Eras
		{
			get
			{
				return (int[])ThaiBuddhistCalendar.M_EraHandler.Eras.Clone();
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00069724 File Offset: 0x00067924
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0006972C File Offset: 0x0006792C
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

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006975C File Offset: 0x0006795C
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = 1;
			}
			if (!ThaiBuddhistCalendar.M_EraHandler.ValidEra(era))
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00069790 File Offset: 0x00067990
		internal int M_CheckYEG(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			return ThaiBuddhistCalendar.M_EraHandler.GregorianYear(year, era);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000697A8 File Offset: 0x000679A8
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckYEG(year, ref era);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x000697B4 File Offset: 0x000679B4
		internal int M_CheckYMEG(int year, int month, ref int era)
		{
			int result = this.M_CheckYEG(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			return result;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000697EC File Offset: 0x000679EC
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
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x06001C8F RID: 7311 RVA: 0x00069820 File Offset: 0x00067A20
		public override DateTime AddMonths(DateTime time, int months)
		{
			return CCGregorianCalendar.AddMonths(time, months);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06001C90 RID: 7312 RVA: 0x0006982C File Offset: 0x00067A2C
		public override DateTime AddYears(DateTime time, int years)
		{
			return CCGregorianCalendar.AddYears(time, years);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C91 RID: 7313 RVA: 0x00069838 File Offset: 0x00067A38
		public override int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C92 RID: 7314 RVA: 0x00069840 File Offset: 0x00067A40
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C93 RID: 7315 RVA: 0x0006985C File Offset: 0x00067A5C
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
		// Token: 0x06001C94 RID: 7316 RVA: 0x00069864 File Offset: 0x00067A64
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
		// Token: 0x06001C95 RID: 7317 RVA: 0x00069884 File Offset: 0x00067A84
		public override int GetDaysInYear(int year, int era)
		{
			int year2 = this.M_CheckYEG(year, ref era);
			return CCGregorianCalendar.GetDaysInYear(year2);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C96 RID: 7318 RVA: 0x000698A4 File Offset: 0x00067AA4
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			ThaiBuddhistCalendar.M_EraHandler.EraYear(out result, date);
			return result;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>The return value is always 0 because the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class does not support the notion of a leap month.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		// Token: 0x06001C97 RID: 7319 RVA: 0x000698C8 File Offset: 0x00067AC8
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C98 RID: 7320 RVA: 0x000698CC File Offset: 0x00067ACC
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
		// Token: 0x06001C99 RID: 7321 RVA: 0x000698D4 File Offset: 0x00067AD4
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return 12;
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A 1-based positive integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <param name="rule">One of the <see cref="T:System.Globalization.CalendarWeekRule" /> values that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> or <paramref name="firstDayOfWeek" /> is outside the range supported by the calendar.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x06001C9A RID: 7322 RVA: 0x000698E4 File Offset: 0x00067AE4
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return base.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C9B RID: 7323 RVA: 0x000698F0 File Offset: 0x00067AF0
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			return ThaiBuddhistCalendar.M_EraHandler.EraYear(out num, date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001C9C RID: 7324 RVA: 0x00069914 File Offset: 0x00067B14
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
		// Token: 0x06001C9D RID: 7325 RVA: 0x00069938 File Offset: 0x00067B38
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
		// Token: 0x06001C9E RID: 7326 RVA: 0x00069948 File Offset: 0x00067B48
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
		// Token: 0x06001C9F RID: 7327 RVA: 0x00069968 File Offset: 0x00067B68
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int year2 = this.M_CheckYMDEG(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			return CCGregorianCalendar.ToDateTime(year2, month, day, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.ThaiBuddhistCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x000699A0 File Offset: 0x00067BA0
		public override int ToFourDigitYear(int year)
		{
			return base.ToFourDigitYear(year);
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" /> value.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x000699AC File Offset: 0x00067BAC
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class, which is equivalent to the first moment of January 1, 0001 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x000699B0 File Offset: 0x00067BB0
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return ThaiBuddhistCalendar.ThaiMin;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x000699B8 File Offset: 0x00067BB8
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return ThaiBuddhistCalendar.ThaiMax;
			}
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000AB2 RID: 2738
		public const int ThaiBuddhistEra = 1;

		// Token: 0x04000AB3 RID: 2739
		internal static readonly CCGregorianEraHandler M_EraHandler;

		// Token: 0x04000AB4 RID: 2740
		private static DateTime ThaiMin = new DateTime(1, 1, 1, 0, 0, 0);

		// Token: 0x04000AB5 RID: 2741
		private static DateTime ThaiMax = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
