using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Xml.XmlNode" /> objects) contains objects that represent any XML element that has no corresponding member in the object being serialized or deserialized.</summary>
	// Token: 0x0200027E RID: 638
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlAnyElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class.</summary>
		// Token: 0x060019C0 RID: 6592 RVA: 0x00087F94 File Offset: 0x00086194
		public XmlAnyElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		// Token: 0x060019C1 RID: 6593 RVA: 0x00087FA4 File Offset: 0x000861A4
		public XmlAnyElementAttribute(string name)
		{
			this.elementName = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document and its XML namespace.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		/// <param name="ns">The XML namespace of the XML element. </param>
		// Token: 0x060019C2 RID: 6594 RVA: 0x00087FBC File Offset: 0x000861BC
		public XmlAnyElementAttribute(string name, string ns)
		{
			this.elementName = name;
			this.ns = ns;
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The name of the XML element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The element name of an array member does not match the element name specified by the <see cref="P:System.Xml.Serialization.XmlAnyElementAttribute.Name" /> property. </exception>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00087FDC File Offset: 0x000861DC
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x00087FF8 File Offset: 0x000861F8
		public string Name
		{
			get
			{
				if (this.elementName == null)
				{
					return string.Empty;
				}
				return this.elementName;
			}
			set
			{
				this.elementName = value;
			}
		}

		/// <summary>Gets or sets the XML namespace generated in the XML document.</summary>
		/// <returns>An XML namespace.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00088004 File Offset: 0x00086204
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0008800C File Offset: 0x0008620C
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.isNamespaceSpecified = true;
				this.ns = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0008801C File Offset: 0x0008621C
		internal bool NamespaceSpecified
		{
			get
			{
				return this.isNamespaceSpecified;
			}
		}

		/// <summary>Gets or sets the explicit order in which the elements are serialized or deserialized.</summary>
		/// <returns>The order of the code generation.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00088024 File Offset: 0x00086224
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x0008802C File Offset: 0x0008622C
		[MonoTODO]
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00088038 File Offset: 0x00086238
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XAEA ");
			KeyHelper.AddField(sb, 1, this.ns);
			KeyHelper.AddField(sb, 2, this.elementName);
			sb.Append('|');
		}

		// Token: 0x04000AA9 RID: 2729
		private string elementName;

		// Token: 0x04000AAA RID: 2730
		private string ns;

		// Token: 0x04000AAB RID: 2731
		private bool isNamespaceSpecified;

		// Token: 0x04000AAC RID: 2732
		private int order = -1;
	}
}
