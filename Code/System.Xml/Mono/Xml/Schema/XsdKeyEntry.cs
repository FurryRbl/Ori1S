using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000022 RID: 34
	internal class XsdKeyEntry
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000081A4 File Offset: 0x000063A4
		public XsdKeyEntry(XsdKeyTable keyseq, int depth, IXmlLineInfo li)
		{
			this.Init(keyseq, depth, li);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000081B8 File Offset: 0x000063B8
		public bool KeyFound
		{
			get
			{
				if (this.keyFound)
				{
					return true;
				}
				for (int i = 0; i < this.KeyFields.Count; i++)
				{
					XsdKeyEntryField xsdKeyEntryField = this.KeyFields[i];
					if (xsdKeyEntryField.FieldFound)
					{
						this.keyFound = true;
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00008210 File Offset: 0x00006410
		private void Init(XsdKeyTable keyseq, int depth, IXmlLineInfo li)
		{
			this.OwnerSequence = keyseq;
			this.KeyFields = new XsdKeyEntryFieldCollection();
			for (int i = 0; i < keyseq.Selector.Fields.Length; i++)
			{
				this.KeyFields.Add(new XsdKeyEntryField(this, keyseq.Selector.Fields[i]));
			}
			this.StartDepth = depth;
			if (li != null && li.HasLineInfo())
			{
				this.SelectorHasLineInfo = true;
				this.SelectorLineNumber = li.LineNumber;
				this.SelectorLinePosition = li.LinePosition;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000082A4 File Offset: 0x000064A4
		public bool CompareIdentity(XsdKeyEntry other)
		{
			for (int i = 0; i < this.KeyFields.Count; i++)
			{
				XsdKeyEntryField xsdKeyEntryField = this.KeyFields[i];
				XsdKeyEntryField xsdKeyEntryField2 = other.KeyFields[i];
				if ((xsdKeyEntryField.IsXsiNil && !xsdKeyEntryField2.IsXsiNil) || (!xsdKeyEntryField.IsXsiNil && xsdKeyEntryField2.IsXsiNil))
				{
					return false;
				}
				if (!XmlSchemaUtil.AreSchemaDatatypeEqual(xsdKeyEntryField2.FieldType, xsdKeyEntryField2.Identity, xsdKeyEntryField.FieldType, xsdKeyEntryField.Identity))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000833C File Offset: 0x0000653C
		public void ProcessMatch(bool isAttribute, ArrayList qnameStack, object sender, XmlNameTable nameTable, string sourceUri, object schemaType, IXmlNamespaceResolver nsResolver, IXmlLineInfo li, int depth, string attrName, string attrNS, object attrValue, bool isXsiNil, ArrayList currentKeyFieldConsumers)
		{
			for (int i = 0; i < this.KeyFields.Count; i++)
			{
				XsdKeyEntryField xsdKeyEntryField = this.KeyFields[i];
				XsdIdentityPath xsdIdentityPath = xsdKeyEntryField.Matches(isAttribute, sender, nameTable, qnameStack, sourceUri, schemaType, nsResolver, li, depth, attrName, attrNS, attrValue);
				if (xsdIdentityPath != null)
				{
					if (xsdKeyEntryField.FieldFound)
					{
						if (!xsdKeyEntryField.Consuming)
						{
							throw new XmlSchemaValidationException("Two or more matching field was found.", sender, sourceUri, this.OwnerSequence.SourceSchemaIdentity, null);
						}
						xsdKeyEntryField.Consuming = false;
					}
					if (!xsdKeyEntryField.Consumed)
					{
						if (isXsiNil && !xsdKeyEntryField.SetIdentityField(Guid.Empty, true, XsdAnySimpleType.Instance, depth, li))
						{
							throw new XmlSchemaValidationException("Two or more identical field was found.", sender, sourceUri, this.OwnerSequence.SourceSchemaIdentity, null);
						}
						XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
						if (xmlSchemaComplexType != null && (xmlSchemaComplexType.ContentType == XmlSchemaContentType.Empty || xmlSchemaComplexType.ContentType == XmlSchemaContentType.ElementOnly) && schemaType != XmlSchemaComplexType.AnyType)
						{
							throw new XmlSchemaValidationException("Specified schema type is complex type, which is not allowed for identity constraints.", sender, sourceUri, this.OwnerSequence.SourceSchemaIdentity, null);
						}
						xsdKeyEntryField.FieldFound = true;
						xsdKeyEntryField.FieldFoundPath = xsdIdentityPath;
						xsdKeyEntryField.FieldFoundDepth = depth;
						xsdKeyEntryField.Consuming = true;
						if (li != null && li.HasLineInfo())
						{
							xsdKeyEntryField.FieldHasLineInfo = true;
							xsdKeyEntryField.FieldLineNumber = li.LineNumber;
							xsdKeyEntryField.FieldLinePosition = li.LinePosition;
						}
						currentKeyFieldConsumers.Add(xsdKeyEntryField);
					}
				}
			}
		}

		// Token: 0x04000104 RID: 260
		public int StartDepth;

		// Token: 0x04000105 RID: 261
		public int SelectorLineNumber;

		// Token: 0x04000106 RID: 262
		public int SelectorLinePosition;

		// Token: 0x04000107 RID: 263
		public bool SelectorHasLineInfo;

		// Token: 0x04000108 RID: 264
		public XsdKeyEntryFieldCollection KeyFields;

		// Token: 0x04000109 RID: 265
		public bool KeyRefFound;

		// Token: 0x0400010A RID: 266
		public XsdKeyTable OwnerSequence;

		// Token: 0x0400010B RID: 267
		private bool keyFound;
	}
}
