using System;
using System.Collections;
using System.IO;
using System.Text;
using Mono.Xml;
using Mono.Xml2;

namespace System.Xml
{
	/// <summary>Provides all the context information required by the <see cref="T:System.Xml.XmlReader" /> to parse an XML fragment.</summary>
	// Token: 0x02000112 RID: 274
	public class XmlParserContext
	{
		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, xml:lang, and xml:space values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000B09 RID: 2825 RVA: 0x00039AC8 File Offset: 0x00037CC8
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace) : this(nt, nsMgr, null, null, null, null, null, xmlLang, xmlSpace, null)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, xml:lang, xml:space, and encoding.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <param name="enc">An <see cref="T:System.Text.Encoding" /> object indicating the encoding setting. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000B0A RID: 2826 RVA: 0x00039AE8 File Offset: 0x00037CE8
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace, Encoding enc) : this(nt, nsMgr, null, null, null, null, null, xmlLang, xmlSpace, enc)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, base URI, xml:lang, xml:space, and document type values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="docTypeName">The name of the document type declaration. </param>
		/// <param name="pubId">The public identifier. </param>
		/// <param name="sysId">The system identifier. </param>
		/// <param name="internalSubset">The internal DTD subset. The internal DTD subset is used for entity resolution, not document validation.</param>
		/// <param name="baseURI">The base URI for the XML fragment (the location from which the fragment was loaded). </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000B0B RID: 2827 RVA: 0x00039B08 File Offset: 0x00037D08
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace) : this(nt, nsMgr, docTypeName, pubId, sysId, internalSubset, baseURI, xmlLang, xmlSpace, null)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, base URI, xml:lang, xml:space, encoding, and document type values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="docTypeName">The name of the document type declaration. </param>
		/// <param name="pubId">The public identifier. </param>
		/// <param name="sysId">The system identifier. </param>
		/// <param name="internalSubset">The internal DTD subset. The internal DTD subset is used for entity resolution, not document validation.</param>
		/// <param name="baseURI">The base URI for the XML fragment (the location from which the fragment was loaded). </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <param name="enc">An <see cref="T:System.Text.Encoding" /> object indicating the encoding setting. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000B0C RID: 2828 RVA: 0x00039B2C File Offset: 0x00037D2C
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace, Encoding enc) : this(nt, nsMgr, (docTypeName == null || !(docTypeName != string.Empty)) ? null : new XmlTextReader(TextReader.Null, nt).GenerateDTDObjectModel(docTypeName, pubId, sysId, internalSubset), baseURI, xmlLang, xmlSpace, enc)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00039B7C File Offset: 0x00037D7C
		internal XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, DTDObjectModel dtd, string baseURI, string xmlLang, XmlSpace xmlSpace, Encoding enc)
		{
			this.namespaceManager = nsMgr;
			this.nameTable = ((nt == null) ? ((nsMgr == null) ? null : nsMgr.NameTable) : nt);
			if (dtd != null)
			{
				this.DocTypeName = dtd.Name;
				this.PublicId = dtd.PublicId;
				this.SystemId = dtd.SystemId;
				this.InternalSubset = dtd.InternalSubset;
				this.dtd = dtd;
			}
			this.encoding = enc;
			this.BaseURI = baseURI;
			this.XmlLang = xmlLang;
			this.xmlSpace = xmlSpace;
			this.contextItems = new ArrayList();
		}

		/// <summary>Gets or sets the base URI.</summary>
		/// <returns>The base URI to use to resolve the DTD file.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00039C64 File Offset: 0x00037E64
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x00039C6C File Offset: 0x00037E6C
		public string BaseURI
		{
			get
			{
				return this.baseURI;
			}
			set
			{
				this.baseURI = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of the document type declaration.</summary>
		/// <returns>The name of the document type declaration.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00039C88 File Offset: 0x00037E88
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x00039CC8 File Offset: 0x00037EC8
		public string DocTypeName
		{
			get
			{
				return (this.docTypeName == null) ? ((this.dtd == null) ? null : this.dtd.Name) : this.docTypeName;
			}
			set
			{
				this.docTypeName = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00039CE4 File Offset: 0x00037EE4
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00039CEC File Offset: 0x00037EEC
		internal DTDObjectModel Dtd
		{
			get
			{
				return this.dtd;
			}
			set
			{
				this.dtd = value;
			}
		}

		/// <summary>Gets or sets the encoding type.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object indicating the encoding type.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00039CF8 File Offset: 0x00037EF8
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00039D00 File Offset: 0x00037F00
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the internal DTD subset.</summary>
		/// <returns>The internal DTD subset. For example, this property returns everything between the square brackets &lt;!DOCTYPE doc [...]&gt;.The internal DTD subset is used for entity resolution, not document validation.</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00039D0C File Offset: 0x00037F0C
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00039D4C File Offset: 0x00037F4C
		public string InternalSubset
		{
			get
			{
				return (this.internalSubset == null) ? ((this.dtd == null) ? null : this.dtd.InternalSubset) : this.internalSubset;
			}
			set
			{
				this.internalSubset = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>The XmlNamespaceManager.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00039D68 File Offset: 0x00037F68
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x00039D70 File Offset: 0x00037F70
		public XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.namespaceManager;
			}
			set
			{
				this.namespaceManager = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> used to atomize strings. For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <returns>The XmlNameTable.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00039D7C File Offset: 0x00037F7C
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00039D84 File Offset: 0x00037F84
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
			set
			{
				this.nameTable = value;
			}
		}

		/// <summary>Gets or sets the public identifier.</summary>
		/// <returns>The public identifier.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00039D90 File Offset: 0x00037F90
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x00039DD0 File Offset: 0x00037FD0
		public string PublicId
		{
			get
			{
				return (this.publicID == null) ? ((this.dtd == null) ? null : this.dtd.PublicId) : this.publicID;
			}
			set
			{
				this.publicID = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the system identifier.</summary>
		/// <returns>The system identifier.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00039DEC File Offset: 0x00037FEC
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00039E2C File Offset: 0x0003802C
		public string SystemId
		{
			get
			{
				return (this.systemID == null) ? ((this.dtd == null) ? null : this.dtd.SystemId) : this.systemID;
			}
			set
			{
				this.systemID = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope. If there is no xml:lang in scope, String.Empty is returned.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00039E48 File Offset: 0x00038048
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x00039E50 File Offset: 0x00038050
		public string XmlLang
		{
			get
			{
				return this.xmlLang;
			}
			set
			{
				this.xmlLang = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the current xml:space scope.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00039E6C File Offset: 0x0003806C
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00039E74 File Offset: 0x00038074
		public XmlSpace XmlSpace
		{
			get
			{
				return this.xmlSpace;
			}
			set
			{
				this.xmlSpace = value;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00039E80 File Offset: 0x00038080
		internal void PushScope()
		{
			XmlParserContext.ContextItem contextItem;
			if (this.contextItems.Count == this.contextItemCount)
			{
				contextItem = new XmlParserContext.ContextItem();
				this.contextItems.Add(contextItem);
			}
			else
			{
				contextItem = (XmlParserContext.ContextItem)this.contextItems[this.contextItemCount];
			}
			contextItem.BaseURI = this.BaseURI;
			contextItem.XmlLang = this.XmlLang;
			contextItem.XmlSpace = this.XmlSpace;
			this.contextItemCount++;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00039F08 File Offset: 0x00038108
		internal void PopScope()
		{
			if (this.contextItemCount == 0)
			{
				throw new XmlException("Unexpected end of element scope.");
			}
			this.contextItemCount--;
			XmlParserContext.ContextItem contextItem = (XmlParserContext.ContextItem)this.contextItems[this.contextItemCount];
			this.baseURI = contextItem.BaseURI;
			this.xmlLang = contextItem.XmlLang;
			this.xmlSpace = contextItem.XmlSpace;
		}

		// Token: 0x04000570 RID: 1392
		private string baseURI = string.Empty;

		// Token: 0x04000571 RID: 1393
		private string docTypeName = string.Empty;

		// Token: 0x04000572 RID: 1394
		private Encoding encoding;

		// Token: 0x04000573 RID: 1395
		private string internalSubset = string.Empty;

		// Token: 0x04000574 RID: 1396
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04000575 RID: 1397
		private XmlNameTable nameTable;

		// Token: 0x04000576 RID: 1398
		private string publicID = string.Empty;

		// Token: 0x04000577 RID: 1399
		private string systemID = string.Empty;

		// Token: 0x04000578 RID: 1400
		private string xmlLang = string.Empty;

		// Token: 0x04000579 RID: 1401
		private XmlSpace xmlSpace;

		// Token: 0x0400057A RID: 1402
		private ArrayList contextItems;

		// Token: 0x0400057B RID: 1403
		private int contextItemCount;

		// Token: 0x0400057C RID: 1404
		private DTDObjectModel dtd;

		// Token: 0x02000113 RID: 275
		private class ContextItem
		{
			// Token: 0x0400057D RID: 1405
			public string BaseURI;

			// Token: 0x0400057E RID: 1406
			public string XmlLang;

			// Token: 0x0400057F RID: 1407
			public XmlSpace XmlSpace;
		}
	}
}
