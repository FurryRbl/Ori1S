using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000C9 RID: 201
	internal class DTDAttributeDefinition : DTDNode
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x0002671C File Offset: 0x0002491C
		internal DTDAttributeDefinition(DTDObjectModel root)
		{
			base.SetRoot(root);
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0002672C File Offset: 0x0002492C
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00026734 File Offset: 0x00024934
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00026740 File Offset: 0x00024940
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00026748 File Offset: 0x00024948
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
			set
			{
				this.datatype = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00026754 File Offset: 0x00024954
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0002675C File Offset: 0x0002495C
		public DTDAttributeOccurenceType OccurenceType
		{
			get
			{
				return this.occurenceType;
			}
			set
			{
				this.occurenceType = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00026768 File Offset: 0x00024968
		public ArrayList EnumeratedAttributeDeclaration
		{
			get
			{
				if (this.enumeratedLiterals == null)
				{
					this.enumeratedLiterals = new ArrayList();
				}
				return this.enumeratedLiterals;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00026788 File Offset: 0x00024988
		public ArrayList EnumeratedNotations
		{
			get
			{
				if (this.enumeratedNotations == null)
				{
					this.enumeratedNotations = new ArrayList();
				}
				return this.enumeratedNotations;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000267A8 File Offset: 0x000249A8
		public string DefaultValue
		{
			get
			{
				if (this.resolvedDefaultValue == null)
				{
					this.resolvedDefaultValue = this.ComputeDefaultValue();
				}
				return this.resolvedDefaultValue;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000267C8 File Offset: 0x000249C8
		public string NormalizedDefaultValue
		{
			get
			{
				if (this.resolvedNormalizedDefaultValue == null)
				{
					string s = this.ComputeDefaultValue();
					try
					{
						object obj = this.Datatype.ParseValue(s, null, null);
						this.resolvedNormalizedDefaultValue = ((!(obj is string[])) ? ((!(obj is IFormattable)) ? obj.ToString() : ((IFormattable)obj).ToString(null, CultureInfo.InvariantCulture)) : string.Join(" ", (string[])obj));
					}
					catch (Exception)
					{
						this.resolvedNormalizedDefaultValue = this.Datatype.Normalize(s);
					}
				}
				return this.resolvedNormalizedDefaultValue;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00026884 File Offset: 0x00024A84
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0002688C File Offset: 0x00024A8C
		public string UnresolvedDefaultValue
		{
			get
			{
				return this.unresolvedDefault;
			}
			set
			{
				this.unresolvedDefault = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00026898 File Offset: 0x00024A98
		public char QuoteChar
		{
			get
			{
				return (this.UnresolvedDefaultValue.Length <= 0) ? '"' : this.UnresolvedDefaultValue[0];
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000268CC File Offset: 0x00024ACC
		internal XmlSchemaAttribute CreateXsdAttribute()
		{
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			base.SetLineInfo(xmlSchemaAttribute);
			xmlSchemaAttribute.Name = this.Name;
			xmlSchemaAttribute.DefaultValue = this.resolvedNormalizedDefaultValue;
			if (this.OccurenceType != DTDAttributeOccurenceType.Required)
			{
				xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
			}
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
			ArrayList arrayList = null;
			if (this.enumeratedNotations != null && this.enumeratedNotations.Count > 0)
			{
				xmlQualifiedName = new XmlQualifiedName("NOTATION", "http://www.w3.org/2001/XMLSchema");
				arrayList = this.enumeratedNotations;
			}
			else if (this.enumeratedLiterals != null)
			{
				arrayList = this.enumeratedLiterals;
			}
			else
			{
				switch (this.Datatype.TokenizedType)
				{
				case XmlTokenizedType.ID:
					xmlQualifiedName = new XmlQualifiedName("ID", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.IDREF:
					xmlQualifiedName = new XmlQualifiedName("IDREF", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.IDREFS:
					xmlQualifiedName = new XmlQualifiedName("IDREFS", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.ENTITY:
					xmlQualifiedName = new XmlQualifiedName("ENTITY", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.ENTITIES:
					xmlQualifiedName = new XmlQualifiedName("ENTITIES", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.NMTOKEN:
					xmlQualifiedName = new XmlQualifiedName("NMTOKEN", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.NMTOKENS:
					xmlQualifiedName = new XmlQualifiedName("NMTOKENS", "http://www.w3.org/2001/XMLSchema");
					break;
				case XmlTokenizedType.NOTATION:
					xmlQualifiedName = new XmlQualifiedName("NOTATION", "http://www.w3.org/2001/XMLSchema");
					break;
				}
			}
			if (arrayList != null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
				base.SetLineInfo(xmlSchemaSimpleType);
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
				base.SetLineInfo(xmlSchemaSimpleTypeRestriction);
				xmlSchemaSimpleTypeRestriction.BaseTypeName = xmlQualifiedName;
				if (this.enumeratedNotations != null)
				{
					foreach (object obj in this.enumeratedNotations)
					{
						string value = (string)obj;
						XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
						base.SetLineInfo(xmlSchemaEnumerationFacet);
						xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
						xmlSchemaEnumerationFacet.Value = value;
					}
				}
				xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
			}
			else if (xmlQualifiedName != XmlQualifiedName.Empty)
			{
				xmlSchemaAttribute.SchemaTypeName = xmlQualifiedName;
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00026B30 File Offset: 0x00024D30
		internal string ComputeDefaultValue()
		{
			if (this.UnresolvedDefaultValue == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int startIndex = 0;
			string unresolvedDefaultValue = this.UnresolvedDefaultValue;
			int num;
			while ((num = unresolvedDefaultValue.IndexOf('&', startIndex)) >= 0)
			{
				int num2 = unresolvedDefaultValue.IndexOf(';', num);
				if (unresolvedDefaultValue[num + 1] == '#')
				{
					char c = unresolvedDefaultValue[num + 2];
					NumberStyles numberStyles = NumberStyles.Integer;
					string s;
					if (c == 'x' || c == 'X')
					{
						s = unresolvedDefaultValue.Substring(num + 3, num2 - num - 3);
						numberStyles |= NumberStyles.HexNumber;
					}
					else
					{
						s = unresolvedDefaultValue.Substring(num + 2, num2 - num - 2);
					}
					stringBuilder.Append((char)int.Parse(s, numberStyles, CultureInfo.InvariantCulture));
				}
				else
				{
					stringBuilder.Append(unresolvedDefaultValue.Substring(startIndex, num - 1));
					string text = unresolvedDefaultValue.Substring(num + 1, num2 - 2);
					int predefinedEntity = XmlChar.GetPredefinedEntity(text);
					if (predefinedEntity >= 0)
					{
						stringBuilder.Append(predefinedEntity);
					}
					else
					{
						stringBuilder.Append(base.Root.ResolveEntity(text));
					}
				}
				startIndex = num2 + 1;
			}
			stringBuilder.Append(unresolvedDefaultValue.Substring(startIndex));
			string result = stringBuilder.ToString(1, stringBuilder.Length - 2);
			stringBuilder.Length = 0;
			return result;
		}

		// Token: 0x04000406 RID: 1030
		private string name;

		// Token: 0x04000407 RID: 1031
		private XmlSchemaDatatype datatype;

		// Token: 0x04000408 RID: 1032
		private ArrayList enumeratedLiterals;

		// Token: 0x04000409 RID: 1033
		private string unresolvedDefault;

		// Token: 0x0400040A RID: 1034
		private ArrayList enumeratedNotations;

		// Token: 0x0400040B RID: 1035
		private DTDAttributeOccurenceType occurenceType;

		// Token: 0x0400040C RID: 1036
		private string resolvedDefaultValue;

		// Token: 0x0400040D RID: 1037
		private string resolvedNormalizedDefaultValue;
	}
}
