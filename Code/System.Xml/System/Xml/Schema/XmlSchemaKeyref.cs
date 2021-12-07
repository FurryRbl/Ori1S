using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>This class represents the keyref element from XMLSchema as specified by the World Wide Web Consortium (W3C).</summary>
	// Token: 0x02000224 RID: 548
	public class XmlSchemaKeyref : XmlSchemaIdentityConstraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaKeyref" /> class.</summary>
		// Token: 0x060015E2 RID: 5602 RVA: 0x000646B8 File Offset: 0x000628B8
		public XmlSchemaKeyref()
		{
			this.refer = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the key that this constraint refers to in another simple or complex type.</summary>
		/// <returns>The QName of the key that this constraint refers to.</returns>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000646CC File Offset: 0x000628CC
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x000646D4 File Offset: 0x000628D4
		[XmlAttribute("refer")]
		public XmlQualifiedName Refer
		{
			get
			{
				return this.refer;
			}
			set
			{
				this.refer = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000646E0 File Offset: 0x000628E0
		internal XmlSchemaIdentityConstraint Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000646E8 File Offset: 0x000628E8
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			base.Compile(h, schema);
			if (this.refer == null || this.refer.IsEmpty)
			{
				base.error(h, "refer must be present");
			}
			else if (!XmlSchemaUtil.CheckQName(this.refer))
			{
				base.error(h, "Refer is not a valid XmlQualifiedName");
			}
			return this.errorCount;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00064754 File Offset: 0x00062954
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = schema.NamedIdentities[this.Refer] as XmlSchemaIdentityConstraint;
			if (xmlSchemaIdentityConstraint == null)
			{
				base.error(h, "Target key was not found.");
			}
			else if (xmlSchemaIdentityConstraint is XmlSchemaKeyref)
			{
				base.error(h, "Target identity constraint was keyref.");
			}
			else if (xmlSchemaIdentityConstraint.Fields.Count != base.Fields.Count)
			{
				base.error(h, "Target identity constraint has different number of fields.");
			}
			else
			{
				this.target = xmlSchemaIdentityConstraint;
			}
			return this.errorCount;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000647E4 File Offset: 0x000629E4
		internal static XmlSchemaKeyref Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaKeyref xmlSchemaKeyref = new XmlSchemaKeyref();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "keyref")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaKeyref.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaKeyref.LineNumber = reader.LineNumber;
			xmlSchemaKeyref.LinePosition = reader.LinePosition;
			xmlSchemaKeyref.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaKeyref.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaKeyref.Name = reader.Value;
				}
				else if (reader.Name == "refer")
				{
					Exception ex;
					xmlSchemaKeyref.refer = XmlSchemaUtil.ReadQNameAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for refer attribute", ex);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for keyref", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaKeyref);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaKeyref;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "keyref")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaKeyref.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaKeyref.Annotation = xmlSchemaAnnotation;
					}
				}
				else if (num <= 2 && reader.LocalName == "selector")
				{
					num = 3;
					XmlSchemaXPath xmlSchemaXPath = XmlSchemaXPath.Read(reader, h, "selector");
					if (xmlSchemaXPath != null)
					{
						xmlSchemaKeyref.Selector = xmlSchemaXPath;
					}
				}
				else if (num <= 3 && reader.LocalName == "field")
				{
					num = 3;
					if (xmlSchemaKeyref.Selector == null)
					{
						XmlSchemaObject.error(h, "selector must be defined before field declarations", null);
					}
					XmlSchemaXPath xmlSchemaXPath2 = XmlSchemaXPath.Read(reader, h, "field");
					if (xmlSchemaXPath2 != null)
					{
						xmlSchemaKeyref.Fields.Add(xmlSchemaXPath2);
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaKeyref;
		}

		// Token: 0x040008CE RID: 2254
		private const string xmlname = "keyref";

		// Token: 0x040008CF RID: 2255
		private XmlQualifiedName refer;

		// Token: 0x040008D0 RID: 2256
		private XmlSchemaIdentityConstraint target;
	}
}
