using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml.Serialization;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) anyAttribute element.</summary>
	// Token: 0x020001FB RID: 507
	public class XmlSchemaAnyAttribute : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> class.</summary>
		// Token: 0x06001423 RID: 5155 RVA: 0x00057378 File Offset: 0x00055578
		public XmlSchemaAnyAttribute()
		{
			this.wildcard = new XsdWildcard(this);
		}

		/// <summary>Gets or sets the namespaces containing the attributes that can be used.</summary>
		/// <returns>Namespaces for attributes that are available for use. The default is ##any.Optional.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0005738C File Offset: 0x0005558C
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x00057394 File Offset: 0x00055594
		[XmlAttribute("namespace")]
		public string Namespace
		{
			get
			{
				return this.nameSpace;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the attributes specified by the anyAttribute element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no processContents attribute is specified, the default is Strict.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000573A0 File Offset: 0x000555A0
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x000573A8 File Offset: 0x000555A8
		[XmlAttribute("processContents")]
		[DefaultValue(XmlSchemaContentProcessing.None)]
		public XmlSchemaContentProcessing ProcessContents
		{
			get
			{
				return this.processing;
			}
			set
			{
				this.processing = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000573B4 File Offset: 0x000555B4
		internal bool HasValueAny
		{
			get
			{
				return this.wildcard.HasValueAny;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000573C4 File Offset: 0x000555C4
		internal bool HasValueLocal
		{
			get
			{
				return this.wildcard.HasValueLocal;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000573D4 File Offset: 0x000555D4
		internal bool HasValueOther
		{
			get
			{
				return this.wildcard.HasValueOther;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x000573E4 File Offset: 0x000555E4
		internal bool HasValueTargetNamespace
		{
			get
			{
				return this.wildcard.HasValueTargetNamespace;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x000573F4 File Offset: 0x000555F4
		internal StringCollection ResolvedNamespaces
		{
			get
			{
				return this.wildcard.ResolvedNamespaces;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00057404 File Offset: 0x00055604
		internal XmlSchemaContentProcessing ResolvedProcessContents
		{
			get
			{
				return this.wildcard.ResolvedProcessing;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00057414 File Offset: 0x00055614
		internal string TargetNamespace
		{
			get
			{
				return this.wildcard.TargetNamespace;
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00057424 File Offset: 0x00055624
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.errorCount = 0;
			this.wildcard.TargetNamespace = base.AncestorSchema.TargetNamespace;
			if (this.wildcard.TargetNamespace == null)
			{
				this.wildcard.TargetNamespace = string.Empty;
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.wildcard.Compile(this.Namespace, h, schema);
			if (this.processing == XmlSchemaContentProcessing.None)
			{
				this.wildcard.ResolvedProcessing = XmlSchemaContentProcessing.Strict;
			}
			else
			{
				this.wildcard.ResolvedProcessing = this.processing;
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000574EC File Offset: 0x000556EC
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return this.errorCount;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000574F4 File Offset: 0x000556F4
		internal void ValidateWildcardSubset(XmlSchemaAnyAttribute other, ValidationEventHandler h, XmlSchema schema)
		{
			this.wildcard.ValidateWildcardSubset(other.wildcard, h, schema);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0005750C File Offset: 0x0005570C
		internal bool ValidateWildcardAllowsNamespaceName(string ns, XmlSchema schema)
		{
			return this.wildcard.ValidateWildcardAllowsNamespaceName(ns, null, schema, false);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00057520 File Offset: 0x00055720
		internal static XmlSchemaAnyAttribute Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "anyAttribute")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAnyAttribute.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAnyAttribute.LineNumber = reader.LineNumber;
			xmlSchemaAnyAttribute.LinePosition = reader.LinePosition;
			xmlSchemaAnyAttribute.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaAnyAttribute.Id = reader.Value;
				}
				else if (reader.Name == "namespace")
				{
					xmlSchemaAnyAttribute.nameSpace = reader.Value;
				}
				else if (reader.Name == "processContents")
				{
					Exception ex;
					xmlSchemaAnyAttribute.processing = XmlSchemaUtil.ReadProcessingAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for processContents", ex);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for anyAttribute", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAnyAttribute);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAnyAttribute;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "anyAttribute")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAnyAttribute.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaAnyAttribute.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x040007B5 RID: 1973
		private const string xmlname = "anyAttribute";

		// Token: 0x040007B6 RID: 1974
		private string nameSpace;

		// Token: 0x040007B7 RID: 1975
		private XmlSchemaContentProcessing processing;

		// Token: 0x040007B8 RID: 1976
		private XsdWildcard wildcard;
	}
}
