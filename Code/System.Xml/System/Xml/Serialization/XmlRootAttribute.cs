using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Controls XML serialization of the attribute target as an XML root element.</summary>
	// Token: 0x0200029C RID: 668
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.ReturnValue)]
	public class XmlRootAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class.</summary>
		// Token: 0x06001B11 RID: 6929 RVA: 0x0008D6F4 File Offset: 0x0008B8F4
		public XmlRootAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class and specifies the name of the XML root element.</summary>
		/// <param name="elementName">The name of the XML root element. </param>
		// Token: 0x06001B12 RID: 6930 RVA: 0x0008D704 File Offset: 0x0008B904
		public XmlRootAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the XSD data type of the XML root element.</summary>
		/// <returns>An XSD (XML Schema Document) data type, as defined by the World Wide Web Consortium (www.w3.org) document named "XML Schema: DataTypes".</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0008D71C File Offset: 0x0008B91C
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0008D738 File Offset: 0x0008B938
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

		/// <summary>Gets or sets the name of the XML element that is generated and recognized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class's <see cref="M:System.Xml.Serialization.XmlSerializer.Serialize(System.IO.TextWriter,System.Object)" /> and <see cref="M:System.Xml.Serialization.XmlSerializer.Deserialize(System.IO.Stream)" /> methods, respectively.</summary>
		/// <returns>The name of the XML root element that is generated and recognized in an XML-document instance. The default is the name of the serialized class.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0008D744 File Offset: 0x0008B944
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0008D760 File Offset: 0x0008B960
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that is set to null into the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0008D76C File Offset: 0x0008B96C
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0008D774 File Offset: 0x0008B974
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullableSpecified = true;
				this.isNullable = value;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0008D784 File Offset: 0x0008B984
		public bool IsNullableSpecified
		{
			get
			{
				return this.isNullableSpecified;
			}
		}

		/// <summary>Gets or sets the namespace for the XML root element.</summary>
		/// <returns>The namespace for the XML element.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0008D78C File Offset: 0x0008B98C
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x0008D794 File Offset: 0x0008B994
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

		// Token: 0x06001B1C RID: 6940 RVA: 0x0008D7A0 File Offset: 0x0008B9A0
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XRA ");
			KeyHelper.AddField(sb, 1, this.ns);
			KeyHelper.AddField(sb, 2, this.elementName);
			KeyHelper.AddField(sb, 3, this.dataType);
			KeyHelper.AddField(sb, 4, this.isNullable);
			sb.Append('|');
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0008D7F8 File Offset: 0x0008B9F8
		internal string Key
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.AddKeyHash(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x04000B19 RID: 2841
		private string dataType;

		// Token: 0x04000B1A RID: 2842
		private string elementName;

		// Token: 0x04000B1B RID: 2843
		private bool isNullable = true;

		// Token: 0x04000B1C RID: 2844
		private bool isNullableSpecified;

		// Token: 0x04000B1D RID: 2845
		private string ns;
	}
}
