using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000161 RID: 353
	public sealed class WheelJoint2D : AnchoredJoint2D
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00017F9C File Offset: 0x0001619C
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x00017FB4 File Offset: 0x000161B4
		public JointSuspension2D suspension
		{
			get
			{
				JointSuspension2D result;
				this.INTERNAL_get_suspension(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_suspension(ref value);
			}
		}

		// Token: 0x060016D1 RID: 5841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_suspension(out JointSuspension2D value);

		// Token: 0x060016D2 RID: 5842
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_suspension(ref JointSuspension2D value);

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060016D3 RID: 5843
		// (set) Token: 0x060016D4 RID: 5844
		public extern bool useMotor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00017FC0 File Offset: 0x000161C0
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x00017FD8 File Offset: 0x000161D8
		public JointMotor2D motor
		{
			get
			{
				JointMotor2D result;
				this.INTERNAL_get_motor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_motor(ref value);
			}
		}

		// Token: 0x060016D7 RID: 5847
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor2D value);

		// Token: 0x060016D8 RID: 5848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor2D value);

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060016D9 RID: 5849
		public extern float jointTranslation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060016DA RID: 5850
		public extern float jointSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060016DB RID: 5851 RVA: 0x00017FE4 File Offset: 0x000161E4
		public float GetMotorTorque(float timeStep)
		{
			return WheelJoint2D.INTERNAL_CALL_GetMotorTorque(this, timeStep);
		}

		// Token: 0x060016DC RID: 5852
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetMotorTorque(WheelJoint2D self, float timeStep);
	}
}
