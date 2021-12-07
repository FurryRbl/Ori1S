using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for any element that can contain annotation elements.</summary>
	// Token: 0x020001F8 RID: 504
	public class XmlSchemaAnnotated : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00056850 File Offset: 0x00054A50
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x00056858 File Offset: 0x00054A58
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" /> representing the annotation property.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00056864 File Offset: 0x00054A64
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x0005686C File Offset: 0x00054A6C
		[XmlElement("annotation", Type = typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the current schema's target namespace.</summary>
		/// <returns>An array of qualified <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00056878 File Offset: 0x00054A78
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x000568C0 File Offset: 0x00054AC0
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				if (this.unhandledAttributeList != null)
				{
					this.unhandledAttributes = (XmlAttribute[])this.unhandledAttributeList.ToArray(typeof(XmlAttribute));
					this.unhandledAttributeList = null;
				}
				return this.unhandledAttributes;
			}
			set
			{
				this.unhandledAttributes = value;
				this.unhandledAttributeList = null;
			}
		}

		// Token: 0x040007A9 RID: 1961
		private XmlSchemaAnnotation annotation;

		// Token: 0x040007AA RID: 1962
		private string id;

		// Token: 0x040007AB RID: 1963
		private XmlAttribute[] unhandledAttributes;
	}
}
