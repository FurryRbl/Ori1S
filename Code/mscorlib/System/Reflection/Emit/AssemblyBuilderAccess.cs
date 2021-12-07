using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the access modes for a dynamic assembly.</summary>
	// Token: 0x020002C5 RID: 709
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		/// <summary>Represents that the dynamic assembly can be executed, but not saved.</summary>
		// Token: 0x04000D9D RID: 3485
		Run = 1,
		/// <summary>Represents that the dynamic assembly can be saved, but not executed.</summary>
		// Token: 0x04000D9E RID: 3486
		Save = 2,
		/// <summary>Represents that the dynamic assembly can be executed and saved.</summary>
		// Token: 0x04000D9F RID: 3487
		RunAndSave = 3,
		/// <summary>Represents that the dynamic assembly is loaded into the reflection-only context, and cannot be executed.</summary>
		// Token: 0x04000DA0 RID: 3488
		ReflectionOnly = 6
	}
}
