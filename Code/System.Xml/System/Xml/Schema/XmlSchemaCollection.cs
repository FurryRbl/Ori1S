using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>Contains a cache of XML Schema definition language (XSD) and XML-Data Reduced (XDR) schemas. This class cannot be inherited.</summary>
	// Token: 0x02000201 RID: 513
	[Obsolete("Use XmlSchemaSet.")]
	public sealed class XmlSchemaCollection : IEnumerable, ICollection
	{
		/// <summary>Initializes a new instance of the XmlSchemaCollection class.</summary>
		// Token: 0x06001478 RID: 5240 RVA: 0x000599D4 File Offset: 0x00057BD4
		public XmlSchemaCollection() : this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the XmlSchemaCollection class with the specified <see cref="T:System.Xml.XmlNameTable" />. The XmlNameTable is used when loading schemas.</summary>
		/// <param name="nametable">The XmlNameTable to use. </param>
		// Token: 0x06001479 RID: 5241 RVA: 0x000599E4 File Offset: 0x00057BE4
		public XmlSchemaCollection(XmlNameTable nameTable) : this(new XmlSchemaSet(nameTable))
		{
			this.schemaSet.ValidationEventHandler += this.OnValidationError;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00059A0C File Offset: 0x00057C0C
		internal XmlSchemaCollection(XmlSchemaSet schemaSet)
		{
			this.schemaSet = schemaSet;
		}

		/// <summary>Sets an event handler for receiving information about the XDR and XML schema validation errors.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600147B RID: 5243 RVA: 0x00059A1C File Offset: 0x00057C1C
		// (remove) Token: 0x0600147C RID: 5244 RVA: 0x00059A38 File Offset: 0x00057C38
		public event ValidationEventHandler ValidationEventHandler;

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.Count" />.</summary>
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00059A54 File Offset: 0x00057C54
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollection.CopyTo(System.Xml.Schema.XmlSchema[],System.Int32)" />.</summary>
		/// <param name="array">The array to copy the objects to. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying will begin. </param>
		// Token: 0x0600147E RID: 5246 RVA: 0x00059A5C File Offset: 0x00057C5C
		void ICollection.CopyTo(Array array, int index)
		{
			XmlSchemaSet obj = this.schemaSet;
			lock (obj)
			{
				this.schemaSet.CopyTo(array, index);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.System.Collections.ICollection.IsSynchronized" />.</summary>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00059AAC File Offset: 0x00057CAC
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollection.GetEnumerator" />.</summary>
		// Token: 0x06001480 RID: 5248 RVA: 0x00059AB0 File Offset: 0x00057CB0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.System.Collections.ICollection.SyncRoot" />.</summary>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00059AB8 File Offset: 0x00057CB8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00059ABC File Offset: 0x00057CBC
		internal XmlSchemaSet SchemaSet
		{
			get
			{
				return this.schemaSet;
			}
		}

		/// <summary>Gets the number of namespaces defined in this collection.</summary>
		/// <returns>The number of namespaces defined in this collection.</returns>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00059AC4 File Offset: 0x00057CC4
		public int Count
		{
			get
			{
				return this.schemaSet.Count;
			}
		}

		/// <summary>Gets the default XmlNameTable used by the XmlSchemaCollection when loading new schemas.</summary>
		/// <returns>An XmlNameTable.</returns>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00059AD4 File Offset: 0x00057CD4
		public XmlNameTable NameTable
		{
			get
			{
				return this.schemaSet.NameTable;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchema" /> associated with the given namespace URI.</summary>
		/// <returns>The XmlSchema associated with the namespace URI; null if there is no loaded schema associated with the given namespace or if the namespace is associated with an XDR schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema you want to return. This will typically be the targetNamespace of the schema. </param>
		// Token: 0x17000648 RID: 1608
		public XmlSchema this[string ns]
		{
			get
			{
				ICollection collection = this.schemaSet.Schemas(ns);
				if (collection == null)
				{
					return null;
				}
				IEnumerator enumerator = collection.GetEnumerator();
				if (enumerator.MoveNext())
				{
					return (XmlSchema)enumerator.Current;
				}
				return null;
			}
		}

		/// <summary>Adds the schema contained in the <see cref="T:System.Xml.XmlReader" /> to the schema collection.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> added to the schema collection; null if the schema being added is an XDR schema or if there are compilation errors in the schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the targetNamespace. </param>
		/// <param name="reader">
		///   <see cref="T:System.Xml.XmlReader" /> containing the schema to add. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06001486 RID: 5254 RVA: 0x00059B28 File Offset: 0x00057D28
		public XmlSchema Add(string ns, XmlReader reader)
		{
			return this.Add(ns, reader, new XmlUrlResolver());
		}

		/// <summary>Adds the schema contained in the <see cref="T:System.Xml.XmlReader" /> to the schema collection. The specified <see cref="T:System.Xml.XmlResolver" /> is used to resolve any external resources.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> added to the schema collection; null if the schema being added is an XDR schema or if there are compilation errors in the schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the targetNamespace. </param>
		/// <param name="reader">
		///   <see cref="T:System.Xml.XmlReader" /> containing the schema to add. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces referenced in include and import elements or x-schema attribute (XDR schemas). If this is null, external references are not resolved. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06001487 RID: 5255 RVA: 0x00059B38 File Offset: 0x00057D38
		public XmlSchema Add(string ns, XmlReader reader, XmlResolver resolver)
		{
			XmlSchema xmlSchema = XmlSchema.Read(reader, this.ValidationEventHandler);
			if (xmlSchema.TargetNamespace == null)
			{
				xmlSchema.TargetNamespace = ns;
			}
			else if (ns != null && xmlSchema.TargetNamespace != ns)
			{
				throw new XmlSchemaException("The actual targetNamespace in the schema does not match the parameter.");
			}
			return this.Add(xmlSchema);
		}

		/// <summary>Adds the schema located by the given URL into the schema collection.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> added to the schema collection; null if the schema being added is an XDR schema or if there are compilation errors in the schema. </returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the targetNamespace. </param>
		/// <param name="uri">The URL that specifies the schema to load. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06001488 RID: 5256 RVA: 0x00059B94 File Offset: 0x00057D94
		public XmlSchema Add(string ns, string uri)
		{
			XmlReader xmlReader = new XmlTextReader(uri);
			XmlSchema result;
			try
			{
				result = this.Add(ns, xmlReader);
			}
			finally
			{
				xmlReader.Close();
			}
			return result;
		}

		/// <summary>Adds the <see cref="T:System.Xml.Schema.XmlSchema" /> to the collection.</summary>
		/// <returns>The XmlSchema object.</returns>
		/// <param name="schema">The XmlSchema to add to the collection. </param>
		// Token: 0x06001489 RID: 5257 RVA: 0x00059BE0 File Offset: 0x00057DE0
		public XmlSchema Add(XmlSchema schema)
		{
			return this.Add(schema, new XmlUrlResolver());
		}

		/// <summary>Adds the <see cref="T:System.Xml.Schema.XmlSchema" /> to the collection. The specified <see cref="T:System.Xml.XmlResolver" /> is used to resolve any external references.</summary>
		/// <returns>The XmlSchema added to the schema collection.</returns>
		/// <param name="schema">The XmlSchema to add to the collection. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces referenced in include and import elements. If this is null, external references are not resolved. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x0600148A RID: 5258 RVA: 0x00059BF0 File Offset: 0x00057DF0
		public XmlSchema Add(XmlSchema schema, XmlResolver resolver)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet(this.schemaSet.NameTable);
			xmlSchemaSet.Add(this.schemaSet);
			xmlSchemaSet.Add(schema);
			xmlSchemaSet.ValidationEventHandler += this.ValidationEventHandler;
			xmlSchemaSet.XmlResolver = resolver;
			xmlSchemaSet.Compile();
			if (!xmlSchemaSet.IsCompiled)
			{
				return null;
			}
			this.schemaSet = xmlSchemaSet;
			return schema;
		}

		/// <summary>Adds all the namespaces defined in the given collection (including their associated schemas) to this collection.</summary>
		/// <param name="schema">The XmlSchemaCollection you want to add to this collection. </param>
		// Token: 0x0600148B RID: 5259 RVA: 0x00059C64 File Offset: 0x00057E64
		public void Add(XmlSchemaCollection schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet(this.schemaSet.NameTable);
			xmlSchemaSet.Add(this.schemaSet);
			xmlSchemaSet.Add(schema.schemaSet);
			xmlSchemaSet.ValidationEventHandler += this.ValidationEventHandler;
			xmlSchemaSet.XmlResolver = this.schemaSet.XmlResolver;
			xmlSchemaSet.Compile();
			if (!xmlSchemaSet.IsCompiled)
			{
				return;
			}
			this.schemaSet = xmlSchemaSet;
		}

		/// <summary>Gets a value indicating whether a schema with the specified namespace is in the collection.</summary>
		/// <returns>true if a schema with the specified namespace is in the collection; otherwise, false.</returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the target namespace. </param>
		// Token: 0x0600148C RID: 5260 RVA: 0x00059CE4 File Offset: 0x00057EE4
		public bool Contains(string ns)
		{
			XmlSchemaSet obj = this.schemaSet;
			bool result;
			lock (obj)
			{
				result = this.schemaSet.Contains(ns);
			}
			return result;
		}

		/// <summary>Gets a value indicating whether the targetNamespace of the specified <see cref="T:System.Xml.Schema.XmlSchema" /> is in the collection.</summary>
		/// <returns>true if there is a schema in the collection with the same targetNamespace; otherwise, false.</returns>
		/// <param name="schema">The XmlSchema object. </param>
		// Token: 0x0600148D RID: 5261 RVA: 0x00059D3C File Offset: 0x00057F3C
		public bool Contains(XmlSchema schema)
		{
			XmlSchemaSet obj = this.schemaSet;
			bool result;
			lock (obj)
			{
				result = this.schemaSet.Contains(schema);
			}
			return result;
		}

		/// <summary>Copies all the XmlSchema objects from this collection into the given array starting at the given index.</summary>
		/// <param name="array">The array to copy the objects to. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying will begin. </param>
		// Token: 0x0600148E RID: 5262 RVA: 0x00059D94 File Offset: 0x00057F94
		public void CopyTo(XmlSchema[] array, int index)
		{
			XmlSchemaSet obj = this.schemaSet;
			lock (obj)
			{
				this.schemaSet.CopyTo(array, index);
			}
		}

		/// <summary>Provides support for the "for each" style iteration over the collection of schemas.</summary>
		/// <returns>An enumerator for iterating over all schemas in the current collection.</returns>
		// Token: 0x0600148F RID: 5263 RVA: 0x00059DE4 File Offset: 0x00057FE4
		public XmlSchemaCollectionEnumerator GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.schemaSet.Schemas());
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00059DF8 File Offset: 0x00057FF8
		private void OnValidationError(object o, ValidationEventArgs e)
		{
			if (this.ValidationEventHandler != null)
			{
				this.ValidationEventHandler(o, e);
			}
			else if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x040007DB RID: 2011
		private XmlSchemaSet schemaSet;
	}
}
