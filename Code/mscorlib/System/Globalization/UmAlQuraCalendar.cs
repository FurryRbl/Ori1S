using System;
using System.IO;

namespace System.Globalization
{
	/// <summary>Represents the Saudi Hijri (Um Al Qura) calendar.</summary>
	// Token: 0x0200022D RID: 557
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class UmAlQuraCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </summary>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000699C0 File Offset: 0x00067BC0
		public UmAlQuraCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"A.H."
			};
			this.M_EraNames = new string[]
			{
				"Anno Hegirae"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 1451;
			}
		}

		/// <summary>Gets a list of the eras that are supported by the current <see cref="T:System.Globalization.UmAlQuraCalendar" />.</summary>
		/// <returns>An array that consists of a single element having a value that is <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x00069A70 File Offset: 0x00067C70
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
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the Um Al Qura calendar year value is less than 1318 but not 99, or is greater than 1450.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00069A7C File Offset: 0x00067C7C
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x00069A84 File Offset: 0x00067C84
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00069AB4 File Offset: 0x00067CB4
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x00069ABC File Offset: 0x00067CBC
		internal virtual int AddHijriDate
		{
			get
			{
				return this.M_AddHijriDate;
			}
			set
			{
				base.CheckReadOnly();
				if (value < -3 && value > 3)
				{
					throw new ArgumentOutOfRangeException("AddHijriDate", "Value should be between -3 and 3.");
				}
				this.M_AddHijriDate = value;
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00069AF8 File Offset: 0x00067CF8
		internal void M_CheckFixedHijri(string param, int rdHijri)
		{
			if (rdHijri < UmAlQuraCalendar.M_MinFixed || rdHijri > UmAlQuraCalendar.M_MaxFixed - this.AddHijriDate)
			{
				StringWriter stringWriter = new StringWriter();
				int num;
				int num2;
				int num3;
				CCHijriCalendar.dmy_from_fixed(out num, out num2, out num3, UmAlQuraCalendar.M_MaxFixed - this.AddHijriDate);
				if (this.AddHijriDate != 0)
				{
					stringWriter.Write("This HijriCalendar (AddHijriDate {0}) allows dates from 1. 1. 1 to {1}. {2}. {3}.", new object[]
					{
						this.AddHijriDate,
						num,
						num2,
						num3
					});
				}
				else
				{
					stringWriter.Write("HijriCalendar allows dates from 1.1.1 to {0}.{1}.{2}.", num, num2, num3);
				}
				throw new ArgumentOutOfRangeException(param, stringWriter.ToString());
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00069BB4 File Offset: 0x00067DB4
		internal void M_CheckDateTime(DateTime time)
		{
			int rdHijri = CCFixed.FromDateTime(time) - this.AddHijriDate;
			this.M_CheckFixedHijri("time", rdHijri);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00069BDC File Offset: 0x00067DDC
		internal int M_FromDateTime(DateTime time)
		{
			return CCFixed.FromDateTime(time) - this.AddHijriDate;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00069BEC File Offset: 0x00067DEC
		internal DateTime M_ToDateTime(int rd)
		{
			return CCFixed.ToDateTime(rd + this.AddHijriDate);
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00069BFC File Offset: 0x00067DFC
		internal DateTime M_ToDateTime(int date, int hour, int minute, int second, int milliseconds)
		{
			return CCFixed.ToDateTime(date + this.AddHijriDate, hour, minute, second, (double)milliseconds);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00069C14 File Offset: 0x00067E14
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

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00069C34 File Offset: 0x00067E34
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			base.M_ArgumentInRange("year", year, 1, 9666);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00069C5C File Offset: 0x00067E5C
		internal void M_CheckYME(int year, int month, ref int era)
		{
			this.M_CheckYE(year, ref era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			if (year == 9666)
			{
				int rdHijri = CCHijriCalendar.fixed_from_dmy(1, month, year);
				this.M_CheckFixedHijri("month", rdHijri);
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00069CB0 File Offset: 0x00067EB0
		internal void M_CheckYMDE(int year, int month, int day, ref int era)
		{
			this.M_CheckYME(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, 1));
			if (year == 9666)
			{
				int rdHijri = CCHijriCalendar.fixed_from_dmy(day, month, year);
				this.M_CheckFixedHijri("day", rdHijri);
			}
		}

		/// <summary>Calculates a date that is a specified number of months away from a specified initial date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the date yielded by adding the number of months specified by the <paramref name="months" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <param name="months">The positive or negative number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting date is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000. -or-<paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06001CB4 RID: 7348 RVA: 0x00069D00 File Offset: 0x00067F00
		public override DateTime AddMonths(DateTime time, int months)
		{
			int num = this.M_FromDateTime(time);
			int day;
			int num2;
			int num3;
			CCHijriCalendar.dmy_from_fixed(out day, out num2, out num3, num);
			num2 += months;
			num3 += CCMath.div_mod(out num2, num2, 12);
			num = CCHijriCalendar.fixed_from_dmy(day, num2, num3);
			this.M_CheckFixedHijri("time", num);
			return this.M_ToDateTime(num).Add(time.TimeOfDay);
		}

		/// <summary>Calculates a date that is a specified number of years away from a specified initial date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the date yielded by adding the number of years specified by the <paramref name="years" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <param name="years">The positive or negative number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting date is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is less than -10,000 or greater than 10,000. -or-<paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06001CB5 RID: 7349 RVA: 0x00069D60 File Offset: 0x00067F60
		public override DateTime AddYears(DateTime time, int years)
		{
			int num = this.M_FromDateTime(time);
			int day;
			int month;
			int num2;
			CCHijriCalendar.dmy_from_fixed(out day, out month, out num2, num);
			num2 += years;
			num = CCHijriCalendar.fixed_from_dmy(day, month, num2);
			this.M_CheckFixedHijri("time", num);
			return this.M_ToDateTime(num).Add(time.TimeOfDay);
		}

		/// <summary>Calculates on which day of the month a specified date occurs.</summary>
		/// <returns>An integer from 1 through 30 that represents the day of the month specified by the <paramref name="time" /> parameter. </returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CB6 RID: 7350 RVA: 0x00069DB4 File Offset: 0x00067FB4
		public override int GetDayOfMonth(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.day_from_fixed(num);
		}

		/// <summary>Calculates on which day of the week a specified date occurs.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CB7 RID: 7351 RVA: 0x00069DDC File Offset: 0x00067FDC
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCFixed.day_of_week(num);
		}

		/// <summary>Calculates on which day of the year a specified date occurs.</summary>
		/// <returns>An integer from 1 through 355 that represents the day of the year specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CB8 RID: 7352 RVA: 0x00069E04 File Offset: 0x00068004
		public override int GetDayOfYear(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			int year = CCHijriCalendar.year_from_fixed(num);
			int num2 = CCHijriCalendar.fixed_from_dmy(1, 1, year);
			return num - num2 + 1;
		}

		/// <summary>Calculates the number of days in the specified month of the specified year and era.</summary>
		/// <returns>The number of days in the specified month in the specified year and era. The return value is 29 in a common year and 30 in a leap year.</returns>
		/// <param name="year">A year. </param>
		/// <param name="month">An integer from 1 through 12 that represents a month. </param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		// Token: 0x06001CB9 RID: 7353 RVA: 0x00069E3C File Offset: 0x0006803C
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			int num = CCHijriCalendar.fixed_from_dmy(1, month, year);
			int num2 = CCHijriCalendar.fixed_from_dmy(1, month + 1, year);
			return num2 - num;
		}

		/// <summary>Calculates the number of days in the specified year of the specified era.</summary>
		/// <returns>The number of days in the specified year and era. The number of days is 354 in a common year or 355 in a leap year.</returns>
		/// <param name="year">A year. </param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		// Token: 0x06001CBA RID: 7354 RVA: 0x00069E6C File Offset: 0x0006806C
		public override int GetDaysInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			int num = CCHijriCalendar.fixed_from_dmy(1, 1, year);
			int num2 = CCHijriCalendar.fixed_from_dmy(1, 1, year + 1);
			return num2 - num;
		}

		/// <summary>Calculates in which era a specified date occurs.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" /> value.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CBB RID: 7355 RVA: 0x00069E9C File Offset: 0x0006809C
		public override int GetEra(DateTime time)
		{
			this.M_CheckDateTime(time);
			return 1;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>Always 0 because the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class does not support leap months.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1318 or greater than 1450.-or-<paramref name="era" /> is not UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</exception>
		// Token: 0x06001CBC RID: 7356 RVA: 0x00069EA8 File Offset: 0x000680A8
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Calculates the month in which a specified date occurs.</summary>
		/// <returns>An integer from 1 through 12 that represents the month in the date specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00069EAC File Offset: 0x000680AC
		public override int GetMonth(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.month_from_fixed(num);
		}

		/// <summary>Calculates the number of months in the specified year of the specified era.</summary>
		/// <returns>The return value is always 12.</returns>
		/// <param name="year">A year. </param>
		/// <param name="era">An era. Specify UmAlQuaraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CBE RID: 7358 RVA: 0x00069ED4 File Offset: 0x000680D4
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return 12;
		}

		/// <summary>Calculates the year of a date represented by a specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 05/13/2029 23:59:59 (Gregorian date).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CBF RID: 7359 RVA: 0x00069EE4 File Offset: 0x000680E4
		public override int GetYear(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.year_from_fixed(num);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false. The return value is always false because the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class does not support the notion of a leap day.</returns>
		/// <param name="year">A year. </param>
		/// <param name="month">An integer from 1 through 12 that represents a month. </param>
		/// <param name="day">An integer from 1 through 30 that represents a day. </param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x00069F0C File Offset: 0x0006810C
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			return this.IsLeapYear(year) && month == 12 && day == 30;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <returns>Always false because the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class does not support leap months.</returns>
		/// <param name="year">A year. </param>
		/// <param name="month">An integer from 1 through 12 that represents a month. </param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		// Token: 0x06001CC1 RID: 7361 RVA: 0x00069F38 File Offset: 0x00068138
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">A year. </param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class. </exception>
		// Token: 0x06001CC2 RID: 7362 RVA: 0x00069F48 File Offset: 0x00068148
		public override bool IsLeapYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCHijriCalendar.is_leap_year(year);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">A year. </param>
		/// <param name="month">An integer from 1 through 12 that represents a month. </param>
		/// <param name="day">An integer from 1 through 29 that represents a day. </param>
		/// <param name="hour">An integer from 0 through 23 that represents an hour. </param>
		/// <param name="minute">An integer from 0 through 59 that represents a minute. </param>
		/// <param name="second">An integer from 0 through 59 that represents a second. </param>
		/// <param name="millisecond">An integer from 0 through 999 that represents a millisecond. </param>
		/// <param name="era">An era. Specify UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. </exception>
		// Token: 0x06001CC3 RID: 7363 RVA: 0x00069F5C File Offset: 0x0006815C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			int date = CCHijriCalendar.fixed_from_dmy(day, month, year);
			return this.M_ToDateTime(date, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.UmAlQuraCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>If the <paramref name="year" /> parameter is a 2-digit year, the return value is the corresponding 4-digit year. If the <paramref name="year" /> parameter is a 4-digit year, the return value is the unchanged <paramref name="year" /> parameter.</returns>
		/// <param name="year">A 2-digit year from 0 through 99, or a 4-digit Um Al Qura calendar year from 1318 through 1450.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001CC4 RID: 7364 RVA: 0x00069F9C File Offset: 0x0006819C
		public override int ToFourDigitYear(int year)
		{
			return base.ToFourDigitYear(year);
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" /> value.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00069FA8 File Offset: 0x000681A8
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by this calendar.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class, which is equivalent to the first moment of April 30, 1900 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00069FAC File Offset: 0x000681AC
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return UmAlQuraCalendar.Min;
			}
		}

		/// <summary>Gets the latest date and time supported by this calendar.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class, which is equivalent to the last moment of May 13, 2029 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00069FB4 File Offset: 0x000681B4
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return UmAlQuraCalendar.Max;
			}
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000AB6 RID: 2742
		public const int UmAlQuraEra = 1;

		// Token: 0x04000AB7 RID: 2743
		internal static readonly int M_MinFixed = CCHijriCalendar.fixed_from_dmy(1, 1, 1);

		// Token: 0x04000AB8 RID: 2744
		internal static readonly int M_MaxFixed = CCGregorianCalendar.fixed_from_dmy(31, 12, 9999);

		// Token: 0x04000AB9 RID: 2745
		internal int M_AddHijriDate;

		// Token: 0x04000ABA RID: 2746
		private static DateTime Min = new DateTime(622, 7, 18, 0, 0, 0);

		// Token: 0x04000ABB RID: 2747
		private static DateTime Max = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
