using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200014A RID: 330
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000178F8 File Offset: 0x00015AF8
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00017910 File Offset: 0x00015B10
		public Vector2 position
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_position(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_position(ref value);
			}
		}

		// Token: 0x060015AA RID: 5546
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_position(out Vector2 value);

		// Token: 0x060015AB RID: 5547
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_position(ref Vector2 value);

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060015AC RID: 5548
		// (set) Token: 0x060015AD RID: 5549
		public extern float rotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060015AE RID: 5550 RVA: 0x0001791C File Offset: 0x00015B1C
		public void MovePosition(Vector2 position)
		{
			Rigidbody2D.INTERNAL_CALL_MovePosition(this, ref position);
		}

		// Token: 0x060015AF RID: 5551
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MovePosition(Rigidbody2D self, ref Vector2 position);

		// Token: 0x060015B0 RID: 5552 RVA: 0x00017928 File Offset: 0x00015B28
		public void MoveRotation(float angle)
		{
			Rigidbody2D.INTERNAL_CALL_MoveRotation(this, angle);
		}

		// Token: 0x060015B1 RID: 5553
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MoveRotation(Rigidbody2D self, float angle);

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00017934 File Offset: 0x00015B34
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0001794C File Offset: 0x00015B4C
		public Vector2 velocity
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x060015B4 RID: 5556
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector2 value);

		// Token: 0x060015B5 RID: 5557
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector2 value);

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060015B6 RID: 5558
		// (set) Token: 0x060015B7 RID: 5559
		public extern float angularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060015B8 RID: 5560
		// (set) Token: 0x060015B9 RID: 5561
		public extern bool useAutoMass { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060015BA RID: 5562
		// (set) Token: 0x060015BB RID: 5563
		public extern float mass { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00017958 File Offset: 0x00015B58
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x00017970 File Offset: 0x00015B70
		public Vector2 centerOfMass
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_centerOfMass(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_centerOfMass(ref value);
			}
		}

		// Token: 0x060015BE RID: 5566
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_centerOfMass(out Vector2 value);

		// Token: 0x060015BF RID: 5567
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_centerOfMass(ref Vector2 value);

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0001797C File Offset: 0x00015B7C
		public Vector2 worldCenterOfMass
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_worldCenterOfMass(out result);
				return result;
			}
		}

		// Token: 0x060015C1 RID: 5569
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldCenterOfMass(out Vector2 value);

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060015C2 RID: 5570
		// (set) Token: 0x060015C3 RID: 5571
		public extern float inertia { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060015C4 RID: 5572
		// (set) Token: 0x060015C5 RID: 5573
		public extern float drag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060015C6 RID: 5574
		// (set) Token: 0x060015C7 RID: 5575
		public extern float angularDrag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060015C8 RID: 5576
		// (set) Token: 0x060015C9 RID: 5577
		public extern float gravityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060015CA RID: 5578
		// (set) Token: 0x060015CB RID: 5579
		public extern bool isKinematic { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060015CC RID: 5580
		// (set) Token: 0x060015CD RID: 5581
		[Obsolete("The fixedAngle is no longer supported. Use constraints instead.")]
		public extern bool fixedAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060015CE RID: 5582
		// (set) Token: 0x060015CF RID: 5583
		public extern bool freezeRotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060015D0 RID: 5584
		// (set) Token: 0x060015D1 RID: 5585
		public extern RigidbodyConstraints2D constraints { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060015D2 RID: 5586
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		// Token: 0x060015D3 RID: 5587
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAwake();

		// Token: 0x060015D4 RID: 5588
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		// Token: 0x060015D5 RID: 5589
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060015D6 RID: 5590
		// (set) Token: 0x060015D7 RID: 5591
		public extern bool simulated { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060015D8 RID: 5592
		// (set) Token: 0x060015D9 RID: 5593
		public extern RigidbodyInterpolation2D interpolation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060015DA RID: 5594
		// (set) Token: 0x060015DB RID: 5595
		public extern RigidbodySleepMode2D sleepMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060015DC RID: 5596
		// (set) Token: 0x060015DD RID: 5597
		public extern CollisionDetectionMode2D collisionDetectionMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060015DE RID: 5598
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouching(Collider2D collider);

		// Token: 0x060015DF RID: 5599
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x060015E0 RID: 5600 RVA: 0x00017994 File Offset: 0x00015B94
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			int layerMask = -1;
			return this.IsTouchingLayers(layerMask);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000179AC File Offset: 0x00015BAC
		public void AddForce(Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			Rigidbody2D.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000179B8 File Offset: 0x00015BB8
		[ExcludeFromDocs]
		public void AddForce(Vector2 force)
		{
			ForceMode2D mode = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x060015E3 RID: 5603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForce(Rigidbody2D self, ref Vector2 force, ForceMode2D mode);

		// Token: 0x060015E4 RID: 5604 RVA: 0x000179D0 File Offset: 0x00015BD0
		public void AddRelativeForce(Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			Rigidbody2D.INTERNAL_CALL_AddRelativeForce(this, ref relativeForce, mode);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000179DC File Offset: 0x00015BDC
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector2 relativeForce)
		{
			ForceMode2D mode = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddRelativeForce(this, ref relativeForce, mode);
		}

		// Token: 0x060015E6 RID: 5606
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddRelativeForce(Rigidbody2D self, ref Vector2 relativeForce, ForceMode2D mode);

		// Token: 0x060015E7 RID: 5607 RVA: 0x000179F4 File Offset: 0x00015BF4
		public void AddForceAtPosition(Vector2 force, Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			Rigidbody2D.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00017A04 File Offset: 0x00015C04
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector2 force, Vector2 position)
		{
			ForceMode2D mode = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x060015E9 RID: 5609
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForceAtPosition(Rigidbody2D self, ref Vector2 force, ref Vector2 position, ForceMode2D mode);

		// Token: 0x060015EA RID: 5610
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTorque(float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060015EB RID: 5611 RVA: 0x00017A20 File Offset: 0x00015C20
		[ExcludeFromDocs]
		public void AddTorque(float torque)
		{
			ForceMode2D mode = ForceMode2D.Force;
			this.AddTorque(torque, mode);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00017A38 File Offset: 0x00015C38
		public Vector2 GetPoint(Vector2 point)
		{
			Vector2 result;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetPoint(this, point, out result);
			return result;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00017A50 File Offset: 0x00015C50
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetPoint(Rigidbody2D rigidbody, Vector2 point, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPoint(rigidbody, ref point, out value);
		}

		// Token: 0x060015EE RID: 5614
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPoint(Rigidbody2D rigidbody, ref Vector2 point, out Vector2 value);

		// Token: 0x060015EF RID: 5615 RVA: 0x00017A5C File Offset: 0x00015C5C
		public Vector2 GetRelativePoint(Vector2 relativePoint)
		{
			Vector2 result;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(this, relativePoint, out result);
			return result;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00017A74 File Offset: 0x00015C74
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(Rigidbody2D rigidbody, Vector2 relativePoint, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(rigidbody, ref relativePoint, out value);
		}

		// Token: 0x060015F1 RID: 5617
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(Rigidbody2D rigidbody, ref Vector2 relativePoint, out Vector2 value);

		// Token: 0x060015F2 RID: 5618 RVA: 0x00017A80 File Offset: 0x00015C80
		public Vector2 GetVector(Vector2 vector)
		{
			Vector2 result;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetVector(this, vector, out result);
			return result;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00017A98 File Offset: 0x00015C98
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetVector(Rigidbody2D rigidbody, Vector2 vector, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetVector(rigidbody, ref vector, out value);
		}

		// Token: 0x060015F4 RID: 5620
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetVector(Rigidbody2D rigidbody, ref Vector2 vector, out Vector2 value);

		// Token: 0x060015F5 RID: 5621 RVA: 0x00017AA4 File Offset: 0x00015CA4
		public Vector2 GetRelativeVector(Vector2 relativeVector)
		{
			Vector2 result;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(this, relativeVector, out result);
			return result;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00017ABC File Offset: 0x00015CBC
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(Rigidbody2D rigidbody, Vector2 relativeVector, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(rigidbody, ref relativeVector, out value);
		}

		// Token: 0x060015F7 RID: 5623
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(Rigidbody2D rigidbody, ref Vector2 relativeVector, out Vector2 value);

		// Token: 0x060015F8 RID: 5624 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public Vector2 GetPointVelocity(Vector2 point)
		{
			Vector2 result;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(this, point, out result);
			return result;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00017AE0 File Offset: 0x00015CE0
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(Rigidbody2D rigidbody, Vector2 point, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(rigidbody, ref point, out value);
		}

		// Token: 0x060015FA RID: 5626
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(Rigidbody2D rigidbody, ref Vector2 point, out Vector2 value);

		// Token: 0x060015FB RID: 5627 RVA: 0x00017AEC File Offset: 0x00015CEC
		public Vector2 GetRelativePointVelocity(Vector2 relativePoint)
		{
			Vector2 result;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(this, relativePoint, out result);
			return result;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00017B04 File Offset: 0x00015D04
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(Rigidbody2D rigidbody, Vector2 relativePoint, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(rigidbody, ref relativePoint, out value);
		}

		// Token: 0x060015FD RID: 5629
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(Rigidbody2D rigidbody, ref Vector2 relativePoint, out Vector2 value);
	}
}
