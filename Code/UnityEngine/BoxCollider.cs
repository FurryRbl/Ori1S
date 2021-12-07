using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000138 RID: 312
	public sealed class BoxCollider : Collider
	{
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0001628C File Offset: 0x0001448C
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x000162A4 File Offset: 0x000144A4
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

		// Token: 0x06001424 RID: 5156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001425 RID: 5157
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000162B0 File Offset: 0x000144B0
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x000162C8 File Offset: 0x000144C8
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_size(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x06001428 RID: 5160
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x06001429 RID: 5161
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000162D4 File Offset: 0x000144D4
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x000162E8 File Offset: 0x000144E8
		[Obsolete("use BoxCollider.size instead.")]
		public Vector3 extents
		{
			get
			{
				return this.size * 0.5f;
			}
			set
			{
				this.size = value * 2f;
			}
		}
	}
}
