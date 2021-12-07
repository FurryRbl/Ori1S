using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x0200028A RID: 650
	internal class XmlCustomFormatter
	{
		// Token: 0x06001A57 RID: 6743 RVA: 0x000898B0 File Offset: 0x00087AB0
		internal static string FromByteArrayBase64(byte[] value)
		{
			return (value != null) ? Convert.ToBase64String(value) : string.Empty;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000898C8 File Offset: 0x00087AC8
		internal static string FromByteArrayHex(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in value)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0008991C File Offset: 0x00087B1C
		internal static string FromChar(char value)
		{
			int num = (int)value;
			return num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00089938 File Offset: 0x00087B38
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00089948 File Offset: 0x00087B48
		internal static string FromDateTime(DateTime value)
		{
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00089954 File Offset: 0x00087B54
		internal static string FromTime(DateTime value)
		{
			return XmlConvert.ToString(value, "HH:mm:ss.fffffffzzz");
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00089964 File Offset: 0x00087B64
		internal static string FromEnum(long value, string[] values, long[] ids)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, null);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00089970 File Offset: 0x00087B70
		internal static string FromEnum(long value, string[] values, long[] ids, string typeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = ids.Length;
			long num2 = value;
			int num3 = -1;
			for (int i = 0; i < num; i++)
			{
				if (ids[i] == 0L)
				{
					num3 = i;
				}
				else
				{
					if (num2 == 0L)
					{
						break;
					}
					if ((ids[i] & value) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(' ');
						}
						stringBuilder.Append(values[i]);
						num2 &= ~ids[i];
					}
				}
			}
			if (num2 == 0L)
			{
				if (stringBuilder.Length == 0 && num3 != -1)
				{
					stringBuilder.Append(values[num3]);
				}
				return stringBuilder.ToString();
			}
			if (typeName != null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value for {1}.", new object[]
				{
					value,
					typeName
				}));
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value.", new object[]
			{
				value
			}));
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00089A70 File Offset: 0x00087C70
		internal static string FromXmlName(string name)
		{
			return XmlConvert.EncodeName(name);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00089A78 File Offset: 0x00087C78
		internal static string FromXmlNCName(string ncName)
		{
			return XmlConvert.EncodeLocalName(ncName);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00089A80 File Offset: 0x00087C80
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.EncodeNmToken(nmToken);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00089A88 File Offset: 0x00087C88
		internal static string FromXmlNmTokens(string nmTokens)
		{
			string[] array = nmTokens.Split(new char[]
			{
				' '
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = XmlCustomFormatter.FromXmlNmToken(array[i]);
			}
			return string.Join(" ", array);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00089AD4 File Offset: 0x00087CD4
		internal static byte[] ToByteArrayBase64(string value)
		{
			return Convert.FromBase64String(value);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00089ADC File Offset: 0x00087CDC
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00089AE4 File Offset: 0x00087CE4
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00089AEC File Offset: 0x00087CEC
		internal static DateTime ToDateTime(string value)
		{
			return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00089AF8 File Offset: 0x00087CF8
		internal static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value);
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00089B00 File Offset: 0x00087D00
		internal static long ToEnum(string value, Hashtable values, string typeName, bool validate)
		{
			long num = 0L;
			string[] array = value.Split(new char[]
			{
				' '
			});
			foreach (string text in array)
			{
				object obj = values[text];
				if (obj != null)
				{
					num |= (long)obj;
				}
				else if (validate && text.Length != 0)
				{
					throw new InvalidOperationException(string.Format("'{0}' is not a valid member of type {1}.", text, typeName));
				}
			}
			return num;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00089B84 File Offset: 0x00087D84
		internal static string ToXmlName(string value)
		{
			return XmlConvert.DecodeName(value);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00089B8C File Offset: 0x00087D8C
		internal static string ToXmlNCName(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00089B94 File Offset: 0x00087D94
		internal static string ToXmlNmToken(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00089B9C File Offset: 0x00087D9C
		internal static string ToXmlNmTokens(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00089BA4 File Offset: 0x00087DA4
		internal static string ToXmlString(TypeData type, object value)
		{
			if (value == null)
			{
				return null;
			}
			string xmlType = type.XmlType;
			switch (xmlType)
			{
			case "boolean":
				return XmlConvert.ToString((bool)value);
			case "unsignedByte":
				return XmlConvert.ToString((byte)value);
			case "char":
				return XmlConvert.ToString((int)((char)value));
			case "dateTime":
				return XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
			case "date":
				return ((DateTime)value).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			case "time":
				return ((DateTime)value).ToString("HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture);
			case "decimal":
				return XmlConvert.ToString((decimal)value);
			case "double":
				return XmlConvert.ToString((double)value);
			case "short":
				return XmlConvert.ToString((short)value);
			case "int":
				return XmlConvert.ToString((int)value);
			case "long":
				return XmlConvert.ToString((long)value);
			case "byte":
				return XmlConvert.ToString((sbyte)value);
			case "float":
				return XmlConvert.ToString((float)value);
			case "unsignedShort":
				return XmlConvert.ToString((ushort)value);
			case "unsignedInt":
				return XmlConvert.ToString((uint)value);
			case "unsignedLong":
				return XmlConvert.ToString((ulong)value);
			case "guid":
				return XmlConvert.ToString((Guid)value);
			case "base64":
			case "base64Binary":
				return (value != null) ? Convert.ToBase64String((byte[])value) : string.Empty;
			case "hexBinary":
				return (value != null) ? XmlConvert.ToBinHexString((byte[])value) : string.Empty;
			case "duration":
				return (string)value;
			}
			return (!(value is IFormattable)) ? value.ToString() : ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00089EA4 File Offset: 0x000880A4
		internal static object FromXmlString(TypeData type, string value)
		{
			if (value == null)
			{
				return null;
			}
			string xmlType = type.XmlType;
			switch (xmlType)
			{
			case "boolean":
				return XmlConvert.ToBoolean(value);
			case "unsignedByte":
				return XmlConvert.ToByte(value);
			case "char":
				return (char)XmlConvert.ToInt32(value);
			case "dateTime":
				return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
			case "date":
				return DateTime.ParseExact(value, "yyyy-MM-dd", null);
			case "time":
				return DateTime.ParseExact(value, "HH:mm:ss.FFFFFFF", null);
			case "decimal":
				return XmlConvert.ToDecimal(value);
			case "double":
				return XmlConvert.ToDouble(value);
			case "short":
				return XmlConvert.ToInt16(value);
			case "int":
				return XmlConvert.ToInt32(value);
			case "long":
				return XmlConvert.ToInt64(value);
			case "byte":
				return XmlConvert.ToSByte(value);
			case "float":
				return XmlConvert.ToSingle(value);
			case "unsignedShort":
				return XmlConvert.ToUInt16(value);
			case "unsignedInt":
				return XmlConvert.ToUInt32(value);
			case "unsignedLong":
				return XmlConvert.ToUInt64(value);
			case "guid":
				return XmlConvert.ToGuid(value);
			case "base64":
			case "base64Binary":
				return Convert.FromBase64String(value);
			case "hexBinary":
				return XmlConvert.FromBinHexString(value);
			case "duration":
				return value;
			}
			if (type.Type != null)
			{
				return Convert.ChangeType(value, type.Type);
			}
			return value;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0008A158 File Offset: 0x00088358
		internal static string GenerateToXmlString(TypeData type, string value)
		{
			if (type.NullableOverride)
			{
				return string.Concat(new string[]
				{
					"(",
					value,
					" != null ? ",
					XmlCustomFormatter.GenerateToXmlStringCore(type, value),
					" : null)"
				});
			}
			return XmlCustomFormatter.GenerateToXmlStringCore(type, value);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0008A1AC File Offset: 0x000883AC
		private static string GenerateToXmlStringCore(TypeData type, string value)
		{
			if (type.NullableOverride)
			{
				value += ".Value";
			}
			string xmlType = type.XmlType;
			switch (xmlType)
			{
			case "boolean":
				return "(" + value + "?\"true\":\"false\")";
			case "unsignedByte":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "char":
				return "((int)(" + value + ")).ToString(CultureInfo.InvariantCulture)";
			case "dateTime":
				return "XmlConvert.ToString (" + value + ", XmlDateTimeSerializationMode.RoundtripKind)";
			case "date":
				return value + ".ToString(\"yyyy-MM-dd\", CultureInfo.InvariantCulture)";
			case "time":
				return value + ".ToString(\"HH:mm:ss.FFFFFFF\", CultureInfo.InvariantCulture)";
			case "decimal":
				return "XmlConvert.ToString (" + value + ")";
			case "double":
				return "XmlConvert.ToString (" + value + ")";
			case "short":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "int":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "long":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "byte":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "float":
				return "XmlConvert.ToString (" + value + ")";
			case "unsignedShort":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "unsignedInt":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "unsignedLong":
				return value + ".ToString(CultureInfo.InvariantCulture)";
			case "guid":
				return "XmlConvert.ToString (" + value + ")";
			case "base64":
			case "base64Binary":
				return value + " == null ? String.Empty : Convert.ToBase64String (" + value + ")";
			case "hexBinary":
				return value + " == null ? String.Empty : ToBinHexString (" + value + ")";
			case "duration":
				return value;
			case "NMTOKEN":
			case "Name":
			case "NCName":
			case "language":
			case "ENTITY":
			case "ID":
			case "IDREF":
			case "NOTATION":
			case "token":
			case "normalizedString":
			case "string":
				return value;
			}
			return string.Concat(new string[]
			{
				"((",
				value,
				" != null) ? (",
				value,
				").ToString() : null)"
			});
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0008A540 File Offset: 0x00088740
		internal static string GenerateFromXmlString(TypeData type, string value)
		{
			if (type.NullableOverride)
			{
				return string.Concat(new string[]
				{
					"(",
					value,
					" != null ? (",
					type.CSharpName,
					"?)",
					XmlCustomFormatter.GenerateFromXmlStringCore(type, value),
					" : null)"
				});
			}
			return XmlCustomFormatter.GenerateFromXmlStringCore(type, value);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0008A5A4 File Offset: 0x000887A4
		private static string GenerateFromXmlStringCore(TypeData type, string value)
		{
			string xmlType = type.XmlType;
			switch (xmlType)
			{
			case "boolean":
				return "XmlConvert.ToBoolean (" + value + ")";
			case "unsignedByte":
				return "byte.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "char":
				return "(char)Int32.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "dateTime":
				return "XmlConvert.ToDateTime (" + value + ", XmlDateTimeSerializationMode.RoundtripKind)";
			case "date":
				return "DateTime.ParseExact (" + value + ", \"yyyy-MM-dd\", CultureInfo.InvariantCulture)";
			case "time":
				return "DateTime.ParseExact (" + value + ", \"HH:mm:ss.FFFFFFF\", CultureInfo.InvariantCulture)";
			case "decimal":
				return "Decimal.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "double":
				return "XmlConvert.ToDouble (" + value + ")";
			case "short":
				return "Int16.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "int":
				return "Int32.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "long":
				return "Int64.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "byte":
				return "SByte.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "float":
				return "XmlConvert.ToSingle (" + value + ")";
			case "unsignedShort":
				return "UInt16.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "unsignedInt":
				return "UInt32.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "unsignedLong":
				return "UInt64.Parse (" + value + ", CultureInfo.InvariantCulture)";
			case "guid":
				return "XmlConvert.ToGuid (" + value + ")";
			case "base64:":
			case "base64Binary":
				return "Convert.FromBase64String (" + value + ")";
			case "hexBinary":
				return "FromBinHexString (" + value + ")";
			case "duration":
				return value;
			}
			return value;
		}
	}
}
