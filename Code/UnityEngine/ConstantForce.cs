using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000135 RID: 309
	public sealed class ConstantForce : Behaviour
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000161A4 File Offset: 0x000143A4
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x000161BC File Offset: 0x000143BC
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

		// Token: 0x06001400 RID: 5120
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_force(out Vector3 value);

		// Token: 0x06001401 RID: 5121
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_force(ref Vector3 value);

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x000161C8 File Offset: 0x000143C8
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x000161E0 File Offset: 0x000143E0
		public Vector3 relativeForce
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_relativeForce(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_relativeForce(ref value);
			}
		}

		// Token: 0x06001404 RID: 5124
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_relativeForce(out Vector3 value);

		// Token: 0x06001405 RID: 5125
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_relativeForce(ref Vector3 value);

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000161EC File Offset: 0x000143EC
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x00016204 File Offset: 0x00014404
		public Vector3 torque
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_torque(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_torque(ref value);
			}
		}

		// Token: 0x06001408 RID: 5128
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_torque(out Vector3 value);

		// Token: 0x06001409 RID: 5129
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_torque(ref Vector3 value);

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00016210 File Offset: 0x00014410
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x00016228 File Offset: 0x00014428
		public Vector3 relativeTorque
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_relativeTorque(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_relativeTorque(ref value);
			}
		}

		// Token: 0x0600140C RID: 5132
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_relativeTorque(out Vector3 value);

		// Token: 0x0600140D RID: 5133
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_relativeTorque(ref Vector3 value);
	}
}
