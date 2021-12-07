using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000049 RID: 73
	[UsedByNativeCode]
	public sealed class RenderTexture : Texture
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00004478 File Offset: 0x00002678
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = format;
			bool sRGB = readWrite == RenderTextureReadWrite.sRGB;
			if (readWrite == RenderTextureReadWrite.Default)
			{
				sRGB = (QualitySettings.activeColorSpace == ColorSpace.Linear);
			}
			RenderTexture.Internal_SetSRGBReadWrite(this, sRGB);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000044CC File Offset: 0x000026CC
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = format;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00004510 File Offset: 0x00002710
		public RenderTexture(int width, int height, int depth)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = RenderTextureFormat.Default;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x060003CE RID: 974
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateRenderTexture([Writable] RenderTexture rt);

		// Token: 0x060003CF RID: 975
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing);

		// Token: 0x060003D0 RID: 976 RVA: 0x00004554 File Offset: 0x00002754
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			int antiAliasing = 1;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00004570 File Offset: 0x00002770
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			int antiAliasing = 1;
			RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000458C File Offset: 0x0000278C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			int antiAliasing = 1;
			RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat format = RenderTextureFormat.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000045AC File Offset: 0x000027AC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			int antiAliasing = 1;
			RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat format = RenderTextureFormat.Default;
			int depthBuffer = 0;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x060003D4 RID: 980
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x060003D5 RID: 981
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth(RenderTexture mono);

		// Token: 0x060003D6 RID: 982
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetWidth(RenderTexture mono, int width);

		// Token: 0x060003D7 RID: 983
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight(RenderTexture mono);

		// Token: 0x060003D8 RID: 984
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHeight(RenderTexture mono, int width);

		// Token: 0x060003D9 RID: 985
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSRGBReadWrite(RenderTexture mono, bool sRGB);

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000045CC File Offset: 0x000027CC
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000045D4 File Offset: 0x000027D4
		public override int width
		{
			get
			{
				return RenderTexture.Internal_GetWidth(this);
			}
			set
			{
				RenderTexture.Internal_SetWidth(this, value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000045E0 File Offset: 0x000027E0
		// (set) Token: 0x060003DD RID: 989 RVA: 0x000045E8 File Offset: 0x000027E8
		public override int height
		{
			get
			{
				return RenderTexture.Internal_GetHeight(this);
			}
			set
			{
				RenderTexture.Internal_SetHeight(this, value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003DE RID: 990
		// (set) Token: 0x060003DF RID: 991
		public extern int depth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003E0 RID: 992
		// (set) Token: 0x060003E1 RID: 993
		public extern bool isPowerOfTwo { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003E2 RID: 994
		public extern bool sRGB { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003E3 RID: 995
		// (set) Token: 0x060003E4 RID: 996
		public extern RenderTextureFormat format { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003E5 RID: 997
		// (set) Token: 0x060003E6 RID: 998
		public extern bool useMipMap { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003E7 RID: 999
		// (set) Token: 0x060003E8 RID: 1000
		public extern bool generateMips { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003E9 RID: 1001
		// (set) Token: 0x060003EA RID: 1002
		public extern bool isCubemap { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003EB RID: 1003
		// (set) Token: 0x060003EC RID: 1004
		public extern bool isVolume { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003ED RID: 1005
		// (set) Token: 0x060003EE RID: 1006
		public extern int volumeDepth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003EF RID: 1007
		// (set) Token: 0x060003F0 RID: 1008
		public extern int antiAliasing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003F1 RID: 1009
		// (set) Token: 0x060003F2 RID: 1010
		public extern bool enableRandomWrite { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003F3 RID: 1011 RVA: 0x000045F4 File Offset: 0x000027F4
		public bool Create()
		{
			return RenderTexture.INTERNAL_CALL_Create(this);
		}

		// Token: 0x060003F4 RID: 1012
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Create(RenderTexture self);

		// Token: 0x060003F5 RID: 1013 RVA: 0x000045FC File Offset: 0x000027FC
		public void Release()
		{
			RenderTexture.INTERNAL_CALL_Release(this);
		}

		// Token: 0x060003F6 RID: 1014
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Release(RenderTexture self);

		// Token: 0x060003F7 RID: 1015 RVA: 0x00004604 File Offset: 0x00002804
		public bool IsCreated()
		{
			return RenderTexture.INTERNAL_CALL_IsCreated(this);
		}

		// Token: 0x060003F8 RID: 1016
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_IsCreated(RenderTexture self);

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000460C File Offset: 0x0000280C
		public void DiscardContents()
		{
			RenderTexture.INTERNAL_CALL_DiscardContents(this);
		}

		// Token: 0x060003FA RID: 1018
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DiscardContents(RenderTexture self);

		// Token: 0x060003FB RID: 1019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DiscardContents(bool discardColor, bool discardDepth);

		// Token: 0x060003FC RID: 1020 RVA: 0x00004614 File Offset: 0x00002814
		public void MarkRestoreExpected()
		{
			RenderTexture.INTERNAL_CALL_MarkRestoreExpected(this);
		}

		// Token: 0x060003FD RID: 1021
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MarkRestoreExpected(RenderTexture self);

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000461C File Offset: 0x0000281C
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer result;
				this.GetColorBuffer(out result);
				return result;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00004634 File Offset: 0x00002834
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer result;
				this.GetDepthBuffer(out result);
				return result;
			}
		}

		// Token: 0x06000400 RID: 1024
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorBuffer(out RenderBuffer res);

		// Token: 0x06000401 RID: 1025
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDepthBuffer(out RenderBuffer res);

		// Token: 0x06000402 RID: 1026
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGlobalShaderProperty(string propertyName);

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000403 RID: 1027
		// (set) Token: 0x06000404 RID: 1028
		public static extern RenderTexture active { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000405 RID: 1029
		// (set) Token: 0x06000406 RID: 1030
		[Obsolete("Use SystemInfo.supportsRenderTextures instead.")]
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000407 RID: 1031
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTexelOffset(RenderTexture tex, out Vector2 output);

		// Token: 0x06000408 RID: 1032 RVA: 0x0000464C File Offset: 0x0000284C
		public Vector2 GetTexelOffset()
		{
			Vector2 result;
			RenderTexture.Internal_GetTexelOffset(this, out result);
			return result;
		}

		// Token: 0x06000409 RID: 1033
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsStencil(RenderTexture rt);

		// Token: 0x0600040A RID: 1034 RVA: 0x00004664 File Offset: 0x00002864
		[Obsolete("SetBorderColor is no longer supported.", true)]
		public void SetBorderColor(Color color)
		{
		}
	}
}
