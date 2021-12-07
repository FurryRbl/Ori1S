using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000157 RID: 343
	public class Joint2D : Behaviour
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600164E RID: 5710
		// (set) Token: 0x0600164F RID: 5711
		public extern Rigidbody2D connectedBody { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001650 RID: 5712
		// (set) Token: 0x06001651 RID: 5713
		public extern bool enableCollision { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001652 RID: 5714
		// (set) Token: 0x06001653 RID: 5715
		public extern float breakForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001654 RID: 5716
		// (set) Token: 0x06001655 RID: 5717
		public extern float breakTorque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00017D94 File Offset: 0x00015F94
		public Vector2 reactionForce
		{
			get
			{
				return this.GetReactionForce(Time.fixedDeltaTime);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public float reactionTorque
		{
			get
			{
				return this.GetReactionTorque(Time.fixedDeltaTime);
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00017DB4 File Offset: 0x00015FB4
		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 result;
			Joint2D.Joint2D_CUSTOM_INTERNAL_GetReactionForce(this, timeStep, out result);
			return result;
		}

		// Token: 0x06001659 RID: 5721
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Joint2D_CUSTOM_INTERNAL_GetReactionForce(Joint2D joint, float timeStep, out Vector2 value);

		// Token: 0x0600165A RID: 5722 RVA: 0x00017DCC File Offset: 0x00015FCC
		public float GetReactionTorque(float timeStep)
		{
			return Joint2D.INTERNAL_CALL_GetReactionTorque(this, timeStep);
		}

		// Token: 0x0600165B RID: 5723
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetReactionTorque(Joint2D self, float timeStep);
	}
}
