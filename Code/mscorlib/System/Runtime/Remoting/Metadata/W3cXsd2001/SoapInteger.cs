using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD integer type.</summary>
	// Token: 0x020004E0 RID: 1248
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class.</summary>
		// Token: 0x06003229 RID: 12841 RVA: 0x000A32C8 File Offset: 0x000A14C8
		public SoapInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		// Token: 0x0600322A RID: 12842 RVA: 0x000A32D0 File Offset: 0x000A14D0
		public SoapInteger(decimal value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000A32E0 File Offset: 0x000A14E0
		// (set) Token: 0x0600322C RID: 12844 RVA: 0x000A32E8 File Offset: 0x000A14E8
		public decimal Value
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
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000A32F4 File Offset: 0x000A14F4
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600322E RID: 12846 RVA: 0x000A32FC File Offset: 0x000A14FC
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x0600322F RID: 12847 RVA: 0x000A3304 File Offset: 0x000A1504
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" />.</returns>
		// Token: 0x06003230 RID: 12848 RVA: 0x000A3314 File Offset: 0x000A1514
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400150F RID: 5391
		private decimal _value;
	}
}
