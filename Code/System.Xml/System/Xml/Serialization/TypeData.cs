using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000279 RID: 633
	internal class TypeData
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x0008646C File Offset: 0x0008466C
		public TypeData(Type type, string elementName, bool isPrimitive) : this(type, elementName, isPrimitive, null, null)
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0008647C File Offset: 0x0008467C
		public TypeData(Type type, string elementName, bool isPrimitive, TypeData mappedType, XmlSchemaPatternFacet facet)
		{
			this.hasPublicConstructor = true;
			base..ctor();
			if (type.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("Generic type definition cannot be used in serialization. Only specific generic types can be used.");
			}
			this.mappedType = mappedType;
			this.facet = facet;
			this.type = type;
			this.typeName = type.Name;
			this.fullTypeName = type.FullName.Replace('+', '.');
			if (isPrimitive)
			{
				this.sType = SchemaTypes.Primitive;
			}
			else if (type.IsEnum)
			{
				this.sType = SchemaTypes.Enum;
			}
			else if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				this.sType = SchemaTypes.XmlSerializable;
			}
			else if (typeof(XmlNode).IsAssignableFrom(type))
			{
				this.sType = SchemaTypes.XmlNode;
			}
			else if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
			{
				this.sType = SchemaTypes.Array;
			}
			else
			{
				this.sType = SchemaTypes.Class;
			}
			if (this.IsListType)
			{
				this.elementName = TypeTranslator.GetArrayName(this.ListItemTypeData.XmlType);
			}
			else
			{
				this.elementName = elementName;
			}
			if (this.sType == SchemaTypes.Array || this.sType == SchemaTypes.Class)
			{
				this.hasPublicConstructor = (!type.IsInterface && (type.IsArray || type.GetConstructor(Type.EmptyTypes) != null || type.IsAbstract || type.IsValueType));
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0008660C File Offset: 0x0008480C
		internal TypeData(string typeName, string fullTypeName, string xmlType, SchemaTypes schemaType, TypeData listItemTypeData)
		{
			this.hasPublicConstructor = true;
			base..ctor();
			this.elementName = xmlType;
			this.typeName = typeName;
			this.fullTypeName = fullTypeName.Replace('+', '.');
			this.listItemTypeData = listItemTypeData;
			this.sType = schemaType;
			this.hasPublicConstructor = true;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0008693C File Offset: 0x00084B3C
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00086944 File Offset: 0x00084B44
		public string XmlType
		{
			get
			{
				return this.elementName;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0008694C File Offset: 0x00084B4C
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00086954 File Offset: 0x00084B54
		public string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0008695C File Offset: 0x00084B5C
		public string CSharpName
		{
			get
			{
				if (this.csharpName == null)
				{
					this.csharpName = ((this.Type != null) ? TypeData.ToCSharpName(this.Type, false) : this.TypeName);
				}
				return this.csharpName;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x000869A4 File Offset: 0x00084BA4
		public string CSharpFullName
		{
			get
			{
				if (this.csharpFullName == null)
				{
					this.csharpFullName = ((this.Type != null) ? TypeData.ToCSharpName(this.Type, true) : this.TypeName);
				}
				return this.csharpFullName;
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000869EC File Offset: 0x00084BEC
		public static string ToCSharpName(Type type, bool full)
		{
			if (type.IsArray)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(TypeData.ToCSharpName(type.GetElementType(), full));
				stringBuilder.Append('[');
				int arrayRank = type.GetArrayRank();
				for (int i = 1; i < arrayRank; i++)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(TypeData.ToCSharpName(type.GetGenericTypeDefinition(), full));
				stringBuilder2.Append('<');
				foreach (Type type2 in type.GetGenericArguments())
				{
					stringBuilder2.Append(TypeData.ToCSharpName(type2, full)).Append(',');
				}
				stringBuilder2.Length--;
				stringBuilder2.Append('>');
				return stringBuilder2.ToString();
			}
			string text = (!full) ? type.Name : type.FullName;
			text = text.Replace('+', '.');
			int num = text.IndexOf('`');
			text = ((num <= 0) ? text : text.Substring(0, num));
			if (TypeData.IsKeyword(text))
			{
				return "@" + text;
			}
			return text;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00086B54 File Offset: 0x00084D54
		private static bool IsKeyword(string name)
		{
			if (TypeData.keywordsTable == null)
			{
				Hashtable hashtable = new Hashtable();
				foreach (string text in TypeData.keywords)
				{
					hashtable[text] = text;
				}
				TypeData.keywordsTable = hashtable;
			}
			return TypeData.keywordsTable.Contains(name);
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00086BA8 File Offset: 0x00084DA8
		public SchemaTypes SchemaType
		{
			get
			{
				return this.sType;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00086BB0 File Offset: 0x00084DB0
		public bool IsListType
		{
			get
			{
				return this.SchemaType == SchemaTypes.Array;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x00086BBC File Offset: 0x00084DBC
		public bool IsComplexType
		{
			get
			{
				return this.SchemaType == SchemaTypes.Class || this.SchemaType == SchemaTypes.Array || this.SchemaType == SchemaTypes.Enum || this.SchemaType == SchemaTypes.XmlNode || this.SchemaType == SchemaTypes.XmlSerializable || !this.IsXsdType;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00086C14 File Offset: 0x00084E14
		public bool IsValueType
		{
			get
			{
				if (this.type != null)
				{
					return this.type.IsValueType;
				}
				return this.sType == SchemaTypes.Primitive || this.sType == SchemaTypes.Enum;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00086C48 File Offset: 0x00084E48
		public bool NullableOverride
		{
			get
			{
				return this.nullableOverride;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00086C50 File Offset: 0x00084E50
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x00086CB0 File Offset: 0x00084EB0
		public bool IsNullable
		{
			get
			{
				return this.nullableOverride || !this.IsValueType || (this.type != null && this.type.IsGenericType && this.type.GetGenericTypeDefinition() == typeof(Nullable<>));
			}
			set
			{
				this.nullableOverride = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00086CBC File Offset: 0x00084EBC
		public TypeData ListItemTypeData
		{
			get
			{
				if (this.listItemTypeData == null && this.type != null)
				{
					this.listItemTypeData = TypeTranslator.GetTypeData(this.ListItemType);
				}
				return this.listItemTypeData;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00086CEC File Offset: 0x00084EEC
		public Type ListItemType
		{
			get
			{
				if (this.type == null)
				{
					throw new InvalidOperationException("Property ListItemType is not supported for custom types");
				}
				if (this.listItemType != null)
				{
					return this.listItemType;
				}
				Type type = null;
				if (this.SchemaType != SchemaTypes.Array)
				{
					throw new InvalidOperationException(this.Type.FullName + " is not a collection");
				}
				if (this.type.IsArray)
				{
					this.listItemType = this.type.GetElementType();
				}
				else if (typeof(ICollection).IsAssignableFrom(this.type) || (type = this.GetGenericListItemType(this.type)) != null)
				{
					if (typeof(IDictionary).IsAssignableFrom(this.type))
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "The type {0} is not supported because it implements IDictionary.", new object[]
						{
							this.type.FullName
						}));
					}
					if (type != null)
					{
						this.listItemType = type;
					}
					else
					{
						PropertyInfo indexerProperty = TypeData.GetIndexerProperty(this.type);
						if (indexerProperty == null)
						{
							throw new InvalidOperationException("You must implement a default accessor on " + this.type.FullName + " because it inherits from ICollection");
						}
						this.listItemType = indexerProperty.PropertyType;
					}
					if (this.type.GetMethod("Add", new Type[]
					{
						this.listItemType
					}) == null)
					{
						throw TypeData.CreateMissingAddMethodException(this.type, "ICollection", this.listItemType);
					}
				}
				else
				{
					MethodInfo method = this.type.GetMethod("GetEnumerator", Type.EmptyTypes);
					if (method == null)
					{
						method = this.type.GetMethod("System.Collections.IEnumerable.GetEnumerator", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
					}
					PropertyInfo property = method.ReturnType.GetProperty("Current");
					if (property == null)
					{
						this.listItemType = typeof(object);
					}
					else
					{
						this.listItemType = property.PropertyType;
					}
					if (this.type.GetMethod("Add", new Type[]
					{
						this.listItemType
					}) == null)
					{
						throw TypeData.CreateMissingAddMethodException(this.type, "IEnumerable", this.listItemType);
					}
				}
				return this.listItemType;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00086F28 File Offset: 0x00085128
		public TypeData ListTypeData
		{
			get
			{
				if (this.listTypeData != null)
				{
					return this.listTypeData;
				}
				this.listTypeData = new TypeData(this.TypeName + "[]", this.FullTypeName + "[]", TypeTranslator.GetArrayName(this.XmlType), SchemaTypes.Array, this);
				return this.listTypeData;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00086F88 File Offset: 0x00085188
		public bool IsXsdType
		{
			get
			{
				return this.mappedType == null;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00086F94 File Offset: 0x00085194
		public TypeData MappedType
		{
			get
			{
				return (this.mappedType == null) ? this : this.mappedType;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00086FB0 File Offset: 0x000851B0
		public XmlSchemaPatternFacet XmlSchemaPatternFacet
		{
			get
			{
				return this.facet;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00086FB8 File Offset: 0x000851B8
		public bool HasPublicConstructor
		{
			get
			{
				return this.hasPublicConstructor;
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00086FC0 File Offset: 0x000851C0
		public static PropertyInfo GetIndexerProperty(Type collectionType)
		{
			PropertyInfo[] properties = collectionType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
				if (indexParameters != null && indexParameters.Length == 1 && indexParameters[0].ParameterType == typeof(int))
				{
					return propertyInfo;
				}
			}
			return null;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00087024 File Offset: 0x00085224
		private static InvalidOperationException CreateMissingAddMethodException(Type type, string inheritFrom, Type argumentType)
		{
			return new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "To be XML serializable, types which inherit from {0} must have an implementation of Add({1}) at all levels of their inheritance hierarchy. {2} does not implement Add({1}).", new object[]
			{
				inheritFrom,
				argumentType.FullName,
				type.FullName
			}));
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00087064 File Offset: 0x00085264
		private Type GetGenericListItemType(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
			{
				return type.GetGenericArguments()[0];
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				Type genericListItemType;
				if ((genericListItemType = this.GetGenericListItemType(type2)) != null)
				{
					return genericListItemType;
				}
			}
			return null;
		}

		// Token: 0x04000A91 RID: 2705
		private Type type;

		// Token: 0x04000A92 RID: 2706
		private string elementName;

		// Token: 0x04000A93 RID: 2707
		private SchemaTypes sType;

		// Token: 0x04000A94 RID: 2708
		private Type listItemType;

		// Token: 0x04000A95 RID: 2709
		private string typeName;

		// Token: 0x04000A96 RID: 2710
		private string fullTypeName;

		// Token: 0x04000A97 RID: 2711
		private string csharpName;

		// Token: 0x04000A98 RID: 2712
		private string csharpFullName;

		// Token: 0x04000A99 RID: 2713
		private TypeData listItemTypeData;

		// Token: 0x04000A9A RID: 2714
		private TypeData listTypeData;

		// Token: 0x04000A9B RID: 2715
		private TypeData mappedType;

		// Token: 0x04000A9C RID: 2716
		private XmlSchemaPatternFacet facet;

		// Token: 0x04000A9D RID: 2717
		private bool hasPublicConstructor;

		// Token: 0x04000A9E RID: 2718
		private bool nullableOverride;

		// Token: 0x04000A9F RID: 2719
		private static Hashtable keywordsTable;

		// Token: 0x04000AA0 RID: 2720
		private static string[] keywords = new string[]
		{
			"abstract",
			"event",
			"new",
			"struct",
			"as",
			"explicit",
			"null",
			"switch",
			"base",
			"extern",
			"this",
			"false",
			"operator",
			"throw",
			"break",
			"finally",
			"out",
			"true",
			"fixed",
			"override",
			"try",
			"case",
			"params",
			"typeof",
			"catch",
			"for",
			"private",
			"foreach",
			"protected",
			"checked",
			"goto",
			"public",
			"unchecked",
			"class",
			"if",
			"readonly",
			"unsafe",
			"const",
			"implicit",
			"ref",
			"continue",
			"in",
			"return",
			"using",
			"virtual",
			"default",
			"interface",
			"sealed",
			"volatile",
			"delegate",
			"internal",
			"do",
			"is",
			"sizeof",
			"while",
			"lock",
			"stackalloc",
			"else",
			"static",
			"enum",
			"namespace",
			"object",
			"bool",
			"byte",
			"float",
			"uint",
			"char",
			"ulong",
			"ushort",
			"decimal",
			"int",
			"sbyte",
			"short",
			"double",
			"long",
			"string",
			"void",
			"partial",
			"yield",
			"where"
		};
	}
}
