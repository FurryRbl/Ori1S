using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	public sealed class StaticBatchingUtility
	{
		// Token: 0x06000320 RID: 800 RVA: 0x00003D50 File Offset: 0x00001F50
		public static void Combine(GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.CombineRoot(staticBatchRoot);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00003D58 File Offset: 0x00001F58
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.CombineGameObjects(gos, staticBatchRoot, false);
		}

		// Token: 0x06000322 RID: 802
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		// Token: 0x06000323 RID: 803
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, [Writable] Mesh combinedMesh);
	}
}
