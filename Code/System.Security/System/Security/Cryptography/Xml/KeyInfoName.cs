using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents a &lt;KeyName&gt; subelement of an XMLDSIG or XML Encryption &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000045 RID: 69
	public class KeyInfoName : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class.</summary>
		// Token: 0x060001E7 RID: 487 RVA: 0x0000948C File Offset: 0x0000768C
		public KeyInfoName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class by specifying the string identifier that is the value of the &lt;KeyName&gt; element.</summary>
		/// <param name="keyName">The string identifier that is the value of the &lt;KeyName&gt; element.</param>
		// Token: 0x060001E8 RID: 488 RVA: 0x00009494 File Offset: 0x00007694
		public KeyInfoName(string keyName)
		{
			this.name = keyName;
		}

		/// <summary>Gets or sets the string identifier contained within a &lt;KeyName&gt; element.</summary>
		/// <returns>The string identifier that is the value of the &lt;KeyName&gt; element.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000094A4 File Offset: 0x000076A4
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000094AC File Offset: 0x000076AC
		public string Value
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001EB RID: 491 RVA: 0x000094B8 File Offset: 0x000076B8
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("KeyName", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.InnerText = this.name;
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060001EC RID: 492 RVA: 0x000094EC File Offset: 0x000076EC
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (value.LocalName != "KeyName" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				this.name = string.Empty;
			}
			else
			{
				this.name = value.InnerText;
			}
		}

		// Token: 0x040000EA RID: 234
		private string name;
	}
}
