using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	public sealed class SystemInfo
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003B RID: 59
		public static extern string operatingSystem { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60
		public static extern string processorType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61
		public static extern int processorFrequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62
		public static extern int processorCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63
		public static extern int systemMemorySize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64
		public static extern int graphicsMemorySize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65
		public static extern string graphicsDeviceName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66
		public static extern string graphicsDeviceVendor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67
		public static extern int graphicsDeviceID { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000044 RID: 68
		public static extern int graphicsDeviceVendorID { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69
		public static extern GraphicsDeviceType graphicsDeviceType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70
		public static extern string graphicsDeviceVersion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71
		public static extern int graphicsShaderLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000245C File Offset: 0x0000065C
		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002460 File Offset: 0x00000660
		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74
		public static extern bool graphicsMultiThreaded { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75
		public static extern bool supportsShadows { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004C RID: 76
		public static extern bool supportsRawShadowDepthSampling { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77
		public static extern bool supportsRenderTextures { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78
		public static extern bool supportsRenderToCubemap { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79
		public static extern bool supportsImageEffects { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80
		public static extern bool supports3DTextures { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81
		public static extern bool supportsComputeShaders { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82
		public static extern bool supportsInstancing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83
		public static extern bool supportsSparseTextures { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84
		public static extern int supportedRenderTargetCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85
		public static extern int supportsStencil { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000056 RID: 86
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsRenderTextureFormat(RenderTextureFormat format);

		// Token: 0x06000057 RID: 87
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsTextureFormat(TextureFormat format);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88
		public static extern NPOTSupport npotSupport { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89
		public static extern string deviceUniqueIdentifier { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90
		public static extern string deviceName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005B RID: 91
		public static extern string deviceModel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005C RID: 92
		public static extern bool supportsAccelerometer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005D RID: 93
		public static extern bool supportsGyroscope { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005E RID: 94
		public static extern bool supportsLocationService { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005F RID: 95
		public static extern bool supportsVibration { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000060 RID: 96
		public static extern DeviceType deviceType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000061 RID: 97
		public static extern int maxTextureSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
