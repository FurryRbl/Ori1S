using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class. Provides information about the included schema.</summary>
	// Token: 0x02000213 RID: 531
	public abstract class XmlSchemaExternal : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) location for the schema, which tells the schema processor where the schema physically resides.</summary>
		/// <returns>The URI location for the schema.Optional for imported schemas.</returns>
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00060834 File Offset: 0x0005EA34
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x0006083C File Offset: 0x0005EA3C
		[XmlAttribute("schemaLocation", DataType = "anyURI")]
		public string SchemaLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		/// <summary>Gets or sets the XmlSchema for the referenced schema.</summary>
		/// <returns>The XmlSchema for the referenced schema.</returns>
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00060848 File Offset: 0x0005EA48
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x00060850 File Offset: 0x0005EA50
		[XmlIgnore]
		public XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0006085C File Offset: 0x0005EA5C
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x00060864 File Offset: 0x0005EA64
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

		/// <summary>Gets and sets the qualified attributes, which do not belong to the schema target namespace.</summary>
		/// <returns>Qualified attributes that belong to another target namespace.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00060870 File Offset: 0x0005EA70
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x000608B8 File Offset: 0x0005EAB8
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

		// Token: 0x04000872 RID: 2162
		private string id;

		// Token: 0x04000873 RID: 2163
		private XmlSchema schema;

		// Token: 0x04000874 RID: 2164
		private string location;

		// Token: 0x04000875 RID: 2165
		private XmlAttribute[] unhandledAttributes;
	}
}
