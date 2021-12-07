using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a property. These attribute values are defined in corhdr.h.</summary>
	// Token: 0x020002B5 RID: 693
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum PropertyAttributes
	{
		/// <summary>Specifies that no attributes are associated with a property.</summary>
		// Token: 0x04000D2D RID: 3373
		None = 0,
		/// <summary>Specifies that the property is special, with the name describing how the property is special.</summary>
		// Token: 0x04000D2E RID: 3374
		SpecialName = 512,
		/// <summary>Specifies a flag reserved for runtime use only.</summary>
		// Token: 0x04000D2F RID: 3375
		ReservedMask = 62464,
		/// <summary>Specifies that the metadata internal APIs check the name encoding.</summary>
		// Token: 0x04000D30 RID: 3376
		RTSpecialName = 1024,
		/// <summary>Specifies that the property has a default value.</summary>
		// Token: 0x04000D31 RID: 3377
		HasDefault = 4096,
		/// <summary>Reserved.</summary>
		// Token: 0x04000D32 RID: 3378
		Reserved2 = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04000D33 RID: 3379
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04000D34 RID: 3380
		Reserved4 = 32768
	}
}
