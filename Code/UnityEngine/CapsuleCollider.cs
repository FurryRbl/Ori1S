using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200013B RID: 315
	public sealed class CapsuleCollider : Collider
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00016340 File Offset: 0x00014540
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x00016358 File Offset: 0x00014558
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

		// Token: 0x0600143D RID: 5181
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x0600143E RID: 5182
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600143F RID: 5183
		// (set) Token: 0x06001440 RID: 5184
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001441 RID: 5185
		// (set) Token: 0x06001442 RID: 5186
		public extern float height { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001443 RID: 5187
		// (set) Token: 0x06001444 RID: 5188
		public extern int direction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
