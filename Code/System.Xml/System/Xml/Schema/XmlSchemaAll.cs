using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) all element (compositor).</summary>
	// Token: 0x020001F7 RID: 503
	public class XmlSchemaAll : XmlSchemaGroupBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAll" /> class.</summary>
		// Token: 0x060013EE RID: 5102 RVA: 0x00055F58 File Offset: 0x00054158
		public XmlSchemaAll()
		{
			this.items = new XmlSchemaObjectCollection();
		}

		/// <summary>Gets the collection of XmlSchemaElement elements contained within the all compositor.</summary>
		/// <returns>The collection of elements contained in XmlSchemaAll.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00055F6C File Offset: 0x0005416C
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00055F74 File Offset: 0x00054174
		internal bool Emptiable
		{
			get
			{
				return this.emptiable;
			}
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00055F7C File Offset: 0x0005417C
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				xmlSchemaObject.SetParent(this);
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00055FF0 File Offset: 0x000541F0
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.schema = schema;
			if (base.MaxOccurs != 1m)
			{
				base.error(h, "maxOccurs must be 1");
			}
			if (base.MinOccurs != 1m && base.MinOccurs != 0m)
			{
				base.error(h, "minOccurs must be 0 or 1");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			base.CompileOccurence(h, schema);
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaElement xmlSchemaElement = xmlSchemaObject as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					if (xmlSchemaElement.ValidatedMaxOccurs != 1m && xmlSchemaElement.ValidatedMaxOccurs != 0m)
					{
						xmlSchemaElement.error(h, "The {max occurs} of all the elements of 'all' must be 0 or 1. ");
					}
					this.errorCount += xmlSchemaElement.Compile(h, schema);
				}
				else
				{
					base.error(h, "XmlSchemaAll can only contain Items of type Element");
				}
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00056164 File Offset: 0x00054364
		internal override XmlSchemaParticle GetOptimizedParticle(bool isTop)
		{
			if (this.OptimizedParticle != null)
			{
				return this.OptimizedParticle;
			}
			if (this.Items.Count == 0 || base.ValidatedMaxOccurs == 0m)
			{
				this.OptimizedParticle = XmlSchemaParticle.Empty;
				return this.OptimizedParticle;
			}
			if (this.Items.Count == 1 && base.ValidatedMinOccurs == 1m && base.ValidatedMaxOccurs == 1m)
			{
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				this.CopyInfo(xmlSchemaSequence);
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.Items[0];
				xmlSchemaParticle = xmlSchemaParticle.GetOptimizedParticle(false);
				if (xmlSchemaParticle == XmlSchemaParticle.Empty)
				{
					this.OptimizedParticle = xmlSchemaParticle;
				}
				else
				{
					xmlSchemaSequence.Items.Add(xmlSchemaParticle);
					xmlSchemaSequence.CompiledItems.Add(xmlSchemaParticle);
					xmlSchemaSequence.Compile(null, this.schema);
					this.OptimizedParticle = xmlSchemaSequence;
				}
				return this.OptimizedParticle;
			}
			XmlSchemaAll xmlSchemaAll = new XmlSchemaAll();
			this.CopyInfo(xmlSchemaAll);
			base.CopyOptimizedItems(xmlSchemaAll);
			this.OptimizedParticle = xmlSchemaAll;
			xmlSchemaAll.ComputeEmptiable();
			return this.OptimizedParticle;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00056294 File Offset: 0x00054494
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.CompilationId))
			{
				return this.errorCount;
			}
			if (!this.parentIsGroupDefinition && base.ValidatedMaxOccurs != 1m)
			{
				base.error(h, "-all- group is limited to be content of a model group, or that of a complex type with maxOccurs to be 1.");
			}
			base.CompiledItems.Clear();
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				this.errorCount += xmlSchemaParticle.Validate(h, schema);
				if (xmlSchemaParticle.ValidatedMaxOccurs != 0m && xmlSchemaParticle.ValidatedMaxOccurs != 1m)
				{
					base.error(h, "MaxOccurs of a particle inside -all- compositor must be either 0 or 1.");
				}
				base.CompiledItems.Add(xmlSchemaParticle);
			}
			this.ComputeEmptiable();
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000563C0 File Offset: 0x000545C0
		private void ComputeEmptiable()
		{
			this.emptiable = true;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (((XmlSchemaParticle)this.Items[i]).ValidatedMinOccurs > 0m)
				{
					this.emptiable = false;
					break;
				}
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00056424 File Offset: 0x00054624
		internal override bool ValidateDerivationByRestriction(XmlSchemaParticle baseParticle, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			XmlSchemaAny xmlSchemaAny = baseParticle as XmlSchemaAny;
			XmlSchemaAll xmlSchemaAll = baseParticle as XmlSchemaAll;
			if (xmlSchemaAny != null)
			{
				return base.ValidateNSRecurseCheckCardinality(xmlSchemaAny, h, schema, raiseError);
			}
			if (xmlSchemaAll != null)
			{
				return this.ValidateOccurenceRangeOK(xmlSchemaAll, h, schema, raiseError) && base.ValidateRecurse(xmlSchemaAll, h, schema, raiseError);
			}
			if (raiseError)
			{
				base.error(h, "Invalid -all- particle derivation was found.");
			}
			return false;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0005648C File Offset: 0x0005468C
		internal override decimal GetMinEffectiveTotalRange()
		{
			return base.GetMinEffectiveTotalRangeAllAndSequence();
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00056494 File Offset: 0x00054694
		internal override void ValidateUniqueParticleAttribution(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
				xmlSchemaElement.ValidateUniqueParticleAttribution(qnames, nsNames, h, schema);
			}
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00056508 File Offset: 0x00054708
		internal override void ValidateUniqueTypeAttribution(XmlSchemaObjectTable labels, ValidationEventHandler h, XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
				xmlSchemaElement.ValidateUniqueTypeAttribution(labels, h, schema);
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0005657C File Offset: 0x0005477C
		internal static XmlSchemaAll Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAll xmlSchemaAll = new XmlSchemaAll();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "all")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAll.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAll.LineNumber = reader.LineNumber;
			xmlSchemaAll.LinePosition = reader.LinePosition;
			xmlSchemaAll.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaAll.Id = reader.Value;
				}
				else if (reader.Name == "maxOccurs")
				{
					try
					{
						xmlSchemaAll.MaxOccursString = reader.Value;
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
						xmlSchemaAll.MinOccursString = reader.Value;
					}
					catch (Exception innerException2)
					{
						XmlSchemaObject.error(h, reader.Value + " is an invalid value for minOccurs", innerException2);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for all", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAll);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAll;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "all")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAll.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaAll.Annotation = xmlSchemaAnnotation;
					}
				}
				else if (num <= 2 && reader.LocalName == "element")
				{
					num = 2;
					XmlSchemaElement xmlSchemaElement = XmlSchemaElement.Read(reader, h);
					if (xmlSchemaElement != null)
					{
						xmlSchemaAll.items.Add(xmlSchemaElement);
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaAll;
		}

		// Token: 0x040007A5 RID: 1957
		private const string xmlname = "all";

		// Token: 0x040007A6 RID: 1958
		private XmlSchema schema;

		// Token: 0x040007A7 RID: 1959
		private XmlSchemaObjectCollection items;

		// Token: 0x040007A8 RID: 1960
		private bool emptiable;
	}
}
