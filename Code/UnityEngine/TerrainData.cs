using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001D1 RID: 465
	public sealed class TerrainData : Object
	{
		// Token: 0x06001C0D RID: 7181 RVA: 0x0001A954 File Offset: 0x00018B54
		public TerrainData()
		{
			this.Internal_Create(this);
		}

		// Token: 0x06001C0E RID: 7182
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_Create([Writable] TerrainData terrainData);

		// Token: 0x06001C0F RID: 7183
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasUser(GameObject user);

		// Token: 0x06001C10 RID: 7184
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddUser(GameObject user);

		// Token: 0x06001C11 RID: 7185
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveUser(GameObject user);

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001C12 RID: 7186
		public extern int heightmapWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001C13 RID: 7187
		public extern int heightmapHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001C14 RID: 7188
		// (set) Token: 0x06001C15 RID: 7189
		public extern int heightmapResolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0001A964 File Offset: 0x00018B64
		public Vector3 heightmapScale
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_heightmapScale(out result);
				return result;
			}
		}

		// Token: 0x06001C17 RID: 7191
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_heightmapScale(out Vector3 value);

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0001A97C File Offset: 0x00018B7C
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x0001A994 File Offset: 0x00018B94
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_size(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x06001C1A RID: 7194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x06001C1B RID: 7195
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001C1C RID: 7196
		// (set) Token: 0x06001C1D RID: 7197
		public extern float thickness { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C1E RID: 7198
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetHeight(int x, int y);

		// Token: 0x06001C1F RID: 7199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetInterpolatedHeight(float x, float y);

		// Token: 0x06001C20 RID: 7200
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[,] GetHeights(int xBase, int yBase, int width, int height);

		// Token: 0x06001C21 RID: 7201 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			if (heights == null)
			{
				throw new NullReferenceException();
			}
			if (xBase + heights.GetLength(1) > this.heightmapWidth || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > this.heightmapHeight)
			{
				throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{3}", new object[]
				{
					xBase + heights.GetLength(1),
					yBase + heights.GetLength(0),
					this.heightmapWidth,
					this.heightmapHeight
				}));
			}
			this.Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		// Token: 0x06001C22 RID: 7202
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

		// Token: 0x06001C23 RID: 7203
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

		// Token: 0x06001C24 RID: 7204 RVA: 0x0001AA78 File Offset: 0x00018C78
		public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			if (heights == null)
			{
				throw new ArgumentNullException("heights");
			}
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			if (xBase < 0 || xBase + length2 < 0 || xBase + length2 > this.heightmapWidth)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.heightmapWidth
				}));
			}
			if (yBase < 0 || yBase + length < 0 || yBase + length > this.heightmapHeight)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.heightmapHeight
				}));
			}
			this.Internal_SetHeightsDelayLOD(xBase, yBase, length2, length, heights);
		}

		// Token: 0x06001C25 RID: 7205
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetSteepness(float x, float y);

		// Token: 0x06001C26 RID: 7206 RVA: 0x0001AB60 File Offset: 0x00018D60
		public Vector3 GetInterpolatedNormal(float x, float y)
		{
			Vector3 result;
			TerrainData.INTERNAL_CALL_GetInterpolatedNormal(this, x, y, out result);
			return result;
		}

		// Token: 0x06001C27 RID: 7207
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetInterpolatedNormal(TerrainData self, float x, float y, out Vector3 value);

		// Token: 0x06001C28 RID: 7208
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAdjustedSize(int size);

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001C29 RID: 7209
		// (set) Token: 0x06001C2A RID: 7210
		public extern float wavingGrassStrength { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001C2B RID: 7211
		// (set) Token: 0x06001C2C RID: 7212
		public extern float wavingGrassAmount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001C2D RID: 7213
		// (set) Token: 0x06001C2E RID: 7214
		public extern float wavingGrassSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0001AB78 File Offset: 0x00018D78
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x0001AB90 File Offset: 0x00018D90
		public Color wavingGrassTint
		{
			get
			{
				Color result;
				this.INTERNAL_get_wavingGrassTint(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_wavingGrassTint(ref value);
			}
		}

		// Token: 0x06001C31 RID: 7217
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_wavingGrassTint(out Color value);

		// Token: 0x06001C32 RID: 7218
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_wavingGrassTint(ref Color value);

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001C33 RID: 7219
		public extern int detailWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001C34 RID: 7220
		public extern int detailHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001C35 RID: 7221
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDetailResolution(int detailResolution, int resolutionPerPatch);

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001C36 RID: 7222
		public extern int detailResolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001C37 RID: 7223
		internal extern int detailResolutionPerPatch { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001C38 RID: 7224
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ResetDirtyDetails();

		// Token: 0x06001C39 RID: 7225
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RefreshPrototypes();

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001C3A RID: 7226
		// (set) Token: 0x06001C3B RID: 7227
		public extern DetailPrototype[] detailPrototypes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C3C RID: 7228
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

		// Token: 0x06001C3D RID: 7229
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

		// Token: 0x06001C3E RID: 7230 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			this.Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		// Token: 0x06001C3F RID: 7231
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001C40 RID: 7232
		// (set) Token: 0x06001C41 RID: 7233
		public extern TreeInstance[] treeInstances { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C42 RID: 7234 RVA: 0x0001ABC4 File Offset: 0x00018DC4
		public TreeInstance GetTreeInstance(int index)
		{
			TreeInstance result;
			TerrainData.INTERNAL_CALL_GetTreeInstance(this, index, out result);
			return result;
		}

		// Token: 0x06001C43 RID: 7235
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetTreeInstance(TerrainData self, int index, out TreeInstance value);

		// Token: 0x06001C44 RID: 7236 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public void SetTreeInstance(int index, TreeInstance instance)
		{
			TerrainData.INTERNAL_CALL_SetTreeInstance(this, index, ref instance);
		}

		// Token: 0x06001C45 RID: 7237
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTreeInstance(TerrainData self, int index, ref TreeInstance instance);

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001C46 RID: 7238
		public extern int treeInstanceCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001C47 RID: 7239
		// (set) Token: 0x06001C48 RID: 7240
		public extern TreePrototype[] treePrototypes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C49 RID: 7241
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveTreePrototype(int index);

		// Token: 0x06001C4A RID: 7242
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RecalculateTreePositions();

		// Token: 0x06001C4B RID: 7243
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveDetailPrototype(int index);

		// Token: 0x06001C4C RID: 7244
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool NeedUpgradeScaledTreePrototypes();

		// Token: 0x06001C4D RID: 7245
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpgradeScaledTreePrototype();

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001C4E RID: 7246
		public extern int alphamapLayers { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001C4F RID: 7247
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[,,] GetAlphamaps(int x, int y, int width, int height);

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001C50 RID: 7248
		// (set) Token: 0x06001C51 RID: 7249
		public extern int alphamapResolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001C52 RID: 7250
		public extern int alphamapWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001C53 RID: 7251
		public extern int alphamapHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001C54 RID: 7252
		// (set) Token: 0x06001C55 RID: 7253
		public extern int baseMapResolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C56 RID: 7254 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			if (map.GetLength(2) != this.alphamapLayers)
			{
				throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", new object[]
				{
					this.alphamapLayers
				}));
			}
			this.Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		// Token: 0x06001C57 RID: 7255
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

		// Token: 0x06001C58 RID: 7256
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RecalculateBasemapIfDirty();

		// Token: 0x06001C59 RID: 7257
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetBasemapDirty(bool dirty);

		// Token: 0x06001C5A RID: 7258
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetAlphamapTexture(int index);

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001C5B RID: 7259
		private extern int alphamapTextureCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0001AC44 File Offset: 0x00018E44
		public Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[this.alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAlphamapTexture(i);
				}
				return array;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001C5D RID: 7261
		// (set) Token: 0x06001C5E RID: 7262
		public extern SplatPrototype[] splatPrototypes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001C5F RID: 7263
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddTree(out TreeInstance tree);

		// Token: 0x06001C60 RID: 7264 RVA: 0x0001AC7C File Offset: 0x00018E7C
		internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			return TerrainData.INTERNAL_CALL_RemoveTrees(this, ref position, radius, prototypeIndex);
		}

		// Token: 0x06001C61 RID: 7265
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_RemoveTrees(TerrainData self, ref Vector2 position, float radius, int prototypeIndex);
	}
}
