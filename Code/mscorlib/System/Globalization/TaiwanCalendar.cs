using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>the Taiwan calendar.</summary>
	// Token: 0x02000227 RID: 551
	[MonoTODO("Serialization format not compatible with.NET")]
	[ComVisible(true)]
	[Serializable]
	public class TaiwanCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		// Token: 0x06001C41 RID: 7233 RVA: 0x00068688 File Offset: 0x00066888
		public TaiwanCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"T.C.E."
			};
			this.M_EraNames = new string[]
			{
				"Taiwan current era"
			};
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000686C4 File Offset: 0x000668C4
		static TaiwanCalendar()
		{
			TaiwanCalendar.M_EraHandler = new CCGregorianEraHandler();
			TaiwanCalendar.M_EraHandler.appendEra(1, CCGregorianCalendar.fixed_from_dmy(1, 1, 1912));
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.TaiwanCalendar" />.</summary>
		/// <returns>An array that consists of a single element for which the value is always the current era.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00068720 File Offset: 0x00066920
		public override int[] Eras
		{
			get
			{
				return (int[])TaiwanCalendar.M_EraHandler.Eras.Clone();
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00068738 File Offset: 0x00066938
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x00068740 File Offset: 0x00066940
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

		// Token: 0x06001C46 RID: 7238 RVA: 0x00068770 File Offset: 0x00066970
		internal void M_CheckDateTime(DateTime time)
		{
			TaiwanCalendar.M_EraHandler.CheckDateTime(time);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00068780 File Offset: 0x00066980
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = 1;
			}
			if (!TaiwanCalendar.M_EraHandler.ValidEra(era))
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000687B4 File Offset: 0x000669B4
		internal int M_CheckYEG(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			return TaiwanCalendar.M_EraHandler.GregorianYear(year, era);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000687CC File Offset: 0x000669CC
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckYEG(year, ref era);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000687D8 File Offset: 0x000669D8
		internal int M_CheckYMEG(int year, int month, ref int era)
		{
			int result = this.M_CheckYEG(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			return result;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00068810 File Offset: 0x00066A10
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
		// Token: 0x06001C4C RID: 7244 RVA: 0x00068844 File Offset: 0x00066A44
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime dateTime = CCGregorianCalendar.AddMonths(time, months);
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06001C4D RID: 7245 RVA: 0x00068864 File Offset: 0x00066A64
		public override DateTime AddYears(DateTime time, int years)
		{
			DateTime dateTime = CCGregorianCalendar.AddYears(time, years);
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C4E RID: 7246 RVA: 0x00068884 File Offset: 0x00066A84
		public override int GetDayOfMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C4F RID: 7247 RVA: 0x00068894 File Offset: 0x00066A94
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C50 RID: 7248 RVA: 0x000688B8 File Offset: 0x00066AB8
		public override int GetDayOfYear(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCGregorianCalendar.GetDayOfYear(time);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001C51 RID: 7249 RVA: 0x000688C8 File Offset: 0x00066AC8
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
		// Token: 0x06001C52 RID: 7250 RVA: 0x000688E8 File Offset: 0x00066AE8
		public override int GetDaysInYear(int year, int era)
		{
			int year2 = this.M_CheckYEG(year, ref era);
			return CCGregorianCalendar.GetDaysInYear(year2);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C53 RID: 7251 RVA: 0x00068908 File Offset: 0x00066B08
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			TaiwanCalendar.M_EraHandler.EraYear(out result, date);
			return result;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>The return value is always 0 because the <see cref="T:System.Globalization.TaiwanCalendar" /> class does not support the notion of a leap month.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		// Token: 0x06001C54 RID: 7252 RVA: 0x0006892C File Offset: 0x00066B2C
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C55 RID: 7253 RVA: 0x00068930 File Offset: 0x00066B30
		public override int GetMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCGregorianCalendar.GetMonth(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001C56 RID: 7254 RVA: 0x00068940 File Offset: 0x00066B40
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYEG(year, ref era);
			return 12;
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <param name="rule">One of the <see cref="T:System.Globalization.CalendarWeekRule" /> values that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> or <paramref name="firstDayOfWeek" /> is outside the range supported by the calendar.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x06001C57 RID: 7255 RVA: 0x00068950 File Offset: 0x00066B50
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return base.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C58 RID: 7256 RVA: 0x0006895C File Offset: 0x00066B5C
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			return TaiwanCalendar.M_EraHandler.EraYear(out num, date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001C59 RID: 7257 RVA: 0x00068980 File Offset: 0x00066B80
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
		// Token: 0x06001C5A RID: 7258 RVA: 0x000689A4 File Offset: 0x00066BA4
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
		// Token: 0x06001C5B RID: 7259 RVA: 0x000689B4 File Offset: 0x00066BB4
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
		// Token: 0x06001C5C RID: 7260 RVA: 0x000689D4 File Offset: 0x00066BD4
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int year2 = this.M_CheckYMDEG(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			return CCGregorianCalendar.ToDateTime(year2, month, day, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.TaiwanCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001C5D RID: 7261 RVA: 0x00068A0C File Offset: 0x00066C0C
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", "Non-negative number required.");
			}
			int num = 0;
			this.M_CheckYE(year, ref num);
			return year;
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" /> value.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00068A3C File Offset: 0x00066C3C
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class, which is equivalent to the first moment of January 1, 1912 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00068A40 File Offset: 0x00066C40
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.TaiwanMin;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00068A48 File Offset: 0x00066C48
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.TaiwanMax;
			}
		}

		// Token: 0x04000A98 RID: 2712
		internal static readonly CCGregorianEraHandler M_EraHandler;

		// Token: 0x04000A99 RID: 2713
		private static DateTime TaiwanMin = new DateTime(1912, 1, 1, 0, 0, 0);

		// Token: 0x04000A9A RID: 2714
		private static DateTime TaiwanMax = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
