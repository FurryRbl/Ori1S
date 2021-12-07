using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML token type.</summary>
	// Token: 0x020004CB RID: 1227
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class.</summary>
		// Token: 0x0600317C RID: 12668 RVA: 0x000A2224 File Offset: 0x000A0424
		public SoapToken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class with an XML token.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML token. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> contains invalid characters (0xD or 0x9).<paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.<paramref name="value" /> contains any spaces. </exception>
		// Token: 0x0600317D RID: 12669 RVA: 0x000A222C File Offset: 0x000A042C
		public SoapToken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML token.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML token.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> contains invalid characters (0xD or 0x9).<paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.<paramref name="value" /> contains any spaces. </exception>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000A2240 File Offset: 0x000A0440
		// (set) Token: 0x0600317F RID: 12671 RVA: 0x000A2248 File Offset: 0x000A0448
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
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000A2254 File Offset: 0x000A0454
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003181 RID: 12673 RVA: 0x000A225C File Offset: 0x000A045C
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003182 RID: 12674 RVA: 0x000A2264 File Offset: 0x000A0464
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" />.</returns>
		// Token: 0x06003183 RID: 12675 RVA: 0x000A226C File Offset: 0x000A046C
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040014F3 RID: 5363
		private string _value;
	}
}
