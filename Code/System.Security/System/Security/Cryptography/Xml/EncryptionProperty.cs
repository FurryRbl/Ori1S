using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;EncryptionProperty&gt; element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000040 RID: 64
	public sealed class EncryptionProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class. </summary>
		// Token: 0x060001CA RID: 458 RVA: 0x00008E88 File Offset: 0x00007088
		public EncryptionProperty()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class using an <see cref="T:System.Xml.XmlElement" /> object. </summary>
		/// <param name="elementProperty">An <see cref="T:System.Xml.XmlElement" /> object to use for initialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elementProperty" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="elementProperty" /> parameter is not "EncryptionProperty". -or-The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="elementProperty" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x060001CB RID: 459 RVA: 0x00008E90 File Offset: 0x00007090
		public EncryptionProperty(XmlElement elemProp)
		{
			this.LoadXml(elemProp);
		}

		/// <summary>Gets the ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008EA0 File Offset: 0x000070A0
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property was set to null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "EncryptionProperty". -or-The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008EA8 File Offset: 0x000070A8
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00008EB0 File Offset: 0x000070B0
		public XmlElement PropertyElement
		{
			get
			{
				return this.elemProp;
			}
			set
			{
				this.LoadXml(value);
			}
		}

		/// <summary>Gets the target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008EBC File Offset: 0x000070BC
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001D0 RID: 464 RVA: 0x00008EC4 File Offset: 0x000070C4
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008ED4 File Offset: 0x000070D4
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptionProperty", "http://www.w3.org/2001/04/xmlenc#");
			if (this.Id != null)
			{
				xmlElement.SetAttribute("Id", this.Id);
			}
			if (this.Target != null)
			{
				xmlElement.SetAttribute("Target", this.Target);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="value" /> parameter is not "EncryptionProperty". -or-The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="value" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x060001D2 RID: 466 RVA: 0x00008F2C File Offset: 0x0000712C
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed EncryptionProperty element.");
			}
			if (value.HasAttribute("Id"))
			{
				this.id = value.Attributes["Id"].Value;
			}
			if (value.HasAttribute("Target"))
			{
				this.target = value.Attributes["Target"].Value;
			}
		}

		// Token: 0x040000E2 RID: 226
		private XmlElement elemProp;

		// Token: 0x040000E3 RID: 227
		private string id;

		// Token: 0x040000E4 RID: 228
		private string target;
	}
}
