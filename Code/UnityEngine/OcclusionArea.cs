using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	public sealed class OcclusionArea : Component
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000028D4 File Offset: 0x00000AD4
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

		// Token: 0x060000C2 RID: 194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x060000C3 RID: 195
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000028E0 File Offset: 0x00000AE0
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000028F8 File Offset: 0x00000AF8
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

		// Token: 0x060000C6 RID: 198
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x060000C7 RID: 199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);
	}
}
