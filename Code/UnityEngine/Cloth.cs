using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000141 RID: 321
	public sealed class Cloth : Component
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060014B5 RID: 5301
		// (set) Token: 0x060014B6 RID: 5302
		public extern float sleepThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060014B7 RID: 5303
		// (set) Token: 0x060014B8 RID: 5304
		public extern float bendingStiffness { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060014B9 RID: 5305
		// (set) Token: 0x060014BA RID: 5306
		public extern float stretchingStiffness { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060014BB RID: 5307
		// (set) Token: 0x060014BC RID: 5308
		public extern float damping { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00016768 File Offset: 0x00014968
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x00016780 File Offset: 0x00014980
		public Vector3 externalAcceleration
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_externalAcceleration(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_externalAcceleration(ref value);
			}
		}

		// Token: 0x060014BF RID: 5311
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_externalAcceleration(out Vector3 value);

		// Token: 0x060014C0 RID: 5312
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_externalAcceleration(ref Vector3 value);

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0001678C File Offset: 0x0001498C
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x000167A4 File Offset: 0x000149A4
		public Vector3 randomAcceleration
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_randomAcceleration(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_randomAcceleration(ref value);
			}
		}

		// Token: 0x060014C3 RID: 5315
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_randomAcceleration(out Vector3 value);

		// Token: 0x060014C4 RID: 5316
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_randomAcceleration(ref Vector3 value);

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060014C5 RID: 5317
		// (set) Token: 0x060014C6 RID: 5318
		public extern bool useGravity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060014C7 RID: 5319
		// (set) Token: 0x060014C8 RID: 5320
		[Obsolete("Deprecated. Cloth.selfCollisions is no longer supported since Unity 5.0.", true)]
		public extern bool selfCollision { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060014C9 RID: 5321
		// (set) Token: 0x060014CA RID: 5322
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060014CB RID: 5323
		public extern Vector3[] vertices { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060014CC RID: 5324
		public extern Vector3[] normals { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060014CD RID: 5325
		// (set) Token: 0x060014CE RID: 5326
		public extern float friction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060014CF RID: 5327
		// (set) Token: 0x060014D0 RID: 5328
		public extern float collisionMassScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060014D1 RID: 5329
		// (set) Token: 0x060014D2 RID: 5330
		public extern float useContinuousCollision { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060014D3 RID: 5331
		// (set) Token: 0x060014D4 RID: 5332
		public extern float useVirtualParticles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060014D5 RID: 5333 RVA: 0x000167B0 File Offset: 0x000149B0
		public void ClearTransformMotion()
		{
			Cloth.INTERNAL_CALL_ClearTransformMotion(this);
		}

		// Token: 0x060014D6 RID: 5334
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClearTransformMotion(Cloth self);

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060014D7 RID: 5335
		// (set) Token: 0x060014D8 RID: 5336
		public extern ClothSkinningCoefficient[] coefficients { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060014D9 RID: 5337
		// (set) Token: 0x060014DA RID: 5338
		public extern float worldVelocityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060014DB RID: 5339
		// (set) Token: 0x060014DC RID: 5340
		public extern float worldAccelerationScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060014DD RID: 5341
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetEnabledFading(bool enabled, [DefaultValue("0.5f")] float interpolationTime);

		// Token: 0x060014DE RID: 5342 RVA: 0x000167B8 File Offset: 0x000149B8
		[ExcludeFromDocs]
		public void SetEnabledFading(bool enabled)
		{
			float interpolationTime = 0.5f;
			this.SetEnabledFading(enabled, interpolationTime);
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060014DF RID: 5343
		// (set) Token: 0x060014E0 RID: 5344
		public extern bool solverFrequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060014E1 RID: 5345
		// (set) Token: 0x060014E2 RID: 5346
		public extern CapsuleCollider[] capsuleColliders { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060014E3 RID: 5347
		// (set) Token: 0x060014E4 RID: 5348
		public extern ClothSphereColliderPair[] sphereColliders { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
