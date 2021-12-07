using System;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlReader" /> object created by the <see cref="Overload:System.Xml.XmlReader.Create" /> method. </summary>
	// Token: 0x02000119 RID: 281
	public sealed class XmlReaderSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
		// Token: 0x06000BDD RID: 3037 RVA: 0x0003C96C File Offset: 0x0003AB6C
		public XmlReaderSettings()
		{
			this.Reset();
		}

		/// <summary>Occurs when the reader encounters validation errors.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000BDE RID: 3038 RVA: 0x0003C97C File Offset: 0x0003AB7C
		// (remove) Token: 0x06000BDF RID: 3039 RVA: 0x0003C998 File Offset: 0x0003AB98
		public event ValidationEventHandler ValidationEventHandler;

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlReaderSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlReaderSettings" /> object.</returns>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003C9B4 File Offset: 0x0003ABB4
		public XmlReaderSettings Clone()
		{
			return (XmlReaderSettings)base.MemberwiseClone();
		}

		/// <summary>Resets the members of the settings class to their default values.</summary>
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003C9C4 File Offset: 0x0003ABC4
		public void Reset()
		{
			this.checkCharacters = true;
			this.closeInput = false;
			this.conformance = ConformanceLevel.Document;
			this.ignoreComments = false;
			this.ignoreProcessingInstructions = false;
			this.ignoreWhitespace = false;
			this.lineNumberOffset = 0;
			this.linePositionOffset = 0;
			this.prohibitDtd = true;
			this.schemas = null;
			this.schemasNeedsInitialization = true;
			this.validationFlags = (XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes);
			this.validationType = ValidationType.None;
			this.xmlResolver = new XmlUrlResolver();
		}

		/// <summary>Gets or sets a value indicating whether to do character checking.</summary>
		/// <returns>true to do character checking; otherwise false. The default is true.Note:If the <see cref="T:System.Xml.XmlReader" /> is processing text data, it always checks that the XML names and text content are valid, regardless of the property setting. Setting <see cref="P:System.Xml.XmlReaderSettings.CheckCharacters" /> to false turns off character checking for character entity references.</returns>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003CA38 File Offset: 0x0003AC38
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0003CA40 File Offset: 0x0003AC40
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

		/// <summary>Gets or sets a value indicating whether the underlying stream or <see cref="T:System.IO.TextReader" /> should be closed when the reader is closed.</summary>
		/// <returns>true to close the underlying stream or <see cref="T:System.IO.TextReader" /> when the reader is closed; otherwise false. The default is false.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0003CA4C File Offset: 0x0003AC4C
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0003CA54 File Offset: 0x0003AC54
		public bool CloseInput
		{
			get
			{
				return this.closeInput;
			}
			set
			{
				this.closeInput = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlReader" /> will comply.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ConformanceLevel" /> values. The default is ConformanceLevel.Document.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0003CA60 File Offset: 0x0003AC60
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0003CA68 File Offset: 0x0003AC68
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

		/// <summary>Gets or sets a value indicating whether to ignore comments.</summary>
		/// <returns>true to ignore comments; otherwise false. The default is false.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0003CA74 File Offset: 0x0003AC74
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		public bool IgnoreComments
		{
			get
			{
				return this.ignoreComments;
			}
			set
			{
				this.ignoreComments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore processing instructions.</summary>
		/// <returns>true to ignore processing instructions; otherwise false. The default is false.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0003CA88 File Offset: 0x0003AC88
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0003CA90 File Offset: 0x0003AC90
		public bool IgnoreProcessingInstructions
		{
			get
			{
				return this.ignoreProcessingInstructions;
			}
			set
			{
				this.ignoreProcessingInstructions = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore insignificant white space.</summary>
		/// <returns>true to ignore white space; otherwise false. The default is false.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0003CA9C File Offset: 0x0003AC9C
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0003CAA4 File Offset: 0x0003ACA4
		public bool IgnoreWhitespace
		{
			get
			{
				return this.ignoreWhitespace;
			}
			set
			{
				this.ignoreWhitespace = value;
			}
		}

		/// <summary>Gets or sets line number offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line number offset. The default is 0.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003CAB0 File Offset: 0x0003ACB0
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0003CAB8 File Offset: 0x0003ACB8
		public int LineNumberOffset
		{
			get
			{
				return this.lineNumberOffset;
			}
			set
			{
				this.lineNumberOffset = value;
			}
		}

		/// <summary>Gets or sets line position offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line number offset. The default is 0.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0003CACC File Offset: 0x0003ACCC
		public int LinePositionOffset
		{
			get
			{
				return this.linePositionOffset;
			}
			set
			{
				this.linePositionOffset = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to prohibit document type definition (DTD) processing.</summary>
		/// <returns>true to prohibit DTD processing; otherwise false. The default is true.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0003CAD8 File Offset: 0x0003ACD8
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0003CAE0 File Offset: 0x0003ACE0
		public bool ProhibitDtd
		{
			get
			{
				return this.prohibitDtd;
			}
			set
			{
				this.prohibitDtd = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNameTable" /> used for atomized string comparisons.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> that stores all the atomized strings used by all <see cref="T:System.Xml.XmlReader" /> instances created using this <see cref="T:System.Xml.XmlReaderSettings" /> object.The default is null. The created <see cref="T:System.Xml.XmlReader" /> instance will use a new empty <see cref="T:System.Xml.NameTable" /> if this value is null.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0003CAF4 File Offset: 0x0003ACF4
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

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use. The default is an empty <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0003CB00 File Offset: 0x0003AD00
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0003CB28 File Offset: 0x0003AD28
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemasNeedsInitialization)
				{
					this.schemas = new XmlSchemaSet();
					this.schemasNeedsInitialization = false;
				}
				return this.schemas;
			}
			set
			{
				this.schemas = value;
				this.schemasNeedsInitialization = false;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003CB38 File Offset: 0x0003AD38
		internal void OnValidationError(object o, ValidationEventArgs e)
		{
			if (this.ValidationEventHandler != null)
			{
				this.ValidationEventHandler(o, e);
			}
			else if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		internal void SetSchemas(XmlSchemaSet schemas)
		{
			this.schemas = schemas;
		}

		/// <summary>Gets or sets a value indicating the schema validation settings. This setting applies to schema validating <see cref="T:System.Xml.XmlReader" /> objects (<see cref="P:System.Xml.XmlReaderSettings.ValidationType" /> property set to ValidationType.Schema).</summary>
		/// <returns>A set of <see cref="T:System.Xml.Schema.XmlSchemaValidationFlags" /> values. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessIdentityConstraints" /> and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.AllowXmlAttributes" /> are enabled by default. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessInlineSchema" />, <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation" />, and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings" /> are disabled by default.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0003CB78 File Offset: 0x0003AD78
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0003CB80 File Offset: 0x0003AD80
		public XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
			set
			{
				this.validationFlags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> will perform validation or type assignment when reading.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ValidationType" /> values. The default is ValidationType.None.</returns>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0003CB8C File Offset: 0x0003AD8C
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x0003CB94 File Offset: 0x0003AD94
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				this.validationType = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to access external documents.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> used to access external documents. If set to null, an <see cref="T:System.Xml.XmlException" /> is thrown when the <see cref="T:System.Xml.XmlReader" /> tries to access an external resource. The default is a new <see cref="T:System.Xml.XmlUrlResolver" /> with no credentials.</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0003CBA0 File Offset: 0x0003ADA0
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x0003CBA8 File Offset: 0x0003ADA8
		public XmlResolver XmlResolver
		{
			internal get
			{
				return this.xmlResolver;
			}
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x04000597 RID: 1431
		private bool checkCharacters;

		// Token: 0x04000598 RID: 1432
		private bool closeInput;

		// Token: 0x04000599 RID: 1433
		private ConformanceLevel conformance;

		// Token: 0x0400059A RID: 1434
		private bool ignoreComments;

		// Token: 0x0400059B RID: 1435
		private bool ignoreProcessingInstructions;

		// Token: 0x0400059C RID: 1436
		private bool ignoreWhitespace;

		// Token: 0x0400059D RID: 1437
		private int lineNumberOffset;

		// Token: 0x0400059E RID: 1438
		private int linePositionOffset;

		// Token: 0x0400059F RID: 1439
		private bool prohibitDtd;

		// Token: 0x040005A0 RID: 1440
		private XmlNameTable nameTable;

		// Token: 0x040005A1 RID: 1441
		private XmlSchemaSet schemas;

		// Token: 0x040005A2 RID: 1442
		private bool schemasNeedsInitialization;

		// Token: 0x040005A3 RID: 1443
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x040005A4 RID: 1444
		private ValidationType validationType;

		// Token: 0x040005A5 RID: 1445
		private XmlResolver xmlResolver;
	}
}
