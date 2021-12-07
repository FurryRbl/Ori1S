using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between .NET Framework Web service methods and Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x02000293 RID: 659
	public class XmlMembersMapping : XmlMapping
	{
		// Token: 0x06001AB6 RID: 6838 RVA: 0x0008AEB0 File Offset: 0x000890B0
		internal XmlMembersMapping()
		{
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0008AEB8 File Offset: 0x000890B8
		internal XmlMembersMapping(XmlMemberMapping[] mapping) : this(string.Empty, null, false, false, mapping)
		{
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0008AECC File Offset: 0x000890CC
		internal XmlMembersMapping(string elementName, string ns, XmlMemberMapping[] mapping) : this(elementName, ns, true, false, mapping)
		{
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0008AEDC File Offset: 0x000890DC
		internal XmlMembersMapping(string elementName, string ns, bool hasWrapperElement, bool writeAccessors, XmlMemberMapping[] mapping) : base(elementName, ns)
		{
			this._hasWrapperElement = hasWrapperElement;
			this._mapping = mapping;
			ClassMap classMap = new ClassMap();
			classMap.IgnoreMemberNamespace = writeAccessors;
			foreach (XmlMemberMapping xmlMemberMapping in mapping)
			{
				classMap.AddMember(xmlMemberMapping.TypeMapMember);
			}
			base.ObjectMap = classMap;
		}

		/// <summary>Gets the number of .NET Framework code entities that belong to a Web service method to which a SOAP message is being mapped. </summary>
		/// <returns>The number of mappings in the collection.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0008AF3C File Offset: 0x0008913C
		public int Count
		{
			get
			{
				return this._mapping.Length;
			}
		}

		/// <summary>Gets an item that contains internal type mapping information for a .NET Framework code entity that belongs to a Web service method being mapped to a SOAP message.</summary>
		/// <returns>The requested <see cref="T:System.Xml.Serialization.XmlMemberMapping" />.</returns>
		/// <param name="index">The index of the mapping to return.</param>
		// Token: 0x170007BD RID: 1981
		public XmlMemberMapping this[int index]
		{
			get
			{
				return this._mapping[index];
			}
		}

		/// <summary>Gets the name of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The name of the .NET Framework type.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0008AF54 File Offset: 0x00089154
		public string TypeName
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The .NET Framework namespace of the mapping.</returns>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0008AF5C File Offset: 0x0008915C
		public string TypeNamespace
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0008AF64 File Offset: 0x00089164
		internal bool HasWrapperElement
		{
			get
			{
				return this._hasWrapperElement;
			}
		}

		// Token: 0x04000AF1 RID: 2801
		private bool _hasWrapperElement;

		// Token: 0x04000AF2 RID: 2802
		private XmlMemberMapping[] _mapping;
	}
}
