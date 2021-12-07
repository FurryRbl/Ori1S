using System;
using System.Runtime.InteropServices;

// Token: 0x0200011B RID: 283
// (Invoke) Token: 0x0600081B RID: 2075
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void MatchmakingPingResponse_ServerRespondedCallback(uint sender, IntPtr server, int serverSize);
