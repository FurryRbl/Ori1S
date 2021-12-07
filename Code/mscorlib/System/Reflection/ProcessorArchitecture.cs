using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
	// Token: 0x020002B4 RID: 692
	[ComVisible(true)]
	[Serializable]
	public enum ProcessorArchitecture
	{
		/// <summary>An unknown or unspecified combination of processor and bits-per-word.</summary>
		// Token: 0x04000D27 RID: 3367
		None,
		/// <summary>Neutral with respect to processor and bits-per-word.</summary>
		// Token: 0x04000D28 RID: 3368
		MSIL,
		/// <summary>A 32-bit Intel processor, either native or in the Windows on Windows (WOW) environment on a 64-bit platform.</summary>
		// Token: 0x04000D29 RID: 3369
		X86,
		/// <summary>A 64-bit Intel processor only.</summary>
		// Token: 0x04000D2A RID: 3370
		IA64,
		/// <summary>A 64-bit AMD processor only.</summary>
		// Token: 0x04000D2B RID: 3371
		Amd64
	}
}
