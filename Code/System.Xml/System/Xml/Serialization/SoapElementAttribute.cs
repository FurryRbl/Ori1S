using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the public member value be serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> as an encoded SOAP XML element.</summary>
	// Token: 0x02000270 RID: 624
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class.</summary>
		// Token: 0x06001941 RID: 6465 RVA: 0x00085028 File Offset: 0x00083228
		public SoapElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class and specifies the name of the XML element.</summary>
		/// <param name="elementName">The XML element name of the serialized member. </param>
		// Token: 0x06001942 RID: 6466 RVA: 0x00085030 File Offset: 0x00083230
		public SoapElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the XML Schema definition language (XSD) data type of the generated XML element.</summary>
		/// <returns>One of the XML Schema data types.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00085040 File Offset: 0x00083240
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x0008505C File Offset: 0x0008325C
		public string DataType
		{
			get
			{
				if (this.dataType == null)
				{
					return string.Empty;
				}
				return this.dataType;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00085068 File Offset: 0x00083268
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x00085084 File Offset: 0x00083284
		public string ElementName
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that has the xsi:null attribute set to "1".</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:null attribute; otherwise, false.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00085090 File Offset: 0x00083290
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x00085098 File Offset: 0x00083298
		public bool IsNullable
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

		// Token: 0x06001949 RID: 6473 RVA: 0x000850A4 File Offset: 0x000832A4
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("SEA ");
			KeyHelper.AddField(sb, 1, this.elementName);
			KeyHelper.AddField(sb, 2, this.dataType);
			KeyHelper.AddField(sb, 3, this.isNullable);
			sb.Append('|');
		}

		// Token: 0x04000A80 RID: 2688
		private string dataType;

		// Token: 0x04000A81 RID: 2689
		private string elementName;

		// Token: 0x04000A82 RID: 2690
		private bool isNullable;
	}
}
