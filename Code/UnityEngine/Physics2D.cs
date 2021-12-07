using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000142 RID: 322
	public class Physics2D
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060014E7 RID: 5351
		// (set) Token: 0x060014E8 RID: 5352
		public static extern int velocityIterations { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060014E9 RID: 5353
		// (set) Token: 0x060014EA RID: 5354
		public static extern int positionIterations { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000167E8 File Offset: 0x000149E8
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00016800 File Offset: 0x00014A00
		public static Vector2 gravity
		{
			get
			{
				Vector2 result;
				Physics2D.INTERNAL_get_gravity(out result);
				return result;
			}
			set
			{
				Physics2D.INTERNAL_set_gravity(ref value);
			}
		}

		// Token: 0x060014ED RID: 5357
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_gravity(out Vector2 value);

		// Token: 0x060014EE RID: 5358
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_gravity(ref Vector2 value);

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060014EF RID: 5359
		// (set) Token: 0x060014F0 RID: 5360
		public static extern bool queriesHitTriggers { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060014F1 RID: 5361
		// (set) Token: 0x060014F2 RID: 5362
		public static extern bool queriesStartInColliders { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060014F3 RID: 5363
		// (set) Token: 0x060014F4 RID: 5364
		public static extern bool changeStopsCallbacks { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060014F5 RID: 5365
		// (set) Token: 0x060014F6 RID: 5366
		public static extern float velocityThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060014F7 RID: 5367
		// (set) Token: 0x060014F8 RID: 5368
		public static extern float maxLinearCorrection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060014F9 RID: 5369
		// (set) Token: 0x060014FA RID: 5370
		public static extern float maxAngularCorrection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060014FB RID: 5371
		// (set) Token: 0x060014FC RID: 5372
		public static extern float maxTranslationSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060014FD RID: 5373
		// (set) Token: 0x060014FE RID: 5374
		public static extern float maxRotationSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060014FF RID: 5375
		// (set) Token: 0x06001500 RID: 5376
		public static extern float minPenetrationForPenalty { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001501 RID: 5377
		// (set) Token: 0x06001502 RID: 5378
		public static extern float baumgarteScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001503 RID: 5379
		// (set) Token: 0x06001504 RID: 5380
		public static extern float baumgarteTOIScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001505 RID: 5381
		// (set) Token: 0x06001506 RID: 5382
		public static extern float timeToSleep { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001507 RID: 5383
		// (set) Token: 0x06001508 RID: 5384
		public static extern float linearSleepTolerance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001509 RID: 5385
		// (set) Token: 0x0600150A RID: 5386
		public static extern float angularSleepTolerance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600150B RID: 5387
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreCollision(Collider2D collider1, Collider2D collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x0600150C RID: 5388 RVA: 0x0001680C File Offset: 0x00014A0C
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider2D collider1, Collider2D collider2)
		{
			bool ignore = true;
			Physics2D.IgnoreCollision(collider1, collider2, ignore);
		}

		// Token: 0x0600150D RID: 5389
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreCollision(Collider2D collider1, Collider2D collider2);

		// Token: 0x0600150E RID: 5390
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreLayerCollision(int layer1, int layer2, [DefaultValue("true")] bool ignore);

		// Token: 0x0600150F RID: 5391 RVA: 0x00016824 File Offset: 0x00014A24
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			bool ignore = true;
			Physics2D.IgnoreLayerCollision(layer1, layer2, ignore);
		}

		// Token: 0x06001510 RID: 5392
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

		// Token: 0x06001511 RID: 5393
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTouching(Collider2D collider1, Collider2D collider2);

		// Token: 0x06001512 RID: 5394
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTouchingLayers(Collider2D collider, [DefaultValue("AllLayers")] int layerMask);

		// Token: 0x06001513 RID: 5395 RVA: 0x0001683C File Offset: 0x00014A3C
		[ExcludeFromDocs]
		public static bool IsTouchingLayers(Collider2D collider)
		{
			int layerMask = -1;
			return Physics2D.IsTouchingLayers(collider, layerMask);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00016854 File Offset: 0x00014A54
		internal static void SetEditorDragMovement(bool dragging, GameObject[] objs)
		{
			foreach (Rigidbody2D rigidbody2D in Physics2D.m_LastDisabledRigidbody2D)
			{
				if (rigidbody2D != null)
				{
					rigidbody2D.isKinematic = false;
				}
			}
			Physics2D.m_LastDisabledRigidbody2D.Clear();
			if (!dragging)
			{
				return;
			}
			foreach (GameObject gameObject in objs)
			{
				Rigidbody2D[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody2D>(false);
				foreach (Rigidbody2D rigidbody2D2 in componentsInChildren)
				{
					if (!rigidbody2D2.isKinematic)
					{
						rigidbody2D2.isKinematic = true;
						Physics2D.m_LastDisabledRigidbody2D.Add(rigidbody2D2);
					}
				}
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00016944 File Offset: 0x00014B44
		private static void Internal_Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_Linecast(ref start, ref end, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x06001516 RID: 5398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Linecast(ref Vector2 start, ref Vector2 end, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06001517 RID: 5399 RVA: 0x00016958 File Offset: 0x00014B58
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.Linecast(start, end, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00016978 File Offset: 0x00014B78
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.Linecast(start, end, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0001699C File Offset: 0x00014B9C
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.Linecast(start, end, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000169C4 File Offset: 0x00014BC4
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D result;
			Physics2D.Internal_Linecast(start, end, layerMask, minDepth, maxDepth, out result);
			return result;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000169E0 File Offset: 0x00014BE0
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000169F0 File Offset: 0x00014BF0
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00016A10 File Offset: 0x00014C10
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00016A38 File Offset: 0x00014C38
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600151F RID: 5407
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_LinecastAll(ref Vector2 start, ref Vector2 end, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001520 RID: 5408 RVA: 0x00016A60 File Offset: 0x00014C60
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00016A74 File Offset: 0x00014C74
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00016A98 File Offset: 0x00014C98
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00016AC0 File Offset: 0x00014CC0
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001524 RID: 5412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_LinecastNonAlloc(ref Vector2 start, ref Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001525 RID: 5413 RVA: 0x00016AEC File Offset: 0x00014CEC
		private static void Internal_Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x06001526 RID: 5414
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Raycast(ref Vector2 origin, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06001527 RID: 5415 RVA: 0x00016B00 File Offset: 0x00014D00
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.Raycast(origin, direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00016B20 File Offset: 0x00014D20
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.Raycast(origin, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00016B44 File Offset: 0x00014D44
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.Raycast(origin, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00016B6C File Offset: 0x00014D6C
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.Raycast(origin, direction, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00016B9C File Offset: 0x00014D9C
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D result;
			Physics2D.Internal_Raycast(origin, direction, distance, layerMask, minDepth, maxDepth, out result);
			return result;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00016BBC File Offset: 0x00014DBC
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00016BD0 File Offset: 0x00014DD0
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00016BF4 File Offset: 0x00014DF4
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00016C1C File Offset: 0x00014E1C
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00016C48 File Offset: 0x00014E48
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001531 RID: 5425
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_RaycastAll(ref Vector2 origin, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001532 RID: 5426 RVA: 0x00016C78 File Offset: 0x00014E78
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00016C8C File Offset: 0x00014E8C
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00016CB0 File Offset: 0x00014EB0
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00016CD8 File Offset: 0x00014ED8
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00016D04 File Offset: 0x00014F04
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001537 RID: 5431
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_RaycastNonAlloc(ref Vector2 origin, ref Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001538 RID: 5432 RVA: 0x00016D34 File Offset: 0x00014F34
		private static void Internal_CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_CircleCast(ref origin, radius, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x06001539 RID: 5433
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_CircleCast(ref Vector2 origin, float radius, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x0600153A RID: 5434 RVA: 0x00016D54 File Offset: 0x00014F54
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00016D78 File Offset: 0x00014F78
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00016DA0 File Offset: 0x00014FA0
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00016DC8 File Offset: 0x00014FC8
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.CircleCast(origin, radius, direction, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00016DF8 File Offset: 0x00014FF8
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D result;
			Physics2D.Internal_CircleCast(origin, radius, direction, distance, layerMask, minDepth, maxDepth, out result);
			return result;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00016E18 File Offset: 0x00015018
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00016E2C File Offset: 0x0001502C
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00016E50 File Offset: 0x00015050
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00016E78 File Offset: 0x00015078
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00016EA4 File Offset: 0x000150A4
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001544 RID: 5444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_CircleCastAll(ref Vector2 origin, float radius, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001545 RID: 5445 RVA: 0x00016ED4 File Offset: 0x000150D4
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00016EF4 File Offset: 0x000150F4
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00016F1C File Offset: 0x0001511C
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00016F48 File Offset: 0x00015148
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00016F74 File Offset: 0x00015174
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600154A RID: 5450
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_CircleCastNonAlloc(ref Vector2 origin, float radius, ref Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600154B RID: 5451 RVA: 0x00016FA8 File Offset: 0x000151A8
		private static void Internal_BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_BoxCast(ref origin, ref size, angle, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x0600154C RID: 5452
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_BoxCast(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x0600154D RID: 5453 RVA: 0x00016FCC File Offset: 0x000151CC
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00016FF0 File Offset: 0x000151F0
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00017018 File Offset: 0x00015218
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00017044 File Offset: 0x00015244
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.BoxCast(origin, size, angle, direction, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00017074 File Offset: 0x00015274
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D result;
			Physics2D.Internal_BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth, out result);
			return result;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00017098 File Offset: 0x00015298
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000170BC File Offset: 0x000152BC
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000170E4 File Offset: 0x000152E4
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00017110 File Offset: 0x00015310
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00017140 File Offset: 0x00015340
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001557 RID: 5463
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_BoxCastAll(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001558 RID: 5464 RVA: 0x00017174 File Offset: 0x00015374
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00017198 File Offset: 0x00015398
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000171C0 File Offset: 0x000153C0
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000171F0 File Offset: 0x000153F0
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00017220 File Offset: 0x00015420
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, positiveInfinity2, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600155D RID: 5469
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_BoxCastNonAlloc(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600155E RID: 5470 RVA: 0x00017254 File Offset: 0x00015454
		private static void Internal_GetRayIntersection(Ray ray, float distance, int layerMask, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_GetRayIntersection(ref ray, distance, layerMask, out raycastHit);
		}

		// Token: 0x0600155F RID: 5471
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetRayIntersection(ref Ray ray, float distance, int layerMask, out RaycastHit2D raycastHit);

		// Token: 0x06001560 RID: 5472 RVA: 0x00017260 File Offset: 0x00015460
		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
		{
			int layerMask = -5;
			return Physics2D.GetRayIntersection(ray, distance, layerMask);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00017278 File Offset: 0x00015478
		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray)
		{
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.GetRayIntersection(ray, positiveInfinity, layerMask);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00017298 File Offset: 0x00015498
		public static RaycastHit2D GetRayIntersection(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			RaycastHit2D result;
			Physics2D.Internal_GetRayIntersection(ray, distance, layerMask, out result);
			return result;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000172B0 File Offset: 0x000154B0
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.INTERNAL_CALL_GetRayIntersectionAll(ref ray, distance, layerMask);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000172BC File Offset: 0x000154BC
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
		{
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionAll(ref ray, distance, layerMask);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000172D8 File Offset: 0x000154D8
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
		{
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionAll(ref ray, positiveInfinity, layerMask);
		}

		// Token: 0x06001566 RID: 5478
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_GetRayIntersectionAll(ref Ray ray, float distance, int layerMask);

		// Token: 0x06001567 RID: 5479 RVA: 0x000172F8 File Offset: 0x000154F8
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, distance, layerMask);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00017304 File Offset: 0x00015504
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
		{
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, distance, layerMask);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00017320 File Offset: 0x00015520
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
		{
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, positiveInfinity, layerMask);
		}

		// Token: 0x0600156A RID: 5482
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_GetRayIntersectionNonAlloc(ref Ray ray, RaycastHit2D[] results, float distance, int layerMask);

		// Token: 0x0600156B RID: 5483 RVA: 0x00017340 File Offset: 0x00015540
		public static Collider2D OverlapPoint(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0001734C File Offset: 0x0001554C
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0001736C File Offset: 0x0001556C
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00017390 File Offset: 0x00015590
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600156F RID: 5487
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D INTERNAL_CALL_OverlapPoint(ref Vector2 point, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001570 RID: 5488 RVA: 0x000173B8 File Offset: 0x000155B8
		public static Collider2D[] OverlapPointAll(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000173C4 File Offset: 0x000155C4
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000173E4 File Offset: 0x000155E4
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00017408 File Offset: 0x00015608
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001574 RID: 5492
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] INTERNAL_CALL_OverlapPointAll(ref Vector2 point, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001575 RID: 5493 RVA: 0x00017430 File Offset: 0x00015630
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00017440 File Offset: 0x00015640
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00017460 File Offset: 0x00015660
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00017484 File Offset: 0x00015684
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001579 RID: 5497
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapPointNonAlloc(ref Vector2 point, Collider2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600157A RID: 5498 RVA: 0x000174AC File Offset: 0x000156AC
		public static Collider2D OverlapCircle(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000174BC File Offset: 0x000156BC
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000174DC File Offset: 0x000156DC
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00017500 File Offset: 0x00015700
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600157E RID: 5502
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D INTERNAL_CALL_OverlapCircle(ref Vector2 point, float radius, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600157F RID: 5503 RVA: 0x00017528 File Offset: 0x00015728
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00017538 File Offset: 0x00015738
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00017558 File Offset: 0x00015758
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0001757C File Offset: 0x0001577C
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001583 RID: 5507
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] INTERNAL_CALL_OverlapCircleAll(ref Vector2 point, float radius, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001584 RID: 5508 RVA: 0x000175A4 File Offset: 0x000157A4
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000175B4 File Offset: 0x000157B4
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000175D4 File Offset: 0x000157D4
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000175FC File Offset: 0x000157FC
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001588 RID: 5512
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapCircleNonAlloc(ref Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001589 RID: 5513 RVA: 0x00017624 File Offset: 0x00015824
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00017634 File Offset: 0x00015834
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00017654 File Offset: 0x00015854
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0001767C File Offset: 0x0001587C
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600158D RID: 5517
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D INTERNAL_CALL_OverlapArea(ref Vector2 pointA, ref Vector2 pointB, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600158E RID: 5518 RVA: 0x000176A4 File Offset: 0x000158A4
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000176B4 File Offset: 0x000158B4
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000176D4 File Offset: 0x000158D4
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x000176FC File Offset: 0x000158FC
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001592 RID: 5522
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] INTERNAL_CALL_OverlapAreaAll(ref Vector2 pointA, ref Vector2 pointB, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001593 RID: 5523 RVA: 0x00017724 File Offset: 0x00015924
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00017738 File Offset: 0x00015938
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0001775C File Offset: 0x0001595C
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00017784 File Offset: 0x00015984
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int layerMask = -5;
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001597 RID: 5527
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapAreaNonAlloc(ref Vector2 pointA, ref Vector2 pointB, Collider2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x040003B3 RID: 947
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x040003B4 RID: 948
		public const int DefaultRaycastLayers = -5;

		// Token: 0x040003B5 RID: 949
		public const int AllLayers = -1;

		// Token: 0x040003B6 RID: 950
		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();
	}
}
