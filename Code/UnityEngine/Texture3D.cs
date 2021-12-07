using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000047 RID: 71
	public sealed class Texture3D : Texture
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x000043A0 File Offset: 0x000025A0
		public Texture3D(int width, int height, int depth, TextureFormat format, bool mipmap)
		{
			Texture3D.Internal_Create(this, width, height, depth, format, mipmap);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003B5 RID: 949
		public extern int depth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003B6 RID: 950
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels([DefaultValue("0")] int miplevel);

		// Token: 0x060003B7 RID: 951 RVA: 0x000043C0 File Offset: 0x000025C0
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			int miplevel = 0;
			return this.GetPixels(miplevel);
		}

		// Token: 0x060003B8 RID: 952
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

		// Token: 0x060003B9 RID: 953 RVA: 0x000043D8 File Offset: 0x000025D8
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			int miplevel = 0;
			return this.GetPixels32(miplevel);
		}

		// Token: 0x060003BA RID: 954
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060003BB RID: 955 RVA: 0x000043F0 File Offset: 0x000025F0
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int miplevel = 0;
			this.SetPixels(colors, miplevel);
		}

		// Token: 0x060003BC RID: 956
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060003BD RID: 957 RVA: 0x00004408 File Offset: 0x00002608
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			int miplevel = 0;
			this.SetPixels32(colors, miplevel);
		}

		// Token: 0x060003BE RID: 958
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps);

		// Token: 0x060003BF RID: 959 RVA: 0x00004420 File Offset: 0x00002620
		[ExcludeFromDocs]
		public void Apply()
		{
			bool updateMipmaps = true;
			this.Apply(updateMipmaps);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C0 RID: 960
		public extern TextureFormat format { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003C1 RID: 961
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Texture3D mono, int width, int height, int depth, TextureFormat format, bool mipmap);
	}
}
