using System;

namespace Mono.Security.X509
{
	// Token: 0x020000C3 RID: 195
	internal class SafeBag
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002EE3C File Offset: 0x0002D03C
		public SafeBag(string bagOID, ASN1 asn1)
		{
			this._bagOID = bagOID;
			this._asn1 = asn1;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002EE54 File Offset: 0x0002D054
		public string BagOID
		{
			get
			{
				return this._bagOID;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002EE5C File Offset: 0x0002D05C
		public ASN1 ASN1
		{
			get
			{
				return this._asn1;
			}
		}

		// Token: 0x040002A6 RID: 678
		private string _bagOID;

		// Token: 0x040002A7 RID: 679
		private ASN1 _asn1;
	}
}
