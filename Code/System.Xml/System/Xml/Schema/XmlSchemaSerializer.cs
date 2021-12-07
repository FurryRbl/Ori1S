using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020001F6 RID: 502
	internal class XmlSchemaSerializer : XmlSerializer
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x00055F30 File Offset: 0x00054130
		protected override void Serialize(object o, XmlSerializationWriter writer)
		{
			XmlSchemaSerializationWriter xmlSchemaSerializationWriter = writer as XmlSchemaSerializationWriter;
			xmlSchemaSerializationWriter.WriteRoot_XmlSchema((XmlSchema)o);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00055F50 File Offset: 0x00054150
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSchemaSerializationWriter();
		}
	}
}
