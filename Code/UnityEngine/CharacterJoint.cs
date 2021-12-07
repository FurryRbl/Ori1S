using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	public sealed class CharacterJoint : Joint
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00015DEC File Offset: 0x00013FEC
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00015E04 File Offset: 0x00014004
		public Vector3 swingAxis
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_swingAxis(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_swingAxis(ref value);
			}
		}

		// Token: 0x06001378 RID: 4984
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swingAxis(out Vector3 value);

		// Token: 0x06001379 RID: 4985
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swingAxis(ref Vector3 value);

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00015E10 File Offset: 0x00014010
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x00015E28 File Offset: 0x00014028
		public SoftJointLimitSpring twistLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.INTERNAL_get_twistLimitSpring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_twistLimitSpring(ref value);
			}
		}

		// Token: 0x0600137C RID: 4988
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_twistLimitSpring(out SoftJointLimitSpring value);

		// Token: 0x0600137D RID: 4989
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_twistLimitSpring(ref SoftJointLimitSpring value);

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00015E34 File Offset: 0x00014034
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x00015E4C File Offset: 0x0001404C
		public SoftJointLimitSpring swingLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.INTERNAL_get_swingLimitSpring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_swingLimitSpring(ref value);
			}
		}

		// Token: 0x06001380 RID: 4992
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swingLimitSpring(out SoftJointLimitSpring value);

		// Token: 0x06001381 RID: 4993
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swingLimitSpring(ref SoftJointLimitSpring value);

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00015E58 File Offset: 0x00014058
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00015E70 File Offset: 0x00014070
		public SoftJointLimit lowTwistLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_lowTwistLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_lowTwistLimit(ref value);
			}
		}

		// Token: 0x06001384 RID: 4996
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lowTwistLimit(out SoftJointLimit value);

		// Token: 0x06001385 RID: 4997
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lowTwistLimit(ref SoftJointLimit value);

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00015E7C File Offset: 0x0001407C
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x00015E94 File Offset: 0x00014094
		public SoftJointLimit highTwistLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_highTwistLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_highTwistLimit(ref value);
			}
		}

		// Token: 0x06001388 RID: 5000
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_highTwistLimit(out SoftJointLimit value);

		// Token: 0x06001389 RID: 5001
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_highTwistLimit(ref SoftJointLimit value);

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00015EA0 File Offset: 0x000140A0
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x00015EB8 File Offset: 0x000140B8
		public SoftJointLimit swing1Limit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_swing1Limit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_swing1Limit(ref value);
			}
		}

		// Token: 0x0600138C RID: 5004
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swing1Limit(out SoftJointLimit value);

		// Token: 0x0600138D RID: 5005
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swing1Limit(ref SoftJointLimit value);

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00015EC4 File Offset: 0x000140C4
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00015EDC File Offset: 0x000140DC
		public SoftJointLimit swing2Limit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_swing2Limit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_swing2Limit(ref value);
			}
		}

		// Token: 0x06001390 RID: 5008
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swing2Limit(out SoftJointLimit value);

		// Token: 0x06001391 RID: 5009
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swing2Limit(ref SoftJointLimit value);

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001392 RID: 5010
		// (set) Token: 0x06001393 RID: 5011
		public extern bool enableProjection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001394 RID: 5012
		// (set) Token: 0x06001395 RID: 5013
		public extern float projectionDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001396 RID: 5014
		// (set) Token: 0x06001397 RID: 5015
		public extern float projectionAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0400039D RID: 925
		[Obsolete("TargetRotation not in use for Unity 5 and assumed disabled.", true)]
		public Quaternion targetRotation;

		// Token: 0x0400039E RID: 926
		[Obsolete("TargetAngularVelocity not in use for Unity 5 and assumed disabled.", true)]
		public Vector3 targetAngularVelocity;

		// Token: 0x0400039F RID: 927
		[Obsolete("RotationDrive not in use for Unity 5 and assumed disabled.", true)]
		public JointDrive rotationDrive;
	}
}
