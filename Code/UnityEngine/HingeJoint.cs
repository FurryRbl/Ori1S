using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200012E RID: 302
	public sealed class HingeJoint : Joint
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00015D68 File Offset: 0x00013F68
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x00015D80 File Offset: 0x00013F80
		public JointMotor motor
		{
			get
			{
				JointMotor result;
				this.INTERNAL_get_motor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_motor(ref value);
			}
		}

		// Token: 0x06001357 RID: 4951
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor value);

		// Token: 0x06001358 RID: 4952
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor value);

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x00015D8C File Offset: 0x00013F8C
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x00015DA4 File Offset: 0x00013FA4
		public JointLimits limits
		{
			get
			{
				JointLimits result;
				this.INTERNAL_get_limits(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_limits(ref value);
			}
		}

		// Token: 0x0600135B RID: 4955
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_limits(out JointLimits value);

		// Token: 0x0600135C RID: 4956
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_limits(ref JointLimits value);

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00015DB0 File Offset: 0x00013FB0
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x00015DC8 File Offset: 0x00013FC8
		public JointSpring spring
		{
			get
			{
				JointSpring result;
				this.INTERNAL_get_spring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_spring(ref value);
			}
		}

		// Token: 0x0600135F RID: 4959
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_spring(out JointSpring value);

		// Token: 0x06001360 RID: 4960
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_spring(ref JointSpring value);

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001361 RID: 4961
		// (set) Token: 0x06001362 RID: 4962
		public extern bool useMotor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001363 RID: 4963
		// (set) Token: 0x06001364 RID: 4964
		public extern bool useLimits { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001365 RID: 4965
		// (set) Token: 0x06001366 RID: 4966
		public extern bool useSpring { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001367 RID: 4967
		public extern float velocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001368 RID: 4968
		public extern float angle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
