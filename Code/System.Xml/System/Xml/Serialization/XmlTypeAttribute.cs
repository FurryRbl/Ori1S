using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Controls the XML schema that is generated when the attribute target is serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x020002BA RID: 698
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class XmlTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class.</summary>
		// Token: 0x06001D58 RID: 7512 RVA: 0x0009B5E0 File Offset: 0x000997E0
		public XmlTypeAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class and specifies the name of the XML type.</summary>
		/// <param name="typeName">The name of the XML type that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates when it serializes the class instance (and recognizes when it deserializes the class instance). </param>
		// Token: 0x06001D59 RID: 7513 RVA: 0x0009B5F0 File Offset: 0x000997F0
		public XmlTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets or sets a value that determines whether the resulting schema type is an XSD anonymous type.</summary>
		/// <returns>true, if the resulting schema type is an XSD anonymous type; otherwise, false.</returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0009B608 File Offset: 0x00099808
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0009B610 File Offset: 0x00099810
		public bool AnonymousType
		{
			get
			{
				return this.anonymousType;
			}
			set
			{
				this.anonymousType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to include the type in XML schema documents.</summary>
		/// <returns>true to include the type in XML schema documents; otherwise, false.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0009B61C File Offset: 0x0009981C
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0009B624 File Offset: 0x00099824
		public bool IncludeInSchema
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

		/// <summary>Gets or sets the namespace of the XML type.</summary>
		/// <returns>The namespace of the XML type.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0009B630 File Offset: 0x00099830
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0009B638 File Offset: 0x00099838
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the name of the XML type.</summary>
		/// <returns>The name of the XML type.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0009B644 File Offset: 0x00099844
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0009B660 File Offset: 0x00099860
		public string TypeName
		{
			get
			{
				if (this.typeName == null)
				{
					return string.Empty;
				}
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0009B66C File Offset: 0x0009986C
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XTA ");
			KeyHelper.AddField(sb, 1, this.ns);
			KeyHelper.AddField(sb, 2, this.typeName);
			KeyHelper.AddField(sb, 4, this.includeInSchema);
			sb.Append('|');
		}

		// Token: 0x04000BA4 RID: 2980
		private bool includeInSchema = true;

		// Token: 0x04000BA5 RID: 2981
		private string ns;

		// Token: 0x04000BA6 RID: 2982
		private string typeName;

		// Token: 0x04000BA7 RID: 2983
		private bool anonymousType;
	}
}
