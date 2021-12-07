using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides information about an <see cref="T:System.Reflection.Assembly" /> reference.</summary>
	// Token: 0x0200027D RID: 637
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyNameFlags
	{
		/// <summary>Specifies that no flags are in effect.</summary>
		// Token: 0x04000C1A RID: 3098
		None = 0,
		/// <summary>Specifies that a public key is formed from the full public key rather than the public key token.</summary>
		// Token: 0x04000C1B RID: 3099
		PublicKey = 1,
		/// <summary>Specifies that the assembly can be retargeted at runtime to an assembly from a different publisher. This method supports the .NET Framework infrastructure and is not intended to be used directly from your code. </summary>
		// Token: 0x04000C1C RID: 3100
		Retargetable = 256,
		/// <summary>Specifies that just-in-time (JIT) compiler optimization is disabled for the assembly. This is the exact opposite of the meaning that is suggested by the member name. </summary>
		// Token: 0x04000C1D RID: 3101
		EnableJITcompileOptimizer = 16384,
		/// <summary>Specifies that just-in-time (JIT) compiler tracking is enabled for the assembly.</summary>
		// Token: 0x04000C1E RID: 3102
		EnableJITcompileTracking = 32768
	}
}
