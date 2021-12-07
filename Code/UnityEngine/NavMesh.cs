using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000171 RID: 369
	public sealed class NavMesh
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x0001827C File Offset: 0x0001647C
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.INTERNAL_CALL_Raycast(ref sourcePosition, ref targetPosition, out hit, areaMask);
		}

		// Token: 0x060017A8 RID: 6056
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Raycast(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int areaMask);

		// Token: 0x060017A9 RID: 6057 RVA: 0x0001828C File Offset: 0x0001648C
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, areaMask, path);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000182A0 File Offset: 0x000164A0
		internal static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			return NavMesh.INTERNAL_CALL_CalculatePathInternal(ref sourcePosition, ref targetPosition, areaMask, path);
		}

		// Token: 0x060017AB RID: 6059
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CalculatePathInternal(ref Vector3 sourcePosition, ref Vector3 targetPosition, int areaMask, NavMeshPath path);

		// Token: 0x060017AC RID: 6060 RVA: 0x000182B0 File Offset: 0x000164B0
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.INTERNAL_CALL_FindClosestEdge(ref sourcePosition, out hit, areaMask);
		}

		// Token: 0x060017AD RID: 6061
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_FindClosestEdge(ref Vector3 sourcePosition, out NavMeshHit hit, int areaMask);

		// Token: 0x060017AE RID: 6062 RVA: 0x000182BC File Offset: 0x000164BC
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
		{
			return NavMesh.INTERNAL_CALL_SamplePosition(ref sourcePosition, out hit, maxDistance, areaMask);
		}

		// Token: 0x060017AF RID: 6063
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SamplePosition(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

		// Token: 0x060017B0 RID: 6064
		[WrapperlessIcall]
		[Obsolete("Use SetAreaCost instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLayerCost(int layer, float cost);

		// Token: 0x060017B1 RID: 6065
		[Obsolete("Use GetAreaCost instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetLayerCost(int layer);

		// Token: 0x060017B2 RID: 6066
		[WrapperlessIcall]
		[Obsolete("Use GetAreaFromName instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNavMeshLayerFromName(string layerName);

		// Token: 0x060017B3 RID: 6067
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAreaCost(int areaIndex, float cost);

		// Token: 0x060017B4 RID: 6068
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAreaCost(int areaIndex);

		// Token: 0x060017B5 RID: 6069
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAreaFromName(string areaName);

		// Token: 0x060017B6 RID: 6070 RVA: 0x000182C8 File Offset: 0x000164C8
		public static NavMeshTriangulation CalculateTriangulation()
		{
			return (NavMeshTriangulation)NavMesh.TriangulateInternal();
		}

		// Token: 0x060017B7 RID: 6071
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object TriangulateInternal();

		// Token: 0x060017B8 RID: 6072
		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Triangulate(out Vector3[] vertices, out int[] indices);

		// Token: 0x060017B9 RID: 6073
		[WrapperlessIcall]
		[Obsolete("AddOffMeshLinks has no effect and is deprecated.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddOffMeshLinks();

		// Token: 0x060017BA RID: 6074
		[Obsolete("RestoreNavMesh has no effect and is deprecated.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RestoreNavMesh();

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000182E4 File Offset: 0x000164E4
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x000182EC File Offset: 0x000164EC
		public static float avoidancePredictionTime
		{
			get
			{
				return NavMesh.GetAvoidancePredictionTime();
			}
			set
			{
				NavMesh.SetAvoidancePredictionTime(value);
			}
		}

		// Token: 0x060017BD RID: 6077
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetAvoidancePredictionTime(float t);

		// Token: 0x060017BE RID: 6078
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetAvoidancePredictionTime();

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000182F4 File Offset: 0x000164F4
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x000182FC File Offset: 0x000164FC
		public static int pathfindingIterationsPerFrame
		{
			get
			{
				return NavMesh.GetPathfindingIterationsPerFrame();
			}
			set
			{
				NavMesh.SetPathfindingIterationsPerFrame(value);
			}
		}

		// Token: 0x060017C1 RID: 6081
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetPathfindingIterationsPerFrame(int iter);

		// Token: 0x060017C2 RID: 6082
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPathfindingIterationsPerFrame();

		// Token: 0x04000403 RID: 1027
		public const int AllAreas = -1;
	}
}
