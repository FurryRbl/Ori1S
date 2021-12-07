using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NMTOKENS attribute.</summary>
	// Token: 0x020004E5 RID: 1253
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class.</summary>
		// Token: 0x06003257 RID: 12887 RVA: 0x000A3540 File Offset: 0x000A1740
		public SoapNmtokens()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class with an XML NMTOKENS attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML NMTOKENS attribute. </param>
		// Token: 0x06003258 RID: 12888 RVA: 0x000A3548 File Offset: 0x000A1748
		public SoapNmtokens(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML NMTOKENS attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NMTOKENS attribute.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000A355C File Offset: 0x000A175C
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000A3564 File Offset: 0x000A1764
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000A3570 File Offset: 0x000A1770
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600325C RID: 12892 RVA: 0x000A3578 File Offset: 0x000A1778
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x0600325D RID: 12893 RVA: 0x000A3580 File Offset: 0x000A1780
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" />.</returns>
		// Token: 0x0600325E RID: 12894 RVA: 0x000A3588 File Offset: 0x000A1788
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001516 RID: 5398
		private string _value;
	}
}
