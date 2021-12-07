using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E2 RID: 226
	public sealed class DynamicGI
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E90 RID: 3728
		// (set) Token: 0x06000E91 RID: 3729
		public static extern float indirectScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000E92 RID: 3730
		// (set) Token: 0x06000E93 RID: 3731
		public static extern float updateThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E94 RID: 3732 RVA: 0x000124D0 File Offset: 0x000106D0
		public static void SetEmissive(Renderer renderer, Color color)
		{
			DynamicGI.INTERNAL_CALL_SetEmissive(renderer, ref color);
		}

		// Token: 0x06000E95 RID: 3733
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetEmissive(Renderer renderer, ref Color color);

		// Token: 0x06000E96 RID: 3734 RVA: 0x000124DC File Offset: 0x000106DC
		public static void UpdateMaterials(Renderer renderer)
		{
			DynamicGI.UpdateMaterialsForRenderer(renderer);
		}

		// Token: 0x06000E97 RID: 3735
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateMaterialsForRenderer(Renderer renderer);

		// Token: 0x06000E98 RID: 3736 RVA: 0x000124E4 File Offset: 0x000106E4
		public static void UpdateMaterials(Terrain terrain)
		{
			if (terrain == null)
			{
				throw new ArgumentNullException("terrain");
			}
			if (terrain.terrainData == null)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			DynamicGI.UpdateMaterialsForTerrain(terrain, new Rect(0f, 0f, 1f, 1f));
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00012544 File Offset: 0x00010744
		public static void UpdateMaterials(Terrain terrain, int x, int y, int width, int height)
		{
			if (terrain == null)
			{
				throw new ArgumentNullException("terrain");
			}
			if (terrain.terrainData == null)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			float num = (float)terrain.terrainData.alphamapWidth;
			float num2 = (float)terrain.terrainData.alphamapHeight;
			DynamicGI.UpdateMaterialsForTerrain(terrain, new Rect((float)x / num, (float)y / num2, (float)width / num, (float)height / num2));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000125BC File Offset: 0x000107BC
		internal static void UpdateMaterialsForTerrain(Terrain terrain, Rect uvBounds)
		{
			DynamicGI.INTERNAL_CALL_UpdateMaterialsForTerrain(terrain, ref uvBounds);
		}

		// Token: 0x06000E9B RID: 3739
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_UpdateMaterialsForTerrain(Terrain terrain, ref Rect uvBounds);

		// Token: 0x06000E9C RID: 3740
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateEnvironment();

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000E9D RID: 3741
		// (set) Token: 0x06000E9E RID: 3742
		public static extern bool synchronousMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
