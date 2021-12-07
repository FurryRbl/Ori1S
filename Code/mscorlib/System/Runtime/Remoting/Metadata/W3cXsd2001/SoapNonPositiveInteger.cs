using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD nonPositiveInteger type.</summary>
	// Token: 0x020004CF RID: 1231
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonPositiveInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> class.</summary>
		// Token: 0x060031A0 RID: 12704 RVA: 0x000A254C File Offset: 0x000A074C
		public SoapNonPositiveInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than zero. </exception>
		// Token: 0x060031A1 RID: 12705 RVA: 0x000A2554 File Offset: 0x000A0754
		public SoapNonPositiveInteger(decimal value)
		{
			if (value > 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than zero. </exception>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000A258C File Offset: 0x000A078C
		// (set) Token: 0x060031A3 RID: 12707 RVA: 0x000A2594 File Offset: 0x000A0794
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
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000A25A0 File Offset: 0x000A07A0
		public static string XsdType
		{
			get
			{
				return "nonPositiveInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031A5 RID: 12709 RVA: 0x000A25A8 File Offset: 0x000A07A8
		public string GetXsdType()
		{
			return SoapNonPositiveInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> object that is obtained from <paramref name="value" /></returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060031A6 RID: 12710 RVA: 0x000A25B0 File Offset: 0x000A07B0
		public static SoapNonPositiveInteger Parse(string value)
		{
			return new SoapNonPositiveInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from Value.</returns>
		// Token: 0x060031A7 RID: 12711 RVA: 0x000A25C0 File Offset: 0x000A07C0
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040014F9 RID: 5369
		private decimal _value;
	}
}
