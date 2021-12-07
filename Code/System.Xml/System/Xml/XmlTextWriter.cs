using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only way of generating streams or files containing XML data that conforms to the W3C Extensible Markup Language (XML) 1.0 and the Namespaces in XML recommendations. </summary>
	// Token: 0x0200012C RID: 300
	public class XmlTextWriter : XmlWriter
	{
		/// <summary>Creates an instance of the <see cref="T:System.Xml.XmlTextWriter" /> class using the specified file.</summary>
		/// <param name="filename">The filename to write to. If the file exists, it truncates it and overwrites it with the new content. </param>
		/// <param name="encoding">The encoding to generate. If encoding is null it writes the file out as UTF-8, and omits the encoding attribute from the ProcessingInstruction. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported; the filename is empty, contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentNullException">The filename is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory to write to is not found. </exception>
		/// <exception cref="T:System.IO.IOException">The filename includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000DDC RID: 3548 RVA: 0x000441A4 File Offset: 0x000423A4
		public XmlTextWriter(string filename, Encoding encoding) : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None), encoding)
		{
		}

		/// <summary>Creates an instance of the XmlTextWriter class using the specified stream and encoding.</summary>
		/// <param name="w">The stream to which you want to write. </param>
		/// <param name="encoding">The encoding to generate. If encoding is null it writes out the stream as UTF-8 and omits the encoding attribute from the ProcessingInstruction. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported or the stream cannot be written to. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="w" /> is null. </exception>
		// Token: 0x06000DDD RID: 3549 RVA: 0x000441B8 File Offset: 0x000423B8
		public XmlTextWriter(Stream stream, Encoding encoding) : this(new StreamWriter(stream, (encoding != null) ? encoding : XmlTextWriter.unmarked_utf8encoding))
		{
			this.ignore_encoding = (encoding == null);
			this.Initialize(this.writer);
			this.allow_doc_fragment = true;
		}

		/// <summary>Creates an instance of the XmlTextWriter class using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="w">The TextWriter to write to. It is assumed that the TextWriter is already set to the correct encoding. </param>
		// Token: 0x06000DDE RID: 3550 RVA: 0x00044200 File Offset: 0x00042400
		public XmlTextWriter(TextWriter writer)
		{
			this.close_output_stream = true;
			this.namespaces = true;
			this.newline_handling = NewLineHandling.None;
			this.elements = new XmlTextWriter.XmlNodeInfo[10];
			this.new_local_namespaces = new Stack();
			this.explicit_nsdecls = new ArrayList();
			this.indent_count = 2;
			this.indent_char = ' ';
			this.indent_string = "  ";
			this.quote_char = '"';
			base..ctor();
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.ignore_encoding = (writer.Encoding == null);
			this.Initialize(writer);
			this.allow_doc_fragment = true;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0004429C File Offset: 0x0004249C
		internal XmlTextWriter(TextWriter writer, XmlWriterSettings settings, bool closeOutput)
		{
			this.close_output_stream = true;
			this.namespaces = true;
			this.newline_handling = NewLineHandling.None;
			this.elements = new XmlTextWriter.XmlNodeInfo[10];
			this.new_local_namespaces = new Stack();
			this.explicit_nsdecls = new ArrayList();
			this.indent_count = 2;
			this.indent_char = ' ';
			this.indent_string = "  ";
			this.quote_char = '"';
			base..ctor();
			this.v2 = true;
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			this.Initialize(writer);
			this.close_output_stream = closeOutput;
			this.allow_doc_fragment = (settings.ConformanceLevel != ConformanceLevel.Document);
			switch (settings.ConformanceLevel)
			{
			case ConformanceLevel.Auto:
				this.xmldecl_state = ((!settings.OmitXmlDeclaration) ? XmlTextWriter.XmlDeclState.Allow : XmlTextWriter.XmlDeclState.Ignore);
				break;
			case ConformanceLevel.Fragment:
				this.xmldecl_state = XmlTextWriter.XmlDeclState.Prohibit;
				break;
			case ConformanceLevel.Document:
				this.xmldecl_state = ((!settings.OmitXmlDeclaration) ? XmlTextWriter.XmlDeclState.Auto : XmlTextWriter.XmlDeclState.Ignore);
				break;
			}
			if (settings.Indent)
			{
				this.Formatting = Formatting.Indented;
			}
			this.indent_string = ((settings.IndentChars != null) ? settings.IndentChars : string.Empty);
			if (settings.NewLineChars != null)
			{
				this.newline = settings.NewLineChars;
			}
			this.indent_attributes = settings.NewLineOnAttributes;
			this.check_character_validity = settings.CheckCharacters;
			this.newline_handling = settings.NewLineHandling;
			this.namespace_handling = settings.NamespaceHandling;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0004442C File Offset: 0x0004262C
		private void Initialize(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			XmlNameTable nameTable = new NameTable();
			this.writer = writer;
			if (writer is StreamWriter)
			{
				this.base_stream = ((StreamWriter)writer).BaseStream;
			}
			this.source = writer;
			this.nsmanager = new XmlNamespaceManager(nameTable);
			this.newline = writer.NewLine;
			char[] array;
			if (this.newline_handling != NewLineHandling.None)
			{
				RuntimeHelpers.InitializeArray(array = new char[5], fieldof(<PrivateImplementationDetails>.$$field-40).FieldHandle);
			}
			else
			{
				char[] array2 = new char[3];
				array2[0] = '&';
				array2[1] = '<';
				array = array2;
				array2[2] = '>';
			}
			XmlTextWriter.escaped_text_chars = array;
			XmlTextWriter.escaped_attr_chars = new char[]
			{
				'"',
				'&',
				'<',
				'>',
				'\r',
				'\n'
			};
		}

		/// <summary>Indicates how the output is formatted.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Formatting" /> values. The default is Formatting.None (no special formatting).</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000444E4 File Offset: 0x000426E4
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000444F8 File Offset: 0x000426F8
		public Formatting Formatting
		{
			get
			{
				return (!this.indent) ? Formatting.None : Formatting.Indented;
			}
			set
			{
				this.indent = (value == Formatting.Indented);
			}
		}

		/// <summary>Gets or sets how many IndentChars to write for each level in the hierarchy when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to Formatting.Indented.</summary>
		/// <returns>Number of IndentChars for each level. The default is 2.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to a negative value. </exception>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00044504 File Offset: 0x00042704
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0004450C File Offset: 0x0004270C
		public int Indentation
		{
			get
			{
				return this.indent_count;
			}
			set
			{
				if (value < 0)
				{
					throw this.ArgumentError("Indentation must be non-negative integer.");
				}
				this.indent_count = value;
				this.indent_string = ((value != 0) ? new string(this.indent_char, this.indent_count) : string.Empty);
			}
		}

		/// <summary>Gets or sets which character to use for indenting when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to Formatting.Indented.</summary>
		/// <returns>The character to use for indenting. The default is space.Note:The XmlTextWriter allows you to set this property to any character. To ensure valid XML, you must specify a valid white space character, 0x9, 0x10, 0x13 or 0x20.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0004455C File Offset: 0x0004275C
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00044564 File Offset: 0x00042764
		public char IndentChar
		{
			get
			{
				return this.indent_char;
			}
			set
			{
				this.indent_char = value;
				this.indent_string = new string(this.indent_char, this.indent_count);
			}
		}

		/// <summary>Gets or sets which character to use to quote attribute values.</summary>
		/// <returns>The character to use to quote attribute values. This must be a single quote (&amp;#39;) or a double quote (&amp;#34;). The default is a double quote.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to something other than either a single or double quote. </exception>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00044584 File Offset: 0x00042784
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0004458C File Offset: 0x0004278C
		public char QuoteChar
		{
			get
			{
				return this.quote_char;
			}
			set
			{
				if (this.state == WriteState.Attribute)
				{
					throw this.InvalidOperation("QuoteChar must not be changed inside attribute value.");
				}
				if (value != '\'' && value != '"')
				{
					throw this.ArgumentError("Only ' and \" are allowed as an attribute quote character.");
				}
				this.quote_char = value;
				XmlTextWriter.escaped_attr_chars[0] = this.quote_char;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang or null if there is no xml:lang in the current scope.</returns>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000445E4 File Offset: 0x000427E4
		public override string XmlLang
		{
			get
			{
				return (this.open_count != 0) ? this.elements[this.open_count - 1].XmlLang : null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlSpace" /> representing the current xml:space scope.</summary>
		/// <returns>An XmlSpace representing the current xml:space scope.Value Meaning None This is the default if no xml:space scope exists. Default The current scope is xml:space="default". Preserve The current scope is xml:space="preserve". </returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0004460C File Offset: 0x0004280C
		public override XmlSpace XmlSpace
		{
			get
			{
				return (this.open_count != 0) ? this.elements[this.open_count - 1].XmlSpace : XmlSpace.None;
			}
		}

		/// <summary>Gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00044634 File Offset: 0x00042834
		public override WriteState WriteState
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <returns>The matching prefix. Or null if no matching namespace URI is found in the current scope.</returns>
		/// <param name="ns">Namespace URI whose prefix you want to find. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns" /> is either null or String.Empty. </exception>
		// Token: 0x06000DED RID: 3565 RVA: 0x0004463C File Offset: 0x0004283C
		public override string LookupPrefix(string namespaceUri)
		{
			if (namespaceUri == null || namespaceUri == string.Empty)
			{
				throw this.ArgumentError("The Namespace cannot be empty.");
			}
			if (namespaceUri == this.nsmanager.DefaultNamespace)
			{
				return string.Empty;
			}
			return this.nsmanager.LookupPrefixExclusive(namespaceUri, false);
		}

		/// <summary>Gets the underlying stream object.</summary>
		/// <returns>The stream to which the XmlTextWriter is writing or null if the XmlTextWriter was constructed using a <see cref="T:System.IO.TextWriter" /> that does not inherit from the <see cref="T:System.IO.StreamWriter" /> class.</returns>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00044698 File Offset: 0x00042898
		public Stream BaseStream
		{
			get
			{
				return this.base_stream;
			}
		}

		/// <summary>Closes this stream and the underlying stream.</summary>
		// Token: 0x06000DEF RID: 3567 RVA: 0x000446A0 File Offset: 0x000428A0
		public override void Close()
		{
			if (this.state != WriteState.Error)
			{
				if (this.state == WriteState.Attribute)
				{
					this.WriteEndAttribute();
				}
				while (this.open_count > 0)
				{
					this.WriteEndElement();
				}
			}
			if (this.close_output_stream)
			{
				this.writer.Close();
			}
			else
			{
				this.writer.Flush();
			}
			this.state = WriteState.Closed;
		}

		/// <summary>Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x00044710 File Offset: 0x00042910
		public override void Flush()
		{
			this.writer.Flush();
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to support namespaces; otherwise, false.The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">You can only change this property when in the WriteState.Start state. </exception>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00044720 File Offset: 0x00042920
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00044728 File Offset: 0x00042928
		public bool Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				if (this.state != WriteState.Start)
				{
					throw this.InvalidOperation("This property must be set before writing output.");
				}
				this.namespaces = value;
			}
		}

		/// <summary>Writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x00044748 File Offset: 0x00042948
		public override void WriteStartDocument()
		{
			this.WriteStartDocumentCore(false, false);
			this.is_document_entity = true;
		}

		/// <summary>Writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no". </param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0004475C File Offset: 0x0004295C
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocumentCore(true, standalone);
			this.is_document_entity = true;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00044770 File Offset: 0x00042970
		private void WriteStartDocumentCore(bool outputStd, bool standalone)
		{
			if (this.state != WriteState.Start)
			{
				throw this.StateError("XmlDeclaration");
			}
			switch (this.xmldecl_state)
			{
			case XmlTextWriter.XmlDeclState.Ignore:
				return;
			case XmlTextWriter.XmlDeclState.Prohibit:
				throw this.InvalidOperation("WriteStartDocument cannot be called when ConformanceLevel is Fragment.");
			}
			this.state = WriteState.Prolog;
			this.writer.Write("<?xml version=");
			this.writer.Write(this.quote_char);
			this.writer.Write("1.0");
			this.writer.Write(this.quote_char);
			if (!this.ignore_encoding)
			{
				this.writer.Write(" encoding=");
				this.writer.Write(this.quote_char);
				this.writer.Write(this.writer.Encoding.WebName);
				this.writer.Write(this.quote_char);
			}
			if (outputStd)
			{
				this.writer.Write(" standalone=");
				this.writer.Write(this.quote_char);
				this.writer.Write((!standalone) ? "no" : "yes");
				this.writer.Write(this.quote_char);
			}
			this.writer.Write("?>");
			this.xmldecl_state = XmlTextWriter.XmlDeclState.Ignore;
		}

		/// <summary>Closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid. </exception>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x000448D4 File Offset: 0x00042AD4
		public override void WriteEndDocument()
		{
			WriteState writeState = this.state;
			if (writeState != WriteState.Closed && writeState != WriteState.Error && writeState != WriteState.Start)
			{
				if (this.state == WriteState.Attribute)
				{
					this.WriteEndAttribute();
				}
				while (this.open_count > 0)
				{
					this.WriteEndElement();
				}
				this.state = WriteState.Start;
				this.is_document_entity = false;
				return;
			}
			throw this.StateError("EndDocument");
		}

		/// <summary>Writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty. </param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments. </param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument. </param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument. </param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or String.Empty-or- the value for <paramref name="name" /> would result in invalid XML. </exception>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00044944 File Offset: 0x00042B44
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (name == null)
			{
				throw this.ArgumentError("name");
			}
			if (!XmlChar.IsName(name))
			{
				throw this.ArgumentError("name");
			}
			if (this.node_state != XmlNodeType.None)
			{
				throw this.StateError("DocType");
			}
			this.node_state = XmlNodeType.DocumentType;
			if (this.xmldecl_state == XmlTextWriter.XmlDeclState.Auto)
			{
				this.OutputAutoStartDocument();
			}
			this.WriteIndent();
			this.writer.Write("<!DOCTYPE ");
			this.writer.Write(name);
			if (pubid != null)
			{
				this.writer.Write(" PUBLIC ");
				this.writer.Write(this.quote_char);
				this.writer.Write(pubid);
				this.writer.Write(this.quote_char);
				this.writer.Write(' ');
				this.writer.Write(this.quote_char);
				if (sysid != null)
				{
					this.writer.Write(sysid);
				}
				this.writer.Write(this.quote_char);
			}
			else if (sysid != null)
			{
				this.writer.Write(" SYSTEM ");
				this.writer.Write(this.quote_char);
				this.writer.Write(sysid);
				this.writer.Write(this.quote_char);
			}
			if (subset != null)
			{
				this.writer.Write("[");
				this.writer.Write(subset);
				this.writer.Write("]");
			}
			this.writer.Write('>');
			this.state = WriteState.Prolog;
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element. </param>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix then the writer automatically writes that prefix also. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x00044AE4 File Offset: 0x00042CE4
		public override void WriteStartElement(string prefix, string localName, string namespaceUri)
		{
			if (this.state == WriteState.Error || this.state == WriteState.Closed)
			{
				throw this.StateError("StartTag");
			}
			this.node_state = XmlNodeType.Element;
			bool flag = prefix == null;
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (!this.namespaces && namespaceUri != null && namespaceUri.Length > 0)
			{
				throw this.ArgumentError("Namespace is disabled in this XmlTextWriter.");
			}
			if (!this.namespaces && prefix.Length > 0)
			{
				throw this.ArgumentError("Namespace prefix is disabled in this XmlTextWriter.");
			}
			if (prefix.Length > 0 && namespaceUri == null)
			{
				namespaceUri = this.nsmanager.LookupNamespace(prefix, false);
				if (namespaceUri == null || namespaceUri.Length == 0)
				{
					throw this.ArgumentError("Namespace URI must not be null when prefix is not an empty string.");
				}
			}
			if (this.namespaces && prefix != null && prefix.Length == 3 && namespaceUri != "http://www.w3.org/XML/1998/namespace" && (prefix[0] == 'x' || prefix[0] == 'X') && (prefix[1] == 'm' || prefix[1] == 'M') && (prefix[2] == 'l' || prefix[2] == 'L'))
			{
				throw new ArgumentException("A prefix cannot be equivalent to \"xml\" in case-insensitive match.");
			}
			if (this.xmldecl_state == XmlTextWriter.XmlDeclState.Auto)
			{
				this.OutputAutoStartDocument();
			}
			if (this.state == WriteState.Element)
			{
				this.CloseStartElement();
			}
			if (this.open_count > 0)
			{
				this.elements[this.open_count - 1].HasElements = true;
			}
			this.nsmanager.PushScope();
			if (this.namespaces && namespaceUri != null)
			{
				if (flag && namespaceUri.Length > 0)
				{
					prefix = this.LookupPrefix(namespaceUri);
				}
				if (prefix == null || namespaceUri.Length == 0)
				{
					prefix = string.Empty;
				}
			}
			this.WriteIndent();
			this.writer.Write("<");
			if (prefix.Length > 0)
			{
				this.writer.Write(prefix);
				this.writer.Write(':');
			}
			this.writer.Write(localName);
			if (this.elements.Length == this.open_count)
			{
				XmlTextWriter.XmlNodeInfo[] destinationArray = new XmlTextWriter.XmlNodeInfo[this.open_count << 1];
				Array.Copy(this.elements, destinationArray, this.open_count);
				this.elements = destinationArray;
			}
			if (this.elements[this.open_count] == null)
			{
				this.elements[this.open_count] = new XmlTextWriter.XmlNodeInfo();
			}
			XmlTextWriter.XmlNodeInfo xmlNodeInfo = this.elements[this.open_count];
			xmlNodeInfo.Prefix = prefix;
			xmlNodeInfo.LocalName = localName;
			xmlNodeInfo.NS = namespaceUri;
			xmlNodeInfo.HasSimple = false;
			xmlNodeInfo.HasElements = false;
			xmlNodeInfo.XmlLang = this.XmlLang;
			xmlNodeInfo.XmlSpace = this.XmlSpace;
			this.open_count++;
			if (this.namespaces && namespaceUri != null)
			{
				string a = this.nsmanager.LookupNamespace(prefix, false);
				if (a != namespaceUri)
				{
					this.nsmanager.AddNamespace(prefix, namespaceUri);
					this.new_local_namespaces.Push(prefix);
				}
			}
			this.state = WriteState.Element;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00044E20 File Offset: 0x00043020
		private void CloseStartElement()
		{
			this.CloseStartElementCore();
			if (this.state == WriteState.Element)
			{
				this.writer.Write('>');
			}
			this.state = WriteState.Content;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00044E54 File Offset: 0x00043054
		private void CloseStartElementCore()
		{
			if (this.state == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			if (this.new_local_namespaces.Count == 0)
			{
				if (this.explicit_nsdecls.Count > 0)
				{
					this.explicit_nsdecls.Clear();
				}
				return;
			}
			int count = this.explicit_nsdecls.Count;
			while (this.new_local_namespaces.Count > 0)
			{
				string text = (string)this.new_local_namespaces.Pop();
				bool flag = false;
				for (int i = 0; i < this.explicit_nsdecls.Count; i++)
				{
					if ((string)this.explicit_nsdecls[i] == text)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.explicit_nsdecls.Add(text);
				}
			}
			for (int j = count; j < this.explicit_nsdecls.Count; j++)
			{
				string text2 = (string)this.explicit_nsdecls[j];
				string text3 = this.nsmanager.LookupNamespace(text2, false);
				if (text3 != null)
				{
					if (text2.Length > 0)
					{
						this.writer.Write(" xmlns:");
						this.writer.Write(text2);
					}
					else
					{
						this.writer.Write(" xmlns");
					}
					this.writer.Write('=');
					this.writer.Write(this.quote_char);
					this.WriteEscapedString(text3, true);
					this.writer.Write(this.quote_char);
				}
			}
			this.explicit_nsdecls.Clear();
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000DFB RID: 3579 RVA: 0x00045000 File Offset: 0x00043200
		public override void WriteEndElement()
		{
			this.WriteEndElementCore(false);
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000DFC RID: 3580 RVA: 0x0004500C File Offset: 0x0004320C
		public override void WriteFullEndElement()
		{
			this.WriteEndElementCore(true);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00045018 File Offset: 0x00043218
		private void WriteEndElementCore(bool full)
		{
			if (this.state == WriteState.Error || this.state == WriteState.Closed)
			{
				throw this.StateError("EndElement");
			}
			if (this.open_count == 0)
			{
				throw this.InvalidOperation("There is no more open element.");
			}
			this.CloseStartElementCore();
			this.nsmanager.PopScope();
			if (this.state == WriteState.Element)
			{
				if (full)
				{
					this.writer.Write('>');
				}
				else
				{
					this.writer.Write(" />");
				}
			}
			if (full || this.state == WriteState.Content)
			{
				this.WriteIndentEndElement();
			}
			XmlTextWriter.XmlNodeInfo xmlNodeInfo = this.elements[--this.open_count];
			if (full || this.state == WriteState.Content)
			{
				this.writer.Write("</");
				if (xmlNodeInfo.Prefix.Length > 0)
				{
					this.writer.Write(xmlNodeInfo.Prefix);
					this.writer.Write(':');
				}
				this.writer.Write(xmlNodeInfo.LocalName);
				this.writer.Write('>');
			}
			this.state = WriteState.Content;
			if (this.open_count == 0)
			{
				this.node_state = XmlNodeType.EndElement;
			}
		}

		/// <summary>Writes the start of an attribute.</summary>
		/// <param name="prefix">Namespace prefix of the attribute. </param>
		/// <param name="localName">LocalName of the attribute. </param>
		/// <param name="ns">NamespaceURI of the attribute </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty. </exception>
		// Token: 0x06000DFE RID: 3582 RVA: 0x00045160 File Offset: 0x00043360
		public override void WriteStartAttribute(string prefix, string localName, string namespaceUri)
		{
			if (this.state == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			if (this.state != WriteState.Element && this.state != WriteState.Start)
			{
				throw this.StateError("Attribute");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			bool flag;
			if (namespaceUri == "http://www.w3.org/2000/xmlns/")
			{
				flag = true;
				if (prefix.Length == 0 && localName != "xmlns")
				{
					prefix = "xmlns";
				}
			}
			else
			{
				flag = (prefix == "xmlns" || (localName == "xmlns" && prefix.Length == 0));
			}
			if (this.namespaces)
			{
				if (prefix == "xml")
				{
					namespaceUri = "http://www.w3.org/XML/1998/namespace";
				}
				else if (namespaceUri == null)
				{
					if (flag)
					{
						namespaceUri = "http://www.w3.org/2000/xmlns/";
					}
					else
					{
						namespaceUri = string.Empty;
					}
				}
				if (flag && namespaceUri != "http://www.w3.org/2000/xmlns/")
				{
					throw this.ArgumentError(string.Format("The 'xmlns' attribute is bound to the reserved namespace '{0}'", "http://www.w3.org/2000/xmlns/"));
				}
				if (prefix.Length > 0 && namespaceUri.Length == 0)
				{
					namespaceUri = this.nsmanager.LookupNamespace(prefix, false);
					if (namespaceUri == null || namespaceUri.Length == 0)
					{
						throw this.ArgumentError("Namespace URI must not be null when prefix is not an empty string.");
					}
				}
				if (!flag && namespaceUri.Length > 0)
				{
					prefix = this.DetermineAttributePrefix(prefix, localName, namespaceUri);
				}
			}
			if (this.indent_attributes)
			{
				this.WriteIndentAttribute();
			}
			else if (this.state != WriteState.Start)
			{
				this.writer.Write(' ');
			}
			if (prefix.Length > 0)
			{
				this.writer.Write(prefix);
				this.writer.Write(':');
			}
			this.writer.Write(localName);
			this.writer.Write('=');
			this.writer.Write(this.quote_char);
			if (flag || prefix == "xml")
			{
				if (this.preserver == null)
				{
					this.preserver = new StringWriter();
				}
				else
				{
					this.preserver.GetStringBuilder().Length = 0;
				}
				this.writer = this.preserver;
				if (!flag)
				{
					this.is_preserved_xmlns = false;
					this.preserved_name = localName;
				}
				else
				{
					this.is_preserved_xmlns = true;
					this.preserved_name = ((!(localName == "xmlns")) ? localName : string.Empty);
				}
			}
			this.state = WriteState.Attribute;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00045400 File Offset: 0x00043600
		private string DetermineAttributePrefix(string prefix, string local, string ns)
		{
			bool flag = false;
			if (prefix.Length == 0)
			{
				prefix = this.LookupPrefix(ns);
				if (prefix != null && prefix.Length > 0)
				{
					return prefix;
				}
				flag = true;
			}
			else
			{
				prefix = this.nsmanager.NameTable.Add(prefix);
				string text = this.nsmanager.LookupNamespace(prefix, true);
				if (text == ns)
				{
					return prefix;
				}
				if (text != null)
				{
					this.nsmanager.RemoveNamespace(prefix, text);
					if (this.nsmanager.LookupNamespace(prefix, true) != text)
					{
						flag = true;
						this.nsmanager.AddNamespace(prefix, text);
					}
				}
			}
			if (flag)
			{
				prefix = this.MockupPrefix(ns, true);
			}
			this.new_local_namespaces.Push(prefix);
			this.nsmanager.AddNamespace(prefix, ns);
			return prefix;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000454D4 File Offset: 0x000436D4
		private string MockupPrefix(string ns, bool skipLookup)
		{
			string text = (!skipLookup) ? this.LookupPrefix(ns) : null;
			if (text != null && text.Length > 0)
			{
				return text;
			}
			int num = 1;
			for (;;)
			{
				text = XmlTextWriter.StringUtil.Format("d{0}p{1}", new object[]
				{
					this.open_count,
					num
				});
				if (!this.new_local_namespaces.Contains(text))
				{
					if (this.nsmanager.LookupNamespace(this.nsmanager.NameTable.Get(text)) == null)
					{
						break;
					}
				}
				num++;
			}
			this.nsmanager.AddNamespace(text, ns);
			this.new_local_namespaces.Push(text);
			return text;
		}

		/// <summary>Closes the previous <see cref="M:System.Xml.XmlTextWriter.WriteStartAttribute(System.String,System.String,System.String)" /> call.</summary>
		// Token: 0x06000E01 RID: 3585 RVA: 0x00045598 File Offset: 0x00043798
		public override void WriteEndAttribute()
		{
			if (this.state != WriteState.Attribute)
			{
				throw this.StateError("End of attribute");
			}
			if (this.writer == this.preserver)
			{
				this.writer = this.source;
				string text = this.preserver.ToString();
				if (this.is_preserved_xmlns)
				{
					if (this.preserved_name.Length > 0 && text.Length == 0)
					{
						throw this.ArgumentError("Non-empty prefix must be mapped to non-empty namespace URI.");
					}
					string a = this.nsmanager.LookupNamespace(this.preserved_name, false);
					if ((this.namespace_handling & NamespaceHandling.OmitDuplicates) == NamespaceHandling.Default || a != text)
					{
						this.explicit_nsdecls.Add(this.preserved_name);
					}
					if (this.open_count > 0)
					{
						if (this.v2 && this.elements[this.open_count - 1].Prefix == this.preserved_name && this.elements[this.open_count - 1].NS != text)
						{
							throw new XmlException(string.Format("Cannot redefine the namespace for prefix '{0}' used at current element", this.preserved_name));
						}
						if (!(this.elements[this.open_count - 1].NS == string.Empty) || !(this.elements[this.open_count - 1].Prefix == this.preserved_name))
						{
							if (a != text)
							{
								this.nsmanager.AddNamespace(this.preserved_name, text);
							}
						}
					}
				}
				else
				{
					string text2 = this.preserved_name;
					if (text2 != null)
					{
						if (XmlTextWriter.<>f__switch$map3B == null)
						{
							XmlTextWriter.<>f__switch$map3B = new Dictionary<string, int>(2)
							{
								{
									"lang",
									0
								},
								{
									"space",
									1
								}
							};
						}
						int num;
						if (XmlTextWriter.<>f__switch$map3B.TryGetValue(text2, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									string text3 = text;
									if (text3 != null)
									{
										if (XmlTextWriter.<>f__switch$map3A == null)
										{
											XmlTextWriter.<>f__switch$map3A = new Dictionary<string, int>(2)
											{
												{
													"default",
													0
												},
												{
													"preserve",
													1
												}
											};
										}
										int num2;
										if (XmlTextWriter.<>f__switch$map3A.TryGetValue(text3, out num2))
										{
											if (num2 != 0)
											{
												if (num2 != 1)
												{
													goto IL_2C5;
												}
												if (this.open_count > 0)
												{
													this.elements[this.open_count - 1].XmlSpace = XmlSpace.Preserve;
												}
											}
											else if (this.open_count > 0)
											{
												this.elements[this.open_count - 1].XmlSpace = XmlSpace.Default;
											}
											goto IL_2D6;
										}
									}
									IL_2C5:
									throw this.ArgumentError("Invalid value for xml:space.");
								}
							}
							else if (this.open_count > 0)
							{
								this.elements[this.open_count - 1].XmlLang = text;
							}
						}
					}
				}
				IL_2D6:
				this.writer.Write(text);
			}
			this.writer.Write(this.quote_char);
			this.state = WriteState.Element;
		}

		/// <summary>Writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000E02 RID: 3586 RVA: 0x000458A0 File Offset: 0x00043AA0
		public override void WriteComment(string text)
		{
			if (text == null)
			{
				throw this.ArgumentError("text");
			}
			if (text.Length > 0 && text[text.Length - 1] == '-')
			{
				throw this.ArgumentError("An input string to WriteComment method must not end with '-'. Escape it with '&#2D;'.");
			}
			if (XmlTextWriter.StringUtil.IndexOf(text, "--") > 0)
			{
				throw this.ArgumentError("An XML comment cannot end with \"-\".");
			}
			if (this.state == WriteState.Attribute || this.state == WriteState.Element)
			{
				this.CloseStartElement();
			}
			this.WriteIndent();
			this.ShiftStateTopLevel("Comment", false, false, false);
			this.writer.Write("<!--");
			this.writer.Write(text);
			this.writer.Write("-->");
		}

		/// <summary>Writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">Name of the processing instruction. </param>
		/// <param name="text">Text to include in the processing instruction. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name" /> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlTextWriter.WriteStartDocument" /> has already been called. </exception>
		// Token: 0x06000E03 RID: 3587 RVA: 0x00045968 File Offset: 0x00043B68
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (name == null)
			{
				throw this.ArgumentError("name");
			}
			if (text == null)
			{
				throw this.ArgumentError("text");
			}
			this.WriteIndent();
			if (!XmlChar.IsName(name))
			{
				throw this.ArgumentError("A processing instruction name must be a valid XML name.");
			}
			if (XmlTextWriter.StringUtil.IndexOf(text, "?>") > 0)
			{
				throw this.ArgumentError("Processing instruction cannot contain \"?>\" as its value.");
			}
			this.ShiftStateTopLevel("ProcessingInstruction", false, name == "xml", false);
			this.writer.Write("<?");
			this.writer.Write(name);
			this.writer.Write(' ');
			this.writer.Write(text);
			this.writer.Write("?>");
			if (this.state == WriteState.Start)
			{
				this.state = WriteState.Prolog;
			}
		}

		/// <summary>Writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters. </param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters. </exception>
		// Token: 0x06000E04 RID: 3588 RVA: 0x00045A44 File Offset: 0x00043C44
		public override void WriteWhitespace(string text)
		{
			if (text == null)
			{
				throw this.ArgumentError("text");
			}
			if (text.Length == 0 || XmlChar.IndexOfNonWhitespace(text) >= 0)
			{
				throw this.ArgumentError("WriteWhitespace method accepts only whitespaces.");
			}
			this.ShiftStateTopLevel("Whitespace", true, false, true);
			this.writer.Write(text);
		}

		/// <summary>Writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">Text to place inside the CDATA block. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000E05 RID: 3589 RVA: 0x00045AA0 File Offset: 0x00043CA0
		public override void WriteCData(string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}
			this.ShiftStateContent("CData", false);
			if (XmlTextWriter.StringUtil.IndexOf(text, "]]>") >= 0)
			{
				throw this.ArgumentError("CDATA section must not contain ']]>'.");
			}
			this.writer.Write("<![CDATA[");
			this.WriteCheckedString(text);
			this.writer.Write("]]>");
		}

		/// <summary>Writes the given text content.</summary>
		/// <param name="text">Text to write. </param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair. </exception>
		// Token: 0x06000E06 RID: 3590 RVA: 0x00045B0C File Offset: 0x00043D0C
		public override void WriteString(string text)
		{
			if (text == null || (text.Length == 0 && !this.v2))
			{
				return;
			}
			this.ShiftStateContent("Text", true);
			this.WriteEscapedString(text, this.state == WriteState.Attribute);
		}

		/// <summary>Writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write. </param>
		// Token: 0x06000E07 RID: 3591 RVA: 0x00045B54 File Offset: 0x00043D54
		public override void WriteRaw(string raw)
		{
			if (raw == null)
			{
				return;
			}
			this.ShiftStateTopLevel("Raw string", true, true, true);
			this.writer.Write(raw);
		}

		/// <summary>Forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">Unicode character for which to generate a character entity. </param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff; or the text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000E08 RID: 3592 RVA: 0x00045B78 File Offset: 0x00043D78
		public override void WriteCharEntity(char ch)
		{
			this.WriteCharacterEntity(ch, '\0', false);
		}

		/// <summary>Generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF. </param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF. </param>
		/// <exception cref="T:System.Exception">An invalid surrogate character pair was passed. </exception>
		// Token: 0x06000E09 RID: 3593 RVA: 0x00045B84 File Offset: 0x00043D84
		public override void WriteSurrogateCharEntity(char low, char high)
		{
			this.WriteCharacterEntity(low, high, true);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00045B90 File Offset: 0x00043D90
		private void WriteCharacterEntity(char ch, char high, bool surrogate)
		{
			if (surrogate && ('\ud800' > high || high > '\udc00' || '\udc00' > ch || ch > '\udfff'))
			{
				throw this.ArgumentError(string.Format("Invalid surrogate pair was found. Low: &#x{0:X}; High: &#x{0:X};", (int)ch, (int)high));
			}
			if (this.check_character_validity && XmlChar.IsInvalid((int)ch))
			{
				throw this.ArgumentError(string.Format("Invalid character &#x{0:X};", (int)ch));
			}
			this.ShiftStateContent("Character", true);
			int num = (!surrogate) ? ((int)ch) : ((int)((high - '\ud800') * 'Ѐ' + ch - '\udc00') + 65536);
			this.writer.Write("&#x");
			this.writer.Write(num.ToString("X", CultureInfo.InvariantCulture));
			this.writer.Write(';');
		}

		/// <summary>Writes out an entity reference as &amp;name;.</summary>
		/// <param name="name">Name of the entity reference. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000E0B RID: 3595 RVA: 0x00045C88 File Offset: 0x00043E88
		public override void WriteEntityRef(string name)
		{
			if (name == null)
			{
				throw this.ArgumentError("name");
			}
			if (!XmlChar.IsName(name))
			{
				throw this.ArgumentError("Argument name must be a valid XML name.");
			}
			this.ShiftStateContent("Entity reference", true);
			this.writer.Write('&');
			this.writer.Write(name);
			this.writer.Write(';');
		}

		/// <summary>Writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000E0C RID: 3596 RVA: 0x00045CF0 File Offset: 0x00043EF0
		public override void WriteName(string name)
		{
			if (name == null)
			{
				throw this.ArgumentError("name");
			}
			if (!XmlChar.IsName(name))
			{
				throw this.ArgumentError("Not a valid name string.");
			}
			this.WriteString(name);
		}

		/// <summary>Writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000E0D RID: 3597 RVA: 0x00045D30 File Offset: 0x00043F30
		public override void WriteNmToken(string nmtoken)
		{
			if (nmtoken == null)
			{
				throw this.ArgumentError("nmtoken");
			}
			if (!XmlChar.IsNmToken(nmtoken))
			{
				throw this.ArgumentError("Not a valid NMTOKEN string.");
			}
			this.WriteString(nmtoken);
		}

		/// <summary>Writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write. </param>
		/// <param name="ns">The namespace URI to associate with the name. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty.<paramref name="localName" /> is not a valid name according to the W3C Namespaces spec. </exception>
		// Token: 0x06000E0E RID: 3598 RVA: 0x00045D70 File Offset: 0x00043F70
		public override void WriteQualifiedName(string localName, string ns)
		{
			if (localName == null)
			{
				throw this.ArgumentError("localName");
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			if (ns == "http://www.w3.org/2000/xmlns/")
			{
				throw this.ArgumentError("Prefix 'xmlns' is reserved and cannot be overriden.");
			}
			if (!XmlChar.IsNCName(localName))
			{
				throw this.ArgumentError("localName must be a valid NCName.");
			}
			this.ShiftStateContent("QName", true);
			string text = (ns.Length <= 0) ? string.Empty : this.LookupPrefix(ns);
			if (text == null)
			{
				if (this.state != WriteState.Attribute)
				{
					throw this.ArgumentError(string.Format("Namespace '{0}' is not declared.", ns));
				}
				text = this.MockupPrefix(ns, false);
			}
			if (text != string.Empty)
			{
				this.writer.Write(text);
				this.writer.Write(":");
			}
			this.writer.Write(localName);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00045E64 File Offset: 0x00044064
		private void CheckChunkRange(Array buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || buffer.Length < index)
			{
				throw this.ArgumentOutOfRangeError("index");
			}
			if (count < 0 || buffer.Length < index + count)
			{
				throw this.ArgumentOutOfRangeError("count");
			}
		}

		/// <summary>Encodes the specified binary bytes as base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position within the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000E10 RID: 3600 RVA: 0x00045EC4 File Offset: 0x000440C4
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.CheckChunkRange(buffer, index, count);
			this.WriteString(Convert.ToBase64String(buffer, index, count));
		}

		/// <summary>Encodes the specified binary bytes as binhex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000E11 RID: 3601 RVA: 0x00045EE0 File Offset: 0x000440E0
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.CheckChunkRange(buffer, index, count);
			this.ShiftStateContent("BinHex", true);
			XmlConvert.WriteBinHex(buffer, index, count, this.writer);
		}

		/// <summary>Writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position in the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000E12 RID: 3602 RVA: 0x00045F10 File Offset: 0x00044110
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.CheckChunkRange(buffer, index, count);
			this.ShiftStateContent("Chars", true);
			this.WriteEscapedBuffer(buffer, index, count, this.state == WriteState.Attribute);
		}

		/// <summary>Writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position within the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06000E13 RID: 3603 RVA: 0x00045F44 File Offset: 0x00044144
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.CheckChunkRange(buffer, index, count);
			this.ShiftStateContent("Raw text", false);
			this.writer.Write(buffer, index, count);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00045F74 File Offset: 0x00044174
		private void WriteIndent()
		{
			this.WriteIndentCore(0, false);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00045F80 File Offset: 0x00044180
		private void WriteIndentEndElement()
		{
			this.WriteIndentCore(-1, false);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00045F8C File Offset: 0x0004418C
		private void WriteIndentAttribute()
		{
			if (!this.WriteIndentCore(0, true))
			{
				this.writer.Write(' ');
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00045FA8 File Offset: 0x000441A8
		private bool WriteIndentCore(int nestFix, bool attribute)
		{
			if (!this.indent)
			{
				return false;
			}
			for (int i = this.open_count - 1; i >= 0; i--)
			{
				if (!attribute && this.elements[i].HasSimple)
				{
					return false;
				}
			}
			if (this.state != WriteState.Start)
			{
				this.writer.Write(this.newline);
			}
			for (int j = 0; j < this.open_count + nestFix; j++)
			{
				this.writer.Write(this.indent_string);
			}
			return true;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0004603C File Offset: 0x0004423C
		private void OutputAutoStartDocument()
		{
			if (this.state != WriteState.Start)
			{
				return;
			}
			this.WriteStartDocumentCore(false, false);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00046054 File Offset: 0x00044254
		private void ShiftStateTopLevel(string occured, bool allowAttribute, bool dontCheckXmlDecl, bool isCharacter)
		{
			switch (this.state)
			{
			case WriteState.Start:
				if (isCharacter)
				{
					this.CheckMixedContentState();
				}
				if (this.xmldecl_state == XmlTextWriter.XmlDeclState.Auto && !dontCheckXmlDecl)
				{
					this.OutputAutoStartDocument();
				}
				this.state = WriteState.Prolog;
				return;
			case WriteState.Prolog:
				return;
			case WriteState.Element:
				if (isCharacter)
				{
					this.CheckMixedContentState();
				}
				this.CloseStartElement();
				return;
			case WriteState.Attribute:
				if (allowAttribute)
				{
					return;
				}
				break;
			case WriteState.Content:
				if (isCharacter)
				{
					this.CheckMixedContentState();
				}
				return;
			case WriteState.Closed:
			case WriteState.Error:
				break;
			default:
				return;
			}
			throw this.StateError(occured);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00046104 File Offset: 0x00044304
		private void CheckMixedContentState()
		{
			if (this.open_count > 0)
			{
				this.elements[this.open_count - 1].HasSimple = true;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00046128 File Offset: 0x00044328
		private void ShiftStateContent(string occured, bool allowAttribute)
		{
			switch (this.state)
			{
			case WriteState.Start:
			case WriteState.Prolog:
				if (this.allow_doc_fragment && !this.is_document_entity)
				{
					if (this.xmldecl_state == XmlTextWriter.XmlDeclState.Auto)
					{
						this.OutputAutoStartDocument();
					}
					this.CheckMixedContentState();
					this.state = WriteState.Content;
					return;
				}
				break;
			case WriteState.Element:
				this.CloseStartElement();
				this.CheckMixedContentState();
				return;
			case WriteState.Attribute:
				if (allowAttribute)
				{
					return;
				}
				break;
			case WriteState.Content:
				this.CheckMixedContentState();
				return;
			case WriteState.Closed:
			case WriteState.Error:
				break;
			default:
				return;
			}
			throw this.StateError(occured);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000461D8 File Offset: 0x000443D8
		private void WriteEscapedString(string text, bool isAttribute)
		{
			char[] anyOf = (!isAttribute) ? XmlTextWriter.escaped_text_chars : XmlTextWriter.escaped_attr_chars;
			int num = text.IndexOfAny(anyOf);
			if (num >= 0)
			{
				char[] array = text.ToCharArray();
				this.WriteCheckedBuffer(array, 0, num);
				this.WriteEscapedBuffer(array, num, array.Length - num, isAttribute);
			}
			else
			{
				this.WriteCheckedString(text);
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00046234 File Offset: 0x00044434
		private void WriteCheckedString(string s)
		{
			int num = XmlChar.IndexOfInvalid(s, true);
			if (num >= 0)
			{
				char[] array = s.ToCharArray();
				this.writer.Write(array, 0, num);
				this.WriteCheckedBuffer(array, num, array.Length - num);
			}
			else
			{
				this.writer.Write(s);
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00046284 File Offset: 0x00044484
		private void WriteCheckedBuffer(char[] text, int idx, int length)
		{
			int num = idx;
			int num2 = idx + length;
			while ((idx = XmlChar.IndexOfInvalid(text, num, length, true)) >= 0)
			{
				if (this.check_character_validity)
				{
					throw this.ArgumentError(string.Format("Input contains invalid character at {0} : &#x{1:X};", idx, (int)text[idx]));
				}
				if (num < idx)
				{
					this.writer.Write(text, num, idx - num);
				}
				this.writer.Write("&#x");
				TextWriter textWriter = this.writer;
				int num3 = (int)text[idx];
				textWriter.Write(num3.ToString("X", CultureInfo.InvariantCulture));
				this.writer.Write(';');
				length -= idx - num + 1;
				num = idx + 1;
			}
			if (num < num2)
			{
				this.writer.Write(text, num, num2 - num);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00046350 File Offset: 0x00044550
		private void WriteEscapedBuffer(char[] text, int index, int length, bool isAttribute)
		{
			int num = index;
			int num2 = index + length;
			int i = num;
			while (i < num2)
			{
				char c = text[i];
				switch (c)
				{
				case '"':
				case '\'':
					if (isAttribute && text[i] == this.quote_char)
					{
						goto IL_6A;
					}
					break;
				default:
				{
					switch (c)
					{
					case '\n':
						break;
					default:
						switch (c)
						{
						case '<':
						case '>':
							goto IL_6A;
						default:
							goto IL_22D;
						}
						break;
					case '\r':
						if (i + 1 < num2 && text[i] == '\n')
						{
							i++;
						}
						break;
					}
					if (num < i)
					{
						this.WriteCheckedBuffer(text, num, i - num);
					}
					if (isAttribute)
					{
						this.writer.Write((text[i] != '\r') ? "&#xA;" : "&#xD;");
						goto IL_229;
					}
					NewLineHandling newLineHandling = this.newline_handling;
					if (newLineHandling != NewLineHandling.Replace)
					{
						if (newLineHandling != NewLineHandling.Entitize)
						{
							this.writer.Write(text[i]);
						}
						else
						{
							this.writer.Write((text[i] != '\r') ? "&#xA;" : "&#xD;");
						}
					}
					else
					{
						this.writer.Write(this.newline);
					}
					goto IL_229;
				}
				case '&':
					goto IL_6A;
				}
				IL_22D:
				i++;
				continue;
				IL_229:
				num = i + 1;
				goto IL_22D;
				IL_6A:
				if (num < i)
				{
					this.WriteCheckedBuffer(text, num, i - num);
				}
				this.writer.Write('&');
				char c2 = text[i];
				switch (c2)
				{
				case '"':
					this.writer.Write("quot;");
					break;
				default:
					switch (c2)
					{
					case '<':
						this.writer.Write("lt;");
						break;
					case '>':
						this.writer.Write("gt;");
						break;
					}
					break;
				case '&':
					this.writer.Write("amp;");
					break;
				case '\'':
					this.writer.Write("apos;");
					break;
				}
				goto IL_229;
			}
			if (num < num2)
			{
				this.WriteCheckedBuffer(text, num, num2 - num);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000465A8 File Offset: 0x000447A8
		private Exception ArgumentOutOfRangeError(string name)
		{
			this.state = WriteState.Error;
			return new ArgumentOutOfRangeException(name);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000465B8 File Offset: 0x000447B8
		private Exception ArgumentError(string msg)
		{
			this.state = WriteState.Error;
			return new ArgumentException(msg);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000465C8 File Offset: 0x000447C8
		private Exception InvalidOperation(string msg)
		{
			this.state = WriteState.Error;
			return new InvalidOperationException(msg);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000465D8 File Offset: 0x000447D8
		private Exception StateError(string occured)
		{
			return this.InvalidOperation(string.Format("This XmlWriter does not accept {0} at this state {1}.", occured, this.state));
		}

		// Token: 0x04000630 RID: 1584
		private const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000631 RID: 1585
		private const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000632 RID: 1586
		private static readonly Encoding unmarked_utf8encoding = new UTF8Encoding(false, false);

		// Token: 0x04000633 RID: 1587
		private static char[] escaped_text_chars;

		// Token: 0x04000634 RID: 1588
		private static char[] escaped_attr_chars;

		// Token: 0x04000635 RID: 1589
		private Stream base_stream;

		// Token: 0x04000636 RID: 1590
		private TextWriter source;

		// Token: 0x04000637 RID: 1591
		private TextWriter writer;

		// Token: 0x04000638 RID: 1592
		private StringWriter preserver;

		// Token: 0x04000639 RID: 1593
		private string preserved_name;

		// Token: 0x0400063A RID: 1594
		private bool is_preserved_xmlns;

		// Token: 0x0400063B RID: 1595
		private bool allow_doc_fragment;

		// Token: 0x0400063C RID: 1596
		private bool close_output_stream;

		// Token: 0x0400063D RID: 1597
		private bool ignore_encoding;

		// Token: 0x0400063E RID: 1598
		private bool namespaces;

		// Token: 0x0400063F RID: 1599
		private XmlTextWriter.XmlDeclState xmldecl_state;

		// Token: 0x04000640 RID: 1600
		private bool check_character_validity;

		// Token: 0x04000641 RID: 1601
		private NewLineHandling newline_handling;

		// Token: 0x04000642 RID: 1602
		private bool is_document_entity;

		// Token: 0x04000643 RID: 1603
		private WriteState state;

		// Token: 0x04000644 RID: 1604
		private XmlNodeType node_state;

		// Token: 0x04000645 RID: 1605
		private XmlNamespaceManager nsmanager;

		// Token: 0x04000646 RID: 1606
		private int open_count;

		// Token: 0x04000647 RID: 1607
		private XmlTextWriter.XmlNodeInfo[] elements;

		// Token: 0x04000648 RID: 1608
		private Stack new_local_namespaces;

		// Token: 0x04000649 RID: 1609
		private ArrayList explicit_nsdecls;

		// Token: 0x0400064A RID: 1610
		private NamespaceHandling namespace_handling;

		// Token: 0x0400064B RID: 1611
		private bool indent;

		// Token: 0x0400064C RID: 1612
		private int indent_count;

		// Token: 0x0400064D RID: 1613
		private char indent_char;

		// Token: 0x0400064E RID: 1614
		private string indent_string;

		// Token: 0x0400064F RID: 1615
		private string newline;

		// Token: 0x04000650 RID: 1616
		private bool indent_attributes;

		// Token: 0x04000651 RID: 1617
		private char quote_char;

		// Token: 0x04000652 RID: 1618
		private bool v2;

		// Token: 0x0200012D RID: 301
		private class XmlNodeInfo
		{
			// Token: 0x04000655 RID: 1621
			public string Prefix;

			// Token: 0x04000656 RID: 1622
			public string LocalName;

			// Token: 0x04000657 RID: 1623
			public string NS;

			// Token: 0x04000658 RID: 1624
			public bool HasSimple;

			// Token: 0x04000659 RID: 1625
			public bool HasElements;

			// Token: 0x0400065A RID: 1626
			public string XmlLang;

			// Token: 0x0400065B RID: 1627
			public XmlSpace XmlSpace;
		}

		// Token: 0x0200012E RID: 302
		internal class StringUtil
		{
			// Token: 0x06000E27 RID: 3623 RVA: 0x00046624 File Offset: 0x00044824
			public static int IndexOf(string src, string target)
			{
				return XmlTextWriter.StringUtil.cmp.IndexOf(src, target);
			}

			// Token: 0x06000E28 RID: 3624 RVA: 0x00046634 File Offset: 0x00044834
			public static int Compare(string s1, string s2)
			{
				return XmlTextWriter.StringUtil.cmp.Compare(s1, s2);
			}

			// Token: 0x06000E29 RID: 3625 RVA: 0x00046644 File Offset: 0x00044844
			public static string Format(string format, params object[] args)
			{
				return string.Format(XmlTextWriter.StringUtil.cul, format, args);
			}

			// Token: 0x0400065C RID: 1628
			private static CultureInfo cul = CultureInfo.InvariantCulture;

			// Token: 0x0400065D RID: 1629
			private static CompareInfo cmp = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0200012F RID: 303
		private enum XmlDeclState
		{
			// Token: 0x0400065F RID: 1631
			Allow,
			// Token: 0x04000660 RID: 1632
			Ignore,
			// Token: 0x04000661 RID: 1633
			Auto,
			// Token: 0x04000662 RID: 1634
			Prohibit
		}
	}
}
