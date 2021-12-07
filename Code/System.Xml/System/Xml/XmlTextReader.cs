using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Mono.Xml;
using Mono.Xml2;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached, forward-only access to XML data.</summary>
	// Token: 0x02000125 RID: 293
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class XmlTextReader : XmlReader, IHasXmlParserContext, IXmlLineInfo, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the XmlTextReader.</summary>
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00041694 File Offset: 0x0003F894
		protected XmlTextReader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		// Token: 0x06000CC8 RID: 3272 RVA: 0x0004169C File Offset: 0x0003F89C
		public XmlTextReader(Stream input) : this(new XmlStreamReader(input))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file.</summary>
		/// <param name="url">The URL for the file containing the XML data. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000CC9 RID: 3273 RVA: 0x000416AC File Offset: 0x0003F8AC
		public XmlTextReader(string url) : this(url, new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		// Token: 0x06000CCA RID: 3274 RVA: 0x000416BC File Offset: 0x0003F8BC
		public XmlTextReader(TextReader input) : this(input, new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nt">The XmlNameTable to use. </param>
		// Token: 0x06000CCB RID: 3275 RVA: 0x000416CC File Offset: 0x0003F8CC
		protected XmlTextReader(XmlNameTable nt) : this(string.Empty, XmlNodeType.Element, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000CCC RID: 3276 RVA: 0x000416DC File Offset: 0x0003F8DC
		public XmlTextReader(Stream input, XmlNameTable nt) : this(new XmlStreamReader(input), nt)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL and stream.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		// Token: 0x06000CCD RID: 3277 RVA: 0x000416EC File Offset: 0x0003F8EC
		public XmlTextReader(string url, Stream input) : this(url, new XmlStreamReader(input))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL and <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		// Token: 0x06000CCE RID: 3278 RVA: 0x000416FC File Offset: 0x0003F8FC
		public XmlTextReader(string url, TextReader input) : this(url, input, new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL for the file containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is null.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000CCF RID: 3279 RVA: 0x0004170C File Offset: 0x0003F90C
		public XmlTextReader(string url, XmlNameTable nt)
		{
			this.source = new XmlTextReader(url, nt);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000CD0 RID: 3280 RVA: 0x00041724 File Offset: 0x0003F924
		public XmlTextReader(TextReader input, XmlNameTable nt) : this(string.Empty, input, nt)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream, <see cref="T:System.Xml.XmlNodeType" />, and <see cref="T:System.Xml.XmlParserContext" />.</summary>
		/// <param name="xmlFragment">The stream containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment can contain. (See table below.) </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the <paramref name="xmlFragment" /> is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang, and the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not an Element, Attribute, or Document XmlNodeType. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xmlFragment" /> is null. </exception>
		// Token: 0x06000CD1 RID: 3281 RVA: 0x00041734 File Offset: 0x0003F934
		public XmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.source = new XmlTextReader(xmlFragment, fragType, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is null, BaseURI is set to String.Empty. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0004174C File Offset: 0x0003F94C
		public XmlTextReader(string url, Stream input, XmlNameTable nt) : this(url, new XmlStreamReader(input), nt)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is null, BaseURI is set to String.Empty. </param>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000CD3 RID: 3283 RVA: 0x0004175C File Offset: 0x0003F95C
		public XmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			this.source = new XmlTextReader(url, input, nt);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified string, <see cref="T:System.Xml.XmlNodeType" />, and <see cref="T:System.Xml.XmlParserContext" />.</summary>
		/// <param name="xmlFragment">The string containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment string can contain. (See table below.) </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the <paramref name="xmlFragment" /> is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang, and the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not an Element, Attribute, or DocumentXmlNodeType. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xmlFragment" /> is null. </exception>
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00041774 File Offset: 0x0003F974
		public XmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.source = new XmlTextReader(xmlFragment, fragType, context);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0004178C File Offset: 0x0003F98C
		internal XmlTextReader(string baseURI, TextReader xmlFragment, XmlNodeType fragType)
		{
			this.source = new XmlTextReader(baseURI, xmlFragment, fragType);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000417A4 File Offset: 0x0003F9A4
		internal XmlTextReader(string baseURI, TextReader xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.source = new XmlTextReader(baseURI, xmlFragment, fragType, context);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000417BC File Offset: 0x0003F9BC
		internal XmlTextReader(bool dummy, XmlResolver resolver, string url, XmlNodeType fragType, XmlParserContext context)
		{
			this.source = new XmlTextReader(dummy, resolver, url, fragType, context);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000417E4 File Offset: 0x0003F9E4
		private XmlTextReader(XmlTextReader entityContainer, bool insideAttribute)
		{
			this.source = entityContainer;
			this.entityInsideAttribute = insideAttribute;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x000417FC File Offset: 0x0003F9FC
		XmlParserContext IHasXmlParserContext.ParserContext
		{
			get
			{
				return this.ParserContext;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains the current in-scope namespaces.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x06000CDA RID: 3290 RVA: 0x00041804 File Offset: 0x0003FA04
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <returns>The prefix that is mapped to the namespace URI; null if the namespace URI is not mapped to a prefix.</returns>
		/// <param name="namespaceName">The namespace URI whose prefix you wish to find.</param>
		// Token: 0x06000CDB RID: 3291 RVA: 0x00041810 File Offset: 0x0003FA10
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			return ((IXmlNamespaceResolver)this.Current).LookupPrefix(ns);
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00041824 File Offset: 0x0003FA24
		private XmlReader Current
		{
			get
			{
				return (this.entity == null || this.entity.ReadState == ReadState.Initial) ? this.source : this.entity;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00041860 File Offset: 0x0003FA60
		public override int AttributeCount
		{
			get
			{
				return this.Current.AttributeCount;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00041870 File Offset: 0x0003FA70
		public override string BaseURI
		{
			get
			{
				return this.Current.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlTextReader" /> class always returns true.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00041880 File Offset: 0x0003FA80
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method; otherwise false. The <see cref="T:System.Xml.XmlTextReader" /> class always returns false.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00041884 File Offset: 0x0003FA84
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. The XmlTextReader class always returns true.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00041888 File Offset: 0x0003FA88
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0004188C File Offset: 0x0003FA8C
		public override int Depth
		{
			get
			{
				if (this.entity != null && this.entity.ReadState == ReadState.Interactive)
				{
					return this.source.Depth + this.entity.Depth + 1;
				}
				return this.source.Depth;
			}
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x000418DC File Offset: 0x0003FADC
		public override bool EOF
		{
			get
			{
				return this.source.EOF;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlTextReader.Value" /> other than String.Empty.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x000418EC File Offset: 0x0003FAEC
		public override bool HasValue
		{
			get
			{
				return this.Current.HasValue;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the DTD or schema.</summary>
		/// <returns>This property always returns false. (<see cref="T:System.Xml.XmlTextReader" /> does not expand default attributes.) </returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x000418FC File Offset: 0x0003FAFC
		public override bool IsDefault
		{
			get
			{
				return this.Current.IsDefault;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlTextReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0004190C File Offset: 0x0003FB0C
		public override bool IsEmptyElement
		{
			get
			{
				return this.Current.IsEmptyElement;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0004191C File Offset: 0x0003FB1C
		public override string LocalName
		{
			get
			{
				return this.Current.LocalName;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0004192C File Offset: 0x0003FB2C
		public override string Name
		{
			get
			{
				return this.Current.Name;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0004193C File Offset: 0x0003FB3C
		public override string NamespaceURI
		{
			get
			{
				return this.Current.NamespaceURI;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0004194C File Offset: 0x0003FB4C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.Current.NameTable;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0004195C File Offset: 0x0003FB5C
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.entity != null)
				{
					return (this.entity.ReadState != ReadState.Initial) ? ((!this.entity.EOF) ? this.entity.NodeType : XmlNodeType.EndEntity) : this.source.NodeType;
				}
				return this.source.NodeType;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000419C4 File Offset: 0x0003FBC4
		internal XmlParserContext ParserContext
		{
			get
			{
				return ((IHasXmlParserContext)this.Current).ParserContext;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000419D8 File Offset: 0x0003FBD8
		public override string Prefix
		{
			get
			{
				return this.Current.Prefix;
			}
		}

		/// <summary>Gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x000419E8 File Offset: 0x0003FBE8
		public override char QuoteChar
		{
			get
			{
				return this.Current.QuoteChar;
			}
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x000419F8 File Offset: 0x0003FBF8
		public override ReadState ReadState
		{
			get
			{
				return (this.entity == null) ? this.source.ReadState : ReadState.Interactive;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReaderSettings" /> object used to create this <see cref="T:System.Xml.XmlTextReader" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlReaderSettings" /> object used to create this <see cref="T:System.Xml.XmlTextReader" /> instance; null if the reader was not created using the <see cref="Overload:System.Xml.XmlReader.Create" /> method. </returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00041A18 File Offset: 0x0003FC18
		public override XmlReaderSettings Settings
		{
			get
			{
				return base.Settings;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space within an xml:space= 'preserve' scope. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00041A20 File Offset: 0x0003FC20
		public override string Value
		{
			get
			{
				return this.Current.Value;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00041A30 File Offset: 0x0003FC30
		public override string XmlLang
		{
			get
			{
				return this.Current.XmlLang;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00041A40 File Offset: 0x0003FC40
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.Current.XmlSpace;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00041A50 File Offset: 0x0003FC50
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00041A80 File Offset: 0x0003FC80
		internal bool CharacterChecking
		{
			get
			{
				if (this.entity != null)
				{
					return this.entity.CharacterChecking;
				}
				return this.source.CharacterChecking;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.CharacterChecking = value;
				}
				this.source.CharacterChecking = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00041AA8 File Offset: 0x0003FCA8
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00041AD8 File Offset: 0x0003FCD8
		internal bool CloseInput
		{
			get
			{
				if (this.entity != null)
				{
					return this.entity.CloseInput;
				}
				return this.source.CloseInput;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.CloseInput = value;
				}
				this.source.CloseInput = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00041B00 File Offset: 0x0003FD00
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00041B10 File Offset: 0x0003FD10
		internal ConformanceLevel Conformance
		{
			get
			{
				return this.source.Conformance;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.Conformance = value;
				}
				this.source.Conformance = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00041B38 File Offset: 0x0003FD38
		internal XmlResolver Resolver
		{
			get
			{
				return this.source.Resolver;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00041B48 File Offset: 0x0003FD48
		private void CopyProperties(XmlTextReader other)
		{
			this.CharacterChecking = other.CharacterChecking;
			this.CloseInput = other.CloseInput;
			if (other.Settings != null)
			{
				this.Conformance = other.Settings.ConformanceLevel;
			}
			this.XmlResolver = other.Resolver;
		}

		/// <summary>Gets the encoding of the document.</summary>
		/// <returns>The encoding value. If no encoding attribute exists, and there is no byte-order mark, this defaults to UTF-8.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00041B98 File Offset: 0x0003FD98
		public Encoding Encoding
		{
			get
			{
				if (this.entity != null)
				{
					return this.entity.Encoding;
				}
				return this.source.Encoding;
			}
		}

		/// <summary>Gets or sets a value that specifies how the reader handles entities.</summary>
		/// <returns>One of the <see cref="T:System.Xml.EntityHandling" /> values. If no EntityHandling is specified, it defaults to EntityHandling.ExpandCharEntities.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00041BC8 File Offset: 0x0003FDC8
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00041BD8 File Offset: 0x0003FDD8
		public EntityHandling EntityHandling
		{
			get
			{
				return this.source.EntityHandling;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.EntityHandling = value;
				}
				this.source.EntityHandling = value;
			}
		}

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00041C00 File Offset: 0x0003FE00
		public int LineNumber
		{
			get
			{
				if (this.entity != null)
				{
					return this.entity.LineNumber;
				}
				return this.source.LineNumber;
			}
		}

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00041C30 File Offset: 0x0003FE30
		public int LinePosition
		{
			get
			{
				if (this.entity != null)
				{
					return this.entity.LinePosition;
				}
				return this.source.LinePosition;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to do namespace support; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property after a read operation has occurred (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is not ReadState.Initial). </exception>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00041C60 File Offset: 0x0003FE60
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00041C70 File Offset: 0x0003FE70
		public bool Namespaces
		{
			get
			{
				return this.source.Namespaces;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.Namespaces = value;
				}
				this.source.Namespaces = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize white space and attribute values.</summary>
		/// <returns>true to normalize; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is ReadState.Closed). </exception>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00041C98 File Offset: 0x0003FE98
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00041CA8 File Offset: 0x0003FEA8
		public bool Normalization
		{
			get
			{
				return this.source.Normalization;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.Normalization = value;
				}
				this.source.Normalization = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to allow DTD processing.</summary>
		/// <returns>true to disallow DTD processing; otherwise false. The default is false.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00041CD0 File Offset: 0x0003FED0
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00041CE0 File Offset: 0x0003FEE0
		public bool ProhibitDtd
		{
			get
			{
				return this.source.ProhibitDtd;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.ProhibitDtd = value;
				}
				this.source.ProhibitDtd = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how white space is handled.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WhitespaceHandling" /> values. The default is WhitespaceHandling.All (returns Whitespace and SignificantWhitespace nodes).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Invalid value specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is ReadState.Closed). </exception>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00041D08 File Offset: 0x0003FF08
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00041D18 File Offset: 0x0003FF18
		public WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.source.WhitespaceHandling;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.WhitespaceHandling = value;
				}
				this.source.WhitespaceHandling = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used for resolving DTD references.</summary>
		/// <returns>The XmlResolver to use. If set to null, external resources are not resolved.In version 1.1 of the .NET Framework, the caller must be fully trusted in order to specify an XmlResolver.</returns>
		// Token: 0x170003CA RID: 970
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00041D40 File Offset: 0x0003FF40
		public XmlResolver XmlResolver
		{
			set
			{
				if (this.entity != null)
				{
					this.entity.XmlResolver = value;
				}
				this.source.XmlResolver = value;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00041D68 File Offset: 0x0003FF68
		internal void AdjustLineInfoOffset(int lineNumberOffset, int linePositionOffset)
		{
			if (this.entity != null)
			{
				this.entity.AdjustLineInfoOffset(lineNumberOffset, linePositionOffset);
			}
			this.source.AdjustLineInfoOffset(lineNumberOffset, linePositionOffset);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00041D90 File Offset: 0x0003FF90
		internal void SetNameTable(XmlNameTable nameTable)
		{
			if (this.entity != null)
			{
				this.entity.SetNameTable(nameTable);
			}
			this.source.SetNameTable(nameTable);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00041DB8 File Offset: 0x0003FFB8
		internal void SkipTextDeclaration()
		{
			if (this.entity != null)
			{
				this.entity.SkipTextDeclaration();
			}
			else
			{
				this.source.SkipTextDeclaration();
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000D0D RID: 3341 RVA: 0x00041DEC File Offset: 0x0003FFEC
		public override void Close()
		{
			if (this.entity != null)
			{
				this.entity.Close();
			}
			this.source.Close();
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlTextReader.AttributeCount" />. </exception>
		// Token: 0x06000D0E RID: 3342 RVA: 0x00041E10 File Offset: 0x00040010
		public override string GetAttribute(int i)
		{
			return this.Current.GetAttribute(i);
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000D0F RID: 3343 RVA: 0x00041E20 File Offset: 0x00040020
		public override string GetAttribute(string name)
		{
			return this.Current.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned. This method does not move the reader.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000D10 RID: 3344 RVA: 0x00041E30 File Offset: 0x00040030
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.Current.GetAttribute(localName, namespaceURI);
		}

		/// <summary>Gets a collection that contains all namespaces currently in-scope.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> object that contains all the current in-scope namespaces. If the reader is not positioned on an element, an empty dictionary (no namespaces) is returned.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x06000D11 RID: 3345 RVA: 0x00041E40 File Offset: 0x00040040
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IXmlNamespaceResolver)this.Current).GetNamespacesInScope(scope);
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Xml.XmlTextReader.Namespaces" /> property is set to true and the <paramref name="prefix" /> value is null. </exception>
		// Token: 0x06000D12 RID: 3346 RVA: 0x00041E54 File Offset: 0x00040054
		public override string LookupNamespace(string prefix)
		{
			return this.Current.LookupNamespace(prefix);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00041E64 File Offset: 0x00040064
		public override void MoveToAttribute(int i)
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.CloseEntity();
			}
			this.Current.MoveToAttribute(i);
			this.insideAttribute = true;
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000D14 RID: 3348 RVA: 0x00041E98 File Offset: 0x00040098
		public override bool MoveToAttribute(string name)
		{
			if (this.entity != null && !this.entityInsideAttribute)
			{
				return this.entity.MoveToAttribute(name);
			}
			if (!this.source.MoveToAttribute(name))
			{
				return false;
			}
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.CloseEntity();
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000D15 RID: 3349 RVA: 0x00041F00 File Offset: 0x00040100
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (this.entity != null && !this.entityInsideAttribute)
			{
				return this.entity.MoveToAttribute(localName, namespaceName);
			}
			if (!this.source.MoveToAttribute(localName, namespaceName))
			{
				return false;
			}
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.CloseEntity();
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000D16 RID: 3350 RVA: 0x00041F6C File Offset: 0x0004016C
		public override bool MoveToElement()
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.CloseEntity();
			}
			if (!this.Current.MoveToElement())
			{
				return false;
			}
			this.insideAttribute = false;
			return true;
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00041FB0 File Offset: 0x000401B0
		public override bool MoveToFirstAttribute()
		{
			if (this.entity != null && !this.entityInsideAttribute)
			{
				return this.entity.MoveToFirstAttribute();
			}
			if (!this.source.MoveToFirstAttribute())
			{
				return false;
			}
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.CloseEntity();
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00042018 File Offset: 0x00040218
		public override bool MoveToNextAttribute()
		{
			if (this.entity != null && !this.entityInsideAttribute)
			{
				return this.entity.MoveToNextAttribute();
			}
			if (!this.source.MoveToNextAttribute())
			{
				return false;
			}
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.CloseEntity();
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00042080 File Offset: 0x00040280
		public override bool Read()
		{
			this.insideAttribute = false;
			if (this.entity != null && (this.entityInsideAttribute || this.entity.EOF))
			{
				this.CloseEntity();
			}
			if (this.entity != null)
			{
				if (this.entity.Read())
				{
					return true;
				}
				if (this.EntityHandling == EntityHandling.ExpandEntities)
				{
					this.CloseEntity();
					return this.Read();
				}
				return true;
			}
			else
			{
				if (!this.source.Read())
				{
					return false;
				}
				if (this.EntityHandling == EntityHandling.ExpandEntities && this.source.NodeType == XmlNodeType.EntityReference)
				{
					this.ResolveEntity();
					return this.Read();
				}
				return true;
			}
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00042138 File Offset: 0x00040338
		public override bool ReadAttributeValue()
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				if (!this.entity.EOF)
				{
					this.entity.Read();
					return true;
				}
				this.CloseEntity();
			}
			return this.Current.ReadAttributeValue();
		}

		/// <summary>Reads the contents of an element or a text node as a string.</summary>
		/// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was attempted. </exception>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00042190 File Offset: 0x00040390
		public override string ReadString()
		{
			return base.ReadString();
		}

		/// <summary>Resets the state of the reader to ReadState.Initial.</summary>
		/// <exception cref="T:System.InvalidOperationException">Calling ResetState if the reader was constructed using an <see cref="T:System.Xml.XmlParserContext" />. </exception>
		/// <exception cref="T:System.Xml.XmlException">Documents in a single stream do not share the same encoding.</exception>
		// Token: 0x06000D1C RID: 3356 RVA: 0x00042198 File Offset: 0x00040398
		public void ResetState()
		{
			if (this.entity != null)
			{
				this.CloseEntity();
			}
			this.source.ResetState();
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		// Token: 0x06000D1D RID: 3357 RVA: 0x000421B8 File Offset: 0x000403B8
		public override void ResolveEntity()
		{
			if (this.entity != null)
			{
				this.entity.ResolveEntity();
			}
			else
			{
				if (this.source.NodeType != XmlNodeType.EntityReference)
				{
					throw new InvalidOperationException("The current node is not an Entity Reference");
				}
				XmlTextReader xmlTextReader = null;
				if (this.ParserContext.Dtd != null)
				{
					xmlTextReader = this.ParserContext.Dtd.GenerateEntityContentReader(this.source.Name, this.ParserContext);
				}
				if (xmlTextReader == null)
				{
					throw new XmlException(this, this.BaseURI, string.Format("Reference to undeclared entity '{0}'.", this.source.Name));
				}
				if (this.entityNameStack == null)
				{
					this.entityNameStack = new Stack<string>();
				}
				else if (this.entityNameStack.Contains(this.Name))
				{
					throw new XmlException(string.Format("General entity '{0}' has an invalid recursive reference to itself.", this.Name));
				}
				this.entityNameStack.Push(this.Name);
				this.entity = new XmlTextReader(xmlTextReader, this.insideAttribute);
				this.entity.entityNameStack = this.entityNameStack;
				this.entity.CopyProperties(this);
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000422E0 File Offset: 0x000404E0
		private void CloseEntity()
		{
			this.entity.Close();
			this.entity = null;
			this.entityNameStack.Pop();
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x06000D1F RID: 3359 RVA: 0x00042300 File Offset: 0x00040500
		public override void Skip()
		{
			base.Skip();
		}

		/// <summary>Gets the remainder of the buffered XML.</summary>
		/// <returns>A <see cref="T:System.IO.TextReader" /> containing the remainder of the buffered XML.</returns>
		// Token: 0x06000D20 RID: 3360 RVA: 0x00042308 File Offset: 0x00040508
		[MonoTODO]
		public TextReader GetRemainder()
		{
			if (this.entity != null)
			{
				this.entity.Close();
				this.entity = null;
				this.entityNameStack.Pop();
			}
			return this.source.GetRemainder();
		}

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if the class can return line information; otherwise, false.</returns>
		// Token: 0x06000D21 RID: 3361 RVA: 0x0004234C File Offset: 0x0004054C
		public bool HasLineInfo()
		{
			return true;
		}

		/// <summary>Decodes Base64 and returns the decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="array">The array of characters that serves as the buffer to which the text contents are written. </param>
		/// <param name="offset">The zero-based index into the array specifying where the method can begin to write to the buffer. </param>
		/// <param name="len">The number of bytes to write into the buffer. </param>
		/// <exception cref="T:System.Xml.XmlException">The Base64 sequence is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> &lt; 0, or <paramref name="len" /> &lt; 0, or <paramref name="len" /> &gt; <paramref name="array" />.Length- <paramref name="offset" />. </exception>
		// Token: 0x06000D22 RID: 3362 RVA: 0x00042350 File Offset: 0x00040550
		[MonoTODO]
		public int ReadBase64(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadBase64(buffer, offset, length);
			}
			return this.source.ReadBase64(buffer, offset, length);
		}

		/// <summary>Decodes BinHex and returns the decoded binary bytes.</summary>
		/// <returns>The number of bytes written to your buffer.</returns>
		/// <param name="array">The byte array that serves as the buffer to which the decoded binary bytes are written. </param>
		/// <param name="offset">The zero-based index into the array specifying where the method can begin to write to the buffer. </param>
		/// <param name="len">The number of bytes to write into the buffer. </param>
		/// <exception cref="T:System.Xml.XmlException">The BinHex sequence is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> &lt; 0, or <paramref name="len" /> &lt; 0, or <paramref name="len" /> &gt; <paramref name="array" />.Length- <paramref name="offset" />. </exception>
		// Token: 0x06000D23 RID: 3363 RVA: 0x00042388 File Offset: 0x00040588
		[MonoTODO]
		public int ReadBinHex(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadBinHex(buffer, offset, length);
			}
			return this.source.ReadBinHex(buffer, offset, length);
		}

		/// <summary>Reads the text contents of an element into a character buffer. This method is designed to read large streams of embedded text by calling it successively.</summary>
		/// <returns>The number of characters read. This can be 0 if the reader is not positioned on an element or if there is no more text content to return in the current context.</returns>
		/// <param name="buffer">The array of characters that serves as the buffer to which the text contents are written. </param>
		/// <param name="index">The position within <paramref name="buffer" /> where the method can begin writing text contents. </param>
		/// <param name="count">The number of characters to write into <paramref name="buffer" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the space specified in the <paramref name="buffer" /> (buffer size - <paramref name="index" />). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" />&lt; 0 or <paramref name="count" />&lt; 0. </exception>
		// Token: 0x06000D24 RID: 3364 RVA: 0x000423C0 File Offset: 0x000405C0
		[MonoTODO]
		public int ReadChars(char[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadChars(buffer, offset, length);
			}
			return this.source.ReadChars(buffer, offset, length);
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlTextReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" />  is not supported in the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000D25 RID: 3365 RVA: 0x000423F8 File Offset: 0x000405F8
		[MonoTODO]
		public override int ReadContentAsBase64(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadContentAsBase64(buffer, offset, length);
			}
			return this.source.ReadContentAsBase64(buffer, offset, length);
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlTextReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x00042430 File Offset: 0x00040630
		[MonoTODO]
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadContentAsBinHex(buffer, offset, length);
			}
			return this.source.ReadContentAsBinHex(buffer, offset, length);
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000D27 RID: 3367 RVA: 0x00042468 File Offset: 0x00040668
		[MonoTODO]
		public override int ReadElementContentAsBase64(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadElementContentAsBase64(buffer, offset, length);
			}
			return this.source.ReadElementContentAsBase64(buffer, offset, length);
		}

		/// <summary>Reads the element and decodes the BinHex content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000D28 RID: 3368 RVA: 0x000424A0 File Offset: 0x000406A0
		[MonoTODO]
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadElementContentAsBinHex(buffer, offset, length);
			}
			return this.source.ReadElementContentAsBinHex(buffer, offset, length);
		}

		// Token: 0x04000603 RID: 1539
		private XmlTextReader entity;

		// Token: 0x04000604 RID: 1540
		private XmlTextReader source;

		// Token: 0x04000605 RID: 1541
		private bool entityInsideAttribute;

		// Token: 0x04000606 RID: 1542
		private bool insideAttribute;

		// Token: 0x04000607 RID: 1543
		private Stack<string> entityNameStack;
	}
}
