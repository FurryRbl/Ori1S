using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the platform targeted by an executable.</summary>
	// Token: 0x02000292 RID: 658
	[ComVisible(true)]
	[Serializable]
	public enum ImageFileMachine
	{
		/// <summary>Targets a 32-bit Intel processor.</summary>
		// Token: 0x04000C77 RID: 3191
		I386 = 332,
		/// <summary>Targets a 64-bit Intel processor.</summary>
		// Token: 0x04000C78 RID: 3192
		IA64 = 512,
		/// <summary>Targets a 64-bit AMD processor.</summary>
		// Token: 0x04000C79 RID: 3193
		AMD64 = 34404
	}
}
