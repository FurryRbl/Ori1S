using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000129 RID: 297
	public class Physics
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00014770 File Offset: 0x00012970
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00014788 File Offset: 0x00012988
		public static Vector3 gravity
		{
			get
			{
				Vector3 result;
				Physics.INTERNAL_get_gravity(out result);
				return result;
			}
			set
			{
				Physics.INTERNAL_set_gravity(ref value);
			}
		}

		// Token: 0x060011FB RID: 4603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_gravity(out Vector3 value);

		// Token: 0x060011FC RID: 4604
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_gravity(ref Vector3 value);

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060011FD RID: 4605
		// (set) Token: 0x060011FE RID: 4606
		[Obsolete("use Physics.defaultContactOffset or Collider.contactOffset instead.", true)]
		public static extern float minPenetrationForPenalty { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060011FF RID: 4607
		// (set) Token: 0x06001200 RID: 4608
		public static extern float defaultContactOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001201 RID: 4609
		// (set) Token: 0x06001202 RID: 4610
		public static extern float bounceThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00014794 File Offset: 0x00012994
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x0001479C File Offset: 0x0001299C
		[Obsolete("Please use bounceThreshold instead.")]
		public static float bounceTreshold
		{
			get
			{
				return Physics.bounceThreshold;
			}
			set
			{
				Physics.bounceThreshold = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001205 RID: 4613
		// (set) Token: 0x06001206 RID: 4614
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public static extern float sleepVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001207 RID: 4615
		// (set) Token: 0x06001208 RID: 4616
		[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public static extern float sleepAngularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001209 RID: 4617
		// (set) Token: 0x0600120A RID: 4618
		[Obsolete("use Rigidbody.maxAngularVelocity instead.", true)]
		public static extern float maxAngularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600120B RID: 4619
		// (set) Token: 0x0600120C RID: 4620
		public static extern int solverIterationCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600120D RID: 4621
		// (set) Token: 0x0600120E RID: 4622
		public static extern float sleepThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600120F RID: 4623
		// (set) Token: 0x06001210 RID: 4624
		public static extern bool queriesHitTriggers { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001211 RID: 4625 RVA: 0x000147A4 File Offset: 0x000129A4
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000147C0 File Offset: 0x000129C0
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000147DC File Offset: 0x000129DC
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(origin, direction, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00014800 File Offset: 0x00012A00
		public static bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_RaycastTest(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00014810 File Offset: 0x00012A10
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0001482C File Offset: 0x00012A2C
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0001484C File Offset: 0x00012A4C
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(origin, direction, out hitInfo, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00014870 File Offset: 0x00012A70
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00014880 File Offset: 0x00012A80
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.Raycast(ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00014898 File Offset: 0x00012A98
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.Raycast(ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000148B4 File Offset: 0x00012AB4
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000148D8 File Offset: 0x00012AD8
		public static bool Raycast(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Raycast(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000148FC File Offset: 0x00012AFC
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00014918 File Offset: 0x00012B18
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00014934 File Offset: 0x00012B34
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, out hitInfo, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00014958 File Offset: 0x00012B58
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00014980 File Offset: 0x00012B80
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00014998 File Offset: 0x00012B98
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000149B4 File Offset: 0x00012BB4
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.RaycastAll(ray, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000149D8 File Offset: 0x00012BD8
		public static RaycastHit[] RaycastAll(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000149FC File Offset: 0x00012BFC
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00014A0C File Offset: 0x00012C0C
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00014A28 File Offset: 0x00012C28
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00014A48 File Offset: 0x00012C48
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001229 RID: 4649
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_RaycastAll(ref Vector3 origin, ref Vector3 direction, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600122A RID: 4650 RVA: 0x00014A6C File Offset: 0x00012C6C
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00014A88 File Offset: 0x00012C88
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00014AA4 File Offset: 0x00012CA4
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.RaycastNonAlloc(ray, results, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastNonAlloc(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00014AF0 File Offset: 0x00012CF0
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00014B04 File Offset: 0x00012D04
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00014B24 File Offset: 0x00012D24
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00014B44 File Offset: 0x00012D44
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001232 RID: 4658
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_RaycastNonAlloc(ref Vector3 origin, ref Vector3 direction, RaycastHit[] results, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001233 RID: 4659 RVA: 0x00014B6C File Offset: 0x00012D6C
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.Linecast(start, end, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00014B84 File Offset: 0x00012D84
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.Linecast(start, end, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00014BA0 File Offset: 0x00012DA0
		public static bool Linecast(Vector3 start, Vector3 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			Vector3 direction = end - start;
			return Physics.Raycast(start, direction, direction.magnitude, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00014BC8 File Offset: 0x00012DC8
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.Linecast(start, end, out hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00014BE4 File Offset: 0x00012DE4
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.Linecast(start, end, out hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00014C00 File Offset: 0x00012E00
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			Vector3 direction = end - start;
			return Physics.Raycast(start, direction, out hitInfo, direction.magnitude, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00014C28 File Offset: 0x00012E28
		public static Collider[] OverlapSphere(Vector3 position, float radius, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_OverlapSphere(ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00014C34 File Offset: 0x00012E34
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_OverlapSphere(ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00014C50 File Offset: 0x00012E50
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -1;
			return Physics.INTERNAL_CALL_OverlapSphere(ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600123C RID: 4668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider[] INTERNAL_CALL_OverlapSphere(ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600123D RID: 4669 RVA: 0x00014C6C File Offset: 0x00012E6C
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_OverlapSphereNonAlloc(ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00014C7C File Offset: 0x00012E7C
		[ExcludeFromDocs]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_OverlapSphereNonAlloc(ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00014C98 File Offset: 0x00012E98
		[ExcludeFromDocs]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -1;
			return Physics.INTERNAL_CALL_OverlapSphereNonAlloc(ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001240 RID: 4672
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapSphereNonAlloc(ref Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001241 RID: 4673 RVA: 0x00014CB4 File Offset: 0x00012EB4
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00014CD4 File Offset: 0x00012ED4
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00014CF4 File Offset: 0x00012EF4
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.CapsuleCast(point1, point2, radius, direction, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00014D18 File Offset: 0x00012F18
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.Internal_CapsuleCast(point1, point2, radius, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00014D38 File Offset: 0x00012F38
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00014D58 File Offset: 0x00012F58
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00014D7C File Offset: 0x00012F7C
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00014DC4 File Offset: 0x00012FC4
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00014DE4 File Offset: 0x00012FE4
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00014E04 File Offset: 0x00013004
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCast(origin, radius, direction, out hitInfo, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00014E28 File Offset: 0x00013028
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_CapsuleCast(origin, origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00014E48 File Offset: 0x00013048
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCast(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00014E64 File Offset: 0x00013064
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCast(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00014E80 File Offset: 0x00013080
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCast(ray, radius, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00014EA4 File Offset: 0x000130A4
		public static bool SphereCast(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.Internal_CapsuleCast(ray.origin, ray.origin, radius, ray.direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00014ED4 File Offset: 0x000130D4
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00014EF0 File Offset: 0x000130F0
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00014F10 File Offset: 0x00013110
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCast(ray, radius, out hitInfo, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00014F34 File Offset: 0x00013134
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_CapsuleCast(ray.origin, ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00014F64 File Offset: 0x00013164
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00014F78 File Offset: 0x00013178
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00014F98 File Offset: 0x00013198
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00014FBC File Offset: 0x000131BC
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001259 RID: 4697
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_CapsuleCastAll(ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600125A RID: 4698 RVA: 0x00014FE4 File Offset: 0x000131E4
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_CapsuleCastNonAlloc(ref point1, ref point2, radius, ref direction, results, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00015008 File Offset: 0x00013208
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_CapsuleCastNonAlloc(ref point1, ref point2, radius, ref direction, results, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0001502C File Offset: 0x0001322C
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_CapsuleCastNonAlloc(ref point1, ref point2, radius, ref direction, results, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00015050 File Offset: 0x00013250
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_CapsuleCastNonAlloc(ref point1, ref point2, radius, ref direction, results, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600125E RID: 4702
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_CapsuleCastNonAlloc(ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, RaycastHit[] results, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600125F RID: 4703 RVA: 0x0001507C File Offset: 0x0001327C
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00015098 File Offset: 0x00013298
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000150B8 File Offset: 0x000132B8
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCastAll(origin, radius, direction, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000150DC File Offset: 0x000132DC
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCastAll(origin, origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000150EC File Offset: 0x000132EC
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00015108 File Offset: 0x00013308
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00015124 File Offset: 0x00013324
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCastAll(ray, radius, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00015148 File Offset: 0x00013348
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCastAll(ray.origin, ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00015174 File Offset: 0x00013374
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00015194 File Offset: 0x00013394
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000151B4 File Offset: 0x000133B4
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000151D8 File Offset: 0x000133D8
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCastNonAlloc(origin, origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000151F8 File Offset: 0x000133F8
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00015214 File Offset: 0x00013414
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00015234 File Offset: 0x00013434
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCastNonAlloc(ray, radius, results, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00015258 File Offset: 0x00013458
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCastNonAlloc(ray.origin, ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00015288 File Offset: 0x00013488
		public static bool CheckSphere(Vector3 position, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_CheckSphere(ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00015294 File Offset: 0x00013494
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_CheckSphere(ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000152B0 File Offset: 0x000134B0
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.INTERNAL_CALL_CheckSphere(ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001272 RID: 4722
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CheckSphere(ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001273 RID: 4723 RVA: 0x000152CC File Offset: 0x000134CC
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_CheckCapsule(ref start, ref end, radius, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000152DC File Offset: 0x000134DC
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_CheckCapsule(ref start, ref end, radius, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000152F8 File Offset: 0x000134F8
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_CheckCapsule(ref start, ref end, radius, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001276 RID: 4726
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CheckCapsule(ref Vector3 start, ref Vector3 end, float radius, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001277 RID: 4727 RVA: 0x00015318 File Offset: 0x00013518
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_CheckBox(ref center, ref halfExtents, ref orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00015328 File Offset: 0x00013528
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_CheckBox(ref center, ref halfExtents, ref orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00015344 File Offset: 0x00013544
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_CheckBox(ref center, ref halfExtents, ref orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00015364 File Offset: 0x00013564
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			Quaternion identity = Quaternion.identity;
			return Physics.INTERNAL_CALL_CheckBox(ref center, ref halfExtents, ref identity, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600127B RID: 4731
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CheckBox(ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600127C RID: 4732 RVA: 0x0001538C File Offset: 0x0001358C
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_OverlapBox(ref center, ref halfExtents, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0001539C File Offset: 0x0001359C
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_OverlapBox(ref center, ref halfExtents, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000153B8 File Offset: 0x000135B8
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -1;
			return Physics.INTERNAL_CALL_OverlapBox(ref center, ref halfExtents, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000153D8 File Offset: 0x000135D8
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -1;
			Quaternion identity = Quaternion.identity;
			return Physics.INTERNAL_CALL_OverlapBox(ref center, ref halfExtents, ref identity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001280 RID: 4736
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider[] INTERNAL_CALL_OverlapBox(ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001281 RID: 4737 RVA: 0x000153FC File Offset: 0x000135FC
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_OverlapBoxNonAlloc(ref center, ref halfExtents, results, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00015410 File Offset: 0x00013610
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_OverlapBoxNonAlloc(ref center, ref halfExtents, results, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00015430 File Offset: 0x00013630
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -1;
			return Physics.INTERNAL_CALL_OverlapBoxNonAlloc(ref center, ref halfExtents, results, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00015450 File Offset: 0x00013650
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -1;
			Quaternion identity = Quaternion.identity;
			return Physics.INTERNAL_CALL_OverlapBoxNonAlloc(ref center, ref halfExtents, results, ref identity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001285 RID: 4741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapBoxNonAlloc(ref Vector3 center, ref Vector3 halfExtents, Collider[] results, ref Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001286 RID: 4742 RVA: 0x00015478 File Offset: 0x00013678
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_BoxCastAll(ref center, ref halfExtents, ref direction, ref orientation, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00015490 File Offset: 0x00013690
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_BoxCastAll(ref center, ref halfExtents, ref direction, ref orientation, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000154B4 File Offset: 0x000136B4
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_BoxCastAll(ref center, ref halfExtents, ref direction, ref orientation, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000154D8 File Offset: 0x000136D8
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_BoxCastAll(ref center, ref halfExtents, ref direction, ref orientation, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00015500 File Offset: 0x00013700
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			Quaternion identity = Quaternion.identity;
			return Physics.INTERNAL_CALL_BoxCastAll(ref center, ref halfExtents, ref direction, ref identity, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600128B RID: 4747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_BoxCastAll(ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, ref Quaternion orientation, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600128C RID: 4748 RVA: 0x00015530 File Offset: 0x00013730
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_BoxCastNonAlloc(ref center, ref halfExtents, ref direction, results, ref orientation, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00015554 File Offset: 0x00013754
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layermask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.INTERNAL_CALL_BoxCastNonAlloc(ref center, ref halfExtents, ref direction, results, ref orientation, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00015578 File Offset: 0x00013778
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			return Physics.INTERNAL_CALL_BoxCastNonAlloc(ref center, ref halfExtents, ref direction, results, ref orientation, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0001559C File Offset: 0x0001379C
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_BoxCastNonAlloc(ref center, ref halfExtents, ref direction, results, ref orientation, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000155C8 File Offset: 0x000137C8
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layermask = -5;
			float positiveInfinity = float.PositiveInfinity;
			Quaternion identity = Quaternion.identity;
			return Physics.INTERNAL_CALL_BoxCastNonAlloc(ref center, ref halfExtents, ref direction, results, ref identity, positiveInfinity, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001291 RID: 4753
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_BoxCastNonAlloc(ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, RaycastHit[] results, ref Quaternion orientation, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001292 RID: 4754 RVA: 0x000155F8 File Offset: 0x000137F8
		private static bool Internal_BoxCast(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_Internal_BoxCast(ref center, ref halfExtents, ref orientation, ref direction, out hitInfo, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x06001293 RID: 4755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_BoxCast(ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, ref Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001294 RID: 4756 RVA: 0x0001561C File Offset: 0x0001381C
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0001563C File Offset: 0x0001383C
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0001565C File Offset: 0x0001385C
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.BoxCast(center, halfExtents, direction, orientation, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00015680 File Offset: 0x00013880
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			Quaternion identity = Quaternion.identity;
			return Physics.BoxCast(center, halfExtents, direction, identity, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000156AC File Offset: 0x000138AC
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.Internal_BoxCast(center, halfExtents, orientation, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000156CC File Offset: 0x000138CC
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000156EC File Offset: 0x000138EC
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00015710 File Offset: 0x00013910
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00015738 File Offset: 0x00013938
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			Quaternion identity = Quaternion.identity;
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, identity, positiveInfinity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00015764 File Offset: 0x00013964
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_BoxCast(center, halfExtents, orientation, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600129E RID: 4766
		// (set) Token: 0x0600129F RID: 4767
		[Obsolete("penetrationPenaltyForce has no effect.")]
		public static extern float penetrationPenaltyForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060012A0 RID: 4768
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreCollision(Collider collider1, Collider collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x060012A1 RID: 4769 RVA: 0x00015784 File Offset: 0x00013984
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider collider1, Collider collider2)
		{
			bool ignore = true;
			Physics.IgnoreCollision(collider1, collider2, ignore);
		}

		// Token: 0x060012A2 RID: 4770
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreLayerCollision(int layer1, int layer2, [DefaultValue("true")] bool ignore);

		// Token: 0x060012A3 RID: 4771 RVA: 0x0001579C File Offset: 0x0001399C
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			bool ignore = true;
			Physics.IgnoreLayerCollision(layer1, layer2, ignore);
		}

		// Token: 0x060012A4 RID: 4772
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

		// Token: 0x060012A5 RID: 4773 RVA: 0x000157B4 File Offset: 0x000139B4
		private static bool Internal_Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, out hitInfo, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x060012A6 RID: 4774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(ref Vector3 origin, ref Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060012A7 RID: 4775 RVA: 0x000157C8 File Offset: 0x000139C8
		private static bool Internal_CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_Internal_CapsuleCast(ref point1, ref point2, radius, ref direction, out hitInfo, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x060012A8 RID: 4776
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_CapsuleCast(ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060012A9 RID: 4777 RVA: 0x000157EC File Offset: 0x000139EC
		private static bool Internal_RaycastTest(Vector3 origin, Vector3 direction, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.INTERNAL_CALL_Internal_RaycastTest(ref origin, ref direction, maxDistance, layermask, queryTriggerInteraction);
		}

		// Token: 0x060012AA RID: 4778
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_RaycastTest(ref Vector3 origin, ref Vector3 direction, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0400038B RID: 907
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x0400038C RID: 908
		[Obsolete("Please use Physics.IgnoreRaycastLayer instead. (UnityUpgradable) -> IgnoreRaycastLayer", true)]
		public const int kIgnoreRaycastLayer = 4;

		// Token: 0x0400038D RID: 909
		public const int DefaultRaycastLayers = -5;

		// Token: 0x0400038E RID: 910
		[Obsolete("Please use Physics.DefaultRaycastLayers instead. (UnityUpgradable) -> DefaultRaycastLayers", true)]
		public const int kDefaultRaycastLayers = -5;

		// Token: 0x0400038F RID: 911
		public const int AllLayers = -1;

		// Token: 0x04000390 RID: 912
		[Obsolete("Please use Physics.AllLayers instead. (UnityUpgradable) -> AllLayers", true)]
		public const int kAllLayers = -1;
	}
}
