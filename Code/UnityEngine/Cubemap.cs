using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000046 RID: 70
	public sealed class Cubemap : Texture
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000042E4 File Offset: 0x000024E4
		public Cubemap(int size, TextureFormat format, bool mipmap)
		{
			Cubemap.Internal_Create(this, size, format, mipmap);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000042F8 File Offset: 0x000024F8
		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			Cubemap.INTERNAL_CALL_SetPixel(this, face, x, y, ref color);
		}

		// Token: 0x060003A5 RID: 933
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPixel(Cubemap self, CubemapFace face, int x, int y, ref Color color);

		// Token: 0x060003A6 RID: 934 RVA: 0x00004308 File Offset: 0x00002508
		public Color GetPixel(CubemapFace face, int x, int y)
		{
			Color result;
			Cubemap.INTERNAL_CALL_GetPixel(this, face, x, y, out result);
			return result;
		}

		// Token: 0x060003A7 RID: 935
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPixel(Cubemap self, CubemapFace face, int x, int y, out Color value);

		// Token: 0x060003A8 RID: 936
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, [DefaultValue("0")] int miplevel);

		// Token: 0x060003A9 RID: 937 RVA: 0x00004324 File Offset: 0x00002524
		[ExcludeFromDocs]
		public Color[] GetPixels(CubemapFace face)
		{
			int miplevel = 0;
			return this.GetPixels(face, miplevel);
		}

		// Token: 0x060003AA RID: 938
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, CubemapFace face, [DefaultValue("0")] int miplevel);

		// Token: 0x060003AB RID: 939 RVA: 0x0000433C File Offset: 0x0000253C
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors, CubemapFace face)
		{
			int miplevel = 0;
			this.SetPixels(colors, face, miplevel);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003AC RID: 940
		public extern int mipmapCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003AD RID: 941
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x060003AE RID: 942 RVA: 0x00004354 File Offset: 0x00002554
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			bool makeNoLongerReadable = false;
			this.Apply(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000436C File Offset: 0x0000256C
		[ExcludeFromDocs]
		public void Apply()
		{
			bool makeNoLongerReadable = false;
			bool updateMipmaps = true;
			this.Apply(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B0 RID: 944
		public extern TextureFormat format { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003B1 RID: 945
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Cubemap mono, int size, TextureFormat format, bool mipmap);

		// Token: 0x060003B2 RID: 946
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

		// Token: 0x060003B3 RID: 947 RVA: 0x00004388 File Offset: 0x00002588
		[ExcludeFromDocs]
		public void SmoothEdges()
		{
			int smoothRegionWidthInPixels = 1;
			this.SmoothEdges(smoothRegionWidthInPixels);
		}
	}
}
