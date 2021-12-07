﻿using System;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a particular class member as an array of XML elements.</summary>
	// Token: 0x02000280 RID: 640
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayAttribute" /> class.</summary>
		// Token: 0x060019D5 RID: 6613 RVA: 0x0008814C File Offset: 0x0008634C
		public XmlArrayAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayAttribute" /> class and specifies the XML element name generated in the XML document instance.</summary>
		/// <param name="elementName">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		// Token: 0x060019D6 RID: 6614 RVA: 0x0008815C File Offset: 0x0008635C
		public XmlArrayAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the XML element name given to the serialized array.</summary>
		/// <returns>The XML element name of the serialized array. The default is the name of the member to which the <see cref="T:System.Xml.Serialization.XmlArrayAttribute" /> is assigned.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00088174 File Offset: 0x00086374
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00088190 File Offset: 0x00086390
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

		/// <summary>Gets or sets a value that indicates whether the XML element name generated by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is qualified or unqualified.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is XmlSchemaForm.None.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0008819C File Offset: 0x0008639C
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x000881A4 File Offset: 0x000863A4
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member as an empty XML tag with the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000881B0 File Offset: 0x000863B0
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x000881B8 File Offset: 0x000863B8
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

		/// <summary>Gets or sets the namespace of the XML element.</summary>
		/// <returns>The namespace of the XML element.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x000881C4 File Offset: 0x000863C4
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x000881CC File Offset: 0x000863CC
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

		/// <summary>Gets or sets the explicit order in which the elements are serialized or deserialized.</summary>
		/// <returns>The order of the code generation.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000881D8 File Offset: 0x000863D8
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x000881E0 File Offset: 0x000863E0
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

		// Token: 0x060019E1 RID: 6625 RVA: 0x000881EC File Offset: 0x000863EC
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XAAT ");
			KeyHelper.AddField(sb, 1, this.ns);
			KeyHelper.AddField(sb, 2, this.elementName);
			KeyHelper.AddField(sb, 3, this.form.ToString(), XmlSchemaForm.None.ToString());
			KeyHelper.AddField(sb, 4, this.isNullable);
			sb.Append('|');
		}

		// Token: 0x04000AAD RID: 2733
		private string elementName;

		// Token: 0x04000AAE RID: 2734
		private XmlSchemaForm form;

		// Token: 0x04000AAF RID: 2735
		private bool isNullable;

		// Token: 0x04000AB0 RID: 2736
		private string ns;

		// Token: 0x04000AB1 RID: 2737
		private int order = -1;
	}
}
