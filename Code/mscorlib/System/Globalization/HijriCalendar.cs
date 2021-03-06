using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Hijri calendar.</summary>
	// Token: 0x02000219 RID: 537
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class HijriCalendar : Calendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.HijriCalendar" /> class.</summary>
		// Token: 0x06001B08 RID: 6920 RVA: 0x000645E4 File Offset: 0x000627E4
		public HijriCalendar()
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

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.HijriCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.HijriCalendar" />.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0006469C File Offset: 0x0006289C
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					HijriCalendar.HijriEra
				};
			}
		}

		/// <summary>Gets or sets the number of days to add or subtract from the calendar to accommodate the variances in the start and the end of Ramadan and to accommodate the date difference between countries/regions.</summary>
		/// <returns>An integer from -2 to 2 that represents the number of days to add or subtract from the calendar.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to an invalid value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000646AC File Offset: 0x000628AC
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x000646B4 File Offset: 0x000628B4
		[MonoTODO("Not supported")]
		public int HijriAdjustment
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 100 or greater than 9666.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000646BC File Offset: 0x000628BC
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x000646C4 File Offset: 0x000628C4
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000646F4 File Offset: 0x000628F4
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x000646FC File Offset: 0x000628FC
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

		// Token: 0x06001B11 RID: 6929 RVA: 0x00064738 File Offset: 0x00062938
		internal void M_CheckFixedHijri(string param, int rdHijri)
		{
			if (rdHijri < HijriCalendar.M_MinFixed || rdHijri > HijriCalendar.M_MaxFixed - this.AddHijriDate)
			{
				StringWriter stringWriter = new StringWriter();
				int num;
				int num2;
				int num3;
				CCHijriCalendar.dmy_from_fixed(out num, out num2, out num3, HijriCalendar.M_MaxFixed - this.AddHijriDate);
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

		// Token: 0x06001B12 RID: 6930 RVA: 0x000647F4 File Offset: 0x000629F4
		internal void M_CheckDateTime(DateTime time)
		{
			int rdHijri = CCFixed.FromDateTime(time) - this.AddHijriDate;
			this.M_CheckFixedHijri("time", rdHijri);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0006481C File Offset: 0x00062A1C
		internal int M_FromDateTime(DateTime time)
		{
			return CCFixed.FromDateTime(time) - this.AddHijriDate;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0006482C File Offset: 0x00062A2C
		internal DateTime M_ToDateTime(int rd)
		{
			return CCFixed.ToDateTime(rd + this.AddHijriDate);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0006483C File Offset: 0x00062A3C
		internal DateTime M_ToDateTime(int date, int hour, int minute, int second, int milliseconds)
		{
			return CCFixed.ToDateTime(date + this.AddHijriDate, hour, minute, second, (double)milliseconds);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00064854 File Offset: 0x00062A54
		internal void M_CheckEra(ref int era)
		{
			if (era == 0)
			{
				era = HijriCalendar.HijriEra;
			}
			if (era != HijriCalendar.HijriEra)
			{
				throw new ArgumentException("Era value was not valid.");
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0006487C File Offset: 0x00062A7C
		internal override void M_CheckYE(int year, ref int era)
		{
			this.M_CheckEra(ref era);
			base.M_ArgumentInRange("year", year, 1, 9666);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000648A4 File Offset: 0x00062AA4
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

		// Token: 0x06001B19 RID: 6937 RVA: 0x000648F8 File Offset: 0x00062AF8
		internal void M_CheckYMDE(int year, int month, int day, ref int era)
		{
			this.M_CheckYME(year, month, ref era);
			base.M_ArgumentInRange("day", day, 1, this.GetDaysInMonth(year, month, HijriCalendar.HijriEra));
			if (year == 9666)
			{
				int rdHijri = CCHijriCalendar.fixed_from_dmy(day, month, year);
				this.M_CheckFixedHijri("day", rdHijri);
			}
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add months to. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B1A RID: 6938 RVA: 0x0006494C File Offset: 0x00062B4C
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

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add years to. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B1B RID: 6939 RVA: 0x000649AC File Offset: 0x00062BAC
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

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 30 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B1C RID: 6940 RVA: 0x00064A00 File Offset: 0x00062C00
		public override int GetDayOfMonth(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.day_from_fixed(num);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B1D RID: 6941 RVA: 0x00064A28 File Offset: 0x00062C28
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCFixed.day_of_week(num);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 355 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B1E RID: 6942 RVA: 0x00064A50 File Offset: 0x00062C50
		public override int GetDayOfYear(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			int year = CCHijriCalendar.year_from_fixed(num);
			int num2 = CCHijriCalendar.fixed_from_dmy(1, 1, year);
			return num - num2 + 1;
		}

		/// <summary>Returns the number of days in the specified month of the specified year and era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar. -or- <paramref name="year" /> is outside the range supported by this calendar.-or- <paramref name="month" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B1F RID: 6943 RVA: 0x00064A88 File Offset: 0x00062C88
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.M_CheckYME(year, month, ref era);
			int num = CCHijriCalendar.fixed_from_dmy(1, month, year);
			int num2 = CCHijriCalendar.fixed_from_dmy(1, month + 1, year);
			return num2 - num;
		}

		/// <summary>Returns the number of days in the specified year and era.</summary>
		/// <returns>The number of days in the specified year and era. The number of days is 354 in a common year or 355 in a leap year.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B20 RID: 6944 RVA: 0x00064AB8 File Offset: 0x00062CB8
		public override int GetDaysInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			int num = CCHijriCalendar.fixed_from_dmy(1, 1, year);
			int num2 = CCHijriCalendar.fixed_from_dmy(1, 1, year + 1);
			return num2 - num;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B21 RID: 6945 RVA: 0x00064AE8 File Offset: 0x00062CE8
		public override int GetEra(DateTime time)
		{
			this.M_CheckDateTime(time);
			return HijriCalendar.HijriEra;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>Always 0 because the <see cref="T:System.Globalization.HijriCalendar" /> type does not support the notion of a leap month.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify <see cref="F:System.Globalization.Calendar.CurrentEra" /> or <see cref="F:System.Globalization.HijriCalendar.HijriEra" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the Hijri calendar year 1 or greater than the year 9666.-or-<paramref name="era" /> is not <see cref="F:System.Globalization.Calendar.CurrentEra" /> or <see cref="F:System.Globalization.HijriCalendar.HijriEra" />.</exception>
		// Token: 0x06001B22 RID: 6946 RVA: 0x00064AF8 File Offset: 0x00062CF8
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return 0;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B23 RID: 6947 RVA: 0x00064AFC File Offset: 0x00062CFC
		public override int GetMonth(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.month_from_fixed(num);
		}

		/// <summary>Returns the number of months in the specified year and era.</summary>
		/// <returns>The number of months in the specified year and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar. -or- <paramref name="year" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B24 RID: 6948 RVA: 0x00064B24 File Offset: 0x00062D24
		public override int GetMonthsInYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B25 RID: 6949 RVA: 0x00064B34 File Offset: 0x00062D34
		public override int GetYear(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.year_from_fixed(num);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 30 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar. -or- <paramref name="year" /> is outside the range supported by this calendar.-or- <paramref name="month" /> is outside the range supported by this calendar.-or- <paramref name="day" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B26 RID: 6950 RVA: 0x00064B5C File Offset: 0x00062D5C
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			return this.IsLeapYear(year) && month == 12 && day == 30;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <returns>This method always returns false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar. -or- <paramref name="year" /> is outside the range supported by this calendar.-or- <paramref name="month" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B27 RID: 6951 RVA: 0x00064B88 File Offset: 0x00062D88
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
		///   <paramref name="era" /> is outside the range supported by this calendar. -or- <paramref name="year" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B28 RID: 6952 RVA: 0x00064B98 File Offset: 0x00062D98
		public override bool IsLeapYear(int year, int era)
		{
			this.M_CheckYE(year, ref era);
			return CCHijriCalendar.is_leap_year(year);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 30 that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar. -or- <paramref name="year" /> is outside the range supported by this calendar.-or- <paramref name="month" /> is outside the range supported by this calendar.-or- <paramref name="day" /> is outside the range supported by this calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06001B29 RID: 6953 RVA: 0x00064BAC File Offset: 0x00062DAC
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.M_CheckYMDE(year, month, day, ref era);
			base.M_CheckHMSM(hour, minute, second, millisecond);
			int date = CCHijriCalendar.fixed_from_dmy(day, month, year);
			return this.M_ToDateTime(date, hour, minute, second, millisecond);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.HijriCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06001B2A RID: 6954 RVA: 0x00064BEC File Offset: 0x00062DEC
		public override int ToFourDigitYear(int year)
		{
			return base.ToFourDigitYear(year);
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns the <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" /> value.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00064BF8 File Offset: 0x00062DF8
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		/// <summary>Gets the earliest date and time supported by this calendar.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.HijriCalendar" /> type, which is equivalent to the first moment of July 18, 622 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x00064BFC File Offset: 0x00062DFC
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HijriCalendar.Min;
			}
		}

		/// <summary>Gets the latest date and time supported by this calendar.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.HijriCalendar" /> type, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00064C04 File Offset: 0x00062E04
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HijriCalendar.Max;
			}
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04000A29 RID: 2601
		public static readonly int HijriEra = 1;

		// Token: 0x04000A2A RID: 2602
		internal static readonly int M_MinFixed = CCHijriCalendar.fixed_from_dmy(1, 1, 1);

		// Token: 0x04000A2B RID: 2603
		internal static readonly int M_MaxFixed = CCGregorianCalendar.fixed_from_dmy(31, 12, 9999);

		// Token: 0x04000A2C RID: 2604
		internal int M_AddHijriDate;

		// Token: 0x04000A2D RID: 2605
		private static DateTime Min = new DateTime(622, 7, 18, 0, 0, 0);

		// Token: 0x04000A2E RID: 2606
		private static DateTime Max = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
