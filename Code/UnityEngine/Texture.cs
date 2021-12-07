using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000044 RID: 68
	public class Texture : Object
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000354 RID: 852
		public static extern int masterTextureLimit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000355 RID: 853
		// (set) Token: 0x06000356 RID: 854
		public static extern AnisotropicFiltering anisotropicFiltering { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000357 RID: 855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		// Token: 0x06000358 RID: 856
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth(Texture mono);

		// Token: 0x06000359 RID: 857
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight(Texture mono);

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00003F70 File Offset: 0x00002170
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00003F78 File Offset: 0x00002178
		public virtual int width
		{
			get
			{
				return Texture.Internal_GetWidth(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00003F84 File Offset: 0x00002184
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00003F8C File Offset: 0x0000218C
		public virtual int height
		{
			get
			{
				return Texture.Internal_GetHeight(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600035E RID: 862
		// (set) Token: 0x0600035F RID: 863
		public extern FilterMode filterMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000360 RID: 864
		// (set) Token: 0x06000361 RID: 865
		public extern int anisoLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000362 RID: 866
		// (set) Token: 0x06000363 RID: 867
		public extern TextureWrapMode wrapMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000364 RID: 868
		// (set) Token: 0x06000365 RID: 869
		public extern float mipMapBias { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00003F98 File Offset: 0x00002198
		public Vector2 texelSize
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_texelSize(out result);
				return result;
			}
		}

		// Token: 0x06000367 RID: 871
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_texelSize(out Vector2 value);

		// Token: 0x06000368 RID: 872
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeTexturePtr();

		// Token: 0x06000369 RID: 873
		[WrapperlessIcall]
		[Obsolete("Use GetNativeTexturePtr instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetNativeTextureID();
	}
}
