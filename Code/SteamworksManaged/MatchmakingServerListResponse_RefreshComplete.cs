using System;
using System.Runtime.InteropServices;

// Token: 0x0200011A RID: 282
// (Invoke) Token: 0x06000817 RID: 2071
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void MatchmakingServerListResponse_RefreshComplete(uint sender, uint request, int response);
