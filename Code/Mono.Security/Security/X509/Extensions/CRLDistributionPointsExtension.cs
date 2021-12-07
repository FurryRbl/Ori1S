using System;
using System.Collections;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000066 RID: 102
	public class CRLDistributionPointsExtension : X509Extension
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00018FC8 File Offset: 0x000171C8
		public CRLDistributionPointsExtension()
		{
			this.extnOid = "2.5.29.31";
			this.dps = new ArrayList();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00018FE8 File Offset: 0x000171E8
		public CRLDistributionPointsExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00018FF4 File Offset: 0x000171F4
		public CRLDistributionPointsExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00019000 File Offset: 0x00017200
		protected override void Decode()
		{
			this.dps = new ArrayList();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid CRLDistributionPoints extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.dps.Add(new CRLDistributionPointsExtension.DP(asn[i]));
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00019070 File Offset: 0x00017270
		public override string Name
		{
			get
			{
				return "CRL Distribution Points";
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00019078 File Offset: 0x00017278
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (object obj in this.dps)
			{
				CRLDistributionPointsExtension.DP dp = (CRLDistributionPointsExtension.DP)obj;
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]CRL Distribution Point");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tDistribution Point Name:");
				stringBuilder.Append("\t\tFull Name:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\t\t\t");
				stringBuilder.Append(dp.DistributionPoint);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001BD RID: 445
		private ArrayList dps;

		// Token: 0x02000067 RID: 103
		internal class DP
		{
			// Token: 0x060003DC RID: 988 RVA: 0x0001916C File Offset: 0x0001736C
			public DP(string dp, CRLDistributionPointsExtension.ReasonFlags reasons, string issuer)
			{
				this.DistributionPoint = dp;
				this.Reasons = reasons;
				this.CRLIssuer = issuer;
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0001918C File Offset: 0x0001738C
			public DP(ASN1 dp)
			{
				for (int i = 0; i < dp.Count; i++)
				{
					ASN1 asn = dp[i];
					switch (asn.Tag)
					{
					case 160:
						for (int j = 0; j < asn.Count; j++)
						{
							ASN1 asn2 = asn[j];
							if (asn2.Tag == 160)
							{
								this.DistributionPoint = new GeneralNames(asn2).ToString();
							}
						}
						break;
					}
				}
			}

			// Token: 0x040001BE RID: 446
			public string DistributionPoint;

			// Token: 0x040001BF RID: 447
			public CRLDistributionPointsExtension.ReasonFlags Reasons;

			// Token: 0x040001C0 RID: 448
			public string CRLIssuer;
		}

		// Token: 0x02000068 RID: 104
		[Flags]
		public enum ReasonFlags
		{
			// Token: 0x040001C2 RID: 450
			Unused = 0,
			// Token: 0x040001C3 RID: 451
			KeyCompromise = 1,
			// Token: 0x040001C4 RID: 452
			CACompromise = 2,
			// Token: 0x040001C5 RID: 453
			AffiliationChanged = 3,
			// Token: 0x040001C6 RID: 454
			Superseded = 4,
			// Token: 0x040001C7 RID: 455
			CessationOfOperation = 5,
			// Token: 0x040001C8 RID: 456
			CertificateHold = 6,
			// Token: 0x040001C9 RID: 457
			PrivilegeWithdrawn = 7,
			// Token: 0x040001CA RID: 458
			AACompromise = 8
		}
	}
}
