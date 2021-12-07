using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a parameter. These are defined in CorHdr.h.</summary>
	// Token: 0x020002AF RID: 687
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ParameterAttributes
	{
		/// <summary>Specifies that there is no parameter attribute.</summary>
		// Token: 0x04000D0B RID: 3339
		None = 0,
		/// <summary>Specifies that the parameter is an input parameter.</summary>
		// Token: 0x04000D0C RID: 3340
		In = 1,
		/// <summary>Specifies that the parameter is an output parameter.</summary>
		// Token: 0x04000D0D RID: 3341
		Out = 2,
		/// <summary>Specifies that the parameter is a locale identifier (lcid).</summary>
		// Token: 0x04000D0E RID: 3342
		Lcid = 4,
		/// <summary>Specifies that the parameter is a return value.</summary>
		// Token: 0x04000D0F RID: 3343
		Retval = 8,
		/// <summary>Specifies that the parameter is optional.</summary>
		// Token: 0x04000D10 RID: 3344
		Optional = 16,
		/// <summary>Specifies that the parameter is reserved.</summary>
		// Token: 0x04000D11 RID: 3345
		ReservedMask = 61440,
		/// <summary>Specifies that the parameter has a default value.</summary>
		// Token: 0x04000D12 RID: 3346
		HasDefault = 4096,
		/// <summary>Specifies that the parameter has field marshaling information.</summary>
		// Token: 0x04000D13 RID: 3347
		HasFieldMarshal = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04000D14 RID: 3348
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04000D15 RID: 3349
		Reserved4 = 32768
	}
}
