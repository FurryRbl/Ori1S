using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Contains a mapping of one type to another.</summary>
	// Token: 0x020002C6 RID: 710
	public class XmlTypeMapping : XmlMapping
	{
		// Token: 0x06001DC7 RID: 7623 RVA: 0x0009C334 File Offset: 0x0009A534
		internal XmlTypeMapping(string elementName, string ns, TypeData typeData, string xmlType, string xmlTypeNamespace) : base(elementName, ns)
		{
			this.type = typeData;
			this.xmlType = xmlType;
			this.xmlTypeNamespace = xmlTypeNamespace;
		}

		/// <summary>The fully qualified type name that includes the namespace (or namespaces) and type.</summary>
		/// <returns>The fully qualified type name.</returns>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0009C368 File Offset: 0x0009A568
		public string TypeFullName
		{
			get
			{
				return this.type.FullTypeName;
			}
		}

		/// <summary>Gets the type name of the mapped object.</summary>
		/// <returns>The type name of the mapped object.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0009C378 File Offset: 0x0009A578
		public string TypeName
		{
			get
			{
				return this.type.TypeName;
			}
		}

		/// <summary>Gets the XML element name of the mapped object.</summary>
		/// <returns>The XML element name of the mapped object. The default is the class name of the object.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0009C388 File Offset: 0x0009A588
		public string XsdTypeName
		{
			get
			{
				return this.XmlType;
			}
		}

		/// <summary>Gets the XML namespace of the mapped object.</summary>
		/// <returns>The XML namespace of the mapped object. The default is an empty string ("").</returns>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x0009C390 File Offset: 0x0009A590
		public string XsdTypeNamespace
		{
			get
			{
				return this.XmlTypeNamespace;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0009C398 File Offset: 0x0009A598
		internal TypeData TypeData
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0009C3A0 File Offset: 0x0009A5A0
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0009C3A8 File Offset: 0x0009A5A8
		internal string XmlType
		{
			get
			{
				return this.xmlType;
			}
			set
			{
				this.xmlType = value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0009C3B4 File Offset: 0x0009A5B4
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0009C3BC File Offset: 0x0009A5BC
		internal string XmlTypeNamespace
		{
			get
			{
				return this.xmlTypeNamespace;
			}
			set
			{
				this.xmlTypeNamespace = value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0009C3C8 File Offset: 0x0009A5C8
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0009C3D0 File Offset: 0x0009A5D0
		internal ArrayList DerivedTypes
		{
			get
			{
				return this._derivedTypes;
			}
			set
			{
				this._derivedTypes = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0009C3DC File Offset: 0x0009A5DC
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0009C3E4 File Offset: 0x0009A5E4
		internal bool MultiReferenceType
		{
			get
			{
				return this.multiReferenceType;
			}
			set
			{
				this.multiReferenceType = value;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0009C3F0 File Offset: 0x0009A5F0
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0009C3F8 File Offset: 0x0009A5F8
		internal XmlTypeMapping BaseMap
		{
			get
			{
				return this.baseMap;
			}
			set
			{
				this.baseMap = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0009C404 File Offset: 0x0009A604
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0009C40C File Offset: 0x0009A60C
		internal bool IsSimpleType
		{
			get
			{
				return this.isSimpleType;
			}
			set
			{
				this.isSimpleType = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0009C424 File Offset: 0x0009A624
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0009C418 File Offset: 0x0009A618
		internal string Documentation
		{
			get
			{
				return this.documentation;
			}
			set
			{
				this.documentation = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0009C42C File Offset: 0x0009A62C
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0009C434 File Offset: 0x0009A634
		internal bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0009C440 File Offset: 0x0009A640
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0009C448 File Offset: 0x0009A648
		internal bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullable = value;
			}
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0009C454 File Offset: 0x0009A654
		internal XmlTypeMapping GetRealTypeMap(Type objectType)
		{
			if (this.TypeData.SchemaType == SchemaTypes.Enum)
			{
				return this;
			}
			if (this.TypeData.Type == objectType)
			{
				return this;
			}
			for (int i = 0; i < this._derivedTypes.Count; i++)
			{
				XmlTypeMapping xmlTypeMapping = (XmlTypeMapping)this._derivedTypes[i];
				if (xmlTypeMapping.TypeData.Type == objectType)
				{
					return xmlTypeMapping;
				}
			}
			return null;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0009C4CC File Offset: 0x0009A6CC
		internal XmlTypeMapping GetRealElementMap(string name, string ens)
		{
			if (this.xmlType == name && this.xmlTypeNamespace == ens)
			{
				return this;
			}
			foreach (object obj in this._derivedTypes)
			{
				XmlTypeMapping xmlTypeMapping = (XmlTypeMapping)obj;
				if (xmlTypeMapping.xmlType == name && xmlTypeMapping.xmlTypeNamespace == ens)
				{
					return xmlTypeMapping;
				}
			}
			return null;
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0009C584 File Offset: 0x0009A784
		internal void UpdateRoot(XmlQualifiedName qname)
		{
			if (qname != null)
			{
				this._elementName = qname.Name;
				this._namespace = qname.Namespace;
			}
		}

		// Token: 0x04000BC8 RID: 3016
		private string xmlType;

		// Token: 0x04000BC9 RID: 3017
		private string xmlTypeNamespace;

		// Token: 0x04000BCA RID: 3018
		private TypeData type;

		// Token: 0x04000BCB RID: 3019
		private XmlTypeMapping baseMap;

		// Token: 0x04000BCC RID: 3020
		private bool multiReferenceType;

		// Token: 0x04000BCD RID: 3021
		private bool isSimpleType;

		// Token: 0x04000BCE RID: 3022
		private string documentation;

		// Token: 0x04000BCF RID: 3023
		private bool includeInSchema;

		// Token: 0x04000BD0 RID: 3024
		private bool isNullable = true;

		// Token: 0x04000BD1 RID: 3025
		private ArrayList _derivedTypes = new ArrayList();
	}
}
