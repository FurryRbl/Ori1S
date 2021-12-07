using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002AA RID: 682
	internal class XmlSerializationReaderInterpreter : XmlSerializationReader
	{
		// Token: 0x06001C33 RID: 7219 RVA: 0x000956F8 File Offset: 0x000938F8
		public XmlSerializationReaderInterpreter(XmlMapping typeMap)
		{
			this._typeMap = typeMap;
			this._format = typeMap.Format;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00095738 File Offset: 0x00093938
		protected override void InitCallbacks()
		{
			ArrayList relatedMaps = this._typeMap.RelatedMaps;
			if (relatedMaps != null)
			{
				foreach (object obj in relatedMaps)
				{
					XmlTypeMapping xmlTypeMapping = (XmlTypeMapping)obj;
					if (xmlTypeMapping.TypeData.SchemaType == SchemaTypes.Class || xmlTypeMapping.TypeData.SchemaType == SchemaTypes.Enum)
					{
						XmlSerializationReaderInterpreter.ReaderCallbackInfo @object = new XmlSerializationReaderInterpreter.ReaderCallbackInfo(this, xmlTypeMapping);
						base.AddReadCallback(xmlTypeMapping.XmlType, xmlTypeMapping.Namespace, xmlTypeMapping.TypeData.Type, new XmlSerializationReadCallback(@object.ReadObject));
					}
				}
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00095804 File Offset: 0x00093A04
		protected override void InitIDs()
		{
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00095808 File Offset: 0x00093A08
		protected XmlTypeMapping GetTypeMap(Type type)
		{
			ArrayList relatedMaps = this._typeMap.RelatedMaps;
			if (relatedMaps != null)
			{
				foreach (object obj in relatedMaps)
				{
					XmlTypeMapping xmlTypeMapping = (XmlTypeMapping)obj;
					if (xmlTypeMapping.TypeData.Type == type)
					{
						return xmlTypeMapping;
					}
				}
			}
			throw new InvalidOperationException("Type " + type + " not mapped");
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000958B0 File Offset: 0x00093AB0
		public object ReadRoot()
		{
			base.Reader.MoveToContent();
			if (!(this._typeMap is XmlTypeMapping))
			{
				return this.ReadMessage((XmlMembersMapping)this._typeMap);
			}
			if (this._format == SerializationFormat.Literal)
			{
				return this.ReadRoot((XmlTypeMapping)this._typeMap);
			}
			return this.ReadEncodedObject((XmlTypeMapping)this._typeMap);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0009591C File Offset: 0x00093B1C
		private object ReadEncodedObject(XmlTypeMapping typeMap)
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!(base.Reader.LocalName == typeMap.ElementName) || !(base.Reader.NamespaceURI == typeMap.Namespace))
				{
					throw base.CreateUnknownNodeException();
				}
				result = base.ReadReferencedElement();
			}
			else
			{
				base.UnknownNode(null);
			}
			base.ReadReferencedElements();
			return result;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000959A4 File Offset: 0x00093BA4
		protected virtual object ReadMessage(XmlMembersMapping typeMap)
		{
			object[] array = new object[typeMap.Count];
			if (typeMap.HasWrapperElement)
			{
				ArrayList allMembers = ((ClassMap)typeMap.ObjectMap).AllMembers;
				for (int i = 0; i < allMembers.Count; i++)
				{
					XmlTypeMapMember xmlTypeMapMember = (XmlTypeMapMember)allMembers[i];
					if (!xmlTypeMapMember.IsReturnValue && xmlTypeMapMember.TypeData.IsValueType)
					{
						this.SetMemberValueFromAttr(xmlTypeMapMember, array, this.CreateInstance(xmlTypeMapMember.TypeData.Type), true);
					}
				}
				if (this._format == SerializationFormat.Encoded)
				{
					while (base.Reader.NodeType == XmlNodeType.Element)
					{
						string attribute = base.Reader.GetAttribute("root", "http://schemas.xmlsoap.org/soap/encoding/");
						if (attribute == null || XmlConvert.ToBoolean(attribute))
						{
							break;
						}
						base.ReadReferencedElement();
						base.Reader.MoveToContent();
					}
				}
				while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.ReadState == ReadState.Interactive)
				{
					if (base.Reader.IsStartElement(typeMap.ElementName, typeMap.Namespace) || this._format == SerializationFormat.Encoded)
					{
						this.ReadAttributeMembers((ClassMap)typeMap.ObjectMap, array, true);
						if (!base.Reader.IsEmptyElement)
						{
							base.Reader.ReadStartElement();
							this.ReadMembers((ClassMap)typeMap.ObjectMap, array, true, false);
							base.ReadEndElement();
							break;
						}
						base.Reader.Skip();
						base.Reader.MoveToContent();
					}
					else
					{
						base.UnknownNode(null);
						base.Reader.MoveToContent();
					}
				}
			}
			else
			{
				this.ReadMembers((ClassMap)typeMap.ObjectMap, array, true, this._format == SerializationFormat.Encoded);
			}
			if (this._format == SerializationFormat.Encoded)
			{
				base.ReadReferencedElements();
			}
			return array;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00095B94 File Offset: 0x00093D94
		private object ReadRoot(XmlTypeMapping rootMap)
		{
			if (rootMap.TypeData.SchemaType == SchemaTypes.XmlNode)
			{
				return this.ReadXmlNodeElement(rootMap, true);
			}
			if (base.Reader.LocalName != rootMap.ElementName || base.Reader.NamespaceURI != rootMap.Namespace)
			{
				throw base.CreateUnknownNodeException();
			}
			return this.ReadObject(rootMap, rootMap.IsNullable, true);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00095C08 File Offset: 0x00093E08
		protected virtual object ReadObject(XmlTypeMapping typeMap, bool isNullable, bool checkType)
		{
			switch (typeMap.TypeData.SchemaType)
			{
			case SchemaTypes.Primitive:
				return this.ReadPrimitiveElement(typeMap, isNullable);
			case SchemaTypes.Enum:
				return this.ReadEnumElement(typeMap, isNullable);
			case SchemaTypes.Array:
				return this.ReadListElement(typeMap, isNullable, null, true);
			case SchemaTypes.Class:
				return this.ReadClassInstance(typeMap, isNullable, checkType);
			case SchemaTypes.XmlSerializable:
				return this.ReadXmlSerializableElement(typeMap, isNullable);
			case SchemaTypes.XmlNode:
				return this.ReadXmlNodeElement(typeMap, isNullable);
			default:
				throw new Exception("Unsupported map type");
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00095C8C File Offset: 0x00093E8C
		protected virtual object ReadClassInstance(XmlTypeMapping typeMap, bool isNullable, bool checkType)
		{
			if (isNullable && base.ReadNull())
			{
				return null;
			}
			if (checkType)
			{
				XmlQualifiedName xsiType = base.GetXsiType();
				if (xsiType != null)
				{
					XmlTypeMapping realElementMap = typeMap.GetRealElementMap(xsiType.Name, xsiType.Namespace);
					if (realElementMap == null)
					{
						if (typeMap.TypeData.Type == typeof(object))
						{
							return base.ReadTypedPrimitive(xsiType);
						}
						throw base.CreateUnknownTypeException(xsiType);
					}
					else if (realElementMap != typeMap)
					{
						return this.ReadObject(realElementMap, false, false);
					}
				}
				else if (typeMap.TypeData.Type == typeof(object))
				{
					return base.ReadTypedPrimitive(XmlSerializationReaderInterpreter.AnyType);
				}
			}
			object obj = Activator.CreateInstance(typeMap.TypeData.Type, true);
			base.Reader.MoveToElement();
			bool isEmptyElement = base.Reader.IsEmptyElement;
			this.ReadClassInstanceMembers(typeMap, obj);
			if (isEmptyElement)
			{
				base.Reader.Skip();
			}
			else
			{
				base.ReadEndElement();
			}
			return obj;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00095D94 File Offset: 0x00093F94
		protected virtual void ReadClassInstanceMembers(XmlTypeMapping typeMap, object ob)
		{
			this.ReadMembers((ClassMap)typeMap.ObjectMap, ob, false, false);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00095DAC File Offset: 0x00093FAC
		private void ReadAttributeMembers(ClassMap map, object ob, bool isValueList)
		{
			XmlTypeMapMember defaultAnyAttributeMember = map.DefaultAnyAttributeMember;
			int length = 0;
			object obj = null;
			while (base.Reader.MoveToNextAttribute())
			{
				XmlTypeMapMemberAttribute attribute = map.GetAttribute(base.Reader.LocalName, base.Reader.NamespaceURI);
				if (attribute != null)
				{
					this.SetMemberValue(attribute, ob, this.GetValueFromXmlString(base.Reader.Value, attribute.TypeData, attribute.MappedType), isValueList);
				}
				else if (base.IsXmlnsAttribute(base.Reader.Name))
				{
					if (map.NamespaceDeclarations != null)
					{
						XmlSerializerNamespaces xmlSerializerNamespaces = this.GetMemberValue(map.NamespaceDeclarations, ob, isValueList) as XmlSerializerNamespaces;
						if (xmlSerializerNamespaces == null)
						{
							xmlSerializerNamespaces = new XmlSerializerNamespaces();
							this.SetMemberValue(map.NamespaceDeclarations, ob, xmlSerializerNamespaces, isValueList);
						}
						if (base.Reader.Prefix == "xmlns")
						{
							xmlSerializerNamespaces.Add(base.Reader.LocalName, base.Reader.Value);
						}
						else
						{
							xmlSerializerNamespaces.Add(string.Empty, base.Reader.Value);
						}
					}
				}
				else if (defaultAnyAttributeMember != null)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
					base.ParseWsdlArrayType(xmlAttribute);
					this.AddListValue(defaultAnyAttributeMember.TypeData, ref obj, length++, xmlAttribute, true);
				}
				else
				{
					this.ProcessUnknownAttribute(ob);
				}
			}
			if (defaultAnyAttributeMember != null)
			{
				obj = base.ShrinkArray((Array)obj, length, defaultAnyAttributeMember.TypeData.Type.GetElementType(), true);
				this.SetMemberValue(defaultAnyAttributeMember, ob, obj, isValueList);
			}
			base.Reader.MoveToElement();
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00095F58 File Offset: 0x00094158
		private void ReadMembers(ClassMap map, object ob, bool isValueList, bool readByOrder)
		{
			this.ReadAttributeMembers(map, ob, isValueList);
			if (!isValueList)
			{
				base.Reader.MoveToElement();
				if (base.Reader.IsEmptyElement)
				{
					this.SetListMembersDefaults(map, ob, isValueList);
					return;
				}
				base.Reader.ReadStartElement();
			}
			bool[] array = new bool[(map.ElementMembers == null) ? 0 : map.ElementMembers.Count];
			bool flag = isValueList && this._format == SerializationFormat.Encoded && map.ReturnMember != null;
			base.Reader.MoveToContent();
			int[] array2 = null;
			object[] array3 = null;
			object[] array4 = null;
			XmlSerializationReader.Fixup fixup = null;
			int num = 0;
			int num2;
			if (readByOrder)
			{
				if (map.ElementMembers != null)
				{
					num2 = map.ElementMembers.Count;
				}
				else
				{
					num2 = 0;
				}
			}
			else
			{
				num2 = int.MaxValue;
			}
			if (map.FlatLists != null)
			{
				array2 = new int[map.FlatLists.Count];
				array3 = new object[map.FlatLists.Count];
				foreach (object obj in map.FlatLists)
				{
					XmlTypeMapMemberExpandable xmlTypeMapMemberExpandable = (XmlTypeMapMemberExpandable)obj;
					if (this.IsReadOnly(xmlTypeMapMemberExpandable, xmlTypeMapMemberExpandable.TypeData, ob, isValueList))
					{
						array3[xmlTypeMapMemberExpandable.FlatArrayIndex] = xmlTypeMapMemberExpandable.GetValue(ob);
					}
					else if (xmlTypeMapMemberExpandable.TypeData.Type.IsArray)
					{
						array3[xmlTypeMapMemberExpandable.FlatArrayIndex] = this.InitializeList(xmlTypeMapMemberExpandable.TypeData);
					}
					else
					{
						object obj2 = xmlTypeMapMemberExpandable.GetValue(ob);
						if (obj2 == null)
						{
							obj2 = this.InitializeList(xmlTypeMapMemberExpandable.TypeData);
							this.SetMemberValue(xmlTypeMapMemberExpandable, ob, obj2, isValueList);
						}
						array3[xmlTypeMapMemberExpandable.FlatArrayIndex] = obj2;
					}
					if (xmlTypeMapMemberExpandable.ChoiceMember != null)
					{
						if (array4 == null)
						{
							array4 = new object[map.FlatLists.Count];
						}
						array4[xmlTypeMapMemberExpandable.FlatArrayIndex] = this.InitializeList(xmlTypeMapMemberExpandable.ChoiceTypeData);
					}
				}
			}
			if (this._format == SerializationFormat.Encoded && map.ElementMembers != null)
			{
				XmlSerializationReaderInterpreter.FixupCallbackInfo @object = new XmlSerializationReaderInterpreter.FixupCallbackInfo(this, map, isValueList);
				fixup = new XmlSerializationReader.Fixup(ob, new XmlSerializationFixupCallback(@object.FixupMembers), map.ElementMembers.Count);
				base.AddFixup(fixup);
			}
			while (base.Reader.NodeType != XmlNodeType.EndElement && num < num2)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo;
					if (readByOrder)
					{
						xmlTypeMapElementInfo = map.GetElement(num++);
					}
					else if (flag)
					{
						xmlTypeMapElementInfo = (XmlTypeMapElementInfo)((XmlTypeMapMemberElement)map.ReturnMember).ElementInfo[0];
						flag = false;
					}
					else
					{
						xmlTypeMapElementInfo = map.GetElement(base.Reader.LocalName, base.Reader.NamespaceURI);
					}
					if (xmlTypeMapElementInfo != null && !array[xmlTypeMapElementInfo.Member.Index])
					{
						if (xmlTypeMapElementInfo.Member.GetType() == typeof(XmlTypeMapMemberList))
						{
							if (this._format == SerializationFormat.Encoded && xmlTypeMapElementInfo.MultiReferenceType)
							{
								object obj3 = base.ReadReferencingElement(out fixup.Ids[xmlTypeMapElementInfo.Member.Index]);
								if (fixup.Ids[xmlTypeMapElementInfo.Member.Index] == null)
								{
									if (this.IsReadOnly(xmlTypeMapElementInfo.Member, xmlTypeMapElementInfo.TypeData, ob, isValueList))
									{
										throw base.CreateReadOnlyCollectionException(xmlTypeMapElementInfo.TypeData.FullTypeName);
									}
									this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, obj3, isValueList);
								}
								else if (!xmlTypeMapElementInfo.MappedType.TypeData.Type.IsArray)
								{
									if (this.IsReadOnly(xmlTypeMapElementInfo.Member, xmlTypeMapElementInfo.TypeData, ob, isValueList))
									{
										obj3 = this.GetMemberValue(xmlTypeMapElementInfo.Member, ob, isValueList);
									}
									else
									{
										obj3 = this.CreateList(xmlTypeMapElementInfo.MappedType.TypeData.Type);
										this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, obj3, isValueList);
									}
									base.AddFixup(new XmlSerializationReader.CollectionFixup(obj3, new XmlSerializationCollectionFixupCallback(this.FillList), fixup.Ids[xmlTypeMapElementInfo.Member.Index]));
									fixup.Ids[xmlTypeMapElementInfo.Member.Index] = null;
								}
							}
							else if (this.IsReadOnly(xmlTypeMapElementInfo.Member, xmlTypeMapElementInfo.TypeData, ob, isValueList))
							{
								this.ReadListElement(xmlTypeMapElementInfo.MappedType, xmlTypeMapElementInfo.IsNullable, this.GetMemberValue(xmlTypeMapElementInfo.Member, ob, isValueList), false);
							}
							else if (xmlTypeMapElementInfo.MappedType.TypeData.Type.IsArray)
							{
								object obj4 = this.ReadListElement(xmlTypeMapElementInfo.MappedType, xmlTypeMapElementInfo.IsNullable, null, true);
								if (obj4 != null || xmlTypeMapElementInfo.IsNullable)
								{
									this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, obj4, isValueList);
								}
							}
							else
							{
								object obj5 = this.GetMemberValue(xmlTypeMapElementInfo.Member, ob, isValueList);
								if (obj5 == null)
								{
									obj5 = this.CreateList(xmlTypeMapElementInfo.MappedType.TypeData.Type);
									this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, obj5, isValueList);
								}
								this.ReadListElement(xmlTypeMapElementInfo.MappedType, xmlTypeMapElementInfo.IsNullable, obj5, true);
							}
							array[xmlTypeMapElementInfo.Member.Index] = true;
						}
						else if (xmlTypeMapElementInfo.Member.GetType() == typeof(XmlTypeMapMemberFlatList))
						{
							XmlTypeMapMemberFlatList xmlTypeMapMemberFlatList = (XmlTypeMapMemberFlatList)xmlTypeMapElementInfo.Member;
							this.AddListValue(xmlTypeMapMemberFlatList.TypeData, ref array3[xmlTypeMapMemberFlatList.FlatArrayIndex], array2[xmlTypeMapMemberFlatList.FlatArrayIndex]++, this.ReadObjectElement(xmlTypeMapElementInfo), !this.IsReadOnly(xmlTypeMapElementInfo.Member, xmlTypeMapElementInfo.TypeData, ob, isValueList));
							if (xmlTypeMapMemberFlatList.ChoiceMember != null)
							{
								this.AddListValue(xmlTypeMapMemberFlatList.ChoiceTypeData, ref array4[xmlTypeMapMemberFlatList.FlatArrayIndex], array2[xmlTypeMapMemberFlatList.FlatArrayIndex] - 1, xmlTypeMapElementInfo.ChoiceValue, true);
							}
						}
						else if (xmlTypeMapElementInfo.Member.GetType() == typeof(XmlTypeMapMemberAnyElement))
						{
							XmlTypeMapMemberAnyElement xmlTypeMapMemberAnyElement = (XmlTypeMapMemberAnyElement)xmlTypeMapElementInfo.Member;
							if (xmlTypeMapMemberAnyElement.TypeData.IsListType)
							{
								this.AddListValue(xmlTypeMapMemberAnyElement.TypeData, ref array3[xmlTypeMapMemberAnyElement.FlatArrayIndex], array2[xmlTypeMapMemberAnyElement.FlatArrayIndex]++, this.ReadXmlNode(xmlTypeMapMemberAnyElement.TypeData.ListItemTypeData, false), true);
							}
							else
							{
								this.SetMemberValue(xmlTypeMapMemberAnyElement, ob, this.ReadXmlNode(xmlTypeMapMemberAnyElement.TypeData, false), isValueList);
							}
						}
						else
						{
							if (xmlTypeMapElementInfo.Member.GetType() != typeof(XmlTypeMapMemberElement))
							{
								throw new InvalidOperationException("Unknown member type");
							}
							array[xmlTypeMapElementInfo.Member.Index] = true;
							if (this._format == SerializationFormat.Encoded)
							{
								object obj6;
								if (xmlTypeMapElementInfo.Member.TypeData.SchemaType != SchemaTypes.Primitive)
								{
									obj6 = base.ReadReferencingElement(out fixup.Ids[xmlTypeMapElementInfo.Member.Index]);
								}
								else
								{
									obj6 = base.ReadReferencingElement(xmlTypeMapElementInfo.Member.TypeData.XmlType, "http://www.w3.org/2001/XMLSchema", out fixup.Ids[xmlTypeMapElementInfo.Member.Index]);
								}
								if (xmlTypeMapElementInfo.MultiReferenceType)
								{
									if (fixup.Ids[xmlTypeMapElementInfo.Member.Index] == null)
									{
										this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, obj6, isValueList);
									}
								}
								else if (obj6 != null)
								{
									this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, obj6, isValueList);
								}
							}
							else
							{
								this.SetMemberValue(xmlTypeMapElementInfo.Member, ob, this.ReadObjectElement(xmlTypeMapElementInfo), isValueList);
								if (xmlTypeMapElementInfo.ChoiceValue != null)
								{
									XmlTypeMapMemberElement xmlTypeMapMemberElement = (XmlTypeMapMemberElement)xmlTypeMapElementInfo.Member;
									xmlTypeMapMemberElement.SetChoice(ob, xmlTypeMapElementInfo.ChoiceValue);
								}
							}
						}
					}
					else if (map.DefaultAnyElementMember != null)
					{
						XmlTypeMapMemberAnyElement defaultAnyElementMember = map.DefaultAnyElementMember;
						if (defaultAnyElementMember.TypeData.IsListType)
						{
							this.AddListValue(defaultAnyElementMember.TypeData, ref array3[defaultAnyElementMember.FlatArrayIndex], array2[defaultAnyElementMember.FlatArrayIndex]++, this.ReadXmlNode(defaultAnyElementMember.TypeData.ListItemTypeData, false), true);
						}
						else
						{
							this.SetMemberValue(defaultAnyElementMember, ob, this.ReadXmlNode(defaultAnyElementMember.TypeData, false), isValueList);
						}
					}
					else
					{
						this.ProcessUnknownElement(ob);
					}
				}
				else if ((base.Reader.NodeType == XmlNodeType.Text || base.Reader.NodeType == XmlNodeType.CDATA) && map.XmlTextCollector != null)
				{
					if (map.XmlTextCollector is XmlTypeMapMemberExpandable)
					{
						XmlTypeMapMemberExpandable xmlTypeMapMemberExpandable2 = (XmlTypeMapMemberExpandable)map.XmlTextCollector;
						XmlTypeMapMemberFlatList xmlTypeMapMemberFlatList2 = xmlTypeMapMemberExpandable2 as XmlTypeMapMemberFlatList;
						TypeData typeData = (xmlTypeMapMemberFlatList2 != null) ? xmlTypeMapMemberFlatList2.ListMap.FindTextElement().TypeData : xmlTypeMapMemberExpandable2.TypeData.ListItemTypeData;
						object value = (typeData.Type != typeof(string)) ? this.ReadXmlNode(typeData, false) : base.Reader.ReadString();
						this.AddListValue(xmlTypeMapMemberExpandable2.TypeData, ref array3[xmlTypeMapMemberExpandable2.FlatArrayIndex], array2[xmlTypeMapMemberExpandable2.FlatArrayIndex]++, value, true);
					}
					else
					{
						XmlTypeMapMemberElement xmlTypeMapMemberElement2 = (XmlTypeMapMemberElement)map.XmlTextCollector;
						XmlTypeMapElementInfo xmlTypeMapElementInfo2 = (XmlTypeMapElementInfo)xmlTypeMapMemberElement2.ElementInfo[0];
						if (xmlTypeMapElementInfo2.TypeData.Type == typeof(string))
						{
							this.SetMemberValue(xmlTypeMapMemberElement2, ob, base.ReadString((string)this.GetMemberValue(xmlTypeMapMemberElement2, ob, isValueList)), isValueList);
						}
						else
						{
							this.SetMemberValue(xmlTypeMapMemberElement2, ob, this.GetValueFromXmlString(base.Reader.ReadString(), xmlTypeMapElementInfo2.TypeData, xmlTypeMapElementInfo2.MappedType), isValueList);
						}
					}
				}
				else
				{
					base.UnknownNode(ob);
				}
				base.Reader.MoveToContent();
			}
			if (array3 != null)
			{
				foreach (object obj7 in map.FlatLists)
				{
					XmlTypeMapMemberExpandable xmlTypeMapMemberExpandable3 = (XmlTypeMapMemberExpandable)obj7;
					object obj8 = array3[xmlTypeMapMemberExpandable3.FlatArrayIndex];
					if (xmlTypeMapMemberExpandable3.TypeData.Type.IsArray)
					{
						obj8 = base.ShrinkArray((Array)obj8, array2[xmlTypeMapMemberExpandable3.FlatArrayIndex], xmlTypeMapMemberExpandable3.TypeData.Type.GetElementType(), true);
					}
					if (!this.IsReadOnly(xmlTypeMapMemberExpandable3, xmlTypeMapMemberExpandable3.TypeData, ob, isValueList) && xmlTypeMapMemberExpandable3.TypeData.Type.IsArray)
					{
						this.SetMemberValue(xmlTypeMapMemberExpandable3, ob, obj8, isValueList);
					}
				}
			}
			if (array4 != null)
			{
				foreach (object obj9 in map.FlatLists)
				{
					XmlTypeMapMemberExpandable xmlTypeMapMemberExpandable4 = (XmlTypeMapMemberExpandable)obj9;
					object obj10 = array4[xmlTypeMapMemberExpandable4.FlatArrayIndex];
					if (obj10 != null)
					{
						obj10 = base.ShrinkArray((Array)obj10, array2[xmlTypeMapMemberExpandable4.FlatArrayIndex], xmlTypeMapMemberExpandable4.ChoiceTypeData.Type.GetElementType(), true);
						XmlTypeMapMember.SetValue(ob, xmlTypeMapMemberExpandable4.ChoiceMember, obj10);
					}
				}
			}
			this.SetListMembersDefaults(map, ob, isValueList);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00096B7C File Offset: 0x00094D7C
		private void SetListMembersDefaults(ClassMap map, object ob, bool isValueList)
		{
			if (map.ListMembers != null)
			{
				ArrayList listMembers = map.ListMembers;
				for (int i = 0; i < listMembers.Count; i++)
				{
					XmlTypeMapMember xmlTypeMapMember = (XmlTypeMapMember)listMembers[i];
					if (!this.IsReadOnly(xmlTypeMapMember, xmlTypeMapMember.TypeData, ob, isValueList))
					{
						if (this.GetMemberValue(xmlTypeMapMember, ob, isValueList) == null)
						{
							this.SetMemberValue(xmlTypeMapMember, ob, this.InitializeList(xmlTypeMapMember.TypeData), isValueList);
						}
					}
				}
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00096BFC File Offset: 0x00094DFC
		internal void FixupMembers(ClassMap map, object obfixup, bool isValueList)
		{
			XmlSerializationReader.Fixup fixup = (XmlSerializationReader.Fixup)obfixup;
			ICollection elementMembers = map.ElementMembers;
			string[] ids = fixup.Ids;
			foreach (object obj in elementMembers)
			{
				XmlTypeMapMember xmlTypeMapMember = (XmlTypeMapMember)obj;
				if (ids[xmlTypeMapMember.Index] != null)
				{
					this.SetMemberValue(xmlTypeMapMember, fixup.Source, base.GetTarget(ids[xmlTypeMapMember.Index]), isValueList);
				}
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00096CA4 File Offset: 0x00094EA4
		protected virtual void ProcessUnknownAttribute(object target)
		{
			base.UnknownNode(target);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00096CB0 File Offset: 0x00094EB0
		protected virtual void ProcessUnknownElement(object target)
		{
			base.UnknownNode(target);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00096CBC File Offset: 0x00094EBC
		private bool IsReadOnly(XmlTypeMapMember member, TypeData memType, object ob, bool isValueList)
		{
			if (isValueList)
			{
				return !memType.HasPublicConstructor;
			}
			return member.IsReadOnly(ob.GetType()) || !memType.HasPublicConstructor;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00096CF8 File Offset: 0x00094EF8
		private void SetMemberValue(XmlTypeMapMember member, object ob, object value, bool isValueList)
		{
			if (isValueList)
			{
				((object[])ob)[member.GlobalIndex] = value;
			}
			else
			{
				member.SetValue(ob, value);
				if (member.IsOptionalValueType)
				{
					member.SetValueSpecified(ob, true);
				}
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00096D3C File Offset: 0x00094F3C
		private void SetMemberValueFromAttr(XmlTypeMapMember member, object ob, object value, bool isValueList)
		{
			if (member.TypeData.Type.IsEnum)
			{
				value = Enum.ToObject(member.TypeData.Type, value);
			}
			this.SetMemberValue(member, ob, value, isValueList);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00096D7C File Offset: 0x00094F7C
		private object GetMemberValue(XmlTypeMapMember member, object ob, bool isValueList)
		{
			if (isValueList)
			{
				return ((object[])ob)[member.GlobalIndex];
			}
			return member.GetValue(ob);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00096D9C File Offset: 0x00094F9C
		private object ReadObjectElement(XmlTypeMapElementInfo elem)
		{
			switch (elem.TypeData.SchemaType)
			{
			case SchemaTypes.Primitive:
			case SchemaTypes.Enum:
				return this.ReadPrimitiveValue(elem);
			case SchemaTypes.Array:
				return this.ReadListElement(elem.MappedType, elem.IsNullable, null, true);
			case SchemaTypes.Class:
				return this.ReadObject(elem.MappedType, elem.IsNullable, true);
			case SchemaTypes.XmlSerializable:
			{
				object obj = Activator.CreateInstance(elem.TypeData.Type, true);
				return base.ReadSerializable((IXmlSerializable)obj);
			}
			case SchemaTypes.XmlNode:
				return this.ReadXmlNode(elem.TypeData, true);
			default:
				throw new NotSupportedException("Invalid value type");
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00096E44 File Offset: 0x00095044
		private object ReadPrimitiveValue(XmlTypeMapElementInfo elem)
		{
			if (elem.TypeData.Type == typeof(XmlQualifiedName))
			{
				if (elem.IsNullable)
				{
					return base.ReadNullableQualifiedName();
				}
				return base.ReadElementQualifiedName();
			}
			else
			{
				if (elem.IsNullable)
				{
					return this.GetValueFromXmlString(base.ReadNullableString(), elem.TypeData, elem.MappedType);
				}
				return this.GetValueFromXmlString(base.Reader.ReadElementString(), elem.TypeData, elem.MappedType);
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00096EC8 File Offset: 0x000950C8
		private object GetValueFromXmlString(string value, TypeData typeData, XmlTypeMapping typeMap)
		{
			if (typeData.SchemaType == SchemaTypes.Array)
			{
				return this.ReadListString(typeMap, value);
			}
			if (typeData.SchemaType == SchemaTypes.Enum)
			{
				return this.GetEnumValue(typeMap, value);
			}
			if (typeData.Type == typeof(XmlQualifiedName))
			{
				return base.ToXmlQualifiedName(value);
			}
			return XmlCustomFormatter.FromXmlString(typeData, value);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00096F24 File Offset: 0x00095124
		private object ReadListElement(XmlTypeMapping typeMap, bool isNullable, object list, bool canCreateInstance)
		{
			Type type = typeMap.TypeData.Type;
			ListMap listMap = (ListMap)typeMap.ObjectMap;
			if (type.IsArray && base.ReadNull())
			{
				return null;
			}
			if (list == null)
			{
				if (!canCreateInstance || !typeMap.TypeData.HasPublicConstructor)
				{
					throw base.CreateReadOnlyCollectionException(typeMap.TypeFullName);
				}
				list = this.CreateList(type);
			}
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				if (type.IsArray)
				{
					list = base.ShrinkArray((Array)list, 0, type.GetElementType(), false);
				}
				return list;
			}
			int length = 0;
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			while (base.Reader.NodeType != XmlNodeType.EndElement)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = listMap.FindElement(base.Reader.LocalName, base.Reader.NamespaceURI);
					if (xmlTypeMapElementInfo != null)
					{
						this.AddListValue(typeMap.TypeData, ref list, length++, this.ReadObjectElement(xmlTypeMapElementInfo), false);
					}
					else
					{
						base.UnknownNode(null);
					}
				}
				else
				{
					base.UnknownNode(null);
				}
				base.Reader.MoveToContent();
			}
			base.ReadEndElement();
			if (type.IsArray)
			{
				list = base.ShrinkArray((Array)list, length, type.GetElementType(), false);
			}
			return list;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000970A4 File Offset: 0x000952A4
		private object ReadListString(XmlTypeMapping typeMap, string values)
		{
			Type type = typeMap.TypeData.Type;
			ListMap listMap = (ListMap)typeMap.ObjectMap;
			values = values.Trim();
			if (values == string.Empty)
			{
				return Array.CreateInstance(type.GetElementType(), 0);
			}
			string[] array = values.Split(new char[]
			{
				' '
			});
			Array array2 = Array.CreateInstance(type.GetElementType(), array.Length);
			XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)listMap.ItemInfo[0];
			for (int i = 0; i < array.Length; i++)
			{
				array2.SetValue(this.GetValueFromXmlString(array[i], xmlTypeMapElementInfo.TypeData, xmlTypeMapElementInfo.MappedType), i);
			}
			return array2;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0009715C File Offset: 0x0009535C
		private void AddListValue(TypeData listType, ref object list, int index, object value, bool canCreateInstance)
		{
			Type type = listType.Type;
			if (type.IsArray)
			{
				list = base.EnsureArrayIndex((Array)list, index, type.GetElementType());
				((Array)list).SetValue(value, index);
			}
			else
			{
				if (list == null)
				{
					if (!canCreateInstance)
					{
						throw base.CreateReadOnlyCollectionException(type.FullName);
					}
					list = Activator.CreateInstance(type, true);
				}
				MethodInfo method = type.GetMethod("Add", new Type[]
				{
					listType.ListItemType
				});
				method.Invoke(list, new object[]
				{
					value
				});
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00097200 File Offset: 0x00095400
		private object CreateInstance(Type type)
		{
			return Activator.CreateInstance(type, XmlSerializationReaderInterpreter.empty_array);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00097210 File Offset: 0x00095410
		private object CreateList(Type listType)
		{
			if (listType.IsArray)
			{
				return base.EnsureArrayIndex(null, 0, listType.GetElementType());
			}
			return Activator.CreateInstance(listType, true);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00097240 File Offset: 0x00095440
		private object InitializeList(TypeData listType)
		{
			if (listType.Type.IsArray)
			{
				return null;
			}
			return Activator.CreateInstance(listType.Type, true);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0009726C File Offset: 0x0009546C
		private void FillList(object list, object items)
		{
			this.CopyEnumerableList(items, list);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00097278 File Offset: 0x00095478
		private void CopyEnumerableList(object source, object dest)
		{
			if (dest == null)
			{
				throw base.CreateReadOnlyCollectionException(source.GetType().FullName);
			}
			object[] array = new object[1];
			MethodInfo method = dest.GetType().GetMethod("Add");
			foreach (object obj in ((IEnumerable)source))
			{
				array[0] = obj;
				method.Invoke(dest, array);
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0009731C File Offset: 0x0009551C
		private object ReadXmlNodeElement(XmlTypeMapping typeMap, bool isNullable)
		{
			return this.ReadXmlNode(typeMap.TypeData, false);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0009732C File Offset: 0x0009552C
		private object ReadXmlNode(TypeData type, bool wrapped)
		{
			if (type.Type == typeof(XmlDocument))
			{
				return base.ReadXmlDocument(wrapped);
			}
			return base.ReadXmlNode(wrapped);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00097360 File Offset: 0x00095560
		private object ReadPrimitiveElement(XmlTypeMapping typeMap, bool isNullable)
		{
			XmlQualifiedName xmlQualifiedName = base.GetXsiType();
			if (xmlQualifiedName == null)
			{
				xmlQualifiedName = new XmlQualifiedName(typeMap.XmlType, typeMap.Namespace);
			}
			return base.ReadTypedPrimitive(xmlQualifiedName);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0009739C File Offset: 0x0009559C
		private object ReadEnumElement(XmlTypeMapping typeMap, bool isNullable)
		{
			base.Reader.ReadStartElement();
			object enumValue = this.GetEnumValue(typeMap, base.Reader.ReadString());
			base.ReadEndElement();
			return enumValue;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000973D0 File Offset: 0x000955D0
		private object GetEnumValue(XmlTypeMapping typeMap, string val)
		{
			if (val == null)
			{
				return null;
			}
			EnumMap enumMap = (EnumMap)typeMap.ObjectMap;
			string enumName = enumMap.GetEnumName(typeMap.TypeFullName, val);
			if (enumName == null)
			{
				throw base.CreateUnknownConstantException(val, typeMap.TypeData.Type);
			}
			return Enum.Parse(typeMap.TypeData.Type, enumName);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0009742C File Offset: 0x0009562C
		private object ReadXmlSerializableElement(XmlTypeMapping typeMap, bool isNullable)
		{
			base.Reader.MoveToContent();
			if (base.Reader.NodeType != XmlNodeType.Element)
			{
				base.UnknownNode(null);
				return null;
			}
			if (base.Reader.LocalName == typeMap.ElementName && base.Reader.NamespaceURI == typeMap.Namespace)
			{
				object obj = Activator.CreateInstance(typeMap.TypeData.Type, true);
				return base.ReadSerializable((IXmlSerializable)obj);
			}
			throw base.CreateUnknownNodeException();
		}

		// Token: 0x04000B65 RID: 2917
		private XmlMapping _typeMap;

		// Token: 0x04000B66 RID: 2918
		private SerializationFormat _format;

		// Token: 0x04000B67 RID: 2919
		private static readonly XmlQualifiedName AnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04000B68 RID: 2920
		private static readonly object[] empty_array = new object[0];

		// Token: 0x020002AB RID: 683
		private class FixupCallbackInfo
		{
			// Token: 0x06001C5A RID: 7258 RVA: 0x000974BC File Offset: 0x000956BC
			public FixupCallbackInfo(XmlSerializationReaderInterpreter sri, ClassMap map, bool isValueList)
			{
				this._sri = sri;
				this._map = map;
				this._isValueList = isValueList;
			}

			// Token: 0x06001C5B RID: 7259 RVA: 0x000974DC File Offset: 0x000956DC
			public void FixupMembers(object fixup)
			{
				this._sri.FixupMembers(this._map, fixup, this._isValueList);
			}

			// Token: 0x04000B69 RID: 2921
			private XmlSerializationReaderInterpreter _sri;

			// Token: 0x04000B6A RID: 2922
			private ClassMap _map;

			// Token: 0x04000B6B RID: 2923
			private bool _isValueList;
		}

		// Token: 0x020002AC RID: 684
		private class ReaderCallbackInfo
		{
			// Token: 0x06001C5C RID: 7260 RVA: 0x000974F8 File Offset: 0x000956F8
			public ReaderCallbackInfo(XmlSerializationReaderInterpreter sri, XmlTypeMapping typeMap)
			{
				this._sri = sri;
				this._typeMap = typeMap;
			}

			// Token: 0x06001C5D RID: 7261 RVA: 0x00097510 File Offset: 0x00095710
			internal object ReadObject()
			{
				return this._sri.ReadObject(this._typeMap, true, true);
			}

			// Token: 0x04000B6C RID: 2924
			private XmlSerializationReaderInterpreter _sri;

			// Token: 0x04000B6D RID: 2925
			private XmlTypeMapping _typeMap;
		}
	}
}
