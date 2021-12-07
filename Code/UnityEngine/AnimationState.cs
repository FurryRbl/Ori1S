using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A4 RID: 420
	[UsedByNativeCode]
	public sealed class AnimationState : TrackedReference
	{
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060019CA RID: 6602
		// (set) Token: 0x060019CB RID: 6603
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060019CC RID: 6604
		// (set) Token: 0x060019CD RID: 6605
		public extern float weight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060019CE RID: 6606
		// (set) Token: 0x060019CF RID: 6607
		public extern WrapMode wrapMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060019D0 RID: 6608
		// (set) Token: 0x060019D1 RID: 6609
		public extern float time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060019D2 RID: 6610
		// (set) Token: 0x060019D3 RID: 6611
		public extern float normalizedTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060019D4 RID: 6612
		// (set) Token: 0x060019D5 RID: 6613
		public extern float speed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060019D6 RID: 6614
		// (set) Token: 0x060019D7 RID: 6615
		public extern float normalizedSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060019D8 RID: 6616
		public extern float length { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060019D9 RID: 6617
		// (set) Token: 0x060019DA RID: 6618
		public extern int layer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060019DB RID: 6619
		public extern AnimationClip clip { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060019DC RID: 6620
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddMixingTransform(Transform mix, [DefaultValue("true")] bool recursive);

		// Token: 0x060019DD RID: 6621 RVA: 0x0001901C File Offset: 0x0001721C
		[ExcludeFromDocs]
		public void AddMixingTransform(Transform mix)
		{
			bool recursive = true;
			this.AddMixingTransform(mix, recursive);
		}

		// Token: 0x060019DE RID: 6622
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveMixingTransform(Transform mix);

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060019DF RID: 6623
		// (set) Token: 0x060019E0 RID: 6624
		public extern string name { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060019E1 RID: 6625
		// (set) Token: 0x060019E2 RID: 6626
		public extern AnimationBlendMode blendMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
