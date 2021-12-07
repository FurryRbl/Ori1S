using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Mono.Xml;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached forward only access to XML data in an <see cref="T:System.Xml.XmlNode" />.</summary>
	// Token: 0x0200010E RID: 270
	public class XmlNodeReader : XmlReader, IHasXmlParserContext, IXmlNamespaceResolver
	{
		/// <summary>Creates an instance of the XmlNodeReader class using the specified <see cref="T:System.Xml.XmlNode" />.</summary>
		/// <param name="node">The XmlNode you want to read. </param>
		// Token: 0x06000AAA RID: 2730 RVA: 0x00037FE4 File Offset: 0x000361E4
		public XmlNodeReader(XmlNode node)
		{
			this.source = new XmlNodeReaderImpl(node);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00037FF8 File Offset: 0x000361F8
		private XmlNodeReader(XmlNodeReaderImpl entityContainer, bool insideAttribute)
		{
			this.source = new XmlNodeReaderImpl(entityContainer);
			this.entityInsideAttribute = insideAttribute;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00038014 File Offset: 0x00036214
		XmlParserContext IHasXmlParserContext.ParserContext
		{
			get
			{
				return ((IHasXmlParserContext)this.Current).ParserContext;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <param name="scope"></param>
		// Token: 0x06000AAD RID: 2733 RVA: 0x00038028 File Offset: 0x00036228
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IXmlNamespaceResolver)this.Current).GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <param name="namespaceName"></param>
		// Token: 0x06000AAE RID: 2734 RVA: 0x0003803C File Offset: 0x0003623C
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			return ((IXmlNamespaceResolver)this.Current).LookupPrefix(ns);
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00038050 File Offset: 0x00036250
		private XmlReader Current
		{
			get
			{
				return (this.entity == null || this.entity.ReadState == ReadState.Initial) ? this.source : this.entity;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node. This number includes default attributes.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0003808C File Offset: 0x0003628C
		public override int AttributeCount
		{
			get
			{
				return this.Current.AttributeCount;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0003809C File Offset: 0x0003629C
		public override string BaseURI
		{
			get
			{
				return this.Current.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlNodeReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlNodeReader" /> class always returns true.</returns>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x000380AC File Offset: 0x000362AC
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. XmlNodeReader always returns true.</returns>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000380B0 File Offset: 0x000362B0
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000380B4 File Offset: 0x000362B4
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
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00038104 File Offset: 0x00036304
		public override bool EOF
		{
			get
			{
				return this.source.EOF;
			}
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00038114 File Offset: 0x00036314
		public override bool HasAttributes
		{
			get
			{
				return this.Current.HasAttributes;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlNodeReader.Value" />.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00038124 File Offset: 0x00036324
		public override bool HasValue
		{
			get
			{
				return this.Current.HasValue;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the document type definition (DTD) or schema.</summary>
		/// <returns>true if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; false if the attribute value was explicitly set.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00038134 File Offset: 0x00036334
		public override bool IsDefault
		{
			get
			{
				return this.Current.IsDefault;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlNodeReader.NodeType" /> equals XmlNodeType.Element) and it ends with /&gt;; otherwise, false.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00038144 File Offset: 0x00036344
		public override bool IsEmptyElement
		{
			get
			{
				return this.Current.IsEmptyElement;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00038154 File Offset: 0x00036354
		public override string LocalName
		{
			get
			{
				return this.Current.LocalName;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlNodeReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00038164 File Offset: 0x00036364
		public override string Name
		{
			get
			{
				return this.Current.Name;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00038174 File Offset: 0x00036374
		public override string NamespaceURI
		{
			get
			{
				return this.Current.NamespaceURI;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00038184 File Offset: 0x00036384
		public override XmlNameTable NameTable
		{
			get
			{
				return this.Current.NameTable;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00038194 File Offset: 0x00036394
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

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000381FC File Offset: 0x000363FC
		public override string Prefix
		{
			get
			{
				return this.Current.Prefix;
			}
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0003820C File Offset: 0x0003640C
		public override ReadState ReadState
		{
			get
			{
				return (this.entity == null) ? this.source.ReadState : ReadState.Interactive;
			}
		}

		/// <summary>Gets the schema information that has been assigned to the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the schema information for the current node.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0003822C File Offset: 0x0003642C
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				IXmlSchemaInfo result;
				if (this.entity != null)
				{
					IXmlSchemaInfo schemaInfo = this.entity.SchemaInfo;
					result = schemaInfo;
				}
				else
				{
					result = this.source.SchemaInfo;
				}
				return result;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNodeReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00038264 File Offset: 0x00036464
		public override string Value
		{
			get
			{
				return this.Current.Value;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00038274 File Offset: 0x00036474
		public override string XmlLang
		{
			get
			{
				return this.Current.XmlLang;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00038284 File Offset: 0x00036484
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.Current.XmlSpace;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlNodeReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00038294 File Offset: 0x00036494
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
		/// <param name="attributeIndex">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlNodeReader.AttributeCount" />. </exception>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x000382B8 File Offset: 0x000364B8
		public override string GetAttribute(int attributeIndex)
		{
			return this.Current.GetAttribute(attributeIndex);
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x000382C8 File Offset: 0x000364C8
		public override string GetAttribute(string name)
		{
			return this.Current.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000382D8 File Offset: 0x000364D8
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.Current.GetAttribute(name, namespaceURI);
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x000382E8 File Offset: 0x000364E8
		public override string LookupNamespace(string prefix)
		{
			return this.Current.LookupNamespace(prefix);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="attributeIndex">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000ACA RID: 2762 RVA: 0x000382F8 File Offset: 0x000364F8
		public override void MoveToAttribute(int i)
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.Current.MoveToAttribute(i);
			this.insideAttribute = true;
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000ACB RID: 2763 RVA: 0x00038338 File Offset: 0x00036538
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
				this.entity.Close();
				this.entity = null;
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000ACC RID: 2764 RVA: 0x000383AC File Offset: 0x000365AC
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.entity != null && !this.entityInsideAttribute)
			{
				return this.entity.MoveToAttribute(localName, namespaceURI);
			}
			if (!this.source.MoveToAttribute(localName, namespaceURI))
			{
				return false;
			}
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.entity.Close();
				this.entity = null;
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000ACD RID: 2765 RVA: 0x00038424 File Offset: 0x00036624
		public override bool MoveToElement()
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.entity = null;
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
		// Token: 0x06000ACE RID: 2766 RVA: 0x00038460 File Offset: 0x00036660
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
				this.entity.Close();
				this.entity = null;
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x06000ACF RID: 2767 RVA: 0x000384D4 File Offset: 0x000366D4
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
				this.entity.Close();
				this.entity = null;
			}
			this.insideAttribute = true;
			return true;
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00038548 File Offset: 0x00036748
		public override bool Read()
		{
			this.insideAttribute = false;
			if (this.entity != null && (this.entityInsideAttribute || this.entity.EOF))
			{
				this.entity = null;
			}
			if (this.entity != null)
			{
				this.entity.Read();
				return true;
			}
			return this.source.Read();
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x000385B0 File Offset: 0x000367B0
		public override bool ReadAttributeValue()
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				if (!this.entity.EOF)
				{
					this.entity.Read();
					return true;
				}
				this.entity = null;
			}
			return this.Current.ReadAttributeValue();
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlNodeReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x00038608 File Offset: 0x00036808
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
		///   <see cref="M:System.Xml.XmlNodeReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x00038640 File Offset: 0x00036840
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
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00038678 File Offset: 0x00036878
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
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x000386B0 File Offset: 0x000368B0
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int length)
		{
			if (this.entity != null)
			{
				return this.entity.ReadElementContentAsBinHex(buffer, offset, length);
			}
			return this.source.ReadElementContentAsBinHex(buffer, offset, length);
		}

		/// <summary>Reads the contents of an element or text node as a string.</summary>
		/// <returns>The contents of the element or text-like node (This can include CDATA, Text nodes, and so on). This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x000386E8 File Offset: 0x000368E8
		public override string ReadString()
		{
			return base.ReadString();
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node. </exception>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000386F0 File Offset: 0x000368F0
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
				this.entity = new XmlNodeReader(this.source, this.insideAttribute);
			}
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0003874C File Offset: 0x0003694C
		public override void Skip()
		{
			if (this.entity != null && this.entityInsideAttribute)
			{
				this.entity = null;
			}
			this.Current.Skip();
		}

		// Token: 0x04000548 RID: 1352
		private XmlReader entity;

		// Token: 0x04000549 RID: 1353
		private XmlNodeReaderImpl source;

		// Token: 0x0400054A RID: 1354
		private bool entityInsideAttribute;

		// Token: 0x0400054B RID: 1355
		private bool insideAttribute;
	}
}
