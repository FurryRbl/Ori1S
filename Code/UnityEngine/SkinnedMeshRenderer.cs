using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B2 RID: 434
		// (set) Token: 0x060001B3 RID: 435
		public extern Transform[] bones { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B4 RID: 436
		// (set) Token: 0x060001B5 RID: 437
		public extern Transform rootBone { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B6 RID: 438
		// (set) Token: 0x060001B7 RID: 439
		public extern SkinQuality quality { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B8 RID: 440
		// (set) Token: 0x060001B9 RID: 441
		public extern Mesh sharedMesh { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BA RID: 442
		// (set) Token: 0x060001BB RID: 443
		public extern bool updateWhenOffscreen { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00002F48 File Offset: 0x00001148
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00002F60 File Offset: 0x00001160
		public Bounds localBounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_localBounds(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x060001BE RID: 446
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060001BF RID: 447
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x060001C0 RID: 448
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh(Mesh mesh);

		// Token: 0x060001C1 RID: 449
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x060001C2 RID: 450
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBlendShapeWeight(int index, float value);
	}
}
