using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	public sealed class GeometryUtility
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00003B90 File Offset: 0x00001D90
		public static Plane[] CalculateFrustumPlanes(Camera camera)
		{
			return GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static Plane[] CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.Internal_ExtractPlanes(array, worldToProjectionMatrix);
			return array;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00003BC4 File Offset: 0x00001DC4
		private static void Internal_ExtractPlanes(Plane[] planes, Matrix4x4 worldToProjectionMatrix)
		{
			GeometryUtility.INTERNAL_CALL_Internal_ExtractPlanes(planes, ref worldToProjectionMatrix);
		}

		// Token: 0x060002CC RID: 716
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ExtractPlanes(Plane[] planes, ref Matrix4x4 worldToProjectionMatrix);

		// Token: 0x060002CD RID: 717 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public static bool TestPlanesAABB(Plane[] planes, Bounds bounds)
		{
			return GeometryUtility.INTERNAL_CALL_TestPlanesAABB(planes, ref bounds);
		}

		// Token: 0x060002CE RID: 718
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_TestPlanesAABB(Plane[] planes, ref Bounds bounds);
	}
}
