using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	public sealed class LightmapSettings : Object
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002BD RID: 701
		// (set) Token: 0x060002BE RID: 702
		public static extern LightmapData[] lightmaps { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002BF RID: 703
		// (set) Token: 0x060002C0 RID: 704
		[Obsolete("Use lightmapsMode property")]
		public static extern LightmapsModeLegacy lightmapsModeLegacy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002C1 RID: 705
		// (set) Token: 0x060002C2 RID: 706
		public static extern LightmapsMode lightmapsMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00003B7C File Offset: 0x00001D7C
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00003B84 File Offset: 0x00001D84
		[Obsolete("bakedColorSpace is no longer valid. Use QualitySettings.desiredColorSpace.", false)]
		public static ColorSpace bakedColorSpace
		{
			get
			{
				return QualitySettings.desiredColorSpace;
			}
			set
			{
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002C5 RID: 709
		// (set) Token: 0x060002C6 RID: 710
		public static extern LightProbes lightProbes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060002C7 RID: 711
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();
	}
}
