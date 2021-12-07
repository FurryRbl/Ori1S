using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Defines how <see cref="T:System.DateTime" /> values are formatted and displayed, depending on the culture.</summary>
	// Token: 0x02000211 RID: 529
	[ComVisible(true)]
	[Serializable]
	public sealed class DateTimeFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x000626A0 File Offset: 0x000608A0
		internal DateTimeFormatInfo(bool read_only)
		{
			this.m_isReadOnly = read_only;
			this.amDesignator = "AM";
			this.pmDesignator = "PM";
			this.dateSeparator = "/";
			this.timeSeparator = ":";
			this.shortDatePattern = "MM/dd/yyyy";
			this.longDatePattern = "dddd, dd MMMM yyyy";
			this.shortTimePattern = "HH:mm";
			this.longTimePattern = "HH:mm:ss";
			this.monthDayPattern = "MMMM dd";
			this.yearMonthPattern = "yyyy MMMM";
			this.fullDateTimePattern = "dddd, dd MMMM yyyy HH:mm:ss";
			this._RFC1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			this._SortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			this._UniversalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			this.firstDayOfWeek = 0;
			this.calendar = new GregorianCalendar();
			this.calendarWeekRule = 0;
			this.abbreviatedDayNames = DateTimeFormatInfo.INVARIANT_ABBREVIATED_DAY_NAMES;
			this.dayNames = DateTimeFormatInfo.INVARIANT_DAY_NAMES;
			this.abbreviatedMonthNames = DateTimeFormatInfo.INVARIANT_ABBREVIATED_MONTH_NAMES;
			this.monthNames = DateTimeFormatInfo.INVARIANT_MONTH_NAMES;
			this.m_genitiveAbbreviatedMonthNames = DateTimeFormatInfo.INVARIANT_ABBREVIATED_MONTH_NAMES;
			this.genitiveMonthNames = DateTimeFormatInfo.INVARIANT_MONTH_NAMES;
			this.shortDayNames = DateTimeFormatInfo.INVARIANT_SHORT_DAY_NAMES;
		}

		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x06001A55 RID: 6741 RVA: 0x000627BC File Offset: 0x000609BC
		public DateTimeFormatInfo() : this(false)
		{
		}

		/// <summary>Returns the <see cref="T:System.Globalization.DateTimeFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</returns>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> that gets the <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or- null to get <see cref="P:System.Globalization.DateTimeFormatInfo.CurrentInfo" />. </param>
		// Token: 0x06001A57 RID: 6743 RVA: 0x000629AC File Offset: 0x00060BAC
		public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
		{
			if (provider != null)
			{
				DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)provider.GetFormat(typeof(DateTimeFormatInfo));
				if (dateTimeFormatInfo != null)
				{
					return dateTimeFormatInfo;
				}
			}
			return DateTimeFormatInfo.CurrentInfo;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000629E4 File Offset: 0x00060BE4
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper around <paramref name="dtfi" />.</returns>
		/// <param name="dtfi">The <see cref="T:System.Globalization.DateTimeFormatInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dtfi" /> is null. </exception>
		// Token: 0x06001A59 RID: 6745 RVA: 0x000629EC File Offset: 0x00060BEC
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.Clone();
			dateTimeFormatInfo.m_isReadOnly = true;
			return dateTimeFormatInfo;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.DateTimeFormatInfo" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.DateTimeFormatInfo" /> copied from the original <see cref="T:System.Globalization.DateTimeFormatInfo" />.</returns>
		// Token: 0x06001A5A RID: 6746 RVA: 0x00062A10 File Offset: 0x00060C10
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.m_isReadOnly = false;
			return dateTimeFormatInfo;
		}

		/// <summary>Returns an object of the specified type that provides a <see cref="T:System.DateTime" /> formatting service.</summary>
		/// <returns>The current <see cref="T:System.Globalization.DateTimeFormatInfo" />, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.DateTimeFormatInfo" />; otherwise, null.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service. </param>
		// Token: 0x06001A5B RID: 6747 RVA: 0x00062A34 File Offset: 0x00060C34
		public object GetFormat(Type formatType)
		{
			return (formatType != base.GetType()) ? null : this;
		}

		/// <summary>Returns the string containing the abbreviated name of the specified era, if an abbreviation exists.</summary>
		/// <returns>A string containing the abbreviated name of the specified era, if an abbreviation exists.-or- A string containing the full name of the era, if an abbreviation does not exist.</returns>
		/// <param name="era">The integer representing the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
		// Token: 0x06001A5C RID: 6748 RVA: 0x00062A4C File Offset: 0x00060C4C
		public string GetAbbreviatedEraName(int era)
		{
			if (era < 0 || era >= this.calendar.AbbreviatedEraNames.Length)
			{
				throw new ArgumentOutOfRangeException("era", era.ToString());
			}
			return this.calendar.AbbreviatedEraNames[era];
		}

		/// <summary>Returns the culture-specific abbreviated name of the specified month based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific abbreviated name of the month represented by <paramref name="month" />.</returns>
		/// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 13. </exception>
		// Token: 0x06001A5D RID: 6749 RVA: 0x00062A94 File Offset: 0x00060C94
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.abbreviatedMonthNames[month - 1];
		}

		/// <summary>Returns the integer representing the specified era.</summary>
		/// <returns>The integer representing the era, if <paramref name="eraName" /> is valid; otherwise, -1.</returns>
		/// <param name="eraName">The string containing the name of the era. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eraName" /> is null. </exception>
		// Token: 0x06001A5E RID: 6750 RVA: 0x00062AB8 File Offset: 0x00060CB8
		public int GetEra(string eraName)
		{
			if (eraName == null)
			{
				throw new ArgumentNullException();
			}
			string[] array = this.calendar.EraNames;
			for (int i = 0; i < array.Length; i++)
			{
				if (CultureInfo.InvariantCulture.CompareInfo.Compare(eraName, array[i], CompareOptions.IgnoreCase) == 0)
				{
					return this.calendar.Eras[i];
				}
			}
			array = this.calendar.AbbreviatedEraNames;
			for (int j = 0; j < array.Length; j++)
			{
				if (CultureInfo.InvariantCulture.CompareInfo.Compare(eraName, array[j], CompareOptions.IgnoreCase) == 0)
				{
					return this.calendar.Eras[j];
				}
			}
			return -1;
		}

		/// <summary>Returns the string containing the name of the specified era.</summary>
		/// <returns>A string containing the name of the era.</returns>
		/// <param name="era">The integer representing the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
		// Token: 0x06001A5F RID: 6751 RVA: 0x00062B60 File Offset: 0x00060D60
		public string GetEraName(int era)
		{
			if (era < 0 || era > this.calendar.EraNames.Length)
			{
				throw new ArgumentOutOfRangeException("era", era.ToString());
			}
			return this.calendar.EraNames[era - 1];
		}

		/// <summary>Returns the culture-specific full name of the specified month based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific full name of the month represented by <paramref name="month" />.</returns>
		/// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 13. </exception>
		// Token: 0x06001A60 RID: 6752 RVA: 0x00062BA8 File Offset: 0x00060DA8
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.monthNames[month - 1];
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the days of the week.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the days of the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", and "Sat".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00062BCC File Offset: 0x00060DCC
		// (set) Token: 0x06001A62 RID: 6754 RVA: 0x00062BE0 File Offset: 0x00060DE0
		public string[] AbbreviatedDayNames
		{
			get
			{
				return (string[])this.RawAbbreviatedDayNames.Clone();
			}
			set
			{
				this.RawAbbreviatedDayNames = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x00062BEC File Offset: 0x00060DEC
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00062BF4 File Offset: 0x00060DF4
		internal string[] RawAbbreviatedDayNames
		{
			get
			{
				return this.abbreviatedDayNames;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.GetLength(0) != 7)
				{
					throw new ArgumentException(DateTimeFormatInfo.MSG_ARRAYSIZE_DAY);
				}
				this.abbreviatedDayNames = (string[])value.Clone();
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the months.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the months. In a 12-month calendar, the 13th element of the array is an empty string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", and "".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00062C4C File Offset: 0x00060E4C
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x00062C60 File Offset: 0x00060E60
		public string[] AbbreviatedMonthNames
		{
			get
			{
				return (string[])this.RawAbbreviatedMonthNames.Clone();
			}
			set
			{
				this.RawAbbreviatedMonthNames = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x00062C6C File Offset: 0x00060E6C
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00062C74 File Offset: 0x00060E74
		internal string[] RawAbbreviatedMonthNames
		{
			get
			{
				return this.abbreviatedMonthNames;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.GetLength(0) != 13)
				{
					throw new ArgumentException(DateTimeFormatInfo.MSG_ARRAYSIZE_MONTH);
				}
				this.abbreviatedMonthNames = (string[])value.Clone();
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the days of the week.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the days of the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", and "Saturday".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00062CD0 File Offset: 0x00060ED0
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00062CE4 File Offset: 0x00060EE4
		public string[] DayNames
		{
			get
			{
				return (string[])this.RawDayNames.Clone();
			}
			set
			{
				this.RawDayNames = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00062CF0 File Offset: 0x00060EF0
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00062CF8 File Offset: 0x00060EF8
		internal string[] RawDayNames
		{
			get
			{
				return this.dayNames;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.GetLength(0) != 7)
				{
					throw new ArgumentException(DateTimeFormatInfo.MSG_ARRAYSIZE_DAY);
				}
				this.dayNames = (string[])value.Clone();
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the months.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the months. In a 12-month calendar, the 13th element of the array is an empty string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", and "".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00062D50 File Offset: 0x00060F50
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00062D64 File Offset: 0x00060F64
		public string[] MonthNames
		{
			get
			{
				return (string[])this.RawMonthNames.Clone();
			}
			set
			{
				this.RawMonthNames = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00062D70 File Offset: 0x00060F70
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00062D78 File Offset: 0x00060F78
		internal string[] RawMonthNames
		{
			get
			{
				return this.monthNames;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.GetLength(0) != 13)
				{
					throw new ArgumentException(DateTimeFormatInfo.MSG_ARRAYSIZE_MONTH);
				}
				this.monthNames = (string[])value.Clone();
			}
		}

		/// <summary>Gets or sets the string designator for hours that are "ante meridiem" (before noon).</summary>
		/// <returns>The string designator for hours that are "ante meridiem" (before noon). The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "AM".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00062DD4 File Offset: 0x00060FD4
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00062DDC File Offset: 0x00060FDC
		public string AMDesignator
		{
			get
			{
				return this.amDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.amDesignator = value;
			}
		}

		/// <summary>Gets or sets the string designator for hours that are "post meridiem" (after noon).</summary>
		/// <returns>The string designator for hours that are "post meridiem" (after noon). The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "PM".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00062E08 File Offset: 0x00061008
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x00062E10 File Offset: 0x00061010
		public string PMDesignator
		{
			get
			{
				return this.pmDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.pmDesignator = value;
			}
		}

		/// <summary>Gets or sets the string that separates the components of a date, that is, the year, month, and day.</summary>
		/// <returns>The string that separates the components of a date, that is, the year, month, and day. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "/".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00062E3C File Offset: 0x0006103C
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00062E44 File Offset: 0x00061044
		public string DateSeparator
		{
			get
			{
				return this.dateSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.dateSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates the components of time, that is, the hour, minutes, and seconds.</summary>
		/// <returns>The string that separates the components of time. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is ":".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00062E70 File Offset: 0x00061070
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x00062E78 File Offset: 0x00061078
		public string TimeSeparator
		{
			get
			{
				return this.timeSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.timeSeparator = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a long date value, which is associated with the "D" format pattern.</summary>
		/// <returns>The format pattern for a long date value, which is associated with the "D" format pattern.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x00062EA4 File Offset: 0x000610A4
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x00062EAC File Offset: 0x000610AC
		public string LongDatePattern
		{
			get
			{
				return this.longDatePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.longDatePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a short date value, which is associated with the "d" format pattern.</summary>
		/// <returns>The format pattern for a short date value, which is associated with the "d" format pattern.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x00062ED8 File Offset: 0x000610D8
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x00062EE0 File Offset: 0x000610E0
		public string ShortDatePattern
		{
			get
			{
				return this.shortDatePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.shortDatePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a short time value, which is associated with the "t" format pattern.</summary>
		/// <returns>The format pattern for a short time value, which is associated with the "t" format pattern.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00062F0C File Offset: 0x0006110C
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00062F14 File Offset: 0x00061114
		public string ShortTimePattern
		{
			get
			{
				return this.shortTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.shortTimePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a long time value, which is associated with the "T" format pattern.</summary>
		/// <returns>The format pattern for a long time value, which is associated with the "T" format pattern.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00062F40 File Offset: 0x00061140
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x00062F48 File Offset: 0x00061148
		public string LongTimePattern
		{
			get
			{
				return this.longTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.longTimePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a month and day value, which is associated with the "m" and "M" format patterns.</summary>
		/// <returns>The format pattern for a month and day value, which is associated with the "m" and "M" format patterns.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00062F74 File Offset: 0x00061174
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00062F7C File Offset: 0x0006117C
		public string MonthDayPattern
		{
			get
			{
				return this.monthDayPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.monthDayPattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a year and month value, which is associated with the "y" and "Y" format patterns.</summary>
		/// <returns>The format pattern for a year and month value, which is associated with the "y" and "Y" format patterns.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00062FA8 File Offset: 0x000611A8
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x00062FB0 File Offset: 0x000611B0
		public string YearMonthPattern
		{
			get
			{
				return this.yearMonthPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.yearMonthPattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for a long date and long time value, which is associated with the "F" format pattern.</summary>
		/// <returns>The format pattern for a long date and long time value, which is associated with the "F" format pattern.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00062FDC File Offset: 0x000611DC
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x00063014 File Offset: 0x00061214
		public string FullDateTimePattern
		{
			get
			{
				if (this.fullDateTimePattern != null)
				{
					return this.fullDateTimePattern;
				}
				return this.longDatePattern + " " + this.longTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.fullDateTimePattern = value;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object based on the <see cref="T:System.Globalization.CultureInfo" /> object for the current thread.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00063040 File Offset: 0x00061240
		public static DateTimeFormatInfo CurrentInfo
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.DateTimeFormat;
			}
		}

		/// <summary>Gets the default read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> that is culture-independent (invariant).</summary>
		/// <returns>The default read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that is culture-independent (invariant).</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00063054 File Offset: 0x00061254
		public static DateTimeFormatInfo InvariantInfo
		{
			get
			{
				if (DateTimeFormatInfo.theInvariantDateTimeFormatInfo == null)
				{
					DateTimeFormatInfo.theInvariantDateTimeFormatInfo = DateTimeFormatInfo.ReadOnly(new DateTimeFormatInfo());
					DateTimeFormatInfo.theInvariantDateTimeFormatInfo.FillInvariantPatterns();
				}
				return DateTimeFormatInfo.theInvariantDateTimeFormatInfo;
			}
		}

		/// <summary>Gets or sets the first day of the week.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value representing the first day of the week. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is <see cref="F:System.DayOfWeek.Sunday" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0006308C File Offset: 0x0006128C
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x00063094 File Offset: 0x00061294
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				return (DayOfWeek)this.firstDayOfWeek;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value < DayOfWeek.Sunday || value > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.firstDayOfWeek = (int)value;
			}
		}

		/// <summary>Gets or sets the calendar to use for the current culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.Calendar" /> indicating the calendar to use for the current culture. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is the <see cref="T:System.Globalization.GregorianCalendar" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to a <see cref="T:System.Globalization.Calendar" /> that is not valid for the current culture. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> is read-only. </exception>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x000630C8 File Offset: 0x000612C8
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x000630D0 File Offset: 0x000612D0
		public Calendar Calendar
		{
			get
			{
				return this.calendar;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.calendar = value;
			}
		}

		/// <summary>Gets or sets a value that specifies which rule is used to determine the first calendar week of the year.</summary>
		/// <returns>A <see cref="T:System.Globalization.CalendarWeekRule" /> value that determines the first calendar week of the year. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is <see cref="F:System.Globalization.CalendarWeekRule.FirstDay" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x000630FC File Offset: 0x000612FC
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x00063104 File Offset: 0x00061304
		public CalendarWeekRule CalendarWeekRule
		{
			get
			{
				return (CalendarWeekRule)this.calendarWeekRule;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				this.calendarWeekRule = (int)value;
			}
		}

		/// <summary>Gets the format pattern for a time value, which is based on the Internet Engineering Task Force (IETF) Request for Comments (RFC) 1123 specification and is associated with the "r" and "R" format patterns.</summary>
		/// <returns>The format pattern for a time value, which is based on the IETF RFC 1123 specification and is associated with the "r" and "R" format patterns.</returns>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00063124 File Offset: 0x00061324
		public string RFC1123Pattern
		{
			get
			{
				return this._RFC1123Pattern;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0006312C File Offset: 0x0006132C
		internal string RoundtripPattern
		{
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			}
		}

		/// <summary>Gets the format pattern for a sortable date and time value, which is associated with the "s" format pattern.</summary>
		/// <returns>The format pattern for a sortable date and time value, which is associated with the "s" format pattern.</returns>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00063134 File Offset: 0x00061334
		public string SortableDateTimePattern
		{
			get
			{
				return this._SortableDateTimePattern;
			}
		}

		/// <summary>Gets the format pattern for a universal sortable date and time value, which is associated with the "u" format pattern.</summary>
		/// <returns>The format pattern for a universal sortable date and time value, which is associated with the "u" format pattern.</returns>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0006313C File Offset: 0x0006133C
		public string UniversalSortableDateTimePattern
		{
			get
			{
				return this._UniversalSortableDateTimePattern;
			}
		}

		/// <summary>Returns all the standard patterns in which date and time values can be formatted.</summary>
		/// <returns>An array containing the standard patterns in which date and time values can be formatted.</returns>
		// Token: 0x06001A93 RID: 6803 RVA: 0x00063144 File Offset: 0x00061344
		public string[] GetAllDateTimePatterns()
		{
			return (string[])this.GetAllDateTimePatternsInternal().Clone();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00063158 File Offset: 0x00061358
		internal string[] GetAllDateTimePatternsInternal()
		{
			this.FillAllDateTimePatterns();
			return this.all_date_time_patterns;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00063168 File Offset: 0x00061368
		private void FillAllDateTimePatterns()
		{
			if (this.all_date_time_patterns != null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.GetAllRawDateTimePatterns('d'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('D'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('g'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('G'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('f'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('F'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('m'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('M'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('r'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('R'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('s'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('t'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('T'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('u'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('U'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('y'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('Y'));
			this.all_date_time_patterns = (string[])arrayList.ToArray(typeof(string));
		}

		/// <summary>Returns all the standard patterns in which date and time values can be formatted using the specified format pattern.</summary>
		/// <returns>An array containing the standard patterns in which date and time values can be formatted using the specified format pattern.</returns>
		/// <param name="format">A standard format pattern. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="format" /> is not a valid standard format pattern. </exception>
		// Token: 0x06001A96 RID: 6806 RVA: 0x00063294 File Offset: 0x00061494
		public string[] GetAllDateTimePatterns(char format)
		{
			return (string[])this.GetAllRawDateTimePatterns(format).Clone();
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000632A8 File Offset: 0x000614A8
		internal string[] GetAllRawDateTimePatterns(char format)
		{
			string[] array;
			switch (format)
			{
			case 'R':
				goto IL_2CB;
			default:
				switch (format)
				{
				case 'r':
					goto IL_2CB;
				case 's':
					return new string[]
					{
						this.SortableDateTimePattern
					};
				case 't':
					if (this.allShortTimePatterns != null && this.allShortTimePatterns.Length > 0)
					{
						return this.allShortTimePatterns;
					}
					return new string[]
					{
						this.ShortTimePattern
					};
				case 'u':
					return new string[]
					{
						this.UniversalSortableDateTimePattern
					};
				default:
					switch (format)
					{
					case 'D':
						if (this.allLongDatePatterns != null && this.allLongDatePatterns.Length > 0)
						{
							return this.allLongDatePatterns;
						}
						return new string[]
						{
							this.LongDatePattern
						};
					default:
						switch (format)
						{
						case 'd':
							if (this.allShortDatePatterns != null && this.allShortDatePatterns.Length > 0)
							{
								return this.allShortDatePatterns;
							}
							return new string[]
							{
								this.ShortDatePattern
							};
						default:
							if (format != 'M' && format != 'm')
							{
								throw new ArgumentException("Format specifier was invalid.");
							}
							if (this.monthDayPatterns != null && this.monthDayPatterns.Length > 0)
							{
								return this.monthDayPatterns;
							}
							return new string[]
							{
								this.MonthDayPattern
							};
						case 'f':
							array = this.PopulateCombinedList(this.allLongDatePatterns, this.allShortTimePatterns);
							if (array != null && array.Length > 0)
							{
								return array;
							}
							return new string[]
							{
								this.LongDatePattern + " " + this.ShortTimePattern
							};
						case 'g':
							array = this.PopulateCombinedList(this.allShortDatePatterns, this.allShortTimePatterns);
							if (array != null && array.Length > 0)
							{
								return array;
							}
							return new string[]
							{
								this.ShortDatePattern + " " + this.ShortTimePattern
							};
						}
						break;
					case 'F':
						break;
					case 'G':
						array = this.PopulateCombinedList(this.allShortDatePatterns, this.allLongTimePatterns);
						if (array != null && array.Length > 0)
						{
							return array;
						}
						return new string[]
						{
							this.ShortDatePattern + " " + this.LongTimePattern
						};
					}
					break;
				case 'y':
					goto IL_29B;
				}
				break;
			case 'T':
				if (this.allLongTimePatterns != null && this.allLongTimePatterns.Length > 0)
				{
					return this.allLongTimePatterns;
				}
				return new string[]
				{
					this.LongTimePattern
				};
			case 'U':
				break;
			case 'Y':
				goto IL_29B;
			}
			array = this.PopulateCombinedList(this.allLongDatePatterns, this.allLongTimePatterns);
			if (array != null && array.Length > 0)
			{
				return array;
			}
			return new string[]
			{
				this.LongDatePattern + " " + this.LongTimePattern
			};
			IL_29B:
			if (this.yearMonthPatterns != null && this.yearMonthPatterns.Length > 0)
			{
				return this.yearMonthPatterns;
			}
			return new string[]
			{
				this.YearMonthPattern
			};
			IL_2CB:
			return new string[]
			{
				this.RFC1123Pattern
			};
		}

		/// <summary>Returns the culture-specific full name of the specified day of the week based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific full name of the day of the week represented by <paramref name="dayofweek" />.</returns>
		/// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		// Token: 0x06001A98 RID: 6808 RVA: 0x000635BC File Offset: 0x000617BC
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.dayNames[(int)dayofweek];
		}

		/// <summary>Returns the culture-specific abbreviated name of the specified day of the week based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific abbreviated name of the day of the week represented by <paramref name="dayofweek" />.</returns>
		/// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		// Token: 0x06001A99 RID: 6809 RVA: 0x000635E8 File Offset: 0x000617E8
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.abbreviatedDayNames[(int)dayofweek];
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00063614 File Offset: 0x00061814
		private void FillInvariantPatterns()
		{
			this.allShortDatePatterns = new string[]
			{
				"MM/dd/yyyy"
			};
			this.allLongDatePatterns = new string[]
			{
				"dddd, dd MMMM yyyy"
			};
			this.allLongTimePatterns = new string[]
			{
				"HH:mm:ss"
			};
			this.allShortTimePatterns = new string[]
			{
				"HH:mm",
				"hh:mm tt",
				"H:mm",
				"h:mm tt"
			};
			this.monthDayPatterns = new string[]
			{
				"MMMM dd"
			};
			this.yearMonthPatterns = new string[]
			{
				"yyyy MMMM"
			};
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000636B4 File Offset: 0x000618B4
		private string[] PopulateCombinedList(string[] dates, string[] times)
		{
			if (dates != null && times != null)
			{
				string[] array = new string[dates.Length * times.Length];
				int num = 0;
				foreach (string str in dates)
				{
					foreach (string str2 in times)
					{
						array[num++] = str + " " + str2;
					}
				}
				return array;
			}
			return null;
		}

		/// <summary>Gets or sets a string array of abbreviated month names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>A string array of abbreviated month names.</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value array or one of the elements of the value array is null.</exception>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00063738 File Offset: 0x00061938
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x00063740 File Offset: 0x00061940
		[ComVisible(false)]
		[MonoTODO("Returns only the English month abbreviated names")]
		public string[] AbbreviatedMonthGenitiveNames
		{
			get
			{
				return this.m_genitiveAbbreviatedMonthNames;
			}
			set
			{
				this.m_genitiveAbbreviatedMonthNames = value;
			}
		}

		/// <summary>Gets or sets a string array of month names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>A string array of month names.</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value array or one of the elements of the value array is null.</exception>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0006374C File Offset: 0x0006194C
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x00063754 File Offset: 0x00061954
		[MonoTODO("Returns only the English moth names")]
		[ComVisible(false)]
		public string[] MonthGenitiveNames
		{
			get
			{
				return this.genitiveMonthNames;
			}
			set
			{
				this.genitiveMonthNames = value;
			}
		}

		/// <summary>Gets the native name of the calendar associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The native name of the calendar used in the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object if that name is available, or the empty string ("") if the native calendar name is not available.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00063760 File Offset: 0x00061960
		[ComVisible(false)]
		[MonoTODO("Returns an empty string as if the calendar name wasn't available")]
		public string NativeCalendarName
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets a string array of the shortest unique abbreviated day names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>A string array of day names.</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value array or one of the elements of the value array is null.</exception>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00063768 File Offset: 0x00061968
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x00063770 File Offset: 0x00061970
		[ComVisible(false)]
		public string[] ShortestDayNames
		{
			get
			{
				return this.shortDayNames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.Length != 7)
				{
					throw new ArgumentException("Array must have 7 entries");
				}
				for (int i = 0; i < 7; i++)
				{
					if (value[i] == null)
					{
						throw new ArgumentNullException(string.Format("Element {0} is null", i));
					}
				}
				this.shortDayNames = value;
			}
		}

		/// <summary>Obtains the shortest abbreviated day name for a specified day of the week associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The abbreviated name of the week that corresponds to the <paramref name="dayOfWeek" /> parameter.</returns>
		/// <param name="dayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values.</param>
		// Token: 0x06001AA3 RID: 6819 RVA: 0x000637D4 File Offset: 0x000619D4
		[ComVisible(false)]
		public string GetShortestDayName(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.shortDayNames[(int)dayOfWeek];
		}

		/// <summary>Sets all the custom date and time format strings that correspond to a specified standard format string.</summary>
		/// <param name="patterns">An array of custom format strings.</param>
		/// <param name="format">The standard format string associated with the custom format strings specified in the <paramref name="patterns" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="patterns" /> is a zero-length array.-or-<paramref name="format" /> is not a valid standard format string or is a standard format string whose patterns cannot be set.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="patterns" /> is null.-or-<paramref name="patterns" /> has an array element whose value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x06001AA4 RID: 6820 RVA: 0x00063800 File Offset: 0x00061A00
		[ComVisible(false)]
		public void SetAllDateTimePatterns(string[] patterns, char format)
		{
			if (patterns == null)
			{
				throw new ArgumentNullException("patterns");
			}
			if (patterns.Length == 0)
			{
				throw new ArgumentException("patterns", "The argument patterns must not be of zero-length");
			}
			if (format != 'D')
			{
				if (format != 'M')
				{
					if (format != 'T')
					{
						if (format != 'Y')
						{
							if (format == 'd')
							{
								this.allShortDatePatterns = patterns;
								return;
							}
							if (format == 'm')
							{
								goto IL_7C;
							}
							if (format == 't')
							{
								this.allShortTimePatterns = patterns;
								return;
							}
							if (format != 'y')
							{
								throw new ArgumentException("format", "Format specifier is invalid");
							}
						}
						this.yearMonthPatterns = patterns;
						return;
					}
					this.allLongTimePatterns = patterns;
					return;
				}
				IL_7C:
				this.monthDayPatterns = patterns;
			}
			else
			{
				this.allLongDatePatterns = patterns;
			}
		}

		// Token: 0x040009CB RID: 2507
		private const string _RoundtripPattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x040009CC RID: 2508
		private static readonly string MSG_READONLY = "This instance is read only";

		// Token: 0x040009CD RID: 2509
		private static readonly string MSG_ARRAYSIZE_MONTH = "An array with exactly 13 elements is needed";

		// Token: 0x040009CE RID: 2510
		private static readonly string MSG_ARRAYSIZE_DAY = "An array with exactly 7 elements is needed";

		// Token: 0x040009CF RID: 2511
		private static readonly string[] INVARIANT_ABBREVIATED_DAY_NAMES = new string[]
		{
			"Sun",
			"Mon",
			"Tue",
			"Wed",
			"Thu",
			"Fri",
			"Sat"
		};

		// Token: 0x040009D0 RID: 2512
		private static readonly string[] INVARIANT_DAY_NAMES = new string[]
		{
			"Sunday",
			"Monday",
			"Tuesday",
			"Wednesday",
			"Thursday",
			"Friday",
			"Saturday"
		};

		// Token: 0x040009D1 RID: 2513
		private static readonly string[] INVARIANT_ABBREVIATED_MONTH_NAMES = new string[]
		{
			"Jan",
			"Feb",
			"Mar",
			"Apr",
			"May",
			"Jun",
			"Jul",
			"Aug",
			"Sep",
			"Oct",
			"Nov",
			"Dec",
			string.Empty
		};

		// Token: 0x040009D2 RID: 2514
		private static readonly string[] INVARIANT_MONTH_NAMES = new string[]
		{
			"January",
			"February",
			"March",
			"April",
			"May",
			"June",
			"July",
			"August",
			"September",
			"October",
			"November",
			"December",
			string.Empty
		};

		// Token: 0x040009D3 RID: 2515
		private static readonly string[] INVARIANT_SHORT_DAY_NAMES = new string[]
		{
			"Su",
			"Mo",
			"Tu",
			"We",
			"Th",
			"Fr",
			"Sa"
		};

		// Token: 0x040009D4 RID: 2516
		private static DateTimeFormatInfo theInvariantDateTimeFormatInfo;

		// Token: 0x040009D5 RID: 2517
		private bool m_isReadOnly;

		// Token: 0x040009D6 RID: 2518
		private string amDesignator;

		// Token: 0x040009D7 RID: 2519
		private string pmDesignator;

		// Token: 0x040009D8 RID: 2520
		private string dateSeparator;

		// Token: 0x040009D9 RID: 2521
		private string timeSeparator;

		// Token: 0x040009DA RID: 2522
		private string shortDatePattern;

		// Token: 0x040009DB RID: 2523
		private string longDatePattern;

		// Token: 0x040009DC RID: 2524
		private string shortTimePattern;

		// Token: 0x040009DD RID: 2525
		private string longTimePattern;

		// Token: 0x040009DE RID: 2526
		private string monthDayPattern;

		// Token: 0x040009DF RID: 2527
		private string yearMonthPattern;

		// Token: 0x040009E0 RID: 2528
		private string fullDateTimePattern;

		// Token: 0x040009E1 RID: 2529
		private string _RFC1123Pattern;

		// Token: 0x040009E2 RID: 2530
		private string _SortableDateTimePattern;

		// Token: 0x040009E3 RID: 2531
		private string _UniversalSortableDateTimePattern;

		// Token: 0x040009E4 RID: 2532
		private int firstDayOfWeek;

		// Token: 0x040009E5 RID: 2533
		private Calendar calendar;

		// Token: 0x040009E6 RID: 2534
		private int calendarWeekRule;

		// Token: 0x040009E7 RID: 2535
		private string[] abbreviatedDayNames;

		// Token: 0x040009E8 RID: 2536
		private string[] dayNames;

		// Token: 0x040009E9 RID: 2537
		private string[] monthNames;

		// Token: 0x040009EA RID: 2538
		private string[] abbreviatedMonthNames;

		// Token: 0x040009EB RID: 2539
		private string[] allShortDatePatterns;

		// Token: 0x040009EC RID: 2540
		private string[] allLongDatePatterns;

		// Token: 0x040009ED RID: 2541
		private string[] allShortTimePatterns;

		// Token: 0x040009EE RID: 2542
		private string[] allLongTimePatterns;

		// Token: 0x040009EF RID: 2543
		private string[] monthDayPatterns;

		// Token: 0x040009F0 RID: 2544
		private string[] yearMonthPatterns;

		// Token: 0x040009F1 RID: 2545
		private string[] shortDayNames;

		// Token: 0x040009F2 RID: 2546
		private int nDataItem;

		// Token: 0x040009F3 RID: 2547
		private bool m_useUserOverride;

		// Token: 0x040009F4 RID: 2548
		private bool m_isDefaultCalendar;

		// Token: 0x040009F5 RID: 2549
		private int CultureID;

		// Token: 0x040009F6 RID: 2550
		private bool bUseCalendarInfo;

		// Token: 0x040009F7 RID: 2551
		private string generalShortTimePattern;

		// Token: 0x040009F8 RID: 2552
		private string generalLongTimePattern;

		// Token: 0x040009F9 RID: 2553
		private string[] m_eraNames;

		// Token: 0x040009FA RID: 2554
		private string[] m_abbrevEraNames;

		// Token: 0x040009FB RID: 2555
		private string[] m_abbrevEnglishEraNames;

		// Token: 0x040009FC RID: 2556
		private string[] m_dateWords;

		// Token: 0x040009FD RID: 2557
		private int[] optionalCalendars;

		// Token: 0x040009FE RID: 2558
		private string[] m_superShortDayNames;

		// Token: 0x040009FF RID: 2559
		private string[] genitiveMonthNames;

		// Token: 0x04000A00 RID: 2560
		private string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x04000A01 RID: 2561
		private string[] leapYearMonthNames;

		// Token: 0x04000A02 RID: 2562
		private DateTimeFormatFlags formatFlags;

		// Token: 0x04000A03 RID: 2563
		private string m_name;

		// Token: 0x04000A04 RID: 2564
		private volatile string[] all_date_time_patterns;
	}
}
