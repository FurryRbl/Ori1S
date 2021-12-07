using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the group element with ref attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). This class is used within complex types that reference a group defined at the schema level.</summary>
	// Token: 0x0200021A RID: 538
	public class XmlSchemaGroupRef : XmlSchemaParticle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" /> class.</summary>
		// Token: 0x06001577 RID: 5495 RVA: 0x00061514 File Offset: 0x0005F714
		public XmlSchemaGroupRef()
		{
			this.refName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of a group defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of a group defined in this schema.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00061528 File Offset: 0x0005F728
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00061530 File Offset: 0x0005F730
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = value;
			}
		}

		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes, which holds the post-compilation value of the Particle property.</summary>
		/// <returns>The post-compilation value of the Particle property, which is one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0006153C File Offset: 0x0005F73C
		[XmlIgnore]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				if (this.TargetGroup != null)
				{
					return this.TargetGroup.Particle;
				}
				return null;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00061558 File Offset: 0x0005F758
		internal XmlSchemaGroup TargetGroup
		{
			get
			{
				if (this.referencedGroup != null && this.referencedGroup.IsCircularDefinition)
				{
					return null;
				}
				return this.referencedGroup;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00061580 File Offset: 0x0005F780
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.schema = schema;
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			base.CompileOccurence(h, schema);
			if (this.refName == null || this.refName.IsEmpty)
			{
				base.error(h, "ref must be present");
			}
			else if (!XmlSchemaUtil.CheckQName(this.RefName))
			{
				base.error(h, "RefName must be a valid XmlQualifiedName");
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00061628 File Offset: 0x0005F828
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			this.referencedGroup = (schema.Groups[this.RefName] as XmlSchemaGroup);
			if (this.referencedGroup == null)
			{
				if (!schema.IsNamespaceAbsent(this.RefName.Namespace))
				{
					base.error(h, "Referenced group " + this.RefName + " was not found in the corresponding schema.");
				}
			}
			else if (this.referencedGroup.Particle is XmlSchemaAll && base.ValidatedMaxOccurs != 1m)
			{
				base.error(h, "Group reference to -all- particle must have schema component {maxOccurs}=1.");
			}
			if (this.TargetGroup != null)
			{
				this.TargetGroup.Validate(h, schema);
			}
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00061710 File Offset: 0x0005F910
		internal override XmlSchemaParticle GetOptimizedParticle(bool isTop)
		{
			if (this.busy)
			{
				return XmlSchemaParticle.Empty;
			}
			if (this.OptimizedParticle != null)
			{
				return this.OptimizedParticle;
			}
			this.busy = true;
			XmlSchemaGroup xmlSchemaGroup = (this.referencedGroup == null) ? (this.schema.Groups[this.RefName] as XmlSchemaGroup) : this.referencedGroup;
			if (xmlSchemaGroup != null && xmlSchemaGroup.Particle != null)
			{
				this.OptimizedParticle = xmlSchemaGroup.Particle;
				this.OptimizedParticle = this.OptimizedParticle.GetOptimizedParticle(isTop);
				if (this.OptimizedParticle != XmlSchemaParticle.Empty && (base.ValidatedMinOccurs != 1m || base.ValidatedMaxOccurs != 1m))
				{
					this.OptimizedParticle = this.OptimizedParticle.GetShallowClone();
					this.OptimizedParticle.OptimizedParticle = null;
					this.OptimizedParticle.MinOccurs = base.MinOccurs;
					this.OptimizedParticle.MaxOccurs = base.MaxOccurs;
					this.OptimizedParticle.CompileOccurence(null, null);
				}
			}
			else
			{
				this.OptimizedParticle = XmlSchemaParticle.Empty;
			}
			this.busy = false;
			return this.OptimizedParticle;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00061850 File Offset: 0x0005FA50
		internal override bool ParticleEquals(XmlSchemaParticle other)
		{
			return this.GetOptimizedParticle(true).ParticleEquals(other);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00061860 File Offset: 0x0005FA60
		internal override bool ValidateDerivationByRestriction(XmlSchemaParticle baseParticle, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			return this.TargetGroup != null && this.TargetGroup.Particle.ValidateDerivationByRestriction(baseParticle, h, schema, raiseError);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00061890 File Offset: 0x0005FA90
		internal override void CheckRecursion(int depth, ValidationEventHandler h, XmlSchema schema)
		{
			if (this.TargetGroup == null)
			{
				return;
			}
			if (this.recursionDepth == -1)
			{
				this.recursionDepth = depth;
				this.TargetGroup.Particle.CheckRecursion(depth, h, schema);
				this.recursionDepth = -2;
			}
			else if (depth == this.recursionDepth)
			{
				throw new XmlSchemaException("Circular group reference was found.", this, null);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000618F8 File Offset: 0x0005FAF8
		internal override void ValidateUniqueParticleAttribution(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
			if (this.TargetGroup != null)
			{
				this.TargetGroup.Particle.ValidateUniqueParticleAttribution(qnames, nsNames, h, schema);
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00061928 File Offset: 0x0005FB28
		internal override void ValidateUniqueTypeAttribution(XmlSchemaObjectTable labels, ValidationEventHandler h, XmlSchema schema)
		{
			if (this.TargetGroup != null)
			{
				this.TargetGroup.Particle.ValidateUniqueTypeAttribution(labels, h, schema);
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00061954 File Offset: 0x0005FB54
		internal static XmlSchemaGroupRef Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaGroupRef xmlSchemaGroupRef = new XmlSchemaGroupRef();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "group")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaGroup.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaGroupRef.LineNumber = reader.LineNumber;
			xmlSchemaGroupRef.LinePosition = reader.LinePosition;
			xmlSchemaGroupRef.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaGroupRef.Id = reader.Value;
				}
				else if (reader.Name == "ref")
				{
					Exception ex;
					xmlSchemaGroupRef.refName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for ref attribute", ex);
					}
				}
				else if (reader.Name == "maxOccurs")
				{
					try
					{
						xmlSchemaGroupRef.MaxOccursString = reader.Value;
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
						xmlSchemaGroupRef.MinOccursString = reader.Value;
					}
					catch (Exception innerException2)
					{
						XmlSchemaObject.error(h, reader.Value + " is an invalid value for minOccurs", innerException2);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for group", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaGroupRef);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaGroupRef;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "group")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaGroupRef.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaGroupRef.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaGroupRef;
		}

		// Token: 0x04000892 RID: 2194
		private const string xmlname = "group";

		// Token: 0x04000893 RID: 2195
		private XmlSchema schema;

		// Token: 0x04000894 RID: 2196
		private XmlQualifiedName refName;

		// Token: 0x04000895 RID: 2197
		private XmlSchemaGroup referencedGroup;

		// Token: 0x04000896 RID: 2198
		private bool busy;
	}
}
