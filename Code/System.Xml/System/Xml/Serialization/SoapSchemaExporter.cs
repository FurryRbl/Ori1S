using System;

namespace System.Xml.Serialization
{
	/// <summary>Populates <see cref="T:System.Xml.Schema.XmlSchema" /> objects with XML Schema data type definitions for .NET Framework types that are serialized using SOAP encoding.</summary>
	// Token: 0x02000275 RID: 629
	public class SoapSchemaExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapSchemaExporter" /> class, which supplies the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which XML Schema element declarations are to be added.</summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which element declarations obtained from type mappings are to be added.</param>
		// Token: 0x0600195D RID: 6493 RVA: 0x000852B0 File Offset: 0x000834B0
		public SoapSchemaExporter(XmlSchemas schemas)
		{
			this._exporter = new XmlSchemaExporter(schemas, true);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for each of the element parts of a SOAP-encoded message definition.</summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x0600195E RID: 6494 RVA: 0x000852C8 File Offset: 0x000834C8
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			this._exporter.ExportMembersMapping(xmlMembersMapping, false);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for each of the element parts of a SOAP-encoded message definition.</summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		/// <param name="exportEnclosingType">true to export a type definition for the parent element of the WSDL parts; otherwise, false.</param>
		// Token: 0x0600195F RID: 6495 RVA: 0x000852D8 File Offset: 0x000834D8
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping, bool exportEnclosingType)
		{
			this._exporter.ExportMembersMapping(xmlMembersMapping, exportEnclosingType);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for a .NET Framework type.</summary>
		/// <param name="xmlTypeMapping">An internal mapping between a .NET Framework type and an XML Schema element.</param>
		// Token: 0x06001960 RID: 6496 RVA: 0x000852E8 File Offset: 0x000834E8
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			this._exporter.ExportTypeMapping(xmlTypeMapping);
		}

		// Token: 0x04000A86 RID: 2694
		private XmlSchemaExporter _exporter;
	}
}
