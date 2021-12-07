using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the sequence element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The sequence requires the elements in the group to appear in the specified sequence within the containing element.</summary>
	// Token: 0x02000238 RID: 568
	public class XmlSchemaSequence : XmlSchemaGroupBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> class.</summary>
		// Token: 0x0600169A RID: 5786 RVA: 0x00067958 File Offset: 0x00065B58
		public XmlSchemaSequence()
		{
			this.items = new XmlSchemaObjectCollection();
		}

		/// <summary>The elements contained within the compositor. Collection of <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaSequence" />, or <see cref="T:System.Xml.Schema.XmlSchemaAny" />.</summary>
		/// <returns>The elements contained within the compositor.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0006796C File Offset: 0x00065B6C
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00067974 File Offset: 0x00065B74
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				xmlSchemaObject.SetParent(this);
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000679E8 File Offset: 0x00065BE8
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			base.CompileOccurence(h, schema);
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				if (xmlSchemaObject is XmlSchemaElement || xmlSchemaObject is XmlSchemaGroupRef || xmlSchemaObject is XmlSchemaChoice || xmlSchemaObject is XmlSchemaSequence || xmlSchemaObject is XmlSchemaAny)
				{
					this.errorCount += xmlSchemaObject.Compile(h, schema);
				}
				else
				{
					base.error(h, "Invalid schema object was specified in the particles of the sequence model group.");
				}
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00067AF0 File Offset: 0x00065CF0
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
			if (!isTop && base.ValidatedMinOccurs == 1m && base.ValidatedMaxOccurs == 1m && this.Items.Count == 1)
			{
				return ((XmlSchemaParticle)this.Items[0]).GetOptimizedParticle(false);
			}
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			this.CopyInfo(xmlSchemaSequence);
			for (int i = 0; i < this.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = this.Items[i] as XmlSchemaParticle;
				xmlSchemaParticle = xmlSchemaParticle.GetOptimizedParticle(false);
				if (xmlSchemaParticle != XmlSchemaParticle.Empty)
				{
					if (xmlSchemaParticle is XmlSchemaSequence && xmlSchemaParticle.ValidatedMinOccurs == 1m && xmlSchemaParticle.ValidatedMaxOccurs == 1m)
					{
						XmlSchemaSequence xmlSchemaSequence2 = xmlSchemaParticle as XmlSchemaSequence;
						for (int j = 0; j < xmlSchemaSequence2.Items.Count; j++)
						{
							xmlSchemaSequence.Items.Add(xmlSchemaSequence2.Items[j]);
							xmlSchemaSequence.CompiledItems.Add(xmlSchemaSequence2.Items[j]);
						}
					}
					else
					{
						xmlSchemaSequence.Items.Add(xmlSchemaParticle);
						xmlSchemaSequence.CompiledItems.Add(xmlSchemaParticle);
					}
				}
			}
			if (xmlSchemaSequence.Items.Count == 0)
			{
				this.OptimizedParticle = XmlSchemaParticle.Empty;
			}
			else
			{
				this.OptimizedParticle = xmlSchemaSequence;
			}
			return this.OptimizedParticle;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00067CD4 File Offset: 0x00065ED4
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.CompilationId))
			{
				return this.errorCount;
			}
			base.CompiledItems.Clear();
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				this.errorCount += xmlSchemaParticle.Validate(h, schema);
				base.CompiledItems.Add(xmlSchemaParticle);
			}
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00067D94 File Offset: 0x00065F94
		internal override bool ValidateDerivationByRestriction(XmlSchemaParticle baseParticle, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			if (this == baseParticle)
			{
				return true;
			}
			XmlSchemaElement xmlSchemaElement = baseParticle as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				if (raiseError)
				{
					base.error(h, "Invalid sequence paricle derivation.");
				}
				return false;
			}
			XmlSchemaSequence xmlSchemaSequence = baseParticle as XmlSchemaSequence;
			if (xmlSchemaSequence != null)
			{
				return this.ValidateOccurenceRangeOK(xmlSchemaSequence, h, schema, raiseError) && ((xmlSchemaSequence.ValidatedMinOccurs == 0m && xmlSchemaSequence.ValidatedMaxOccurs == 0m && base.ValidatedMinOccurs == 0m && base.ValidatedMaxOccurs == 0m) || base.ValidateRecurse(xmlSchemaSequence, h, schema, raiseError));
			}
			XmlSchemaAll xmlSchemaAll = baseParticle as XmlSchemaAll;
			if (xmlSchemaAll != null)
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = new XmlSchemaObjectCollection();
				for (int i = 0; i < this.Items.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement2 = this.Items[i] as XmlSchemaElement;
					if (xmlSchemaElement2 == null)
					{
						if (raiseError)
						{
							base.error(h, "Invalid sequence particle derivation by restriction from all.");
						}
						return false;
					}
					foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaAll.Items)
					{
						XmlSchemaElement xmlSchemaElement3 = (XmlSchemaElement)xmlSchemaObject;
						if (xmlSchemaElement3.QualifiedName == xmlSchemaElement2.QualifiedName)
						{
							if (xmlSchemaObjectCollection.Contains(xmlSchemaElement3))
							{
								if (raiseError)
								{
									base.error(h, "Base element particle is mapped to the derived element particle in a sequence two or more times.");
								}
								return false;
							}
							xmlSchemaObjectCollection.Add(xmlSchemaElement3);
							if (!xmlSchemaElement2.ValidateDerivationByRestriction(xmlSchemaElement3, h, schema, raiseError))
							{
								return false;
							}
						}
					}
				}
				foreach (XmlSchemaObject xmlSchemaObject2 in xmlSchemaAll.Items)
				{
					XmlSchemaElement xmlSchemaElement4 = (XmlSchemaElement)xmlSchemaObject2;
					if (!xmlSchemaObjectCollection.Contains(xmlSchemaElement4) && !xmlSchemaElement4.ValidateIsEmptiable())
					{
						if (raiseError)
						{
							base.error(h, "In base -all- particle, mapping-skipped base element which is not emptiable was found.");
						}
						return false;
					}
				}
				return true;
			}
			XmlSchemaAny xmlSchemaAny = baseParticle as XmlSchemaAny;
			if (xmlSchemaAny != null)
			{
				return base.ValidateNSRecurseCheckCardinality(xmlSchemaAny, h, schema, raiseError);
			}
			XmlSchemaChoice xmlSchemaChoice = baseParticle as XmlSchemaChoice;
			return xmlSchemaChoice == null || base.ValidateSeqRecurseMapSumCommon(xmlSchemaChoice, h, schema, false, true, raiseError);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00068050 File Offset: 0x00066250
		internal override decimal GetMinEffectiveTotalRange()
		{
			return base.GetMinEffectiveTotalRangeAllAndSequence();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00068058 File Offset: 0x00066258
		internal override void ValidateUniqueParticleAttribution(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
			this.ValidateUPAOnHeadingOptionalComponents(qnames, nsNames, h, schema);
			this.ValidateUPAOnItems(qnames, nsNames, h, schema);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00068070 File Offset: 0x00066270
		private void ValidateUPAOnHeadingOptionalComponents(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				xmlSchemaParticle.ValidateUniqueParticleAttribution(qnames, nsNames, h, schema);
				if (xmlSchemaParticle.ValidatedMinOccurs != 0m)
				{
					break;
				}
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00068100 File Offset: 0x00066300
		private void ValidateUPAOnItems(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
			XmlSchemaObjectTable xmlSchemaObjectTable = new XmlSchemaObjectTable();
			ArrayList arrayList = new ArrayList();
			XmlSchemaObjectTable xmlSchemaObjectTable2 = new XmlSchemaObjectTable();
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < this.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = this.Items[i] as XmlSchemaParticle;
				xmlSchemaParticle.ValidateUniqueParticleAttribution(xmlSchemaObjectTable, arrayList, h, schema);
				if (xmlSchemaParticle.ValidatedMinOccurs == xmlSchemaParticle.ValidatedMaxOccurs)
				{
					xmlSchemaObjectTable.Clear();
					arrayList.Clear();
				}
				else
				{
					if (xmlSchemaParticle.ValidatedMinOccurs != 0m)
					{
						foreach (object obj in xmlSchemaObjectTable2.Names)
						{
							XmlQualifiedName name = (XmlQualifiedName)obj;
							xmlSchemaObjectTable.Set(name, null);
						}
						foreach (object obj2 in arrayList2)
						{
							arrayList.Remove(obj2);
						}
					}
					foreach (object obj3 in xmlSchemaObjectTable.Names)
					{
						XmlQualifiedName name2 = (XmlQualifiedName)obj3;
						xmlSchemaObjectTable2.Set(name2, xmlSchemaObjectTable[name2]);
					}
					arrayList2.Clear();
					arrayList2.AddRange(arrayList);
				}
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000682EC File Offset: 0x000664EC
		internal override void ValidateUniqueTypeAttribution(XmlSchemaObjectTable labels, ValidationEventHandler h, XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				xmlSchemaParticle.ValidateUniqueTypeAttribution(labels, h, schema);
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00068360 File Offset: 0x00066560
		internal static XmlSchemaSequence Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "sequence")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaSequence.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaSequence.LineNumber = reader.LineNumber;
			xmlSchemaSequence.LinePosition = reader.LinePosition;
			xmlSchemaSequence.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaSequence.Id = reader.Value;
				}
				else if (reader.Name == "maxOccurs")
				{
					try
					{
						xmlSchemaSequence.MaxOccursString = reader.Value;
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
						xmlSchemaSequence.MinOccursString = reader.Value;
					}
					catch (Exception innerException2)
					{
						XmlSchemaObject.error(h, reader.Value + " is an invalid value for minOccurs", innerException2);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for sequence", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaSequence);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaSequence;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "sequence")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaSequence.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaSequence.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "element")
						{
							num = 2;
							XmlSchemaElement xmlSchemaElement = XmlSchemaElement.Read(reader, h);
							if (xmlSchemaElement != null)
							{
								xmlSchemaSequence.items.Add(xmlSchemaElement);
							}
							continue;
						}
						if (reader.LocalName == "group")
						{
							num = 2;
							XmlSchemaGroupRef xmlSchemaGroupRef = XmlSchemaGroupRef.Read(reader, h);
							if (xmlSchemaGroupRef != null)
							{
								xmlSchemaSequence.items.Add(xmlSchemaGroupRef);
							}
							continue;
						}
						if (reader.LocalName == "choice")
						{
							num = 2;
							XmlSchemaChoice xmlSchemaChoice = XmlSchemaChoice.Read(reader, h);
							if (xmlSchemaChoice != null)
							{
								xmlSchemaSequence.items.Add(xmlSchemaChoice);
							}
							continue;
						}
						if (reader.LocalName == "sequence")
						{
							num = 2;
							XmlSchemaSequence xmlSchemaSequence2 = XmlSchemaSequence.Read(reader, h);
							if (xmlSchemaSequence2 != null)
							{
								xmlSchemaSequence.items.Add(xmlSchemaSequence2);
							}
							continue;
						}
						if (reader.LocalName == "any")
						{
							num = 2;
							XmlSchemaAny xmlSchemaAny = XmlSchemaAny.Read(reader, h);
							if (xmlSchemaAny != null)
							{
								xmlSchemaSequence.items.Add(xmlSchemaAny);
							}
							continue;
						}
					}
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaSequence;
		}

		// Token: 0x0400090B RID: 2315
		private const string xmlname = "sequence";

		// Token: 0x0400090C RID: 2316
		private XmlSchemaObjectCollection items;
	}
}
