using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000264 RID: 612
	[XmlType("select")]
	internal class Select
	{
		// Token: 0x04000A5B RID: 2651
		[XmlElement("typeName")]
		public string TypeName;

		// Token: 0x04000A5C RID: 2652
		[XmlElement("typeAttribute")]
		public string[] TypeAttributes;

		// Token: 0x04000A5D RID: 2653
		[XmlElement("typeMember")]
		public string TypeMember;
	}
}
