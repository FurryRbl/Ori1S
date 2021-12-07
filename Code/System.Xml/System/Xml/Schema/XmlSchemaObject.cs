using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the root class for the Xml schema object model hierarchy and serves as a base class for classes such as the <see cref="T:System.Xml.Schema.XmlSchema" /> class.</summary>
	// Token: 0x0200022E RID: 558
	public abstract class XmlSchemaObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> class.</summary>
		// Token: 0x0600160A RID: 5642 RVA: 0x00065DE8 File Offset: 0x00063FE8
		protected XmlSchemaObject()
		{
			this.namespaces = new XmlSerializerNamespaces();
			this.unhandledAttributeList = null;
			this.CompilationId = Guid.Empty;
		}

		/// <summary>Gets or sets the line number in the file to which the schema element refers.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00065E10 File Offset: 0x00064010
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00065E18 File Offset: 0x00064018
		[XmlIgnore]
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		/// <summary>Gets or sets the line position in the file to which the schema element refers.</summary>
		/// <returns>The line position.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00065E24 File Offset: 0x00064024
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00065E2C File Offset: 0x0006402C
		[XmlIgnore]
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		/// <summary>Gets or sets the source location for the file that loaded the schema.</summary>
		/// <returns>The source location (URI) for the file.</returns>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00065E38 File Offset: 0x00064038
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00065E40 File Offset: 0x00064040
		[XmlIgnore]
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
			}
		}

		/// <summary>Gets or sets the parent of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00065E4C File Offset: 0x0006404C
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00065E54 File Offset: 0x00064054
		[XmlIgnore]
		public XmlSchemaObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00065E60 File Offset: 0x00064060
		internal XmlSchema AncestorSchema
		{
			get
			{
				for (XmlSchemaObject xmlSchemaObject = this.Parent; xmlSchemaObject != null; xmlSchemaObject = xmlSchemaObject.Parent)
				{
					if (xmlSchemaObject is XmlSchema)
					{
						return (XmlSchema)xmlSchemaObject;
					}
				}
				throw new Exception(string.Format("INTERNAL ERROR: Parent object is not set properly : {0} ({1},{2})", this.SourceUri, this.LineNumber, this.LinePosition));
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00065EC4 File Offset: 0x000640C4
		internal virtual void SetParent(XmlSchemaObject parent)
		{
			this.Parent = parent;
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> to use with this schema object.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> property for the schema object.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00065ED0 File Offset: 0x000640D0
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x00065ED8 File Offset: 0x000640D8
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00065EE4 File Offset: 0x000640E4
		internal void error(ValidationEventHandler handle, string message)
		{
			this.errorCount++;
			XmlSchemaObject.error(handle, message, null, this, null);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00065F00 File Offset: 0x00064100
		internal void warn(ValidationEventHandler handle, string message)
		{
			XmlSchemaObject.warn(handle, message, null, this, null);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00065F0C File Offset: 0x0006410C
		internal static void error(ValidationEventHandler handle, string message, Exception innerException)
		{
			XmlSchemaObject.error(handle, message, innerException, null, null);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00065F18 File Offset: 0x00064118
		internal static void warn(ValidationEventHandler handle, string message, Exception innerException)
		{
			XmlSchemaObject.warn(handle, message, innerException, null, null);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00065F24 File Offset: 0x00064124
		internal static void error(ValidationEventHandler handle, string message, Exception innerException, XmlSchemaObject xsobj, object sender)
		{
			ValidationHandler.RaiseValidationEvent(handle, innerException, message, xsobj, sender, null, XmlSeverityType.Error);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00065F34 File Offset: 0x00064134
		internal static void warn(ValidationEventHandler handle, string message, Exception innerException, XmlSchemaObject xsobj, object sender)
		{
			ValidationHandler.RaiseValidationEvent(handle, innerException, message, xsobj, sender, null, XmlSeverityType.Warning);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00065F44 File Offset: 0x00064144
		internal virtual int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			return 0;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00065F48 File Offset: 0x00064148
		internal virtual int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return 0;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00065F4C File Offset: 0x0006414C
		internal bool IsValidated(Guid validationId)
		{
			return this.ValidationId == validationId;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00065F5C File Offset: 0x0006415C
		internal virtual void CopyInfo(XmlSchemaParticle obj)
		{
			obj.LineNumber = this.LineNumber;
			obj.LinePosition = this.LinePosition;
			obj.SourceUri = this.SourceUri;
			obj.errorCount = this.errorCount;
		}

		// Token: 0x040008DD RID: 2269
		private int lineNumber;

		// Token: 0x040008DE RID: 2270
		private int linePosition;

		// Token: 0x040008DF RID: 2271
		private string sourceUri;

		// Token: 0x040008E0 RID: 2272
		private XmlSerializerNamespaces namespaces;

		// Token: 0x040008E1 RID: 2273
		internal ArrayList unhandledAttributeList;

		// Token: 0x040008E2 RID: 2274
		internal bool isCompiled;

		// Token: 0x040008E3 RID: 2275
		internal int errorCount;

		// Token: 0x040008E4 RID: 2276
		internal Guid CompilationId;

		// Token: 0x040008E5 RID: 2277
		internal Guid ValidationId;

		// Token: 0x040008E6 RID: 2278
		internal bool isRedefineChild;

		// Token: 0x040008E7 RID: 2279
		internal bool isRedefinedComponent;

		// Token: 0x040008E8 RID: 2280
		internal XmlSchemaObject redefinedObject;

		// Token: 0x040008E9 RID: 2281
		private XmlSchemaObject parent;
	}
}
