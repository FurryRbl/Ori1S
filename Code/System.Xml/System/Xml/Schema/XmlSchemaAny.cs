using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml.Serialization;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) any element.</summary>
	// Token: 0x020001FA RID: 506
	public class XmlSchemaAny : XmlSchemaParticle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAny" /> class.</summary>
		// Token: 0x0600140B RID: 5131 RVA: 0x00056BC0 File Offset: 0x00054DC0
		public XmlSchemaAny()
		{
			this.wildcard = new XsdWildcard(this);
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00056BD4 File Offset: 0x00054DD4
		internal static XmlSchemaAny AnyTypeContent
		{
			get
			{
				if (XmlSchemaAny.anyTypeContent == null)
				{
					XmlSchemaAny.anyTypeContent = new XmlSchemaAny();
					XmlSchemaAny.anyTypeContent.MaxOccursString = "unbounded";
					XmlSchemaAny.anyTypeContent.MinOccurs = 0m;
					XmlSchemaAny.anyTypeContent.CompileOccurence(null, null);
					XmlSchemaAny.anyTypeContent.Namespace = "##any";
					XmlSchemaAny.anyTypeContent.wildcard.HasValueAny = true;
					XmlSchemaAny.anyTypeContent.wildcard.ResolvedNamespaces = new StringCollection();
					XsdWildcard xsdWildcard = XmlSchemaAny.anyTypeContent.wildcard;
					XmlSchemaContentProcessing xmlSchemaContentProcessing = XmlSchemaContentProcessing.Lax;
					XmlSchemaAny.anyTypeContent.ProcessContents = xmlSchemaContentProcessing;
					xsdWildcard.ResolvedProcessing = xmlSchemaContentProcessing;
					XmlSchemaAny.anyTypeContent.wildcard.SkipCompile = true;
				}
				return XmlSchemaAny.anyTypeContent;
			}
		}

		/// <summary>Gets or sets the namespaces containing the elements that can be used.</summary>
		/// <returns>Namespaces for elements that are available for use. The default is ##any.Optional.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00056C88 File Offset: 0x00054E88
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00056C90 File Offset: 0x00054E90
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

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the elements specified by the any element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no processContents attribute is specified, the default is Strict.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00056C9C File Offset: 0x00054E9C
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x00056CA4 File Offset: 0x00054EA4
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

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00056CB0 File Offset: 0x00054EB0
		internal bool HasValueAny
		{
			get
			{
				return this.wildcard.HasValueAny;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00056CC0 File Offset: 0x00054EC0
		internal bool HasValueLocal
		{
			get
			{
				return this.wildcard.HasValueLocal;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00056CD0 File Offset: 0x00054ED0
		internal bool HasValueOther
		{
			get
			{
				return this.wildcard.HasValueOther;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00056CE0 File Offset: 0x00054EE0
		internal bool HasValueTargetNamespace
		{
			get
			{
				return this.wildcard.HasValueTargetNamespace;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00056CF0 File Offset: 0x00054EF0
		internal StringCollection ResolvedNamespaces
		{
			get
			{
				return this.wildcard.ResolvedNamespaces;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00056D00 File Offset: 0x00054F00
		internal XmlSchemaContentProcessing ResolvedProcessContents
		{
			get
			{
				return this.wildcard.ResolvedProcessing;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00056D10 File Offset: 0x00054F10
		internal string TargetNamespace
		{
			get
			{
				return this.wildcard.TargetNamespace;
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00056D20 File Offset: 0x00054F20
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.errorCount = 0;
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.wildcard.TargetNamespace = base.AncestorSchema.TargetNamespace;
			if (this.wildcard.TargetNamespace == null)
			{
				this.wildcard.TargetNamespace = string.Empty;
			}
			base.CompileOccurence(h, schema);
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

		// Token: 0x06001419 RID: 5145 RVA: 0x00056DF0 File Offset: 0x00054FF0
		internal override XmlSchemaParticle GetOptimizedParticle(bool isTop)
		{
			if (this.OptimizedParticle != null)
			{
				return this.OptimizedParticle;
			}
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			this.CopyInfo(xmlSchemaAny);
			xmlSchemaAny.CompileOccurence(null, null);
			xmlSchemaAny.wildcard = this.wildcard;
			this.OptimizedParticle = xmlSchemaAny;
			xmlSchemaAny.Namespace = this.Namespace;
			xmlSchemaAny.ProcessContents = this.ProcessContents;
			xmlSchemaAny.Annotation = base.Annotation;
			xmlSchemaAny.UnhandledAttributes = base.UnhandledAttributes;
			return this.OptimizedParticle;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00056E70 File Offset: 0x00055070
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return this.errorCount;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00056E78 File Offset: 0x00055078
		internal override bool ParticleEquals(XmlSchemaParticle other)
		{
			XmlSchemaAny xmlSchemaAny = other as XmlSchemaAny;
			if (xmlSchemaAny == null)
			{
				return false;
			}
			if (this.HasValueAny != xmlSchemaAny.HasValueAny || this.HasValueLocal != xmlSchemaAny.HasValueLocal || this.HasValueOther != xmlSchemaAny.HasValueOther || this.HasValueTargetNamespace != xmlSchemaAny.HasValueTargetNamespace || this.ResolvedProcessContents != xmlSchemaAny.ResolvedProcessContents || base.ValidatedMaxOccurs != xmlSchemaAny.ValidatedMaxOccurs || base.ValidatedMinOccurs != xmlSchemaAny.ValidatedMinOccurs || this.ResolvedNamespaces.Count != xmlSchemaAny.ResolvedNamespaces.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ResolvedNamespaces.Count; i++)
			{
				if (this.ResolvedNamespaces[i] != xmlSchemaAny.ResolvedNamespaces[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00056F74 File Offset: 0x00055174
		internal bool ExamineAttributeWildcardIntersection(XmlSchemaAny other, ValidationEventHandler h, XmlSchema schema)
		{
			return this.wildcard.ExamineAttributeWildcardIntersection(other, h, schema);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00056F84 File Offset: 0x00055184
		internal override bool ValidateDerivationByRestriction(XmlSchemaParticle baseParticle, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			XmlSchemaAny xmlSchemaAny = baseParticle as XmlSchemaAny;
			if (xmlSchemaAny == null)
			{
				if (raiseError)
				{
					base.error(h, "Invalid particle derivation by restriction was found.");
				}
				return false;
			}
			return this.ValidateOccurenceRangeOK(baseParticle, h, schema, raiseError) && this.wildcard.ValidateWildcardSubset(xmlSchemaAny.wildcard, h, schema, raiseError);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00056FDC File Offset: 0x000551DC
		internal override void CheckRecursion(int depth, ValidationEventHandler h, XmlSchema schema)
		{
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00056FE0 File Offset: 0x000551E0
		internal override void ValidateUniqueParticleAttribution(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
			foreach (object obj in nsNames)
			{
				XmlSchemaAny other = (XmlSchemaAny)obj;
				if (!this.ExamineAttributeWildcardIntersection(other, h, schema))
				{
					base.error(h, "Ambiguous -any- particle was found.");
				}
			}
			nsNames.Add(this);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00057068 File Offset: 0x00055268
		internal override void ValidateUniqueTypeAttribution(XmlSchemaObjectTable labels, ValidationEventHandler h, XmlSchema schema)
		{
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0005706C File Offset: 0x0005526C
		internal bool ValidateWildcardAllowsNamespaceName(string ns, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			return this.wildcard.ValidateWildcardAllowsNamespaceName(ns, h, schema, raiseError);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00057080 File Offset: 0x00055280
		internal static XmlSchemaAny Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "any")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAny.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAny.LineNumber = reader.LineNumber;
			xmlSchemaAny.LinePosition = reader.LinePosition;
			xmlSchemaAny.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaAny.Id = reader.Value;
				}
				else if (reader.Name == "maxOccurs")
				{
					try
					{
						xmlSchemaAny.MaxOccursString = reader.Value;
					}
					catch (Exception innerException)
					{
						XmlSchemaObject.error(h, reader.Value + " is an invalid value for maxOccurs", innerException);
					}
				}
				else if (reader.Name == "minOccurs")
				{
					try
					{
						xmlSchemaAny.MinOccursString = reader.Value;
					}
					catch (Exception innerException2)
					{
						XmlSchemaObject.error(h, reader.Value + " is an invalid value for minOccurs", innerException2);
					}
				}
				else if (reader.Name == "namespace")
				{
					xmlSchemaAny.nameSpace = reader.Value;
				}
				else if (reader.Name == "processContents")
				{
					Exception ex;
					xmlSchemaAny.processing = XmlSchemaUtil.ReadProcessingAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for processContents", ex);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for any", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAny);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAny;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "any")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAny.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaAny.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaAny;
		}

		// Token: 0x040007B0 RID: 1968
		private const string xmlname = "any";

		// Token: 0x040007B1 RID: 1969
		private static XmlSchemaAny anyTypeContent;

		// Token: 0x040007B2 RID: 1970
		private string nameSpace;

		// Token: 0x040007B3 RID: 1971
		private XmlSchemaContentProcessing processing;

		// Token: 0x040007B4 RID: 1972
		private XsdWildcard wildcard;
	}
}
