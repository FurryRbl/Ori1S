using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D4 RID: 468
	[UsedByNativeCode]
	public sealed class Terrain : Behaviour
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001C63 RID: 7267
		// (set) Token: 0x06001C64 RID: 7268
		public extern TerrainRenderFlags editorRenderFlags { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001C65 RID: 7269
		// (set) Token: 0x06001C66 RID: 7270
		public extern TerrainData terrainData { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001C67 RID: 7271
		// (set) Token: 0x06001C68 RID: 7272
		public extern float treeDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001C69 RID: 7273
		// (set) Token: 0x06001C6A RID: 7274
		public extern float treeBillboardDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001C6B RID: 7275
		// (set) Token: 0x06001C6C RID: 7276
		public extern float treeCrossFadeLength { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001C6D RID: 7277
		// (set) Token: 0x06001C6E RID: 7278
		public extern int treeMaximumFullLODCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001C6F RID: 7279
		// (set) Token: 0x06001C70 RID: 7280
		public extern float detailObjectDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001C71 RID: 7281
		// (set) Token: 0x06001C72 RID: 7282
		public extern float detailObjectDensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001C73 RID: 7283
		// (set) Token: 0x06001C74 RID: 7284
		public extern bool collectDetailPatches { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001C75 RID: 7285
		// (set) Token: 0x06001C76 RID: 7286
		public extern float heightmapPixelError { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001C77 RID: 7287
		// (set) Token: 0x06001C78 RID: 7288
		public extern int heightmapMaximumLOD { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001C79 RID: 7289
		// (set) Token: 0x06001C7A RID: 7290
		public extern float basemapDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0001AC90 File Offset: 0x00018E90
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0001AC98 File Offset: 0x00018E98
		[Obsolete("use basemapDistance", true)]
		public float splatmapDistance
		{
			get
			{
				return this.basemapDistance;
			}
			set
			{
				this.basemapDistance = value;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001C7D RID: 7293
		// (set) Token: 0x06001C7E RID: 7294
		public extern int lightmapIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001C7F RID: 7295
		// (set) Token: 0x06001C80 RID: 7296
		public extern int realtimeLightmapIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x0001ACBC File Offset: 0x00018EBC
		public Vector4 lightmapScaleOffset
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_lightmapScaleOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_lightmapScaleOffset(ref value);
			}
		}

		// Token: 0x06001C83 RID: 7299
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lightmapScaleOffset(out Vector4 value);

		// Token: 0x06001C84 RID: 7300
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lightmapScaleOffset(ref Vector4 value);

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_realtimeLightmapScaleOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_realtimeLightmapScaleOffset(ref value);
			}
		}

		// Token: 0x06001C87 RID: 7303
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_realtimeLightmapScaleOffset(out Vector4 value);

		// Token: 0x06001C88 RID: 7304
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_realtimeLightmapScaleOffset(ref Vector4 value);

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001C89 RID: 7305
		// (set) Token: 0x06001C8A RID: 7306
		public extern bool castShadows { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001C8B RID: 7307
		// (set) Token: 0x06001C8C RID: 7308
		public extern ReflectionProbeUsage reflectionProbeUsage { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C8D RID: 7309
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetClosestReflectionProbesInternal(object result);

		// Token: 0x06001C8E RID: 7310 RVA: 0x0001ACEC File Offset: 0x00018EEC
		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			this.GetClosestReflectionProbesInternal(result);
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001C8F RID: 7311
		// (set) Token: 0x06001C90 RID: 7312
		public extern Terrain.MaterialType materialType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001C91 RID: 7313
		// (set) Token: 0x06001C92 RID: 7314
		public extern Material materialTemplate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x0001AD10 File Offset: 0x00018F10
		public Color legacySpecular
		{
			get
			{
				Color result;
				this.INTERNAL_get_legacySpecular(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_legacySpecular(ref value);
			}
		}

		// Token: 0x06001C95 RID: 7317
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_legacySpecular(out Color value);

		// Token: 0x06001C96 RID: 7318
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_legacySpecular(ref Color value);

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001C97 RID: 7319
		// (set) Token: 0x06001C98 RID: 7320
		public extern float legacyShininess { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001C99 RID: 7321
		// (set) Token: 0x06001C9A RID: 7322
		public extern bool drawHeightmap { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001C9B RID: 7323
		// (set) Token: 0x06001C9C RID: 7324
		public extern bool drawTreesAndFoliage { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C9D RID: 7325 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public float SampleHeight(Vector3 worldPosition)
		{
			return Terrain.INTERNAL_CALL_SampleHeight(this, ref worldPosition);
		}

		// Token: 0x06001C9E RID: 7326
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_SampleHeight(Terrain self, ref Vector3 worldPosition);

		// Token: 0x06001C9F RID: 7327
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ApplyDelayedHeightmapModification();

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0001AD28 File Offset: 0x00018F28
		public void AddTreeInstance(TreeInstance instance)
		{
			Terrain.INTERNAL_CALL_AddTreeInstance(this, ref instance);
		}

		// Token: 0x06001CA1 RID: 7329
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddTreeInstance(Terrain self, ref TreeInstance instance);

		// Token: 0x06001CA2 RID: 7330
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom);

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0001AD34 File Offset: 0x00018F34
		public Vector3 GetPosition()
		{
			Vector3 result;
			Terrain.INTERNAL_CALL_GetPosition(this, out result);
			return result;
		}

		// Token: 0x06001CA4 RID: 7332
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPosition(Terrain self, out Vector3 value);

		// Token: 0x06001CA5 RID: 7333
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Flush();

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0001AD4C File Offset: 0x00018F4C
		internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			Terrain.INTERNAL_CALL_RemoveTrees(this, ref position, radius, prototypeIndex);
		}

		// Token: 0x06001CA7 RID: 7335
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RemoveTrees(Terrain self, ref Vector2 position, float radius, int prototypeIndex);

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001CA8 RID: 7336
		public static extern Terrain activeTerrain { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001CA9 RID: 7337
		public static extern Terrain[] activeTerrains { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001CAA RID: 7338
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject CreateTerrainGameObject(TerrainData assignTerrain);

		// Token: 0x020001D5 RID: 469
		public enum MaterialType
		{
			// Token: 0x040005B9 RID: 1465
			BuiltInStandard,
			// Token: 0x040005BA RID: 1466
			BuiltInLegacyDiffuse,
			// Token: 0x040005BB RID: 1467
			BuiltInLegacySpecular,
			// Token: 0x040005BC RID: 1468
			Custom
		}
	}
}
