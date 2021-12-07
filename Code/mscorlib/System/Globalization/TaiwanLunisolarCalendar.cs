using System;

namespace System.Globalization
{
	/// <summary>Represents the Taiwan lunisolar calendar. As for the Taiwan calendar, years are calculated using the Gregorian calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x02000228 RID: 552
	[Serializable]
	public class TaiwanLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class. </summary>
		// Token: 0x06001C61 RID: 7265 RVA: 0x00068A50 File Offset: 0x00066C50
		[MonoTODO]
		public TaiwanLunisolarCalendar() : base(TaiwanLunisolarCalendar.era_handler)
		{
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00068A60 File Offset: 0x00066C60
		static TaiwanLunisolarCalendar()
		{
			TaiwanLunisolarCalendar.era_handler = new CCEastAsianLunisolarEraHandler();
			TaiwanLunisolarCalendar.era_handler.appendEra(1, CCFixed.FromDateTime(TaiwanLunisolarCalendar.TaiwanMin), CCFixed.FromDateTime(TaiwanLunisolarCalendar.TaiwanMax));
		}

		/// <summary>Gets the eras that are relevant to the current <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> object.</summary>
		/// <returns>An array that consists of a single element having a value that is always the current era.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00068AC8 File Offset: 0x00066CC8
		public override int[] Eras
		{
			get
			{
				return (int[])TaiwanLunisolarCalendar.era_handler.Eras.Clone();
			}
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001C64 RID: 7268 RVA: 0x00068AE0 File Offset: 0x00066CE0
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			TaiwanLunisolarCalendar.era_handler.EraYear(out result, date);
			return result;
		}

		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class, which is equivalent to the first moment of February 18, 1912 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00068B04 File Offset: 0x00066D04
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanLunisolarCalendar.TaiwanMin;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class, which is equivalent to the last moment of February 10, 2051 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00068B0C File Offset: 0x00066D0C
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return TaiwanLunisolarCalendar.TaiwanMax;
			}
		}

		// Token: 0x04000A9B RID: 2715
		private const int TaiwanEra = 1;

		// Token: 0x04000A9C RID: 2716
		internal static readonly CCEastAsianLunisolarEraHandler era_handler;

		// Token: 0x04000A9D RID: 2717
		private static DateTime TaiwanMin = new DateTime(1912, 2, 18);

		// Token: 0x04000A9E RID: 2718
		private static DateTime TaiwanMax = new DateTime(2051, 2, 10, 23, 59, 59, 999);
	}
}
