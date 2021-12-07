using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between code entities in .NET Framework Web service methods and the content of Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x0200029B RID: 667
	public class XmlReflectionMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlReflectionMember" /> class. </summary>
		// Token: 0x06001AFF RID: 6911 RVA: 0x0008D56C File Offset: 0x0008B76C
		public XmlReflectionMember()
		{
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0008D574 File Offset: 0x0008B774
		internal XmlReflectionMember(string name, Type type, XmlAttributes attributes)
		{
			this.memberName = name;
			this.memberType = type;
			this.xmlAttributes = attributes;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0008D594 File Offset: 0x0008B794
		internal XmlReflectionMember(string name, Type type, SoapAttributes attributes)
		{
			this.memberName = name;
			this.memberType = type;
			this.soapAttributes = attributes;
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlReflectionMember" /> represents a Web service method return value, as opposed to an output parameter. </summary>
		/// <returns>true, if the member represents a Web service return value; otherwise, false.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0008D5B4 File Offset: 0x0008B7B4
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x0008D5BC File Offset: 0x0008B7BC
		public bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		/// <summary>Gets or sets the name of the Web service method member for this mapping. </summary>
		/// <returns>The name of the Web service method.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0008D5C8 File Offset: 0x0008B7C8
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0008D5D0 File Offset: 0x0008B7D0
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		/// <summary>Gets or sets the type of the Web service method member code entity that is represented by this mapping. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the Web service method member code entity that is represented by this mapping.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0008D5DC File Offset: 0x0008B7DC
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0008D5E4 File Offset: 0x0008B7E4
		public Type MemberType
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

		/// <summary>Gets or sets a value that indicates that the value of the corresponding XML element definition's isNullable attribute is false.</summary>
		/// <returns>True to override the <see cref="P:System.Xml.Serialization.XmlElementAttribute.IsNullable" /> property; otherwise, false.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0008D5F0 File Offset: 0x0008B7F0
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x0008D5F8 File Offset: 0x0008B7F8
		public bool OverrideIsNullable
		{
			get
			{
				return this.overrideIsNullable;
			}
			set
			{
				this.overrideIsNullable = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapAttributes" /> with the collection of SOAP-related attributes that have been applied to the member code entity. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that contains the objects that represent SOAP attributes applied to the member.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0008D604 File Offset: 0x0008B804
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x0008D624 File Offset: 0x0008B824
		public SoapAttributes SoapAttributes
		{
			get
			{
				if (this.soapAttributes == null)
				{
					this.soapAttributes = new SoapAttributes();
				}
				return this.soapAttributes;
			}
			set
			{
				this.soapAttributes = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.Serialization.XmlAttributes" /> with the collection of <see cref="T:System.Xml.Serialization.XmlSerializer" />-related attributes that have been applied to the member code entity. </summary>
		/// <returns>An <see cref="T:System.XML.Serialization.XmlAttributes" /> that represents XML attributes that have been applied to the member code.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x0008D630 File Offset: 0x0008B830
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x0008D650 File Offset: 0x0008B850
		public XmlAttributes XmlAttributes
		{
			get
			{
				if (this.xmlAttributes == null)
				{
					this.xmlAttributes = new XmlAttributes();
				}
				return this.xmlAttributes;
			}
			set
			{
				this.xmlAttributes = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0008D65C File Offset: 0x0008B85C
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x0008D664 File Offset: 0x0008B864
		internal Type DeclaringType
		{
			get
			{
				return this.declaringType;
			}
			set
			{
				this.declaringType = value;
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0008D670 File Offset: 0x0008B870
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XRM ");
			KeyHelper.AddField(sb, 1, this.isReturnValue);
			KeyHelper.AddField(sb, 1, this.memberName);
			KeyHelper.AddField(sb, 1, this.memberType);
			KeyHelper.AddField(sb, 1, this.overrideIsNullable);
			if (this.soapAttributes != null)
			{
				this.soapAttributes.AddKeyHash(sb);
			}
			if (this.xmlAttributes != null)
			{
				this.xmlAttributes.AddKeyHash(sb);
			}
			sb.Append('|');
		}

		// Token: 0x04000B12 RID: 2834
		private bool isReturnValue;

		// Token: 0x04000B13 RID: 2835
		private string memberName;

		// Token: 0x04000B14 RID: 2836
		private Type memberType;

		// Token: 0x04000B15 RID: 2837
		private bool overrideIsNullable;

		// Token: 0x04000B16 RID: 2838
		private SoapAttributes soapAttributes;

		// Token: 0x04000B17 RID: 2839
		private XmlAttributes xmlAttributes;

		// Token: 0x04000B18 RID: 2840
		private Type declaringType;
	}
}
