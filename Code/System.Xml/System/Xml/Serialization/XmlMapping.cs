using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Supports mappings between .NET Framework types and XML Schema data types. </summary>
	// Token: 0x02000294 RID: 660
	public abstract class XmlMapping
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x0008AF6C File Offset: 0x0008916C
		internal XmlMapping()
		{
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0008AF74 File Offset: 0x00089174
		internal XmlMapping(string elementName, string ns)
		{
			this._elementName = elementName;
			this._namespace = ns;
		}

		/// <summary>Gets the name of the XSD element of the mapping.</summary>
		/// <returns>The XSD element name.</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0008AF8C File Offset: 0x0008918C
		[MonoTODO]
		public string XsdElementName
		{
			get
			{
				return this._elementName;
			}
		}

		/// <summary>Get the name of the mapped element.</summary>
		/// <returns>The name of the mapped element.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0008AF94 File Offset: 0x00089194
		public string ElementName
		{
			get
			{
				return this._elementName;
			}
		}

		/// <summary>Gets the namespace of the mapped element.</summary>
		/// <returns>The namespace of the mapped element.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0008AF9C File Offset: 0x0008919C
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
		}

		/// <summary>Sets the key used to look up the mapping.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the lookup key.</param>
		// Token: 0x06001AC4 RID: 6852 RVA: 0x0008AFA4 File Offset: 0x000891A4
		public void SetKey(string key)
		{
			this.key = key;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0008AFB0 File Offset: 0x000891B0
		internal string GetKey()
		{
			return this.key;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0008AFB8 File Offset: 0x000891B8
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0008AFC0 File Offset: 0x000891C0
		internal ObjectMap ObjectMap
		{
			get
			{
				return this.map;
			}
			set
			{
				this.map = value;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0008AFCC File Offset: 0x000891CC
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x0008AFD4 File Offset: 0x000891D4
		internal ArrayList RelatedMaps
		{
			get
			{
				return this.relatedMaps;
			}
			set
			{
				this.relatedMaps = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0008AFE0 File Offset: 0x000891E0
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x0008AFE8 File Offset: 0x000891E8
		internal SerializationFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0008AFF4 File Offset: 0x000891F4
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x0008AFFC File Offset: 0x000891FC
		internal SerializationSource Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x04000AF3 RID: 2803
		private ObjectMap map;

		// Token: 0x04000AF4 RID: 2804
		private ArrayList relatedMaps;

		// Token: 0x04000AF5 RID: 2805
		private SerializationFormat format;

		// Token: 0x04000AF6 RID: 2806
		private SerializationSource source;

		// Token: 0x04000AF7 RID: 2807
		internal string _elementName;

		// Token: 0x04000AF8 RID: 2808
		internal string _namespace;

		// Token: 0x04000AF9 RID: 2809
		private string key;
	}
}
