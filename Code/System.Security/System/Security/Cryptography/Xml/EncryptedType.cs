using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which the classes <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> and <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> derive.</summary>
	// Token: 0x0200003C RID: 60
	public abstract class EncryptedType
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		// Token: 0x0600016D RID: 365 RVA: 0x00007D5C File Offset: 0x00005F5C
		protected EncryptedType()
		{
			this.cipherData = new CipherData();
			this.encryptionProperties = new EncryptionPropertyCollection();
			this.keyInfo = new KeyInfo();
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> value for an instance of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedType.CipherData" /> property was set to null.</exception>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007D88 File Offset: 0x00005F88
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00007D90 File Offset: 0x00005F90
		public virtual CipherData CipherData
		{
			get
			{
				return this.cipherData;
			}
			set
			{
				this.cipherData = value;
			}
		}

		/// <summary>Gets or sets the Encoding attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the encoding of the encrypted data.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007D9C File Offset: 0x00005F9C
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public virtual string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the &lt;EncryptionMethod&gt; element for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that represents the &lt;EncryptionMethod&gt; element.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007DB0 File Offset: 0x00005FB0
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public virtual EncryptionMethod EncryptionMethod
		{
			get
			{
				return this.encryptionMethod;
			}
			set
			{
				this.encryptionMethod = value;
			}
		}

		/// <summary>Gets or sets the &lt;EncryptionProperties&gt; element in XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public virtual EncryptionPropertyCollection EncryptionProperties
		{
			get
			{
				return this.encryptionProperties;
			}
		}

		/// <summary>Gets or sets the Id attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string of the Id attribute of the &lt;EncryptedType&gt; element.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007DCC File Offset: 0x00005FCC
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public virtual string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets of sets the &lt;KeyInfo&gt; element in XML encryption.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007DE0 File Offset: 0x00005FE0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public KeyInfo KeyInfo
		{
			get
			{
				return this.keyInfo;
			}
			set
			{
				this.keyInfo = value;
			}
		}

		/// <summary>Gets or sets the MimeType attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the media type of the encrypted data.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007DF4 File Offset: 0x00005FF4
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00007DFC File Offset: 0x00005FFC
		public virtual string MimeType
		{
			get
			{
				return this.mimeType;
			}
			set
			{
				this.mimeType = value;
			}
		}

		/// <summary>Gets or sets the Type attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the text form of the encrypted data.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007E08 File Offset: 0x00006008
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00007E10 File Offset: 0x00006010
		public virtual string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Adds an &lt;EncryptionProperty&gt; child element to the &lt;EncryptedProperties&gt; element in the current <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object in XML encryption.</summary>
		/// <param name="ep">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</param>
		// Token: 0x0600017D RID: 381 RVA: 0x00007E1C File Offset: 0x0000601C
		public void AddProperty(EncryptionProperty ep)
		{
			this.EncryptionProperties.Add(ep);
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the &lt;EncryptedType&gt; element in XML encryption.</returns>
		// Token: 0x0600017E RID: 382
		public abstract XmlElement GetXml();

		/// <summary>Loads XML information into the &lt;EncryptedType&gt; element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use in the &lt;EncryptedType&gt; element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is null.</exception>
		// Token: 0x0600017F RID: 383
		public abstract void LoadXml(XmlElement value);

		// Token: 0x040000BB RID: 187
		private CipherData cipherData;

		// Token: 0x040000BC RID: 188
		private string encoding;

		// Token: 0x040000BD RID: 189
		private EncryptionMethod encryptionMethod;

		// Token: 0x040000BE RID: 190
		private EncryptionPropertyCollection encryptionProperties;

		// Token: 0x040000BF RID: 191
		private string id;

		// Token: 0x040000C0 RID: 192
		private KeyInfo keyInfo;

		// Token: 0x040000C1 RID: 193
		private string mimeType;

		// Token: 0x040000C2 RID: 194
		private string type;
	}
}
