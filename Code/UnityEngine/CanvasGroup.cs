using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E7 RID: 487
	public sealed class CanvasGroup : Component, ICanvasRaycastFilter
	{
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001D75 RID: 7541
		// (set) Token: 0x06001D76 RID: 7542
		public extern float alpha { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001D77 RID: 7543
		// (set) Token: 0x06001D78 RID: 7544
		public extern bool interactable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001D79 RID: 7545
		// (set) Token: 0x06001D7A RID: 7546
		public extern bool blocksRaycasts { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001D7B RID: 7547
		// (set) Token: 0x06001D7C RID: 7548
		public extern bool ignoreParentGroups { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D7D RID: 7549 RVA: 0x0001BBDC File Offset: 0x00019DDC
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.blocksRaycasts;
		}
	}
}
