using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;Signature&gt; element of an XML signature.</summary>
	// Token: 0x0200004E RID: 78
	public class Signature
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> class.</summary>
		// Token: 0x06000247 RID: 583 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public Signature()
		{
			this.list = new ArrayList();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A7EC File Offset: 0x000089EC
		static Signature()
		{
			Signature.dsigNsmgr.AddNamespace("xd", "http://www.w3.org/2000/09/xmldsig#");
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />. The default is null.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A814 File Offset: 0x00008A14
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000A81C File Offset: 0x00008A1C
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.element = null;
				this.id = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A82C File Offset: 0x00008A2C
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000A834 File Offset: 0x00008A34
		public KeyInfo KeyInfo
		{
			get
			{
				return this.key;
			}
			set
			{
				this.element = null;
				this.key = value;
			}
		}

		/// <summary>Gets or sets a list of objects to be signed.</summary>
		/// <returns>A list of objects to be signed.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A844 File Offset: 0x00008A44
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A84C File Offset: 0x00008A4C
		public IList ObjectList
		{
			get
			{
				return this.list;
			}
			set
			{
				this.list = ArrayList.Adapter(value);
			}
		}

		/// <summary>Gets or sets the value of the digital signature.</summary>
		/// <returns>A byte array that contains the value of the digital signature.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A85C File Offset: 0x00008A5C
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000A864 File Offset: 0x00008A64
		public byte[] SignatureValue
		{
			get
			{
				return this.signature;
			}
			set
			{
				this.element = null;
				this.signature = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A874 File Offset: 0x00008A74
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A87C File Offset: 0x00008A7C
		public SignedInfo SignedInfo
		{
			get
			{
				return this.info;
			}
			set
			{
				this.element = null;
				this.info = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to be added to the list of objects to be signed. </param>
		// Token: 0x06000253 RID: 595 RVA: 0x0000A88C File Offset: 0x00008A8C
		public void AddObject(DataObject dataObject)
		{
			this.list.Add(dataObject);
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000254 RID: 596 RVA: 0x0000A89C File Offset: 0x00008A9C
		public XmlElement GetXml()
		{
			return this.GetXml(null);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.element != null)
			{
				return this.element;
			}
			if (this.info == null)
			{
				throw new CryptographicException("SignedInfo");
			}
			if (this.signature == null)
			{
				throw new CryptographicException("SignatureValue");
			}
			if (document == null)
			{
				document = new XmlDocument();
			}
			XmlElement xmlElement = document.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			XmlNode xml = this.info.GetXml();
			XmlNode newChild = document.ImportNode(xml, true);
			xmlElement.AppendChild(newChild);
			if (this.signature != null)
			{
				XmlElement xmlElement2 = document.CreateElement("SignatureValue", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement2.InnerText = Convert.ToBase64String(this.signature);
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.key != null)
			{
				xml = this.key.GetXml();
				newChild = document.ImportNode(xml, true);
				xmlElement.AppendChild(newChild);
			}
			if (this.list.Count > 0)
			{
				foreach (object obj in this.list)
				{
					DataObject dataObject = (DataObject)obj;
					xml = dataObject.GetXml();
					newChild = document.ImportNode(xml, true);
					xmlElement.AppendChild(newChild);
				}
			}
			return xmlElement;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AA38 File Offset: 0x00008C38
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			return (xmlAttribute == null) ? null : xmlAttribute.InnerText;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Signature" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Signature" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" />.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" />. </exception>
		// Token: 0x06000257 RID: 599 RVA: 0x0000AA64 File Offset: 0x00008C64
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value.LocalName == "Signature") || !(value.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#"))
			{
				throw new CryptographicException("Malformed element: Signature.");
			}
			this.id = this.GetAttribute(value, "Id");
			int num = this.NextElementPos(value.ChildNodes, 0, "SignedInfo", "http://www.w3.org/2000/09/xmldsig#", true);
			XmlElement value2 = (XmlElement)value.ChildNodes[num];
			this.info = new SignedInfo();
			this.info.LoadXml(value2);
			num = this.NextElementPos(value.ChildNodes, num + 1, "SignatureValue", "http://www.w3.org/2000/09/xmldsig#", true);
			XmlElement xmlElement = (XmlElement)value.ChildNodes[num];
			this.signature = Convert.FromBase64String(xmlElement.InnerText);
			num = this.NextElementPos(value.ChildNodes, num + 1, "KeyInfo", "http://www.w3.org/2000/09/xmldsig#", false);
			if (num > 0)
			{
				XmlElement value3 = (XmlElement)value.ChildNodes[num];
				this.key = new KeyInfo();
				this.key.LoadXml(value3);
			}
			XmlNodeList xmlNodeList = value.SelectNodes("xd:Object", Signature.dsigNsmgr);
			foreach (object obj in xmlNodeList)
			{
				XmlElement value4 = (XmlElement)obj;
				DataObject dataObject = new DataObject();
				dataObject.LoadXml(value4);
				this.AddObject(dataObject);
			}
			if (this.info == null)
			{
				throw new CryptographicException("SignedInfo");
			}
			if (this.signature == null)
			{
				throw new CryptographicException("SignatureValue");
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000AC54 File Offset: 0x00008E54
		private int NextElementPos(XmlNodeList nl, int pos, string name, string ns, bool required)
		{
			while (pos < nl.Count)
			{
				if (nl[pos].NodeType == XmlNodeType.Element)
				{
					if (!(nl[pos].LocalName != name) && !(nl[pos].NamespaceURI != ns))
					{
						return pos;
					}
					if (required)
					{
						throw new CryptographicException("Malformed element " + name);
					}
					return -2;
				}
				else
				{
					pos++;
				}
			}
			if (required)
			{
				throw new CryptographicException("Malformed element " + name);
			}
			return -1;
		}

		// Token: 0x04000101 RID: 257
		private static XmlNamespaceManager dsigNsmgr = new XmlNamespaceManager(new NameTable());

		// Token: 0x04000102 RID: 258
		private ArrayList list;

		// Token: 0x04000103 RID: 259
		private SignedInfo info;

		// Token: 0x04000104 RID: 260
		private KeyInfo key;

		// Token: 0x04000105 RID: 261
		private string id;

		// Token: 0x04000106 RID: 262
		private byte[] signature;

		// Token: 0x04000107 RID: 263
		private XmlElement element;
	}
}
