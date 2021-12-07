using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents the collection of XML schemas.</summary>
	// Token: 0x020002A3 RID: 675
	public class XmlSchemas : CollectionBase, IEnumerable<XmlSchema>, IEnumerable
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x000935E0 File Offset: 0x000917E0
		IEnumerator<XmlSchema> IEnumerable<XmlSchema>.GetEnumerator()
		{
			return new XmlSchemaEnumerator(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchema" /> object at the specified index. </summary>
		/// <returns>The specified <see cref="T:System.Xml.Schema.XmlSchema" />.</returns>
		/// <param name="index">The index of the item to retrieve.</param>
		// Token: 0x170007E5 RID: 2021
		public XmlSchema this[int index]
		{
			get
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				return (XmlSchema)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Gets a specified <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents the XML schema associated with the specified namespace.</summary>
		/// <returns>The specified <see cref="T:System.Xml.Schema.XmlSchema" /> object.</returns>
		/// <param name="ns">The namespace of the specified object.</param>
		// Token: 0x170007E6 RID: 2022
		public XmlSchema this[string ns]
		{
			get
			{
				return (XmlSchema)this.table[(ns == null) ? string.Empty : ns];
			}
		}

		/// <summary>Gets a value that indicates whether the schemas have been compiled.</summary>
		/// <returns>true, if the schemas have been compiled; otherwise, false.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00093654 File Offset: 0x00091854
		[MonoTODO]
		public bool IsCompiled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Processes the element and attribute names in the XML schemas and, optionally, validates the XML schemas. </summary>
		/// <param name="handler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that specifies the callback method that handles errors and warnings during XML Schema validation, if the strict parameter is set to true.</param>
		/// <param name="fullCompile">true to validate the XML schemas in the collection using the <see cref="M:System.Xml.Serialization.XmlSchemas.Compile(System.Xml.Schema.ValidationEventHandler,System.Boolean)" /> method of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> class; otherwise, false.</param>
		// Token: 0x06001BB6 RID: 7094 RVA: 0x0009365C File Offset: 0x0009185C
		[MonoTODO]
		public void Compile(ValidationEventHandler handler, bool fullCompile)
		{
			foreach (object obj in this)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (fullCompile || !xmlSchema.IsCompiled)
				{
					xmlSchema.Compile(handler);
				}
			}
		}

		/// <summary>Adds an object to the end of the collection.</summary>
		/// <returns>The index at which the <see cref="T:System.Xml.Schema.XmlSchema" /> is added.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to be added to the collection of objects. </param>
		// Token: 0x06001BB7 RID: 7095 RVA: 0x000936D8 File Offset: 0x000918D8
		public int Add(XmlSchema schema)
		{
			this.Insert(this.Count, schema);
			return this.Count - 1;
		}

		/// <summary>Adds an instance of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> class to the end of the collection.</summary>
		/// <param name="schemas">The <see cref="T:System.Xml.Serialization.XmlSchemas" /> object to be added to the end of the collection. </param>
		// Token: 0x06001BB8 RID: 7096 RVA: 0x000936FC File Offset: 0x000918FC
		public void Add(XmlSchemas schemas)
		{
			foreach (object obj in schemas)
			{
				XmlSchema schema = (XmlSchema)obj;
				this.Add(schema);
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents an assembly reference to the collection.</summary>
		/// <returns>The index at which the <see cref="T:System.Xml.Schema.XmlSchema" /> is added.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to add.</param>
		/// <param name="baseUri">The <see cref="T:System.Uri" /> of the schema object.</param>
		// Token: 0x06001BB9 RID: 7097 RVA: 0x00093768 File Offset: 0x00091968
		[MonoNotSupported("")]
		public int Add(XmlSchema schema, Uri baseUri)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents an assembly reference to the collection.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to add.</param>
		// Token: 0x06001BBA RID: 7098 RVA: 0x00093770 File Offset: 0x00091970
		[MonoNotSupported("")]
		public void AddReference(XmlSchema schema)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.Serialization.XmlSchemas" /> contains a specific schema.</summary>
		/// <returns>true, if the collection contains the specified item; otherwise, false.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to locate. </param>
		// Token: 0x06001BBB RID: 7099 RVA: 0x00093778 File Offset: 0x00091978
		public bool Contains(XmlSchema schema)
		{
			return base.List.Contains(schema);
		}

		/// <summary>Returns a value that indicates whether the collection contains an <see cref="T:System.Xml.Schema.XmlSchema" /> object that belongs to the specified namespace.</summary>
		/// <returns>true if the item is found; otherwise, false.</returns>
		/// <param name="targetNamespace">The namespace of the item to check for.</param>
		// Token: 0x06001BBC RID: 7100 RVA: 0x00093788 File Offset: 0x00091988
		[MonoNotSupported("")]
		public bool Contains(string targetNamespace)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the entire <see cref="T:System.Xml.Serialization.XmlSchemas" /> to a compatible one-dimensional <see cref="T:System.Array" />, which starts at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the schemas copied from <see cref="T:System.Xml.Serialization.XmlSchemas" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">A 32-bit integer that represents the index in the array where copying begins.</param>
		// Token: 0x06001BBD RID: 7101 RVA: 0x00093790 File Offset: 0x00091990
		public void CopyTo(XmlSchema[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Locates in one of the XML schemas an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of the specified name and type. </summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> instance, such as an <see cref="T:System.Xml.Schema.XmlSchemaElement" /> or <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />.</returns>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies a fully qualified name with a namespace used to locate an <see cref="T:System.Xml.Schema.XmlSchema" /> object in the collection.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to find. Possible types include: <see cref="T:System.Xml.Schema.XmlSchemaGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, and <see cref="T:System.Xml.Schema.XmlSchemaNotation" />.</param>
		// Token: 0x06001BBE RID: 7102 RVA: 0x000937A0 File Offset: 0x000919A0
		public object Find(XmlQualifiedName name, Type type)
		{
			XmlSchema xmlSchema = this.table[name.Namespace] as XmlSchema;
			if (xmlSchema == null)
			{
				foreach (object obj in this)
				{
					XmlSchema schema = (XmlSchema)obj;
					object obj2 = this.Find(schema, name, type);
					if (obj2 != null)
					{
						return obj2;
					}
				}
				return null;
			}
			object obj3 = this.Find(xmlSchema, name, type);
			if (obj3 == null)
			{
				foreach (object obj4 in this)
				{
					XmlSchema schema2 = (XmlSchema)obj4;
					object obj5 = this.Find(schema2, name, type);
					if (obj5 != null)
					{
						return obj5;
					}
				}
			}
			return obj3;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000938CC File Offset: 0x00091ACC
		private object Find(XmlSchema schema, XmlQualifiedName name, Type type)
		{
			if (!schema.IsCompiled)
			{
				schema.Compile(null);
			}
			XmlSchemaObjectTable xmlSchemaObjectTable = null;
			if (type == typeof(XmlSchemaSimpleType) || type == typeof(XmlSchemaComplexType))
			{
				xmlSchemaObjectTable = schema.SchemaTypes;
			}
			else if (type == typeof(XmlSchemaAttribute))
			{
				xmlSchemaObjectTable = schema.Attributes;
			}
			else if (type == typeof(XmlSchemaAttributeGroup))
			{
				xmlSchemaObjectTable = schema.AttributeGroups;
			}
			else if (type == typeof(XmlSchemaElement))
			{
				xmlSchemaObjectTable = schema.Elements;
			}
			else if (type == typeof(XmlSchemaGroup))
			{
				xmlSchemaObjectTable = schema.Groups;
			}
			else if (type == typeof(XmlSchemaNotation))
			{
				xmlSchemaObjectTable = schema.Notations;
			}
			object obj = (xmlSchemaObjectTable == null) ? null : xmlSchemaObjectTable[name];
			if (obj != null && obj.GetType() != type)
			{
				return null;
			}
			return obj;
		}

		/// <summary>Gets a collection of schemas that belong to the same namespace.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> implementation that contains the schemas.</returns>
		/// <param name="ns">The namespace of the schemas to retrieve.</param>
		// Token: 0x06001BC0 RID: 7104 RVA: 0x000939CC File Offset: 0x00091BCC
		[MonoNotSupported("")]
		public IList GetSchemas(string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for the specified schema and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Xml.Serialization.XmlSchemas" />.</summary>
		/// <returns>The zero-based index of the first occurrence of the value within the entire <see cref="T:System.Xml.Serialization.XmlSchemas" />, if found; otherwise, -1.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to locate. </param>
		// Token: 0x06001BC1 RID: 7105 RVA: 0x000939D4 File Offset: 0x00091BD4
		public int IndexOf(XmlSchema schema)
		{
			return base.List.IndexOf(schema);
		}

		/// <summary>Inserts a schema into the <see cref="T:System.Xml.Serialization.XmlSchemas" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="schema" /> should be inserted. </param>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to be inserted. </param>
		// Token: 0x06001BC2 RID: 7106 RVA: 0x000939E4 File Offset: 0x00091BE4
		public void Insert(int index, XmlSchema schema)
		{
			base.List.Insert(index, schema);
		}

		/// <summary>Static method that determines whether the specified XML schema contains a custom IsDataSet attribute set to true, or its equivalent. </summary>
		/// <returns>true if the specified schema exists; otherwise, false.</returns>
		/// <param name="schema">The XML schema to check for an IsDataSet attribute with a true value.</param>
		// Token: 0x06001BC3 RID: 7107 RVA: 0x000939F4 File Offset: 0x00091BF4
		public static bool IsDataSet(XmlSchema schema)
		{
			XmlSchemaElement xmlSchemaElement = (schema.Items.Count != 1) ? null : (schema.Items[0] as XmlSchemaElement);
			if (xmlSchemaElement != null && xmlSchemaElement.UnhandledAttributes != null && xmlSchemaElement.UnhandledAttributes.Length > 0)
			{
				for (int i = 0; i < xmlSchemaElement.UnhandledAttributes.Length; i++)
				{
					XmlAttribute xmlAttribute = xmlSchemaElement.UnhandledAttributes[i];
					if (xmlAttribute.NamespaceURI == XmlSchemas.msdataNS && xmlAttribute.LocalName == "IsDataSet")
					{
						return xmlAttribute.Value.ToLower(CultureInfo.InvariantCulture) == "true";
					}
				}
			}
			return false;
		}

		/// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		// Token: 0x06001BC4 RID: 7108 RVA: 0x00093AB4 File Offset: 0x00091CB4
		protected override void OnClear()
		{
			this.table.Clear();
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the element at <paramref name="index" />. </param>
		// Token: 0x06001BC5 RID: 7109 RVA: 0x00093AC4 File Offset: 0x00091CC4
		protected override void OnInsert(int index, object value)
		{
			string text = ((XmlSchema)value).TargetNamespace;
			if (text == null)
			{
				text = string.Empty;
			}
			this.table[text] = value;
		}

		/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The value of the element to remove at <paramref name="index" />. </param>
		// Token: 0x06001BC6 RID: 7110 RVA: 0x00093AF8 File Offset: 0x00091CF8
		protected override void OnRemove(int index, object value)
		{
			this.table.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Xml.Serialization.XmlSchemas" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />. </param>
		// Token: 0x06001BC7 RID: 7111 RVA: 0x00093B08 File Offset: 0x00091D08
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			string text = ((XmlSchema)oldValue).TargetNamespace;
			if (text == null)
			{
				text = string.Empty;
			}
			this.table[text] = newValue;
		}

		/// <summary>Removes the first occurrence of a specific schema from the <see cref="T:System.Xml.Serialization.XmlSchemas" />.</summary>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> to remove. </param>
		// Token: 0x06001BC8 RID: 7112 RVA: 0x00093B3C File Offset: 0x00091D3C
		public void Remove(XmlSchema schema)
		{
			base.List.Remove(schema);
		}

		// Token: 0x04000B3C RID: 2876
		private static string msdataNS = "urn:schemas-microsoft-com:xml-msdata";

		// Token: 0x04000B3D RID: 2877
		private Hashtable table = new Hashtable();
	}
}
