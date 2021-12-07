using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as months, days, and years. Years are calculated using the Chinese calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class ChineseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> class. </summary>
		// Token: 0x060019C3 RID: 6595 RVA: 0x00060548 File Offset: 0x0005E748
		[MonoTODO]
		public ChineseLunisolarCalendar() : base(ChineseLunisolarCalendar.era_handler)
		{
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00060558 File Offset: 0x0005E758
		static ChineseLunisolarCalendar()
		{
			ChineseLunisolarCalendar.era_handler = new CCEastAsianLunisolarEraHandler();
			ChineseLunisolarCalendar.era_handler.appendEra(1, CCFixed.FromDateTime(new DateTime(1, 1, 1)));
		}

		/// <summary>Gets the eras that correspond to the range of dates and times supported by the current <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> object.</summary>
		/// <returns>An array of 32-bit signed integers that specify the relevant eras. The return value for a <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> object is always an array containing one element equal to the <see cref="F:System.Globalization.ChineseLunisolarCalendar.ChineseEra" /> value.</returns>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x000605B8 File Offset: 0x0005E7B8
		[ComVisible(false)]
		public override int[] Eras
		{
			get
			{
				return (int[])ChineseLunisolarCalendar.era_handler.Eras.Clone();
			}
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" /> type.</summary>
		/// <returns>An integer that represents the era in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> type to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.ChineseLunisolarCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.ChineseLunisolarCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060019C6 RID: 6598 RVA: 0x000605D0 File Offset: 0x0005E7D0
		[ComVisible(false)]
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			ChineseLunisolarCalendar.era_handler.EraYear(out result, date);
			return result;
		}

		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> class.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> type that represents February 19, 1901 in the Gregorian calendar, which is equivalent to the constructor, DateTime(1901, 2, 19).</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x000605F4 File Offset: 0x0005E7F4
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return ChineseLunisolarCalendar.ChineseMin;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> class.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> type that represents the last moment on January 28, 2101 in the Gregorian calendar, which is approximately equal to the constructor DateTime(2101, 1, 28, 23, 59, 59, 999).</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000605FC File Offset: 0x0005E7FC
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return ChineseLunisolarCalendar.ChineseMax;
			}
		}

		/// <summary>Specifies the era that corresponds to the current <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> object.</summary>
		// Token: 0x04000972 RID: 2418
		public const int ChineseEra = 1;

		// Token: 0x04000973 RID: 2419
		internal static readonly CCEastAsianLunisolarEraHandler era_handler;

		// Token: 0x04000974 RID: 2420
		private static DateTime ChineseMin = new DateTime(1901, 2, 19);

		// Token: 0x04000975 RID: 2421
		private static DateTime ChineseMax = new DateTime(2101, 1, 28, 23, 59, 59, 999);
	}
}
