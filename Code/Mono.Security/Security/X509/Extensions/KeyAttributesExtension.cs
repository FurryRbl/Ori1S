using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200006C RID: 108
	public class KeyAttributesExtension : X509Extension
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x00019EC8 File Offset: 0x000180C8
		public KeyAttributesExtension()
		{
			this.extnOid = "2.5.29.2";
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00019EDC File Offset: 0x000180DC
		public KeyAttributesExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00019EE8 File Offset: 0x000180E8
		public KeyAttributesExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00019EF4 File Offset: 0x000180F4
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid KeyAttributesExtension extension");
			}
			int num = 0;
			if (num < asn.Count)
			{
				ASN1 asn2 = asn[num];
				if (asn2.Tag == 4)
				{
					num++;
					this.keyId = asn2.Value;
				}
			}
			if (num < asn.Count)
			{
				ASN1 asn3 = asn[num];
				if (asn3.Tag == 3)
				{
					num++;
					int i = 1;
					while (i < asn3.Value.Length)
					{
						this.kubits = (this.kubits << 8) + (int)asn3.Value[i++];
					}
				}
			}
			if (num < asn.Count)
			{
				ASN1 asn4 = asn[num];
				if (asn4.Tag == 48)
				{
					int num2 = 0;
					if (num2 < asn4.Count)
					{
						ASN1 asn5 = asn4[num2];
						if (asn5.Tag == 129)
						{
							num2++;
							this.notBefore = ASN1Convert.ToDateTime(asn5);
						}
					}
					if (num2 < asn4.Count)
					{
						ASN1 asn6 = asn4[num2];
						if (asn6.Tag == 130)
						{
							this.notAfter = ASN1Convert.ToDateTime(asn6);
						}
					}
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001A050 File Offset: 0x00018250
		public byte[] KeyIdentifier
		{
			get
			{
				if (this.keyId == null)
				{
					return null;
				}
				return (byte[])this.keyId.Clone();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001A070 File Offset: 0x00018270
		public override string Name
		{
			get
			{
				return "Key Attributes";
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001A078 File Offset: 0x00018278
		public DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001A080 File Offset: 0x00018280
		public DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001A088 File Offset: 0x00018288
		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.kubits) == num;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001A0B4 File Offset: 0x000182B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.keyId != null)
			{
				stringBuilder.Append("KeyID=");
				for (int i = 0; i < this.keyId.Length; i++)
				{
					stringBuilder.Append(this.keyId[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
				}
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.kubits != 0)
			{
				stringBuilder.Append("Key Usage=");
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
			}
			if (this.notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before=");
				stringBuilder.Append(this.notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After=");
				stringBuilder.Append(this.notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001D4 RID: 468
		private byte[] keyId;

		// Token: 0x040001D5 RID: 469
		private int kubits;

		// Token: 0x040001D6 RID: 470
		private DateTime notBefore;

		// Token: 0x040001D7 RID: 471
		private DateTime notAfter;
	}
}
