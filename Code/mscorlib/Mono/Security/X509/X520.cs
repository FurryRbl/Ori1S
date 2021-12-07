using System;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x020000D5 RID: 213
	internal class X520
	{
		// Token: 0x020000D6 RID: 214
		public abstract class AttributeTypeAndValue
		{
			// Token: 0x06000BDF RID: 3039 RVA: 0x00036A78 File Offset: 0x00034C78
			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = byte.MaxValue;
			}

			// Token: 0x06000BE0 RID: 3040 RVA: 0x00036A9C File Offset: 0x00034C9C
			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00036ABC File Offset: 0x00034CBC
			// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00036AC4 File Offset: 0x00034CC4
			public string Value
			{
				get
				{
					return this.attrValue;
				}
				set
				{
					if (this.attrValue != null && this.attrValue.Length > this.upperBound)
					{
						string text = Locale.GetText("Value length bigger than upperbound ({0}).");
						throw new FormatException(string.Format(text, this.upperBound));
					}
					this.attrValue = value;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00036B1C File Offset: 0x00034D1C
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x06000BE4 RID: 3044 RVA: 0x00036B24 File Offset: 0x00034D24
			internal ASN1 GetASN1(byte encoding)
			{
				byte b = encoding;
				if (b == 255)
				{
					b = this.SelectBestEncoding();
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.oid));
				byte b2 = b;
				switch (b2)
				{
				case 19:
					asn.Add(new ASN1(19, Encoding.ASCII.GetBytes(this.attrValue)));
					break;
				default:
					if (b2 == 30)
					{
						asn.Add(new ASN1(30, Encoding.BigEndianUnicode.GetBytes(this.attrValue)));
					}
					break;
				case 22:
					asn.Add(new ASN1(22, Encoding.ASCII.GetBytes(this.attrValue)));
					break;
				}
				return asn;
			}

			// Token: 0x06000BE5 RID: 3045 RVA: 0x00036BF4 File Offset: 0x00034DF4
			internal ASN1 GetASN1()
			{
				return this.GetASN1(this.encoding);
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x00036C04 File Offset: 0x00034E04
			public byte[] GetBytes(byte encoding)
			{
				return this.GetASN1(encoding).GetBytes();
			}

			// Token: 0x06000BE7 RID: 3047 RVA: 0x00036C14 File Offset: 0x00034E14
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x00036C24 File Offset: 0x00034E24
			private byte SelectBestEncoding()
			{
				foreach (char c in this.attrValue)
				{
					char c2 = c;
					if (c2 == '@' || c2 == '_')
					{
						return 30;
					}
					if (c > '\u007f')
					{
						return 30;
					}
				}
				return 19;
			}

			// Token: 0x04000334 RID: 820
			private string oid;

			// Token: 0x04000335 RID: 821
			private string attrValue;

			// Token: 0x04000336 RID: 822
			private int upperBound;

			// Token: 0x04000337 RID: 823
			private byte encoding;
		}

		// Token: 0x020000D7 RID: 215
		public class Name : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BE9 RID: 3049 RVA: 0x00036C84 File Offset: 0x00034E84
			public Name() : base("2.5.4.41", 32768)
			{
			}
		}

		// Token: 0x020000D8 RID: 216
		public class CommonName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BEA RID: 3050 RVA: 0x00036C98 File Offset: 0x00034E98
			public CommonName() : base("2.5.4.3", 64)
			{
			}
		}

		// Token: 0x020000D9 RID: 217
		public class SerialNumber : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BEB RID: 3051 RVA: 0x00036CA8 File Offset: 0x00034EA8
			public SerialNumber() : base("2.5.4.5", 64, 19)
			{
			}
		}

		// Token: 0x020000DA RID: 218
		public class LocalityName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BEC RID: 3052 RVA: 0x00036CBC File Offset: 0x00034EBC
			public LocalityName() : base("2.5.4.7", 128)
			{
			}
		}

		// Token: 0x020000DB RID: 219
		public class StateOrProvinceName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BED RID: 3053 RVA: 0x00036CD0 File Offset: 0x00034ED0
			public StateOrProvinceName() : base("2.5.4.8", 128)
			{
			}
		}

		// Token: 0x020000DC RID: 220
		public class OrganizationName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BEE RID: 3054 RVA: 0x00036CE4 File Offset: 0x00034EE4
			public OrganizationName() : base("2.5.4.10", 64)
			{
			}
		}

		// Token: 0x020000DD RID: 221
		public class OrganizationalUnitName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BEF RID: 3055 RVA: 0x00036CF4 File Offset: 0x00034EF4
			public OrganizationalUnitName() : base("2.5.4.11", 64)
			{
			}
		}

		// Token: 0x020000DE RID: 222
		public class EmailAddress : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF0 RID: 3056 RVA: 0x00036D04 File Offset: 0x00034F04
			public EmailAddress() : base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		// Token: 0x020000DF RID: 223
		public class DomainComponent : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF1 RID: 3057 RVA: 0x00036D18 File Offset: 0x00034F18
			public DomainComponent() : base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		// Token: 0x020000E0 RID: 224
		public class UserId : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF2 RID: 3058 RVA: 0x00036D2C File Offset: 0x00034F2C
			public UserId() : base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		// Token: 0x020000E1 RID: 225
		public class Oid : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF3 RID: 3059 RVA: 0x00036D40 File Offset: 0x00034F40
			public Oid(string oid) : base(oid, int.MaxValue)
			{
			}
		}

		// Token: 0x020000E2 RID: 226
		public class Title : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF4 RID: 3060 RVA: 0x00036D50 File Offset: 0x00034F50
			public Title() : base("2.5.4.12", 64)
			{
			}
		}

		// Token: 0x020000E3 RID: 227
		public class CountryName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF5 RID: 3061 RVA: 0x00036D60 File Offset: 0x00034F60
			public CountryName() : base("2.5.4.6", 2, 19)
			{
			}
		}

		// Token: 0x020000E4 RID: 228
		public class DnQualifier : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x00036D70 File Offset: 0x00034F70
			public DnQualifier() : base("2.5.4.46", 2, 19)
			{
			}
		}

		// Token: 0x020000E5 RID: 229
		public class Surname : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF7 RID: 3063 RVA: 0x00036D80 File Offset: 0x00034F80
			public Surname() : base("2.5.4.4", 32768)
			{
			}
		}

		// Token: 0x020000E6 RID: 230
		public class GivenName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF8 RID: 3064 RVA: 0x00036D94 File Offset: 0x00034F94
			public GivenName() : base("2.5.4.42", 16)
			{
			}
		}

		// Token: 0x020000E7 RID: 231
		public class Initial : X520.AttributeTypeAndValue
		{
			// Token: 0x06000BF9 RID: 3065 RVA: 0x00036DA4 File Offset: 0x00034FA4
			public Initial() : base("2.5.4.43", 5)
			{
			}
		}
	}
}
