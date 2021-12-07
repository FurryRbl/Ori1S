using System;
using System.Xml.Serialization;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>Represents the simpleType element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a simple type. Simple types can specify information and constraints for the value of attributes or elements with text-only content.</summary>
	// Token: 0x0200023D RID: 573
	public class XmlSchemaSimpleType : XmlSchemaType
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x0007043C File Offset: 0x0006E63C
		static XmlSchemaSimpleType()
		{
			XmlSchemaSimpleType.XdtUntypedAtomic = XmlSchemaSimpleType.BuildSchemaType("untypedAtomic", "anyAtomicType", true, true);
			XmlSchemaSimpleType.XdtDayTimeDuration = XmlSchemaSimpleType.BuildSchemaType("dayTimeDuration", "duration", true, false);
			XmlSchemaSimpleType.XdtYearMonthDuration = XmlSchemaSimpleType.BuildSchemaType("yearMonthDuration", "duration", true, false);
			XmlSchemaSimpleType.XsIDRefs = new XmlSchemaSimpleType();
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
			xmlSchemaSimpleTypeList.ItemType = XmlSchemaSimpleType.XsIDRef;
			XmlSchemaSimpleType.XsIDRefs.Content = xmlSchemaSimpleTypeList;
			XmlSchemaSimpleType.XsEntities = new XmlSchemaSimpleType();
			xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
			xmlSchemaSimpleTypeList.ItemType = XmlSchemaSimpleType.XsEntity;
			XmlSchemaSimpleType.XsEntities.Content = xmlSchemaSimpleTypeList;
			XmlSchemaSimpleType.XsNMTokens = new XmlSchemaSimpleType();
			xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
			xmlSchemaSimpleTypeList.ItemType = XmlSchemaSimpleType.XsNMToken;
			XmlSchemaSimpleType.XsNMTokens.Content = xmlSchemaSimpleTypeList;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00070894 File Offset: 0x0006EA94
		private static XmlSchemaSimpleType BuildSchemaType(string name, string baseName)
		{
			return XmlSchemaSimpleType.BuildSchemaType(name, baseName, false, false);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000708A0 File Offset: 0x0006EAA0
		private static XmlSchemaSimpleType BuildSchemaType(string name, string baseName, bool xdt, bool baseXdt)
		{
			string ns = (!xdt) ? "http://www.w3.org/2001/XMLSchema" : "http://www.w3.org/2003/11/xpath-datatypes";
			string ns2 = (!baseXdt) ? "http://www.w3.org/2001/XMLSchema" : "http://www.w3.org/2003/11/xpath-datatypes";
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.QNameInternal = new XmlQualifiedName(name, ns);
			if (baseName != null)
			{
				xmlSchemaSimpleType.BaseXmlSchemaTypeInternal = XmlSchemaType.GetBuiltInSimpleType(new XmlQualifiedName(baseName, ns2));
			}
			xmlSchemaSimpleType.DatatypeInternal = XmlSchemaDatatype.FromName(xmlSchemaSimpleType.QualifiedName);
			return xmlSchemaSimpleType;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00070918 File Offset: 0x0006EB18
		internal static XsdAnySimpleType AnySimpleType
		{
			get
			{
				return XsdAnySimpleType.Instance;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00070920 File Offset: 0x0006EB20
		internal static XmlSchemaSimpleType SchemaLocationType
		{
			get
			{
				return XmlSchemaSimpleType.schemaLocationType;
			}
		}

		/// <summary>Gets or sets one of <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeUnion" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeList" />, or <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeRestriction" />.</summary>
		/// <returns>One of XmlSchemaSimpleTypeUnion, XmlSchemaSimpleTypeList, or XmlSchemaSimpleTypeRestriction.</returns>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00070928 File Offset: 0x0006EB28
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x00070930 File Offset: 0x0006EB30
		[XmlElement("union", typeof(XmlSchemaSimpleTypeUnion))]
		[XmlElement("restriction", typeof(XmlSchemaSimpleTypeRestriction))]
		[XmlElement("list", typeof(XmlSchemaSimpleTypeList))]
		public XmlSchemaSimpleTypeContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x0007093C File Offset: 0x0006EB3C
		internal XmlSchemaDerivationMethod Variety
		{
			get
			{
				return this.variety;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00070944 File Offset: 0x0006EB44
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.Content != null)
			{
				this.Content.SetParent(this);
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00070970 File Offset: 0x0006EB70
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.errorCount = 0;
			if (this.islocal)
			{
				if (base.Name != null)
				{
					base.error(h, "Name is prohibited in a local simpletype");
				}
				else
				{
					this.QNameInternal = new XmlQualifiedName(base.Name, base.AncestorSchema.TargetNamespace);
				}
				if (base.Final != XmlSchemaDerivationMethod.None)
				{
					base.error(h, "Final is prohibited in a local simpletype");
				}
			}
			else
			{
				if (base.Name == null)
				{
					base.error(h, "Name is required in top level simpletype");
				}
				else if (!XmlSchemaUtil.CheckNCName(base.Name))
				{
					base.error(h, "name attribute of a simpleType must be NCName");
				}
				else
				{
					this.QNameInternal = new XmlQualifiedName(base.Name, base.AncestorSchema.TargetNamespace);
				}
				XmlSchemaDerivationMethod final = base.Final;
				if (final != XmlSchemaDerivationMethod.All)
				{
					if (final != XmlSchemaDerivationMethod.None)
					{
						if (final == XmlSchemaDerivationMethod.Restriction || final == XmlSchemaDerivationMethod.List || final == XmlSchemaDerivationMethod.Union)
						{
							this.finalResolved = base.Final;
							goto IL_19F;
						}
						base.error(h, "The value of final attribute is not valid for simpleType");
					}
					XmlSchemaDerivationMethod xmlSchemaDerivationMethod = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union;
					XmlSchemaDerivationMethod finalDefault = schema.FinalDefault;
					if (finalDefault != XmlSchemaDerivationMethod.All)
					{
						if (finalDefault != XmlSchemaDerivationMethod.None)
						{
							this.finalResolved = (schema.FinalDefault & xmlSchemaDerivationMethod);
						}
						else
						{
							this.finalResolved = XmlSchemaDerivationMethod.Empty;
						}
					}
					else
					{
						this.finalResolved = XmlSchemaDerivationMethod.All;
					}
				}
				else
				{
					this.finalResolved = XmlSchemaDerivationMethod.All;
				}
			}
			IL_19F:
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			if (this.Content != null)
			{
				this.Content.OwnerType = this;
			}
			if (this.Content == null)
			{
				base.error(h, "Content is required in a simpletype");
			}
			else if (this.Content is XmlSchemaSimpleTypeRestriction)
			{
				this.resolvedDerivedBy = XmlSchemaDerivationMethod.Restriction;
				this.errorCount += ((XmlSchemaSimpleTypeRestriction)this.Content).Compile(h, schema);
			}
			else if (this.Content is XmlSchemaSimpleTypeList)
			{
				this.resolvedDerivedBy = XmlSchemaDerivationMethod.List;
				this.errorCount += ((XmlSchemaSimpleTypeList)this.Content).Compile(h, schema);
			}
			else if (this.Content is XmlSchemaSimpleTypeUnion)
			{
				this.resolvedDerivedBy = XmlSchemaDerivationMethod.Union;
				this.errorCount += ((XmlSchemaSimpleTypeUnion)this.Content).Compile(h, schema);
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00070C24 File Offset: 0x0006EE24
		internal void CollectBaseType(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.Content is XmlSchemaSimpleTypeRestriction)
			{
				object actualType = ((XmlSchemaSimpleTypeRestriction)this.Content).GetActualType(h, schema, false);
				this.BaseXmlSchemaTypeInternal = (actualType as XmlSchemaSimpleType);
				if (this.BaseXmlSchemaTypeInternal != null)
				{
					this.DatatypeInternal = this.BaseXmlSchemaTypeInternal.Datatype;
				}
				else
				{
					this.DatatypeInternal = (actualType as XmlSchemaDatatype);
				}
			}
			else
			{
				this.DatatypeInternal = XmlSchemaSimpleType.AnySimpleType;
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			if (this.recursed)
			{
				base.error(h, "Circular type reference was found.");
				return this.errorCount;
			}
			this.recursed = true;
			this.CollectBaseType(h, schema);
			if (this.content != null)
			{
				this.errorCount += this.content.Validate(h, schema);
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = base.BaseXmlSchemaType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				this.DatatypeInternal = xmlSchemaSimpleType.Datatype;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType2 = base.BaseXmlSchemaType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType2 != null && (xmlSchemaSimpleType2.FinalResolved & this.resolvedDerivedBy) != XmlSchemaDerivationMethod.Empty)
			{
				base.error(h, "Specified derivation is prohibited by the base simple type.");
			}
			if (this.resolvedDerivedBy == XmlSchemaDerivationMethod.Restriction && xmlSchemaSimpleType2 != null)
			{
				this.variety = xmlSchemaSimpleType2.Variety;
			}
			else
			{
				this.variety = this.resolvedDerivedBy;
			}
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = this.Content as XmlSchemaSimpleTypeRestriction;
			object baseType = (base.BaseXmlSchemaType == null) ? base.Datatype : base.BaseXmlSchemaType;
			if (xmlSchemaSimpleTypeRestriction != null)
			{
				this.ValidateDerivationValid(baseType, xmlSchemaSimpleTypeRestriction.Facets, h, schema);
			}
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = this.Content as XmlSchemaSimpleTypeList;
			if (xmlSchemaSimpleTypeList != null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType3 = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType3 != null && xmlSchemaSimpleType3.Content is XmlSchemaSimpleTypeList)
				{
					base.error(h, "List type must not be derived from another list type.");
				}
			}
			this.recursed = false;
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00070E34 File Offset: 0x0006F034
		internal void ValidateDerivationValid(object baseType, XmlSchemaObjectCollection facets, ValidationEventHandler h, XmlSchema schema)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = baseType as XmlSchemaSimpleType;
			XmlSchemaDerivationMethod xmlSchemaDerivationMethod = this.Variety;
			if (xmlSchemaDerivationMethod != XmlSchemaDerivationMethod.Restriction)
			{
				if (xmlSchemaDerivationMethod != XmlSchemaDerivationMethod.List)
				{
					if (xmlSchemaDerivationMethod == XmlSchemaDerivationMethod.Union)
					{
						if (facets != null)
						{
							foreach (XmlSchemaObject xmlSchemaObject in facets)
							{
								XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
								if (!(xmlSchemaFacet is XmlSchemaEnumerationFacet) && !(xmlSchemaFacet is XmlSchemaPatternFacet))
								{
									base.error(h, "Not allowed facet was found on this simple type which derives list type.");
								}
							}
						}
					}
				}
				else if (facets != null)
				{
					foreach (XmlSchemaObject xmlSchemaObject2 in facets)
					{
						XmlSchemaFacet xmlSchemaFacet2 = (XmlSchemaFacet)xmlSchemaObject2;
						if (!(xmlSchemaFacet2 is XmlSchemaLengthFacet) && !(xmlSchemaFacet2 is XmlSchemaMaxLengthFacet) && !(xmlSchemaFacet2 is XmlSchemaMinLengthFacet) && !(xmlSchemaFacet2 is XmlSchemaEnumerationFacet) && !(xmlSchemaFacet2 is XmlSchemaPatternFacet))
						{
							base.error(h, "Not allowed facet was found on this simple type which derives list type.");
						}
					}
				}
			}
			else
			{
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.resolvedDerivedBy != XmlSchemaDerivationMethod.Restriction)
				{
					base.error(h, "Base schema type is not either atomic type or primitive type.");
				}
				if (xmlSchemaSimpleType != null && (xmlSchemaSimpleType.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
				{
					base.error(h, "Derivation by restriction is prohibited by the base simple type.");
				}
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00070FE8 File Offset: 0x0006F1E8
		internal bool ValidateTypeDerivationOK(object baseType, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			if (this == baseType || baseType == XmlSchemaSimpleType.AnySimpleType || baseType == XmlSchemaComplexType.AnyType)
			{
				return true;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = baseType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null && (xmlSchemaSimpleType.FinalResolved & this.resolvedDerivedBy) != XmlSchemaDerivationMethod.Empty)
			{
				if (raiseError)
				{
					base.error(h, "Specified derivation is prohibited by the base type.");
				}
				return false;
			}
			if (base.BaseXmlSchemaType == baseType || base.Datatype == baseType)
			{
				return true;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType2 = base.BaseXmlSchemaType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType2 != null && xmlSchemaSimpleType2.ValidateTypeDerivationOK(baseType, h, schema, false))
			{
				return true;
			}
			XmlSchemaDerivationMethod xmlSchemaDerivationMethod = this.Variety;
			if (xmlSchemaDerivationMethod == XmlSchemaDerivationMethod.List || xmlSchemaDerivationMethod == XmlSchemaDerivationMethod.Union)
			{
				if (baseType == XmlSchemaSimpleType.AnySimpleType)
				{
					return true;
				}
			}
			if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.Variety == XmlSchemaDerivationMethod.Union)
			{
				foreach (object baseType2 in ((XmlSchemaSimpleTypeUnion)xmlSchemaSimpleType.Content).ValidatedTypes)
				{
					if (this.ValidateTypeDerivationOK(baseType2, h, schema, false))
					{
						return true;
					}
				}
			}
			if (raiseError)
			{
				base.error(h, "Invalid simple type derivation was found.");
			}
			return false;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0007111C File Offset: 0x0006F31C
		internal string Normalize(string s, XmlNameTable nt, XmlNamespaceManager nsmgr)
		{
			return this.Content.Normalize(s, nt, nsmgr);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0007112C File Offset: 0x0006F32C
		internal static XmlSchemaSimpleType Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "simpleType")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaGroup.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaSimpleType.LineNumber = reader.LineNumber;
			xmlSchemaSimpleType.LinePosition = reader.LinePosition;
			xmlSchemaSimpleType.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "final")
				{
					Exception ex;
					xmlSchemaSimpleType.Final = XmlSchemaUtil.ReadDerivationAttribute(reader, out ex, "final", XmlSchemaUtil.FinalAllowed);
					if (ex != null)
					{
						XmlSchemaObject.error(h, "some invalid values not a valid value for final", ex);
					}
				}
				else if (reader.Name == "id")
				{
					xmlSchemaSimpleType.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaSimpleType.Name = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for simpleType", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaSimpleType);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaSimpleType;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "simpleType")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaSimpleType.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaSimpleType.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "restriction")
						{
							num = 3;
							XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = XmlSchemaSimpleTypeRestriction.Read(reader, h);
							if (xmlSchemaSimpleTypeRestriction != null)
							{
								xmlSchemaSimpleType.content = xmlSchemaSimpleTypeRestriction;
							}
							continue;
						}
						if (reader.LocalName == "list")
						{
							num = 3;
							XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = XmlSchemaSimpleTypeList.Read(reader, h);
							if (xmlSchemaSimpleTypeList != null)
							{
								xmlSchemaSimpleType.content = xmlSchemaSimpleTypeList;
							}
							continue;
						}
						if (reader.LocalName == "union")
						{
							num = 3;
							XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = XmlSchemaSimpleTypeUnion.Read(reader, h);
							if (xmlSchemaSimpleTypeUnion != null)
							{
								xmlSchemaSimpleType.content = xmlSchemaSimpleTypeUnion;
							}
							continue;
						}
					}
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x0400091B RID: 2331
		private const string xmlname = "simpleType";

		// Token: 0x0400091C RID: 2332
		private static XmlSchemaSimpleType schemaLocationType = new XmlSchemaSimpleType
		{
			Content = new XmlSchemaSimpleTypeList
			{
				ItemTypeName = new XmlQualifiedName("anyURI", "http://www.w3.org/2001/XMLSchema")
			},
			BaseXmlSchemaTypeInternal = null,
			variety = XmlSchemaDerivationMethod.List
		};

		// Token: 0x0400091D RID: 2333
		private XmlSchemaSimpleTypeContent content;

		// Token: 0x0400091E RID: 2334
		internal bool islocal = true;

		// Token: 0x0400091F RID: 2335
		private bool recursed;

		// Token: 0x04000920 RID: 2336
		private XmlSchemaDerivationMethod variety;

		// Token: 0x04000921 RID: 2337
		internal static readonly XmlSchemaSimpleType XsAnySimpleType = XmlSchemaSimpleType.BuildSchemaType("anySimpleType", null);

		// Token: 0x04000922 RID: 2338
		internal static readonly XmlSchemaSimpleType XsString = XmlSchemaSimpleType.BuildSchemaType("string", "anySimpleType");

		// Token: 0x04000923 RID: 2339
		internal static readonly XmlSchemaSimpleType XsBoolean = XmlSchemaSimpleType.BuildSchemaType("boolean", "anySimpleType");

		// Token: 0x04000924 RID: 2340
		internal static readonly XmlSchemaSimpleType XsDecimal = XmlSchemaSimpleType.BuildSchemaType("decimal", "anySimpleType");

		// Token: 0x04000925 RID: 2341
		internal static readonly XmlSchemaSimpleType XsFloat = XmlSchemaSimpleType.BuildSchemaType("float", "anySimpleType");

		// Token: 0x04000926 RID: 2342
		internal static readonly XmlSchemaSimpleType XsDouble = XmlSchemaSimpleType.BuildSchemaType("double", "anySimpleType");

		// Token: 0x04000927 RID: 2343
		internal static readonly XmlSchemaSimpleType XsDuration = XmlSchemaSimpleType.BuildSchemaType("duration", "anySimpleType");

		// Token: 0x04000928 RID: 2344
		internal static readonly XmlSchemaSimpleType XsDateTime = XmlSchemaSimpleType.BuildSchemaType("dateTime", "anySimpleType");

		// Token: 0x04000929 RID: 2345
		internal static readonly XmlSchemaSimpleType XsTime = XmlSchemaSimpleType.BuildSchemaType("time", "anySimpleType");

		// Token: 0x0400092A RID: 2346
		internal static readonly XmlSchemaSimpleType XsDate = XmlSchemaSimpleType.BuildSchemaType("date", "anySimpleType");

		// Token: 0x0400092B RID: 2347
		internal static readonly XmlSchemaSimpleType XsGYearMonth = XmlSchemaSimpleType.BuildSchemaType("gYearMonth", "anySimpleType");

		// Token: 0x0400092C RID: 2348
		internal static readonly XmlSchemaSimpleType XsGYear = XmlSchemaSimpleType.BuildSchemaType("gYear", "anySimpleType");

		// Token: 0x0400092D RID: 2349
		internal static readonly XmlSchemaSimpleType XsGMonthDay = XmlSchemaSimpleType.BuildSchemaType("gMonthDay", "anySimpleType");

		// Token: 0x0400092E RID: 2350
		internal static readonly XmlSchemaSimpleType XsGDay = XmlSchemaSimpleType.BuildSchemaType("gDay", "anySimpleType");

		// Token: 0x0400092F RID: 2351
		internal static readonly XmlSchemaSimpleType XsGMonth = XmlSchemaSimpleType.BuildSchemaType("gMonth", "anySimpleType");

		// Token: 0x04000930 RID: 2352
		internal static readonly XmlSchemaSimpleType XsHexBinary = XmlSchemaSimpleType.BuildSchemaType("hexBinary", "anySimpleType");

		// Token: 0x04000931 RID: 2353
		internal static readonly XmlSchemaSimpleType XsBase64Binary = XmlSchemaSimpleType.BuildSchemaType("base64Binary", "anySimpleType");

		// Token: 0x04000932 RID: 2354
		internal static readonly XmlSchemaSimpleType XsAnyUri = XmlSchemaSimpleType.BuildSchemaType("anyURI", "anySimpleType");

		// Token: 0x04000933 RID: 2355
		internal static readonly XmlSchemaSimpleType XsQName = XmlSchemaSimpleType.BuildSchemaType("QName", "anySimpleType");

		// Token: 0x04000934 RID: 2356
		internal static readonly XmlSchemaSimpleType XsNotation = XmlSchemaSimpleType.BuildSchemaType("NOTATION", "anySimpleType");

		// Token: 0x04000935 RID: 2357
		internal static readonly XmlSchemaSimpleType XsNormalizedString = XmlSchemaSimpleType.BuildSchemaType("normalizedString", "string");

		// Token: 0x04000936 RID: 2358
		internal static readonly XmlSchemaSimpleType XsToken = XmlSchemaSimpleType.BuildSchemaType("token", "normalizedString");

		// Token: 0x04000937 RID: 2359
		internal static readonly XmlSchemaSimpleType XsLanguage = XmlSchemaSimpleType.BuildSchemaType("language", "token");

		// Token: 0x04000938 RID: 2360
		internal static readonly XmlSchemaSimpleType XsNMToken = XmlSchemaSimpleType.BuildSchemaType("NMTOKEN", "token");

		// Token: 0x04000939 RID: 2361
		internal static readonly XmlSchemaSimpleType XsNMTokens;

		// Token: 0x0400093A RID: 2362
		internal static readonly XmlSchemaSimpleType XsName = XmlSchemaSimpleType.BuildSchemaType("Name", "token");

		// Token: 0x0400093B RID: 2363
		internal static readonly XmlSchemaSimpleType XsNCName = XmlSchemaSimpleType.BuildSchemaType("NCName", "Name");

		// Token: 0x0400093C RID: 2364
		internal static readonly XmlSchemaSimpleType XsID = XmlSchemaSimpleType.BuildSchemaType("ID", "NCName");

		// Token: 0x0400093D RID: 2365
		internal static readonly XmlSchemaSimpleType XsIDRef = XmlSchemaSimpleType.BuildSchemaType("IDREF", "NCName");

		// Token: 0x0400093E RID: 2366
		internal static readonly XmlSchemaSimpleType XsIDRefs;

		// Token: 0x0400093F RID: 2367
		internal static readonly XmlSchemaSimpleType XsEntity = XmlSchemaSimpleType.BuildSchemaType("ENTITY", "NCName");

		// Token: 0x04000940 RID: 2368
		internal static readonly XmlSchemaSimpleType XsEntities;

		// Token: 0x04000941 RID: 2369
		internal static readonly XmlSchemaSimpleType XsInteger = XmlSchemaSimpleType.BuildSchemaType("integer", "decimal");

		// Token: 0x04000942 RID: 2370
		internal static readonly XmlSchemaSimpleType XsNonPositiveInteger = XmlSchemaSimpleType.BuildSchemaType("nonPositiveInteger", "integer");

		// Token: 0x04000943 RID: 2371
		internal static readonly XmlSchemaSimpleType XsNegativeInteger = XmlSchemaSimpleType.BuildSchemaType("negativeInteger", "nonPositiveInteger");

		// Token: 0x04000944 RID: 2372
		internal static readonly XmlSchemaSimpleType XsLong = XmlSchemaSimpleType.BuildSchemaType("long", "integer");

		// Token: 0x04000945 RID: 2373
		internal static readonly XmlSchemaSimpleType XsInt = XmlSchemaSimpleType.BuildSchemaType("int", "long");

		// Token: 0x04000946 RID: 2374
		internal static readonly XmlSchemaSimpleType XsShort = XmlSchemaSimpleType.BuildSchemaType("short", "int");

		// Token: 0x04000947 RID: 2375
		internal static readonly XmlSchemaSimpleType XsByte = XmlSchemaSimpleType.BuildSchemaType("byte", "short");

		// Token: 0x04000948 RID: 2376
		internal static readonly XmlSchemaSimpleType XsNonNegativeInteger = XmlSchemaSimpleType.BuildSchemaType("nonNegativeInteger", "integer");

		// Token: 0x04000949 RID: 2377
		internal static readonly XmlSchemaSimpleType XsUnsignedLong = XmlSchemaSimpleType.BuildSchemaType("unsignedLong", "nonNegativeInteger");

		// Token: 0x0400094A RID: 2378
		internal static readonly XmlSchemaSimpleType XsUnsignedInt = XmlSchemaSimpleType.BuildSchemaType("unsignedInt", "unsignedLong");

		// Token: 0x0400094B RID: 2379
		internal static readonly XmlSchemaSimpleType XsUnsignedShort = XmlSchemaSimpleType.BuildSchemaType("unsignedShort", "unsignedInt");

		// Token: 0x0400094C RID: 2380
		internal static readonly XmlSchemaSimpleType XsUnsignedByte = XmlSchemaSimpleType.BuildSchemaType("unsignedByte", "unsignedShort");

		// Token: 0x0400094D RID: 2381
		internal static readonly XmlSchemaSimpleType XsPositiveInteger = XmlSchemaSimpleType.BuildSchemaType("positiveInteger", "nonNegativeInteger");

		// Token: 0x0400094E RID: 2382
		internal static readonly XmlSchemaSimpleType XdtUntypedAtomic;

		// Token: 0x0400094F RID: 2383
		internal static readonly XmlSchemaSimpleType XdtAnyAtomicType = XmlSchemaSimpleType.BuildSchemaType("anyAtomicType", "anySimpleType", true, false);

		// Token: 0x04000950 RID: 2384
		internal static readonly XmlSchemaSimpleType XdtYearMonthDuration;

		// Token: 0x04000951 RID: 2385
		internal static readonly XmlSchemaSimpleType XdtDayTimeDuration;
	}
}
