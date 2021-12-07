using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200013C RID: 316
	public sealed class WheelCollider : Collider
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0001636C File Offset: 0x0001456C
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00016384 File Offset: 0x00014584
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

		// Token: 0x06001448 RID: 5192
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001449 RID: 5193
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600144A RID: 5194
		// (set) Token: 0x0600144B RID: 5195
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600144C RID: 5196
		// (set) Token: 0x0600144D RID: 5197
		public extern float suspensionDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00016390 File Offset: 0x00014590
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x000163A8 File Offset: 0x000145A8
		public JointSpring suspensionSpring
		{
			get
			{
				JointSpring result;
				this.INTERNAL_get_suspensionSpring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_suspensionSpring(ref value);
			}
		}

		// Token: 0x06001450 RID: 5200
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_suspensionSpring(out JointSpring value);

		// Token: 0x06001451 RID: 5201
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_suspensionSpring(ref JointSpring value);

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001452 RID: 5202
		// (set) Token: 0x06001453 RID: 5203
		public extern float forceAppPointDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001454 RID: 5204
		// (set) Token: 0x06001455 RID: 5205
		public extern float mass { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001456 RID: 5206
		// (set) Token: 0x06001457 RID: 5207
		public extern float wheelDampingRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000163B4 File Offset: 0x000145B4
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x000163CC File Offset: 0x000145CC
		public WheelFrictionCurve forwardFriction
		{
			get
			{
				WheelFrictionCurve result;
				this.INTERNAL_get_forwardFriction(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_forwardFriction(ref value);
			}
		}

		// Token: 0x0600145A RID: 5210
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_forwardFriction(out WheelFrictionCurve value);

		// Token: 0x0600145B RID: 5211
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_forwardFriction(ref WheelFrictionCurve value);

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x000163D8 File Offset: 0x000145D8
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x000163F0 File Offset: 0x000145F0
		public WheelFrictionCurve sidewaysFriction
		{
			get
			{
				WheelFrictionCurve result;
				this.INTERNAL_get_sidewaysFriction(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_sidewaysFriction(ref value);
			}
		}

		// Token: 0x0600145E RID: 5214
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_sidewaysFriction(out WheelFrictionCurve value);

		// Token: 0x0600145F RID: 5215
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_sidewaysFriction(ref WheelFrictionCurve value);

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001460 RID: 5216
		// (set) Token: 0x06001461 RID: 5217
		public extern float motorTorque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001462 RID: 5218
		// (set) Token: 0x06001463 RID: 5219
		public extern float brakeTorque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001464 RID: 5220
		// (set) Token: 0x06001465 RID: 5221
		public extern float steerAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001466 RID: 5222
		public extern bool isGrounded { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001467 RID: 5223
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ConfigureVehicleSubsteps(float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold);

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001468 RID: 5224
		public extern float sprungMass { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001469 RID: 5225
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetGroundHit(out WheelHit hit);

		// Token: 0x0600146A RID: 5226
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetWorldPose(out Vector3 pos, out Quaternion quat);

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600146B RID: 5227
		public extern float rpm { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
