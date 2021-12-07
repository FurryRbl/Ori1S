using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200004A RID: 74
	public sealed class ReflectionProbe : Behaviour
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600040C RID: 1036
		// (set) Token: 0x0600040D RID: 1037
		public extern ReflectionProbeType type { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600040E RID: 1038
		// (set) Token: 0x0600040F RID: 1039
		public extern bool hdr { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00004670 File Offset: 0x00002870
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00004688 File Offset: 0x00002888
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

		// Token: 0x06000412 RID: 1042
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x06000413 RID: 1043
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00004694 File Offset: 0x00002894
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000046AC File Offset: 0x000028AC
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_center(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x06000416 RID: 1046
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06000417 RID: 1047
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000418 RID: 1048
		// (set) Token: 0x06000419 RID: 1049
		public extern float nearClipPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600041A RID: 1050
		// (set) Token: 0x0600041B RID: 1051
		public extern float farClipPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600041C RID: 1052
		// (set) Token: 0x0600041D RID: 1053
		public extern float shadowDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600041E RID: 1054
		// (set) Token: 0x0600041F RID: 1055
		public extern int resolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000420 RID: 1056
		// (set) Token: 0x06000421 RID: 1057
		public extern int cullingMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000422 RID: 1058
		// (set) Token: 0x06000423 RID: 1059
		public extern ReflectionProbeClearFlags clearFlags { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000046B8 File Offset: 0x000028B8
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x000046D0 File Offset: 0x000028D0
		public Color backgroundColor
		{
			get
			{
				Color result;
				this.INTERNAL_get_backgroundColor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x06000426 RID: 1062
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x06000427 RID: 1063
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000428 RID: 1064
		// (set) Token: 0x06000429 RID: 1065
		public extern float intensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600042A RID: 1066
		// (set) Token: 0x0600042B RID: 1067
		public extern float blendDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042C RID: 1068
		// (set) Token: 0x0600042D RID: 1069
		public extern bool boxProjection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000046DC File Offset: 0x000028DC
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x0600042F RID: 1071
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000430 RID: 1072
		// (set) Token: 0x06000431 RID: 1073
		public extern ReflectionProbeMode mode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000432 RID: 1074
		// (set) Token: 0x06000433 RID: 1075
		public extern int importance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000434 RID: 1076
		// (set) Token: 0x06000435 RID: 1077
		public extern ReflectionProbeRefreshMode refreshMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000436 RID: 1078
		// (set) Token: 0x06000437 RID: 1079
		public extern ReflectionProbeTimeSlicingMode timeSlicingMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000438 RID: 1080
		// (set) Token: 0x06000439 RID: 1081
		public extern Texture bakedTexture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600043A RID: 1082
		// (set) Token: 0x0600043B RID: 1083
		public extern Texture customBakedTexture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600043C RID: 1084
		public extern Texture texture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600043D RID: 1085
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int RenderProbe([DefaultValue("null")] RenderTexture targetTexture);

		// Token: 0x0600043E RID: 1086 RVA: 0x000046F4 File Offset: 0x000028F4
		[ExcludeFromDocs]
		public int RenderProbe()
		{
			RenderTexture targetTexture = null;
			return this.RenderProbe(targetTexture);
		}

		// Token: 0x0600043F RID: 1087
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsFinishedRendering(int renderId);

		// Token: 0x06000440 RID: 1088
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool BlendCubemap(Texture src, Texture dst, float blend, RenderTexture target);
	}
}
