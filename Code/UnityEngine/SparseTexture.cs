using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000048 RID: 72
	public sealed class SparseTexture : Texture
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x00004438 File Offset: 0x00002638
		public SparseTexture(int width, int height, TextureFormat format, int mipCount)
		{
			SparseTexture.Internal_Create(this, width, height, format, mipCount, false);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00004458 File Offset: 0x00002658
		public SparseTexture(int width, int height, TextureFormat format, int mipCount, bool linear)
		{
			SparseTexture.Internal_Create(this, width, height, format, mipCount, linear);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C4 RID: 964
		public extern int tileWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003C5 RID: 965
		public extern int tileHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003C6 RID: 966
		public extern bool isCreated { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003C7 RID: 967
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, TextureFormat format, int mipCount, bool linear);

		// Token: 0x060003C8 RID: 968
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data);

		// Token: 0x060003C9 RID: 969
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data);

		// Token: 0x060003CA RID: 970
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UnloadTile(int tileX, int tileY, int miplevel);
	}
}
