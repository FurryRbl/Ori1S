using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	internal struct RenderBufferHelper
	{
		// Token: 0x06000259 RID: 601
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLoadAction(out RenderBuffer b);

		// Token: 0x0600025A RID: 602
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLoadAction(out RenderBuffer b, int a);

		// Token: 0x0600025B RID: 603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetStoreAction(out RenderBuffer b);

		// Token: 0x0600025C RID: 604
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetStoreAction(out RenderBuffer b, int a);
	}
}
