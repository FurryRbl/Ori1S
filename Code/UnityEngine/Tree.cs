using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001D6 RID: 470
	public sealed class Tree : Component
	{
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001CAC RID: 7340
		// (set) Token: 0x06001CAD RID: 7341
		public extern ScriptableObject data { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001CAE RID: 7342
		public extern bool hasSpeedTreeWind { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
