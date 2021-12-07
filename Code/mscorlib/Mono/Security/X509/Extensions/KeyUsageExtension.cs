using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x020000EA RID: 234
	internal class KeyUsageExtension : X509Extension
	{
		// Token: 0x06000C05 RID: 3077 RVA: 0x0003700C File Offset: 0x0003520C
		public KeyUsageExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00037018 File Offset: 0x00035218
		public KeyUsageExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00037024 File Offset: 0x00035224
		public KeyUsageExtension()
		{
			this.extnOid = "2.5.29.15";
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00037038 File Offset: 0x00035238
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

		// Token: 0x06000C09 RID: 3081 RVA: 0x000370A0 File Offset: 0x000352A0
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

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00037190 File Offset: 0x00035390
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00037198 File Offset: 0x00035398
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000371B0 File Offset: 0x000353B0
		public override string Name
		{
			get
			{
				return "Key Usage";
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000371B8 File Offset: 0x000353B8
		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.kubits) == num;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000371E4 File Offset: 0x000353E4
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

		// Token: 0x04000346 RID: 838
		private int kubits;
	}
}
