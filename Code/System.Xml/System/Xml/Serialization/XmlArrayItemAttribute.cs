using System;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Specifies the derived types that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> can place in a serialized array.</summary>
	// Token: 0x02000281 RID: 641
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlArrayItemAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class.</summary>
		// Token: 0x060019E2 RID: 6626 RVA: 0x00088258 File Offset: 0x00086458
		public XmlArrayItemAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		// Token: 0x060019E3 RID: 6627 RVA: 0x00088260 File Offset: 0x00086460
		public XmlArrayItemAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the <see cref="T:System.Type" /> that can be inserted into the serialized array.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x060019E4 RID: 6628 RVA: 0x00088270 File Offset: 0x00086470
		public XmlArrayItemAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document and the <see cref="T:System.Type" /> that can be inserted into the generated XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x060019E5 RID: 6629 RVA: 0x00088280 File Offset: 0x00086480
		public XmlArrayItemAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		/// <summary>Gets or sets the XML data type of the generated XML element.</summary>
		/// <returns>An XML Schema definition (XSD) data type, as defined by the World Wide Web Consortium (www.w3.org) document "XML Schema Part 2: DataTypes".</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00088298 File Offset: 0x00086498
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x000882B4 File Offset: 0x000864B4
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
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000882C0 File Offset: 0x000864C0
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x000882DC File Offset: 0x000864DC
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

		/// <summary>Gets or sets a value that indicates whether the name of the generated XML element is qualified.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is XmlSchemaForm.None.</returns>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Form" /> property is set to XmlSchemaForm.Unqualified and a <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Namespace" /> value is specified. </exception>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x000882E8 File Offset: 0x000864E8
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x000882F0 File Offset: 0x000864F0
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		/// <summary>Gets or sets the namespace of the generated XML element.</summary>
		/// <returns>The namespace of the generated XML element.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x000882FC File Offset: 0x000864FC
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x00088304 File Offset: 0x00086504
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member as an empty XML tag with the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false, and no instance is generated. The default is true.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00088310 File Offset: 0x00086510
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x00088318 File Offset: 0x00086518
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

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00088328 File Offset: 0x00086528
		internal bool IsNullableSpecified
		{
			get
			{
				return this.isNullableSpecified;
			}
		}

		/// <summary>Gets or sets the type allowed in an array.</summary>
		/// <returns>A <see cref="T:System.Type" /> that is allowed in the array.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00088330 File Offset: 0x00086530
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00088338 File Offset: 0x00086538
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the level in a hierarchy of XML elements that the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> affects.</summary>
		/// <returns>The zero-based index of a set of indexes in an array of arrays.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00088344 File Offset: 0x00086544
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0008834C File Offset: 0x0008654C
		public int NestingLevel
		{
			get
			{
				return this.nestingLevel;
			}
			set
			{
				this.nestingLevel = value;
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00088358 File Offset: 0x00086558
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XAIA ");
			KeyHelper.AddField(sb, 1, this.ns);
			KeyHelper.AddField(sb, 2, this.elementName);
			KeyHelper.AddField(sb, 3, this.form.ToString(), XmlSchemaForm.None.ToString());
			KeyHelper.AddField(sb, 4, this.isNullable, true);
			KeyHelper.AddField(sb, 5, this.dataType);
			KeyHelper.AddField(sb, 6, this.nestingLevel, 0);
			KeyHelper.AddField(sb, 7, this.type);
			sb.Append('|');
		}

		// Token: 0x04000AB2 RID: 2738
		private string dataType;

		// Token: 0x04000AB3 RID: 2739
		private string elementName;

		// Token: 0x04000AB4 RID: 2740
		private XmlSchemaForm form;

		// Token: 0x04000AB5 RID: 2741
		private string ns;

		// Token: 0x04000AB6 RID: 2742
		private bool isNullable;

		// Token: 0x04000AB7 RID: 2743
		private bool isNullableSpecified;

		// Token: 0x04000AB8 RID: 2744
		private int nestingLevel;

		// Token: 0x04000AB9 RID: 2745
		private Type type;
	}
}
