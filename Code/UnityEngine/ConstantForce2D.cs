using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000164 RID: 356
	public sealed class ConstantForce2D : PhysicsUpdateBehaviour2D
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00018020 File Offset: 0x00016220
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x00018038 File Offset: 0x00016238
		public Vector2 force
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_force(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_force(ref value);
			}
		}

		// Token: 0x060016E8 RID: 5864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_force(out Vector2 value);

		// Token: 0x060016E9 RID: 5865
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_force(ref Vector2 value);

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00018044 File Offset: 0x00016244
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x0001805C File Offset: 0x0001625C
		public Vector2 relativeForce
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_relativeForce(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_relativeForce(ref value);
			}
		}

		// Token: 0x060016EC RID: 5868
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_relativeForce(out Vector2 value);

		// Token: 0x060016ED RID: 5869
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_relativeForce(ref Vector2 value);

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060016EE RID: 5870
		// (set) Token: 0x060016EF RID: 5871
		public extern float torque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
