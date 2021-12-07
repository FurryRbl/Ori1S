using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000065 RID: 101
	public class BasicConstraintsExtension : X509Extension
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00018D70 File Offset: 0x00016F70
		public BasicConstraintsExtension()
		{
			this.extnOid = "2.5.29.19";
			this.pathLenConstraint = -1;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00018D8C File Offset: 0x00016F8C
		public BasicConstraintsExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00018D98 File Offset: 0x00016F98
		public BasicConstraintsExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00018DA4 File Offset: 0x00016FA4
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

		// Token: 0x060003CF RID: 975 RVA: 0x00018E48 File Offset: 0x00017048
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00018EC8 File Offset: 0x000170C8
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00018ED0 File Offset: 0x000170D0
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00018EDC File Offset: 0x000170DC
		public override string Name
		{
			get
			{
				return "Basic Constraints";
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00018EE4 File Offset: 0x000170E4
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00018EEC File Offset: 0x000170EC
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

		// Token: 0x060003D5 RID: 981 RVA: 0x00018F28 File Offset: 0x00017128
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

		// Token: 0x040001BA RID: 442
		public const int NoPathLengthConstraint = -1;

		// Token: 0x040001BB RID: 443
		private bool cA;

		// Token: 0x040001BC RID: 444
		private int pathLenConstraint;
	}
}
