using System;
using System.IO;
using Mono.Xml;
using Mono.Xml2;

namespace System.Xml
{
	/// <summary>Represents the document type declaration.</summary>
	// Token: 0x020000F3 RID: 243
	public class XmlDocumentType : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentType" /> class.</summary>
		/// <param name="name">The qualified name; see the <see cref="P:System.Xml.XmlDocumentType.Name" /> property.</param>
		/// <param name="publicId">The public identifier; see the <see cref="P:System.Xml.XmlDocumentType.PublicId" /> property.</param>
		/// <param name="systemId">The system identifier; see the <see cref="P:System.Xml.XmlDocumentType.SystemId" /> property.</param>
		/// <param name="internalSubset">The DTD internal subset; see the <see cref="P:System.Xml.XmlDocumentType.InternalSubset" /> property.</param>
		/// <param name="doc">The parent document.</param>
		// Token: 0x06000964 RID: 2404 RVA: 0x00032D28 File Offset: 0x00030F28
		protected internal XmlDocumentType(string name, string publicId, string systemId, string internalSubset, XmlDocument doc) : base(doc)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(this.BaseURI, new StringReader(string.Empty), doc.NameTable);
			xmlTextReader.XmlResolver = doc.Resolver;
			xmlTextReader.GenerateDTDObjectModel(name, publicId, systemId, internalSubset);
			this.dtd = xmlTextReader.DTD;
			this.ImportFromDTD();
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00032D88 File Offset: 0x00030F88
		internal XmlDocumentType(DTDObjectModel dtd, XmlDocument doc) : base(doc)
		{
			this.dtd = dtd;
			this.ImportFromDTD();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00032DA0 File Offset: 0x00030FA0
		private void ImportFromDTD()
		{
			this.entities = new XmlNamedNodeMap(this);
			this.notations = new XmlNamedNodeMap(this);
			foreach (DTDNode dtdnode in this.DTD.EntityDecls.Values)
			{
				DTDEntityDeclaration dtdentityDeclaration = (DTDEntityDeclaration)dtdnode;
				XmlNode namedItem = new XmlEntity(dtdentityDeclaration.Name, dtdentityDeclaration.NotationName, dtdentityDeclaration.PublicId, dtdentityDeclaration.SystemId, this.OwnerDocument);
				this.entities.SetNamedItem(namedItem);
			}
			foreach (DTDNode dtdnode2 in this.DTD.NotationDecls.Values)
			{
				DTDNotationDeclaration dtdnotationDeclaration = (DTDNotationDeclaration)dtdnode2;
				XmlNode namedItem2 = new XmlNotation(dtdnotationDeclaration.LocalName, dtdnotationDeclaration.Prefix, dtdnotationDeclaration.PublicId, dtdnotationDeclaration.SystemId, this.OwnerDocument);
				this.notations.SetNamedItem(namedItem2);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00032EE8 File Offset: 0x000310E8
		internal DTDObjectModel DTD
		{
			get
			{
				return this.dtd;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlEntity" /> nodes declared in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the XmlEntity nodes. The returned XmlNamedNodeMap is read-only.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00032EF0 File Offset: 0x000310F0
		public XmlNamedNodeMap Entities
		{
			get
			{
				return this.entities;
			}
		}

		/// <summary>Gets the value of the document type definition (DTD) internal subset on the DOCTYPE declaration.</summary>
		/// <returns>The DTD internal subset on the DOCTYPE. If there is no DTD internal subset, String.Empty is returned.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00032EF8 File Offset: 0x000310F8
		public string InternalSubset
		{
			get
			{
				return this.dtd.InternalSubset;
			}
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because DocumentType nodes are read-only, this property always returns true.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00032F08 File Offset: 0x00031108
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00032F0C File Offset: 0x0003110C
		public override string LocalName
		{
			get
			{
				return this.dtd.Name;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00032F1C File Offset: 0x0003111C
		public override string Name
		{
			get
			{
				return this.dtd.Name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For DocumentType nodes, this value is XmlNodeType.DocumentType.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00032F2C File Offset: 0x0003112C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlNotation" /> nodes present in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the XmlNotation nodes. The returned XmlNamedNodeMap is read-only.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00032F30 File Offset: 0x00031130
		public XmlNamedNodeMap Notations
		{
			get
			{
				return this.notations;
			}
		}

		/// <summary>Gets the value of the public identifier on the DOCTYPE declaration.</summary>
		/// <returns>The public identifier on the DOCTYPE. If there is no public identifier, null is returned.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00032F38 File Offset: 0x00031138
		public string PublicId
		{
			get
			{
				return this.dtd.PublicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the DOCTYPE declaration.</summary>
		/// <returns>The system identifier on the DOCTYPE. If there is no system identifier, null is returned.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00032F48 File Offset: 0x00031148
		public string SystemId
		{
			get
			{
				return this.dtd.SystemId;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For document type nodes, the cloned node always includes the subtree, regardless of the parameter setting. </param>
		// Token: 0x06000971 RID: 2417 RVA: 0x00032F58 File Offset: 0x00031158
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlDocumentType(this.dtd, this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlDocumentType nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000972 RID: 2418 RVA: 0x00032F6C File Offset: 0x0003116C
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000973 RID: 2419 RVA: 0x00032F70 File Offset: 0x00031170
		public override void WriteTo(XmlWriter w)
		{
			w.WriteDocType(this.Name, this.PublicId, this.SystemId, this.InternalSubset);
		}

		// Token: 0x040004E0 RID: 1248
		internal XmlNamedNodeMap entities;

		// Token: 0x040004E1 RID: 1249
		internal XmlNamedNodeMap notations;

		// Token: 0x040004E2 RID: 1250
		private DTDObjectModel dtd;
	}
}
