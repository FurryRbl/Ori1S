using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>Contains a cache of XML Schema definition language (XSD) schemas. </summary>
	// Token: 0x02000237 RID: 567
	public class XmlSchemaSet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class.</summary>
		// Token: 0x06001677 RID: 5751 RVA: 0x00066D90 File Offset: 0x00064F90
		public XmlSchemaSet() : this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlNameTable" /> object passed as a parameter is null.</exception>
		// Token: 0x06001678 RID: 5752 RVA: 0x00066DA0 File Offset: 0x00064FA0
		public XmlSchemaSet(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
			this.schemas = new ArrayList();
			this.CompilationId = Guid.NewGuid();
		}

		/// <summary>Sets an event handler for receiving information about XML Schema definition language (XSD) schema validation errors.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001679 RID: 5753 RVA: 0x00066DF8 File Offset: 0x00064FF8
		// (remove) Token: 0x0600167A RID: 5754 RVA: 0x00066E14 File Offset: 0x00065014
		public event ValidationEventHandler ValidationEventHandler;

		/// <summary>Gets the number of logical XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The number of logical schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00066E30 File Offset: 0x00065030
		public int Count
		{
			get
			{
				return this.schemas.Count;
			}
		}

		/// <summary>Gets all the global attributes in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00066E40 File Offset: 0x00065040
		public XmlSchemaObjectTable GlobalAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectTable();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets all the global elements in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00066E60 File Offset: 0x00065060
		public XmlSchemaObjectTable GlobalElements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new XmlSchemaObjectTable();
				}
				return this.elements;
			}
		}

		/// <summary>Gets all of the global simple and complex types in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00066E80 File Offset: 0x00065080
		public XmlSchemaObjectTable GlobalTypes
		{
			get
			{
				if (this.types == null)
				{
					this.types = new XmlSchemaObjectTable();
				}
				return this.types;
			}
		}

		/// <summary>Indicates if the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> have been compiled.</summary>
		/// <returns>Returns true if the schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> have been compiled since the last time a schema was added or removed from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, false.</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00066EA0 File Offset: 0x000650A0
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled;
			}
		}

		/// <summary>Gets the default <see cref="T:System.Xml.XmlNameTable" /> used by the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> when loading new XML Schema definition language (XSD) schemas.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNameTable" />.</returns>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00066EA8 File Offset: 0x000650A8
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> for the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> for the <see cref="T:System.Xml.Schema.XmlSchemaSet" />. The default is an <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> instance with the <see cref="P:System.Xml.Schema.XmlSchemaCompilationSettings.EnableUpaCheck" /> property set to true.</returns>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00066EB0 File Offset: 0x000650B0
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x00066EB8 File Offset: 0x000650B8
		public XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.settings;
			}
			set
			{
				this.settings = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces or locations referenced in include and import elements of a schema.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces or locations referenced in include and import elements of a schema.</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00066ED0 File Offset: 0x000650D0
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x00066EC4 File Offset: 0x000650C4
		public XmlResolver XmlResolver
		{
			internal get
			{
				return this.xmlResolver;
			}
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00066ED8 File Offset: 0x000650D8
		internal Hashtable IDCollection
		{
			get
			{
				if (this.idCollection == null)
				{
					this.idCollection = new Hashtable();
				}
				return this.idCollection;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00066EF8 File Offset: 0x000650F8
		internal XmlSchemaObjectTable NamedIdentities
		{
			get
			{
				if (this.namedIdentities == null)
				{
					this.namedIdentities = new XmlSchemaObjectTable();
				}
				return this.namedIdentities;
			}
		}

		/// <summary>Adds the XML Schema definition language (XSD) schema at the URL specified to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then null is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <param name="targetNamespace">The schema targetNamespace property, or null to use the targetNamespace specified in the schema.</param>
		/// <param name="schemaUri">The URL that specifies the schema to load.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The URL passed as a parameter is null or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06001687 RID: 5767 RVA: 0x00066F18 File Offset: 0x00065118
		public XmlSchema Add(string targetNamespace, string url)
		{
			XmlTextReader xmlTextReader = null;
			XmlSchema result;
			try
			{
				xmlTextReader = new XmlTextReader(url, this.nameTable);
				result = this.Add(targetNamespace, xmlTextReader);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		/// <summary>Adds the XML Schema definition language (XSD) schema contained in the <see cref="T:System.Xml.XmlReader" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then null is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <param name="targetNamespace">The schema targetNamespace property, or null to use the targetNamespace specified in the schema.</param>
		/// <param name="schemaDocument">The <see cref="T:System.Xml.XmlReader" /> object.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object passed as a parameter is null.</exception>
		// Token: 0x06001688 RID: 5768 RVA: 0x00066F74 File Offset: 0x00065174
		public XmlSchema Add(string targetNamespace, XmlReader reader)
		{
			XmlSchema xmlSchema = XmlSchema.Read(reader, this.ValidationEventHandler);
			if (xmlSchema.TargetNamespace == null)
			{
				xmlSchema.TargetNamespace = targetNamespace;
			}
			else if (targetNamespace != null && xmlSchema.TargetNamespace != targetNamespace)
			{
				throw new XmlSchemaException("The actual targetNamespace in the schema does not match the parameter.");
			}
			this.Add(xmlSchema);
			return xmlSchema;
		}

		/// <summary>Adds all the XML Schema definition language (XSD) schemas in the given <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemas">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">A schema in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object passed as a parameter is null.</exception>
		// Token: 0x06001689 RID: 5769 RVA: 0x00066FD0 File Offset: 0x000651D0
		[MonoTODO]
		public void Add(XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in schemaSet.schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (!this.schemas.Contains(xmlSchema))
				{
					arrayList.Add(xmlSchema);
				}
			}
			foreach (object obj2 in arrayList)
			{
				XmlSchema schema = (XmlSchema)obj2;
				this.Add(schema);
			}
		}

		/// <summary>Adds the given <see cref="T:System.Xml.Schema.XmlSchema" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then null is returned and the appropriate validation event is raised. Otherwise an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to add to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter is null.</exception>
		// Token: 0x0600168A RID: 5770 RVA: 0x000670BC File Offset: 0x000652BC
		public XmlSchema Add(XmlSchema schema)
		{
			this.schemas.Add(schema);
			this.ResetCompile();
			return schema;
		}

		/// <summary>Compiles the XML Schema definition language (XSD) schemas added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into one logical schema.</summary>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An error occurred when validating and compiling the schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</exception>
		// Token: 0x0600168B RID: 5771 RVA: 0x000670D4 File Offset: 0x000652D4
		public void Compile()
		{
			this.ClearGlobalComponents();
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.schemas);
			this.IDCollection.Clear();
			this.NamedIdentities.Clear();
			Hashtable handledUris = new Hashtable();
			foreach (object obj in arrayList)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (!xmlSchema.IsCompiled)
				{
					xmlSchema.CompileSubset(this.ValidationEventHandler, this, this.xmlResolver, handledUris);
				}
			}
			foreach (object obj2 in arrayList)
			{
				XmlSchema xmlSchema2 = (XmlSchema)obj2;
				foreach (object obj3 in xmlSchema2.Elements.Values)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj3;
					xmlSchemaElement.FillSubstitutionElementInfo();
				}
			}
			foreach (object obj4 in arrayList)
			{
				XmlSchema xmlSchema3 = (XmlSchema)obj4;
				xmlSchema3.Validate(this.ValidationEventHandler);
			}
			foreach (object obj5 in arrayList)
			{
				XmlSchema schema = (XmlSchema)obj5;
				this.AddGlobalComponents(schema);
			}
			this.isCompiled = true;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00067324 File Offset: 0x00065524
		private void ClearGlobalComponents()
		{
			this.GlobalElements.Clear();
			this.GlobalAttributes.Clear();
			this.GlobalTypes.Clear();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00067354 File Offset: 0x00065554
		private void AddGlobalComponents(XmlSchema schema)
		{
			foreach (object obj in schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				this.GlobalElements.Add(xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			foreach (object obj2 in schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				this.GlobalAttributes.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
			}
			foreach (object obj3 in schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				this.GlobalTypes.Add(xmlSchemaType.QualifiedName, xmlSchemaType);
			}
		}

		/// <summary>Indicates whether an XML Schema definition language (XSD) schema with the specified target namespace URI is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>Returns true if a schema with the specified target namespace URI is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, false.</returns>
		/// <param name="targetNamespace">The schema targetNamespace property.</param>
		// Token: 0x0600168E RID: 5774 RVA: 0x000674C0 File Offset: 0x000656C0
		public bool Contains(string targetNamespace)
		{
			targetNamespace = this.GetSafeNs(targetNamespace);
			foreach (object obj in this.schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (this.GetSafeNs(xmlSchema.TargetNamespace) == targetNamespace)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Indicates whether the specified XML Schema definition language (XSD) <see cref="T:System.Xml.Schema.XmlSchema" /> object is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.Schema.XmlSchema" /> object is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, false.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> passed as a parameter is null.</exception>
		// Token: 0x0600168F RID: 5775 RVA: 0x00067554 File Offset: 0x00065754
		public bool Contains(XmlSchema targetNamespace)
		{
			foreach (object obj in this.schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (xmlSchema == targetNamespace)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchema" /> objects from the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to the given array, starting at the given index.</summary>
		/// <param name="schemas">The array to copy the objects to.</param>
		/// <param name="index">The index in the array where copying will begin.</param>
		// Token: 0x06001690 RID: 5776 RVA: 0x000675D0 File Offset: 0x000657D0
		public void CopyTo(XmlSchema[] array, int index)
		{
			this.schemas.CopyTo(array, index);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000675E0 File Offset: 0x000657E0
		internal void CopyTo(Array array, int index)
		{
			this.schemas.CopyTo(array, index);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000675F0 File Offset: 0x000657F0
		private string GetSafeNs(string ns)
		{
			return (ns != null) ? ns : string.Empty;
		}

		/// <summary>Removes the specified XML Schema definition language (XSD) schema from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object removed from the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> or null if the schema was not found in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to remove from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not a valid schema.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> passed as a parameter is null.</exception>
		// Token: 0x06001693 RID: 5779 RVA: 0x00067604 File Offset: 0x00065804
		[MonoTODO]
		public XmlSchema Remove(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.schemas);
			if (!arrayList.Contains(schema))
			{
				return null;
			}
			if (!schema.IsCompiled)
			{
				schema.CompileSubset(this.ValidationEventHandler, this, this.xmlResolver);
			}
			this.schemas.Remove(schema);
			this.ResetCompile();
			return schema;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00067674 File Offset: 0x00065874
		private void ResetCompile()
		{
			this.isCompiled = false;
			this.ClearGlobalComponents();
		}

		/// <summary>Removes the specified XML Schema definition language (XSD) schema and all the schemas it imports from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.Schema.XmlSchema" /> object and all its imports were successfully removed; otherwise, false.</returns>
		/// <param name="schemaToRemove">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to remove from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> passed as a parameter is null.</exception>
		// Token: 0x06001695 RID: 5781 RVA: 0x00067684 File Offset: 0x00065884
		public bool RemoveRecursive(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.schemas);
			if (!arrayList.Contains(schema))
			{
				return false;
			}
			arrayList.Remove(schema);
			this.schemas.Remove(schema);
			if (!this.IsCompiled)
			{
				return true;
			}
			this.ClearGlobalComponents();
			foreach (object obj in arrayList)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (xmlSchema.IsCompiled)
				{
					this.AddGlobalComponents(schema);
				}
			}
			return true;
		}

		/// <summary>Reprocesses an XML Schema definition language (XSD) schema that already exists in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is a valid schema. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, null is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <param name="schema">The schema to reprocess.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter does not already exist in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</exception>
		// Token: 0x06001696 RID: 5782 RVA: 0x00067754 File Offset: 0x00065954
		public XmlSchema Reprocess(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.schemas);
			if (!arrayList.Contains(schema))
			{
				throw new ArgumentException("Target schema is not contained in the schema set.");
			}
			this.ClearGlobalComponents();
			foreach (object obj in arrayList)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (schema == xmlSchema)
				{
					schema.CompileSubset(this.ValidationEventHandler, this, this.xmlResolver);
				}
				if (xmlSchema.IsCompiled)
				{
					this.AddGlobalComponents(schema);
				}
			}
			return (!schema.IsCompiled) ? null : schema;
		}

		/// <summary>Returns a collection of all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing all the schemas that have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />. If no schemas have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />, an empty <see cref="T:System.Collections.ICollection" /> object is returned.</returns>
		// Token: 0x06001697 RID: 5783 RVA: 0x00067838 File Offset: 0x00065A38
		public ICollection Schemas()
		{
			return this.schemas;
		}

		/// <summary>Returns a collection of all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that belong to the given namespace.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing all the schemas that have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that belong to the given namespace. If no schemas have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />, an empty <see cref="T:System.Collections.ICollection" /> object is returned.</returns>
		/// <param name="targetNamespace">The schema targetNamespace property.</param>
		// Token: 0x06001698 RID: 5784 RVA: 0x00067840 File Offset: 0x00065A40
		public ICollection Schemas(string targetNamespace)
		{
			targetNamespace = this.GetSafeNs(targetNamespace);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (this.GetSafeNs(xmlSchema.TargetNamespace) == targetNamespace)
				{
					arrayList.Add(xmlSchema);
				}
			}
			return arrayList;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000678D8 File Offset: 0x00065AD8
		internal bool MissedSubComponents(string targetNamespace)
		{
			foreach (object obj in this.Schemas(targetNamespace))
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (xmlSchema.missedSubComponents)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040008FF RID: 2303
		private XmlNameTable nameTable;

		// Token: 0x04000900 RID: 2304
		private XmlResolver xmlResolver = new XmlUrlResolver();

		// Token: 0x04000901 RID: 2305
		private ArrayList schemas;

		// Token: 0x04000902 RID: 2306
		private XmlSchemaObjectTable attributes;

		// Token: 0x04000903 RID: 2307
		private XmlSchemaObjectTable elements;

		// Token: 0x04000904 RID: 2308
		private XmlSchemaObjectTable types;

		// Token: 0x04000905 RID: 2309
		private Hashtable idCollection;

		// Token: 0x04000906 RID: 2310
		private XmlSchemaObjectTable namedIdentities;

		// Token: 0x04000907 RID: 2311
		private XmlSchemaCompilationSettings settings = new XmlSchemaCompilationSettings();

		// Token: 0x04000908 RID: 2312
		private bool isCompiled;

		// Token: 0x04000909 RID: 2313
		internal Guid CompilationId;
	}
}
