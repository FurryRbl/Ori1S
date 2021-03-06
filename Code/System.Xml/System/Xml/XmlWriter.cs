using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only means of generating streams or files containing XML data.</summary>
	// Token: 0x0200012A RID: 298
	public abstract class XmlWriter : IDisposable
	{
		/// <summary>For a description of this member, see <see cref="M:System.IDisposable.Dispose" />.</summary>
		// Token: 0x06000D7B RID: 3451 RVA: 0x00043318 File Offset: 0x00041518
		void IDisposable.Dispose()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this writer instance. If this writer was not created using the <see cref="Overload:System.Xml.XmlWriter.Create" /> method, this property returns null.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00043324 File Offset: 0x00041524
		public virtual XmlWriterSettings Settings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = new XmlWriterSettings();
				}
				return this.settings;
			}
		}

		/// <summary>When overridden in a derived class, gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000D7D RID: 3453
		public abstract WriteState WriteState { get; }

		/// <summary>When overridden in a derived class, gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00043344 File Offset: 0x00041544
		public virtual string XmlLang
		{
			get
			{
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets an <see cref="T:System.Xml.XmlSpace" /> representing the current xml:space scope.</summary>
		/// <returns>An XmlSpace representing the current xml:space scope.Value Meaning NoneThis is the default if no xml:space scope exists. DefaultThe current scope is xml:space="default". PreserveThe current scope is xml:space="preserve". </returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00043348 File Offset: 0x00041548
		public virtual XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		/// <summary>When overridden in a derived class, closes this stream and the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">A call is made to write more output after Close has been called or the result of this call is an invalid XML document. </exception>
		// Token: 0x06000D80 RID: 3456
		public abstract void Close();

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified stream.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is null.</exception>
		// Token: 0x06000D81 RID: 3457 RVA: 0x0004334C File Offset: 0x0004154C
		public static XmlWriter Create(Stream stream)
		{
			return XmlWriter.Create(stream, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified filename.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName" /> must be a file system path.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> value is null.</exception>
		// Token: 0x06000D82 RID: 3458 RVA: 0x00043358 File Offset: 0x00041558
		public static XmlWriter Create(string file)
		{
			return XmlWriter.Create(file, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is null.</exception>
		// Token: 0x06000D83 RID: 3459 RVA: 0x00043364 File Offset: 0x00041564
		public static XmlWriter Create(TextWriter writer)
		{
			return XmlWriter.Create(writer, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> object that you want to use as the underlying writer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> value is null.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00043370 File Offset: 0x00041570
		public static XmlWriter Create(XmlWriter writer)
		{
			return XmlWriter.Create(writer, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Text.StringBuilder" /> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter" /> is appended to the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="builder" /> value is null.</exception>
		// Token: 0x06000D85 RID: 3461 RVA: 0x0004337C File Offset: 0x0004157C
		public static XmlWriter Create(StringBuilder builder)
		{
			return XmlWriter.Create(builder, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the stream and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is null.</exception>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00043388 File Offset: 0x00041588
		public static XmlWriter Create(Stream stream, XmlWriterSettings settings)
		{
			Encoding encoding = (settings == null) ? Encoding.UTF8 : settings.Encoding;
			return XmlWriter.Create(new StreamWriter(stream, encoding), settings);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the filename and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName" /> must be a file system path.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> value is null.</exception>
		// Token: 0x06000D87 RID: 3463 RVA: 0x000433BC File Offset: 0x000415BC
		public static XmlWriter Create(string file, XmlWriterSettings settings)
		{
			Encoding encoding = (settings == null) ? Encoding.UTF8 : settings.Encoding;
			return XmlWriter.CreateTextWriter(new StreamWriter(file, false, encoding), settings, true);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.Text.StringBuilder" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Text.StringBuilder" /> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter" /> is appended to the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="builder" /> value is null.</exception>
		// Token: 0x06000D88 RID: 3464 RVA: 0x000433F0 File Offset: 0x000415F0
		public static XmlWriter Create(StringBuilder builder, XmlWriterSettings settings)
		{
			return XmlWriter.Create(new StringWriter(builder), settings);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is null.</exception>
		// Token: 0x06000D89 RID: 3465 RVA: 0x00043400 File Offset: 0x00041600
		public static XmlWriter Create(TextWriter writer, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return XmlWriter.CreateTextWriter(writer, settings, settings.CloseOutput);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> object that you want to use as the underlying writer.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> value is null.</exception>
		// Token: 0x06000D8A RID: 3466 RVA: 0x0004341C File Offset: 0x0004161C
		public static XmlWriter Create(XmlWriter writer, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			writer.settings = settings;
			return writer;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00043434 File Offset: 0x00041634
		private static XmlWriter CreateTextWriter(TextWriter writer, XmlWriterSettings settings, bool closeOutput)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			XmlTextWriter writer2 = new XmlTextWriter(writer, settings, closeOutput);
			return XmlWriter.Create(writer2, settings);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Xml.XmlWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000D8C RID: 3468 RVA: 0x00043460 File Offset: 0x00041660
		protected virtual void Dispose(bool disposing)
		{
			this.Close();
		}

		/// <summary>When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		// Token: 0x06000D8D RID: 3469
		public abstract void Flush();

		/// <summary>When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <returns>The matching prefix or null if no matching namespace URI is found in the current scope.</returns>
		/// <param name="ns">The namespace URI whose prefix you want to find. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns" /> is either null or String.Empty. </exception>
		// Token: 0x06000D8E RID: 3470
		public abstract string LookupPrefix(string ns);

		// Token: 0x06000D8F RID: 3471 RVA: 0x00043468 File Offset: 0x00041668
		private void WriteAttribute(XmlReader reader, bool defattr)
		{
			if (!defattr && reader.IsDefault)
			{
				return;
			}
			this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
			while (reader.ReadAttributeValue())
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Text:
					this.WriteString(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					this.WriteEntityRef(reader.Name);
					break;
				}
			}
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader from which to copy the attributes. </param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		/// <exception cref="T:System.Xml.XmlException">The reader is not positioned on an element, attribute or XmlDeclaration node. </exception>
		// Token: 0x06000D90 RID: 3472 RVA: 0x000434F8 File Offset: 0x000416F8
		public virtual void WriteAttributes(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentException("null XmlReader specified.", "reader");
			}
			XmlNodeType nodeType = reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.XmlDeclaration)
					{
						throw new XmlException("NodeType is not one of Element, Attribute, nor XmlDeclaration.");
					}
					this.WriteAttributeString("version", reader["version"]);
					if (reader["encoding"] != null)
					{
						this.WriteAttributeString("encoding", reader["encoding"]);
					}
					if (reader["standalone"] != null)
					{
						this.WriteAttributeString("standalone", reader["standalone"]);
					}
					return;
				}
			}
			else if (!reader.MoveToFirstAttribute())
			{
				return;
			}
			do
			{
				this.WriteAttribute(reader, defattr);
			}
			while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified local name and value.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		// Token: 0x06000D91 RID: 3473 RVA: 0x000435E4 File Offset: 0x000417E4
		public void WriteAttributeString(string localName, string value)
		{
			this.WriteAttributeString(string.Empty, localName, null, value);
		}

		/// <summary>When overridden in a derived class, writes an attribute with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="ns">The namespace URI to associate with the attribute. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		// Token: 0x06000D92 RID: 3474 RVA: 0x000435F4 File Offset: 0x000417F4
		public void WriteAttributeString(string localName, string ns, string value)
		{
			this.WriteAttributeString(string.Empty, localName, ns, value);
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The namespace prefix of the attribute. </param>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="ns">The namespace URI of the attribute. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception>
		/// <exception cref="T:System.Xml.XmlException">The <paramref name="localName" /> or <paramref name="ns" /> is null. </exception>
		// Token: 0x06000D93 RID: 3475 RVA: 0x00043604 File Offset: 0x00041804
		public void WriteAttributeString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartAttribute(prefix, localName, ns);
			if (value != null && value.Length > 0)
			{
				this.WriteString(value);
			}
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06000D94 RID: 3476
		public abstract void WriteBase64(byte[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as BinHex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06000D95 RID: 3477 RVA: 0x00043634 File Offset: 0x00041834
		public virtual void WriteBinHex(byte[] buffer, int index, int count)
		{
			StringWriter stringWriter = new StringWriter();
			XmlConvert.WriteBinHex(buffer, index, count, stringWriter);
			this.WriteString(stringWriter.ToString());
		}

		/// <summary>When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">The text to place inside the CDATA block. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document. </exception>
		// Token: 0x06000D96 RID: 3478
		public abstract void WriteCData(string text);

		/// <summary>When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">The Unicode character for which to generate a character entity. </param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff. </exception>
		// Token: 0x06000D97 RID: 3479
		public abstract void WriteCharEntity(char ch);

		/// <summary>When overridden in a derived class, writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position in the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="buffer" /> parameter value is not valid.</exception>
		// Token: 0x06000D98 RID: 3480
		public abstract void WriteChars(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document. </exception>
		// Token: 0x06000D99 RID: 3481
		public abstract void WriteComment(string text);

		/// <summary>When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty. </param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments. </param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument. </param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument. </param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="name" /> would result in invalid XML. </exception>
		// Token: 0x06000D9A RID: 3482
		public abstract void WriteDocType(string name, string pubid, string sysid, string subset);

		/// <summary>When overridden in a derived class, writes an element with the specified local name and value.</summary>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="value">The value of the element. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x0004365C File Offset: 0x0004185C
		public void WriteElementString(string localName, string value)
		{
			this.WriteStartElement(localName);
			if (value != null && value.Length > 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>When overridden in a derived class, writes an element with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. </param>
		/// <param name="value">The value of the element. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000D9C RID: 3484 RVA: 0x00043690 File Offset: 0x00041890
		public void WriteElementString(string localName, string ns, string value)
		{
			this.WriteStartElement(localName, ns);
			if (value != null && value.Length > 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>Writes an element with the specified local name, namespace URI, and value.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is null or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000D9D RID: 3485 RVA: 0x000436BC File Offset: 0x000418BC
		public void WriteElementString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartElement(prefix, localName, ns);
			if (value != null && value.Length > 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		// Token: 0x06000D9E RID: 3486
		public abstract void WriteEndAttribute();

		/// <summary>When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid. </exception>
		// Token: 0x06000D9F RID: 3487
		public abstract void WriteEndDocument();

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">This results in an invalid XML document. </exception>
		// Token: 0x06000DA0 RID: 3488
		public abstract void WriteEndElement();

		/// <summary>When overridden in a derived class, writes out an entity reference as &amp;name;.</summary>
		/// <param name="name">The name of the entity reference. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000DA1 RID: 3489
		public abstract void WriteEntityRef(string name);

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000DA2 RID: 3490
		public abstract void WriteFullEndElement();

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x000436EC File Offset: 0x000418EC
		public virtual void WriteName(string name)
		{
			this.WriteNameInternal(name);
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either null or String.Empty. </exception>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x000436F8 File Offset: 0x000418F8
		public virtual void WriteNmToken(string name)
		{
			this.WriteNmTokenInternal(name);
		}

		/// <summary>When overridden in a derived class, writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write. </param>
		/// <param name="ns">The namespace URI for the name. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="localName" /> is either null or String.Empty.<paramref name="localName" /> is not a valid name. </exception>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00043704 File Offset: 0x00041904
		public virtual void WriteQualifiedName(string localName, string ns)
		{
			this.WriteQualifiedNameInternal(localName, ns);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00043710 File Offset: 0x00041910
		internal void WriteNameInternal(string name)
		{
			ConformanceLevel conformanceLevel = this.Settings.ConformanceLevel;
			if (conformanceLevel == ConformanceLevel.Fragment || conformanceLevel == ConformanceLevel.Document)
			{
				XmlConvert.VerifyName(name);
			}
			this.WriteString(name);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00043750 File Offset: 0x00041950
		internal virtual void WriteNmTokenInternal(string name)
		{
			bool flag = true;
			ConformanceLevel conformanceLevel = this.Settings.ConformanceLevel;
			if (conformanceLevel == ConformanceLevel.Fragment || conformanceLevel == ConformanceLevel.Document)
			{
				flag = XmlChar.IsNmToken(name);
			}
			if (!flag)
			{
				throw new ArgumentException("Argument name is not a valid NMTOKEN.");
			}
			this.WriteString(name);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000437A4 File Offset: 0x000419A4
		internal void WriteQualifiedNameInternal(string localName, string ns)
		{
			if (localName == null || localName == string.Empty)
			{
				throw new ArgumentException();
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			ConformanceLevel conformanceLevel = this.Settings.ConformanceLevel;
			if (conformanceLevel == ConformanceLevel.Fragment || conformanceLevel == ConformanceLevel.Document)
			{
				XmlConvert.VerifyNCName(localName);
			}
			string text = (ns.Length <= 0) ? string.Empty : this.LookupPrefix(ns);
			if (text == null)
			{
				throw new ArgumentException(string.Format("Namespace '{0}' is not declared.", ns));
			}
			if (text != string.Empty)
			{
				this.WriteString(text);
				this.WriteString(":");
				this.WriteString(localName);
			}
			else
			{
				this.WriteString(localName);
			}
		}

		/// <summary>Copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator" /> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> remains unchanged.</summary>
		/// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to copy from.</param>
		/// <param name="defattr">true to copy the default attributes; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="navigator" /> is null.</exception>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00043870 File Offset: 0x00041A70
		public virtual void WriteNode(XPathNavigator navigator, bool defattr)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			switch (navigator.NodeType)
			{
			case XPathNodeType.Root:
				if (navigator.MoveToFirstChild())
				{
					do
					{
						this.WriteNode(navigator, defattr);
					}
					while (navigator.MoveToNext());
					navigator.MoveToParent();
				}
				break;
			case XPathNodeType.Element:
				this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					do
					{
						if (defattr || navigator.SchemaInfo == null || navigator.SchemaInfo.IsDefault)
						{
							this.WriteAttributeString(navigator.Prefix, (!(navigator.LocalName == string.Empty)) ? navigator.LocalName : "xmlns", "http://www.w3.org/2000/xmlns/", navigator.Value);
						}
					}
					while (navigator.MoveToNextNamespace(XPathNamespaceScope.Local));
					navigator.MoveToParent();
				}
				if (navigator.MoveToFirstAttribute())
				{
					do
					{
						if (defattr || navigator.SchemaInfo == null || navigator.SchemaInfo.IsDefault)
						{
							this.WriteAttributeString(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI, navigator.Value);
						}
					}
					while (navigator.MoveToNextAttribute());
					navigator.MoveToParent();
				}
				if (navigator.MoveToFirstChild())
				{
					do
					{
						this.WriteNode(navigator, defattr);
					}
					while (navigator.MoveToNext());
					navigator.MoveToParent();
				}
				if (navigator.IsEmptyElement)
				{
					this.WriteEndElement();
				}
				else
				{
					this.WriteFullEndElement();
				}
				break;
			case XPathNodeType.Attribute:
				break;
			case XPathNodeType.Namespace:
				break;
			case XPathNodeType.Text:
				this.WriteString(navigator.Value);
				break;
			case XPathNodeType.SignificantWhitespace:
				this.WriteWhitespace(navigator.Value);
				break;
			case XPathNodeType.Whitespace:
				this.WriteWhitespace(navigator.Value);
				break;
			case XPathNodeType.ProcessingInstruction:
				this.WriteProcessingInstruction(navigator.Name, navigator.Value);
				break;
			case XPathNodeType.Comment:
				this.WriteComment(navigator.Value);
				break;
			default:
				throw new NotSupportedException();
			}
		}

		/// <summary>When overridden in a derived class, copies everything from the reader to the writer and moves the reader to the start of the next sibling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to read from. </param>
		/// <param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="reader" /> contains invalid characters. </exception>
		// Token: 0x06000DAA RID: 3498 RVA: 0x00043A90 File Offset: 0x00041C90
		public virtual void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentException();
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
				do
				{
					this.WriteNode(reader, defattr);
				}
				while (!reader.EOF);
				return;
			}
			switch (reader.NodeType)
			{
			case XmlNodeType.None:
				goto IL_218;
			case XmlNodeType.Element:
				this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
				if (reader.HasAttributes)
				{
					for (int i = 0; i < reader.AttributeCount; i++)
					{
						reader.MoveToAttribute(i);
						this.WriteAttribute(reader, defattr);
					}
					reader.MoveToElement();
				}
				if (reader.IsEmptyElement)
				{
					this.WriteEndElement();
				}
				else
				{
					int depth = reader.Depth;
					reader.Read();
					if (reader.NodeType != XmlNodeType.EndElement)
					{
						do
						{
							this.WriteNode(reader, defattr);
						}
						while (depth < reader.Depth);
					}
					this.WriteFullEndElement();
				}
				goto IL_218;
			case XmlNodeType.Attribute:
				return;
			case XmlNodeType.Text:
				this.WriteString(reader.Value);
				goto IL_218;
			case XmlNodeType.CDATA:
				this.WriteCData(reader.Value);
				goto IL_218;
			case XmlNodeType.EntityReference:
				this.WriteEntityRef(reader.Name);
				goto IL_218;
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				goto IL_1E0;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.XmlDeclaration:
				this.WriteProcessingInstruction(reader.Name, reader.Value);
				goto IL_218;
			case XmlNodeType.Comment:
				this.WriteComment(reader.Value);
				goto IL_218;
			case XmlNodeType.DocumentType:
				this.WriteDocType(reader.Name, reader["PUBLIC"], reader["SYSTEM"], reader.Value);
				goto IL_218;
			case XmlNodeType.Whitespace:
				break;
			case XmlNodeType.SignificantWhitespace:
				break;
			case XmlNodeType.EndElement:
				this.WriteFullEndElement();
				goto IL_218;
			case XmlNodeType.EndEntity:
				goto IL_218;
			default:
				goto IL_1E0;
			}
			this.WriteWhitespace(reader.Value);
			goto IL_218;
			IL_1E0:
			throw new XmlException(string.Concat(new object[]
			{
				"Unexpected node ",
				reader.Name,
				" of type ",
				reader.NodeType
			}));
			IL_218:
			reader.Read();
		}

		/// <summary>When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">The name of the processing instruction. </param>
		/// <param name="text">The text to include in the processing instruction. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name" /> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlWriter.WriteStartDocument" /> has already been called. </exception>
		// Token: 0x06000DAB RID: 3499
		public abstract void WriteProcessingInstruction(string name, string text);

		/// <summary>When overridden in a derived class, writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is either null or String.Empty. </exception>
		// Token: 0x06000DAC RID: 3500
		public abstract void WriteRaw(string data);

		/// <summary>When overridden in a derived class, writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position within the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06000DAD RID: 3501
		public abstract void WriteRaw(char[] buffer, int index, int count);

		/// <summary>Writes the start of an attribute with the specified local name.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000DAE RID: 3502 RVA: 0x00043CBC File Offset: 0x00041EBC
		public void WriteStartAttribute(string localName)
		{
			this.WriteStartAttribute(null, localName, null);
		}

		/// <summary>Writes the start of an attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="ns">The namespace URI of the attribute. </param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000DAF RID: 3503 RVA: 0x00043CC8 File Offset: 0x00041EC8
		public void WriteStartAttribute(string localName, string ns)
		{
			this.WriteStartAttribute(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix of the attribute. </param>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="ns">The namespace URI for the attribute. </param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000DB0 RID: 3504
		public abstract void WriteStartAttribute(string prefix, string localName, string ns);

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000DB1 RID: 3505
		public abstract void WriteStartDocument();

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no". </param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000DB2 RID: 3506
		public abstract void WriteStartDocument(bool standalone);

		/// <summary>When overridden in a derived class, writes out a start tag with the specified local name.</summary>
		/// <param name="localName">The local name of the element. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x00043CD4 File Offset: 0x00041ED4
		public void WriteStartElement(string localName)
		{
			this.WriteStartElement(null, localName, null);
		}

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace.</summary>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix, the writer automatically writes that prefix also. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00043CE0 File Offset: 0x00041EE0
		public void WriteStartElement(string localName, string ns)
		{
			this.WriteStartElement(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element. </param>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character may be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instruction, or CDATA sections. If a character not valid for the output encoding is encountered, an <see cref="T:System.Text.EncoderFallbackException" /> is thrown. </exception>
		// Token: 0x06000DB5 RID: 3509
		public abstract void WriteStartElement(string prefix, string localName, string ns);

		/// <summary>When overridden in a derived class, writes the given text content.</summary>
		/// <param name="text">The text to write. </param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair. </exception>
		// Token: 0x06000DB6 RID: 3510
		public abstract void WriteString(string text);

		/// <summary>When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF. </param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF. </param>
		/// <exception cref="T:System.ArgumentException">An invalid surrogate character pair was passed. </exception>
		// Token: 0x06000DB7 RID: 3511
		public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);

		/// <summary>When overridden in a derived class, writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters. </param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters. </exception>
		// Token: 0x06000DB8 RID: 3512
		public abstract void WriteWhitespace(string ws);

		/// <summary>Writes a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x00043CEC File Offset: 0x00041EEC
		public virtual void WriteValue(bool value)
		{
			this.WriteString(XQueryConvert.BooleanToString(value));
		}

		/// <summary>Writes a <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DBA RID: 3514 RVA: 0x00043CFC File Offset: 0x00041EFC
		public virtual void WriteValue(DateTime value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DBB RID: 3515 RVA: 0x00043D0C File Offset: 0x00041F0C
		public virtual void WriteValue(decimal value)
		{
			this.WriteString(XQueryConvert.DecimalToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Double" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DBC RID: 3516 RVA: 0x00043D1C File Offset: 0x00041F1C
		public virtual void WriteValue(double value)
		{
			this.WriteString(XQueryConvert.DoubleToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int32" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DBD RID: 3517 RVA: 0x00043D2C File Offset: 0x00041F2C
		public virtual void WriteValue(int value)
		{
			this.WriteString(XQueryConvert.IntToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int64" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DBE RID: 3518 RVA: 0x00043D3C File Offset: 0x00041F3C
		public virtual void WriteValue(long value)
		{
			this.WriteString(XQueryConvert.IntegerToString(value));
		}

		/// <summary>Writes the object value.</summary>
		/// <param name="value">The object value to write. Note   With the release of the .NET Framework 3.5, this method accepts <see cref="T:System.DateTimeOffset" /> as a parameter.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		// Token: 0x06000DBF RID: 3519 RVA: 0x00043D4C File Offset: 0x00041F4C
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is string)
			{
				this.WriteString((string)value);
			}
			else if (value is bool)
			{
				this.WriteValue((bool)value);
			}
			else if (value is byte)
			{
				this.WriteValue((int)value);
			}
			else if (value is byte[])
			{
				this.WriteBase64((byte[])value, 0, ((byte[])value).Length);
			}
			else if (value is char[])
			{
				this.WriteChars((char[])value, 0, ((char[])value).Length);
			}
			else if (value is DateTime)
			{
				this.WriteValue((DateTime)value);
			}
			else if (value is decimal)
			{
				this.WriteValue((decimal)value);
			}
			else if (value is double)
			{
				this.WriteValue((double)value);
			}
			else if (value is short)
			{
				this.WriteValue((int)value);
			}
			else if (value is int)
			{
				this.WriteValue((int)value);
			}
			else if (value is long)
			{
				this.WriteValue((long)value);
			}
			else if (value is float)
			{
				this.WriteValue((float)value);
			}
			else if (value is TimeSpan)
			{
				this.WriteString(XmlConvert.ToString((TimeSpan)value));
			}
			else if (value is XmlQualifiedName)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)value;
				if (!xmlQualifiedName.Equals(XmlQualifiedName.Empty))
				{
					if (xmlQualifiedName.Namespace.Length > 0 && this.LookupPrefix(xmlQualifiedName.Namespace) == null)
					{
						throw new InvalidCastException(string.Format("The QName '{0}' cannot be written. No corresponding prefix is declared", xmlQualifiedName));
					}
					this.WriteQualifiedName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
				}
				else
				{
					this.WriteString(string.Empty);
				}
			}
			else
			{
				if (!(value is IEnumerable))
				{
					throw new InvalidCastException(string.Format("Type '{0}' cannot be cast to string", value.GetType()));
				}
				bool flag = false;
				foreach (object value2 in ((IEnumerable)value))
				{
					if (flag)
					{
						this.WriteString(" ");
					}
					else
					{
						flag = true;
					}
					this.WriteValue(value2);
				}
			}
		}

		/// <summary>Writes a single-precision floating-point number.</summary>
		/// <param name="value">The single-precision floating-point number to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0004400C File Offset: 0x0004220C
		public virtual void WriteValue(float value)
		{
			this.WriteString(XQueryConvert.FloatToString(value));
		}

		/// <summary>Writes a <see cref="T:System.String" /> value.</summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0004401C File Offset: 0x0004221C
		public virtual void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x04000623 RID: 1571
		private XmlWriterSettings settings;
	}
}
