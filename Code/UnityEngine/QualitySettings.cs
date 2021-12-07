using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	public sealed class QualitySettings : Object
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000107 RID: 263
		public static extern string[] names { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000108 RID: 264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetQualityLevel();

		// Token: 0x06000109 RID: 265
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		// Token: 0x0600010A RID: 266 RVA: 0x000029F4 File Offset: 0x00000BF4
		[ExcludeFromDocs]
		public static void SetQualityLevel(int index)
		{
			bool applyExpensiveChanges = true;
			QualitySettings.SetQualityLevel(index, applyExpensiveChanges);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010B RID: 267
		// (set) Token: 0x0600010C RID: 268
		[Obsolete("Use GetQualityLevel and SetQualityLevel")]
		public static extern QualityLevel currentLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600010D RID: 269
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IncreaseLevel([DefaultValue("false")] bool applyExpensiveChanges);

		// Token: 0x0600010E RID: 270 RVA: 0x00002A0C File Offset: 0x00000C0C
		[ExcludeFromDocs]
		public static void IncreaseLevel()
		{
			bool applyExpensiveChanges = false;
			QualitySettings.IncreaseLevel(applyExpensiveChanges);
		}

		// Token: 0x0600010F RID: 271
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DecreaseLevel([DefaultValue("false")] bool applyExpensiveChanges);

		// Token: 0x06000110 RID: 272 RVA: 0x00002A24 File Offset: 0x00000C24
		[ExcludeFromDocs]
		public static void DecreaseLevel()
		{
			bool applyExpensiveChanges = false;
			QualitySettings.DecreaseLevel(applyExpensiveChanges);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000111 RID: 273
		// (set) Token: 0x06000112 RID: 274
		public static extern int pixelLightCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000113 RID: 275
		// (set) Token: 0x06000114 RID: 276
		public static extern ShadowProjection shadowProjection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000115 RID: 277
		// (set) Token: 0x06000116 RID: 278
		public static extern int shadowCascades { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000117 RID: 279
		// (set) Token: 0x06000118 RID: 280
		public static extern float shadowDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000119 RID: 281
		// (set) Token: 0x0600011A RID: 282
		public static extern float shadowNearPlaneOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011B RID: 283
		// (set) Token: 0x0600011C RID: 284
		public static extern float shadowCascade2Split { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00002A54 File Offset: 0x00000C54
		public static Vector3 shadowCascade4Split
		{
			get
			{
				Vector3 result;
				QualitySettings.INTERNAL_get_shadowCascade4Split(out result);
				return result;
			}
			set
			{
				QualitySettings.INTERNAL_set_shadowCascade4Split(ref value);
			}
		}

		// Token: 0x0600011F RID: 287
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_shadowCascade4Split(out Vector3 value);

		// Token: 0x06000120 RID: 288
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_shadowCascade4Split(ref Vector3 value);

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000121 RID: 289
		// (set) Token: 0x06000122 RID: 290
		public static extern int masterTextureLimit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000123 RID: 291
		// (set) Token: 0x06000124 RID: 292
		public static extern AnisotropicFiltering anisotropicFiltering { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000125 RID: 293
		// (set) Token: 0x06000126 RID: 294
		public static extern float lodBias { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000127 RID: 295
		// (set) Token: 0x06000128 RID: 296
		public static extern int maximumLODLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000129 RID: 297
		// (set) Token: 0x0600012A RID: 298
		public static extern int particleRaycastBudget { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012B RID: 299
		// (set) Token: 0x0600012C RID: 300
		public static extern bool softVegetation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012D RID: 301
		// (set) Token: 0x0600012E RID: 302
		public static extern bool realtimeReflectionProbes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012F RID: 303
		// (set) Token: 0x06000130 RID: 304
		public static extern bool billboardsFaceCameraPosition { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000131 RID: 305
		// (set) Token: 0x06000132 RID: 306
		public static extern int maxQueuedFrames { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000133 RID: 307
		// (set) Token: 0x06000134 RID: 308
		public static extern int vSyncCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000135 RID: 309
		// (set) Token: 0x06000136 RID: 310
		public static extern int antiAliasing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000137 RID: 311
		public static extern ColorSpace desiredColorSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000138 RID: 312
		public static extern ColorSpace activeColorSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000139 RID: 313
		// (set) Token: 0x0600013A RID: 314
		public static extern BlendWeights blendWeights { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013B RID: 315
		// (set) Token: 0x0600013C RID: 316
		public static extern int asyncUploadTimeSlice { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		public static extern int asyncUploadBufferSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
