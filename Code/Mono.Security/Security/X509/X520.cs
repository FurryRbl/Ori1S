using System;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x02000051 RID: 81
	public class X520
	{
		// Token: 0x02000052 RID: 82
		public abstract class AttributeTypeAndValue
		{
			// Token: 0x060003A9 RID: 937 RVA: 0x000188DC File Offset: 0x00016ADC
			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = byte.MaxValue;
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00018900 File Offset: 0x00016B00
			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060003AB RID: 939 RVA: 0x00018920 File Offset: 0x00016B20
			// (set) Token: 0x060003AC RID: 940 RVA: 0x00018928 File Offset: 0x00016B28
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

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060003AD RID: 941 RVA: 0x00018980 File Offset: 0x00016B80
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x060003AE RID: 942 RVA: 0x00018988 File Offset: 0x00016B88
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

			// Token: 0x060003AF RID: 943 RVA: 0x00018A58 File Offset: 0x00016C58
			internal ASN1 GetASN1()
			{
				return this.GetASN1(this.encoding);
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00018A68 File Offset: 0x00016C68
			public byte[] GetBytes(byte encoding)
			{
				return this.GetASN1(encoding).GetBytes();
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00018A78 File Offset: 0x00016C78
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x00018A88 File Offset: 0x00016C88
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

			// Token: 0x040001B5 RID: 437
			private string oid;

			// Token: 0x040001B6 RID: 438
			private string attrValue;

			// Token: 0x040001B7 RID: 439
			private int upperBound;

			// Token: 0x040001B8 RID: 440
			private byte encoding;
		}

		// Token: 0x02000053 RID: 83
		public class Name : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B3 RID: 947 RVA: 0x00018AE8 File Offset: 0x00016CE8
			public Name() : base("2.5.4.41", 32768)
			{
			}
		}

		// Token: 0x02000054 RID: 84
		public class CommonName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B4 RID: 948 RVA: 0x00018AFC File Offset: 0x00016CFC
			public CommonName() : base("2.5.4.3", 64)
			{
			}
		}

		// Token: 0x02000055 RID: 85
		public class SerialNumber : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B5 RID: 949 RVA: 0x00018B0C File Offset: 0x00016D0C
			public SerialNumber() : base("2.5.4.5", 64, 19)
			{
			}
		}

		// Token: 0x02000056 RID: 86
		public class LocalityName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B6 RID: 950 RVA: 0x00018B20 File Offset: 0x00016D20
			public LocalityName() : base("2.5.4.7", 128)
			{
			}
		}

		// Token: 0x02000057 RID: 87
		public class StateOrProvinceName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B7 RID: 951 RVA: 0x00018B34 File Offset: 0x00016D34
			public StateOrProvinceName() : base("2.5.4.8", 128)
			{
			}
		}

		// Token: 0x02000058 RID: 88
		public class OrganizationName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B8 RID: 952 RVA: 0x00018B48 File Offset: 0x00016D48
			public OrganizationName() : base("2.5.4.10", 64)
			{
			}
		}

		// Token: 0x02000059 RID: 89
		public class OrganizationalUnitName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003B9 RID: 953 RVA: 0x00018B58 File Offset: 0x00016D58
			public OrganizationalUnitName() : base("2.5.4.11", 64)
			{
			}
		}

		// Token: 0x0200005A RID: 90
		public class EmailAddress : X520.AttributeTypeAndValue
		{
			// Token: 0x060003BA RID: 954 RVA: 0x00018B68 File Offset: 0x00016D68
			public EmailAddress() : base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		// Token: 0x0200005B RID: 91
		public class DomainComponent : X520.AttributeTypeAndValue
		{
			// Token: 0x060003BB RID: 955 RVA: 0x00018B7C File Offset: 0x00016D7C
			public DomainComponent() : base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		// Token: 0x0200005C RID: 92
		public class UserId : X520.AttributeTypeAndValue
		{
			// Token: 0x060003BC RID: 956 RVA: 0x00018B90 File Offset: 0x00016D90
			public UserId() : base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		// Token: 0x0200005D RID: 93
		public class Oid : X520.AttributeTypeAndValue
		{
			// Token: 0x060003BD RID: 957 RVA: 0x00018BA4 File Offset: 0x00016DA4
			public Oid(string oid) : base(oid, int.MaxValue)
			{
			}
		}

		// Token: 0x0200005E RID: 94
		public class Title : X520.AttributeTypeAndValue
		{
			// Token: 0x060003BE RID: 958 RVA: 0x00018BB4 File Offset: 0x00016DB4
			public Title() : base("2.5.4.12", 64)
			{
			}
		}

		// Token: 0x0200005F RID: 95
		public class CountryName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003BF RID: 959 RVA: 0x00018BC4 File Offset: 0x00016DC4
			public CountryName() : base("2.5.4.6", 2, 19)
			{
			}
		}

		// Token: 0x02000060 RID: 96
		public class DnQualifier : X520.AttributeTypeAndValue
		{
			// Token: 0x060003C0 RID: 960 RVA: 0x00018BD4 File Offset: 0x00016DD4
			public DnQualifier() : base("2.5.4.46", 2, 19)
			{
			}
		}

		// Token: 0x02000061 RID: 97
		public class Surname : X520.AttributeTypeAndValue
		{
			// Token: 0x060003C1 RID: 961 RVA: 0x00018BE4 File Offset: 0x00016DE4
			public Surname() : base("2.5.4.4", 32768)
			{
			}
		}

		// Token: 0x02000062 RID: 98
		public class GivenName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003C2 RID: 962 RVA: 0x00018BF8 File Offset: 0x00016DF8
			public GivenName() : base("2.5.4.42", 16)
			{
			}
		}

		// Token: 0x02000063 RID: 99
		public class Initial : X520.AttributeTypeAndValue
		{
			// Token: 0x060003C3 RID: 963 RVA: 0x00018C08 File Offset: 0x00016E08
			public Initial() : base("2.5.4.43", 5)
			{
			}
		}
	}
}
