using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the resource location.</summary>
	// Token: 0x020002B9 RID: 697
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ResourceLocation
	{
		/// <summary>Specifies an embedded (that is, non-linked) resource.</summary>
		// Token: 0x04000D3B RID: 3387
		Embedded = 1,
		/// <summary>Specifies that the resource is contained in another assembly.</summary>
		// Token: 0x04000D3C RID: 3388
		ContainedInAnotherAssembly = 2,
		/// <summary>Specifies that the resource is contained in the manifest file.</summary>
		// Token: 0x04000D3D RID: 3389
		ContainedInManifestFile = 4
	}
}
