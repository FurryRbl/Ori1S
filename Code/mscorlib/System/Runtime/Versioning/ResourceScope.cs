using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the scope of a sharable resource.</summary>
	// Token: 0x0200052F RID: 1327
	[Flags]
	public enum ResourceScope
	{
		/// <summary>There is no shared state.</summary>
		// Token: 0x0400160E RID: 5646
		None = 0,
		/// <summary>The state is shared by objects within the machine.</summary>
		// Token: 0x0400160F RID: 5647
		Machine = 1,
		/// <summary>The state is shared within a process.</summary>
		// Token: 0x04001610 RID: 5648
		Process = 2,
		/// <summary>The state is shared by objects within an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x04001611 RID: 5649
		AppDomain = 4,
		/// <summary>The state is shared by objects within a library.</summary>
		// Token: 0x04001612 RID: 5650
		Library = 8,
		/// <summary>The resource is visible to only the type.</summary>
		// Token: 0x04001613 RID: 5651
		Private = 16,
		/// <summary>The resource is visible at an assembly scope.</summary>
		// Token: 0x04001614 RID: 5652
		Assembly = 32
	}
}
