using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015E RID: 350
	public sealed class SliderJoint2D : AnchoredJoint2D
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060016A1 RID: 5793
		// (set) Token: 0x060016A2 RID: 5794
		public extern bool autoConfigureAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060016A3 RID: 5795
		// (set) Token: 0x060016A4 RID: 5796
		public extern float angle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060016A5 RID: 5797
		// (set) Token: 0x060016A6 RID: 5798
		public extern bool useMotor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060016A7 RID: 5799
		// (set) Token: 0x060016A8 RID: 5800
		public extern bool useLimits { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00017EE8 File Offset: 0x000160E8
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x00017F00 File Offset: 0x00016100
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

		// Token: 0x060016AB RID: 5803
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor2D value);

		// Token: 0x060016AC RID: 5804
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor2D value);

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00017F0C File Offset: 0x0001610C
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x00017F24 File Offset: 0x00016124
		public JointTranslationLimits2D limits
		{
			get
			{
				JointTranslationLimits2D result;
				this.INTERNAL_get_limits(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_limits(ref value);
			}
		}

		// Token: 0x060016AF RID: 5807
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_limits(out JointTranslationLimits2D value);

		// Token: 0x060016B0 RID: 5808
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_limits(ref JointTranslationLimits2D value);

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060016B1 RID: 5809
		public extern JointLimitState2D limitState { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060016B2 RID: 5810
		public extern float referenceAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060016B3 RID: 5811
		public extern float jointTranslation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060016B4 RID: 5812
		public extern float jointSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060016B5 RID: 5813 RVA: 0x00017F30 File Offset: 0x00016130
		public float GetMotorForce(float timeStep)
		{
			return SliderJoint2D.INTERNAL_CALL_GetMotorForce(this, timeStep);
		}

		// Token: 0x060016B6 RID: 5814
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetMotorForce(SliderJoint2D self, float timeStep);
	}
}
