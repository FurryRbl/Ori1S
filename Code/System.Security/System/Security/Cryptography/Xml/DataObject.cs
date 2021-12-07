using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the object element of an XML signature that holds data to be signed.</summary>
	// Token: 0x02000036 RID: 54
	public class DataObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class.</summary>
		// Token: 0x06000138 RID: 312 RVA: 0x000066E4 File Offset: 0x000048E4
		public DataObject()
		{
			this.Build(null, null, null, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class with the specified identification, MIME type, encoding, and data.</summary>
		/// <param name="id">The identification to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with. </param>
		/// <param name="mimeType">The MIME type of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />. </param>
		/// <param name="encoding">The encoding of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />. </param>
		/// <param name="data">The data to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="data" /> parameter is null. </exception>
		// Token: 0x06000139 RID: 313 RVA: 0x000066F8 File Offset: 0x000048F8
		public DataObject(string id, string mimeType, string encoding, XmlElement data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.Build(id, mimeType, encoding, data);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006720 File Offset: 0x00004920
		private void Build(string id, string mimeType, string encoding, XmlElement data)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
			if (id != null)
			{
				xmlElement.SetAttribute("Id", id);
			}
			if (mimeType != null)
			{
				xmlElement.SetAttribute("MimeType", mimeType);
			}
			if (encoding != null)
			{
				xmlElement.SetAttribute("Encoding", encoding);
			}
			if (data != null)
			{
				XmlNode newChild = xmlDocument.ImportNode(data, true);
				xmlElement.AppendChild(newChild);
			}
			this.element = xmlElement;
		}

		/// <summary>Gets or sets the data value of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The data of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value used to set the property is null.</exception>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000679C File Offset: 0x0000499C
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000067AC File Offset: 0x000049AC
		public XmlNodeList Data
		{
			get
			{
				return this.element.ChildNodes;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = (XmlElement)xmlDocument.ImportNode(this.element, true);
				while (xmlElement.LastChild != null)
				{
					xmlElement.RemoveChild(xmlElement.LastChild);
				}
				foreach (object obj in value)
				{
					XmlNode node = (XmlNode)obj;
					xmlElement.AppendChild(xmlDocument.ImportNode(node, true));
				}
				this.element = xmlElement;
				this.propertyModified = true;
			}
		}

		/// <summary>Gets or sets the encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The type of encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006878 File Offset: 0x00004A78
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006888 File Offset: 0x00004A88
		public string Encoding
		{
			get
			{
				return this.GetField("Encoding");
			}
			set
			{
				this.SetField("Encoding", value);
			}
		}

		/// <summary>Gets or sets the identification of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The name of the element that contains data to be used. </returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006898 File Offset: 0x00004A98
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000068A8 File Offset: 0x00004AA8
		public string Id
		{
			get
			{
				return this.GetField("Id");
			}
			set
			{
				this.SetField("Id", value);
			}
		}

		/// <summary>Gets or sets the MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object. </summary>
		/// <returns>The MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object. The default is null.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000068B8 File Offset: 0x00004AB8
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000068C8 File Offset: 0x00004AC8
		public string MimeType
		{
			get
			{
				return this.GetField("MimeType");
			}
			set
			{
				this.SetField("MimeType", value);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000068D8 File Offset: 0x00004AD8
		private string GetField(string attribute)
		{
			XmlNode xmlNode = this.element.Attributes[attribute];
			return (xmlNode == null) ? null : xmlNode.Value;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000690C File Offset: 0x00004B0C
		private void SetField(string attribute, string value)
		{
			if (value == null)
			{
				return;
			}
			if (this.propertyModified)
			{
				this.element.SetAttribute(attribute, value);
			}
			else
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.ImportNode(this.element, true) as XmlElement;
				xmlElement.SetAttribute(attribute, value);
				this.element = xmlElement;
				this.propertyModified = true;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000145 RID: 325 RVA: 0x0000696C File Offset: 0x00004B6C
		public XmlElement GetXml()
		{
			if (this.propertyModified)
			{
				XmlElement xmlElement = this.element;
				XmlDocument xmlDocument = new XmlDocument();
				this.element = xmlDocument.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
				foreach (object obj in xmlElement.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					string name = xmlAttribute.Name;
					if (name != null)
					{
						if (DataObject.<>f__switch$map4 == null)
						{
							DataObject.<>f__switch$map4 = new Dictionary<string, int>(3)
							{
								{
									"Id",
									0
								},
								{
									"Encoding",
									0
								},
								{
									"MimeType",
									0
								}
							};
						}
						int num;
						if (DataObject.<>f__switch$map4.TryGetValue(name, out num))
						{
							if (num == 0)
							{
								this.element.SetAttribute(xmlAttribute.Name, xmlAttribute.Value);
							}
						}
					}
				}
				foreach (object obj2 in xmlElement.ChildNodes)
				{
					XmlNode node = (XmlNode)obj2;
					this.element.AppendChild(xmlDocument.ImportNode(node, true));
				}
			}
			return this.element;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from. </param>
		/// <exception cref="T:System.ArgumentNullException">The value from the XML element is null.</exception>
		// Token: 0x06000146 RID: 326 RVA: 0x00006B0C File Offset: 0x00004D0C
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.element = value;
			this.propertyModified = false;
		}

		// Token: 0x040000AB RID: 171
		private XmlElement element;

		// Token: 0x040000AC RID: 172
		private bool propertyModified;
	}
}
