using System;

namespace Mono.Security.X509
{
	// Token: 0x0200003D RID: 61
	internal class SafeBag
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00010538 File Offset: 0x0000E738
		public SafeBag(string bagOID, ASN1 asn1)
		{
			this._bagOID = bagOID;
			this._asn1 = asn1;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00010550 File Offset: 0x0000E750
		public string BagOID
		{
			get
			{
				return this._bagOID;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00010558 File Offset: 0x0000E758
		public ASN1 ASN1
		{
			get
			{
				return this._asn1;
			}
		}

		// Token: 0x0400011A RID: 282
		private string _bagOID;

		// Token: 0x0400011B RID: 283
		private ASN1 _asn1;
	}
}
