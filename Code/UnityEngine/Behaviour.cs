using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B0 RID: 176
	public class Behaviour : Component
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A07 RID: 2567
		// (set) Token: 0x06000A08 RID: 2568
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A09 RID: 2569
		public extern bool isActiveAndEnabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
