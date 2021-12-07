using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200013F RID: 319
	public sealed class CharacterController : Collider
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x000166A4 File Offset: 0x000148A4
		public bool SimpleMove(Vector3 speed)
		{
			return CharacterController.INTERNAL_CALL_SimpleMove(this, ref speed);
		}

		// Token: 0x06001497 RID: 5271
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SimpleMove(CharacterController self, ref Vector3 speed);

		// Token: 0x06001498 RID: 5272 RVA: 0x000166B0 File Offset: 0x000148B0
		public CollisionFlags Move(Vector3 motion)
		{
			return CharacterController.INTERNAL_CALL_Move(this, ref motion);
		}

		// Token: 0x06001499 RID: 5273
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionFlags INTERNAL_CALL_Move(CharacterController self, ref Vector3 motion);

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600149A RID: 5274
		public extern bool isGrounded { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x000166BC File Offset: 0x000148BC
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
		}

		// Token: 0x0600149C RID: 5276
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600149D RID: 5277
		public extern CollisionFlags collisionFlags { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600149E RID: 5278
		// (set) Token: 0x0600149F RID: 5279
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060014A0 RID: 5280
		// (set) Token: 0x060014A1 RID: 5281
		public extern float height { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000166D4 File Offset: 0x000148D4
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x000166EC File Offset: 0x000148EC
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

		// Token: 0x060014A4 RID: 5284
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x060014A5 RID: 5285
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060014A6 RID: 5286
		// (set) Token: 0x060014A7 RID: 5287
		public extern float slopeLimit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060014A8 RID: 5288
		// (set) Token: 0x060014A9 RID: 5289
		public extern float stepOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060014AA RID: 5290
		// (set) Token: 0x060014AB RID: 5291
		public extern float skinWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060014AC RID: 5292
		// (set) Token: 0x060014AD RID: 5293
		public extern bool detectCollisions { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
