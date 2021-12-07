using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000098 RID: 152
	public sealed class ProceduralMaterial : Material
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		internal ProceduralMaterial() : base(null)
		{
		}

		// Token: 0x060008C8 RID: 2248
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProceduralPropertyDescription[] GetProceduralPropertyDescriptions();

		// Token: 0x060008C9 RID: 2249
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProceduralProperty(string inputName);

		// Token: 0x060008CA RID: 2250
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetProceduralBoolean(string inputName);

		// Token: 0x060008CB RID: 2251
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsProceduralPropertyVisible(string inputName);

		// Token: 0x060008CC RID: 2252
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralBoolean(string inputName, bool value);

		// Token: 0x060008CD RID: 2253
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetProceduralFloat(string inputName);

		// Token: 0x060008CE RID: 2254
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralFloat(string inputName, float value);

		// Token: 0x060008CF RID: 2255 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public Vector4 GetProceduralVector(string inputName)
		{
			Vector4 result;
			ProceduralMaterial.INTERNAL_CALL_GetProceduralVector(this, inputName, out result);
			return result;
		}

		// Token: 0x060008D0 RID: 2256
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetProceduralVector(ProceduralMaterial self, string inputName, out Vector4 value);

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public void SetProceduralVector(string inputName, Vector4 value)
		{
			ProceduralMaterial.INTERNAL_CALL_SetProceduralVector(this, inputName, ref value);
		}

		// Token: 0x060008D2 RID: 2258
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetProceduralVector(ProceduralMaterial self, string inputName, ref Vector4 value);

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000CAF8 File Offset: 0x0000ACF8
		public Color GetProceduralColor(string inputName)
		{
			Color result;
			ProceduralMaterial.INTERNAL_CALL_GetProceduralColor(this, inputName, out result);
			return result;
		}

		// Token: 0x060008D4 RID: 2260
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetProceduralColor(ProceduralMaterial self, string inputName, out Color value);

		// Token: 0x060008D5 RID: 2261 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public void SetProceduralColor(string inputName, Color value)
		{
			ProceduralMaterial.INTERNAL_CALL_SetProceduralColor(this, inputName, ref value);
		}

		// Token: 0x060008D6 RID: 2262
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetProceduralColor(ProceduralMaterial self, string inputName, ref Color value);

		// Token: 0x060008D7 RID: 2263
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetProceduralEnum(string inputName);

		// Token: 0x060008D8 RID: 2264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralEnum(string inputName, int value);

		// Token: 0x060008D9 RID: 2265
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture2D GetProceduralTexture(string inputName);

		// Token: 0x060008DA RID: 2266
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralTexture(string inputName, Texture2D value);

		// Token: 0x060008DB RID: 2267
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsProceduralPropertyCached(string inputName);

		// Token: 0x060008DC RID: 2268
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CacheProceduralProperty(string inputName, bool value);

		// Token: 0x060008DD RID: 2269
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearCache();

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008DE RID: 2270
		// (set) Token: 0x060008DF RID: 2271
		public extern ProceduralCacheSize cacheSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008E0 RID: 2272
		// (set) Token: 0x060008E1 RID: 2273
		public extern int animationUpdateRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060008E2 RID: 2274
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebuildTextures();

		// Token: 0x060008E3 RID: 2275
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebuildTexturesImmediately();

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008E4 RID: 2276
		public extern bool isProcessing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060008E5 RID: 2277
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopRebuilds();

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008E6 RID: 2278
		public extern bool isCachedDataAvailable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008E7 RID: 2279
		// (set) Token: 0x060008E8 RID: 2280
		public extern bool isLoadTimeGenerated { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008E9 RID: 2281
		public extern ProceduralLoadingBehavior loadingBehavior { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008EA RID: 2282
		public static extern bool isSupported { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008EB RID: 2283
		// (set) Token: 0x060008EC RID: 2284
		public static extern ProceduralProcessorUsage substanceProcessorUsage { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008ED RID: 2285
		// (set) Token: 0x060008EE RID: 2286
		public extern string preset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060008EF RID: 2287
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture[] GetGeneratedTextures();

		// Token: 0x060008F0 RID: 2288
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProceduralTexture GetGeneratedTexture(string textureName);

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008F1 RID: 2289
		// (set) Token: 0x060008F2 RID: 2290
		public extern bool isReadable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
