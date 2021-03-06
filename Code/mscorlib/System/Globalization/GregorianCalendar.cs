using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Gregorian calendar.</summary>
	// Token: 0x02000216 RID: 534
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class GregorianCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.GregorianCalendar" /> class using the specified <see cref="T:System.Globalization.GregorianCalendarTypes" /> value.</summary>
		/// <param name="type">The <see cref="T:System.Globalization.GregorianCalendarTypes" /> value that denotes which language version of the calendar to create. </param>
		// Token: 0x06001AC7 RID: 6855 RVA: 0x00063CA0 File Offset: 0x00061EA0
		public GregorianCalendar(GregorianCalendarTypes type)
		{
			this.CalendarType = type;
			this.M_AbbrEraNames = new string[]
			{
				"AD"
			};
			this.M_EraNames = new string[]
			{
				"A.D."
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 2029;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.GregorianCalendar" /> class using the default <see cref="T:System.Globalization.GregorianCalendarTypes" /> value.</summary>
		// Token: 0x06001AC8 RID: 6856 RVA: 0x00063CFC File Offset: 0x00061EFC
		public GregorianCalendar() : this(GregorianCalendarTypes.Localized)
		{
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.GregorianCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.GregorianCalendar" />.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x00063D08 File Offset: 0x00061F08
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					1
				};
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00063D14 File Offset: 0x00061F14
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00063D1C File Offset: 0x00061F1C
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

		/// <summary>Gets or sets the <see cref="T:System.Globalization.GregorianCalendarTypes" /> value that denotes the language version of the current <see cref="T:System.Globalization.GregorianCalendar" />.</summary>
		/// <returns>A <see cref="T:System.Globalization.GregorianCalendarTypes" /> value that denotes the language version of the current <see cref="T:System.Globalization.GregorianCalendar" />.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00063D4C File Offset: 0x00061F4C
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00063D54 File Offset: 0x00061F54
		public virtual GregorianCalendarTypes CalendarType
		{
			get
			{
				return this.m_type;
			}
			set
			{
				base.CheckReadOnly();
				this.m_type = value;
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00063D64 File Offset: 0x00061F64
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = 1;
			}
			if (era != 1)
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00063D84 File Offset: 0x00061F84
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			base.M_ArgumentInRange("year", year, 1, 9999);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00063DAC File Offset: 0x00061FAC
		internal void M_CheckYME(int year, int month, ref int era)
		{
			this.M_CheckYE(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00063DD8 File Offset: 0x00061FD8
		internal void M_CheckYMDE(int year, int month, int day, ref int era)
		{
			this.M_CheckYME(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, era));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00063E08 File Offset: 0x00062008
		public override DateTime AddMonths(DateTime time, int months)
		{
			return CCGregorianCalendar.AddMonths(time, months);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06001AD3 RID: 6867 RVA: 0x00063E14 File Offset: 0x00062014
		public override DateTime AddYears(DateTime time, int years)
		{
			return CCGregorianCalendar.AddYears(time, years);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AD4 RID: 6868 RVA: 0x00063E20 File Offset: 0x00062020
		public override int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AD5 RID: 6869 RVA: 0x00063E28 File Offset: 0x00062028
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AD6 RID: 6870 RVA: 0x00063E44 File Offset: 0x00062044
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
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001AD7 RID: 6871 RVA: 0x00063E4C File Offset: 0x0006204C
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			return CCGregorianCalendar.GetDaysInMonth(year, month);
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06001AD8 RID: 6872 RVA: 0x00063E60 File Offset: 0x00062060
		public override int GetDaysInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCGregorianCalendar.GetDaysInYear(year);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AD9 RID: 6873 RVA: 0x00063E74 File Offset: 0x00062074
		public override int GetEra(DateTime time)
		{
			return 1;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>Always 0 because the <see cref="T:System.Globalization.GregorianCalendar" /> type does not recognize leap months.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify either <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> or GregorianCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the Gregorian calendar year 1 or greater than the Gregorian calendar year 9999.-or-<paramref name="era" /> is not <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> or GregorianCalendar.Eras[Calendar.CurrentEra].</exception>
		// Token: 0x06001ADA RID: 6874 RVA: 0x00063E78 File Offset: 0x00062078
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001ADB RID: 6875 RVA: 0x00063E7C File Offset: 0x0006207C
		public override int GetMonth(DateTime time)
		{
			return CCGregorianCalendar.GetMonth(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001ADC RID: 6876 RVA: 0x00063E84 File Offset: 0x00062084
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return 12;
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A 1-based integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> object to read. </param>
		/// <param name="rule">One of the <see cref="T:System.Globalization.CalendarWeekRule" /> values that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="firstDayOfWeek" /> is outside the range supported by the calendar.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x06001ADD RID: 6877 RVA: 0x00063E94 File Offset: 0x00062094
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return base.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001ADE RID: 6878 RVA: 0x00063EA0 File Offset: 0x000620A0
		public override int GetYear(DateTime time)
		{
			return CCGregorianCalendar.GetYear(time);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001ADF RID: 6879 RVA: 0x00063EA8 File Offset: 0x000620A8
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			return CCGregorianCalendar.IsLeapDay(year, month, day);
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>This method always returns false, unless overridden by a derived class.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001AE0 RID: 6880 RVA: 0x00063EC0 File Offset: 0x000620C0
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
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00063ED0 File Offset: 0x000620D0
		public override bool IsLeapYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCGregorianCalendar.is_leap_year(year);
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
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. </exception>
		// Token: 0x06001AE2 RID: 6882 RVA: 0x00063EE4 File Offset: 0x000620E4
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			return CCGregorianCalendar.ToDateTime(year, month, day, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.GregorianCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06001AE3 RID: 6883 RVA: 0x00063F1C File Offset: 0x0006211C
		public override int ToFourDigitYear(int year)
		{
			return base.ToFourDigitYear(year);
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>A <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" /> object.</returns>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00063F28 File Offset: 0x00062128
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type, which is the first moment of January 1, 0001 C.E. and is equivalent to <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00063F2C File Offset: 0x0006212C
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				DateTime? min = GregorianCalendar.Min;
				if (min == null)
				{
					GregorianCalendar.Min = new DateTime?(new DateTime(1, 1, 1, 0, 0, 0));
				}
				return GregorianCalendar.Min.Value;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type, which is the last moment of December 31, 9999 C.E. and is equivalent to <see cref="F:System.DateTime.MaxValue" />.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00063F70 File Offset: 0x00062170
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				DateTime? max = GregorianCalendar.Max;
				if (max == null)
				{
					GregorianCalendar.Max = new DateTime?(new DateTime(9999, 12, 31, 11, 59, 59));
				}
				return GregorianCalendar.Max.Value;
			}
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000A18 RID: 2584
		public const int ADEra = 1;

		// Token: 0x04000A19 RID: 2585
		[NonSerialized]
		internal GregorianCalendarTypes m_type;

		// Token: 0x04000A1A RID: 2586
		private static DateTime? Min;

		// Token: 0x04000A1B RID: 2587
		private static DateTime? Max;
	}
}
