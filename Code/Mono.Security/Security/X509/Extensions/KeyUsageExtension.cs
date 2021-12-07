using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200006E RID: 110
	public class KeyUsageExtension : X509Extension
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public KeyUsageExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001A3D0 File Offset: 0x000185D0
		public KeyUsageExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001A3DC File Offset: 0x000185DC
		public KeyUsageExtension()
		{
			this.extnOid = "2.5.29.15";
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001A3F0 File Offset: 0x000185F0
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 3)
			{
				throw new ArgumentException("Invalid KeyUsage extension");
			}
			int i = 1;
			while (i < asn.Value.Length)
			{
				this.kubits = (this.kubits << 8) + (int)asn.Value[i++];
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001A458 File Offset: 0x00018658
		protected override void Encode()
		{
			this.extnValue = new ASN1(4);
			ushort num = (ushort)this.kubits;
			if (num > 0)
			{
				byte b;
				for (b = 15; b > 0; b -= 1)
				{
					if ((num & 32768) == 32768)
					{
						break;
					}
					num = (ushort)(num << 1);
				}
				if (this.kubits > 255)
				{
					b -= 8;
					this.extnValue.Add(new ASN1(3, new byte[]
					{
						b,
						(byte)this.kubits,
						(byte)(this.kubits >> 8)
					}));
				}
				else
				{
					this.extnValue.Add(new ASN1(3, new byte[]
					{
						b,
						(byte)this.kubits
					}));
				}
			}
			else
			{
				ASN1 extnValue = this.extnValue;
				byte tag = 3;
				byte[] array = new byte[2];
				array[0] = 7;
				extnValue.Add(new ASN1(tag, array));
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001A548 File Offset: 0x00018748
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0001A550 File Offset: 0x00018750
		public KeyUsages KeyUsage
		{
			get
			{
				return (KeyUsages)this.kubits;
			}
			set
			{
				this.kubits = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001A568 File Offset: 0x00018768
		public override string Name
		{
			get
			{
				return "Key Usage";
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001A570 File Offset: 0x00018770
		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.kubits) == num;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001A59C File Offset: 0x0001879C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Support(KeyUsages.digitalSignature))
			{
				stringBuilder.Append("Digital Signature");
			}
			if (this.Support(KeyUsages.nonRepudiation))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Non-Repudiation");
			}
			if (this.Support(KeyUsages.keyEncipherment))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Key Encipherment");
			}
			if (this.Support(KeyUsages.dataEncipherment))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Data Encipherment");
			}
			if (this.Support(KeyUsages.keyAgreement))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Key Agreement");
			}
			if (this.Support(KeyUsages.keyCertSign))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Certificate Signing");
			}
			if (this.Support(KeyUsages.cRLSign))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("CRL Signing");
			}
			if (this.Support(KeyUsages.encipherOnly))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Encipher Only ");
			}
			if (this.Support(KeyUsages.decipherOnly))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Decipher Only");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(this.kubits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040001E3 RID: 483
		private int kubits;
	}
}
