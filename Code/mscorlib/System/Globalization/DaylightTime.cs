using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the period of daylight saving time.</summary>
	// Token: 0x02000213 RID: 531
	[ComVisible(true)]
	[Serializable]
	public class DaylightTime
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.DaylightTime" /> class.</summary>
		/// <param name="start">The <see cref="T:System.DateTime" /> that represents the date and time when the daylight saving period begins. The value must be in local time. </param>
		/// <param name="end">The <see cref="T:System.DateTime" /> that represents the date and time when the daylight saving period ends. The value must be in local time. </param>
		/// <param name="delta">The <see cref="T:System.TimeSpan" /> that represents the difference between the standard time and the daylight saving time in ticks. </param>
		// Token: 0x06001AA5 RID: 6821 RVA: 0x000638D8 File Offset: 0x00061AD8
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this.m_start = start;
			this.m_end = end;
			this.m_delta = delta;
		}

		/// <summary>Gets the <see cref="T:System.DateTime" /> that represents the date and time when the daylight saving period begins.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that represents the date and time when the daylight saving period begins. The value is in local time.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x000638F8 File Offset: 0x00061AF8
		public DateTime Start
		{
			get
			{
				return this.m_start;
			}
		}

		/// <summary>Gets the <see cref="T:System.DateTime" /> that represents the date and time when the daylight saving period ends.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that represents the date and time when the daylight saving period ends. The value is in local time.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00063900 File Offset: 0x00061B00
		public DateTime End
		{
			get
			{
				return this.m_end;
			}
		}

		/// <summary>Gets the <see cref="T:System.TimeSpan" /> that represents the difference between the standard time and the daylight saving time.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> that represents the difference between the standard time and the daylight saving time.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00063908 File Offset: 0x00061B08
		public TimeSpan Delta
		{
			get
			{
				return this.m_delta;
			}
		}

		// Token: 0x04000A10 RID: 2576
		private DateTime m_start;

		// Token: 0x04000A11 RID: 2577
		private DateTime m_end;

		// Token: 0x04000A12 RID: 2578
		private TimeSpan m_delta;
	}
}
