using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200013A RID: 314
	public sealed class MeshCollider : Collider
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001434 RID: 5172
		// (set) Token: 0x06001435 RID: 5173
		public extern Mesh sharedMesh { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001436 RID: 5174
		// (set) Token: 0x06001437 RID: 5175
		public extern bool convex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00016330 File Offset: 0x00014530
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x00016334 File Offset: 0x00014534
		[Obsolete("Configuring smooth sphere collisions is no longer needed. PhysX3 has a better behaviour in place.")]
		public bool smoothSphereCollisions
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
	}
}
