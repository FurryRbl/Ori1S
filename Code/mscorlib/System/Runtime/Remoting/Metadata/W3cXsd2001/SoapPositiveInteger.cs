using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD positiveInteger type.</summary>
	// Token: 0x020004DE RID: 1246
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> class.</summary>
		// Token: 0x0600321D RID: 12829 RVA: 0x000A2DFC File Offset: 0x000A0FFC
		public SoapPositiveInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1. </exception>
		// Token: 0x0600321E RID: 12830 RVA: 0x000A2E04 File Offset: 0x000A1004
		public SoapPositiveInteger(decimal value)
		{
			if (value <= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> indicating the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1. </exception>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000A2E3C File Offset: 0x000A103C
		// (set) Token: 0x06003220 RID: 12832 RVA: 0x000A2E44 File Offset: 0x000A1044
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
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000A2E50 File Offset: 0x000A1050
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003222 RID: 12834 RVA: 0x000A2E58 File Offset: 0x000A1058
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object that is obtained from <paramref name="value" />. </returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003223 RID: 12835 RVA: 0x000A2E60 File Offset: 0x000A1060
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" />.</returns>
		// Token: 0x06003224 RID: 12836 RVA: 0x000A2E70 File Offset: 0x000A1070
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400150E RID: 5390
		private decimal _value;
	}
}
