using System;
using System.Text;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlWriter" /> object created by the <see cref="Overload:System.Xml.XmlWriter.Create" /> method.</summary>
	// Token: 0x0200012B RID: 299
	public sealed class XmlWriterSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWriterSettings" /> class.</summary>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x00044028 File Offset: 0x00042228
		public XmlWriterSettings()
		{
			this.Reset();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlWriterSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlWriterSettings" /> object.</returns>
		// Token: 0x06000DC3 RID: 3523 RVA: 0x00044038 File Offset: 0x00042238
		public XmlWriterSettings Clone()
		{
			return (XmlWriterSettings)base.MemberwiseClone();
		}

		/// <summary>Resets the members of the settings class to their default values.</summary>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00044048 File Offset: 0x00042248
		public void Reset()
		{
			this.checkCharacters = true;
			this.closeOutput = false;
			this.conformance = ConformanceLevel.Document;
			this.encoding = Encoding.UTF8;
			this.indent = false;
			this.indentChars = "  ";
			this.newLineChars = Environment.NewLine;
			this.newLineOnAttributes = false;
			this.newLineHandling = NewLineHandling.None;
			this.omitXmlDeclaration = false;
			this.outputMethod = XmlOutputMethod.AutoDetect;
		}

		/// <summary>Gets or sets a value indicating whether to do character checking.</summary>
		/// <returns>true to do character checking; otherwise false. The default is true.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x000440B0 File Offset: 0x000422B0
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x000440B8 File Offset: 0x000422B8
		public bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.checkCharacters = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlWriter" /> should also close the underlying stream or <see cref="T:System.IO.TextWriter" /> when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>true to also close the underlying stream or <see cref="T:System.IO.TextWriter" />; otherwise false. The default is false.</returns>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x000440C4 File Offset: 0x000422C4
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x000440CC File Offset: 0x000422CC
		public bool CloseOutput
		{
			get
			{
				return this.closeOutput;
			}
			set
			{
				this.closeOutput = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlWriter" /> complies with.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ConformanceLevel" /> values. The default is ConformanceLevel.Document.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000440D8 File Offset: 0x000422D8
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x000440E0 File Offset: 0x000422E0
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformance;
			}
			set
			{
				this.conformance = value;
			}
		}

		/// <summary>Gets or sets the type of text encoding to use.</summary>
		/// <returns>The text encoding to use. The default is Encoding.UTF-8.</returns>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000440EC File Offset: 0x000422EC
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x000440F4 File Offset: 0x000422F4
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

		/// <summary>Gets or sets a value indicating whether to indent elements.</summary>
		/// <returns>true to write individual elements on new lines and indent; otherwise false. The default is false.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00044100 File Offset: 0x00042300
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00044108 File Offset: 0x00042308
		public bool Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		/// <summary>Gets or sets the character string to use when indenting. This setting is used when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property is set to true.</summary>
		/// <returns>The character string to use when indenting. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is two spaces.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.IndentChars" /> is null. </exception>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00044114 File Offset: 0x00042314
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0004411C File Offset: 0x0004231C
		public string IndentChars
		{
			get
			{
				return this.indentChars;
			}
			set
			{
				this.indentChars = value;
			}
		}

		/// <summary>Gets or sets the character string to use for line breaks. </summary>
		/// <returns>The character string to use for line breaks. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is \r\n (carriage return, new line).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.NewLineChars" /> is null. </exception>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00044128 File Offset: 0x00042328
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x00044130 File Offset: 0x00042330
		public string NewLineChars
		{
			get
			{
				return this.newLineChars;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.newLineChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to write attributes on a new line.</summary>
		/// <returns>true to write attributes on individual lines; otherwise false. The default is false.Note:This setting has no effect when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property value is false.When <see cref="P:System.Xml.XmlWriterSettings.NewLineOnAttributes" /> is set to true, each attribute is pre-pended with a new line and one extra level of indentation.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0004414C File Offset: 0x0004234C
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x00044154 File Offset: 0x00042354
		public bool NewLineOnAttributes
		{
			get
			{
				return this.newLineOnAttributes;
			}
			set
			{
				this.newLineOnAttributes = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize line breaks in the output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.NewLineHandling" /> values. The default is <see cref="F:System.Xml.NewLineHandling.Replace" />.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00044160 File Offset: 0x00042360
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00044168 File Offset: 0x00042368
		public NewLineHandling NewLineHandling
		{
			get
			{
				return this.newLineHandling;
			}
			set
			{
				this.newLineHandling = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to write an XML declaration.</summary>
		/// <returns>true to omit the XML declaration; otherwise false. The default is false, an XML declaration is written.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00044174 File Offset: 0x00042374
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0004417C File Offset: 0x0004237C
		public bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDeclaration;
			}
			set
			{
				this.omitXmlDeclaration = value;
			}
		}

		/// <summary>Gets the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlOutputMethod" /> values. The default is <see cref="F:System.Xml.XmlOutputMethod.Xml" />.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00044188 File Offset: 0x00042388
		public XmlOutputMethod OutputMethod
		{
			get
			{
				return this.outputMethod;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00044190 File Offset: 0x00042390
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00044198 File Offset: 0x00042398
		internal NamespaceHandling NamespaceHandling { get; set; }

		// Token: 0x04000624 RID: 1572
		private bool checkCharacters;

		// Token: 0x04000625 RID: 1573
		private bool closeOutput;

		// Token: 0x04000626 RID: 1574
		private ConformanceLevel conformance;

		// Token: 0x04000627 RID: 1575
		private Encoding encoding;

		// Token: 0x04000628 RID: 1576
		private bool indent;

		// Token: 0x04000629 RID: 1577
		private string indentChars;

		// Token: 0x0400062A RID: 1578
		private string newLineChars;

		// Token: 0x0400062B RID: 1579
		private bool newLineOnAttributes;

		// Token: 0x0400062C RID: 1580
		private NewLineHandling newLineHandling;

		// Token: 0x0400062D RID: 1581
		private bool omitXmlDeclaration;

		// Token: 0x0400062E RID: 1582
		private XmlOutputMethod outputMethod;
	}
}
