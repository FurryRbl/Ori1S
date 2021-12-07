using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML IDREFS attribute.</summary>
	// Token: 0x020004D8 RID: 1240
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class.</summary>
		// Token: 0x060031EC RID: 12780 RVA: 0x000A2BA4 File Offset: 0x000A0DA4
		public SoapIdref()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class with an XML IDREF attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML IDREF attribute. </param>
		// Token: 0x060031ED RID: 12781 RVA: 0x000A2BAC File Offset: 0x000A0DAC
		public SoapIdref(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML IDREF attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML IDREF attribute.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000A2BC0 File Offset: 0x000A0DC0
		// (set) Token: 0x060031EF RID: 12783 RVA: 0x000A2BC8 File Offset: 0x000A0DC8
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
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000A2BD4 File Offset: 0x000A0DD4
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031F1 RID: 12785 RVA: 0x000A2BDC File Offset: 0x000A0DDC
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060031F2 RID: 12786 RVA: 0x000A2BE4 File Offset: 0x000A0DE4
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" />.</returns>
		// Token: 0x060031F3 RID: 12787 RVA: 0x000A2BEC File Offset: 0x000A0DEC
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001507 RID: 5383
		private string _value;
	}
}
