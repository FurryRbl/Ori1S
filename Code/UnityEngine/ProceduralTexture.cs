using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000099 RID: 153
	public sealed class ProceduralTexture : Texture
	{
		// Token: 0x060008F4 RID: 2292
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProceduralOutputType GetProceduralOutputType();

		// Token: 0x060008F5 RID: 2293
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ProceduralMaterial GetProceduralMaterial();

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008F6 RID: 2294
		public extern bool hasAlpha { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060008F7 RID: 2295
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasBeenGenerated();

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008F8 RID: 2296
		public extern TextureFormat format { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060008F9 RID: 2297
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(int x, int y, int blockWidth, int blockHeight);
	}
}
