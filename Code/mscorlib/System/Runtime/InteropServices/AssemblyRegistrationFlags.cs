using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines a set of flags used when registering assemblies.</summary>
	// Token: 0x0200036B RID: 875
	[ComVisible(true)]
	[Flags]
	public enum AssemblyRegistrationFlags
	{
		/// <summary>Indicates no special settings.</summary>
		// Token: 0x040010AE RID: 4270
		None = 0,
		/// <summary>Indicates that the code base key for the assembly should be set in the registry.</summary>
		// Token: 0x040010AF RID: 4271
		SetCodeBase = 1
	}
}
