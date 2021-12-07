using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X509 extension.</summary>
	// Token: 0x0200044E RID: 1102
	public class X509Extension : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		// Token: 0x060027C7 RID: 10183 RVA: 0x0007D570 File Offset: 0x0007B770
		protected X509Extension()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="encodedExtension">The encoded data to be used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		// Token: 0x060027C8 RID: 10184 RVA: 0x0007D578 File Offset: 0x0007B778
		public X509Extension(AsnEncodedData encodedExtension, bool critical)
		{
			if (encodedExtension.Oid == null)
			{
				throw new ArgumentNullException("encodedExtension.Oid");
			}
			base.Oid = encodedExtension.Oid;
			base.RawData = encodedExtension.RawData;
			this._critical = critical;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">The object identifier used to identify the extension.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oid" /> is an empty string ("").</exception>
		// Token: 0x060027C9 RID: 10185 RVA: 0x0007D5C0 File Offset: 0x0007B7C0
		public X509Extension(Oid oid, byte[] rawData, bool critical)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			base.Oid = oid;
			base.RawData = rawData;
			this._critical = critical;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">A string representing the object identifier.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		// Token: 0x060027CA RID: 10186 RVA: 0x0007D5FC File Offset: 0x0007B7FC
		public X509Extension(string oid, byte[] rawData, bool critical) : base(oid, rawData)
		{
			this._critical = critical;
		}

		/// <summary>Gets a Boolean value indicating whether the extension is critical.</summary>
		/// <returns>true if the extension is critical; otherwise, false.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x0007D610 File Offset: 0x0007B810
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x0007D618 File Offset: 0x0007B818
		public bool Critical
		{
			get
			{
				return this._critical;
			}
			set
			{
				this._critical = value;
			}
		}

		/// <summary>Copies the extension properties of the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodedData" /> does not have a valid X.509 extension.</exception>
		// Token: 0x060027CD RID: 10189 RVA: 0x0007D624 File Offset: 0x0007B824
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(Locale.GetText("Expected a X509Extension instance."));
			}
			base.CopyFrom(asnEncodedData);
			this._critical = x509Extension.Critical;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0007D674 File Offset: 0x0007B874
		internal string FormatUnkownData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(data[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001865 RID: 6245
		private bool _critical;
	}
}
