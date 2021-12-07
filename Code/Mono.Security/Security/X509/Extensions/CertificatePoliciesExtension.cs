using System;
using System.Collections;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000069 RID: 105
	public class CertificatePoliciesExtension : X509Extension
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00019234 File Offset: 0x00017434
		public CertificatePoliciesExtension()
		{
			this.extnOid = "2.5.29.32";
			this.policies = new Hashtable();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00019254 File Offset: 0x00017454
		public CertificatePoliciesExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00019260 File Offset: 0x00017460
		public CertificatePoliciesExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001926C File Offset: 0x0001746C
		protected override void Decode()
		{
			this.policies = new Hashtable();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid CertificatePolicies extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.policies.Add(ASN1Convert.ToOid(asn[i][0]), null);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000192E4 File Offset: 0x000174E4
		public override string Name
		{
			get
			{
				return "Certificate Policies";
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000192EC File Offset: 0x000174EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (object obj in this.policies)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]Certificate Policy:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tPolicyIdentifier=");
				stringBuilder.Append((string)dictionaryEntry.Key);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001CB RID: 459
		private Hashtable policies;
	}
}
