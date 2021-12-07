using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;
using Mono.Xml;
using Mono.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents a reader that provides document type definition (DTD), XML-Data Reduced (XDR) schema, and XML Schema definition language (XSD) validation.</summary>
	// Token: 0x02000128 RID: 296
	[Obsolete("Use XmlReader created by XmlReader.Create() method using appropriate XmlReaderSettings instead.")]
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class XmlValidatingReader : XmlReader, IHasXmlParserContext, IXmlLineInfo, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the XmlValidatingReader class that validates the content returned from the given <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader to read from while validating. The current implementation supports only <see cref="T:System.Xml.XmlTextReader" />. </param>
		/// <exception cref="T:System.ArgumentException">The reader specified is not an XmlTextReader. </exception>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00042630 File Offset: 0x00040830
		public XmlValidatingReader(XmlReader reader)
		{
			this.sourceReader = reader;
			this.xmlTextReader = (reader as XmlTextReader);
			if (this.xmlTextReader == null)
			{
				this.resolver = new XmlUrlResolver();
			}
			this.entityHandling = EntityHandling.ExpandEntities;
			this.validationType = ValidationType.Auto;
			this.storedCharacters = new StringBuilder();
		}

		/// <summary>Initializes a new instance of the XmlValidatingReader class with the specified values.</summary>
		/// <param name="xmlFragment">The stream containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This determines what the fragment can contain (see table below). </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the XML fragment is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, current xml:lang, and xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not one of the node types listed in the table below. </exception>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00042688 File Offset: 0x00040888
		public XmlValidatingReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : this(new XmlTextReader(xmlFragment, fragType, context))
		{
		}

		/// <summary>Initializes a new instance of the XmlValidatingReader class with the specified values.</summary>
		/// <param name="xmlFragment">The string containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment string can contain (see table below). </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the XML fragment is to be parsed. This includes the <see cref="T:System.Xml.NameTable" /> to use, encoding, namespace scope, current xml:lang, and xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not one of the node types listed in the table below. </exception>
		// Token: 0x06000D30 RID: 3376 RVA: 0x00042698 File Offset: 0x00040898
		public XmlValidatingReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : this(new XmlTextReader(xmlFragment, fragType, context))
		{
		}

		/// <summary>Sets an event handler for receiving information about document type definition (DTD), XML-Data Reduced (XDR) schema, and XML Schema definition language (XSD) schema validation errors.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000D31 RID: 3377 RVA: 0x000426A8 File Offset: 0x000408A8
		// (remove) Token: 0x06000D32 RID: 3378 RVA: 0x000426C4 File Offset: 0x000408C4
		public event ValidationEventHandler ValidationEventHandler;

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x000426E0 File Offset: 0x000408E0
		XmlParserContext IHasXmlParserContext.ParserContext
		{
			get
			{
				if (this.dtdReader != null)
				{
					return this.dtdReader.ParserContext;
				}
				IHasXmlParserContext hasXmlParserContext = this.sourceReader as IHasXmlParserContext;
				return (hasXmlParserContext == null) ? null : hasXmlParserContext.ParserContext;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <param name="scope"></param>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00042724 File Offset: 0x00040924
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IHasXmlParserContext)this).ParserContext.NamespaceManager.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <param name="namespaceName"></param>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00042738 File Offset: 0x00040938
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			IXmlNamespaceResolver xmlNamespaceResolver;
			if (this.validatingReader != null)
			{
				xmlNamespaceResolver = (this.sourceReader as IXmlNamespaceResolver);
			}
			else
			{
				xmlNamespaceResolver = (this.validatingReader as IXmlNamespaceResolver);
			}
			return (xmlNamespaceResolver == null) ? null : xmlNamespaceResolver.LookupNamespace(ns);
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node. This number includes default attributes.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00042784 File Offset: 0x00040984
		public override int AttributeCount
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.AttributeCount : 0;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000427A4 File Offset: 0x000409A4
		public override string BaseURI
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.BaseURI : this.sourceReader.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlValidatingReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlValidatingReader" /> class returns true.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000427D8 File Offset: 0x000409D8
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. XmlValidatingReader always returns true.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x000427DC File Offset: 0x000409DC
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x000427E0 File Offset: 0x000409E0
		public override int Depth
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.Depth : 0;
			}
		}

		/// <summary>Gets the encoding attribute for the document.</summary>
		/// <returns>The encoding value. If no encoding attribute exists, and there is not byte-order mark, this defaults to UTF-8.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00042800 File Offset: 0x00040A00
		public Encoding Encoding
		{
			get
			{
				if (this.xmlTextReader != null)
				{
					return this.xmlTextReader.Encoding;
				}
				throw new NotSupportedException("Encoding is supported only for XmlTextReader.");
			}
		}

		/// <summary>Gets or sets a value that specifies how the reader handles entities.</summary>
		/// <returns>One of the <see cref="T:System.Xml.EntityHandling" /> values. If no EntityHandling is specified, it defaults to EntityHandling.ExpandEntities.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Invalid value was specified. </exception>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00042824 File Offset: 0x00040A24
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x0004282C File Offset: 0x00040A2C
		public EntityHandling EntityHandling
		{
			get
			{
				return this.entityHandling;
			}
			set
			{
				this.entityHandling = value;
				if (this.dtdReader != null)
				{
					this.dtdReader.EntityHandling = value;
				}
			}
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0004284C File Offset: 0x00040A4C
		public override bool EOF
		{
			get
			{
				return this.validatingReader != null && this.validatingReader.EOF;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlValidatingReader.Value" /> other than String.Empty.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0004286C File Offset: 0x00040A6C
		public override bool HasValue
		{
			get
			{
				return this.validatingReader != null && this.validatingReader.HasValue;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the document type definition (DTD) or schema.</summary>
		/// <returns>true if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; false if the attribute value was explicitly set.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0004288C File Offset: 0x00040A8C
		public override bool IsDefault
		{
			get
			{
				return this.validatingReader != null && this.validatingReader.IsDefault;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlValidatingReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000428AC File Offset: 0x00040AAC
		public override bool IsEmptyElement
		{
			get
			{
				return this.validatingReader != null && this.validatingReader.IsEmptyElement;
			}
		}

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number. The starting value for this property is 1.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000428CC File Offset: 0x00040ACC
		public int LineNumber
		{
			get
			{
				if (this.IsDefault)
				{
					return 0;
				}
				IXmlLineInfo xmlLineInfo = this.validatingReader as IXmlLineInfo;
				return (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber;
			}
		}

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position. The starting value for this property is 1.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00042904 File Offset: 0x00040B04
		public int LinePosition
		{
			get
			{
				if (this.IsDefault)
				{
					return 0;
				}
				IXmlLineInfo xmlLineInfo = this.validatingReader as IXmlLineInfo;
				return (xmlLineInfo == null) ? 0 : xmlLineInfo.LinePosition;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0004293C File Offset: 0x00040B3C
		public override string LocalName
		{
			get
			{
				if (this.validatingReader == null)
				{
					return string.Empty;
				}
				if (this.Namespaces)
				{
					return this.validatingReader.LocalName;
				}
				return this.validatingReader.Name;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlValidatingReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0004297C File Offset: 0x00040B7C
		public override string Name
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.Name : string.Empty;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to do namespace support; otherwise, false. The default is true.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x000429AC File Offset: 0x00040BAC
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x000429C8 File Offset: 0x00040BC8
		public bool Namespaces
		{
			get
			{
				return this.xmlTextReader == null || this.xmlTextReader.Namespaces;
			}
			set
			{
				if (this.ReadState != ReadState.Initial)
				{
					throw new InvalidOperationException("Namespaces have to be set before reading.");
				}
				if (this.xmlTextReader != null)
				{
					this.xmlTextReader.Namespaces = value;
					return;
				}
				throw new NotSupportedException("Property 'Namespaces' is supported only for XmlTextReader.");
			}
		}

		/// <summary>Gets the namespace Uniform Resource Identifier (URI) (as defined in the World Wide Web Consortium (W3C) Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00042A08 File Offset: 0x00040C08
		public override string NamespaceURI
		{
			get
			{
				if (this.validatingReader == null)
				{
					return string.Empty;
				}
				if (this.Namespaces)
				{
					return this.validatingReader.NamespaceURI;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>XmlNameTable that enables you to get the atomized version of a string within the node.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00042A38 File Offset: 0x00040C38
		public override XmlNameTable NameTable
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.NameTable : this.sourceReader.NameTable;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00042A6C File Offset: 0x00040C6C
		public override XmlNodeType NodeType
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.NodeType : XmlNodeType.None;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00042A8C File Offset: 0x00040C8C
		public override string Prefix
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.Prefix : string.Empty;
			}
		}

		/// <summary>Gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00042ABC File Offset: 0x00040CBC
		public override char QuoteChar
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.QuoteChar : this.sourceReader.QuoteChar;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReader" /> used to construct this XmlValidatingReader.</summary>
		/// <returns>The XmlReader specified in the constructor.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00042AF0 File Offset: 0x00040CF0
		public XmlReader Reader
		{
			get
			{
				return this.sourceReader;
			}
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00042AF8 File Offset: 0x00040CF8
		public override ReadState ReadState
		{
			get
			{
				if (this.validatingReader == null)
				{
					return ReadState.Initial;
				}
				return this.validatingReader.ReadState;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00042B14 File Offset: 0x00040D14
		internal XmlResolver Resolver
		{
			get
			{
				if (this.xmlTextReader != null)
				{
					return this.xmlTextReader.Resolver;
				}
				if (this.resolverSpecified)
				{
					return this.resolver;
				}
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Xml.Schema.XmlSchemaCollection" /> to use for validation.</summary>
		/// <returns>The XmlSchemaCollection to use for validation.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00042B4C File Offset: 0x00040D4C
		public XmlSchemaCollection Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaCollection(this.NameTable);
				}
				return this.schemas;
			}
		}

		/// <summary>Gets a schema type object.</summary>
		/// <returns>
		///   <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" />, or <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> depending whether the node value is a built in XML Schema definition language (XSD) type or a user defined simpleType or complexType; null if the current node has no schema type.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00042B7C File Offset: 0x00040D7C
		public object SchemaType
		{
			get
			{
				return this.schemaInfo.SchemaType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReaderSettings" /> object that was used to create this <see cref="T:System.Xml.XmlValidatingReader" /> instance.</summary>
		/// <returns>null because <see cref="T:System.Xml.XmlValidatingReader" /> objects are not instantiated using the <see cref="T:System.Xml.XmlReaderSettings" /> class and the <see cref="Overload:System.Xml.XmlReader.Create" /> method.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00042B8C File Offset: 0x00040D8C
		[MonoTODO]
		public override XmlReaderSettings Settings
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.Settings : this.sourceReader.Settings;
			}
		}

		/// <summary>Gets or sets a value indicating the type of validation to perform.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ValidationType" /> values. If this property is not set, it defaults to ValidationType.Auto.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting the property after a Read has been called. </exception>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00042BC0 File Offset: 0x00040DC0
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00042BC8 File Offset: 0x00040DC8
		[MonoTODO]
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				if (this.ReadState != ReadState.Initial)
				{
					throw new InvalidOperationException("ValidationType cannot be set after the first call to Read method.");
				}
				switch (this.validationType)
				{
				case ValidationType.None:
				case ValidationType.Auto:
				case ValidationType.DTD:
				case ValidationType.Schema:
					this.validationType = value;
					break;
				case ValidationType.XDR:
					throw new NotSupportedException();
				}
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlValidatingReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00042C24 File Offset: 0x00040E24
		public override string Value
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.Value : string.Empty;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00042C54 File Offset: 0x00040E54
		public override string XmlLang
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.XmlLang : string.Empty;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used for resolving external document type definition (DTD) and schema location references. The XmlResolver is also used to handle any import or include elements found in XML Schema definition language (XSD) schemas.</summary>
		/// <returns>The XmlResolver to use. If set to null, external resources are not resolved.In version 1.1 of the .NET Framework, the caller must be fully trusted to specify an XmlResolver.</returns>
		// Token: 0x170003EB RID: 1003
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00042C84 File Offset: 0x00040E84
		public XmlResolver XmlResolver
		{
			set
			{
				this.resolverSpecified = true;
				this.resolver = value;
				if (this.xmlTextReader != null)
				{
					this.xmlTextReader.XmlResolver = value;
				}
				XsdValidatingReader xsdValidatingReader = this.validatingReader as XsdValidatingReader;
				if (xsdValidatingReader != null)
				{
					xsdValidatingReader.XmlResolver = value;
				}
				DTDValidatingReader dtdvalidatingReader = this.validatingReader as DTDValidatingReader;
				if (dtdvalidatingReader != null)
				{
					dtdvalidatingReader.XmlResolver = value;
				}
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00042CE8 File Offset: 0x00040EE8
		public override XmlSpace XmlSpace
		{
			get
			{
				return (this.validatingReader != null) ? this.validatingReader.XmlSpace : XmlSpace.None;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000D59 RID: 3417 RVA: 0x00042D08 File Offset: 0x00040F08
		public override void Close()
		{
			if (this.validatingReader == null)
			{
				this.sourceReader.Close();
			}
			else
			{
				this.validatingReader.Close();
			}
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlValidatingReader.AttributeCount" />. </exception>
		// Token: 0x06000D5A RID: 3418 RVA: 0x00042D3C File Offset: 0x00040F3C
		public override string GetAttribute(int i)
		{
			if (this.validatingReader == null)
			{
				throw new IndexOutOfRangeException("Reader is not started.");
			}
			return this.validatingReader[i];
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000D5B RID: 3419 RVA: 0x00042D6C File Offset: 0x00040F6C
		public override string GetAttribute(string name)
		{
			return (this.validatingReader != null) ? this.validatingReader[name] : null;
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned. This method does not move the reader.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000D5C RID: 3420 RVA: 0x00042D8C File Offset: 0x00040F8C
		public override string GetAttribute(string localName, string namespaceName)
		{
			return (this.validatingReader != null) ? this.validatingReader[localName, namespaceName] : null;
		}

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if the class can return line information; otherwise, false.</returns>
		// Token: 0x06000D5D RID: 3421 RVA: 0x00042DAC File Offset: 0x00040FAC
		public bool HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.validatingReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace Uniform Resource Identifier (URI) you want to resolve. To match the default namespace, pass an empty string. </param>
		// Token: 0x06000D5E RID: 3422 RVA: 0x00042DD8 File Offset: 0x00040FD8
		public override string LookupNamespace(string prefix)
		{
			if (this.validatingReader != null)
			{
				return this.validatingReader.LookupNamespace(prefix);
			}
			return this.sourceReader.LookupNamespace(prefix);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000D5F RID: 3423 RVA: 0x00042E0C File Offset: 0x0004100C
		public override void MoveToAttribute(int i)
		{
			if (this.validatingReader == null)
			{
				throw new IndexOutOfRangeException("Reader is not started.");
			}
			this.validatingReader.MoveToAttribute(i);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the position of the reader does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000D60 RID: 3424 RVA: 0x00042E3C File Offset: 0x0004103C
		public override bool MoveToAttribute(string name)
		{
			return this.validatingReader != null && this.validatingReader.MoveToAttribute(name);
		}

		/// <summary>Moves to the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the position of the reader does not change.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000D61 RID: 3425 RVA: 0x00042E58 File Offset: 0x00041058
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			return this.validatingReader != null && this.validatingReader.MoveToAttribute(localName, namespaceName);
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000D62 RID: 3426 RVA: 0x00042E74 File Offset: 0x00041074
		public override bool MoveToElement()
		{
			return this.validatingReader != null && this.validatingReader.MoveToElement();
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x06000D63 RID: 3427 RVA: 0x00042E90 File Offset: 0x00041090
		public override bool MoveToFirstAttribute()
		{
			return this.validatingReader != null && this.validatingReader.MoveToFirstAttribute();
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x06000D64 RID: 3428 RVA: 0x00042EAC File Offset: 0x000410AC
		public override bool MoveToNextAttribute()
		{
			return this.validatingReader != null && this.validatingReader.MoveToNextAttribute();
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		// Token: 0x06000D65 RID: 3429 RVA: 0x00042EC8 File Offset: 0x000410C8
		[MonoTODO]
		public override bool Read()
		{
			if (this.validatingReader == null)
			{
				switch (this.ValidationType)
				{
				case ValidationType.None:
				case ValidationType.Auto:
					break;
				case ValidationType.DTD:
					this.validatingReader = (this.dtdReader = new DTDValidatingReader(this.sourceReader, this));
					this.dtdReader.XmlResolver = this.Resolver;
					goto IL_F3;
				case ValidationType.XDR:
					throw new NotSupportedException();
				case ValidationType.Schema:
					break;
				default:
					goto IL_F3;
				}
				this.dtdReader = new DTDValidatingReader(this.sourceReader, this);
				XsdValidatingReader xsdValidatingReader = new XsdValidatingReader(this.dtdReader);
				XsdValidatingReader xsdValidatingReader2 = xsdValidatingReader;
				xsdValidatingReader2.ValidationEventHandler = (ValidationEventHandler)Delegate.Combine(xsdValidatingReader2.ValidationEventHandler, new ValidationEventHandler(this.OnValidationEvent));
				xsdValidatingReader.ValidationType = this.ValidationType;
				xsdValidatingReader.Schemas = this.Schemas.SchemaSet;
				xsdValidatingReader.XmlResolver = this.Resolver;
				this.validatingReader = xsdValidatingReader;
				this.dtdReader.XmlResolver = this.Resolver;
				IL_F3:
				this.schemaInfo = (this.validatingReader as IHasXmlSchemaInfo);
			}
			return this.validatingReader.Read();
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06000D66 RID: 3430 RVA: 0x00042FE4 File Offset: 0x000411E4
		public override bool ReadAttributeValue()
		{
			return this.validatingReader != null && this.validatingReader.ReadAttributeValue();
		}

		/// <summary>Reads the contents of an element or text node as a string.</summary>
		/// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note:The text node can be either an element or an attribute text node.</returns>
		// Token: 0x06000D67 RID: 3431 RVA: 0x00043000 File Offset: 0x00041200
		public override string ReadString()
		{
			return base.ReadString();
		}

		/// <summary>Gets the common language runtime type for the specified XML Schema definition language (XSD) type.</summary>
		/// <returns>The common language runtime type for the specified XML Schema type.</returns>
		// Token: 0x06000D68 RID: 3432 RVA: 0x00043008 File Offset: 0x00041208
		public object ReadTypedValue()
		{
			if (this.dtdReader == null)
			{
				return null;
			}
			XmlSchemaDatatype xmlSchemaDatatype = this.schemaInfo.SchemaType as XmlSchemaDatatype;
			if (xmlSchemaDatatype == null)
			{
				XmlSchemaType xmlSchemaType = this.schemaInfo.SchemaType as XmlSchemaType;
				if (xmlSchemaType != null)
				{
					xmlSchemaDatatype = xmlSchemaType.Datatype;
				}
			}
			if (xmlSchemaDatatype == null)
			{
				return null;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					return null;
				}
				return xmlSchemaDatatype.ParseValue(this.Value, this.NameTable, this.dtdReader.ParserContext.NamespaceManager);
			}
			else
			{
				if (this.IsEmptyElement)
				{
					return null;
				}
				this.storedCharacters.Length = 0;
				bool flag = true;
				for (;;)
				{
					this.Read();
					XmlNodeType nodeType2 = this.NodeType;
					switch (nodeType2)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						goto IL_C6;
					default:
						if (nodeType2 == XmlNodeType.Whitespace || nodeType2 == XmlNodeType.SignificantWhitespace)
						{
							goto IL_C6;
						}
						flag = false;
						break;
					case XmlNodeType.Comment:
						break;
					}
					IL_E9:
					if (!flag || this.EOF)
					{
						break;
					}
					continue;
					IL_C6:
					this.storedCharacters.Append(this.Value);
					goto IL_E9;
				}
				return xmlSchemaDatatype.ParseValue(this.storedCharacters.ToString(), this.NameTable, this.dtdReader.ParserContext.NamespaceManager);
			}
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node. </exception>
		// Token: 0x06000D69 RID: 3433 RVA: 0x0004315C File Offset: 0x0004135C
		public override void ResolveEntity()
		{
			this.validatingReader.ResolveEntity();
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0004316C File Offset: 0x0004136C
		internal void OnValidationEvent(object o, ValidationEventArgs e)
		{
			if (this.ValidationEventHandler != null)
			{
				this.ValidationEventHandler(o, e);
			}
			else if (this.ValidationType != ValidationType.None && e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlValidatingReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000D6B RID: 3435 RVA: 0x000431B4 File Offset: 0x000413B4
		[MonoTODO]
		public override int ReadContentAsBase64(byte[] buffer, int offset, int length)
		{
			if (this.validatingReader != null)
			{
				return this.validatingReader.ReadContentAsBase64(buffer, offset, length);
			}
			return this.sourceReader.ReadContentAsBase64(buffer, offset, length);
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlValidatingReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlValidatingReader" /> implementation does not support this method.</exception>
		// Token: 0x06000D6C RID: 3436 RVA: 0x000431EC File Offset: 0x000413EC
		[MonoTODO]
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int length)
		{
			if (this.validatingReader != null)
			{
				return this.validatingReader.ReadContentAsBinHex(buffer, offset, length);
			}
			return this.sourceReader.ReadContentAsBinHex(buffer, offset, length);
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlValidatingReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000D6D RID: 3437 RVA: 0x00043224 File Offset: 0x00041424
		[MonoTODO]
		public override int ReadElementContentAsBase64(byte[] buffer, int offset, int length)
		{
			if (this.validatingReader != null)
			{
				return this.validatingReader.ReadElementContentAsBase64(buffer, offset, length);
			}
			return this.sourceReader.ReadElementContentAsBase64(buffer, offset, length);
		}

		/// <summary>Reads the element and decodes the BinHex content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlValidatingReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000D6E RID: 3438 RVA: 0x0004325C File Offset: 0x0004145C
		[MonoTODO]
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int length)
		{
			if (this.validatingReader != null)
			{
				return this.validatingReader.ReadElementContentAsBinHex(buffer, offset, length);
			}
			return this.sourceReader.ReadElementContentAsBinHex(buffer, offset, length);
		}

		// Token: 0x04000617 RID: 1559
		private EntityHandling entityHandling;

		// Token: 0x04000618 RID: 1560
		private XmlReader sourceReader;

		// Token: 0x04000619 RID: 1561
		private XmlTextReader xmlTextReader;

		// Token: 0x0400061A RID: 1562
		private XmlReader validatingReader;

		// Token: 0x0400061B RID: 1563
		private XmlResolver resolver;

		// Token: 0x0400061C RID: 1564
		private bool resolverSpecified;

		// Token: 0x0400061D RID: 1565
		private ValidationType validationType;

		// Token: 0x0400061E RID: 1566
		private XmlSchemaCollection schemas;

		// Token: 0x0400061F RID: 1567
		private DTDValidatingReader dtdReader;

		// Token: 0x04000620 RID: 1568
		private IHasXmlSchemaInfo schemaInfo;

		// Token: 0x04000621 RID: 1569
		private StringBuilder storedCharacters;
	}
}
