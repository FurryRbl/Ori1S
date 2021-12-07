using System;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as months, days, and years. Years are calculated as for the Japanese calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x0200021E RID: 542
	[Serializable]
	public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class. </summary>
		// Token: 0x06001B69 RID: 7017 RVA: 0x00065D54 File Offset: 0x00063F54
		[MonoTODO]
		public JapaneseLunisolarCalendar() : base(JapaneseLunisolarCalendar.era_handler)
		{
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00065D64 File Offset: 0x00063F64
		static JapaneseLunisolarCalendar()
		{
			JapaneseLunisolarCalendar.era_handler = new CCEastAsianLunisolarEraHandler();
			JapaneseLunisolarCalendar.era_handler.appendEra(3, CCGregorianCalendar.fixed_from_dmy(25, 12, 1926), CCGregorianCalendar.fixed_from_dmy(7, 1, 1989));
			JapaneseLunisolarCalendar.era_handler.appendEra(4, CCGregorianCalendar.fixed_from_dmy(8, 1, 1989));
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00065DE4 File Offset: 0x00063FE4
		internal override int ActualCurrentEra
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Gets the eras that are relevant to the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> object.</summary>
		/// <returns>An array of 32-bit signed integers that specify the relevant eras.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x00065DE8 File Offset: 0x00063FE8
		public override int[] Eras
		{
			get
			{
				return (int[])JapaneseLunisolarCalendar.era_handler.Eras.Clone();
			}
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06001B6D RID: 7021 RVA: 0x00065E00 File Offset: 0x00064000
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			JapaneseLunisolarCalendar.era_handler.EraYear(out result, date);
			return result;
		}

		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class, which is equivalent to the first moment of January 28, 1960 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x00065E24 File Offset: 0x00064024
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.JapanMin;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class, which is equivalent to the last moment of January 22, 2050 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00065E2C File Offset: 0x0006402C
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.JapanMax;
			}
		}

		/// <summary>Specifies the current era.</summary>
		// Token: 0x04000A3D RID: 2621
		public const int JapaneseEra = 1;

		// Token: 0x04000A3E RID: 2622
		internal static readonly CCEastAsianLunisolarEraHandler era_handler;

		// Token: 0x04000A3F RID: 2623
		private static DateTime JapanMin = new DateTime(1960, 1, 28, 0, 0, 0);

		// Token: 0x04000A40 RID: 2624
		private static DateTime JapanMax = new DateTime(2050, 1, 22, 23, 59, 59);
	}
}
