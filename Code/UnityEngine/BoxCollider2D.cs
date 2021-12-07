using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200014D RID: 333
	public sealed class BoxCollider2D : Collider2D
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00017B88 File Offset: 0x00015D88
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x00017BA0 File Offset: 0x00015DA0
		public Vector2 size
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_size(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x0600161B RID: 5659
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector2 value);

		// Token: 0x0600161C RID: 5660
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector2 value);
	}
}
