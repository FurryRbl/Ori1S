using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD negativeInteger type.</summary>
	// Token: 0x020004DD RID: 1245
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> class.</summary>
		// Token: 0x06003215 RID: 12821 RVA: 0x000A2D78 File Offset: 0x000A0F78
		public SoapNegativeInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than -1. </exception>
		// Token: 0x06003216 RID: 12822 RVA: 0x000A2D80 File Offset: 0x000A0F80
		public SoapNegativeInteger(decimal value)
		{
			if (value >= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than -1. </exception>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x000A2DC0 File Offset: 0x000A0FC0
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
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000A2DCC File Offset: 0x000A0FCC
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600321A RID: 12826 RVA: 0x000A2DD4 File Offset: 0x000A0FD4
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> object that is obtained from <paramref name="value" />. </returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x0600321B RID: 12827 RVA: 0x000A2DDC File Offset: 0x000A0FDC
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from Value.</returns>
		// Token: 0x0600321C RID: 12828 RVA: 0x000A2DEC File Offset: 0x000A0FEC
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400150D RID: 5389
		private decimal _value;
	}
}
