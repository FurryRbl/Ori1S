using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD hexBinary type.</summary>
	// Token: 0x020004CD RID: 1229
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapHexBinary : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> class.</summary>
		// Token: 0x0600318C RID: 12684 RVA: 0x000A22D0 File Offset: 0x000A04D0
		public SoapHexBinary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> class.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a hexadecimal number. </param>
		// Token: 0x0600318D RID: 12685 RVA: 0x000A22E4 File Offset: 0x000A04E4
		public SoapHexBinary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the hexadecimal representation of a number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the hexadecimal representation of a number.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000A2300 File Offset: 0x000A0500
		// (set) Token: 0x0600318F RID: 12687 RVA: 0x000A2308 File Offset: 0x000A0508
		public byte[] Value
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
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000A2314 File Offset: 0x000A0514
		public static string XsdType
		{
			get
			{
				return "hexBinary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003191 RID: 12689 RVA: 0x000A231C File Offset: 0x000A051C
		public string GetXsdType()
		{
			return SoapHexBinary.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003192 RID: 12690 RVA: 0x000A2324 File Offset: 0x000A0524
		public static SoapHexBinary Parse(string value)
		{
			byte[] value2 = SoapHexBinary.FromBinHexString(value);
			return new SoapHexBinary(value2);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000A2340 File Offset: 0x000A0540
		internal static byte[] FromBinHexString(string value)
		{
			char[] array = value.ToCharArray();
			byte[] array2 = new byte[array.Length / 2 + array.Length % 2];
			int num = array.Length;
			if (num % 2 != 0)
			{
				throw SoapHexBinary.CreateInvalidValueException(value);
			}
			int num2 = 0;
			for (int i = 0; i < num - 1; i += 2)
			{
				array2[num2] = SoapHexBinary.FromHex(array[i], value);
				byte[] array3 = array2;
				int num3 = num2;
				array3[num3] = (byte)(array3[num3] << 4);
				byte[] array4 = array2;
				int num4 = num2;
				array4[num4] += SoapHexBinary.FromHex(array[i + 1], value);
				num2++;
			}
			return array2;
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000A23CC File Offset: 0x000A05CC
		private static byte FromHex(char hexDigit, string value)
		{
			byte result;
			try
			{
				result = byte.Parse(hexDigit.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw SoapHexBinary.CreateInvalidValueException(value);
			}
			return result;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000A2424 File Offset: 0x000A0624
		private static Exception CreateInvalidValueException(string value)
		{
			return new RemotingException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for xsd:{1}.", new object[]
			{
				value,
				SoapHexBinary.XsdType
			}));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" />.</returns>
		// Token: 0x06003196 RID: 12694 RVA: 0x000A2458 File Offset: 0x000A0658
		public override string ToString()
		{
			this.sb.Length = 0;
			foreach (byte b in this._value)
			{
				this.sb.Append(b.ToString("X2"));
			}
			return this.sb.ToString();
		}

		// Token: 0x040014F5 RID: 5365
		private byte[] _value;

		// Token: 0x040014F6 RID: 5366
		private StringBuilder sb = new StringBuilder();
	}
}
