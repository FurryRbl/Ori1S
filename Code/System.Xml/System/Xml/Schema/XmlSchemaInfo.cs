using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x02000222 RID: 546
	[MonoTODO]
	public class XmlSchemaInfo : IXmlSchemaInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> class.</summary>
		// Token: 0x060015CD RID: 5581 RVA: 0x00064310 File Offset: 0x00062510
		public XmlSchemaInfo()
		{
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00064318 File Offset: 0x00062518
		internal XmlSchemaInfo(IXmlSchemaInfo info)
		{
			this.isDefault = info.IsDefault;
			this.isNil = info.IsNil;
			this.memberType = info.MemberType;
			this.attr = info.SchemaAttribute;
			this.elem = info.SchemaElement;
			this.type = info.SchemaType;
			this.validity = info.Validity;
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object that corresponds to the content type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00064380 File Offset: 0x00062580
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x00064388 File Offset: 0x00062588
		[MonoTODO]
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		/// <summary>Gets or sets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>A bool value.</returns>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00064394 File Offset: 0x00062594
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x0006439C File Offset: 0x0006259C
		[MonoTODO]
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		/// <summary>Gets or sets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>A bool value.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000643A8 File Offset: 0x000625A8
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x000643B0 File Offset: 0x000625B0
		[MonoTODO]
		public bool IsNil
		{
			get
			{
				return this.isNil;
			}
			set
			{
				this.isNil = value;
			}
		}

		/// <summary>Gets or sets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000643BC File Offset: 0x000625BC
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000643C4 File Offset: 0x000625C4
		[MonoTODO]
		public XmlSchemaSimpleType MemberType
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

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object.</returns>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000643D0 File Offset: 0x000625D0
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x000643D8 File Offset: 0x000625D8
		[MonoTODO]
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.attr;
			}
			set
			{
				this.attr = value;
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object.</returns>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000643E4 File Offset: 0x000625E4
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x000643EC File Offset: 0x000625EC
		[MonoTODO]
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.elem;
			}
			set
			{
				this.elem = value;
			}
		}

		/// <summary>Gets or sets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x000643F8 File Offset: 0x000625F8
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x00064400 File Offset: 0x00062600
		[MonoTODO]
		public XmlSchemaType SchemaType
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

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value.</returns>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0006440C File Offset: 0x0006260C
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x00064414 File Offset: 0x00062614
		[MonoTODO]
		public XmlSchemaValidity Validity
		{
			get
			{
				return this.validity;
			}
			set
			{
				this.validity = value;
			}
		}

		// Token: 0x040008C5 RID: 2245
		private bool isDefault;

		// Token: 0x040008C6 RID: 2246
		private bool isNil;

		// Token: 0x040008C7 RID: 2247
		private XmlSchemaSimpleType memberType;

		// Token: 0x040008C8 RID: 2248
		private XmlSchemaAttribute attr;

		// Token: 0x040008C9 RID: 2249
		private XmlSchemaElement elem;

		// Token: 0x040008CA RID: 2250
		private XmlSchemaType type;

		// Token: 0x040008CB RID: 2251
		private XmlSchemaValidity validity;

		// Token: 0x040008CC RID: 2252
		private XmlSchemaContentType contentType;
	}
}
