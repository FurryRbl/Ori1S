using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000071 RID: 113
	public class PrivateKeyUsagePeriodExtension : X509Extension
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public PrivateKeyUsagePeriodExtension()
		{
			this.extnOid = "2.5.29.16";
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001AA0C File Offset: 0x00018C0C
		public PrivateKeyUsagePeriodExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001AA18 File Offset: 0x00018C18
		public PrivateKeyUsagePeriodExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001AA24 File Offset: 0x00018C24
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				byte tag = asn[i].Tag;
				if (tag != 128)
				{
					if (tag != 129)
					{
						throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
					}
					this.notAfter = ASN1Convert.ToDateTime(asn[i]);
				}
				else
				{
					this.notBefore = ASN1Convert.ToDateTime(asn[i]);
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		public override string Name
		{
			get
			{
				return "Private Key Usage Period";
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001AADC File Offset: 0x00018CDC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before: ");
				stringBuilder.Append(this.notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After: ");
				stringBuilder.Append(this.notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001ED RID: 493
		private DateTime notBefore;

		// Token: 0x040001EE RID: 494
		private DateTime notAfter;
	}
}
