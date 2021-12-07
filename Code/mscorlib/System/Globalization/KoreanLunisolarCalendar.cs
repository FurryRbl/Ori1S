using System;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as months, days, and years. Years are calculated using the Gregorian calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class KoreanLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> class. </summary>
		// Token: 0x06001BAC RID: 7084 RVA: 0x000665A4 File Offset: 0x000647A4
		[MonoTODO]
		public KoreanLunisolarCalendar() : base(KoreanLunisolarCalendar.era_handler)
		{
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000665B4 File Offset: 0x000647B4
		static KoreanLunisolarCalendar()
		{
			KoreanLunisolarCalendar.era_handler = new CCEastAsianLunisolarEraHandler();
			KoreanLunisolarCalendar.era_handler.appendEra(1, CCFixed.FromDateTime(new DateTime(1, 1, 1)));
		}

		/// <summary>Gets the eras that correspond to the range of dates and times supported by the current <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> object.</summary>
		/// <returns>An array of 32-bit signed integers that specify the relevant eras. The return value for a <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> object is always an array containing one element equal to the <see cref="F:System.Globalization.KoreanLunisolarCalendar.GregorianEra" /> value.</returns>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00066610 File Offset: 0x00064810
		public override int[] Eras
		{
			get
			{
				return (int[])KoreanLunisolarCalendar.era_handler.Eras.Clone();
			}
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era specified by the <paramref name="time" /> parameter. The return value for a <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> object is always the <see cref="F:System.Globalization.KoreanLunisolarCalendar.GregorianEra" /> value.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> represents a date and time less than <see cref="P:System.Globalization.KoreanLunisolarCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.KoreanLunisolarCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06001BAF RID: 7087 RVA: 0x00066628 File Offset: 0x00064828
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			KoreanLunisolarCalendar.era_handler.EraYear(out result, date);
			return result;
		}

		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> class, which is equivalent to the first moment of February 14, 918 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0006664C File Offset: 0x0006484C
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return KoreanLunisolarCalendar.KoreanMin;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> class, which is equivalent to the last  moment of February 10, 2051 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00066654 File Offset: 0x00064854
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return KoreanLunisolarCalendar.KoreanMax;
			}
		}

		/// <summary>Specifies the Gregorian era that corresponds to the current <see cref="T:System.Globalization.KoreanLunisolarCalendar" /> object.</summary>
		// Token: 0x04000A48 RID: 2632
		public const int GregorianEra = 1;

		// Token: 0x04000A49 RID: 2633
		internal static readonly CCEastAsianLunisolarEraHandler era_handler;

		// Token: 0x04000A4A RID: 2634
		private static DateTime KoreanMin = new DateTime(918, 2, 14, 0, 0, 0);

		// Token: 0x04000A4B RID: 2635
		private static DateTime KoreanMax = new DateTime(2051, 2, 10, 23, 59, 59);
	}
}
