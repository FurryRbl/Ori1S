using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C9 RID: 713
	internal class ListMap : ObjectMap
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0009CFD8 File Offset: 0x0009B1D8
		public bool IsMultiArray
		{
			get
			{
				return this.NestedArrayMapping != null;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0009CFE8 File Offset: 0x0009B1E8
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x0009CFF0 File Offset: 0x0009B1F0
		public string ChoiceMember
		{
			get
			{
				return this._choiceMember;
			}
			set
			{
				this._choiceMember = value;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0009CFFC File Offset: 0x0009B1FC
		public XmlTypeMapping NestedArrayMapping
		{
			get
			{
				if (this._gotNestedMapping)
				{
					return this._nestedArrayMapping;
				}
				this._gotNestedMapping = true;
				this._nestedArrayMapping = ((XmlTypeMapElementInfo)this._itemInfo[0]).MappedType;
				if (this._nestedArrayMapping == null)
				{
					return null;
				}
				if (this._nestedArrayMapping.TypeData.SchemaType != SchemaTypes.Array)
				{
					this._nestedArrayMapping = null;
					return null;
				}
				foreach (object obj in this._itemInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					if (xmlTypeMapElementInfo.MappedType != this._nestedArrayMapping)
					{
						this._nestedArrayMapping = null;
						return null;
					}
				}
				return this._nestedArrayMapping;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0009D0EC File Offset: 0x0009B2EC
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0009D0F4 File Offset: 0x0009B2F4
		public XmlTypeMapElementInfoList ItemInfo
		{
			get
			{
				return this._itemInfo;
			}
			set
			{
				this._itemInfo = value;
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0009D100 File Offset: 0x0009B300
		public XmlTypeMapElementInfo FindElement(object ob, int index, object memberValue)
		{
			if (this._itemInfo.Count == 1)
			{
				return (XmlTypeMapElementInfo)this._itemInfo[0];
			}
			if (this._choiceMember != null && index != -1)
			{
				Array array = (Array)XmlTypeMapMember.GetValue(ob, this._choiceMember);
				if (array == null || index >= array.Length)
				{
					throw new InvalidOperationException("Invalid or missing choice enum value in member '" + this._choiceMember + "'.");
				}
				object value = array.GetValue(index);
				foreach (object obj in this._itemInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					if (xmlTypeMapElementInfo.ChoiceValue != null && xmlTypeMapElementInfo.ChoiceValue.Equals(value))
					{
						return xmlTypeMapElementInfo;
					}
				}
			}
			else
			{
				if (memberValue == null)
				{
					return null;
				}
				Type type = memberValue.GetType();
				foreach (object obj2 in this._itemInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo2 = (XmlTypeMapElementInfo)obj2;
					if (xmlTypeMapElementInfo2.TypeData.Type == type)
					{
						return xmlTypeMapElementInfo2;
					}
				}
			}
			return null;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0009D2A0 File Offset: 0x0009B4A0
		public XmlTypeMapElementInfo FindElement(string elementName, string ns)
		{
			foreach (object obj in this._itemInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				if (xmlTypeMapElementInfo.ElementName == elementName && xmlTypeMapElementInfo.Namespace == ns)
				{
					return xmlTypeMapElementInfo;
				}
			}
			return null;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0009D334 File Offset: 0x0009B534
		public XmlTypeMapElementInfo FindTextElement()
		{
			foreach (object obj in this._itemInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				if (xmlTypeMapElementInfo.IsTextElement)
				{
					return xmlTypeMapElementInfo;
				}
			}
			return null;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0009D3B4 File Offset: 0x0009B5B4
		public string GetSchemaArrayName()
		{
			XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)this._itemInfo[0];
			if (xmlTypeMapElementInfo.MappedType != null)
			{
				return TypeTranslator.GetArrayName(xmlTypeMapElementInfo.MappedType.XmlType);
			}
			return TypeTranslator.GetArrayName(xmlTypeMapElementInfo.TypeData.XmlType);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0009D400 File Offset: 0x0009B600
		public void GetArrayType(int itemCount, out string localName, out string ns)
		{
			string str;
			if (itemCount != -1)
			{
				str = "[" + itemCount + "]";
			}
			else
			{
				str = "[]";
			}
			XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)this._itemInfo[0];
			if (xmlTypeMapElementInfo.TypeData.SchemaType == SchemaTypes.Array)
			{
				string str2;
				((ListMap)xmlTypeMapElementInfo.MappedType.ObjectMap).GetArrayType(-1, out str2, out ns);
				localName = str2 + str;
			}
			else if (xmlTypeMapElementInfo.MappedType != null)
			{
				localName = xmlTypeMapElementInfo.MappedType.XmlType + str;
				ns = xmlTypeMapElementInfo.MappedType.Namespace;
			}
			else
			{
				localName = xmlTypeMapElementInfo.TypeData.XmlType + str;
				ns = xmlTypeMapElementInfo.DataTypeNamespace;
			}
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0009D4CC File Offset: 0x0009B6CC
		public override bool Equals(object other)
		{
			ListMap listMap = other as ListMap;
			if (listMap == null)
			{
				return false;
			}
			if (this._itemInfo.Count != listMap._itemInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this._itemInfo.Count; i++)
			{
				if (!this._itemInfo[i].Equals(listMap._itemInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0009D548 File Offset: 0x0009B748
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000BE5 RID: 3045
		private XmlTypeMapElementInfoList _itemInfo;

		// Token: 0x04000BE6 RID: 3046
		private bool _gotNestedMapping;

		// Token: 0x04000BE7 RID: 3047
		private XmlTypeMapping _nestedArrayMapping;

		// Token: 0x04000BE8 RID: 3048
		private string _choiceMember;
	}
}
