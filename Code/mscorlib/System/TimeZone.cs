using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a time zone.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000182 RID: 386
	[ComVisible(true)]
	[Serializable]
	public abstract class TimeZone
	{
		/// <summary>Gets the time zone of the current computer.</summary>
		/// <returns>A <see cref="T:System.TimeZone" /> object that represents the current local time zone.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00051870 File Offset: 0x0004FA70
		public static TimeZone CurrentTimeZone
		{
			get
			{
				return TimeZone.currentTimeZone;
			}
		}

		/// <summary>Gets the daylight saving time zone name.</summary>
		/// <returns>The daylight saving time zone name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600143C RID: 5180
		public abstract string DaylightName { get; }

		/// <summary>Gets the standard time zone name.</summary>
		/// <returns>The standard time zone name.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set this property to null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600143D RID: 5181
		public abstract string StandardName { get; }

		/// <summary>Returns the daylight saving time period for a particular year.</summary>
		/// <returns>A <see cref="T:System.Globalization.DaylightTime" /> object that contains the start and end date for daylight saving time in <paramref name="year" />.</returns>
		/// <param name="year">The year that the daylight saving time period applies to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600143E RID: 5182
		public abstract DaylightTime GetDaylightChanges(int year);

		/// <summary>Returns the Coordinated Universal Time (UTC) offset for the specified local time.</summary>
		/// <returns>The Coordinated Universal Time (UTC) offset from <paramref name="Time" />.</returns>
		/// <param name="time">A date and time value.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600143F RID: 5183
		public abstract TimeSpan GetUtcOffset(DateTime time);

		/// <summary>Returns a value indicating whether the specified date and time is within a daylight saving time period.</summary>
		/// <returns>true if <paramref name="time" /> is in a daylight saving time period; otherwise, false.</returns>
		/// <param name="time">A date and time. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001440 RID: 5184 RVA: 0x00051878 File Offset: 0x0004FA78
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		/// <summary>Returns a value indicating whether the specified date and time is within the specified daylight saving time period.</summary>
		/// <returns>true if <paramref name="time" /> is in <paramref name="daylightTimes" />; otherwise, false.</returns>
		/// <param name="time">A date and time. </param>
		/// <param name="daylightTimes">A daylight saving time period. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="daylightTimes" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001441 RID: 5185 RVA: 0x00051890 File Offset: 0x0004FA90
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				throw new ArgumentNullException("daylightTimes");
			}
			if (daylightTimes.Start.Ticks == daylightTimes.End.Ticks)
			{
				return false;
			}
			if (daylightTimes.Start.Ticks < daylightTimes.End.Ticks)
			{
				if (daylightTimes.Start.Ticks < time.Ticks && daylightTimes.End.Ticks > time.Ticks)
				{
					return true;
				}
			}
			else if (time.Year == daylightTimes.Start.Year && time.Year == daylightTimes.End.Year && (time.Ticks < daylightTimes.End.Ticks || time.Ticks > daylightTimes.Start.Ticks))
			{
				return true;
			}
			return false;
		}

		/// <summary>Returns the local time that corresponds to a specified date and time value.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the local time that corresponds to <paramref name="time" />.</returns>
		/// <param name="time">A Coordinated Universal Time (UTC) time. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001442 RID: 5186 RVA: 0x000519A0 File Offset: 0x0004FBA0
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			TimeSpan utcOffset = this.GetUtcOffset(time);
			if (utcOffset.Ticks > 0L)
			{
				if (DateTime.MaxValue - utcOffset < time)
				{
					return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Local);
				}
			}
			else if (utcOffset.Ticks < 0L && time.Ticks + utcOffset.Ticks < DateTime.MinValue.Ticks)
			{
				return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Local);
			}
			DateTime dateTime = time.Add(utcOffset);
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			if (daylightChanges.Delta.Ticks == 0L)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
			}
			if (dateTime < daylightChanges.End && daylightChanges.End.Subtract(daylightChanges.Delta) <= dateTime)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
			}
			TimeSpan utcOffset2 = this.GetUtcOffset(dateTime);
			return DateTime.SpecifyKind(time.Add(utcOffset2), DateTimeKind.Local);
		}

		/// <summary>Returns the Coordinated Universal Time (UTC) that corresponds to a specified time.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the Coordinated Universal Time (UTC) that corresponds to <paramref name="time" />.</returns>
		/// <param name="time">A date and time. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001443 RID: 5187 RVA: 0x00051AB8 File Offset: 0x0004FCB8
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			TimeSpan utcOffset = this.GetUtcOffset(time);
			if (utcOffset.Ticks < 0L)
			{
				if (DateTime.MaxValue + utcOffset < time)
				{
					return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
				}
			}
			else if (utcOffset.Ticks > 0L && DateTime.MinValue + utcOffset > time)
			{
				return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
			}
			return DateTime.SpecifyKind(new DateTime(time.Ticks - utcOffset.Ticks), DateTimeKind.Utc);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00051B5C File Offset: 0x0004FD5C
		internal TimeSpan GetLocalTimeDiff(DateTime time)
		{
			return this.GetLocalTimeDiff(time, this.GetUtcOffset(time));
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00051B6C File Offset: 0x0004FD6C
		internal TimeSpan GetLocalTimeDiff(DateTime time, TimeSpan utc_offset)
		{
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			if (daylightChanges.Delta.Ticks == 0L)
			{
				return utc_offset;
			}
			DateTime dateTime = time.Add(utc_offset);
			if (dateTime < daylightChanges.End && daylightChanges.End.Subtract(daylightChanges.Delta) <= dateTime)
			{
				return utc_offset;
			}
			if (dateTime >= daylightChanges.Start && daylightChanges.Start.Add(daylightChanges.Delta) > dateTime)
			{
				return utc_offset - daylightChanges.Delta;
			}
			return this.GetUtcOffset(dateTime);
		}

		// Token: 0x040007D6 RID: 2006
		private static TimeZone currentTimeZone = new CurrentSystemTimeZone(DateTime.GetNow());
	}
}
