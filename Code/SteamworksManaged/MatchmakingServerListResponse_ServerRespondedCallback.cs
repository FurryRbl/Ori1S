using System;
using System.Runtime.InteropServices;

// Token: 0x02000118 RID: 280
// (Invoke) Token: 0x0600080F RID: 2063
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void MatchmakingServerListResponse_ServerRespondedCallback(uint sender, uint request, int server);
