using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x020000E8 RID: 232
	internal class BasicConstraintsExtension : X509Extension
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00036DB4 File Offset: 0x00034FB4
		public BasicConstraintsExtension()
		{
			this.extnOid = "2.5.29.19";
			this.pathLenConstraint = -1;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00036DD0 File Offset: 0x00034FD0
		public BasicConstraintsExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00036DDC File Offset: 0x00034FDC
		public BasicConstraintsExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00036DE8 File Offset: 0x00034FE8
		protected override void Decode()
		{
			this.cA = false;
			this.pathLenConstraint = -1;
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid BasicConstraints extension");
			}
			int num = 0;
			ASN1 asn2 = asn[num++];
			if (asn2 != null && asn2.Tag == 1)
			{
				this.cA = (asn2.Value[0] == byte.MaxValue);
				asn2 = asn[num++];
			}
			if (asn2 != null && asn2.Tag == 2)
			{
				this.pathLenConstraint = ASN1Convert.ToInt32(asn2);
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00036E8C File Offset: 0x0003508C
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			if (this.cA)
			{
				asn.Add(new ASN1(1, new byte[]
				{
					byte.MaxValue
				}));
			}
			if (this.cA && this.pathLenConstraint >= 0)
			{
				asn.Add(ASN1Convert.FromInt32(this.pathLenConstraint));
			}
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00036F0C File Offset: 0x0003510C
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00036F14 File Offset: 0x00035114
		public bool CertificateAuthority
		{
			get
			{
				return this.cA;
			}
			set
			{
				this.cA = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00036F20 File Offset: 0x00035120
		public override string Name
		{
			get
			{
				return "Basic Constraints";
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00036F28 File Offset: 0x00035128
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00036F30 File Offset: 0x00035130
		public int PathLenConstraint
		{
			get
			{
				return this.pathLenConstraint;
			}
			set
			{
				if (value < -1)
				{
					string text = Locale.GetText("PathLenConstraint must be positive or -1 for none ({0}).", new object[]
					{
						value
					});
					throw new ArgumentOutOfRangeException(text);
				}
				this.pathLenConstraint = value;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00036F6C File Offset: 0x0003516C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Subject Type=");
			stringBuilder.Append((!this.cA) ? "End Entity" : "CA");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("Path Length Constraint=");
			if (this.pathLenConstraint == -1)
			{
				stringBuilder.Append("None");
			}
			else
			{
				stringBuilder.Append(this.pathLenConstraint.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04000338 RID: 824
		public const int NoPathLengthConstraint = -1;

		// Token: 0x04000339 RID: 825
		private bool cA;

		// Token: 0x0400033A RID: 826
		private int pathLenConstraint;
	}
}
