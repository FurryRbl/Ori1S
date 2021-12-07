using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD date type.</summary>
	// Token: 0x020004D1 RID: 1233
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class.</summary>
		// Token: 0x060031B4 RID: 12724 RVA: 0x000A2704 File Offset: 0x000A0904
		public SoapDate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x060031B5 RID: 12725 RVA: 0x000A270C File Offset: 0x000A090C
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive. </param>
		// Token: 0x060031B6 RID: 12726 RVA: 0x000A271C File Offset: 0x000A091C
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is positive or negative.</returns>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000A2774 File Offset: 0x000A0974
		// (set) Token: 0x060031B9 RID: 12729 RVA: 0x000A277C File Offset: 0x000A097C
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
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000A2788 File Offset: 0x000A0988
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x000A2790 File Offset: 0x000A0990
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
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000A279C File Offset: 0x000A099C
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031BD RID: 12733 RVA: 0x000A27A4 File Offset: 0x000A09A4
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x060031BE RID: 12734 RVA: 0x000A27AC File Offset: 0x000A09AC
		public static SoapDate Parse(string value)
		{
			DateTime value2 = DateTime.ParseExact(value, SoapDate._datetimeFormats, null, DateTimeStyles.None);
			SoapDate soapDate = new SoapDate(value2);
			if (value.StartsWith("-"))
			{
				soapDate.Sign = -1;
			}
			else
			{
				soapDate.Sign = 0;
			}
			return soapDate;
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> in the format "yyyy-MM-dd" or "'-'yyyy-MM-dd" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Sign" /> is negative.</returns>
		// Token: 0x060031BF RID: 12735 RVA: 0x000A27F4 File Offset: 0x000A09F4
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x040014FD RID: 5373
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy-MM-dd",
			"'+'yyyy-MM-dd",
			"'-'yyyy-MM-dd",
			"yyyy-MM-ddzzz",
			"'+'yyyy-MM-ddzzz",
			"'-'yyyy-MM-ddzzz"
		};

		// Token: 0x040014FE RID: 5374
		private int _sign;

		// Token: 0x040014FF RID: 5375
		private DateTime _value;
	}
}
