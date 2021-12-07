using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>Represents a time interval.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000180 RID: 384
	[ComVisible(true)]
	[Serializable]
	public struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>
	{
		/// <summary>Initializes a new <see cref="T:System.TimeSpan" /> to the specified number of ticks.</summary>
		/// <param name="ticks">A time period expressed in 100-nanosecond units. </param>
		// Token: 0x06001400 RID: 5120 RVA: 0x00050CE8 File Offset: 0x0004EEE8
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		/// <summary>Initializes a new <see cref="T:System.TimeSpan" /> to a specified number of hours, minutes, and seconds.</summary>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06001401 RID: 5121 RVA: 0x00050CF4 File Offset: 0x0004EEF4
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.CalculateTicks(0, hours, minutes, seconds, 0);
		}

		/// <summary>Initializes a new <see cref="T:System.TimeSpan" /> to a specified number of days, hours, minutes, and seconds.</summary>
		/// <param name="days">Number of days. </param>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06001402 RID: 5122 RVA: 0x00050D08 File Offset: 0x0004EF08
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.CalculateTicks(days, hours, minutes, seconds, 0);
		}

		/// <summary>Initializes a new <see cref="T:System.TimeSpan" /> to a specified number of days, hours, minutes, seconds, and milliseconds.</summary>
		/// <param name="days">Number of days. </param>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <param name="milliseconds">Number of milliseconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06001403 RID: 5123 RVA: 0x00050D1C File Offset: 0x0004EF1C
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			this._ticks = TimeSpan.CalculateTicks(days, hours, minutes, seconds, milliseconds);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00050D70 File Offset: 0x0004EF70
		internal static long CalculateTicks(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			int num = hours * 3600;
			int num2 = minutes * 60;
			long num3 = (long)(num + num2 + seconds) * 1000L + (long)milliseconds;
			num3 *= 10000L;
			bool flag = false;
			if (days > 0)
			{
				long num4 = 864000000000L * (long)days;
				if (num3 < 0L)
				{
					long num5 = num3;
					num3 += num4;
					flag = (num5 > num3);
				}
				else
				{
					num3 += num4;
					flag = (num3 < 0L);
				}
			}
			else if (days < 0)
			{
				long num6 = 864000000000L * (long)days;
				if (num3 <= 0L)
				{
					num3 += num6;
					flag = (num3 > 0L);
				}
				else
				{
					long num7 = num3;
					num3 += num6;
					flag = (num3 > num7);
				}
			}
			if (flag)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("The timespan is too big or too small."));
			}
			return num3;
		}

		/// <summary>Gets the days component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The day component of this instance. The return value can be positive or negative.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00050E3C File Offset: 0x0004F03C
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		/// <summary>Gets the hours component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The hour component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -23 through 23.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00050E50 File Offset: 0x0004F050
		public int Hours
		{
			get
			{
				return (int)(this._ticks % 864000000000L / 36000000000L);
			}
		}

		/// <summary>Gets the milliseconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -999 through 999.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00050E70 File Offset: 0x0004F070
		public int Milliseconds
		{
			get
			{
				return (int)(this._ticks % 10000000L / 10000L);
			}
		}

		/// <summary>Gets the minutes component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The minute component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00050E88 File Offset: 0x0004F088
		public int Minutes
		{
			get
			{
				return (int)(this._ticks % 36000000000L / 600000000L);
			}
		}

		/// <summary>Gets the seconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The second component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00050EA4 File Offset: 0x0004F0A4
		public int Seconds
		{
			get
			{
				return (int)(this._ticks % 600000000L / 10000000L);
			}
		}

		/// <summary>Gets the number of ticks that represent the value of the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The number of ticks contained in this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00050EBC File Offset: 0x0004F0BC
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional days.</summary>
		/// <returns>The total number of days represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00050EC4 File Offset: 0x0004F0C4
		public double TotalDays
		{
			get
			{
				return (double)this._ticks / 864000000000.0;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional hours.</summary>
		/// <returns>The total number of hours represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		public double TotalHours
		{
			get
			{
				return (double)this._ticks / 36000000000.0;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional milliseconds.</summary>
		/// <returns>The total number of milliseconds represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00050EEC File Offset: 0x0004F0EC
		public double TotalMilliseconds
		{
			get
			{
				return (double)this._ticks / 10000.0;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional minutes.</summary>
		/// <returns>The total number of minutes represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00050F00 File Offset: 0x0004F100
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks / 600000000.0;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional seconds.</summary>
		/// <returns>The total number of seconds represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00050F14 File Offset: 0x0004F114
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks / 10000000.0;
			}
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the sum of the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <returns>A new object that represents the value of this instance plus the value of <paramref name="ts" />.</returns>
		/// <param name="ts">A <see cref="T:System.TimeSpan" />. </param>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001411 RID: 5137 RVA: 0x00050F28 File Offset: 0x0004F128
		public TimeSpan Add(TimeSpan ts)
		{
			TimeSpan result;
			try
			{
				result = new TimeSpan(checked(this._ticks + ts.Ticks));
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("Resulting timespan is too big."));
			}
			return result;
		}

		/// <summary>Compares two <see cref="T:System.TimeSpan" /> values and returns an integer that indicates whether the first value is shorter than, equal to, or longer than the second value.</summary>
		/// <returns>Value Condition -1 <paramref name="t1" /> is shorter than <paramref name="t2" />0 <paramref name="t1" /> is equal to <paramref name="t2" />1 <paramref name="t1" /> is longer than <paramref name="t2" /></returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A <see cref="T:System.TimeSpan" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001412 RID: 5138 RVA: 0x00050F88 File Offset: 0x0004F188
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the specified object.</summary>
		/// <returns>Value Condition -1 This instance is shorter than <paramref name="value" />. 0 This instance is equal to <paramref name="value" />. 1 This instance is longer than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.TimeSpan" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001413 RID: 5139 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException(Locale.GetText("Argument has to be a TimeSpan."), "value");
			}
			return TimeSpan.Compare(this, (TimeSpan)value);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.TimeSpan" /> object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Value Description A negative integer This instance is shorter than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is longer than <paramref name="value" />. </returns>
		/// <param name="value">A <see cref="T:System.TimeSpan" /> object to compare to this instance.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001414 RID: 5140 RVA: 0x00050FF4 File Offset: 0x0004F1F4
		public int CompareTo(TimeSpan value)
		{
			return TimeSpan.Compare(this, value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> represents the same time interval as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.TimeSpan" /> object to compare with this instance. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001415 RID: 5141 RVA: 0x00051004 File Offset: 0x0004F204
		public bool Equals(TimeSpan obj)
		{
			return obj._ticks == this._ticks;
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A new object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001416 RID: 5142 RVA: 0x00051018 File Offset: 0x0004F218
		public TimeSpan Duration()
		{
			TimeSpan result;
			try
			{
				result = new TimeSpan(Math.Abs(this._ticks));
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("This TimeSpan value is MinValue so you cannot get the duration."));
			}
			return result;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.TimeSpan" /> object that represents the same time interval as the current <see cref="T:System.TimeSpan" /> structure; otherwise, false.</returns>
		/// <param name="value">An object to compare with this instance. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001417 RID: 5143 RVA: 0x00051074 File Offset: 0x0004F274
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		/// <summary>Returns a value indicating whether two specified instances of <see cref="T:System.TimeSpan" /> are equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001418 RID: 5144 RVA: 0x000510A4 File Offset: 0x0004F2A4
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of days, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of days, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. -or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001419 RID: 5145 RVA: 0x000510B8 File Offset: 0x0004F2B8
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.From(value, 864000000000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of hours, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of hours accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. -or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600141A RID: 5146 RVA: 0x000510CC File Offset: 0x0004F2CC
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.From(value, 36000000000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of minutes, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600141B RID: 5147 RVA: 0x000510E0 File Offset: 0x0004F2E0
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.From(value, 600000000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of seconds, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600141C RID: 5148 RVA: 0x000510F0 File Offset: 0x0004F2F0
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.From(value, 10000000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of milliseconds.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of milliseconds. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600141D RID: 5149 RVA: 0x00051100 File Offset: 0x0004F300
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.From(value, 10000L);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00051110 File Offset: 0x0004F310
		private static TimeSpan From(double value, long tickMultiplicator)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException(Locale.GetText("Value cannot be NaN."), "value");
			}
			if (double.IsNegativeInfinity(value) || double.IsPositiveInfinity(value) || value < (double)TimeSpan.MinValue.Ticks || value > (double)TimeSpan.MaxValue.Ticks)
			{
				throw new OverflowException(Locale.GetText("Outside range [MinValue,MaxValue]"));
			}
			TimeSpan result;
			try
			{
				value *= (double)(tickMultiplicator / 10000L);
				checked
				{
					long num = (long)Math.Round(value);
					result = new TimeSpan(num * 10000L);
				}
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("Resulting timespan is too big."));
			}
			return result;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified time, where the specification is in units of ticks.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> with a value of <paramref name="value" />.</returns>
		/// <param name="value">A number of ticks that represent a time. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600141F RID: 5151 RVA: 0x000511EC File Offset: 0x0004F3EC
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001420 RID: 5152 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public override int GetHashCode()
		{
			return this._ticks.GetHashCode();
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the negated value of this instance.</summary>
		/// <returns>A new object with the same numeric value as this instance, but with the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001421 RID: 5153 RVA: 0x00051204 File Offset: 0x0004F404
		public TimeSpan Negate()
		{
			if (this._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException(Locale.GetText("This TimeSpan value is MinValue and cannot be negated."));
			}
			return new TimeSpan(-this._ticks);
		}

		/// <summary>Constructs a new <see cref="T:System.TimeSpan" /> object from a time interval specified in a string.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that corresponds to <paramref name="s" />.</returns>
		/// <param name="s">A string that specifies a time interval. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components is outside its valid range. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001422 RID: 5154 RVA: 0x00051248 File Offset: 0x0004F448
		public static TimeSpan Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			TimeSpan.Parser parser = new TimeSpan.Parser(s);
			return parser.Execute();
		}

		/// <summary>Constructs a new <see cref="T:System.TimeSpan" /> object from a time interval specified in a string. Parameters specify the time interval and the variable where the new <see cref="T:System.TimeSpan" /> object is returned. </summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false. This operation returns false if the <paramref name="s" /> parameter is null, has an invalid format, represents a time interval less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		/// <param name="s">A string that specifies a time interval.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="s" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001423 RID: 5155 RVA: 0x00051274 File Offset: 0x0004F474
		public static bool TryParse(string s, out TimeSpan result)
		{
			if (s == null)
			{
				result = TimeSpan.Zero;
				return false;
			}
			bool result2;
			try
			{
				result = TimeSpan.Parse(s);
				result2 = true;
			}
			catch
			{
				result = TimeSpan.Zero;
				result2 = false;
			}
			return result2;
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the difference between the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <returns>A new time interval whose value is the result of the value of this instance minus the value of <paramref name="ts" />.</returns>
		/// <param name="ts">A <see cref="T:System.TimeSpan" />. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001424 RID: 5156 RVA: 0x000512E4 File Offset: 0x0004F4E4
		public TimeSpan Subtract(TimeSpan ts)
		{
			TimeSpan result;
			try
			{
				result = new TimeSpan(checked(this._ticks - ts.Ticks));
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("Resulting timespan is too big."));
			}
			return result;
		}

		/// <summary>Returns the string representation of the value of this instance.</summary>
		/// <returns>A string that represents the value of this instance. The return value is of the form: [-][d.]hh:mm:ss[.fffffff] Elements in square brackets ([ and ]) may not be included in the returned string. Colons and periods (: and.) are literal characters. The non-literal elements are listed in the following table.Item Description "-" A minus sign, which indicates a negative time span. No sign is included for a positive time span."d" The number of days in the time span. This element is omitted if the time span is less than one day. "hh" The number of hours in the time span, ranging from 0 to 23. "mm" The number of minutes in the time span, ranging from 0 to 59. "ss" The number of seconds in the time span, ranging from 0 to 59. "fffffff" Fractional seconds in the time span. This element is omitted if the time span does not include fractional seconds. If present, fractional seconds are always expressed using 7 decimal digits. Note:For more information about comparing the string representation of <see cref="T:System.TimeSpan" /> and Oracle data types, see article Q324577, "System.TimeSpan Does Not Match Oracle 9i INTERVAL DAY TO SECOND Data Type," in the Microsoft Knowledge Base at http://support.microsoft.com.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001425 RID: 5157 RVA: 0x00051344 File Offset: 0x0004F544
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(14);
			if (this._ticks < 0L)
			{
				stringBuilder.Append('-');
			}
			if (this.Days != 0)
			{
				stringBuilder.Append(Math.Abs(this.Days));
				stringBuilder.Append('.');
			}
			stringBuilder.Append(Math.Abs(this.Hours).ToString("D2"));
			stringBuilder.Append(':');
			stringBuilder.Append(Math.Abs(this.Minutes).ToString("D2"));
			stringBuilder.Append(':');
			stringBuilder.Append(Math.Abs(this.Seconds).ToString("D2"));
			int num = (int)Math.Abs(this._ticks % 10000000L);
			if (num != 0)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(num.ToString("D7"));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Adds two specified <see cref="T:System.TimeSpan" /> instances.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> whose value is the sum of the values of <paramref name="t1" /> and <paramref name="t2" />.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A <see cref="T:System.TimeSpan" />. </param>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001426 RID: 5158 RVA: 0x00051444 File Offset: 0x0004F644
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001427 RID: 5159 RVA: 0x00051450 File Offset: 0x0004F650
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is greater than the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001428 RID: 5160 RVA: 0x00051464 File Offset: 0x0004F664
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is greater than or equal to the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001429 RID: 5161 RVA: 0x00051478 File Offset: 0x0004F678
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are not equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are not equal; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600142A RID: 5162 RVA: 0x00051490 File Offset: 0x0004F690
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is less than the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600142B RID: 5163 RVA: 0x000514A8 File Offset: 0x0004F6A8
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is less than or equal to the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600142C RID: 5164 RVA: 0x000514BC File Offset: 0x0004F6BC
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		/// <summary>Subtracts a specified <see cref="T:System.TimeSpan" /> from another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>A TimeSpan whose value is the result of the value of <paramref name="t1" /> minus the value of <paramref name="t2" />.</returns>
		/// <param name="t1">A <see cref="T:System.TimeSpan" />. </param>
		/// <param name="t2">A TimeSpan. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600142D RID: 5165 RVA: 0x000514D4 File Offset: 0x0004F6D4
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> whose value is the negated value of the specified instance.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> with the same numeric value as this instance, but the opposite sign.</returns>
		/// <param name="t">A <see cref="T:System.TimeSpan" />. </param>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600142E RID: 5166 RVA: 0x000514E0 File Offset: 0x0004F6E0
		public static TimeSpan operator -(TimeSpan t)
		{
			return t.Negate();
		}

		/// <summary>Returns the specified instance of <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>Returns <paramref name="t" />.</returns>
		/// <param name="t">A <see cref="T:System.TimeSpan" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600142F RID: 5167 RVA: 0x000514EC File Offset: 0x0004F6EC
		public static TimeSpan operator +(TimeSpan t)
		{
			return t;
		}

		/// <summary>Represents the number of ticks in 1 day. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007C9 RID: 1993
		public const long TicksPerDay = 864000000000L;

		/// <summary>Represents the number of ticks in 1 hour. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CA RID: 1994
		public const long TicksPerHour = 36000000000L;

		/// <summary>Represents the number of ticks in 1 millisecond. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CB RID: 1995
		public const long TicksPerMillisecond = 10000L;

		/// <summary>Represents the number of ticks in 1 minute. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CC RID: 1996
		public const long TicksPerMinute = 600000000L;

		/// <summary>Represents the number of ticks in 1 second.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CD RID: 1997
		public const long TicksPerSecond = 10000000L;

		/// <summary>Represents the maximum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CE RID: 1998
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		/// <summary>Represents the minimum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CF RID: 1999
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		/// <summary>Represents the zero <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007D0 RID: 2000
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		// Token: 0x040007D1 RID: 2001
		private long _ticks;

		// Token: 0x02000181 RID: 385
		private class Parser
		{
			// Token: 0x06001430 RID: 5168 RVA: 0x000514F0 File Offset: 0x0004F6F0
			public Parser(string src)
			{
				this._src = src;
				this._length = this._src.Length;
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06001431 RID: 5169 RVA: 0x00051510 File Offset: 0x0004F710
			public bool AtEnd
			{
				get
				{
					return this._cur >= this._length;
				}
			}

			// Token: 0x06001432 RID: 5170 RVA: 0x00051524 File Offset: 0x0004F724
			private void ParseWhiteSpace()
			{
				while (!this.AtEnd && char.IsWhiteSpace(this._src, this._cur))
				{
					this._cur++;
				}
			}

			// Token: 0x06001433 RID: 5171 RVA: 0x00051568 File Offset: 0x0004F768
			private bool ParseSign()
			{
				bool result = false;
				if (!this.AtEnd && this._src[this._cur] == '-')
				{
					result = true;
					this._cur++;
				}
				return result;
			}

			// Token: 0x06001434 RID: 5172 RVA: 0x000515AC File Offset: 0x0004F7AC
			private int ParseInt(bool optional)
			{
				if (optional && this.AtEnd)
				{
					return 0;
				}
				int num = 0;
				int num2 = 0;
				while (!this.AtEnd && char.IsDigit(this._src, this._cur))
				{
					num = checked(num * 10 + (int)this._src[this._cur] - 48);
					this._cur++;
					num2++;
				}
				if (!optional && num2 == 0)
				{
					this.formatError = true;
				}
				return num;
			}

			// Token: 0x06001435 RID: 5173 RVA: 0x00051638 File Offset: 0x0004F838
			private bool ParseOptDot()
			{
				if (this.AtEnd)
				{
					return false;
				}
				if (this._src[this._cur] == '.')
				{
					this._cur++;
					return true;
				}
				return false;
			}

			// Token: 0x06001436 RID: 5174 RVA: 0x0005167C File Offset: 0x0004F87C
			private void ParseOptColon()
			{
				if (!this.AtEnd)
				{
					if (this._src[this._cur] == ':')
					{
						this._cur++;
					}
					else
					{
						this.formatError = true;
					}
				}
			}

			// Token: 0x06001437 RID: 5175 RVA: 0x000516BC File Offset: 0x0004F8BC
			private long ParseTicks()
			{
				long num = 1000000L;
				long num2 = 0L;
				bool flag = false;
				while (num > 0L && !this.AtEnd && char.IsDigit(this._src, this._cur))
				{
					num2 += (long)(this._src[this._cur] - '0') * num;
					this._cur++;
					num /= 10L;
					flag = true;
				}
				if (!flag)
				{
					this.formatError = true;
				}
				return num2;
			}

			// Token: 0x06001438 RID: 5176 RVA: 0x00051744 File Offset: 0x0004F944
			public TimeSpan Execute()
			{
				int num = 0;
				this.ParseWhiteSpace();
				bool flag = this.ParseSign();
				int num2 = this.ParseInt(false);
				if (this.ParseOptDot())
				{
					num = this.ParseInt(true);
				}
				else if (!this.AtEnd)
				{
					num = num2;
					num2 = 0;
				}
				this.ParseOptColon();
				int num3 = this.ParseInt(true);
				this.ParseOptColon();
				int num4 = this.ParseInt(true);
				long num5;
				if (this.ParseOptDot())
				{
					num5 = this.ParseTicks();
				}
				else
				{
					num5 = 0L;
				}
				this.ParseWhiteSpace();
				if (!this.AtEnd)
				{
					this.formatError = true;
				}
				if (num > 23 || num3 > 59 || num4 > 59)
				{
					throw new OverflowException(Locale.GetText("Invalid time data."));
				}
				if (this.formatError)
				{
					throw new FormatException(Locale.GetText("Invalid format for TimeSpan.Parse."));
				}
				long num6 = TimeSpan.CalculateTicks(num2, num, num3, num4, 0);
				num6 = checked((!flag) ? (num6 + num5) : ((long)(unchecked((ulong)0) - (ulong)num6 - (ulong)num5)));
				return new TimeSpan(num6);
			}

			// Token: 0x040007D2 RID: 2002
			private string _src;

			// Token: 0x040007D3 RID: 2003
			private int _cur;

			// Token: 0x040007D4 RID: 2004
			private int _length;

			// Token: 0x040007D5 RID: 2005
			private bool formatError;
		}
	}
}
