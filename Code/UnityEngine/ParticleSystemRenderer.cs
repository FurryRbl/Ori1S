using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010B RID: 267
	public sealed class ParticleSystemRenderer : Renderer
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600110B RID: 4363
		// (set) Token: 0x0600110C RID: 4364
		public extern ParticleSystemRenderMode renderMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600110D RID: 4365
		// (set) Token: 0x0600110E RID: 4366
		public extern float lengthScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600110F RID: 4367
		// (set) Token: 0x06001110 RID: 4368
		public extern float velocityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001111 RID: 4369
		// (set) Token: 0x06001112 RID: 4370
		public extern float cameraVelocityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001113 RID: 4371
		// (set) Token: 0x06001114 RID: 4372
		public extern float normalDirection { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001115 RID: 4373
		// (set) Token: 0x06001116 RID: 4374
		public extern ParticleSystemRenderSpace alignment { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00014090 File Offset: 0x00012290
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x000140A8 File Offset: 0x000122A8
		public Vector3 pivot
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_pivot(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_pivot(ref value);
			}
		}

		// Token: 0x06001119 RID: 4377
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pivot(out Vector3 value);

		// Token: 0x0600111A RID: 4378
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pivot(ref Vector3 value);

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600111B RID: 4379
		// (set) Token: 0x0600111C RID: 4380
		public extern ParticleSystemSortMode sortMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600111D RID: 4381
		// (set) Token: 0x0600111E RID: 4382
		public extern float sortingFudge { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600111F RID: 4383
		// (set) Token: 0x06001120 RID: 4384
		public extern float minParticleSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001121 RID: 4385
		// (set) Token: 0x06001122 RID: 4386
		public extern float maxParticleSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001123 RID: 4387
		// (set) Token: 0x06001124 RID: 4388
		public extern Mesh mesh { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
