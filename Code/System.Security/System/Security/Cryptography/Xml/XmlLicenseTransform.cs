using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the license transform algorithm used to normalize XrML licenses for signatures.</summary>
	// Token: 0x02000064 RID: 100
	public class XmlLicenseTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> class. </summary>
		// Token: 0x06000325 RID: 805 RVA: 0x0000E754 File Offset: 0x0000C954
		public XmlLicenseTransform()
		{
			base.Algorithm = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
		}

		/// <summary>Gets or sets the decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000E768 File Offset: 0x0000C968
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000E770 File Offset: 0x0000C970
		public IRelDecryptor Decryptor
		{
			get
			{
				return this._decryptor;
			}
			set
			{
				this._decryptor = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; you can pass only objects of one of these types to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000E77C File Offset: 0x0000C97C
		public override Type[] InputTypes
		{
			get
			{
				if (this.inputTypes == null)
				{
					this.inputTypes = new Type[]
					{
						typeof(XmlDocument)
					};
				}
				return this.inputTypes;
			}
		}

		/// <summary>Gets an array of types that are valid outputs from the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlLicenseTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
		public override Type[] OutputTypes
		{
			get
			{
				if (this.outputTypes == null)
				{
					this.outputTypes = new Type[]
					{
						typeof(XmlDocument)
					};
				}
				return this.outputTypes;
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x0600032A RID: 810 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		[MonoTODO]
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x0600032B RID: 811 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		[MonoTODO]
		public override object GetOutput()
		{
			return null;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlDocument" /> is the only valid type for this parameter.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.</exception>
		// Token: 0x0600032C RID: 812 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlDocument))
			{
				throw new ArgumentException("type");
			}
			return this.GetOutput();
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that encapsulates the transform to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object. </param>
		// Token: 0x0600032D RID: 813 RVA: 0x0000E818 File Offset: 0x0000CA18
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object. The type of the input object must be <see cref="T:System.Xml.XmlDocument" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The context was not set before this transform was invoked.-or-The &lt;issuer&gt; element was not set before this transform was invoked.-or-The &lt;license&gt; element was not set before this transform was invoked.-or-The <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.Decryptor" /> property was not set before this transform was invoked.</exception>
		// Token: 0x0600032E RID: 814 RVA: 0x0000E81C File Offset: 0x0000CA1C
		[MonoTODO]
		public override void LoadInput(object obj)
		{
			if (obj != typeof(XmlDocument))
			{
				throw new ArgumentException("obj");
			}
			if (this._decryptor == null)
			{
				throw new CryptographicException(Locale.GetText("missing decryptor"));
			}
		}

		// Token: 0x0400016D RID: 365
		private IRelDecryptor _decryptor;

		// Token: 0x0400016E RID: 366
		private Type[] inputTypes;

		// Token: 0x0400016F RID: 367
		private Type[] outputTypes;
	}
}
