using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004A RID: 74
	internal class Manifest
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00009EF8 File Offset: 0x000080F8
		public Manifest()
		{
			this.references = new ArrayList();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009F0C File Offset: 0x0000810C
		public Manifest(XmlElement xel) : this()
		{
			this.LoadXml(xel);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009F1C File Offset: 0x0000811C
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00009F24 File Offset: 0x00008124
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009F34 File Offset: 0x00008134
		public ArrayList References
		{
			get
			{
				return this.references;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009F3C File Offset: 0x0000813C
		public void AddReference(Reference reference)
		{
			this.references.Add(reference);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009F4C File Offset: 0x0000814C
		public XmlElement GetXml()
		{
			if (this.element != null)
			{
				return this.element;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			foreach (object obj in this.references)
			{
				Reference reference = (Reference)obj;
				XmlNode xml = reference.GetXml();
				XmlNode newChild = xmlDocument.ImportNode(xml, true);
				xmlElement.AppendChild(newChild);
			}
			return xmlElement;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A01C File Offset: 0x0000821C
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			return (xmlAttribute == null) ? null : xmlAttribute.InnerText;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A048 File Offset: 0x00008248
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "Manifest" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException();
			}
			this.id = this.GetAttribute(value, "Id");
			for (int i = 0; i < value.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = value.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == "Reference" && xmlNode.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
				{
					Reference reference = new Reference();
					reference.LoadXml((XmlElement)xmlNode);
					this.AddReference(reference);
				}
			}
			this.element = value;
		}

		// Token: 0x040000F4 RID: 244
		private ArrayList references;

		// Token: 0x040000F5 RID: 245
		private string id;

		// Token: 0x040000F6 RID: 246
		private XmlElement element;
	}
}
