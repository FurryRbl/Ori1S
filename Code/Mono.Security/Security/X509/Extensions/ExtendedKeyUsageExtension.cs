using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200006A RID: 106
	public class ExtendedKeyUsageExtension : X509Extension
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x000193C0 File Offset: 0x000175C0
		public ExtendedKeyUsageExtension()
		{
			this.extnOid = "2.5.29.37";
			this.keyPurpose = new ArrayList();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000193E0 File Offset: 0x000175E0
		public ExtendedKeyUsageExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000193EC File Offset: 0x000175EC
		public ExtendedKeyUsageExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000193F8 File Offset: 0x000175F8
		protected override void Decode()
		{
			this.keyPurpose = new ArrayList();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid ExtendedKeyUsage extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.keyPurpose.Add(ASN1Convert.ToOid(asn[i]));
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00019468 File Offset: 0x00017668
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			foreach (object obj in this.keyPurpose)
			{
				string oid = (string)obj;
				asn.Add(ASN1Convert.FromOid(oid));
			}
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00019500 File Offset: 0x00017700
		public ArrayList KeyPurpose
		{
			get
			{
				return this.keyPurpose;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00019508 File Offset: 0x00017708
		public override string Name
		{
			get
			{
				return "Extended Key Usage";
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00019510 File Offset: 0x00017710
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.keyPurpose)
			{
				string text = (string)obj;
				string text2 = text;
				if (text2 == null)
				{
					goto IL_12E;
				}
				if (ExtendedKeyUsageExtension.<>f__switch$map14 == null)
				{
					ExtendedKeyUsageExtension.<>f__switch$map14 = new Dictionary<string, int>(6)
					{
						{
							"1.3.6.1.5.5.7.3.1",
							0
						},
						{
							"1.3.6.1.5.5.7.3.2",
							1
						},
						{
							"1.3.6.1.5.5.7.3.3",
							2
						},
						{
							"1.3.6.1.5.5.7.3.4",
							3
						},
						{
							"1.3.6.1.5.5.7.3.8",
							4
						},
						{
							"1.3.6.1.5.5.7.3.9",
							5
						}
					};
				}
				int num;
				if (!ExtendedKeyUsageExtension.<>f__switch$map14.TryGetValue(text2, out num))
				{
					goto IL_12E;
				}
				switch (num)
				{
				case 0:
					stringBuilder.Append("Server Authentication");
					break;
				case 1:
					stringBuilder.Append("Client Authentication");
					break;
				case 2:
					stringBuilder.Append("Code Signing");
					break;
				case 3:
					stringBuilder.Append("Email Protection");
					break;
				case 4:
					stringBuilder.Append("Time Stamping");
					break;
				case 5:
					stringBuilder.Append("OCSP Signing");
					break;
				default:
					goto IL_12E;
				}
				IL_13F:
				stringBuilder.AppendFormat(" ({0}){1}", text, Environment.NewLine);
				continue;
				IL_12E:
				stringBuilder.Append("unknown");
				goto IL_13F;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001CC RID: 460
		private ArrayList keyPurpose;
	}
}
