using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000045 RID: 69
	public sealed class Texture2D : Texture
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00003FB0 File Offset: 0x000021B0
		public Texture2D(int width, int height)
		{
			Texture2D.Internal_Create(this, width, height, TextureFormat.ARGB32, true, false, IntPtr.Zero);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00003FD4 File Offset: 0x000021D4
		public Texture2D(int width, int height, TextureFormat format, bool mipmap)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, false, IntPtr.Zero);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00003FF8 File Offset: 0x000021F8
		public Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, linear, IntPtr.Zero);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00004020 File Offset: 0x00002220
		internal Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, linear, nativeTex);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600036E RID: 878
		public extern int mipmapCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600036F RID: 879
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Texture2D mono, int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex);

		// Token: 0x06000370 RID: 880 RVA: 0x00004044 File Offset: 0x00002244
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			return new Texture2D(width, height, format, mipmap, linear, nativeTex);
		}

		// Token: 0x06000371 RID: 881
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000372 RID: 882
		public extern TextureFormat format { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000373 RID: 883
		public static extern Texture2D whiteTexture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000374 RID: 884
		public static extern Texture2D blackTexture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000375 RID: 885 RVA: 0x00004054 File Offset: 0x00002254
		public void SetPixel(int x, int y, Color color)
		{
			Texture2D.INTERNAL_CALL_SetPixel(this, x, y, ref color);
		}

		// Token: 0x06000376 RID: 886
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPixel(Texture2D self, int x, int y, ref Color color);

		// Token: 0x06000377 RID: 887 RVA: 0x00004060 File Offset: 0x00002260
		public Color GetPixel(int x, int y)
		{
			Color result;
			Texture2D.INTERNAL_CALL_GetPixel(this, x, y, out result);
			return result;
		}

		// Token: 0x06000378 RID: 888
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPixel(Texture2D self, int x, int y, out Color value);

		// Token: 0x06000379 RID: 889 RVA: 0x00004078 File Offset: 0x00002278
		public Color GetPixelBilinear(float u, float v)
		{
			Color result;
			Texture2D.INTERNAL_CALL_GetPixelBilinear(this, u, v, out result);
			return result;
		}

		// Token: 0x0600037A RID: 890
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPixelBilinear(Texture2D self, float u, float v, out Color value);

		// Token: 0x0600037B RID: 891 RVA: 0x00004090 File Offset: 0x00002290
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int miplevel = 0;
			this.SetPixels(colors, miplevel);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000040A8 File Offset: 0x000022A8
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x0600037D RID: 893
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x0600037E RID: 894 RVA: 0x000040EC File Offset: 0x000022EC
		[ExcludeFromDocs]
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			int miplevel = 0;
			this.SetPixels(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x0600037F RID: 895
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAllPixels32(Color32[] colors, int miplevel);

		// Token: 0x06000380 RID: 896
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel);

		// Token: 0x06000381 RID: 897 RVA: 0x0000410C File Offset: 0x0000230C
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			int miplevel = 0;
			this.SetPixels32(colors, miplevel);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00004124 File Offset: 0x00002324
		public void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00004130 File Offset: 0x00002330
		[ExcludeFromDocs]
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			int miplevel = 0;
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00004150 File Offset: 0x00002350
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x06000385 RID: 901
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool LoadImage(byte[] data, [DefaultValue("false")] bool markNonReadable);

		// Token: 0x06000386 RID: 902 RVA: 0x00004164 File Offset: 0x00002364
		[ExcludeFromDocs]
		public bool LoadImage(byte[] data)
		{
			bool markNonReadable = false;
			return this.LoadImage(data, markNonReadable);
		}

		// Token: 0x06000387 RID: 903
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void LoadRawTextureData_ImplArray(byte[] data);

		// Token: 0x06000388 RID: 904
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void LoadRawTextureData_ImplPointer(IntPtr data, int size);

		// Token: 0x06000389 RID: 905 RVA: 0x0000417C File Offset: 0x0000237C
		public void LoadRawTextureData(byte[] data)
		{
			this.LoadRawTextureData_ImplArray(data);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00004188 File Offset: 0x00002388
		public void LoadRawTextureData(IntPtr data, int size)
		{
			this.LoadRawTextureData_ImplPointer(data, size);
		}

		// Token: 0x0600038B RID: 907
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] GetRawTextureData();

		// Token: 0x0600038C RID: 908 RVA: 0x00004194 File Offset: 0x00002394
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			int miplevel = 0;
			return this.GetPixels(miplevel);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000041AC File Offset: 0x000023AC
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x0600038E RID: 910
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x0600038F RID: 911 RVA: 0x000041F0 File Offset: 0x000023F0
		[ExcludeFromDocs]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			int miplevel = 0;
			return this.GetPixels(x, y, blockWidth, blockHeight, miplevel);
		}

		// Token: 0x06000390 RID: 912
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

		// Token: 0x06000391 RID: 913 RVA: 0x0000420C File Offset: 0x0000240C
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			int miplevel = 0;
			return this.GetPixels32(miplevel);
		}

		// Token: 0x06000392 RID: 914
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x06000393 RID: 915 RVA: 0x00004224 File Offset: 0x00002424
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			bool makeNoLongerReadable = false;
			this.Apply(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000423C File Offset: 0x0000243C
		[ExcludeFromDocs]
		public void Apply()
		{
			bool makeNoLongerReadable = false;
			bool updateMipmaps = true;
			this.Apply(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000395 RID: 917
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Resize(int width, int height, TextureFormat format, bool hasMipMap);

		// Token: 0x06000396 RID: 918 RVA: 0x00004258 File Offset: 0x00002458
		public bool Resize(int width, int height)
		{
			return this.Internal_ResizeWH(width, height);
		}

		// Token: 0x06000397 RID: 919
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_ResizeWH(int width, int height);

		// Token: 0x06000398 RID: 920 RVA: 0x00004264 File Offset: 0x00002464
		public void Compress(bool highQuality)
		{
			Texture2D.INTERNAL_CALL_Compress(this, highQuality);
		}

		// Token: 0x06000399 RID: 921
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Compress(Texture2D self, bool highQuality);

		// Token: 0x0600039A RID: 922
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, [DefaultValue("2048")] int maximumAtlasSize, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x0600039B RID: 923 RVA: 0x00004270 File Offset: 0x00002470
		[ExcludeFromDocs]
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			bool makeNoLongerReadable = false;
			return this.PackTextures(textures, padding, maximumAtlasSize, makeNoLongerReadable);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000428C File Offset: 0x0000248C
		[ExcludeFromDocs]
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			bool makeNoLongerReadable = false;
			int maximumAtlasSize = 2048;
			return this.PackTextures(textures, padding, maximumAtlasSize, makeNoLongerReadable);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000042AC File Offset: 0x000024AC
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			Texture2D.INTERNAL_CALL_ReadPixels(this, ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000042BC File Offset: 0x000024BC
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			bool recalculateMipMaps = true;
			Texture2D.INTERNAL_CALL_ReadPixels(this, ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x0600039F RID: 927
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ReadPixels(Texture2D self, ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x060003A0 RID: 928
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] EncodeToPNG();

		// Token: 0x060003A1 RID: 929
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] EncodeToJPG(int quality);

		// Token: 0x060003A2 RID: 930 RVA: 0x000042D8 File Offset: 0x000024D8
		public byte[] EncodeToJPG()
		{
			return this.EncodeToJPG(75);
		}
	}
}
