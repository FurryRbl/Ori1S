using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	public sealed class MeshFilter : Component
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000140 RID: 320
		// (set) Token: 0x06000141 RID: 321
		public extern Mesh mesh { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000142 RID: 322
		// (set) Token: 0x06000143 RID: 323
		public extern Mesh sharedMesh { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
