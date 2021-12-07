using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Japanese calendar.</summary>
	// Token: 0x0200021D RID: 541
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class JapaneseCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JapaneseCalendar" /> class.</summary>
		// Token: 0x06001B49 RID: 6985 RVA: 0x000658EC File Offset: 0x00063AEC
		public JapaneseCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"M",
				"T",
				"S",
				"H"
			};
			this.M_EraNames = new string[]
			{
				"Meiji",
				"Taisho",
				"Showa",
				"Heisei"
			};
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00065958 File Offset: 0x00063B58
		static JapaneseCalendar()
		{
			JapaneseCalendar.M_EraHandler = new CCGregorianEraHandler();
			JapaneseCalendar.M_EraHandler.appendEra(1, CCGregorianCalendar.fixed_from_dmy(8, 9, 1868), CCGregorianCalendar.fixed_from_dmy(29, 7, 1912));
			JapaneseCalendar.M_EraHandler.appendEra(2, CCGregorianCalendar.fixed_from_dmy(30, 7, 1912), CCGregorianCalendar.fixed_from_dmy(24, 12, 1926));
			JapaneseCalendar.M_EraHandler.appendEra(3, CCGregorianCalendar.fixed_from_dmy(25, 12, 1926), CCGregorianCalendar.fixed_from_dmy(7, 1, 1989));
			JapaneseCalendar.M_EraHandler.appendEra(4, CCGregorianCalendar.fixed_from_dmy(8, 1, 1989));
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.JapaneseCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.JapaneseCalendar" />.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x00065A24 File Offset: 0x00063C24
		public override int[] Eras
		{
			get
			{
				return (int[])JapaneseCalendar.M_EraHandler.Eras.Clone();
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x00065A3C File Offset: 0x00063C3C
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x00065A44 File Offset: 0x00063C44
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

		// Token: 0x06001B4E RID: 6990 RVA: 0x00065A74 File Offset: 0x00063C74
		internal void M_CheckDateTime(DateTime time)
		{
			JapaneseCalendar.M_EraHandler.CheckDateTime(time);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00065A84 File Offset: 0x00063C84
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = 4;
			}
			if (!JapaneseCalendar.M_EraHandler.ValidEra(era))
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00065AB8 File Offset: 0x00063CB8
		internal int M_CheckYEG(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			return JapaneseCalendar.M_EraHandler.GregorianYear(year, era);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00065AD0 File Offset: 0x00063CD0
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckYEG(year, ref era);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00065ADC File Offset: 0x00063CDC
		internal int M_CheckYMEG(int year, int month, ref int era)
		{
			int result = this.M_CheckYEG(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			return result;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00065B14 File Offset: 0x00063D14
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
		// Token: 0x06001B54 RID: 6996 RVA: 0x00065B48 File Offset: 0x00063D48
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
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the supported range of the <see cref="T:System.Globalization.JapaneseCalendar" /> type.-or-<paramref name="years" /> is less than -10,000 or greater than 10,000. </exception>
		// Token: 0x06001B55 RID: 6997 RVA: 0x00065B68 File Offset: 0x00063D68
		public override DateTime AddYears(DateTime time, int years)
		{
			DateTime dateTime = CCGregorianCalendar.AddYears(time, years);
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B56 RID: 6998 RVA: 0x00065B88 File Offset: 0x00063D88
		public override int GetDayOfMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B57 RID: 6999 RVA: 0x00065B98 File Offset: 0x00063D98
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B58 RID: 7000 RVA: 0x00065BBC File Offset: 0x00063DBC
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
		// Token: 0x06001B59 RID: 7001 RVA: 0x00065BCC File Offset: 0x00063DCC
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
		// Token: 0x06001B5A RID: 7002 RVA: 0x00065BEC File Offset: 0x00063DEC
		public override int GetDaysInYear(int year, int era)
		{
			int year2 = this.M_CheckYEG(year, ref era);
			return CCGregorianCalendar.GetDaysInYear(year2);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06001B5B RID: 7003 RVA: 0x00065C0C File Offset: 0x00063E0C
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			JapaneseCalendar.M_EraHandler.EraYear(out result, date);
			return result;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>The return value is always 0 because the <see cref="T:System.Globalization.JapaneseCalendar" /> type does not support the notion of a leap month.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.JapaneseCalendar" /> type.</exception>
		// Token: 0x06001B5C RID: 7004 RVA: 0x00065C30 File Offset: 0x00063E30
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B5D RID: 7005 RVA: 0x00065C34 File Offset: 0x00063E34
		public override int GetMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCGregorianCalendar.GetMonth(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The return value is always 12.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B5E RID: 7006 RVA: 0x00065C44 File Offset: 0x00063E44
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return 12;
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A 1-based integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <param name="rule">One of the <see cref="T:System.Globalization.CalendarWeekRule" /> values that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> or <paramref name="firstDayOfWeek" /> is outside the range supported by the calendar.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x06001B5F RID: 7007 RVA: 0x00065C54 File Offset: 0x00063E54
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return base.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B60 RID: 7008 RVA: 0x00065C60 File Offset: 0x00063E60
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			return JapaneseCalendar.M_EraHandler.EraYear(out num, date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true, if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B61 RID: 7009 RVA: 0x00065C84 File Offset: 0x00063E84
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
		// Token: 0x06001B62 RID: 7010 RVA: 0x00065CA8 File Offset: 0x00063EA8
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.M_CheckYMEG(year, month, ref era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true, if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B63 RID: 7011 RVA: 0x00065CB8 File Offset: 0x00063EB8
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
		// Token: 0x06001B64 RID: 7012 RVA: 0x00065CD8 File Offset: 0x00063ED8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int year2 = this.M_CheckYMDEG(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			return CCGregorianCalendar.ToDateTime(year2, month, day, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.JapaneseCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">An integer (usually two digits) that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001B65 RID: 7013 RVA: 0x00065D10 File Offset: 0x00063F10
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
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x00065D40 File Offset: 0x00063F40
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the current <see cref="T:System.Globalization.JapaneseCalendar" /> object.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JapaneseCalendar" /> type, which is equivalent to the first moment of January 1, 1868 C.E. in the Gregorian calendar. </returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00065D44 File Offset: 0x00063F44
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.JapanMin;
			}
		}

		/// <summary>Gets the latest date and time supported by the current <see cref="T:System.Globalization.JapaneseCalendar" /> object.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JapaneseCalendar" /> type, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x00065D4C File Offset: 0x00063F4C
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.JapanMax;
			}
		}

		// Token: 0x04000A3A RID: 2618
		internal static readonly CCGregorianEraHandler M_EraHandler;

		// Token: 0x04000A3B RID: 2619
		private static DateTime JapanMin = new DateTime(1868, 9, 8, 0, 0, 0);

		// Token: 0x04000A3C RID: 2620
		private static DateTime JapanMax = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
