using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gYear type. </summary>
	// Token: 0x020004D0 RID: 1232
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYear : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class.</summary>
		// Token: 0x060031A8 RID: 12712 RVA: 0x000A25D0 File Offset: 0x000A07D0
		public SoapYear()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x060031A9 RID: 12713 RVA: 0x000A25D8 File Offset: 0x000A07D8
		public SoapYear(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive. </param>
		// Token: 0x060031AA RID: 12714 RVA: 0x000A25E8 File Offset: 0x000A07E8
		public SoapYear(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> is positive or negative.</returns>
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000A2640 File Offset: 0x000A0840
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x000A2648 File Offset: 0x000A0848
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000A2654 File Offset: 0x000A0854
		// (set) Token: 0x060031AF RID: 12719 RVA: 0x000A265C File Offset: 0x000A085C
		public DateTime Value
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
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000A2668 File Offset: 0x000A0868
		public static string XsdType
		{
			get
			{
				return "gYear";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031B1 RID: 12721 RVA: 0x000A2670 File Offset: 0x000A0870
		public string GetXsdType()
		{
			return SoapYear.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x060031B2 RID: 12722 RVA: 0x000A2678 File Offset: 0x000A0878
		public static SoapYear Parse(string value)
		{
			DateTime value2 = DateTime.ParseExact(value, SoapYear._datetimeFormats, null, DateTimeStyles.None);
			SoapYear soapYear = new SoapYear(value2);
			if (value.StartsWith("-"))
			{
				soapYear.Sign = -1;
			}
			else
			{
				soapYear.Sign = 0;
			}
			return soapYear;
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> in the format "yyyy" or "-yyyy" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Sign" /> is negative.</returns>
		// Token: 0x060031B3 RID: 12723 RVA: 0x000A26C0 File Offset: 0x000A08C0
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x040014FA RID: 5370
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy",
			"'+'yyyy",
			"'-'yyyy",
			"yyyyzzz",
			"'+'yyyyzzz",
			"'-'yyyyzzz"
		};

		// Token: 0x040014FB RID: 5371
		private int _sign;

		// Token: 0x040014FC RID: 5372
		private DateTime _value;
	}
}
