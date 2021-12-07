using System;
using System.CodeDom.Compiler;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Maps a code entity in a .NET Framework Web service method to an element in a Web Services Description Language (WSDL) message.</summary>
	// Token: 0x02000292 RID: 658
	public class XmlMemberMapping
	{
		// Token: 0x06001AA9 RID: 6825 RVA: 0x0008AC50 File Offset: 0x00088E50
		internal XmlMemberMapping(string memberName, string defaultNamespace, XmlTypeMapMember mapMem, bool encodedFormat)
		{
			this._mapMember = mapMem;
			this._memberName = memberName;
			if (mapMem is XmlTypeMapMemberAnyElement)
			{
				XmlTypeMapMemberAnyElement xmlTypeMapMemberAnyElement = (XmlTypeMapMemberAnyElement)mapMem;
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)xmlTypeMapMemberAnyElement.ElementInfo[xmlTypeMapMemberAnyElement.ElementInfo.Count - 1];
				this._elementName = xmlTypeMapElementInfo.ElementName;
				this._namespace = xmlTypeMapElementInfo.Namespace;
				if (xmlTypeMapElementInfo.MappedType != null)
				{
					this._typeNamespace = xmlTypeMapElementInfo.MappedType.Namespace;
				}
				else
				{
					this._typeNamespace = string.Empty;
				}
			}
			else if (mapMem is XmlTypeMapMemberElement)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo2 = (XmlTypeMapElementInfo)((XmlTypeMapMemberElement)mapMem).ElementInfo[0];
				this._elementName = xmlTypeMapElementInfo2.ElementName;
				if (encodedFormat)
				{
					this._namespace = defaultNamespace;
					if (xmlTypeMapElementInfo2.MappedType != null)
					{
						this._typeNamespace = string.Empty;
					}
					else
					{
						this._typeNamespace = xmlTypeMapElementInfo2.DataTypeNamespace;
					}
				}
				else
				{
					this._namespace = xmlTypeMapElementInfo2.Namespace;
					if (xmlTypeMapElementInfo2.MappedType != null)
					{
						this._typeNamespace = xmlTypeMapElementInfo2.MappedType.Namespace;
					}
					else
					{
						this._typeNamespace = string.Empty;
					}
					this._form = xmlTypeMapElementInfo2.Form;
				}
			}
			else
			{
				this._elementName = this._memberName;
				this._namespace = string.Empty;
			}
			if (this._form == XmlSchemaForm.None)
			{
				this._form = XmlSchemaForm.Qualified;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the .NET Framework type maps to an XML element or attribute of any type. </summary>
		/// <returns>true, if the type maps to an XML any element or attribute; otherwise, false.</returns>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0008ADC4 File Offset: 0x00088FC4
		public bool Any
		{
			get
			{
				return this._mapMember is XmlTypeMapMemberAnyElement;
			}
		}

		/// <summary>Gets the unqualified name of the XML element declaration that applies to this mapping. </summary>
		/// <returns>The unqualified name of the XML element declaration that applies to this mapping.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0008ADD4 File Offset: 0x00088FD4
		public string ElementName
		{
			get
			{
				return this._elementName;
			}
		}

		/// <summary>Gets the name of the Web service method member that is represented by this mapping. </summary>
		/// <returns>The name of the Web service method member represented by this mapping.</returns>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0008ADDC File Offset: 0x00088FDC
		public string MemberName
		{
			get
			{
				return this._memberName;
			}
		}

		/// <summary>Gets the XML namespace that applies to this mapping. </summary>
		/// <returns>The XML namespace that applies to this mapping.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0008ADE4 File Offset: 0x00088FE4
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
		}

		/// <summary>Gets the fully qualified type name of the .NET Framework type for this mapping. </summary>
		/// <returns>The fully qualified type name of the .NET Framework type for this mapping.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0008ADEC File Offset: 0x00088FEC
		public string TypeFullName
		{
			get
			{
				return this._mapMember.TypeData.FullTypeName;
			}
		}

		/// <summary>Gets the type name of the .NET Framework type for this mapping. </summary>
		/// <returns>The type name of the .NET Framework type for this mapping.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0008AE00 File Offset: 0x00089000
		public string TypeName
		{
			get
			{
				return this._mapMember.TypeData.XmlType;
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type for this mapping.</summary>
		/// <returns>The namespace of the .NET Framework type for this mapping.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0008AE14 File Offset: 0x00089014
		public string TypeNamespace
		{
			get
			{
				return this._typeNamespace;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0008AE1C File Offset: 0x0008901C
		internal XmlTypeMapMember TypeMapMember
		{
			get
			{
				return this._mapMember;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0008AE24 File Offset: 0x00089024
		internal XmlSchemaForm Form
		{
			get
			{
				return this._form;
			}
		}

		/// <summary>Gets the XML element name as it appears in the service description document.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0008AE2C File Offset: 0x0008902C
		public string XsdElementName
		{
			get
			{
				return this._mapMember.Name;
			}
		}

		/// <summary>Returns the name of the type associated with the specified <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />.</summary>
		/// <returns>The name of the type.</returns>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  that contains the name of the type.</param>
		// Token: 0x06001AB4 RID: 6836 RVA: 0x0008AE3C File Offset: 0x0008903C
		public string GenerateTypeName(CodeDomProvider codeProvider)
		{
			string text = codeProvider.CreateValidIdentifier(this._mapMember.TypeData.FullTypeName);
			return (!this._mapMember.TypeData.IsValueType || !this._mapMember.TypeData.IsNullable) ? text : ("System.Nullable`1[" + text + "]");
		}

		/// <summary>Gets a value that indicates whether the accompanying field in the .NET Framework type has a value specified.</summary>
		/// <returns>true, if the accompanying field has a value specified; otherwise, false.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0008AEA0 File Offset: 0x000890A0
		public bool CheckSpecified
		{
			get
			{
				return this._mapMember.IsOptionalValueType;
			}
		}

		// Token: 0x04000AEB RID: 2795
		private XmlTypeMapMember _mapMember;

		// Token: 0x04000AEC RID: 2796
		private string _elementName;

		// Token: 0x04000AED RID: 2797
		private string _memberName;

		// Token: 0x04000AEE RID: 2798
		private string _namespace;

		// Token: 0x04000AEF RID: 2799
		private string _typeNamespace;

		// Token: 0x04000AF0 RID: 2800
		private XmlSchemaForm _form;
	}
}
