using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D7 RID: 215
	public sealed class BillboardAsset : Object
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E03 RID: 3587
		// (set) Token: 0x06000E04 RID: 3588
		public extern float width { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E05 RID: 3589
		// (set) Token: 0x06000E06 RID: 3590
		public extern float height { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E07 RID: 3591
		// (set) Token: 0x06000E08 RID: 3592
		public extern float bottom { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E09 RID: 3593
		public extern int imageCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E0A RID: 3594
		public extern int vertexCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E0B RID: 3595
		public extern int indexCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E0C RID: 3596
		// (set) Token: 0x06000E0D RID: 3597
		public extern Material material { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E0E RID: 3598
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void MakeRenderMesh(Mesh mesh, float widthScale, float heightScale, float rotation);

		// Token: 0x06000E0F RID: 3599
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void MakeMaterialProperties(MaterialPropertyBlock properties, Camera camera);

		// Token: 0x06000E10 RID: 3600
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void MakePreviewMesh(Mesh mesh);
	}
}
