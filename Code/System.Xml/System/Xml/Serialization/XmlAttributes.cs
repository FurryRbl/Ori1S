using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of attribute objects that control how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes an object.</summary>
	// Token: 0x02000286 RID: 646
	public class XmlAttributes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributes" /> class.</summary>
		// Token: 0x06001A1D RID: 6685 RVA: 0x000887E4 File Offset: 0x000869E4
		public XmlAttributes()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributes" /> class and customizes how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes an object. </summary>
		/// <param name="provider">A class that can provide alternative implementations of attributes that control XML serialization.</param>
		// Token: 0x06001A1E RID: 6686 RVA: 0x00088824 File Offset: 0x00086A24
		public XmlAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			foreach (object obj in customAttributes)
			{
				if (obj is XmlAnyAttributeAttribute)
				{
					this.xmlAnyAttribute = (XmlAnyAttributeAttribute)obj;
				}
				else if (obj is XmlAnyElementAttribute)
				{
					this.xmlAnyElements.Add((XmlAnyElementAttribute)obj);
				}
				else if (obj is XmlArrayAttribute)
				{
					this.xmlArray = (XmlArrayAttribute)obj;
				}
				else if (obj is XmlArrayItemAttribute)
				{
					this.xmlArrayItems.Add((XmlArrayItemAttribute)obj);
				}
				else if (obj is XmlAttributeAttribute)
				{
					this.xmlAttribute = (XmlAttributeAttribute)obj;
				}
				else if (obj is XmlChoiceIdentifierAttribute)
				{
					this.xmlChoiceIdentifier = (XmlChoiceIdentifierAttribute)obj;
				}
				else if (obj is DefaultValueAttribute)
				{
					this.xmlDefaultValue = ((DefaultValueAttribute)obj).Value;
				}
				else if (obj is XmlElementAttribute)
				{
					this.xmlElements.Add((XmlElementAttribute)obj);
				}
				else if (obj is XmlEnumAttribute)
				{
					this.xmlEnum = (XmlEnumAttribute)obj;
				}
				else if (obj is XmlIgnoreAttribute)
				{
					this.xmlIgnore = true;
				}
				else if (obj is XmlNamespaceDeclarationsAttribute)
				{
					this.xmlns = true;
				}
				else if (obj is XmlRootAttribute)
				{
					this.xmlRoot = (XmlRootAttribute)obj;
				}
				else if (obj is XmlTextAttribute)
				{
					this.xmlText = (XmlTextAttribute)obj;
				}
				else if (obj is XmlTypeAttribute)
				{
					this.xmlType = (XmlTypeAttribute)obj;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> to override.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> to override.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00088A18 File Offset: 0x00086C18
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x00088A20 File Offset: 0x00086C20
		public XmlAnyAttributeAttribute XmlAnyAttribute
		{
			get
			{
				return this.xmlAnyAttribute;
			}
			set
			{
				this.xmlAnyAttribute = value;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects to override.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAnyElementAttributes" /> object that represents the collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00088A2C File Offset: 0x00086C2C
		public XmlAnyElementAttributes XmlAnyElements
		{
			get
			{
				return this.xmlAnyElements;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property that returns an array.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlArrayAttribute" /> that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property that returns an array.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00088A34 File Offset: 0x00086C34
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00088A3C File Offset: 0x00086C3C
		public XmlArrayAttribute XmlArray
		{
			get
			{
				return this.xmlArray;
			}
			set
			{
				this.xmlArray = value;
			}
		}

		/// <summary>Gets or sets a collection of objects that specify how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes items inserted into an array returned by a public field or read/write property.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlArrayItemAttributes" /> object that contains a collection of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00088A48 File Offset: 0x00086C48
		public XmlArrayItemAttributes XmlArrayItems
		{
			get
			{
				return this.xmlArrayItems;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or public read/write property as an XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeAttribute" /> that controls the serialization of a public field or read/write property as an XML attribute.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00088A50 File Offset: 0x00086C50
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x00088A58 File Offset: 0x00086C58
		public XmlAttributeAttribute XmlAttribute
		{
			get
			{
				return this.xmlAttribute;
			}
			set
			{
				this.xmlAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that allows you to distinguish between a set of choices.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> that can be applied to a class member that is serialized as an xsi:choice element.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00088A64 File Offset: 0x00086C64
		public XmlChoiceIdentifierAttribute XmlChoiceIdentifier
		{
			get
			{
				return this.xmlChoiceIdentifier;
			}
		}

		/// <summary>Gets or sets the default value of an XML element or attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of an XML element or attribute.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00088A6C File Offset: 0x00086C6C
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x00088A74 File Offset: 0x00086C74
		public object XmlDefaultValue
		{
			get
			{
				return this.xmlDefaultValue;
			}
			set
			{
				this.xmlDefaultValue = value;
			}
		}

		/// <summary>Gets a collection of objects that specify how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or read/write property as an XML element.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementAttributes" /> that contains a collection of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x00088A80 File Offset: 0x00086C80
		public XmlElementAttributes XmlElements
		{
			get
			{
				return this.xmlElements;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x00088A88 File Offset: 0x00086C88
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x00088A90 File Offset: 0x00086C90
		public XmlEnumAttribute XmlEnum
		{
			get
			{
				return this.xmlEnum;
			}
			set
			{
				this.xmlEnum = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether or not the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or public read/write property.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must not serialize the field or property; otherwise, false.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x00088A9C File Offset: 0x00086C9C
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x00088AA4 File Offset: 0x00086CA4
		public bool XmlIgnore
		{
			get
			{
				return this.xmlIgnore;
			}
			set
			{
				this.xmlIgnore = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to keep all namespace declarations when an object containing a member that returns an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object is overridden.</summary>
		/// <returns>true if the namespace declarations should be kept; otherwise, false.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x00088AB0 File Offset: 0x00086CB0
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x00088AB8 File Offset: 0x00086CB8
		public bool Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a class as an XML root element.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that overrides a class attributed as an XML root element.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00088AC4 File Offset: 0x00086CC4
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x00088ACC File Offset: 0x00086CCC
		public XmlRootAttribute XmlRoot
		{
			get
			{
				return this.xmlRoot;
			}
			set
			{
				this.xmlRoot = value;
			}
		}

		/// <summary>Gets or sets an object that instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize a public field or public read/write property as XML text.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTextAttribute" /> that overrides the default serialization of a public property or field.</returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00088AD8 File Offset: 0x00086CD8
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x00088AE0 File Offset: 0x00086CE0
		public XmlTextAttribute XmlText
		{
			get
			{
				return this.xmlText;
			}
			set
			{
				this.xmlText = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a class to which the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> has been applied.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> that overrides an <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> applied to a class declaration.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00088AEC File Offset: 0x00086CEC
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x00088AF4 File Offset: 0x00086CF4
		public XmlTypeAttribute XmlType
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

		// Token: 0x06001A37 RID: 6711 RVA: 0x00088B00 File Offset: 0x00086D00
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XA ");
			KeyHelper.AddField(sb, 1, this.xmlIgnore);
			KeyHelper.AddField(sb, 2, this.xmlns);
			KeyHelper.AddField(sb, 3, this.xmlAnyAttribute != null);
			this.xmlAnyElements.AddKeyHash(sb);
			this.xmlArrayItems.AddKeyHash(sb);
			this.xmlElements.AddKeyHash(sb);
			if (this.xmlArray != null)
			{
				this.xmlArray.AddKeyHash(sb);
			}
			if (this.xmlAttribute != null)
			{
				this.xmlAttribute.AddKeyHash(sb);
			}
			if (this.xmlDefaultValue == null)
			{
				sb.Append("n");
			}
			else if (!(this.xmlDefaultValue is DBNull))
			{
				string str = XmlCustomFormatter.ToXmlString(TypeTranslator.GetTypeData(this.xmlDefaultValue.GetType()), this.xmlDefaultValue);
				sb.Append("v" + str);
			}
			if (this.xmlEnum != null)
			{
				this.xmlEnum.AddKeyHash(sb);
			}
			if (this.xmlRoot != null)
			{
				this.xmlRoot.AddKeyHash(sb);
			}
			if (this.xmlText != null)
			{
				this.xmlText.AddKeyHash(sb);
			}
			if (this.xmlType != null)
			{
				this.xmlType.AddKeyHash(sb);
			}
			if (this.xmlChoiceIdentifier != null)
			{
				this.xmlChoiceIdentifier.AddKeyHash(sb);
			}
			sb.Append("|");
		}

		// Token: 0x04000AC5 RID: 2757
		private XmlAnyAttributeAttribute xmlAnyAttribute;

		// Token: 0x04000AC6 RID: 2758
		private XmlAnyElementAttributes xmlAnyElements = new XmlAnyElementAttributes();

		// Token: 0x04000AC7 RID: 2759
		private XmlArrayAttribute xmlArray;

		// Token: 0x04000AC8 RID: 2760
		private XmlArrayItemAttributes xmlArrayItems = new XmlArrayItemAttributes();

		// Token: 0x04000AC9 RID: 2761
		private XmlAttributeAttribute xmlAttribute;

		// Token: 0x04000ACA RID: 2762
		private XmlChoiceIdentifierAttribute xmlChoiceIdentifier;

		// Token: 0x04000ACB RID: 2763
		private object xmlDefaultValue = DBNull.Value;

		// Token: 0x04000ACC RID: 2764
		private XmlElementAttributes xmlElements = new XmlElementAttributes();

		// Token: 0x04000ACD RID: 2765
		private XmlEnumAttribute xmlEnum;

		// Token: 0x04000ACE RID: 2766
		private bool xmlIgnore;

		// Token: 0x04000ACF RID: 2767
		private bool xmlns;

		// Token: 0x04000AD0 RID: 2768
		private XmlRootAttribute xmlRoot;

		// Token: 0x04000AD1 RID: 2769
		private XmlTextAttribute xmlText;

		// Token: 0x04000AD2 RID: 2770
		private XmlTypeAttribute xmlType;
	}
}
