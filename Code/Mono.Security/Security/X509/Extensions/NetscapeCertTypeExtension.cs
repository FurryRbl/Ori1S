using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200006F RID: 111
	public class NetscapeCertTypeExtension : X509Extension
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0001A798 File Offset: 0x00018998
		public NetscapeCertTypeExtension()
		{
			this.extnOid = "2.16.840.1.113730.1.1";
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001A7AC File Offset: 0x000189AC
		public NetscapeCertTypeExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001A7B8 File Offset: 0x000189B8
		public NetscapeCertTypeExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001A7C4 File Offset: 0x000189C4
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 3)
			{
				throw new ArgumentException("Invalid NetscapeCertType extension");
			}
			int i = 1;
			while (i < asn.Value.Length)
			{
				this.ctbits = (this.ctbits << 8) + (int)asn.Value[i++];
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001A82C File Offset: 0x00018A2C
		public override string Name
		{
			get
			{
				return "NetscapeCertType";
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001A834 File Offset: 0x00018A34
		public bool Support(NetscapeCertTypeExtension.CertTypes usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.ctbits) == num;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001A860 File Offset: 0x00018A60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslClient))
			{
				stringBuilder.Append("SSL Client Authentication");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslServer))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL Server Authentication");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.Smime))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.ObjectSigning))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL CA");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SmimeCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME CA");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.ObjectSigningCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing CA");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(this.ctbits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040001E4 RID: 484
		private int ctbits;

		// Token: 0x02000070 RID: 112
		[Flags]
		public enum CertTypes
		{
			// Token: 0x040001E6 RID: 486
			SslClient = 128,
			// Token: 0x040001E7 RID: 487
			SslServer = 64,
			// Token: 0x040001E8 RID: 488
			Smime = 32,
			// Token: 0x040001E9 RID: 489
			ObjectSigning = 16,
			// Token: 0x040001EA RID: 490
			SslCA = 4,
			// Token: 0x040001EB RID: 491
			SmimeCA = 2,
			// Token: 0x040001EC RID: 492
			ObjectSigningCA = 1
		}
	}
}
