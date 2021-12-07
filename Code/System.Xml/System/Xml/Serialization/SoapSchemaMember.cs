using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents certain attributes of a XSD &lt;part&gt; element in a WSDL document for generating classes from the document. </summary>
	// Token: 0x02000276 RID: 630
	public class SoapSchemaMember
	{
		/// <summary>Gets or sets a value that corresponds to the name attribute of the WSDL part element. </summary>
		/// <returns>The element name.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0008530C File Offset: 0x0008350C
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x00085328 File Offset: 0x00083528
		public string MemberName
		{
			get
			{
				if (this.memberName == null)
				{
					return string.Empty;
				}
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		/// <summary>Gets or sets a value that corresponds to the type attribute of the WSDL part element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that corresponds to the XML type.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00085334 File Offset: 0x00083534
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x0008533C File Offset: 0x0008353C
		public XmlQualifiedName MemberType
		{
			get
			{
				return this.memberType;
			}
			set
			{
				this.memberType = value;
			}
		}

		// Token: 0x04000A87 RID: 2695
		private string memberName;

		// Token: 0x04000A88 RID: 2696
		private XmlQualifiedName memberType = XmlQualifiedName.Empty;
	}
}
