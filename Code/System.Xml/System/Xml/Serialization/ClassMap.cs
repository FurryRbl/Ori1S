using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002C8 RID: 712
	internal class ClassMap : ObjectMap
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0009C890 File Offset: 0x0009AA90
		public void AddMember(XmlTypeMapMember member)
		{
			member.GlobalIndex = this._allMembers.Count;
			this._allMembers.Add(member);
			if (!(member.DefaultValue is DBNull) && member.DefaultValue != null)
			{
				if (this._membersWithDefault == null)
				{
					this._membersWithDefault = new ArrayList();
				}
				this._membersWithDefault.Add(member);
			}
			if (member.IsReturnValue)
			{
				this._returnMember = member;
			}
			if (!(member is XmlTypeMapMemberAttribute))
			{
				if (member is XmlTypeMapMemberFlatList)
				{
					this.RegisterFlatList((XmlTypeMapMemberFlatList)member);
				}
				else if (member is XmlTypeMapMemberAnyElement)
				{
					XmlTypeMapMemberAnyElement xmlTypeMapMemberAnyElement = (XmlTypeMapMemberAnyElement)member;
					if (xmlTypeMapMemberAnyElement.IsDefaultAny)
					{
						this._defaultAnyElement = xmlTypeMapMemberAnyElement;
					}
					if (xmlTypeMapMemberAnyElement.TypeData.IsListType)
					{
						this.RegisterFlatList(xmlTypeMapMemberAnyElement);
					}
				}
				else
				{
					if (member is XmlTypeMapMemberAnyAttribute)
					{
						this._defaultAnyAttribute = (XmlTypeMapMemberAnyAttribute)member;
						return;
					}
					if (member is XmlTypeMapMemberNamespaces)
					{
						this._namespaceDeclarations = (XmlTypeMapMemberNamespaces)member;
						return;
					}
				}
				if (member is XmlTypeMapMemberElement && ((XmlTypeMapMemberElement)member).IsXmlTextCollector)
				{
					if (this._xmlTextCollector != null)
					{
						throw new InvalidOperationException("XmlTextAttribute can only be applied once in a class");
					}
					this._xmlTextCollector = member;
				}
				if (this._elementMembers == null)
				{
					this._elementMembers = new ArrayList();
					this._elements = new Hashtable();
				}
				member.Index = this._elementMembers.Count;
				this._elementMembers.Add(member);
				ICollection elementInfo = ((XmlTypeMapMemberElement)member).ElementInfo;
				foreach (object obj in elementInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					string key = this.BuildKey(xmlTypeMapElementInfo.ElementName, xmlTypeMapElementInfo.Namespace);
					if (this._elements.ContainsKey(key))
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"The XML element named '",
							xmlTypeMapElementInfo.ElementName,
							"' from namespace '",
							xmlTypeMapElementInfo.Namespace,
							"' is already present in the current scope. Use XML attributes to specify another XML name or namespace for the element."
						}));
					}
					this._elements.Add(key, xmlTypeMapElementInfo);
				}
				if (member.TypeData.IsListType && member.TypeData.Type != null && !member.TypeData.Type.IsArray)
				{
					if (this._listMembers == null)
					{
						this._listMembers = new ArrayList();
					}
					this._listMembers.Add(member);
				}
				return;
			}
			XmlTypeMapMemberAttribute xmlTypeMapMemberAttribute = (XmlTypeMapMemberAttribute)member;
			if (this._attributeMembers == null)
			{
				this._attributeMembers = new Hashtable();
			}
			string key2 = this.BuildKey(xmlTypeMapMemberAttribute.AttributeName, xmlTypeMapMemberAttribute.Namespace);
			if (this._attributeMembers.ContainsKey(key2))
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"The XML attribute named '",
					xmlTypeMapMemberAttribute.AttributeName,
					"' from namespace '",
					xmlTypeMapMemberAttribute.Namespace,
					"' is already present in the current scope. Use XML attributes to specify another XML name or namespace for the attribute."
				}));
			}
			member.Index = this._attributeMembers.Count;
			this._attributeMembers.Add(key2, member);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0009CBEC File Offset: 0x0009ADEC
		private void RegisterFlatList(XmlTypeMapMemberExpandable member)
		{
			if (this._flatLists == null)
			{
				this._flatLists = new ArrayList();
			}
			member.FlatArrayIndex = this._flatLists.Count;
			this._flatLists.Add(member);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0009CC30 File Offset: 0x0009AE30
		public XmlTypeMapMemberAttribute GetAttribute(string name, string ns)
		{
			if (this._attributeMembers == null)
			{
				return null;
			}
			return (XmlTypeMapMemberAttribute)this._attributeMembers[this.BuildKey(name, ns)];
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0009CC58 File Offset: 0x0009AE58
		public XmlTypeMapElementInfo GetElement(string name, string ns)
		{
			if (this._elements == null)
			{
				return null;
			}
			return (XmlTypeMapElementInfo)this._elements[this.BuildKey(name, ns)];
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0009CC80 File Offset: 0x0009AE80
		public XmlTypeMapElementInfo GetElement(int index)
		{
			if (this._elements == null)
			{
				return null;
			}
			if (this._elementsByIndex == null)
			{
				this._elementsByIndex = new XmlTypeMapElementInfo[this._elementMembers.Count];
				foreach (object obj in this._elementMembers)
				{
					XmlTypeMapMemberElement xmlTypeMapMemberElement = (XmlTypeMapMemberElement)obj;
					if (xmlTypeMapMemberElement.ElementInfo.Count != 1)
					{
						throw new InvalidOperationException("Read by order only possible for encoded/bare format");
					}
					this._elementsByIndex[xmlTypeMapMemberElement.Index] = (XmlTypeMapElementInfo)xmlTypeMapMemberElement.ElementInfo[0];
				}
			}
			return this._elementsByIndex[index];
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0009CD58 File Offset: 0x0009AF58
		private string BuildKey(string name, string ns)
		{
			if (this._ignoreMemberNamespace)
			{
				return name;
			}
			return name + " / " + ns;
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0009CD74 File Offset: 0x0009AF74
		public ICollection AllElementInfos
		{
			get
			{
				return this._elements.Values;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0009CD84 File Offset: 0x0009AF84
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x0009CD8C File Offset: 0x0009AF8C
		public bool IgnoreMemberNamespace
		{
			get
			{
				return this._ignoreMemberNamespace;
			}
			set
			{
				this._ignoreMemberNamespace = value;
			}
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0009CD98 File Offset: 0x0009AF98
		public XmlTypeMapMember FindMember(string name)
		{
			for (int i = 0; i < this._allMembers.Count; i++)
			{
				if (((XmlTypeMapMember)this._allMembers[i]).Name == name)
				{
					return (XmlTypeMapMember)this._allMembers[i];
				}
			}
			return null;
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0009CDF8 File Offset: 0x0009AFF8
		public XmlTypeMapMemberAnyElement DefaultAnyElementMember
		{
			get
			{
				return this._defaultAnyElement;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0009CE00 File Offset: 0x0009B000
		public XmlTypeMapMemberAnyAttribute DefaultAnyAttributeMember
		{
			get
			{
				return this._defaultAnyAttribute;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0009CE08 File Offset: 0x0009B008
		public XmlTypeMapMemberNamespaces NamespaceDeclarations
		{
			get
			{
				return this._namespaceDeclarations;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0009CE10 File Offset: 0x0009B010
		public ICollection AttributeMembers
		{
			get
			{
				if (this._attributeMembers == null)
				{
					return null;
				}
				if (this._attributeMembersArray != null)
				{
					return this._attributeMembersArray;
				}
				this._attributeMembersArray = new XmlTypeMapMemberAttribute[this._attributeMembers.Count];
				foreach (object obj in this._attributeMembers.Values)
				{
					XmlTypeMapMemberAttribute xmlTypeMapMemberAttribute = (XmlTypeMapMemberAttribute)obj;
					this._attributeMembersArray[xmlTypeMapMemberAttribute.Index] = xmlTypeMapMemberAttribute;
				}
				return this._attributeMembersArray;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0009CEC8 File Offset: 0x0009B0C8
		public ICollection ElementMembers
		{
			get
			{
				return this._elementMembers;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0009CED0 File Offset: 0x0009B0D0
		public ArrayList AllMembers
		{
			get
			{
				return this._allMembers;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0009CED8 File Offset: 0x0009B0D8
		public ArrayList FlatLists
		{
			get
			{
				return this._flatLists;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0009CEE0 File Offset: 0x0009B0E0
		public ArrayList MembersWithDefault
		{
			get
			{
				return this._membersWithDefault;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0009CEE8 File Offset: 0x0009B0E8
		public ArrayList ListMembers
		{
			get
			{
				return this._listMembers;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0009CEF0 File Offset: 0x0009B0F0
		public XmlTypeMapMember XmlTextCollector
		{
			get
			{
				return this._xmlTextCollector;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0009CEF8 File Offset: 0x0009B0F8
		public XmlTypeMapMember ReturnMember
		{
			get
			{
				return this._returnMember;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0009CF00 File Offset: 0x0009B100
		public XmlQualifiedName SimpleContentBaseType
		{
			get
			{
				if (!this._canBeSimpleType || this._elementMembers == null || this._elementMembers.Count != 1)
				{
					return null;
				}
				XmlTypeMapMemberElement xmlTypeMapMemberElement = (XmlTypeMapMemberElement)this._elementMembers[0];
				if (xmlTypeMapMemberElement.ElementInfo.Count != 1)
				{
					return null;
				}
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)xmlTypeMapMemberElement.ElementInfo[0];
				if (!xmlTypeMapElementInfo.IsTextElement)
				{
					return null;
				}
				if (xmlTypeMapMemberElement.TypeData.SchemaType == SchemaTypes.Primitive || xmlTypeMapMemberElement.TypeData.SchemaType == SchemaTypes.Enum)
				{
					return new XmlQualifiedName(xmlTypeMapElementInfo.TypeData.XmlType, xmlTypeMapElementInfo.DataTypeNamespace);
				}
				return null;
			}
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0009CFB4 File Offset: 0x0009B1B4
		public void SetCanBeSimpleType(bool can)
		{
			this._canBeSimpleType = can;
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0009CFC0 File Offset: 0x0009B1C0
		public bool HasSimpleContent
		{
			get
			{
				return this.SimpleContentBaseType != null;
			}
		}

		// Token: 0x04000BD5 RID: 3029
		private Hashtable _elements = new Hashtable();

		// Token: 0x04000BD6 RID: 3030
		private ArrayList _elementMembers;

		// Token: 0x04000BD7 RID: 3031
		private Hashtable _attributeMembers;

		// Token: 0x04000BD8 RID: 3032
		private XmlTypeMapMemberAttribute[] _attributeMembersArray;

		// Token: 0x04000BD9 RID: 3033
		private XmlTypeMapElementInfo[] _elementsByIndex;

		// Token: 0x04000BDA RID: 3034
		private ArrayList _flatLists;

		// Token: 0x04000BDB RID: 3035
		private ArrayList _allMembers = new ArrayList();

		// Token: 0x04000BDC RID: 3036
		private ArrayList _membersWithDefault;

		// Token: 0x04000BDD RID: 3037
		private ArrayList _listMembers;

		// Token: 0x04000BDE RID: 3038
		private XmlTypeMapMemberAnyElement _defaultAnyElement;

		// Token: 0x04000BDF RID: 3039
		private XmlTypeMapMemberAnyAttribute _defaultAnyAttribute;

		// Token: 0x04000BE0 RID: 3040
		private XmlTypeMapMemberNamespaces _namespaceDeclarations;

		// Token: 0x04000BE1 RID: 3041
		private XmlTypeMapMember _xmlTextCollector;

		// Token: 0x04000BE2 RID: 3042
		private XmlTypeMapMember _returnMember;

		// Token: 0x04000BE3 RID: 3043
		private bool _ignoreMemberNamespace;

		// Token: 0x04000BE4 RID: 3044
		private bool _canBeSimpleType = true;
	}
}
