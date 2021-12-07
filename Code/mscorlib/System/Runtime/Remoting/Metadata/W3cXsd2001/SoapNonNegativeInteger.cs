using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD nonNegativeInteger type.</summary>
	// Token: 0x020004CA RID: 1226
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> class.</summary>
		// Token: 0x06003174 RID: 12660 RVA: 0x000A21A0 File Offset: 0x000A03A0
		public SoapNonNegativeInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 0. </exception>
		// Token: 0x06003175 RID: 12661 RVA: 0x000A21A8 File Offset: 0x000A03A8
		public SoapNonNegativeInteger(decimal value)
		{
			if (value < 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 0. </exception>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000A21E0 File Offset: 0x000A03E0
		// (set) Token: 0x06003177 RID: 12663 RVA: 0x000A21E8 File Offset: 0x000A03E8
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
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000A21F4 File Offset: 0x000A03F4
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003179 RID: 12665 RVA: 0x000A21FC File Offset: 0x000A03FC
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> object that is obtained from <paramref name="value" />. </returns>
		/// <param name="value">The <see cref="T:System.String" />  to convert. </param>
		// Token: 0x0600317A RID: 12666 RVA: 0x000A2204 File Offset: 0x000A0404
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" />.</returns>
		// Token: 0x0600317B RID: 12667 RVA: 0x000A2214 File Offset: 0x000A0414
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040014F2 RID: 5362
		private decimal _value;
	}
}
