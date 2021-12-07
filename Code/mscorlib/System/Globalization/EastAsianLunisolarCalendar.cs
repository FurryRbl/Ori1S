using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents a calendar that divides time into months, days, years, and eras, and has dates that are based on cycles of the sun and the moon.</summary>
	// Token: 0x02000215 RID: 533
	[ComVisible(true)]
	[Serializable]
	public abstract class EastAsianLunisolarCalendar : Calendar
	{
		// Token: 0x06001AA9 RID: 6825 RVA: 0x00063910 File Offset: 0x00061B10
		internal EastAsianLunisolarCalendar(CCEastAsianLunisolarEraHandler eraHandler)
		{
			this.M_EraHandler = eraHandler;
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.EastAsianLunisolarCalendar" />  is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 99 or greater than the maximum supported year in the current calendar.</exception>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00063920 File Offset: 0x00061B20
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x00063928 File Offset: 0x00061B28
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

		// Token: 0x06001AAC RID: 6828 RVA: 0x00063958 File Offset: 0x00061B58
		internal void M_CheckDateTime(DateTime time)
		{
			this.M_EraHandler.CheckDateTime(time);
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00063968 File Offset: 0x00061B68
		internal virtual int ActualCurrentEra
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0006396C File Offset: 0x00061B6C
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = this.ActualCurrentEra;
			}
			if (!this.M_EraHandler.ValidEra(era))
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000639A8 File Offset: 0x00061BA8
		internal int M_CheckYEG(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			return this.M_EraHandler.GregorianYear(year, era);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000639C0 File Offset: 0x00061BC0
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckYEG(year, ref era);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000639CC File Offset: 0x00061BCC
		internal int M_CheckYMEG(int year, int month, ref int era)
		{
			int result = this.M_CheckYEG(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			return result;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00063A04 File Offset: 0x00061C04
		internal int M_CheckYMDEG(int year, int month, int day, ref int era)
		{
			int result = this.M_CheckYMEG(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, era));
			return result;
		}

		/// <summary>Calculates the date that is the specified number of months away from the specified date.</summary>
		/// <returns>A new <see cref="T:System.DateTime" /> that results from adding the specified number of months to the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The result is outside the supported range of a <see cref="T:System.DateTime" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000 or greater than 120000. -or-<paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06001AB3 RID: 6835 RVA: 0x00063A38 File Offset: 0x00061C38
		[MonoTODO]
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime dateTime = CCEastAsianLunisolarCalendar.AddMonths(time, months);
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Calculates the date that is the specified number of years away from the specified date.</summary>
		/// <returns>A new <see cref="T:System.DateTime" /> that results from adding the specified number of years to the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The result is outside the supported range of a <see cref="T:System.DateTime" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06001AB4 RID: 6836 RVA: 0x00063A58 File Offset: 0x00061C58
		[MonoTODO]
		public override DateTime AddYears(DateTime time, int years)
		{
			DateTime dateTime = CCEastAsianLunisolarCalendar.AddYears(time, years);
			this.M_CheckDateTime(dateTime);
			return dateTime;
		}

		/// <summary>Calculates the day of the month in the specified date.</summary>
		/// <returns>An integer from 1 through 31 that represents the day of the month specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AB5 RID: 6837 RVA: 0x00063A78 File Offset: 0x00061C78
		[MonoTODO]
		public override int GetDayOfMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCEastAsianLunisolarCalendar.GetDayOfMonth(time);
		}

		/// <summary>Calculates the day of the week in the specified date.</summary>
		/// <returns>One of the <see cref="T:System.DayOfWeek" /> values that represents the day of the week specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06001AB6 RID: 6838 RVA: 0x00063A88 File Offset: 0x00061C88
		[MonoTODO]
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.M_CheckDateTime(time);
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		/// <summary>Calculates the day of the year in the specified date.</summary>
		/// <returns>An integer from 1 through 354 in a common year, or 1 through 384 in a leap year, that represents the day of the year specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001AB7 RID: 6839 RVA: 0x00063AAC File Offset: 0x00061CAC
		[MonoTODO]
		public override int GetDayOfYear(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCEastAsianLunisolarCalendar.GetDayOfYear(time);
		}

		/// <summary>Calculates the number of days in the specified month of the specified year and era.</summary>
		/// <returns>The number of days in the specified month of the specified year and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 12 in a common year, or 1 through 13 in a leap year, that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001AB8 RID: 6840 RVA: 0x00063ABC File Offset: 0x00061CBC
		[MonoTODO]
		public override int GetDaysInMonth(int year, int month, int era)
		{
			int gyear = this.M_CheckYMEG(year, month, ref era);
			return CCEastAsianLunisolarCalendar.GetDaysInMonth(gyear, month);
		}

		/// <summary>Calculates the number of days in the specified year and era.</summary>
		/// <returns>The number of days in the specified year and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001AB9 RID: 6841 RVA: 0x00063ADC File Offset: 0x00061CDC
		[MonoTODO]
		public override int GetDaysInYear(int year, int era)
		{
			int year2 = this.M_CheckYEG(year, ref era);
			return CCEastAsianLunisolarCalendar.GetDaysInYear(year2);
		}

		/// <summary>Calculates the leap month for the specified year and era.</summary>
		/// <returns>A positive integer from 1 through 13 that indicates the leap month in the specified year and era. -or-Zero if this calendar does not support a leap month, or if the <paramref name="year" /> and <paramref name="era" /> parameters do not specify a leap year.</returns>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		// Token: 0x06001ABA RID: 6842 RVA: 0x00063AFC File Offset: 0x00061CFC
		[MonoTODO]
		public override int GetLeapMonth(int year, int era)
		{
			return base.GetLeapMonth(year, era);
		}

		/// <summary>Returns the month in the specified date.</summary>
		/// <returns>An integer from 1 to 13 that represents the month specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001ABB RID: 6843 RVA: 0x00063B08 File Offset: 0x00061D08
		[MonoTODO]
		public override int GetMonth(DateTime time)
		{
			this.M_CheckDateTime(time);
			return CCEastAsianLunisolarCalendar.GetMonth(time);
		}

		/// <summary>Calculates the number of months in the specified year and era.</summary>
		/// <returns>The number of months in the specified year in the specified era. The return value is 12 months in a common year or 13 months in a leap year.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001ABC RID: 6844 RVA: 0x00063B18 File Offset: 0x00061D18
		[MonoTODO]
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return (!this.IsLeapYear(year, era)) ? 12 : 13;
		}

		/// <summary>Returns the year in the specified date.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001ABD RID: 6845 RVA: 0x00063B3C File Offset: 0x00061D3C
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			return this.M_EraHandler.EraYear(out num, date);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 13 that represents the month. </param>
		/// <param name="day">An integer from 1 through 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001ABE RID: 6846 RVA: 0x00063B60 File Offset: 0x00061D60
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int gyear = this.M_CheckYMDEG(year, month, day, ref era);
			return CCEastAsianLunisolarCalendar.IsLeapMonth(gyear, month);
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <returns>true if the <paramref name="month" /> parameter is a leap month; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 13 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001ABF RID: 6847 RVA: 0x00063B80 File Offset: 0x00061D80
		[MonoTODO]
		public override bool IsLeapMonth(int year, int month, int era)
		{
			int gyear = this.M_CheckYMEG(year, month, ref era);
			return CCEastAsianLunisolarCalendar.IsLeapMonth(gyear, month);
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001AC0 RID: 6848 RVA: 0x00063BA0 File Offset: 0x00061DA0
		public override bool IsLeapYear(int year, int era)
		{
			int gyear = this.M_CheckYEG(year, ref era);
			return CCEastAsianLunisolarCalendar.IsLeapYear(gyear);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 13 that represents the month. </param>
		/// <param name="day">An integer from 1 through 31 that represents the day. </param>
		/// <param name="hour">An integer from 0 through 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 through 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 through 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 through 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001AC1 RID: 6849 RVA: 0x00063BC0 File Offset: 0x00061DC0
		[MonoTODO]
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int year2 = this.M_CheckYMDEG(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			return CCGregorianCalendar.ToDateTime(year2, month, day, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year.</summary>
		/// <returns>An integer that contains the four-digit representation of the <paramref name="year" /> parameter.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001AC2 RID: 6850 RVA: 0x00063BF8 File Offset: 0x00061DF8
		[MonoTODO]
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

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>This property always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" /> value.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00063C28 File Offset: 0x00061E28
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		/// <summary>Calculates the celestial stem of the specified year in the sexagenary (60-year) cycle.</summary>
		/// <returns>A number from 1 through 10.</returns>
		/// <param name="sexagenaryYear">An integer from 1 through 60 that represents a year in the sexagenary cycle. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sexagenaryYear" /> is less than 1 or greater than 60.</exception>
		// Token: 0x06001AC4 RID: 6852 RVA: 0x00063C2C File Offset: 0x00061E2C
		public int GetCelestialStem(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || 60 < sexagenaryYear)
			{
				throw new ArgumentOutOfRangeException("sexagendaryYear is less than 0 or greater than 60");
			}
			return (sexagenaryYear - 1) % 10 + 1;
		}

		/// <summary>Calculates the year in the sexagenary (60-year) cycle that corresponds to the specified date.</summary>
		/// <returns>A number from 1 through 60 in the sexagenary cycle that corresponds to the <paramref name="date" /> parameter.</returns>
		/// <param name="time">A <see cref="T:System.DateTime" /> to read.</param>
		// Token: 0x06001AC5 RID: 6853 RVA: 0x00063C5C File Offset: 0x00061E5C
		public virtual int GetSexagenaryYear(DateTime time)
		{
			return (this.GetYear(time) - 1900) % 60;
		}

		/// <summary>Calculates the terrestrial branch of the specified year in the sexagenary (60-year) cycle.</summary>
		/// <returns>An integer from 1 through 12.</returns>
		/// <param name="sexagenaryYear">An integer from 1 through 60 that represents a year in the sexagenary cycle.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sexagenaryYear" /> is less than 1 or greater than 60.</exception>
		// Token: 0x06001AC6 RID: 6854 RVA: 0x00063C70 File Offset: 0x00061E70
		public int GetTerrestrialBranch(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || 60 < sexagenaryYear)
			{
				throw new ArgumentOutOfRangeException("sexagendaryYear is less than 0 or greater than 60");
			}
			return (sexagenaryYear - 1) % 12 + 1;
		}

		// Token: 0x04000A17 RID: 2583
		internal readonly CCEastAsianLunisolarEraHandler M_EraHandler;
	}
}
