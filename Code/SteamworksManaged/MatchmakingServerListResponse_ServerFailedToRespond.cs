using System;
using System.Runtime.InteropServices;

// Token: 0x02000119 RID: 281
// (Invoke) Token: 0x06000813 RID: 2067
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void MatchmakingServerListResponse_ServerFailedToRespond(uint sender, uint request, int server);
