using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000114 RID: 276
	public sealed class ParticleAnimator : Component
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001177 RID: 4471
		// (set) Token: 0x06001178 RID: 4472
		public extern bool doesAnimateColor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00014320 File Offset: 0x00012520
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x00014338 File Offset: 0x00012538
		public Vector3 worldRotationAxis
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_worldRotationAxis(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_worldRotationAxis(ref value);
			}
		}

		// Token: 0x0600117B RID: 4475
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldRotationAxis(out Vector3 value);

		// Token: 0x0600117C RID: 4476
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_worldRotationAxis(ref Vector3 value);

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00014344 File Offset: 0x00012544
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0001435C File Offset: 0x0001255C
		public Vector3 localRotationAxis
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localRotationAxis(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localRotationAxis(ref value);
			}
		}

		// Token: 0x0600117F RID: 4479
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localRotationAxis(out Vector3 value);

		// Token: 0x06001180 RID: 4480
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localRotationAxis(ref Vector3 value);

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001181 RID: 4481
		// (set) Token: 0x06001182 RID: 4482
		public extern float sizeGrow { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00014368 File Offset: 0x00012568
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00014380 File Offset: 0x00012580
		public Vector3 rndForce
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_rndForce(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rndForce(ref value);
			}
		}

		// Token: 0x06001185 RID: 4485
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rndForce(out Vector3 value);

		// Token: 0x06001186 RID: 4486
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rndForce(ref Vector3 value);

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0001438C File Offset: 0x0001258C
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x000143A4 File Offset: 0x000125A4
		public Vector3 force
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_force(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_force(ref value);
			}
		}

		// Token: 0x06001189 RID: 4489
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_force(out Vector3 value);

		// Token: 0x0600118A RID: 4490
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_force(ref Vector3 value);

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600118B RID: 4491
		// (set) Token: 0x0600118C RID: 4492
		public extern float damping { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600118D RID: 4493
		// (set) Token: 0x0600118E RID: 4494
		public extern bool autodestruct { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600118F RID: 4495
		// (set) Token: 0x06001190 RID: 4496
		public extern Color[] colorAnimation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
