using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020002CE RID: 718
	internal class XQueryConvert
	{
		// Token: 0x06001E2D RID: 7725 RVA: 0x0009DBB8 File Offset: 0x0009BDB8
		private XQueryConvert()
		{
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0009DBC0 File Offset: 0x0009BDC0
		public static XmlTypeCode GetFallbackType(XmlTypeCode type)
		{
			switch (type)
			{
			case XmlTypeCode.AnyAtomicType:
				return XmlTypeCode.Item;
			case XmlTypeCode.UntypedAtomic:
				return XmlTypeCode.String;
			case XmlTypeCode.Notation:
				return XmlTypeCode.QName;
			case XmlTypeCode.NormalizedString:
			case XmlTypeCode.Token:
			case XmlTypeCode.Language:
			case XmlTypeCode.NmToken:
			case XmlTypeCode.Name:
			case XmlTypeCode.NCName:
			case XmlTypeCode.Id:
			case XmlTypeCode.Idref:
			case XmlTypeCode.Entity:
				return XmlTypeCode.String;
			case XmlTypeCode.NonPositiveInteger:
				return XmlTypeCode.Decimal;
			case XmlTypeCode.NegativeInteger:
				return XmlTypeCode.NonPositiveInteger;
			case XmlTypeCode.Long:
				return XmlTypeCode.Integer;
			case XmlTypeCode.Short:
				return XmlTypeCode.Int;
			case XmlTypeCode.Byte:
				return XmlTypeCode.Int;
			case XmlTypeCode.NonNegativeInteger:
				return XmlTypeCode.Decimal;
			case XmlTypeCode.UnsignedLong:
				return XmlTypeCode.NonNegativeInteger;
			case XmlTypeCode.UnsignedInt:
				return XmlTypeCode.Integer;
			case XmlTypeCode.UnsignedShort:
				return XmlTypeCode.Int;
			case XmlTypeCode.UnsignedByte:
				return XmlTypeCode.UnsignedShort;
			case XmlTypeCode.PositiveInteger:
				return XmlTypeCode.NonNegativeInteger;
			}
			return XmlTypeCode.None;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0009DCB8 File Offset: 0x0009BEB8
		public static string AnyUriToString(string value)
		{
			return value;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0009DCBC File Offset: 0x0009BEBC
		public static byte[] Base64BinaryToHexBinary(byte[] value)
		{
			return XmlConvert.FromBinHexString(Convert.ToBase64String(value));
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0009DCCC File Offset: 0x0009BECC
		public static string Base64BinaryToString(byte[] value)
		{
			return Convert.ToBase64String(value);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0009DCD4 File Offset: 0x0009BED4
		public static decimal BooleanToDecimal(bool value)
		{
			return Convert.ToDecimal(value);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public static double BooleanToDouble(bool value)
		{
			return Convert.ToDouble(value);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0009DCE4 File Offset: 0x0009BEE4
		public static float BooleanToFloat(bool value)
		{
			return Convert.ToSingle(value);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0009DCEC File Offset: 0x0009BEEC
		public static int BooleanToInt(bool value)
		{
			return Convert.ToInt32(value);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0009DCF4 File Offset: 0x0009BEF4
		public static long BooleanToInteger(bool value)
		{
			return Convert.ToInt64(value);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0009DCFC File Offset: 0x0009BEFC
		public static string BooleanToString(bool value)
		{
			return (!value) ? "false" : "true";
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0009DD14 File Offset: 0x0009BF14
		public static DateTime DateTimeToDate(DateTime value)
		{
			return value.Date;
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0009DD20 File Offset: 0x0009BF20
		public static DateTime DateTimeToGDay(DateTime value)
		{
			return new DateTime(0, 0, value.Day);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0009DD30 File Offset: 0x0009BF30
		public static DateTime DateTimeToGMonth(DateTime value)
		{
			return new DateTime(0, value.Month, 0);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0009DD40 File Offset: 0x0009BF40
		public static DateTime DateTimeToGMonthDay(DateTime value)
		{
			return new DateTime(0, value.Month, value.Day);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0009DD58 File Offset: 0x0009BF58
		public static DateTime DateTimeToGYear(DateTime value)
		{
			return new DateTime(value.Year, 0, 0);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0009DD68 File Offset: 0x0009BF68
		public static DateTime DateTimeToGYearMonth(DateTime value)
		{
			return new DateTime(value.Year, value.Month, 0);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0009DD80 File Offset: 0x0009BF80
		public static DateTime DateTimeToTime(DateTime value)
		{
			return new DateTime(value.TimeOfDay.Ticks);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0009DDA4 File Offset: 0x0009BFA4
		public static DateTime DateToDateTime(DateTime value)
		{
			return value.Date;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0009DDB0 File Offset: 0x0009BFB0
		public static DateTime DateToGDay(DateTime value)
		{
			return new DateTime(0, 0, value.Day);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0009DDC0 File Offset: 0x0009BFC0
		public static DateTime DateToGMonth(DateTime value)
		{
			return new DateTime(0, value.Month, 0);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0009DDD0 File Offset: 0x0009BFD0
		public static DateTime DateToGMonthDay(DateTime value)
		{
			return new DateTime(0, value.Month, value.Day);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0009DDE8 File Offset: 0x0009BFE8
		public static DateTime DateToGYear(DateTime value)
		{
			return new DateTime(value.Year, 0, 0);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0009DDF8 File Offset: 0x0009BFF8
		public static DateTime DateToGYearMonth(DateTime value)
		{
			return new DateTime(value.Year, value.Month, 0);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0009DE10 File Offset: 0x0009C010
		public static string DateToString(DateTime value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0009DE18 File Offset: 0x0009C018
		public static string DateTimeToString(DateTime value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0009DE20 File Offset: 0x0009C020
		public static string DayTimeDurationToDuration(TimeSpan value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0009DE28 File Offset: 0x0009C028
		public static string DayTimeDurationToString(TimeSpan value)
		{
			return XQueryConvert.DayTimeDurationToDuration(value);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0009DE30 File Offset: 0x0009C030
		public static bool DecimalToBoolean(decimal value)
		{
			return value != 0m;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0009DE40 File Offset: 0x0009C040
		public static double DecimalToDouble(decimal value)
		{
			return Convert.ToDouble(value);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0009DE48 File Offset: 0x0009C048
		public static float DecimalToFloat(decimal value)
		{
			return Convert.ToSingle(value);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0009DE50 File Offset: 0x0009C050
		public static int DecimalToInt(decimal value)
		{
			return Convert.ToInt32(value);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0009DE58 File Offset: 0x0009C058
		public static long DecimalToInteger(decimal value)
		{
			return Convert.ToInt64(value);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0009DE60 File Offset: 0x0009C060
		public static string DecimalToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0009DE68 File Offset: 0x0009C068
		public static bool DoubleToBoolean(double value)
		{
			return value != 0.0;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0009DE7C File Offset: 0x0009C07C
		public static decimal DoubleToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0009DE84 File Offset: 0x0009C084
		public static float DoubleToFloat(double value)
		{
			return Convert.ToSingle(value);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0009DE8C File Offset: 0x0009C08C
		public static int DoubleToInt(double value)
		{
			return Convert.ToInt32(value);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0009DE94 File Offset: 0x0009C094
		public static long DoubleToInteger(double value)
		{
			return Convert.ToInt64(value);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0009DE9C File Offset: 0x0009C09C
		public static string DoubleToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0009DEA4 File Offset: 0x0009C0A4
		public static TimeSpan DurationToDayTimeDuration(string value)
		{
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0009DEAC File Offset: 0x0009C0AC
		public static string DurationToString(string value)
		{
			return XmlConvert.ToString(XmlConvert.ToTimeSpan(value));
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0009DEBC File Offset: 0x0009C0BC
		public static TimeSpan DurationToYearMonthDuration(string value)
		{
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0009DEC4 File Offset: 0x0009C0C4
		public static bool FloatToBoolean(float value)
		{
			return value != 0f;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0009DED4 File Offset: 0x0009C0D4
		public static decimal FloatToDecimal(float value)
		{
			return (decimal)value;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0009DEDC File Offset: 0x0009C0DC
		public static double FloatToDouble(float value)
		{
			return Convert.ToDouble(value);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0009DEE4 File Offset: 0x0009C0E4
		public static int FloatToInt(float value)
		{
			return Convert.ToInt32(value);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0009DEEC File Offset: 0x0009C0EC
		public static long FloatToInteger(float value)
		{
			return Convert.ToInt64(value);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0009DEF4 File Offset: 0x0009C0F4
		public static string FloatToString(float value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0009DEFC File Offset: 0x0009C0FC
		public static string GDayToString(DateTime value)
		{
			return XmlConvert.ToString(TimeSpan.FromDays((double)value.Day));
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0009DF10 File Offset: 0x0009C110
		public static string GMonthDayToString(DateTime value)
		{
			return XmlConvert.ToString(new TimeSpan(value.Day, value.Hour, value.Minute, value.Second));
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0009DF44 File Offset: 0x0009C144
		public static string GMonthToString(DateTime value)
		{
			return XmlConvert.ToString(new TimeSpan(0, value.Month, 0));
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0009DF5C File Offset: 0x0009C15C
		public static string GYearMonthToString(DateTime value)
		{
			return XmlConvert.ToString(new TimeSpan(value.Year, value.Month, 0));
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0009DF78 File Offset: 0x0009C178
		public static string GYearToString(DateTime value)
		{
			DateTime dateTime = new DateTime(value.Year, 0, 0);
			return XmlConvert.ToString(new TimeSpan(dateTime.Ticks));
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0009DFA8 File Offset: 0x0009C1A8
		public static string HexBinaryToString(byte[] data)
		{
			return XmlConvert.ToBinHexString(data);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0009DFB0 File Offset: 0x0009C1B0
		public static byte[] HexBinaryToBase64Binary(byte[] data)
		{
			return data;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0009DFB4 File Offset: 0x0009C1B4
		public static bool IntegerToBoolean(long value)
		{
			return value != 0L;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0009DFC0 File Offset: 0x0009C1C0
		public static decimal IntegerToDecimal(long value)
		{
			return value;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0009DFC8 File Offset: 0x0009C1C8
		public static double IntegerToDouble(long value)
		{
			return Convert.ToDouble(value);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0009DFD0 File Offset: 0x0009C1D0
		public static float IntegerToFloat(long value)
		{
			return Convert.ToSingle(value);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0009DFD8 File Offset: 0x0009C1D8
		public static int IntegerToInt(long value)
		{
			return Convert.ToInt32(value);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0009DFE0 File Offset: 0x0009C1E0
		public static string IntegerToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0009DFE8 File Offset: 0x0009C1E8
		public static bool IntToBoolean(int value)
		{
			return value != 0;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0009DFF4 File Offset: 0x0009C1F4
		public static decimal IntToDecimal(int value)
		{
			return value;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0009DFFC File Offset: 0x0009C1FC
		public static double IntToDouble(int value)
		{
			return Convert.ToDouble(value);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0009E004 File Offset: 0x0009C204
		public static float IntToFloat(int value)
		{
			return Convert.ToSingle(value);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0009E00C File Offset: 0x0009C20C
		public static long IntToInteger(int value)
		{
			return (long)value;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0009E010 File Offset: 0x0009C210
		public static string IntToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0009E018 File Offset: 0x0009C218
		public static string NonNegativeIntegerToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0009E020 File Offset: 0x0009C220
		public static string NonPositiveIntegerToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0009E028 File Offset: 0x0009C228
		public static DateTime TimeToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0009E02C File Offset: 0x0009C22C
		public static string TimeToString(DateTime value)
		{
			return XmlConvert.ToString(value, "HH:mm:ssZ");
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0009E03C File Offset: 0x0009C23C
		public static string YearMonthDurationToDuration(TimeSpan value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0009E044 File Offset: 0x0009C244
		public static string YearMonthDurationToString(TimeSpan value)
		{
			return XQueryConvert.YearMonthDurationToDuration(value);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0009E04C File Offset: 0x0009C24C
		public static string StringToAnyUri(string value)
		{
			return value;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0009E050 File Offset: 0x0009C250
		public static byte[] StringToBase64Binary(string value)
		{
			return Convert.FromBase64String(value);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0009E058 File Offset: 0x0009C258
		public static bool StringToBoolean(string value)
		{
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0009E060 File Offset: 0x0009C260
		public static DateTime StringToDate(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0009E068 File Offset: 0x0009C268
		public static DateTime StringToDateTime(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0009E070 File Offset: 0x0009C270
		public static TimeSpan StringToDayTimeDuration(string value)
		{
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0009E078 File Offset: 0x0009C278
		public static decimal StringToDecimal(string value)
		{
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0009E080 File Offset: 0x0009C280
		public static double StringToDouble(string value)
		{
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0009E088 File Offset: 0x0009C288
		public static string StringToDuration(string value)
		{
			return XmlConvert.ToString(XmlConvert.ToTimeSpan(value));
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0009E098 File Offset: 0x0009C298
		public static float StringToFloat(string value)
		{
			return XmlConvert.ToSingle(value);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0009E0A0 File Offset: 0x0009C2A0
		public static DateTime StringToGDay(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0009E0A8 File Offset: 0x0009C2A8
		public static DateTime StringToGMonth(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0009E0B0 File Offset: 0x0009C2B0
		public static DateTime StringToGMonthDay(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0009E0B8 File Offset: 0x0009C2B8
		public static DateTime StringToGYear(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0009E0C0 File Offset: 0x0009C2C0
		public static DateTime StringToGYearMonth(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0009E0C8 File Offset: 0x0009C2C8
		public static byte[] StringToHexBinary(string value)
		{
			return XmlConvert.FromBinHexString(value);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0009E0D0 File Offset: 0x0009C2D0
		public static int StringToInt(string value)
		{
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0009E0D8 File Offset: 0x0009C2D8
		public static long StringToInteger(string value)
		{
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0009E0E0 File Offset: 0x0009C2E0
		public static decimal StringToNonNegativeInteger(string value)
		{
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0009E0E8 File Offset: 0x0009C2E8
		public static decimal StringToNonPositiveInteger(string value)
		{
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0009E0F0 File Offset: 0x0009C2F0
		public static DateTime StringToTime(string value)
		{
			return XmlConvert.ToDateTime(value);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0009E0F8 File Offset: 0x0009C2F8
		public static long StringToUnsignedInt(string value)
		{
			return (long)((ulong)XmlConvert.ToUInt32(value));
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0009E104 File Offset: 0x0009C304
		public static decimal StringToUnsignedLong(string value)
		{
			return XmlConvert.ToUInt64(value);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0009E114 File Offset: 0x0009C314
		public static int StringToUnsignedShort(string value)
		{
			return (int)XmlConvert.ToUInt16(value);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0009E11C File Offset: 0x0009C31C
		public static TimeSpan StringToYearMonthDuration(string value)
		{
			return XmlConvert.ToTimeSpan(value);
		}
	}
}
