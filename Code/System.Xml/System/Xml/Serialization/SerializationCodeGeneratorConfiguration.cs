using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000261 RID: 609
	[XmlType("configuration")]
	internal class SerializationCodeGeneratorConfiguration
	{
		// Token: 0x04000A46 RID: 2630
		[XmlElement("serializer")]
		public SerializerInfo[] Serializers;
	}
}
