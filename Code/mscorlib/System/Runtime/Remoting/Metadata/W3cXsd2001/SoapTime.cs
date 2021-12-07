using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD time type. </summary>
	// Token: 0x020004E6 RID: 1254
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapTime : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> class.</summary>
		// Token: 0x0600325F RID: 12895 RVA: 0x000A3590 File Offset: 0x000A1790
		public SoapTime()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x06003260 RID: 12896 RVA: 0x000A3598 File Offset: 0x000A1798
		public SoapTime(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000A3690 File Offset: 0x000A1890
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x000A3698 File Offset: 0x000A1898
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
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000A36A4 File Offset: 0x000A18A4
		public static string XsdType
		{
			get
			{
				return "time";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003265 RID: 12901 RVA: 0x000A36AC File Offset: 0x000A18AC
		public string GetXsdType()
		{
			return SoapTime.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x06003266 RID: 12902 RVA: 0x000A36B4 File Offset: 0x000A18B4
		public static SoapTime Parse(string value)
		{
			return new SoapTime(DateTime.ParseExact(value, SoapTime._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime.Value" /> in the format "HH:mm:ss.fffzzz".</returns>
		// Token: 0x06003267 RID: 12903 RVA: 0x000A36C8 File Offset: 0x000A18C8
		public override string ToString()
		{
			return this._value.ToString("HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x04001517 RID: 5399
		private static readonly string[] _datetimeFormats = new string[]
		{
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:sszzz",
			"HH:mm:ss.fzzz",
			"HH:mm:ss.ffzzz",
			"HH:mm:ss.fffzzz",
			"HH:mm:ss.ffffzzz",
			"HH:mm:ss.fffffzzz",
			"HH:mm:ss.ffffffzzz",
			"HH:mm:ss.fffffffzzz",
			"HH:mm:ssZ",
			"HH:mm:ss.fZ",
			"HH:mm:ss.ffZ",
			"HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ",
			"HH:mm:ss.fffffZ",
			"HH:mm:ss.ffffffZ",
			"HH:mm:ss.fffffffZ"
		};

		// Token: 0x04001518 RID: 5400
		private DateTime _value;
	}
}
