using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000075 RID: 117
	public sealed class Ping
	{
		// Token: 0x060006E1 RID: 1761
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Ping(string address);

		// Token: 0x060006E2 RID: 1762
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DestroyPing();

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000A574 File Offset: 0x00008774
		~Ping()
		{
			this.DestroyPing();
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006E4 RID: 1764
		public extern bool isDone { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006E5 RID: 1765
		public extern int time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006E6 RID: 1766
		public extern string ip { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0400014F RID: 335
		private IntPtr pingWrapper;
	}
}
