using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000266 RID: 614
	[XmlType("hookType")]
	internal enum HookType
	{
		// Token: 0x04000A63 RID: 2659
		attributes,
		// Token: 0x04000A64 RID: 2660
		elements,
		// Token: 0x04000A65 RID: 2661
		unknownAttribute,
		// Token: 0x04000A66 RID: 2662
		unknownElement,
		// Token: 0x04000A67 RID: 2663
		member,
		// Token: 0x04000A68 RID: 2664
		type
	}
}
