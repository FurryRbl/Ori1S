using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML Name type.</summary>
	// Token: 0x020004D4 RID: 1236
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class.</summary>
		// Token: 0x060031CB RID: 12747 RVA: 0x000A28B4 File Offset: 0x000A0AB4
		public SoapName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class with an XML Name type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML Name type. </param>
		// Token: 0x060031CC RID: 12748 RVA: 0x000A28BC File Offset: 0x000A0ABC
		public SoapName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML Name type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML Name type.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000A28D0 File Offset: 0x000A0AD0
		// (set) Token: 0x060031CE RID: 12750 RVA: 0x000A28D8 File Offset: 0x000A0AD8
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
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000A28E4 File Offset: 0x000A0AE4
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031D0 RID: 12752 RVA: 0x000A28EC File Offset: 0x000A0AEC
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060031D1 RID: 12753 RVA: 0x000A28F4 File Offset: 0x000A0AF4
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" />.</returns>
		// Token: 0x060031D2 RID: 12754 RVA: 0x000A28FC File Offset: 0x000A0AFC
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001501 RID: 5377
		private string _value;
	}
}
