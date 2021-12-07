using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	public sealed class Projector : Behaviour
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000209 RID: 521
		// (set) Token: 0x0600020A RID: 522
		public extern float nearClipPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020B RID: 523
		// (set) Token: 0x0600020C RID: 524
		public extern float farClipPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020D RID: 525
		// (set) Token: 0x0600020E RID: 526
		public extern float fieldOfView { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600020F RID: 527
		// (set) Token: 0x06000210 RID: 528
		public extern float aspectRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000211 RID: 529
		// (set) Token: 0x06000212 RID: 530
		public extern bool orthographic { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000213 RID: 531
		// (set) Token: 0x06000214 RID: 532
		public extern float orthographicSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000215 RID: 533
		// (set) Token: 0x06000216 RID: 534
		public extern int ignoreLayers { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000217 RID: 535
		// (set) Token: 0x06000218 RID: 536
		public extern Material material { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
