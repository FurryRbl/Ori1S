using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000139 RID: 313
	public sealed class SphereCollider : Collider
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00016304 File Offset: 0x00014504
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x0001631C File Offset: 0x0001451C
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_center(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x0600142F RID: 5167
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001430 RID: 5168
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001431 RID: 5169
		// (set) Token: 0x06001432 RID: 5170
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
