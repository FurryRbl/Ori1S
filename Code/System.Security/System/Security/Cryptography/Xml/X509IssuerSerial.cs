using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;X509IssuerSerial&gt; element of an XML digital signature.</summary>
	// Token: 0x02000054 RID: 84
	public struct X509IssuerSerial
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000CE60 File Offset: 0x0000B060
		internal X509IssuerSerial(string issuer, string serial)
		{
			this._issuerName = issuer;
			this._serialNumber = serial;
		}

		/// <summary>Gets or sets an X.509 certificate issuer's distinguished name.</summary>
		/// <returns>An X.509 certificate issuer's distinguished name.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000CE70 File Offset: 0x0000B070
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000CE78 File Offset: 0x0000B078
		public string IssuerName
		{
			get
			{
				return this._issuerName;
			}
			set
			{
				this._issuerName = value;
			}
		}

		/// <summary>Gets or sets an X.509 certificate issuer's serial number.</summary>
		/// <returns>An X.509 certificate issuer's serial number.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000CE84 File Offset: 0x0000B084
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public string SerialNumber
		{
			get
			{
				return this._serialNumber;
			}
			set
			{
				this._serialNumber = value;
			}
		}

		// Token: 0x04000130 RID: 304
		private string _issuerName;

		// Token: 0x04000131 RID: 305
		private string _serialNumber;
	}
}
