using System;
using System.Runtime.InteropServices;

// Token: 0x0200011D RID: 285
// (Invoke) Token: 0x06000823 RID: 2083
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void MatchmakingPlayersResponse_AddPlayerToList(uint sender, IntPtr name, int score, float timePlayed);
