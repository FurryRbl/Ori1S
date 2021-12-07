using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	public sealed class RenderSettings : Object
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CC RID: 204
		// (set) Token: 0x060000CD RID: 205
		public static extern bool fog { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CE RID: 206
		// (set) Token: 0x060000CF RID: 207
		public static extern FogMode fogMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002914 File Offset: 0x00000B14
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000292C File Offset: 0x00000B2C
		public static Color fogColor
		{
			get
			{
				Color result;
				RenderSettings.INTERNAL_get_fogColor(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_fogColor(ref value);
			}
		}

		// Token: 0x060000D2 RID: 210
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_fogColor(out Color value);

		// Token: 0x060000D3 RID: 211
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_fogColor(ref Color value);

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		public static extern float fogDensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D6 RID: 214
		// (set) Token: 0x060000D7 RID: 215
		public static extern float fogStartDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D8 RID: 216
		// (set) Token: 0x060000D9 RID: 217
		public static extern float fogEndDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DA RID: 218
		// (set) Token: 0x060000DB RID: 219
		public static extern AmbientMode ambientMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002938 File Offset: 0x00000B38
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00002950 File Offset: 0x00000B50
		public static Color ambientSkyColor
		{
			get
			{
				Color result;
				RenderSettings.INTERNAL_get_ambientSkyColor(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientSkyColor(ref value);
			}
		}

		// Token: 0x060000DE RID: 222
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_ambientSkyColor(out Color value);

		// Token: 0x060000DF RID: 223
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_ambientSkyColor(ref Color value);

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000295C File Offset: 0x00000B5C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002974 File Offset: 0x00000B74
		public static Color ambientEquatorColor
		{
			get
			{
				Color result;
				RenderSettings.INTERNAL_get_ambientEquatorColor(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientEquatorColor(ref value);
			}
		}

		// Token: 0x060000E2 RID: 226
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_ambientEquatorColor(out Color value);

		// Token: 0x060000E3 RID: 227
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_ambientEquatorColor(ref Color value);

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002980 File Offset: 0x00000B80
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002998 File Offset: 0x00000B98
		public static Color ambientGroundColor
		{
			get
			{
				Color result;
				RenderSettings.INTERNAL_get_ambientGroundColor(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientGroundColor(ref value);
			}
		}

		// Token: 0x060000E6 RID: 230
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_ambientGroundColor(out Color value);

		// Token: 0x060000E7 RID: 231
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_ambientGroundColor(ref Color value);

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000029A4 File Offset: 0x00000BA4
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000029BC File Offset: 0x00000BBC
		public static Color ambientLight
		{
			get
			{
				Color result;
				RenderSettings.INTERNAL_get_ambientLight(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientLight(ref value);
			}
		}

		// Token: 0x060000EA RID: 234
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_ambientLight(out Color value);

		// Token: 0x060000EB RID: 235
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_ambientLight(ref Color value);

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EC RID: 236
		// (set) Token: 0x060000ED RID: 237
		public static extern float ambientIntensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000029C8 File Offset: 0x00000BC8
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static SphericalHarmonicsL2 ambientProbe
		{
			get
			{
				SphericalHarmonicsL2 result;
				RenderSettings.INTERNAL_get_ambientProbe(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientProbe(ref value);
			}
		}

		// Token: 0x060000F0 RID: 240
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_ambientProbe(out SphericalHarmonicsL2 value);

		// Token: 0x060000F1 RID: 241
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_ambientProbe(ref SphericalHarmonicsL2 value);

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		public static extern float reflectionIntensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F4 RID: 244
		// (set) Token: 0x060000F5 RID: 245
		public static extern int reflectionBounces { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F6 RID: 246
		// (set) Token: 0x060000F7 RID: 247
		public static extern float haloStrength { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F8 RID: 248
		// (set) Token: 0x060000F9 RID: 249
		public static extern float flareStrength { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FA RID: 250
		// (set) Token: 0x060000FB RID: 251
		public static extern float flareFadeSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FC RID: 252
		// (set) Token: 0x060000FD RID: 253
		public static extern Material skybox { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FE RID: 254
		// (set) Token: 0x060000FF RID: 255
		public static extern DefaultReflectionMode defaultReflectionMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000100 RID: 256
		// (set) Token: 0x06000101 RID: 257
		public static extern int defaultReflectionResolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000102 RID: 258
		// (set) Token: 0x06000103 RID: 259
		public static extern Cubemap customReflection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000104 RID: 260
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x06000105 RID: 261
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object GetRenderSettings();
	}
}
