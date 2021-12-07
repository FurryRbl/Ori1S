using System;

namespace System
{
	/// <summary>Specifies the behavior for a forced garbage collection.</summary>
	// Token: 0x0200013B RID: 315
	[Serializable]
	public enum GCCollectionMode
	{
		/// <summary>The default setting for this enumeration, which is currently <see cref="F:System.GCCollectionMode.Forced" />. </summary>
		// Token: 0x04000501 RID: 1281
		Default,
		/// <summary>Forces the garbage collection to occur immediately.</summary>
		// Token: 0x04000502 RID: 1282
		Forced,
		/// <summary>Allows the garbage collector to determine whether the current time is optimal to reclaim objects. </summary>
		// Token: 0x04000503 RID: 1283
		Optimized
	}
}
