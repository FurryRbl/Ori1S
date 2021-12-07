using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000020 RID: 32
	internal class XsdKeyEntryField
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00007C30 File Offset: 0x00005E30
		public XsdKeyEntryField(XsdKeyEntry entry, XsdIdentityField field)
		{
			this.entry = entry;
			this.field = field;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007C48 File Offset: 0x00005E48
		public XsdIdentityField Field
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007C50 File Offset: 0x00005E50
		public bool SetIdentityField(object identity, bool isXsiNil, XsdAnySimpleType type, int depth, IXmlLineInfo li)
		{
			this.FieldFoundDepth = depth;
			this.Identity = identity;
			this.IsXsiNil = isXsiNil;
			this.FieldFound = (this.FieldFound || isXsiNil);
			this.FieldType = type;
			this.Consuming = false;
			this.Consumed = true;
			if (li != null && li.HasLineInfo())
			{
				this.FieldHasLineInfo = true;
				this.FieldLineNumber = li.LineNumber;
				this.FieldLinePosition = li.LinePosition;
			}
			if (!(this.entry.OwnerSequence.SourceSchemaIdentity is XmlSchemaKeyref))
			{
				for (int i = 0; i < this.entry.OwnerSequence.FinishedEntries.Count; i++)
				{
					XsdKeyEntry other = this.entry.OwnerSequence.FinishedEntries[i];
					if (this.entry.CompareIdentity(other))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007D38 File Offset: 0x00005F38
		internal XsdIdentityPath Matches(bool matchesAttr, object sender, XmlNameTable nameTable, ArrayList qnameStack, string sourceUri, object schemaType, IXmlNamespaceResolver nsResolver, IXmlLineInfo lineInfo, int depth, string attrName, string attrNS, object attrValue)
		{
			XsdIdentityPath xsdIdentityPath = null;
			for (int i = 0; i < this.field.Paths.Length; i++)
			{
				XsdIdentityPath xsdIdentityPath2 = this.field.Paths[i];
				bool isAttribute = xsdIdentityPath2.IsAttribute;
				if (matchesAttr == isAttribute)
				{
					if (xsdIdentityPath2.IsAttribute)
					{
						XsdIdentityStep xsdIdentityStep = xsdIdentityPath2.OrderedSteps[xsdIdentityPath2.OrderedSteps.Length - 1];
						bool flag = false;
						if (xsdIdentityStep.IsAnyName || xsdIdentityStep.NsName != null)
						{
							if (xsdIdentityStep.IsAnyName || attrNS == xsdIdentityStep.NsName)
							{
								flag = true;
							}
						}
						else if (xsdIdentityStep.Name == attrName && xsdIdentityStep.Namespace == attrNS)
						{
							flag = true;
						}
						if (!flag)
						{
							goto IL_2AF;
						}
						if (this.entry.StartDepth + (xsdIdentityPath2.OrderedSteps.Length - 1) != depth - 1)
						{
							goto IL_2AF;
						}
						xsdIdentityPath = xsdIdentityPath2;
					}
					if (!this.FieldFound || depth <= this.FieldFoundDepth || this.FieldFoundPath != xsdIdentityPath2)
					{
						if (xsdIdentityPath2.OrderedSteps.Length == 0)
						{
							if (depth == this.entry.StartDepth)
							{
								return xsdIdentityPath2;
							}
						}
						else if (depth - this.entry.StartDepth >= xsdIdentityPath2.OrderedSteps.Length - 1)
						{
							int j = xsdIdentityPath2.OrderedSteps.Length;
							if (isAttribute)
							{
								j--;
							}
							if (!xsdIdentityPath2.Descendants || depth >= this.entry.StartDepth + j)
							{
								if (xsdIdentityPath2.Descendants || depth == this.entry.StartDepth + j)
								{
									for (j--; j >= 0; j--)
									{
										XsdIdentityStep xsdIdentityStep = xsdIdentityPath2.OrderedSteps[j];
										if (!xsdIdentityStep.IsCurrent && !xsdIdentityStep.IsAnyName)
										{
											XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)qnameStack[this.entry.StartDepth + j + ((!isAttribute) ? 1 : 0)];
											if (xsdIdentityStep.NsName == null || !(xmlQualifiedName.Namespace == xsdIdentityStep.NsName))
											{
												if ((!(xsdIdentityStep.Name == "*") && !(xsdIdentityStep.Name == xmlQualifiedName.Name)) || !(xsdIdentityStep.Namespace == xmlQualifiedName.Namespace))
												{
													break;
												}
											}
										}
									}
									if (j < 0)
									{
										if (!matchesAttr)
										{
											return xsdIdentityPath2;
										}
									}
								}
							}
						}
					}
				}
				IL_2AF:;
			}
			if (xsdIdentityPath != null)
			{
				this.FillAttributeFieldValue(sender, nameTable, sourceUri, schemaType, nsResolver, attrValue, lineInfo, depth);
				if (this.Identity != null)
				{
					return xsdIdentityPath;
				}
			}
			return null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00008034 File Offset: 0x00006234
		private void FillAttributeFieldValue(object sender, XmlNameTable nameTable, string sourceUri, object schemaType, IXmlNamespaceResolver nsResolver, object identity, IXmlLineInfo lineInfo, int depth)
		{
			if (this.FieldFound)
			{
				throw new XmlSchemaValidationException(string.Format("The key value was already found as '{0}'{1}.", this.Identity, (!this.FieldHasLineInfo) ? string.Empty : string.Format(CultureInfo.InvariantCulture, " at line {0}, position {1}", new object[]
				{
					this.FieldLineNumber,
					this.FieldLinePosition
				})), sender, sourceUri, this.entry.OwnerSequence.SourceSchemaIdentity, null);
			}
			XmlSchemaDatatype xmlSchemaDatatype = schemaType as XmlSchemaDatatype;
			XmlSchemaSimpleType xmlSchemaSimpleType = schemaType as XmlSchemaSimpleType;
			if (xmlSchemaDatatype == null && xmlSchemaSimpleType != null)
			{
				xmlSchemaDatatype = xmlSchemaSimpleType.Datatype;
			}
			try
			{
				if (!this.SetIdentityField(identity, false, xmlSchemaDatatype as XsdAnySimpleType, depth, lineInfo))
				{
					throw new XmlSchemaValidationException("Two or more identical field was found.", sender, sourceUri, this.entry.OwnerSequence.SourceSchemaIdentity, null);
				}
				this.Consuming = true;
				this.FieldFound = true;
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaValidationException("Failed to read typed value.", sender, sourceUri, this.entry.OwnerSequence.SourceSchemaIdentity, innerException);
			}
		}

		// Token: 0x040000F7 RID: 247
		private XsdKeyEntry entry;

		// Token: 0x040000F8 RID: 248
		private XsdIdentityField field;

		// Token: 0x040000F9 RID: 249
		public bool FieldFound;

		// Token: 0x040000FA RID: 250
		public int FieldLineNumber;

		// Token: 0x040000FB RID: 251
		public int FieldLinePosition;

		// Token: 0x040000FC RID: 252
		public bool FieldHasLineInfo;

		// Token: 0x040000FD RID: 253
		public XsdAnySimpleType FieldType;

		// Token: 0x040000FE RID: 254
		public object Identity;

		// Token: 0x040000FF RID: 255
		public bool IsXsiNil;

		// Token: 0x04000100 RID: 256
		public int FieldFoundDepth;

		// Token: 0x04000101 RID: 257
		public XsdIdentityPath FieldFoundPath;

		// Token: 0x04000102 RID: 258
		public bool Consuming;

		// Token: 0x04000103 RID: 259
		public bool Consumed;
	}
}
