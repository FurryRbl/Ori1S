using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML language type.</summary>
	// Token: 0x020004E4 RID: 1252
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class.</summary>
		// Token: 0x0600324F RID: 12879 RVA: 0x000A34F0 File Offset: 0x000A16F0
		public SoapLanguage()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class with the language identifier value of language attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the language identifier value of a language attribute. </param>
		// Token: 0x06003250 RID: 12880 RVA: 0x000A34F8 File Offset: 0x000A16F8
		public SoapLanguage(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets the language identifier of a language attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the language identifier of a language attribute.</returns>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000A350C File Offset: 0x000A170C
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x000A3514 File Offset: 0x000A1714
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
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000A3520 File Offset: 0x000A1720
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003254 RID: 12884 RVA: 0x000A3528 File Offset: 0x000A1728
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003255 RID: 12885 RVA: 0x000A3530 File Offset: 0x000A1730
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" />.</returns>
		// Token: 0x06003256 RID: 12886 RVA: 0x000A3538 File Offset: 0x000A1738
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001515 RID: 5397
		private string _value;
	}
}
