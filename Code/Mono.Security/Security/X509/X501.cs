using System;
using System.Globalization;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000041 RID: 65
	public sealed class X501
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00014468 File Offset: 0x00012668
		private X501()
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000145EC File Offset: 0x000127EC
		public static string ToString(ASN1 seq)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < seq.Count; i++)
			{
				ASN1 entry = seq[i];
				X501.AppendEntry(stringBuilder, entry, true);
				if (i < seq.Count - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00014648 File Offset: 0x00012848
		public static string ToString(ASN1 seq, bool reversed, string separator, bool quotes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (reversed)
			{
				for (int i = seq.Count - 1; i >= 0; i--)
				{
					ASN1 entry = seq[i];
					X501.AppendEntry(stringBuilder, entry, quotes);
					if (i > 0)
					{
						stringBuilder.Append(separator);
					}
				}
			}
			else
			{
				for (int j = 0; j < seq.Count; j++)
				{
					ASN1 entry2 = seq[j];
					X501.AppendEntry(stringBuilder, entry2, quotes);
					if (j < seq.Count - 1)
					{
						stringBuilder.Append(separator);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000146E4 File Offset: 0x000128E4
		private static void AppendEntry(StringBuilder sb, ASN1 entry, bool quotes)
		{
			for (int i = 0; i < entry.Count; i++)
			{
				ASN1 asn = entry[i];
				ASN1 asn2 = asn[1];
				if (asn2 != null)
				{
					ASN1 asn3 = asn[0];
					if (asn3 != null)
					{
						if (asn3.CompareValue(X501.countryName))
						{
							sb.Append("C=");
						}
						else if (asn3.CompareValue(X501.organizationName))
						{
							sb.Append("O=");
						}
						else if (asn3.CompareValue(X501.organizationalUnitName))
						{
							sb.Append("OU=");
						}
						else if (asn3.CompareValue(X501.commonName))
						{
							sb.Append("CN=");
						}
						else if (asn3.CompareValue(X501.localityName))
						{
							sb.Append("L=");
						}
						else if (asn3.CompareValue(X501.stateOrProvinceName))
						{
							sb.Append("S=");
						}
						else if (asn3.CompareValue(X501.streetAddress))
						{
							sb.Append("STREET=");
						}
						else if (asn3.CompareValue(X501.domainComponent))
						{
							sb.Append("DC=");
						}
						else if (asn3.CompareValue(X501.userid))
						{
							sb.Append("UID=");
						}
						else if (asn3.CompareValue(X501.email))
						{
							sb.Append("E=");
						}
						else if (asn3.CompareValue(X501.dnQualifier))
						{
							sb.Append("dnQualifier=");
						}
						else if (asn3.CompareValue(X501.title))
						{
							sb.Append("T=");
						}
						else if (asn3.CompareValue(X501.surname))
						{
							sb.Append("SN=");
						}
						else if (asn3.CompareValue(X501.givenName))
						{
							sb.Append("G=");
						}
						else if (asn3.CompareValue(X501.initial))
						{
							sb.Append("I=");
						}
						else
						{
							sb.Append("OID.");
							sb.Append(ASN1Convert.ToOid(asn3));
							sb.Append("=");
						}
						string text;
						if (asn2.Tag == 30)
						{
							StringBuilder stringBuilder = new StringBuilder();
							for (int j = 1; j < asn2.Value.Length; j += 2)
							{
								stringBuilder.Append((char)asn2.Value[j]);
							}
							text = stringBuilder.ToString();
						}
						else
						{
							if (asn2.Tag == 20)
							{
								text = Encoding.UTF7.GetString(asn2.Value);
							}
							else
							{
								text = Encoding.UTF8.GetString(asn2.Value);
							}
							char[] anyOf = new char[]
							{
								',',
								'+',
								'"',
								'\\',
								'<',
								'>',
								';'
							};
							if (quotes && (text.IndexOfAny(anyOf, 0, text.Length) > 0 || text.StartsWith(" ") || text.EndsWith(" ")))
							{
								text = "\"" + text + "\"";
							}
						}
						sb.Append(text);
						if (i < entry.Count - 1)
						{
							sb.Append(", ");
						}
					}
				}
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00014A5C File Offset: 0x00012C5C
		private static X520.AttributeTypeAndValue GetAttributeFromOid(string attributeType)
		{
			string text = attributeType.ToUpper(CultureInfo.InvariantCulture).Trim();
			string text2 = text;
			switch (text2)
			{
			case "C":
				return new X520.CountryName();
			case "O":
				return new X520.OrganizationName();
			case "OU":
				return new X520.OrganizationalUnitName();
			case "CN":
				return new X520.CommonName();
			case "L":
				return new X520.LocalityName();
			case "S":
			case "ST":
				return new X520.StateOrProvinceName();
			case "E":
				return new X520.EmailAddress();
			case "DC":
				return new X520.DomainComponent();
			case "UID":
				return new X520.UserId();
			case "DNQUALIFIER":
				return new X520.DnQualifier();
			case "T":
				return new X520.Title();
			case "SN":
				return new X520.Surname();
			case "G":
				return new X520.GivenName();
			case "I":
				return new X520.Initial();
			}
			if (text.StartsWith("OID."))
			{
				return new X520.Oid(text.Substring(4));
			}
			if (X501.IsOid(text))
			{
				return new X520.Oid(text);
			}
			return null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00014C2C File Offset: 0x00012E2C
		private static bool IsOid(string oid)
		{
			bool result;
			try
			{
				ASN1 asn = ASN1Convert.FromOid(oid);
				result = (asn.Tag == 6);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00014C80 File Offset: 0x00012E80
		private static X520.AttributeTypeAndValue ReadAttribute(string value, ref int pos)
		{
			while (value[pos] == ' ' && pos < value.Length)
			{
				pos++;
			}
			int num = value.IndexOf('=', pos);
			if (num == -1)
			{
				string text = Locale.GetText("No attribute found.");
				throw new FormatException(text);
			}
			string text2 = value.Substring(pos, num - pos);
			X520.AttributeTypeAndValue attributeFromOid = X501.GetAttributeFromOid(text2);
			if (attributeFromOid == null)
			{
				string text3 = Locale.GetText("Unknown attribute '{0}'.");
				throw new FormatException(string.Format(text3, text2));
			}
			pos = num + 1;
			return attributeFromOid;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00014D14 File Offset: 0x00012F14
		private static bool IsHex(char c)
		{
			if (char.IsDigit(c))
			{
				return true;
			}
			char c2 = char.ToUpper(c, CultureInfo.InvariantCulture);
			return c2 >= 'A' && c2 <= 'F';
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00014D50 File Offset: 0x00012F50
		private static string ReadHex(string value, ref int pos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value[pos++]);
			stringBuilder.Append(value[pos]);
			if (pos < value.Length - 4 && value[pos + 1] == '\\' && X501.IsHex(value[pos + 2]))
			{
				pos += 2;
				stringBuilder.Append(value[pos++]);
				stringBuilder.Append(value[pos]);
			}
			byte[] bytes = CryptoConvert.FromHex(stringBuilder.ToString());
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00014E00 File Offset: 0x00013000
		private static int ReadEscaped(StringBuilder sb, string value, int pos)
		{
			char c = value[pos];
			switch (c)
			{
			case ';':
			case '<':
			case '=':
			case '>':
				break;
			default:
				if (c != '"' && c != '#' && c != '+' && c != ',' && c != '\\')
				{
					if (pos >= value.Length - 2)
					{
						string text = Locale.GetText("Malformed escaped value '{0}'.");
						throw new FormatException(string.Format(text, value.Substring(pos)));
					}
					sb.Append(X501.ReadHex(value, ref pos));
					return pos;
				}
				break;
			}
			sb.Append(value[pos]);
			return pos;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00014EA8 File Offset: 0x000130A8
		private static int ReadQuoted(StringBuilder sb, string value, int pos)
		{
			int startIndex = pos;
			while (pos <= value.Length)
			{
				char c = value[pos];
				if (c == '"')
				{
					return pos;
				}
				if (c == '\\')
				{
					return X501.ReadEscaped(sb, value, pos);
				}
				sb.Append(value[pos]);
				pos++;
			}
			string text = Locale.GetText("Malformed quoted value '{0}'.");
			throw new FormatException(string.Format(text, value.Substring(startIndex)));
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014F28 File Offset: 0x00013128
		private static string ReadValue(string value, ref int pos)
		{
			int startIndex = pos;
			StringBuilder stringBuilder = new StringBuilder();
			while (pos < value.Length)
			{
				char c = value[pos];
				switch (c)
				{
				case ';':
				case '<':
				case '=':
				case '>':
				{
					string text = Locale.GetText("Malformed value '{0}' contains '{1}' outside quotes.");
					throw new FormatException(string.Format(text, value.Substring(startIndex), value[pos]));
				}
				default:
					if (c != '"')
					{
						if (c == '#' || c == '+')
						{
							throw new NotImplementedException();
						}
						if (c == ',')
						{
							pos++;
							return stringBuilder.ToString();
						}
						if (c != '\\')
						{
							stringBuilder.Append(value[pos]);
						}
						else
						{
							pos = X501.ReadEscaped(stringBuilder, value, ++pos);
						}
					}
					else
					{
						pos = X501.ReadQuoted(stringBuilder, value, ++pos);
					}
					pos++;
					break;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00015030 File Offset: 0x00013230
		public static ASN1 FromString(string rdn)
		{
			if (rdn == null)
			{
				throw new ArgumentNullException("rdn");
			}
			int i = 0;
			ASN1 asn = new ASN1(48);
			while (i < rdn.Length)
			{
				X520.AttributeTypeAndValue attributeTypeAndValue = X501.ReadAttribute(rdn, ref i);
				attributeTypeAndValue.Value = X501.ReadValue(rdn, ref i);
				ASN1 asn2 = new ASN1(49);
				asn2.Add(attributeTypeAndValue.GetASN1());
				asn.Add(asn2);
			}
			return asn;
		}

		// Token: 0x0400014B RID: 331
		private static byte[] countryName = new byte[]
		{
			85,
			4,
			6
		};

		// Token: 0x0400014C RID: 332
		private static byte[] organizationName = new byte[]
		{
			85,
			4,
			10
		};

		// Token: 0x0400014D RID: 333
		private static byte[] organizationalUnitName = new byte[]
		{
			85,
			4,
			11
		};

		// Token: 0x0400014E RID: 334
		private static byte[] commonName = new byte[]
		{
			85,
			4,
			3
		};

		// Token: 0x0400014F RID: 335
		private static byte[] localityName = new byte[]
		{
			85,
			4,
			7
		};

		// Token: 0x04000150 RID: 336
		private static byte[] stateOrProvinceName = new byte[]
		{
			85,
			4,
			8
		};

		// Token: 0x04000151 RID: 337
		private static byte[] streetAddress = new byte[]
		{
			85,
			4,
			9
		};

		// Token: 0x04000152 RID: 338
		private static byte[] domainComponent = new byte[]
		{
			9,
			146,
			38,
			137,
			147,
			242,
			44,
			100,
			1,
			25
		};

		// Token: 0x04000153 RID: 339
		private static byte[] userid = new byte[]
		{
			9,
			146,
			38,
			137,
			147,
			242,
			44,
			100,
			1,
			1
		};

		// Token: 0x04000154 RID: 340
		private static byte[] email = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			9,
			1
		};

		// Token: 0x04000155 RID: 341
		private static byte[] dnQualifier = new byte[]
		{
			85,
			4,
			46
		};

		// Token: 0x04000156 RID: 342
		private static byte[] title = new byte[]
		{
			85,
			4,
			12
		};

		// Token: 0x04000157 RID: 343
		private static byte[] surname = new byte[]
		{
			85,
			4,
			4
		};

		// Token: 0x04000158 RID: 344
		private static byte[] givenName = new byte[]
		{
			85,
			4,
			42
		};

		// Token: 0x04000159 RID: 345
		private static byte[] initial = new byte[]
		{
			85,
			4,
			43
		};
	}
}
