using System;

namespace System.Xml.Schema
{
	/// <summary>Infers an XML Schema Definition Language (XSD) schema from an XML document. The <see cref="T:System.Xml.Schema.XmlSchemaInference" /> class cannot be inherited.</summary>
	// Token: 0x0200021E RID: 542
	[MonoTODO]
	public sealed class XmlSchemaInference
	{
		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaInference.InferenceOption" /> value that affects schema occurrence declarations inferred from the XML document.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaInference.InferenceOption" /> object.</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0006239C File Offset: 0x0006059C
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x000623A4 File Offset: 0x000605A4
		public XmlSchemaInference.InferenceOption Occurrence
		{
			get
			{
				return this.occurrence;
			}
			set
			{
				this.occurrence = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaInference.InferenceOption" /> value that affects types inferred from the XML document.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaInference.InferenceOption" /> object.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x000623B0 File Offset: 0x000605B0
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x000623B8 File Offset: 0x000605B8
		public XmlSchemaInference.InferenceOption TypeInference
		{
			get
			{
				return this.typeInference;
			}
			set
			{
				this.typeInference = value;
			}
		}

		/// <summary>Infers an XML Schema Definition Language (XSD) schema from the XML document contained in the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the inferred schemas.</returns>
		/// <param name="instanceDocument">An <see cref="T:System.Xml.XmlReader" /> object containing the XML document to infer a schema from.</param>
		/// <exception cref="T:System.Xml.XmlException">The XML document is not well-formed.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaInferenceException">The <see cref="T:System.Xml.XmlReader" /> object is not positioned on the root node or on an element. An error occurs during the schema inference process.</exception>
		// Token: 0x0600159E RID: 5534 RVA: 0x000623C4 File Offset: 0x000605C4
		public XmlSchemaSet InferSchema(XmlReader xmlReader)
		{
			return this.InferSchema(xmlReader, new XmlSchemaSet());
		}

		/// <summary>Infers an XML Schema Definition Language (XSD) schema from the XML document contained in the <see cref="T:System.Xml.XmlReader" /> object specified, and refines the inferred schema using an existing schema in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object specified with the same target namespace.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the inferred schemas.</returns>
		/// <param name="instanceDocument">An <see cref="T:System.Xml.XmlReader" /> object containing the XML document to infer a schema from.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing an existing schema used to refine the inferred schema.</param>
		/// <exception cref="T:System.Xml.XmlException">The XML document is not well-formed.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaInferenceException">The <see cref="T:System.Xml.XmlReader" /> object is not positioned on the root node or on an element. An error occurs during the schema inference process.</exception>
		// Token: 0x0600159F RID: 5535 RVA: 0x000623D4 File Offset: 0x000605D4
		public XmlSchemaSet InferSchema(XmlReader xmlReader, XmlSchemaSet schemas)
		{
			return XsdInference.Process(xmlReader, schemas, this.occurrence == XmlSchemaInference.InferenceOption.Relaxed, this.typeInference == XmlSchemaInference.InferenceOption.Relaxed);
		}

		// Token: 0x040008A1 RID: 2209
		private XmlSchemaInference.InferenceOption occurrence;

		// Token: 0x040008A2 RID: 2210
		private XmlSchemaInference.InferenceOption typeInference;

		/// <summary>Affects occurrence and type information inferred by the <see cref="T:System.Xml.Schema.XmlSchemaInference" /> class for elements and attributes in an XML document. </summary>
		// Token: 0x0200021F RID: 543
		public enum InferenceOption
		{
			/// <summary>Indicates that a more restrictive schema declaration should be inferred for a particular element or attribute.</summary>
			// Token: 0x040008A4 RID: 2212
			Restricted,
			/// <summary>Indicates that a less restrictive schema declaration should be inferred for a particular element or attribute.</summary>
			// Token: 0x040008A5 RID: 2213
			Relaxed
		}
	}
}
