using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015C RID: 348
	public sealed class HingeJoint2D : AnchoredJoint2D
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600167D RID: 5757
		// (set) Token: 0x0600167E RID: 5758
		public extern bool useMotor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600167F RID: 5759
		// (set) Token: 0x06001680 RID: 5760
		public extern bool useLimits { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00017E48 File Offset: 0x00016048
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x00017E60 File Offset: 0x00016060
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

		// Token: 0x06001683 RID: 5763
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor2D value);

		// Token: 0x06001684 RID: 5764
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor2D value);

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00017E6C File Offset: 0x0001606C
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x00017E84 File Offset: 0x00016084
		public JointAngleLimits2D limits
		{
			get
			{
				JointAngleLimits2D result;
				this.INTERNAL_get_limits(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_limits(ref value);
			}
		}

		// Token: 0x06001687 RID: 5767
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_limits(out JointAngleLimits2D value);

		// Token: 0x06001688 RID: 5768
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_limits(ref JointAngleLimits2D value);

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001689 RID: 5769
		public extern JointLimitState2D limitState { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600168A RID: 5770
		public extern float referenceAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600168B RID: 5771
		public extern float jointAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600168C RID: 5772
		public extern float jointSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600168D RID: 5773 RVA: 0x00017E90 File Offset: 0x00016090
		public float GetMotorTorque(float timeStep)
		{
			return HingeJoint2D.INTERNAL_CALL_GetMotorTorque(this, timeStep);
		}

		// Token: 0x0600168E RID: 5774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetMotorTorque(HingeJoint2D self, float timeStep);
	}
}
