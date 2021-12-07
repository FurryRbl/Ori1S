using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML ENTITIES attribute.</summary>
	// Token: 0x020004E3 RID: 1251
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class.</summary>
		// Token: 0x06003247 RID: 12871 RVA: 0x000A34A0 File Offset: 0x000A16A0
		public SoapEntities()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class with an XML ENTITIES attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML ENTITIES attribute. </param>
		// Token: 0x06003248 RID: 12872 RVA: 0x000A34A8 File Offset: 0x000A16A8
		public SoapEntities(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML ENTITIES attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML ENTITIES attribute.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000A34BC File Offset: 0x000A16BC
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x000A34C4 File Offset: 0x000A16C4
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
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000A34D0 File Offset: 0x000A16D0
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600324C RID: 12876 RVA: 0x000A34D8 File Offset: 0x000A16D8
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x0600324D RID: 12877 RVA: 0x000A34E0 File Offset: 0x000A16E0
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" />.</returns>
		// Token: 0x0600324E RID: 12878 RVA: 0x000A34E8 File Offset: 0x000A16E8
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001514 RID: 5396
		private string _value;
	}
}
